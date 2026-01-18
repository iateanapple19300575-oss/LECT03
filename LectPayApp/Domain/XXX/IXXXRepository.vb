Namespace Domain.User

    ''' <summary>
    ''' ユーザー情報の永続化を行うリポジトリのインターフェース。
    ''' Domain 層ではインターフェースのみ定義し、
    ''' 実装は Infrastructure 層に委ねる。
    ''' </summary>
    Public Interface IXXXRepository

        ''' <summary>
        ''' ユーザーを保存する（新規 or 更新）。
        ''' </summary>
        Sub Insert(entity As XXXEntity)

        ''' <summary>
        ''' ユーザーを保存する（新規 or 更新）。
        ''' </summary>
        Sub Update(entity As XXXEntity)

        ''' <summary>
        ''' ユーザーを削除する。
        ''' </summary>
        Sub Delete(userId As Integer)

        ''' <summary>
        ''' ユーザー一覧を取得する。
        ''' </summary>
        Function GetAll() As List(Of XXXEntity)

        ''' <summary>
        ''' ユーザーIDを指定して取得する。
        ''' </summary>
        Function GetById(userId As Integer) As XXXEntity

    End Interface

End Namespace