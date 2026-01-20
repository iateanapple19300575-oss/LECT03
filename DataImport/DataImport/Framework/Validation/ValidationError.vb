' ValidationError.vb
Public Class ValidationError
    Public Property RowIndex As Integer
    Public Property FieldName As String
    Public Property Message As String

    Public Sub New(rowIndex As Integer, fieldName As String, message As String)
        Me.RowIndex = rowIndex
        Me.FieldName = fieldName
        Me.Message = message
    End Sub
End Class