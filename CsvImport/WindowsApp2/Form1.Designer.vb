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
        Me.cmbCsvType = New System.Windows.Forms.ComboBox()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmbCsvType
        '
        Me.cmbCsvType.DisplayMember = """Yearly"",""Monthly"",""NoPeriod"""
        Me.cmbCsvType.FormattingEnabled = True
        Me.cmbCsvType.Items.AddRange(New Object() {"Yearly", "Monthly", "NoPeriod"})
        Me.cmbCsvType.Location = New System.Drawing.Point(35, 49)
        Me.cmbCsvType.Name = "cmbCsvType"
        Me.cmbCsvType.Size = New System.Drawing.Size(87, 20)
        Me.cmbCsvType.TabIndex = 5
        Me.cmbCsvType.ValueMember = """Yearly"",""Monthly"",""NoPeriod"""
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(12, 12)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(432, 19)
        Me.txtFilePath.TabIndex = 4
        Me.txtFilePath.Text = "C:\開発３\Lesson02\月単位.csv"
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(259, 55)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(185, 39)
        Me.btnImport.TabIndex = 3
        Me.btnImport.Text = "Button1"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 135)
        Me.Controls.Add(Me.cmbCsvType)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.btnImport)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbCsvType As ComboBox
    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents btnImport As Button
End Class
