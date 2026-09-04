' Generato da frmAntenne.frm (VB6) — coordinate convertite: twips / 15 = pixel
' TrueDBGrid80.TDBGrid + VB.Data → DataGridView con colonne da DB Antenne
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAntenne
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
        Me.components       = New System.ComponentModel.Container()
        Me.MenuStrip1       = New System.Windows.Forms.MenuStrip()
        Me.MnuAntenne       = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSeleziona     = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuConvAngoli    = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuConvDMS       = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuConvDD        = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuElimina       = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuParametri     = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvAntenne       = New System.Windows.Forms.DataGridView()
        CType(Me.dgvAntenne, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        ' MenuStrip1 (sostituisce mnuAntenne[0..2] VB6)
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuAntenne})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name     = "MenuStrip1"
        Me.MenuStrip1.Size     = New System.Drawing.Size(546, 28)
        ' Antenna menu → Exit, Angular Conversion (submenu DMS/DD), Parametri
        Me.MnuAntenne.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.MnuSeleziona, Me.MnuConvAngoli, Me.MnuElimina, Me.MnuParametri})
        Me.MnuAntenne.Name = "MnuAntenne"
        Me.MnuAntenne.Text = "Antenna"
        Me.MnuSeleziona.Name = "MnuSeleziona"
        Me.MnuSeleziona.Text = "Exit"
        Me.MnuConvAngoli.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.MnuConvDMS, Me.MnuConvDD})
        Me.MnuConvAngoli.Name = "MnuConvAngoli"
        Me.MnuConvAngoli.Text = "Angular conversion"
        Me.MnuConvDMS.Name = "MnuConvDMS"
        Me.MnuConvDMS.Text = "Degrees, minutes, seconds"
        Me.MnuConvDD.Name = "MnuConvDD"
        Me.MnuConvDD.Text = "Degrees, decimal minutes"
        Me.MnuElimina.Name = "MnuElimina"
        Me.MnuElimina.Text = "Elimina"
        Me.MnuParametri.Name = "MnuParametri"
        Me.MnuParametri.Text = "Parametri"
        Me.MnuParametri.Enabled = False
        '
        ' dgvAntenne — sostituisce TrueDBGrid80+VB.Data
        ' (tdbAntenne VB6: Left=144/15=10, Top=324/15=22, Width=7860/15=524, Height=5412/15=361)
        ' Aggiunto offset +28 per MenuStrip
        '
        Me.dgvAntenne.AllowUserToAddRows    = True
        Me.dgvAntenne.AllowUserToDeleteRows = True
        Me.dgvAntenne.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.dgvAntenne.Location = New System.Drawing.Point(10, 50)
        Me.dgvAntenne.Size     = New System.Drawing.Size(524, 361)
        Me.dgvAntenne.TabIndex = 0
        Me.dgvAntenne.Name     = "dgvAntenne"
        Me.dgvAntenne.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        ' Colonne corrispondenti alla tabella Antenne (da VB6 TDBGrid Columns)
        Me.dgvAntenne.Columns.Add("Indice",     "Nr.")
        Me.dgvAntenne.Columns.Add("Antenna",    "Name")
        Me.dgvAntenne.Columns.Add("Latitudine", "Latitude")
        Me.dgvAntenne.Columns.Add("Longitudine","Longitude")
        Me.dgvAntenne.Columns.Add("Altezza",    "Altitude (m)")
        Me.dgvAntenne.Columns.Add("AZoffset",   "AZ offset")
        Me.dgvAntenne.Columns.Add("ELoffset",   "EL offset")
        '
        ' FrmAntenne (ClientWidth=8184/15=546, ClientHeight=6180/15=412; +28 menu = 440)
        '
        Me.ClientSize  = New System.Drawing.Size(546, 440)
        Me.ControlBox  = False
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.dgvAntenne)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name  = "FrmAntenne"
        Me.Text  = "Antenna"
        CType(Me.dgvAntenne, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1    As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuAntenne    As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSeleziona  As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuConvAngoli As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuConvDMS    As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuConvDD     As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuElimina    As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuParametri  As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvAntenne    As System.Windows.Forms.DataGridView

End Class
