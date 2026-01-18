Namespace Framework.IO.Export

    ''' <summary>
    ''' エクスポート処理の共通基底クラス。
    ''' <para>
    ''' ・列定義（<see cref="ExportDefinition(Of T)"/>）の保持  
    ''' ・派生クラスでのエクスポート処理実装の強制  
    ''' </para>
    ''' を担い、CSV や Excel など異なる形式のエクスポート処理に共通の枠組みを提供する。
    ''' </summary>
    ''' <typeparam name="T">エクスポート対象エンティティの型。</typeparam>
    Public MustInherit Class ExporterBase(Of T)

        ''' <summary>
        ''' エクスポート対象の列定義。
        ''' 派生クラスはこの定義に基づいて出力内容を生成する。
        ''' </summary>
        Protected ReadOnly _definition As ExportDefinition(Of T)

        ''' <summary>
        ''' 列定義を指定してエクスポーターを初期化する。
        ''' </summary>
        ''' <param name="definition">エクスポート列の定義。</param>
        Protected Sub New(definition As ExportDefinition(Of T))
            _definition = definition
        End Sub

        ''' <summary>
        ''' エクスポート処理を実行する抽象メソッド。
        ''' 派生クラスで具体的な出力形式（CSV、Excel など）を実装する。
        ''' </summary>
        ''' <param name="path">出力先ファイルパス。</param>
        ''' <param name="data">エクスポート対象データ。</param>
        Public MustOverride Sub Export(path As String, data As IEnumerable(Of T))

    End Class

End Namespace