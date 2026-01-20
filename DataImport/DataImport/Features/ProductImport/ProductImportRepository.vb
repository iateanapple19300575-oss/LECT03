' ProductRepository.vb
Imports System.Data.SqlClient
Imports System.Data

Public Class ProductRepository
    Implements IImportRepository(Of ProductDto)

    Public Sub ExecuteWithTransaction(dtos As IEnumerable(Of ProductDto), count As Integer) _
        Implements IImportRepository(Of ProductDto).ExecuteWithTransaction

        Using con = DbHelper.GetConnection()
            con.Open()

            Using tran = con.BeginTransaction()
                Try
                    ' 1. 削除
                    Dim deleteSql = "DELETE FROM Product"
                    Using cmd As New SqlCommand(deleteSql, con, tran)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' 2. BulkCopy
                    Dim dt As New DataTable()
                    dt.Columns.Add("ProductId", GetType(String))
                    dt.Columns.Add("ProductName", GetType(String))
                    dt.Columns.Add("Price", GetType(Integer))

                    For Each d In dtos
                        dt.Rows.Add(d.ProductId, d.ProductName, d.Price)
                    Next

                    Using bulk As New SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran)
                        bulk.DestinationTableName = "Product"
                        bulk.WriteToServer(dt)
                    End Using

                    ' 3. 履歴
                    Dim historySql = "INSERT INTO ImportHistory (ImportType, ImportCount, ProcessedAt) VALUES ('Product', @cnt, GETDATE())"
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