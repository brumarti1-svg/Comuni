Imports System.Reflection.Emit

Public Class FrmRicevitore
    ' Le dichiarazioni dei controlli sono in FrmRicevitore.Designer.vb
    Friend tmrTimeoutMSG1() As System.Windows.Forms.Timer

    Dim d_Valore As String
    Dim i_Auto As Integer, i_AutoAugmented As Integer
    Dim PhaseOldEl As Integer, PhaseOldAz As Integer, d_Attesa As Date, PhaseNew As Integer, ErrorElOld As System.Single, ErrorAzOld As System.Single, OldPhaseEl As Integer, OldPhaseAz As Integer
    Dim s_Step As String, SEND_RX As String, Port As Integer, AzPro As Double, ElPro As Double
    Dim PTKold As Integer, i_StatusOld As Integer, b_Vai As Boolean, i_cont1 As Integer
    Dim f_InFreq As System.Single, i_InputPort As Integer, f_Acquisition As System.Single, i_OpMode As Integer
    Dim MaxAz As Double, MaxEl As Double, tmpSQL As String
    Dim PhaseNewAz As Integer, PhaseNewEl As Integer, IncEl As System.Single, IncEl1 As System.Single
    Private Sub CmbAFC_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbAFC.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbAFC.Text
                Case "Off"
                    d_Valore = 0
                Case 10
                    d_Valore = 1
                Case 100
                    d_Valore = 2
                Case 1000
                    d_Valore = 3
                Case 1
                    d_Valore = 4
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()

    End Sub

    Private Sub CmbAGC_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbAGC.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbAGC.Text
                Case 1
                    d_Valore = 0
                Case 10
                    d_Valore = 1
                Case 100
                    d_Valore = 2
                Case 1000
                    d_Valore = 3
                Case 0.1
                    d_Valore = 4
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()

    End Sub

    Private Sub CmbBandWidth_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbBandWidth.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbBandWidth.Text
                Case 10000
                    d_Valore = 0
                Case 1000
                    d_Valore = 1
                Case 100
                    d_Valore = 2
                Case 5000
                    d_Valore = 3
                Case 3000
                    d_Valore = 4
                Case 1500
                    d_Valore = 5
                Case 500
                    d_Valore = 6
                Case 300
                    d_Valore = 7
                Case 150
                    d_Valore = 8
                Case 50
                    d_Valore = 9
                Case 30
                    d_Valore = 10
                Case 15
                    d_Valore = 11
                Case 10
                    d_Valore = 12
                Case 13
                    d_Valore = 13
                Case 5
                    d_Valore = 14
                Case "None"
                    d_Valore = 15
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmbCarrier_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbCarrier.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbCarrier.Text
                Case "Normal"
                    d_Valore = 0
                Case "High"
                    d_Valore = 1
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmbEl_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbEl.KeyUp
        'impostato lo step size per le coordinate lo invio alla ACU
        If e.KeyCode = CR Then
            i_Phase = OldPhaseEl
            TxtFreccia.Focus()
        End If
    End Sub



    Private Sub CmbInc_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbInc.KeyUp
        If e.KeyCode = CR Then
            TxtFreccia.Focus()
        End If
    End Sub

    Private Sub CmbSelection_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbSelection.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbSelection.Text
                Case "Nominal"
                    d_Valore = 0
                Case "Alternate"
                    d_Valore = 1
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmbLoop_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbLoop.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbLoop.Text
                Case 100
                    d_Valore = 0
                Case 300
                    d_Valore = 1
                Case 1000
                    d_Valore = 2
                Case 3000
                    d_Valore = 3
                Case 30
                    d_Valore = 4
                Case 10
                    d_Valore = 5
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()

    End Sub


    Private Sub CmbOperMode_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbOperMode.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbOperMode.Text
                Case "Automatic"
                    d_Valore = 0
                Case "Manual"
                    d_Valore = 1
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()
    End Sub

    Private Sub CmbRevAz_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbRevAz.KeyUp
        If CmbRevAz.Text = "N" Then
            d_Valore = 0
        Else
            d_Valore = 1
        End If
        '    tmrPhasing.Enabled = False
        'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
        '    tmrPhase.Enabled = True
    End Sub


    Private Sub CmbInputPort_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbInputPort.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbInputPort.Text
                Case "IF Port #1"
                    d_Valore = 0
                Case "IF Port #2"
                    d_Valore = 3
                Case "IF Port #3"
                    d_Valore = 4
                Case "Auxiliary #1"
                    d_Valore = 5
                Case "Auxiliary #2"
                    d_Valore = 6
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()

    End Sub

    Private Sub CmbVideo_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbVideo.KeyUp
        If e.KeyCode = CR Then
            Select Case CmbVideo.Text
                Case "PM"
                    d_Valore = 0
                Case "FM"
                    d_Valore = 1
                Case "Spectrum"
                    d_Valore = 2
                Case "By-pass"
                    d_Valore = 3
                Case "AM Ncohe"
                    d_Valore = 4
                Case "AM Cohe"
                    d_Valore = 5
                Case Else
                    TxtFreccia.Focus()
                    Exit Sub
            End Select
            'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
            'Attesa 0.4
            Interroga = True
        End If
        TxtFreccia.Focus()

    End Sub

    Private Sub CmdCCW_Click(sender As Object, e As EventArgs) Handles CmdCCW.Click
        If AzInibhit Then Exit Sub
        If IsNumeric(AzPro) Then
            '        AzPro = CDbl(TxtAz)
            AzPro -= CDbl(CmbEl.Text)
            '    ElseIf IsNumeric(s_Az) Then
            '        AzPro = (s_Az / 100) - CDbl(CmbEl.Text)
        Else
            Exit Sub
        End If
        If AzPro < 0 Then AzPro += 360

        '    TxtAz.Text = Format$(AzPro, "000.000")
        '    If TxtAz < 0 Then TxtAz = 360 - CDbl(CmbEl.Text)
        b_Vai = True
    End Sub
    Private Sub CmdCW_Click(sender As Object, e As EventArgs) Handles CmdCW.Click
        If AzInibhit Then Exit Sub
        If IsNumeric(AzPro) Then
            '        AzPro = CDbl(TxtAz)
            AzPro += CDbl(CmbEl.Text)
            '    ElseIf IsNumeric(s_Az) Then
            '        AzPro = (s_Az / 100) + CDbl(CmbEl.Text)
        Else
            Exit Sub
        End If
        If AzPro >= 360 Then AzPro -= 360
        '    TxtAz.Text = Format$(AzPro, "000.000")
        '    If TxtAz >= 360 Then TxtAz = 0
        b_Vai = True
    End Sub

    Private Sub CmdDown_Click(sender As Object, e As EventArgs) Handles CmdDown.Click
        If ElInibhit Then Exit Sub
        If IsNumeric(ElPro) Then
            '        ElPro = CDbl(TxtEl)
            ElPro -= CDbl(CmbEl.Text)
            '    ElseIf IsNumeric(s_El) Then
            '        ElPro = (s_El / 100) - CDbl(CmbEl.Text)
        Else
            Exit Sub
        End If
        '    TxtEl.Text = Format$(ElPro, "00.000")
        If ElPro < 0 Then ElPro = 0
        b_Vai = True
    End Sub

    Private Sub CmdMenoAz_Click(sender As Object, e As EventArgs) Handles CmdMenoAz.Click
        If IsNumeric(TxtPhAz.Text) Then
            TxtPhAz.Text = CStr(CDbl(TxtPhAz.Text) - CDbl(CmbInc.Text))
        ElseIf IsNumeric(TxtPhaseAz.Text) Then
            TxtPhAz.Text = TxtPhaseAz.Text
            TxtPhAz.Text = CStr(CDbl(TxtPhAz.Text) - CDbl(CmbInc.Text))
        End If
        If IsNumeric(TxtPhAz.Text) Then
            If CDbl(TxtPhAz.Text) < 0 Then TxtPhAz.Text = "359"
            d_Valore = Format$(CDbl(TxtPhAz.Text), "0")
            If CDbl(TxtPhAz.Text) >= 0 Then d_Valore = "+" & d_Valore
            '    tmrPhase.Enabled = False
            InvioPhase(d_Valore, 0, 22, ComponentCode)
        End If
        '    tmrPhase.Enabled = True
    End Sub

    Private Sub CmdPiuAz_Click(sender As Object, e As EventArgs) Handles CmdPiuAz.Click
        If IsNumeric(TxtPhAz.Text) Then
            TxtPhAz.Text = CStr(CDbl(TxtPhAz.Text) + CDbl(CmbInc.Text))
        ElseIf IsNumeric(TxtPhaseAz.Text) Then
            TxtPhAz.Text = TxtPhaseAz.Text
            TxtPhAz.Text = CStr(CDbl(TxtPhAz.Text) + CDbl(CmbInc.Text))
        End If
        If IsNumeric(TxtPhAz.Text) Then
            If CDbl(TxtPhAz.Text) > 359 Then TxtPhAz.Text = "1"
            d_Valore = Format$(CDbl(TxtPhAz.Text), "0")
            If CDbl(TxtPhAz.Text) >= 0 Then d_Valore = "+" & d_Valore
        End If
        '    tmrPhase.Enabled = False
        'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')'
        '    tmrPhase.Enabled = True

    End Sub

    Private Sub CmdUp_Click(sender As Object, e As EventArgs) Handles CmdUp.Click
        If ElInibhit Then Exit Sub
        If IsNumeric(ElPro) Then
            '        ElPro = CDbl(TxtEl)
            ElPro += CDbl(CmbEl.Text)
            '    ElseIf IsNumeric(s_El) Then
            '        ElPro = (s_El / 100) + CDbl(CmbEl.Text)
        Else
            Exit Sub
        End If
        '    TxtEl.Text = Format$(ElPro, "00.000")
        If ElPro >= 90 Then ElPro = 90
        b_Vai = True
    End Sub
    Private Sub FineFase()
        i_Auto = 0
        i_AutoAugmented = 0
        g_NewFrequency = True
        '    tmrPhasing.Enabled = False
        '    Attesa 0.5
        '   InvioACU(Chr(STX) & "08 S79" & Chr(ETX)         'metto l'ACU in Stand-by
        Me.frmManual.Visible = False
        '    CmdMeno.Enabled = False
        '    CmdPiu.Enabled = False
        '    frmRicezione.tmrTimeoutMSG(ACU).Enabled = False
        '    Port = Canali(ACU).Port_IN
        '    frmRicezione.tmrTimeoutMSG(ACU).interval = Port_control(acu).Time_out * 1000
    End Sub

    Private Sub CmdAnnulla_Click(sender As Object, e As EventArgs) Handles CmdAnnulla.Click
        FineFase()
        Me.frmManual.Visible = False
        d_Valore = Format$(PhaseOldEl, "0")
        'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
        'Attesa 0.5
        d_Valore = Format$(PhaseOldAz, "0")
        'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
        '04/12/2018    Aggiunto per permettere lo scambio con l'ACU
        i_Cont = 1
    End Sub

    Private Sub CmdMeno_Click(sender As Object, e As EventArgs) Handles CmdMeno.Click
        If Not IsNumeric(TxtPhase.Text) AndAlso IsNumeric(TxtPhaseEl.Text) Then
            TxtPhase.Text = TxtPhaseEl.Text
        End If
        If IsNumeric(TxtPhase.Text) Then
            TxtPhase.Text = CStr(CDbl(TxtPhase.Text) - CDbl(CmbInc.Text))
            If CDbl(TxtPhase.Text) < 0 Then TxtPhase.Text = "359"
            d_Valore = Format$(CDbl(TxtPhase.Text), "###0")
            If CDbl(TxtPhase.Text) >= 0 Then d_Valore = "+" & d_Valore
        End If
        '    tmrPhase.Enabled = False
        'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')' 
        '    tmrPhase.Enabled = True
    End Sub

    Private Sub CmdSave_Click(sender As Object, e As EventArgs) Handles CmdSave.Click
        Dim rs_Save As Object
        Dim tmpSQL As String
        Dim b_Open As Boolean
        Try
        If Not IsNumeric(TxtPhaseEl.Text) And Not IsNumeric(TxtFrequency.Text) And Not IsNumeric(TxtLna.Text) And TxtPol.Text = "" Then
            FrmMain.stpMessaggio.Text = "Wrong data"
            FrmMain.TmrMsg.Enabled = True
            Exit Sub
        End If
        '    tmpSQL = "SELECT * FROM [Fasi] where Ultima "
        '19/10/2022
        DbExecute("UPDATE [Fasi] SET Ultima = " & CStr(vbUnchecked))
        b_Checked = False
        DbExecute("UPDATE [Fasi] SET FaseEl = " & TxtPhaseEl.Text & " WHERE frequenza = '" & Format$(CDbl(TxtFrequency.Text) / 1000, "#0000.00#") & "' and Pol = '" & TxtPol.Text & "' and Lna ='" & TxtLna.Text & "' and CirLin= '" & TxtPol1.Text & "'")

        If TxtPol.Text = "" Then TxtPol.Text = "RHCP"
        If s_Satellite = "REMOTE" Then
            tmpSQL = "SELECT * FROM [Fasi] where Sat ='" & FrmRicezione.LblSatellite.Text & "'"  'Nel caso che il satellite sia "REMOTE" imposto una sola stringa di frequenza
        Else
            '                tmpSQL = "SELECT * FROM [Fasi] where sat ='" & LblSatellite.Text & "' and frequenza = '" & Format$(CDbl(TxtFrequency.Text) / 1000, "#0000.000") & "' and Pol = '" & TxtPol.Text & "' and Lna ='" & TxtLna.Text & "' and CirLin= '" & TxtPol1.Text & "' and PolErr = '" & TxtPol2 & "'"
            tmpSQL = "SELECT * FROM [Fasi] where sat ='" & LblSatellite.Text & "' and frequenza = '" & Format$(CDbl(TxtFrequency.Text) / 1000, "#0000.00#") & "' and Pol = '" & TxtPol.Text & "' and Lna ='" & TxtLna.Text & "' and CirLin= '" & TxtPol1.Text & "'"      '25/11/2022
        End If
        ' modificato il 04/11/2017
        rs_Save = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
        b_Open = True
        If Not rs_Save.BOF Then
            rs_Save.Edit
            rs_Save!faseEl = TxtPhaseEl.Text
            rs_Save!faseAz = TxtPhaseAz.Text
            rs_Save!RevAzErr = LblRevAz.Text
            If IsNumeric(TxtOffsetAz.Text) Then rs_Save!OffsetAz = TxtOffsetAz.Text
            If IsNumeric(TxtOffsetEl.Text) Then rs_Save!OffsetEl = TxtOffsetEl.Text
            If IsNumeric(g_ValPol) And TxtPol1.Text = "Linear" Then
                rs_Save!polangle = g_ValPol
            Else
                rs_Save!polangle = 0
            End If
        Else
            rs_Save.AddNew
            rs_Save!sat = LblSatellite.Text
            rs_Save!FREQUENZA = Format$(CDbl(TxtFrequency.Text) / 1000, "#0000.00#")
            rs_Save!POL = TxtPol.Text
            rs_Save!LNA = TxtLna.Text
            rs_Save!faseEl = TxtPhaseEl.Text
            rs_Save!faseAz = TxtPhaseAz.Text
            rs_Save!RevAzErr = LblRevAz.Text
            rs_Save!CirLin = TxtPol1.Text
            '                rs_Save!PolErr = TxtPol2
            If IsNumeric(TxtOffsetAz.Text) Then rs_Save!OffsetAz = TxtOffsetAz.Text
            If IsNumeric(TxtOffsetEl.Text) Then rs_Save!OffsetEl = TxtOffsetEl.Text
            '23/07/2012
            If TxtPol1.Text = "Linear" Then
                rs_Save!polangle = g_ValPol
            Else
                rs_Save!polangle = 0
                LblPol.Text = ""
            End If
        End If
        rs_Save!Ultima = vbChecked
        b_Checked = True
        rs_Save.Update
        rs_Save.Close
        b_Open = False
        g_PhaseEl = TxtPhaseEl.Text
        g_PhaseAz = TxtPhaseAz.Text
        g_RevAz = LblRevAz.Text
        g_Frequenza = Format$(CDbl(TxtFrequency.Text) / 1000, "#0000.00#")
        g_LNA = TxtLna.Text
        g_Polar = TxtPol.Text
        ' frmRicezione.LblPolRic.Text = g_Polar
        ' frmRicezione.TxtPhase.Text = CStr(g_PhaseEl)
        ' frmRicezione.TxtPhAz.Text = CStr(g_PhaseAz)
        ' frmRicezione.TxtRevAz = g_RevAz
        Me.frmManual.Visible = False
        '    CmdMeno.Enabled = False
        '    CmdPiu.Enabled = False
        Me.frmManual.Visible = False
        i_Auto = 0
        '    tmrPhasing.Enabled = False
        '    LanReset1 LanCtrl
        FineFase()
        Catch ex As Exception
            If b_Open Then
                b_Open = False
                rs_Save.Close
            End If
            WriteError(0, "CmdSave_Click: " & ex.Message)
        End Try
    End Sub

    Private Sub CmdPiu_Click(sender As Object, e As EventArgs) Handles CmdPiu.Click
        If IsNumeric(TxtPhase.Text) Then
            TxtPhase.Text = CStr(CDbl(TxtPhase.Text) + CDbl(CmbInc.Text))
        ElseIf IsNumeric(TxtPhaseEl.Text) Then
            TxtPhase.Text = TxtPhaseEl.Text
            TxtPhase.Text = CStr(CDbl(TxtPhase.Text) + CDbl(CmbInc.Text))
        End If
        If IsNumeric(TxtPhase.Text) Then
            If CDbl(TxtPhase.Text) > 359 Then TxtPhase.Text = "1"
            d_Valore = Format$(CDbl(TxtPhase.Text), "##0")
            If CDbl(TxtPhase.Text) >= 0 Then d_Valore = "+" & d_Valore
        End If
        'InvioPhase(' + $args[0].Groups[1].Value.TrimEnd() + ')'
    End Sub


    Private Sub Form_Activate(sender As Object, e As EventArgs) Handles MyBase.Activated
        Dim tmpDato As String
        'La procedura per il calcolo della fase e' la seguente:
        'Invio alla ACU del comando di step track.
        'Attesa del termine dello step track
        'richiedere l'attuale valore di fase del ricevitore
        'accertarsi che il ricevitore sia sulla frequenza, polarizzatore e LNB attualmente
        'impostati sul sistema
        'Nel caso di calcolo manuale:
        'Presentare tutti i dati e rimanere in attesa per spostare l'antenna della quantita' desiderata
        'inviare all'ACU il comando di invio continuo dei dati
        'attendere l'incremento del dato di fase con invio al ricevitore del nuovo valore
        'aggiornare i dati di errore AZ ed EL
        'attendere il comando di EXIT (esci senza memorizzare) o END (esci e memorizza i nuovi valori
        'sia sul ricevitore che nel database
        Try
        SocketManager.CloseRX(LanCtrl)
        b_int(ACU) = True
        PhasingDataFlow = 0
        SystemConfiguration = 0
        PhasingMode = 0
        PhaseValue = 0
        PolarizationAngle = 0
        ElevationGainSlope = 0
        AzimuthGainSlope = 0
        RfInputFrequency = 0
        Process = 0
        Valid = 0
        ElPhaseValue = 0
        AzPhaseValue = 0
        i_Phase = 3
        '   i_Inter = 1
        i_StatusOld = i_Status
        PTKold = PTK
        PTK = 0
        b_Vai = False
        Me.KeyPreview = True
        '    tmrPhase.Enabled = True
        Interroga = True
        '    If IsNumeric(s_POL) Then LblPol = Format$(CDbl(Mid$(s_POL, 1, 4)) / 100, "00.00")
        If IsNumeric(s_Az) Then
            TxtAz.Text = Format$(s_Az / 1000, "00.000")
            AzPro = CDbl(TxtAz.Text)
        End If
        If IsNumeric(s_El) Then
            TxtEl.Text = Format$(s_El / 1000, "0.000")
            ElPro = CDbl(TxtEl.Text)
        End If
        '    i_ric = 3                                       'abilito lo scambio con il ricevitore solo da qui
        '    LanReset1 LanCtrl
        '    Attesa(0.2)
        '    LanReset1 LanPhase
        '    Attesa(0.2)
        i_Cont = 0
        tmrPhasing.Interval = 300
        tmrPhasing.Enabled = True
        FrmMain.TmrPhase.Interval = 300
        g_NewFrequency = False
        '12/03/2019
        tmpDato = LeggiFileIni("Default", "IncEl")
        IncEl = CSng(Val(tmpDato))
        tmpDato = LeggiFileIni("Default", "IncEl1")
        IncEl1 = CSng(Val(tmpDato))
        TxtFreccia.Focus()
        Catch ex As Exception
            WriteError(0, "Form_Activate: " & ex.Message)
        End Try
    End Sub


    Private Sub FrmRicevitore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form_Initialize()
        ' Indicizzato con le costanti LanMon..LanCal (1..6, Predisposizioni.vb):
        ' dimensionato a 4 mancavano gli indici 5 (LanPhase) e 6 (LanCal),
        ' usati ad es. da PortaRx = LanPhase/LanCal, causando un
        ' IndexOutOfRangeException. In VB6 il control array tmrTimeoutMSG1
        ' ha infatti Index 1..6 (frmRicevitore.frm).
        ReDim tmrTimeoutMSG1(6)
        For k As Integer = 0 To 6
            tmrTimeoutMSG1(k) = New System.Windows.Forms.Timer(components) With {.Interval = 30000}
            AddHandler tmrTimeoutMSG1(k).Tick, AddressOf TmrTimeoutMSG1_Tick
        Next
        Dim rs_Ric As Object
        TxtPhase.Text = CStr(g_PhaseEl)
        TxtPhAz.Text = CStr(g_PhaseAz)
        CmbRevAz.Text = CStr(g_RevAz)
        tmpSQL = "SELECT * FROM [Fasi] where Ultima"
        rs_Ric = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
        If Not rs_Ric.BOF Then
            If Not IsDBNull(rs_Ric!OffsetAz) Then TxtOffsetAz.Text = CStr(rs_Ric!OffsetAz)
            If Not IsDBNull(rs_Ric!OffsetEl) Then TxtOffsetEl.Text = CStr(rs_Ric!OffsetEl)
        End If
        rs_Ric.Close
        SocketManager.CloseRX(LanCtrl)
        CmbInc.Text = 5
        CmbEl.Text = 0.01
        b_int(ACU) = True
        PhasingDataFlow = 0
        SystemConfiguration = 0
        PhasingMode = 0
        PhaseValue = 0
        PolarizationAngle = 0
        ElevationGainSlope = 0
        AzimuthGainSlope = 0
        RfInputFrequency = 0
        Process = 0
        Valid = 0
        ElPhaseValue = 0
        AzPhaseValue = 0
    End Sub



    ' Richiamata direttamente da CallbackRicezioneRX (frmRicezione.Socket.vb)
    ' ad ogni arrivo dati sul canale RX: porting del VB6 lblInputRx_Change,
    ' che scattava automaticamente quando il codice assegnava
    ' lblInputRx.Caption — il Label di .NET non ha un evento Change
    ' equivalente, va richiamata esplicitamente.
    Public Sub LblInputRx_Change()
        Dim lun As Integer, MessageType As System.Single
        Dim pos As Integer, rx_Buffer As String, ICDs As System.Single
        Dim i_Carrier As Integer, i_LoopFilter As Integer, i_Input As Integer, i_AGC As Integer
        Dim i_Video As Integer, i_AFC As Integer, i_IFBand As Integer, lunbuffer As Integer
        On Error Resume Next
        LblInputRx.Text = ""
        If InputBuffer3 = "" Then Exit Sub
        LanRx(PortaRx).CKS_Error = 0
        FrmMain.TmrPhase.Enabled = True
        With LanRx(PortaRx)
            rx_Buffer = StringToInt(Mid$(InputBuffer3, 1, 4))
            If rx_Buffer = rx_Header Then
                lunbuffer = Len(InputBuffer3)
                If lunbuffer < 5 Then GoTo esci
                lun = StringToInt(Mid$(InputBuffer3, 5, 4))           'prelevo lunghezza buffer per capire il tipo di buffer
                'Correzione apportata il 21/08/2016 perchè si è verificato che il buffer d'ingresso era eccessivo e non permetteva ulteriori
                'scambi con il ricevitore
                If lun = 0 Or lunbuffer > lun Then
                    InputBuffer3 = ""
                    Exit Sub
                End If
                'prelevo gli ultimi 4 byte per controllare se il messaggio e' completo
                rx_Buffer = StringToInt(Mid$(InputBuffer3, Len(InputBuffer3) - 3, 4))
                If rx_Buffer = rx_Postamble Then
                    .Buffer = ""
                    FrmRicezione.LblConfCNL3in.BackColor = PCS_NORMAL_COL
                Else
                    If Len(InputBuffer3) > lun Then
                        InputBuffer3 = ""
                        '                FrmRicevitore.tmrTimeoutMSG1(PortaRx).Enabled = False
                    End If
                    Exit Sub
                End If
            Else
                InputBuffer3 = ""
                Exit Sub
            End If
        End With
        ' Controllo se e' un messaggio di respinta buffer
        lun = StringToInt(Mid$(InputBuffer3, 5, 4))           'prelevo lunghezza buffer per capire il tipo di buffer
        If lun = 20 Then              'richiesta respinta
            MessageType = StringToInt(Mid$(InputBuffer3, 13, 4)) 'tipo errore
            If MessageType = -1 Then
                FrmMain.stpMessaggio.Text = "Unidentified message"
                '                    frmRicezione.sckRX(RX).Close
                FrmMain.TmrMsg.Enabled = True
                InputBuffer3 = ""
                Me.tmrTimeoutMSG1(PortaRx).Enabled = False
                Exit Sub
            ElseIf MessageType = -2 Then
                FrmMain.stpMessaggio.Text = "Connection rejection message"
                FrmMain.TmrMsg.Enabled = True
                '                   For i = 1 To 6
                '                   If PortaRx <> LanCtrl Then frmRicezione.sckRX(LanCtrl).Close
                SocketManager.CloseRX(PortaRx)
                '                    Attesa(0.2)
                LanReset1(PortaRx)
                '        Next i
                '        If PortaRx <> LanRst Then

                '             LanReset1(PortaRx)
                '             LanReset1 LanRst
                '             ResetDTR LanMon
                '            Attesa 0.5

                '        End If
                If PortaRx = LanPhase Then NoPhasing = False
                InputBuffer3 = ""
                '            g_NewFrequency = False
                GoTo esci
            End If
        End If

        Select Case PortaRx
            Case LanMon              'Monitor
                pos = 17
                ICDs = StringToInt(Mid$(InputBuffer3, 13, 4))
                '        If lun = 420 Then
                If ICDs = ComponentCode Then
                    'IF receiver table pag. 38 dell'STI 100013_CRT
                    PhasingDataFlow = StringToInt(Mid$(InputBuffer3, pos, 4))
                    TxtPhasingFlow.Text = CStr(PhasingDataFlow)
                    pos += (4 * 3)
                    SystemConfiguration = StringToInt(Mid$(InputBuffer3, pos, 4))
                    pos += 4
                    PhasingMode = StringToInt(Mid$(InputBuffer3, pos, 4))
                    If PhasingMode = 0 Then
                        TxtPhasingMode.Text = "Automatic"
                    Else
                        TxtPhasingMode.Text = "Manual"
                    End If
                    pos += 4
                    PhaseValue = StringToInt(Mid$(InputBuffer3, pos, 4))
                    pos += 4
                    MessageType = StringToInt(Mid$(InputBuffer3, pos, 4))
                    If MessageType = 0 Then
                        LblRevAz.Text = "N"
                    Else
                        LblRevAz.Text = "Y"
                    End If
                    '            g_RevAz = LblRevAz.Text
                    LblRevAz.Text = LblRevAz.Text
                    pos += 4
                    PolarizationAngle = FloatToDecimal(Mid$(InputBuffer3, pos, 4))
                    TxtPolarizationAngle.Text = Format$(PolarizationAngle, "0.00")
                    pos += 4
                    ElevationGainSlope = FloatToDecimal(Mid$(InputBuffer3, pos, 4))
                    TxtElGain.Text = CStr(ElevationGainSlope)
                    pos += 4
                    AzimuthGainSlope = FloatToDecimal(Mid$(InputBuffer3, pos, 4))
                    TxtAzGain.Text = CStr(AzimuthGainSlope)
                    pos += 4
                    RfInputFrequency = StringToInt(Mid$(InputBuffer3, pos, 4))
                    TxtInpFrequency.Text = Format$(RfInputFrequency, "#0000.000")
                    '            g_Frequenza = Format$(TxtInpFrequency / 1000, "#00.00#")
                    pos += (4 * 50)
                    Process = StringToInt(Mid$(InputBuffer3, pos, 4))
                    If Process = 0 Then
                        TxtProcess.Text = "Phasing"
                    ElseIf Process = 1 Then
                        TxtProcess.Text = "Calibration"
                    Else
                        TxtProcess.Text = "ConfiguratiON"
                    End If
                    pos += 4
                    Valid = StringToInt(Mid$(InputBuffer3, pos, 4))
                    If Valid = 0 Then
                        TxtValid.Text = "Valid"
                    Else
                        TxtValid.Text = "Not available"
                    End If
                    pos = 17 + 4 * 64
                    ElPhaseValue = StringToInt(Mid$(InputBuffer3, pos, 4))
                    TxtPhaseEl.Text = CStr(ElPhaseValue)
                    '            g_PhaseEl = ElPhaseValue
                    ' frmRicezione.TxtPhaseEl.Text = ElPhaseValue
                    pos = 17 + 4 * 69
                    '            AzPhaseValue = StringToInt(Mid$(InputBuffer3, pos, 4))
                    '            TxtPhaseAz = AzPhaseValue
                    '            g_PhaseAz = AzPhaseValue
                    '            frmRicezione.TxtPhaseaz.Text = azPhaseValue
                    '            frmRicezione.sckRX(PortaRx).Close               'Chiudo la porta
                ElseIf ICDs = ComponentCodeIF1 Then 'lun = 224 Then
                    'IF receiver table pag. 38 dell'STI 100013_CRT
                    'Dim f_InFreq As System.Single, i_InputPort As Integer, f_Acquisition As System.Single, i_OpMode As Integer
                    'Dim i_Carrier As Integer, i_LoopFilter As Integer, i_Input As Integer, i_AGC As Integer
                    'Dim i_Video As Integer, i_AFC As Integer, i_IFBand As Integer
                    pos = 17 + 4
                    f_InFreq = FloatToDecimal(Mid$(InputBuffer3, pos, 4))
                    TxtFreq.Text = CStr(f_InFreq / 1000)
                    pos += 4
                    i_InputPort = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_InputPort
                        Case 0
                            CmbSelection.Text = "Nominal"
                        Case 1
                            CmbSelection.Text = "Alternate"
                    End Select
                    pos += 4
                    i_OpMode = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_OpMode
                        Case 0
                            CmbOperMode.Text = "Automatic"
                        Case 1
                            CmbOperMode.Text = "Manual"
                    End Select
                    pos += 4
                    i_Carrier = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_Carrier
                        Case 0
                            CmbCarrier.Text = "Normal"
                        Case 1
                            CmbCarrier.Text = "High"
                    End Select
                    pos += 4
                    i_LoopFilter = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_LoopFilter
                        Case 0
                            CmbLoop.Text = CStr(100)
                        Case 1
                            CmbLoop.Text = CStr(300)
                        Case 2
                            CmbLoop.Text = CStr(1000)
                        Case 3
                            CmbLoop.Text = CStr(3000)
                        Case 4
                            CmbLoop.Text = CStr(30)
                        Case 5
                            CmbLoop.Text = CStr(10)
                    End Select
                    pos += 4
                    i_AGC = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_AGC
                        Case 0
                            CmbAGC.Text = CStr(1)
                        Case 1
                            CmbAGC.Text = CStr(10)
                        Case 2
                            CmbAGC.Text = CStr(100)
                        Case 3
                            CmbAGC.Text = CStr(1000)
                        Case 4
                            CmbAGC.Text = CStr(0.1)
                    End Select
                    pos += 4 * 5
                    f_Acquisition = FloatToDecimal(Mid$(InputBuffer3, pos, 4))
                    TxtRange.Text = CStr(f_Acquisition)
                    pos += 4
                    i_Video = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_Video
                        Case 0
                            CmbVideo.Text = "PM"
                        Case 1
                            CmbVideo.Text = "FM"
                        Case 2
                            CmbVideo.Text = "Spectrum"
                        Case 3
                            CmbVideo.Text = "By-pass"
                        Case 4
                            CmbVideo.Text = "AM Ncohe"
                        Case 5
                            CmbVideo.Text = "AM Cohe"
                    End Select
                    pos += 4
                    i_Input = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_Input
                        Case 0
                            CmbInputPort.Text = "IF Port 1"
                        Case 3
                            CmbInputPort.Text = "IF Port 2"
                        Case 4
                            CmbInputPort.Text = "IF Port 3"
                        Case 5
                            CmbInputPort.Text = "Auxiliary 1"
                        Case 6
                            CmbInputPort.Text = "Auxiliary 2"
                    End Select
                    pos += 4
                    i_AFC = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_AFC
                        Case 0
                            CmbAFC.Text = "Off"
                        Case 1
                            CmbAFC.Text = CStr(10)
                        Case 2
                            CmbAFC.Text = CStr(100)
                        Case 3
                            CmbAFC.Text = CStr(1000)
                        Case 4
                            CmbAFC.Text = CStr(1)
                    End Select
                    pos += 4 * 5
                    i_IFBand = StringToInt(Mid$(InputBuffer3, pos, 4))
                    Select Case i_IFBand
                        Case 0
                            CmbBandWidth.Text = CStr(10000)
                        Case 1
                            CmbBandWidth.Text = CStr(1000)
                        Case 2
                            CmbBandWidth.Text = CStr(100)
                        Case 3
                            CmbBandWidth.Text = CStr(5000)
                        Case 4
                            CmbBandWidth.Text = CStr(3000)
                        Case 5
                            CmbBandWidth.Text = CStr(1500)
                        Case 6
                            CmbBandWidth.Text = CStr(500)
                        Case 7
                            CmbBandWidth.Text = CStr(300)
                        Case 8
                            CmbBandWidth.Text = CStr(150)
                        Case 9
                            CmbBandWidth.Text = CStr(50)
                        Case 10
                            CmbBandWidth.Text = CStr(30)
                        Case 11
                            CmbBandWidth.Text = CStr(15)
                        Case 12
                            CmbBandWidth.Text = CStr(10)
                        Case 13
                            CmbBandWidth.Text = CStr(5)
                        Case 14
                            CmbBandWidth.Text = CStr(13000)
                        Case 15
                            CmbBandWidth.Text = "None"
                    End Select
                    Interroga = False

                End If
            Case LanCtrl               'Control
                lun = StringToInt(Mid$(InputBuffer3, 5, 4))           'prelevo lunghezza buffer per capire il tipo di buffer
                If lun = 20 Then              'richiesta respinta o comando OK
                    MessageType = StringToInt(Mid$(InputBuffer3, 13, 4)) 'tipo errore
                    If MessageType <> 0 Then
                        FrmMain.stpMessaggio.Text = "Reject command"
                        'frmRicezione.sckRX(RX).Close
                        FrmMain.TmrMsg.Enabled = True
                    End If
                End If
                If Not Me.tmrPhasing.Enabled Then FrmRicezione.SckRX_Close(LanCtrl)
                GoTo esci
            Case LanRst               'Restart
                lun = StringToInt(Mid$(InputBuffer3, 5, 4))           'prelevo lunghezza buffer per capire il tipo di buffer
                If lun = 20 Then              'richiesta respinta o comando OK
                    MessageType = StringToInt(Mid$(InputBuffer3, 13, 4)) 'tipo errore
                    If MessageType <> 0 Then
                        FrmMain.stpMessaggio.Text = "Reject command"
                        'frmRicezione.sckRX(RX).Close
                        FrmMain.TmrMsg.Enabled = True
                    End If
                End If
                GoTo esci
            Case LanLog               'Logging Data
            Case LanPhase               'Beacon
                MessageType = StringToInt(Mid$(InputBuffer3, 13, 4))  'tipo di messaggio - Attendo phasing
                If MessageType <> 0 Then GoTo esci               'non e' phasing
                If lun = 24 Then              'richiesta respinta
                    MessageType = StringToInt(Mid$(InputBuffer3, 17, 4)) 'tipo errore
                    If MessageType = 1 Then
                        FrmMain.stpMessaggio.Text = "DTR invalid syntax"
                        FrmMain.TmrMsg.Enabled = True
                    ElseIf MessageType = 2 Then
                        FrmMain.stpMessaggio.Text = "DTR Calibration in progress"
                        FrmMain.TmrMsg.Enabled = True
                    End If
                    GoTo esci
                    '            ElseIf lun = 64 And RequestDTR = 1 Then          'messaggio di fase
                ElseIf lun = 64 Then          'messaggio di fase
                    MessageType = StringToInt(Mid$(InputBuffer3, 25, 4))   'controllo se i dati sono validi
                    If MessageType = 1 Then GoTo esci
                    MessageType = StringToInt(Mid$(InputBuffer3, 29, 4))   'prelevo solo i due byte lsb per il canale A
                    If MessageType = 0 Then
                        LblLock.BackColor = vbRed
                        LblLock.Text = "UNLOCK"
                    Else
                        LblLock.BackColor = vbGreen
                        LblLock.Text = "LOCK"
                    End If
                    LockRic = LblLock.Text
                    '                If LockRic = "LOCK" Then
                    '                    frmRicezione.LblLock.BackColor = vbGreen
                    '                    s_StatusPTIC(3) = s_StatusPTIC(3) Or &H2
                    '                Else
                    '                    frmRicezione.LblLock.BackColor = vbRed
                    '                    s_StatusPTIC(3) = s_StatusPTIC(3) And &HD
                    '                End If
                    FrmRicezione.LblLock.Text = LockRic
                    MessageType = FloatToDecimal(Mid$(InputBuffer3, 33, 4))   'Valore IFR in dBm
                    TxtLev.Text = Format$(MessageType, "0.00")
                    s_Lev1 = Format$(MessageType, "0.00")
                    '21/01/2019
                    TxtLev.BackColor = PCS_NORMAL_COL
                    ' frmRicezione.TxtLev = s_Lev1
                    If IsNumeric(s_Lev1) Then
                        If s_Lev1 - ScalaMin > FrmRicezione.BarBeacon.Maximum Then
                            FrmRicezione.BarBeacon.Value = FrmRicezione.BarBeacon.Maximum
                        ElseIf s_Lev1 < ScalaMin Then
                            FrmRicezione.BarBeacon.Value = 0
                        Else
                            FrmRicezione.BarBeacon.Value = Math.Abs(CDbl(s_Lev1) - ScalaMin)
                        End If
                    End If
                    MessageType = FloatToDecimal(Mid$(InputBuffer3, 37, 4))   'ElevatiON
                    LblErrEl.Text = Format$(MessageType, "0.00")
                    s_LevEl1 = LblErrEl.Text      'errore di elevazione
                    MessageType = FloatToDecimal(Mid$(InputBuffer3, 41, 4))   'Azimuth error
                    LblErrAz.Text = Format$(MessageType, "0.00")
                    s_LevAz1 = LblErrAz.Text        'errore di azimuth
                    MessageType = FloatToDecimal(Mid$(InputBuffer3, 45, 4))   'Polarization angle
                    TxtPolarizationAngle.Text = Format$(MessageType, "0.00")
                    '               frmRicezione.tmrTimeoutMSG(PortaRx) = False

                    '               TxtPolarizationAngle = MessageType
                    '                frmRicezione.AggiornaDati
                    NoPhasing = True
                    '                frmMain.tmrPhase.interval = i_InterRic / 2 * 1000
                    Me.tmrTimeoutMSG1(PortaRx).Interval = i_InterRic / 2 * 1000 + 100
                    Me.tmrTimeoutMSG1(PortaRx).Enabled = False
                    Me.tmrTimeoutMSG1(PortaRx).Enabled = True
                End If
            Case LanCal               'Calibration
                Select Case RequestDTR
                    Case 2                  'richiesta calibrazione
                        MessageType = StringToInt(Mid$(InputBuffer3, 13, 4))  'tipo di messaggio - Attendo phasing
                        If MessageType <> 1 Then GoTo esci               'non e' phasing
                        If lun = 24 Then              'richiesta respinta
                            MessageType = StringToInt(Mid$(InputBuffer3, 17, 4)) 'tipo errore
                            If MessageType = 1 Then
                                FrmMain.stpMessaggio.Text = "DTR invalid syntax"
                                FrmMain.TmrMsg.Enabled = True
                            ElseIf MessageType = 2 Then
                                FrmMain.stpMessaggio.Text = "DTR Premature end"
                                FrmMain.TmrMsg.Enabled = True
                            ElseIf MessageType = 3 Then
                                FrmMain.stpMessaggio.Text = "DTR invalid sequence/calib. aborted"
                                FrmMain.TmrMsg.Enabled = True
                            ElseIf MessageType = 4 Then
                                FrmMain.stpMessaggio.Text = "DTR IFR unlocked"
                                FrmMain.TmrMsg.Enabled = True
                            ElseIf MessageType = 5 Then
                                FrmMain.stpMessaggio.Text = "DTR DCU output signal invalid"
                                FrmMain.TmrMsg.Enabled = True
                            End If
                            GoTo esci
                        ElseIf lun = 64 Then          'messaggio di fase

                        End If
                    Case 3                  'richiesta tabella calibrazione
                        If lun = 20 Then              'richiesta respinta
                            MessageType = StringToInt(Mid$(InputBuffer3, 13, 4)) 'tipo errore
                            If MessageType = 1 Then
                                FrmMain.stpMessaggio.Text = "DTR invalid syntax"
                                FrmMain.TmrMsg.Enabled = True
                            End If
                            GoTo esci
                        Else
                        End If
                    Case 4                  'richiesta tabella estesa calibrazione
                        If lun = 20 Then              'richiesta respinta
                            MessageType = StringToInt(Mid$(InputBuffer3, 13, 4)) 'tipo errore
                            If MessageType = 1 Then
                                FrmMain.stpMessaggio.Text = "DTR invalid syntax"
                                FrmMain.TmrMsg.Enabled = True
                            End If
                            GoTo esci
                        Else
                        End If
                End Select

        End Select
esci:
        InputBuffer3 = ""
        Me.tmrTimeoutMSG1(PortaRx).Enabled = False
    End Sub
    Private Sub Form_Initialize()
        Me.KeyPreview = True
        i_Auto = 0  'contatore di passi per auto calibrazione
        i_AutoAugmented = 0
    End Sub

    Private Sub MnuPhasing_Click(Index As Integer)
        If s_ACULR = "L" Then
            LblWarning.Text = "ACU in Local"
            LblWarning.Visible = True
            tmrAlarm.Enabled = True
            Exit Sub
        End If
        If i_Alarm Then
            LblWarning.Text = "Command Inhibit"
            LblWarning.Visible = True
            tmrAlarm.Enabled = True
            Exit Sub
        End If
        i_Phase = Index
        Select Case Index
            Case 0
                '           Procedura Augmented
                b_ClosePreset = True
                ' Unload(FrmPreset) ' VB6
                Me.frmManual.Visible = True
                b_Ricezione(ACU) = False
                b_int(ACU) = True
                i_Cont = 2
                Attesa(0.2)
                Port = Canali(ACU).Port_IN
                Port_Control(ACU).Rec_State = PCR_WAIT
                InvioACU(Chr(STX) & "08 S79" & Chr(ETX))
                PTK = &H0
                PTKold = PTK
                i_StatusOld = i_Status
                PTK = 10
                i_Status = OP_PROGRAMTRACK
                i_ProgTrack = 1
                FrmRicezione.LblCommand.Text = "Program Track"
                '            frmRicezione.tmrTimeoutMSG(ACU).interval = 30000
                If IsNumeric(TxtPhaseEl.Text) Then PhaseOldEl = CInt(TxtPhaseEl.Text)            'memorizzo la phase iniziale
                If IsNumeric(TxtPhaseAz.Text) Then PhaseOldAz = CInt(TxtPhaseAz.Text)          'memorizzo la phase iniziale
                LblTesto.BackColor = vbYellow
                LblTesto.Text = "Program Track"
                i_AutoAugmented = 1
                i_Cont = 0
                '           AzOffset = 0
                '           ElOffset = 0
                FrmRicezione.LblAzError.Text = AzOffset
                FrmRicezione.LblElError.Text = ElOffset
            Case 1
                '           Procedura automatica
                If LblLock.Text = "UNLOCK" Then
                    Response = MessageBox.Show("RX UNLOCK", "Warning", MessageBoxButtons.OK)
                    Exit Sub
                End If
                b_ClosePreset = True
                ' Unload FrmPreset ' VB6
                Me.frmManual.Visible = True
                b_Ricezione(ACU) = False
                b_int(ACU) = True
                i_Cont = 2
                Port = Canali(ACU).Port_IN
                Port_Control(ACU).Rec_State = PCR_WAIT
                Attesa(0.2)
                InvioACU(Chr(STX) & "08 S79" & Chr(ETX))
                Me.frmManual.Visible = True
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                i_Cont = 2
                Port = Canali(ACU).Port_IN
                Port_Control(ACU).Rec_State = PCR_WAIT
                Attesa(0.2)
                SEND_ACU = CheckSum("09 P ")     'Metto l'antenna in preset
                InvioACU(SEND_ACU)
                '            frmRicezione.tmrTimeoutMSG(ACU).interval = 30000
                If IsNumeric(TxtPhaseEl.Text) Then PhaseOldEl = CInt(TxtPhaseEl.Text)            'memorizzo la phase iniziale
                If IsNumeric(TxtPhaseAz.Text) Then PhaseOldAz = CInt(TxtPhaseAz.Text)         'memorizzo la phase iniziale
                InvioACU(Chr(STX) & "09 ST2C" & Chr(ETX))      'Step Track
                '                frmRicezione.CmdStepTrack_Click
                d_Attesa = DateAdd("s", 2, Now)
                PTK = &H0
                '            d_DataInizioProg = CDate("31/12/3000 00.00.00")
                d_DataInizioProg = ""
                FrmMain.stpProgram.Text = d_DataInizioProg
                LblTesto.BackColor = vbYellow
                LblTesto.Text = "Step Track"
                i_Auto = 1
                FrmRicezione.LblCommand.Text = "Step Track"
            Case 2
                'frmRicezione.tmrTimeoutMSG(ACU).interval = 30000
                If IsNumeric(TxtPhaseEl.Text) Then PhaseOldEl = CInt(TxtPhaseEl.Text)            'memorizzo la phase iniziale
                If IsNumeric(TxtPhaseAz.Text) Then PhaseOldAz = CInt(TxtPhaseAz.Text)           'memorizzo la phase iniziale
                i_PhaseFrm = 1              'richiama la phase dal quadro operativo
                Me.frmManual.Visible = True
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                Port = Canali(ACU).Port_IN
                Port_Control(ACU).Rec_State = PCR_WAIT
                i_Cont = 2
                Attesa(0.2)
                SEND_ACU = CheckSum("09 P ")
                InvioACU(SEND_ACU)
                CmdMeno.Enabled = True
                CmdPiu.Enabled = True
                If Not IsNumeric(TxtPhase.Text) Then TxtPhase.Text = TxtPhaseEl.Text
        End Select
        TxtFreccia.Focus()

    End Sub
    Private Sub MnuRicevitore_Click(Index As Integer)
        Select Case Index
            Case 0
                FineFase()
                FrmRicezione.SckRX_Close(LanCtrl)
                NoPhasing = False
                tmrPhasing.Enabled = False
                '            frmRicezione.sckRX(LanPhase).Close
                i_ric = 0
                '            frmRicezione.sckRX_Close 1
                '           Attesa 0.5
                '            frmRicezione.sckRX_Close 2
                Me.frmManual.Visible = False
                '           CmdEnd_Click
                '            i_Status = i_Statusold
                '           Select Case i_Status
                '               Case OP_STANDBY
                '                   frmRicezione.CmdSTBY_Click
                '               Case OP_STEPTRACK
                '                   frmRicezione.CmdStepTrack_Click
                '               Case OP_PROGRAMTRACK
                '                   frmRicezione.LblCommand.Text = "Program Track"
                '               Case OP_AUTOTRACK
                '                   frmRicezione.CmdAutoTrack_Click
                '               Case OP_AUGMENTED
                '                   frmRicezione.LblCommand.Text = "Augmented"
                '           End Select
                '            PTK = PTKold
                '            Port = Canali(ACU).Port_IN
                '            frmRicezione.tmrTimeoutMSG(ACU).interval = Port_control(acu).Time_out * 1000
                FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
                i_Inter = CSng(Val(LeggiFileIni("Canale1", "Inter")))
                i_Cont = 1
                FrmMain.TmrPhase.Interval = If(i_InterRic / 2 * 1000 > 0, CInt(i_InterRic / 2 * 1000), 30000)
                '            ' frmRicezione.TxtPhase.Text = CStr(g_PhaseEl)
                '            ' frmRicezione.TxtPhAz.Text = CStr(g_PhaseAz)
                '            ' frmRicezione.TxtRevAz = g_RevAz
                Me.Close()
        End Select

    End Sub

    Private Sub MnuExit_Click(sender As Object, e As EventArgs) Handles MnuExit.Click
        MnuRicevitore_Click(0)
    End Sub

    Private Sub MnuAugmented_Click(sender As Object, e As EventArgs) Handles MnuAugmented.Click
        MnuPhasing_Click(0)
    End Sub

    Private Sub MnuAutomatic_Click(sender As Object, e As EventArgs) Handles MnuAutomatic.Click
        MnuPhasing_Click(1)
    End Sub

    Private Sub MnuManual_Click(sender As Object, e As EventArgs) Handles MnuManual.Click
        MnuPhasing_Click(2)
    End Sub

    Public Sub TmrPhasing_Timer(sender As Object, e As EventArgs) Handles tmrPhasing.Tick
        '    Phasing
        If b_Vai Then InviaCoord()
        If i_Auto <> 0 Then AutoPhase()
        If i_AutoAugmented <> 0 Then AutoAugmented()
        AggiornaDatiRX()
        '    tmrPhasing.Enabled = True
    End Sub

    Private Function AggiornaDatiRX()
        If IsNumeric(s_Az) Then TxtAz.Text = Format$(CDbl(Mid$(s_Az, 1, 6)) / 1000, "000.000")
        If IsNumeric(s_El) Then TxtEl.Text = Format$(CDbl(Mid$(s_El, 1, 5)) / 1000, "00.000")
        TxtAzErr.Text = s_LevAz
        TxtElErr.Text = s_LevEl
        TxtFrequency.Text = Format$(f_FrqBeacon * 1000, "#0000.000")
        TxtLna.Text = FrmRicezione.LblLNA.Text                                               '15/12/2022 Momentaneamente tolta
        TxtPol1.Text = FrmRicezione.LblPol1.Text
        TxtPol2.Text = FrmRicezione.TxtPolErr.Text
        TxtPol.Text = FrmRicezione.LblPol.Text
        LblPol.Text = FrmRicezione.TxtPol.Text
        If IsNumeric(s_Lev) Then LblBeacon.Text = Format$(s_Lev, "##0.0#")
        '    LblRevAz.Text = g_RevAz
        LblSatellite.Text = s_Satellite
        '    TxtFreccia.Focus()
        AggiornaDatiRX = True
    End Function



    Private Sub TxtAzGain1_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtAzGain1.KeyUp
        If e.KeyCode = CR Then
            If IsNumeric(TxtAzGain1.Text) Then
                If CDbl(TxtAzGain1.Text) > -250 And CDbl(TxtAzGain1.Text) < 250 Then
                    InvioPhase(CDbl(TxtAzGain1.Text), 1, 9, ComponentCode)
                    'Attesa 0.5
                    Interroga = True
                End If
            End If
            TxtFreccia.Focus()
        End If
    End Sub

    Private Sub TxtElGain1_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtElGain1.KeyUp
        If e.KeyCode = CR Then
            If IsNumeric(TxtElGain1.Text) Then
                If CDbl(TxtElGain1.Text) > -250 And CDbl(TxtElGain1.Text) < 250 Then
                    InvioPhase(CDbl(TxtElGain1.Text), 1, 8, ComponentCode)
                    'Attesa 0.5
                    Interroga = True
                End If
            End If
            TxtFreccia.Focus()
        End If

    End Sub

    Private Sub TxtFreq_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtFreq.KeyUp
        If e.KeyCode = CR Then
            d_Valore = CStr(CDbl(TxtFreq.Text) * 1000)
            InvioPhase(d_Valore, 1, 1, ComponentCodeIF1)
            'Attesa 0.5
            Interroga = True
            TxtFreccia.Focus()
        End If
    End Sub


    Private Sub TxtOffsetAz_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtOffsetAz.KeyUp
        Dim valoffset As String
        If e.KeyCode = CR Then
            If IsNumeric(TxtOffsetAz.Text) Then
                If CDbl(TxtOffsetAz.Text) >= 0 And CDbl(TxtOffsetAz.Text) <= 99 Then
                    valoffset = CheckSum("14MOFA +" & Format$(Math.Abs(CDbl(TxtOffsetAz.Text)), "00"))
                ElseIf CDbl(TxtOffsetAz.Text) >= -99 And CDbl(TxtOffsetAz.Text) < 0 Then
                    valoffset = CheckSum("14MOFA -" & Format$(Math.Abs(CDbl(TxtOffsetAz.Text)), "00"))
                Else
                    TxtOffsetAz.Text = ""
                    Exit Sub
                End If
                i_Cont = 2
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                Port = Canali(ACU).Port_IN
                Port_Control(ACU).Rec_State = PCR_WAIT
                Attesa(0.2)
                InvioACU(valoffset)
            End If
        End If
    End Sub
    Private Sub TxtOffsetEL_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtOffsetEl.KeyUp
        Dim valoffset As String
        If e.KeyCode = CR Then
            If IsNumeric(TxtOffsetEl.Text) Then
                If CDbl(TxtOffsetEl.Text) >= 0 And CDbl(TxtOffsetEl.Text) <= 99 Then
                    valoffset = CheckSum("14MOFE +" & Format$(Math.Abs(CDbl(TxtOffsetEl.Text)), "00"))
                ElseIf CDbl(TxtOffsetEl.Text) >= -99 And CDbl(TxtOffsetEl.Text) < 0 Then
                    valoffset = CheckSum("14MOFE -" & Format$(Math.Abs(CDbl(TxtOffsetEl.Text)), "00"))
                Else
                    TxtOffsetEl.Text = ""
                    Exit Sub
                End If
                i_Cont = 2
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                Port = Canali(ACU).Port_IN
                Port_Control(ACU).Rec_State = PCR_WAIT
                Attesa(0.2)
                InvioACU(valoffset)
            End If
        End If
    End Sub

    Private Sub TxtPhase_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtPhase.KeyUp
        If e.KeyCode = CR Then
            d_Valore = Format$(CDbl(TxtPhase.Text), "##0")
            '        tmrPhasing.Enabled = False
            InvioPhase(d_Valore, 0, 5, ComponentCode)
            '        tmrPhase.Enabled = True
            TxtFreccia.Focus()
        End If
    End Sub
    Private Sub TmrTimeoutMSG1_Tick(sender As Object, e As EventArgs)
        Dim tmr As Timer = TryCast(sender, Timer)
        If tmr Is Nothing OrElse tmrTimeoutMSG1 Is Nothing Then Exit Sub
        For i As Integer = 0 To tmrTimeoutMSG1.Length - 1
            If tmrTimeoutMSG1(i) Is tmr Then
                TmrTimeoutMSG1_Timer(i)
                Exit Sub
            End If
        Next
    End Sub

    Public Sub TmrTimeoutMSG1_Timer(Index As Integer)
        Dim s_Device As String = ""
        On Error Resume Next
        If Index = LanPhase Then
            NoPhasing = False
            '        frmMain.tmrPhase.interval = 300
        End If
        Err.Clear()
        '    g_NewFrequency = False
        Port = Canali(Index).Port_IN
        LanRx(Index).Rec_State = PCR_WAIT
        'chiudo e apro la porta
        LanReset1(Index)
        ' aggiorno lo stato della porta
        LanRx(Index).Port_State = PCS_STOP
        ' aggiorno il colore di visualizzazione
        ' pulisco il buffer di visualizzazione
        '     MilSecR = 0
        tmrTimeoutMSG1(Index).Enabled = False
        If FrmRicezione.LblConfCNL3in.BackColor <> PCS_STOP_COL Then
            Select Case Index
                Case 1
                    s_Device = " RX Mon "
                Case 2
                    s_Device = " RX CTRL "
                Case 5
                    s_Device = " Phasing "
                Case 6
                    s_Device = " Calibration "
            End Select
        End If
        If LanRx(Index).CKS_Error > 3 Then
            If FrmRicezione.LblConfCNL3in.BackColor <> PCS_STOP_COL Then
                FrmRicezione.LblConfCNL3in.BackColor = PCS_STOP_COL
                '              frmRicezione.LblConfCNL3in.ForeColor = vbBlack
                '                    s_Device = " Phasing "
            End If
            LanRx(Index).CKS_Error = 0
            LockRic = ""
            '          frmRicezione.BarBeacon.Value = 0                                 '09/07/2018
        Else
            LanRx(Index).CKS_Error = LanRx(Index).CKS_Error + 1
            s_Device = ""
        End If
        If s_Device <> "" Then
            FrmMain.stpMessaggio.Text = "Time out " & s_Device
            FrmMain.TmrMsg.Enabled = True
            ScriviAlarm("Time out " & s_Device, 1)
            ' frmRicezione.Datlog.Me.Refresh()
            ' frmRicezione.tdbLog.ReBind
            ' frmRicezione.tdbLog.Me.Refresh()
        End If
        FrmMain.TmrPhase.Enabled = True
    End Sub
    Private Sub AutoPhase()
        Dim s_program As String, iix As Integer, i As Integer
        Port = Canali(ACU).Port_IN
        Try
        Select Case i_Auto
            Case 1
                If i_StepTrack = 12 Then        'Controlla che il valore di fine Step Track sia = 12
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    Port = Canali(ACU).Port_IN
                    Port_Control(ACU).Rec_State = PCR_WAIT
                    MaxAz = CDbl(TxtAz.Text)
                    MaxEl = CDbl(TxtEl.Text)
                    If CSng(LblErrAz.Text) > 0.5 Or CSng(LblErrEl.Text) > 0.5 Then
                        SEND_ACU = CheckSum("09 P ")
                        InvioACU(SEND_ACU)
                        Attesa(0.2)
                        InvioACU(Chr(STX) & "09 ST2C" & Chr(ETX))      'Step Track
                        '                frmRicezione.CmdStepTrack_Click
                        d_Attesa = DateAdd("s", 15, Now)
                    Else
                        '               s_program = " P " & Format$(MaxAz, "000.000") & " " & _
                        '               Format$(CDbl(TxtEl.Text) + CDbl(0.05), "00.000") + " "
                        Attesa(5)
                        '               SEND_acu = CheckSum("09 P ")
                        '                InvioACU(SEND_ACU)
                        '                Attesa 1
                        s_program = "EP " & Format$(CDbl(TxtEl.Text) + CDbl(IncEl), "00.000") + " "
                        i = Len(s_program) + 6
                        SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                        b_int(ACU) = True
                        b_Ricezione(ACU) = False
                        Port = Canali(ACU).Port_IN
                        Port_Control(ACU).Rec_State = PCR_WAIT
                        i_Cont = 3
                        InvioACU(SEND_ACU)
                        '                Attesa 2
                        ElPro = CDbl(TxtEl.Text)
                        FrmRicezione.LblCommand.Text = "Command Position"
                        LblTesto.Text = "Elevation Preset"
                        i_Status = OP_COMMANDPOSITION
                        d_Attesa = DateAdd("s", 15, Now)
                        '            CmdPiu_Click
                        '            LblTesto.Text = "Phase +"
                        i_Auto = 2
                        '20/01/2025
                        FrmRicezione.TmrSpiralAcquire.Enabled = False
                        i_StepTrack = 0
                        FrmRicezione.LblStepTrack.Text = ""
                        '18/02/2021
                        i_Cont = 1
                    End If
                Else
                    If Now > d_Attesa Then
                        '                If frmRicezione.TxtMode <> "Step Track" Then
                        '                    Response = MessageBox.Show("ACU in " & frmRicezione.TxtMode, vbOKOnly)
                        '                    CmdAnnulla_Click
                        '                    Exit Sub
                        '                End If
                        d_Attesa = DateAdd("s", 1, Now)
                    End If
                End If
            Case 2
                If Now > d_Attesa Then
                    If Math.Abs(CDbl(TxtEl.Text) - ElPro) < IncEl / 2 Then
                        CmdAnnulla_Click(Nothing, EventArgs.Empty)
                        Exit Sub
                    End If
                    '                b_Ricezione(acu) = False
                    '                InvioACU(Chr(STX) & "08 S79" & Chr(ETX)         'metto l'ACU in Stand-by
                    '                Attesa(0.2)
                    If IsNumeric(LblErrEl.Text) Then ErrorElOld = CSng(LblErrEl.Text)
                    If IsNumeric(TxtPhaseEl.Text) Then PhaseNew = CInt(TxtPhaseEl.Text)
                    CmdPiu_Click(Nothing, EventArgs.Empty)
                    LblTesto.Text = "Phase +"
                    i_Auto = 3
                    '               Port_control(acu).Rec_State = PCR_STANDBY
                    d_Attesa = DateAdd("s", 0.6, Now)
                End If
            Case 3
                If Now > d_Attesa Then
                    If Math.Abs(ErrorElOld - CSng(LblErrEl.Text)) > 0.08 Then
                        If CSng(LblErrEl.Text) > ErrorElOld Then
                            ErrorElOld = CSng(LblErrEl.Text)
                            CmdPiu_Click(Nothing, EventArgs.Empty)
                            LblTesto.Text = "Phase ++"
                            '                     d_Attesa = DateAdd("s", 0.5, Now)
                        ElseIf CSng(LblErrEl.Text) < ErrorElOld Then
                            ErrorElOld = CSng(LblErrEl.Text)
                            CmdMeno_Click(Nothing, EventArgs.Empty)
                            LblTesto.Text = "Phase -"
                            i_Auto = 4
                            d_Attesa = DateAdd("s", 0.6, Now)
                        End If
                    Else
                        CmdPiu_Click(Nothing, EventArgs.Empty)
                        LblTesto.Text = "Phase +"
                    End If
                    d_Attesa = DateAdd("s", 0.6, Now)
                End If
                i_cont1 = 0
            Case 4
                If Now > d_Attesa Then
                    If Math.Abs(ErrorElOld - CSng(LblErrEl.Text)) > 0.08 Then
                        d_Attesa = DateAdd("s", 0.6, Now)
                        If CSng(LblErrEl.Text) > ErrorElOld Then
                            ErrorElOld = CSng(LblErrEl.Text)
                            CmdMeno_Click(Nothing, EventArgs.Empty)
                            LblTesto.Text = "Phase -"
                            d_Attesa = DateAdd("s", 0.6, Now)
                            i_cont1 = 0
                        Else
                            ErrorElOld = CSng(LblErrEl.Text)
                            iix = 1
                            For iix = 1 To i_cont1
                                CmdPiu_Click(Nothing, EventArgs.Empty)
                                Attesa(0.2)
                            Next
                            b_Ricezione(ACU) = False
                            b_int(ACU) = True
                            s_program = " P " & Format$(CDbl(TxtAz.Text) + CDbl(IncEl), "000.000") & " " &
                        Format$(MaxEl, "00.000") + " "
                            i = Len(s_program) + 6
                            FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
                            SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                            i_Cont = 3
                            '                  Attesa 0.15
                            InvioACU(SEND_ACU)
                            '                   LblTesto.Text = "Azimuth Preset"
                            i_Cont = 1
                            d_Attesa = DateAdd("s", 15, Now)
                            i_Auto = 5
                        End If
                    Else
                        i_cont1 += 1
                        CmdMeno_Click(Nothing, EventArgs.Empty)
                        LblTesto.Text = "Phase -"
                    End If
                End If
            Case 5
                '         If Now > d_Attesa Then
                '            If LblErrAz.Text < 0 Then
                '                If CmbRevAz.Text = "N" Then
                '                    CmbRevAz.Text = "Y"
                '                    d_Valore = 1
                '                    If g_Polar <> "RHCP" Then
                '                        CmbRevAz.Text = "N"
                '                        d_Valore = 0
                '                    End If
                '                Else
                '                    CmbRevAz.Text = "N"
                '                    d_Valore = 0
                '                    If g_Polar <> "LHCP" Then
                '                        CmbRevAz.Text = "Y"
                '                        d_Valore = 1
                '                    End If
                '                End If
                'aggiornato il 22/07/2020 sul calcolo del reverse az. La statistica dice che con Pol = RHCP il RecAz = Y altrimenti = N
                If g_Polar = "RHCP" Then
                    CmbRevAz.Text = "Y"
                    d_Valore = 1
                ElseIf g_Polar = "LHCP" Then
                    CmbRevAz.Text = "N"
                    d_Valore = 0
                End If
                FrmMain.TmrPhase.Enabled = False
                InvioPhase(d_Valore, 0, 6, ComponentCode)
                FrmMain.TmrPhase.Enabled = True
                '            End If
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                Port = Canali(ACU).Port_IN
                Port_Control(ACU).Rec_State = PCR_WAIT
                s_program = " P " & Format$(MaxAz, "000.000") & " " &
                Format$(MaxEl, "00.000") + " "
                i = Len(s_program) + 6
                FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                i_Cont = 3
                Attesa(0.15)
                InvioACU(SEND_ACU)
                LblTesto.BackColor = vbGreen
                LblTesto.Text = "Phase calculated"
                i_Auto = 0
                '04/12/2018 Permette lo scambio con l'ACU
                i_Cont = 1
                FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
                If Port_Control(ACU).Socket = "N" Then
                    FrmRicezione.TmrTimeoutMSG(ACU).Interval = If(Port_Control(ACU).Time_out * 1000 > 0, Port_Control(ACU).Time_out * 1000, 30000)
                Else
                    FrmRicezione.TmrTimeoutMSG(ACU).Interval = If(Port_Control(ACU).Time_out * 1000 > 0, Port_Control(ACU).Time_out * 1000, 30000)
                End If
                d_Attesa = DateAdd("s", 0.5, Now)
                '        End If

        End Select
        i_Cont = 1
        TxtFreccia.Focus()
        Catch ex As Exception
            WriteError(0, "FrmRicevitore - AutoPhase " & ex.Message)
        End Try
    End Sub
    Private Sub AutoAugmented()
        Dim s_program As String, iiz As Integer
        Try
        Port = Canali(ACU).Port_IN
        Select Case i_AutoAugmented
            Case 1
                If LblLock.Text = "LOCK" Then
                    If IsNumeric(TxtLev.Text) Then ErrorElOld = CSng(TxtLev.Text)
                    If IsNumeric(TxtPhaseEl.Text) Then PhaseNew = CInt(TxtPhaseEl.Text)
                    AzOffset += CDbl(IncEl1)
                    FrmRicezione.LblAzError.Text = AzOffset
                    FrmRicezione.LblElError.Text = ElOffset
                    d_Attesa = DateAdd("s", 10, Now)
                    i_AutoAugmented = 2
                End If
            Case 2
                If Now > d_Attesa Then
                    If CSng(TxtLev.Text) - ErrorElOld > 0 Then                    'Errore maggiore di prima
                        ErrorElOld = CSng(TxtLev.Text)
                        AzOffset += CDbl(IncEl1)
                        FrmRicezione.LblAzError.Text = AzOffset
                        FrmRicezione.LblElError.Text = ElOffset
                    Else
                        ErrorElOld = CSng(TxtLev.Text)
                        AzOffset -= (CDbl(IncEl1) * 2)    'Errore minore - torna indietro
                        FrmRicezione.LblAzError.Text = AzOffset
                        FrmRicezione.LblElError.Text = ElOffset
                        d_Attesa = DateAdd("s", 10, Now)
                        i_AutoAugmented = 3
                    End If
                End If
            Case 3
                If Now > d_Attesa Then
                    If CSng(TxtLev.Text) - ErrorElOld > 0 Then                    'Errore maggiore di prima
                        ErrorElOld = CSng(TxtLev.Text)
                        AzOffset -= CDbl(IncEl1)
                        FrmRicezione.LblAzError.Text = AzOffset
                        FrmRicezione.LblElError.Text = ElOffset
                        d_Attesa = DateAdd("s", 10, Now)
                    Else
                        ErrorElOld = CSng(TxtLev.Text)
                        AzOffset += CDbl(IncEl1)     'Errore minore - torna indietro
                        ElOffset += CDbl(IncEl1)     'Errore minore - torna indietro
                        FrmRicezione.LblAzError.Text = AzOffset
                        FrmRicezione.LblElError.Text = ElOffset
                        d_Attesa = DateAdd("s", 10, Now)
                        i_AutoAugmented = 4
                    End If
                End If
            Case 4
                If Now > d_Attesa Then
                    If CSng(TxtLev.Text) - ErrorElOld > 0 Then                    'Errore maggiore di prima
                        ErrorElOld = CSng(TxtLev.Text)
                        ElOffset += CDbl(IncEl1)
                        d_Attesa = DateAdd("s", 10, Now)
                        FrmRicezione.LblAzError.Text = AzOffset
                        FrmRicezione.LblElError.Text = ElOffset
                    Else
                        ErrorElOld = CSng(TxtLev.Text)
                        ElOffset -= (CDbl(IncEl1) * 2)    'Errore minore - torna indietro
                        d_Attesa = DateAdd("s", 10, Now)
                        FrmRicezione.LblAzError.Text = AzOffset
                        FrmRicezione.LblElError.Text = ElOffset
                        i_AutoAugmented = 5
                    End If
                End If
            Case 5
                If Now > d_Attesa Then
                    If CSng(TxtLev.Text) - ErrorElOld > 0 Then                    'Errore maggiore di prima
                        ErrorElOld = CSng(TxtLev.Text)
                        ElOffset -= CDbl(IncEl1)
                        FrmRicezione.LblAzError.Text = AzOffset
                        FrmRicezione.LblElError.Text = ElOffset
                        d_Attesa = DateAdd("s", 10, Now)
                    Else
                        ErrorElOld = CSng(TxtLev.Text)
                        ElOffset += CDbl(IncEl1) * 2   'Errore minore - torna indietro e posizionati per inizio fase
                        ElOffset += CDbl(IncEl)   'Errore minore - torna indietro e posizionati per inizio fase
                        FrmRicezione.LblAzError.Text = AzOffset
                        FrmRicezione.LblElError.Text = ElOffset
                        d_Attesa = DateAdd("s", 10, Now)
                        i_AutoAugmented = 6
                        MaxAz = CDbl(TxtAz.Text)
                        MaxEl = CDbl(TxtEl.Text)
                    End If
                End If
            Case 6
                If Now > d_Attesa Then
                    If IsNumeric(TxtElErr.Text) Then ErrorElOld = CSng(TxtElErr.Text)
                    CmdPiu_Click(Nothing, EventArgs.Empty)
                    LblTesto.Text = "Phase +"
                    i_AutoAugmented = 7
                    d_Attesa = DateAdd("s", 1, Now)
                End If
            Case 7
                If Now > d_Attesa Then
                    If Math.Abs(ErrorElOld - CSng(TxtElErr.Text)) > 0.08 Then
                        If CSng(TxtElErr.Text) > ErrorElOld Then
                            ErrorElOld = CSng(TxtElErr.Text)
                            CmdPiu_Click(Nothing, EventArgs.Empty)
                            LblTesto.Text = "Phase ++"
                            '                     d_Attesa = DateAdd("s", 0.5, Now)
                        ElseIf CSng(TxtElErr.Text) < ErrorElOld Then
                            ErrorElOld = CSng(TxtElErr.Text)
                            CmdMeno_Click(Nothing, EventArgs.Empty)
                            LblTesto.Text = "Phase -"
                            i_AutoAugmented = 8
                            '                     d_Attesa = DateAdd("s", 0.6, Now)
                        End If
                    Else
                        CmdPiu_Click(Nothing, EventArgs.Empty)
                        LblTesto.Text = "Phase +"
                    End If
                    d_Attesa = DateAdd("s", 0.6, Now)
                End If
                i_cont1 = 0
            Case 8
                If Now > d_Attesa Then
                    If Math.Abs(ErrorElOld - CSng(TxtElErr.Text)) > 0.08 Then
                        d_Attesa = DateAdd("s", 0.6, Now)
                        If CSng(TxtElErr.Text) > ErrorElOld Then
                            ErrorElOld = CSng(TxtElErr.Text)
                            CmdMeno_Click(Nothing, EventArgs.Empty)
                            LblTesto.Text = "Phase -"
                            d_Attesa = DateAdd("s", 0.6, Now)
                            i_cont1 = 0
                        Else
                            ErrorElOld = CSng(TxtElErr.Text)
                            iiz = 1
                            For iiz = 1 To i_cont1
                                CmdPiu_Click(Nothing, EventArgs.Empty)
                                Attesa(0.2)
                            Next
                            AzOffset += CDbl(IncEl)
                            ElOffset -= CDbl(IncEl)
                            FrmRicezione.LblAzError.Text = AzOffset
                            FrmRicezione.LblElError.Text = ElOffset
                            d_Attesa = DateAdd("s", 10, Now)
                            i_AutoAugmented = 9
                        End If
                    Else
                        i_cont1 += 1
                        CmdMeno_Click(Nothing, EventArgs.Empty)
                        LblTesto.Text = "Phase -"
                    End If
                End If
            Case 9
                '         If Now > d_Attesa Then
                '            If LblErrAz.Text < 0 Then
                '                If CmbRevAz.Text = "N" Then
                '                    CmbRevAz.Text = "Y"
                '                    d_Valore = 1
                '                    If g_Polar <> "RHCP" Then
                '                        CmbRevAz.Text = "N"
                '                        d_Valore = 0
                '                    End If
                '                Else
                '                    CmbRevAz.Text = "N"
                '                    d_Valore = 0
                '                    If g_Polar <> "LHCP" Then
                '                        CmbRevAz.Text = "Y"
                '                        d_Valore = 1
                '                    End If
                '                End If
                'aggiornato il 22/07/2020 sul calcolo del reverse az. La statistica dice che con Pol = RHCP il RecAz = Y altrimenti = N
                If g_Polar = "RHCP" Then
                    CmbRevAz.Text = "Y"
                    d_Valore = 1
                ElseIf g_Polar = "LHCP" Then
                    CmbRevAz.Text = "N"
                    d_Valore = 0
                End If
                FrmMain.TmrPhase.Enabled = False
                InvioPhase(d_Valore, 0, 6, ComponentCode)
                FrmMain.TmrPhase.Enabled = True
                '            End If
                AzOffset -= IncEl
                '            ElOffset = 0
                FrmRicezione.LblAzError.Text = AzOffset
                FrmRicezione.LblElError.Text = ElOffset
                LblTesto.BackColor = vbGreen
                LblTesto.Text = "Phase calculated"
                i_AutoAugmented = 0
                FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
                If Port_Control(ACU).Socket = "N" Then
                    FrmRicezione.TmrTimeoutMSG(ACU).Interval = If(Port_Control(ACU).Time_out * 1000 > 0, Port_Control(ACU).Time_out * 1000, 30000)
                Else
                    FrmRicezione.TmrTimeoutMSG(ACU).Interval = If(Port_Control(ACU).Time_out * 1000 > 0, Port_Control(ACU).Time_out * 1000, 30000)
                End If
                d_Attesa = DateAdd("s", 0.5, Now)
                '        End If

        End Select
        TxtFreccia.Focus()
        Catch ex As Exception
            WriteError(0, "AutoAugmented: " & ex.Message)
        End Try
    End Sub

    Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Left) Then
            Select Case e.KeyCode
                Case Keys.Up
                    CmdUp_Click(Nothing, EventArgs.Empty)
                Case Keys.Down
                    CmdDown_Click(Nothing, EventArgs.Empty)
                Case Keys.Right
                    CmdCW_Click(Nothing, EventArgs.Empty)
                Case Keys.Left
                    CmdCCW_Click(Nothing, EventArgs.Empty)
            End Select
        End If
    End Sub

    Private Sub InviaCoord()
        Dim i As Integer
        Dim s_program As String
        s_program = ""
        'Invia = False
        If Port_Control(ACU).Socket = "N" Then
            Port = Canali(ACU).Port_IN
            If Port_Control(ACU).Rec_State <> PCR_STANDBY Then

                Exit Sub
            End If

            If Not IsNumeric(AzPro) Or Not IsNumeric(ElPro) Then
                '             CmbEl.Focus()
                TxtFreccia.Focus()
                Exit Sub
            End If
        End If
        'invia solo se la porta e' abilitata
        b_Ricezione(ACU) = False
        If AzInibhit And ElInibhit Then Exit Sub
        If Not AzInibhit And Not ElInibhit Then
            s_program = " P " & Format$(AzPro, "000.000") & " " &
            Format$(ElPro, "00.000") + " "
        ElseIf Not AzInibhit Then
            s_program = "AP " & Format$(AzPro, "000.000") & " "
        ElseIf Not ElInibhit Then
            s_program = "EP " & Format$(ElPro, "00.000") & " "
        End If
        i = Len(s_program) + 6
        FrmRicezione.TmrTimeoutMSG(ACU).Enabled = False
        b_int(ACU) = True
        b_Ricezione(ACU) = False
        Port = Canali(ACU).Port_IN
        Port_Control(ACU).Rec_State = PCR_WAIT
        SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
        InvioACU(SEND_ACU)
        b_Vai = False
        FrmRicezione.LblAzimuth.Text = Format$(AzPro, "0.000")
        FrmRicezione.LblElevazione.Text = Format$(ElPro, "0.000")
        AzCalc = CDbl(FrmRicezione.LblAzimuth.Text)
        ElCalc = CDbl(FrmRicezione.LblElevazione.Text)
        TxtFreccia.Focus()
    End Sub
    Private Sub TxtFreccia_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtFreccia.KeyDown
        If (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Left) Then
            Select Case e.KeyCode
                Case Keys.Up
                    CmdUp_Click(Nothing, EventArgs.Empty)
                Case Keys.Down
                    CmdDown_Click(Nothing, EventArgs.Empty)
                Case Keys.Right
                    CmdCW_Click(Nothing, EventArgs.Empty)
                Case Keys.Left
                    CmdCCW_Click(Nothing, EventArgs.Empty)
            End Select
        End If

    End Sub

    Private Sub TxtRange_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtRange.KeyUp
        If e.KeyCode = CR Then
            d_Valore = TxtRange.Text
            InvioPhase(d_Valore, 1, 11, ComponentCodeIF1)
            '        Attesa 0.5
            Interroga = True
        End If
        TxtFreccia.Focus()
    End Sub
    Private Sub TmrAlarm_Timer(sender As Object, e As EventArgs) Handles tmrAlarm.Tick
        LblWarning.Text = ""
        LblWarning.Visible = False
        tmrAlarm.Enabled = False
    End Sub

    ' Nessun ridimensionamento/riscalatura dei controlli: form non a schermo
    ' intero, non serve.

End Class

