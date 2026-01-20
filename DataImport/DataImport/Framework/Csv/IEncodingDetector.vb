' IEncodingDetector.vb
Imports System.Text
Imports System.IO

Public Interface IEncodingDetector
    Function Detect(path As String) As Encoding
End Interface

Public Class SimpleEncodingDetector
    Implements IEncodingDetector

    Public Function Detect(path As String) As Encoding _
        Implements IEncodingDetector.Detect

        Dim bytes = File.ReadAllBytes(path)

        If bytes.Length >= 3 AndAlso
           bytes(0) = &HEF AndAlso bytes(1) = &HBB AndAlso bytes(2) = &HBF Then
            Return Encoding.UTF8
        End If

        Dim utf8 As New UTF8Encoding(False, True)
        Try
            utf8.GetString(bytes)
            Return utf8
        Catch
            Return Encoding.GetEncoding("Shift_JIS")
        End Try
    End Function
End Class