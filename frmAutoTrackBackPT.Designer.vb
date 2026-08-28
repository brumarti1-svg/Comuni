<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoTrackBackPT
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
        Me.CmbBackUpPT = New System.Windows.Forms.ComboBox()
        Me.CmdOK       = New System.Windows.Forms.Button()
        Me.LblTitle    = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        ' LblTitle
        Me.LblTitle.Location = New System.Drawing.Point(12, 12)
        Me.LblTitle.Size     = New System.Drawing.Size(260, 20)
        Me.LblTitle.Text     = "Modalità Back-Up AutoTrack:"
        ' CmbBackUpPT
        Me.CmbBackUpPT.Location      = New System.Drawing.Point(12, 36)
        Me.CmbBackUpPT.Name          = "CmbBackUpPT"
        Me.CmbBackUpPT.Size          = New System.Drawing.Size(260, 24)
        Me.CmbBackUpPT.TabIndex      = 0
        Me.CmbBackUpPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbBackUpPT.Items.AddRange(New Object() {"P", "S", "M"})
        ' CmdOK
        Me.CmdOK.Location  = New System.Drawing.Point(12, 72)
        Me.CmdOK.Name      = "CmdOK"
        Me.CmdOK.Size      = New System.Drawing.Size(260, 30)
        Me.CmdOK.TabIndex  = 1
        Me.CmdOK.Text      = "OK"
        Me.CmdOK.BackColor = System.Drawing.Color.LightGreen
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize          = New System.Drawing.Size(286, 116)
        Me.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox         = False
        Me.MinimizeBox         = False
        Me.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Name = "frmAutoTrackBackPT"
        Me.Text = "AutoTrack Back-Up"
        Me.Controls.Add(Me.LblTitle)
        Me.Controls.Add(Me.CmbBackUpPT)
        Me.Controls.Add(Me.CmdOK)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents CmbBackUpPT As System.Windows.Forms.ComboBox
    Friend WithEvents CmdOK       As System.Windows.Forms.Button
    Friend           LblTitle     As System.Windows.Forms.Label
End Class

