' AppCommon/ImportController.vb
Public Class ImportController

    Private ReadOnly _customerService As CustomerImportService
    Private ReadOnly _productService As ProductImportService

    Public Sub New()
        Dim detector = New SimpleEncodingDetector()
        Dim reader = New CsvReader()

        _customerService = New CustomerImportService(detector, reader)
        _productService = New ProductImportService(detector, reader)
    End Sub

    Public Function ImportCustomer(path As String) As ImportResult
        Return _customerService.Execute(path)
    End Function

    Public Function ImportProduct(path As String) As ImportResult
        Return _productService.Execute(path)
    End Function

End Class