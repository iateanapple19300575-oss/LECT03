Imports Framework.Validation

Namespace UI.XXX

    ''' <summary>
    ''' XXX 画面の入力チェックを担当するバリデータ。
    ''' </summary>
    Public Class XXXValidator
        Implements IValidator(Of XXXForm)

        Private ReadOnly _form As XXXForm

        ''' <summary>
        ''' コンストラクタ。
        ''' </summary>
        Public Sub New(form As XXXForm)
            _form = form
        End Sub

        ''' <summary>
        ''' 入力チェックを実行する。
        ''' </summary>
        Public Function Validate(entity As XXXForm) As ValidationResult _
            Implements IValidator(Of XXXForm).Validate

            ' entity と _form は同じものなので entity を使う
            If String.IsNullOrEmpty(entity.txtUserId.Text) Then
                Return ValidationResult.Fail("ユーザIDを入力してください。")
            End If

            If String.IsNullOrEmpty(entity.txtUserName.Text) Then
                Return ValidationResult.Fail("ユーザ名を入力してください。")
            End If

            Dim num As Integer
            If Not Integer.TryParse(entity.txtAge.Text, num) Then
                Return ValidationResult.Fail("年齢は数値で入力してください。")
            End If

            Return ValidationResult.Ok()
        End Function

    End Class

End Namespace