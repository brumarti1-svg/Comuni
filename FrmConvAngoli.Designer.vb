<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmConvAngoli
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
        Me.CmbLat = New System.Windows.Forms.ComboBox()
        Me.CmbLong = New System.Windows.Forms.ComboBox()
        Me.TxtGraLat = New System.Windows.Forms.TextBox()
        Me.TxtPrimiLat = New System.Windows.Forms.TextBox()
        Me.TxtSecLat = New System.Windows.Forms.TextBox()
        Me.TxtGraLon = New System.Windows.Forms.TextBox()
        Me.TxtPrimiLon = New System.Windows.Forms.TextBox()
        Me.TxtSecLon = New System.Windows.Forms.TextBox()
        Me.LblLatitudine = New System.Windows.Forms.Label()
        Me.LblLongitudine = New System.Windows.Forms.Label()
        Me.CmdCalcolo = New System.Windows.Forms.Button()
        Me.CmdEsci = New System.Windows.Forms.Button()
        Me.LblHdrLat = New System.Windows.Forms.Label()
        Me.LblHdrLon = New System.Windows.Forms.Label()
        Me.LblHdrGra = New System.Windows.Forms.Label()
        Me.LblHdrPrimi = New System.Windows.Forms.Label()
        Me.LblHdrSec = New System.Windows.Forms.Label()
        Me.LblHdrResult = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        ' Intestazioni colonne
        '
        Me.LblHdrGra.Location = New System.Drawing.Point(85, 12)
        Me.LblHdrGra.Name = "LblHdrGra"
        Me.LblHdrGra.Size = New System.Drawing.Size(60, 20)
        Me.LblHdrGra.Text = "Degrees"
        Me.LblHdrPrimi.Location = New System.Drawing.Point(155, 12)
        Me.LblHdrPrimi.Name = "LblHdrPrimi"
        Me.LblHdrPrimi.Size = New System.Drawing.Size(60, 20)
        Me.LblHdrPrimi.Text = "Minutes"
        Me.LblHdrSec.Location = New System.Drawing.Point(225, 12)
        Me.LblHdrSec.Name = "LblHdrSec"
        Me.LblHdrSec.Size = New System.Drawing.Size(60, 20)
        Me.LblHdrSec.Text = "Seconds"
        Me.LblHdrResult.Location = New System.Drawing.Point(300, 12)
        Me.LblHdrResult.Name = "LblHdrResult"
        Me.LblHdrResult.Size = New System.Drawing.Size(120, 20)
        Me.LblHdrResult.Text = "Decimal"
        '
        ' Latitudine
        '
        Me.LblHdrLat.Location = New System.Drawing.Point(12, 38)
        Me.LblHdrLat.Name = "LblHdrLat"
        Me.LblHdrLat.Size = New System.Drawing.Size(65, 20)
        Me.LblHdrLat.Text = "Latitude"
        Me.CmbLat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLat.Items.AddRange(New Object() {"N", "S"})
        Me.CmbLat.Location = New System.Drawing.Point(12, 56)
        Me.CmbLat.Name = "CmbLat"
        Me.CmbLat.Size = New System.Drawing.Size(65, 21)
        Me.TxtGraLat.Location = New System.Drawing.Point(85, 56)
        Me.TxtGraLat.Name = "TxtGraLat"
        Me.TxtGraLat.Size = New System.Drawing.Size(60, 20)
        Me.TxtPrimiLat.Location = New System.Drawing.Point(155, 56)
        Me.TxtPrimiLat.Name = "TxtPrimiLat"
        Me.TxtPrimiLat.Size = New System.Drawing.Size(60, 20)
        Me.TxtSecLat.Location = New System.Drawing.Point(225, 56)
        Me.TxtSecLat.Name = "TxtSecLat"
        Me.TxtSecLat.Size = New System.Drawing.Size(60, 20)
        Me.LblLatitudine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblLatitudine.Location = New System.Drawing.Point(300, 56)
        Me.LblLatitudine.Name = "LblLatitudine"
        Me.LblLatitudine.Size = New System.Drawing.Size(120, 20)
        '
        ' Longitudine
        '
        Me.LblHdrLon.Location = New System.Drawing.Point(12, 88)
        Me.LblHdrLon.Name = "LblHdrLon"
        Me.LblHdrLon.Size = New System.Drawing.Size(65, 20)
        Me.LblHdrLon.Text = "Longitude"
        Me.CmbLong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLong.Items.AddRange(New Object() {"E", "W"})
        Me.CmbLong.Location = New System.Drawing.Point(12, 106)
        Me.CmbLong.Name = "CmbLong"
        Me.CmbLong.Size = New System.Drawing.Size(65, 21)
        Me.TxtGraLon.Location = New System.Drawing.Point(85, 106)
        Me.TxtGraLon.Name = "TxtGraLon"
        Me.TxtGraLon.Size = New System.Drawing.Size(60, 20)
        Me.TxtPrimiLon.Location = New System.Drawing.Point(155, 106)
        Me.TxtPrimiLon.Name = "TxtPrimiLon"
        Me.TxtPrimiLon.Size = New System.Drawing.Size(60, 20)
        Me.TxtSecLon.Location = New System.Drawing.Point(225, 106)
        Me.TxtSecLon.Name = "TxtSecLon"
        Me.TxtSecLon.Size = New System.Drawing.Size(60, 20)
        Me.LblLongitudine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblLongitudine.Location = New System.Drawing.Point(300, 106)
        Me.LblLongitudine.Name = "LblLongitudine"
        Me.LblLongitudine.Size = New System.Drawing.Size(120, 20)
        '
        ' Pulsanti
        '
        Me.CmdCalcolo.Location = New System.Drawing.Point(85, 148)
        Me.CmdCalcolo.Name = "CmdCalcolo"
        Me.CmdCalcolo.Size = New System.Drawing.Size(90, 28)
        Me.CmdCalcolo.Text = "Calculate"
        Me.CmdEsci.Location = New System.Drawing.Point(185, 148)
        Me.CmdEsci.Name = "CmdEsci"
        Me.CmdEsci.Size = New System.Drawing.Size(90, 28)
        Me.CmdEsci.Text = "Exit"
        '
        ' FrmConvAngoli
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(440, 192)
        Me.Controls.Add(Me.LblHdrGra)
        Me.Controls.Add(Me.LblHdrPrimi)
        Me.Controls.Add(Me.LblHdrSec)
        Me.Controls.Add(Me.LblHdrResult)
        Me.Controls.Add(Me.LblHdrLat)
        Me.Controls.Add(Me.CmbLat)
        Me.Controls.Add(Me.TxtGraLat)
        Me.Controls.Add(Me.TxtPrimiLat)
        Me.Controls.Add(Me.TxtSecLat)
        Me.Controls.Add(Me.LblLatitudine)
        Me.Controls.Add(Me.LblHdrLon)
        Me.Controls.Add(Me.CmbLong)
        Me.Controls.Add(Me.TxtGraLon)
        Me.Controls.Add(Me.TxtPrimiLon)
        Me.Controls.Add(Me.TxtSecLon)
        Me.Controls.Add(Me.LblLongitudine)
        Me.Controls.Add(Me.CmdCalcolo)
        Me.Controls.Add(Me.CmdEsci)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Angle Conversion"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents CmbLat As System.Windows.Forms.ComboBox
    Friend WithEvents CmbLong As System.Windows.Forms.ComboBox
    Friend WithEvents TxtGraLat As System.Windows.Forms.TextBox
    Friend WithEvents TxtPrimiLat As System.Windows.Forms.TextBox
    Friend WithEvents TxtSecLat As System.Windows.Forms.TextBox
    Friend WithEvents TxtGraLon As System.Windows.Forms.TextBox
    Friend WithEvents TxtPrimiLon As System.Windows.Forms.TextBox
    Friend WithEvents TxtSecLon As System.Windows.Forms.TextBox
    Friend WithEvents LblLatitudine As System.Windows.Forms.Label
    Friend WithEvents LblLongitudine As System.Windows.Forms.Label
    Friend WithEvents CmdCalcolo As System.Windows.Forms.Button
    Friend WithEvents CmdEsci As System.Windows.Forms.Button
    Friend WithEvents LblHdrLat As System.Windows.Forms.Label
    Friend WithEvents LblHdrLon As System.Windows.Forms.Label
    Friend WithEvents LblHdrGra As System.Windows.Forms.Label
    Friend WithEvents LblHdrPrimi As System.Windows.Forms.Label
    Friend WithEvents LblHdrSec As System.Windows.Forms.Label
    Friend WithEvents LblHdrResult As System.Windows.Forms.Label

End Class
