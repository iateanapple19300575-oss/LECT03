Public Class SiteComboBox
    Inherits ComboBox

    Private _service As SiteService
    Private _initialized As Boolean = False

    Public Sub New()
        ' デザイナではサービスを生成しない
        If IsInDesignMode() Then
            Return
        End If

        _service = New SiteService()
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()

        If IsInDesignMode() Then
            Return
        End If

        If Not _initialized Then
            InitializeData()
            _initialized = True
        End If
    End Sub

    Private Sub InitializeData()
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.DataSource = _service.GetSites()
        Me.DisplayMember = "DisplayName"
        Me.ValueMember = "Value"
    End Sub

    Private Function IsInDesignMode() As Boolean
        Return Process.GetCurrentProcess().ProcessName = "devenv"
    End Function

End Class