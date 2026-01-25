Imports System.Windows.Forms
Imports System.Drawing

Public Class MdiParentBase
    Inherits FormBase

    Private _mdiClient As MdiClient = Nothing

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        _mdiClient = FindMdiClient()

        If UiScale <> 1.0F Then
            ApplyScaleToMdiParent(UiScale)
        End If
    End Sub

    '-----------------------------------------
    ' MDI 親フォームのスケール
    '-----------------------------------------
    Private Sub ApplyScaleToMdiParent(scale As Single)

        Me.Width = CInt(Me.Width * scale)
        Me.Height = CInt(Me.Height * scale)

        For Each ctrl As Control In Me.Controls
            If Not TypeOf ctrl Is MdiClient Then
                ApplyScaleToControl(ctrl, scale)
            End If
        Next

        If _mdiClient IsNot Nothing Then
            _mdiClient.Left = CInt(_mdiClient.Left * scale)
            _mdiClient.Top = CInt(_mdiClient.Top * scale)
            _mdiClient.Width = CInt(_mdiClient.Width * scale)
            _mdiClient.Height = CInt(_mdiClient.Height * scale)
        End If
    End Sub

    '-----------------------------------------
    ' MdiClient を取得
    '-----------------------------------------
    Private Function FindMdiClient() As MdiClient
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is MdiClient Then
                Return CType(ctrl, MdiClient)
            End If
        Next
        Return Nothing
    End Function

End Class