Imports System.Data.SqlClient

Public Class ImportRepository

    Public Sub Delete(tableName As String, period As ImportPeriod)
        Dim sql As String

        If period.IsAll Then
            sql = $"DELETE FROM {tableName}"
        ElseIf period.Month.HasValue Then
            sql = $"DELETE FROM {tableName} WHERE Year = @Year AND Month = @Month"
        Else
            sql = $"DELETE FROM {tableName} WHERE Year = @Year"
        End If

        Using cn As New SqlConnection("connection string")
            cn.Open()
            Using cmd As New SqlCommand(sql, cn)
                If period.Year.HasValue Then cmd.Parameters.AddWithValue("@Year", period.Year.Value)
                If period.Month.HasValue Then cmd.Parameters.AddWithValue("@Month", period.Month.Value)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class