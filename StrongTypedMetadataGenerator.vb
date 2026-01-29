Imports System
Imports System.Text
Imports System.Collections.Generic

Module Program

    Sub Main()
        ' ★ 実行すると、強い型メタデータを使った SQL 生成が確認できます。

        ' 1. モックのスキーマを用意（実際はDBから取得する想定）
        Dim schema As DatabaseSchema = MockSchema()

        ' 2. メタデータクラスを生成
        Dim generator As New StrongTypedMetadataGenerator()
        Dim code As String = generator.GenerateSchemaClass(schema)

        Console.WriteLine("===== Generated Schema.vb =====")
        Console.WriteLine(code)
        Console.WriteLine("================================")
        Console.WriteLine()

        ' 3. 実際にメタデータを使って SQL を生成してみる
        Dim customer = schema.Tables.Find(Function(t) t.Name = "Customer")
        Dim sql = SqlBuilder.SelectColumns(customer, customer.Columns("Name"), customer.Columns("Address"))

        Console.WriteLine("===== SQL Sample =====")
        Console.WriteLine(sql)
        Console.WriteLine("======================")
        Console.WriteLine()

        Console.WriteLine("Enterキーで終了します。")
        Console.ReadLine()
    End Sub


    ' =========================
    ' モックのスキーマ（実際はDBから取得）
    ' =========================
    Private Function MockSchema() As DatabaseSchema
        Dim schema As New DatabaseSchema()

        Dim customer As New TableSchema("Customer")
        customer.Columns.Add(New ColumnSchema("CustomerId"))
        customer.Columns.Add(New ColumnSchema("Name"))
        customer.Columns.Add(New ColumnSchema("Address"))
        schema.Tables.Add(customer)

        Dim orders As New TableSchema("Orders")
        orders.Columns.Add(New ColumnSchema("OrderId"))
        orders.Columns.Add(New ColumnSchema("CustomerId"))
        orders.Columns.Add(New ColumnSchema("Total"))
        schema.Tables.Add(orders)

        Return schema
    End Function

End Module


' =========================
' スキーマ定義クラス
' =========================

Public Class DatabaseSchema
    Public Property Tables As List(Of TableSchema)

    Public Sub New()
        Me.Tables = New List(Of TableSchema)()
    End Sub
End Class

Public Class TableSchema
    Public Property Name As String
    Public Property Columns As List(Of ColumnSchema)

    Public Sub New(name As String)
        Me.Name = name
        Me.Columns = New List(Of ColumnSchema)()
    End Sub
End Class

Public Class ColumnSchema
    Public Property Name As String

    Public Sub New(name As String)
        Me.Name = name
    End Sub
End Class


' =========================
' 強い型メタデータ生成
' =========================

Public Class StrongTypedMetadataGenerator

    Public Function GenerateSchemaClass(schema As DatabaseSchema) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("' 自動生成コード")
        sb.AppendLine("Public NotInheritable Class Schema")
        sb.AppendLine("    Public Shared ReadOnly Tables As New TablesInfo()")
        sb.AppendLine("End Class")
        sb.AppendLine()

        sb.AppendLine("Public NotInheritable Class TablesInfo")

        For Each tbl In schema.Tables
            sb.AppendLine("    Public ReadOnly " & tbl.Name & " As New " & tbl.Name & "Info()")
        Next

        sb.AppendLine("End Class")
        sb.AppendLine()

        For Each tbl In schema.Tables
            sb.AppendLine("Public NotInheritable Class " & tbl.Name & "Info")
            sb.AppendLine("    Public ReadOnly Name As String = """ & tbl.Name & """")
            sb.AppendLine("    Public ReadOnly Columns As New Dictionary(Of String, ColumnInfo)()")

            sb.AppendLine("    Public Sub New()")

            For Each col In tbl.Columns
                sb.AppendLine("        Columns.Add(""" & col.Name & """, New ColumnInfo(""" & col.Name & """))")
            Next

            sb.AppendLine("    End Sub")
            sb.AppendLine("End Class")
            sb.AppendLine()
        Next

        sb.AppendLine("Public Class ColumnInfo")
        sb.AppendLine("    Public ReadOnly Name As String")
        sb.AppendLine("    Public Sub New(name As String)")
        sb.AppendLine("        Me.Name = name")
        sb.AppendLine("    End Sub")
        sb.AppendLine("End Class")

        Return sb.ToString()
    End Function

End Class


' =========================
' SQL ビルダー（メタデータ対応）
' =========================

Public Class SqlBuilder

    Public Shared Function SelectColumns(table As TableSchema, ParamArray cols() As ColumnSchema) As String
        Dim colList As New List(Of String)()

        For Each c In cols
            colList.Add(c.Name)
        Next

        Return "SELECT " & String.Join(", ", colList.ToArray()) &
               " FROM " & table.Name
    End Function

End Class