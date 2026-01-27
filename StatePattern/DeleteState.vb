Public Class DeleteState
    Implements IFormState

    Public Sub Apply(form As MainForm) Implements IFormState.Apply
        form.btnAdd.Enabled = False
        form.btnEdit.Enabled = False
        form.btnDelete.Enabled = False
        form.btnSave.Enabled = False
        form.btnCancel.Enabled = True
        form.txtName.ReadOnly = True
    End Sub

    Public Sub OnAdd(form As MainForm) Implements IFormState.OnAdd
        ' 追加中に追加はできない
    End Sub

    Public Sub OnEdit(form As MainForm) Implements IFormState.OnEdit
        ' 追加中に編集はできない
    End Sub

    Public Sub OnSave(form As MainForm) Implements IFormState.OnSave
        ' 保存処理
        'form.SaveData()
        form.ChangeState(New InitialState())
    End Sub

    Public Sub OnCancel(form As MainForm) Implements IFormState.OnCancel
        form.ChangeState(New InitialState())
    End Sub
End Class