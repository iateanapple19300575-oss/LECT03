Namespace Framework.Data

    ''' <summary>
    ''' SQL 文の組み立てを一元化するユーティリティクラス。
    ''' SELECT / INSERT / UPDATE / DELETE といった基本的な SQL を安全かつ簡潔に生成する。
    ''' 列名やテーブル名を文字列結合で扱う際の記述ミスを防ぎ、再利用性を高める目的で使用される。
    ''' </summary>
    Public NotInheritable Class QueryBuilder

        ' ============================
        ' SELECT
        ' ============================

        ''' <summary>
        ''' 指定したテーブルの全列を取得する SELECT 文を生成する。
        ''' </summary>
        ''' <param name="tableName">対象テーブル名。</param>
        ''' <returns>SELECT * FROM テーブル名 の SQL 文。</returns>
        Public Shared Function SelectAll(tableName As String) As String
            Return String.Format("SELECT * FROM {0}", tableName)
        End Function

        ''' <summary>
        ''' 指定した列のみを取得する SELECT 文を生成する。
        ''' </summary>
        ''' <param name="tableName">対象テーブル名。</param>
        ''' <param name="columns">取得する列名の配列。</param>
        ''' <returns>SELECT 列 FROM テーブル名 の SQL 文。</returns>
        Public Shared Function SelectColumns(tableName As String, ParamArray columns() As String) As String
            Dim col As String = String.Join(",", columns)
            Return String.Format("SELECT {0} FROM {1}", col, tableName)
        End Function

        ''' <summary>
        ''' WHERE 句付きの SELECT 文を生成する。
        ''' </summary>
        ''' <param name="tableName">対象テーブル名。</param>
        ''' <param name="whereClause">WHERE 条件（パラメータ化前提）。</param>
        ''' <returns>SELECT * FROM テーブル名 WHERE 条件 の SQL 文。</returns>
        Public Shared Function SelectWhere(tableName As String, whereClause As String) As String
            Return String.Format("SELECT * FROM {0} WHERE {1}", tableName, whereClause)
        End Function

        ''' <summary>
        ''' TOP 句付きの SELECT 文を生成する。
        ''' </summary>
        ''' <param name="tableName">対象テーブル名。</param>
        ''' <param name="top">取得件数。</param>
        ''' <param name="whereClause">WHERE 条件（任意）。</param>
        ''' <returns>TOP 指定の SELECT 文。</returns>
        Public Shared Function SelectTop(tableName As String, top As Integer, Optional whereClause As String = Nothing) As String
            If String.IsNullOrEmpty(whereClause) Then
                Return String.Format("SELECT TOP {0} * FROM {1}", top, tableName)
            Else
                Return String.Format("SELECT TOP {0} * FROM {1} WHERE {2}", top, tableName, whereClause)
            End If
        End Function

        ' ============================
        ' INSERT
        ' ============================

        ''' <summary>
        ''' INSERT 文を生成する。
        ''' 列名とパラメータ名を自動で対応付ける。
        ''' </summary>
        ''' <param name="tableName">対象テーブル名。</param>
        ''' <param name="columns">挿入対象の列名一覧。</param>
        ''' <returns>INSERT INTO テーブル名 (列) VALUES (@列) の SQL 文。</returns>
        Public Shared Function Insert(tableName As String, columns As IEnumerable(Of String)) As String
            Dim colList As String = String.Join(",", columns.ToArray())
            Dim paramList As String = "@" & String.Join(",@", columns.ToArray())

            Return String.Format(
                "INSERT INTO {0} ({1}) VALUES ({2})",
                tableName, colList, paramList
            )
        End Function

        ' ============================
        ' UPDATE
        ' ============================

        ''' <summary>
        ''' UPDATE 文を生成する。
        ''' 列名=@列名 の形式で SET 句を自動生成する。
        ''' </summary>
        ''' <param name="tableName">対象テーブル名。</param>
        ''' <param name="columns">更新対象の列名一覧。</param>
        ''' <param name="whereClause">WHERE 条件。</param>
        ''' <returns>UPDATE テーブル名 SET 列=@列 WHERE 条件 の SQL 文。</returns>
        Public Shared Function Update(tableName As String, columns As IEnumerable(Of String), whereClause As String) As String
            Dim setList As New List(Of String)

            For Each col In columns
                setList.Add(String.Format("{0}=@{0}", col))
            Next

            Return String.Format(
                "UPDATE {0} SET {1} WHERE {2}",
                tableName,
                String.Join(",", setList.ToArray()),
                whereClause
            )
        End Function

        ' ============================
        ' DELETE
        ' ============================

        ''' <summary>
        ''' DELETE 文を生成する。
        ''' </summary>
        ''' <param name="tableName">対象テーブル名。</param>
        ''' <param name="whereClause">WHERE 条件。</param>
        ''' <returns>DELETE FROM テーブル名 WHERE 条件 の SQL 文。</returns>
        Public Shared Function Delete(tableName As String, whereClause As String) As String
            Return String.Format("DELETE FROM {0} WHERE {1}", tableName, whereClause)
        End Function

        ' ============================
        ' WHERE / ORDER BY（補助）
        ' ============================

        ''' <summary>
        ''' 単純な等価比較の WHERE 句を生成する。
        ''' 例: Column=@Column
        ''' </summary>
        ''' <param name="column">列名。</param>
        ''' <returns>Column=@Column の文字列。</returns>
        Public Shared Function WhereEquals(column As String) As String
            Return String.Format("{0}=@{0}", column)
        End Function

        ''' <summary>
        ''' 任意の WHERE 条件をそのまま返す。
        ''' 複雑な条件式を直接指定したい場合に使用する。
        ''' </summary>
        ''' <param name="condition">WHERE 条件式。</param>
        ''' <returns>指定された条件文字列。</returns>
        Public Shared Function WhereRaw(condition As String) As String
            Return condition
        End Function

        ''' <summary>
        ''' ORDER BY 句を生成する。
        ''' </summary>
        ''' <param name="columns">並び替え対象の列名。</param>
        ''' <returns>ORDER BY 列1,列2,... の文字列。</returns>
        Public Shared Function OrderBy(ParamArray columns() As String) As String
            Return "ORDER BY " & String.Join(",", columns)
        End Function

    End Class

End Namespace