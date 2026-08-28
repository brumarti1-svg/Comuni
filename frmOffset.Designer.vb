<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOffset
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
        Me.CmbInc      = New System.Windows.Forms.ComboBox()
        Me.TxtAzOffset = New System.Windows.Forms.TextBox()
        Me.TxtElOffset = New System.Windows.Forms.TextBox()
        Me.TxtFreccia  = New System.Windows.Forms.TextBox()
        Me.TxtDeltaTime = New System.Windows.Forms.TextBox()
        Me.CmdCCW      = New System.Windows.Forms.Button()
        Me.CmdCW       = New System.Windows.Forms.Button()
        Me.CmdUp       = New System.Windows.Forms.Button()
        Me.CmdDown     = New System.Windows.Forms.Button()
        Me.CmdEnd      = New System.Windows.Forms.Button()
        Me.CmdAnnulla  = New System.Windows.Forms.Button()
        Me.LblAz       = New System.Windows.Forms.Label()
        Me.LblEl       = New System.Windows.Forms.Label()
        Me.LblInc      = New System.Windows.Forms.Label()
        Me.LblDT       = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        ' LblAz / TxtAzOffset
        Me.LblAz.Location      = New System.Drawing.Point(8, 12)
        Me.LblAz.Size          = New System.Drawing.Size(80, 20)
        Me.LblAz.Text          = "Offset Az (°):"
        Me.TxtAzOffset.Location = New System.Drawing.Point(90, 10)
        Me.TxtAzOffset.Name    = "TxtAzOffset"
        Me.TxtAzOffset.Size    = New System.Drawing.Size(80, 22)
        Me.TxtAzOffset.TabIndex = 0
        Me.TxtAzOffset.Text    = "0.000"
        ' LblEl / TxtElOffset
        Me.LblEl.Location      = New System.Drawing.Point(8, 38)
        Me.LblEl.Size          = New System.Drawing.Size(80, 20)
        Me.LblEl.Text          = "Offset El (°):"
        Me.TxtElOffset.Location = New System.Drawing.Point(90, 36)
        Me.TxtElOffset.Name    = "TxtElOffset"
        Me.TxtElOffset.Size    = New System.Drawing.Size(80, 22)
        Me.TxtElOffset.TabIndex = 1
        Me.TxtElOffset.Text    = "0.000"
        ' LblDT / TxtDeltaTime
        Me.LblDT.Location       = New System.Drawing.Point(8, 64)
        Me.LblDT.Size           = New System.Drawing.Size(80, 20)
        Me.LblDT.Text           = "Delta T (s):"
        Me.TxtDeltaTime.Location = New System.Drawing.Point(90, 62)
        Me.TxtDeltaTime.Name    = "TxtDeltaTime"
        Me.TxtDeltaTime.Size    = New System.Drawing.Size(80, 22)
        Me.TxtDeltaTime.TabIndex = 2
        Me.TxtDeltaTime.Text    = "0"
        ' LblInc / CmbInc
        Me.LblInc.Location = New System.Drawing.Point(8, 92)
        Me.LblInc.Size     = New System.Drawing.Size(80, 20)
        Me.LblInc.Text     = "Incremento:"
        Me.CmbInc.Location = New System.Drawing.Point(90, 90)
        Me.CmbInc.Name     = "CmbInc"
        Me.CmbInc.Size     = New System.Drawing.Size(80, 24)
        Me.CmbInc.TabIndex = 3
        Me.CmbInc.Items.AddRange(New Object() {"0.001", "0.010", "0.100", "0.500", "1.000"})
        Me.CmbInc.Text     = "0.010"
        ' Direction buttons (3x3 grid)
        Me.CmdUp.Location  = New System.Drawing.Point(90, 126)
        Me.CmdUp.Name      = "CmdUp"
        Me.CmdUp.Size      = New System.Drawing.Size(50, 30)
        Me.CmdUp.Text      = "▲"
        Me.CmdUp.TabIndex  = 4
        Me.CmdCCW.Location = New System.Drawing.Point(36, 162)
        Me.CmdCCW.Name     = "CmdCCW"
        Me.CmdCCW.Size     = New System.Drawing.Size(50, 30)
        Me.CmdCCW.Text     = "◄"
        Me.CmdCCW.TabIndex = 5
        Me.CmdCW.Location  = New System.Drawing.Point(144, 162)
        Me.CmdCW.Name      = "CmdCW"
        Me.CmdCW.Size      = New System.Drawing.Size(50, 30)
        Me.CmdCW.Text      = "►"
        Me.CmdCW.TabIndex  = 6
        Me.CmdDown.Location = New System.Drawing.Point(90, 198)
        Me.CmdDown.Name    = "CmdDown"
        Me.CmdDown.Size    = New System.Drawing.Size(50, 30)
        Me.CmdDown.Text    = "▼"
        Me.CmdDown.TabIndex = 7
        ' TxtFreccia (focus keeper, hidden)
        Me.TxtFreccia.Location = New System.Drawing.Point(90, 162)
        Me.TxtFreccia.Name     = "TxtFreccia"
        Me.TxtFreccia.Size     = New System.Drawing.Size(50, 30)
        Me.TxtFreccia.TabIndex = 8
        Me.TxtFreccia.Text     = ""
        Me.TxtFreccia.BackColor = System.Drawing.SystemColors.Control
        Me.TxtFreccia.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFreccia.ReadOnly = True
        ' CmdEnd / CmdAnnulla
        Me.CmdEnd.Location     = New System.Drawing.Point(8, 244)
        Me.CmdEnd.Name         = "CmdEnd"
        Me.CmdEnd.Size         = New System.Drawing.Size(90, 30)
        Me.CmdEnd.Text         = "Conferma"
        Me.CmdEnd.BackColor    = System.Drawing.Color.LightGreen
        Me.CmdEnd.TabIndex     = 9
        Me.CmdAnnulla.Location = New System.Drawing.Point(110, 244)
        Me.CmdAnnulla.Name     = "CmdAnnulla"
        Me.CmdAnnulla.Size     = New System.Drawing.Size(90, 30)
        Me.CmdAnnulla.Text     = "Annulla"
        Me.CmdAnnulla.BackColor = System.Drawing.Color.Salmon
        Me.CmdAnnulla.TabIndex = 10
        ' Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize          = New System.Drawing.Size(210, 286)
        Me.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox         = False
        Me.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent
        Me.KeyPreview          = True
        Me.Name = "FrmOffset"
        Me.Text = "Offset Az/El"
        Me.Controls.AddRange(New Control() {
            Me.LblAz, Me.TxtAzOffset, Me.LblEl, Me.TxtElOffset,
            Me.LblDT, Me.TxtDeltaTime, Me.LblInc, Me.CmbInc,
            Me.CmdUp, Me.CmdCCW, Me.TxtFreccia, Me.CmdCW, Me.CmdDown,
            Me.CmdEnd, Me.CmdAnnulla})
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents CmbInc       As System.Windows.Forms.ComboBox
    Friend WithEvents TxtAzOffset  As System.Windows.Forms.TextBox
    Friend WithEvents TxtElOffset  As System.Windows.Forms.TextBox
    Friend WithEvents TxtFreccia   As System.Windows.Forms.TextBox
    Friend WithEvents TxtDeltaTime As System.Windows.Forms.TextBox
    Friend WithEvents CmdCCW       As System.Windows.Forms.Button
    Friend WithEvents CmdCW        As System.Windows.Forms.Button
    Friend WithEvents CmdUp        As System.Windows.Forms.Button
    Friend WithEvents CmdDown      As System.Windows.Forms.Button
    Friend WithEvents CmdEnd       As System.Windows.Forms.Button
    Friend WithEvents CmdAnnulla   As System.Windows.Forms.Button
    Friend           LblAz         As System.Windows.Forms.Label
    Friend           LblEl         As System.Windows.Forms.Label
    Friend           LblInc        As System.Windows.Forms.Label
    Friend           LblDT         As System.Windows.Forms.Label
End Class

