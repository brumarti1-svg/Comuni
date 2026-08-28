Partial Public Class frmBackPT
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        BackUpPT = CmbBackUpPT.Text
        Me.Close()
    End Sub

    Private Sub frmBackPT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CmbBackUpPT.Text = BackUpPT
    End Sub

End Class
