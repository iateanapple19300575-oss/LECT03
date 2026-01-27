Imports System.Data.SqlClient

Public Class ImportErrorRepository

    Public Sub InsertWithTran(cn As SqlConnection,
                              tran As SqlTransaction,
                              importType As String,
                              rowIndex As Integer,
                              dto As Object,
                              errorMessage As String)

        Dim sql = "
INSERT INTO ImportErrorLog
(ImportType, RowIndex, ErrorMessage, DtoJson, CreatedAt)
VALUES (@ImportType, @RowIndex, @ErrorMessage, @DtoJson, @CreatedAt)
"

        Using cmd As New SqlCommand(sql, cn, tran)
            cmd.Parameters.AddWithValue("@ImportType", importType)
            cmd.Parameters.AddWithValue("@RowIndex", rowIndex)
            cmd.Parameters.AddWithValue("@ErrorMessage", errorMessage)
            cmd.Parameters.AddWithValue("@DtoJson", Serialize(dto))
            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now)
            cmd.ExecuteNonQuery()
        End Using

    End Sub

    Private Function Serialize(dto As Object) As String
        If dto Is Nothing Then Return ""
        Return String.Join(",",
            dto.GetType().GetProperties().
                Select(Function(p) String.Format("{0}={1}", p.Name, p.GetValue(dto, Nothing))))
    End Function

End Class