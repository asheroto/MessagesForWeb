<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.SystemTrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Startup = New System.Windows.Forms.Timer(Me.components)
        Me.WV = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.ContextMenu.SuspendLayout()
        CType(Me.WV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SystemTrayIcon
        '
        Me.SystemTrayIcon.ContextMenuStrip = Me.ContextMenu
        Me.SystemTrayIcon.Icon = CType(resources.GetObject("SystemTrayIcon.Icon"), System.Drawing.Icon)
        Me.SystemTrayIcon.Text = "Messages for web"
        Me.SystemTrayIcon.Visible = True
        '
        'ContextMenu
        '
        Me.ContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowToolStripMenuItem, Me.RestartToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ContextMenu.Name = "SystemTrayIconContextMenu"
        Me.ContextMenu.Size = New System.Drawing.Size(111, 70)
        '
        'ShowToolStripMenuItem
        '
        Me.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem"
        Me.ShowToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.ShowToolStripMenuItem.Text = "Show"
        '
        'RestartToolStripMenuItem
        '
        Me.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem"
        Me.RestartToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.RestartToolStripMenuItem.Text = "Restart"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Startup
        '
        Me.Startup.Enabled = True
        Me.Startup.Interval = 10
        '
        'WV
        '
        Me.WV.AllowExternalDrop = True
        Me.WV.CreationProperties = Nothing
        Me.WV.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WV.Location = New System.Drawing.Point(0, 0)
        Me.WV.Name = "WV"
        Me.WV.Size = New System.Drawing.Size(1011, 687)
        Me.WV.Source = New System.Uri("https://messages.google.com/web/conversations", System.UriKind.Absolute)
        Me.WV.TabIndex = 1
        Me.WV.ZoomFactor = 1.0R
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1011, 687)
        Me.Controls.Add(Me.WV)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Opacity = 0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Messages for Web"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ContextMenu.ResumeLayout(False)
        CType(Me.WV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SystemTrayIcon As NotifyIcon
    Friend Shadows WithEvents ContextMenu As ContextMenuStrip
    Friend WithEvents ShowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Startup As Timer
    Friend WithEvents RestartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WV As Microsoft.Web.WebView2.WinForms.WebView2
End Class