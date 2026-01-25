Imports System.Data.SqlClient
Imports Entities

Public Class SqlXxxImportRepository
    Implements IImportRepository

    Private ReadOnly _connectionString As String

    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    Public Function BulkInsert(entities As List(Of XxxImportEntity)) As Integer _
        Implements IImportRepository.BulkInsert

        If entities Is Nothing OrElse entities.Count = 0 Then
            Return 0
        End If

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Using tran = conn.BeginTransaction()
                Try
                    Dim count As Integer = 0

                    For Each e In entities
                        Using cmd As New SqlCommand("
                            INSERT INTO CsvImportTable (Site_Code, Grade, Class_Code, Koma_Seq, Subjects)
                            VALUES (@Site_Code, @Grade, @Class_Code, @Koma_Seq, @Subjects)
                        ", conn, tran)

                            cmd.Parameters.AddWithValue("@Site_Code", e.Site_Code)
                            cmd.Parameters.AddWithValue("@Grade", e.Grade)
                            cmd.Parameters.AddWithValue("@Class_Code", e.Class_Code)
                            cmd.Parameters.AddWithValue("@Koma_Seq", e.Koma_Seq)
                            cmd.Parameters.AddWithValue("@Subjects", e.Subjects)

                            count += cmd.ExecuteNonQuery()
                        End Using
                    Next

                    tran.Commit()
                    Return count

                Catch ex As SqlException
                    tran.Rollback()
                    Throw New AppException("DB登録中にエラーが発生しました。")

                Catch ex As Exception
                    tran.Rollback()
                    Throw
                End Try
            End Using
        End Using

    End Function

End Class