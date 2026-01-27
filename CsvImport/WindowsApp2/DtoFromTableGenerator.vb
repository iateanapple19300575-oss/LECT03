Imports System.Data.SqlClient

Public Class DtoFromTableGenerator

    Public Shared Function Generate(tableName As String, className As String) As String
        Dim dt = GetTableSchema(tableName)
        Dim sb As New System.Text.StringBuilder()

        sb.AppendLine("Public Class " & className)

        For Each row As DataRow In dt.Rows
            Dim colName = row("COLUMN_NAME").ToString()
            Dim dataType = MapSqlTypeToVb(row("DATA_TYPE").ToString())
            sb.AppendLine($"    Public Property {colName} As {dataType}")
        Next

        sb.AppendLine("End Class")

        Return sb.ToString()
    End Function

    Private Shared Function GetTableSchema(tableName As String) As DataTable
        Dim sql = "
SELECT COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @TableName
ORDER BY ORDINAL_POSITION
"
        Dim dt As New DataTable()

        Using cn As New SqlConnection("connection string")
            cn.Open()
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@TableName", tableName)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Private Shared Function MapSqlTypeToVb(sqlType As String) As String
        Select Case sqlType.ToLower()
            Case "int" : Return "Integer"
            Case "bigint" : Return "Long"
            Case "decimal", "numeric" : Return "Decimal"
            Case "float", "real" : Return "Double"
            Case "datetime", "smalldatetime" : Return "DateTime"
            Case "bit" : Return "Boolean"
            Case Else : Return "String"
        End Select
    End Function

End Class