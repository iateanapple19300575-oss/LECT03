Namespace Framework.IO.Csv

    ''' <summary>
    ''' CSV インポート処理の結果を保持するクラス。
    ''' <para>
    ''' ・インポート成功／失敗  
    ''' ・変換済みエンティティ一覧  
    ''' ・行単位のバリデーションエラー一覧  
    ''' </para>
    ''' をまとめて管理し、呼び出し側が処理結果を判断しやすくする。
    ''' </summary>
    ''' <typeparam name="TEntity">CSV 行から生成されるエンティティ型。</typeparam>
    Public Class CsvResult(Of TEntity)

        ''' <summary>
        ''' インポート処理が成功したかどうかを示す。
        ''' True の場合は致命的エラーがなく、エンティティが正常に生成されている。
        ''' </summary>
        Public Property Success As Boolean

        ''' <summary>
        ''' CSV の各行から生成されたエンティティの一覧。
        ''' エラー行は含まれない。
        ''' </summary>
        Public Property Entities As List(Of TEntity)

        ''' <summary>
        ''' 行単位で発生したバリデーションエラーの一覧。
        ''' ヘッダー不一致や変換エラーなどが含まれる。
        ''' </summary>
        Public Property Errors As List(Of CsvValidationError)

        ''' <summary>
        ''' 新しい結果オブジェクトを初期化する。
        ''' 初期状態では Success=True とし、空のリストを生成する。
        ''' </summary>
        Public Sub New()
            Entities = New List(Of TEntity)()
            Errors = New List(Of CsvValidationError)()
            Success = True
        End Sub

    End Class

End Namespace