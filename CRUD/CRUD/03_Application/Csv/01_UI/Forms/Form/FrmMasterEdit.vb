Imports System.Runtime.InteropServices

Public Class FrmMasterEdit
    Inherits FormBase
    ''' <summary>
    ''' ビューコントローラ
    ''' </summary>
    Private _view As MasterEditViewController

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub
    ''' <summary>
    ''' Loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        _view = New MasterEditViewController(Me)
        _view.Initialize(New MasterEditModel)
        ReadDataAll()
    End Sub

    ''' <summary>
    ''' [追加]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        _view.AddButtonClick(GetModel)
    End Sub

    ''' <summary>
    ''' [編集]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dgv.CurrentRow Is Nothing Then
            Return
        End If
        SetRowDataInTextBox()
        _view.EditButtonClick(GetModel)
    End Sub

    ''' <summary>
    ''' [削除]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgv.CurrentRow Is Nothing Then
            Return
        End If

        If MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.YesNo) = DialogResult.No Then Return

        SetModel(New MasterEditModel)
        _view.DeleteButtonClick(GetModel)

        ReadDataAll()
    End Sub

    ''' <summary>
    ''' [保存]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            _view.SaveButtonClick(GetModel)

            ReadDataAll()
            SetModel(New MasterEditModel)
        Catch ex As Exception
            MessageBox.Show("保存に失敗しました。" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' [キャンセル]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _view.CancelButtonClick(New MasterEditModel)
    End Sub

    ''' <summary>
    ''' 全データ取得
    ''' </summary>
    Private Sub ReadDataAll()
        dgv.DataSource = _view.LoadData()
    End Sub

    ''' <summary>
    ''' 画面Model設定
    ''' </summary>
    ''' <param name="model"></param>
    Public Sub SetModel(ByVal model As MasterEditModel)
        txtSiteCode.Text = model.Site_Code
        txtGrade.Text = model.Grade
        txtClassCode.Text = model.Class_Code
        txtKoma.Text = model.Koma_Seq
        txtSubjects.Text = model.Subjects
        btnAdd.Enabled = model.Add
        btnEdit.Enabled = model.Edit
        btnDelete.Enabled = model.Delete
        btnSave.Enabled = model.Save
        btnCancel.Enabled = model.Cancel
    End Sub

    ''' <summary>
    ''' 画面Model取得
    ''' </summary>
    ''' <returns></returns>
    Public Function GetModel() As MasterEditModel
        Dim row As DataGridViewRow = dgv.CurrentRow

        Return New MasterEditModel With {
                    .Site_Code = txtSiteCode.Text,
                    .Grade = txtGrade.Text,
                    .Class_Code = txtClassCode.Text,
                    .Koma_Seq = txtKoma.Text,
                    .Subjects = txtSubjects.Text,
                    .Add = btnAdd.Enabled,
                    .Edit = btnEdit.Enabled,
                    .Delete = btnDelete.Enabled,
                    .Save = btnSave.Enabled,
                    .Cancel = btnCancel.Enabled
                }
    End Function

    ''' <summary>
    ''' 入力欄に選択中のROW内容を設定する。
    ''' </summary>
    Private Sub SetRowDataInTextBox()
        Dim row As DataGridViewRow = dgv.CurrentRow
        If row.Selected Then
            txtSiteCode.Text = CStr(row.Cells("Site_Code").Value)
            txtGrade.Text = CStr(row.Cells("Grade").Value)
            txtClassCode.Text = CStr(row.Cells("Class_Code").Value)
            txtKoma.Text = CInt(row.Cells("Koma_Seq").Value)
            txtSubjects.Text = CStr(row.Cells("Subjects").Value)
        End If
    End Sub


    Public Function GetDpiScale() As Single
        Return DpiUtil.GetScaleFactor()
    End Function


End Class
