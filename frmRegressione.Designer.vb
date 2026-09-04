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
        Me.grpGrado.Location = New System.Drawing.Point(12, 12)
        Me.grpGrado.Size     = New System.Drawing.Size(200, 130)
        Me.grpGrado.Text     = "Grado regressione"
        Me.grpGrado.Controls.AddRange(New Control() {Me.opt1, Me.opt2, Me.opt3, Me.opt4})
        ' opt1
        Me.opt1.Location = New System.Drawing.Point(12, 24)
        Me.opt1.Name     = "opt1"
        Me.opt1.Size     = New System.Drawing.Size(80, 22)
        Me.opt1.Text     = "Grado 1"
        Me.opt1.TabIndex = 0
        ' opt2
        Me.opt2.Location = New System.Drawing.Point(12, 50)
        Me.opt2.Name     = "opt2"
        Me.opt2.Size     = New System.Drawing.Size(80, 22)
        Me.opt2.Text     = "Grado 2"
        Me.opt2.TabIndex = 1
        Me.opt2.Checked  = True
        ' opt3
        Me.opt3.Location = New System.Drawing.Point(12, 76)
        Me.opt3.Name     = "opt3"
        Me.opt3.Size     = New System.Drawing.Size(80, 22)
        Me.opt3.Text     = "Grado 3"
        Me.opt3.TabIndex = 2
        ' opt4
        Me.opt4.Location = New System.Drawing.Point(12, 102)
        Me.opt4.Name     = "opt4"
        Me.opt4.Size     = New System.Drawing.Size(80, 22)
        Me.opt4.Text     = "Grado 4"
        Me.opt4.TabIndex = 3
        ' CmdExit
        Me.CmdExit.Location  = New System.Drawing.Point(12, 154)
        Me.CmdExit.Name      = "CmdExit"
        Me.CmdExit.Size      = New System.Drawing.Size(200, 30)
        Me.CmdExit.TabIndex  = 4
        Me.CmdExit.Text      = "Chiudi"
        Me.CmdExit.BackColor = System.Drawing.Color.Salmon
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize          = New System.Drawing.Size(224, 198)
        Me.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox         = False
        Me.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Name = "FrmRegressione"
        Me.Text = "Grado Regressione"
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

