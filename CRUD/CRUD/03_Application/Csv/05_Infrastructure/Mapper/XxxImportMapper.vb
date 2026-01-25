Imports System.Data.SqlClient
Imports Entities
Imports Framework

Namespace Mappers
    Public NotInheritable Class XxxImportMapper

        Private Sub New()
        End Sub

        Public Shared Function Map(r As SqlDataReader) As XxxImportEntity
            Return New XxxImportEntity() With {
                .Site_Code = SafeGet.Str(r, "Site_Code"),
                .Grade = SafeGet.Str(r, "Grade"),
                .Class_Code = SafeGet.Str(r, "Class_Code"),
                .Koma_Seq = SafeGet.Str(r, "Koma_Seq"),
                .Subjects = SafeGet.Str(r, "Subjects")
            }
        End Function

    End Class
End Namespace