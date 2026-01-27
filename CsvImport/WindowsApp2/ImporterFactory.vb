Public Class ImporterFactory

    Public Shared Function Create(type As String, filePath As String) As CsvImporterBase
        Select Case type
            Case "Yearly"
                Return New YearlyCsvImporter(filePath)
            Case "Monthly"
                Return New MonthlyCsvImporter(filePath)
            Case "NoPeriod"
                Return New NoPeriodCsvImporter(filePath)
            Case Else
                Throw New NotSupportedException("未対応の CSV 種類です: " & type)
        End Select
    End Function

End Class