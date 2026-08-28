Public Class frmAutoTrackBackPT

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        BackUpAutoT = CmbBackUpPT.Text
        SEND_ACU = CheckSum("10BM " & BackUpAutoT)
        b_Ricezione(ACU) = False
        Attesa(0.1)
        InvioACU(SEND_ACU)
        Me.Close()
    End Sub

    Private Sub frmAutoTrackBackPT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CmbBackUpPT.Text = BackUpAutoT
    End Sub

End Class

