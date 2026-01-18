Imports System.Data.SqlClient
Imports Framework.Core

Namespace Framework.Data

    ''' <summary>
    ''' SQL Server への接続およびコマンド実行を一元化するクラス。
    ''' トランザクション管理、例外処理、ログ出力を統合し、
    ''' リポジトリ層からの SQL 実行を簡潔かつ安全に行えるようにする。
    ''' </summary>
    Public Class SqlExecutor
        Implements IDisposable

        ''' <summary>
        ''' 内部で使用する SQL Server 接続。
        ''' </summary>
        Private _connection As SqlConnection

        ''' <summary>
        ''' 現在のトランザクション（存在する場合）。
        ''' </summary>
        Private _transaction As SqlTransaction

        ''' <summary>
        ''' Dispose 済みかどうかのフラグ。
        ''' </summary>
        Private _disposed As Boolean = False

        ''' <summary>
        ''' 接続文字列を受け取り、SQL 実行クラスを初期化する。
        ''' </summary>
        ''' <param name="connectionString">SQL Server への接続文字列。</param>
        Public Sub New(connectionString As String)
            _connection = New SqlConnection(connectionString)
        End Sub

        ''' <summary>
        ''' 接続が閉じている場合に自動的に開く。
        ''' </summary>
        Private Sub EnsureConnection()
            If _connection.State <> ConnectionState.Open Then
                _connection.Open()
            End If
        End Sub

        ' ============================
        ' トランザクション制御
        ' ============================

        ''' <summary>
        ''' トランザクションを開始する。
        ''' </summary>
        Public Sub BeginTransaction()
            EnsureConnection()
            _transaction = _connection.BeginTransaction()
            AppLogger.Info("Transaction started")
        End Sub

        ''' <summary>
        ''' トランザクションをコミットする。
        ''' </summary>
        Public Sub Commit()
            If _transaction IsNot Nothing Then
                _transaction.Commit()
                AppLogger.Info("Transaction committed")
                _transaction = Nothing
            End If
        End Sub

        ''' <summary>
        ''' トランザクションをロールバックする。
        ''' </summary>
        Public Sub Rollback()
            If _transaction IsNot Nothing Then
                _transaction.Rollback()
                AppLogger.Warn("Transaction rolled back")
                _transaction = Nothing
            End If
        End Sub

        ' ============================
        ' SQL 実行（共通処理）
        ' ============================

        ''' <summary>
        ''' SQL コマンドを生成し、必要に応じてパラメータやトランザクションを設定する。
        ''' </summary>
        ''' <param name="sql">実行する SQL 文。</param>
        ''' <param name="params">SQL パラメータ一覧。</param>
        ''' <returns>構築済みの <see cref="SqlCommand"/>。</returns>
        Private Function CreateCommand(sql As String, params As IEnumerable(Of SqlParameter)) As SqlCommand
            EnsureConnection()

            Dim cmd As New SqlCommand(sql, _connection)

            If _transaction IsNot Nothing Then
                cmd.Transaction = _transaction
            End If

            If params IsNot Nothing Then
                For Each p In params
                    cmd.Parameters.Add(p)
                Next
            End If

            Return cmd
        End Function

        ' ============================
        ' SELECT（DataTable）
        ' ============================

        ''' <summary>
        ''' SELECT 文を実行し、結果を <see cref="DataTable"/> として取得する。
        ''' </summary>
        ''' <param name="sql">実行する SQL 文。</param>
        ''' <param name="params">SQL パラメータ一覧。</param>
        ''' <returns>取得されたデータを格納した <see cref="DataTable"/>。</returns>
        Public Function ExecuteDataTable(sql As String, Optional params As IEnumerable(Of SqlParameter) = Nothing) As DataTable
            Try
                Using cmd As SqlCommand = CreateCommand(sql, params)
                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        Return dt
                    End Using
                End Using

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "ExecuteDataTable"))
            End Try
        End Function

        ' ============================
        ' SELECT（DataReader）
        ' ============================

        ''' <summary>
        ''' SELECT 文を実行し、<see cref="SqlDataReader"/> を返す。
        ''' 呼び出し側で DataReader を読み終えた後、CloseConnection により接続が閉じられる。
        ''' </summary>
        ''' <param name="sql">実行する SQL 文。</param>
        ''' <param name="params">SQL パラメータ一覧。</param>
        ''' <returns><see cref="SqlDataReader"/>。</returns>
        Public Function ExecuteReader(sql As String, Optional params As IEnumerable(Of SqlParameter) = Nothing) As SqlDataReader
            Try
                Dim cmd As SqlCommand = CreateCommand(sql, params)
                Return cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "ExecuteReader"))
            End Try
        End Function

        ' ============================
        ' SELECT（Scalar）
        ' ============================

        ''' <summary>
        ''' SELECT 文を実行し、単一値（スカラー値）を取得する。
        ''' </summary>
        ''' <param name="sql">実行する SQL 文。</param>
        ''' <param name="params">SQL パラメータ一覧。</param>
        ''' <returns>取得されたスカラー値。</returns>
        Public Function ExecuteScalar(sql As String, Optional params As IEnumerable(Of SqlParameter) = Nothing) As Object
            Try
                Using cmd As SqlCommand = CreateCommand(sql, params)
                    Return cmd.ExecuteScalar()
                End Using

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "ExecuteScalar"))
            End Try
        End Function

        ' ============================
        ' INSERT / UPDATE / DELETE
        ' ============================

        ''' <summary>
        ''' INSERT / UPDATE / DELETE 文を実行し、影響を受けた行数を返す。
        ''' </summary>
        ''' <param name="sql">実行する SQL 文。</param>
        ''' <param name="params">SQL パラメータ一覧。</param>
        ''' <returns>影響を受けた行数。</returns>
        Public Function ExecuteNonQuery(sql As String, Optional params As IEnumerable(Of SqlParameter) = Nothing) As Integer
            Try
                Using cmd As SqlCommand = CreateCommand(sql, params)
                    Return cmd.ExecuteNonQuery()
                End Using

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "ExecuteNonQuery"))
            End Try
        End Function

        ' ============================
        ' IDisposable
        ' ============================

        ''' <summary>
        ''' リソースを解放する。
        ''' トランザクションおよび接続を安全に破棄する。
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            If Not _disposed Then
                If _transaction IsNot Nothing Then
                    _transaction.Dispose()
                End If
                If _connection IsNot Nothing Then
                    _connection.Dispose()
                End If
                _disposed = True
            End If
        End Sub

    End Class

End Namespace