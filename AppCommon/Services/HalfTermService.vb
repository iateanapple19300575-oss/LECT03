Public Class HalfTermService
    Public Function GetHalfTerms() As List(Of HalfTermModel)
        Return New List(Of HalfTermModel) From {
            New HalfTermModel With {.Value = HalfTermType.FirstHalf, .DisplayName = "上期"},
            New HalfTermModel With {.Value = HalfTermType.SecondHalf, .DisplayName = "下期"}
        }
    End Function
End Class