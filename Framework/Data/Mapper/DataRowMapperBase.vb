Imports Framework.Core

Namespace Framework.Data.Mapper

    ''' <summary>
    ''' <see cref="DataRow"/> をエンティティへマッピングするための基底クラス。
    ''' 派生クラスは <see cref="Map(DataRow)"/> を実装し、1 行分のデータをエンティティへ変換する処理を定義する。
    ''' SafeGet を利用した型安全な値取得ヘルパーも提供する。
    ''' </summary>
    ''' <typeparam name="TEntity">マッピング対象となるエンティティ型。</typeparam>
    Public MustInherit Class DataRowMapperBase(Of TEntity)
        Implements IDataRowMapper(Of TEntity)

        ''' <summary>
        ''' 派生クラスで実装する、1 行分のマッピング処理。
        ''' </summary>
        ''' <param name="row">データ行を表す <see cref="DataRow"/>。</param>
        ''' <returns>マッピングされたエンティティ。</returns>
        Public MustOverride Function Map(row As DataRow) As TEntity

        ''' <summary>
        ''' SafeGet を利用して指定列の値を安全に取得するヘルパーメソッド。
        ''' NULL の場合は Nothing を返す。
        ''' </summary>
        ''' <typeparam name="T">取得する値の型。</typeparam>
        ''' <param name="row">値を取得する対象の <see cref="DataRow"/>。</param>
        ''' <param name="column">列名。</param>
        ''' <returns>取得した値、または Nothing。</returns>
        Protected Function GetValue(Of T)(row As DataRow, column As String) As T
            Return SafeGet.FromDataRow(Of T)(row, column)
        End Function

        ''' <summary>
        ''' 明示的インターフェイス実装。
        ''' 内部的には <see cref="Map(DataRow)"/> を呼び出す。
        ''' </summary>
        Private Function IDataRowMapper_Map(row As DataRow) As TEntity Implements IDataRowMapper(Of TEntity).Map
            Return Map(row)
        End Function

    End Class

End Namespace