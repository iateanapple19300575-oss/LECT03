Namespace Framework.Validation

    ''' <summary>
    ''' バリデーションルールの基底クラス。
    ''' <para>
    ''' ・すべてのバリデーションルールは本クラスを継承して実装する  
    ''' ・<see cref="Validate"/> をオーバーライドして検証ロジックを記述  
    ''' ・<see cref="Result"/> により空の <see cref="ValidationResult"/> を生成  
    ''' </para>
    ''' 共通的な構造を提供し、各ルール実装の簡潔化と統一性を確保する。
    ''' </summary>
    ''' <typeparam name="T">検証対象エンティティの型。</typeparam>
    Public MustInherit Class ValidationRuleBase(Of T)
        Implements IValidationRule(Of T)

        ''' <summary>
        ''' 検証処理を実行する抽象メソッド。
        ''' 派生クラスで具体的な検証ロジックを実装する。
        ''' </summary>
        ''' <param name="entity">検証対象のエンティティ。</param>
        ''' <returns>検証結果。</returns>
        Public MustOverride Function Validate(entity As T) As ValidationResult _
            Implements IValidationRule(Of T).Validate

        ''' <summary>
        ''' 新しい <see cref="ValidationResult"/> を生成するヘルパーメソッド。
        ''' </summary>
        ''' <returns>空の検証結果。</returns>
        Protected Function Result() As ValidationResult
            Return New ValidationResult()
        End Function

    End Class

End Namespace