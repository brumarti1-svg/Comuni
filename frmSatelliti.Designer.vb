<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSatelliti
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuParameter = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlToolBar = New System.Windows.Forms.Panel()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.dgvSatelliti = New System.Windows.Forms.DataGridView()
        Me.pnlNewSatellite = New System.Windows.Forms.Panel()
        Me.LblAlgorithm = New System.Windows.Forms.Label()
        Me.TxtAlgorithm = New System.Windows.Forms.TextBox()
        Me.LblFile = New System.Windows.Forms.Label()
        Me.TxtParametri = New System.Windows.Forms.TextBox()
        Me.LblSatName = New System.Windows.Forms.Label()
        Me.CmbSatelliti = New System.Windows.Forms.ComboBox()
        Me.LblPolAngleLbl = New System.Windows.Forms.Label()
        Me.TxtPolAngle = New System.Windows.Forms.TextBox()
        Me.CmdAbort = New System.Windows.Forms.Button()
        Me.chkVerso = New System.Windows.Forms.CheckBox()
        Me.LblAlgDesc = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
        CType(Me.dgvSatelliti, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNewSatellite.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuExit, Me.MnuRemove, Me.MnuParameter})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 28)
        Me.MenuStrip1.TabIndex = 0
        '
        'MnuExit
        '
        Me.MnuExit.Name = "MnuExit"
        Me.MnuExit.Size = New System.Drawing.Size(47, 24)
        Me.MnuExit.Text = "Exit"
        '
        'MnuRemove
        '
        Me.MnuRemove.Name = "MnuRemove"
        Me.MnuRemove.Size = New System.Drawing.Size(77, 24)
        Me.MnuRemove.Text = "Remove"
        '
        'MnuParameter
        '
        Me.MnuParameter.Name = "MnuParameter"
        Me.MnuParameter.Size = New System.Drawing.Size(90, 24)
        Me.MnuParameter.Text = "Parameter"
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlToolBar.Controls.Add(Me.btnNew)
        Me.pnlToolBar.Controls.Add(Me.btnSave)
        Me.pnlToolBar.Controls.Add(Me.btnPrint)
        Me.pnlToolBar.Controls.Add(Me.btnDelete)
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 24)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(800, 30)
        Me.pnlToolBar.TabIndex = 1
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(2, 3)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(55, 24)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "New"
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(60, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(55, 24)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(118, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(55, 24)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(176, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(55, 24)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        '
        'dgvSatelliti
        '
        Me.dgvSatelliti.AllowUserToAddRows = False
        Me.dgvSatelliti.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSatelliti.ColumnHeadersHeight = 29
        Me.dgvSatelliti.Location = New System.Drawing.Point(0, 54)
        Me.dgvSatelliti.MultiSelect = False
        Me.dgvSatelliti.Name = "dgvSatelliti"
        Me.dgvSatelliti.RowHeadersVisible = False
        Me.dgvSatelliti.RowHeadersWidth = 51
        Me.dgvSatelliti.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSatelliti.Size = New System.Drawing.Size(800, 330)
        Me.dgvSatelliti.TabIndex = 4
        '
        'pnlNewSatellite
        '
        Me.pnlNewSatellite.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlNewSatellite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlNewSatellite.Controls.Add(Me.LblAlgorithm)
        Me.pnlNewSatellite.Controls.Add(Me.TxtAlgorithm)
        Me.pnlNewSatellite.Controls.Add(Me.LblFile)
        Me.pnlNewSatellite.Controls.Add(Me.TxtParametri)
        Me.pnlNewSatellite.Controls.Add(Me.LblSatName)
        Me.pnlNewSatellite.Controls.Add(Me.CmbSatelliti)
        Me.pnlNewSatellite.Controls.Add(Me.LblPolAngleLbl)
        Me.pnlNewSatellite.Controls.Add(Me.TxtPolAngle)
        Me.pnlNewSatellite.Controls.Add(Me.CmdAbort)
        Me.pnlNewSatellite.Controls.Add(Me.chkVerso)
        Me.pnlNewSatellite.Location = New System.Drawing.Point(0, 385)
        Me.pnlNewSatellite.Name = "pnlNewSatellite"
        Me.pnlNewSatellite.Size = New System.Drawing.Size(800, 90)
        Me.pnlNewSatellite.TabIndex = 5
        Me.pnlNewSatellite.Visible = True
        '
        'LblAlgorithm
        '
        Me.LblAlgorithm.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.LblAlgorithm.Location = New System.Drawing.Point(8, 10)
        Me.LblAlgorithm.Name = "LblAlgorithm"
        Me.LblAlgorithm.Size = New System.Drawing.Size(100, 18)
        Me.LblAlgorithm.TabIndex = 0
        Me.LblAlgorithm.Text = "Algorithm"
        '
        'TxtAlgorithm
        '
        Me.TxtAlgorithm.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.TxtAlgorithm.Location = New System.Drawing.Point(125, 7)
        Me.TxtAlgorithm.Name = "TxtAlgorithm"
        Me.TxtAlgorithm.Size = New System.Drawing.Size(32, 27)
        Me.TxtAlgorithm.TabIndex = 1
        Me.TxtAlgorithm.Text = "1"
        Me.TxtAlgorithm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblFile
        '
        Me.LblFile.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.LblFile.Location = New System.Drawing.Point(211, 10)
        Me.LblFile.Name = "LblFile"
        Me.LblFile.Size = New System.Drawing.Size(83, 18)
        Me.LblFile.TabIndex = 2
        Me.LblFile.Text = "File of Parameters"
        '
        'TxtParametri
        '
        Me.TxtParametri.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.TxtParametri.Location = New System.Drawing.Point(300, 7)
        Me.TxtParametri.Name = "TxtParametri"
        Me.TxtParametri.Size = New System.Drawing.Size(230, 27)
        Me.TxtParametri.TabIndex = 3
        '
        'LblSatName
        '
        Me.LblSatName.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.LblSatName.Location = New System.Drawing.Point(8, 50)
        Me.LblSatName.Name = "LblSatName"
        Me.LblSatName.Size = New System.Drawing.Size(100, 18)
        Me.LblSatName.TabIndex = 4
        Me.LblSatName.Text = "Satellite Name"
        '
        'CmbSatelliti
        '
        Me.CmbSatelliti.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.CmbSatelliti.Location = New System.Drawing.Point(112, 47)
        Me.CmbSatelliti.Name = "CmbSatelliti"
        Me.CmbSatelliti.Size = New System.Drawing.Size(207, 27)
        Me.CmbSatelliti.Sorted = True
        Me.CmbSatelliti.TabIndex = 5
        '
        'LblPolAngleLbl
        '
        Me.LblPolAngleLbl.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.LblPolAngleLbl.Location = New System.Drawing.Point(328, 50)
        Me.LblPolAngleLbl.Name = "LblPolAngleLbl"
        Me.LblPolAngleLbl.Size = New System.Drawing.Size(68, 18)
        Me.LblPolAngleLbl.TabIndex = 6
        Me.LblPolAngleLbl.Text = "Pol. Angle"
        '
        'TxtPolAngle
        '
        Me.TxtPolAngle.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.TxtPolAngle.Location = New System.Drawing.Point(400, 47)
        Me.TxtPolAngle.Name = "TxtPolAngle"
        Me.TxtPolAngle.Size = New System.Drawing.Size(56, 27)
        Me.TxtPolAngle.TabIndex = 7
        Me.TxtPolAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmdAbort
        '
        Me.CmdAbort.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.CmdAbort.Location = New System.Drawing.Point(500, 47)
        Me.CmdAbort.Name = "CmdAbort"
        Me.CmdAbort.Size = New System.Drawing.Size(70, 24)
        Me.CmdAbort.TabIndex = 5
        Me.CmdAbort.Text = "Abort"
        '
        'chkVerso
        '
        Me.chkVerso.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.chkVerso.Location = New System.Drawing.Point(580, 47)
        Me.chkVerso.Name = "chkVerso"
        Me.chkVerso.Size = New System.Drawing.Size(70, 24)
        Me.chkVerso.TabIndex = 8
        Me.chkVerso.Text = "Verso"
        '
        'LblAlgDesc
        '
        Me.LblAlgDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblAlgDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblAlgDesc.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold)
        Me.LblAlgDesc.Location = New System.Drawing.Point(0, 476)
        Me.LblAlgDesc.Name = "LblAlgDesc"
        Me.LblAlgDesc.Size = New System.Drawing.Size(800, 34)
        Me.LblAlgDesc.TabIndex = 6
        Me.LblAlgDesc.Text = "Algorithm: 0 = 11 Elements Intelsat    1 = TLE      2 = 11 Elements Inmarsat     " &
    "3 = Track Table    4 = Stars"
        '
        'FrmSatelliti
        '
        Me.ClientSize = New System.Drawing.Size(800, 510)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.pnlToolBar)
        Me.Controls.Add(Me.dgvSatelliti)
        Me.Controls.Add(Me.pnlNewSatellite)
        Me.Controls.Add(Me.LblAlgDesc)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FrmSatelliti"
        Me.Text = "Satellite"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.pnlToolBar.ResumeLayout(False)
        CType(Me.dgvSatelliti, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlNewSatellite.ResumeLayout(False)
        Me.pnlNewSatellite.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    ' ── Dichiarazioni campi (in Designer per evitare duplicati) ──
    Friend WithEvents MenuStrip1      As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuExit         As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuRemove       As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuParameter    As System.Windows.Forms.ToolStripMenuItem
    Friend pnlToolBar                 As System.Windows.Forms.Panel
    Friend WithEvents btnNew          As System.Windows.Forms.Button
    Friend WithEvents btnSave         As System.Windows.Forms.Button
    Friend WithEvents btnPrint        As System.Windows.Forms.Button
    Friend WithEvents btnDelete       As System.Windows.Forms.Button
    Friend WithEvents dgvSatelliti    As System.Windows.Forms.DataGridView
    Friend pnlNewSatellite            As System.Windows.Forms.Panel
    Friend LblAlgorithm               As System.Windows.Forms.Label
    Friend LblFile                    As System.Windows.Forms.Label
    Friend LblSatName                 As System.Windows.Forms.Label
    Friend LblPolAngleLbl             As System.Windows.Forms.Label
    Friend WithEvents CmdAbort        As System.Windows.Forms.Button
    Friend chkVerso                   As System.Windows.Forms.CheckBox
    Friend LblAlgDesc                 As System.Windows.Forms.Label
    Friend WithEvents CmbSatelliti    As System.Windows.Forms.ComboBox
    Friend WithEvents TxtAlgorithm    As System.Windows.Forms.TextBox
    Friend WithEvents TxtParametri    As System.Windows.Forms.TextBox
    Friend WithEvents TxtPolAngle     As System.Windows.Forms.TextBox

End Class
