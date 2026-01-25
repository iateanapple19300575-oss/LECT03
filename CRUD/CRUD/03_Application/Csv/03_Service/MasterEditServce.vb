Imports Entities
Imports Repositories

''' <summary>
''' フォームコントローラ
''' </summary>
Public Class MasterEditServce

    Private connectionString As String = "Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI"

    Private ReadOnly _validator As IValidator(Of MasterEditEntity)
    Private ReadOnly _repository As IRepository(Of MasterEditEntity)

    Public Sub New(repo As IRepository(Of MasterEditEntity), validator As IValidator(Of MasterEditEntity))
        _repository = repo
        _validator = validator
    End Sub

    ''' <summary>
    ''' 全データをDataTableに取得する。
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllDataTable() As DataTable
        Return _repository.FindAllForDataTable()
    End Function

    ''' <summary>
    ''' データ追加
    ''' </summary>
    ''' <param name="dto"></param>
    ''' <returns></returns>
    Public Function ExecuteInsert(dto As MasterEditInputDto) As BasicResult
        Try
            Dim entity As MasterEditEntity = ToEntity(dto)
            '_validator.Validate(entity)

            Using uow As New UnitOfWork()
                _repository.Insert(entity)
                uow.Commit()
            End Using

            Return BasicResult.Success()

        Catch ex As Exception
            Return HandleException(ex)
        End Try
    End Function

    ''' <summary>
    ''' データ更新
    ''' </summary>
    ''' <param name="dto"></param>
    ''' <returns></returns>
    Public Function ExecuteUpdate(dto As MasterEditInputDto) As BasicResult
        Try
            Dim entity As MasterEditEntity = ToEntity(dto)
            '_validator.Validate(entity)

            Using uow As New UnitOfWork()
                _repository.Update(entity)
                uow.Commit()
            End Using

            Return BasicResult.Success()

        Catch ex As Exception
            Return HandleException(ex)
        End Try
    End Function

    ''' <summary>
    ''' データ削除
    ''' </summary>
    ''' <param name="dto"></param>
    ''' <returns></returns>
    Public Function ExecuteDelete(dto As MasterEditInputDto) As BasicResult
        Try
            Dim entity As MasterEditEntity = ToEntity(dto)
            '_validator.Validate(entity)

            Using uow As New UnitOfWork()
                _repository.Delete(entity)
                uow.Commit()
            End Using

            Return BasicResult.Success()

        Catch ex As Exception
            Return HandleException(ex)
        End Try
    End Function

    Private Function HandleException(ex As Exception) As BasicResult
        If TypeOf ex Is ValidationException Then
            Return BasicResult.Fail(New List(Of ImportError) From {
                New ImportError With {.Message = ex.Message, .ErrorType = ImportErrorType.Validation}
            })
        End If

        Throw New TechnicalException("予期しないエラー", ex)
    End Function

    Public Shared Function ToEntity(dto As MasterEditInputDto) As MasterEditEntity
        If dto Is Nothing Then
            Return Nothing
        End If

        Return New MasterEditEntity With {
            .Site_Code = dto.Site_Code,
            .Grade = dto.Grade,
            .Class_Code = dto.Class_Code,
            .Koma_Seq = dto.Koma_Seq,
            .Subjects = dto.Subjects
        }
    End Function

End Class