Imports Framework.Validation

''' <summary>
''' 文字列が指定された正規表現パターンに一致するかを検証するバリデーションルール。
''' <para>
''' ・任意のエンティティ T から文字列を取得するセレクタを使用  
''' ・正規表現パターンに一致しない場合はエラーを追加  
''' ・メール形式チェックやコード形式チェックなど幅広く利用可能  
''' </para>
''' </summary>
''' <typeparam name="T">検証対象エンティティの型。</typeparam>
Public Class RegexRule(Of T)
    Inherits ValidationRuleBase(Of T)

    ''' <summary>検証対象の文字列を取得するためのセレクタ。</summary>
    Private ReadOnly _selector As Func(Of T, String)

    ''' <summary>エラー時に使用するフィールド名。</summary>
    Private ReadOnly _field As String

    ''' <summary>一致を要求する正規表現パターン。</summary>
    Private ReadOnly _pattern As String

    ''' <summary>
    ''' 正規表現チェックルールを生成する。
    ''' </summary>
    ''' <param name="field">エラー時に表示するフィールド名。</param>
    ''' <param name="selector">検証対象の文字列を取得する関数。</param>
    ''' <param name="pattern">一致を要求する正規表現パターン。</param>
    Public Sub New(field As String, selector As Func(Of T, String), pattern As String)
        _field = field
        _selector = selector
        _pattern = pattern
    End Sub

    ''' <summary>
    ''' 文字列が正規表現パターンに一致するかを検証する。
    ''' </summary>
    ''' <param name="entity">検証対象のエンティティ。</param>
    ''' <returns>検証結果。</returns>
    Public Overrides Function Validate(entity As T) As ValidationResult
        Dim r = Result()
        Dim v = _selector(entity)

        ' パターン不一致の場合はエラー
        If Not System.Text.RegularExpressions.Regex.IsMatch(v, _pattern) Then
            r.AddError(_field,
                       $"{_field} の形式が正しくありません。",
                       ValidationSeverity.Error)
        End If

        Return r
    End Function

End Class