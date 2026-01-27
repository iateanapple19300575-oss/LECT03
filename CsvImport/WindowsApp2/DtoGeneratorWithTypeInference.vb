Public Class DtoGeneratorWithTypeInference

    Public Shared Function GenerateDto(csvPath As String, className As String) As String
        Dim dt = CsvParser.Load(csvPath)
        Dim sb As New System.Text.StringBuilder()

        sb.AppendLine("Public Class " & className)

        For Each col As DataColumn In dt.Columns
            Dim inferredType = InferType(dt, col.ColumnName)
            sb.AppendLine(String.Format("    Public Property {0} As {1}",
                                        NormalizeName(col.ColumnName), inferredType))
        Next

        sb.AppendLine("End Class")

        Return sb.ToString()
    End Function

    Private Shared Function InferType(dt As DataTable, colName As String) As String
        Dim allValues = dt.AsEnumerable().
            Select(Function(r) r(colName).ToString()).
            Where(Function(v) v <> "").
            ToList()

        If allValues.Count = 0 Then Return "String"

        Dim i As Integer
        Dim d As Decimal
        Dim dtm As DateTime

        If allValues.All(Function(v) Integer.TryParse(v, i)) Then
            Return "Integer"
        End If

        If allValues.All(Function(v) Decimal.TryParse(v, d)) Then
            Return "Decimal"
        End If

        If allValues.All(Function(v) DateTime.TryParse(v, dtm)) Then
            Return "DateTime"
        End If

        Return "String"
    End Function

    Private Shared Function NormalizeName(header As String) As String
        Dim name = header.Trim().Replace(" ", "").Replace("-", "_").Replace("/", "_")
        If Char.IsDigit(name(0)) Then name = "_" & name
        Return name
    End Function

End Class