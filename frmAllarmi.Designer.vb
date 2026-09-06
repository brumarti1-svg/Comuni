<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAllarmi
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
        Me.components = New System.ComponentModel.Container()

        Me.LblAntenna    = New System.Windows.Forms.Label()
        Me.LblDaLbl      = New System.Windows.Forms.Label()
        Me.LblALbl       = New System.Windows.Forms.Label()
        Me.mskDataIni    = New System.Windows.Forms.MaskedTextBox()
        Me.mskOraIni     = New System.Windows.Forms.MaskedTextBox()
        Me.mskDataFin    = New System.Windows.Forms.MaskedTextBox()
        Me.mskOraFin     = New System.Windows.Forms.MaskedTextBox()
        Me.CmdRicerca    = New System.Windows.Forms.Button()
        Me.CmdReset      = New System.Windows.Forms.Button()
        Me.CmdAnnulla    = New System.Windows.Forms.Button()
        Me.dgvAllarmi    = New System.Windows.Forms.DataGridView()
        CType(Me.dgvAllarmi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        ' LblAntenna
        Me.LblAntenna.Location  = New System.Drawing.Point(10, 10)
        Me.LblAntenna.Size      = New System.Drawing.Size(200, 20)
        Me.LblAntenna.Font      = New System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
        Me.LblAntenna.Name      = "LblAntenna"

        ' Filtro data/ora
        Me.LblDaLbl.Text     = "Da:"
        Me.LblDaLbl.Location = New System.Drawing.Point(10, 40)
        Me.LblDaLbl.AutoSize = True

        Me.mskDataIni.Mask     = "00/00/0000"
        Me.mskDataIni.Location = New System.Drawing.Point(35, 37)
        Me.mskDataIni.Size     = New System.Drawing.Size(80, 21)
        Me.mskDataIni.Name     = "mskDataIni"

        Me.mskOraIni.Mask     = "00:00"
        Me.mskOraIni.Location = New System.Drawing.Point(120, 37)
        Me.mskOraIni.Size     = New System.Drawing.Size(45, 21)
        Me.mskOraIni.Name     = "mskOraIni"

        Me.LblALbl.Text     = "A:"
        Me.LblALbl.Location = New System.Drawing.Point(175, 40)
        Me.LblALbl.AutoSize = True

        Me.mskDataFin.Mask     = "00/00/0000"
        Me.mskDataFin.Location = New System.Drawing.Point(195, 37)
        Me.mskDataFin.Size     = New System.Drawing.Size(80, 21)
        Me.mskDataFin.Name     = "mskDataFin"

        Me.mskOraFin.Mask     = "00:00"
        Me.mskOraFin.Location = New System.Drawing.Point(280, 37)
        Me.mskOraFin.Size     = New System.Drawing.Size(45, 21)
        Me.mskOraFin.Name     = "mskOraFin"

        Me.CmdRicerca.Text     = "Search"
        Me.CmdRicerca.Location = New System.Drawing.Point(335, 35)
        Me.CmdRicerca.Size     = New System.Drawing.Size(70, 25)
        Me.CmdRicerca.Name     = "CmdRicerca"

        Me.CmdReset.Text     = "Reset Log"
        Me.CmdReset.Location = New System.Drawing.Point(415, 35)
        Me.CmdReset.Size     = New System.Drawing.Size(80, 25)
        Me.CmdReset.Name     = "CmdReset"

        Me.CmdAnnulla.Text     = "Close"
        Me.CmdAnnulla.Location = New System.Drawing.Point(505, 35)
        Me.CmdAnnulla.Size     = New System.Drawing.Size(70, 25)
        Me.CmdAnnulla.Name     = "CmdAnnulla"

        ' DataGridView
        Me.dgvAllarmi.Location              = New System.Drawing.Point(10, 70)
        Me.dgvAllarmi.Anchor                = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        Me.dgvAllarmi.Size                  = New System.Drawing.Size(860, 380)
        Me.dgvAllarmi.ReadOnly              = True
        Me.dgvAllarmi.AllowUserToAddRows    = False
        Me.dgvAllarmi.AllowUserToDeleteRows = False
        Me.dgvAllarmi.RowHeadersVisible     = False
        Me.dgvAllarmi.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAllarmi.Name                  = "dgvAllarmi"

        ' Form
        Me.ClientSize = New System.Drawing.Size(880, 460)
        Me.Text       = "Allarmi"
        Me.Name       = "frmAllarmi"
        Me.Controls.Add(Me.LblAntenna)
        Me.Controls.Add(Me.LblDaLbl)
        Me.Controls.Add(Me.mskDataIni)
        Me.Controls.Add(Me.mskOraIni)
        Me.Controls.Add(Me.LblALbl)
        Me.Controls.Add(Me.mskDataFin)
        Me.Controls.Add(Me.mskOraFin)
        Me.Controls.Add(Me.CmdRicerca)
        Me.Controls.Add(Me.CmdReset)
        Me.Controls.Add(Me.CmdAnnulla)
        Me.Controls.Add(Me.dgvAllarmi)
        CType(Me.dgvAllarmi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents LblAntenna  As System.Windows.Forms.Label
    Friend WithEvents LblDaLbl    As System.Windows.Forms.Label
    Friend WithEvents LblALbl     As System.Windows.Forms.Label
    Friend WithEvents mskDataIni  As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskOraIni   As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskDataFin  As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskOraFin   As System.Windows.Forms.MaskedTextBox
    Friend WithEvents CmdRicerca  As System.Windows.Forms.Button
    Friend WithEvents CmdReset    As System.Windows.Forms.Button
    Friend WithEvents CmdAnnulla  As System.Windows.Forms.Button
    Friend WithEvents dgvAllarmi  As System.Windows.Forms.DataGridView

End Class
