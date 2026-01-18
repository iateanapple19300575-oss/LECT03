Imports Framework.Core

Namespace Framework.UI

    ''' <summary>
    ''' フォーム内の UI コントロールに対して、共通的なイベントハンドラを自動付与するクラス。
    ''' <para>
    ''' ・TextBox / CheckBox / ComboBox / Button の主要イベントを一元的にログ化  
    ''' ・フォーム配下のすべてのコントロールを再帰的に探索  
    ''' ・UI 操作ログ（<see cref="AppLogger.UiAction"/>）を統一フォーマットで記録  
    ''' </para>
    ''' 運用監視やユーザー操作トレースを容易にするためのユーティリティ。
    ''' </summary>
    Public NotInheritable Class EventWrapper

        ''' <summary>
        ''' 指定されたフォーム内のすべてのコントロールにイベントハンドラを付与する。
        ''' </summary>
        ''' <param name="form">対象となるフォーム。</param>
        Public Shared Sub AttachHandlers(form As Form)
            For Each ctrl As Control In GetAllControls(form)
                AttachControlEvents(ctrl)
            Next
        End Sub

        ''' <summary>
        ''' 指定されたコントロール配下のすべての子コントロールを再帰的に取得する。
        ''' </summary>
        ''' <param name="parent">親コントロール。</param>
        ''' <returns>親コントロール配下のすべてのコントロール。</returns>
        Private Shared Function GetAllControls(parent As Control) As IEnumerable(Of Control)
            Dim list As New List(Of Control)
            For Each c As Control In parent.Controls
                list.Add(c)
                list.AddRange(GetAllControls(c))
            Next
            Return list
        End Function

        ''' <summary>
        ''' コントロールの種類に応じて適切なイベントハンドラを付与する。
        ''' </summary>
        ''' <param name="ctrl">対象コントロール。</param>
        Private Shared Sub AttachControlEvents(ctrl As Control)

            ' TextBox
            If TypeOf ctrl Is TextBox Then
                AddHandler DirectCast(ctrl, TextBox).TextChanged,
                    Sub(sender, e)
                        AppLogger.UiAction("TextChanged", ctrl.Name, DirectCast(ctrl, TextBox).Text)
                    End Sub

                AddHandler DirectCast(ctrl, TextBox).LostFocus,
                    Sub(sender, e)
                        AppLogger.UiAction("LostFocus", ctrl.Name, DirectCast(ctrl, TextBox).Text)
                    End Sub
            End If

            ' CheckBox
            If TypeOf ctrl Is CheckBox Then
                AddHandler DirectCast(ctrl, CheckBox).CheckedChanged,
                    Sub(sender, e)
                        Dim val = If(DirectCast(ctrl, CheckBox).Checked, "ON", "OFF")
                        AppLogger.UiAction("CheckedChanged", ctrl.Name, val)
                    End Sub
            End If

            ' ComboBox
            If TypeOf ctrl Is ComboBox Then
                AddHandler DirectCast(ctrl, ComboBox).SelectedIndexChanged,
                    Sub(sender, e)
                        AppLogger.UiAction("SelectedIndexChanged", ctrl.Name, DirectCast(ctrl, ComboBox).Text)
                    End Sub
            End If

            ' Button
            If TypeOf ctrl Is Button Then
                AddHandler DirectCast(ctrl, Button).Click,
                    Sub(sender, e)
                        AppLogger.UiAction("Click", ctrl.Name, "")
                    End Sub
            End If

        End Sub

    End Class

End Namespace