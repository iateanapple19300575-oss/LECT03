Imports Entities

Public Interface IImportRepository
    Function BulkInsert(entities As List(Of XxxImportEntity)) As Integer
End Interface