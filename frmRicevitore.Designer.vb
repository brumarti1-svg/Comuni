<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRicevitore
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRicevitore))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPhase = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAugmented = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAutomatic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuManual = New System.Windows.Forms.ToolStripMenuItem()
        Me.TxtOffsetAz = New System.Windows.Forms.TextBox()
        Me.TxtOffsetEl = New System.Windows.Forms.TextBox()
        Me.tmrPhasing = New System.Windows.Forms.Timer(Me.components)
        Me.Frame2 = New System.Windows.Forms.Panel()
        Me.CmdPiu = New System.Windows.Forms.Button()
        Me.CmdMeno = New System.Windows.Forms.Button()
        Me.TxtPhase = New System.Windows.Forms.TextBox()
        Me.TxtPhAz = New System.Windows.Forms.TextBox()
        Me.CmdMenoAz = New System.Windows.Forms.Button()
        Me.CmdPiuAz = New System.Windows.Forms.Button()
        Me.TxtElGain1 = New System.Windows.Forms.TextBox()
        Me.TxtAzGain1 = New System.Windows.Forms.TextBox()
        Me.CmbRevAz = New System.Windows.Forms.ComboBox()
        Me.CmbInc = New System.Windows.Forms.ComboBox()
        Me.LblCapRevAzError = New System.Windows.Forms.Label()
        Me.LblLock = New System.Windows.Forms.Label()
        Me.LblCapRXStatus = New System.Windows.Forms.Label()
        Me.LblCapError = New System.Windows.Forms.Label()
        Me.LblErrAz = New System.Windows.Forms.Label()
        Me.LblErrEl = New System.Windows.Forms.Label()
        Me.LblCapDB = New System.Windows.Forms.Label()
        Me.TxtLev = New System.Windows.Forms.TextBox()
        Me.LblCapBCNLevel = New System.Windows.Forms.Label()
        Me.LblCapEL = New System.Windows.Forms.Label()
        Me.TxtEl = New System.Windows.Forms.TextBox()
        Me.TxtAz = New System.Windows.Forms.TextBox()
        Me.LblCapAZ = New System.Windows.Forms.Label()
        Me.LblBeacon = New System.Windows.Forms.Label()
        Me.LblCapLNA = New System.Windows.Forms.Label()
        Me.TxtAzErr = New System.Windows.Forms.TextBox()
        Me.TxtElErr = New System.Windows.Forms.TextBox()
        Me.TxtLna = New System.Windows.Forms.TextBox()
        Me.LblCapError2 = New System.Windows.Forms.Label()
        Me.LblCapPhaseInc = New System.Windows.Forms.Label()
        Me.TxtPhaseEl = New System.Windows.Forms.TextBox()
        Me.LblCapPhaseEl = New System.Windows.Forms.Label()
        Me.LblRevAz = New System.Windows.Forms.Label()
        Me.LblCapPhaseAz = New System.Windows.Forms.Label()
        Me.TxtPhaseAz = New System.Windows.Forms.TextBox()
        Me.TxtElGain = New System.Windows.Forms.TextBox()
        Me.LblCapElevation = New System.Windows.Forms.Label()
        Me.TxtAzGain = New System.Windows.Forms.TextBox()
        Me.LblCapGain = New System.Windows.Forms.Label()
        Me.LblCapAzimuth = New System.Windows.Forms.Label()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.tmrAlarm = New System.Windows.Forms.Timer(Me.components)
        Me.frmIFR = New System.Windows.Forms.GroupBox()
        Me.TxtRange = New System.Windows.Forms.TextBox()
        Me.TxtFreq = New System.Windows.Forms.TextBox()
        Me.CmbBandWidth = New System.Windows.Forms.ComboBox()
        Me.CmbVideo = New System.Windows.Forms.ComboBox()
        Me.CmbAFC = New System.Windows.Forms.ComboBox()
        Me.CmbAGC = New System.Windows.Forms.ComboBox()
        Me.CmbSelection = New System.Windows.Forms.ComboBox()
        Me.CmbLoop = New System.Windows.Forms.ComboBox()
        Me.CmbInputPort = New System.Windows.Forms.ComboBox()
        Me.CmbOperMode = New System.Windows.Forms.ComboBox()
        Me.CmbCarrier = New System.Windows.Forms.ComboBox()
        Me.LblCapMs = New System.Windows.Forms.Label()
        Me.LblCapMs2 = New System.Windows.Forms.Label()
        Me.LblCapKHz = New System.Windows.Forms.Label()
        Me.LblCapHz = New System.Windows.Forms.Label()
        Me.LblCapHz2 = New System.Windows.Forms.Label()
        Me.LblCapKHz2 = New System.Windows.Forms.Label()
        Me.LblCapIFBwInAMEFM = New System.Windows.Forms.Label()
        Me.LblCapVideoDemodulation = New System.Windows.Forms.Label()
        Me.LblCapAFC = New System.Windows.Forms.Label()
        Me.LblCapInputPort = New System.Windows.Forms.Label()
        Me.LblCapAGCTimeConstant = New System.Windows.Forms.Label()
        Me.LblCapInputSelection = New System.Windows.Forms.Label()
        Me.LblCapCarrierAcquisition = New System.Windows.Forms.Label()
        Me.LblCapLoopFilterBw = New System.Windows.Forms.Label()
        Me.LblCapOperatingMode = New System.Windows.Forms.Label()
        Me.LblCapInputFrequency = New System.Windows.Forms.Label()
        Me.LblCapAcquisitionRange = New System.Windows.Forms.Label()
        Me.TxtFreccia = New System.Windows.Forms.TextBox()
        Me.frmManual = New System.Windows.Forms.GroupBox()
        Me.CmbEl = New System.Windows.Forms.ComboBox()
        Me.CmdAnnulla = New System.Windows.Forms.Button()
        Me.CmdUp = New System.Windows.Forms.Button()
        Me.CmdCCW = New System.Windows.Forms.Button()
        Me.CmdCW = New System.Windows.Forms.Button()
        Me.CmdDown = New System.Windows.Forms.Button()
        Me.Image1 = New System.Windows.Forms.PictureBox()
        Me.LblTesto = New System.Windows.Forms.Label()
        Me.LblCapStepSize = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.LblCapPolErr = New System.Windows.Forms.Label()
        Me.TxtPol2 = New System.Windows.Forms.TextBox()
        Me.TxtPol1 = New System.Windows.Forms.TextBox()
        Me.LblPol = New System.Windows.Forms.Label()
        Me.TxtFrequency = New System.Windows.Forms.TextBox()
        Me.TxtInpFrequency = New System.Windows.Forms.TextBox()
        Me.TxtPhasingMode = New System.Windows.Forms.TextBox()
        Me.TxtPol = New System.Windows.Forms.TextBox()
        Me.TxtPolarizationAngle = New System.Windows.Forms.TextBox()
        Me.TxtPhasingFlow = New System.Windows.Forms.TextBox()
        Me.LblCapReceivingMode = New System.Windows.Forms.Label()
        Me.LblCapRFInputFrequencyKHz = New System.Windows.Forms.Label()
        Me.LblCapPolarizationAngleDeg = New System.Windows.Forms.Label()
        Me.LblCapPhasingFlowIdHex = New System.Windows.Forms.Label()
        Me.LblCapOffsetAzimuth = New System.Windows.Forms.Label()
        Me.LblCapElevation2 = New System.Windows.Forms.Label()
        Me.LblWarning = New System.Windows.Forms.Label()
        Me.LblCapRXValue = New System.Windows.Forms.Label()
        Me.LblCapACUValue = New System.Windows.Forms.Label()
        Me.LblRxValueBox = New System.Windows.Forms.Label()
        Me.LblAcuValueBox = New System.Windows.Forms.Label()
        Me.TxtValid = New System.Windows.Forms.TextBox()
        Me.TxtProcess = New System.Windows.Forms.TextBox()
        Me.LblPhase = New System.Windows.Forms.Label()
        Me.LblCapSATELLITE = New System.Windows.Forms.Label()
        Me.LblSatellite = New System.Windows.Forms.Label()
        Me.LblCapProcess = New System.Windows.Forms.Label()
        Me.LblCapValid = New System.Windows.Forms.Label()
        Me.LblInputRx = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.frmIFR.SuspendLayout()
        Me.frmManual.SuspendLayout()
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuExit, Me.MnuPhase})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(760, 28)
        Me.MenuStrip1.TabIndex = 23
        '
        'MnuExit
        '
        Me.MnuExit.Name = "MnuExit"
        Me.MnuExit.Size = New System.Drawing.Size(47, 24)
        Me.MnuExit.Text = "Exit"
        '
        'MnuPhase
        '
        Me.MnuPhase.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuAugmented, Me.MnuAutomatic, Me.MnuManual})
        Me.MnuPhase.Name = "MnuPhase"
        Me.MnuPhase.Size = New System.Drawing.Size(61, 24)
        Me.MnuPhase.Text = "Phase"
        '
        'MnuAugmented
        '
        Me.MnuAugmented.Name = "MnuAugmented"
        Me.MnuAugmented.Size = New System.Drawing.Size(224, 26)
        Me.MnuAugmented.Text = "Augmented"
        '
        'MnuAutomatic
        '
        Me.MnuAutomatic.Name = "MnuAutomatic"
        Me.MnuAutomatic.Size = New System.Drawing.Size(224, 26)
        Me.MnuAutomatic.Text = "Automatic"
        '
        'MnuManual
        '
        Me.MnuManual.Name = "MnuManual"
        Me.MnuManual.Size = New System.Drawing.Size(224, 26)
        Me.MnuManual.Text = "Manual"
        '
        'TxtOffsetAz
        '
        Me.TxtOffsetAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtOffsetAz.Location = New System.Drawing.Point(549, 477)
        Me.TxtOffsetAz.Name = "TxtOffsetAz"
        Me.TxtOffsetAz.Size = New System.Drawing.Size(49, 22)
        Me.TxtOffsetAz.TabIndex = 0
        Me.TxtOffsetAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtOffsetEl
        '
        Me.TxtOffsetEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtOffsetEl.Location = New System.Drawing.Point(684, 476)
        Me.TxtOffsetEl.Name = "TxtOffsetEl"
        Me.TxtOffsetEl.Size = New System.Drawing.Size(49, 22)
        Me.TxtOffsetEl.TabIndex = 1
        Me.TxtOffsetEl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tmrPhasing
        '
        Me.tmrPhasing.Interval = 30000
        '
        'Frame2
        '
        Me.Frame2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Frame2.Controls.Add(Me.CmdPiu)
        Me.Frame2.Controls.Add(Me.CmdMeno)
        Me.Frame2.Controls.Add(Me.TxtPhase)
        Me.Frame2.Controls.Add(Me.TxtPhAz)
        Me.Frame2.Controls.Add(Me.CmdMenoAz)
        Me.Frame2.Controls.Add(Me.CmdPiuAz)
        Me.Frame2.Controls.Add(Me.TxtElGain1)
        Me.Frame2.Controls.Add(Me.TxtAzGain1)
        Me.Frame2.Controls.Add(Me.CmbRevAz)
        Me.Frame2.Controls.Add(Me.CmbInc)
        Me.Frame2.Controls.Add(Me.LblCapRevAzError)
        Me.Frame2.Controls.Add(Me.LblLock)
        Me.Frame2.Controls.Add(Me.LblCapRXStatus)
        Me.Frame2.Controls.Add(Me.LblCapError)
        Me.Frame2.Controls.Add(Me.LblErrAz)
        Me.Frame2.Controls.Add(Me.LblErrEl)
        Me.Frame2.Controls.Add(Me.LblCapDB)
        Me.Frame2.Controls.Add(Me.TxtLev)
        Me.Frame2.Controls.Add(Me.LblCapBCNLevel)
        Me.Frame2.Controls.Add(Me.LblCapEL)
        Me.Frame2.Controls.Add(Me.TxtEl)
        Me.Frame2.Controls.Add(Me.TxtAz)
        Me.Frame2.Controls.Add(Me.LblCapAZ)
        Me.Frame2.Controls.Add(Me.LblBeacon)
        Me.Frame2.Controls.Add(Me.LblCapLNA)
        Me.Frame2.Controls.Add(Me.TxtAzErr)
        Me.Frame2.Controls.Add(Me.TxtElErr)
        Me.Frame2.Controls.Add(Me.TxtLna)
        Me.Frame2.Controls.Add(Me.LblCapError2)
        Me.Frame2.Controls.Add(Me.LblCapPhaseInc)
        Me.Frame2.Controls.Add(Me.TxtPhaseEl)
        Me.Frame2.Controls.Add(Me.LblCapPhaseEl)
        Me.Frame2.Controls.Add(Me.LblRevAz)
        Me.Frame2.Controls.Add(Me.LblCapPhaseAz)
        Me.Frame2.Controls.Add(Me.TxtPhaseAz)
        Me.Frame2.Controls.Add(Me.TxtElGain)
        Me.Frame2.Controls.Add(Me.LblCapElevation)
        Me.Frame2.Controls.Add(Me.TxtAzGain)
        Me.Frame2.Controls.Add(Me.LblCapGain)
        Me.Frame2.Controls.Add(Me.LblCapAzimuth)
        Me.Frame2.Location = New System.Drawing.Point(4, 159)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Size = New System.Drawing.Size(478, 234)
        Me.Frame2.TabIndex = 2
        '
        'CmdPiu
        '
        Me.CmdPiu.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.CmdPiu.Location = New System.Drawing.Point(170, 192)
        Me.CmdPiu.Name = "CmdPiu"
        Me.CmdPiu.Size = New System.Drawing.Size(18, 30)
        Me.CmdPiu.TabIndex = 0
        Me.CmdPiu.Text = "+"
        '
        'CmdMeno
        '
        Me.CmdMeno.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmdMeno.Location = New System.Drawing.Point(103, 192)
        Me.CmdMeno.Name = "CmdMeno"
        Me.CmdMeno.Size = New System.Drawing.Size(18, 30)
        Me.CmdMeno.TabIndex = 1
        Me.CmdMeno.Text = "-"
        '
        'TxtPhase
        '
        Me.TxtPhase.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhase.Location = New System.Drawing.Point(124, 200)
        Me.TxtPhase.Name = "TxtPhase"
        Me.TxtPhase.Size = New System.Drawing.Size(42, 22)
        Me.TxtPhase.TabIndex = 2
        Me.TxtPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtPhAz
        '
        Me.TxtPhAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhAz.Location = New System.Drawing.Point(28, 200)
        Me.TxtPhAz.Name = "TxtPhAz"
        Me.TxtPhAz.Size = New System.Drawing.Size(42, 22)
        Me.TxtPhAz.TabIndex = 3
        Me.TxtPhAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPhAz.Visible = False
        '
        'CmdMenoAz
        '
        Me.CmdMenoAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmdMenoAz.Location = New System.Drawing.Point(5, 192)
        Me.CmdMenoAz.Name = "CmdMenoAz"
        Me.CmdMenoAz.Size = New System.Drawing.Size(18, 30)
        Me.CmdMenoAz.TabIndex = 4
        Me.CmdMenoAz.Text = "-"
        Me.CmdMenoAz.Visible = False
        '
        'CmdPiuAz
        '
        Me.CmdPiuAz.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.CmdPiuAz.Location = New System.Drawing.Point(70, 192)
        Me.CmdPiuAz.Name = "CmdPiuAz"
        Me.CmdPiuAz.Size = New System.Drawing.Size(18, 30)
        Me.CmdPiuAz.TabIndex = 5
        Me.CmdPiuAz.Text = "+"
        Me.CmdPiuAz.Visible = False
        '
        'TxtElGain1
        '
        Me.TxtElGain1.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtElGain1.Location = New System.Drawing.Point(353, 158)
        Me.TxtElGain1.Name = "TxtElGain1"
        Me.TxtElGain1.Size = New System.Drawing.Size(49, 22)
        Me.TxtElGain1.TabIndex = 6
        Me.TxtElGain1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtAzGain1
        '
        Me.TxtAzGain1.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtAzGain1.Location = New System.Drawing.Point(286, 158)
        Me.TxtAzGain1.Name = "TxtAzGain1"
        Me.TxtAzGain1.Size = New System.Drawing.Size(49, 22)
        Me.TxtAzGain1.TabIndex = 7
        Me.TxtAzGain1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmbRevAz
        '
        Me.CmbRevAz.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbRevAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbRevAz.Items.AddRange(New Object() {"Y", "N"})
        Me.CmbRevAz.Location = New System.Drawing.Point(401, 94)
        Me.CmbRevAz.Name = "CmbRevAz"
        Me.CmbRevAz.Size = New System.Drawing.Size(37, 24)
        Me.CmbRevAz.TabIndex = 8
        '
        'CmbInc
        '
        Me.CmbInc.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbInc.Items.AddRange(New Object() {"1", "2", "5", "10"})
        Me.CmbInc.Location = New System.Drawing.Point(307, 200)
        Me.CmbInc.Name = "CmbInc"
        Me.CmbInc.Size = New System.Drawing.Size(49, 24)
        Me.CmbInc.TabIndex = 9
        '
        'LblCapRevAzError
        '
        Me.LblCapRevAzError.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapRevAzError.Location = New System.Drawing.Point(360, 72)
        Me.LblCapRevAzError.Name = "LblCapRevAzError"
        Me.LblCapRevAzError.Size = New System.Drawing.Size(90, 18)
        Me.LblCapRevAzError.TabIndex = 10
        Me.LblCapRevAzError.Text = "Rev Az Error"
        Me.LblCapRevAzError.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblLock
        '
        Me.LblLock.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.LblLock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblLock.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblLock.Location = New System.Drawing.Point(248, 94)
        Me.LblLock.Name = "LblLock"
        Me.LblLock.Size = New System.Drawing.Size(56, 22)
        Me.LblLock.TabIndex = 11
        Me.LblLock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblCapRXStatus
        '
        Me.LblCapRXStatus.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapRXStatus.Location = New System.Drawing.Point(237, 72)
        Me.LblCapRXStatus.Name = "LblCapRXStatus"
        Me.LblCapRXStatus.Size = New System.Drawing.Size(68, 18)
        Me.LblCapRXStatus.TabIndex = 12
        Me.LblCapRXStatus.Text = "RX Status"
        Me.LblCapRXStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapError
        '
        Me.LblCapError.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapError.Location = New System.Drawing.Point(25, 72)
        Me.LblCapError.Name = "LblCapError"
        Me.LblCapError.Size = New System.Drawing.Size(50, 18)
        Me.LblCapError.TabIndex = 13
        Me.LblCapError.Text = "Error "
        Me.LblCapError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblErrAz
        '
        Me.LblErrAz.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.LblErrAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblErrAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblErrAz.Location = New System.Drawing.Point(15, 94)
        Me.LblErrAz.Name = "LblErrAz"
        Me.LblErrAz.Size = New System.Drawing.Size(68, 22)
        Me.LblErrAz.TabIndex = 14
        Me.LblErrAz.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblErrEl
        '
        Me.LblErrEl.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.LblErrEl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblErrEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblErrEl.Location = New System.Drawing.Point(111, 94)
        Me.LblErrEl.Name = "LblErrEl"
        Me.LblErrEl.Size = New System.Drawing.Size(68, 22)
        Me.LblErrEl.TabIndex = 15
        Me.LblErrEl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapDB
        '
        Me.LblCapDB.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapDB.Location = New System.Drawing.Point(338, 41)
        Me.LblCapDB.Name = "LblCapDB"
        Me.LblCapDB.Size = New System.Drawing.Size(30, 18)
        Me.LblCapDB.TabIndex = 16
        Me.LblCapDB.Text = "dB"
        Me.LblCapDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtLev
        '
        Me.TxtLev.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtLev.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtLev.Location = New System.Drawing.Point(288, 38)
        Me.TxtLev.Name = "TxtLev"
        Me.TxtLev.Size = New System.Drawing.Size(44, 22)
        Me.TxtLev.TabIndex = 17
        Me.TxtLev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblCapBCNLevel
        '
        Me.LblCapBCNLevel.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapBCNLevel.Location = New System.Drawing.Point(259, 12)
        Me.LblCapBCNLevel.Name = "LblCapBCNLevel"
        Me.LblCapBCNLevel.Size = New System.Drawing.Size(97, 18)
        Me.LblCapBCNLevel.TabIndex = 18
        Me.LblCapBCNLevel.Text = "BCN Level"
        Me.LblCapBCNLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapEL
        '
        Me.LblCapEL.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapEL.Location = New System.Drawing.Point(131, 14)
        Me.LblCapEL.Name = "LblCapEL"
        Me.LblCapEL.Size = New System.Drawing.Size(27, 18)
        Me.LblCapEL.TabIndex = 19
        Me.LblCapEL.Text = "EL"
        Me.LblCapEL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtEl
        '
        Me.TxtEl.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtEl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TxtEl.Location = New System.Drawing.Point(104, 35)
        Me.TxtEl.Name = "TxtEl"
        Me.TxtEl.Size = New System.Drawing.Size(82, 30)
        Me.TxtEl.TabIndex = 20
        Me.TxtEl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtAz
        '
        Me.TxtAz.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtAz.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TxtAz.Location = New System.Drawing.Point(7, 34)
        Me.TxtAz.Name = "TxtAz"
        Me.TxtAz.Size = New System.Drawing.Size(82, 30)
        Me.TxtAz.TabIndex = 21
        Me.TxtAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblCapAZ
        '
        Me.LblCapAZ.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapAZ.Location = New System.Drawing.Point(38, 14)
        Me.LblCapAZ.Name = "LblCapAZ"
        Me.LblCapAZ.Size = New System.Drawing.Size(32, 18)
        Me.LblCapAZ.TabIndex = 22
        Me.LblCapAZ.Text = "AZ"
        Me.LblCapAZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblBeacon
        '
        Me.LblBeacon.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblBeacon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblBeacon.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblBeacon.Location = New System.Drawing.Point(240, 38)
        Me.LblBeacon.Name = "LblBeacon"
        Me.LblBeacon.Size = New System.Drawing.Size(44, 22)
        Me.LblBeacon.TabIndex = 23
        Me.LblBeacon.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblBeacon.Visible = False
        '
        'LblCapLNA
        '
        Me.LblCapLNA.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapLNA.Location = New System.Drawing.Point(394, 12)
        Me.LblCapLNA.Name = "LblCapLNA"
        Me.LblCapLNA.Size = New System.Drawing.Size(39, 18)
        Me.LblCapLNA.TabIndex = 24
        Me.LblCapLNA.Text = "LNA"
        Me.LblCapLNA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapLNA.Visible = False
        '
        'TxtAzErr
        '
        Me.TxtAzErr.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtAzErr.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtAzErr.Location = New System.Drawing.Point(15, 118)
        Me.TxtAzErr.Name = "TxtAzErr"
        Me.TxtAzErr.Size = New System.Drawing.Size(68, 22)
        Me.TxtAzErr.TabIndex = 25
        Me.TxtAzErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAzErr.Visible = False
        '
        'TxtElErr
        '
        Me.TxtElErr.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtElErr.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtElErr.Location = New System.Drawing.Point(111, 120)
        Me.TxtElErr.Name = "TxtElErr"
        Me.TxtElErr.Size = New System.Drawing.Size(68, 22)
        Me.TxtElErr.TabIndex = 26
        Me.TxtElErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtElErr.Visible = False
        '
        'TxtLna
        '
        Me.TxtLna.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtLna.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtLna.Location = New System.Drawing.Point(384, 38)
        Me.TxtLna.Name = "TxtLna"
        Me.TxtLna.Size = New System.Drawing.Size(54, 22)
        Me.TxtLna.TabIndex = 27
        Me.TxtLna.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLna.Visible = False
        '
        'LblCapError2
        '
        Me.LblCapError2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapError2.Location = New System.Drawing.Point(122, 72)
        Me.LblCapError2.Name = "LblCapError2"
        Me.LblCapError2.Size = New System.Drawing.Size(38, 18)
        Me.LblCapError2.TabIndex = 28
        Me.LblCapError2.Text = "Error "
        Me.LblCapError2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapPhaseInc
        '
        Me.LblCapPhaseInc.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapPhaseInc.Location = New System.Drawing.Point(206, 202)
        Me.LblCapPhaseInc.Name = "LblCapPhaseInc"
        Me.LblCapPhaseInc.Size = New System.Drawing.Size(95, 18)
        Me.LblCapPhaseInc.TabIndex = 29
        Me.LblCapPhaseInc.Text = "Phase Inc."
        Me.LblCapPhaseInc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtPhaseEl
        '
        Me.TxtPhaseEl.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtPhaseEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhaseEl.Location = New System.Drawing.Point(122, 173)
        Me.TxtPhaseEl.Name = "TxtPhaseEl"
        Me.TxtPhaseEl.Size = New System.Drawing.Size(42, 22)
        Me.TxtPhaseEl.TabIndex = 30
        Me.TxtPhaseEl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblCapPhaseEl
        '
        Me.LblCapPhaseEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapPhaseEl.Location = New System.Drawing.Point(114, 152)
        Me.LblCapPhaseEl.Name = "LblCapPhaseEl"
        Me.LblCapPhaseEl.Size = New System.Drawing.Size(65, 18)
        Me.LblCapPhaseEl.TabIndex = 31
        Me.LblCapPhaseEl.Text = "PhaseEl"
        Me.LblCapPhaseEl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblRevAz
        '
        Me.LblRevAz.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.LblRevAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblRevAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblRevAz.Location = New System.Drawing.Point(372, 95)
        Me.LblRevAz.Name = "LblRevAz"
        Me.LblRevAz.Size = New System.Drawing.Size(25, 22)
        Me.LblRevAz.TabIndex = 32
        Me.LblRevAz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblCapPhaseAz
        '
        Me.LblCapPhaseAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapPhaseAz.Location = New System.Drawing.Point(18, 152)
        Me.LblCapPhaseAz.Name = "LblCapPhaseAz"
        Me.LblCapPhaseAz.Size = New System.Drawing.Size(74, 18)
        Me.LblCapPhaseAz.TabIndex = 33
        Me.LblCapPhaseAz.Text = "PhaseAz"
        Me.LblCapPhaseAz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPhaseAz.Visible = False
        '
        'TxtPhaseAz
        '
        Me.TxtPhaseAz.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtPhaseAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhaseAz.Location = New System.Drawing.Point(28, 173)
        Me.TxtPhaseAz.Name = "TxtPhaseAz"
        Me.TxtPhaseAz.Size = New System.Drawing.Size(42, 22)
        Me.TxtPhaseAz.TabIndex = 34
        Me.TxtPhaseAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtPhaseAz.Visible = False
        '
        'TxtElGain
        '
        Me.TxtElGain.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtElGain.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtElGain.Location = New System.Drawing.Point(415, 158)
        Me.TxtElGain.Name = "TxtElGain"
        Me.TxtElGain.Size = New System.Drawing.Size(46, 22)
        Me.TxtElGain.TabIndex = 35
        Me.TxtElGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblCapElevation
        '
        Me.LblCapElevation.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapElevation.Location = New System.Drawing.Point(370, 134)
        Me.LblCapElevation.Name = "LblCapElevation"
        Me.LblCapElevation.Size = New System.Drawing.Size(68, 18)
        Me.LblCapElevation.TabIndex = 36
        Me.LblCapElevation.Text = "Elevation"
        Me.LblCapElevation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtAzGain
        '
        Me.TxtAzGain.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtAzGain.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtAzGain.Location = New System.Drawing.Point(228, 158)
        Me.TxtAzGain.Name = "TxtAzGain"
        Me.TxtAzGain.Size = New System.Drawing.Size(46, 22)
        Me.TxtAzGain.TabIndex = 37
        Me.TxtAzGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblCapGain
        '
        Me.LblCapGain.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapGain.Location = New System.Drawing.Point(309, 134)
        Me.LblCapGain.Name = "LblCapGain"
        Me.LblCapGain.Size = New System.Drawing.Size(49, 18)
        Me.LblCapGain.TabIndex = 38
        Me.LblCapGain.Text = "Gain"
        Me.LblCapGain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapAzimuth
        '
        Me.LblCapAzimuth.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapAzimuth.Location = New System.Drawing.Point(240, 134)
        Me.LblCapAzimuth.Name = "LblCapAzimuth"
        Me.LblCapAzimuth.Size = New System.Drawing.Size(63, 18)
        Me.LblCapAzimuth.TabIndex = 39
        Me.LblCapAzimuth.Text = "Azimuth"
        Me.LblCapAzimuth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmdSave
        '
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmdSave.Location = New System.Drawing.Point(676, 508)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.Size = New System.Drawing.Size(54, 27)
        Me.CmdSave.TabIndex = 3
        Me.CmdSave.Text = "SAVE"
        '
        'tmrAlarm
        '
        Me.tmrAlarm.Interval = 30000
        '
        'frmIFR
        '
        Me.frmIFR.Controls.Add(Me.TxtRange)
        Me.frmIFR.Controls.Add(Me.TxtFreq)
        Me.frmIFR.Controls.Add(Me.CmbBandWidth)
        Me.frmIFR.Controls.Add(Me.CmbVideo)
        Me.frmIFR.Controls.Add(Me.CmbAFC)
        Me.frmIFR.Controls.Add(Me.CmbAGC)
        Me.frmIFR.Controls.Add(Me.CmbSelection)
        Me.frmIFR.Controls.Add(Me.CmbLoop)
        Me.frmIFR.Controls.Add(Me.CmbInputPort)
        Me.frmIFR.Controls.Add(Me.CmbOperMode)
        Me.frmIFR.Controls.Add(Me.CmbCarrier)
        Me.frmIFR.Controls.Add(Me.LblCapMs)
        Me.frmIFR.Controls.Add(Me.LblCapMs2)
        Me.frmIFR.Controls.Add(Me.LblCapKHz)
        Me.frmIFR.Controls.Add(Me.LblCapHz)
        Me.frmIFR.Controls.Add(Me.LblCapHz2)
        Me.frmIFR.Controls.Add(Me.LblCapKHz2)
        Me.frmIFR.Controls.Add(Me.LblCapIFBwInAMEFM)
        Me.frmIFR.Controls.Add(Me.LblCapVideoDemodulation)
        Me.frmIFR.Controls.Add(Me.LblCapAFC)
        Me.frmIFR.Controls.Add(Me.LblCapInputPort)
        Me.frmIFR.Controls.Add(Me.LblCapAGCTimeConstant)
        Me.frmIFR.Controls.Add(Me.LblCapInputSelection)
        Me.frmIFR.Controls.Add(Me.LblCapCarrierAcquisition)
        Me.frmIFR.Controls.Add(Me.LblCapLoopFilterBw)
        Me.frmIFR.Controls.Add(Me.LblCapOperatingMode)
        Me.frmIFR.Controls.Add(Me.LblCapInputFrequency)
        Me.frmIFR.Controls.Add(Me.LblCapAcquisitionRange)
        Me.frmIFR.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.frmIFR.Location = New System.Drawing.Point(488, 73)
        Me.frmIFR.Name = "frmIFR"
        Me.frmIFR.Size = New System.Drawing.Size(272, 397)
        Me.frmIFR.TabIndex = 4
        Me.frmIFR.TabStop = False
        Me.frmIFR.Text = "IF Processing"
        '
        'TxtRange
        '
        Me.TxtRange.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtRange.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtRange.Location = New System.Drawing.Point(139, 60)
        Me.TxtRange.Name = "TxtRange"
        Me.TxtRange.Size = New System.Drawing.Size(75, 22)
        Me.TxtRange.TabIndex = 0
        Me.TxtRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtFreq
        '
        Me.TxtFreq.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtFreq.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtFreq.Location = New System.Drawing.Point(139, 29)
        Me.TxtFreq.Name = "TxtFreq"
        Me.TxtFreq.Size = New System.Drawing.Size(75, 22)
        Me.TxtFreq.TabIndex = 1
        Me.TxtFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmbBandWidth
        '
        Me.CmbBandWidth.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbBandWidth.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbBandWidth.Items.AddRange(New Object() {"10000", "1000", "100", "5000", "3000", "1500", "500", "300", "150", "50", "30", "15", "10", "5", "13000", "None"})
        Me.CmbBandWidth.Location = New System.Drawing.Point(139, 358)
        Me.CmbBandWidth.Name = "CmbBandWidth"
        Me.CmbBandWidth.Size = New System.Drawing.Size(80, 24)
        Me.CmbBandWidth.TabIndex = 2
        '
        'CmbVideo
        '
        Me.CmbVideo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbVideo.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbVideo.Items.AddRange(New Object() {"PM", "FM", "Spectrum", "By-pass", "AM Ncohe", "AM Cohe"})
        Me.CmbVideo.Location = New System.Drawing.Point(139, 291)
        Me.CmbVideo.Name = "CmbVideo"
        Me.CmbVideo.Size = New System.Drawing.Size(80, 24)
        Me.CmbVideo.TabIndex = 3
        '
        'CmbAFC
        '
        Me.CmbAFC.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbAFC.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbAFC.Items.AddRange(New Object() {"Off", "10", "100", "1000", "1"})
        Me.CmbAFC.Location = New System.Drawing.Point(139, 325)
        Me.CmbAFC.Name = "CmbAFC"
        Me.CmbAFC.Size = New System.Drawing.Size(80, 24)
        Me.CmbAFC.TabIndex = 4
        '
        'CmbAGC
        '
        Me.CmbAGC.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbAGC.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbAGC.Items.AddRange(New Object() {"1", "10", "100", "1000", "0.1"})
        Me.CmbAGC.Location = New System.Drawing.Point(139, 226)
        Me.CmbAGC.Name = "CmbAGC"
        Me.CmbAGC.Size = New System.Drawing.Size(80, 24)
        Me.CmbAGC.TabIndex = 5
        '
        'CmbSelection
        '
        Me.CmbSelection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbSelection.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbSelection.Items.AddRange(New Object() {"Nominal", "Alternate"})
        Me.CmbSelection.Location = New System.Drawing.Point(139, 258)
        Me.CmbSelection.Name = "CmbSelection"
        Me.CmbSelection.Size = New System.Drawing.Size(80, 24)
        Me.CmbSelection.TabIndex = 6
        '
        'CmbLoop
        '
        Me.CmbLoop.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbLoop.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbLoop.Items.AddRange(New Object() {"100", "300", "1000", "3000", "30", "10"})
        Me.CmbLoop.Location = New System.Drawing.Point(139, 160)
        Me.CmbLoop.Name = "CmbLoop"
        Me.CmbLoop.Size = New System.Drawing.Size(80, 24)
        Me.CmbLoop.TabIndex = 7
        '
        'CmbInputPort
        '
        Me.CmbInputPort.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbInputPort.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbInputPort.Items.AddRange(New Object() {"IF Port 1", "IF Port 2", "IF Port 3", "Auxiliary 1", "Auxiliary 2"})
        Me.CmbInputPort.Location = New System.Drawing.Point(139, 193)
        Me.CmbInputPort.Name = "CmbInputPort"
        Me.CmbInputPort.Size = New System.Drawing.Size(80, 24)
        Me.CmbInputPort.TabIndex = 8
        '
        'CmbOperMode
        '
        Me.CmbOperMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbOperMode.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbOperMode.Items.AddRange(New Object() {"Automatic", "Manual"})
        Me.CmbOperMode.Location = New System.Drawing.Point(139, 94)
        Me.CmbOperMode.Name = "CmbOperMode"
        Me.CmbOperMode.Size = New System.Drawing.Size(80, 24)
        Me.CmbOperMode.TabIndex = 9
        '
        'CmbCarrier
        '
        Me.CmbCarrier.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbCarrier.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbCarrier.Items.AddRange(New Object() {"Normal", "High"})
        Me.CmbCarrier.Location = New System.Drawing.Point(139, 127)
        Me.CmbCarrier.Name = "CmbCarrier"
        Me.CmbCarrier.Size = New System.Drawing.Size(80, 24)
        Me.CmbCarrier.TabIndex = 10
        '
        'LblCapMs
        '
        Me.LblCapMs.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapMs.Location = New System.Drawing.Point(223, 326)
        Me.LblCapMs.Name = "LblCapMs"
        Me.LblCapMs.Size = New System.Drawing.Size(37, 18)
        Me.LblCapMs.TabIndex = 11
        Me.LblCapMs.Text = "ms"
        Me.LblCapMs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapMs2
        '
        Me.LblCapMs2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapMs2.Location = New System.Drawing.Point(223, 226)
        Me.LblCapMs2.Name = "LblCapMs2"
        Me.LblCapMs2.Size = New System.Drawing.Size(37, 18)
        Me.LblCapMs2.TabIndex = 12
        Me.LblCapMs2.Text = "ms"
        Me.LblCapMs2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapKHz
        '
        Me.LblCapKHz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapKHz.Location = New System.Drawing.Point(223, 360)
        Me.LblCapKHz.Name = "LblCapKHz"
        Me.LblCapKHz.Size = New System.Drawing.Size(37, 18)
        Me.LblCapKHz.TabIndex = 13
        Me.LblCapKHz.Text = "KHz"
        Me.LblCapKHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapHz
        '
        Me.LblCapHz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapHz.Location = New System.Drawing.Point(223, 161)
        Me.LblCapHz.Name = "LblCapHz"
        Me.LblCapHz.Size = New System.Drawing.Size(25, 18)
        Me.LblCapHz.TabIndex = 14
        Me.LblCapHz.Text = "Hz"
        Me.LblCapHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapHz2
        '
        Me.LblCapHz2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapHz2.Location = New System.Drawing.Point(223, 62)
        Me.LblCapHz2.Name = "LblCapHz2"
        Me.LblCapHz2.Size = New System.Drawing.Size(37, 18)
        Me.LblCapHz2.TabIndex = 15
        Me.LblCapHz2.Text = "Hz"
        Me.LblCapHz2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapKHz2
        '
        Me.LblCapKHz2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapKHz2.Location = New System.Drawing.Point(223, 29)
        Me.LblCapKHz2.Name = "LblCapKHz2"
        Me.LblCapKHz2.Size = New System.Drawing.Size(37, 18)
        Me.LblCapKHz2.TabIndex = 16
        Me.LblCapKHz2.Text = "KHz"
        Me.LblCapKHz2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapIFBwInAMEFM
        '
        Me.LblCapIFBwInAMEFM.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapIFBwInAMEFM.Location = New System.Drawing.Point(10, 362)
        Me.LblCapIFBwInAMEFM.Name = "LblCapIFBwInAMEFM"
        Me.LblCapIFBwInAMEFM.Size = New System.Drawing.Size(116, 18)
        Me.LblCapIFBwInAMEFM.TabIndex = 17
        Me.LblCapIFBwInAMEFM.Text = "IF Bw in AM and FM"
        Me.LblCapIFBwInAMEFM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapVideoDemodulation
        '
        Me.LblCapVideoDemodulation.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapVideoDemodulation.Location = New System.Drawing.Point(10, 295)
        Me.LblCapVideoDemodulation.Name = "LblCapVideoDemodulation"
        Me.LblCapVideoDemodulation.Size = New System.Drawing.Size(116, 18)
        Me.LblCapVideoDemodulation.TabIndex = 18
        Me.LblCapVideoDemodulation.Text = "Video Demodulation"
        Me.LblCapVideoDemodulation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapAFC
        '
        Me.LblCapAFC.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapAFC.Location = New System.Drawing.Point(10, 329)
        Me.LblCapAFC.Name = "LblCapAFC"
        Me.LblCapAFC.Size = New System.Drawing.Size(116, 18)
        Me.LblCapAFC.TabIndex = 19
        Me.LblCapAFC.Text = "AFC"
        Me.LblCapAFC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapInputPort
        '
        Me.LblCapInputPort.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapInputPort.Location = New System.Drawing.Point(10, 262)
        Me.LblCapInputPort.Name = "LblCapInputPort"
        Me.LblCapInputPort.Size = New System.Drawing.Size(116, 18)
        Me.LblCapInputPort.TabIndex = 20
        Me.LblCapInputPort.Text = "Input Port"
        Me.LblCapInputPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapAGCTimeConstant
        '
        Me.LblCapAGCTimeConstant.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapAGCTimeConstant.Location = New System.Drawing.Point(10, 229)
        Me.LblCapAGCTimeConstant.Name = "LblCapAGCTimeConstant"
        Me.LblCapAGCTimeConstant.Size = New System.Drawing.Size(116, 18)
        Me.LblCapAGCTimeConstant.TabIndex = 21
        Me.LblCapAGCTimeConstant.Text = "AGC Time Constant"
        Me.LblCapAGCTimeConstant.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapInputSelection
        '
        Me.LblCapInputSelection.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapInputSelection.Location = New System.Drawing.Point(10, 195)
        Me.LblCapInputSelection.Name = "LblCapInputSelection"
        Me.LblCapInputSelection.Size = New System.Drawing.Size(116, 18)
        Me.LblCapInputSelection.TabIndex = 22
        Me.LblCapInputSelection.Text = "Input Selection"
        Me.LblCapInputSelection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapCarrierAcquisition
        '
        Me.LblCapCarrierAcquisition.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapCarrierAcquisition.Location = New System.Drawing.Point(10, 129)
        Me.LblCapCarrierAcquisition.Name = "LblCapCarrierAcquisition"
        Me.LblCapCarrierAcquisition.Size = New System.Drawing.Size(116, 18)
        Me.LblCapCarrierAcquisition.TabIndex = 23
        Me.LblCapCarrierAcquisition.Text = "Carrier Acquisition"
        Me.LblCapCarrierAcquisition.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapLoopFilterBw
        '
        Me.LblCapLoopFilterBw.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapLoopFilterBw.Location = New System.Drawing.Point(10, 162)
        Me.LblCapLoopFilterBw.Name = "LblCapLoopFilterBw"
        Me.LblCapLoopFilterBw.Size = New System.Drawing.Size(116, 18)
        Me.LblCapLoopFilterBw.TabIndex = 24
        Me.LblCapLoopFilterBw.Text = "Loop Filter Bw"
        Me.LblCapLoopFilterBw.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapOperatingMode
        '
        Me.LblCapOperatingMode.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapOperatingMode.Location = New System.Drawing.Point(10, 95)
        Me.LblCapOperatingMode.Name = "LblCapOperatingMode"
        Me.LblCapOperatingMode.Size = New System.Drawing.Size(116, 18)
        Me.LblCapOperatingMode.TabIndex = 25
        Me.LblCapOperatingMode.Text = "Operating Mode"
        Me.LblCapOperatingMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapInputFrequency
        '
        Me.LblCapInputFrequency.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapInputFrequency.Location = New System.Drawing.Point(7, 31)
        Me.LblCapInputFrequency.Name = "LblCapInputFrequency"
        Me.LblCapInputFrequency.Size = New System.Drawing.Size(116, 18)
        Me.LblCapInputFrequency.TabIndex = 26
        Me.LblCapInputFrequency.Text = "Input Frequency"
        Me.LblCapInputFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCapAcquisitionRange
        '
        Me.LblCapAcquisitionRange.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapAcquisitionRange.Location = New System.Drawing.Point(10, 62)
        Me.LblCapAcquisitionRange.Name = "LblCapAcquisitionRange"
        Me.LblCapAcquisitionRange.Size = New System.Drawing.Size(116, 18)
        Me.LblCapAcquisitionRange.TabIndex = 27
        Me.LblCapAcquisitionRange.Text = "Acquisition Range"
        Me.LblCapAcquisitionRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtFreccia
        '
        Me.TxtFreccia.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtFreccia.Location = New System.Drawing.Point(266, 562)
        Me.TxtFreccia.Name = "TxtFreccia"
        Me.TxtFreccia.Size = New System.Drawing.Size(9, 22)
        Me.TxtFreccia.TabIndex = 5
        '
        'frmManual
        '
        Me.frmManual.Controls.Add(Me.CmbEl)
        Me.frmManual.Controls.Add(Me.CmdAnnulla)
        Me.frmManual.Controls.Add(Me.CmdUp)
        Me.frmManual.Controls.Add(Me.CmdCCW)
        Me.frmManual.Controls.Add(Me.CmdCW)
        Me.frmManual.Controls.Add(Me.CmdDown)
        Me.frmManual.Controls.Add(Me.Image1)
        Me.frmManual.Controls.Add(Me.LblTesto)
        Me.frmManual.Controls.Add(Me.LblCapStepSize)
        Me.frmManual.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.frmManual.Location = New System.Drawing.Point(2, 397)
        Me.frmManual.Name = "frmManual"
        Me.frmManual.Size = New System.Drawing.Size(482, 121)
        Me.frmManual.TabIndex = 6
        Me.frmManual.TabStop = False
        Me.frmManual.Text = "Step-Preset"
        Me.frmManual.Visible = False
        '
        'CmbEl
        '
        Me.CmbEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmbEl.Items.AddRange(New Object() {"0.01", "0.05", "0.10", "0.50", "0.90"})
        Me.CmbEl.Location = New System.Drawing.Point(223, 22)
        Me.CmbEl.Name = "CmbEl"
        Me.CmbEl.Size = New System.Drawing.Size(63, 24)
        Me.CmbEl.TabIndex = 0
        '
        'CmdAnnulla
        '
        Me.CmdAnnulla.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmdAnnulla.Location = New System.Drawing.Point(317, 17)
        Me.CmdAnnulla.Name = "CmdAnnulla"
        Me.CmdAnnulla.Size = New System.Drawing.Size(54, 27)
        Me.CmdAnnulla.TabIndex = 1
        Me.CmdAnnulla.Text = "EXIT"
        '
        'CmdUp
        '
        Me.CmdUp.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmdUp.Location = New System.Drawing.Point(72, 17)
        Me.CmdUp.Name = "CmdUp"
        Me.CmdUp.Size = New System.Drawing.Size(39, 27)
        Me.CmdUp.TabIndex = 2
        Me.CmdUp.Text = "UP"
        '
        'CmdCCW
        '
        Me.CmdCCW.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmdCCW.Location = New System.Drawing.Point(16, 50)
        Me.CmdCCW.Name = "CmdCCW"
        Me.CmdCCW.Size = New System.Drawing.Size(45, 27)
        Me.CmdCCW.TabIndex = 3
        Me.CmdCCW.Text = "CCW"
        '
        'CmdCW
        '
        Me.CmdCW.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmdCW.Location = New System.Drawing.Point(116, 50)
        Me.CmdCW.Name = "CmdCW"
        Me.CmdCW.Size = New System.Drawing.Size(53, 27)
        Me.CmdCW.TabIndex = 4
        Me.CmdCW.Text = "CW"
        '
        'CmdDown
        '
        Me.CmdDown.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.CmdDown.Location = New System.Drawing.Point(58, 88)
        Me.CmdDown.Name = "CmdDown"
        Me.CmdDown.Size = New System.Drawing.Size(66, 27)
        Me.CmdDown.TabIndex = 5
        Me.CmdDown.Text = "DOWN"
        '
        'Image1
        '
        Me.Image1.Image = CType(resources.GetObject("Image1.Image"), System.Drawing.Image)
        Me.Image1.Location = New System.Drawing.Point(73, 46)
        Me.Image1.Name = "Image1"
        Me.Image1.Size = New System.Drawing.Size(35, 34)
        Me.Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Image1.TabIndex = 6
        Me.Image1.TabStop = False
        '
        'LblTesto
        '
        Me.LblTesto.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LblTesto.Location = New System.Drawing.Point(163, 79)
        Me.LblTesto.Name = "LblTesto"
        Me.LblTesto.Size = New System.Drawing.Size(306, 27)
        Me.LblTesto.TabIndex = 7
        Me.LblTesto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapStepSize
        '
        Me.LblCapStepSize.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapStepSize.Location = New System.Drawing.Point(166, 24)
        Me.LblCapStepSize.Name = "LblCapStepSize"
        Me.LblCapStepSize.Size = New System.Drawing.Size(51, 18)
        Me.LblCapStepSize.TabIndex = 8
        Me.LblCapStepSize.Text = "Step Size"
        Me.LblCapStepSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Frame1
        '
        Me.Frame1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Frame1.Controls.Add(Me.LblCapPolErr)
        Me.Frame1.Controls.Add(Me.TxtPol2)
        Me.Frame1.Controls.Add(Me.TxtPol1)
        Me.Frame1.Controls.Add(Me.LblPol)
        Me.Frame1.Controls.Add(Me.TxtFrequency)
        Me.Frame1.Controls.Add(Me.TxtInpFrequency)
        Me.Frame1.Controls.Add(Me.TxtPhasingMode)
        Me.Frame1.Controls.Add(Me.TxtPol)
        Me.Frame1.Controls.Add(Me.TxtPolarizationAngle)
        Me.Frame1.Controls.Add(Me.TxtPhasingFlow)
        Me.Frame1.Controls.Add(Me.LblCapReceivingMode)
        Me.Frame1.Controls.Add(Me.LblCapRFInputFrequencyKHz)
        Me.Frame1.Controls.Add(Me.LblCapPolarizationAngleDeg)
        Me.Frame1.Controls.Add(Me.LblCapPhasingFlowIdHex)
        Me.Frame1.Location = New System.Drawing.Point(4, 63)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Size = New System.Drawing.Size(478, 90)
        Me.Frame1.TabIndex = 7
        '
        'LblCapPolErr
        '
        Me.LblCapPolErr.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapPolErr.Location = New System.Drawing.Point(10, 38)
        Me.LblCapPolErr.Name = "LblCapPolErr"
        Me.LblCapPolErr.Size = New System.Drawing.Size(44, 18)
        Me.LblCapPolErr.TabIndex = 0
        Me.LblCapPolErr.Text = "Pol.Err."
        Me.LblCapPolErr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPolErr.Visible = False
        '
        'TxtPol2
        '
        Me.TxtPol2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtPol2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPol2.Location = New System.Drawing.Point(67, 36)
        Me.TxtPol2.Name = "TxtPol2"
        Me.TxtPol2.Size = New System.Drawing.Size(78, 22)
        Me.TxtPol2.TabIndex = 1
        Me.TxtPol2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPol2.Visible = False
        '
        'TxtPol1
        '
        Me.TxtPol1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtPol1.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPol1.Location = New System.Drawing.Point(66, 60)
        Me.TxtPol1.Name = "TxtPol1"
        Me.TxtPol1.Size = New System.Drawing.Size(78, 22)
        Me.TxtPol1.TabIndex = 2
        Me.TxtPol1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPol1.Visible = False
        '
        'LblPol
        '
        Me.LblPol.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblPol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblPol.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblPol.Location = New System.Drawing.Point(151, 36)
        Me.LblPol.Name = "LblPol"
        Me.LblPol.Size = New System.Drawing.Size(85, 20)
        Me.LblPol.TabIndex = 3
        Me.LblPol.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblPol.Visible = False
        '
        'TxtFrequency
        '
        Me.TxtFrequency.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtFrequency.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtFrequency.Location = New System.Drawing.Point(394, 58)
        Me.TxtFrequency.Name = "TxtFrequency"
        Me.TxtFrequency.Size = New System.Drawing.Size(80, 22)
        Me.TxtFrequency.TabIndex = 4
        Me.TxtFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFrequency.Visible = False
        '
        'TxtInpFrequency
        '
        Me.TxtInpFrequency.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtInpFrequency.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtInpFrequency.Location = New System.Drawing.Point(394, 34)
        Me.TxtInpFrequency.Name = "TxtInpFrequency"
        Me.TxtInpFrequency.Size = New System.Drawing.Size(80, 22)
        Me.TxtInpFrequency.TabIndex = 5
        Me.TxtInpFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtPhasingMode
        '
        Me.TxtPhasingMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtPhasingMode.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhasingMode.Location = New System.Drawing.Point(394, 10)
        Me.TxtPhasingMode.Name = "TxtPhasingMode"
        Me.TxtPhasingMode.Size = New System.Drawing.Size(80, 22)
        Me.TxtPhasingMode.TabIndex = 6
        Me.TxtPhasingMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtPol
        '
        Me.TxtPol.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TxtPol.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPol.Location = New System.Drawing.Point(151, 60)
        Me.TxtPol.Name = "TxtPol"
        Me.TxtPol.Size = New System.Drawing.Size(85, 22)
        Me.TxtPol.TabIndex = 7
        Me.TxtPol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPol.Visible = False
        '
        'TxtPolarizationAngle
        '
        Me.TxtPolarizationAngle.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtPolarizationAngle.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPolarizationAngle.Location = New System.Drawing.Point(151, 10)
        Me.TxtPolarizationAngle.Name = "TxtPolarizationAngle"
        Me.TxtPolarizationAngle.Size = New System.Drawing.Size(85, 22)
        Me.TxtPolarizationAngle.TabIndex = 8
        Me.TxtPolarizationAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPolarizationAngle.Visible = False
        '
        'TxtPhasingFlow
        '
        Me.TxtPhasingFlow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtPhasingFlow.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhasingFlow.Location = New System.Drawing.Point(166, 72)
        Me.TxtPhasingFlow.Name = "TxtPhasingFlow"
        Me.TxtPhasingFlow.Size = New System.Drawing.Size(63, 22)
        Me.TxtPhasingFlow.TabIndex = 9
        Me.TxtPhasingFlow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPhasingFlow.Visible = False
        '
        'LblCapReceivingMode
        '
        Me.LblCapReceivingMode.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapReceivingMode.Location = New System.Drawing.Point(245, 14)
        Me.LblCapReceivingMode.Name = "LblCapReceivingMode"
        Me.LblCapReceivingMode.Size = New System.Drawing.Size(116, 18)
        Me.LblCapReceivingMode.TabIndex = 10
        Me.LblCapReceivingMode.Text = "Receiving Mode"
        Me.LblCapReceivingMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapRFInputFrequencyKHz
        '
        Me.LblCapRFInputFrequencyKHz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapRFInputFrequencyKHz.Location = New System.Drawing.Point(245, 36)
        Me.LblCapRFInputFrequencyKHz.Name = "LblCapRFInputFrequencyKHz"
        Me.LblCapRFInputFrequencyKHz.Size = New System.Drawing.Size(147, 18)
        Me.LblCapRFInputFrequencyKHz.TabIndex = 11
        Me.LblCapRFInputFrequencyKHz.Text = "RF Input Frequency (kHz)"
        Me.LblCapRFInputFrequencyKHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapPolarizationAngleDeg
        '
        Me.LblCapPolarizationAngleDeg.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapPolarizationAngleDeg.Location = New System.Drawing.Point(5, 14)
        Me.LblCapPolarizationAngleDeg.Name = "LblCapPolarizationAngleDeg"
        Me.LblCapPolarizationAngleDeg.Size = New System.Drawing.Size(140, 20)
        Me.LblCapPolarizationAngleDeg.TabIndex = 12
        Me.LblCapPolarizationAngleDeg.Text = "Polarization Angle (deg)"
        Me.LblCapPolarizationAngleDeg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPolarizationAngleDeg.Visible = False
        '
        'LblCapPhasingFlowIdHex
        '
        Me.LblCapPhasingFlowIdHex.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapPhasingFlowIdHex.Location = New System.Drawing.Point(10, 74)
        Me.LblCapPhasingFlowIdHex.Name = "LblCapPhasingFlowIdHex"
        Me.LblCapPhasingFlowIdHex.Size = New System.Drawing.Size(135, 18)
        Me.LblCapPhasingFlowIdHex.TabIndex = 13
        Me.LblCapPhasingFlowIdHex.Text = "Phasing Flow Id. (hex.)"
        Me.LblCapPhasingFlowIdHex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPhasingFlowIdHex.Visible = False
        '
        'LblCapOffsetAzimuth
        '
        Me.LblCapOffsetAzimuth.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapOffsetAzimuth.Location = New System.Drawing.Point(482, 479)
        Me.LblCapOffsetAzimuth.Name = "LblCapOffsetAzimuth"
        Me.LblCapOffsetAzimuth.Size = New System.Drawing.Size(80, 18)
        Me.LblCapOffsetAzimuth.TabIndex = 8
        Me.LblCapOffsetAzimuth.Text = "Offset Azimuth"
        Me.LblCapOffsetAzimuth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapElevation2
        '
        Me.LblCapElevation2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapElevation2.Location = New System.Drawing.Point(604, 481)
        Me.LblCapElevation2.Name = "LblCapElevation2"
        Me.LblCapElevation2.Size = New System.Drawing.Size(74, 18)
        Me.LblCapElevation2.TabIndex = 9
        Me.LblCapElevation2.Text = "Elevation"
        Me.LblCapElevation2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblWarning
        '
        Me.LblWarning.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblWarning.Location = New System.Drawing.Point(490, 490)
        Me.LblWarning.Name = "LblWarning"
        Me.LblWarning.Size = New System.Drawing.Size(171, 27)
        Me.LblWarning.TabIndex = 10
        Me.LblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapRXValue
        '
        Me.LblCapRXValue.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapRXValue.Location = New System.Drawing.Point(331, 524)
        Me.LblCapRXValue.Name = "LblCapRXValue"
        Me.LblCapRXValue.Size = New System.Drawing.Size(92, 20)
        Me.LblCapRXValue.TabIndex = 11
        Me.LblCapRXValue.Text = "RX Value"
        Me.LblCapRXValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapACUValue
        '
        Me.LblCapACUValue.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapACUValue.Location = New System.Drawing.Point(19, 524)
        Me.LblCapACUValue.Name = "LblCapACUValue"
        Me.LblCapACUValue.Size = New System.Drawing.Size(92, 20)
        Me.LblCapACUValue.TabIndex = 12
        Me.LblCapACUValue.Text = "ACU Value"
        Me.LblCapACUValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblRxValueBox
        '
        Me.LblRxValueBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.LblRxValueBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblRxValueBox.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblRxValueBox.Location = New System.Drawing.Point(441, 524)
        Me.LblRxValueBox.Name = "LblRxValueBox"
        Me.LblRxValueBox.Size = New System.Drawing.Size(25, 20)
        Me.LblRxValueBox.TabIndex = 13
        Me.LblRxValueBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblAcuValueBox
        '
        Me.LblAcuValueBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblAcuValueBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblAcuValueBox.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LblAcuValueBox.Location = New System.Drawing.Point(129, 524)
        Me.LblAcuValueBox.Name = "LblAcuValueBox"
        Me.LblAcuValueBox.Size = New System.Drawing.Size(22, 20)
        Me.LblAcuValueBox.TabIndex = 14
        Me.LblAcuValueBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtValid
        '
        Me.TxtValid.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtValid.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtValid.Location = New System.Drawing.Point(531, 33)
        Me.TxtValid.Name = "TxtValid"
        Me.TxtValid.Size = New System.Drawing.Size(80, 22)
        Me.TxtValid.TabIndex = 15
        '
        'TxtProcess
        '
        Me.TxtProcess.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtProcess.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtProcess.Location = New System.Drawing.Point(332, 33)
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(80, 22)
        Me.TxtProcess.TabIndex = 16
        '
        'LblPhase
        '
        Me.LblPhase.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblPhase.Location = New System.Drawing.Point(439, 5)
        Me.LblPhase.Name = "LblPhase"
        Me.LblPhase.Size = New System.Drawing.Size(46, 18)
        Me.LblPhase.TabIndex = 17
        Me.LblPhase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblPhase.Visible = False
        '
        'LblCapSATELLITE
        '
        Me.LblCapSATELLITE.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapSATELLITE.Location = New System.Drawing.Point(10, 35)
        Me.LblCapSATELLITE.Name = "LblCapSATELLITE"
        Me.LblCapSATELLITE.Size = New System.Drawing.Size(75, 18)
        Me.LblCapSATELLITE.TabIndex = 18
        Me.LblCapSATELLITE.Text = "SATELLITE:"
        Me.LblCapSATELLITE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblSatellite
        '
        Me.LblSatellite.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSatellite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblSatellite.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblSatellite.Location = New System.Drawing.Point(89, 33)
        Me.LblSatellite.Name = "LblSatellite"
        Me.LblSatellite.Size = New System.Drawing.Size(174, 22)
        Me.LblSatellite.TabIndex = 19
        Me.LblSatellite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblCapProcess
        '
        Me.LblCapProcess.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapProcess.Location = New System.Drawing.Point(272, 35)
        Me.LblCapProcess.Name = "LblCapProcess"
        Me.LblCapProcess.Size = New System.Drawing.Size(61, 18)
        Me.LblCapProcess.TabIndex = 20
        Me.LblCapProcess.Text = "Process"
        Me.LblCapProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCapValid
        '
        Me.LblCapValid.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblCapValid.Location = New System.Drawing.Point(482, 35)
        Me.LblCapValid.Name = "LblCapValid"
        Me.LblCapValid.Size = New System.Drawing.Size(43, 18)
        Me.LblCapValid.TabIndex = 21
        Me.LblCapValid.Text = "Valid"
        Me.LblCapValid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblInputRx
        '
        Me.LblInputRx.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblInputRx.Location = New System.Drawing.Point(502, 7)
        Me.LblInputRx.Name = "LblInputRx"
        Me.LblInputRx.Size = New System.Drawing.Size(32, 15)
        Me.LblInputRx.TabIndex = 22
        Me.LblInputRx.Text = "Label17"
        Me.LblInputRx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblInputRx.Visible = False
        '
        'FrmRicevitore
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(760, 561)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtOffsetAz)
        Me.Controls.Add(Me.TxtOffsetEl)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.CmdSave)
        Me.Controls.Add(Me.frmIFR)
        Me.Controls.Add(Me.TxtFreccia)
        Me.Controls.Add(Me.frmManual)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.LblCapOffsetAzimuth)
        Me.Controls.Add(Me.LblCapElevation2)
        Me.Controls.Add(Me.LblWarning)
        Me.Controls.Add(Me.LblCapRXValue)
        Me.Controls.Add(Me.LblCapACUValue)
        Me.Controls.Add(Me.LblRxValueBox)
        Me.Controls.Add(Me.LblAcuValueBox)
        Me.Controls.Add(Me.TxtValid)
        Me.Controls.Add(Me.TxtProcess)
        Me.Controls.Add(Me.LblPhase)
        Me.Controls.Add(Me.LblCapSATELLITE)
        Me.Controls.Add(Me.LblSatellite)
        Me.Controls.Add(Me.LblCapProcess)
        Me.Controls.Add(Me.LblCapValid)
        Me.Controls.Add(Me.LblInputRx)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FrmRicevitore"
        Me.Text = "TDR - Phasing Mode"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.frmIFR.ResumeLayout(False)
        Me.frmIFR.PerformLayout()
        Me.frmManual.ResumeLayout(False)
        CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPhase As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAugmented As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAutomatic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuManual As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TxtOffsetAz As System.Windows.Forms.TextBox
    Friend WithEvents TxtOffsetEl As System.Windows.Forms.TextBox
    Friend WithEvents tmrPhasing As System.Windows.Forms.Timer
    Friend WithEvents Frame2 As System.Windows.Forms.Panel
    Friend WithEvents CmdPiu As System.Windows.Forms.Button
    Friend WithEvents CmdMeno As System.Windows.Forms.Button
    Friend WithEvents TxtPhase As System.Windows.Forms.TextBox
    Friend WithEvents TxtPhAz As System.Windows.Forms.TextBox
    Friend WithEvents CmdMenoAz As System.Windows.Forms.Button
    Friend WithEvents CmdPiuAz As System.Windows.Forms.Button
    Friend WithEvents TxtElGain1 As System.Windows.Forms.TextBox
    Friend WithEvents TxtAzGain1 As System.Windows.Forms.TextBox
    Friend WithEvents CmbRevAz As System.Windows.Forms.ComboBox
    Friend WithEvents CmbInc As System.Windows.Forms.ComboBox
    Friend WithEvents LblCapRevAzError As System.Windows.Forms.Label
    Friend WithEvents LblLock As System.Windows.Forms.Label
    Friend WithEvents LblCapRXStatus As System.Windows.Forms.Label
    Friend WithEvents LblCapError As System.Windows.Forms.Label
    Friend WithEvents LblErrAz As System.Windows.Forms.Label
    Friend WithEvents LblErrEl As System.Windows.Forms.Label
    Friend WithEvents LblCapDB As System.Windows.Forms.Label
    Friend WithEvents TxtLev As System.Windows.Forms.TextBox
    Friend WithEvents LblCapBCNLevel As System.Windows.Forms.Label
    Friend WithEvents LblCapEL As System.Windows.Forms.Label
    Friend WithEvents TxtEl As System.Windows.Forms.TextBox
    Friend WithEvents TxtAz As System.Windows.Forms.TextBox
    Friend WithEvents LblCapAZ As System.Windows.Forms.Label
    Friend WithEvents LblBeacon As System.Windows.Forms.Label
    Friend WithEvents LblCapLNA As System.Windows.Forms.Label
    Friend WithEvents TxtAzErr As System.Windows.Forms.TextBox
    Friend WithEvents TxtElErr As System.Windows.Forms.TextBox
    Friend WithEvents TxtLna As System.Windows.Forms.TextBox
    Friend WithEvents LblCapError2 As System.Windows.Forms.Label
    Friend WithEvents LblCapPhaseInc As System.Windows.Forms.Label
    Friend WithEvents TxtPhaseEl As System.Windows.Forms.TextBox
    Friend WithEvents LblCapPhaseEl As System.Windows.Forms.Label
    Friend WithEvents LblRevAz As System.Windows.Forms.Label
    Friend WithEvents LblCapPhaseAz As System.Windows.Forms.Label
    Friend WithEvents TxtPhaseAz As System.Windows.Forms.TextBox
    Friend WithEvents TxtElGain As System.Windows.Forms.TextBox
    Friend WithEvents LblCapElevation As System.Windows.Forms.Label
    Friend WithEvents TxtAzGain As System.Windows.Forms.TextBox
    Friend WithEvents LblCapGain As System.Windows.Forms.Label
    Friend WithEvents LblCapAzimuth As System.Windows.Forms.Label
    Friend WithEvents CmdSave As System.Windows.Forms.Button
    Friend WithEvents tmrAlarm As System.Windows.Forms.Timer
    Friend WithEvents frmIFR As System.Windows.Forms.GroupBox
    Friend WithEvents TxtRange As System.Windows.Forms.TextBox
    Friend WithEvents TxtFreq As System.Windows.Forms.TextBox
    Friend WithEvents CmbBandWidth As System.Windows.Forms.ComboBox
    Friend WithEvents CmbVideo As System.Windows.Forms.ComboBox
    Friend WithEvents CmbAFC As System.Windows.Forms.ComboBox
    Friend WithEvents CmbAGC As System.Windows.Forms.ComboBox
    Friend WithEvents CmbSelection As System.Windows.Forms.ComboBox
    Friend WithEvents CmbLoop As System.Windows.Forms.ComboBox
    Friend WithEvents CmbInputPort As System.Windows.Forms.ComboBox
    Friend WithEvents CmbOperMode As System.Windows.Forms.ComboBox
    Friend WithEvents CmbCarrier As System.Windows.Forms.ComboBox
    Friend WithEvents LblCapMs As System.Windows.Forms.Label
    Friend WithEvents LblCapMs2 As System.Windows.Forms.Label
    Friend WithEvents LblCapKHz As System.Windows.Forms.Label
    Friend WithEvents LblCapHz As System.Windows.Forms.Label
    Friend WithEvents LblCapHz2 As System.Windows.Forms.Label
    Friend WithEvents LblCapKHz2 As System.Windows.Forms.Label
    Friend WithEvents LblCapIFBwInAMEFM As System.Windows.Forms.Label
    Friend WithEvents LblCapVideoDemodulation As System.Windows.Forms.Label
    Friend WithEvents LblCapAFC As System.Windows.Forms.Label
    Friend WithEvents LblCapInputPort As System.Windows.Forms.Label
    Friend WithEvents LblCapAGCTimeConstant As System.Windows.Forms.Label
    Friend WithEvents LblCapInputSelection As System.Windows.Forms.Label
    Friend WithEvents LblCapCarrierAcquisition As System.Windows.Forms.Label
    Friend WithEvents LblCapLoopFilterBw As System.Windows.Forms.Label
    Friend WithEvents LblCapOperatingMode As System.Windows.Forms.Label
    Friend WithEvents LblCapInputFrequency As System.Windows.Forms.Label
    Friend WithEvents LblCapAcquisitionRange As System.Windows.Forms.Label
    Friend WithEvents TxtFreccia As System.Windows.Forms.TextBox
    Friend WithEvents frmManual As System.Windows.Forms.GroupBox
    Friend WithEvents CmbEl As System.Windows.Forms.ComboBox
    Friend WithEvents CmdAnnulla As System.Windows.Forms.Button
    Friend WithEvents CmdUp As System.Windows.Forms.Button
    Friend WithEvents CmdCCW As System.Windows.Forms.Button
    Friend WithEvents CmdCW As System.Windows.Forms.Button
    Friend WithEvents CmdDown As System.Windows.Forms.Button
    Friend WithEvents Image1 As System.Windows.Forms.PictureBox
    Friend WithEvents LblTesto As System.Windows.Forms.Label
    Friend WithEvents LblCapStepSize As System.Windows.Forms.Label
    Friend WithEvents Frame1 As System.Windows.Forms.Panel
    Friend WithEvents LblCapPolErr As System.Windows.Forms.Label
    Friend WithEvents TxtPol2 As System.Windows.Forms.TextBox
    Friend WithEvents TxtPol1 As System.Windows.Forms.TextBox
    Friend WithEvents LblPol As System.Windows.Forms.Label
    Friend WithEvents TxtFrequency As System.Windows.Forms.TextBox
    Friend WithEvents TxtInpFrequency As System.Windows.Forms.TextBox
    Friend WithEvents TxtPhasingMode As System.Windows.Forms.TextBox
    Friend WithEvents TxtPol As System.Windows.Forms.TextBox
    Friend WithEvents TxtPolarizationAngle As System.Windows.Forms.TextBox
    Friend WithEvents TxtPhasingFlow As System.Windows.Forms.TextBox
    Friend WithEvents LblCapReceivingMode As System.Windows.Forms.Label
    Friend WithEvents LblCapRFInputFrequencyKHz As System.Windows.Forms.Label
    Friend WithEvents LblCapPolarizationAngleDeg As System.Windows.Forms.Label
    Friend WithEvents LblCapPhasingFlowIdHex As System.Windows.Forms.Label
    Friend WithEvents LblCapOffsetAzimuth As System.Windows.Forms.Label
    Friend WithEvents LblCapElevation2 As System.Windows.Forms.Label
    Friend WithEvents LblWarning As System.Windows.Forms.Label
    Friend WithEvents LblCapRXValue As System.Windows.Forms.Label
    Friend WithEvents LblCapACUValue As System.Windows.Forms.Label
    Friend WithEvents LblRxValueBox As System.Windows.Forms.Label
    Friend WithEvents LblAcuValueBox As System.Windows.Forms.Label
    Friend WithEvents TxtValid As System.Windows.Forms.TextBox
    Friend WithEvents TxtProcess As System.Windows.Forms.TextBox
    Friend WithEvents LblPhase As System.Windows.Forms.Label
    Friend WithEvents LblCapSATELLITE As System.Windows.Forms.Label
    Friend WithEvents LblSatellite As System.Windows.Forms.Label
    Friend WithEvents LblCapProcess As System.Windows.Forms.Label
    Friend WithEvents LblCapValid As System.Windows.Forms.Label
    Friend WithEvents LblInputRx As System.Windows.Forms.Label


End Class
