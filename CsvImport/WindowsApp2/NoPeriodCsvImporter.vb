Public Class NoPeriodCsvImporter
    Inherits CsvImporterBase
    Public Sub New(filePath As String)
        MyBase.New(filePath)
    End Sub

    Protected Overrides ReadOnly Property TargetTableName As String
        Get
            Return "NoPeriodTable"
        End Get
    End Property

    Protected Overrides ReadOnly Property ImportTypeName As String
        Get
            Return "NoPeriod"
        End Get
    End Property

    Protected Overrides Function MapRow(row As DataRow) As Object
        Return New NoPeriodInDto With {
            .Code = row("Code").ToString(),
            .Name = row("Name").ToString()
        }
    End Function

    Protected Overrides Sub ValidateSpecific(dto As Object)
        ' 期間なしの業務バリデーション
    End Sub

    Protected Overrides Function ResolveDeletePeriods(dtos As List(Of Object)) As List(Of ImportPeriod)
        Return New List(Of ImportPeriod) From {ImportPeriod.All()}
    End Function

    Protected Overrides Function GetDtoType() As Type
        Return GetType(NoPeriodInDto)
    End Function
End Class