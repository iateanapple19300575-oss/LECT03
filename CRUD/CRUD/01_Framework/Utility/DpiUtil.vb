Imports System.Runtime.InteropServices

Public NotInheritable Class DpiUtil

    Private Sub New()
    End Sub

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function LoadLibrary(lpFileName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Ansi)>
    Private Shared Function GetProcAddress(hModule As IntPtr, lpProcName As String) As IntPtr
    End Function

    <UnmanagedFunctionPointer(CallingConvention.StdCall)>
    Private Delegate Function GetDpiForSystemDelegate() As UInteger

    ''' <summary>
    ''' Windows 10/11 の実 DPI を取得する。
    ''' アプリが DPI 非対応でも正しい DPI を返す。
    ''' </summary>
    Public Shared Function GetSystemDpi() As Integer
        Dim hUser32 = LoadLibrary("user32.dll")
        If hUser32 = IntPtr.Zero Then Return 96

        Dim proc = GetProcAddress(hUser32, "GetDpiForSystem")
        If proc = IntPtr.Zero Then Return 96

        Dim fn = CType(Marshal.GetDelegateForFunctionPointer(proc, GetType(GetDpiForSystemDelegate)), GetDpiForSystemDelegate)
        Return CInt(fn())
    End Function

    Public Shared Function GetScaleFactor() As Single
        Return CSng(GetSystemDpi()) / 96.0F
    End Function

End Class