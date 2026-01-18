Namespace Framework.IO.Export

    ''' <summary>
    ''' エクスポート対象データの 1 列分の定義を表すクラス。
    ''' <para>
    ''' ・列ヘッダー名  
    ''' ・エンティティから値を取得するためのセレクタ  
    ''' </para>
    ''' を保持し、CSV や Excel などのエクスポート処理で使用される。
    ''' </summary>
    ''' <typeparam name="T">エクスポート対象エンティティの型。</typeparam>
    Public Class ExportColumn(Of T)

        ''' <summary>
        ''' 出力される列のヘッダー名。
        ''' CSV の 1 行目や Excel の表見出しとして使用される。
        ''' </summary>
        Public Property Header As String

        ''' <summary>
        ''' エンティティから列値を取得するための関数。
        ''' <para>
        ''' 例：<c>Function(x) x.UserName</c>  
        ''' </para>
        ''' </summary>
        Public Property ValueSelector As Func(Of T, Object)

    End Class

End Namespace