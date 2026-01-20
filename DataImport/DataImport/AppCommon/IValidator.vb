' IValidator.vb
Public Interface IValidator(Of T)
    Function Validate(dto As T) As IEnumerable(Of ValidationError)
End Interface