<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMasterEdit
    Inherits FormBase

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.txtSiteCode = New System.Windows.Forms.TextBox()
        Me.txtGrade = New System.Windows.Forms.TextBox()
        Me.txtClassCode = New System.Windows.Forms.TextBox()
        Me.txtKoma = New System.Windows.Forms.TextBox()
        Me.txtSubjects = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(645, 99)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(143, 26)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(645, 139)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(143, 26)
        Me.btnEdit.TabIndex = 8
        Me.btnEdit.Text = "編集"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(645, 179)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(143, 26)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(645, 219)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(143, 26)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'dgv
        '
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(24, 66)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowTemplate.Height = 21
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(590, 366)
        Me.dgv.TabIndex = 5
        '
        'txtSiteCode
        '
        Me.txtSiteCode.Location = New System.Drawing.Point(24, 28)
        Me.txtSiteCode.Name = "txtSiteCode"
        Me.txtSiteCode.Size = New System.Drawing.Size(94, 19)
        Me.txtSiteCode.TabIndex = 0
        '
        'txtGrade
        '
        Me.txtGrade.Location = New System.Drawing.Point(148, 28)
        Me.txtGrade.Name = "txtGrade"
        Me.txtGrade.Size = New System.Drawing.Size(94, 19)
        Me.txtGrade.TabIndex = 1
        '
        'txtClassCode
        '
        Me.txtClassCode.Location = New System.Drawing.Point(272, 28)
        Me.txtClassCode.Name = "txtClassCode"
        Me.txtClassCode.Size = New System.Drawing.Size(94, 19)
        Me.txtClassCode.TabIndex = 2
        '
        'txtKoma
        '
        Me.txtKoma.Location = New System.Drawing.Point(396, 28)
        Me.txtKoma.Name = "txtKoma"
        Me.txtKoma.Size = New System.Drawing.Size(94, 19)
        Me.txtKoma.TabIndex = 3
        '
        'txtSubjects
        '
        Me.txtSubjects.Location = New System.Drawing.Point(520, 28)
        Me.txtSubjects.Name = "txtSubjects"
        Me.txtSubjects.Size = New System.Drawing.Size(94, 19)
        Me.txtSubjects.TabIndex = 4
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(645, 28)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(143, 26)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'FrmMasterEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtSubjects)
        Me.Controls.Add(Me.txtKoma)
        Me.Controls.Add(Me.txtClassCode)
        Me.Controls.Add(Me.txtGrade)
        Me.Controls.Add(Me.txtSiteCode)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAdd)
        Me.Name = "FrmMasterEdit"
        Me.Text = "Form1"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnAdd As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents dgv As DataGridView
    Friend WithEvents txtSiteCode As TextBox
    Friend WithEvents txtGrade As TextBox
    Friend WithEvents txtClassCode As TextBox
    Friend WithEvents txtKoma As TextBox
    Friend WithEvents txtSubjects As TextBox
    Friend WithEvents btnSave As Button
End Class
