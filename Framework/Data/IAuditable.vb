Namespace Framework.Data

    ''' <summary>
    ''' 監査情報（作成日時・更新日時）を保持するエンティティが実装すべきインターフェイス。
    ''' INSERT / UPDATE 時に <see cref="AuditHelper"/> によって自動的に値が設定される。
    ''' </summary>
    Public Interface IAuditable

        ''' <summary>
        ''' レコードの作成日時。
        ''' 新規登録時に自動設定される。
        ''' </summary>
        Property CreatedAt As DateTime

        ''' <summary>
        ''' レコードの最終更新日時。
        ''' 更新時に自動設定される。
        ''' </summary>
        Property UpdatedAt As DateTime

    End Interface

End Namespace