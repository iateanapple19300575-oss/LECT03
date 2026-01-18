Imports System.Data.SqlClient

Namespace Framework.Data.Mapper

    ''' <summary>
    ''' <see cref="SqlDataReader"/> の 1 レコードをエンティティへマッピングするためのインターフェイス。
    ''' 実装クラスは、データリーダーから必要な列を取得し、<typeparamref name="TEntity"/> のインスタンスへ変換する処理を提供する。
    ''' </summary>
    ''' <typeparam name="TEntity">マッピング対象となるエンティティ型。</typeparam>
    Public Interface IDataReaderMapper(Of TEntity)

        ''' <summary>
        ''' <see cref="SqlDataReader"/> の現在行を <typeparamref name="TEntity"/> にマッピングする。
        ''' </summary>
        ''' <param name="reader">データ読み取り元の <see cref="SqlDataReader"/>。</param>
        ''' <returns>マッピングされたエンティティ。</returns>
        Function Map(reader As SqlDataReader) As TEntity

    End Interface

End Namespace