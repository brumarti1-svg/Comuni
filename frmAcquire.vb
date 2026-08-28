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
        mskDataIni.Text = Now.AddHours(-i_Offset / 24).ToString("dd/MM/yyyy HH:mm:ss")
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
        If IsNumeric(mskAzimuth.Text) Then g_AzTable = CDbl(mskAzimuth.Text)
        If IsNumeric(mskElevazione.Text) Then g_ElTable = CDbl(mskElevazione.Text)

        ' Timeout programma
        If IsNumeric(TxtTout.Text) Then
            Dim tout As Double = CDbl(TxtTout.Text)
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
        If Not b_Acquire Then i_ProgTrack = 1
        i_Quadrati = 1
        i_ConStato = 0

        Dim oraNow As Date = Now.AddHours(-i_Offset / 24)

        If InizProg <= oraNow Then
            ' Inserisce coordinata di partenza in tmpEffemeridi
            Try
                Using Cmd As New SQLiteCommand(
                    "INSERT INTO tmpEffemeridi (date, time, azimuth, elevation, long, lat, orbite, intervallo) " &
                    "VALUES (@d, @t, @az, @el, 0, 0, 1, 0)", dbOrbite)
                    Cmd.Parameters.AddWithValue("@d", Mid(d_DataInizioProg, 1, 10))
                    Cmd.Parameters.AddWithValue("@t", Mid(d_DataInizioProg, 12, 8))
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
            frmRicezioneRef.TmrSpiralAcquire.Interval = CInt(i_InterAcq * 1000)
            frmRicezioneRef.TmrSpiralAcquire.Enabled = True
        End If

        Me.Close()
    End Sub

    ' -------------------------------------------------------
    ' Helper — valida un campo numerico in un range
    ' -------------------------------------------------------
    Private Function ValidaRange(Txt As TextBox, minVal As Double, maxVal As Double,
                                  ByRef target As Double) As Boolean
        If IsNumeric(Txt.Text) Then
            Dim v As Double = CDbl(Txt.Text)
            If v >= minVal AndAlso v <= maxVal Then
                target = v
                Return True
            End If
        End If
        Txt.Focus()
        Return False
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
            mskDataIni.Text = Now.AddHours(-i_Offset / 24).ToString("dd/MM/yyyy HH:mm:ss")
            If IsDate(mskDataIni.Text) Then
                AzOffset = 0 : ElOffset = 0
                i_AzOffsetLock = 0 : i_ElOffsetLock = 0
                Calcolo_Coordinate1(mskDataIni.Text)
                If frmRicezioneRef IsNot Nothing Then
                    If IsNumeric(frmRicezioneRef.LblElevazione.Text) Then
                        g_AzTable = CDbl(frmRicezioneRef.LblAzimuth.Text)
                        g_ElTable = CDbl(frmRicezioneRef.LblElevazione.Text)
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
                    Else
                        Exit Sub
                    End If
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

