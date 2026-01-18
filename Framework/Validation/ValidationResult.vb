Namespace Framework.Validation

    ''' <summary>
    ''' 単一のバリデーションエラーを表すクラス。
    ''' <para>
    ''' ・エラーが発生したフィールド名  
    ''' ・エラーメッセージ  
    ''' </para>
    ''' バリデーション結果の構成要素として使用される。
    ''' </summary>
    Public Class ValidationError

        ''' <summary>
        ''' エラーが発生したフィールド名。
        ''' </summary>
        Public Property Field As String

        ''' <summary>
        ''' エラー内容を表すメッセージ。
        ''' </summary>
        Public Property Message As String

    End Class

    ''' <summary>
    ''' バリデーション実行結果を保持するクラス。
    ''' <para>
    ''' ・IsValid により成功／失敗を判定  
    ''' ・Errors に複数の <see cref="ValidationError"/> を保持  
    ''' ・AddError によりエラーを追加すると自動的に IsValid が False になる  
    ''' </para>
    ''' バリデーション処理の標準的な返却形式として利用される。
    ''' </summary>
    Public Class ValidationResult

        ''' <summary>
        ''' バリデーションが成功したかどうかを示すフラグ。
        ''' </summary>
        Public Property IsValid As Boolean

        ''' <summary>
        ''' 発生したバリデーションエラーの一覧。
        ''' </summary>
        Public Property Message As String

        ''' <summary>
        ''' 発生したバリデーションエラーの一覧。
        ''' </summary>
        Public Property Errors As List(Of ValidationError)

        ''' <summary>
        ''' 新しい ValidationResult を生成する。
        ''' 初期状態ではエラーなし（IsValid = True）。
        ''' </summary>
        Public Sub New()
            Errors = New List(Of ValidationError)()
            IsValid = True
        End Sub

        ''' <summary>
        ''' バリデーションエラーを追加する。
        ''' </summary>
        ''' <param name="field">エラーが発生したフィールド名。</param>
        ''' <param name="message">エラーメッセージ。</param>
        Public Sub AddError(field As String, message As String)
            IsValid = False
            Errors.Add(New ValidationError With {.Field = field, .Message = message})
        End Sub

        ''' <summary>
        ''' バリデーションエラーを追加する（Severity 付き）。
        ''' <para>
        ''' ※ 現状では Severity は保持していないため、メッセージのみ追加される。  
        '''    将来的な拡張を見据えたオーバーロード。  
        ''' </para>
        ''' </summary>
        ''' <param name="field">エラーが発生したフィールド名。</param>
        ''' <param name="message">エラーメッセージ。</param>
        ''' <param name="severity">エラーの重要度。</param>
        Public Sub AddError(field As String, message As String, severity As ValidationSeverity)
            IsValid = False
            Errors.Add(New ValidationError With {.Field = field, .Message = message})
        End Sub

        ''' <summary>
        ''' 成功結果を生成する。
        ''' </summary>
        ''' <returns>成功状態の <see cref="ValidationResult"/>。</returns>
        Public Shared Function Ok() As ValidationResult
            Return New ValidationResult With {.IsValid = True}
        End Function

        ''' <summary>
        ''' 失敗結果を生成する。
        ''' </summary>
        ''' <param name="msg">失敗理由のメッセージ。</param>
        ''' <returns>失敗状態の <see cref="ValidationResult"/>。</returns>
        Public Shared Function Fail(msg As String) As ValidationResult
            Return New ValidationResult With {.IsValid = False, .Message = msg}
        End Function

    End Class

End Namespace