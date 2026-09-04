<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSplash
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
        Me.LblProductName = New System.Windows.Forms.Label()
        Me.LblVersion = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        ' LblProductName
        '
        Me.LblProductName.AutoSize = False
        Me.LblProductName.Font = New System.Drawing.Font("Arial", 18.0F, System.Drawing.FontStyle.Bold)
        Me.LblProductName.ForeColor = System.Drawing.Color.White
        Me.LblProductName.Location = New System.Drawing.Point(20, 30)
        Me.LblProductName.Name = "LblProductName"
        Me.LblProductName.Size = New System.Drawing.Size(460, 40)
        Me.LblProductName.Text = "Product Name"
        '
        ' LblVersion
        '
        Me.LblVersion.AutoSize = False
        Me.LblVersion.Font = New System.Drawing.Font("Arial", 10.0F)
        Me.LblVersion.ForeColor = System.Drawing.Color.White
        Me.LblVersion.Location = New System.Drawing.Point(20, 80)
        Me.LblVersion.Name = "LblVersion"
        Me.LblVersion.Size = New System.Drawing.Size(460, 24)
        Me.LblVersion.Text = "Version"
        '
        ' Label3
        '
        Me.Label3.AutoSize = False
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0F)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(20, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(460, 24)
        '
        ' FrmSplash
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Navy
        Me.ClientSize = New System.Drawing.Size(500, 200)
        Me.Controls.Add(Me.LblProductName)
        Me.Controls.Add(Me.LblVersion)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Splash"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents LblProductName As System.Windows.Forms.Label
    Friend WithEvents LblVersion As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
