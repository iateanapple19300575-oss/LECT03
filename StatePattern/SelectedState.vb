Public Class SelectedState
    Implements IFormState

    Public Sub Apply(form As MainForm) Implements IFormState.Apply
        form.btnAdd.Enabled = False
        form.btnEdit.Enabled = True
        form.btnDelete.Enabled = True
        form.btnSave.Enabled = True
        form.btnCancel.Enabled = False
        form.txtName.ReadOnly = False
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