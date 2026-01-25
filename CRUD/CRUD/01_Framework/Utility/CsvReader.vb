Imports System.IO
Imports System.Text

''' <summary>
''' CSV ファイルを読み込み、1 行ごとにフィールドへ分割した結果を返すユーティリティクラス。
''' </summary>
Public Class CsvReader
    Implements ICsvReader

    Private ReadOnly Property CsvClass As ICsvFile

    Public Sub New(ByVal csvFile As CsvFile)
        Me.CsvClass = csvFile
    End Sub

    ''' <summary>
    ''' 指定されたCSVファイルを読み込み、各行をString配列として返す。
    ''' </summary>
    ''' <param name="filePath">読み込むCSVファイルのパス。</param>
    ''' <returns>CSVの各行を表すString()のリスト。</returns>
    ''' <remarks>
    ''' ・BOMを判定して、UTF-8/Shift_JISを自動判別する。<br/>
    ''' ・空行はスキップする。<br/>
    ''' ・ダブルクォートで囲まれたフィールドにも対応しています。
    ''' </remarks>
    Public Shared Function Read(filePath As String) As List(Of String())
        Dim result As New List(Of String())()

        Using sr As New StreamReader(filePath, DetectEncoding(filePath))
            While Not sr.EndOfStream
                Dim line = sr.ReadLine()

                If String.IsNullOrEmpty(line) Then
                    Continue While
                End If

                result.Add(ParseLine(line))
            End While
        End Using

        Return result
    End Function

    ''' <summary>
    ''' ファイルのBOMを確認し、UTF-8または、Shift_JISのエンコーディングを返す。
    ''' </summary>
    ''' <param name="filePath">判定対象のファイルパス。</param>
    ''' <returns>判定された Encoding オブジェクト。</returns>
    ''' <remarks>
    ''' UTF-8 BOM（EF BB BF）が存在する場合は UTF-8 として扱う。
    ''' それ以外は Shift_JIS として扱う。
    ''' </remarks>
    Private Shared Function DetectEncoding(filePath As String) As Encoding
        Dim bom = New Byte(2) {}
        Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
            fs.Read(bom, 0, 3)
        End Using

        If bom(0) = &HEF AndAlso bom(1) = &HBB AndAlso bom(2) = &HBF Then
            Return New UTF8Encoding(True)
        End If

        Return Encoding.GetEncoding("Shift_JIS")
    End Function

    ''' <summary>
    ''' CSVの1 行を解析し、フィールドごとに分割して返す。
    ''' </summary>
    ''' <param name="line">解析対象の1行の文字列。</param>
    ''' <returns>分割されたフィールドの配列。</returns>
    ''' <remarks>
    ''' ・ダブルクォートで囲まれたフィールドに対応。<br/>
    ''' ・エスケープされた二重引用符（""）も処理。<br/>
    ''' ・カンマは引用符の外にある場合のみ区切りとして扱う。
    ''' </remarks>
    Private Shared Function ParseLine(ByVal line As String) As String()
        Dim fields As New List(Of String)()
        Dim sb As New StringBuilder()
        Dim inQuote As Boolean = False

        For i As Integer = 0 To line.Length - 1
            Dim c = line(i)

            If c = """"c Then
                If inQuote AndAlso i < line.Length - 1 AndAlso line(i + 1) = """"c Then
                    sb.Append(""""c)
                    i += 1
                Else
                    inQuote = Not inQuote
                End If

            ElseIf c = ","c AndAlso Not inQuote Then
                fields.Add(sb.ToString())
                sb.Length = 0

            Else
                sb.Append(c)
            End If
        Next

        fields.Add(sb.ToString())
        Return fields.ToArray()
    End Function

    Public Shared Function Read(csv As ICsvFile) As List(Of String())
        Dim result As New List(Of String())()

        Using sr As New StreamReader(csv.FilePath, DetectEncoding(csv.FilePath))
            While Not sr.EndOfStream
                Dim line = sr.ReadLine()

                If String.IsNullOrEmpty(line) Then
                    Continue While
                End If

                result.Add(ParseLine(line))
            End While
        End Using

        Return result
    End Function

End Class