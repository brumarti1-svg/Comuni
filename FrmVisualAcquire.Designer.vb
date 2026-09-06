<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmVisualAcquire
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.components    = New System.ComponentModel.Container()
        Me.tmrAcquire    = New System.Windows.Forms.Timer(Me.components)
        Me.mskDataIni    = New System.Windows.Forms.MaskedTextBox()
        Me.mskDataFin    = New System.Windows.Forms.MaskedTextBox()
        Me.mskOraIni     = New System.Windows.Forms.MaskedTextBox()
        Me.mskOraFin     = New System.Windows.Forms.MaskedTextBox()
        Me.picGrafico    = New System.Windows.Forms.PictureBox()
        Me.chkPrincipale = New System.Windows.Forms.CheckBox()
        Me.chkSecondaria = New System.Windows.Forms.CheckBox()
        Me.CmdElabora    = New System.Windows.Forms.Button()
        Me.CmdEsci       = New System.Windows.Forms.Button()
        Me.TxtAzRange    = New System.Windows.Forms.TextBox()
        Me.TxtElRange    = New System.Windows.Forms.TextBox()
        Me.TxtAzStep     = New System.Windows.Forms.TextBox()
        Me.TxtElStep     = New System.Windows.Forms.TextBox()
        Me.TxtAzStepLock = New System.Windows.Forms.TextBox()
        Me.TxtElStepLock = New System.Windows.Forms.TextBox()
        Me.TxtDeltaTime  = New System.Windows.Forms.TextBox()
        Me.TxtCoorAcq    = New System.Windows.Forms.TextBox()
        Me.TxtLockThr    = New System.Windows.Forms.TextBox()
        Me.pnlTop        = New System.Windows.Forms.Panel()
        Me.pnlAcq        = New System.Windows.Forms.Panel()
        ' Static labels for pnlTop
        Me.LblDI2        = New System.Windows.Forms.Label()
        Me.LblOI2        = New System.Windows.Forms.Label()
        Me.LblDF2        = New System.Windows.Forms.Label()
        Me.LblOF2        = New System.Windows.Forms.Label()
        ' Static labels for pnlAcq
        Me.LblAzRange    = New System.Windows.Forms.Label()
        Me.LblElRange    = New System.Windows.Forms.Label()
        Me.LblAzStep     = New System.Windows.Forms.Label()
        Me.LblElStep     = New System.Windows.Forms.Label()
        Me.LblAzLock     = New System.Windows.Forms.Label()
        Me.LblElLock     = New System.Windows.Forms.Label()
        Me.LblDeltaTime  = New System.Windows.Forms.Label()
        Me.LblCoorAcq    = New System.Windows.Forms.Label()
        Me.LblLockThr    = New System.Windows.Forms.Label()
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.pnlAcq.SuspendLayout()
        Me.SuspendLayout()
        ' tmrAcquire
        Me.tmrAcquire.Interval = 1000
        ' pnlTop — date/ora selectors
        Me.pnlTop.Dock   = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Height = 60
        Me.pnlTop.Name   = "pnlTop"
        ' LblDI2
        Me.LblDI2.Location = New System.Drawing.Point(4, 8)
        Me.LblDI2.Size     = New System.Drawing.Size(60, 18)
        Me.LblDI2.Text     = "Dal:"
        ' mskDataIni
        Me.mskDataIni.Location = New System.Drawing.Point(66, 6)
        Me.mskDataIni.Name     = "mskDataIni"
        Me.mskDataIni.Size     = New System.Drawing.Size(88, 22)
        Me.mskDataIni.Mask     = "00/00/0000"
        ' LblOI2
        Me.LblOI2.Location = New System.Drawing.Point(158, 8)
        Me.LblOI2.Size     = New System.Drawing.Size(34, 18)
        Me.LblOI2.Text     = "Ora:"
        ' mskOraIni
        Me.mskOraIni.Location = New System.Drawing.Point(194, 6)
        Me.mskOraIni.Name     = "mskOraIni"
        Me.mskOraIni.Size     = New System.Drawing.Size(60, 22)
        Me.mskOraIni.Mask     = "00.00.00"
        ' LblDF2
        Me.LblDF2.Location = New System.Drawing.Point(4, 34)
        Me.LblDF2.Size     = New System.Drawing.Size(60, 18)
        Me.LblDF2.Text     = "Al:"
        ' mskDataFin
        Me.mskDataFin.Location = New System.Drawing.Point(66, 32)
        Me.mskDataFin.Name     = "mskDataFin"
        Me.mskDataFin.Size     = New System.Drawing.Size(88, 22)
        Me.mskDataFin.Mask     = "00/00/0000"
        ' LblOF2
        Me.LblOF2.Location = New System.Drawing.Point(158, 34)
        Me.LblOF2.Size     = New System.Drawing.Size(34, 18)
        Me.LblOF2.Text     = "Ora:"
        ' mskOraFin
        Me.mskOraFin.Location = New System.Drawing.Point(194, 32)
        Me.mskOraFin.Name     = "mskOraFin"
        Me.mskOraFin.Size     = New System.Drawing.Size(60, 22)
        Me.mskOraFin.Mask     = "00.00.00"
        ' chkPrincipale
        Me.chkPrincipale.Location = New System.Drawing.Point(264, 8)
        Me.chkPrincipale.Name     = "chkPrincipale"
        Me.chkPrincipale.Size     = New System.Drawing.Size(90, 20)
        Me.chkPrincipale.Text     = "Principale"
        Me.chkPrincipale.Checked  = True
        ' chkSecondaria
        Me.chkSecondaria.Location = New System.Drawing.Point(264, 32)
        Me.chkSecondaria.Name     = "chkSecondaria"
        Me.chkSecondaria.Size     = New System.Drawing.Size(90, 20)
        Me.chkSecondaria.Text     = "Secondaria"
        ' CmdElabora
        Me.CmdElabora.Location  = New System.Drawing.Point(364, 8)
        Me.CmdElabora.Name      = "CmdElabora"
        Me.CmdElabora.Size      = New System.Drawing.Size(90, 22)
        Me.CmdElabora.Text      = "Elabora"
        Me.CmdElabora.BackColor = System.Drawing.Color.LightBlue
        ' CmdEsci
        Me.CmdEsci.Location  = New System.Drawing.Point(364, 34)
        Me.CmdEsci.Name      = "CmdEsci"
        Me.CmdEsci.Size      = New System.Drawing.Size(90, 22)
        Me.CmdEsci.Text      = "Close"
        Me.CmdEsci.BackColor = System.Drawing.Color.Salmon
        Me.pnlTop.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.LblDI2, Me.mskDataIni, Me.LblOI2, Me.mskOraIni,
            Me.LblDF2, Me.mskDataFin, Me.LblOF2, Me.mskOraFin,
            Me.chkPrincipale, Me.chkSecondaria, Me.CmdElabora, Me.CmdEsci})
        ' pnlAcq — Acquire parameters (right side)
        Me.pnlAcq.Dock  = System.Windows.Forms.DockStyle.Right
        Me.pnlAcq.Width = 160
        Me.pnlAcq.Name  = "pnlAcq"
        ' Az Range
        Me.LblAzRange.Text     = "Az Range:"
        Me.LblAzRange.Location = New System.Drawing.Point(4, 8)
        Me.LblAzRange.Size     = New System.Drawing.Size(80, 18)
        Me.TxtAzRange.Location = New System.Drawing.Point(86, 8)
        Me.TxtAzRange.Size     = New System.Drawing.Size(68, 21)
        ' El Range
        Me.LblElRange.Text     = "El Range:"
        Me.LblElRange.Location = New System.Drawing.Point(4, 34)
        Me.LblElRange.Size     = New System.Drawing.Size(80, 18)
        Me.TxtElRange.Location = New System.Drawing.Point(86, 34)
        Me.TxtElRange.Size     = New System.Drawing.Size(68, 21)
        ' Az Step
        Me.LblAzStep.Text     = "Az Step:"
        Me.LblAzStep.Location = New System.Drawing.Point(4, 60)
        Me.LblAzStep.Size     = New System.Drawing.Size(80, 18)
        Me.TxtAzStep.Location = New System.Drawing.Point(86, 60)
        Me.TxtAzStep.Size     = New System.Drawing.Size(68, 21)
        ' El Step
        Me.LblElStep.Text     = "El Step:"
        Me.LblElStep.Location = New System.Drawing.Point(4, 86)
        Me.LblElStep.Size     = New System.Drawing.Size(80, 18)
        Me.TxtElStep.Location = New System.Drawing.Point(86, 86)
        Me.TxtElStep.Size     = New System.Drawing.Size(68, 21)
        ' Az Lock
        Me.LblAzLock.Text        = "Az Lock:"
        Me.LblAzLock.Location    = New System.Drawing.Point(4, 112)
        Me.LblAzLock.Size        = New System.Drawing.Size(80, 18)
        Me.TxtAzStepLock.Location = New System.Drawing.Point(86, 112)
        Me.TxtAzStepLock.Size    = New System.Drawing.Size(68, 21)
        ' El Lock
        Me.LblElLock.Text        = "El Lock:"
        Me.LblElLock.Location    = New System.Drawing.Point(4, 138)
        Me.LblElLock.Size        = New System.Drawing.Size(80, 18)
        Me.TxtElStepLock.Location = New System.Drawing.Point(86, 138)
        Me.TxtElStepLock.Size    = New System.Drawing.Size(68, 21)
        ' DeltaT
        Me.LblDeltaTime.Text     = "DeltaT:"
        Me.LblDeltaTime.Location = New System.Drawing.Point(4, 164)
        Me.LblDeltaTime.Size     = New System.Drawing.Size(80, 18)
        Me.TxtDeltaTime.Location = New System.Drawing.Point(86, 164)
        Me.TxtDeltaTime.Size     = New System.Drawing.Size(68, 21)
        ' Coor Acq
        Me.LblCoorAcq.Text     = "Coor Acq:"
        Me.LblCoorAcq.Location = New System.Drawing.Point(4, 190)
        Me.LblCoorAcq.Size     = New System.Drawing.Size(80, 18)
        Me.TxtCoorAcq.Location = New System.Drawing.Point(86, 190)
        Me.TxtCoorAcq.Size     = New System.Drawing.Size(68, 21)
        ' Lock Thr
        Me.LblLockThr.Text     = "Lock Thr:"
        Me.LblLockThr.Location = New System.Drawing.Point(4, 216)
        Me.LblLockThr.Size     = New System.Drawing.Size(80, 18)
        Me.TxtLockThr.Location = New System.Drawing.Point(86, 216)
        Me.TxtLockThr.Size     = New System.Drawing.Size(68, 21)
        Me.pnlAcq.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.LblAzRange,   Me.TxtAzRange,
            Me.LblElRange,   Me.TxtElRange,
            Me.LblAzStep,    Me.TxtAzStep,
            Me.LblElStep,    Me.TxtElStep,
            Me.LblAzLock,    Me.TxtAzStepLock,
            Me.LblElLock,    Me.TxtElStepLock,
            Me.LblDeltaTime, Me.TxtDeltaTime,
            Me.LblCoorAcq,   Me.TxtCoorAcq,
            Me.LblLockThr,   Me.TxtLockThr})
        ' picGrafico
        Me.picGrafico.Dock      = System.Windows.Forms.DockStyle.Fill
        Me.picGrafico.Name      = "picGrafico"
        Me.picGrafico.BackColor = System.Drawing.Color.Black
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize          = New System.Drawing.Size(900, 600)
        Me.Name = "FrmVisualAcquire"
        Me.Text = "Visual Acquire"
        Me.Controls.Add(Me.picGrafico)
        Me.Controls.Add(Me.pnlAcq)
        Me.Controls.Add(Me.pnlTop)
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlAcq.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend pnlTop As System.Windows.Forms.Panel
    Friend pnlAcq As System.Windows.Forms.Panel

    Friend WithEvents chkPrincipale As System.Windows.Forms.CheckBox
    Friend WithEvents chkSecondaria As System.Windows.Forms.CheckBox
    Friend WithEvents CmdElabora    As System.Windows.Forms.Button
    Friend WithEvents CmdEsci       As System.Windows.Forms.Button
    Friend WithEvents tmrAcquire    As System.Windows.Forms.Timer
    Friend WithEvents TxtAzRange    As System.Windows.Forms.TextBox
    Friend WithEvents TxtAzStep     As System.Windows.Forms.TextBox
    Friend WithEvents TxtAzStepLock As System.Windows.Forms.TextBox
    Friend WithEvents TxtCoorAcq    As System.Windows.Forms.TextBox
    Friend WithEvents TxtDeltaTime  As System.Windows.Forms.TextBox
    Friend WithEvents TxtElRange    As System.Windows.Forms.TextBox
    Friend WithEvents TxtElStep     As System.Windows.Forms.TextBox
    Friend WithEvents TxtElStepLock As System.Windows.Forms.TextBox
    Friend WithEvents TxtLockThr    As System.Windows.Forms.TextBox
    Friend picGrafico As System.Windows.Forms.PictureBox
    Friend mskDataIni As System.Windows.Forms.MaskedTextBox
    Friend mskDataFin As System.Windows.Forms.MaskedTextBox
    Friend mskOraIni  As System.Windows.Forms.MaskedTextBox
    Friend mskOraFin  As System.Windows.Forms.MaskedTextBox
    Friend LblDI2     As System.Windows.Forms.Label
    Friend LblOI2     As System.Windows.Forms.Label
    Friend LblDF2     As System.Windows.Forms.Label
    Friend LblOF2     As System.Windows.Forms.Label
    Friend LblAzRange  As System.Windows.Forms.Label
    Friend LblElRange  As System.Windows.Forms.Label
    Friend LblAzStep   As System.Windows.Forms.Label
    Friend LblElStep   As System.Windows.Forms.Label
    Friend LblAzLock   As System.Windows.Forms.Label
    Friend LblElLock   As System.Windows.Forms.Label
    Friend LblDeltaTime As System.Windows.Forms.Label
    Friend LblCoorAcq  As System.Windows.Forms.Label
    Friend LblLockThr  As System.Windows.Forms.Label

End Class
