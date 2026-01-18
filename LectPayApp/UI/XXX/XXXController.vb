Imports Application.User
Imports Framework.UI
Imports Infrastructure.User

Namespace UI.XXX

    ''' <summary>
    ''' XXX 画面のコントローラ。
    ''' 画面のユースケース処理を統括し、
    ''' Validator / Loader / Presenter / Service と連携して処理を実行する。
    ''' </summary>
    Public Class XXXController

        Private ReadOnly _form As XXXForm
        Private ReadOnly _validator As XXXValidator
        Private ReadOnly _loader As XXXLoader
        Private ReadOnly _presenter As XXXPresenter
        Private ReadOnly _service As XXXService

        Private _mode As Integer = 0
        ''' <summary>
        ''' コンストラクタ。画面と各コンポーネントを初期化する。
        ''' </summary>
        Public Sub New(form As XXXForm)
            _form = form
            _validator = New XXXValidator(form)
            _loader = New XXXLoader(form)
            _presenter = New XXXPresenter(form)
            _service = New XXXService(New XXXRepository())
        End Sub

        ''' <summary>
        ''' 初期表示処理。
        ''' </summary>
        Public Sub Initialize()
            _loader.LoadInitialData()
            RefreshList()
        End Sub

        ''' <summary>
        ''' 1件取得処理。
        ''' </summary>
        Public Sub GetById()
            If _form.SelectedId <= 0 Then
                'NotificationService.Warn("削除対象が選択されていません。")
                Exit Sub
            End If

            Dim row As XXXDto = _service.GetById(_form.SelectedId)
            _presenter.ShowDetail(row)
            _mode = 1
        End Sub

        ''' <summary>
        ''' 保存処理（新規 or 更新）。
        ''' </summary>
        Public Sub Save()
            Dim result = _validator.Validate(_form)
            If Not result.IsValid Then
                NotificationService.Warn(result.Message)
                Exit Sub
            End If

            Dim dto As New XXXDto With {
                .User_Id = _form.txtUserId.Text,
                .User_Name = _form.txtUserName.Text,
                .User_Address = _form.txtAddress.Text,
                .User_TelNo = _form.txtTelNo.Text,
                .Age = CInt(_form.txtAge.Text)
            }

            _service.Save(_mode, dto)
            NotificationService.Success("保存しました。")
            RefreshList()
            _mode = 0
        End Sub

        ''' <summary>
        ''' 削除処理。
        ''' </summary>
        Public Sub Delete()
            If _form.SelectedId <= 0 Then
                NotificationService.Warn("削除対象が選択されていません。")
                Exit Sub
            End If

            _service.Delete(_form.SelectedId)
            NotificationService.Success("削除しました。")
            RefreshList()
        End Sub

        ''' <summary>
        ''' 検索処理。
        ''' </summary>
        Public Sub Search()
            RefreshList()
        End Sub

        ''' <summary>
        ''' 一覧再表示。
        ''' </summary>
        Public Sub RefreshList()
            Dim list = _service.GetList()
            _presenter.ShowList(list)
            _mode = 0
        End Sub

    End Class

End Namespace