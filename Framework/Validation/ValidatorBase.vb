Namespace Framework.Validation

    ''' <summary>
    ''' RuleBuilder ベースのバリデーションを行うための抽象バリデータ基底クラス。
    ''' <para>
    ''' ・AddRule によりフィールド単位のルール（<see cref="RuleBuilder(Of T)"/>）を登録  
    ''' ・Validate 実行時に全ルールを順次評価し、<see cref="ValidationResult"/> に集約  
    ''' ・業務ごとの Validator は本クラスを継承してルール定義を行う  
    ''' </para>
    ''' フィールド単位の柔軟なバリデーション構築を支援するための基盤クラス。
    ''' </summary>
    ''' <typeparam name="T">検証対象エンティティの型。</typeparam>
    Public MustInherit Class ValidatorBase(Of T)
        Implements IValidator(Of T)

        ''' <summary>
        ''' 登録された RuleBuilder の一覧。
        ''' </summary>
        Private ReadOnly _rules As New List(Of RuleBuilder(Of T))()

        ''' <summary>
        ''' バリデーションルールを追加する。
        ''' </summary>
        ''' <param name="rule">追加する <see cref="RuleBuilder(Of T)"/>。</param>
        Protected Sub AddRule(rule As RuleBuilder(Of T))
            _rules.Add(rule)
        End Sub

        ''' <summary>
        ''' 登録されたすべてのルールを実行し、検証結果を返す。
        ''' </summary>
        ''' <param name="entity">検証対象のエンティティ。</param>
        ''' <returns>全ルールの結果を統合した <see cref="ValidationResult"/>。</returns>
        Public Function Validate(entity As T) As ValidationResult Implements IValidator(Of T).Validate
            Dim result As New ValidationResult()

            For Each rule In _rules
                rule.Execute(entity, result)
            Next

            Return result
        End Function

    End Class

End Namespace