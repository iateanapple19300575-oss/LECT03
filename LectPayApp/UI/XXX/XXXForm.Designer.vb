<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XXXForm
    Inherits System.Windows.Forms.Form

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
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.txtAge = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtTelNo = New System.Windows.Forms.TextBox()
        Me.SeasonComboBox1 = New SeasonComboBox()
        Me.HalfTermComboBox1 = New HalfTermComboBox()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(640, 25)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(137, 28)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(62, 45)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(148, 19)
        Me.txtUserId.TabIndex = 1
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(62, 77)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(148, 19)
        Me.txtUserName.TabIndex = 2
        '
        'dgvList
        '
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(12, 234)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(575, 204)
        Me.dgvList.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(640, 59)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(137, 28)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(640, 95)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(137, 28)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(640, 129)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(137, 28)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'txtAge
        '
        Me.txtAge.Location = New System.Drawing.Point(62, 184)
        Me.txtAge.Name = "txtAge"
        Me.txtAge.Size = New System.Drawing.Size(148, 19)
        Me.txtAge.TabIndex = 7
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(62, 114)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(148, 19)
        Me.txtAddress.TabIndex = 10
        '
        'txtTelNo
        '
        Me.txtTelNo.Location = New System.Drawing.Point(62, 148)
        Me.txtTelNo.Name = "txtTelNo"
        Me.txtTelNo.Size = New System.Drawing.Size(148, 19)
        Me.txtTelNo.TabIndex = 11
        '
        'SeasonComboBox1
        '
        Me.SeasonComboBox1.FormattingEnabled = True
        Me.SeasonComboBox1.Location = New System.Drawing.Point(298, 100)
        Me.SeasonComboBox1.Name = "SeasonComboBox1"
        Me.SeasonComboBox1.Size = New System.Drawing.Size(135, 20)
        Me.SeasonComboBox1.TabIndex = 9
        '
        'HalfTermComboBox1
        '
        Me.HalfTermComboBox1.FormattingEnabled = True
        Me.HalfTermComboBox1.Location = New System.Drawing.Point(302, 37)
        Me.HalfTermComboBox1.Name = "HalfTermComboBox1"
        Me.HalfTermComboBox1.Size = New System.Drawing.Size(153, 20)
        Me.HalfTermComboBox1.TabIndex = 8
        '
        'XXXForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.txtTelNo)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.SeasonComboBox1)
        Me.Controls.Add(Me.HalfTermComboBox1)
        Me.Controls.Add(Me.txtAge)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.btnSave)
        Me.Name = "XXXForm"
        Me.Text = "XXXForm"
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSave As Button
    Friend WithEvents txtUserId As TextBox
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents txtAge As TextBox
    Friend WithEvents HalfTermComboBox1 As HalfTermComboBox
    Friend WithEvents SeasonComboBox1 As SeasonComboBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents txtTelNo As TextBox
End Class
