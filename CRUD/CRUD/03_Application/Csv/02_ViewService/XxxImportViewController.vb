
''' <summary>
''' ビューコントローラ
''' </summary>
Public Class XxxImportViewController

    Private ReadOnly _view As FrmCsvImport
    Private ReadOnly _service As IImportService

    Public Sub New(ByVal view As FrmCsvImport)
        _view = view
        _service = New XxxImportService(New CsvReader(New CsvFile), New CsvImportValidator)

        AddHandler _view.ImportControl1.FileSelected, AddressOf OnFileSelected
        AddHandler _view.ImportControl1.ImportRequested, AddressOf OnImportRequested
        AddHandler _view.ImportControl1.PreviewRequested, AddressOf OnPreviewRequested
    End Sub

    ''' <summary>
    ''' ファイル選択
    ''' </summary>
    ''' <param name="filePath"></param>
    Private Sub OnFileSelected(ByVal filePath As String)
        ' 必要ならログなど
    End Sub

    ''' <summary>
    ''' [取込]ボタン押下イベント処理
    ''' </summary>
    ''' <param name="filePath"></param>
    Private Sub OnImportRequested(ByVal filePath As String)
        Try
            _view.ImportControl1.IsBusy = True

            ' CSVインポート要求
            Dim result = _service.Execute(CreateDto(filePath))

            ApplyResult(result)

        Finally
            _view.ImportControl1.IsBusy = False
        End Try
    End Sub

    ''' <summary>
    ''' [確認ボタン]押下イベント処理
    ''' </summary>
    ''' <param name="filePath"></param>
    Private Sub OnPreviewRequested(ByVal filePath As String)
        ' CSV 内容表示画面を開くなど
    End Sub

    ''' <summary>
    ''' 取込結果をFormに返す。
    ''' </summary>
    ''' <param name="result"></param>
    Private Sub ApplyResult(ByVal result As CsvImportResult)
        _view.ImportControl1.ImportedAt = result.ImportedAt
        _view.ImportControl1.ImportedCount = result.ImportedCount
        _view.ImportControl1.ImportedResult = result.ImportedResult
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



End Class
