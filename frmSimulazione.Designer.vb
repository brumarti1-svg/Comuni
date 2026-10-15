<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSimulazione
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
        Me.components     = New System.ComponentModel.Container()
        Me.tmrSimulation  = New System.Windows.Forms.Timer(Me.components)
        Me.mskDataIni     = New System.Windows.Forms.MaskedTextBox()
        Me.mskDataFin     = New System.Windows.Forms.MaskedTextBox()
        Me.LblDateSimul   = New System.Windows.Forms.Label()
        Me.CmdOK          = New System.Windows.Forms.Button()
        Me.CmdLoad        = New System.Windows.Forms.Button()
        Me.CmdAnnulla     = New System.Windows.Forms.Button()
        Me.LblIni         = New System.Windows.Forms.Label()
        Me.LblFin         = New System.Windows.Forms.Label()
        Me.LblDat         = New System.Windows.Forms.Label()
        Me.SuspendLayout()

        ' tmrSimulation
        Me.tmrSimulation.Interval = 1000

        ' mskDataIni
        Me.mskDataIni.Location = New System.Drawing.Point(140, 14)
        Me.mskDataIni.Mask = "00/00/0000 00:00:00"
        Me.mskDataIni.Name = "mskDataIni"
        Me.mskDataIni.Size = New System.Drawing.Size(140, 21)
        Me.mskDataIni.TabIndex = 0

        ' mskDataFin
        Me.mskDataFin.Location = New System.Drawing.Point(140, 44)
        Me.mskDataFin.Mask = "00/00/0000 00:00:00"
        Me.mskDataFin.Name = "mskDataFin"
        Me.mskDataFin.Size = New System.Drawing.Size(140, 21)
        Me.mskDataFin.TabIndex = 1

        ' LblDateSimul
        Me.LblDateSimul.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDateSimul.Location = New System.Drawing.Point(140, 74)
        Me.LblDateSimul.Name = "LblDateSimul"
        Me.LblDateSimul.Size = New System.Drawing.Size(140, 21)
        Me.LblDateSimul.Text = ""

        ' CmdLoad
        Me.CmdLoad.Location = New System.Drawing.Point(290, 14)
        Me.CmdLoad.Name = "CmdLoad"
        Me.CmdLoad.Size = New System.Drawing.Size(75, 23)
        Me.CmdLoad.TabIndex = 2
        Me.CmdLoad.Text = "Load"

        ' CmdOK
        Me.CmdOK.Location = New System.Drawing.Point(290, 44)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.Size = New System.Drawing.Size(75, 23)
        Me.CmdOK.TabIndex = 3
        Me.CmdOK.Text = "OK"
        Me.CmdOK.BackColor = System.Drawing.Color.LightGreen

        ' CmdAnnulla
        Me.CmdAnnulla.Location = New System.Drawing.Point(290, 74)
        Me.CmdAnnulla.Name = "CmdAnnulla"
        Me.CmdAnnulla.Size = New System.Drawing.Size(75, 23)
        Me.CmdAnnulla.TabIndex = 4
        Me.CmdAnnulla.Text = "Cancel"
        Me.CmdAnnulla.BackColor = System.Drawing.Color.Salmon

        ' LblIni
        Me.LblIni.Text     = "From:"
        Me.LblIni.Location = New System.Drawing.Point(14, 17)
        Me.LblIni.AutoSize = True
        '
        ' LblFin
        Me.LblFin.Text     = "To:"
        Me.LblFin.Location = New System.Drawing.Point(14, 47)
        Me.LblFin.AutoSize = True
        '
        ' LblDat
        Me.LblDat.Text     = "Simulation date:"
        Me.LblDat.Location = New System.Drawing.Point(14, 77)
        Me.LblDat.AutoSize = True
        '
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(380, 115)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.LblIni, Me.mskDataIni, Me.CmdLoad,
            Me.LblFin, Me.mskDataFin, Me.CmdOK,
            Me.LblDat, Me.LblDateSimul, Me.CmdAnnulla})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSimulazione"
        Me.Text = "Program Track Simulation"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents CmdAnnulla    As System.Windows.Forms.Button
    Friend WithEvents CmdLoad       As System.Windows.Forms.Button
    Friend WithEvents CmdOK         As System.Windows.Forms.Button
    Friend WithEvents mskDataIni    As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskDataFin    As System.Windows.Forms.MaskedTextBox
    Friend WithEvents LblDateSimul  As System.Windows.Forms.Label
    Friend WithEvents tmrSimulation As System.Windows.Forms.Timer
    Friend LblIni As System.Windows.Forms.Label
    Friend LblFin As System.Windows.Forms.Label
    Friend LblDat As System.Windows.Forms.Label

End Class
