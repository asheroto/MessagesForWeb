Imports System.ComponentModel
Imports Microsoft.Web.WebView2.Core

Public Class Main
    Dim AllowClose As Boolean = False

    Private Sub WV_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)
        Try
            If Text <> WV.CoreWebView2.DocumentTitle Then
                Text = WV.CoreWebView2.DocumentTitle
            End If
        Catch ex As Exception
            ' Handle exception (optional)
        End Try
    End Sub

    Private Sub Main_Invalidated(sender As Object, e As InvalidateEventArgs) Handles Me.Invalidated
        ' Register hotkey
        Hotkey.registerHotkey(Me, "m", Hotkey.KeyModifier.Control + Hotkey.KeyModifier.Alt)
    End Sub

    Private Sub SystemTrayIcon_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles SystemTrayIcon.MouseDoubleClick
        DoShow()
    End Sub

    Private Sub Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not AllowClose Then
            e.Cancel = True
            Hide()
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = Hotkey.WM_HOTKEY Then
            Hotkey.handleHotKeyEvent(m.WParam)
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        AllowClose = True
        Hotkey.unregisterHotkeys(Me)
        Close()
    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click
        DoShow()
    End Sub

    Private Sub Startup_Tick(sender As Object, e As EventArgs) Handles Startup.Tick
        Startup.Enabled = False
        Dim s() As String = Environment.GetCommandLineArgs()
        For i = 1 To s.Length - 1
            If LCase(s(i)) = "-startup" Then
                Close()
            End If
        Next
        Opacity = 100
    End Sub

    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem.Click
        Try
            Dim p As New Process()
            p.StartInfo.FileName = "cmd"
            p.StartInfo.Arguments = $"/C timeout /t 3 && start """" ""{Application.ExecutablePath}"""
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            p.Start()
        Catch ex As Exception
            ' Handle exception (optional)
        End Try

        Application.Exit()
    End Sub

    Public Shared Sub DoShow()
        If Main.WindowState = FormWindowState.Minimized Then
            Main.WindowState = FormWindowState.Maximized
        End If
        Main.Show()
        Main.Activate()
    End Sub

    Private Sub WV_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WV.CoreWebView2InitializationCompleted
        AddHandler WV.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
    End Sub

    Private Sub CoreWebView2_NewWindowRequested(sender As Object, e As CoreWebView2NewWindowRequestedEventArgs)
        e.Handled = True

        Try
            Dim p As New Process()
            p.StartInfo.UseShellExecute = True
            p.StartInfo.FileName = e.Uri
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            p.Start()
        Catch ex As Exception
            ' Handle exception (optional)
        End Try
    End Sub
End Class

Public Class Hotkey

#Region "Declarations - WinAPI, Hotkey constant and Modifier Enum"

    ''' <summary>
    '''     Declaration of winAPI function wrappers. The winAPI functions are used to register / unregister a hotkey
    ''' </summary>
    Private Declare Function RegisterHotKey Lib "user32" _
        (hwnd As IntPtr, id As Integer, fsModifiers As Integer, vk As Integer) As Integer

    Private Declare Function UnregisterHotKey Lib "user32" (hwnd As IntPtr, id As Integer) As Integer

    Public Const WM_HOTKEY As Integer = &H312

    Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
    End Enum _
    'This enum is just to make it easier to call the registerHotKey function: The modifier integer codes are replaced by a friendly "Alt","Shift" etc.

#End Region

#Region "Hotkey registration, unregistration and handling"

    Public Shared Sub registerHotkey(ByRef sourceForm As Form, triggerKey As String, modifier As KeyModifier)
        Dim val As Integer
        If triggerKey = "ESC" Then
            val = Keys.Escape
        Else
            val = Asc(triggerKey.ToUpper)
        End If
        RegisterHotKey(sourceForm.Handle, 1, modifier, val)
    End Sub

    Public Shared Sub unregisterHotkeys(ByRef sourceForm As Form)
        UnregisterHotKey(sourceForm.Handle, 1)  'Remember to call unregisterHotkeys() when closing your application.
    End Sub

    Public Shared Sub handleHotKeyEvent(hotkeyID As IntPtr)
        Main.DoShow()
    End Sub

#End Region
End Class