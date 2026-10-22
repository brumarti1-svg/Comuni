<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPreset
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MskAzimuth = New System.Windows.Forms.TextBox()
        Me.MskElevazione = New System.Windows.Forms.TextBox()
        Me.TxtFreccia = New System.Windows.Forms.TextBox()
        Me.CmbInc = New System.Windows.Forms.ComboBox()
        Me.CmdUp = New System.Windows.Forms.Button()
        Me.CmdDown = New System.Windows.Forms.Button()
        Me.CmdCW = New System.Windows.Forms.Button()
        Me.CmdCCW = New System.Windows.Forms.Button()
        Me.CmdOK = New System.Windows.Forms.Button()
        Me.CmdAnnulla = New System.Windows.Forms.Button()
        Me.LblAzimuth = New System.Windows.Forms.Label()
        Me.LblElevazione = New System.Windows.Forms.Label()
        Me.LblInc = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'MskAzimuth
        '
        Me.MskAzimuth.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MskAzimuth.Location = New System.Drawing.Point(124, 14)
        Me.MskAzimuth.Name = "MskAzimuth"
        Me.MskAzimuth.Size = New System.Drawing.Size(120, 28)
        Me.MskAzimuth.TabIndex = 1
        Me.MskAzimuth.Text = "000.000"
        Me.MskAzimuth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MskElevazione
        '
        Me.MskElevazione.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MskElevazione.Location = New System.Drawing.Point(124, 48)
        Me.MskElevazione.Name = "MskElevazione"
        Me.MskElevazione.Size = New System.Drawing.Size(120, 28)
        Me.MskElevazione.TabIndex = 3
        Me.MskElevazione.Text = "00.000"
        Me.MskElevazione.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtFreccia
        '
        Me.TxtFreccia.BackColor = System.Drawing.SystemColors.Control
        Me.TxtFreccia.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFreccia.Location = New System.Drawing.Point(120, 186)
        Me.TxtFreccia.Name = "TxtFreccia"
        Me.TxtFreccia.ReadOnly = True
        Me.TxtFreccia.Size = New System.Drawing.Size(60, 20)
        Me.TxtFreccia.TabIndex = 8
        '
        'CmbInc
        '
        Me.CmbInc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.CmbInc.Items.AddRange(New Object() {"0.001", "0.010", "0.100", "0.500", "1.000"})
        Me.CmbInc.Location = New System.Drawing.Point(124, 82)
        Me.CmbInc.Name = "CmbInc"
        Me.CmbInc.Size = New System.Drawing.Size(120, 30)
        Me.CmbInc.TabIndex = 5
        Me.CmbInc.Text = "0.010"
        '
        'CmdUp
        '
        Me.CmdUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.CmdUp.Location = New System.Drawing.Point(120, 130)
        Me.CmdUp.Name = "CmdUp"
        Me.CmdUp.Size = New System.Drawing.Size(60, 40)
        Me.CmdUp.TabIndex = 6
        Me.CmdUp.Text = "▲"
        '
        'CmdDown
        '
        Me.CmdDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.CmdDown.Location = New System.Drawing.Point(120, 230)
        Me.CmdDown.Name = "CmdDown"
        Me.CmdDown.Size = New System.Drawing.Size(60, 40)
        Me.CmdDown.TabIndex = 10
        Me.CmdDown.Text = "▼"
        '
        'CmdCW
        '
        Me.CmdCW.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.CmdCW.Location = New System.Drawing.Point(190, 180)
        Me.CmdCW.Name = "CmdCW"
        Me.CmdCW.Size = New System.Drawing.Size(60, 40)
        Me.CmdCW.TabIndex = 9
        Me.CmdCW.Text = "►"
        '
        'CmdCCW
        '
        Me.CmdCCW.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.CmdCCW.Location = New System.Drawing.Point(50, 180)
        Me.CmdCCW.Name = "CmdCCW"
        Me.CmdCCW.Size = New System.Drawing.Size(60, 40)
        Me.CmdCCW.TabIndex = 7
        Me.CmdCCW.Text = "◄"
        '
        'CmdOK
        '
        Me.CmdOK.BackColor = System.Drawing.Color.LightGreen
        Me.CmdOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CmdOK.Location = New System.Drawing.Point(16, 290)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.Size = New System.Drawing.Size(130, 42)
        Me.CmdOK.TabIndex = 11
        Me.CmdOK.Text = "Execute"
        Me.CmdOK.UseVisualStyleBackColor = False
        '
        'CmdAnnulla
        '
        Me.CmdAnnulla.BackColor = System.Drawing.Color.Salmon
        Me.CmdAnnulla.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CmdAnnulla.Location = New System.Drawing.Point(154, 290)
        Me.CmdAnnulla.Name = "CmdAnnulla"
        Me.CmdAnnulla.Size = New System.Drawing.Size(130, 42)
        Me.CmdAnnulla.TabIndex = 12
        Me.CmdAnnulla.Text = "Exit"
        Me.CmdAnnulla.UseVisualStyleBackColor = False
        '
        'LblAzimuth
        '
        Me.LblAzimuth.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblAzimuth.Location = New System.Drawing.Point(16, 18)
        Me.LblAzimuth.Name = "LblAzimuth"
        Me.LblAzimuth.Size = New System.Drawing.Size(100, 24)
        Me.LblAzimuth.TabIndex = 0
        Me.LblAzimuth.Text = "Azimuth:"
        '
        'LblElevazione
        '
        Me.LblElevazione.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblElevazione.Location = New System.Drawing.Point(16, 52)
        Me.LblElevazione.Name = "LblElevazione"
        Me.LblElevazione.Size = New System.Drawing.Size(100, 24)
        Me.LblElevazione.TabIndex = 2
        Me.LblElevazione.Text = "Elevation:"
        '
        'LblInc
        '
        Me.LblInc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblInc.Location = New System.Drawing.Point(16, 86)
        Me.LblInc.Name = "LblInc"
        Me.LblInc.Size = New System.Drawing.Size(100, 24)
        Me.LblInc.TabIndex = 4
        Me.LblInc.Text = "Increment:"
        '
        'FrmPreset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(300, 370)
        Me.Controls.Add(Me.LblAzimuth)
        Me.Controls.Add(Me.MskAzimuth)
        Me.Controls.Add(Me.LblElevazione)
        Me.Controls.Add(Me.MskElevazione)
        Me.Controls.Add(Me.LblInc)
        Me.Controls.Add(Me.CmbInc)
        Me.Controls.Add(Me.CmdUp)
        Me.Controls.Add(Me.CmdCCW)
        Me.Controls.Add(Me.TxtFreccia)
        Me.Controls.Add(Me.CmdCW)
        Me.Controls.Add(Me.CmdDown)
        Me.Controls.Add(Me.CmdOK)
        Me.Controls.Add(Me.CmdAnnulla)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmPreset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Preset Position"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MskAzimuth    As System.Windows.Forms.TextBox
    Friend WithEvents MskElevazione As System.Windows.Forms.TextBox
    Friend WithEvents TxtFreccia    As System.Windows.Forms.TextBox
    Friend WithEvents CmbInc        As System.Windows.Forms.ComboBox
    Friend WithEvents CmdUp         As System.Windows.Forms.Button
    Friend WithEvents CmdDown       As System.Windows.Forms.Button
    Friend WithEvents CmdCW         As System.Windows.Forms.Button
    Friend WithEvents CmdCCW        As System.Windows.Forms.Button
    Friend WithEvents CmdOK         As System.Windows.Forms.Button
    Friend WithEvents CmdAnnulla    As System.Windows.Forms.Button
    Friend WithEvents LblAzimuth     As System.Windows.Forms.Label
    Friend WithEvents LblElevazione  As System.Windows.Forms.Label
    Friend WithEvents LblInc         As System.Windows.Forms.Label
End Class

