Public Class EditState
    Implements IFormState

    Public Sub Apply(form As MainForm) Implements IFormState.Apply
        form.btnAdd.Enabled = False
        form.btnEdit.Enabled = False
        form.btnDelete.Enabled = False
        form.btnSave.Enabled = True
        form.btnCancel.Enabled = True
        form.txtName.ReadOnly = False
    End Sub

    Public Sub OnAdd(form As MainForm) Implements IFormState.OnAdd
        form.ChangeState(New AddState())
    End Sub

    Public Sub OnEdit(form As MainForm) Implements IFormState.OnEdit
        form.ChangeState(New EditState())
    End Sub

    Public Sub OnSave(form As MainForm) Implements IFormState.OnSave
        form.UpdateData()
        form.ChangeState(New InitialState())
    End Sub

    Public Sub OnCancel(form As MainForm) Implements IFormState.OnCancel
        form.ChangeState(New InitialState())
    End Sub
End Class