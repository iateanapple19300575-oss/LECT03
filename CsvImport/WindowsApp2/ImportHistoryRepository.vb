Imports System.Data.SqlClient

Public Class ImportHistoryRepository

    Public Sub InsertWithTran(cn As SqlConnection,
                              tran As SqlTransaction,
                              entity As ImportHistoryEntity)

        Dim sql = "
INSERT INTO ImportHistory
(ImportType, TargetTable, ImportedCount, ErrorCount, Periods, ImportedAt)
VALUES (@ImportType, @TargetTable, @ImportedCount, @ErrorCount, @Periods, @ImportedAt)
"

        Using cmd As New SqlCommand(sql, cn, tran)
            cmd.Parameters.AddWithValue("@ImportType", entity.ImportType)
            cmd.Parameters.AddWithValue("@TargetTable", entity.TargetTable)
            cmd.Parameters.AddWithValue("@ImportedCount", entity.ImportedCount)
            cmd.Parameters.AddWithValue("@ErrorCount", entity.ErrorCount)
            cmd.Parameters.AddWithValue("@Periods", entity.Periods)
            cmd.Parameters.AddWithValue("@ImportedAt", entity.ImportedAt)
            cmd.ExecuteNonQuery()
        End Using

    End Sub

End Class