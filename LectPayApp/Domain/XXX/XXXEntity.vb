
Imports System.Net

Namespace Domain.User

    ''' <summary>
    ''' ユーザーを表すドメインエンティティ。
    ''' ビジネスルールはこのクラス内で完結させる。
    ''' </summary>
    Public Class XXXEntity

        ''' <summary>
        ''' ユーザーID（0 の場合は未登録）
        ''' </summary>
        Public Property User_Id As String

        ''' <summary>
        ''' ユーザー名
        ''' </summary>
        Public Property User_Name As String

        ''' <summary>
        ''' ユーザー名
        ''' </summary>
        Public Property User_Address As String

        ''' <summary>
        ''' ユーザー名
        ''' </summary>
        Public Property User_TelNo As String

        ''' <summary>
        ''' 年齢
        ''' </summary>
        Public Property Age As Integer
        Public Sub New()
        End Sub

        '''' <summary>
        '''' コンストラクタは外部から呼ばせない（不正な生成を防ぐ）
        '''' </summary>
        'Public Sub New(id As String, name As String, address As String, telno As String, age As Integer)
        '    Me.User_Id = id
        '    Me.User_Name = name
        '    Me.User_Address = address
        '    Me.User_TelNo = telno
        '    Me.Age = age
        'End Sub

        ''===========================================================
        '' Factory メソッド（新規作成）
        ''===========================================================
        'Public Shared Function Create(id As String, name As String, address As String, telno As String, age As Integer) As XXXEntity

        '    If String.IsNullOrEmpty(name) Then
        '        Throw New DomainException("ユーザー名は必須です。")
        '    End If

        '    If age < 0 OrElse age > 120 Then
        '        Throw New DomainException("年齢が不正です。")
        '    End If

        '    Return New XXXEntity(id, name, address, telno, age)
        'End Function

        ''===========================================================
        '' Factory メソッド（DB からの復元）
        ''===========================================================
        'Public Shared Function Reconstruct(id As String, name As String, address As String, telno As String, age As Integer) As XXXEntity

        '    ' DB からの復元でも Domain ルールは守る
        '    If String.IsNullOrEmpty(id) Then
        '        Throw New DomainException("ID が不正です。")
        '    End If

        '    If String.IsNullOrEmpty(name) Then
        '        Throw New DomainException("ユーザー名が不正です。")
        '    End If

        '    If age < 0 OrElse age > 120 Then
        '        Throw New DomainException("年齢が不正です。")
        '    End If

        '    Return New XXXEntity(id, name, address, telno, age)
        'End Function

        ''===========================================================
        '' 値の変更（必要なら）
        ''===========================================================
        'Public Function ChangeName(newName As String) As XXXEntity
        '    If String.IsNullOrEmpty(newName) Then
        '        Throw New DomainException("ユーザー名は必須です。")
        '    End If

        '    Return New XXXEntity(Me.User_Id, newName, Me.User_Address, Me.User_TelNo, Me.Age)
        'End Function

        'Public Function ChangeAge(newAge As Integer) As XXXEntity
        '    If newAge < 0 OrElse newAge > 120 Then
        '        Throw New DomainException("年齢が不正です。")
        '    End If

        '    Return New XXXEntity(Me.User_Id, Me.User_Name, Me.User_Address, Me.User_TelNo, newAge)
        'End Function

    End Class

End Namespace