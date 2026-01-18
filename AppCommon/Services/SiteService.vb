Public Class SiteService
    Public Function GetSites() As List(Of SiteModel)
        Return New List(Of SiteModel) From {
            New SiteModel With {.Value = 0, .DisplayName = "(未選択)"},
            New SiteModel With {.Value = 1, .DisplayName = "春期"},
            New SiteModel With {.Value = 2, .DisplayName = "夏期"},
            New SiteModel With {.Value = 4, .DisplayName = "冬期"}
        }
    End Function
End Class
