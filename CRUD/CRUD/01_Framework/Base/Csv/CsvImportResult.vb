''' <summary>
''' インポート処理の結果を保持する DTO。
''' 成功・警告・失敗の状態と、件数・メッセージ・実行時刻をまとめて返します。
''' </summary>
''' <remarks>
''' CsvImportTemplate や各種インポート処理の戻り値として使用されます。<br/>
''' 成功・警告・失敗を統一的に扱えるよう、静的ファクトリメソッドを提供します。
''' </remarks>
Public Class CsvImportResult

    ''' <summary>
    ''' 処理が成功したかどうかを示す値を取得または設定します。
    ''' </summary>
    ''' <returns>成功時は True、失敗時は False。</returns>
    Public Property IsSuccess As Boolean

    ''' <summary>
    ''' 成功だが警告が発生したかどうかを示す値を取得または設定します。
    ''' </summary>
    ''' <returns>警告がある場合は True。</returns>
    Public Property IsWarning As Boolean

    ''' <summary>
    ''' 処理が実行された日時を取得または設定します。
    ''' </summary>
    ''' <returns>処理実行日時。</returns>
    Public Property ImportedAt As DateTime

    ''' <summary>
    ''' 成功または警告時のインポート件数を取得または設定します。
    ''' </summary>
    ''' <returns>インポートされた件数。</returns>
    Public Property ImportedCount As Integer

    ''' <summary>
    ''' 成功・警告・失敗に関するメッセージを取得または設定します。
    ''' </summary>
    ''' <returns>ユーザー向けのメッセージ。</returns>
    Public Property ImportedResult As String

    '-----------------------------------------
    ' 静的ファクトリメソッド
    '-----------------------------------------

    ''' <summary>
    ''' 成功状態の ImportResult を生成します。
    ''' </summary>
    ''' <param name="count">インポート件数。</param>
    ''' <returns>成功状態の ImportResult。</returns>
    Public Shared Function Success(count As Integer) As CsvImportResult
        Return New CsvImportResult With {
            .IsSuccess = True,
            .IsWarning = False,
            .ImportedAt = DateTime.Now,
            .ImportedCount = count,
            .ImportedResult = "成功しました。"
        }
    End Function

    ''' <summary>
    ''' 警告付き成功状態の ImportResult を生成します。
    ''' </summary>
    ''' <param name="count">インポート件数。</param>
    ''' <param name="msg">警告メッセージ。</param>
    ''' <returns>警告状態の ImportResult。</returns>
    Public Shared Function Warning(count As Integer, msg As String) As CsvImportResult
        Return New CsvImportResult With {
            .IsSuccess = True,
            .IsWarning = True,
            .ImportedAt = DateTime.Now,
            .ImportedCount = count,
            .ImportedResult = msg
        }
    End Function

    ''' <summary>
    ''' 失敗状態の ImportResult を生成します。
    ''' </summary>
    ''' <param name="msg">エラーメッセージ。</param>
    ''' <returns>失敗状態の ImportResult。</returns>
    Public Shared Function Fail(msg As String) As CsvImportResult
        Return New CsvImportResult With {
            .IsSuccess = False,
            .IsWarning = False,
            .ImportedAt = DateTime.Now,
            .ImportedCount = 0,
            .ImportedResult = msg
        }
    End Function

End Class