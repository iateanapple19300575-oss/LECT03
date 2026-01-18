Namespace Framework.Validation

    ''' <summary>
    ''' エンティティ T に対してバリデーションを実行するためのインターフェイス。
    ''' <para>
    ''' ・複数のバリデーションルールをまとめて評価する実装で利用  
    ''' ・検証結果は <see cref="ValidationResult"/> として返却  
    ''' ・業務ごとに専用の Validator クラスを作成する際の基本契約  
    ''' </para>
    ''' バリデーション処理の統一化と拡張性を確保するためのインターフェイス。
    ''' </summary>
    ''' <typeparam name="T">検証対象エンティティの型。</typeparam>
    Public Interface IValidator(Of T)

        ''' <summary>
        ''' 指定されたエンティティに対してバリデーションを実行する。
        ''' </summary>
        ''' <param name="entity">検証対象のエンティティ。</param>
        ''' <returns>検証結果を表す <see cref="ValidationResult"/>。</returns>
        Function Validate(entity As T) As ValidationResult

    End Interface

End Namespace