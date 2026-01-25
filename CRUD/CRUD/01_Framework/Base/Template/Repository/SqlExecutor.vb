Imports System.Data.SqlClient

Namespace Framework

    ''' <summary>
    ''' SQL 実行を簡潔に行うためのユーティリティクラス。
    ''' SELECT（複数行・1行）、INSERT、UPDATE、DELETE を共通化し、
    ''' パラメータ自動バインドにも対応します。
    ''' </summary>
    ''' <remarks>
    ''' ・接続文字列は <see cref="Initialize"/> で設定します。<br/>
    ''' ・パラメータは匿名オブジェクトのプロパティ名を自動で SQL パラメータへ変換します。<br/>
    ''' ・FW3.5 でも動作するようリフレクションを使用しています。
    ''' </remarks>
    Public NotInheritable Class SqlExecutor

        Private Sub New()
        End Sub

        ''' <summary>
        ''' SQL Server 接続文字列を初期化します。
        ''' </summary>
        ''' <param name="connectionString">使用する接続文字列。</param>
        Public Shared Sub Initialize(connectionString As String)
            _connectionString = connectionString
        End Sub

        Private Shared _connectionString As String = "Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI"

        Public Shared Function QueryDataTable(sql As String,
                                           Optional param As Object = Nothing) As DataTable

            Dim result As New DataTable

            Using conn As New SqlConnection(_connectionString)
                Using cmd As New SqlCommand(sql, conn)
                    AddParameters(cmd, param)
                    conn.Open()
                    Using da As New SqlDataAdapter(sql, conn)
                        da.Fill(result)
                    End Using
                End Using
            End Using

            Return result
        End Function



        '-----------------------------------------
        ' SELECT（複数行）
        '-----------------------------------------

        ''' <summary>
        ''' SELECT 文を実行し、複数行の結果を取得します。
        ''' </summary>
        ''' <typeparam name="T">1 行をマッピングする型。</typeparam>
        ''' <param name="sql">実行する SQL 文。</param>
        ''' <param name="mapper">SqlDataReader から T へ変換する関数。</param>
        ''' <param name="param">SQL パラメータとして使用する匿名オブジェクト。</param>
        ''' <returns>取得した T のリスト。</returns>
        ''' <remarks>
        ''' mapper は 1 行ごとに呼び出されます。<br/>
        ''' パラメータは匿名オブジェクトのプロパティ名を @プロパティ名 として自動追加します。
        ''' </remarks>
        Public Shared Function Query(Of T)(sql As String,
                                           mapper As Func(Of SqlDataReader, T),
                                           Optional param As Object = Nothing) As List(Of T)

            Dim result As New List(Of T)

            Using conn As New SqlConnection(_connectionString)
                Using cmd As New SqlCommand(sql, conn)

                    AddParameters(cmd, param)

                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(mapper(reader))
                        End While
                    End Using

                End Using
            End Using

            Return result
        End Function

        '-----------------------------------------
        ' SELECT（1行）
        '-----------------------------------------

        ''' <summary>
        ''' SELECT 文を実行し、最初の 1 行のみを取得します。
        ''' </summary>
        ''' <typeparam name="T">1 行をマッピングする型。</typeparam>
        ''' <param name="sql">実行する SQL 文。</param>
        ''' <param name="mapper">SqlDataReader から T へ変換する関数。</param>
        ''' <param name="param">SQL パラメータとして使用する匿名オブジェクト。</param>
        ''' <returns>取得した T。該当行が無い場合は Nothing。</returns>
        Public Shared Function QuerySingle(Of T)(sql As String,
                                                 mapper As Func(Of SqlDataReader, T),
                                                 Optional param As Object = Nothing) As T

            Using conn As New SqlConnection(_connectionString)
                Using cmd As New SqlCommand(sql, conn)

                    AddParameters(cmd, param)

                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return mapper(reader)
                        End If
                    End Using

                End Using
            End Using

            Return Nothing
        End Function

        '-----------------------------------------
        ' INSERT / UPDATE / DELETE
        '-----------------------------------------

        ''' <summary>
        ''' INSERT / UPDATE / DELETE 文を実行します。
        ''' </summary>
        ''' <param name="sql">実行する SQL 文。</param>
        ''' <param name="param">SQL パラメータとして使用する匿名オブジェクト。</param>
        ''' <returns>影響を受けた行数。</returns>
        Public Shared Function Execute(sql As String,
                                       Optional param As Object = Nothing) As Integer

            Using conn As New SqlConnection(_connectionString)
                Using cmd As New SqlCommand(sql, conn)

                    AddParameters(cmd, param)

                    conn.Open()
                    Return cmd.ExecuteNonQuery()

                End Using
            End Using

        End Function

        '-----------------------------------------
        ' パラメータ自動生成
        '-----------------------------------------

        ''' <summary>
        ''' 匿名オブジェクトのプロパティを SQL パラメータへ自動追加します。
        ''' </summary>
        ''' <param name="cmd">対象の SqlCommand。</param>
        ''' <param name="param">プロパティを持つ匿名オブジェクト。</param>
        ''' <remarks>
        ''' プロパティ名が Name の場合、@Name として追加されます。<br/>
        ''' 値が Nothing の場合は DBNull.Value が設定されます。
        ''' </remarks>
        Private Shared Sub AddParameters(cmd As SqlCommand, param As Object)
            If param Is Nothing Then Return

            Dim props = param.GetType().GetProperties()

            For Each p In props
                Dim value = p.GetValue(param, Nothing)
                If value Is Nothing Then value = DBNull.Value
                cmd.Parameters.AddWithValue("@" & p.Name, value)
            Next
        End Sub

    End Class

End Namespace