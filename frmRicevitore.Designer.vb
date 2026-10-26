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
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPhase = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAugmented = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAutomatic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuManual = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.TxtOffsetAz = New System.Windows.Forms.TextBox()
        Me.TxtOffsetEl = New System.Windows.Forms.TextBox()
        Me.tmrPhasing = New System.Windows.Forms.Timer()
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
        Me.tmrAlarm = New System.Windows.Forms.Timer()
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


        ' Liste dedotte dalla tabella di decodifica reale in AggiornaDatiRX
        ' (Select Case su i_OpMode/i_Carrier/i_LoopFilter/i_AGC/i_Video/i_Input/i_AFC/i_IFBand),
        ' non dal file binario FrmRicevitore.frx - stessi valori che l'apparato puo' restituire.
        Me.CmbOperMode.Items.AddRange(New Object() {"Automatic", "Manual"})
        Me.CmbCarrier.Items.AddRange(New Object() {"Normal", "High"})
        Me.CmbSelection.Items.AddRange(New Object() {"Nominal", "Alternate"})
        Me.CmbInputPort.Items.AddRange(New Object() {"IF Port 1", "IF Port 2", "IF Port 3", "Auxiliary 1", "Auxiliary 2"})
        Me.CmbVideo.Items.AddRange(New Object() {"PM", "FM", "Spectrum", "By-pass", "AM Ncohe", "AM Cohe"})
        Me.CmbLoop.Items.AddRange(New Object() {"100", "300", "1000", "3000", "30", "10"})
        Me.CmbAGC.Items.AddRange(New Object() {"1", "10", "100", "1000", "0.1"})
        Me.CmbAFC.Items.AddRange(New Object() {"Off", "10", "100", "1000", "1"})
        Me.CmbBandWidth.Items.AddRange(New Object() {"10000", "1000", "100", "5000", "3000", "1500", "500", "300", "150", "50", "30", "15", "10", "5", "13000", "None"})
        Me.CmbEl.Items.AddRange(New Object() {"0.01", "0.05", "0.10", "0.50", "0.90"})
        Me.CmbRevAz.Items.AddRange(New Object() {"Y", "N"})
        Me.CmbInc.Items.AddRange(New Object() {"1", "2", "5", "10"})
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuExit, Me.MnuPhase})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(760, 24)
        '
        'MnuExit
        '
        Me.MnuExit.Name = "MnuExit"
        Me.MnuExit.Text = "Exit"
        '
        'MnuPhase
        '
        Me.MnuPhase.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuAugmented, Me.MnuAutomatic, Me.MnuManual})
        Me.MnuPhase.Name = "MnuPhase"
        Me.MnuPhase.Text = "Phase"
        '
        'MnuAugmented
        '
        Me.MnuAugmented.Name = "MnuAugmented"
        Me.MnuAugmented.Text = "Augmented"
        '
        'MnuAutomatic
        '
        Me.MnuAutomatic.Name = "MnuAutomatic"
        Me.MnuAutomatic.Text = "Automatic"
        '
        'MnuManual
        '
        Me.MnuManual.Name = "MnuManual"
        Me.MnuManual.Text = "Manual"
        '
        'TxtOffsetAz
        '
        Me.TxtOffsetAz.Location = New System.Drawing.Point(571, 458)
        Me.TxtOffsetAz.Size = New System.Drawing.Size(49, 22)
        Me.TxtOffsetAz.Name = "TxtOffsetAz"
        Me.TxtOffsetAz.Visible = True
        Me.TxtOffsetAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtOffsetAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtOffsetAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtOffsetEl
        '
        Me.TxtOffsetEl.Location = New System.Drawing.Point(684, 458)
        Me.TxtOffsetEl.Size = New System.Drawing.Size(49, 22)
        Me.TxtOffsetEl.Name = "TxtOffsetEl"
        Me.TxtOffsetEl.Visible = True
        Me.TxtOffsetEl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtOffsetEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtOffsetEl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'tmrPhasing
        '
        Me.tmrPhasing.Interval = 30000
        '
        'Frame2
        '
        Me.Frame2.Location = New System.Drawing.Point(2, 139)
        Me.Frame2.Size = New System.Drawing.Size(478, 234)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        'CmdPiu
        '
        Me.CmdPiu.Location = New System.Drawing.Point(170, 200)
        Me.CmdPiu.Size = New System.Drawing.Size(18, 22)
        Me.CmdPiu.Name = "CmdPiu"
        Me.CmdPiu.Text = "+"
        Me.CmdPiu.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        '
        'CmdMeno
        '
        Me.CmdMeno.Location = New System.Drawing.Point(103, 200)
        Me.CmdMeno.Size = New System.Drawing.Size(18, 22)
        Me.CmdMeno.Name = "CmdMeno"
        Me.CmdMeno.Text = "-"
        Me.CmdMeno.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtPhase
        '
        Me.TxtPhase.Location = New System.Drawing.Point(124, 200)
        Me.TxtPhase.Size = New System.Drawing.Size(42, 22)
        Me.TxtPhase.Name = "TxtPhase"
        Me.TxtPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPhase.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtPhAz
        '
        Me.TxtPhAz.Location = New System.Drawing.Point(28, 200)
        Me.TxtPhAz.Size = New System.Drawing.Size(42, 22)
        Me.TxtPhAz.Name = "TxtPhAz"
        Me.TxtPhAz.Visible = True
        Me.TxtPhAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPhAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'CmdMenoAz
        '
        Me.CmdMenoAz.Location = New System.Drawing.Point(5, 200)
        Me.CmdMenoAz.Size = New System.Drawing.Size(18, 22)
        Me.CmdMenoAz.Name = "CmdMenoAz"
        Me.CmdMenoAz.Text = "-"
        Me.CmdMenoAz.Visible = True
        Me.CmdMenoAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmdPiuAz
        '
        Me.CmdPiuAz.Location = New System.Drawing.Point(70, 200)
        Me.CmdPiuAz.Size = New System.Drawing.Size(18, 22)
        Me.CmdPiuAz.Name = "CmdPiuAz"
        Me.CmdPiuAz.Text = "+"
        Me.CmdPiuAz.Visible = True
        Me.CmdPiuAz.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        '
        'TxtElGain1
        '
        Me.TxtElGain1.Location = New System.Drawing.Point(353, 158)
        Me.TxtElGain1.Size = New System.Drawing.Size(49, 22)
        Me.TxtElGain1.Name = "TxtElGain1"
        Me.TxtElGain1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtElGain1.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtElGain1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtAzGain1
        '
        Me.TxtAzGain1.Location = New System.Drawing.Point(286, 158)
        Me.TxtAzGain1.Size = New System.Drawing.Size(49, 22)
        Me.TxtAzGain1.Name = "TxtAzGain1"
        Me.TxtAzGain1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAzGain1.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtAzGain1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'CmbRevAz
        '
        Me.CmbRevAz.Location = New System.Drawing.Point(437, 82)
        Me.CmbRevAz.Size = New System.Drawing.Size(37, 19)
        Me.CmbRevAz.Name = "CmbRevAz"
        Me.CmbRevAz.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbRevAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbInc
        '
        Me.CmbInc.Location = New System.Drawing.Point(286, 200)
        Me.CmbInc.Size = New System.Drawing.Size(49, 22)
        Me.CmbInc.Name = "CmbInc"
        Me.CmbInc.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapRevAzError
        '
        Me.LblCapRevAzError.Location = New System.Drawing.Point(336, 82)
        Me.LblCapRevAzError.Size = New System.Drawing.Size(68, 18)
        Me.LblCapRevAzError.Name = "LblCapRevAzError"
        Me.LblCapRevAzError.Text = "Rev Az Error"
        Me.LblCapRevAzError.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapRevAzError.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblLock
        '
        Me.LblLock.Location = New System.Drawing.Point(262, 79)
        Me.LblLock.Size = New System.Drawing.Size(66, 22)
        Me.LblLock.Name = "LblLock"
        Me.LblLock.BackColor = Color.FromArgb(255, 255, 128)
        Me.LblLock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblLock.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblLock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapRXStatus
        '
        Me.LblCapRXStatus.Location = New System.Drawing.Point(185, 82)
        Me.LblCapRXStatus.Size = New System.Drawing.Size(68, 18)
        Me.LblCapRXStatus.Name = "LblCapRXStatus"
        Me.LblCapRXStatus.Text = "RX Status"
        Me.LblCapRXStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapRXStatus.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapError
        '
        Me.LblCapError.Location = New System.Drawing.Point(34, 72)
        Me.LblCapError.Size = New System.Drawing.Size(30, 18)
        Me.LblCapError.Name = "LblCapError"
        Me.LblCapError.Text = "Error "
        Me.LblCapError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapError.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblErrAz
        '
        Me.LblErrAz.Location = New System.Drawing.Point(15, 94)
        Me.LblErrAz.Size = New System.Drawing.Size(68, 22)
        Me.LblErrAz.Name = "LblErrAz"
        Me.LblErrAz.BackColor = Color.FromArgb(255, 255, 128)
        Me.LblErrAz.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblErrAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblErrAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblErrEl
        '
        Me.LblErrEl.Location = New System.Drawing.Point(111, 94)
        Me.LblErrEl.Size = New System.Drawing.Size(68, 22)
        Me.LblErrEl.Name = "LblErrEl"
        Me.LblErrEl.BackColor = Color.FromArgb(255, 255, 128)
        Me.LblErrEl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblErrEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblErrEl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapDB
        '
        Me.LblCapDB.Location = New System.Drawing.Point(338, 41)
        Me.LblCapDB.Size = New System.Drawing.Size(18, 18)
        Me.LblCapDB.Name = "LblCapDB"
        Me.LblCapDB.Text = "dB"
        Me.LblCapDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapDB.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtLev
        '
        Me.TxtLev.Location = New System.Drawing.Point(288, 38)
        Me.TxtLev.Size = New System.Drawing.Size(44, 22)
        Me.TxtLev.Name = "TxtLev"
        Me.TxtLev.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtLev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLev.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtLev.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapBCNLevel
        '
        Me.LblCapBCNLevel.Location = New System.Drawing.Point(259, 12)
        Me.LblCapBCNLevel.Size = New System.Drawing.Size(58, 18)
        Me.LblCapBCNLevel.Name = "LblCapBCNLevel"
        Me.LblCapBCNLevel.Text = "BCN Level"
        Me.LblCapBCNLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapBCNLevel.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapEL
        '
        Me.LblCapEL.Location = New System.Drawing.Point(131, 14)
        Me.LblCapEL.Size = New System.Drawing.Size(27, 18)
        Me.LblCapEL.Name = "LblCapEL"
        Me.LblCapEL.Text = "EL"
        Me.LblCapEL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapEL.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtEl
        '
        Me.TxtEl.Location = New System.Drawing.Point(104, 35)
        Me.TxtEl.Size = New System.Drawing.Size(82, 30)
        Me.TxtEl.Name = "TxtEl"
        Me.TxtEl.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtEl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtEl.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        Me.TxtEl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtAz
        '
        Me.TxtAz.Location = New System.Drawing.Point(7, 34)
        Me.TxtAz.Size = New System.Drawing.Size(82, 30)
        Me.TxtAz.Name = "TxtAz"
        Me.TxtAz.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAz.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        Me.TxtAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapAZ
        '
        Me.LblCapAZ.Location = New System.Drawing.Point(38, 14)
        Me.LblCapAZ.Size = New System.Drawing.Size(22, 18)
        Me.LblCapAZ.Name = "LblCapAZ"
        Me.LblCapAZ.Text = "AZ"
        Me.LblCapAZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapAZ.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblBeacon
        '
        Me.LblBeacon.Location = New System.Drawing.Point(240, 38)
        Me.LblBeacon.Size = New System.Drawing.Size(44, 22)
        Me.LblBeacon.Name = "LblBeacon"
        Me.LblBeacon.BackColor = Color.FromArgb(192, 192, 0)
        Me.LblBeacon.Visible = True
        Me.LblBeacon.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblBeacon.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblBeacon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapLNA
        '
        Me.LblCapLNA.Location = New System.Drawing.Point(394, 12)
        Me.LblCapLNA.Size = New System.Drawing.Size(27, 18)
        Me.LblCapLNA.Name = "LblCapLNA"
        Me.LblCapLNA.Text = "LNA"
        Me.LblCapLNA.Visible = True
        Me.LblCapLNA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapLNA.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtAzErr
        '
        Me.TxtAzErr.Location = New System.Drawing.Point(15, 118)
        Me.TxtAzErr.Size = New System.Drawing.Size(68, 22)
        Me.TxtAzErr.Name = "TxtAzErr"
        Me.TxtAzErr.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtAzErr.Visible = True
        Me.TxtAzErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAzErr.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtAzErr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtElErr
        '
        Me.TxtElErr.Location = New System.Drawing.Point(111, 120)
        Me.TxtElErr.Size = New System.Drawing.Size(68, 22)
        Me.TxtElErr.Name = "TxtElErr"
        Me.TxtElErr.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtElErr.Visible = True
        Me.TxtElErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtElErr.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtElErr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtLna
        '
        Me.TxtLna.Location = New System.Drawing.Point(384, 38)
        Me.TxtLna.Size = New System.Drawing.Size(54, 22)
        Me.TxtLna.Name = "TxtLna"
        Me.TxtLna.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtLna.Visible = True
        Me.TxtLna.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLna.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtLna.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapError2
        '
        Me.LblCapError2.Location = New System.Drawing.Point(130, 72)
        Me.LblCapError2.Size = New System.Drawing.Size(30, 18)
        Me.LblCapError2.Name = "LblCapError2"
        Me.LblCapError2.Text = "Error "
        Me.LblCapError2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapError2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapPhaseInc
        '
        Me.LblCapPhaseInc.Location = New System.Drawing.Point(206, 202)
        Me.LblCapPhaseInc.Size = New System.Drawing.Size(58, 18)
        Me.LblCapPhaseInc.Name = "LblCapPhaseInc"
        Me.LblCapPhaseInc.Text = "Phase Inc."
        Me.LblCapPhaseInc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapPhaseInc.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtPhaseEl
        '
        Me.TxtPhaseEl.Location = New System.Drawing.Point(122, 173)
        Me.TxtPhaseEl.Size = New System.Drawing.Size(42, 22)
        Me.TxtPhaseEl.Name = "TxtPhaseEl"
        Me.TxtPhaseEl.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtPhaseEl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtPhaseEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhaseEl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapPhaseEl
        '
        Me.LblCapPhaseEl.Location = New System.Drawing.Point(123, 154)
        Me.LblCapPhaseEl.Size = New System.Drawing.Size(44, 18)
        Me.LblCapPhaseEl.Name = "LblCapPhaseEl"
        Me.LblCapPhaseEl.Text = "PhaseEl"
        Me.LblCapPhaseEl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPhaseEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblRevAz
        '
        Me.LblRevAz.Location = New System.Drawing.Point(408, 80)
        Me.LblRevAz.Size = New System.Drawing.Size(25, 22)
        Me.LblRevAz.Name = "LblRevAz"
        Me.LblRevAz.BackColor = Color.FromArgb(255, 255, 128)
        Me.LblRevAz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblRevAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblRevAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapPhaseAz
        '
        Me.LblCapPhaseAz.Location = New System.Drawing.Point(27, 154)
        Me.LblCapPhaseAz.Size = New System.Drawing.Size(44, 18)
        Me.LblCapPhaseAz.Name = "LblCapPhaseAz"
        Me.LblCapPhaseAz.Text = "PhaseAz"
        Me.LblCapPhaseAz.Visible = True
        Me.LblCapPhaseAz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPhaseAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtPhaseAz
        '
        Me.TxtPhaseAz.Location = New System.Drawing.Point(28, 173)
        Me.TxtPhaseAz.Size = New System.Drawing.Size(42, 22)
        Me.TxtPhaseAz.Name = "TxtPhaseAz"
        Me.TxtPhaseAz.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtPhaseAz.Visible = True
        Me.TxtPhaseAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtPhaseAz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhaseAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtElGain
        '
        Me.TxtElGain.Location = New System.Drawing.Point(415, 158)
        Me.TxtElGain.Size = New System.Drawing.Size(46, 22)
        Me.TxtElGain.Name = "TxtElGain"
        Me.TxtElGain.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtElGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtElGain.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtElGain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapElevation
        '
        Me.LblCapElevation.Location = New System.Drawing.Point(370, 134)
        Me.LblCapElevation.Size = New System.Drawing.Size(58, 18)
        Me.LblCapElevation.Name = "LblCapElevation"
        Me.LblCapElevation.Text = "Elevation"
        Me.LblCapElevation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapElevation.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtAzGain
        '
        Me.TxtAzGain.Location = New System.Drawing.Point(228, 158)
        Me.TxtAzGain.Size = New System.Drawing.Size(46, 22)
        Me.TxtAzGain.Name = "TxtAzGain"
        Me.TxtAzGain.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtAzGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtAzGain.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtAzGain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapGain
        '
        Me.LblCapGain.Location = New System.Drawing.Point(326, 134)
        Me.LblCapGain.Size = New System.Drawing.Size(32, 18)
        Me.LblCapGain.Name = "LblCapGain"
        Me.LblCapGain.Text = "Gain"
        Me.LblCapGain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapGain.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapAzimuth
        '
        Me.LblCapAzimuth.Location = New System.Drawing.Point(257, 134)
        Me.LblCapAzimuth.Size = New System.Drawing.Size(46, 18)
        Me.LblCapAzimuth.Name = "LblCapAzimuth"
        Me.LblCapAzimuth.Text = "Azimuth"
        Me.LblCapAzimuth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapAzimuth.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmdSave
        '
        Me.CmdSave.Location = New System.Drawing.Point(674, 490)
        Me.CmdSave.Size = New System.Drawing.Size(54, 27)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.Text = "SAVE"
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'tmrAlarm
        '
        Me.tmrAlarm.Interval = 30000
        '
        'frmIFR
        '
        Me.frmIFR.Location = New System.Drawing.Point(485, 41)
        Me.frmIFR.Size = New System.Drawing.Size(253, 397)
        Me.frmIFR.Name = "frmIFR"
        Me.frmIFR.Text = "IF Processing"
        Me.frmIFR.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtRange
        '
        Me.TxtRange.Location = New System.Drawing.Point(139, 60)
        Me.TxtRange.Size = New System.Drawing.Size(75, 22)
        Me.TxtRange.Name = "TxtRange"
        Me.TxtRange.BackColor = Color.FromArgb(255, 255, 255)
        Me.TxtRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtRange.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtFreq
        '
        Me.TxtFreq.Location = New System.Drawing.Point(139, 29)
        Me.TxtFreq.Size = New System.Drawing.Size(75, 22)
        Me.TxtFreq.Name = "TxtFreq"
        Me.TxtFreq.BackColor = Color.FromArgb(255, 255, 255)
        Me.TxtFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFreq.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtFreq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'CmbBandWidth
        '
        Me.CmbBandWidth.Location = New System.Drawing.Point(139, 358)
        Me.CmbBandWidth.Size = New System.Drawing.Size(80, 19)
        Me.CmbBandWidth.Name = "CmbBandWidth"
        Me.CmbBandWidth.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbBandWidth.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbVideo
        '
        Me.CmbVideo.Location = New System.Drawing.Point(139, 291)
        Me.CmbVideo.Size = New System.Drawing.Size(80, 19)
        Me.CmbVideo.Name = "CmbVideo"
        Me.CmbVideo.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbVideo.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbAFC
        '
        Me.CmbAFC.Location = New System.Drawing.Point(139, 325)
        Me.CmbAFC.Size = New System.Drawing.Size(80, 19)
        Me.CmbAFC.Name = "CmbAFC"
        Me.CmbAFC.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbAFC.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbAGC
        '
        Me.CmbAGC.Location = New System.Drawing.Point(139, 226)
        Me.CmbAGC.Size = New System.Drawing.Size(80, 19)
        Me.CmbAGC.Name = "CmbAGC"
        Me.CmbAGC.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbAGC.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbSelection
        '
        Me.CmbSelection.Location = New System.Drawing.Point(139, 258)
        Me.CmbSelection.Size = New System.Drawing.Size(80, 19)
        Me.CmbSelection.Name = "CmbSelection"
        Me.CmbSelection.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbSelection.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbLoop
        '
        Me.CmbLoop.Location = New System.Drawing.Point(139, 160)
        Me.CmbLoop.Size = New System.Drawing.Size(80, 19)
        Me.CmbLoop.Name = "CmbLoop"
        Me.CmbLoop.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbLoop.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbInputPort
        '
        Me.CmbInputPort.Location = New System.Drawing.Point(139, 193)
        Me.CmbInputPort.Size = New System.Drawing.Size(80, 19)
        Me.CmbInputPort.Name = "CmbInputPort"
        Me.CmbInputPort.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbInputPort.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbOperMode
        '
        Me.CmbOperMode.Location = New System.Drawing.Point(139, 94)
        Me.CmbOperMode.Size = New System.Drawing.Size(80, 19)
        Me.CmbOperMode.Name = "CmbOperMode"
        Me.CmbOperMode.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbOperMode.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbCarrier
        '
        Me.CmbCarrier.Location = New System.Drawing.Point(139, 127)
        Me.CmbCarrier.Size = New System.Drawing.Size(80, 19)
        Me.CmbCarrier.Name = "CmbCarrier"
        Me.CmbCarrier.BackColor = Color.FromArgb(255, 255, 255)
        Me.CmbCarrier.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapMs
        '
        Me.LblCapMs.Location = New System.Drawing.Point(223, 326)
        Me.LblCapMs.Size = New System.Drawing.Size(22, 18)
        Me.LblCapMs.Name = "LblCapMs"
        Me.LblCapMs.Text = "ms"
        Me.LblCapMs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapMs.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapMs2
        '
        Me.LblCapMs2.Location = New System.Drawing.Point(223, 226)
        Me.LblCapMs2.Size = New System.Drawing.Size(22, 18)
        Me.LblCapMs2.Name = "LblCapMs2"
        Me.LblCapMs2.Text = "ms"
        Me.LblCapMs2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapMs2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapKHz
        '
        Me.LblCapKHz.Location = New System.Drawing.Point(223, 360)
        Me.LblCapKHz.Size = New System.Drawing.Size(25, 18)
        Me.LblCapKHz.Name = "LblCapKHz"
        Me.LblCapKHz.Text = "KHz"
        Me.LblCapKHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapKHz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapHz
        '
        Me.LblCapHz.Location = New System.Drawing.Point(223, 161)
        Me.LblCapHz.Size = New System.Drawing.Size(22, 18)
        Me.LblCapHz.Name = "LblCapHz"
        Me.LblCapHz.Text = "Hz"
        Me.LblCapHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapHz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapHz2
        '
        Me.LblCapHz2.Location = New System.Drawing.Point(223, 62)
        Me.LblCapHz2.Size = New System.Drawing.Size(22, 18)
        Me.LblCapHz2.Name = "LblCapHz2"
        Me.LblCapHz2.Text = "Hz"
        Me.LblCapHz2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapHz2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapKHz2
        '
        Me.LblCapKHz2.Location = New System.Drawing.Point(223, 29)
        Me.LblCapKHz2.Size = New System.Drawing.Size(25, 18)
        Me.LblCapKHz2.Name = "LblCapKHz2"
        Me.LblCapKHz2.Text = "KHz"
        Me.LblCapKHz2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapKHz2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapIFBwInAMEFM
        '
        Me.LblCapIFBwInAMEFM.Location = New System.Drawing.Point(10, 362)
        Me.LblCapIFBwInAMEFM.Size = New System.Drawing.Size(116, 18)
        Me.LblCapIFBwInAMEFM.Name = "LblCapIFBwInAMEFM"
        Me.LblCapIFBwInAMEFM.Text = "IF Bw in AM and FM"
        Me.LblCapIFBwInAMEFM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapIFBwInAMEFM.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapVideoDemodulation
        '
        Me.LblCapVideoDemodulation.Location = New System.Drawing.Point(10, 295)
        Me.LblCapVideoDemodulation.Size = New System.Drawing.Size(116, 18)
        Me.LblCapVideoDemodulation.Name = "LblCapVideoDemodulation"
        Me.LblCapVideoDemodulation.Text = "Video Demodulation"
        Me.LblCapVideoDemodulation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapVideoDemodulation.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapAFC
        '
        Me.LblCapAFC.Location = New System.Drawing.Point(10, 329)
        Me.LblCapAFC.Size = New System.Drawing.Size(116, 18)
        Me.LblCapAFC.Name = "LblCapAFC"
        Me.LblCapAFC.Text = "AFC"
        Me.LblCapAFC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapAFC.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapInputPort
        '
        Me.LblCapInputPort.Location = New System.Drawing.Point(10, 262)
        Me.LblCapInputPort.Size = New System.Drawing.Size(116, 18)
        Me.LblCapInputPort.Name = "LblCapInputPort"
        Me.LblCapInputPort.Text = "Input Port"
        Me.LblCapInputPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapInputPort.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapAGCTimeConstant
        '
        Me.LblCapAGCTimeConstant.Location = New System.Drawing.Point(10, 229)
        Me.LblCapAGCTimeConstant.Size = New System.Drawing.Size(116, 18)
        Me.LblCapAGCTimeConstant.Name = "LblCapAGCTimeConstant"
        Me.LblCapAGCTimeConstant.Text = "AGC Time Constant"
        Me.LblCapAGCTimeConstant.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapAGCTimeConstant.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapInputSelection
        '
        Me.LblCapInputSelection.Location = New System.Drawing.Point(10, 195)
        Me.LblCapInputSelection.Size = New System.Drawing.Size(116, 18)
        Me.LblCapInputSelection.Name = "LblCapInputSelection"
        Me.LblCapInputSelection.Text = "Input Selection"
        Me.LblCapInputSelection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapInputSelection.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapCarrierAcquisition
        '
        Me.LblCapCarrierAcquisition.Location = New System.Drawing.Point(10, 129)
        Me.LblCapCarrierAcquisition.Size = New System.Drawing.Size(116, 18)
        Me.LblCapCarrierAcquisition.Name = "LblCapCarrierAcquisition"
        Me.LblCapCarrierAcquisition.Text = "Carrier Acquisition"
        Me.LblCapCarrierAcquisition.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapCarrierAcquisition.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapLoopFilterBw
        '
        Me.LblCapLoopFilterBw.Location = New System.Drawing.Point(10, 162)
        Me.LblCapLoopFilterBw.Size = New System.Drawing.Size(116, 18)
        Me.LblCapLoopFilterBw.Name = "LblCapLoopFilterBw"
        Me.LblCapLoopFilterBw.Text = "Loop Filter Bw"
        Me.LblCapLoopFilterBw.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapLoopFilterBw.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapOperatingMode
        '
        Me.LblCapOperatingMode.Location = New System.Drawing.Point(10, 95)
        Me.LblCapOperatingMode.Size = New System.Drawing.Size(116, 18)
        Me.LblCapOperatingMode.Name = "LblCapOperatingMode"
        Me.LblCapOperatingMode.Text = "Operating Mode"
        Me.LblCapOperatingMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapOperatingMode.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapInputFrequency
        '
        Me.LblCapInputFrequency.Location = New System.Drawing.Point(7, 31)
        Me.LblCapInputFrequency.Size = New System.Drawing.Size(116, 18)
        Me.LblCapInputFrequency.Name = "LblCapInputFrequency"
        Me.LblCapInputFrequency.Text = "Input Frequency"
        Me.LblCapInputFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapInputFrequency.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapAcquisitionRange
        '
        Me.LblCapAcquisitionRange.Location = New System.Drawing.Point(10, 62)
        Me.LblCapAcquisitionRange.Size = New System.Drawing.Size(116, 18)
        Me.LblCapAcquisitionRange.Name = "LblCapAcquisitionRange"
        Me.LblCapAcquisitionRange.Text = "Acquisition Range"
        Me.LblCapAcquisitionRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCapAcquisitionRange.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtFreccia
        '
        Me.TxtFreccia.Location = New System.Drawing.Point(266, 562)
        Me.TxtFreccia.Size = New System.Drawing.Size(9, 19)
        Me.TxtFreccia.Name = "TxtFreccia"
        Me.TxtFreccia.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtFreccia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'frmManual
        '
        Me.frmManual.Location = New System.Drawing.Point(0, 377)
        Me.frmManual.Size = New System.Drawing.Size(482, 121)
        Me.frmManual.Name = "frmManual"
        Me.frmManual.Text = "Step-Preset"
        Me.frmManual.Visible = True
        Me.frmManual.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmbEl
        '
        Me.CmbEl.Location = New System.Drawing.Point(223, 22)
        Me.CmbEl.Size = New System.Drawing.Size(63, 19)
        Me.CmbEl.Name = "CmbEl"
        Me.CmbEl.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmdAnnulla
        '
        Me.CmdAnnulla.Location = New System.Drawing.Point(317, 17)
        Me.CmdAnnulla.Size = New System.Drawing.Size(54, 27)
        Me.CmdAnnulla.Name = "CmdAnnulla"
        Me.CmdAnnulla.Text = "EXIT"
        Me.CmdAnnulla.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmdUp
        '
        Me.CmdUp.Location = New System.Drawing.Point(67, 17)
        Me.CmdUp.Size = New System.Drawing.Size(39, 27)
        Me.CmdUp.Name = "CmdUp"
        Me.CmdUp.Text = "UP"
        Me.CmdUp.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmdCCW
        '
        Me.CmdCCW.Location = New System.Drawing.Point(24, 50)
        Me.CmdCCW.Size = New System.Drawing.Size(39, 27)
        Me.CmdCCW.Name = "CmdCCW"
        Me.CmdCCW.Text = "CCW"
        Me.CmdCCW.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmdCW
        '
        Me.CmdCW.Location = New System.Drawing.Point(108, 50)
        Me.CmdCW.Size = New System.Drawing.Size(39, 27)
        Me.CmdCW.Name = "CmdCW"
        Me.CmdCW.Text = "CW"
        Me.CmdCW.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'CmdDown
        '
        Me.CmdDown.Location = New System.Drawing.Point(67, 84)
        Me.CmdDown.Size = New System.Drawing.Size(39, 27)
        Me.CmdDown.Name = "CmdDown"
        Me.CmdDown.Text = "DOWN"
        Me.CmdDown.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'Image1
        '
        Me.Image1.Location = New System.Drawing.Point(67, 46)
        Me.Image1.Size = New System.Drawing.Size(37, 34)
        Me.Image1.Name = "Image1"
        '
        'LblTesto
        '
        Me.LblTesto.Location = New System.Drawing.Point(163, 79)
        Me.LblTesto.Size = New System.Drawing.Size(306, 27)
        Me.LblTesto.Name = "LblTesto"
        Me.LblTesto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTesto.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        '
        'LblCapStepSize
        '
        Me.LblCapStepSize.Location = New System.Drawing.Point(166, 24)
        Me.LblCapStepSize.Size = New System.Drawing.Size(51, 18)
        Me.LblCapStepSize.Name = "LblCapStepSize"
        Me.LblCapStepSize.Text = "Step Size"
        Me.LblCapStepSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapStepSize.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'Frame1
        '
        Me.Frame1.Location = New System.Drawing.Point(2, 43)
        Me.Frame1.Size = New System.Drawing.Size(478, 90)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        'LblCapPolErr
        '
        Me.LblCapPolErr.Location = New System.Drawing.Point(10, 38)
        Me.LblCapPolErr.Size = New System.Drawing.Size(44, 18)
        Me.LblCapPolErr.Name = "LblCapPolErr"
        Me.LblCapPolErr.Text = "Pol.Err."
        Me.LblCapPolErr.Visible = True
        Me.LblCapPolErr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPolErr.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'TxtPol2
        '
        Me.TxtPol2.Location = New System.Drawing.Point(67, 36)
        Me.TxtPol2.Size = New System.Drawing.Size(78, 20)
        Me.TxtPol2.Name = "TxtPol2"
        Me.TxtPol2.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtPol2.Visible = True
        Me.TxtPol2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPol2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPol2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtPol1
        '
        Me.TxtPol1.Location = New System.Drawing.Point(66, 60)
        Me.TxtPol1.Size = New System.Drawing.Size(78, 20)
        Me.TxtPol1.Name = "TxtPol1"
        Me.TxtPol1.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtPol1.Visible = True
        Me.TxtPol1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPol1.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPol1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblPol
        '
        Me.LblPol.Location = New System.Drawing.Point(151, 36)
        Me.LblPol.Size = New System.Drawing.Size(85, 20)
        Me.LblPol.Name = "LblPol"
        Me.LblPol.BackColor = Color.FromArgb(192, 192, 0)
        Me.LblPol.Visible = True
        Me.LblPol.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblPol.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblPol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtFrequency
        '
        Me.TxtFrequency.Location = New System.Drawing.Point(394, 58)
        Me.TxtFrequency.Size = New System.Drawing.Size(80, 22)
        Me.TxtFrequency.Name = "TxtFrequency"
        Me.TxtFrequency.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtFrequency.Visible = True
        Me.TxtFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFrequency.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtFrequency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtInpFrequency
        '
        Me.TxtInpFrequency.Location = New System.Drawing.Point(394, 34)
        Me.TxtInpFrequency.Size = New System.Drawing.Size(80, 20)
        Me.TxtInpFrequency.Name = "TxtInpFrequency"
        Me.TxtInpFrequency.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtInpFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtInpFrequency.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtInpFrequency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtPhasingMode
        '
        Me.TxtPhasingMode.Location = New System.Drawing.Point(394, 10)
        Me.TxtPhasingMode.Size = New System.Drawing.Size(80, 20)
        Me.TxtPhasingMode.Name = "TxtPhasingMode"
        Me.TxtPhasingMode.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtPhasingMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPhasingMode.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhasingMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtPol
        '
        Me.TxtPol.Location = New System.Drawing.Point(151, 60)
        Me.TxtPol.Size = New System.Drawing.Size(85, 20)
        Me.TxtPol.Name = "TxtPol"
        Me.TxtPol.BackColor = Color.FromArgb(192, 192, 0)
        Me.TxtPol.Visible = True
        Me.TxtPol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPol.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtPolarizationAngle
        '
        Me.TxtPolarizationAngle.Location = New System.Drawing.Point(151, 10)
        Me.TxtPolarizationAngle.Size = New System.Drawing.Size(85, 20)
        Me.TxtPolarizationAngle.Name = "TxtPolarizationAngle"
        Me.TxtPolarizationAngle.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtPolarizationAngle.Visible = True
        Me.TxtPolarizationAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPolarizationAngle.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPolarizationAngle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtPhasingFlow
        '
        Me.TxtPhasingFlow.Location = New System.Drawing.Point(166, 72)
        Me.TxtPhasingFlow.Size = New System.Drawing.Size(63, 20)
        Me.TxtPhasingFlow.Name = "TxtPhasingFlow"
        Me.TxtPhasingFlow.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtPhasingFlow.Visible = True
        Me.TxtPhasingFlow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtPhasingFlow.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtPhasingFlow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapReceivingMode
        '
        Me.LblCapReceivingMode.Location = New System.Drawing.Point(245, 14)
        Me.LblCapReceivingMode.Size = New System.Drawing.Size(116, 18)
        Me.LblCapReceivingMode.Name = "LblCapReceivingMode"
        Me.LblCapReceivingMode.Text = "Receiving Mode"
        Me.LblCapReceivingMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapReceivingMode.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapRFInputFrequencyKHz
        '
        Me.LblCapRFInputFrequencyKHz.Location = New System.Drawing.Point(245, 36)
        Me.LblCapRFInputFrequencyKHz.Size = New System.Drawing.Size(147, 18)
        Me.LblCapRFInputFrequencyKHz.Name = "LblCapRFInputFrequencyKHz"
        Me.LblCapRFInputFrequencyKHz.Text = "RF Input Frequency (kHz)"
        Me.LblCapRFInputFrequencyKHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapRFInputFrequencyKHz.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapPolarizationAngleDeg
        '
        Me.LblCapPolarizationAngleDeg.Location = New System.Drawing.Point(5, 14)
        Me.LblCapPolarizationAngleDeg.Size = New System.Drawing.Size(140, 20)
        Me.LblCapPolarizationAngleDeg.Name = "LblCapPolarizationAngleDeg"
        Me.LblCapPolarizationAngleDeg.Text = "Polarization Angle (deg)"
        Me.LblCapPolarizationAngleDeg.Visible = True
        Me.LblCapPolarizationAngleDeg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPolarizationAngleDeg.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapPhasingFlowIdHex
        '
        Me.LblCapPhasingFlowIdHex.Location = New System.Drawing.Point(10, 74)
        Me.LblCapPhasingFlowIdHex.Size = New System.Drawing.Size(135, 18)
        Me.LblCapPhasingFlowIdHex.Name = "LblCapPhasingFlowIdHex"
        Me.LblCapPhasingFlowIdHex.Text = "Phasing Flow Id. (hex.)"
        Me.LblCapPhasingFlowIdHex.Visible = True
        Me.LblCapPhasingFlowIdHex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapPhasingFlowIdHex.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapOffsetAzimuth
        '
        Me.LblCapOffsetAzimuth.Location = New System.Drawing.Point(482, 461)
        Me.LblCapOffsetAzimuth.Size = New System.Drawing.Size(80, 18)
        Me.LblCapOffsetAzimuth.Name = "LblCapOffsetAzimuth"
        Me.LblCapOffsetAzimuth.Text = "Offset Azimuth"
        Me.LblCapOffsetAzimuth.Visible = True
        Me.LblCapOffsetAzimuth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapOffsetAzimuth.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapElevation2
        '
        Me.LblCapElevation2.Location = New System.Drawing.Point(624, 461)
        Me.LblCapElevation2.Size = New System.Drawing.Size(58, 18)
        Me.LblCapElevation2.Name = "LblCapElevation2"
        Me.LblCapElevation2.Text = "Elevation"
        Me.LblCapElevation2.Visible = True
        Me.LblCapElevation2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapElevation2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblWarning
        '
        Me.LblWarning.Location = New System.Drawing.Point(490, 490)
        Me.LblWarning.Size = New System.Drawing.Size(171, 27)
        Me.LblWarning.Name = "LblWarning"
        Me.LblWarning.Visible = True
        Me.LblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblWarning.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapRXValue
        '
        Me.LblCapRXValue.Location = New System.Drawing.Point(329, 506)
        Me.LblCapRXValue.Size = New System.Drawing.Size(92, 20)
        Me.LblCapRXValue.Name = "LblCapRXValue"
        Me.LblCapRXValue.Text = "RX Value"
        Me.LblCapRXValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapRXValue.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapACUValue
        '
        Me.LblCapACUValue.Location = New System.Drawing.Point(17, 506)
        Me.LblCapACUValue.Size = New System.Drawing.Size(92, 20)
        Me.LblCapACUValue.Name = "LblCapACUValue"
        Me.LblCapACUValue.Text = "ACU Value"
        Me.LblCapACUValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapACUValue.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblRxValueBox
        '
        Me.LblRxValueBox.Location = New System.Drawing.Point(439, 506)
        Me.LblRxValueBox.Size = New System.Drawing.Size(25, 20)
        Me.LblRxValueBox.Name = "LblRxValueBox"
        Me.LblRxValueBox.BackColor = Color.FromArgb(255, 255, 128)
        Me.LblRxValueBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblRxValueBox.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblRxValueBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblAcuValueBox
        '
        Me.LblAcuValueBox.Location = New System.Drawing.Point(127, 506)
        Me.LblAcuValueBox.Size = New System.Drawing.Size(22, 20)
        Me.LblAcuValueBox.Name = "LblAcuValueBox"
        Me.LblAcuValueBox.BackColor = Color.FromArgb(192, 192, 0)
        Me.LblAcuValueBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblAcuValueBox.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        Me.LblAcuValueBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtValid
        '
        Me.TxtValid.Location = New System.Drawing.Point(456, 17)
        Me.TxtValid.Size = New System.Drawing.Size(80, 20)
        Me.TxtValid.Name = "TxtValid"
        Me.TxtValid.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtValid.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtValid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'TxtProcess
        '
        Me.TxtProcess.Location = New System.Drawing.Point(334, 17)
        Me.TxtProcess.Size = New System.Drawing.Size(80, 20)
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.BackColor = Color.FromArgb(255, 255, 128)
        Me.TxtProcess.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TxtProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblPhase
        '
        Me.LblPhase.Location = New System.Drawing.Point(439, 5)
        Me.LblPhase.Size = New System.Drawing.Size(46, 18)
        Me.LblPhase.Name = "LblPhase"
        Me.LblPhase.Visible = True
        Me.LblPhase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblPhase.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapSATELLITE
        '
        Me.LblCapSATELLITE.Location = New System.Drawing.Point(12, 17)
        Me.LblCapSATELLITE.Size = New System.Drawing.Size(75, 18)
        Me.LblCapSATELLITE.Name = "LblCapSATELLITE"
        Me.LblCapSATELLITE.Text = "SATELLITE:"
        Me.LblCapSATELLITE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapSATELLITE.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblSatellite
        '
        Me.LblSatellite.Location = New System.Drawing.Point(91, 14)
        Me.LblSatellite.Size = New System.Drawing.Size(174, 22)
        Me.LblSatellite.Name = "LblSatellite"
        Me.LblSatellite.BackColor = Color.FromArgb(192, 192, 192)
        Me.LblSatellite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblSatellite.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        Me.LblSatellite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        '
        'LblCapProcess
        '
        Me.LblCapProcess.Location = New System.Drawing.Point(274, 19)
        Me.LblCapProcess.Size = New System.Drawing.Size(61, 18)
        Me.LblCapProcess.Name = "LblCapProcess"
        Me.LblCapProcess.Text = "Process"
        Me.LblCapProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapProcess.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblCapValid
        '
        Me.LblCapValid.Location = New System.Drawing.Point(418, 17)
        Me.LblCapValid.Size = New System.Drawing.Size(37, 18)
        Me.LblCapValid.Name = "LblCapValid"
        Me.LblCapValid.Text = "Valid"
        Me.LblCapValid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCapValid.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        '
        'LblInputRx
        '
        Me.LblInputRx.Location = New System.Drawing.Point(502, 7)
        Me.LblInputRx.Size = New System.Drawing.Size(32, 15)
        Me.LblInputRx.Name = "LblInputRx"
        Me.LblInputRx.Text = "Label17"
        Me.LblInputRx.Visible = True
        Me.LblInputRx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblInputRx.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)


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
        Me.frmManual.Controls.Add(Me.CmbEl)
        Me.frmManual.Controls.Add(Me.CmdAnnulla)
        Me.frmManual.Controls.Add(Me.CmdUp)
        Me.frmManual.Controls.Add(Me.CmdCCW)
        Me.frmManual.Controls.Add(Me.CmdCW)
        Me.frmManual.Controls.Add(Me.CmdDown)
        Me.frmManual.Controls.Add(Me.Image1)
        Me.frmManual.Controls.Add(Me.LblTesto)
        Me.frmManual.Controls.Add(Me.LblCapStepSize)
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


        '
        'FrmRicevitore
        '
        Me.ControlBox = False
        Me.ClientSize = New System.Drawing.Size(760, 536)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FrmRicevitore"
        Me.Text = "TDR - Phasing Mode"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
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
