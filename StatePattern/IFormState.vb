Public Interface IFormState
    Sub Apply(form As MainForm)
    Sub OnAdd(form As MainForm)
    Sub OnEdit(form As MainForm)
    Sub OnSave(form As MainForm)
    Sub OnCancel(form As MainForm)
End Interface