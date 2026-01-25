''' <summary>
''' 業務エラーを表すアプリケーション例外クラス。
''' システム例外とは区別し、ユーザーに通知可能なメッセージを保持します。
''' </summary>
''' <remarks>
''' 業務ロジック上のエラー（入力不備、整合性エラーなど）を通知するために使用します。<br/>
''' 例外メッセージはそのまま画面やログに表示されることを想定しています。
''' </remarks>
Public Class AppException
    Inherits Exception

    ''' <summary>
    ''' 指定したメッセージを持つ AppException を生成します。
    ''' </summary>
    ''' <param name="msg">業務エラー内容を表すメッセージ。</param>
    Public Sub New(msg As String)
        MyBase.New(msg)
    End Sub

End Class