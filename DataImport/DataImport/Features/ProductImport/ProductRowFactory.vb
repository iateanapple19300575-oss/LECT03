' ProductRowFactory.vb
Public Class ProductRowFactory
    Implements IRowFactory(Of ProductDto)

    Public Function Create(fields As String()) As ProductDto _
        Implements IRowFactory(Of ProductDto).Create

        Return New ProductDto With {
            .ProductId = fields(0),
            .ProductName = fields(1),
            .Price = Integer.Parse(fields(2))
        }
    End Function
End Class