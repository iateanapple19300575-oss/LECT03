Imports Framework.Core

Namespace Framework.IO.Csv

    ''' <summary>
    ''' CSV インポート処理の共通基底クラス。
    ''' <para>
    ''' ・ヘッダー検証  
    ''' ・行ごとのパース  
    ''' ・エラー収集  
    ''' ・Strategy パターンによる型変換  
    ''' </para>
    ''' を統一し、CSV 形式が異なる複数のインポート処理を拡張しやすくする。
    ''' </summary>
    ''' <typeparam name="TEntity">CSV 1 行を変換したエンティティ型。</typeparam>
    Public MustInherit Class CsvImporterBase(Of TEntity)

        ''' <summary>
        ''' 行変換・ヘッダー定義を提供する Strategy。
        ''' </summary>
        Private ReadOnly _strategy As ICsvStrategy(Of TEntity)

        ''' <summary>
        ''' Strategy を指定してインポーターを初期化する。
        ''' </summary>
        ''' <param name="strategy">CSV 行変換ロジックを提供する Strategy。</param>
        Protected Sub New(strategy As ICsvStrategy(Of TEntity))
            _strategy = strategy
        End Sub

        ''' <summary>
        ''' CSV ファイルを読み込み、ヘッダー検証と行変換を行う。
        ''' </summary>
        ''' <param name="filePath">読み込む CSV ファイルのパス。</param>
        ''' <returns>
        ''' 成功・失敗、変換済みエンティティ一覧、エラー情報を含む <see cref="CsvResult(Of TEntity)"/>。
        ''' </returns>
        Public Function Import(filePath As String) As CsvResult(Of TEntity)
            Dim result As New CsvResult(Of TEntity)()

            Try
                Dim lines = System.IO.File.ReadAllLines(filePath)

                ' ============================
                ' 0. 空ファイルチェック
                ' ============================
                If lines.Length = 0 Then
                    result.Success = False
                    result.Errors.Add(New CsvValidationError With {
                        .LineNumber = 0,
                        .Message = "CSV が空です。"
                    })
                    Return result
                End If

                ' ============================
                ' 1. ヘッダー検証
                ' ============================
                Dim header = lines(0).Split(","c)

                If Not ValidateHeader(header, _strategy.ExpectedHeaders) Then
                    result.Success = False
                    result.Errors.Add(New CsvValidationError With {
                        .LineNumber = 1,
                        .Message = "ヘッダーが一致しません。"
                    })
                    Return result
                End If

                ' ============================
                ' 2. 行ごとの処理
                ' ============================
                For i As Integer = 1 To lines.Length - 1
                    Dim line = lines(i)
                    If StringUtil.IsNullOrWhiteSpace(line) Then Continue For

                    Dim fields = line.Split(","c)

                    Try
                        Dim entity = _strategy.ConvertRow(fields, i + 1)
                        result.Entities.Add(entity)

                    Catch ex As Exception
                        result.Success = False
                        result.Errors.Add(New CsvValidationError With {
                            .LineNumber = i + 1,
                            .Message = ex.Message
                        })
                    End Try
                Next

            Catch ex As Exception
                result.Success = False
                result.Errors.Add(New CsvValidationError With {
                    .LineNumber = 0,
                    .Message = ErrorHandler.Handle(ex)
                })
            End Try

            Return result
        End Function

        ' ============================
        ' ヘッダー検証
        ' ============================

        ''' <summary>
        ''' CSV のヘッダー行が期待される形式と一致するか検証する。
        ''' </summary>
        ''' <param name="actual">CSV の実際のヘッダー。</param>
        ''' <param name="expected">Strategy が定義する期待ヘッダー。</param>
        ''' <returns>一致する場合 True。</returns>
        Private Function ValidateHeader(actual As String(), expected As String()) As Boolean
            If actual.Length <> expected.Length Then Return False

            For i As Integer = 0 To expected.Length - 1
                If actual(i).Trim() <> expected(i).Trim() Then
                    Return False
                End If
            Next

            Return True
        End Function

    End Class

End Namespace