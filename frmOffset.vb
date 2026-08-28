Public Class FrmOffset
    Dim IncOffset As Double, Port As Integer, AzOffsetOld, ElOffsetOld, OldDeltaTime
    Private Sub CmbInc_KeyUp(KeyCode As Integer, Shift As Integer)
        IncOffset = CDbl(CmbInc.Text)
    End Sub

    Private Sub CmdAnnulla_Click()
        '        frmRicezione.CmdOffset.Enabled = False
        '        frmRicezione.CmdOffset.Visible = False
        AzOffset = AzOffsetOld
        ElOffset = ElOffsetOld
        frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
        frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
        DeltaTime = OldDeltaTime

        Me.Close()
End Sub

    Private Sub CmdCCW_Click()
        If IsNumeric(TxtAzOffset.Text) Then
            TxtAzOffset.Text = Format$(CDbl(TxtAzOffset.Text) - CDbl(CmbInc.Text), "0.000")
            If CDbl(TxtAzOffset.Text) < -92 Then TxtAzOffset.Text = "-91.99"
            AzOffset = CDbl(TxtAzOffset.Text)
            frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmdCW_Click()
        If IsNumeric(TxtAzOffset.Text) Then
            TxtAzOffset.Text = Format$(CDbl(TxtAzOffset.Text) + CDbl(CmbInc.Text), "0.000")
            If CDbl(TxtAzOffset.Text) > 92 Then TxtAzOffset.Text = "91.99"
            AzOffset = CDbl(TxtAzOffset.Text)
            frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmdDown_Click()
        If IsNumeric(TxtElOffset.Text) Then
            TxtElOffset.Text = Format$(CDbl(TxtElOffset.Text) - CDbl(CmbInc.Text), "0.000")
            If CDbl(TxtElOffset.Text) < -92 Then TxtElOffset.Text = "-91.999"
            ElOffset = CDbl(TxtElOffset.Text)
            frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmdEnd_Click()
        If IsNumeric(TxtAzOffset.Text) Then AzOffset = Format$(CDbl(TxtAzOffset.Text), "0.000")
        If IsNumeric(TxtElOffset.Text) Then ElOffset = Format$(CDbl(TxtElOffset.Text), "0.000")
        '    frmRicezione.CmdOffset.Enabled = False
        '    frmRicezione.CmdOffset.Visible = False
        frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
        frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
        If IsNumeric(TxtDeltaTime.Text) Then DeltaTime = CDbl(TxtDeltaTime.Text)
        Me.Close()
End Sub

    Private Sub CmdUp_Click()
        If IsNumeric(TxtElOffset.Text) Then
            TxtElOffset.Text = Format$(CDbl(TxtElOffset.Text) + CDbl(CmbInc.Text), "0.000")
            If CDbl(TxtElOffset.Text) > 92 Then TxtElOffset.Text = "91.999"
            ElOffset = CDbl(TxtElOffset.Text)
            frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub Form_Activate()
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


    Private Sub TxtAzOffset_KeyUp(KeyCode As Integer, Shift As Integer)
        If KeyCode = CR Then
            If IsNumeric(TxtAzOffset.Text) Then
                If Port_Control(ACU).Socket = "N" Then
                    Port_Control(ACU).Rec_State = PCR_STANDBY
                End If
                If IsNumeric(TxtAzOffset.Text) Then AzOffset = CDbl(TxtAzOffset.Text)
                frmRicezione.LblAzError.Text = Format$(AzOffset, "0.000")
                TxtFreccia.Focus()
            End If
        End If
    End Sub

    Private Sub TxtElOffset_KeyUp(KeyCode As Integer, Shift As Integer)
        If KeyCode = CR Then
            If IsNumeric(TxtElOffset.Text) Then
                ElOffset = CDbl(TxtElOffset.Text)
                frmRicezione.LblElError.Text = Format$(ElOffset, "0.000")
            End If
            TxtFreccia.Focus()
        End If
    End Sub

    Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
        KeyPreview = True
        If (KeyCode = Keys.Up Or KeyCode = Keys.Down Or KeyCode = Keys.Right Or KeyCode = Keys.Left) Then
            Select Case KeyCode
                Case Keys.Up
                    CmdUp_Click()
                Case Keys.Down
                    CmdDown_Click()
                Case Keys.Right
                    CmdCW_Click()
                Case Keys.Left
                    CmdCCW_Click()
            End Select
            '               CmdOK_Click
            KeyCode = 0
        End If
    End Sub
    Private Sub Form_Initialize()
        KeyPreview = True
    End Sub

    Private Sub TxtFreccia_KeyDown(KeyCode As Integer, Shift As Integer)
        If (KeyCode = Keys.Up Or KeyCode = Keys.Down Or KeyCode = Keys.Right Or KeyCode = Keys.Left) Then
            Select Case KeyCode
                Case Keys.Up
                    CmdUp_Click()
                Case Keys.Down
                    CmdDown_Click()
                Case Keys.Right
                    CmdCW_Click()
                Case Keys.Left
                    CmdCCW_Click()
            End Select
            KeyCode = 0
            '                CmdOK_Click
            KeyCode = 0
        End If
    End Sub


    Private Sub CmdMeno_Click()
        TxtDeltaTime.Text = Val(TxtDeltaTime.Text) - 1
        DeltaTime = CDbl(TxtDeltaTime.Text)
    End Sub
    Private Sub CmdPiu_Click()
        TxtDeltaTime.Text = Val(TxtDeltaTime.Text) + 1
        DeltaTime = CDbl(TxtDeltaTime.Text)
    End Sub


End Class
