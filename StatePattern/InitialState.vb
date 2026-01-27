Public Class InitialState
    Implements IFormState

    Public Sub Apply(form As MainForm) Implements IFormState.Apply
        form.btnAdd.Enabled = True
        form.btnEdit.Enabled = True
        form.btnDelete.Enabled = False
        form.btnSave.Enabled = False
        form.btnCancel.Enabled = False
        form.txtName.ReadOnly = True
    End Sub

    Public Sub OnAdd(form As MainForm) Implements IFormState.OnAdd
        form.ChangeState(New AddState())
    End Sub

    Public Sub OnEdit(form As MainForm) Implements IFormState.OnEdit
        form.ChangeState(New EditState())
        form.UpdateData()
    End Sub

    Public Sub OnSave(form As MainForm) Implements IFormState.OnSave
        ' 初期状態では保存不可
    End Sub

    Public Sub OnCancel(form As MainForm) Implements IFormState.OnCancel
        ' 初期状態ではキャンセル不可
    End Sub
End Class