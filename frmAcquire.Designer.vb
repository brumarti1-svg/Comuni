<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAcquire
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
        Me.components    = New System.ComponentModel.Container()
        Me.CmdAnnulla    = New System.Windows.Forms.Button()
        Me.CmdEnd        = New System.Windows.Forms.Button()
        Me.optPreset     = New System.Windows.Forms.RadioButton()
        Me.optProgram    = New System.Windows.Forms.RadioButton()
        Me.mskDataIni    = New System.Windows.Forms.TextBox()
        Me.mskAzimuth    = New System.Windows.Forms.TextBox()
        Me.mskElevazione = New System.Windows.Forms.TextBox()
        Me.TxtAzRange    = New System.Windows.Forms.TextBox()
        Me.TxtElRange    = New System.Windows.Forms.TextBox()
        Me.TxtAzStep     = New System.Windows.Forms.TextBox()
        Me.TxtElStep     = New System.Windows.Forms.TextBox()
        Me.TxtAzStepLock = New System.Windows.Forms.TextBox()
        Me.TxtElStepLock = New System.Windows.Forms.TextBox()
        Me.TxtDeltaTime  = New System.Windows.Forms.TextBox()
        Me.TxtTout       = New System.Windows.Forms.TextBox()
        Me.TxtCoorAcq    = New System.Windows.Forms.TextBox()
        Me.TxtLockThr    = New System.Windows.Forms.TextBox()
        Me.grpAcqPar     = New System.Windows.Forms.GroupBox()
        ' Labels
        Dim LblDataIni   As New System.Windows.Forms.Label()
        Dim LblAz        As New System.Windows.Forms.Label()
        Dim LblEl        As New System.Windows.Forms.Label()
        Dim LblAzRange   As New System.Windows.Forms.Label()
        Dim LblElRange   As New System.Windows.Forms.Label()
        Dim LblAzStep    As New System.Windows.Forms.Label()
        Dim LblElStep    As New System.Windows.Forms.Label()
        Dim LblAzLock    As New System.Windows.Forms.Label()
        Dim LblElLock    As New System.Windows.Forms.Label()
        Dim LblDelta     As New System.Windows.Forms.Label()
        Dim LblTout      As New System.Windows.Forms.Label()
        Dim LblCoor      As New System.Windows.Forms.Label()
        Dim LblLock      As New System.Windows.Forms.Label()
        Me.grpAcqPar.SuspendLayout()
        Me.SuspendLayout()
        '
        ' optPreset
        '
        Me.optPreset.Location = New System.Drawing.Point(12, 12)
        Me.optPreset.Name = "optPreset"
        Me.optPreset.Size = New System.Drawing.Size(120, 24)
        Me.optPreset.TabIndex = 0
        Me.optPreset.Text = "Preset position"
        '
        ' optProgram
        '
        Me.optProgram.Location = New System.Drawing.Point(140, 12)
        Me.optProgram.Name = "optProgram"
        Me.optProgram.Size = New System.Drawing.Size(140, 24)
        Me.optProgram.TabIndex = 1
        Me.optProgram.Text = "Program position"
        '
        ' LblDataIni
        '
        LblDataIni.Location = New System.Drawing.Point(12, 44)
        LblDataIni.Size = New System.Drawing.Size(88, 23)
        LblDataIni.Text = "Start date/time:"
        '
        ' mskDataIni
        '
        Me.mskDataIni.Location = New System.Drawing.Point(108, 41)
        Me.mskDataIni.Name = "mskDataIni"
        Me.mskDataIni.Size = New System.Drawing.Size(160, 23)
        Me.mskDataIni.TabIndex = 2
        '
        ' LblAz
        '
        LblAz.Location = New System.Drawing.Point(12, 74)
        LblAz.Size = New System.Drawing.Size(88, 23)
        LblAz.Text = "Azimuth:"
        '
        ' mskAzimuth
        '
        Me.mskAzimuth.Location = New System.Drawing.Point(108, 71)
        Me.mskAzimuth.Name = "mskAzimuth"
        Me.mskAzimuth.Size = New System.Drawing.Size(80, 23)
        Me.mskAzimuth.TabIndex = 3
        '
        ' LblEl
        '
        LblEl.Location = New System.Drawing.Point(196, 74)
        LblEl.Size = New System.Drawing.Size(80, 23)
        LblEl.Text = "Elevation:"
        '
        ' mskElevazione
        '
        Me.mskElevazione.Location = New System.Drawing.Point(284, 71)
        Me.mskElevazione.Name = "mskElevazione"
        Me.mskElevazione.Size = New System.Drawing.Size(80, 23)
        Me.mskElevazione.TabIndex = 4
        '
        ' grpAcqPar
        '
        Me.grpAcqPar.Location = New System.Drawing.Point(12, 104)
        Me.grpAcqPar.Name = "grpAcqPar"
        Me.grpAcqPar.Size = New System.Drawing.Size(700, 160)
        Me.grpAcqPar.TabIndex = 5
        Me.grpAcqPar.Text = "Acquisition Parameters"
        Me.grpAcqPar.Visible = True
        ' --- controls inside grpAcqPar ---
        LblAzRange.Location = New System.Drawing.Point(8, 24)
        LblAzRange.Size = New System.Drawing.Size(80, 20)
        LblAzRange.Text = "Az Range:"
        Me.TxtAzRange.Location = New System.Drawing.Point(96, 21)
        Me.TxtAzRange.Name = "TxtAzRange"
        Me.TxtAzRange.Size = New System.Drawing.Size(60, 23)
        Me.TxtAzRange.TabIndex = 0

        LblElRange.Location = New System.Drawing.Point(164, 24)
        LblElRange.Size = New System.Drawing.Size(80, 20)
        LblElRange.Text = "El Range:"
        Me.TxtElRange.Location = New System.Drawing.Point(252, 21)
        Me.TxtElRange.Name = "TxtElRange"
        Me.TxtElRange.Size = New System.Drawing.Size(60, 23)
        Me.TxtElRange.TabIndex = 1

        LblAzStep.Location = New System.Drawing.Point(8, 54)
        LblAzStep.Size = New System.Drawing.Size(80, 20)
        LblAzStep.Text = "Az Step:"
        Me.TxtAzStep.Location = New System.Drawing.Point(96, 51)
        Me.TxtAzStep.Name = "TxtAzStep"
        Me.TxtAzStep.Size = New System.Drawing.Size(60, 23)
        Me.TxtAzStep.TabIndex = 2

        LblElStep.Location = New System.Drawing.Point(164, 54)
        LblElStep.Size = New System.Drawing.Size(80, 20)
        LblElStep.Text = "El Step:"
        Me.TxtElStep.Location = New System.Drawing.Point(252, 51)
        Me.TxtElStep.Name = "TxtElStep"
        Me.TxtElStep.Size = New System.Drawing.Size(60, 23)
        Me.TxtElStep.TabIndex = 3

        LblAzLock.Location = New System.Drawing.Point(8, 84)
        LblAzLock.Size = New System.Drawing.Size(88, 20)
        LblAzLock.Text = "Az Step Lock:"
        Me.TxtAzStepLock.Location = New System.Drawing.Point(96, 81)
        Me.TxtAzStepLock.Name = "TxtAzStepLock"
        Me.TxtAzStepLock.Size = New System.Drawing.Size(60, 23)
        Me.TxtAzStepLock.TabIndex = 4

        LblElLock.Location = New System.Drawing.Point(164, 84)
        LblElLock.Size = New System.Drawing.Size(88, 20)
        LblElLock.Text = "El Step Lock:"
        Me.TxtElStepLock.Location = New System.Drawing.Point(252, 81)
        Me.TxtElStepLock.Name = "TxtElStepLock"
        Me.TxtElStepLock.Size = New System.Drawing.Size(60, 23)
        Me.TxtElStepLock.TabIndex = 5

        LblDelta.Location = New System.Drawing.Point(328, 24)
        LblDelta.Size = New System.Drawing.Size(80, 20)
        LblDelta.Text = "Int. Acq (s):"
        Me.TxtDeltaTime.Location = New System.Drawing.Point(416, 21)
        Me.TxtDeltaTime.Name = "TxtDeltaTime"
        Me.TxtDeltaTime.Size = New System.Drawing.Size(60, 23)
        Me.TxtDeltaTime.TabIndex = 6

        LblTout.Location = New System.Drawing.Point(328, 54)
        LblTout.Size = New System.Drawing.Size(80, 20)
        LblTout.Text = "Timeout (m):"
        Me.TxtTout.Location = New System.Drawing.Point(416, 51)
        Me.TxtTout.Name = "TxtTout"
        Me.TxtTout.Size = New System.Drawing.Size(60, 23)
        Me.TxtTout.TabIndex = 7

        LblCoor.Location = New System.Drawing.Point(328, 84)
        LblCoor.Size = New System.Drawing.Size(80, 20)
        LblCoor.Text = "Coor Acq:"
        Me.TxtCoorAcq.Location = New System.Drawing.Point(416, 81)
        Me.TxtCoorAcq.Name = "TxtCoorAcq"
        Me.TxtCoorAcq.Size = New System.Drawing.Size(60, 23)
        Me.TxtCoorAcq.TabIndex = 8

        LblLock.Location = New System.Drawing.Point(484, 24)
        LblLock.Size = New System.Drawing.Size(80, 20)
        LblLock.Text = "Lock Thr:"
        Me.TxtLockThr.Location = New System.Drawing.Point(572, 21)
        Me.TxtLockThr.Name = "TxtLockThr"
        Me.TxtLockThr.Size = New System.Drawing.Size(60, 23)
        Me.TxtLockThr.TabIndex = 9

        Me.grpAcqPar.Controls.AddRange(New System.Windows.Forms.Control() {
            LblAzRange, Me.TxtAzRange, LblElRange, Me.TxtElRange,
            LblAzStep, Me.TxtAzStep, LblElStep, Me.TxtElStep,
            LblAzLock, Me.TxtAzStepLock, LblElLock, Me.TxtElStepLock,
            LblDelta, Me.TxtDeltaTime, LblTout, Me.TxtTout,
            LblCoor, Me.TxtCoorAcq, LblLock, Me.TxtLockThr})
        '
        ' CmdEnd
        '
        Me.CmdEnd.Location = New System.Drawing.Point(540, 280)
        Me.CmdEnd.Name = "CmdEnd"
        Me.CmdEnd.Size = New System.Drawing.Size(100, 30)
        Me.CmdEnd.TabIndex = 6
        Me.CmdEnd.Text = "OK"
        Me.CmdEnd.Visible = True
        '
        ' CmdAnnulla
        '
        Me.CmdAnnulla.Location = New System.Drawing.Point(648, 280)
        Me.CmdAnnulla.Name = "CmdAnnulla"
        Me.CmdAnnulla.Size = New System.Drawing.Size(100, 30)
        Me.CmdAnnulla.TabIndex = 7
        Me.CmdAnnulla.Text = "Annulla"
        '
        ' frmAcquire
        '
        Me.ClientSize = New System.Drawing.Size(1378, 321)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.optPreset, Me.optProgram,
            LblDataIni, Me.mskDataIni,
            LblAz, Me.mskAzimuth, LblEl, Me.mskElevazione,
            Me.grpAcqPar, Me.CmdEnd, Me.CmdAnnulla})
        Me.Name = "frmAcquire"
        Me.Text = "Spiral Acquire"
        Me.grpAcqPar.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents CmdAnnulla    As System.Windows.Forms.Button
    Friend WithEvents CmdEnd        As System.Windows.Forms.Button
    Friend WithEvents optPreset     As System.Windows.Forms.RadioButton
    Friend WithEvents optProgram    As System.Windows.Forms.RadioButton
    Friend mskDataIni               As System.Windows.Forms.TextBox
    Friend mskAzimuth               As System.Windows.Forms.TextBox
    Friend mskElevazione            As System.Windows.Forms.TextBox
    Friend TxtAzRange               As System.Windows.Forms.TextBox
    Friend TxtElRange               As System.Windows.Forms.TextBox
    Friend TxtAzStep                As System.Windows.Forms.TextBox
    Friend TxtElStep                As System.Windows.Forms.TextBox
    Friend TxtAzStepLock            As System.Windows.Forms.TextBox
    Friend TxtElStepLock            As System.Windows.Forms.TextBox
    Friend TxtDeltaTime             As System.Windows.Forms.TextBox
    Friend TxtTout                  As System.Windows.Forms.TextBox
    Friend TxtCoorAcq               As System.Windows.Forms.TextBox
    Friend TxtLockThr               As System.Windows.Forms.TextBox
    Friend grpAcqPar                As System.Windows.Forms.GroupBox

End Class
