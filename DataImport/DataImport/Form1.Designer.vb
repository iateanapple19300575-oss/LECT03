<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnImportCustomer = New System.Windows.Forms.Button()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.btnImportProduct = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.dgvError = New System.Windows.Forms.DataGridView()
        CType(Me.dgvError, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnImportCustomer
        '
        Me.btnImportCustomer.Location = New System.Drawing.Point(379, 12)
        Me.btnImportCustomer.Name = "btnImportCustomer"
        Me.btnImportCustomer.Size = New System.Drawing.Size(194, 32)
        Me.btnImportCustomer.TabIndex = 0
        Me.btnImportCustomer.Text = "Button1"
        Me.btnImportCustomer.UseVisualStyleBackColor = True
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(33, 57)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(38, 12)
        Me.lblCount.TabIndex = 1
        Me.lblCount.Text = "Label1"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(151, 57)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(38, 12)
        Me.lblDate.TabIndex = 2
        Me.lblDate.Text = "Label1"
        '
        'btnImportProduct
        '
        Me.btnImportProduct.Location = New System.Drawing.Point(379, 50)
        Me.btnImportProduct.Name = "btnImportProduct"
        Me.btnImportProduct.Size = New System.Drawing.Size(194, 27)
        Me.btnImportProduct.TabIndex = 3
        Me.btnImportProduct.Text = "Button1"
        Me.btnImportProduct.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(22, 12)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(274, 19)
        Me.txtPath.TabIndex = 4
        '
        'dgvError
        '
        Me.dgvError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvError.Location = New System.Drawing.Point(32, 104)
        Me.dgvError.Name = "dgvError"
        Me.dgvError.RowTemplate.Height = 21
        Me.dgvError.Size = New System.Drawing.Size(540, 298)
        Me.dgvError.TabIndex = 5
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 425)
        Me.Controls.Add(Me.dgvError)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.btnImportProduct)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.btnImportCustomer)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.dgvError, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnImportCustomer As Button
    Friend WithEvents lblCount As Label
    Friend WithEvents lblDate As Label
    Friend WithEvents btnImportProduct As Button
    Friend WithEvents txtPath As TextBox
    Friend WithEvents dgvError As DataGridView
End Class
