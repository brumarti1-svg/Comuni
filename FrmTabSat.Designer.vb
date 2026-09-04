<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTabSat
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.MnuMemory = New System.Windows.Forms.MenuStrip()
        Me.MnuClose  = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLoad   = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrint  = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvSat    = New System.Windows.Forms.DataGridView()
        Me.MnuMemory.SuspendLayout()
        CType(Me.dgvSat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' MnuMemory
        Me.MnuMemory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuClose, Me.MnuLoad, Me.MnuPrint})
        Me.MnuMemory.Location = New System.Drawing.Point(0, 0)
        Me.MnuMemory.Name     = "MnuMemory"
        Me.MnuMemory.Size     = New System.Drawing.Size(700, 24)
        Me.MnuClose.Name = "MnuClose"
        Me.MnuClose.Text = "Chiudi"
        Me.MnuLoad.Name = "MnuLoad"
        Me.MnuLoad.Text = "Carica da ACU"
        Me.MnuPrint.Name = "MnuPrint"
        Me.MnuPrint.Text = "Stampa"
        ' dgvSat
        Me.dgvSat.Dock                  = System.Windows.Forms.DockStyle.Fill
        Me.dgvSat.Name                  = "dgvSat"
        Me.dgvSat.ReadOnly              = True
        Me.dgvSat.AllowUserToAddRows    = False
        Me.dgvSat.AllowUserToDeleteRows = False
        Me.dgvSat.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvSat.BackgroundColor       = System.Drawing.Color.Black
        Me.dgvSat.ForeColor             = System.Drawing.Color.Lime
        Me.dgvSat.TabIndex              = 0
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize          = New System.Drawing.Size(700, 420)
        Me.MainMenuStrip       = Me.MnuMemory
        Me.Name = "FrmTabSat"
        Me.Text = "Tabella Satelliti"
        Me.Controls.Add(Me.dgvSat)
        Me.Controls.Add(Me.MnuMemory)
        CType(Me.dgvSat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MnuMemory.ResumeLayout(False)
        Me.MnuMemory.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents MnuMemory As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuClose  As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuLoad   As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPrint  As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvSat    As System.Windows.Forms.DataGridView
End Class

