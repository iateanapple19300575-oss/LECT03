Namespace Application.User

    ''' <summary>
    ''' ユーザー情報を画面 ⇔ Application ⇔ Domain 間で受け渡すための DTO。
    ''' </summary>
    Public Class XXXDto

        ''' <summary>
        ''' ユーザーID（新規登録時は 0 または Nothing）
        ''' </summary>
        Public Property User_Id As String

        ''' <summary>
        ''' ユーザー名
        ''' </summary>
        Public Property User_Name As String

        ''' <summary>
        ''' ユーザー名
        ''' </summary>
        Public Property User_Address As String

        ''' <summary>
        ''' ユーザー名
        ''' </summary>
        Public Property User_TelNo As String

        ''' <summary>
        ''' 年齢
        ''' </summary>
        Public Property Age As Integer

    End Class

End Namespace
