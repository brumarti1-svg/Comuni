<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRegressione
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
        Me.opt1    = New System.Windows.Forms.RadioButton()
        Me.opt2    = New System.Windows.Forms.RadioButton()
        Me.opt3    = New System.Windows.Forms.RadioButton()
        Me.opt4    = New System.Windows.Forms.RadioButton()
        Me.CmdExit = New System.Windows.Forms.Button()
        Me.grpGrado = New System.Windows.Forms.GroupBox()
        Me.grpGrado.SuspendLayout()
        Me.SuspendLayout()
        ' grpGrado
        ' Larghezza aumentata: a 80px "Degree N" veniva tagliato e i 4
        ' pulsanti sembravano tutti dire solo "Degree", indistinguibili.
        Me.grpGrado.Location = New System.Drawing.Point(12, 12)
        Me.grpGrado.Size     = New System.Drawing.Size(220, 130)
        Me.grpGrado.Text     = "Regression degree"
        Me.grpGrado.Controls.AddRange(New Control() {Me.opt1, Me.opt2, Me.opt3, Me.opt4})
        ' opt1
        Me.opt1.Location = New System.Drawing.Point(12, 24)
        Me.opt1.Name     = "opt1"
        Me.opt1.Size     = New System.Drawing.Size(160, 22)
        Me.opt1.Text     = "Degree 1 (linear)"
        Me.opt1.TabIndex = 0
        ' opt2
        Me.opt2.Location = New System.Drawing.Point(12, 50)
        Me.opt2.Name     = "opt2"
        Me.opt2.Size     = New System.Drawing.Size(160, 22)
        Me.opt2.Text     = "Degree 2 (parabolic)"
        Me.opt2.TabIndex = 1
        Me.opt2.Checked  = True
        ' opt3
        Me.opt3.Location = New System.Drawing.Point(12, 76)
        Me.opt3.Name     = "opt3"
        Me.opt3.Size     = New System.Drawing.Size(160, 22)
        Me.opt3.Text     = "Degree 3 (cubic)"
        Me.opt3.TabIndex = 2
        ' opt4
        Me.opt4.Location = New System.Drawing.Point(12, 102)
        Me.opt4.Name     = "opt4"
        Me.opt4.Size     = New System.Drawing.Size(160, 22)
        Me.opt4.Text     = "Degree 4 (geostationary)"
        Me.opt4.TabIndex = 3
        ' CmdExit
        Me.CmdExit.Location  = New System.Drawing.Point(12, 154)
        Me.CmdExit.Name      = "CmdExit"
        Me.CmdExit.Size      = New System.Drawing.Size(220, 30)
        Me.CmdExit.TabIndex  = 4
        Me.CmdExit.Text      = "Close"
        Me.CmdExit.BackColor = System.Drawing.Color.Salmon
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize          = New System.Drawing.Size(244, 198)
        Me.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox         = False
        Me.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Name = "FrmRegressione"
        Me.Text = "Regression Degree"
        Me.Controls.Add(Me.grpGrado)
        Me.Controls.Add(Me.CmdExit)
        Me.grpGrado.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents opt1    As System.Windows.Forms.RadioButton
    Friend WithEvents opt2    As System.Windows.Forms.RadioButton
    Friend WithEvents opt3    As System.Windows.Forms.RadioButton
    Friend WithEvents opt4    As System.Windows.Forms.RadioButton
    Friend WithEvents CmdExit As System.Windows.Forms.Button
    Friend           grpGrado As System.Windows.Forms.GroupBox
End Class

