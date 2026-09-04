' Generato da frmEffemeridi.frm (VB6) — coordinate convertite: twips / 15 = pixel
' TrueDBGrid80 + VB.Data → DataGridView; MaskEdBox → TextBox; Crystal → non implementato
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEffemeridi
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
        Me.components      = New System.ComponentModel.Container()
        ' Menu
        Me.MenuStrip1      = New System.Windows.Forms.MenuStrip()
        Me.MnuEfem         = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCalcola      = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuInviaTabella = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSalva        = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSalvaFile    = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStampa       = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChiudi       = New System.Windows.Forms.ToolStripMenuItem()
        ' Input controls
        Me.MskDataIni      = New System.Windows.Forms.TextBox()
        Me.TxtOre          = New System.Windows.Forms.TextBox()
        Me.TxtMinuti       = New System.Windows.Forms.TextBox()
        Me.TxtAzOffset     = New System.Windows.Forms.TextBox()
        Me.TxtElOffset     = New System.Windows.Forms.TextBox()
        ' Display labels
        Me.LblAntenna      = New System.Windows.Forms.Label()
        Me.LblSatellite    = New System.Windows.Forms.Label()
        ' Static labels
        Me.LblStartDate    = New System.Windows.Forms.Label()
        Me.LblAntennaTag   = New System.Windows.Forms.Label()
        Me.LblSatTag       = New System.Windows.Forms.Label()
        Me.LblForHours     = New System.Windows.Forms.Label()
        Me.LblOffset       = New System.Windows.Forms.Label()
        Me.LblAz           = New System.Windows.Forms.Label()
        Me.LblEl           = New System.Windows.Forms.Label()
        ' Data grid (sostituisce TrueDBGrid80.TDBGrid tdbEffemeridi)
        Me.dgvEffemeridi   = New System.Windows.Forms.DataGridView()
        ' Timers
        Me.TmrTable        = New System.Windows.Forms.Timer(Me.components)
        Me.TmrEffemeridi   = New System.Windows.Forms.Timer(Me.components)
        CType(Me.dgvEffemeridi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        ' ── MenuStrip ─────────────────────────────────────────
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuEfem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name     = "MenuStrip1"
        Me.MenuStrip1.Size     = New System.Drawing.Size(756, 28)
        Me.MnuEfem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.MnuCalcola, Me.MnuInviaTabella, Me.MnuSalva,
            Me.MnuSalvaFile, Me.MnuStampa, Me.MnuChiudi})
        Me.MnuEfem.Name          = "MnuEfem"
        Me.MnuEfem.Text          = "Ephemeris"
        Me.MnuCalcola.Name       = "MnuCalcola"
        Me.MnuCalcola.Text       = "Calcola"
        Me.MnuInviaTabella.Name    = "MnuInviaTabella"
        Me.MnuInviaTabella.Text    = "Invia Tabella"
        Me.MnuInviaTabella.Enabled = False
        Me.MnuSalva.Name         = "MnuSalva"
        Me.MnuSalva.Text         = "Salva"
        Me.MnuSalvaFile.Name     = "MnuSalvaFile"
        Me.MnuSalvaFile.Text     = "Salva come..."
        Me.MnuStampa.Name        = "MnuStampa"
        Me.MnuStampa.Text        = "Stampa"
        Me.MnuChiudi.Name        = "MnuChiudi"
        Me.MnuChiudi.Text        = "Chiudi"
        ' ── Row 1: Antenna label (Left=1656/15=110, Top=144/15=10+28=38) ──
        Me.LblAntennaTag.Location = New System.Drawing.Point(26, 42)
        Me.LblAntennaTag.Size     = New System.Drawing.Size(80, 18)
        Me.LblAntennaTag.Text     = "Antenna"
        Me.LblAntenna.Location    = New System.Drawing.Point(110, 38)
        Me.LblAntenna.Size        = New System.Drawing.Size(158, 27)
        Me.LblAntenna.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblAntenna.TextAlign   = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblAntenna.Font        = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        Me.LblAntenna.Name        = "LblAntenna"
        ' Satellite label (Left=5436/15=362, Top=144/15=10+28=38)
        Me.LblSatTag.Location = New System.Drawing.Point(286, 42)
        Me.LblSatTag.Size     = New System.Drawing.Size(70, 18)
        Me.LblSatTag.Text     = "SATELLITE:"
        Me.LblSatellite.Location    = New System.Drawing.Point(362, 38)
        Me.LblSatellite.Size        = New System.Drawing.Size(231, 27)
        Me.LblSatellite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblSatellite.TextAlign   = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblSatellite.Font        = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        Me.LblSatellite.ForeColor   = System.Drawing.Color.LightGreen
        Me.LblSatellite.Name        = "LblSatellite"
        ' ── Row 2: Start date + ore + minuti ──────────────────
        ' LblAzimuth "Start date/time" (Left=72/15=5, Top=756/15=50+28=78)
        Me.LblStartDate.Location = New System.Drawing.Point(5, 78)
        Me.LblStartDate.Size     = New System.Drawing.Size(94, 20)
        Me.LblStartDate.Text     = "Start date/time"
        Me.LblStartDate.Font     = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        ' mskDataIni (Left=1584/15=106, Top=720/15=48+28=76)
        Me.MskDataIni.Location = New System.Drawing.Point(106, 76)
        Me.MskDataIni.Size     = New System.Drawing.Size(162, 26)
        Me.MskDataIni.Font     = New System.Drawing.Font("Arial", 12!)
        Me.MskDataIni.TabIndex = 7
        Me.MskDataIni.Name     = "MskDataIni"
        ' Label2 "For hours:" (Left=4140/15=276, Top=792/15=53+28=81)
        Me.LblForHours.Location = New System.Drawing.Point(276, 81)
        Me.LblForHours.Size     = New System.Drawing.Size(63, 18)
        Me.LblForHours.Text     = "For hours:"
        Me.LblForHours.Font     = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold)
        ' TxtOre (Left=5184/15=346, Top=756/15=50+28=78)
        Me.TxtOre.Location = New System.Drawing.Point(346, 78)
        Me.TxtOre.Size     = New System.Drawing.Size(39, 22)
        Me.TxtOre.TabIndex = 6
        Me.TxtOre.Text     = "24"
        Me.TxtOre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtOre.Font     = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.TxtOre.Name     = "TxtOre"
        ' TxtMinuti (Left=9540/15=636, Top=756/15=50+28=78)
        Me.TxtMinuti.Location  = New System.Drawing.Point(636, 78)
        Me.TxtMinuti.Size      = New System.Drawing.Size(80, 22)
        Me.TxtMinuti.TabIndex  = 0
        Me.TxtMinuti.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtMinuti.Font      = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.TxtMinuti.Name      = "TxtMinuti"
        ' ── Row 3: Offset Az/El ───────────────────────────────
        ' Label5 "Offset" (Left=252/15=17, Top=1212/15=81+28=109)
        Me.LblOffset.Location = New System.Drawing.Point(17, 109)
        Me.LblOffset.Size     = New System.Drawing.Size(63, 20)
        Me.LblOffset.Text     = "Offset"
        Me.LblOffset.Font     = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        ' Label4 "Az." (Left=1980/15=132, Top=1224/15=82+28=110)
        Me.LblAz.Location = New System.Drawing.Point(132, 110)
        Me.LblAz.Size     = New System.Drawing.Size(27, 18)
        Me.LblAz.Text     = "Az."
        Me.LblAz.Font     = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        ' TxtAzOffset (Left=2520/15=168, Top=1188/15=79+28=107)
        Me.TxtAzOffset.Location  = New System.Drawing.Point(168, 107)
        Me.TxtAzOffset.Size      = New System.Drawing.Size(66, 22)
        Me.TxtAzOffset.TabIndex  = 12
        Me.TxtAzOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAzOffset.Font      = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.TxtAzOffset.Name      = "TxtAzOffset"
        ' Label3 "El." (Left=3744/15=250, Top=1224/15=82+28=110)
        Me.LblEl.Location = New System.Drawing.Point(250, 110)
        Me.LblEl.Size     = New System.Drawing.Size(18, 18)
        Me.LblEl.Text     = "El."
        Me.LblEl.Font     = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        ' TxtElOffset (Left=4104/15=274, Top=1188/15=79+28=107)
        Me.TxtElOffset.Location  = New System.Drawing.Point(274, 107)
        Me.TxtElOffset.Size      = New System.Drawing.Size(66, 22)
        Me.TxtElOffset.TabIndex  = 11
        Me.TxtElOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtElOffset.Font      = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.TxtElOffset.Name      = "TxtElOffset"
        ' ── DataGridView (tdbEffemeridi: Left=972/15=65, Top=1692/15=113+28=141) ──
        Me.dgvEffemeridi.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.dgvEffemeridi.Location     = New System.Drawing.Point(65, 141)
        Me.dgvEffemeridi.Size         = New System.Drawing.Size(610, 423)
        Me.dgvEffemeridi.TabIndex     = 2
        Me.dgvEffemeridi.Name         = "dgvEffemeridi"
        Me.dgvEffemeridi.ReadOnly     = True
        Me.dgvEffemeridi.AllowUserToAddRows    = False
        Me.dgvEffemeridi.AllowUserToDeleteRows = False
        Me.dgvEffemeridi.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvEffemeridi.Columns.Add("Date",      "Date")
        Me.dgvEffemeridi.Columns.Add("Time",      "Time")
        Me.dgvEffemeridi.Columns.Add("Azimuth",   "Azimuth")
        Me.dgvEffemeridi.Columns.Add("Elevation", "Elevation")
        Me.dgvEffemeridi.Columns.Add("Long",      "Long")
        Me.dgvEffemeridi.Columns.Add("Lat",       "Lat")
        ' ── Timers ────────────────────────────────────────────
        Me.TmrTable.Interval    = 400  ' VB6: 400ms
        Me.TmrEffemeridi.Interval = 60000
        ' ── Form (ClientWidth=11340/15=756, ClientHeight=8544/15=570 + 28 menu) ──
        Me.ClientSize  = New System.Drawing.Size(756, 598)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.LblAntennaTag, Me.LblAntenna, Me.LblSatTag, Me.LblSatellite,
            Me.LblStartDate, Me.MskDataIni, Me.LblForHours, Me.TxtOre, Me.TxtMinuti,
            Me.LblOffset, Me.LblAz, Me.TxtAzOffset, Me.LblEl, Me.TxtElOffset,
            Me.dgvEffemeridi})
        Me.MainMenuStrip  = Me.MenuStrip1
        Me.ControlBox     = False
        Me.MaximizeBox    = False
        Me.MinimizeBox    = False
        Me.ShowInTaskBar  = False
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name           = "FrmEffemeridi"
        Me.Text           = "Ephemeris"
        CType(Me.dgvEffemeridi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1      As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuEfem         As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCalcola      As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuInviaTabella As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSalva        As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSalvaFile    As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStampa       As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuChiudi       As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TxtOre          As System.Windows.Forms.TextBox
    Friend LblAntenna                 As System.Windows.Forms.Label
    Friend LblSatellite               As System.Windows.Forms.Label
    Friend MskDataIni                 As System.Windows.Forms.TextBox
    Friend TxtAzOffset                As System.Windows.Forms.TextBox
    Friend TxtElOffset                As System.Windows.Forms.TextBox
    Friend WithEvents TxtMinuti       As System.Windows.Forms.TextBox
    Friend WithEvents TmrEffemeridi   As System.Windows.Forms.Timer
    Friend WithEvents TmrTable        As System.Windows.Forms.Timer
    Friend WithEvents dgvEffemeridi   As System.Windows.Forms.DataGridView
    Friend LblStartDate               As System.Windows.Forms.Label
    Friend LblAntennaTag              As System.Windows.Forms.Label
    Friend LblSatTag                  As System.Windows.Forms.Label
    Friend LblForHours                As System.Windows.Forms.Label
    Friend LblOffset                  As System.Windows.Forms.Label
    Friend LblAz                      As System.Windows.Forms.Label
    Friend LblEl                      As System.Windows.Forms.Label

End Class
