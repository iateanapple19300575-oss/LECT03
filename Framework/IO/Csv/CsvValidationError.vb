Namespace Framework.IO.Csv

    ''' <summary>
    ''' CSV インポート時に発生した行単位のエラー情報を表すクラス。
    ''' <para>
    ''' ・エラーが発生した行番号  
    ''' ・エラーメッセージ  
    ''' </para>
    ''' を保持し、呼び出し側がどの行で何が起きたかを特定しやすくする。
    ''' </summary>
    Public Class CsvValidationError

        ''' <summary>
        ''' エラーが発生した CSV の行番号。
        ''' 1 行目はヘッダーとして扱われるため、通常は 2 行目以降が対象となる。
        ''' </summary>
        Public Property LineNumber As Integer

        ''' <summary>
        ''' エラー内容を説明するメッセージ。
        ''' 例：必須項目の欠落、型変換エラー、フォーマット不正など。
        ''' </summary>
        Public Property Message As String

    End Class

End Namespace