Public Class MappingUtil

    'Public Shared Function ToDto(model As CustomerModel) As CustomerDto
    '    If model Is Nothing Then Return Nothing

    '    Return New CustomerDto With {
    '        .Id = model.Id,
    '        .Name = model.Name,
    '        .BirthDate = model.BirthDate.ToString("yyyy/MM/dd")
    '    }
    'End Function

    'Public Shared Function ToModel(dto As CustomerDto) As CustomerModel
    '    If dto Is Nothing Then Return Nothing

    '    Return New CustomerModel With {
    '        .Id = dto.Id,
    '        .Name = dto.Name,
    '        .BirthDate = DateTime.Parse(dto.BirthDate)
    '    }
    'End Function

    Public Shared Function Map(Of TSource, TDest)(src As TSource) As TDest
        Dim dest As TDest = Activator.CreateInstance(Of TDest)()

        For Each p In GetType(TSource).GetProperties()
            Dim dp = GetType(TDest).GetProperty(p.Name)
            If dp IsNot Nothing AndAlso dp.CanWrite Then
                dp.SetValue(dest, p.GetValue(src, Nothing), Nothing)
            End If
        Next

        Return dest
    End Function
End Class