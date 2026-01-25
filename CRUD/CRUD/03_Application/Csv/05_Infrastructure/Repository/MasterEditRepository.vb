Imports Entities
Imports Framework
Imports Repositories

''' <summary>
''' XxxImportEntity を対象とした Repository クラス。
''' TableDefinition を基に QueryBuilder で SQL を自動生成し、
''' SqlExecutor と Mapper を用いてデータアクセスを行います。
''' </summary>
''' <remarks>
''' ・SELECT / INSERT / UPDATE / DELETE の SQL は TableDefinition から自動生成されます。<br/>
''' ・Mapper により SqlDataReader → XxxImportEntity への変換が行われます。<br/>
''' ・Repository は DB アクセスのみを担当し、業務ロジックは Service 層に委譲します。
''' </remarks>
Public Class MasterEditRepository
    Inherits BaseRepository(Of MasterEditEntity)

    Private Shared ReadOnly _def As New TableDefinition(
        "CsvImportTable",
        New List(Of String) From {"Site_Code", "Grade", "Class_Code", "Koma_Seq", "Subjects"},
        New List(Of String) From {"Site_Code", "Grade", "Class_Code"}
    )

    Protected Overrides ReadOnly Property Def As TableDefinition
        Get
            Return _def
        End Get
    End Property
End Class