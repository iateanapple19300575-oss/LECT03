Public Class MonthlyCsvImporter
    Inherits CsvImporterBase

    Public Sub New(filePath As String)
        MyBase.New(filePath)
    End Sub

    Protected Overrides ReadOnly Property TargetTableName As String
        Get
            Return "MonthlyTable"
        End Get
    End Property

    Protected Overrides ReadOnly Property ImportTypeName As String
        Get
            Return "Monthly"
        End Get
    End Property

    Protected Overrides Function GetDtoType() As Type
        Return GetType(MonthlyInDto)
    End Function

    Protected Overrides Function MapRow(row As DataRow) As Object
        Return New MonthlyInDto With {
            .Year = CInt(row("Year")),
            .Month = CInt(row("Month")),
            .Value = CDec(row("Value")),
            .UpdatedAt = CDate(row("UpdatedAt"))
        }
    End Function

    Protected Overrides Sub ValidateSpecific(dto As Object)
        ' 月単位の業務バリデーション
    End Sub

    Protected Overrides Function ResolveDeletePeriods(dtos As List(Of Object)) As List(Of ImportPeriod)
        Dim months = dtos.Cast(Of MonthlyInDto)().
            Select(Function(x) New With {x.Year, x.Month}).
            Distinct()

        Return months.Select(Function(m) ImportPeriod.ForMonth(m.Year, m.Month)).ToList()
    End Function

End Class