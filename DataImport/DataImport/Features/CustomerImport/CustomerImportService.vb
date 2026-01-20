' CustomerImportService.vb
Public Class CustomerImportService
    Inherits CsvImportTemplate(Of CustomerDto)

    Public Sub New(detector As IEncodingDetector, reader As ICsvReader)
        MyBase.New(detector, reader)
    End Sub

    Protected Overrides Function GetRowFactory() As IRowFactory(Of CustomerDto)
        Return New CustomerRowFactory()
    End Function

    Protected Overrides Function GetValidators() As IEnumerable(Of IValidator(Of CustomerDto))
        Return {New CustomerValidator()}
    End Function

    Protected Overrides Function GetRepository() As IImportRepository(Of CustomerDto)
        Return New CustomerRepository()
    End Function
End Class