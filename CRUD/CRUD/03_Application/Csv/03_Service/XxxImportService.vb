Imports Entities

''' <summary>
''' フォームコントローラ
''' </summary>
Public Class XxxImportService
    Inherits CsvImportTemplate
    Implements IImportService

    ''' <summary>
    ''' リポジトリ
    ''' </summary>
    Private ReadOnly _repo As IImportRepository

    Private connectionString As String = "Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New(csvReader As ICsvReader, validator As IValidator)
        MyBase.New(csvReader, validator)

        Dim repo As IImportRepository = New SqlXxxImportRepository(connectionString)
        _repo = repo
    End Sub

    ''' <summary>
    ''' CSV インポート実行（Template Method 呼び出し）
    ''' </summary>
    Public Function Execute(request As CsvImportRequest) As CsvImportResult _
        Implements IImportService.Execute

        ' インポート処理実行
        Dim result As CsvImportResult
        result = MyBase.ExecuteTemplate(request)

        Return result
    End Function

    ''' <summary>
    ''' CSV行データ(String())をEntityに変換する。
    ''' </summary>
    Protected Overrides Function ConvertToEntity(lines As List(Of String())) As Object
        Dim list As New List(Of XxxImportEntity)

        For Each row In lines
            ' CSV の列数チェック（例）
            If row.Length < 2 Then
                Throw New AppException("CSV の列数が不足しています。")
            End If

            Dim entity As New XxxImportEntity With {
                .Site_Code = row(0).Trim(),
                .Grade = row(1).Trim(),
                .Class_Code = row(2).Trim(),
                .Koma_Seq = row(3).Trim(),
                .Subjects = row(4).Trim()
            }

            list.Add(entity)
        Next

        Return list
    End Function

    ''' <summary>
    ''' Entity内のデータをバリデーションチェックする。
    ''' </summary>
    Protected Overrides Sub Validate(entities As Object)
        For Each entity As XxxImportEntity In DirectCast(entities, List(Of XxxImportEntity))

            If String.IsNullOrEmpty(entity.Site_Code) Then
                Throw New AppException("コードが空の行があります。")
            End If

            If entity.Site_Code.Length > 10 Then
                Throw New AppException("コードが長すぎます。（最大10桁）")
            End If

        Next
    End Sub

    ''' <summary>
    ''' BulkCopyによりEntityのデータをインポートする。)
    ''' </summary>
    Protected Overrides Function BulkCopyImport(entities As Object) As Integer
        Dim list = DirectCast(entities, List(Of XxxImportEntity))
        Return _repo.BulkInsert(list)
    End Function

    ''' <summary>
    ''' 取込日時、取込件数、取込データの情報等を取込履歴として登録する。
    ''' </summary>
    Protected Overrides Function RegisterImportHistory(entities As Object) As Integer
        'Dim list = DirectCast(entities, List(Of XxxImportEntity))
        Return 1
    End Function

End Class