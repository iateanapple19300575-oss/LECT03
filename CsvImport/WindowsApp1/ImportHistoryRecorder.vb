Public Class ImportHistoryRecorder

    Private Shared ReadOnly _repo As New ImportHistoryRepository()

    Public Shared Sub Record(typeName As String,
                             tableName As String,
                             success As Integer,
                             errorCount As Integer,
                             periods As List(Of ImportPeriod))

        Dim entity As New ImportHistoryEntity With {
            .ImportType = typeName,
            .TargetTable = tableName,
            .ImportedCount = success,
            .ErrorCount = errorCount,
            .Periods = String.Join(",", periods.Select(Function(p) p.ToString())),
            .ImportedAt = DateTime.Now
        }

        _repo.Insert(entity)
    End Sub

End Class