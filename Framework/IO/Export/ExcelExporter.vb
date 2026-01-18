Imports System.Text
Imports Framework.Core

Namespace Framework.IO.Export

    ''' <summary>
    ''' Excel（互換 HTML 形式）へのエクスポートを行うクラス。
    ''' <para>
    ''' ・列定義（<see cref="ExportDefinition(Of T)"/>）に基づきヘッダーを生成  
    ''' ・データを HTML テーブル形式に変換  
    ''' ・Excel で開ける形式としてファイル出力  
    ''' </para>
    ''' を行う。
    ''' </summary>
    ''' <typeparam name="T">エクスポート対象のエンティティ型。</typeparam>
    Public Class ExcelExporter(Of T)
        Inherits ExporterBase(Of T)

        ''' <summary>
        ''' 列定義を指定して Excel エクスポーターを初期化する。
        ''' </summary>
        ''' <param name="definition">エクスポート列の定義。</param>
        Public Sub New(definition As ExportDefinition(Of T))
            MyBase.New(definition)
        End Sub

        ''' <summary>
        ''' 指定されたデータを Excel（HTML テーブル形式）として出力する。
        ''' </summary>
        ''' <param name="path">出力先ファイルパス。</param>
        ''' <param name="data">エクスポート対象データ。</param>
        ''' <exception cref="Exception">出力処理中にエラーが発生した場合。</exception>
        Public Overrides Sub Export(path As String, data As IEnumerable(Of T))
            Try
                Dim sb As New StringBuilder()

                sb.AppendLine("<table border='1'>")

                ' ============================
                ' ヘッダー行
                ' ============================
                sb.Append("<tr>")
                For Each col In _definition.Columns
                    sb.AppendFormat("<th>{0}</th>", col.Header)
                Next
                sb.AppendLine("</tr>")

                ' ============================
                ' データ行
                ' ============================
                For Each item In data
                    sb.Append("<tr>")
                    For Each col In _definition.Columns
                        Dim val = col.ValueSelector(item)
                        sb.AppendFormat("<td>{0}</td>", val)
                    Next
                    sb.AppendLine("</tr>")
                Next

                sb.AppendLine("</table>")

                ' ============================
                ' ファイル出力
                ' ============================
                FileHelper.WriteAllText(path, sb.ToString())

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "ExcelExporter.Export"))
            End Try
        End Sub

    End Class

End Namespace