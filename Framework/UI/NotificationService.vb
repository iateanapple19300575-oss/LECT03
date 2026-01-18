Imports Framework.Core

Namespace Framework.UI

    ''' <summary>
    ''' ユーザーへの通知処理を一元管理するサービスクラス。
    ''' <para>
    ''' ・MessageBox 表示  
    ''' ・ログ出力（Info / Warn / Error）  
    ''' ・確認ダイアログ  
    ''' ・Validation エラーのまとめ表示  
    ''' </para>
    ''' UI 層での通知処理を統一し、可読性と保守性を向上させる。
    ''' </summary>
    Public NotInheritable Class NotificationService

        ' ============================
        ' 情報通知
        ' ============================

        ''' <summary>
        ''' 情報メッセージを表示する。
        ''' </summary>
        ''' <param name="message">表示するメッセージ。</param>
        ''' <param name="title">ダイアログタイトル（省略時は「情報」）。</param>
        Public Shared Sub Info(message As String, Optional title As String = "情報")
            AppLogger.Info("Notify Info: " & message)
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        ' ============================
        ' 成功通知
        ' ============================

        ''' <summary>
        ''' 成功メッセージを表示する。
        ''' </summary>
        ''' <param name="message">表示するメッセージ。</param>
        ''' <param name="title">ダイアログタイトル（省略時は「完了」）。</param>
        Public Shared Sub Success(message As String, Optional title As String = "完了")
            AppLogger.Info("Notify Success: " & message)
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        ' ============================
        ' 警告通知
        ' ============================

        ''' <summary>
        ''' 警告メッセージを表示する。
        ''' </summary>
        ''' <param name="message">表示するメッセージ。</param>
        ''' <param name="title">ダイアログタイトル（省略時は「警告」）。</param>
        Public Shared Sub Warn(message As String, Optional title As String = "警告")
            AppLogger.Warn("Notify Warn: " & message)
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Sub

        ' ============================
        ' エラー通知
        ' ============================

        ''' <summary>
        ''' エラーメッセージを表示する。
        ''' </summary>
        ''' <param name="message">表示するメッセージ。</param>
        ''' <param name="title">ダイアログタイトル（省略時は「エラー」）。</param>
        Public Shared Sub [Error](message As String, Optional title As String = "エラー")
            AppLogger.Error("Notify Error: " & message)
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

        ' ============================
        ' 確認ダイアログ
        ' ============================

        ''' <summary>
        ''' Yes/No の確認ダイアログを表示する。
        ''' </summary>
        ''' <param name="message">確認メッセージ。</param>
        ''' <param name="title">ダイアログタイトル（省略時は「確認」）。</param>
        ''' <returns>Yes が選択された場合 True。</returns>
        Public Shared Function Confirm(message As String, Optional title As String = "確認") As Boolean
            AppLogger.Info("Notify Confirm: " & message)
            Dim result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Return result = DialogResult.Yes
        End Function

        ' ============================
        ' ValidationException の通知
        ' ============================

        ''' <summary>
        ''' Validation エラー一覧をまとめて表示する。
        ''' </summary>
        ''' <param name="errors">ValidationError の列挙。</param>
        Public Shared Sub ShowValidationErrors(errors As IEnumerable(Of Validation.ValidationError))
            Dim msg As String = String.Join(Environment.NewLine,
                                            errors.Select(Function(e) e.Field & ": " & e.Message).ToArray())

            AppLogger.Warn("Validation Errors: " & msg)
            MessageBox.Show(msg, "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Sub

    End Class

End Namespace