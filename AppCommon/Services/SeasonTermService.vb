Public Class SeasonService
    Public Function GetSeasons() As List(Of SeasonTermModel)
        Return New List(Of SeasonTermModel) From {
            New SeasonTermModel With {.Value = SeasonType.Spring, .DisplayName = "春期"},
            New SeasonTermModel With {.Value = SeasonType.Summer, .DisplayName = "夏期"},
            New SeasonTermModel With {.Value = SeasonType.Winter, .DisplayName = "冬期"}
        }
    End Function
End Class