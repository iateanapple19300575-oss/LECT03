Public Class MDIUtil
    Public Shared Function GetMdiClient(mdiParent As Form) As MdiClient
        For Each ctrl As Control In mdiParent.Controls
            If TypeOf ctrl Is MdiClient Then
                Return CType(ctrl, MdiClient)
            End If
        Next
        Return Nothing
    End Function

End Class
