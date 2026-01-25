Public Class FrmCsvImport
    Inherits FormBase
    
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Private Sub FrmCsvImport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim controller As New XxxImportViewController(Me)
        'Me.AutoScaleMode = AutoScaleMode.None
        'Me.AutoScaleDimensions = New SizeF(96.0F, 96.0F)

        Me.UiScale = 1.0

    End Sub
End Class