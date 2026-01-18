Namespace Framework.UI

    ''' <summary>
    ''' 画面で使用する各種メッセージを一元管理するクラス。
    ''' <para>
    ''' ・保存／削除などの共通メッセージ  
    ''' ・入力チェック（Validation）メッセージ  
    ''' ・DB／IO エラーなどの汎用メッセージ  
    ''' </para>
    ''' メッセージ文言をコードから分離し、保守性と統一性を高めるための基盤となる。
    ''' </summary>
    Public NotInheritable Class MessageProvider

        ' ============================
        ' 共通メッセージ
        ' ============================

        ''' <summary>
        ''' 保存確認メッセージ。
        ''' </summary>
        Public Shared ReadOnly Property ConfirmSave As String
            Get
                Return "保存してもよろしいですか？"
            End Get
        End Property

        ''' <summary>
        ''' 保存完了メッセージ。
        ''' </summary>
        Public Shared ReadOnly Property SaveCompleted As String
            Get
                Return "保存が完了しました。"
            End Get
        End Property

        ''' <summary>
        ''' 削除確認メッセージ。
        ''' </summary>
        Public Shared ReadOnly Property ConfirmDelete As String
            Get
                Return "削除してもよろしいですか？"
            End Get
        End Property

        ''' <summary>
        ''' 削除完了メッセージ。
        ''' </summary>
        Public Shared ReadOnly Property DeleteCompleted As String
            Get
                Return "削除が完了しました。"
            End Get
        End Property

        ''' <summary>
        ''' 想定外のエラー発生時に表示するメッセージ。
        ''' </summary>
        Public Shared ReadOnly Property UnexpectedError As String
            Get
                Return "予期しないエラーが発生しました。"
            End Get
        End Property

        ' ============================
        ' Validation メッセージ
        ' ============================

        ''' <summary>
        ''' 必須入力チェックメッセージを生成する。
        ''' </summary>
        ''' <param name="field">項目名。</param>
        ''' <returns>「◯◯は必須です。」の形式のメッセージ。</returns>
        Public Shared Function Required(field As String) As String
            Return String.Format("{0}は必須です。", field)
        End Function

        ''' <summary>
        ''' 最大文字数チェックメッセージを生成する。
        ''' </summary>
        ''' <param name="field">項目名。</param>
        ''' <param name="length">最大文字数。</param>
        ''' <returns>「◯◯は◯文字以内で入力してください。」の形式のメッセージ。</returns>
        Public Shared Function MaxLength(field As String, length As Integer) As String
            Return String.Format("{0}は{1}文字以内で入力してください。", field, length)
        End Function

        ''' <summary>
        ''' 数値範囲チェックメッセージを生成する。
        ''' </summary>
        ''' <param name="field">項目名。</param>
        ''' <param name="min">最小値。</param>
        ''' <param name="max">最大値。</param>
        ''' <returns>「◯◯は◯～◯の範囲で入力してください。」の形式のメッセージ。</returns>
        Public Shared Function Range(field As String, min As Decimal, max As Decimal) As String
            Return String.Format("{0}は{1}～{2}の範囲で入力してください。", field, min, max)
        End Function

        ' ============================
        ' DB / IO / その他
        ' ============================

        ''' <summary>
        ''' データベース処理中のエラーを表すメッセージ。
        ''' </summary>
        Public Shared ReadOnly Property DatabaseError As String
            Get
                Return "データベース処理中にエラーが発生しました。"
            End Get
        End Property

        ''' <summary>
        ''' ファイル／フォルダアクセス時のエラーを表すメッセージ。
        ''' </summary>
        Public Shared ReadOnly Property IOError As String
            Get
                Return "ファイルまたはフォルダへのアクセス中にエラーが発生しました。"
            End Get
        End Property

    End Class

End Namespace