Namespace Domain

    ''' <summary>
    ''' ドメインルール違反を表す例外。
    ''' ビジネスロジックの整合性が崩れた場合に使用する。
    ''' </summary>
    Public Class DomainException
        Inherits Exception

        ''' <summary>
        ''' メッセージのみを指定するコンストラクタ。
        ''' </summary>
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        ''' <summary>
        ''' メッセージと内部例外を指定するコンストラクタ。
        ''' </summary>
        Public Sub New(message As String, inner As Exception)
            MyBase.New(message, inner)
        End Sub

    End Class

End Namespace