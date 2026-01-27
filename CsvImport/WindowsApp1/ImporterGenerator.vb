Imports System.Data.SqlClient

Public Class ImporterGenerator

    Public Shared Function GenerateImporter(tableName As String,
                                            className As String,
                                            dtoClassName As String,
                                            importTypeName As String) As String

        Dim schema = GetTableSchema(tableName)
        Dim sb As New System.Text.StringBuilder()

        sb.AppendLine($"Public Class {className}")
        sb.AppendLine("    Inherits CsvImporterBase")
        sb.AppendLine()
        sb.AppendLine("    Public Sub New(filePath As String)")
        sb.AppendLine("        MyBase.New(filePath)")
        sb.AppendLine("    End Sub")
        sb.AppendLine()
        sb.AppendLine("    Protected Overrides ReadOnly Property TargetTableName As String")
        sb.AppendLine("        Get")
        sb.AppendLine($"            Return ""{tableName}""")
        sb.AppendLine("        End Get")
        sb.AppendLine("    End Property")
        sb.AppendLine()
        sb.AppendLine("    Protected Overrides ReadOnly Property ImportTypeName As String")
        sb.AppendLine("        Get")
        sb.AppendLine($"            Return ""{importTypeName}""")
        sb.AppendLine("        End Get")
        sb.AppendLine("    End Property")
        sb.AppendLine()
        sb.AppendLine("    Protected Overrides Function GetDtoType() As Type")
        sb.AppendLine($"        Return GetType({dtoClassName})")
        sb.AppendLine("    End Function")
        sb.AppendLine()
        sb.AppendLine("    Protected Overrides Function MapRow(row As DataRow) As Object")
        sb.AppendLine($"        Return New {dtoClassName} With {{")

        For i As Integer = 0 To schema.Rows.Count - 1
            Dim col = schema.Rows(i)
            Dim colName = col("COLUMN_NAME").ToString()
            Dim vbType = MapSqlTypeToVb(col("DATA_TYPE").ToString())
            Dim castFunc = GetCastFunction(vbType)
            Dim comma = If(i = schema.Rows.Count - 1, "", ",")
            sb.AppendLine($"            .{colName} = {castFunc}(row(""{colName}"")){comma}")
        Next

        sb.AppendLine("        }")
        sb.AppendLine("    End Function")
        sb.AppendLine()
        sb.AppendLine("    Protected Overrides Sub ValidateSpecific(dto As Object)")
        sb.AppendLine("        ' TODO: 業務バリデーションを実装")
        sb.AppendLine("    End Sub")
        sb.AppendLine()
        sb.AppendLine("    Protected Overrides Function ResolveDeletePeriods(dtos As List(Of Object)) As List(Of ImportPeriod)")
        sb.AppendLine("        ' TODO: 年単位 / 月単位 / 期間なしなどの期間ロジックを実装")
        sb.AppendLine("        Return New List(Of ImportPeriod) From {ImportPeriod.All()}")
        sb.AppendLine("    End Function")
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

    Private Shared Function GetCastFunction(vbType As String) As String
        Select Case vbType
            Case "Integer" : Return "CInt"
            Case "Long" : Return "CLng"
            Case "Decimal" : Return "CDec"
            Case "Double" : Return "CDbl"
            Case "DateTime" : Return "CDate"
            Case "Boolean" : Return "CBool"
            Case Else : Return "CStr"
        End Select
    End Function

End Class