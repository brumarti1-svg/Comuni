Option Explicit On

Public Class FrmPreset
    Dim s_Step As String, Port As Integer, AzPro As Double, ElPro As Double, PTKold As Integer, i_StatusOld As Integer

    Private Sub CmdAnnulla_Click()
        i_Inter = LeggiFileIni("Canale1", "Inter")
        FrmRicezione.LblAzimuth.Text = ""
        FrmRicezione.LblElevazione.Text = ""
        FrmRicezione.LblAzimuthC.Text = ""
        FrmRicezione.LblElevazioneC.Text = ""
        AzCalc = 0
        ElCalc = 0
        FrmRicezione.LblAzError.Text = ""
        FrmRicezione.LblElError.Text = ""
        FrmRicezione.LblAzErrEn.Text = ""
        FrmRicezione.LblElErrEn.Text = ""
        i_Cont = 1
        FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
        FrmRicezione.TmrTimeoutMSG(ACU).Enabled = True
        KeyPreview = False
        Me.Close()
    End Sub

    Private Sub CmdOK_Click()
        Dim s_Polar As String
        Dim s_program As String
        Dim i As Integer
        On Error Resume Next
        s_program = ""
        MskAzimuth.Text = Format$(MskAzimuth.Text, "000.000")
        MskElevazione.Text = Format$(MskElevazione.Text, "00.000")
        If Not IsNumeric(MskAzimuth.Text) Then
            MskAzimuth.Text = "   .  "
        ElseIf Val(MskAzimuth.Text) < 0 And Val(MskAzimuth.Text) > 359.999 Then
            MskAzimuth.Text = "   .  "
        End If
        If Not IsNumeric(MskElevazione.Text) Then
            MskElevazione.Text = "  .  "
        ElseIf Val(MskElevazione.Text) < 0 And Val(MskElevazione.Text) > 91.999 Then
            MskElevazione.Text = "  .  "
        End If
        If Not IsNumeric(MskAzimuth.Text) And Not IsNumeric(MskElevazione.Text) Then
            KeyPreview = True
            TxtFreccia.Focus()
            Exit Sub
        End If
        b_Ricezione(ACU) = False
        Select Case i_Assi
            Case 0
                s_program = " P " & Format$(MskAzimuth.Text, "000.000") & " " &
                Format$(MskElevazione.Text, "00.000")
                FrmRicezione.LblElevazione.Text = Format$(MskElevazione.Text, "0.000")
                FrmRicezione.LblAzimuth.Text = Format$(MskAzimuth.Text, "0.000")
            Case 1
                s_program = "AP " & Format$(MskAzimuth.Text, "000.000")
                FrmRicezione.LblAzimuth.Text = Format$(MskAzimuth.Text, "0.000")
            Case 2
                s_program = "EP " & Format$(MskElevazione.Text, "00.000")
                FrmRicezione.LblElevazione.Text = Format$(MskElevazione.Text, "0.000")
        End Select
        If IsNumeric(LblAzimuth.Text) Then AzCalc = CDbl(LblAzimuth.Text)
        If IsNumeric(LblElevazione.Text) Then ElCalc = CDbl(LblElevazione.Text)
        i = Len(s_program) + 6
        b_Ricezione(ACU) = False
        FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
        SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
        InvioACU(SEND_ACU)
        b_int(ACU) = True
        KeyPreview = True
        TxtFreccia.Focus()
    End Sub

    Private Sub Form_Activate()
        Select Case i_Assi
            Case 1
                MskElevazione.Visible = False
                MskElevazione.Enabled = False
                LblElevazione.Visible = False
                CmdUp.Enabled = False
                CmdUp.Visible = False
                CmdDown.Enabled = False
                CmdDown.Visible = False
            Case 2
                MskAzimuth.Visible = False
                MskAzimuth.Enabled = False
                LblAzimuth.Visible = False
                CmdCCW.Enabled = False
                CmdCCW.Visible = False
                CmdCW.Enabled = False
                CmdCW.Visible = False
        End Select
        If IsNumeric(s_Az) Then MskAzimuth.Text = Format$(s_Az / 1000, "000.000")
        If IsNumeric(s_El) Then MskElevazione.Text = Format$(s_El / 1000, "00.000")
        CmbInc.Text = "0.01"
        TxtFreccia.Focus()
        KeyPreview = True
    End Sub

    Private Sub Form_Initialize()
        KeyPreview = True
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
        End If
    End Sub

    Private Sub frmPreset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not b_ClosePreset Then
            CmbInc.Text = "0.01"
            b_Ricezione(ACU) = False
            b_int(ACU) = True
            i_Cont = 2
            Attesa(0.2)
            SEND_ACU = Chr(STX) & "09 P 5B" & Chr(ETX)
            InvioACU(SEND_ACU)
            PTKold = PTK
            PTK = &H0
            d_DataInizioProg = ""
            FrmMain.stpProgram.Text = d_DataInizioProg
            i_StatusOld = i_Status
            i_Status = OP_COMMANDPOSITION
            FrmRicezione.LblCommand.Text = "Command Position"
            MskElevazione.Visible = True
            MskElevazione.Enabled = True
            LblElevazione.Visible = True
            MskAzimuth.Visible = True
            MskAzimuth.Enabled = True
            LblAzimuth.Visible = True
            KeyPreview = True
            i_Cont = 0
        End If
    End Sub

    Private Sub CmdCW_Click(sender As Object, e As EventArgs) Handles CmdCW.Click

    End Sub

    Private Sub CmdCCW_Click()
        If IsNumeric(MskAzimuth.Text) Then
            AzPro = CDbl(MskAzimuth.Text)
            AzPro -= CDbl(CmbInc.Text)
        ElseIf IsNumeric(s_Az) Then
            AzPro = (s_Az / 1000) - CDbl(CmbInc.Text)
        Else
            TxtFreccia.Focus()
            Exit Sub
        End If
        MskAzimuth.Text = Format$(AzPro, "000.000")
        If CDbl(MskAzimuth.Text) < 0 Then MskAzimuth.Text = Format$(360 - CDbl(CmbInc.Text), "000.000")
        CmdOK_Click()
    End Sub

    Private Sub CmdCW_Click()
        KeyPreview = True
        If IsNumeric(MskAzimuth.Text) Then
            AzPro = CDbl(MskAzimuth.Text)
            AzPro += CDbl(CmbInc.Text)
        ElseIf IsNumeric(s_Az) Then
            AzPro = (s_Az / 1000) + CDbl(CmbInc.Text)
        Else
            TxtFreccia.Focus()
            Exit Sub
        End If
        MskAzimuth.Text = Format$(AzPro, "000.000")
        If CDbl(MskAzimuth.Text) >= 360 Then MskAzimuth.Text = "0.000"
        CmdOK_Click()
    End Sub

    Private Sub CmdDown_Click()
        KeyPreview = True
        If IsNumeric(MskElevazione.Text) Then
            ElPro = CDbl(MskElevazione.Text)
            ElPro -= CDbl(CmbInc.Text)
        ElseIf IsNumeric(s_El) Then
            ElPro = (s_El / 1000) - CDbl(CmbInc.Text)
        Else
            TxtFreccia.Focus()
            Exit Sub
        End If
        MskElevazione.Text = Format$(ElPro, "00.000")
        If CDbl(MskElevazione.Text) < 0 Then MskElevazione.Text = "  .  "
        CmdOK_Click()
    End Sub

    Private Sub CmdUp_Click()
        KeyPreview = True
        If IsNumeric(MskElevazione.Text) Then
            ElPro = CDbl(MskElevazione.Text)
            ElPro += CDbl(CmbInc.Text)
        ElseIf IsNumeric(s_El) Then
            ElPro = (s_El / 1000) + CDbl(CmbInc.Text)
        Else
            TxtFreccia.Focus()
            Exit Sub
        End If
        MskElevazione.Text = Format$(ElPro, "00.000")
        If CDbl(MskElevazione.Text) >= 90 Then MskElevazione.Text = "90.000"
        CmdOK_Click()
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
        End If
    End Sub

End Class

