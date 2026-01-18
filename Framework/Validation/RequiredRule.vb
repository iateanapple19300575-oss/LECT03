Imports Framework.Core
Imports Framework.Validation

''' <summary>
''' 必須入力チェックを行うバリデーションルール。
''' <para>
''' ・任意のエンティティ T から値を取得するセレクタを使用  
''' ・Nothing または空文字（空白のみ含む場合も含む）の場合にエラーを追加  
''' ・文字列以外の型にも対応（Nothing 判定）  
''' </para>
''' 入力必須項目の基本的なチェックとして広く利用される。
''' </summary>
''' <typeparam name="T">検証対象エンティティの型。</typeparam>
Public Class RequiredRule(Of T)
    Inherits ValidationRuleBase(Of T)

    ''' <summary>検証対象の値を取得するためのセレクタ。</summary>
    Private ReadOnly _selector As Func(Of T, Object)

    ''' <summary>エラー時に使用するフィールド名。</summary>
    Private ReadOnly _field As String

    ''' <summary>
    ''' 必須チェックルールを生成する。
    ''' </summary>
    ''' <param name="field">エラー時に表示するフィールド名。</param>
    ''' <param name="selector">検証対象の値を取得する関数。</param>
    Public Sub New(field As String, selector As Func(Of T, Object))
        _field = field
        _selector = selector
    End Sub

    ''' <summary>
    ''' 値が入力されているかを検証する。
    ''' </summary>
    ''' <param name="entity">検証対象のエンティティ。</param>
    ''' <returns>検証結果。</returns>
    Public Overrides Function Validate(entity As T) As ValidationResult
        Dim r = Result()
        Dim v = _selector(entity)

        ' Nothing または空文字の場合はエラー
        If v Is Nothing OrElse (TypeOf v Is String AndAlso StringUtil.IsNullOrWhiteSpace(v.ToString())) Then
            r.AddError(_field,
                       $"{_field} は必須です。",
                       ValidationSeverity.Error)
        End If

        Return r
    End Function

End Class