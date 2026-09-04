<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProgram
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grpTime = New System.Windows.Forms.GroupBox()
        Me.LblDataInizio = New System.Windows.Forms.Label()
        Me.MskDataIni = New System.Windows.Forms.TextBox()
        Me.LblOre = New System.Windows.Forms.Label()
        Me.TxtOre = New System.Windows.Forms.TextBox()
        Me.TxtMinuti = New System.Windows.Forms.TextBox()
        Me.CmdOK = New System.Windows.Forms.Button()
        Me.CmdAnnulla = New System.Windows.Forms.Button()
        Me.LblOra = New System.Windows.Forms.Label()
        Me.CmdLoad = New System.Windows.Forms.Button()
        Me.grpTime.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpTime
        '
        Me.grpTime.Controls.Add(Me.LblDataInizio)
        Me.grpTime.Controls.Add(Me.LblOra)
        Me.grpTime.Controls.Add(Me.MskDataIni)
        Me.grpTime.Controls.Add(Me.LblOre)
        Me.grpTime.Controls.Add(Me.TxtOre)
        Me.grpTime.Controls.Add(Me.TxtMinuti)
        Me.grpTime.Location = New System.Drawing.Point(12, 33)
        Me.grpTime.Name = "grpTime"
        Me.grpTime.Size = New System.Drawing.Size(452, 144)
        Me.grpTime.TabIndex = 3
        Me.grpTime.TabStop = False
        '
        'LblDataInizio
        '
        Me.LblDataInizio.Location = New System.Drawing.Point(11, 27)
        Me.LblDataInizio.Name = "LblDataInizio"
        Me.LblDataInizio.Size = New System.Drawing.Size(110, 20)
        Me.LblDataInizio.TabIndex = 0
        Me.LblDataInizio.Text = "Data/Ora inizio (UTC):"
        '
        'MskDataIni
        '
        Me.MskDataIni.Location = New System.Drawing.Point(194, 24)
        Me.MskDataIni.Name = "MskDataIni"
        Me.MskDataIni.Size = New System.Drawing.Size(222, 22)
        Me.MskDataIni.TabIndex = 0
        '
        'LblOre
        '
        Me.LblOre.Location = New System.Drawing.Point(8, 101)
        Me.LblOre.Name = "LblOre"
        Me.LblOre.Size = New System.Drawing.Size(192, 20)
        Me.LblOre.TabIndex = 2
        Me.LblOre.Text = "Period length (999 no stop)"
        '
        'TxtOre
        '
        Me.TxtOre.Location = New System.Drawing.Point(225, 98)
        Me.TxtOre.Name = "TxtOre"
        Me.TxtOre.Size = New System.Drawing.Size(60, 22)
        Me.TxtOre.TabIndex = 2
        Me.TxtOre.Text = "999"
        Me.TxtOre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtMinuti
        '
        Me.TxtMinuti.Location = New System.Drawing.Point(354, 98)
        Me.TxtMinuti.Name = "TxtMinuti"
        Me.TxtMinuti.Size = New System.Drawing.Size(50, 22)
        Me.TxtMinuti.TabIndex = 3
        Me.TxtMinuti.Text = "00"
        Me.TxtMinuti.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmdOK
        '
        Me.CmdOK.BackColor = System.Drawing.Color.LightGreen
        Me.CmdOK.Location = New System.Drawing.Point(187, 224)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.Size = New System.Drawing.Size(110, 32)
        Me.CmdOK.TabIndex = 4
        Me.CmdOK.Text = "Execute"
        Me.CmdOK.UseVisualStyleBackColor = False
        '
        'CmdAnnulla
        '
        Me.CmdAnnulla.BackColor = System.Drawing.Color.Salmon
        Me.CmdAnnulla.Location = New System.Drawing.Point(354, 224)
        Me.CmdAnnulla.Name = "CmdAnnulla"
        Me.CmdAnnulla.Size = New System.Drawing.Size(110, 32)
        Me.CmdAnnulla.TabIndex = 5
        Me.CmdAnnulla.Text = "Abort"
        Me.CmdAnnulla.UseVisualStyleBackColor = False
        '
        'LblOra
        '
        Me.LblOra.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LblOra.Location = New System.Drawing.Point(232, 60)
        Me.LblOra.Name = "LblOra"
        Me.LblOra.Size = New System.Drawing.Size(204, 22)
        Me.LblOra.TabIndex = 0
        Me.LblOra.Text = "Hour              Minutes"
        '
        'CmdLoad
        '
        Me.CmdLoad.Location = New System.Drawing.Point(23, 227)
        Me.CmdLoad.Name = "CmdLoad"
        Me.CmdLoad.Size = New System.Drawing.Size(110, 26)
        Me.CmdLoad.TabIndex = 6
        Me.CmdLoad.Text = "Load"
        '
        'FrmProgram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(476, 268)
        Me.Controls.Add(Me.CmdLoad)
        Me.Controls.Add(Me.grpTime)
        Me.Controls.Add(Me.CmdOK)
        Me.Controls.Add(Me.CmdAnnulla)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProgram"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Program Track "
        Me.grpTime.ResumeLayout(False)
        Me.grpTime.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpTime As System.Windows.Forms.GroupBox
    Friend WithEvents LblOra As System.Windows.Forms.Label
    Friend WithEvents LblDataInizio As System.Windows.Forms.Label
    Friend WithEvents MskDataIni As System.Windows.Forms.TextBox
    Friend WithEvents LblOre As System.Windows.Forms.Label
    Friend WithEvents TxtOre As System.Windows.Forms.TextBox
    Friend WithEvents TxtMinuti As System.Windows.Forms.TextBox
    Friend WithEvents CmdOK As System.Windows.Forms.Button
    Friend WithEvents CmdAnnulla As System.Windows.Forms.Button
    Friend WithEvents CmdLoad As Button
End Class
