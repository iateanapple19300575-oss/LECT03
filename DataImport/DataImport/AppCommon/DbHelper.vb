Imports System.Data.SqlClient

Public Class DbHelper
    Public Shared Function GetConnection() As SqlConnection
        Return New SqlConnection("Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI")
    End Function
End Class