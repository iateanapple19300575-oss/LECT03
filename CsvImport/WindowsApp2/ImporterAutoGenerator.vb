Public Class ImporterAutoGenerator

    Public Shared Function GenerateAll(tableName As String,
                                       csvPath As String,
                                       dtoClassName As String,
                                       importerClassName As String,
                                       importTypeName As String) As String

        Dim dtoCode = DtoGeneratorWithTypeInference.GenerateDto(csvPath, dtoClassName)

        Dim importerCode = ImporterGenerator.GenerateImporter(
            tableName,
            importerClassName,
            dtoClassName,
            importTypeName
        )

        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("' ===== DTO =====")
        sb.AppendLine(dtoCode)
        sb.AppendLine()
        sb.AppendLine("' ===== Importer =====")
        sb.AppendLine(importerCode)

        Return sb.ToString()
    End Function

End Class