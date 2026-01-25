Imports Framework
Imports Mappers

Namespace Repositories

    ''' <summary>
    ''' Repository の共通処理を提供する基底クラス。
    ''' </summary>
    Public MustInherit Class BaseRepository(Of T As New)
        Implements IRepository(Of T)

        Protected MustOverride ReadOnly Property Def As TableDefinition

        Private Function FindAllForDataTable() As DataTable _
            Implements IRepository(Of T).FindAllForDataTable
            Dim sql As String = QueryBuilder.BuildSelect(Def)
            Return SqlExecutor.QueryDataTable(
                sql,
                Nothing
            )
        End Function

        Public Function GetAllForDataTable() As DataTable _
            Implements IRepository(Of T).GetAllForDataTable
            Dim sql As String = QueryBuilder.BuildSelect(Def)
            Return SqlExecutor.QueryDataTable(
                sql,
                Nothing
            )
        End Function

        Public Function GetAll() As List(Of T)
            Dim sql As String = QueryBuilder.BuildSelect(Def)
            Return SqlExecutor.Query(
                sql,
                Function(r) Mapper.Map(Of T)(r)
            )
        End Function

        Public Overridable Function FindAll() As List(Of T) _
            Implements IRepository(Of T).FindAll

            Dim sql = $"SELECT * FROM {Def.TableName}"

            Return SqlExecutor.Query(
                sql,
                Function(r) Mapper.Map(Of T)(r)
            )
        End Function

        Public Overridable Function FindById(id As Object) As T _
            Implements IRepository(Of T).FindById

            Dim sql = $"SELECT * FROM {Def.TableName} WHERE {Def.KeyColumns(0)} = @{Def.KeyColumns(0)}"

            Return SqlExecutor.QuerySingle(
                sql,
                Function(r) Mapper.Map(Of T)(r),
                New With {.KeyColumn = id}
            )
        End Function

        Public Overridable Function UpdateById(id As Object) As Integer _
            Implements IRepository(Of T).UpdateById

            Dim sql = $"UPDATE {Def.TableName} SET {Def.KeyColumns(0)} = @{Def.KeyColumns(0)}"

            Return SqlExecutor.Execute(
                sql,
                New With {.KeyColumn = id}
            )
        End Function

        Public Overridable Function DeleteById(id As Object) As Integer _
            Implements IRepository(Of T).DeleteById

            Dim sql = $"DELETE FROM {Def.TableName} WHERE {Def.KeyColumns(0)} = @{Def.KeyColumns(0)}"

            Return SqlExecutor.Execute(
                sql,
                New With {.KeyColumn = id}
            )
        End Function

        Public Overridable Function Insert(entity As T) As Integer _
            Implements IRepository(Of T).Insert
            Dim sql As String = QueryBuilder.BuildInsert(Def)
            Return SqlExecutor.Execute(sql, entity)
        End Function

        Public Overridable Function Update(entity As T) As Integer _
            Implements IRepository(Of T).Update
            Dim sql As String = QueryBuilder.BuildUpdate(Def)
            Return SqlExecutor.Execute(sql, entity)
        End Function

        Public Overridable Function Delete(entity As T) As Integer _
            Implements IRepository(Of T).Delete
            Dim sql As String = QueryBuilder.BuildDelete(Def)
            Return SqlExecutor.Execute(sql, entity)
        End Function
    End Class

End Namespace