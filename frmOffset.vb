Public Class FrmOffset
    Dim IncOffset As Double, Port As Integer, AzOffsetOld, ElOffsetOld, OldDeltaTime

    Private Sub CmbInc_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbInc.KeyUp
        IncOffset = CDbl(CmbInc.Text)
    End Sub

    Private Sub CmdAnnulla_Click(sender As Object, e As EventArgs) Handles CmdAnnulla.Click
        AzOffset = AzOffsetOld
        ElOffset = ElOffsetOld
        frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
        frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
        DeltaTime = OldDeltaTime

        Me.Close()
    End Sub

    Private Sub CmdCCW_Click(sender As Object, e As EventArgs) Handles CmdCCW.Click
        If IsNumeric(TxtAzOffset.Text) Then
            TxtAzOffset.Text = Format$(CDbl(TxtAzOffset.Text) - CDbl(CmbInc.Text), "0.000")
            If CDbl(TxtAzOffset.Text) < -92 Then TxtAzOffset.Text = "-91.99"
            AzOffset = CDbl(TxtAzOffset.Text)
            frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmdCW_Click(sender As Object, e As EventArgs) Handles CmdCW.Click
        If IsNumeric(TxtAzOffset.Text) Then
            TxtAzOffset.Text = Format$(CDbl(TxtAzOffset.Text) + CDbl(CmbInc.Text), "0.000")
            If CDbl(TxtAzOffset.Text) > 92 Then TxtAzOffset.Text = "91.99"
            AzOffset = CDbl(TxtAzOffset.Text)
            frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmdDown_Click(sender As Object, e As EventArgs) Handles CmdDown.Click
        If IsNumeric(TxtElOffset.Text) Then
            TxtElOffset.Text = Format$(CDbl(TxtElOffset.Text) - CDbl(CmbInc.Text), "0.000")
            If CDbl(TxtElOffset.Text) < -92 Then TxtElOffset.Text = "-91.999"
            ElOffset = CDbl(TxtElOffset.Text)
            frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmdEnd_Click(sender As Object, e As EventArgs) Handles CmdEnd.Click
        If IsNumeric(TxtAzOffset.Text) Then AzOffset = CDbl(TxtAzOffset.Text)
        If IsNumeric(TxtElOffset.Text) Then ElOffset = CDbl(TxtElOffset.Text)
        frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
        frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
        If IsNumeric(TxtDeltaTime.Text) Then DeltaTime = CDbl(TxtDeltaTime.Text)
        Me.Close()
    End Sub

    Private Sub CmdUp_Click(sender As Object, e As EventArgs) Handles CmdUp.Click
        If IsNumeric(TxtElOffset.Text) Then
            TxtElOffset.Text = Format$(CDbl(TxtElOffset.Text) + CDbl(CmbInc.Text), "0.000")
            If CDbl(TxtElOffset.Text) > 92 Then TxtElOffset.Text = "91.999"
            ElOffset = CDbl(TxtElOffset.Text)
            frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub FrmOffset_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        TxtAzOffset.Text = AzOffset
        TxtElOffset.Text = ElOffset
        TxtDeltaTime.Text = DeltaTime
        AzOffsetOld = AzOffset
        ElOffsetOld = ElOffset
        OldDeltaTime = DeltaTime
        CmbInc.Text = "0.005"
        IncOffset = CDbl(CmbInc.Text)
        b_int(ACU) = True
        KeyPreview = True
        TxtFreccia.Focus()
    End Sub

    Private Sub TxtAzOffset_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtAzOffset.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtAzOffset.Text) Then
                If Port_Control(ACU).Socket = "N" Then
                    Port_Control(ACU).Rec_State = PCR_STANDBY
                End If
                AzOffset = CDbl(TxtAzOffset.Text)
                frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
                TxtFreccia.Focus()
            End If
        End If
    End Sub

    Private Sub TxtElOffset_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtElOffset.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtElOffset.Text) Then
                ElOffset = CDbl(TxtElOffset.Text)
                frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
            End If
            TxtFreccia.Focus()
        End If
    End Sub

    Private Sub FrmOffset_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        KeyPreview = True
        If (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Left) Then
            Select Case e.KeyCode
                Case Keys.Up
                    CmdUp_Click(Me, EventArgs.Empty)
                Case Keys.Down
                    CmdDown_Click(Me, EventArgs.Empty)
                Case Keys.Right
                    CmdCW_Click(Me, EventArgs.Empty)
                Case Keys.Left
                    CmdCCW_Click(Me, EventArgs.Empty)
            End Select
        End If
    End Sub

    Private Sub TxtFreccia_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtFreccia.KeyDown
        If (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Left) Then
            Select Case e.KeyCode
                Case Keys.Up
                    CmdUp_Click(Me, EventArgs.Empty)
                Case Keys.Down
                    CmdDown_Click(Me, EventArgs.Empty)
                Case Keys.Right
                    CmdCW_Click(Me, EventArgs.Empty)
                Case Keys.Left
                    CmdCCW_Click(Me, EventArgs.Empty)
            End Select
        End If
    End Sub

End Class
