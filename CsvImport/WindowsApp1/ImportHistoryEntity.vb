Public Class ImportHistoryEntity
    Public Property Id As Integer
    Public Property ImportType As String
    Public Property TargetTable As String
    Public Property ImportedCount As Integer
    Public Property ErrorCount As Integer
    Public Property Periods As String
    Public Property ImportedAt As DateTime
End Class