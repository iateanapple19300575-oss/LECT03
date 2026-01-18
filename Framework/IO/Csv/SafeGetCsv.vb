Namespace Framework.IO.Csv

    ''' <summary>
    ''' CSV のフィールド値を安全に型変換するためのユーティリティクラス。
    ''' <para>
    ''' ・数値  
    ''' ・小数  
    ''' ・日付  
    ''' </para>
    ''' の変換を行い、失敗した場合はフィールド名を含む分かりやすい例外をスローする。
    ''' CSV インポート時のバリデーションを簡潔に記述するために使用される。
    ''' </summary>
    Public NotInheritable Class SafeGetCsv

        ''' <summary>
        ''' 文字列を整数に変換する。
        ''' 変換できない場合は例外をスローする。
        ''' </summary>
        ''' <param name="value">変換対象の文字列。</param>
        ''' <param name="fieldName">エラー時に表示するフィールド名。</param>
        ''' <returns>整数値。</returns>
        ''' <exception cref="Exception">数値に変換できない場合。</exception>
        Public Shared Function ToInt(value As String, fieldName As String) As Integer
            Dim num As Integer
            If Not Integer.TryParse(value, num) Then
                Throw New Exception(fieldName & " は数値である必要があります。")
            End If
            Return num
        End Function

        ''' <summary>
        ''' 文字列を Decimal に変換する。
        ''' 変換できない場合は例外をスローする。
        ''' </summary>
        ''' <param name="value">変換対象の文字列。</param>
        ''' <param name="fieldName">エラー時に表示するフィールド名。</param>
        ''' <returns>Decimal 値。</returns>
        ''' <exception cref="Exception">数値に変換できない場合。</exception>
        Public Shared Function ToDecimal(value As String, fieldName As String) As Decimal
            Dim num As Decimal
            If Not Decimal.TryParse(value, num) Then
                Throw New Exception(fieldName & " は数値である必要があります。")
            End If
            Return num
        End Function

        ''' <summary>
        ''' 文字列を日付型に変換する。
        ''' 変換できない場合は例外をスローする。
        ''' </summary>
        ''' <param name="value">変換対象の文字列。</param>
        ''' <param name="fieldName">エラー時に表示するフィールド名。</param>
        ''' <returns>DateTime 値。</returns>
        ''' <exception cref="Exception">日付形式に変換できない場合。</exception>
        Public Shared Function ToDate(value As String, fieldName As String) As DateTime
            Dim dt As DateTime
            If Not DateTime.TryParse(value, dt) Then
                Throw New Exception(fieldName & " は日付形式である必要があります。")
            End If
            Return dt
        End Function

    End Class

End Namespace