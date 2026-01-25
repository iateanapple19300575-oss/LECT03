Public Interface IImportService
    Function Execute(request As CsvImportRequest) As CsvImportResult
End Interface