' ProductValidator.vb
Public Class ProductValidator
    Implements IValidator(Of ProductDto)

    Public Function Validate(dto As ProductDto) As IEnumerable(Of ValidationError) _
        Implements IValidator(Of ProductDto).Validate

        Dim list As New List(Of ValidationError)

        If dto.Price < 0 Then
            list.Add(New ValidationError(0, "Price", "価格は0以上"))
        End If

        Return list
    End Function
End Class