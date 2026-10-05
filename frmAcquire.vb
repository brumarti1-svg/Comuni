Imports System.Windows.Forms
Imports System.Data.SQLite

' ============================================================
' frmAcquire.vb — convertito da frmAcquire.frm (VB6)
' Impostazione parametri Spiral Acquire
' ============================================================
' NOTE DI CONVERSIONE:
'   - SQLite → SQLite INSERT parametrizzato
'   - MaskEdBox → MaskedTextBox
'   - optPreset / optProgram → RadioButton
'   - frmMain.stbMain1.Panels() → accesso via proprietà frmMain
'   - Me.Close() → Me.Close()
'   - On Error Resume Next → Try/Catch
'   - .Text → .Text
' ============================================================

Partial Public Class frmAcquire
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
    End Sub

    ' -------------------------------------------------------
    ' Form Activate — reset stato
    ' -------------------------------------------------------
    Private Sub frmAcquire_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If frmRicezioneRef IsNot Nothing Then
            frmRicezioneRef.TmrSpiralAcquire.Enabled = False
        End If
        DbExecute("DELETE FROM tmpEffemeridi")
        mskDataIni.Text = Now.AddHours(-i_Offset).ToString("dd/MM/yyyy HH:mm:ss")
        mskDataIni.Focus()
        optPreset.Checked = False
        optProgram.Checked = False
        If i_Status = OP_SPIRALACQUIRE Then i_Status = 0
    End Sub

    ' -------------------------------------------------------
    ' Bottone Annulla
    ' -------------------------------------------------------
    Private Sub CmdAnnulla_Click(sender As Object, e As EventArgs) Handles CmdAnnulla.Click
        PTK = 0
        d_DataInizioProg = ""
        If frmMainRef IsNot Nothing Then
            frmMainRef.ProgramStatus = d_DataInizioProg
        End If
        i_AzOffsetLock = 0
        i_ElOffsetLock = 0
        If frmRicezioneRef IsNot Nothing Then
            frmRicezioneRef.TmrSpiralAcquire.Enabled = False
        End If
        Me.Close()
    End Sub

    ' -------------------------------------------------------
    ' Legge i valori dal file INI
    ' -------------------------------------------------------
    Private Sub ImpostaValori()
        Try
            Dim s As String = "Acquire"
            TxtAzRange.Text = LeggiFileIni(s, "RangeAz")
            TxtElRange.Text = LeggiFileIni(s, "RangeEl")
            TxtAzStep.Text = LeggiFileIni(s, "StepAz")
            TxtElStep.Text = LeggiFileIni(s, "StepEl")
            TxtAzStepLock.Text = LeggiFileIni(s, "StepAzLock")
            TxtElStepLock.Text = LeggiFileIni(s, "StepElLock")
            TxtDeltaTime.Text = LeggiFileIni(s, "IntAcq")
            TxtTout.Text = LeggiFileIni(s, "TimeOut")
            TxtCoorAcq.Text = LeggiFileIni(s, "CoorAcq")
            TxtLockThr.Text = LeggiFileIni(s, "LockThr")
        Catch ex As Exception
            WriteError(0, "frmAcquire.ImpostaValori: " & ex.Message)
        End Try
    End Sub

    ' -------------------------------------------------------
    ' Salva i valori nel file INI
    ' -------------------------------------------------------
    Private Sub NuoviValori()
        Dim s As String = "Acquire"
        ScriviFileIni(s, "RangeAz", TxtAzRange.Text)
        ScriviFileIni(s, "RangeEl", TxtElRange.Text)
        ScriviFileIni(s, "StepAz", TxtAzStep.Text)
        ScriviFileIni(s, "StepEl", TxtElStep.Text)
        ScriviFileIni(s, "StepAzLock", TxtAzStepLock.Text)
        ScriviFileIni(s, "StepElLock", TxtElStepLock.Text)
        ScriviFileIni(s, "IntAcq", TxtDeltaTime.Text)
        ScriviFileIni(s, "TimeOut", TxtTout.Text)
        ScriviFileIni(s, "CoorAcq", TxtCoorAcq.Text)
        ScriviFileIni(s, "LockThr", TxtLockThr.Text)
    End Sub

    ' -------------------------------------------------------
    ' Bottone OK / Avvia Spiral Acquire
    ' -------------------------------------------------------
    Private Sub CmdEnd_Click(sender As Object, e As EventArgs) Handles CmdEnd.Click
        If Not optPreset.Checked AndAlso Not optProgram.Checked Then Exit Sub

        ' Validazione data inizio
        If Not IsDate(mskDataIni.Text) Then
            mskDataIni.Focus()
            Exit Sub
        End If
        d_DataInizioProg = mskDataIni.Text
        Dim InizProg As Date = CDate(d_DataInizioProg)

        ' Validazione parametri con range
        If Not ValidaRange(TxtAzRange, 1, 30, i_AzRange) Then Exit Sub
        If Not ValidaRange(TxtElRange, 1, 30, i_ElRange) Then Exit Sub
        If Not ValidaRange(TxtAzStep, 0.01, 1, i_AzStep) Then Exit Sub
        If Not ValidaRange(TxtElStep, 0.01, 1, i_ElStep) Then Exit Sub
        If Not ValidaRange(TxtAzStepLock, 0.01, 1, i_AzStepLock) Then Exit Sub
        If Not ValidaRange(TxtElStepLock, 0.01, 1, i_ElStepLock) Then Exit Sub
        If Not ValidaRange(TxtDeltaTime, 1, 5, i_InterAcq) Then Exit Sub
        MilSecC = i_InterAcq
        If Not ValidaRange(TxtCoorAcq, 0.05, 1, CoorAcq) Then Exit Sub
        If Not ValidaRange(TxtLockThr, -110, -40, i_LockThr) Then Exit Sub

        ' Coordinate antenna
        ' CDbl(mskAzimuth.Text) sotto locale italiano legge il "." come
        ' separatore delle migliaia (non decimale): "180.373" diventava
        ' 180373 invece di 180,373. Val() usa sempre il punto come
        ' decimale, indipendentemente dal locale (stesso bug già noto
        ' altrove in questo progetto, es. CLAUDE.md).
        If IsNumeric(mskAzimuth.Text) Then g_AzTable = CDbl(Val(mskAzimuth.Text))
        If IsNumeric(mskElevazione.Text) Then g_ElTable = CDbl(Val(mskElevazione.Text))

        ' Timeout programma
        If IsNumeric(TxtTout.Text) Then
            Dim tout As Double = CDbl(Val(TxtTout.Text))
            If tout >= 10 AndAlso tout <= 480 Then
                d_DataFineProg = InizProg.AddMinutes(tout).ToString()
            Else
                TxtTout.Focus()
                Exit Sub
            End If
        Else
            d_DataFineProg = "31/12/2999 23:59"
        End If

        b_Acquire = optPreset.Checked
        Debug.WriteLine("frmAcquire.CmdEnd_Click: optPreset.Checked=" & optPreset.Checked & " optProgram.Checked=" & optProgram.Checked & " b_Acquire=" & b_Acquire & " Algoritmo=" & Algoritmo) ' TEMPORANEO: da rimuovere dopo la diagnosi
        If Not b_Acquire Then i_ProgTrack = 1
        i_Quadrati = 1
        i_ConStato = 0

        ' tmpEffemeridi va azzerata qui, all'avvio effettivo dell'acquire,
        ' non solo all'apertura del dialogo (frmAcquire_Activated): altrimenti
        ' righe residue di un run precedente restano visibili nel grafico
        ' "Acquire Trace" fin dal primo redraw, sembrando già completo.
        DbExecute("DELETE FROM tmpEffemeridi")

        ' VB6: Now - CDate(i_Offset/24) — i_Offset è intero in ore, /24
        ' convertiva in giorni OLE (stesso bug già corretto altrove, es.
        ' frmSimulazione.vb): qui era rimasto "/24" anche con AddHours, che
        ' si aspetta già le ore, riducendo lo scarto a pochi minuti invece
        ' delle ore corrette.
        Dim oraNow As Date = Now.AddHours(-i_Offset)

        If InizProg <= oraNow Then
            ' Inserisce coordinata di partenza in tmpEffemeridi
            Try
                ' d_DataInizioProg è in ora locale (da mskDataIni/Now), mentre
                ' tutte le righe successive inserite da Calcolo_Aquire usano
                ' NowUTC() (Ricezione.vb): usare d_DataInizioProg qui creava
                ' un salto di fuso orario tra la prima riga e tutte le altre
                ' in tmpEffemeridi. Si usa NowUTC() anche per il seed, stesso
                ' formato "dd/MM/yyyy"/"HH:mm:ss" di Calcolo_Aquire.
                Dim oraInizioUtc As Date = NowUTC()
                Using Cmd As New SQLiteCommand(
                    "INSERT INTO tmpEffemeridi (date, time, azimuth, elevation, long, lat, orbite, intervallo) " &
                    "VALUES (@d, @t, @az, @el, 0, 0, 1, 0)", CType(dbOrbite, DAODatabase).Connection)
                    Cmd.Parameters.AddWithValue("@d", oraInizioUtc.ToString("dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture))
                    Cmd.Parameters.AddWithValue("@t", oraInizioUtc.ToString("HH:mm:ss", Globalization.CultureInfo.InvariantCulture))
                    Cmd.Parameters.AddWithValue("@az", Format(g_AzTable, "000.000"))
                    Cmd.Parameters.AddWithValue("@el", Format(g_ElTable, "00.000"))
                    Cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                WriteError(0, "frmAcquire.CmdEnd INSERT: " & ex.Message)
            End Try
            i_Status = OP_SPIRALACQUIRE
        Else
            Dim DiffProg As String = Format(InizProg - oraNow, "HH\:mm\:ss")
            If frmMainRef IsNot Nothing Then
                frmMainRef.ProgramStatus = d_DataInizioProg & "  " & DiffProg
            End If
            PTK = 0
            If frmRicezioneRef IsNot Nothing Then
                frmRicezioneRef.LblCommand.Text = "Spiral Acquire Enabled"
            End If
            i_Status = 0
            i_StatusDiff = OP_SPIRALACQUIRE
        End If

        NuoviValori()
        i_Spiral = 1
        i_AzOffsetLock = 0
        i_ElOffsetLock = 0
        AzOffset = 0
        ElOffset = 0
        b_Lock = False
        b_LockOn = False

        If frmRicezioneRef IsNot Nothing Then
            frmRicezioneRef.LblAzErrEn.Visible = True
            frmRicezioneRef.LblElErrEn.Visible = True
            frmRicezioneRef.CmdOffset.Enabled = True
            frmRicezioneRef.CmdOffset.Visible = True
            ' Timer.Interval=0 (o negativo) è illegale e lancia eccezione:
            ' con ValidaRange bypassato per i test, DeltaT potrebbe non
            ' essere nel range 1-5 originale. Minimo di sicurezza 1000ms.
            Dim intervalMs As Integer = CInt(i_InterAcq * 1000)
            frmRicezioneRef.TmrSpiralAcquire.Interval = If(intervalMs > 0, intervalMs, 1000)
            frmRicezioneRef.TmrSpiralAcquire.Enabled = True
        End If

        Me.Close()
    End Sub

    ' -------------------------------------------------------
    ' Helper — valida un campo numerico in un range
    ' -------------------------------------------------------
    ' TEMPORANEO (test): il controllo di range (minVal/maxVal) è disattivato
    ' per poter avviare lo Spiral Acquire anche con valori fuori soglia o
    ' "sporchi", così da testare il comportamento di Calcolo_Aquire/Visual
    ' Acquire senza dover rispettare i vincoli reali. Resta il solo
    ' controllo IsNumeric, altrimenti CDbl più sotto crasherebbe. Da
    ' ripristinare (basta decommentare il blocco originale) a test finito.
    Private Function ValidaRange(Txt As TextBox, minVal As Double, maxVal As Double,
                                  ByRef target As Double) As Boolean
        ' CDbl(Txt.Text) sotto locale italiano legge il "." come separatore
        ' delle migliaia, non decimale (es. "0.5" diventa 5): Val() usa
        ' sempre il punto come decimale, indipendentemente dal locale.
        If IsNumeric(Txt.Text) Then
            target = CDbl(Val(Txt.Text))
            Return True
        End If
        Txt.Focus()
        Return False
        ' --- controllo originale, da ripristinare a test finito ---
        'If IsNumeric(Txt.Text) Then
        '    Dim v As Double = CDbl(Val(Txt.Text))
        '    If v >= minVal AndAlso v <= maxVal Then
        '        target = v
        '        Return True
        '    End If
        'End If
        'Txt.Focus()
        'Return False
    End Function

    ' -------------------------------------------------------
    ' RadioButton Preset — legge posizione attuale antenna
    ' -------------------------------------------------------
    Private Sub optPreset_CheckedChanged(sender As Object, e As EventArgs) Handles optPreset.CheckedChanged
        If Not optPreset.Checked Then Exit Sub
        Try
            If frmRicezioneRef IsNot Nothing Then
                If IsNumeric(frmRicezioneRef.TxtAz.Text) Then
                    mskAzimuth.Text = frmRicezioneRef.TxtAz.Text
                End If
                If IsNumeric(frmRicezioneRef.TxtEl.Text) Then
                    mskElevazione.Text = frmRicezioneRef.TxtEl.Text
                End If
            End If
            grpAcqPar.Visible = True
            CmdEnd.Visible = True
            ImpostaValori()
        Catch ex As Exception
            WriteError(0, "optPreset: " & ex.Message)
        End Try
    End Sub

    ' -------------------------------------------------------
    ' RadioButton Program — calcola coordinata da orbita
    ' -------------------------------------------------------
    Private Sub optProgram_CheckedChanged(sender As Object, e As EventArgs) Handles optProgram.CheckedChanged
        If Not optProgram.Checked Then Exit Sub
        Try
            mskDataIni.Text = Now.AddHours(-i_Offset).ToString("dd/MM/yyyy HH:mm:ss")
            If IsDate(mskDataIni.Text) Then
                AzOffset = 0 : ElOffset = 0
                i_AzOffsetLock = 0 : i_ElOffsetLock = 0
                Calcolo_Coordinate1(mskDataIni.Text)
                If frmRicezioneRef IsNot Nothing Then
                    ' Calcolo_Coordinate1 scrive il risultato in LblAzimuthC/
                    ' LblElevazioneC (colonna "Program"), non in LblAzimuth/
                    ' LblElevazione (colonna "Command", valorizzata solo
                    ' durante il comando vero e proprio) — leggere da qui
                    ' lasciava i campi sempre vuoti.
                    If IsNumeric(frmRicezioneRef.LblElevazioneC.Text) Then
                        g_AzTable = CDbl(frmRicezioneRef.LblAzimuthC.Text)
                        g_ElTable = CDbl(frmRicezioneRef.LblElevazioneC.Text)
                        If g_ElTable < g_ElMin Then g_ElTable = g_ElMin
                        mskAzimuth.Text = Format(g_AzTable, "000.000")
                        mskElevazione.Text = Format(g_ElTable, "00.000")
                        frmRicezioneRef.LblAzimuth.Text = ""
                        frmRicezioneRef.LblElevazione.Text = ""
                        frmRicezioneRef.LblAzimuthC.Text = ""
                        frmRicezioneRef.LblElevazioneC.Text = ""
                        AzCalc = 0 : ElCalc = 0
                        frmRicezioneRef.LblAzError.Text = ""
                        frmRicezioneRef.LblElError.Text = ""
                        AzOffset = 0 : ElOffset = 0
                    End If
                    ' Se LblElevazioneC non è ancora numerico (nessun calcolo
                    ' fatto finora, es. Algoritmo non impostato o nessun
                    ' satellite/TLE caricato) si salta solo la copia della
                    ' posizione: il pannello parametri va comunque mostrato
                    ' più sotto, non deve dipendere da una lettura live già
                    ' disponibile.
                End If
            End If
            grpAcqPar.Visible = True
            CmdEnd.Visible = True
            ImpostaValori()
        Catch ex As Exception
            WriteError(0, "optProgram: " & ex.Message)
        End Try
    End Sub

    ' -------------------------------------------------------
    ' Riferimenti ai form principali
    ' -------------------------------------------------------
    Public frmMainRef As FrmMain
    Public frmRicezioneRef As frmRicezione

End Class

