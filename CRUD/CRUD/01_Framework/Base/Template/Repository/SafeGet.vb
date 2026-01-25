Imports System.Data.SqlClient

Namespace Framework

    ''' <summary>
    ''' SqlDataReader / DataRow から安全に値を取得するためのユーティリティクラス。
    ''' DBNull や型変換エラーを吸収し、型に応じたデフォルト値を返します。
    ''' Nullable, Enum, 任意型に対応。
    ''' </summary>
    Public NotInheritable Class SafeGet

        Private Sub New()
        End Sub

        '-----------------------------------------
        ' 型マッピング辞書（高速化）
        '-----------------------------------------
        Private Shared ReadOnly TypeMap As Dictionary(Of Type, Func(Of SqlDataReader, Integer, Object)) =
            New Dictionary(Of Type, Func(Of SqlDataReader, Integer, Object)) From {
                {GetType(String), Function(r, i) r.GetString(i)},
                {GetType(Integer), Function(r, i) r.GetInt32(i)},
                {GetType(Decimal), Function(r, i) r.GetDecimal(i)},
                {GetType(Boolean), Function(r, i) r.GetBoolean(i)},
                {GetType(DateTime), Function(r, i) r.GetDateTime(i)},
                {GetType(TimeSpan), Function(r, i) CType(r.GetValue(i), TimeSpan)}
            }

        '-----------------------------------------
        ' SqlDataReader 用汎用 SafeGet
        '-----------------------------------------
        ''' <summary>
        ''' 任意の型 T の値を安全に取得します（SqlDataReader）。
        ''' DBNull の場合は型 T のデフォルト値を返します。
        ''' Nullable, Enum にも対応。
        ''' </summary>
        Public Shared Function [Get](Of T)(r As SqlDataReader, col As String) As T
            Dim i As Integer = GetColumnIndex(r, col)
            If i < 0 Then Return CType(GetDefault(GetType(T)), T)

            If r.IsDBNull(i) Then
                Return CType(GetDefault(GetType(T)), T)
            End If

            Dim targetType = GetType(T)
            Dim nonNullableType = Nullable.GetUnderlyingType(targetType)

            ' Nullable(Of T)
            If nonNullableType IsNot Nothing Then
                Return CType(ConvertValue(r, i, nonNullableType), T)
            End If

            ' Enum
            If targetType.IsEnum Then
                Return CType([Enum].ToObject(targetType, ConvertValue(r, i, GetEnumBaseType(targetType))), T)
            End If

            ' 型マッピング辞書
            If TypeMap.ContainsKey(targetType) Then
                Return CType(TypeMap(targetType)(r, i), T)
            End If

            ' 汎用変換
            Return CType(Convert.ChangeType(r.GetValue(i), targetType), T)
        End Function

        '-----------------------------------------
        ' DataRow 用 SafeGet
        '-----------------------------------------
        ''' <summary>
        ''' DataRow から任意の型 T の値を安全に取得します。
        ''' DBNull, Nullable, Enum に対応。
        ''' </summary>
        Public Shared Function FromRow(Of T)(row As DataRow, col As String) As T
            If Not row.Table.Columns.Contains(col) Then
                Return CType(GetDefault(GetType(T)), T)
            End If

            Dim v = row(col)
            If v Is DBNull.Value Then
                Return CType(GetDefault(GetType(T)), T)
            End If

            Dim targetType = GetType(T)
            Dim nonNullableType = Nullable.GetUnderlyingType(targetType)

            ' Nullable
            If nonNullableType IsNot Nothing Then
                Return CType(Convert.ChangeType(v, nonNullableType), T)
            End If

            ' Enum
            If targetType.IsEnum Then
                Return CType([Enum].ToObject(targetType, v), T)
            End If

            Return CType(Convert.ChangeType(v, targetType), T)
        End Function

        '-----------------------------------------
        ' 列存在チェック
        '-----------------------------------------
        Private Shared Function GetColumnIndex(r As SqlDataReader, col As String) As Integer
            For i As Integer = 0 To r.FieldCount - 1
                If r.GetName(i).Equals(col, StringComparison.OrdinalIgnoreCase) Then
                    Return i
                End If
            Next
            Return -1
        End Function

        '-----------------------------------------
        ' Enum の基底型を取得
        '-----------------------------------------
        Private Shared Function GetEnumBaseType(t As Type) As Type
            Return [Enum].GetUnderlyingType(t)
        End Function

        '-----------------------------------------
        ' 値変換（Nullable / Enum 対応）
        '-----------------------------------------
        Private Shared Function ConvertValue(r As SqlDataReader, i As Integer, t As Type) As Object
            Dim v = r.GetValue(i)
            Return Convert.ChangeType(v, t)
        End Function

        '-----------------------------------------
        ' デフォルト値生成
        '-----------------------------------------
        Private Shared Function GetDefault(t As Type) As Object
            If t.IsValueType Then Return Activator.CreateInstance(t)
            Return Nothing
        End Function

        '-----------------------------------------
        ' 型別ショートカット（既存互換）
        '-----------------------------------------
        Public Shared Function Str(r As SqlDataReader, col As String) As String
            Return [Get](Of String)(r, col)
        End Function

        Public Shared Function Int(r As SqlDataReader, col As String) As Integer
            Return [Get](Of Integer)(r, col)
        End Function

        Public Shared Function Dec(r As SqlDataReader, col As String) As Decimal
            Return [Get](Of Decimal)(r, col)
        End Function

        Public Shared Function Dt(r As SqlDataReader, col As String) As DateTime
            Return [Get](Of DateTime)(r, col)
        End Function

        Public Shared Function Bool(r As SqlDataReader, col As String) As Boolean
            Return [Get](Of Boolean)(r, col)
        End Function

        Public Shared Function Time(r As SqlDataReader, col As String) As TimeSpan
            Return [Get](Of TimeSpan)(r, col)
        End Function

    End Class

End Namespace