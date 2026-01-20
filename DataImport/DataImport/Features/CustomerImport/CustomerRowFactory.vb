' CustomerRowFactory.vb
Public Class CustomerRowFactory
    Implements IRowFactory(Of CustomerDto)

    Public Function Create(fields As String()) As CustomerDto _
        Implements IRowFactory(Of CustomerDto).Create

        Return New CustomerDto With {
            .CustomerId = fields(0),
            .Name = fields(1),
            .Birth = Date.Parse(fields(2))
        }
    End Function
End Class