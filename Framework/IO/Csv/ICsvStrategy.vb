Namespace Framework.IO.Csv

    ''' <summary>
    ''' CSV インポート処理における Strategy パターンのインターフェイス。
    ''' <para>
    ''' ・期待されるヘッダー定義  
    ''' ・1 行分のフィールド配列をエンティティへ変換する処理  
    ''' </para>
    ''' を提供し、CSVImporterBase が共通フローを維持しつつ、
    ''' CSV の種類ごとに柔軟な変換ロジックを差し替えられるようにする。
    ''' </summary>
    ''' <typeparam name="TEntity">CSV 行から生成されるエンティティ型。</typeparam>
    Public Interface ICsvStrategy(Of TEntity)

        ''' <summary>
        ''' CSV のヘッダー行として期待される列名一覧。
        ''' <para>
        ''' CsvImporterBase によってヘッダー検証に使用される。
        ''' </para>
        ''' </summary>
        ReadOnly Property ExpectedHeaders As String()

        ''' <summary>
        ''' CSV の 1 行（フィールド配列）をエンティティへ変換する。
        ''' </summary>
        ''' <param name="fields">カンマ区切りで分割されたフィールド配列。</param>
        ''' <param name="lineNumber">CSV 上の行番号（エラー時のメッセージ用）。</param>
        ''' <returns>変換されたエンティティ。</returns>
        ''' <exception cref="Exception">変換に失敗した場合、適切なメッセージを含む例外をスローする。</exception>
        Function ConvertRow(fields As String(), lineNumber As Integer) As TEntity

    End Interface

End Namespace