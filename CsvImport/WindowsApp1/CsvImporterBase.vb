Public MustInherit Class CsvImporterBase

    Private ReadOnly _filePath As String
    Private ReadOnly _repo As New ImportRepository()

    Protected Sub New(filePath As String)
        _filePath = filePath
    End Sub

    Public Function Execute() As ImportResult
        Dim result As New ImportResult()
        Dim dt As DataTable = Nothing
        Dim dtos As New List(Of Object)()
        Dim rowIndex As Integer = 0

        Try
            Dim rows = LoadCsv()
            dt = CreateDataTable()

            For Each row As DataRow In rows.Rows
                rowIndex += 1
                Dim dto As Object = Nothing
                Try
                    dto = MapRow(row)
                    ValidateCommon(dto)
                    ValidateSpecific(dto)
                    dtos.Add(dto)
                    AddToDataTable(dt, dto)
                    result.SuccessCount += 1
                Catch ex As Exception
                    LogError(rowIndex, dto, ex)
                    result.ErrorCount += 1
                End Try
            Next

            Dim periods = ResolveDeletePeriods(dtos)
            For Each p In periods
                _repo.Delete(TargetTableName, p)
            Next

            BulkCopyExecutor.Execute(TargetTableName, dt)

            ImportHistoryRecorder.Record(
                ImportTypeName,
                TargetTableName,
                result.SuccessCount,
                result.ErrorCount,
                periods
            )

        Catch ex As Exception
            result.ErrorMessage = ex.Message
        End Try

        Return result
    End Function

    Protected Overridable Function LoadCsv() As DataTable
        Return CsvParser.Load(_filePath)
    End Function

    Protected Overridable Sub ValidateCommon(dto As Object)
        ' 必須チェックなど共通があればここに
    End Sub

    Protected Overridable Function CreateDataTable() As DataTable
        Return DataTableBuilder.FromDtoType(GetDtoType())
    End Function

    Protected Overridable Sub AddToDataTable(dt As DataTable, dto As Object)
        DataTableBuilder.AddRow(dt, dto)
    End Sub

    Protected Sub LogError(rowIndex As Integer, dto As Object, ex As Exception)
        Dim dtoStr As String = If(dto Is Nothing, "(null)",
            String.Join(",",
                dto.GetType().GetProperties().
                    Select(Function(p) $"{p.Name}={p.GetValue(dto, Nothing)}")))

        Dim log = $"{DateTime.Now} 行:{rowIndex} DTO:{dtoStr} Error:{ex.Message}"
        System.IO.File.AppendAllText("import_error.log", log & Environment.NewLine)
    End Sub

    Protected MustOverride Function MapRow(row As DataRow) As Object
    Protected MustOverride Sub ValidateSpecific(dto As Object)
    Protected MustOverride Function ResolveDeletePeriods(dtos As List(Of Object)) As List(Of ImportPeriod)
    Protected MustOverride ReadOnly Property TargetTableName As String
    Protected MustOverride ReadOnly Property ImportTypeName As String
    Protected MustOverride Function GetDtoType() As Type

End Class