Imports System.Data.SqlClient

Public Class BulkCopyExecutor

    Public Shared Sub ExecuteWithTran(cn As SqlConnection,
                                      tran As SqlTransaction,
                                      tableName As String,
                                      dt As DataTable,
                                      batchSize As Integer,
                                      notifyAfter As Integer,
                                      notifyHandler As SqlRowsCopiedEventHandler)

        Using bulk As New SqlBulkCopy(cn, SqlBulkCopyOptions.Default, tran)
            bulk.DestinationTableName = tableName

            For Each col As DataColumn In dt.Columns
                bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName)
            Next

            bulk.BatchSize = batchSize
            bulk.NotifyAfter = notifyAfter

            If notifyHandler IsNot Nothing Then
                AddHandler bulk.SqlRowsCopied, notifyHandler
            End If

            bulk.WriteToServer(dt)
        End Using

    End Sub

End Class