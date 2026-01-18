Imports Framework.Core

Namespace Framework.UI

    ''' <summary>
    ''' 画面遷移を一元管理するクラス。
    ''' <para>
    ''' ・フォームの生成、表示方法、ライフサイクルを統合管理  
    ''' ・Singleton 表示（多重起動防止）と毎回新規表示の両方に対応  
    ''' ・画面遷移（NavigateTo）を簡潔に実装可能  
    ''' </para>
    ''' アプリケーション全体の画面管理を標準化し、可読性と保守性を向上させる。
    ''' </summary>
    Public NotInheritable Class FormNavigator

        ''' <summary>
        ''' Singleton 表示用にフォームインスタンスを保持するキャッシュ。
        ''' </summary>
        Private Shared ReadOnly _formCache As New Dictionary(Of String, Form)

        ' ============================
        ' 画面を開く（Singleton）
        ' ============================

        ''' <summary>
        ''' 指定したフォームを Singleton として開く。
        ''' <para>
        ''' ・既に開いている場合はアクティブ化  
        ''' ・閉じられた場合はキャッシュから削除  
        ''' ・owner が指定されていればモーダル表示  
        ''' </para>
        ''' </summary>
        ''' <typeparam name="T">表示するフォーム型。</typeparam>
        ''' <param name="owner">親フォーム（省略可）。</param>
        Public Shared Sub Open(Of T As {Form, New})(Optional owner As Form = Nothing)
            Dim key As String = GetType(T).FullName

            Try
                Dim frm As Form = Nothing

                ' 既に開いている場合はアクティブ化
                If _formCache.ContainsKey(key) AndAlso Not _formCache(key).IsDisposed Then
                    frm = _formCache(key)
                    frm.WindowState = FormWindowState.Normal
                    frm.Activate()
                    Return
                End If

                ' 新規生成
                frm = New T()
                _formCache(key) = frm

                AddHandler frm.FormClosed,
                    Sub()
                        _formCache.Remove(key)
                    End Sub

                If owner IsNot Nothing Then
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog(owner)
                Else
                    frm.StartPosition = FormStartPosition.CenterScreen
                    frm.Show()
                End If

                AppLogger.Info("Open Form: " & key)

            Catch ex As Exception
                MessageBox.Show(ErrorHandler.Handle(ex, "FormNavigator.Open"))
            End Try
        End Sub

        ' ============================
        ' 画面を開く（毎回新規）
        ' ============================

        ''' <summary>
        ''' 指定したフォームを毎回新規インスタンスとして開く。
        ''' </summary>
        ''' <typeparam name="T">表示するフォーム型。</typeparam>
        ''' <param name="owner">親フォーム（省略可）。</param>
        Public Shared Sub OpenNew(Of T As {Form, New})(Optional owner As Form = Nothing)
            Try
                Dim frm As New T()

                If owner IsNot Nothing Then
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog(owner)
                Else
                    frm.StartPosition = FormStartPosition.CenterScreen
                    frm.Show()
                End If

                AppLogger.Info("OpenNew Form: " & GetType(T).FullName)

            Catch ex As Exception
                MessageBox.Show(ErrorHandler.Handle(ex, "FormNavigator.OpenNew"))
            End Try
        End Sub

        ' ============================
        ' 現在の画面を閉じて別画面へ遷移
        ' ============================

        ''' <summary>
        ''' 現在のフォームを閉じ、指定したフォームへ遷移する。
        ''' <para>
        ''' ・current.Hide → 新画面表示 → current.Close の順で実行  
        ''' ・owner を引き継いで遷移  
        ''' </para>
        ''' </summary>
        ''' <typeparam name="T">遷移先のフォーム型。</typeparam>
        ''' <param name="current">現在表示中のフォーム。</param>
        Public Shared Sub NavigateTo(Of T As {Form, New})(current As Form)
            Try
                current.Hide()
                Open(Of T)(current.Owner)
                current.Close()

            Catch ex As Exception
                MessageBox.Show(ErrorHandler.Handle(ex, "FormNavigator.NavigateTo"))
            End Try
        End Sub

    End Class

End Namespace