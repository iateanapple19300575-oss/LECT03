' ProductImportService.vb
Public Class ProductImportService
    Inherits CsvImportTemplate(Of ProductDto)

    Public Sub New(detector As IEncodingDetector, reader As ICsvReader)
        MyBase.New(detector, reader)
    End Sub

    Protected Overrides Function GetRowFactory() As IRowFactory(Of ProductDto)
        Return New ProductRowFactory()
    End Function

    Protected Overrides Function GetValidators() As IEnumerable(Of IValidator(Of ProductDto))
        Return {New ProductValidator()}
    End Function

    Protected Overrides Function GetRepository() As IImportRepository(Of ProductDto)
        Return New ProductRepository()
    End Function
End Class