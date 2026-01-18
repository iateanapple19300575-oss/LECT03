Imports System.Data.SqlClient
Imports Domain.User

Namespace Infrastructure.User

    ''' <summary>
    ''' IUserRepository の SQL Server 実装。
    ''' </summary>
    Public Class XXXRepository
        Implements IXXXRepository

        Private ReadOnly _connectionString As String = "Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI"

        '===========================================================
        ' 追加
        '===========================================================
        Public Sub Insert(entity As XXXEntity) Implements IXXXRepository.Insert

            Using con As New SqlConnection(_connectionString)
                con.Open()

                Dim sql As String

                ' 新規登録
                sql = "
                        INSERT INTO Users 
                            (User_Id, User_Name, User_Address, User_TelNo, Age)
                        VALUES 
                            (@User_Id, @User_Name, @User_Address, @User_TelNo, @Age)
                    "

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@User_Id", entity.User_Id)
                    cmd.Parameters.AddWithValue("@User_Name", entity.User_Name)
                    cmd.Parameters.AddWithValue("@User_Address", entity.User_Address)
                    cmd.Parameters.AddWithValue("@User_TelNo", entity.User_TelNo)
                    cmd.Parameters.AddWithValue("@Age", entity.Age)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        '===========================================================
        ' 更新
        '===========================================================
        Public Sub Update(entity As XXXEntity) Implements IXXXRepository.Update

            Using con As New SqlConnection(_connectionString)
                con.Open()

                Dim sql As String

                ' 更新
                sql = "
                        UPDATE Users
                        SET User_Id = @User_Id,
                            User_Name = @User_Name,
                            User_Address = @User_Address,
                            User_TelNo = @User_TelNo,
                           Age = @Age
                        WHERE User_Id = @User_Id
                    "

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@User_Id", entity.User_Id)
                    cmd.Parameters.AddWithValue("@User_Name", entity.User_Name)
                    cmd.Parameters.AddWithValue("@User_Address", entity.User_Address)
                    cmd.Parameters.AddWithValue("@User_TelNo", entity.User_TelNo)
                    cmd.Parameters.AddWithValue("@Age", entity.Age)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        '===========================================================
        ' 削除
        '===========================================================
        Public Sub Delete(User_Id As Integer) Implements IXXXRepository.Delete

            Using con As New SqlConnection(_connectionString)
                con.Open()

                Dim sql As String = "
                    DELETE FROM Users
                    WHERE User_Id = @User_Id
                "

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@User_Id", User_Id)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

        End Sub

        '===========================================================
        ' 全件取得
        '===========================================================
        Public Function GetAll() As List(Of XXXEntity) Implements IXXXRepository.GetAll

            Dim list As New List(Of XXXEntity)

            Using con As New SqlConnection(_connectionString)
                con.Open()

                Dim sql As String = "
                    SELECT User_Id, User_Name, User_Address, User_TelNo, Age
                    FROM Users
                    ORDER BY User_Id
                "

                Using cmd As New SqlCommand(sql, con)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(New XXXEntity With {
                                .User_Id = SafeGetString(reader, "User_Id"),
                                .User_Name = SafeGetString(reader, "User_Name"),
                                .User_Address = SafeGetString(reader, "User_Address"),
                                .User_TelNo = SafeGetString(reader, "User_TelNo"),
                                .Age = SafeGetInt(reader, "Age")
                            })
                        End While

                    End Using
                End Using
            End Using

            Return list

        End Function

        '===========================================================
        ' 単一取得
        '===========================================================
        Public Function GetById(User_Id As Integer) As XXXEntity Implements IXXXRepository.GetById

            Using con As New SqlConnection(_connectionString)
                con.Open()

                Dim sql As String = "
                    SELECT User_Id, User_Name, User_Address, User_TelNo, Age
                    FROM Users
                    WHERE User_Id = @User_Id
                "

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@User_Id", User_Id)

                    Using reader As SqlDataReader = cmd.ExecuteReader()

                        If reader.Read() Then
                            Return New XXXEntity With {
                                .User_Id = SafeGetString(reader, "User_Id"),
                                .User_Name = SafeGetString(reader, "User_Name"),
                                .User_Address = SafeGetString(reader, "User_Address"),
                                .User_TelNo = SafeGetString(reader, "User_TelNo"),
                                .Age = SafeGetInt(reader, "Age")
                            }
                        End If

                    End Using
                End Using
            End Using

            Return Nothing

        End Function

        '===========================================================
        ' SafeGet ユーティリティ（FW3.5 互換）
        '===========================================================
        Private Function SafeGetString(r As SqlDataReader, col As String) As String
            Dim idx = r.GetOrdinal(col)
            If r.IsDBNull(idx) Then Return ""
            Return r.GetString(idx)
        End Function

        Private Function SafeGetInt(r As SqlDataReader, col As String) As Integer
            Dim idx = r.GetOrdinal(col)
            If r.IsDBNull(idx) Then Return 0
            Return r.GetInt32(idx)
        End Function

    End Class

End Namespace