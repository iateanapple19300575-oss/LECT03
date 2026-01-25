''' <summary>
''' CSV インポート処理のテンプレート基底クラス。
''' Template Method パターンにより、共通フロー（読込 → 変換 → 検証 → 登録）を提供し、
''' 派生クラスは変換・検証・登録処理のみを実装します。
''' </summary>
Public MustInherit Class CsvImportTemplate

    Private _csvReader As ICsvReader

    Private _validator As IValidator


    Public Sub New(
                  csvReader As ICsvReader,
                  validator As IValidator)
        _csvReader = csvReader
        _validator = validator

    End Sub


    ''' <summary>
    ''' CSV インポート処理の共通フローを実行します。
    ''' </summary>
    ''' <param name="request">インポート対象ファイルの情報を保持する DTO。</param>
    ''' <returns>成功・失敗・件数などを保持する ImportResult。</returns>
    ''' <remarks>
    ''' 処理の流れは以下の通りです。<br/>
    ''' 1. CSV ファイル読込<br/>
    ''' 2. エンティティ変換（ConvertToEntity）<br/>
    ''' 3. 業務ルール検証（Validate）<br/>
    ''' 4. 登録処理（Register）<br/>
    ''' <br/>
    ''' AppException は業務エラーとして扱い、メッセージをそのまま返します。<br/>
    ''' その他の例外は「予期せぬエラー」として扱います。
    ''' </remarks>
    Public Function ExecuteTemplate(request As CsvImportRequest) As CsvImportResult
        Try
            ' 取込対象のCSVデータを読み込む
            Dim lines = CsvReader.Read(request.FilePath)

            ' CSVデータエンティティ変換
            Dim entities = ConvertToEntity(lines)

            ' CSVデータのバリデーション
            Validate(entities)

            ' 重複エラー回避するため同一期間データを削除する
            DeleteTatgetData(entities)

            ' CSVインポート(BulkCopy)
            Dim count = BulkCopyImport(entities)

            ' 履歴登録
            RegisterImportHistory(entities)

            ' 処理結果(SUCCESS)を返す。
            Return CsvImportResult.Success(count)

        Catch ex As AppException
            Return CsvImportResult.Fail(ex.Message)

        Catch ex As Exception
            Return CsvImportResult.Fail("予期せぬエラーが発生しました。")
        End Try
    End Function

    ''' <summary>
    ''' インポート履歴登録
    ''' </summary>
    ''' <returns></returns>
    Protected Overridable Function RegisterImportHistory(entities As Object) As Integer
        Return 1
    End Function

    ''' <summary>
    ''' 重複エラー回避の為取込同一期間のデータを削除する。
    ''' </summary>
    ''' <returns></returns>
    Protected Overridable Function DeleteTatgetData(entities As Object) As Integer
        Return 1
    End Function



    ''' <summary>
    ''' CSV の行データを業務エンティティへ変換します。
    ''' </summary>
    ''' <param name="lines">CSV の全行（1 行 = String 配列）。</param>
    ''' <returns>変換後のエンティティ一覧。</returns>
    ''' <remarks>
    ''' 派生クラスで CSV の構造に応じた変換処理を実装します。
    ''' </remarks>
    Protected MustOverride Function ConvertToEntity(lines As List(Of String())) As Object

    ''' <summary>
    ''' 変換後のエンティティに対して業務ルール検証を行います。
    ''' </summary>
    ''' <param name="entities">変換済みエンティティ一覧。</param>
    ''' <remarks>
    ''' 不正データがある場合は AppException をスローしてください。
    ''' </remarks>
    Protected MustOverride Sub Validate(entities As Object)

    ''' <summary>
    ''' エンティティをデータベースへ登録します。
    ''' </summary>
    ''' <param name="entities">登録対象のエンティティ一覧。</param>
    ''' <returns>登録件数。</returns>
    ''' <remarks>
    ''' トランザクション管理は派生クラス側で行うことを推奨します。
    ''' </remarks>
    Protected MustOverride Function BulkCopyImport(entities As Object) As Integer

End Class