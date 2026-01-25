Namespace Framework
    Public NotInheritable Class QueryBuilder

        Private Sub New()
        End Sub

        ''' <summary>
        ''' TableDefinitionの対象項目と項目値を元にSELECT文を自動生成する。
        ''' </summary>
        ''' <param name="def"></param>
        ''' <returns></returns>
        Public Shared Function BuildSelect(def As TableDefinition) As String
            Return "SELECT " & String.Join(", ", def.Columns.ToArray()) &
                   " FROM " & def.TableName
        End Function

        ''' <summary>
        ''' TableDefinitionの対象項目と項目値を元にINSERT文を自動生成する。
        ''' </summary>
        ''' <param name="def"></param>
        ''' <returns></returns>
        Public Shared Function BuildInsert(def As TableDefinition) As String
            Dim colList As String = String.Join(", ", def.Columns.ToArray())
            Dim paramList As String = "@" & String.Join(", @", def.Columns.ToArray())

            Return "INSERT INTO " & def.TableName &
                   " (" & colList & ") VALUES (" & paramList & ")"
        End Function

        ''' <summary>
        ''' TableDefinitionの対象項目と項目値を元にUPDATE文を自動生成する。
        ''' </summary>
        ''' <param name="def"></param>
        ''' <returns></returns>
        Public Shared Function BuildUpdate(def As TableDefinition) As String
            Dim setList As New List(Of String)

            ' UPDATE句の自動生成
            For Each col In def.Columns
                If Not def.KeyColumns.Contains(col) Then
                    setList.Add(col & "=@" & col)
                End If
            Next

            ' WHERE句の自動生成
            Dim whereList As New List(Of String)
            For Each key In def.KeyColumns
                whereList.Add(key & "=@" & key)
            Next

            ' UPDATE SQL返却
            Return "UPDATE " & def.TableName &
                   " SET " & String.Join(", ", setList.ToArray()) &
                   " WHERE " & String.Join(" AND ", whereList.ToArray())
        End Function

        ''' <summary>
        ''' TableDefinitionの対象項目と項目値を元にDELETE文を自動生成する。
        ''' </summary>
        ''' <param name="def"></param>
        ''' <returns></returns>
        Public Shared Function BuildDelete(def As TableDefinition) As String
            Dim whereList As New List(Of String)

            ' WHERE句の自動生成
            For Each key In def.KeyColumns
                whereList.Add(key & "=@" & key)
            Next

            ' DELETE SQL文返却
            Return "DELETE FROM " & def.TableName &
                   " WHERE " & String.Join(" AND ", whereList.ToArray())
        End Function

    End Class
End Namespace