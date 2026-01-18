Namespace Framework.Core

    ''' <summary>
    ''' 文字列に関するユーティリティメソッドを提供するクラス。
    ''' 主に NULL または空白文字のみで構成される文字列の判定を行う。
    ''' </summary>
    Public NotInheritable Class StringUtil

        ''' <summary>
        ''' 指定した文字列が NULL または空白文字のみで構成されているかを判定する。
        ''' </summary>
        ''' <param name="value">判定対象の文字列。</param>
        ''' <returns>
        ''' 文字列が NULL または空白のみの場合は <c>True</c>、それ以外は <c>False</c>。
        ''' </returns>
        Public Shared Function IsNullOrWhiteSpace(value As String) As Boolean
            If value Is Nothing Then Return True
            If value.Trim().Length = 0 Then Return True
            Return False
        End Function

    End Class

End Namespace