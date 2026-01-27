Imports System.Data.SqlClient

Public Class ImportHistoryRecorder

    Private Shared ReadOnly _repo As New ImportHistoryRepository()

    ''' <summary>
    ''' インポート履歴を登録する（トランザクション内で呼び出す）
    ''' </summary>
    Public Shared Sub RecordWithTran(cn As SqlConnection,
                                     tran As SqlTransaction,
                                     importType As String,
                                     tableName As String,
                                     successCount As Integer,
                                     errorCount As Integer,
                                     periods As List(Of ImportPeriod))

        Dim entity As New ImportHistoryEntity With {
            .ImportType = importType,
            .TargetTable = tableName,
            .ImportedCount = successCount,
            .ErrorCount = errorCount,
            .Periods = String.Join(",", periods.Select(Function(p) p.ToString())),
            .ImportedAt = DateTime.Now
        }

        _repo.InsertWithTran(cn, tran, entity)
    End Sub

End Class