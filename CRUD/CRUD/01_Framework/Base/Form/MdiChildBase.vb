Imports System.Windows.Forms
Imports System.Drawing

Public Class MdiChildBase
    Inherits FormBase

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        AdjustToMdiClient()
    End Sub

    '-----------------------------------------
    ' MDI 親の MdiClient に合わせて最大化サイズを調整
    '-----------------------------------------
    Private Sub AdjustToMdiClient()
        If Me.MdiParent Is Nothing Then Exit Sub

        Dim client As MdiClient = Nothing

        For Each ctrl As Control In Me.MdiParent.Controls
            If TypeOf ctrl Is MdiClient Then
                client = CType(ctrl, MdiClient)
                Exit For
            End If
        Next

        If client Is Nothing Then Exit Sub

        Me.MaximumSize = client.ClientSize

        If Me.WindowState = FormWindowState.Maximized Then
            Me.Bounds = client.ClientRectangle
        End If
    End Sub

End Class