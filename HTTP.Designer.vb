<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HTTP
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.BtSend = New System.Windows.Forms.Button()
        Me.BtGet = New System.Windows.Forms.Button()
        Me.BtClear = New System.Windows.Forms.Button()
        Me.BtExit = New System.Windows.Forms.Button()
        Me.TxtURL = New System.Windows.Forms.TextBox()
        Me.TxtHTML = New System.Windows.Forms.TextBox()
        Me.TxtResponseHeaders = New System.Windows.Forms.TextBox()
        Me.TxtRequestHeaders = New System.Windows.Forms.TextBox()
        Me.LblContentType = New System.Windows.Forms.Label()
        Me.LblContentLength = New System.Windows.Forms.Label()
        Me.LblLastModified = New System.Windows.Forms.Label()
        Me.LblVersion = New System.Windows.Forms.Label()
        Me.LblStatusCode = New System.Windows.Forms.Label()
        Me.LblStatusText = New System.Windows.Forms.Label()
        Me.LblRawHeaders = New System.Windows.Forms.Label()
        Me.LblForwarded = New System.Windows.Forms.Label()
        Me.LblServer = New System.Windows.Forms.Label()
        Me.LblRequestMethod = New System.Windows.Forms.Label()
        Me.LblRequestMethod2 = New System.Windows.Forms.Label()
        Me.LblPragma = New System.Windows.Forms.Label()
        Me.LblUserAgent = New System.Windows.Forms.Label()
        Me.LblMajor = New System.Windows.Forms.Label()
        Me.LblMinor = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabMain = New System.Windows.Forms.TabControl()
        Me.sbrMain = New System.Windows.Forms.StatusBar()
        Me.SuspendLayout()
        '
        ' sbrMain
        '
        Me.sbrMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.sbrMain.Location = New System.Drawing.Point(0, 420)
        Me.sbrMain.Name = "sbrMain"
        Dim sbPanel As New System.Windows.Forms.StatusBarPanel()
        sbPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        sbPanel.Text = "Ready"
        Me.sbrMain.Panels.Add(sbPanel)
        Me.sbrMain.ShowPanels = True
        Me.sbrMain.Size = New System.Drawing.Size(800, 22)
        '
        ' BtSend
        '
        Me.BtSend.Location = New System.Drawing.Point(12, 12)
        Me.BtSend.Name = "BtSend"
        Me.BtSend.Size = New System.Drawing.Size(75, 23)
        Me.BtSend.Text = "Send"
        '
        ' BtGet
        '
        Me.BtGet.Enabled = False
        Me.BtGet.Location = New System.Drawing.Point(93, 12)
        Me.BtGet.Name = "BtGet"
        Me.BtGet.Size = New System.Drawing.Size(75, 23)
        Me.BtGet.Text = "Get"
        '
        ' BtClear
        '
        Me.BtClear.Location = New System.Drawing.Point(174, 12)
        Me.BtClear.Name = "BtClear"
        Me.BtClear.Size = New System.Drawing.Size(75, 23)
        Me.BtClear.Text = "Clear"
        '
        ' BtExit
        '
        Me.BtExit.Location = New System.Drawing.Point(255, 12)
        Me.BtExit.Name = "BtExit"
        Me.BtExit.Size = New System.Drawing.Size(75, 23)
        Me.BtExit.Text = "Exit"
        '
        ' TxtURL
        '
        Me.TxtURL.Location = New System.Drawing.Point(12, 41)
        Me.TxtURL.Name = "TxtURL"
        Me.TxtURL.Size = New System.Drawing.Size(776, 20)
        '
        ' ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 67)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(776, 16)
        '
        ' TabMain
        '
        Me.TabMain.Location = New System.Drawing.Point(12, 89)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.Size = New System.Drawing.Size(776, 320)
        '
        ' Response header labels (posizionati nella tab o nel form)
        '
        Me.LblContentType.Location = New System.Drawing.Point(336, 12)
        Me.LblContentType.Name = "LblContentType"
        Me.LblContentType.Size = New System.Drawing.Size(200, 15)
        Me.LblContentLength.Location = New System.Drawing.Point(336, 30)
        Me.LblContentLength.Name = "LblContentLength"
        Me.LblContentLength.Size = New System.Drawing.Size(200, 15)
        Me.LblLastModified.Location = New System.Drawing.Point(336, 48)
        Me.LblLastModified.Name = "LblLastModified"
        Me.LblLastModified.Size = New System.Drawing.Size(200, 15)
        Me.LblVersion.Location = New System.Drawing.Point(550, 12)
        Me.LblVersion.Name = "LblVersion"
        Me.LblVersion.Size = New System.Drawing.Size(100, 15)
        Me.LblStatusCode.Location = New System.Drawing.Point(550, 30)
        Me.LblStatusCode.Name = "LblStatusCode"
        Me.LblStatusCode.Size = New System.Drawing.Size(100, 15)
        Me.LblStatusText.Location = New System.Drawing.Point(550, 48)
        Me.LblStatusText.Name = "LblStatusText"
        Me.LblStatusText.Size = New System.Drawing.Size(200, 15)
        Me.LblRawHeaders.Location = New System.Drawing.Point(336, 66)
        Me.LblRawHeaders.Name = "LblRawHeaders"
        Me.LblRawHeaders.Size = New System.Drawing.Size(200, 15)
        Me.LblForwarded.Location = New System.Drawing.Point(336, 84)
        Me.LblForwarded.Name = "LblForwarded"
        Me.LblForwarded.Size = New System.Drawing.Size(200, 15)
        Me.LblServer.Location = New System.Drawing.Point(550, 66)
        Me.LblServer.Name = "LblServer"
        Me.LblServer.Size = New System.Drawing.Size(200, 15)
        Me.LblRequestMethod.Location = New System.Drawing.Point(550, 84)
        Me.LblRequestMethod.Name = "LblRequestMethod"
        Me.LblRequestMethod.Size = New System.Drawing.Size(200, 15)
        Me.LblRequestMethod2.Location = New System.Drawing.Point(550, 102)
        Me.LblRequestMethod2.Name = "LblRequestMethod2"
        Me.LblRequestMethod2.Size = New System.Drawing.Size(200, 15)
        Me.LblPragma.Location = New System.Drawing.Point(336, 102)
        Me.LblPragma.Name = "LblPragma"
        Me.LblPragma.Size = New System.Drawing.Size(200, 15)
        Me.LblUserAgent.Location = New System.Drawing.Point(336, 120)
        Me.LblUserAgent.Name = "LblUserAgent"
        Me.LblUserAgent.Size = New System.Drawing.Size(200, 15)
        Me.LblMajor.Location = New System.Drawing.Point(550, 120)
        Me.LblMajor.Name = "LblMajor"
        Me.LblMajor.Size = New System.Drawing.Size(50, 15)
        Me.LblMinor.Location = New System.Drawing.Point(610, 120)
        Me.LblMinor.Name = "LblMinor"
        Me.LblMinor.Size = New System.Drawing.Size(50, 15)
        '
        ' TxtHTML (ReadOnly, multiline)
        '
        Me.TxtHTML.Location = New System.Drawing.Point(12, 89)
        Me.TxtHTML.Multiline = True
        Me.TxtHTML.Name = "TxtHTML"
        Me.TxtHTML.ReadOnly = True
        Me.TxtHTML.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtHTML.Size = New System.Drawing.Size(776, 150)
        Me.TxtHTML.Visible = True
        '
        ' TxtResponseHeaders
        '
        Me.TxtResponseHeaders.Location = New System.Drawing.Point(12, 245)
        Me.TxtResponseHeaders.Multiline = True
        Me.TxtResponseHeaders.Name = "TxtResponseHeaders"
        Me.TxtResponseHeaders.ReadOnly = True
        Me.TxtResponseHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtResponseHeaders.Size = New System.Drawing.Size(370, 160)
        Me.TxtResponseHeaders.Visible = True
        '
        ' TxtRequestHeaders
        '
        Me.TxtRequestHeaders.Location = New System.Drawing.Point(400, 245)
        Me.TxtRequestHeaders.Multiline = True
        Me.TxtRequestHeaders.Name = "TxtRequestHeaders"
        Me.TxtRequestHeaders.ReadOnly = True
        Me.TxtRequestHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtRequestHeaders.Size = New System.Drawing.Size(370, 160)
        Me.TxtRequestHeaders.Visible = True
        '
        ' HTTP
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(800, 442)
        Me.Controls.Add(Me.sbrMain)
        Me.Controls.Add(Me.BtSend)
        Me.Controls.Add(Me.BtGet)
        Me.Controls.Add(Me.BtClear)
        Me.Controls.Add(Me.BtExit)
        Me.Controls.Add(Me.TxtURL)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.TabMain)
        Me.Controls.Add(Me.LblContentType)
        Me.Controls.Add(Me.LblContentLength)
        Me.Controls.Add(Me.LblLastModified)
        Me.Controls.Add(Me.LblVersion)
        Me.Controls.Add(Me.LblStatusCode)
        Me.Controls.Add(Me.LblStatusText)
        Me.Controls.Add(Me.LblRawHeaders)
        Me.Controls.Add(Me.LblForwarded)
        Me.Controls.Add(Me.LblServer)
        Me.Controls.Add(Me.LblRequestMethod)
        Me.Controls.Add(Me.LblRequestMethod2)
        Me.Controls.Add(Me.LblPragma)
        Me.Controls.Add(Me.LblUserAgent)
        Me.Controls.Add(Me.LblMajor)
        Me.Controls.Add(Me.LblMinor)
        Me.Controls.Add(Me.TxtHTML)
        Me.Controls.Add(Me.TxtResponseHeaders)
        Me.Controls.Add(Me.TxtRequestHeaders)
        Me.Text = "HTTP Request Viewer"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents BtSend As System.Windows.Forms.Button
    Friend WithEvents BtGet As System.Windows.Forms.Button
    Friend WithEvents BtClear As System.Windows.Forms.Button
    Friend WithEvents BtExit As System.Windows.Forms.Button
    Friend WithEvents TxtURL As System.Windows.Forms.TextBox
    Friend WithEvents TxtHTML As System.Windows.Forms.TextBox
    Friend WithEvents TxtResponseHeaders As System.Windows.Forms.TextBox
    Friend WithEvents TxtRequestHeaders As System.Windows.Forms.TextBox
    Friend WithEvents LblContentType As System.Windows.Forms.Label
    Friend WithEvents LblContentLength As System.Windows.Forms.Label
    Friend WithEvents LblLastModified As System.Windows.Forms.Label
    Friend WithEvents LblVersion As System.Windows.Forms.Label
    Friend WithEvents LblStatusCode As System.Windows.Forms.Label
    Friend WithEvents LblStatusText As System.Windows.Forms.Label
    Friend WithEvents LblRawHeaders As System.Windows.Forms.Label
    Friend WithEvents LblForwarded As System.Windows.Forms.Label
    Friend WithEvents LblServer As System.Windows.Forms.Label
    Friend WithEvents LblRequestMethod As System.Windows.Forms.Label
    Friend WithEvents LblRequestMethod2 As System.Windows.Forms.Label
    Friend WithEvents LblPragma As System.Windows.Forms.Label
    Friend WithEvents LblUserAgent As System.Windows.Forms.Label
    Friend WithEvents LblMajor As System.Windows.Forms.Label
    Friend WithEvents LblMinor As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents TabMain As System.Windows.Forms.TabControl
    Friend WithEvents sbrMain As System.Windows.Forms.StatusBar

End Class
