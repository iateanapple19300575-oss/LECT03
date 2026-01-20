Imports System.Text

Public Class ShiftJisCsvReader
    Implements ICsvReader

    Public Function Read(path As String, enc As Encoding) As IEnumerable(Of String()) Implements ICsvReader.Read
        Throw New NotImplementedException()
    End Function
    ' Shift-JIS 固定の読み
End Class
