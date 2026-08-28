' Generato da frmBrowse.frm (VB6, ScaleMode=3 Pixel)
' Coordinate già in pixel. DirListBox + DriveListBox → btnSfoglia + FolderBrowserDialog
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBrowse
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
        Me.components  = New System.ComponentModel.Container()
        Me.MenuStrip1  = New System.Windows.Forms.MenuStrip()
        Me.MnuFile     = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChiudi   = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSfoglia  = New System.Windows.Forms.Button()
        Me.lstMdb      = New System.Windows.Forms.ListBox()
        Me.LblPreview  = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        ' MenuStrip (sostituisce mnuBrowse VB6)
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuFile})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name     = "MenuStrip1"
        Me.MenuStrip1.Size     = New System.Drawing.Size(876, 28)
        Me.MnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuChiudi})
        Me.MnuFile.Name    = "MnuFile"
        Me.MnuFile.Text    = "File"
        Me.MnuChiudi.Name  = "MnuChiudi"
        Me.MnuChiudi.Text  = "Exit"
        '
        ' btnSfoglia — sostituisce DriveListBox + DirListBox VB6
        ' (posizionato dove era la DirListBox: Left=16, Top=55 + 28 MenuStrip)
        '
        Me.btnSfoglia.Location = New System.Drawing.Point(16, 44)
        Me.btnSfoglia.Size     = New System.Drawing.Size(193, 30)
        Me.btnSfoglia.TabIndex = 0
        Me.btnSfoglia.Text     = "Sfoglia..."
        Me.btnSfoglia.Name     = "btnSfoglia"
        '
        ' LblPreview (fraSelection.Left=16 + Lbl.Left=444 = 460; fraSelection.Top=16+28menu + Lbl.Top=17 = 61)
        '
        Me.LblPreview.Anchor       = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.LblPreview.Location     = New System.Drawing.Point(460, 48)
        Me.LblPreview.Size         = New System.Drawing.Size(390, 23)
        Me.LblPreview.Text         = "Double click to select"
        Me.LblPreview.BorderStyle  = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblPreview.Name         = "LblPreview"
        '
        ' lstMdb (fraSelection.Left=16 + Lbl.Left=218 = 234; fraSelection.Top=16+28menu + Lbl.Top=43 = 87)
        '
        Me.lstMdb.Anchor   = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.lstMdb.Location = New System.Drawing.Point(218, 80)
        Me.lstMdb.Size     = New System.Drawing.Size(642, 450)
        Me.lstMdb.TabIndex = 1
        Me.lstMdb.Name     = "lstMdb"
        '
        ' frmBrowse (ScaleWidth=876, ScaleHeight=568 px)
        '
        Me.ClientSize  = New System.Drawing.Size(876, 568)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.btnSfoglia)
        Me.Controls.Add(Me.LblPreview)
        Me.Controls.Add(Me.lstMdb)
        Me.MainMenuStrip   = Me.MenuStrip1
        Me.ControlBox      = False
        Me.Name            = "frmBrowse"
        Me.Text            = "Parameter List"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuFile    As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuChiudi  As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSfoglia As System.Windows.Forms.Button
    Friend WithEvents lstMdb     As System.Windows.Forms.ListBox
    Friend LblPreview            As System.Windows.Forms.Label

End Class
