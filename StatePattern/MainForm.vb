Public Class MainForm
    Private CurrentState As IFormState

    Public Sub New()
        InitializeComponent()
        ChangeState(New InitialState())
    End Sub

    Public Sub ChangeState(state As IFormState)
        CurrentState = state
        state.Apply(Me)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        CurrentState.OnAdd(Me)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        CurrentState.OnEdit(Me)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        CurrentState.Apply(Me)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        CurrentState.OnSave(Me)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CurrentState.OnCancel(Me)
    End Sub

    'Public Sub UpdateData(state As IFormState)
    '    'CurrentState = state
    '    'state.Apply(Me)
    'End Sub

    Public Sub UpdateData()
        MessageBox.Show("UPDATE")
        'CurrentState = state
        'state.Apply(Me)
    End Sub

    Public Sub SaveData(state As IFormState)
        CurrentState = state
        state.Apply(Me)
    End Sub
End Class
