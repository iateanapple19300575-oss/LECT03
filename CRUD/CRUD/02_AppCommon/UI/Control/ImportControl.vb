''' <summary>
''' インポート画面部品（[参照][取込][確認][ファイルパス]）
''' </summary>
Public Class ImportControl
    '==========================
    ' イベント定義
    '==========================
    Public Event FileSelected(ByVal filePath As String)
    Public Event ImportRequested(ByVal filePath As String)
    Public Event PreviewRequested(ByVal filePath As String)

    ''' <summary>
    ''' ファイルパス。
    ''' </summary>
    Private _filePath As String = String.Empty
    Public ReadOnly Property FilePath As String
        Get
            Return _filePath
        End Get
    End Property

    ''' <summary>
    ''' 処理日時。
    ''' </summary>
    ''' <returns></returns>
    Public Property ImportedAt As DateTime
        Get
            Return DateTime.Parse(lblLastDateTime.Text)
        End Get
        Set(value As DateTime)
            lblLastDateTime.Text = value.ToString("yyyy/MM/dd HH:mm")
        End Set
    End Property

    ''' <summary>
    ''' 処理件数。
    ''' </summary>
    ''' <returns></returns>
    Public Property ImportedCount As Integer
        Get
            Return Integer.Parse(lblLastCount.Text)
        End Get
        Set(value As Integer)
            lblLastCount.Text = value & " 件"
        End Set
    End Property

    ''' <summary>
    ''' 処理結果。
    ''' </summary>
    ''' <returns></returns>
    Public Property ImportedResult As String
        Get
            Return lblLastMessage.Text
        End Get
        Set(value As String)
            lblLastMessage.Text = value
        End Set
    End Property

    ''' <summary>
    ''' ロック状態。
    ''' </summary>
    ''' <returns></returns>
    Public Property IsBusy As Boolean
        Get
            Return Not btnImport.Enabled
        End Get
        Set(value As Boolean)
            btnSelect.Enabled = Not value
            btnImport.Enabled = Not value
            btnPreview.Enabled = Not value
        End Set
    End Property


    ''' <summary>
    ''' Loadイベント。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ImportControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnSelect.Enabled = True
        btnImport.Enabled = False
        btnPreview.Enabled = True
    End Sub

    ''' <summary>
    ''' [参照]ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Using dlg As New OpenFileDialog
            dlg.Filter = "CSVファイル (*.csv)|*.csv"
            If dlg.ShowDialog() = DialogResult.OK Then
                _filePath = dlg.FileName
                txtFilePath.Text = _filePath
                RaiseEvent FileSelected(_filePath)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' [取込]ボタン押下イベント。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        If _filePath <> "" Then
            RaiseEvent ImportRequested(_filePath)
        End If
    End Sub

    ''' <summary>
    ''' [確認]ボタン押下イベント。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If _filePath <> "" Then
            RaiseEvent PreviewRequested(_filePath)
        End If
    End Sub

    ''' <summary>
    ''' ファイルパス選択イベント。
    ''' </summary>
    ''' <remarks>
    ''' パス指定されたならば、[取込]ボタンを活性化する。
    ''' </remarks>
    ''' <param name="filePath"></param>
    Private Sub ImportControl_FileSelected(filePath As String) Handles Me.FileSelected
        btnImport.Enabled = False
        If Not String.IsNullOrEmpty(txtFilePath.Text) Then
            btnImport.Enabled = True
        End If
    End Sub
End Class
