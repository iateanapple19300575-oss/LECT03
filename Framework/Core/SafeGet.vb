Imports System.Data.SqlClient

Namespace Framework.Core

    ''' <summary>
    ''' DataRow および SqlDataReader からの値取得を安全に行うためのユーティリティクラス。
    ''' NULL チェックと型変換を共通化し、例外発生を防ぐ目的で使用する。
    ''' </summary>
    Public NotInheritable Class SafeGet

        ''' <summary>
        ''' DataRow から指定列の値を安全に取得する。
        ''' 列が NULL の場合は Nothing を返す。
        ''' </summary>
        ''' <typeparam name="T">取得する値の型。</typeparam>
        ''' <param name="row">対象の DataRow。</param>
        ''' <param name="column">列名。</param>
        ''' <returns>値が存在する場合はその値、NULL の場合は Nothing。</returns>
        Public Shared Function FromDataRow(Of T)(row As DataRow, column As String) As T
            If row.IsNull(column) Then
                Return Nothing
            End If
            Return CType(row(column), T)
        End Function

        ''' <summary>
        ''' SqlDataReader から指定列の値を安全に取得する。
        ''' 列が NULL の場合は Nothing を返す。
        ''' </summary>
        ''' <typeparam name="T">取得する値の型。</typeparam>
        ''' <param name="reader">対象の SqlDataReader。</param>
        ''' <param name="column">列名。</param>
        ''' <returns>値が存在する場合はその値、NULL の場合は Nothing。</returns>
        Public Shared Function FromReader(Of T)(reader As SqlDataReader, column As String) As T
            Dim ordinal As Integer = reader.GetOrdinal(column)
            If reader.IsDBNull(ordinal) Then
                Return Nothing
            End If
            Return CType(reader.GetValue(ordinal), T)
        End Function

    End Class

End Namespace