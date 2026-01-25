Imports Entities
Imports Repositories

Public Class MasterEditViewController

    Private ReadOnly _view As FrmMasterEdit
    Private ReadOnly _service As MasterEditServce
    Private ReadOnly _repository As IRepository(Of MasterEditEntity)
    Private ReadOnly _validator As IValidator(Of MasterEditEntity)

    Private Enum EditMode
        None
        Add
        Edit
    End Enum

    Private _editMode As EditMode = EditMode.None

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="view"></param>
    Public Sub New(ByVal view As FrmMasterEdit)
        _view = view
        _repository = New MasterEditRepository
        _validator = New MasterEditValidator
        _service = New MasterEditServce(_repository, _validator)
    End Sub

    ''' <summary>
    ''' 初期化処理
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub Initialize(ByVal model As MasterEditModel)
        SetBlankInTextBox(model)
        _view.SetModel(model)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadData() As DataTable
        _editMode = EditMode.None

        Return _service.GetAllDataTable()
    End Function


    ''' <summary>
    ''' [追加]ボタン押下
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub AddButtonClick(ByVal model As MasterEditModel)
        _editMode = EditMode.Add
        SetBlankInTextBox(model)
        SetModel(model)
    End Sub

    ''' <summary>
    ''' [編集]ボタン押下
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub EditButtonClick(ByVal model As MasterEditModel)
        _editMode = EditMode.Edit
        SetModel(model)
    End Sub

    ''' <summary>
    ''' [削除]ボタン押下
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub DeleteButtonClick(ByVal model As MasterEditModel)
        _editMode = EditMode.None
        _service.ExecuteDelete(ToDto(model))
        SetBlankInTextBox(model)
        SetModel(model)
    End Sub

    ''' <summary>
    ''' [保存]ボタン押下
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub SaveButtonClick(ByVal model As MasterEditModel)
        _editMode = EditMode.None

        Dim dto As MasterEditInputDto = ToDto(model)

        If _editMode = EditMode.Add Then
            _service.ExecuteInsert(dto)
        Else
            _service.ExecuteUpdate(dto)
        End If

        SetModel(model)
    End Sub

    ''' <summary>
    ''' [キャンセル]ボタン押下
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub CancelButtonClick(ByVal model As MasterEditModel)
        _editMode = EditMode.None
        SetBlankInTextBox(model)
        SetModel(model)
    End Sub

    ''' <summary>
    ''' 入力欄データをクリアする。
    ''' </summary>
    ''' <param name="model"></param>
    Private Sub SetBlankInTextBox(ByRef model As MasterEditModel)
        model.Site_Code = ""
        model.Grade = ""
        model.Class_Code = ""
        model.Koma_Seq = ""
        model.Subjects = ""
    End Sub

    ''' <summary>
    ''' Formに渡すデータをモデルに設定する。
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub SetModel(ByVal model As MasterEditModel)
        _view.SetModel(
            New MasterEditModel With {
                .Site_Code = model.Site_Code,
                .Grade = model.Grade,
                .Class_Code = model.Class_Code,
                .Koma_Seq = model.Koma_Seq,
                .Subjects = model.Subjects,
                .Add = (_editMode = EditMode.None),
                .Edit = (_editMode = EditMode.None AndAlso model.CurrentRow IsNot Nothing),
                .Delete = (_editMode = EditMode.None AndAlso model.CurrentRow IsNot Nothing),
                .Save = (_editMode <> EditMode.None),
                .Cancel = (_editMode <> EditMode.None)
            }
        )
    End Sub

    ''' <summary>
    ''' インポート要求DTOを作成する。
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    Private Function CreateDto(ByVal filePath As String) As CsvImportRequest
        Return New CsvImportRequest With {
                .FilePath = filePath
            }
    End Function

    ''' <summary>
    ''' MODEL→DTO変換
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    Public Shared Function ToDto(model As MasterEditModel) As MasterEditInputDto
        If model Is Nothing Then
            Return Nothing
        End If

        Return New MasterEditInputDto With {
            .Site_Code = model.Site_Code,
            .Grade = model.Grade,
            .Class_Code = model.Class_Code,
            .Koma_Seq = model.Koma_Seq,
            .Subjects = model.Subjects
        }
    End Function

    ''' <summary>
    ''' DTO→MODEL変換
    ''' </summary>
    ''' <param name="dto"></param>
    ''' <returns></returns>
    Public Shared Function ToModel(dto As MasterEditInputDto) As MasterEditModel
        If dto Is Nothing Then
            Return Nothing
        End If

        Return New MasterEditModel With {
            .Site_Code = dto.Site_Code,
            .Grade = dto.Grade,
            .Class_Code = dto.Class_Code,
            .Koma_Seq = dto.Koma_Seq,
            .Subjects = dto.Subjects
        }
    End Function

End Class
