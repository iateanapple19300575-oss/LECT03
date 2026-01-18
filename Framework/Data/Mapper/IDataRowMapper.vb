Namespace Framework.Data.Mapper

    ''' <summary>
    ''' <see cref="DataRow"/> の 1 行をエンティティへマッピングするためのインターフェイス。
    ''' 実装クラスは、DataRow から必要な列を取得し、
    ''' <typeparamref name="TEntity"/> のインスタンスへ変換する処理を提供する。
    ''' </summary>
    ''' <typeparam name="TEntity">マッピング対象となるエンティティ型。</typeparam>
    Public Interface IDataRowMapper(Of TEntity)

        ''' <summary>
        ''' <see cref="DataRow"/> の内容を <typeparamref name="TEntity"/> にマッピングする。
        ''' </summary>
        ''' <param name="row">マッピング対象の <see cref="DataRow"/>。</param>
        ''' <returns>マッピングされたエンティティ。</returns>
        Function Map(row As DataRow) As TEntity

    End Interface

End Namespace