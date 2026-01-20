' CustomerRepository.vb
Imports System.Data.SqlClient
Imports System.Data

Public Class CustomerRepository
    Implements IImportRepository(Of CustomerDto)

    Public Sub ExecuteWithTransaction(dtos As IEnumerable(Of CustomerDto), count As Integer) _
        Implements IImportRepository(Of CustomerDto).ExecuteWithTransaction

        Using con = DbHelper.GetConnection()
            con.Open()

            Using tran = con.BeginTransaction()
                Try
                    ' 1. 削除
                    Dim deleteSql = "DELETE FROM Customer"
                    Using cmd As New SqlCommand(deleteSql, con, tran)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' 2. BulkCopy
                    Dim dt As New DataTable()
                    dt.Columns.Add("CustomerId", GetType(String))
                    dt.Columns.Add("Name", GetType(String))
                    dt.Columns.Add("Birth", GetType(Date))

                    For Each d In dtos
                        dt.Rows.Add(d.CustomerId, d.Name, d.Birth)
                    Next

                    Using bulk As New SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran)
                        bulk.DestinationTableName = "Customer"
                        bulk.WriteToServer(dt)
                    End Using

                    ' 3. 履歴
                    Dim historySql = "INSERT INTO ImportHistory (ImportType, ImportCount, ProcessedAt) VALUES ('Customer', @cnt, GETDATE())"
                    Using cmd As New SqlCommand(historySql, con, tran)
                        cmd.Parameters.AddWithValue("@cnt", count)
                        cmd.ExecuteNonQuery()
                    End Using

                    tran.Commit()
                Catch
                    tran.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub
End Class