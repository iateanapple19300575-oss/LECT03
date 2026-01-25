Imports System.Data.SqlClient

Public Class ConnectionFactory
    Public Shared Function Create() As SqlConnection
        Dim cn As New SqlConnection("Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI")
        cn.Open()
        Return cn
    End Function
End Class