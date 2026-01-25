Public Class MasterEditModel
    Public Property Add As Boolean = True
    Public Property Edit As Boolean = True
    Public Property Delete As Boolean = True
    Public Property Save As Boolean = False
    Public Property Cancel As Boolean = False

    Public Property CurrentRow As Integer? = 0


    Public Property Site_Code As String = ""
    Public Property Grade As String = ""
    Public Property Class_Code As String = ""
    Public Property Koma_Seq As String = ""
    Public Property Subjects As String = ""

End Class
