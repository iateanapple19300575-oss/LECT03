Imports System.Text
Imports Framework.Core

Namespace Framework.IO.Export

    ''' <summary>
    ''' 汎用的な CSV エクスポート機能を提供するクラス。
    ''' <para>
    ''' ・列定義（<see cref="ExportDefinition(Of T)"/>）に基づきヘッダーを生成  
    ''' ・エンティティの値を 1 行ずつ CSV 形式に変換  
    ''' ・カンマやダブルクォートを含む値は適切にエスケープ  
    ''' </para>
    ''' を行い、任意の型 T のリストを CSV ファイルとして出力する。
    ''' </summary>
    ''' <typeparam name="T">エクスポート対象のエンティティ型。</typeparam>
    Public Class CsvExporter(Of T)
        Inherits ExporterBase(Of T)

        ''' <summary>
        ''' 列定義を指定して CSV エクスポーターを初期化する。
        ''' </summary>
        ''' <param name="definition">エクスポート列の定義。</param>
        Public Sub New(definition As ExportDefinition(Of T))
            MyBase.New(definition)
        End Sub

        ''' <summary>
        ''' 指定されたデータを CSV ファイルとして出力する。
        ''' </summary>
        ''' <param name="path">出力先ファイルパス。</param>
        ''' <param name="data">エクスポート対象データ。</param>
        ''' <exception cref="Exception">書き込み中にエラーが発生した場合。</exception>
        Public Overrides Sub Export(path As String, data As IEnumerable(Of T))
            Try
                Dim sb As New StringBuilder()

                ' ============================
                ' ヘッダー行の生成
                ' ============================
                sb.AppendLine(String.Join(",", _definition.Columns.ConvertAll(Function(c) c.Header).ToArray()))

                ' ============================
                ' データ行の生成
                ' ============================
                For Each item In data
                    Dim row As New List(Of String)

                    For Each col In _definition.Columns
                        Dim val = col.ValueSelector(item)
                        row.Add(Escape(val))
                    Next

                    sb.AppendLine(String.Join(",", row.ToArray()))
                Next

                ' ============================
                ' ファイル出力
                ' ============================
                FileHelper.WriteAllText(path, sb.ToString())

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "CsvExporter.Export"))
            End Try
        End Sub

        ''' <summary>
        ''' CSV の仕様に従って値をエスケープする。
        ''' <para>
        ''' ・カンマまたはダブルクォートを含む場合はダブルクォートで囲む  
        ''' ・内部のダブルクォートは 2 つにエスケープ  
        ''' </para>
        ''' </summary>
        ''' <param name="value">エスケープ対象の値。</param>
        ''' <returns>CSV 形式として安全な文字列。</returns>
        Private Function Escape(value As Object) As String
            If value Is Nothing Then Return ""

            Dim s = value.ToString()

            If s.Contains(",") OrElse s.Contains("""") Then
                s = """" & s.Replace("""", """""") & """"
            End If

            Return s
        End Function

    End Class

End Namespace