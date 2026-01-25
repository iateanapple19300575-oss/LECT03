Public Class BasicResult
    Public Property IsSuccess As Boolean
    Public Property Errors As List(Of ImportError)

    Public Shared Function Success() As BasicResult
        Return New BasicResult With {
            .IsSuccess = True,
            .Errors = New List(Of ImportError)
        }
    End Function

    Public Shared Function Fail(errors As List(Of ImportError)) As BasicResult
        Return New BasicResult With {
            .IsSuccess = False,
            .Errors = errors
        }
    End Function
End Class

Public Class ImportError
    Public Property Message As String
    Public Property ErrorType As ImportErrorType
    Public Property Severity As SeverityLevel
    Public Property RowNumber As Integer?
    Public Property ColumnName As String
End Class

Public Enum SeverityLevel
    Info
    Warning
    [Error]
End Enum

Public Enum ImportErrorType
    None
    Validation
    Business
    System
End Enum