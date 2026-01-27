Imports System.Data.SqlClient

Public Class ImportRepository

    Public Sub DeleteWithTran(cn As SqlConnection,
                              tran As SqlTransaction,
                              tableName As String,
                              period As ImportPeriod)

        Dim sql As String

        If period.IsAll Then
            sql = String.Format("DELETE FROM {0}", tableName)
        ElseIf period.Month.HasValue Then
            sql = String.Format("DELETE FROM {0} WHERE Year = @Year AND Month = @Month", tableName)
        Else
            sql = String.Format("DELETE FROM {0} WHERE Year = @Year", tableName)
        End If

        Using cmd As New SqlCommand(sql, cn, tran)
            If period.Year.HasValue Then cmd.Parameters.AddWithValue("@Year", period.Year.Value)
            If period.Month.HasValue Then cmd.Parameters.AddWithValue("@Month", period.Month.Value)
            cmd.ExecuteNonQuery()
        End Using

    End Sub

End Class