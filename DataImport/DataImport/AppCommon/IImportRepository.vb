' IImportRepository.vb
Public Interface IImportRepository(Of T)
    ' トランザクション付きで、削除→BulkCopy→履歴までまとめて実行
    Sub ExecuteWithTransaction(dtos As IEnumerable(Of T), count As Integer)
End Interface