Namespace Framework.Validation

    ''' <summary>
    ''' バリデーション結果の重要度を表す列挙体。
    ''' <para>
    ''' ・Error   … 入力内容に重大な問題がある状態  
    ''' ・Warning … 注意が必要だが処理続行は可能な状態  
    ''' ・Info    … 情報提供レベルの通知  
    ''' </para>
    ''' バリデーションメッセージの分類や UI 表示制御に利用される。
    ''' </summary>
    Public Enum ValidationSeverity
        ''' <summary>重大なエラーを表す。</summary>
        [Error]

        ''' <summary>注意喚起レベルの警告を表す。</summary>
        Warning

        ''' <summary>情報提供レベルの通知を表す。</summary>
        Info
    End Enum

End Namespace