' Generato da frmBackPT.frm (VB6) — coordinate convertite: twips / 15 = pixel
' Form 318x157 px, nessun menu VB6 → aggiunto AcceptButton
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBackPT
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
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.components  = New System.ComponentModel.Container()
        Me.CmbBackUpPT = New System.Windows.Forms.ComboBox()
        Me.CmdOK       = New System.Windows.Forms.Button()
        Me.LblTitle    = New System.Windows.Forms.Label()
        Me.LblLegenda  = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        ' LblTitle  (Label1 in VB6: Left=108/15=7, Top=468/15=31, Width=3036/15=202, Height=336/15=22)
        '
        Me.LblTitle.Location = New System.Drawing.Point(7, 31)
        Me.LblTitle.Size     = New System.Drawing.Size(220, 22)
        Me.LblTitle.Text     = "Program Track Back-Up Mode"
        Me.LblTitle.Font     = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Regular)
        '
        ' CmbBackUpPT (Left=3456/15=230, Top=468/15=31, Width=840/15=56, Height=324/15=22)
        ' Allargato per usabilità moderna
        '
        Me.CmbBackUpPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.CmbBackUpPT.Location      = New System.Drawing.Point(230, 29)
        Me.CmbBackUpPT.Size          = New System.Drawing.Size(80, 22)
        Me.CmbBackUpPT.Font          = New System.Drawing.Font("Arial", 10.2!)
        Me.CmbBackUpPT.TabIndex      = 1
        Me.CmbBackUpPT.Name          = "CmbBackUpPT"
        '
        ' LblLegenda (Label2: Left=108/15=7, Top=972/15=65, Width=1812/15=121, Height=768/15=51)
        '
        Me.LblLegenda.Location = New System.Drawing.Point(7, 65)
        Me.LblLegenda.Size     = New System.Drawing.Size(210, 51)
        Me.LblLegenda.Text     = "P - Program Track" & vbCrLf & "S - Stand-By" & vbCrLf & "T - Step Track"
        Me.LblLegenda.Font     = New System.Drawing.Font("Arial", 10.2!)
        '
        ' CmdOK (Left=3744/15=250, Top=1764/15=118, Width=660/15=44, Height=444/15=30)
        '
        Me.CmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.CmdOK.Location     = New System.Drawing.Point(250, 118)
        Me.CmdOK.Size         = New System.Drawing.Size(60, 30)
        Me.CmdOK.Text         = "O.K."
        Me.CmdOK.Font         = New System.Drawing.Font("Arial", 10.2!)
        Me.CmdOK.TabIndex     = 0
        Me.CmdOK.Name         = "CmdOK"
        '
        ' frmBackPT (ClientWidth=4764/15=318, ClientHeight=2352/15=157)
        '
        Me.AcceptButton         = Me.CmdOK
        Me.ClientSize           = New System.Drawing.Size(318, 157)
        Me.FormBorderStyle      = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox          = False
        Me.MinimizeBox          = False
        Me.StartPosition        = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Controls.Add(Me.LblTitle)
        Me.Controls.Add(Me.CmbBackUpPT)
        Me.Controls.Add(Me.LblLegenda)
        Me.Controls.Add(Me.CmdOK)
        Me.Name  = "frmBackPT"
        Me.Text  = "Program Track Back-up"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents CmbBackUpPT As System.Windows.Forms.ComboBox
    Friend WithEvents CmdOK       As System.Windows.Forms.Button
    Friend LblTitle   As System.Windows.Forms.Label
    Friend LblLegenda As System.Windows.Forms.Label

End Class
