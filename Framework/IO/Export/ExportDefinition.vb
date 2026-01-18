Namespace Framework.IO.Export

    ''' <summary>
    ''' エクスポート処理に使用する列定義をまとめたクラス。
    ''' <para>
    ''' ・列ヘッダー名  
    ''' ・エンティティから値を取得するためのセレクタ  
    ''' </para>
    ''' を保持し、CSV や Excel などのエクスポート処理で利用される。
    ''' </summary>
    ''' <typeparam name="T">エクスポート対象エンティティの型。</typeparam>
    Public Class ExportDefinition(Of T)

        ''' <summary>
        ''' エクスポート対象の列定義一覧。
        ''' </summary>
        Public Property Columns As List(Of ExportColumn(Of T))

        ''' <summary>
        ''' 新しいエクスポート定義を初期化する。
        ''' 初期状態では空の列リストを生成する。
        ''' </summary>
        Public Sub New()
            Columns = New List(Of ExportColumn(Of T))()
        End Sub

        ''' <summary>
        ''' 新しい列定義を追加する。
        ''' </summary>
        ''' <param name="header">列ヘッダー名。</param>
        ''' <param name="selector">エンティティから値を取得する関数。</param>
        Public Sub Add(header As String, selector As Func(Of T, Object))
            Columns.Add(New ExportColumn(Of T) With {
                .Header = header,
                .ValueSelector = selector
            })
        End Sub

    End Class

End Namespace