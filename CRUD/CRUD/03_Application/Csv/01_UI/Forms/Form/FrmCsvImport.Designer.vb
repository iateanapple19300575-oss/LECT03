<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCsvImport
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
        Me.ImportControl1 = New ImportControl()
        Me.SuspendLayout()
        '
        'ImportControl1
        '
        Me.ImportControl1.AutoSize = True
        Me.ImportControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ImportControl1.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ImportControl1.ImportedAt = New Date(2026, 1, 1, 0, 0, 0, 0)
        Me.ImportControl1.ImportedResult = "１２３４５６７８９０"
        Me.ImportControl1.IsBusy = True
        Me.ImportControl1.Location = New System.Drawing.Point(27, 19)
        Me.ImportControl1.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ImportControl1.Name = "ImportControl1"
        Me.ImportControl1.Size = New System.Drawing.Size(683, 156)
        Me.ImportControl1.TabIndex = 0
        '
        'FrmCsvImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(734, 450)
        Me.Controls.Add(Me.ImportControl1)
        Me.Name = "FrmCsvImport"
        Me.Text = "CsvImport"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ImportControl1 As ImportControl
End Class
