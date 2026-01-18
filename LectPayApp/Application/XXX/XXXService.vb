Imports Application.User
Imports Domain.User

Namespace Application.User

    ''' <summary>
    ''' ユーザー管理のユースケースを実行するサービス。
    ''' UI には依存せず、Domain と Infrastructure を仲介する。
    ''' </summary>
    Public Class XXXService

        Private ReadOnly _repo As IXXXRepository

        ''' <summary>
        ''' Repository を DI（依存性注入）で受け取る。
        ''' </summary>
        Public Sub New(repo As IXXXRepository)
            _repo = repo
        End Sub

        '''' <summary>
        '''' ユーザーを保存する（新規 or 更新）。
        '''' </summary>
        'Public Sub Insert(dto As XXXDto)

        '    ' Repository に保存を依頼
        '    _repo.Insert(entity)
        'End Sub

        '''' <summary>
        '''' ユーザーを保存する（新規 or 更新）。
        '''' </summary>
        'Public Sub Update(dto As XXXDto)

        '    ' Repository に保存を依頼
        '    _repo.Update(entity)
        'End Sub

        ''' <summary>
        ''' ユーザーを保存する（新規 or 更新）。
        ''' </summary>
        Public Sub Save(mode As Integer, dto As XXXDto)

            ' Domain Entity を生成（ビジネスルールは Domain 側でチェック）
            Dim entity = New XXXEntity With {
                    .User_Id = dto.User_Id,
                    .User_Name = dto.User_Name,
                    .User_Address = dto.User_Address,
                    .User_TelNo = dto.User_TelNo,
                    .Age = dto.Age
                    }

            ' Repository に保存を依頼
            '_repo.Save(entity)
            If mode = 0 Then
                _repo.Insert(entity)
            Else
                _repo.Update(entity)
            End If

        End Sub

        ''' <summary>
        ''' ユーザーを削除する。
        ''' </summary>
        Public Sub Delete(userId As Integer)
            _repo.Delete(userId)
        End Sub

        ''' <summary>
        ''' ユーザー一覧を取得する。
        ''' </summary>
        Public Function GetList() As List(Of XXXDto)

            Dim entities = _repo.GetAll()

            ' Entity → DTO 変換
            Dim list As New List(Of XXXDto)
            For Each e In entities
                list.Add(New XXXDto With {
                    .User_Id = e.User_Id,
                    .User_Name = e.User_Name,
                    .User_Address = e.User_Address,
                    .User_TelNo = e.User_TelNo,
                    .Age = e.Age
                })
            Next

            Return list
        End Function

        ''' <summary>
        ''' 単一ユーザーを取得する。
        ''' </summary>
        Public Function GetById(userId As Integer) As XXXDto

            Dim e = _repo.GetById(userId)
            If e Is Nothing Then
                Return Nothing
            End If

            Return New XXXDto With {
                .User_Id = e.User_Id,
                .User_Name = e.User_Name,
                .User_Address = e.User_Address,
                .User_TelNo = e.User_TelNo,
                .Age = e.Age
            }
        End Function

    End Class

End Namespace