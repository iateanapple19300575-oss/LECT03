Imports System.Data
Imports System.Data.SqlClient

Public MustInherit Class CsvImporterBase

    Private ReadOnly _filePath As String
    Private ReadOnly _repo As New ImportRepository()
    Private ReadOnly _historyRepo As New ImportHistoryRepository()
    Private ReadOnly _errorRepo As New ImportErrorRepository()

    Protected Sub New(filePath As String)
        _filePath = filePath
    End Sub

    Public Function Execute() As ImportResult
        Dim result As New ImportResult()
        Dim dt As DataTable = Nothing
        Dim dtos As New List(Of Object)()
        Dim rowIndex As Integer = 0

        Try
            ' CSV 読み込み
            Dim rows = LoadCsv()
            dt = CreateDataTable()

            ' DTO 生成 & バリデーション
            For Each row As DataRow In rows.Rows
                rowIndex += 1
                Dim dto As Object = Nothing
                Try
                    dto = MapRow(row)
                    ValidateCommon(dto)
                    ValidateSpecific(dto)
                    dtos.Add(dto)
                    AddToDataTable(dt, dto)
                    result.SuccessCount += 1
                Catch ex As Exception
                    ' エラー行は後で DB にも保存する（トランザクション内）
                    LogErrorToFile(rowIndex, dto, ex)
                    result.ErrorCount += 1
                End Try
            Next

            ' 削除対象期間の決定
            Dim periods = ResolveDeletePeriods(dtos)

            ' トランザクション開始
            Using cn As New SqlConnection("Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI")
                cn.Open()
                Using tran = cn.BeginTransaction(Me.TransactionIsolationLevel)

                    Try
                        ' 事前削除
                        For Each p In periods
                            _repo.DeleteWithTran(cn, tran, TargetTableName, p)
                        Next

                        ' BulkCopy
                        BulkCopyExecutor.ExecuteWithTran(
                            cn, tran, TargetTableName, dt,
                            BulkCopyBatchSize,
                            BulkCopyNotifyAfter,
                            AddressOf OnRowsCopied
                        )

                        ' 履歴登録
                        Dim entity As New ImportHistoryEntity With {
                            .ImportType = ImportTypeName,
                            .TargetTable = TargetTableName,
                            .ImportedCount = result.SuccessCount,
                            .ErrorCount = result.ErrorCount,
                            .Periods = String.Join(",", periods.Select(Function(p) p.ToString()).ToArray()),
                            .ImportedAt = DateTime.Now
                        }
                        _historyRepo.InsertWithTran(cn, tran, entity)

                        ' エラー行を DB に保存（必要ならここで別途保持しておいた情報を使う）
                        ' ※簡易版としては、ファイルログのみでもよい

                        tran.Commit()

                    Catch ex As Exception
                        tran.Rollback()
                        Throw
                    End Try

                End Using
            End Using

        Catch ex As Exception
            result.ErrorMessage = ex.Message
        End Try

        Return result
    End Function

    ' --- トランザクション設定 ---
    Protected Overridable ReadOnly Property TransactionIsolationLevel As IsolationLevel
        Get
            Return IsolationLevel.ReadCommitted
        End Get
    End Property

    ' --- BulkCopy 設定 ---
    Protected Overridable ReadOnly Property BulkCopyBatchSize As Integer
        Get
            Return 5000
        End Get
    End Property

    Protected Overridable ReadOnly Property BulkCopyNotifyAfter As Integer
        Get
            Return 5000
        End Get
    End Property

    Protected Overridable Sub OnRowsCopied(sender As Object, e As SqlRowsCopiedEventArgs)
        ' 必要なら進捗ログ
        ' File.AppendAllText("bulkcopy_progress.log",
        '                   String.Format("{0} Copied:{1}", DateTime.Now, e.RowsCopied) & Environment.NewLine)
    End Sub

    ' --- 共通処理 ---
    Protected Overridable Function LoadCsv() As DataTable
        Return CsvParser.Load(_filePath)
    End Function

    Protected Overridable Sub ValidateCommon(dto As Object)
        ' 必須チェックなど共通があればここに
    End Sub

    Protected Overridable Function CreateDataTable() As DataTable
        Return DataTableBuilder.FromDtoType(GetDtoType())
    End Function

    Protected Overridable Sub AddToDataTable(dt As DataTable, dto As Object)
        DataTableBuilder.AddRow(dt, dto)
    End Sub

    Protected Sub LogErrorToFile(rowIndex As Integer, dto As Object, ex As Exception)
        Dim dtoStr As String = If(dto Is Nothing, "(null)",
            String.Join(",",
                dto.GetType().GetProperties().
                    Select(Function(p) String.Format("{0}={1}", p.Name, p.GetValue(dto, Nothing)))))

        Dim log = String.Format("{0} 行:{1} DTO:{2} Error:{3}",
                                DateTime.Now, rowIndex, dtoStr, ex.Message)
        System.IO.File.AppendAllText("import_error.log", log & Environment.NewLine)
    End Sub

    ' --- サブクラスが実装する差分 ---
    Protected MustOverride Function MapRow(row As DataRow) As Object
    Protected MustOverride Sub ValidateSpecific(dto As Object)
    Protected MustOverride Function ResolveDeletePeriods(dtos As List(Of Object)) As List(Of ImportPeriod)
    Protected MustOverride ReadOnly Property TargetTableName As String
    Protected MustOverride ReadOnly Property ImportTypeName As String
    Protected MustOverride Function GetDtoType() As Type

End Class