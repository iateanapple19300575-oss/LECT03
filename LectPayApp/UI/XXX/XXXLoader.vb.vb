Namespace UI.XXX

    ''' <summary>
    ''' XXX 画面の初期表示処理を担当するローダー。
    ''' </summary>
    Public Class XXXLoader

        Private ReadOnly _form As XXXForm

        ''' <summary>
        ''' コンストラクタ。
        ''' </summary>
        Public Sub New(form As XXXForm)
            _form = form
        End Sub

        ''' <summary>
        ''' 初期データの読み込みを行う。
        ''' </summary>
        Public Sub LoadInitialData()
            ' 必要に応じてコンボボックス等の初期化を行う
        End Sub

    End Class

End Namespace