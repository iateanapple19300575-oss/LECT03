Public Class Form1
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim importer = ImporterFactory.Create(cmbCsvType.SelectedItem.ToString(), txtFilePath.Text)
        Dim result = importer.Execute()

        Dim msg = $"成功件数: {result.SuccessCount}{Environment.NewLine}" &
              $"エラー件数: {result.ErrorCount}"

        If Not String.IsNullOrEmpty(result.ErrorMessage) Then
            msg &= $"{Environment.NewLine}エラー: {result.ErrorMessage}"
        End If

        MessageBox.Show(msg)
    End Sub


End Class
