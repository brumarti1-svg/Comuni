Option Explicit On

Public Class FrmPreset
    Dim s_Step As String, Port As Integer, AzPro As Double, ElPro As Double, PTKold As Integer, i_StatusOld As Integer

    Private Declare Function ReleaseCapture Lib "user32.dll" () As Boolean
    Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = &H2

    ' FixedDialog è già spostabile dalla barra del titolo; questo handler
    ' permette di trascinare la finestra anche cliccando sulle etichette
    ' statiche o sull'area del form, non solo sulla barra del titolo.
    Private Sub TrascinaFinestra(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, LblAzimuth.MouseDown, LblElevazione.MouseDown, LblInc.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        End If
    End Sub

    ' CDbl() sul locale italiano interpreta il punto come separatore delle
    ' migliaia, non decimale: CDbl("0.01") = 1, CDbl("45.000") = 45000. I
    ' letterali con il punto usati in questo form (CmbInc, "0.000"/"90.000")
    ' andrebbero quindi in pezzi non appena coinvolti in un calcolo. Val()
    ' interpreta sempre il punto come decimale a prescindere dal locale, ma
    ' si ferma alla virgola — qui la si normalizza prima, così funziona
    ' anche sui valori "045,123" prodotti da Format$ in questo stesso form.
    Private Function ParseGrad(testo As String) As Double
        Return Val(testo.Replace(","c, "."c))
    End Function

    Private Sub CmdAnnulla_Click(sender As Object, e As EventArgs) Handles CmdAnnulla.Click
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

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        InviaComando()
        Me.Close()
    End Sub

    ' Invio del comando all'ACU, senza chiudere la maschera: usato sia dal
    ' bottone Execute (che poi chiude) sia dalle frecce di incremento (che
    ' devono restare aperte per permettere aggiustamenti successivi).
    Private Sub InviaComando()
        Dim s_Polar As String
        Dim s_program As String
        Dim i As Integer
        On Error Resume Next
        s_program = ""
        ' Format$(stringa, "000.000") con una STRINGA come primo argomento
        ' restituisce sempre "000.000"/zero (verificato: non riconverte la
        ' stringa in numero come faceva VB6) — a differenza di
        ' Format$(numero, "000.000"), che funziona correttamente. Ogni
        ' Format$ qui sotto deve quindi ricevere un valore numerico
        ' (ParseGrad/CDbl), mai .Text direttamente.
        MskAzimuth.Text = Format$(ParseGrad(MskAzimuth.Text), "000.000")
        MskElevazione.Text = Format$(ParseGrad(MskElevazione.Text), "00.000")
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
                s_program = " P " & Format$(ParseGrad(MskAzimuth.Text), "000.000") & " " &
                Format$(ParseGrad(MskElevazione.Text), "00.000")
                FrmRicezione.LblElevazione.Text = Format$(ParseGrad(MskElevazione.Text), "0.000")
                FrmRicezione.LblAzimuth.Text = Format$(ParseGrad(MskAzimuth.Text), "0.000")
            Case 1
                s_program = "AP " & Format$(ParseGrad(MskAzimuth.Text), "000.000")
                FrmRicezione.LblAzimuth.Text = Format$(ParseGrad(MskAzimuth.Text), "0.000")
            Case 2
                s_program = "EP " & Format$(ParseGrad(MskElevazione.Text), "00.000")
                FrmRicezione.LblElevazione.Text = Format$(ParseGrad(MskElevazione.Text), "0.000")
        End Select
        i = Len(s_program) + 6
        b_Ricezione(ACU) = False
        FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
        SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
        InvioACU(SEND_ACU)
        b_int(ACU) = True
        KeyPreview = True
        TxtFreccia.Focus()
    End Sub

    ' Visibilità assi + coordinate attuali: fattorizzata per poter essere
    ' chiamata anche da Load, non solo da Activated. Activated scatta DOPO
    ' che la finestra è già visibile — nello spazio fra la comparsa della
    ' finestra (con i valori di default del Designer "000.000"/"00.000") e
    ' l'esecuzione di questo codice, si vede per un istante il placeholder
    ' invece delle coordinate reali (segnalato dall'utente come "azzeramento").
    Private Sub PopolaCoordinateAttuali()
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
    End Sub

    Private Sub FrmPreset_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        PopolaCoordinateAttuali()
        TxtFreccia.Focus()
        KeyPreview = True
    End Sub

    Private Sub FrmPreset_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
            PopolaCoordinateAttuali()
            KeyPreview = True
            i_Cont = 0
        End If
    End Sub

    Private Sub CmdCCW_Click(sender As Object, e As EventArgs) Handles CmdCCW.Click
        If IsNumeric(MskAzimuth.Text) Then
            AzPro = ParseGrad(MskAzimuth.Text)
            AzPro -= ParseGrad(CmbInc.Text)
        ElseIf IsNumeric(s_Az) Then
            AzPro = (s_Az / 1000) - ParseGrad(CmbInc.Text)
        Else
            TxtFreccia.Focus()
            Exit Sub
        End If
        MskAzimuth.Text = Format$(AzPro, "000.000")
        If ParseGrad(MskAzimuth.Text) < 0 Then MskAzimuth.Text = Format$(360 - ParseGrad(CmbInc.Text), "000.000")
        InviaComando()
    End Sub

    Private Sub CmdCW_Click(sender As Object, e As EventArgs) Handles CmdCW.Click
        KeyPreview = True
        If IsNumeric(MskAzimuth.Text) Then
            AzPro = ParseGrad(MskAzimuth.Text)
            AzPro += ParseGrad(CmbInc.Text)
        ElseIf IsNumeric(s_Az) Then
            AzPro = (s_Az / 1000) + ParseGrad(CmbInc.Text)
        Else
            TxtFreccia.Focus()
            Exit Sub
        End If
        MskAzimuth.Text = Format$(AzPro, "000.000")
        If ParseGrad(MskAzimuth.Text) >= 360 Then MskAzimuth.Text = Format$(0, "000.000")
        InviaComando()
    End Sub

    Private Sub CmdDown_Click(sender As Object, e As EventArgs) Handles CmdDown.Click
        KeyPreview = True
        If IsNumeric(MskElevazione.Text) Then
            ElPro = ParseGrad(MskElevazione.Text)
            ElPro -= ParseGrad(CmbInc.Text)
        ElseIf IsNumeric(s_El) Then
            ElPro = (s_El / 1000) - ParseGrad(CmbInc.Text)
        Else
            TxtFreccia.Focus()
            Exit Sub
        End If
        MskElevazione.Text = Format$(ElPro, "00.000")
        If ParseGrad(MskElevazione.Text) < 0 Then MskElevazione.Text = "  .  "
        InviaComando()
    End Sub

    Private Sub CmdUp_Click(sender As Object, e As EventArgs) Handles CmdUp.Click
        KeyPreview = True
        If IsNumeric(MskElevazione.Text) Then
            ElPro = ParseGrad(MskElevazione.Text)
            ElPro += ParseGrad(CmbInc.Text)
        ElseIf IsNumeric(s_El) Then
            ElPro = (s_El / 1000) + ParseGrad(CmbInc.Text)
        Else
            TxtFreccia.Focus()
            Exit Sub
        End If
        MskElevazione.Text = Format$(ElPro, "00.000")
        If ParseGrad(MskElevazione.Text) >= 90 Then MskElevazione.Text = Format$(90, "00.000")
        InviaComando()
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

