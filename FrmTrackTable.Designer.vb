<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTrackTable
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
        Me.dgvTrackTable = New System.Windows.Forms.DataGridView()
        Me.LblSatellite = New System.Windows.Forms.Label()
        Me.cmsMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiEsci = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiStampa = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dgvTrackTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        ' LblSatellite
        '
        Me.LblSatellite.Location = New System.Drawing.Point(12, 8)
        Me.LblSatellite.Name = "LblSatellite"
        Me.LblSatellite.Size = New System.Drawing.Size(200, 20)
        '
        ' dgvTrackTable
        '
        Me.dgvTrackTable.AllowUserToAddRows = False
        Me.dgvTrackTable.Anchor = System.Windows.Forms.AnchorStyles.Top Or
                                  System.Windows.Forms.AnchorStyles.Bottom Or
                                  System.Windows.Forms.AnchorStyles.Left Or
                                  System.Windows.Forms.AnchorStyles.Right
        Me.dgvTrackTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTrackTable.ContextMenuStrip = Me.cmsMain
        Me.dgvTrackTable.Location = New System.Drawing.Point(0, 30)
        Me.dgvTrackTable.Name = "dgvTrackTable"
        Me.dgvTrackTable.ReadOnly = True
        Me.dgvTrackTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTrackTable.Size = New System.Drawing.Size(780, 420)
        Me.dgvTrackTable.TabIndex = 0
        '
        ' cmsMain
        '
        Me.cmsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.tsmiEsci, Me.tsmiStampa})
        Me.cmsMain.Name = "cmsMain"
        Me.cmsMain.Size = New System.Drawing.Size(120, 48)
        '
        ' tsmiEsci
        '
        Me.tsmiEsci.Name = "tsmiEsci"
        Me.tsmiEsci.Text = "Esci"
        '
        ' tsmiStampa
        '
        Me.tsmiStampa.Name = "tsmiStampa"
        Me.tsmiStampa.Text = "Stampa"
        '
        ' FrmTrackTable
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(780, 450)
        Me.Controls.Add(Me.LblSatellite)
        Me.Controls.Add(Me.dgvTrackTable)
        Me.Text = "Track Table"
        CType(Me.dgvTrackTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsMain.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents dgvTrackTable As System.Windows.Forms.DataGridView
    Friend WithEvents LblSatellite As System.Windows.Forms.Label
    Friend WithEvents cmsMain As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiEsci As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiStampa As System.Windows.Forms.ToolStripMenuItem

End Class
