' ImportResult.vb
Public Class ImportResult
    Public Property IsSuccess As Boolean
    Public Property Errors As List(Of ValidationError)
    Public Property Count As Integer
    Public Property ProcessedAt As DateTime

    Private Sub New()
    End Sub

    Public Shared Function Success(count As Integer) As ImportResult
        Return New ImportResult With {
            .IsSuccess = True,
            .Count = count,
            .ProcessedAt = DateTime.Now,
            .Errors = New List(Of ValidationError)()
        }
    End Function

    Public Shared Function Fail(errors As IEnumerable(Of ValidationError)) As ImportResult
        Return New ImportResult With {
            .IsSuccess = False,
            .Errors = errors.ToList(),
            .Count = 0,
            .ProcessedAt = DateTime.Now
        }
    End Function

    Public ReadOnly Property HasError As Boolean
        Get
            Return Not IsSuccess
        End Get
    End Property
End Class