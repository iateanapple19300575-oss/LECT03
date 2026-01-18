Imports Framework.IO.Export

''' <summary>
''' エクスポート処理を簡潔に利用するためのサービスクラス。
''' <para>
''' ・CSV 出力  
''' ・Excel（HTML テーブル形式）出力  
''' </para>
''' を統一されたインターフェイスで提供し、呼び出し側が
''' ExporterBase の具体実装を意識せずに利用できるようにする。
''' </summary>
''' <typeparam name="T">エクスポート対象エンティティの型。</typeparam>
Public Class ExportService(Of T)

    ''' <summary>
    ''' エクスポート列定義。
    ''' </summary>
    Private ReadOnly _definition As ExportDefinition(Of T)

    ''' <summary>
    ''' 列定義を指定してサービスを初期化する。
    ''' </summary>
    ''' <param name="definition">エクスポート列の定義。</param>
    Public Sub New(definition As ExportDefinition(Of T))
        _definition = definition
    End Sub

    ''' <summary>
    ''' データを CSV 形式で出力する。
    ''' </summary>
    ''' <param name="path">出力先ファイルパス。</param>
    ''' <param name="data">エクスポート対象データ。</param>
    Public Sub ExportCsv(path As String, data As IEnumerable(Of T))
        Dim exporter As New CsvExporter(Of T)(_definition)
        exporter.Export(path, data)
    End Sub

    ''' <summary>
    ''' データを Excel（HTML テーブル形式）で出力する。
    ''' </summary>
    ''' <param name="path">出力先ファイルパス。</param>
    ''' <param name="data">エクスポート対象データ。</param>
    Public Sub ExportExcel(path As String, data As IEnumerable(Of T))
        Dim exporter As New ExcelExporter(Of T)(_definition)
        exporter.Export(path, data)
    End Sub

End Class