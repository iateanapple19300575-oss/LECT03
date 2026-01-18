Namespace Framework.Validation

    ''' <summary>
    ''' 複数のバリデーションルールをまとめて実行するための Validator クラス。
    ''' <para>
    ''' ・AddRule で任意の <see cref="IValidationRule(Of T)"/> を登録  
    ''' ・Validate で全ルールを順次実行し、エラーを集約  
    ''' ・各ルールの結果を統合して <see cref="ValidationResult"/> を返す  
    ''' </para>
    ''' エンティティ単位の総合的なバリデーション処理を担う中心的コンポーネント。
    ''' </summary>
    ''' <typeparam name="T">検証対象エンティティの型。</typeparam>
    Public Class Validator(Of T)

        ''' <summary>
        ''' 登録されたバリデーションルールの一覧。
        ''' </summary>
        Private ReadOnly _rules As New List(Of IValidationRule(Of T))

        ''' <summary>
        ''' バリデーションルールを追加する。
        ''' </summary>
        ''' <param name="rule">追加するバリデーションルール。</param>
        Public Sub AddRule(rule As IValidationRule(Of T))
            _rules.Add(rule)
        End Sub

        ''' <summary>
        ''' 登録されたすべてのルールを実行し、検証結果を返す。
        ''' </summary>
        ''' <param name="entity">検証対象のエンティティ。</param>
        ''' <returns>全ルールの結果を統合した <see cref="ValidationResult"/>。</returns>
        Public Function Validate(entity As T) As ValidationResult
            Dim result As New ValidationResult()

            For Each r In _rules
                Dim r2 = r.Validate(entity)
                result.Errors.AddRange(r2.Errors)
            Next

            Return result
        End Function

    End Class

End Namespace