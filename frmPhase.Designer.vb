<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPhase
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
        Me.components  = New System.ComponentModel.Container()
        Me.MenuStrip1  = New System.Windows.Forms.MenuStrip()
        Me.MnuExit     = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuApply    = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReceiver = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRemove   = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrint    = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvPhase    = New System.Windows.Forms.DataGridView()

        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgvPhase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        ' MenuStrip
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {
            Me.MnuExit, Me.MnuApply, Me.MnuReceiver, Me.MnuRemove, Me.MnuPrint})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(760, 24)

        Me.MnuExit.Name = "MnuExit"
        Me.MnuExit.Text = "Exit"
        Me.MnuExit.Size = New System.Drawing.Size(50, 20)

        Me.MnuApply.Name = "MnuApply"
        Me.MnuApply.Text = "Apply"
        Me.MnuApply.Size = New System.Drawing.Size(52, 20)

        Me.MnuReceiver.Name = "MnuReceiver"
        Me.MnuReceiver.Text = "Receiver"
        Me.MnuReceiver.Size = New System.Drawing.Size(63, 20)

        Me.MnuRemove.Name = "MnuRemove"
        Me.MnuRemove.Text = "Remove"
        Me.MnuRemove.Size = New System.Drawing.Size(62, 20)

        Me.MnuPrint.Name = "MnuPrint"
        Me.MnuPrint.Text = "Print"
        Me.MnuPrint.Size = New System.Drawing.Size(50, 20)

        ' DataGridView
        Me.dgvPhase.Location = New System.Drawing.Point(0, 24)
        Me.dgvPhase.Name = "dgvPhase"
        Me.dgvPhase.Size = New System.Drawing.Size(760, 440)
        Me.dgvPhase.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        Me.dgvPhase.AllowUserToAddRows = False
        Me.dgvPhase.MultiSelect = False
        Me.dgvPhase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvPhase.ReadOnly = True
        Me.dgvPhase.RowHeadersVisible = False
        Me.dgvPhase.TabIndex = 0

        ' Form
        Me.MainMenuStrip = Me.MenuStrip1
        Me.ClientSize = New System.Drawing.Size(760, 464)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.dgvPhase)
        Me.Name = "FrmPhase"
        Me.Text = "Phase Selection"

        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgvPhase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1  As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuExit     As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuApply    As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuReceiver As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuRemove   As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPrint    As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvPhase    As System.Windows.Forms.DataGridView

End Class
