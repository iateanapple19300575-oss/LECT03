Namespace Repositories

    ''' <summary>
    ''' Repository の基本インターフェース。
    ''' </summary>
    Public Interface IRepository(Of T)

        Function FindAll() As List(Of T)

        Function FindById(id As Object) As T

        Function GetAllForDataTable() As DataTable

        Function FindAllForDataTable() As DataTable

        Function UpdateById(id As Object) As Integer

        Function DeleteById(id As Object) As Integer

        Function Insert(entity As T) As Integer

        Function Update(entity As T) As Integer

        Function Delete(entity As T) As Integer

    End Interface

End Namespace