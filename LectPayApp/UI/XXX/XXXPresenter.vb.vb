Imports Application.User

Namespace UI.XXX

    ''' <summary>
    ''' XXX 画面の表示更新を担当するプレゼンター。
    ''' </summary>
    Public Class XXXPresenter

        Private ReadOnly _form As XXXForm

        ''' <summary>
        ''' コンストラクタ。
        ''' </summary>
        Public Sub New(form As XXXForm)
            _form = form
        End Sub

        ''' <summary>
        ''' 一覧を画面に表示する。
        ''' </summary>
        Public Sub ShowList(list As List(Of XXXDto))
            _form.SetList(list)
        End Sub

        ''' <summary>
        ''' 詳細情報を画面に表示する。
        ''' </summary>
        Public Sub ShowDetail(dto As XXXDto)
            _form.SetDetail(dto)
        End Sub

    End Class

End Namespace