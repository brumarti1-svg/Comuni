Imports System.Reflection

Public Class FrmSplash

    Private Sub FrmSplash_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ver As Version = Assembly.GetExecutingAssembly().GetName().Version
        LblVersion.Text = "Version " & ver.Major & "." & ver.Minor & "." & ver.Revision
        LblProductName.Text = My.Application.Info.Title
        Label3.Text = g_3Riga
    End Sub

End Class
