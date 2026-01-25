Imports System.Data.SqlClient
Imports System.Reflection
Imports Framework

Namespace Mappers

    ''' <summary>
    ''' SqlDataReader および CSV データを任意のエンティティへマッピングする汎用 Mapper クラス。
    ''' プロパティ名・辞書・ColumnNameAttribute の優先順位でカラム名を解決し、
    ''' 型に応じた変換を行います。
    ''' </summary>
    ''' <remarks>
    ''' ・SQL → Entity、CSV → Entity の両方に対応。<br/>
    ''' ・ColumnNameAttribute によるマッピング指定が可能。<br/>
    ''' ・辞書（Dictionary）による柔軟なマッピング指定も可能。<br/>
    ''' ・SafeGet と連携し、DBNull や型変換を安全に処理します。
    ''' </remarks>
    Public NotInheritable Class Mapper

        Private Sub New()
        End Sub

        '-----------------------------------------
        ' SQL → Entity
        '-----------------------------------------

        ''' <summary>
        ''' SqlDataReader の現在行を T 型のエンティティへ自動マッピングします。
        ''' </summary>
        ''' <typeparam name="T">マッピング対象のエンティティ型。</typeparam>
        ''' <param name="r">SqlDataReader。</param>
        ''' <param name="mapDic">プロパティ名とカラム名のマッピング辞書（任意）。</param>
        ''' <returns>マッピングされたエンティティ。</returns>
        ''' <remarks>
        ''' カラム名の解決順序は以下の通りです。<br/>
        ''' 1. ColumnNameAttribute<br/>
        ''' 2. map 辞書<br/>
        ''' 3. プロパティ名<br/>
        ''' </remarks>
        Public Shared Function Map(Of T As New)(
            r As SqlDataReader,
            Optional mapDic As Dictionary(Of String, String) = Nothing
        ) As T

            Dim entity As New T()
            Dim props = GetType(T).GetProperties()

            For Each p In props
                Dim colName = ResolveColumnName(p, mapDic)
                Dim index = GetColumnIndex(r, colName)
                If index < 0 Then Continue For

                Dim value = SafeGetValue(r, index, p.PropertyType)
                p.SetValue(entity, value, Nothing)
            Next

            Return entity
        End Function

        '-----------------------------------------
        ' CSV → Entity
        '-----------------------------------------

        ''' <summary>
        ''' CSV のヘッダー行とデータ行を T 型のエンティティへマッピングします。
        ''' </summary>
        ''' <typeparam name="T">マッピング対象のエンティティ型。</typeparam>
        ''' <param name="header">CSV のヘッダー行。</param>
        ''' <param name="row">CSV の 1 行分のデータ。</param>
        ''' <param name="map">プロパティ名とカラム名のマッピング辞書（任意）。</param>
        ''' <returns>マッピングされたエンティティ。</returns>
        ''' <remarks>
        ''' ・CSV のヘッダー名とプロパティ名が一致していれば自動マッピングされます。<br/>
        ''' ・ColumnNameAttribute または map 辞書でカラム名を上書きできます。
        ''' </remarks>
        Public Shared Function MapCsv(Of T As New)(
            header As String(),
            row As String(),
            Optional map As Dictionary(Of String, String) = Nothing
        ) As T

            Dim entity As New T()
            Dim props = GetType(T).GetProperties()

            For Each p In props
                Dim colName = ResolveColumnName(p, map)

                Dim idx = Array.FindIndex(header, Function(h) h.Equals(colName, StringComparison.OrdinalIgnoreCase))
                If idx < 0 Then Continue For

                Dim raw = row(idx)
                Dim value = ConvertCsvValue(raw, p.PropertyType)

                p.SetValue(entity, value, Nothing)
            Next

            Return entity
        End Function

        '-----------------------------------------
        ' ColumnNameAttribute / 辞書 / プロパティ名
        '-----------------------------------------

        ''' <summary>
        ''' プロパティに対応するカラム名を解決します。
        ''' ColumnNameAttribute → 辞書 → プロパティ名 の順で判定します。
        ''' </summary>
        Private Shared Function ResolveColumnName(p As PropertyInfo, map As Dictionary(Of String, String)) As String
            Dim attr = CType(Attribute.GetCustomAttribute(p, GetType(ColumnNameAttribute)), ColumnNameAttribute)
            If attr IsNot Nothing Then Return attr.Name

            If map IsNot Nothing AndAlso map.ContainsKey(p.Name) Then
                Return map(p.Name)
            End If

            Return p.Name
        End Function

        ''' <summary>
        ''' SqlDataReader からカラム名に対応するインデックスを取得します。
        ''' 見つからない場合は -1 を返します。
        ''' </summary>
        Private Shared Function GetColumnIndex(r As SqlDataReader, col As String) As Integer
            For i As Integer = 0 To r.FieldCount - 1
                If r.GetName(i).Equals(col, StringComparison.OrdinalIgnoreCase) Then
                    Return i
                End If
            Next
            Return -1
        End Function

        ''' <summary>
        ''' SafeGet を使用して SqlDataReader から型に応じた値を取得します。
        ''' </summary>
        Private Shared Function SafeGetValue(r As SqlDataReader, index As Integer, t As Type) As Object
            Dim method = GetType(SafeGet).GetMethod("Get").MakeGenericMethod(t)
            Return method.Invoke(Nothing, New Object() {r, r.GetName(index)})
        End Function

        ''' <summary>
        ''' CSV の文字列値をプロパティ型に変換します。
        ''' Nullable, Enum, 基本型に対応します。
        ''' </summary>
        Private Shared Function ConvertCsvValue(raw As String, t As Type) As Object
            If String.IsNullOrEmpty(raw) Then Return Nothing

            Dim underlying = Nullable.GetUnderlyingType(t)
            If underlying IsNot Nothing Then
                Return Convert.ChangeType(raw, underlying)
            End If

            If t.IsEnum Then
                Return [Enum].Parse(t, raw)
            End If

            Return Convert.ChangeType(raw, t)
        End Function

    End Class

End Namespace