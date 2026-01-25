Namespace Entities

    ''' <summary>
    ''' CSV インポートで取り込む 1 行分のデータを表すエンティティ。
    ''' </summary>
    ''' <remarks>
    ''' CsvImportTemplate の ConvertToEntity で生成され、Validate や Register の処理で使用されます。<br/>
    ''' 業務ロジックを持たない純粋なデータ保持クラスとして扱います。
    ''' </remarks>
    Public Class MasterEditEntity

        ''' <summary>
        ''' サイトコードを取得または設定します。
        ''' </summary>
        Public Property Site_Code As String

        ''' <summary>
        ''' 学年を取得または設定します。
        ''' </summary>
        Public Property Grade As String

        ''' <summary>
        ''' クラスコードを取得または設定します。
        ''' </summary>
        Public Property Class_Code As String

        ''' <summary>
        ''' コマ番号（連番）を取得または設定します。
        ''' </summary>
        Public Property Koma_Seq As String

        ''' <summary>
        ''' 科目名を取得または設定します。
        ''' </summary>
        Public Property Subjects As String

    End Class

End Namespace