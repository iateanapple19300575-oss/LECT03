Namespace Framework

    ''' <summary>
    ''' テーブル定義を保持するクラス。
    ''' テーブル名、カラム一覧、キー項目一覧を管理する。
    ''' </summary>
    Public Class TableDefinition

        ''' <summary>
        ''' テーブル名を取得または設定する。
        ''' </summary>
        ''' <returns>テーブル名を表す文字列</returns>
        Public Property TableName As String

        ''' <summary>
        ''' テーブルに属する全カラム名のリストを取得または設定する。
        ''' </summary>
        ''' <returns>カラム名のリスト</returns>
        Public Property Columns As List(Of String)

        ''' <summary>
        ''' テーブルのキー項目名のリストを取得または設定する。
        ''' </summary>
        ''' <returns>キー項目名のリスト</returns>
        Public Property KeyColumns As List(Of String)

        ''' <summary>
        ''' テーブル定義を初期化する。
        ''' </summary>
        ''' <param name="tableName">テーブル名</param>
        ''' <param name="columns">全カラム名のリスト</param>
        ''' <param name="keyColumns">キー項目名のリスト</param>
        Public Sub New(tableName As String, columns As List(Of String), keyColumns As List(Of String))
            Me.TableName = tableName
            Me.Columns = columns
            Me.KeyColumns = keyColumns
        End Sub

    End Class

End Namespace