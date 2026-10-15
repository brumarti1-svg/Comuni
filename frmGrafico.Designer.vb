' Generato da frmGrafico.frm (VB6) — coordinate convertite: twips / 15 = pixel
' Nessun menu VB6 originale → nessun MenuStrip.  TxtIntervallo recuperato dal .frm.
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGrafico
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
        Me.components    = New System.ComponentModel.Container()
        Me.picGrafico    = New System.Windows.Forms.PictureBox()
        Me.TmrGrafico    = New System.Windows.Forms.Timer(Me.components)
        Me.TxtMin        = New System.Windows.Forms.TextBox()
        Me.TxtMax        = New System.Windows.Forms.TextBox()
        Me.LblAntenna    = New System.Windows.Forms.Label()
        Me.CmbAssex      = New System.Windows.Forms.ComboBox()
        Me.CmdEsci       = New System.Windows.Forms.Button()
        Me.CmdScale      = New System.Windows.Forms.Button()
        Me.LblLev        = New System.Windows.Forms.Label()
        Me.TxtIntervallo = New System.Windows.Forms.TextBox()
        ' Static labels
        Dim LblAntTag    As New System.Windows.Forms.Label()  ' Label3 "Antenna"
        Dim LblInputLev  As New System.Windows.Forms.Label()  ' Label5 "Input Level"
        Dim LblAsseX     As New System.Windows.Forms.Label()  ' Label6 "Asse x"
        Dim LblMinutes   As New System.Windows.Forms.Label()  ' Label7 "minutes"
        Dim LblInterval  As New System.Windows.Forms.Label()  ' Label2 "Interval"
        Dim LblMin       As New System.Windows.Forms.Label()  ' Label8 "min"
        Dim LblMax       As New System.Windows.Forms.Label()  ' Label9 "max"
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        ' picGrafico (Left=72/15=5, Top=540/15=36, Width=9888/15=659, Height=4728/15=315)
        '
        Me.picGrafico.Anchor      = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.picGrafico.BackColor   = System.Drawing.Color.FromArgb(224, 224, 224)
        Me.picGrafico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picGrafico.Location    = New System.Drawing.Point(5, 36)
        Me.picGrafico.Size        = New System.Drawing.Size(659, 315)
        Me.picGrafico.TabStop     = False
        Me.picGrafico.Name        = "picGrafico"
        '
        ' tmrGrafico (Interval=1000)
        '
        Me.TmrGrafico.Interval = 1000
        '
        ' Row 1 header: Antenna label / LblAntenna / Input Level / LblLev / Asse x / CmbAssex / minutes
        '
        ' Label3 "Antenna" (Left=180/15=12, Top=144/15=10, Width=888/15=59, H=264/15=18, RightAlign)
        LblAntTag.Location  = New System.Drawing.Point(12, 10)
        LblAntTag.Size      = New System.Drawing.Size(59, 18)
        LblAntTag.Text      = "Antenna"
        LblAntTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        ' LblAntenna (Left=1188/15=79, Top=72/15=5, Width=2376/15=158, H=408/15=27, Center, Border)
        Me.LblAntenna.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblAntenna.Location    = New System.Drawing.Point(79, 5)
        Me.LblAntenna.Size        = New System.Drawing.Size(158, 27)
        Me.LblAntenna.TextAlign   = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblAntenna.Font        = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold)
        Me.LblAntenna.Name        = "LblAntenna"
        ' Label5 "Input Level" (Left=3816/15=254, Top=144/15=10, Width=1068/15=71, RightAlign)
        LblInputLev.Location  = New System.Drawing.Point(254, 10)
        LblInputLev.Size      = New System.Drawing.Size(71, 18)
        LblInputLev.Text      = "Input Level"
        LblInputLev.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        ' LblLev (Left=5076/15=338, Top=144/15=10, Width=936/15=62, H=264/15=18, Center, Border)
        Me.LblLev.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblLev.Location    = New System.Drawing.Point(338, 10)
        Me.LblLev.Size        = New System.Drawing.Size(62, 18)
        Me.LblLev.TextAlign   = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblLev.Name        = "LblLev"
        ' Label6 "Asse x" (Left=6372/15=425, Top=144/15=10, Width=912/15=61, RightAlign)
        LblAsseX.Location  = New System.Drawing.Point(425, 10)
        LblAsseX.Size      = New System.Drawing.Size(61, 18)
        LblAsseX.Text      = "Asse x"
        LblAsseX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        ' CmbAssex (Left=7380/15=492, Top=114/15=8, Width=984/15=66, H=324/15=22)
        Me.CmbAssex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.CmbAssex.Items.AddRange(New Object() {"2", "5", "10", "30", "60"})
        Me.CmbAssex.Location  = New System.Drawing.Point(492, 8)
        Me.CmbAssex.Size      = New System.Drawing.Size(66, 22)
        Me.CmbAssex.Font      = New System.Drawing.Font("Arial", 10.2!)
        Me.CmbAssex.TabIndex  = 2
        Me.CmbAssex.Name      = "CmbAssex"
        ' Label7 "minutes" (Left=8532/15=569, Top=144/15=10, Width=948/15=63)
        LblMinutes.Location = New System.Drawing.Point(569, 10)
        LblMinutes.Size     = New System.Drawing.Size(63, 18)
        LblMinutes.Text     = "minutes"
        '
        ' Row 2 bottom: CmdScale / TxtIntervallo / Label2 "Interval" / TxtMin / Label8 "min"
        '              / TxtMax / Label9 "max" / CmdEsci
        '
        ' CmdScale (Left=180/15=12, Top=5544/15=370, Width=828/15=55, H=495/15=33)
        Me.CmdScale.Anchor   = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        Me.CmdScale.Location = New System.Drawing.Point(12, 370)
        Me.CmdScale.Size     = New System.Drawing.Size(55, 33)
        Me.CmdScale.Text     = "Scale"
        Me.CmdScale.Font     = New System.Drawing.Font("Arial", 10.2!)
        Me.CmdScale.TabIndex = 4
        Me.CmdScale.Name     = "CmdScale"
        ' TxtIntervallo (Left=2808/15=187, Top=5508/15=367, Width=696/15=46, H=336/15=22)
        Me.TxtIntervallo.Anchor    = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        Me.TxtIntervallo.Location  = New System.Drawing.Point(187, 367)
        Me.TxtIntervallo.Size      = New System.Drawing.Size(46, 22)
        Me.TxtIntervallo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtIntervallo.Font      = New System.Drawing.Font("Arial", 10.2!)
        Me.TxtIntervallo.TabIndex  = 3
        Me.TxtIntervallo.Name      = "TxtIntervallo"
        Me.TxtIntervallo.ReadOnly  = True
        Me.TxtIntervallo.TabStop   = False
        ' Label2 "Interval" (Left=1368/15=91, Top=5544/15=370, Width=1140/15=76, RightAlign)
        LblInterval.Anchor    = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        LblInterval.Location  = New System.Drawing.Point(91, 370)
        LblInterval.Size      = New System.Drawing.Size(76, 17)
        LblInterval.Text      = "Interval"
        LblInterval.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        ' TxtMin (Left=2052/15=137, Top=5976/15=399, Width=768/15=51, H=336/15=22)
        Me.TxtMin.Anchor   = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        Me.TxtMin.Location  = New System.Drawing.Point(137, 399)
        Me.TxtMin.Size      = New System.Drawing.Size(51, 22)
        Me.TxtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtMin.Font      = New System.Drawing.Font("Arial", 10.2!)
        Me.TxtMin.TabIndex  = 6
        Me.TxtMin.Name      = "TxtMin"
        ' Label8 "min" (Left=1296/15=86, Top=6012/15=401, Width=636/15=42, RightAlign)
        LblMin.Anchor    = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        LblMin.Location  = New System.Drawing.Point(86, 401)
        LblMin.Size      = New System.Drawing.Size(42, 17)
        LblMin.Text      = "min"
        LblMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        ' TxtMax (Left=3744/15=250, Top=5976/15=399)
        Me.TxtMax.Anchor   = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        Me.TxtMax.Location  = New System.Drawing.Point(250, 399)
        Me.TxtMax.Size      = New System.Drawing.Size(51, 22)
        Me.TxtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtMax.Font      = New System.Drawing.Font("Arial", 10.2!)
        Me.TxtMax.TabIndex  = 5
        Me.TxtMax.Name      = "TxtMax"
        ' Label9 "max"
        LblMax.Anchor    = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        LblMax.Location  = New System.Drawing.Point(206, 401)
        LblMax.Size      = New System.Drawing.Size(42, 17)
        LblMax.Text      = "max"
        LblMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        ' CmdEsci (Left=7848/15=523, Top=5652/15=377, Width=1080/15=72, H=444/15=30)
        Me.CmdEsci.Anchor   = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.CmdEsci.Location = New System.Drawing.Point(523, 377)
        Me.CmdEsci.Size     = New System.Drawing.Size(72, 30)
        Me.CmdEsci.Text     = "Exit"
        Me.CmdEsci.Font     = New System.Drawing.Font("Arial", 10.8!, System.Drawing.FontStyle.Bold)
        Me.CmdEsci.TabIndex = 1
        Me.CmdEsci.Name     = "CmdEsci"
        '
        ' frmGrafico (ClientWidth=10104/15=674, ClientHeight=6636/15=443)
        '
        Me.ClientSize  = New System.Drawing.Size(674, 443)
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.Add(Me.picGrafico)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {
            LblAntTag, Me.LblAntenna, LblInputLev, Me.LblLev,
            LblAsseX, Me.CmbAssex, LblMinutes,
            Me.CmdScale, Me.TxtIntervallo, LblInterval,
            LblMin, Me.TxtMin, LblMax, Me.TxtMax,
            Me.CmdEsci})
        Me.Name  = "frmGrafico"
        Me.Text  = "Beacon Graphic"
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend picGrafico              As System.Windows.Forms.PictureBox
    Friend WithEvents TmrGrafico   As System.Windows.Forms.Timer
    Friend WithEvents TxtMin       As System.Windows.Forms.TextBox
    Friend WithEvents TxtMax       As System.Windows.Forms.TextBox
    Friend LblAntenna              As System.Windows.Forms.Label
    Friend WithEvents CmbAssex     As System.Windows.Forms.ComboBox
    Friend WithEvents CmdEsci      As System.Windows.Forms.Button
    Friend WithEvents CmdScale     As System.Windows.Forms.Button
    Friend LblLev                  As System.Windows.Forms.Label
    Friend WithEvents TxtIntervallo As System.Windows.Forms.TextBox

End Class
