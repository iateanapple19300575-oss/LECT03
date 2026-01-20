' CustomerValidator.vb
Public Class CustomerValidator
    Implements IValidator(Of CustomerDto)

    Public Function Validate(dto As CustomerDto) As IEnumerable(Of ValidationError) _
        Implements IValidator(Of CustomerDto).Validate

        Dim list As New List(Of ValidationError)

        If String.IsNullOrEmpty(dto.CustomerId) Then
            list.Add(New ValidationError(0, "CustomerId", "必須"))
        End If

        If dto.Birth > Date.Today Then
            list.Add(New ValidationError(0, "Birth", "未来日は不可"))
        End If

        Return list
    End Function
End Class