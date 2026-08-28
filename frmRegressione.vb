Public Class FrmRegressione

    Private Sub CmdExit_Click()
        ReDim pts(0)
        NumProMax = 30
        grado = 2
        Me.Close()
    End Sub

    Private Sub FrmRegressione_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' opt1-4 radio buttons: designer missing
    End Sub

    ' Opt1_Click/Opt2_Click/Opt3_Click/Opt4_Click: radio buttons not migrated

End Class
