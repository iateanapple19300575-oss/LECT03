' CsvImportTemplate.vb
Imports System.Linq

Public MustInherit Class CsvImportTemplate(Of T)

    Protected MustOverride Function GetRowFactory() As IRowFactory(Of T)
    Protected MustOverride Function GetValidators() As IEnumerable(Of IValidator(Of T))
    Protected MustOverride Function GetRepository() As IImportRepository(Of T)

    Private ReadOnly _detector As IEncodingDetector
    Private ReadOnly _reader As ICsvReader

    Public Sub New(detector As IEncodingDetector, reader As ICsvReader)
        _detector = detector
        _reader = reader
    End Sub

    Public Function Execute(path As String) As ImportResult
        Try
            ' 1. 文字コード判定
            Dim enc = _detector.Detect(path)

            ' 2. CSV読み込み
            Dim rows = _reader.Read(path, enc)

            ' 3. DTO生成
            Dim factory = GetRowFactory()
            Dim dtos = rows.Select(Function(r) factory.Create(r)).ToList()

            ' 4. バリデーション
            Dim validators = GetValidators()
            Dim errors As New List(Of ValidationError)

            Dim rowIndex As Integer = 1
            For Each dto In dtos
                For Each v In validators
                    For Each Err1 In v.Validate(dto)
                        errors.Add(New ValidationError(rowIndex, Err1.FieldName, Err1.Message))
                    Next
                Next
                rowIndex += 1
            Next

            If errors.Any() Then
                Return ImportResult.Fail(errors)
            End If

            ' 5〜7. トランザクション実行
            Dim repo = GetRepository()
            repo.ExecuteWithTransaction(dtos, dtos.Count)

            Return ImportResult.Success(dtos.Count)

            '===========================
            ' 例外の種類ごとにメッセージを変える
            '===========================

        Catch ex As SqlClient.SqlException
            'Return ImportResult.Fail({
            '    New ValidationError(0, "DB", "データベース処理中にエラーが発生しました：" & ex.Message)
            '})

            Select Case ex.Number
                Case 2627 ' PK重複
                    Return ImportResult.Fail({New ValidationError(0, "DB", "重複データが存在します。")})
                Case Else
                    Return ImportResult.Fail({New ValidationError(0, "DB", "DBエラー：" & ex.Message)})
            End Select


        Catch ex As IO.IOException
            Return ImportResult.Fail({
                New ValidationError(0, "File", "CSVファイルを読み込めませんでした。ファイルが使用中の可能性があります。")
            })

        Catch ex As FormatException
            Return ImportResult.Fail({
                New ValidationError(0, "Format", "CSVの形式が正しくありません。数値または日付の変換に失敗しました。")
            })

        Catch ex As Exception
            ' その他の予期しない例外
            Return ImportResult.Fail({
                New ValidationError(0, "System", "予期しないエラーが発生しました：" & ex.Message)
            })

        End Try
    End Function
End Class