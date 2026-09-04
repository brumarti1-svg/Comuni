<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMemoryTrack
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
        Me.MnuMem   = New System.Windows.Forms.MenuStrip()
        Me.MnuClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLoad  = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvMem   = New System.Windows.Forms.DataGridView()
        Me.MnuMem.SuspendLayout()
        CType(Me.dgvMem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' MnuMem
        Me.MnuMem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuClose, Me.MnuLoad, Me.MnuPrint})
        Me.MnuMem.Location = New System.Drawing.Point(0, 0)
        Me.MnuMem.Name     = "MnuMem"
        Me.MnuMem.Size     = New System.Drawing.Size(600, 24)
        Me.MnuClose.Name = "MnuClose"
        Me.MnuClose.Text = "Chiudi"
        Me.MnuLoad.Name = "MnuLoad"
        Me.MnuLoad.Text = "Carica da ACU"
        Me.MnuPrint.Name = "MnuPrint"
        Me.MnuPrint.Text = "Stampa"
        ' dgvMem
        Me.dgvMem.Dock                  = System.Windows.Forms.DockStyle.Fill
        Me.dgvMem.Name                  = "dgvMem"
        Me.dgvMem.ReadOnly              = True
        Me.dgvMem.AllowUserToAddRows    = False
        Me.dgvMem.AllowUserToDeleteRows = False
        Me.dgvMem.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvMem.BackgroundColor       = System.Drawing.Color.Black
        Me.dgvMem.ForeColor             = System.Drawing.Color.Lime
        Me.dgvMem.TabIndex              = 0
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize          = New System.Drawing.Size(600, 420)
        Me.MainMenuStrip       = Me.MnuMem
        Me.Name = "FrmMemoryTrack"
        Me.Text = "Memory Track"
        Me.Controls.Add(Me.dgvMem)
        Me.Controls.Add(Me.MnuMem)
        CType(Me.dgvMem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MnuMem.ResumeLayout(False)
        Me.MnuMem.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents MnuMem   As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuLoad  As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvMem   As System.Windows.Forms.DataGridView
End Class

