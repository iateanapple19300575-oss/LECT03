' IRowFactory.vb
Public Interface IRowFactory(Of T)
    Function Create(fields As String()) As T
End Interface