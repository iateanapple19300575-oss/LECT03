<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportControl
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.lblLastDateTime = New System.Windows.Forms.Label()
        Me.lblLastCount = New System.Windows.Forms.Label()
        Me.lblLastMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(17, 18)
        Me.txtFilePath.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(476, 28)
        Me.txtFilePath.TabIndex = 0
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(536, 20)
        Me.btnSelect.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(127, 29)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "参照..."
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(537, 64)
        Me.btnImport.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(127, 29)
        Me.btnImport.TabIndex = 5
        Me.btnImport.Text = "取込"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(538, 108)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(127, 29)
        Me.btnPreview.TabIndex = 6
        Me.btnPreview.Text = "確認"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'lblLastDateTime
        '
        Me.lblLastDateTime.AutoSize = True
        Me.lblLastDateTime.Location = New System.Drawing.Point(15, 61)
        Me.lblLastDateTime.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblLastDateTime.Name = "lblLastDateTime"
        Me.lblLastDateTime.Size = New System.Drawing.Size(130, 17)
        Me.lblLastDateTime.TabIndex = 1
        Me.lblLastDateTime.Text = "2026/01/01 00:00:00"
        '
        'lblLastCount
        '
        Me.lblLastCount.AutoSize = True
        Me.lblLastCount.Location = New System.Drawing.Point(184, 61)
        Me.lblLastCount.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblLastCount.Name = "lblLastCount"
        Me.lblLastCount.Size = New System.Drawing.Size(63, 17)
        Me.lblLastCount.TabIndex = 2
        Me.lblLastCount.Text = "10,000 件"
        '
        'lblLastMessage
        '
        Me.lblLastMessage.AutoSize = True
        Me.lblLastMessage.Location = New System.Drawing.Point(286, 61)
        Me.lblLastMessage.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblLastMessage.Name = "lblLastMessage"
        Me.lblLastMessage.Size = New System.Drawing.Size(138, 17)
        Me.lblLastMessage.TabIndex = 3
        Me.lblLastMessage.Text = "１２３４５６７８９０"
        '
        'ImportControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.lblLastMessage)
        Me.Controls.Add(Me.lblLastCount)
        Me.Controls.Add(Me.lblLastDateTime)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.txtFilePath)
        Me.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "ImportControl"
        Me.Size = New System.Drawing.Size(681, 156)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents btnSelect As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents btnPreview As Button
    Friend WithEvents lblLastDateTime As Label
    Friend WithEvents lblLastCount As Label
    Friend WithEvents lblLastMessage As Label
End Class
