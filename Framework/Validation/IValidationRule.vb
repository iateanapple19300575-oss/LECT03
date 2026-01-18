Namespace Framework.Validation

    ''' <summary>
    ''' バリデーションルールを表す基本インターフェイス。
    ''' <para>
    ''' ・任意のエンティティ T に対して検証処理を実行  
    ''' ・検証結果は <see cref="ValidationResult"/> として返却  
    ''' ・個別ルールはこのインターフェイスを実装して作成する  
    ''' </para>
    ''' バリデーションフレームワークの最小単位となる契約インターフェイス。
    ''' </summary>
    ''' <typeparam name="T">検証対象エンティティの型。</typeparam>
    Public Interface IValidationRule(Of T)

        ''' <summary>
        ''' 指定されたエンティティに対して検証を実行する。
        ''' </summary>
        ''' <param name="entity">検証対象のエンティティ。</param>
        ''' <returns>検証結果を表す <see cref="ValidationResult"/>。</returns>
        Function Validate(entity As T) As ValidationResult

    End Interface

End Namespace