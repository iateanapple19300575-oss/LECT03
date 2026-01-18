Imports Framework.Validation

''' <summary>
''' 日付の From ～ To の整合性を検証するバリデーションルール。
''' <para>
''' ・From 日付と To 日付を取得するセレクタを受け取り、  
'''   「From > To」になっていないかをチェックする  
''' ・両方の値が存在する場合のみ比較を実施  
''' ・不正な場合はエラーとして <see cref="ValidationResult"/> に追加  
''' </para>
''' 日付範囲入力の基本的な整合性チェックとして利用される。
''' </summary>
''' <typeparam name="T">検証対象エンティティの型。</typeparam>
Public Class DateRangeRule(Of T)
    Inherits ValidationRuleBase(Of T)

    ''' <summary>From 日付を取得するためのセレクタ。</summary>
    Private ReadOnly _fromSelector As Func(Of T, Date?)

    ''' <summary>To 日付を取得するためのセレクタ。</summary>
    Private ReadOnly _toSelector As Func(Of T, Date?)

    ''' <summary>エラー時に使用するフィールド名。</summary>
    Private ReadOnly _field As String

    ''' <summary>
    ''' 日付範囲チェックルールを生成する。
    ''' </summary>
    ''' <param name="field">エラー時に表示するフィールド名。</param>
    ''' <param name="fromSelector">From 日付を取得する関数。</param>
    ''' <param name="toSelector">To 日付を取得する関数。</param>
    Public Sub New(field As String, fromSelector As Func(Of T, Date?), toSelector As Func(Of T, Date?))
        _field = field
        _fromSelector = fromSelector
        _toSelector = toSelector
    End Sub

    ''' <summary>
    ''' 日付範囲の整合性を検証する。
    ''' </summary>
    ''' <param name="entity">検証対象のエンティティ。</param>
    ''' <returns>検証結果。</returns>
    Public Overrides Function Validate(entity As T) As ValidationResult
        Dim r = Result()
        Dim f = _fromSelector(entity)
        Dim t = _toSelector(entity)

        ' From > To の場合はエラー
        If f.HasValue AndAlso t.HasValue AndAlso f.Value > t.Value Then
            r.AddError(_field, "日付範囲が不正です（From > To）。", ValidationSeverity.Error)
        End If

        Return r
    End Function

End Class