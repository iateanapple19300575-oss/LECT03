Imports Microsoft.VisualBasic.FileIO

Public Class CsvParser

    Public Shared Function Load(filePath As String) As DataTable
        Dim dt As New DataTable()

        Using parser As New TextFieldParser(filePath, System.Text.Encoding.UTF8)
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",")

            ' ヘッダ行
            If Not parser.EndOfData Then
                Dim headers = parser.ReadFields()
                For Each h In headers
                    dt.Columns.Add(h)
                Next
            End If

            ' データ行
            While Not parser.EndOfData
                Dim fields = parser.ReadFields()
                dt.Rows.Add(fields)
            End While
        End Using

        Return dt
    End Function

End Class