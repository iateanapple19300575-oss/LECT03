Imports System.Data.SqlClient
Imports Framework.Core

Namespace Framework.Data.Mapper

    ''' <summary>
    ''' <see cref="SqlDataReader"/> をエンティティへマッピングするための基底クラス。
    ''' 派生クラスは <see cref="Map(SqlDataReader)"/> を実装し、1 レコード分の変換処理を定義する。
    ''' SafeGet を利用した型安全な値取得ヘルパーも提供する。
    ''' </summary>
    ''' <typeparam name="TEntity">マッピング対象のエンティティ型。</typeparam>
    Public MustInherit Class DataReaderMapperBase(Of TEntity)
        Implements IDataReaderMapper(Of TEntity)

        ''' <summary>
        ''' 派生クラスで実装する、1 レコード分のマッピング処理。
        ''' </summary>
        ''' <param name="reader">データ読み取り元の <see cref="SqlDataReader"/>。</param>
        ''' <returns>マッピングされたエンティティ。</returns>
        Public MustOverride Function Map(reader As SqlDataReader) As TEntity

        ''' <summary>
        ''' SafeGet を利用して指定列の値を安全に取得するヘルパーメソッド。
        ''' NULL の場合は Nothing を返す。
        ''' </summary>
        ''' <typeparam name="T">取得する値の型。</typeparam>
        ''' <param name="reader">データ読み取り元の <see cref="SqlDataReader"/>。</param>
        ''' <param name="column">列名。</param>
        ''' <returns>取得した値、または Nothing。</returns>
        Protected Function GetValue(Of T)(reader As SqlDataReader, column As String) As T
            Return SafeGet.FromReader(Of T)(reader, column)
        End Function

        ''' <summary>
        ''' 明示的インターフェイス実装。
        ''' 内部的には <see cref="Map(SqlDataReader)"/> を呼び出す。
        ''' </summary>
        Private Function IDataReaderMapper_Map(reader As SqlDataReader) As TEntity Implements IDataReaderMapper(Of TEntity).Map
            Return Map(reader)
        End Function

    End Class

End Namespace