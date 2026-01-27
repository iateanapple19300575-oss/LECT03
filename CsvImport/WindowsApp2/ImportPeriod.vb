Public Class ImportPeriod
    Public Property Year As Integer?
    Public Property Month As Integer?
    Public Property IsAll As Boolean

    Public Shared Function ForYear(y As Integer) As ImportPeriod
        Return New ImportPeriod With {.Year = y}
    End Function

    Public Shared Function ForMonth(y As Integer, m As Integer) As ImportPeriod
        Return New ImportPeriod With {.Year = y, .Month = m}
    End Function

    Public Shared Function All() As ImportPeriod
        Return New ImportPeriod With {.IsAll = True}
    End Function

    Public Overrides Function ToString() As String
        If IsAll Then Return "ALL"
        If Month.HasValue Then Return String.Format("{0}{1:00}", Year, Month.Value)
        Return Year.ToString()
    End Function
End Class