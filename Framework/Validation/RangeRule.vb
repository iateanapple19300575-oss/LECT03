Imports Framework.Validation

''' <summary>
''' 数値が指定された範囲内に収まっているかを検証するバリデーションルール。
''' <para>
''' ・任意のエンティティ T から数値を取得するセレクタを使用  
''' ・最小値～最大値の範囲外であればエラーを追加  
''' ・範囲チェックの基本ルールとして汎用的に利用可能  
''' </para>
''' </summary>
''' <typeparam name="T">検証対象エンティティの型。</typeparam>
Public Class RangeRule(Of T)
    Inherits ValidationRuleBase(Of T)

    ''' <summary>検証対象の数値を取得するためのセレクタ。</summary>
    Private ReadOnly _selector As Func(Of T, Decimal)

    ''' <summary>エラー時に使用するフィールド名。</summary>
    Private ReadOnly _field As String

    ''' <summary>許容される最小値。</summary>
    Private ReadOnly _min As Decimal

    ''' <summary>許容される最大値。</summary>
    Private ReadOnly _max As Decimal

    ''' <summary>
    ''' 数値範囲チェックルールを生成する。
    ''' </summary>
    ''' <param name="field">エラー時に表示するフィールド名。</param>
    ''' <param name="selector">検証対象の数値を取得する関数。</param>
    ''' <param name="min">許容される最小値。</param>
    ''' <param name="max">許容される最大値。</param>
    Public Sub New(field As String, selector As Func(Of T, Decimal), min As Decimal, max As Decimal)
        _field = field
        _selector = selector
        _min = min
        _max = max
    End Sub

    ''' <summary>
    ''' 数値が指定範囲内に収まっているかを検証する。
    ''' </summary>
    ''' <param name="entity">検証対象のエンティティ。</param>
    ''' <returns>検証結果。</returns>
    Public Overrides Function Validate(entity As T) As ValidationResult
        Dim r = Result()
        Dim v = _selector(entity)

        ' 範囲外の場合はエラー
        If v < _min OrElse v > _max Then
            r.AddError(_field,
                       $"{_field} は {_min} ～ {_max} の範囲で入力してください。",
                       ValidationSeverity.Error)
        End If

        Return r
    End Function

End Class