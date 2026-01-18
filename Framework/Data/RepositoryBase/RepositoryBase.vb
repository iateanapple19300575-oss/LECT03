Imports System.Data.SqlClient
Imports Framework.Core

Namespace Framework.Data

    ''' <summary>
    ''' すべてのリポジトリの基底クラス。
    ''' CRUD の共通処理と、Entity ↔ DataRow / DataReader の変換ロジックを統一するための抽象クラス。
    ''' 派生クラスはテーブル名・主キー名・マッピング処理を実装する。
    ''' </summary>
    ''' <typeparam name="TEntity">対象となるエンティティ型。</typeparam>
    Public MustInherit Class RepositoryBase(Of TEntity As Class)

        ''' <summary>
        ''' データベース接続文字列。
        ''' </summary>
        Protected ReadOnly _connectionString As String

        ''' <summary>
        ''' 接続文字列を指定してリポジトリを初期化する。
        ''' </summary>
        ''' <param name="connectionString">DB 接続文字列。</param>
        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        ' ============================
        ' 抽象メンバ（派生クラスが実装）
        ' ============================

        ''' <summary>
        ''' 対象テーブル名。
        ''' </summary>
        Protected MustOverride ReadOnly Property TableName As String

        ''' <summary>
        ''' 主キー列名。
        ''' </summary>
        Protected MustOverride ReadOnly Property PrimaryKey As String

        ''' <summary>
        ''' DataRow → Entity のマッピング処理。
        ''' </summary>
        ''' <param name="row">データ行。</param>
        ''' <returns>マッピングされたエンティティ。</returns>
        Protected MustOverride Function MapRow(row As DataRow) As TEntity

        ''' <summary>
        ''' Entity → SqlParameter の変換処理（INSERT/UPDATE 用）。
        ''' </summary>
        ''' <param name="entity">エンティティ。</param>
        ''' <returns>SQL パラメータ一覧。</returns>
        Protected MustOverride Function MapParameters(entity As TEntity) As List(Of SqlParameter)

        ' ============================
        ' SELECT（1件）
        ' ============================

        ''' <summary>
        ''' 主キーを指定して 1 件取得する。
        ''' </summary>
        ''' <param name="id">主キー値。</param>
        ''' <returns>該当エンティティ。存在しない場合は Nothing。</returns>
        Public Function GetById(id As Object) As TEntity
            Try
                Dim sql As String = String.Format(
                    "SELECT * FROM {0} WHERE {1} = @Id",
                    TableName, PrimaryKey
                )

                Dim params = New List(Of SqlParameter) From {
                    New SqlParameter("@Id", id)
                }

                Using exec As New SqlExecutor(_connectionString)
                    Dim dt As DataTable = exec.ExecuteDataTable(sql, params)

                    If dt.Rows.Count = 0 Then
                        Return Nothing
                    End If

                    Return MapRow(dt.Rows(0))
                End Using

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "GetById"))
            End Try
        End Function

        ' ============================
        ' SELECT（全件）
        ' ============================

        ''' <summary>
        ''' テーブルの全件を取得する。
        ''' </summary>
        ''' <returns>エンティティのリスト。</returns>
        Public Function GetAll() As List(Of TEntity)
            Try
                Dim sql As String = String.Format("SELECT * FROM {0}", TableName)

                Using exec As New SqlExecutor(_connectionString)
                    Dim dt As DataTable = exec.ExecuteDataTable(sql)

                    Dim list As New List(Of TEntity)
                    For Each row As DataRow In dt.Rows
                        list.Add(MapRow(row))
                    Next

                    Return list
                End Using

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "GetAll"))
            End Try
        End Function

        ' ============================
        ' INSERT
        ' ============================

        ''' <summary>
        ''' エンティティを INSERT する。
        ''' </summary>
        ''' <param name="entity">挿入対象のエンティティ。</param>
        Public Sub Insert(entity As TEntity)
            Try
                Dim params As List(Of SqlParameter) = MapParameters(entity)

                Dim columns As String = String.Join(",", params.ConvertAll(Function(p) p.ParameterName.TrimStart("@"c)).ToArray())
                Dim values As String = String.Join(",", params.ConvertAll(Function(p) p.ParameterName).ToArray())

                Dim sql As String = String.Format(
                    "INSERT INTO {0} ({1}) VALUES ({2})",
                    TableName, columns, values
                )

                Using exec As New SqlExecutor(_connectionString)
                    exec.ExecuteNonQuery(sql, params)
                End Using

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "Insert"))
            End Try
        End Sub

        ' ============================
        ' UPDATE
        ' ============================

        ''' <summary>
        ''' 主キーを指定してエンティティを UPDATE する。
        ''' </summary>
        ''' <param name="entity">更新内容を持つエンティティ。</param>
        ''' <param name="id">更新対象の主キー値。</param>
        Public Sub Update(entity As TEntity, id As Object)
            Try
                Dim params As List(Of SqlParameter) = MapParameters(entity)

                Dim setClause = String.Join(","c,
                    params.Select(Function(p) $"{p.ParameterName.TrimStart("@"c)}={p.ParameterName}").ToArray()
                )

                Dim sql As String = String.Format(
                    "UPDATE {0} SET {1} WHERE {2} = @Id",
                    TableName, setClause, PrimaryKey
                )

                params.Add(New SqlParameter("@Id", id))

                Using exec As New SqlExecutor(_connectionString)
                    exec.ExecuteNonQuery(sql, params)
                End Using

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "Update"))
            End Try
        End Sub

        ' ============================
        ' DELETE
        ' ============================

        ''' <summary>
        ''' 主キーを指定して 1 件削除する。
        ''' </summary>
        ''' <param name="id">削除対象の主キー値。</param>
        Public Sub Delete(id As Object)
            Try
                Dim sql As String = String.Format(
                    "DELETE FROM {0} WHERE {1} = @Id",
                    TableName, PrimaryKey
                )

                Dim params = New List(Of SqlParameter) From {
                    New SqlParameter("@Id", id)
                }

                Using exec As New SqlExecutor(_connectionString)
                    exec.ExecuteNonQuery(sql, params)
                End Using

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "Delete"))
            End Try
        End Sub

    End Class

End Namespace