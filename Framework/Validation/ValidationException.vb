Namespace Framework.Validation

    ''' <summary>
    ''' バリデーションエラーが発生した際にスローされる例外クラス。
    ''' <para>
    ''' ・複数の <see cref="ValidationError"/> を保持  
    ''' ・例外メッセージは共通文言「入力内容に誤りがあります。」  
    ''' ・UI 層やサービス層でまとめてエラー表示する用途に最適  
    ''' </para>
    ''' バリデーション処理と例外処理を分離し、保守性を高めるための例外クラス。
    ''' </summary>
    Public Class ValidationException
        Inherits ApplicationException

        ''' <summary>
        ''' 発生したバリデーションエラーの一覧。
        ''' </summary>
        Public Property Errors As List(Of ValidationError)

        ''' <summary>
        ''' バリデーション例外を生成する。
        ''' </summary>
        ''' <param name="errors">発生したバリデーションエラーのリスト。</param>
        Public Sub New(errors As List(Of ValidationError))
            MyBase.New("入力内容に誤りがあります。")
            Me.Errors = errors
        End Sub

    End Class

End Namespace