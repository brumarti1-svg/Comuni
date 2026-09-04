<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPolare
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
        Me.picGrafico = New System.Windows.Forms.PictureBox()
        Me.CmdEsci = New System.Windows.Forms.Button()
        Me.tmrPolare = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picGrafico
        '
        Me.picGrafico.BackColor = System.Drawing.Color.White
        Me.picGrafico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picGrafico.Location = New System.Drawing.Point(0, 0)
        Me.picGrafico.Name = "picGrafico"
        Me.picGrafico.Size = New System.Drawing.Size(1029, 447)
        Me.picGrafico.TabIndex = 0
        Me.picGrafico.TabStop = False
        '
        'CmdEsci
        '
        Me.CmdEsci.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmdEsci.Location = New System.Drawing.Point(962, 453)
        Me.CmdEsci.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CmdEsci.Name = "CmdEsci"
        Me.CmdEsci.Size = New System.Drawing.Size(68, 23)
        Me.CmdEsci.TabIndex = 1
        Me.CmdEsci.Text = "Esci"
        '
        'tmrPolare
        '
        Me.tmrPolare.Interval = 1000
        '
        'FrmPolare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1040, 483)
        Me.Controls.Add(Me.picGrafico)
        Me.Controls.Add(Me.CmdEsci)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "FrmPolare"
        Me.Text = "Data Analysis (Polar)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.picGrafico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picGrafico As System.Windows.Forms.PictureBox
    Friend WithEvents CmdEsci As System.Windows.Forms.Button
    Friend WithEvents tmrPolare As System.Windows.Forms.Timer

End Class
