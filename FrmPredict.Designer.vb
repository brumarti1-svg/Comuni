<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPredict
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.components    = New System.ComponentModel.Container()
        Me.tmrSim        = New System.Windows.Forms.Timer(Me.components)
        Me.picGrafico    = New System.Windows.Forms.PictureBox()
        Me.picOrbita     = New System.Windows.Forms.PictureBox()
        Me.lvwPredizioni = New System.Windows.Forms.ListView()
        Me.fraLog        = New System.Windows.Forms.GroupBox()
        Me.TxtLog        = New System.Windows.Forms.RichTextBox()
        Me.LblStatus     = New System.Windows.Forms.Label()
        Me.LblInfo       = New System.Windows.Forms.Label()
        ' Buffer group labels/controls
        Me.LblBufPunti    = New System.Windows.Forms.Label()
        Me.LblBufFinestra = New System.Windows.Forms.Label()
        Me.LblBufUltimo   = New System.Windows.Forms.Label()
        Me.TxtDecim       = New System.Windows.Forms.TextBox()
        Me.TxtBufOre      = New System.Windows.Forms.TextBox()
        Me.TxtRifitOgni   = New System.Windows.Forms.TextBox()
        Me.grpBuf         = New System.Windows.Forms.GroupBox()
        ' Model group labels/controls
        Me.LblModRMSE    = New System.Windows.Forms.Label()
        Me.LblModR2      = New System.Windows.Forms.Label()
        Me.LblModUpdate  = New System.Windows.Forms.Label()
        Me.TxtArm        = New System.Windows.Forms.TextBox()
        Me.grpMod        = New System.Windows.Forms.GroupBox()
        ' Predict group labels/controls
        Me.TxtTPred      = New System.Windows.Forms.TextBox()
        Me.TxtNPassi     = New System.Windows.Forms.TextBox()
        Me.TxtIntervallo = New System.Windows.Forms.TextBox()
        Me.LblPredAz     = New System.Windows.Forms.Label()
        Me.LblPredEl     = New System.Windows.Forms.Label()
        Me.LblPredConf   = New System.Windows.Forms.Label()
        Me.BtnPredici    = New System.Windows.Forms.Button()
        Me.BtnReset      = New System.Windows.Forms.Button()
        Me.grpPred       = New System.Windows.Forms.GroupBox()
        ' Sim / Feed group labels/controls
        Me.TxtTempo      = New System.Windows.Forms.TextBox()
        Me.TxtAzimuth    = New System.Windows.Forms.TextBox()
        Me.TxtElevazione = New System.Windows.Forms.TextBox()
        Me.BtnFeed       = New System.Windows.Forms.Button()
        Me.BtnSimStart   = New System.Windows.Forms.Button()
        Me.BtnSimStop    = New System.Windows.Forms.Button()
        Me.BtnEsempio    = New System.Windows.Forms.Button()
        Me.grpSim        = New System.Windows.Forms.GroupBox()
        ' Config group (placeholder)
        Me.grpCfg        = New System.Windows.Forms.GroupBox()
        ' Static labels
        Me.LblBufDecim   = New System.Windows.Forms.Label()
        Me.LblBufOre     = New System.Windows.Forms.Label()
        Me.LblBufRifit   = New System.Windows.Forms.Label()
        Me.LblModArm     = New System.Windows.Forms.Label()
        Me.LblPredTP     = New System.Windows.Forms.Label()
        Me.LblPredNP     = New System.Windows.Forms.Label()
        Me.LblPredIv     = New System.Windows.Forms.Label()
        Me.LblSimTm      = New System.Windows.Forms.Label()
        Me.LblSimAzF     = New System.Windows.Forms.Label()
        Me.LblSimElF     = New System.Windows.Forms.Label()
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picOrbita, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraLog.SuspendLayout()
        Me.grpBuf.SuspendLayout()
        Me.grpMod.SuspendLayout()
        Me.grpPred.SuspendLayout()
        Me.grpSim.SuspendLayout()
        Me.SuspendLayout()
        ' tmrSim
        Me.tmrSim.Interval = 500
        ' picGrafico (left, tall)
        Me.picGrafico.Location    = New System.Drawing.Point(4, 4)
        Me.picGrafico.Name        = "picGrafico"
        Me.picGrafico.Size        = New System.Drawing.Size(380, 260)
        Me.picGrafico.BackColor   = System.Drawing.Color.Black
        Me.picGrafico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        ' picOrbita (left bottom)
        Me.picOrbita.Location    = New System.Drawing.Point(4, 270)
        Me.picOrbita.Name        = "picOrbita"
        Me.picOrbita.Size        = New System.Drawing.Size(380, 200)
        Me.picOrbita.BackColor   = System.Drawing.Color.DarkSlateGray
        Me.picOrbita.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        ' lvwPredizioni
        Me.lvwPredizioni.Location      = New System.Drawing.Point(4, 476)
        Me.lvwPredizioni.Name          = "lvwPredizioni"
        Me.lvwPredizioni.Size          = New System.Drawing.Size(380, 100)
        Me.lvwPredizioni.View          = System.Windows.Forms.View.Details
        Me.lvwPredizioni.FullRowSelect = True
        Me.lvwPredizioni.Columns.Add("Tempo", 80)
        Me.lvwPredizioni.Columns.Add("Az",    80)
        Me.lvwPredizioni.Columns.Add("El",    80)
        Me.lvwPredizioni.Columns.Add("Conf",  80)
        ' Right panel — GroupBoxes stacked
        Dim rx As Integer = 392
        ' grpBuf
        Me.grpBuf.Location = New System.Drawing.Point(rx, 4)
        Me.grpBuf.Size     = New System.Drawing.Size(490, 80)
        Me.grpBuf.Text     = "Buffer"
        Me.LblBufPunti.Location    = New System.Drawing.Point(6, 18)
        Me.LblBufPunti.Size        = New System.Drawing.Size(240, 18)
        Me.LblBufPunti.Text        = "Punti: ---"
        Me.LblBufFinestra.Location = New System.Drawing.Point(6, 38)
        Me.LblBufFinestra.Size     = New System.Drawing.Size(240, 18)
        Me.LblBufFinestra.Text     = "Finestra: ---"
        Me.LblBufUltimo.Location   = New System.Drawing.Point(6, 58)
        Me.LblBufUltimo.Size       = New System.Drawing.Size(240, 18)
        Me.LblBufUltimo.Text       = "Ultimo: ---"
        Me.LblBufDecim.Location    = New System.Drawing.Point(256, 18)
        Me.LblBufDecim.Size        = New System.Drawing.Size(60, 18)
        Me.LblBufDecim.Text        = "Decim (s):"
        Me.TxtDecim.Location       = New System.Drawing.Point(320, 16)
        Me.TxtDecim.Size           = New System.Drawing.Size(60, 22)
        Me.TxtDecim.Text           = "30"
        Me.LblBufOre.Location      = New System.Drawing.Point(256, 40)
        Me.LblBufOre.Size          = New System.Drawing.Size(60, 18)
        Me.LblBufOre.Text          = "Ore buf:"
        Me.TxtBufOre.Location      = New System.Drawing.Point(320, 38)
        Me.TxtBufOre.Size          = New System.Drawing.Size(60, 22)
        Me.TxtBufOre.Text          = "3"
        Me.LblBufRifit.Location    = New System.Drawing.Point(256, 58)
        Me.LblBufRifit.Size        = New System.Drawing.Size(60, 18)
        Me.LblBufRifit.Text        = "Rifit ogni:"
        Me.TxtRifitOgni.Location   = New System.Drawing.Point(320, 56)
        Me.TxtRifitOgni.Size       = New System.Drawing.Size(60, 22)
        Me.TxtRifitOgni.Text       = "5"
        Me.grpBuf.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.LblBufPunti, Me.LblBufFinestra, Me.LblBufUltimo,
            Me.LblBufDecim, Me.TxtDecim, Me.LblBufOre, Me.TxtBufOre, Me.LblBufRifit, Me.TxtRifitOgni})
        ' grpMod
        Me.grpMod.Location = New System.Drawing.Point(rx, 90)
        Me.grpMod.Size     = New System.Drawing.Size(490, 80)
        Me.grpMod.Text     = "Modello"
        Me.LblModRMSE.Location   = New System.Drawing.Point(6, 18)
        Me.LblModRMSE.Size       = New System.Drawing.Size(200, 18)
        Me.LblModRMSE.Text       = "RMSE: ---"
        Me.LblModR2.Location     = New System.Drawing.Point(6, 38)
        Me.LblModR2.Size         = New System.Drawing.Size(200, 18)
        Me.LblModR2.Text         = "R²: ---"
        Me.LblModUpdate.Location = New System.Drawing.Point(6, 58)
        Me.LblModUpdate.Size     = New System.Drawing.Size(200, 18)
        Me.LblModUpdate.Text     = "Agg.: ---"
        Me.LblModArm.Location    = New System.Drawing.Point(256, 18)
        Me.LblModArm.Size        = New System.Drawing.Size(60, 18)
        Me.LblModArm.Text        = "Armoniche:"
        Me.TxtArm.Location       = New System.Drawing.Point(320, 16)
        Me.TxtArm.Size           = New System.Drawing.Size(60, 22)
        Me.TxtArm.Text           = "3"
        Me.grpMod.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.LblModRMSE, Me.LblModR2, Me.LblModUpdate, Me.LblModArm, Me.TxtArm})
        ' grpPred
        Me.grpPred.Location = New System.Drawing.Point(rx, 176)
        Me.grpPred.Size     = New System.Drawing.Size(490, 110)
        Me.grpPred.Text     = "Predizione"
        Me.LblPredTP.Location  = New System.Drawing.Point(6, 20)
        Me.LblPredTP.Size      = New System.Drawing.Size(80, 18)
        Me.LblPredTP.Text      = "T pred (min):"
        Me.TxtTPred.Location   = New System.Drawing.Point(90, 18)
        Me.TxtTPred.Size       = New System.Drawing.Size(60, 22)
        Me.TxtTPred.Text       = "15"
        Me.LblPredNP.Location  = New System.Drawing.Point(6, 46)
        Me.LblPredNP.Size      = New System.Drawing.Size(80, 18)
        Me.LblPredNP.Text      = "N passi:"
        Me.TxtNPassi.Location  = New System.Drawing.Point(90, 44)
        Me.TxtNPassi.Size      = New System.Drawing.Size(60, 22)
        Me.TxtNPassi.Text      = "5"
        Me.LblPredIv.Location  = New System.Drawing.Point(6, 72)
        Me.LblPredIv.Size      = New System.Drawing.Size(80, 18)
        Me.LblPredIv.Text      = "Intervallo:"
        Me.TxtIntervallo.Location = New System.Drawing.Point(90, 70)
        Me.TxtIntervallo.Size  = New System.Drawing.Size(60, 22)
        Me.TxtIntervallo.Text  = "60"
        Me.BtnPredici.Location  = New System.Drawing.Point(160, 18)
        Me.BtnPredici.Name      = "BtnPredici"
        Me.BtnPredici.Size      = New System.Drawing.Size(80, 74)
        Me.BtnPredici.Text      = "Predici"
        Me.BtnPredici.BackColor = System.Drawing.Color.LightBlue
        Me.LblPredAz.Location   = New System.Drawing.Point(250, 18)
        Me.LblPredAz.Size       = New System.Drawing.Size(220, 18)
        Me.LblPredAz.Text       = "Az: ---"
        Me.LblPredEl.Location   = New System.Drawing.Point(250, 38)
        Me.LblPredEl.Size       = New System.Drawing.Size(220, 18)
        Me.LblPredEl.Text       = "El: ---"
        Me.LblPredConf.Location = New System.Drawing.Point(250, 58)
        Me.LblPredConf.Size     = New System.Drawing.Size(220, 18)
        Me.LblPredConf.Text     = "Conf: ---"
        Me.grpPred.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.LblPredTP, Me.TxtTPred, Me.LblPredNP, Me.TxtNPassi, Me.LblPredIv, Me.TxtIntervallo,
            Me.BtnPredici, Me.LblPredAz, Me.LblPredEl, Me.LblPredConf})
        ' grpSim
        Me.grpSim.Location = New System.Drawing.Point(rx, 292)
        Me.grpSim.Size     = New System.Drawing.Size(490, 110)
        Me.grpSim.Text     = "Feed / Simulazione"
        Me.LblSimTm.Location     = New System.Drawing.Point(6, 20)
        Me.LblSimTm.Size         = New System.Drawing.Size(60, 18)
        Me.LblSimTm.Text         = "Tempo:"
        Me.TxtTempo.Location     = New System.Drawing.Point(70, 18)
        Me.TxtTempo.Size         = New System.Drawing.Size(80, 22)
        Me.TxtTempo.Text         = "0"
        Me.LblSimAzF.Location    = New System.Drawing.Point(6, 46)
        Me.LblSimAzF.Size        = New System.Drawing.Size(60, 18)
        Me.LblSimAzF.Text        = "Azimuth:"
        Me.TxtAzimuth.Location   = New System.Drawing.Point(70, 44)
        Me.TxtAzimuth.Size       = New System.Drawing.Size(80, 22)
        Me.TxtAzimuth.Text       = "0"
        Me.LblSimElF.Location    = New System.Drawing.Point(6, 72)
        Me.LblSimElF.Size        = New System.Drawing.Size(60, 18)
        Me.LblSimElF.Text        = "Elevaz.:"
        Me.TxtElevazione.Location = New System.Drawing.Point(70, 70)
        Me.TxtElevazione.Size    = New System.Drawing.Size(80, 22)
        Me.TxtElevazione.Text    = "0"
        Me.BtnFeed.Location      = New System.Drawing.Point(160, 18)
        Me.BtnFeed.Name          = "BtnFeed"
        Me.BtnFeed.Size          = New System.Drawing.Size(70, 26)
        Me.BtnFeed.Text          = "Feed"
        Me.BtnFeed.BackColor     = System.Drawing.Color.LightGreen
        Me.BtnSimStart.Location  = New System.Drawing.Point(236, 18)
        Me.BtnSimStart.Name      = "BtnSimStart"
        Me.BtnSimStart.Size      = New System.Drawing.Size(70, 26)
        Me.BtnSimStart.Text      = "Sim Start"
        Me.BtnSimStart.BackColor = System.Drawing.Color.LightYellow
        Me.BtnSimStop.Location   = New System.Drawing.Point(312, 18)
        Me.BtnSimStop.Name       = "BtnSimStop"
        Me.BtnSimStop.Size       = New System.Drawing.Size(70, 26)
        Me.BtnSimStop.Text       = "Sim Stop"
        Me.BtnSimStop.BackColor  = System.Drawing.Color.Orange
        Me.BtnEsempio.Location   = New System.Drawing.Point(388, 18)
        Me.BtnEsempio.Name       = "BtnEsempio"
        Me.BtnEsempio.Size       = New System.Drawing.Size(70, 26)
        Me.BtnEsempio.Text       = "Esempio"
        Me.BtnReset.Location     = New System.Drawing.Point(388, 50)
        Me.BtnReset.Name         = "BtnReset"
        Me.BtnReset.Size         = New System.Drawing.Size(70, 26)
        Me.BtnReset.Text         = "Reset"
        Me.BtnReset.BackColor    = System.Drawing.Color.Salmon
        Me.grpSim.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.LblSimTm, Me.TxtTempo, Me.LblSimAzF, Me.TxtAzimuth, Me.LblSimElF, Me.TxtElevazione,
            Me.BtnFeed, Me.BtnSimStart, Me.BtnSimStop, Me.BtnEsempio, Me.BtnReset})
        ' fraLog
        Me.fraLog.Location = New System.Drawing.Point(rx, 408)
        Me.fraLog.Name     = "fraLog"
        Me.fraLog.Size     = New System.Drawing.Size(490, 168)
        Me.fraLog.Text     = "Log"
        Me.TxtLog.Dock      = System.Windows.Forms.DockStyle.Fill
        Me.TxtLog.Name      = "TxtLog"
        Me.TxtLog.ReadOnly  = True
        Me.TxtLog.BackColor = System.Drawing.Color.Black
        Me.TxtLog.ForeColor = System.Drawing.Color.Lime
        Me.TxtLog.Font      = New System.Drawing.Font("Courier New", 8)
        Me.fraLog.Controls.Add(Me.TxtLog)
        ' LblStatus / LblInfo
        Me.LblStatus.Location  = New System.Drawing.Point(rx, 582)
        Me.LblStatus.Size      = New System.Drawing.Size(490, 18)
        Me.LblStatus.Text      = ""
        Me.LblStatus.BackColor = System.Drawing.Color.LightYellow
        Me.LblInfo.Location    = New System.Drawing.Point(4, 582)
        Me.LblInfo.Size        = New System.Drawing.Size(380, 18)
        Me.LblInfo.Text        = ""
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize          = New System.Drawing.Size(886, 604)
        Me.Name = "FrmPredict"
        Me.Text = "Predict — Predizione Orbita"
        Me.Controls.AddRange(New System.Windows.Forms.Control() {
            Me.picGrafico, Me.picOrbita, Me.lvwPredizioni,
            Me.grpBuf, Me.grpMod, Me.grpPred, Me.grpSim,
            Me.fraLog, Me.LblStatus, Me.LblInfo})
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picOrbita, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBuf.ResumeLayout(False)
        Me.grpMod.ResumeLayout(False)
        Me.grpPred.ResumeLayout(False)
        Me.grpSim.ResumeLayout(False)
        Me.fraLog.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    ' Controls referenced in Handles clauses
    Friend WithEvents BtnPredici  As System.Windows.Forms.Button
    Friend WithEvents BtnEsempio  As System.Windows.Forms.Button
    Friend WithEvents BtnFeed     As System.Windows.Forms.Button
    Friend WithEvents BtnSimStart As System.Windows.Forms.Button
    Friend WithEvents BtnSimStop  As System.Windows.Forms.Button
    Friend WithEvents BtnReset    As System.Windows.Forms.Button
    Friend WithEvents tmrSim      As System.Windows.Forms.Timer
    Friend WithEvents TxtDecim    As System.Windows.Forms.TextBox
    Friend WithEvents TxtBufOre   As System.Windows.Forms.TextBox
    Friend WithEvents TxtRifitOgni  As System.Windows.Forms.TextBox
    Friend WithEvents TxtElevazione As System.Windows.Forms.TextBox
    Friend WithEvents lvwPredizioni As System.Windows.Forms.ListView

    ' Display controls
    Friend picGrafico    As System.Windows.Forms.PictureBox
    Friend picOrbita     As System.Windows.Forms.PictureBox
    Friend LblInfo       As System.Windows.Forms.Label
    Friend LblBufPunti   As System.Windows.Forms.Label
    Friend LblBufFinestra As System.Windows.Forms.Label
    Friend LblBufUltimo  As System.Windows.Forms.Label
    Friend TxtArm        As System.Windows.Forms.TextBox
    Friend LblModRMSE    As System.Windows.Forms.Label
    Friend LblModR2      As System.Windows.Forms.Label
    Friend LblModUpdate  As System.Windows.Forms.Label
    Friend TxtTPred      As System.Windows.Forms.TextBox
    Friend TxtNPassi     As System.Windows.Forms.TextBox
    Friend TxtIntervallo As System.Windows.Forms.TextBox
    Friend LblPredAz     As System.Windows.Forms.Label
    Friend LblPredEl     As System.Windows.Forms.Label
    Friend LblPredConf   As System.Windows.Forms.Label
    Friend TxtTempo      As System.Windows.Forms.TextBox
    Friend TxtAzimuth    As System.Windows.Forms.TextBox
    Friend TxtLog        As System.Windows.Forms.RichTextBox
    Friend LblStatus     As System.Windows.Forms.Label
    Friend fraLog        As System.Windows.Forms.GroupBox
    ' GroupBoxes
    Friend grpBuf  As System.Windows.Forms.GroupBox
    Friend grpMod  As System.Windows.Forms.GroupBox
    Friend grpPred As System.Windows.Forms.GroupBox
    Friend grpSim  As System.Windows.Forms.GroupBox
    Friend grpCfg  As System.Windows.Forms.GroupBox
    ' Static labels
    Friend LblBufDecim As System.Windows.Forms.Label
    Friend LblBufOre   As System.Windows.Forms.Label
    Friend LblBufRifit As System.Windows.Forms.Label
    Friend LblModArm   As System.Windows.Forms.Label
    Friend LblPredTP   As System.Windows.Forms.Label
    Friend LblPredNP   As System.Windows.Forms.Label
    Friend LblPredIv   As System.Windows.Forms.Label
    Friend LblSimTm    As System.Windows.Forms.Label
    Friend LblSimAzF   As System.Windows.Forms.Label
    Friend LblSimElF   As System.Windows.Forms.Label

End Class
