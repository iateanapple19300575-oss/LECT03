Namespace Framework.UI.Theme

    ''' <summary>
    ''' アプリケーション全体のテーマ（色・フォント）を適用するマネージャークラス。
    ''' <para>
    ''' ・<see cref="CurrentTheme"/> に設定されたテーマをフォームおよび全コントロールへ反映  
    ''' ・フォーム直下のコントロールだけでなく、子コントロールにも再帰的に適用  
    ''' ・コントロールの種類に応じて適切な色・フォントを割り当てる  
    ''' </para>
    ''' UI の統一感を保ち、テーマ変更を一括で反映するための中心的コンポーネント。
    ''' </summary>
    Public NotInheritable Class ThemeManager

        ''' <summary>
        ''' 現在適用されるテーマ。
        ''' アプリケーション起動時にデフォルトテーマが設定される。
        ''' </summary>
        Public Shared Property CurrentTheme As New Theme()

        ''' <summary>
        ''' 指定したフォームにテーマを適用する。
        ''' </summary>
        ''' <param name="form">テーマを適用する対象フォーム。</param>
        Public Shared Sub ApplyTheme(form As Form)
            form.BackColor = CurrentTheme.Colors.FormBackColor
            form.Font = CurrentTheme.Fonts.DefaultFont

            ApplyToControls(form.Controls)
        End Sub

        ''' <summary>
        ''' コントロールコレクションに対してテーマを適用する（再帰的に子コントロールも処理）。
        ''' </summary>
        ''' <param name="controls">テーマを適用するコントロールのコレクション。</param>
        Private Shared Sub ApplyToControls(controls As Control.ControlCollection)
            For Each ctrl As Control In controls

                ' 共通フォント適用
                ctrl.Font = CurrentTheme.Fonts.DefaultFont

                ' コントロール種類別のテーマ適用
                If TypeOf ctrl Is Panel Then
                    ctrl.BackColor = CurrentTheme.Colors.PanelBackColor

                ElseIf TypeOf ctrl Is Label Then
                    ctrl.ForeColor = CurrentTheme.Colors.LabelForeColor

                ElseIf TypeOf ctrl Is TextBox Then
                    ctrl.BackColor = CurrentTheme.Colors.TextBoxBackColor
                    ctrl.ForeColor = CurrentTheme.Colors.TextBoxForeColor

                ElseIf TypeOf ctrl Is Button Then
                    ctrl.BackColor = CurrentTheme.Colors.ButtonBackColor
                    ctrl.ForeColor = CurrentTheme.Colors.ButtonForeColor
                    ctrl.Font = CurrentTheme.Fonts.ButtonFont

                ElseIf TypeOf ctrl Is DataGridView Then
                    Dim grid = DirectCast(ctrl, DataGridView)
                    grid.EnableHeadersVisualStyles = False
                    grid.ColumnHeadersDefaultCellStyle.BackColor = CurrentTheme.Colors.GridHeaderBackColor
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = CurrentTheme.Colors.GridHeaderForeColor
                End If

                ' 子コントロールへ再帰適用
                If ctrl.HasChildren Then
                    ApplyToControls(ctrl.Controls)
                End If
            Next
        End Sub

    End Class

End Namespace