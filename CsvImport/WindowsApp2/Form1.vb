Public Class Form1
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        cmbCsvType.SelectedIndex = 1


        Dim importer = ImporterFactory.Create(cmbCsvType.SelectedItem.ToString(), txtFilePath.Text)
        Dim result = importer.Execute()

        Dim msg = String.Format("成功件数: {0}{1}エラー件数: {2}",
                                result.SuccessCount,
                                Environment.NewLine,
                                result.ErrorCount)

        If Not String.IsNullOrEmpty(result.ErrorMessage) Then
            msg &= Environment.NewLine & "エラー: " & result.ErrorMessage
        End If

        MessageBox.Show(msg)
    End Sub

End Class
