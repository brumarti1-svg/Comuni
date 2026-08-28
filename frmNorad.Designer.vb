<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNorad
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
        Me.MenuStrip1    = New System.Windows.Forms.MenuStrip()
        Me.MnuSave       = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuClose      = New System.Windows.Forms.ToolStripMenuItem()
        ' Controls declared Friend WithEvents in frmNorad.vb
        Me.TxtSatellite  = New System.Windows.Forms.TextBox()
        Me.LblInvalido   = New System.Windows.Forms.Label()
        Me.TxtLinea1     = New System.Windows.Forms.TextBox()
        Me.TxtLinea2     = New System.Windows.Forms.TextBox()
        Me.TxtCatalogo   = New System.Windows.Forms.TextBox()
        Me.TxtEpoch      = New System.Windows.Forms.TextBox()
        Me.TxtEpochDate  = New System.Windows.Forms.TextBox()
        Me.TxtDecay      = New System.Windows.Forms.TextBox()
        Me.TxtBSTAR      = New System.Windows.Forms.TextBox()
        Me.TxtElement    = New System.Windows.Forms.TextBox()
        Me.TxtInclination= New System.Windows.Forms.TextBox()
        Me.TxtRAAN       = New System.Windows.Forms.TextBox()
        Me.TxtEccentricity=New System.Windows.Forms.TextBox()
        Me.TxtPerigee    = New System.Windows.Forms.TextBox()
        Me.TxtAnomaly    = New System.Windows.Forms.TextBox()
        Me.TxtMotion     = New System.Windows.Forms.TextBox()
        Me.TxtOrbite     = New System.Windows.Forms.TextBox()
        ' Labels (non dichiarate nel vb)
        Me.LblSat        = New System.Windows.Forms.Label()
        Me.LblL1         = New System.Windows.Forms.Label()
        Me.LblL2         = New System.Windows.Forms.Label()
        Me.LblCat        = New System.Windows.Forms.Label()
        Me.LblEpoch      = New System.Windows.Forms.Label()
        Me.LblEpochDate  = New System.Windows.Forms.Label()
        Me.LblDecay      = New System.Windows.Forms.Label()
        Me.LblBSTAR      = New System.Windows.Forms.Label()
        Me.LblElement    = New System.Windows.Forms.Label()
        Me.LblIncl       = New System.Windows.Forms.Label()
        Me.LblRAAN       = New System.Windows.Forms.Label()
        Me.LblEcc        = New System.Windows.Forms.Label()
        Me.LblPer        = New System.Windows.Forms.Label()
        Me.LblAnom       = New System.Windows.Forms.Label()
        Me.LblMot        = New System.Windows.Forms.Label()
        Me.LblOrb        = New System.Windows.Forms.Label()

        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()

        ' ── MenuStrip ────────────────────────────────────────────
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuSave, Me.MnuClose})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)

        Me.MnuSave.Name = "MnuSave"
        Me.MnuSave.Text = "Save"

        Me.MnuClose.Name = "MnuClose"
        Me.MnuClose.Text = "Cancel"

        ' ── Helper for rows ──────────────────────────────────────
        Dim bF As New System.Drawing.Font("Arial", 9.0!, FontStyle.Bold)

        ' Row 0 — Satellite name + status
        Dim y0 As Integer = 30
        Me.LblSat.Location = New System.Drawing.Point(4, y0 + 3)
        Me.LblSat.Size = New System.Drawing.Size(80, 18)
        Me.LblSat.Text = "Satellite"
        Me.LblSat.Font = bF

        Me.TxtSatellite.Location = New System.Drawing.Point(88, y0)
        Me.TxtSatellite.Size = New System.Drawing.Size(200, 22)
        Me.TxtSatellite.Font = bF
        Me.TxtSatellite.ReadOnly = True

        Me.LblInvalido.Location = New System.Drawing.Point(300, y0 + 3)
        Me.LblInvalido.Size = New System.Drawing.Size(480, 18)
        Me.LblInvalido.Font = bF
        Me.LblInvalido.ForeColor = System.Drawing.Color.Red

        ' Row 1 — Line 1
        Dim y1 As Integer = 58
        Me.LblL1.Location = New System.Drawing.Point(4, y1 + 3)
        Me.LblL1.Size = New System.Drawing.Size(80, 18)
        Me.LblL1.Text = "Line 1"
        Me.LblL1.Font = bF

        Me.TxtLinea1.Location = New System.Drawing.Point(88, y1)
        Me.TxtLinea1.Size = New System.Drawing.Size(695, 22)
        Me.TxtLinea1.Font = bF

        ' Row 2 — Line 2
        Dim y2 As Integer = 86
        Me.LblL2.Location = New System.Drawing.Point(4, y2 + 3)
        Me.LblL2.Size = New System.Drawing.Size(80, 18)
        Me.LblL2.Text = "Line 2"
        Me.LblL2.Font = bF

        Me.TxtLinea2.Location = New System.Drawing.Point(88, y2)
        Me.TxtLinea2.Size = New System.Drawing.Size(695, 22)
        Me.TxtLinea2.Font = bF

        ' Row 3 — Catalog | Epoch | EpochDate
        Dim y3 As Integer = 114
        Me.LblCat.Location = New System.Drawing.Point(4, y3 + 3)
        Me.LblCat.Size = New System.Drawing.Size(56, 18)
        Me.LblCat.Text = "Cat. #"
        Me.LblCat.Font = bF
        Me.TxtCatalogo.Location = New System.Drawing.Point(64, y3)
        Me.TxtCatalogo.Size = New System.Drawing.Size(60, 22)
        Me.TxtCatalogo.Font = bF

        Me.LblEpoch.Location = New System.Drawing.Point(132, y3 + 3)
        Me.LblEpoch.Size = New System.Drawing.Size(50, 18)
        Me.LblEpoch.Text = "Epoch"
        Me.LblEpoch.Font = bF
        Me.TxtEpoch.Location = New System.Drawing.Point(186, y3)
        Me.TxtEpoch.Size = New System.Drawing.Size(120, 22)
        Me.TxtEpoch.Font = bF

        Me.LblEpochDate.Location = New System.Drawing.Point(314, y3 + 3)
        Me.LblEpochDate.Size = New System.Drawing.Size(70, 18)
        Me.LblEpochDate.Text = "Epoch Date"
        Me.LblEpochDate.Font = bF
        Me.TxtEpochDate.Location = New System.Drawing.Point(388, y3)
        Me.TxtEpochDate.Size = New System.Drawing.Size(180, 22)
        Me.TxtEpochDate.Font = bF
        Me.TxtEpochDate.ReadOnly = True

        ' Row 4 — Decay | BSTAR | Element #
        Dim y4 As Integer = 142
        Me.LblDecay.Location = New System.Drawing.Point(4, y4 + 3)
        Me.LblDecay.Size = New System.Drawing.Size(56, 18)
        Me.LblDecay.Text = "Decay"
        Me.LblDecay.Font = bF
        Me.TxtDecay.Location = New System.Drawing.Point(64, y4)
        Me.TxtDecay.Size = New System.Drawing.Size(100, 22)
        Me.TxtDecay.Font = bF

        Me.LblBSTAR.Location = New System.Drawing.Point(172, y4 + 3)
        Me.LblBSTAR.Size = New System.Drawing.Size(46, 18)
        Me.LblBSTAR.Text = "BSTAR"
        Me.LblBSTAR.Font = bF
        Me.TxtBSTAR.Location = New System.Drawing.Point(222, y4)
        Me.TxtBSTAR.Size = New System.Drawing.Size(100, 22)
        Me.TxtBSTAR.Font = bF

        Me.LblElement.Location = New System.Drawing.Point(330, y4 + 3)
        Me.LblElement.Size = New System.Drawing.Size(60, 18)
        Me.LblElement.Text = "Element #"
        Me.LblElement.Font = bF
        Me.TxtElement.Location = New System.Drawing.Point(394, y4)
        Me.TxtElement.Size = New System.Drawing.Size(60, 22)
        Me.TxtElement.Font = bF

        ' Row 5 — Inclination | RAAN | Eccentricity
        Dim y5 As Integer = 170
        Me.LblIncl.Location = New System.Drawing.Point(4, y5 + 3)
        Me.LblIncl.Size = New System.Drawing.Size(76, 18)
        Me.LblIncl.Text = "Inclination"
        Me.LblIncl.Font = bF
        Me.TxtInclination.Location = New System.Drawing.Point(84, y5)
        Me.TxtInclination.Size = New System.Drawing.Size(100, 22)
        Me.TxtInclination.Font = bF

        Me.LblRAAN.Location = New System.Drawing.Point(192, y5 + 3)
        Me.LblRAAN.Size = New System.Drawing.Size(40, 18)
        Me.LblRAAN.Text = "RAAN"
        Me.LblRAAN.Font = bF
        Me.TxtRAAN.Location = New System.Drawing.Point(236, y5)
        Me.TxtRAAN.Size = New System.Drawing.Size(100, 22)
        Me.TxtRAAN.Font = bF

        Me.LblEcc.Location = New System.Drawing.Point(344, y5 + 3)
        Me.LblEcc.Size = New System.Drawing.Size(84, 18)
        Me.LblEcc.Text = "Eccentricity"
        Me.LblEcc.Font = bF
        Me.TxtEccentricity.Location = New System.Drawing.Point(432, y5)
        Me.TxtEccentricity.Size = New System.Drawing.Size(100, 22)
        Me.TxtEccentricity.Font = bF

        ' Row 6 — Perigee | Anomaly | Motion | Rev@Epoch
        Dim y6 As Integer = 198
        Me.LblPer.Location = New System.Drawing.Point(4, y6 + 3)
        Me.LblPer.Size = New System.Drawing.Size(56, 18)
        Me.LblPer.Text = "Perigee"
        Me.LblPer.Font = bF
        Me.TxtPerigee.Location = New System.Drawing.Point(64, y6)
        Me.TxtPerigee.Size = New System.Drawing.Size(100, 22)
        Me.TxtPerigee.Font = bF

        Me.LblAnom.Location = New System.Drawing.Point(172, y6 + 3)
        Me.LblAnom.Size = New System.Drawing.Size(60, 18)
        Me.LblAnom.Text = "M. Anomaly"
        Me.LblAnom.Font = bF
        Me.TxtAnomaly.Location = New System.Drawing.Point(236, y6)
        Me.TxtAnomaly.Size = New System.Drawing.Size(100, 22)
        Me.TxtAnomaly.Font = bF

        Me.LblMot.Location = New System.Drawing.Point(344, y6 + 3)
        Me.LblMot.Size = New System.Drawing.Size(80, 18)
        Me.LblMot.Text = "Mean Motion"
        Me.LblMot.Font = bF
        Me.TxtMotion.Location = New System.Drawing.Point(428, y6)
        Me.TxtMotion.Size = New System.Drawing.Size(100, 22)
        Me.TxtMotion.Font = bF

        Me.LblOrb.Location = New System.Drawing.Point(536, y6 + 3)
        Me.LblOrb.Size = New System.Drawing.Size(70, 18)
        Me.LblOrb.Text = "Rev@Epoch"
        Me.LblOrb.Font = bF
        Me.TxtOrbite.Location = New System.Drawing.Point(610, y6)
        Me.TxtOrbite.Size = New System.Drawing.Size(80, 22)
        Me.TxtOrbite.Font = bF

        ' ── Form ──────────────────────────────────────────────────
        Me.MainMenuStrip = Me.MenuStrip1
        Me.ClientSize = New System.Drawing.Size(800, 232)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.LblSat)
        Me.Controls.Add(Me.TxtSatellite)
        Me.Controls.Add(Me.LblInvalido)
        Me.Controls.Add(Me.LblL1)
        Me.Controls.Add(Me.TxtLinea1)
        Me.Controls.Add(Me.LblL2)
        Me.Controls.Add(Me.TxtLinea2)
        Me.Controls.Add(Me.LblCat)
        Me.Controls.Add(Me.TxtCatalogo)
        Me.Controls.Add(Me.LblEpoch)
        Me.Controls.Add(Me.TxtEpoch)
        Me.Controls.Add(Me.LblEpochDate)
        Me.Controls.Add(Me.TxtEpochDate)
        Me.Controls.Add(Me.LblDecay)
        Me.Controls.Add(Me.TxtDecay)
        Me.Controls.Add(Me.LblBSTAR)
        Me.Controls.Add(Me.TxtBSTAR)
        Me.Controls.Add(Me.LblElement)
        Me.Controls.Add(Me.TxtElement)
        Me.Controls.Add(Me.LblIncl)
        Me.Controls.Add(Me.TxtInclination)
        Me.Controls.Add(Me.LblRAAN)
        Me.Controls.Add(Me.TxtRAAN)
        Me.Controls.Add(Me.LblEcc)
        Me.Controls.Add(Me.TxtEccentricity)
        Me.Controls.Add(Me.LblPer)
        Me.Controls.Add(Me.TxtPerigee)
        Me.Controls.Add(Me.LblAnom)
        Me.Controls.Add(Me.TxtAnomaly)
        Me.Controls.Add(Me.LblMot)
        Me.Controls.Add(Me.TxtMotion)
        Me.Controls.Add(Me.LblOrb)
        Me.Controls.Add(Me.TxtOrbite)
        Me.Name = "FrmNorad"
        Me.Text = "TLE Parameters"

        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    ' ── Campi aggiuntivi (label e menu — non nel vb) ─────────
    Friend WithEvents MenuStrip1  As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuSave     As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuClose    As System.Windows.Forms.ToolStripMenuItem
    Friend LblSat       As System.Windows.Forms.Label
    Friend LblL1        As System.Windows.Forms.Label
    Friend LblL2        As System.Windows.Forms.Label
    Friend LblCat       As System.Windows.Forms.Label
    Friend LblEpoch     As System.Windows.Forms.Label
    Friend LblEpochDate As System.Windows.Forms.Label
    Friend LblDecay     As System.Windows.Forms.Label
    Friend LblBSTAR     As System.Windows.Forms.Label
    Friend LblElement   As System.Windows.Forms.Label
    Friend LblIncl      As System.Windows.Forms.Label
    Friend LblRAAN      As System.Windows.Forms.Label
    Friend LblEcc       As System.Windows.Forms.Label
    Friend LblPer       As System.Windows.Forms.Label
    Friend LblAnom      As System.Windows.Forms.Label
    Friend LblMot       As System.Windows.Forms.Label
    Friend LblOrb       As System.Windows.Forms.Label
    ' TxtLinea1..TxtOrbite, TxtSatellite, LblInvalido dichiarati Friend WithEvents in frmNorad.vb

End Class
