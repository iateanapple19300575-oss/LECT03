''' <summary>
''' CSV インポート処理の入力情報を保持するリクエスト DTO。
''' </summary>
''' <remarks>
''' CsvImportTemplate.ExecuteTemplate に渡される入力データを表します。<br/>
''' 主にインポート対象ファイルのパスを保持します。
''' </remarks>
Public Class CsvImportRequest

    ''' <summary>
    ''' インポート対象となる CSV ファイルのパスを取得または設定します。
    ''' </summary>
    ''' <returns>CSV ファイルのフルパス。</returns>
    Public Property FilePath As String

End Class