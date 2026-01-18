Namespace Framework.Validation

    ''' <summary>
    ''' 単一フィールドに対する複数のバリデーションルールを
    ''' チェーン形式で組み立てるためのビルダークラス。
    ''' <para>
    ''' ・NotEmpty / MaxLength / Range などの基本ルールを簡潔に定義  
    ''' ・Custom により任意の検証ロジックも追加可能  
    ''' ・Execute にて登録済みルールを順次実行し、エラーを <see cref="ValidationResult"/> に追加  
    ''' </para>
    ''' フィールド単位の柔軟なバリデーション構築を支援する。
    ''' </summary>
    ''' <typeparam name="T">検証対象エンティティの型。</typeparam>
    Public Class RuleBuilder(Of T)

        ''' <summary>エラー時に使用するフィールド名。</summary>
        Private ReadOnly _field As String

        ''' <summary>検証対象の値を取得するための関数。</summary>
        Private ReadOnly _getter As Func(Of T, Object)

        ''' <summary>登録されたバリデーション関数の一覧。</summary>
        Private ReadOnly _validators As New List(Of Func(Of Object, String))

        ''' <summary>
        ''' RuleBuilder を生成する。
        ''' </summary>
        ''' <param name="field">エラー時に表示するフィールド名。</param>
        ''' <param name="getter">検証対象の値を取得する関数。</param>
        Public Sub New(field As String, getter As Func(Of T, Object))
            _field = field
            _getter = getter
        End Sub

        ''' <summary>
        ''' 値が空でないことを検証するルールを追加する。
        ''' </summary>
        ''' <param name="message">エラーメッセージ。</param>
        ''' <returns>自身のインスタンス。</returns>
        Public Function NotEmpty(message As String) As RuleBuilder(Of T)
            _validators.Add(Function(v)
                                If v Is Nothing OrElse v.ToString().Trim() = "" Then
                                    Return message
                                End If
                                Return Nothing
                            End Function)
            Return Me
        End Function

        ''' <summary>
        ''' 最大文字数を超えていないことを検証するルールを追加する。
        ''' </summary>
        ''' <param name="length">最大文字数。</param>
        ''' <param name="message">エラーメッセージ。</param>
        ''' <returns>自身のインスタンス。</returns>
        Public Function MaxLength(length As Integer, message As String) As RuleBuilder(Of T)
            _validators.Add(Function(v)
                                If v IsNot Nothing AndAlso v.ToString().Length > length Then
                                    Return message
                                End If
                                Return Nothing
                            End Function)
            Return Me
        End Function

        ''' <summary>
        ''' 数値が指定範囲内に収まっているかを検証するルールを追加する。
        ''' </summary>
        ''' <param name="min">最小値。</param>
        ''' <param name="max">最大値。</param>
        ''' <param name="message">エラーメッセージ。</param>
        ''' <returns>自身のインスタンス。</returns>
        Public Function Range(min As Decimal, max As Decimal, message As String) As RuleBuilder(Of T)
            _validators.Add(Function(v)
                                If v Is Nothing Then Return Nothing
                                Dim num As Decimal
                                If Decimal.TryParse(v.ToString(), num) Then
                                    If num < min OrElse num > max Then
                                        Return message
                                    End If
                                End If
                                Return Nothing
                            End Function)
            Return Me
        End Function

        ''' <summary>
        ''' 任意のカスタムバリデーションを追加する。
        ''' </summary>
        ''' <param name="validator">値を受け取り、エラーメッセージまたは Nothing を返す関数。</param>
        ''' <returns>自身のインスタンス。</returns>
        Public Function Custom(validator As Func(Of Object, String)) As RuleBuilder(Of T)
            _validators.Add(validator)
            Return Me
        End Function

        ''' <summary>
        ''' 登録されたすべてのバリデーションを実行し、結果にエラーを追加する。
        ''' </summary>
        ''' <param name="entity">検証対象のエンティティ。</param>
        ''' <param name="result">検証結果を格納する <see cref="ValidationResult"/>。</param>
        Friend Sub Execute(entity As T, result As ValidationResult)
            Dim value = _getter(entity)

            For Each v In _validators
                Dim err = v(value)
                If err IsNot Nothing Then
                    result.AddError(_field, err)
                End If
            Next
        End Sub

    End Class

End Namespace