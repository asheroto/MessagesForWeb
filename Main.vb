Imports Microsoft.Web.WebView2.WinForms
Imports Microsoft.Web.WebView2.Core
Imports System.ComponentModel

Public Class Main
    Dim AllowClose As Boolean = False
    Private Sub WV_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WV.NavigationCompleted
        Text = WV.CoreWebView2.DocumentTitle
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Register hotkey
        Hotkey.registerHotkey(Me, "M", Hotkey.KeyModifier.Control + Hotkey.KeyModifier.Alt)

        WindowState = FormWindowState.Maximized
    End Sub
    Private Sub wVBrowser_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WV.CoreWebView2InitializationCompleted
        AddHandler WV.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
    End Sub
    Private Sub CoreWebView2_NewWindowRequested(ByVal sender As Object, ByVal e As Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs)
        e.Handled = True

        Try
            Dim p As New Process
            p.StartInfo.UseShellExecute = True
            p.StartInfo.FileName = e.Uri
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            p.Start()
        Catch ex As Exception

        End Try
    End Sub

    Sub WaitNice(ms As Long)
        Dim x As New Stopwatch
        x.Start()
        Do While x.ElapsedMilliseconds <= ms
            Application.DoEvents()
        Loop
        x.Stop()
    End Sub

    Private Sub SystemTrayIcon_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles SystemTrayIcon.MouseDoubleClick
        Show()
        WaitNice(100)
        Activate()
        WaitNice(100)
        BringToFront()
        WaitNice(100)
        Activate()
    End Sub

    Private Sub Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If AllowClose = False Then
            e.Cancel = True
            Hide()
        End If
    End Sub
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = Hotkey.WM_HOTKEY Then
            Hotkey.handleHotKeyEvent(m.WParam)
        End If
        MyBase.WndProc(m)
    End Sub 'System wide hotkey event handling

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        AllowClose = True
        Hotkey.unregisterHotkeys(Me)
        Close()
    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click
        Show()
        Activate()
    End Sub

    Private Sub Startup_Tick(sender As Object, e As EventArgs) Handles Startup.Tick
        Startup.Enabled = False
        Dim s() As String = Environment.GetCommandLineArgs()
        For i As Integer = 1 To s.Length - 1
            Select Case LCase(s(i))
                Case "-startup"
                    Close()
            End Select
        Next
        Opacity = 100
    End Sub
End Class

Public Class Hotkey

#Region "Declarations - WinAPI, Hotkey constant and Modifier Enum"
    ''' <summary>
    ''' Declaration of winAPI function wrappers. The winAPI functions are used to register / unregister a hotkey
    ''' </summary>
    Private Declare Function RegisterHotKey Lib "user32" _
        (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer

    Private Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Public Const WM_HOTKEY As Integer = &H312

    Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
    End Enum 'This enum is just to make it easier to call the registerHotKey function: The modifier integer codes are replaced by a friendly "Alt","Shift" etc.
#End Region


#Region "Hotkey registration, unregistration and handling"
    Public Shared Sub registerHotkey(ByRef sourceForm As Form, ByVal triggerKey As String, ByVal modifier As KeyModifier)
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
    Public Shared Sub handleHotKeyEvent(ByVal hotkeyID As IntPtr)
        Main.Show()
        Main.Activate()
    End Sub
#End Region

End Class