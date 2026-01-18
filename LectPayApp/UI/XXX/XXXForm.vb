Imports Application.User
Imports UI.XXX

Public Class XXXForm

    Private _controller As XXXController

    ''' <summary>
    ''' コンストラクタ。コントローラを初期化する。
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _controller = New XXXController(Me)
    End Sub

    ''' <summary>
    ''' フォームロード時の初期処理。
    ''' </summary>
    Private Sub XXXForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _controller.Initialize()
    End Sub

    ''' <summary>
    ''' 保存ボタン押下イベント。
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        _controller.Save()
    End Sub

    ''' <summary>
    ''' 検索ボタン押下イベント。
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        _controller.Search()
    End Sub

    ''' <summary>
    ''' 削除ボタン押下イベント。
    ''' </summary>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        _controller.Delete()
    End Sub

    '-----------------------------------------
    ' Form → Controller に渡す入力値
    '-----------------------------------------

    ''' <summary>
    ''' 入力項目1（例：名称）。
    ''' </summary>
    Public ReadOnly Property UserId As String
        Get
            Return txtUserId.Text
        End Get
    End Property

    ''' <summary>
    ''' 入力項目2（例：数値）。
    ''' </summary>
    Public ReadOnly Property UserName As String
        Get
            Return txtUserId.Text
        End Get
    End Property

    ''' <summary>
    ''' 選択中の ID。
    ''' </summary>
    Public ReadOnly Property SelectedId As Integer
        Get
            If dgvList.CurrentRow Is Nothing Then
                Return -1
            End If
            Return CInt(dgvList.CurrentRow.Cells("User_Id").Value)
        End Get
    End Property

    ''' <summary>
    ''' 選択中の ID。
    ''' </summary>
    Public ReadOnly Property SelectedKey As Integer
        Get
            If dgvList.CurrentRow Is Nothing Then
                Return -1
            End If
            Return CInt(dgvList.CurrentRow.Cells("User_Id").Value)
        End Get
    End Property

    '-----------------------------------------
    ' Presenter から呼ばれる画面更新メソッド
    '-----------------------------------------

    ''' <summary>
    ''' 一覧を DataGridView に表示する。
    ''' </summary>
    Public Sub SetList(list As List(Of XXXDto))
        dgvList.DataSource = list
        dgvList.Refresh()
    End Sub

    ''' <summary>
    ''' 詳細情報を画面に反映する。
    ''' </summary>
    Public Sub SetDetail(dto As XXXDto)
        txtUserId.Text = dto.User_Id.ToString()
        txtUserName.Text = dto.User_Name.ToString()
        txtAddress.Text = dto.User_Address.ToString()
        txtTelNo.Text = dto.User_TelNo.ToString()
        txtAge.Text = dto.Age.ToString()
    End Sub

    ''' <summary>
    ''' 入力欄をクリアする。
    ''' </summary>
    Public Sub ClearInputs()
        txtUserId.Clear()
        txtUserName.Clear()
    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        _controller.GetById()
    End Sub
End Class