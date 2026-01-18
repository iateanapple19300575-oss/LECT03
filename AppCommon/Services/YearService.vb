Public Class YearService
    Public Function GetYears() As List(Of YearModel)
        Dim list As New List(Of YearModel)

        For i As Integer = DateTime.Now.Year - 10 To DateTime.Now.Year + 1
            list.Add(New YearModel With {
                .Value = i,
                .DisplayName = $"{i}年"
            })
        Next

        Return list
    End Function
End Class