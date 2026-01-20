' ICsvReader.vb
Imports System.Text
Imports System.IO

Public Interface ICsvReader
    Function Read(path As String, enc As Encoding) As IEnumerable(Of String())
End Interface

Public Class CsvReader
    Implements ICsvReader

    Public Function Read(path As String, enc As Encoding) As IEnumerable(Of String()) _
        Implements ICsvReader.Read

        Dim lines = File.ReadAllLines(path, enc)
        Return lines.Select(Function(l) l.Split(","c))
    End Function
End Class