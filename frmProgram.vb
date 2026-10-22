Option Explicit On

Public Class FrmProgram

    ' -------------------------------------------------------
    ' Load — verifica b_Calcola, poi popola come Activate
    ' -------------------------------------------------------
    Private Sub FrmProgram_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not b_Calcola Then
            MessageBox.Show("No satellite loaded. Load a satellite first.", "Program Track",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            b_Abort = True
            Me.Close()
            Exit Sub
        End If
        Popolaform()
    End Sub

    ' -------------------------------------------------------
    ' Popola il form con i valori correnti
    ' -------------------------------------------------------
    Private Sub Popolaform()
        ' Coordinate attuali (lette dalle label di frmRicezione)
        ' Imposta data/ora UTC corrente come proposta
        Dim nowUTC As Date = If(s_Ora = "G", DateTime.UtcNow, DateTime.Now)
        MskDataIni.Text = nowUTC.ToString("dd/MM/yyyy HH:mm:ss")
        TxtOre.Text = "999"
        TxtMinuti.Text = "00"
        ' Pulisce i valori calcolati finché l'operatore non fa Verifica
        MskDataIni.Focus()
    End Sub



    ' -------------------------------------------------------
    ' Conferma — attiva Program Track
    ' -------------------------------------------------------
    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        If Not IsDate(MskDataIni.Text) Then
            MskDataIni.Text = If(s_Ora = "G", DateTime.UtcNow, DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss")
            MskDataIni.Focus()
            Exit Sub
        End If

        d_DataInizioProg = MskDataIni.Text
        FrmMain.stpProgram.Text = d_DataInizioProg

        Dim InizProg As Date = CDate(d_DataInizioProg)

        ' Calcola data fine
        If IsNumeric(TxtOre.Text) AndAlso CDbl(TxtOre.Text) < 999 Then
            d_DataFineProg = DateAdd("h", CDbl(TxtOre.Text), d_DataInizioProg)
            If IsNumeric(TxtMinuti.Text) Then
                d_DataFineProg = DateAdd("n", CDbl(TxtMinuti.Text), d_DataFineProg)
            End If
        Else
            d_DataFineProg = "31/12/2999 23.59"
        End If

        Dim nowCalc As Date = If(s_Ora = "G", DateTime.UtcNow, DateTime.Now)

        If InizProg <= nowCalc Then
            ' Inizio già passato: parte subito
            PTK        = &H10
            i_ProgTrack = 1
            Calcolo_Coordinate1(nowCalc)
            If g_Verso Then Calcolo_Coordinate1(nowCalc)
        Else
            ' Inizio futuro: precalcola la posizione iniziale e mette ACU in attesa
            Calcolo_Coordinate1(InizProg)
            If g_Verso Then Calcolo_Coordinate1(InizProg)
            PTK = &H0
            If IsNumeric(frmRicezione.LblElevazione.Text) Then
                g_AzTable = CDbl(frmRicezione.LblAzimuth.Text)
                g_ElTable = CDbl(frmRicezione.LblElevazione.Text)
                If g_ElTable < g_ElMin Then g_ElTable = g_ElMin
                ' Invia posizione iniziale all'ACU e congela i_Cont
                Dim s_program As String = " P " &
                    Format$(g_AzTable, "000.000") & " " &
                    Format$(g_ElTable, "00.000")
                Dim msgLen As Integer = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(msgLen, "00") & Mid$(s_program, 1, Len(s_program)))
                frmRicezione.TmrTimeoutMSG(ACU).Enabled = False
                b_Ricezione(ACU) = False
                b_int(ACU)       = True
                Port_Control(ACU).Rec_State = PCR_WAIT
                i_Cont = 2
                Attesa(0.2)
                InvioACU(SEND_ACU)
            End If
        End If

        b_Abort = False
        Me.Close()
    End Sub

    ' -------------------------------------------------------
    ' Annulla — ripristina e non attiva Program Track
    ' -------------------------------------------------------
    Private Sub CmdAnnulla_Click(sender As Object, e As EventArgs) Handles CmdAnnulla.Click
        frmRicezione.LblAzimuth.Text    = ""
        frmRicezione.LblElevazione.Text = ""
        frmRicezione.LblAzimuthC.Text   = ""
        frmRicezione.LblElevazioneC.Text = ""
        AzCalc = 0
        ElCalc = 0
        d_DataInizioProg = ""
        FrmMain.stpProgram.Text = d_DataInizioProg
        b_Abort = True
        Me.Close()
    End Sub

    ' -------------------------------------------------------
    ' KeyUp su MskDataIni — invio = verifica
    ' -------------------------------------------------------
    Private Sub MskDataIni_KeyUp(sender As Object, e As KeyEventArgs) Handles MskDataIni.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsDate(MskDataIni.Text) Then
                Calcolo_Coordinate1(CDate(MskDataIni.Text))
                TxtOre.Focus()
            Else
                MskDataIni.Text = If(s_Ora = "G", DateTime.UtcNow, DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss")
                MskDataIni.Focus()
            End If
        End If
    End Sub

    ' -------------------------------------------------------
    ' KeyUp su TxtOre — invio sposta focus
    ' -------------------------------------------------------
    Private Sub TxtOre_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtOre.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtOre.Text) AndAlso CDbl(TxtOre.Text) < 999 Then
                TxtMinuti.Focus()
            End If
        End If
    End Sub

    Private Sub CmdLoad_Click_1(sender As Object, e As EventArgs) Handles CmdLoad.Click
        If Not IsDate(MskDataIni.Text) Then
            MskDataIni.Text = If(s_Ora = "G", DateTime.UtcNow, DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss")
            MskDataIni.Focus()
        End If
        Dim dataRichiesta As Date = CDate(MskDataIni.Text)
        If Not g_Verso Then Calcolo_Coordinate1(dataRichiesta)
        Calcolo_Coordinate1(dataRichiesta)
        ' Legge i valori aggiornati dalle label
    End Sub
End Class
