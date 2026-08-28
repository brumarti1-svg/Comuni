<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmStampaLog
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
        Me.LblDataIni = New System.Windows.Forms.Label()
        Me.TxtDataIni = New System.Windows.Forms.TextBox()
        Me.LblOraIni = New System.Windows.Forms.Label()
        Me.TxtOraIni = New System.Windows.Forms.TextBox()
        Me.LblDataFin = New System.Windows.Forms.Label()
        Me.TxtDataFin = New System.Windows.Forms.TextBox()
        Me.LblOraFin = New System.Windows.Forms.Label()
        Me.TxtOraFin = New System.Windows.Forms.TextBox()
        Me.radAll = New System.Windows.Forms.RadioButton()
        Me.rad0 = New System.Windows.Forms.RadioButton()
        Me.rad1 = New System.Windows.Forms.RadioButton()
        Me.CmdRicerca = New System.Windows.Forms.Button()
        Me.CmdEsci = New System.Windows.Forms.Button()
        Me.dgvLog = New System.Windows.Forms.DataGridView()
        CType(Me.dgvLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        ' LblDataIni
        '
        Me.LblDataIni.Location = New System.Drawing.Point(12, 18)
        Me.LblDataIni.Name = "LblDataIni"
        Me.LblDataIni.Size = New System.Drawing.Size(80, 20)
        Me.LblDataIni.Text = "Data Inizio:"
        '
        ' TxtDataIni
        '
        Me.TxtDataIni.Location = New System.Drawing.Point(95, 15)
        Me.TxtDataIni.Name = "TxtDataIni"
        Me.TxtDataIni.Size = New System.Drawing.Size(90, 20)
        '
        ' LblOraIni
        '
        Me.LblOraIni.Location = New System.Drawing.Point(200, 18)
        Me.LblOraIni.Name = "LblOraIni"
        Me.LblOraIni.Size = New System.Drawing.Size(70, 20)
        Me.LblOraIni.Text = "Ora Inizio:"
        '
        ' TxtOraIni
        '
        Me.TxtOraIni.Location = New System.Drawing.Point(275, 15)
        Me.TxtOraIni.Name = "TxtOraIni"
        Me.TxtOraIni.Size = New System.Drawing.Size(60, 20)
        '
        ' LblDataFin
        '
        Me.LblDataFin.Location = New System.Drawing.Point(12, 52)
        Me.LblDataFin.Name = "LblDataFin"
        Me.LblDataFin.Size = New System.Drawing.Size(80, 20)
        Me.LblDataFin.Text = "Data Fine:"
        '
        ' TxtDataFin
        '
        Me.TxtDataFin.Location = New System.Drawing.Point(95, 49)
        Me.TxtDataFin.Name = "TxtDataFin"
        Me.TxtDataFin.Size = New System.Drawing.Size(90, 20)
        '
        ' LblOraFin
        '
        Me.LblOraFin.Location = New System.Drawing.Point(200, 52)
        Me.LblOraFin.Name = "LblOraFin"
        Me.LblOraFin.Size = New System.Drawing.Size(70, 20)
        Me.LblOraFin.Text = "Ora Fine:"
        '
        ' TxtOraFin
        '
        Me.TxtOraFin.Location = New System.Drawing.Point(275, 49)
        Me.TxtOraFin.Name = "TxtOraFin"
        Me.TxtOraFin.Size = New System.Drawing.Size(60, 20)
        '
        ' radAll
        '
        Me.radAll.Location = New System.Drawing.Point(12, 85)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(80, 20)
        Me.radAll.Text = "Tutti"
        '
        ' rad0
        '
        Me.rad0.Location = New System.Drawing.Point(100, 85)
        Me.rad0.Name = "rad0"
        Me.rad0.Size = New System.Drawing.Size(90, 20)
        Me.rad0.Text = "Livello 0"
        '
        ' rad1
        '
        Me.rad1.Location = New System.Drawing.Point(200, 85)
        Me.rad1.Name = "rad1"
        Me.rad1.Size = New System.Drawing.Size(90, 20)
        Me.rad1.Text = "Livello 1"
        '
        ' CmdRicerca
        '
        Me.CmdRicerca.Location = New System.Drawing.Point(12, 115)
        Me.CmdRicerca.Name = "CmdRicerca"
        Me.CmdRicerca.Size = New System.Drawing.Size(90, 28)
        Me.CmdRicerca.Text = "Ricerca"
        '
        ' CmdEsci
        '
        Me.CmdEsci.Location = New System.Drawing.Point(112, 115)
        Me.CmdEsci.Name = "CmdEsci"
        Me.CmdEsci.Size = New System.Drawing.Size(90, 28)
        Me.CmdEsci.Text = "Esci"
        '
        ' dgvLog
        '
        Me.dgvLog.Anchor = System.Windows.Forms.AnchorStyles.Top Or
                           System.Windows.Forms.AnchorStyles.Bottom Or
                           System.Windows.Forms.AnchorStyles.Left Or
                           System.Windows.Forms.AnchorStyles.Right
        Me.dgvLog.AllowUserToAddRows = False
        Me.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLog.Location = New System.Drawing.Point(0, 153)
        Me.dgvLog.Name = "dgvLog"
        Me.dgvLog.ReadOnly = True
        Me.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLog.Size = New System.Drawing.Size(600, 280)
        Me.dgvLog.TabIndex = 0
        '
        ' FrmStampaLog
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 440)
        Me.Controls.Add(Me.LblDataIni)
        Me.Controls.Add(Me.TxtDataIni)
        Me.Controls.Add(Me.LblOraIni)
        Me.Controls.Add(Me.TxtOraIni)
        Me.Controls.Add(Me.LblDataFin)
        Me.Controls.Add(Me.TxtDataFin)
        Me.Controls.Add(Me.LblOraFin)
        Me.Controls.Add(Me.TxtOraFin)
        Me.Controls.Add(Me.radAll)
        Me.Controls.Add(Me.rad0)
        Me.Controls.Add(Me.rad1)
        Me.Controls.Add(Me.CmdRicerca)
        Me.Controls.Add(Me.CmdEsci)
        Me.Controls.Add(Me.dgvLog)
        Me.Text = "Stampa Log"
        CType(Me.dgvLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents LblDataIni As System.Windows.Forms.Label
    Friend WithEvents TxtDataIni As System.Windows.Forms.TextBox
    Friend WithEvents LblOraIni As System.Windows.Forms.Label
    Friend WithEvents TxtOraIni As System.Windows.Forms.TextBox
    Friend WithEvents LblDataFin As System.Windows.Forms.Label
    Friend WithEvents TxtDataFin As System.Windows.Forms.TextBox
    Friend WithEvents LblOraFin As System.Windows.Forms.Label
    Friend WithEvents TxtOraFin As System.Windows.Forms.TextBox
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents rad0 As System.Windows.Forms.RadioButton
    Friend WithEvents rad1 As System.Windows.Forms.RadioButton
    Friend WithEvents CmdRicerca As System.Windows.Forms.Button
    Friend WithEvents CmdEsci As System.Windows.Forms.Button
    Friend WithEvents dgvLog As System.Windows.Forms.DataGridView

End Class
