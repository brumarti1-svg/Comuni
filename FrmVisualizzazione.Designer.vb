<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmVisualizzazione
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.mskDataIni = New System.Windows.Forms.MaskedTextBox()
        Me.mskDataFin = New System.Windows.Forms.MaskedTextBox()
        Me.mskOraIni = New System.Windows.Forms.MaskedTextBox()
        Me.mskOraFin = New System.Windows.Forms.MaskedTextBox()
        Me.picGrafico = New System.Windows.Forms.PictureBox()
        Me.chkPrincipale = New System.Windows.Forms.CheckBox()
        Me.chkSecondaria = New System.Windows.Forms.CheckBox()
        Me.optLinee = New System.Windows.Forms.RadioButton()
        Me.optPunti = New System.Windows.Forms.RadioButton()
        Me.LblMostraDati = New System.Windows.Forms.Label()
        Me.optDatiSi = New System.Windows.Forms.RadioButton()
        Me.optDatiNo = New System.Windows.Forms.RadioButton()
        Me.CmdElabora = New System.Windows.Forms.Button()
        Me.CmdStampa = New System.Windows.Forms.Button()
        Me.CmdEsci = New System.Windows.Forms.Button()
        Me.pnlControls = New System.Windows.Forms.Panel()
        Me.LblDI = New System.Windows.Forms.Label()
        Me.LblOI = New System.Windows.Forms.Label()
        Me.LblDF = New System.Windows.Forms.Label()
        Me.LblOF = New System.Windows.Forms.Label()
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlControls.SuspendLayout()
        Me.SuspendLayout()
        '
        'mskDataIni
        '
        Me.mskDataIni.Location = New System.Drawing.Point(66, 6)
        Me.mskDataIni.Mask = "00/00/0000"
        Me.mskDataIni.Name = "mskDataIni"
        Me.mskDataIni.Size = New System.Drawing.Size(88, 22)
        Me.mskDataIni.TabIndex = 1
        '
        'mskDataFin
        '
        Me.mskDataFin.Location = New System.Drawing.Point(66, 32)
        Me.mskDataFin.Mask = "00/00/0000"
        Me.mskDataFin.Name = "mskDataFin"
        Me.mskDataFin.Size = New System.Drawing.Size(88, 22)
        Me.mskDataFin.TabIndex = 5
        '
        'mskOraIni
        '
        Me.mskOraIni.Location = New System.Drawing.Point(194, 6)
        Me.mskOraIni.Mask = "00.00.00"
        Me.mskOraIni.Name = "mskOraIni"
        Me.mskOraIni.Size = New System.Drawing.Size(60, 22)
        Me.mskOraIni.TabIndex = 3
        '
        'mskOraFin
        '
        Me.mskOraFin.Location = New System.Drawing.Point(194, 32)
        Me.mskOraFin.Mask = "00.00.00"
        Me.mskOraFin.Name = "mskOraFin"
        Me.mskOraFin.Size = New System.Drawing.Size(60, 22)
        Me.mskOraFin.TabIndex = 7
        '
        'picGrafico
        '
        Me.picGrafico.BackColor = System.Drawing.Color.White
        Me.picGrafico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picGrafico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picGrafico.Location = New System.Drawing.Point(0, 118)
        Me.picGrafico.Name = "picGrafico"
        Me.picGrafico.Size = New System.Drawing.Size(1394, 537)
        Me.picGrafico.TabIndex = 0
        Me.picGrafico.TabStop = False
        '
        'chkPrincipale
        '
        Me.chkPrincipale.Location = New System.Drawing.Point(264, 8)
        Me.chkPrincipale.Name = "chkPrincipale"
        Me.chkPrincipale.Size = New System.Drawing.Size(90, 20)
        Me.chkPrincipale.TabIndex = 8
        Me.chkPrincipale.Text = "Principale"
        '
        'chkSecondaria
        '
        Me.chkSecondaria.Location = New System.Drawing.Point(264, 32)
        Me.chkSecondaria.Name = "chkSecondaria"
        Me.chkSecondaria.Size = New System.Drawing.Size(103, 20)
        Me.chkSecondaria.TabIndex = 9
        Me.chkSecondaria.Text = "Secondaria"
        '
        'optLinee
        '
        Me.optLinee.Checked = True
        Me.optLinee.Location = New System.Drawing.Point(460, 6)
        Me.optLinee.Name = "optLinee"
        Me.optLinee.Size = New System.Drawing.Size(60, 20)
        Me.optLinee.TabIndex = 13
        Me.optLinee.TabStop = True
        Me.optLinee.Text = "Linee"
        '
        'optPunti
        '
        Me.optPunti.Location = New System.Drawing.Point(460, 30)
        Me.optPunti.Name = "optPunti"
        Me.optPunti.Size = New System.Drawing.Size(60, 20)
        Me.optPunti.TabIndex = 14
        Me.optPunti.Text = "Punti"
        '
        'LblMostraDati
        '
        Me.LblMostraDati.Location = New System.Drawing.Point(530, 6)
        Me.LblMostraDati.Name = "LblMostraDati"
        Me.LblMostraDati.Size = New System.Drawing.Size(76, 18)
        Me.LblMostraDati.Text = "Mostra dati:"
        '
        'optDatiSi
        '
        Me.optDatiSi.Location = New System.Drawing.Point(530, 26)
        Me.optDatiSi.Name = "optDatiSi"
        Me.optDatiSi.Size = New System.Drawing.Size(50, 20)
        Me.optDatiSi.TabIndex = 16
        Me.optDatiSi.Text = "Sì"
        '
        'optDatiNo  (default: selezionato)
        '
        Me.optDatiNo.Location = New System.Drawing.Point(530, 48)
        Me.optDatiNo.Name = "optDatiNo"
        Me.optDatiNo.Size = New System.Drawing.Size(50, 20)
        Me.optDatiNo.TabIndex = 17
        Me.optDatiNo.Text = "No"
        Me.optDatiNo.Checked = True
        '
        'CmdElabora
        '
        Me.CmdElabora.BackColor = System.Drawing.Color.LightBlue
        Me.CmdElabora.Location = New System.Drawing.Point(676, 6)
        Me.CmdElabora.Name = "CmdElabora"
        Me.CmdElabora.Size = New System.Drawing.Size(90, 30)
        Me.CmdElabora.TabIndex = 10
        Me.CmdElabora.Text = "Elabora"
        Me.CmdElabora.UseVisualStyleBackColor = False
        '
        'CmdStampa
        '
        Me.CmdStampa.BackColor = System.Drawing.Color.LightGreen
        Me.CmdStampa.Location = New System.Drawing.Point(676, 42)
        Me.CmdStampa.Name = "CmdStampa"
        Me.CmdStampa.Size = New System.Drawing.Size(90, 24)
        Me.CmdStampa.TabIndex = 12
        Me.CmdStampa.Text = "Stampa"
        Me.CmdStampa.UseVisualStyleBackColor = False
        '
        'CmdEsci
        '
        Me.CmdEsci.BackColor = System.Drawing.Color.Salmon
        Me.CmdEsci.Location = New System.Drawing.Point(676, 72)
        Me.CmdEsci.Name = "CmdEsci"
        Me.CmdEsci.Size = New System.Drawing.Size(90, 24)
        Me.CmdEsci.TabIndex = 11
        Me.CmdEsci.Text = "Chiudi"
        Me.CmdEsci.UseVisualStyleBackColor = False
        '
        'pnlControls
        '
        Me.pnlControls.Controls.Add(Me.LblDI)
        Me.pnlControls.Controls.Add(Me.mskDataIni)
        Me.pnlControls.Controls.Add(Me.LblOI)
        Me.pnlControls.Controls.Add(Me.mskOraIni)
        Me.pnlControls.Controls.Add(Me.LblDF)
        Me.pnlControls.Controls.Add(Me.mskDataFin)
        Me.pnlControls.Controls.Add(Me.LblOF)
        Me.pnlControls.Controls.Add(Me.mskOraFin)
        Me.pnlControls.Controls.Add(Me.chkPrincipale)
        Me.pnlControls.Controls.Add(Me.chkSecondaria)
        Me.pnlControls.Controls.Add(Me.optLinee)
        Me.pnlControls.Controls.Add(Me.optPunti)
        Me.pnlControls.Controls.Add(Me.LblMostraDati)
        Me.pnlControls.Controls.Add(Me.optDatiSi)
        Me.pnlControls.Controls.Add(Me.optDatiNo)
        Me.pnlControls.Controls.Add(Me.CmdElabora)
        Me.pnlControls.Controls.Add(Me.CmdStampa)
        Me.pnlControls.Controls.Add(Me.CmdEsci)
        Me.pnlControls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(1394, 118)
        Me.pnlControls.TabIndex = 1
        '
        'LblDI
        '
        Me.LblDI.Location = New System.Drawing.Point(4, 8)
        Me.LblDI.Name = "LblDI"
        Me.LblDI.Size = New System.Drawing.Size(60, 18)
        Me.LblDI.TabIndex = 0
        Me.LblDI.Text = "Dal:"
        '
        'LblOI
        '
        Me.LblOI.Location = New System.Drawing.Point(158, 8)
        Me.LblOI.Name = "LblOI"
        Me.LblOI.Size = New System.Drawing.Size(34, 18)
        Me.LblOI.TabIndex = 2
        Me.LblOI.Text = "Ora:"
        '
        'LblDF
        '
        Me.LblDF.Location = New System.Drawing.Point(4, 34)
        Me.LblDF.Name = "LblDF"
        Me.LblDF.Size = New System.Drawing.Size(60, 18)
        Me.LblDF.TabIndex = 4
        Me.LblDF.Text = "Al:"
        '
        'LblOF
        '
        Me.LblOF.Location = New System.Drawing.Point(158, 34)
        Me.LblOF.Name = "LblOF"
        Me.LblOF.Size = New System.Drawing.Size(34, 18)
        Me.LblOF.TabIndex = 6
        Me.LblOF.Text = "Ora:"
        '
        'FrmVisualizzazione
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1394, 655)
        Me.Controls.Add(Me.picGrafico)
        Me.Controls.Add(Me.pnlControls)
        Me.Name = "FrmVisualizzazione"
        Me.Text = "Data Analysis (View)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlControls.ResumeLayout(False)
        Me.pnlControls.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private pnlControls  As System.Windows.Forms.Panel
    Private LblDI As System.Windows.Forms.Label : Private LblDF As System.Windows.Forms.Label
    Private LblOI As System.Windows.Forms.Label : Private LblOF As System.Windows.Forms.Label

    Friend WithEvents CmdElabora As System.Windows.Forms.Button
    Friend WithEvents CmdStampa As System.Windows.Forms.Button
    Friend WithEvents CmdEsci As System.Windows.Forms.Button
    Friend WithEvents chkPrincipale As System.Windows.Forms.CheckBox
    Friend WithEvents chkSecondaria As System.Windows.Forms.CheckBox
    Friend WithEvents optLinee As System.Windows.Forms.RadioButton
    Friend WithEvents optPunti As System.Windows.Forms.RadioButton
    Private LblMostraDati As System.Windows.Forms.Label
    Friend WithEvents optDatiSi As System.Windows.Forms.RadioButton
    Friend WithEvents optDatiNo As System.Windows.Forms.RadioButton
    Friend picGrafico As System.Windows.Forms.PictureBox
    Friend mskDataIni As System.Windows.Forms.MaskedTextBox
    Friend mskDataFin As System.Windows.Forms.MaskedTextBox
    Friend mskOraIni As System.Windows.Forms.MaskedTextBox
    Friend mskOraFin As System.Windows.Forms.MaskedTextBox

End Class
