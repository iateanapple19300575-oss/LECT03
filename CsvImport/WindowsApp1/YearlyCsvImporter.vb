Public Class YearlyCsvImporter
    Inherits CsvImporterBase
    Public Sub New(filePath As String)
        MyBase.New(filePath)
    End Sub

    Protected Overrides ReadOnly Property TargetTableName As String
        Get
            Return "YearlyTable"
        End Get
    End Property

    Protected Overrides ReadOnly Property ImportTypeName As String
        Get
            Return "Yearly"
        End Get
    End Property

    Protected Overrides Function MapRow(row As DataRow) As Object
        Return New YearlyInDto With {
            .Year = CInt(row("Year")),
            .Value = CInt(row("Value"))
        }
    End Function

    Protected Overrides Sub ValidateSpecific(dto As Object)
        ' 年単位の業務バリデーション
    End Sub

    Protected Overrides Function ResolveDeletePeriods(dtos As List(Of Object)) As List(Of ImportPeriod)
        Dim y = CType(dtos.First(), YearlyInDto).Year
        Return New List(Of ImportPeriod) From {ImportPeriod.ForYear(y)}
    End Function

    Protected Overrides Function GetDtoType() As Type
        Return GetType(YearlyInDto)
    End Function
End Class