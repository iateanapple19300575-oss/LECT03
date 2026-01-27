Public Class DataTableBuilder

    Public Shared Function FromDtoType(dtoType As Type) As DataTable
        Dim dt As New DataTable()

        For Each p In dtoType.GetProperties()
            Dim colType = GetColumnType(p.PropertyType)
            dt.Columns.Add(p.Name, colType)
        Next

        Return dt
    End Function

    Public Shared Sub AddRow(dt As DataTable, dto As Object)
        Dim row = dt.NewRow()

        For Each p In dto.GetType().GetProperties()
            Dim value = p.GetValue(dto, Nothing)

            If value Is Nothing OrElse value.ToString() = "" Then
                row(p.Name) = DBNull.Value
            Else
                row(p.Name) = ConvertValue(value, dt.Columns(p.Name).DataType)
            End If
        Next

        dt.Rows.Add(row)
    End Sub

    Private Shared Function GetColumnType(t As Type) As Type
        If t.IsGenericType AndAlso t.GetGenericTypeDefinition() Is GetType(Nullable(Of )) Then
            Return Nullable.GetUnderlyingType(t)
        End If
        Return t
    End Function

    Private Shared Function ConvertValue(value As Object, targetType As Type) As Object
        If value Is Nothing OrElse value.ToString() = "" Then
            Return DBNull.Value
        End If

        If targetType Is GetType(Integer) Then
            Return Integer.Parse(value.ToString())
        ElseIf targetType Is GetType(Decimal) Then
            Return Decimal.Parse(value.ToString())
        ElseIf targetType Is GetType(DateTime) Then
            Return DateTime.Parse(value.ToString())
        ElseIf targetType Is GetType(Boolean) Then
            Return Boolean.Parse(value.ToString())
        End If

        Return value
    End Function

End Class