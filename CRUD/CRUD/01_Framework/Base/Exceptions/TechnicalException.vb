Public Class TechnicalException
    Inherits ApplicationException
    Public Sub New(message As String, inner As Exception)
        MyBase.New(message, inner)
    End Sub
End Class