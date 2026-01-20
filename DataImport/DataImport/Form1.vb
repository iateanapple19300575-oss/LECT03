Public Class Form1
    Private Sub btnImportCustomer_Click(sender As Object, e As EventArgs) Handles btnImportCustomer.Click
        Dim result = ServiceLocator.ImportController.ImportCustomer(txtPath.Text)
        ShowResult(result)
    End Sub

    Private Sub btnImportProduct_Click(sender As Object, e As EventArgs) Handles btnImportProduct.Click
        Dim result = ServiceLocator.ImportController.ImportProduct(txtPath.Text)
        ShowResult(result)
    End Sub

    Private Sub ShowResult(result As ImportResult)
        If result.HasError Then
            dgvError.DataSource = result.Errors
        Else
            lblCount.Text = result.Count.ToString()
            lblDate.Text = result.ProcessedAt.ToString("yyyy/MM/dd HH:mm:ss")
        End If
    End Sub


    'Private Sub btnImportCustomer_Click(sender As Object, e As EventArgs) Handles btnImportCustomer.Click
    '    Dim service As New CustomerImportService(New SimpleEncodingDetector(), New CsvReader())
    '    Dim result = service.Execute(txtPath.Text)

    '    If result.HasError Then
    '        dgvError.DataSource = result.Errors
    '    Else
    '        lblCount.Text = result.Count.ToString()
    '        lblDate.Text = result.ProcessedAt.ToString("yyyy/MM/dd HH:mm:ss")
    '    End If
    'End Sub

    'Private Sub btnImportProduct_Click(sender As Object, e As EventArgs) Handles btnImportProduct.Click
    '    Dim service As New ProductImportService(New SimpleEncodingDetector(), New CsvReader())
    '    Dim result = service.Execute(txtPath.Text)

    '    If result.HasError Then
    '        dgvError.DataSource = result.Errors
    '    Else
    '        lblCount.Text = result.Count.ToString()
    '        lblDate.Text = result.ProcessedAt.ToString("yyyy/MM/dd HH:mm:ss")
    '    End If
    'End Sub


End Class
