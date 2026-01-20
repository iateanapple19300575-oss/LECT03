Imports System.Text

Public Class DefaultCsvReader
    Implements ICsvReader

    Public Function Read(path As String, enc As Encoding) As IEnumerable(Of String()) Implements ICsvReader.Read
        Throw New NotImplementedException()
    End Function
    ' 標準的なCSV読み
End Class
