Public Class TransactionDataCsvImport
    Inherits CsvImportTemplate

    Public Sub New(csvReader As ICsvReader, validator As IValidator)
        MyBase.New(csvReader, validator)
    End Sub

    Protected Overrides Sub Validate(entities As Object)
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Function ConvertToEntity(lines As List(Of String())) As Object
        Throw New NotImplementedException()
    End Function

    Protected Overrides Function BulkCopyImport(entities As Object) As Integer
        Throw New NotImplementedException()
    End Function
End Class
