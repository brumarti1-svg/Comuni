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
        Me.MskAzimuth.Location = New System.Drawing.Point(82, 10)
        Me.MskAzimuth.Name = "MskAzimuth"
        Me.MskAzimuth.Size = New System.Drawing.Size(90, 22)
        Me.MskAzimuth.TabIndex = 1
        Me.MskAzimuth.Text = "000.000"
        '
        'MskElevazione
        '
        Me.MskElevazione.Location = New System.Drawing.Point(82, 36)
        Me.MskElevazione.Name = "MskElevazione"
        Me.MskElevazione.Size = New System.Drawing.Size(90, 22)
        Me.MskElevazione.TabIndex = 3
        Me.MskElevazione.Text = "00.000"
        '
        'TxtFreccia
        '
        Me.TxtFreccia.BackColor = System.Drawing.SystemColors.Control
        Me.TxtFreccia.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFreccia.Location = New System.Drawing.Point(82, 136)
        Me.TxtFreccia.Name = "TxtFreccia"
        Me.TxtFreccia.ReadOnly = True
        Me.TxtFreccia.Size = New System.Drawing.Size(46, 15)
        Me.TxtFreccia.TabIndex = 8
        '
        'CmbInc
        '
        Me.CmbInc.Items.AddRange(New Object() {"0.001", "0.010", "0.100", "0.500", "1.000"})
        Me.CmbInc.Location = New System.Drawing.Point(82, 64)
        Me.CmbInc.Name = "CmbInc"
        Me.CmbInc.Size = New System.Drawing.Size(90, 24)
        Me.CmbInc.TabIndex = 5
        Me.CmbInc.Text = "0.010"
        '
        'CmdUp
        '
        Me.CmdUp.Location = New System.Drawing.Point(82, 100)
        Me.CmdUp.Name = "CmdUp"
        Me.CmdUp.Size = New System.Drawing.Size(46, 30)
        Me.CmdUp.TabIndex = 6
        Me.CmdUp.Text = "▲"
        '
        'CmdDown
        '
        Me.CmdDown.Location = New System.Drawing.Point(82, 172)
        Me.CmdDown.Name = "CmdDown"
        Me.CmdDown.Size = New System.Drawing.Size(46, 30)
        Me.CmdDown.TabIndex = 10
        Me.CmdDown.Text = "▼"
        '
        'CmdCW
        '
        Me.CmdCW.Location = New System.Drawing.Point(130, 136)
        Me.CmdCW.Name = "CmdCW"
        Me.CmdCW.Size = New System.Drawing.Size(46, 30)
        Me.CmdCW.TabIndex = 9
        Me.CmdCW.Text = "►"
        '
        'CmdCCW
        '
        Me.CmdCCW.Location = New System.Drawing.Point(34, 136)
        Me.CmdCCW.Name = "CmdCCW"
        Me.CmdCCW.Size = New System.Drawing.Size(46, 30)
        Me.CmdCCW.TabIndex = 7
        Me.CmdCCW.Text = "◄"
        '
        'CmdOK
        '
        Me.CmdOK.BackColor = System.Drawing.Color.LightGreen
        Me.CmdOK.Location = New System.Drawing.Point(8, 218)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.Size = New System.Drawing.Size(80, 30)
        Me.CmdOK.TabIndex = 11
        Me.CmdOK.Text = "OK"
        Me.CmdOK.UseVisualStyleBackColor = False
        '
        'CmdAnnulla
        '
        Me.CmdAnnulla.BackColor = System.Drawing.Color.Salmon
        Me.CmdAnnulla.Location = New System.Drawing.Point(100, 218)
        Me.CmdAnnulla.Name = "CmdAnnulla"
        Me.CmdAnnulla.Size = New System.Drawing.Size(80, 30)
        Me.CmdAnnulla.TabIndex = 12
        Me.CmdAnnulla.Text = "Cancel"
        Me.CmdAnnulla.UseVisualStyleBackColor = False
        '
        'LblAzimuth
        '
        Me.LblAzimuth.Location = New System.Drawing.Point(8, 12)
        Me.LblAzimuth.Name = "LblAzimuth"
        Me.LblAzimuth.Size = New System.Drawing.Size(70, 20)
        Me.LblAzimuth.TabIndex = 0
        Me.LblAzimuth.Text = "Azimuth:"
        '
        'LblElevazione
        '
        Me.LblElevazione.Location = New System.Drawing.Point(8, 38)
        Me.LblElevazione.Name = "LblElevazione"
        Me.LblElevazione.Size = New System.Drawing.Size(70, 20)
        Me.LblElevazione.TabIndex = 2
        Me.LblElevazione.Text = "Elevation:"
        '
        'LblInc
        '
        Me.LblInc.Location = New System.Drawing.Point(8, 66)
        Me.LblInc.Name = "LblInc"
        Me.LblInc.Size = New System.Drawing.Size(70, 20)
        Me.LblInc.TabIndex = 4
        Me.LblInc.Text = "Increment:"
        '
        'FrmPreset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(192, 260)
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
    Friend           LblAzimuth     As System.Windows.Forms.Label
    Friend           LblElevazione  As System.Windows.Forms.Label
    Friend           LblInc         As System.Windows.Forms.Label
End Class

