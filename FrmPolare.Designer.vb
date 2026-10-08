<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPolare
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
        Me.picGrafico = New System.Windows.Forms.PictureBox()
        Me.CmdEsci = New System.Windows.Forms.Button()
        Me.tmrPolare = New System.Windows.Forms.Timer(Me.components)
        Me.pnlFiltro = New System.Windows.Forms.Panel()
        Me.LblDal = New System.Windows.Forms.Label()
        Me.mskDataIni = New System.Windows.Forms.MaskedTextBox()
        Me.mskOraIni = New System.Windows.Forms.MaskedTextBox()
        Me.LblAl = New System.Windows.Forms.Label()
        Me.mskDataFin = New System.Windows.Forms.MaskedTextBox()
        Me.mskOraFin = New System.Windows.Forms.MaskedTextBox()
        Me.BtnFiltra = New System.Windows.Forms.Button()
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFiltro.SuspendLayout()
        Me.SuspendLayout()
        '
        'picGrafico
        '
        Me.picGrafico.BackColor = System.Drawing.Color.White
        Me.picGrafico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picGrafico.Location = New System.Drawing.Point(0, 36)
        Me.picGrafico.Name = "picGrafico"
        Me.picGrafico.Size = New System.Drawing.Size(1029, 411)
        Me.picGrafico.TabIndex = 0
        Me.picGrafico.TabStop = False
        '
        'pnlFiltro
        '
        ' Riga di filtro Dal/Al in un pannello Dock=Top, aggiunto PRIMA di
        ' picGrafico (Dock=Fill): così il grafico riempie lo spazio
        ' rimanente sotto, senza mai coprire questi controlli — compreso
        ' CmdEsci, spostato qui dentro per essere sempre visibile (prima
        ' era ancorato in basso a destra, ma con la finestra massimizzata
        ' e il grafico a tutto schermo risultava difficile da notare).
        Me.pnlFiltro.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFiltro.Size = New System.Drawing.Size(1029, 36)
        Me.pnlFiltro.Controls.Add(Me.LblDal)
        Me.pnlFiltro.Controls.Add(Me.mskDataIni)
        Me.pnlFiltro.Controls.Add(Me.mskOraIni)
        Me.pnlFiltro.Controls.Add(Me.LblAl)
        Me.pnlFiltro.Controls.Add(Me.mskDataFin)
        Me.pnlFiltro.Controls.Add(Me.mskOraFin)
        Me.pnlFiltro.Controls.Add(Me.BtnFiltra)
        Me.pnlFiltro.Controls.Add(Me.CmdEsci)
        '
        'LblDal
        '
        Me.LblDal.AutoSize = True
        Me.LblDal.Location = New System.Drawing.Point(8, 11)
        Me.LblDal.Name = "LblDal"
        Me.LblDal.Size = New System.Drawing.Size(28, 15)
        Me.LblDal.Text = "Dal:"
        '
        'mskDataIni
        '
        Me.mskDataIni.Location = New System.Drawing.Point(42, 7)
        Me.mskDataIni.Mask = "00/00/0000"
        Me.mskDataIni.Name = "mskDataIni"
        Me.mskDataIni.Size = New System.Drawing.Size(80, 22)
        Me.mskDataIni.TabIndex = 0
        '
        'mskOraIni
        '
        Me.mskOraIni.Location = New System.Drawing.Point(126, 7)
        Me.mskOraIni.Mask = "00\.00"
        Me.mskOraIni.Name = "mskOraIni"
        Me.mskOraIni.Size = New System.Drawing.Size(44, 22)
        Me.mskOraIni.TabIndex = 1
        '
        'LblAl
        '
        Me.LblAl.AutoSize = True
        Me.LblAl.Location = New System.Drawing.Point(182, 11)
        Me.LblAl.Name = "LblAl"
        Me.LblAl.Size = New System.Drawing.Size(21, 15)
        Me.LblAl.Text = "Al:"
        '
        'mskDataFin
        '
        Me.mskDataFin.Location = New System.Drawing.Point(209, 7)
        Me.mskDataFin.Mask = "00/00/0000"
        Me.mskDataFin.Name = "mskDataFin"
        Me.mskDataFin.Size = New System.Drawing.Size(80, 22)
        Me.mskDataFin.TabIndex = 2
        '
        'mskOraFin
        '
        Me.mskOraFin.Location = New System.Drawing.Point(293, 7)
        Me.mskOraFin.Mask = "00\.00"
        Me.mskOraFin.Name = "mskOraFin"
        Me.mskOraFin.Size = New System.Drawing.Size(44, 22)
        Me.mskOraFin.TabIndex = 3
        '
        'BtnFiltra
        '
        Me.BtnFiltra.Location = New System.Drawing.Point(349, 6)
        Me.BtnFiltra.Name = "BtnFiltra"
        Me.BtnFiltra.Size = New System.Drawing.Size(80, 24)
        Me.BtnFiltra.TabIndex = 4
        Me.BtnFiltra.Text = "Elabora"
        '
        'CmdEsci
        '
        Me.CmdEsci.Location = New System.Drawing.Point(941, 6)
        Me.CmdEsci.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CmdEsci.Name = "CmdEsci"
        Me.CmdEsci.Size = New System.Drawing.Size(80, 24)
        Me.CmdEsci.TabIndex = 5
        Me.CmdEsci.Text = "Exit"
        '
        'tmrPolare
        '
        Me.tmrPolare.Interval = 1000
        '
        'FrmPolare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1040, 483)
        Me.Controls.Add(Me.pnlFiltro)
        Me.Controls.Add(Me.picGrafico)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "FrmPolare"
        Me.Text = "Data Analysis (Polar)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFiltro.ResumeLayout(False)
        Me.pnlFiltro.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picGrafico As System.Windows.Forms.PictureBox
    Friend WithEvents CmdEsci As System.Windows.Forms.Button
    Friend WithEvents tmrPolare As System.Windows.Forms.Timer
    Friend WithEvents pnlFiltro As System.Windows.Forms.Panel
    Friend WithEvents LblDal As System.Windows.Forms.Label
    Friend WithEvents mskDataIni As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskOraIni As System.Windows.Forms.MaskedTextBox
    Friend WithEvents LblAl As System.Windows.Forms.Label
    Friend WithEvents mskDataFin As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskOraFin As System.Windows.Forms.MaskedTextBox
    Friend WithEvents BtnFiltra As System.Windows.Forms.Button

End Class
