<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmUrl
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
        Me.dgvUrl = New System.Windows.Forms.DataGridView()
        Me.cmsMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiEsci = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiElimina = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dgvUrl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvUrl
        '
        Me.dgvUrl.AllowUserToAddRows = False
        Me.dgvUrl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUrl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUrl.ContextMenuStrip = Me.cmsMain
        Me.dgvUrl.Location = New System.Drawing.Point(0, 0)
        Me.dgvUrl.Name = "dgvUrl"
        Me.dgvUrl.ReadOnly = True
        Me.dgvUrl.RowHeadersWidth = 51
        Me.dgvUrl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUrl.Size = New System.Drawing.Size(680, 400)
        Me.dgvUrl.TabIndex = 0
        '
        'cmsMain
        '
        Me.cmsMain.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiEsci, Me.tsmiElimina})
        Me.cmsMain.Name = "cmsMain"
        Me.cmsMain.Size = New System.Drawing.Size(128, 52)
        '
        'tsmiEsci
        '
        Me.tsmiEsci.Name = "tsmiEsci"
        Me.tsmiEsci.Size = New System.Drawing.Size(127, 24)
        Me.tsmiEsci.Text = "Esci"
        '
        'tsmiElimina
        '
        Me.tsmiElimina.Name = "tsmiElimina"
        Me.tsmiElimina.Size = New System.Drawing.Size(127, 24)
        Me.tsmiElimina.Text = "Elimina"
        '
        'FrmUrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(680, 400)
        Me.Controls.Add(Me.dgvUrl)
        Me.Name = "FrmUrl"
        Me.Text = "URL"
        CType(Me.dgvUrl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvUrl As System.Windows.Forms.DataGridView
    Friend WithEvents cmsMain As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiEsci As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiElimina As System.Windows.Forms.ToolStripMenuItem

End Class
