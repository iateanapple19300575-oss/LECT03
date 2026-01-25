Imports System.Windows.Forms
Imports System.Drawing

Public Class FormBase
    Inherits Form

    Public Property UiScale As Single = 1.0F
    Private _scaled As Boolean = False

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        'Me.AutoScaleMode = AutoScaleMode.Dpi
        If Not _scaled AndAlso UiScale <> 1.0F Then
            ApplyScaleToForm(Me, UiScale)
            _scaled = True
        End If
    End Sub

    '-----------------------------------------
    ' フォーム全体をスケール
    '-----------------------------------------
    Protected Sub ApplyScaleToForm(frm As Form, scale As Single)
        frm.Width = CInt(frm.Width * scale)
        frm.Height = CInt(frm.Height * scale)

        For Each ctrl As Control In frm.Controls
            ApplyScaleToControl(ctrl, scale)
        Next
    End Sub

    '-----------------------------------------
    ' コントロールを再帰的にスケール
    '-----------------------------------------
    Protected Sub ApplyScaleToControl(ctrl As Control, scale As Single)

        Dim auto As Boolean = ctrl.AutoSize

        ctrl.Left = CInt(ctrl.Left * scale)
        ctrl.Top = CInt(ctrl.Top * scale)

        If Not auto Then
            ctrl.Width = CInt(ctrl.Width * scale)
            ctrl.Height = CInt(ctrl.Height * scale)
        End If

        ctrl.Font = New Font(ctrl.Font.FontFamily, ctrl.Font.Size * scale, ctrl.Font.Style)

        If TypeOf ctrl Is DataGridView Then
            Dim dgv = DirectCast(ctrl, DataGridView)
            For Each col As DataGridViewColumn In dgv.Columns
                col.Width = CInt(col.Width * scale)
            Next
        End If

        For Each child As Control In ctrl.Controls
            ApplyScaleToControl(child, scale)
        Next
    End Sub

End Class