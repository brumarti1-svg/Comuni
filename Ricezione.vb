Imports System.Data.SQLite

Module Ricezione
    Dim s_analisi As String
    Dim i As Integer, lun As Integer, d_Az As String, d_El As String, d_Po As String
    Dim rs_Ricez As Object
    Dim Az_prov As Double, El_Prov As Double


    '13/03/2024 Viene creato un modulo comune a tutti i programmi ACU che raccoglie le routine di calcolo dei vari algoritmi uguali per tutti
    'attualmente inseriti nella frmRicezione di ciascun programma. Si cerca System.Math.ì di razionalizzare i programmi per evitare
    'di dover ripetere eventuali correzioni ogni volta per tutti

    Public Function OrdiniMcs(Ordine As String) As Boolean
        OrdiniMcs = True
        g_aggOrdini = True
        lun = Len(Ordine)
        i = InStr(2, Ordine, " ", 1)
        If i = 0 Then
            s_analisi = Mid$(Ordine, 1, lun)
        Else
            s_analisi = Mid$(Ordine, 1, i - 1)
        End If
        Select Case s_analisi
            Case "*", "?", ">", "<"
                b_Ricezione(ACU) = True
                i_Cont = 2
                Attesa(0.2)
            Exit Function
            Case " S", "AS", "ES"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.LblCommand.Text = "Stand By"
                i_Status = OP_STANDBY
                Exit Function
            Case " ST", "AST", "EST"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.CmdOffset.Enabled = False
                frmRicezione.CmdOffset.Visible = False
                i_Status = OP_STEPTRACK
                frmRicezione.LblStepTrack.Visible = True
                frmRicezione.LblCommand.Text = "Step Track"
                Exit Function
            Case " AQ", "AAQ", "EAQ"
                PTK = &H0
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.CmdOffset.Enabled = False
                frmRicezione.CmdOffset.Visible = False
                i_Status = OP_AUTOMATICAQUIRE
                frmRicezione.LblCommand.Text = "Automatic Acquire"
                i_ProgTrack = 7
                Exit Function
            Case " M", "AM", "EM"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblCommand.Text = "Memory Track"
                i_Status = OP_MEMORYTRACK
                Exit Function
            Case " W", "AW", "EW"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.LblCommand.Text = "Stow"
                i_Status = OP_STANDBY
                Exit Function
            Case " U", "AU", "EU"
                PTK = &H0
                '            d_DataInizioProg = CDate("31/12/3000 00.00.00")
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.LblCommand.Text = "Unstow"
                i_Status = OP_STANDBY
                Exit Function
            Case " P", " X", "WS", "US", "BP"
                If lun > 15 Then
                    d_Az = Mid$(Ordine, 4, 7)
                    d_El = Mid$(Ordine, 12, 6)
                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            If d_El >= 0 And d_El < 92 Then
                                PTK = &H0
                                d_DataInizioProg = ""
                                MainForm.ProgramStatus = d_DataInizioProg
                                i_Status = OP_COMMANDPOSITION
                                frmRicezione.LblCommand.Text = "Command Position"
                                frmRicezione.LblAzimuth.Text = d_Az
                                frmRicezione.LblElevazione.Text = d_El
                                AzCalc = d_Az
                                ElCalc = d_El
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 4, 6)
                    d_El = Mid$(Ordine, 11, 5)
                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            If d_El >= 0 And d_El < 92 Then
                                PTK = &H0
                                d_DataInizioProg = ""
                                MainForm.ProgramStatus = d_DataInizioProg
                                i_Status = OP_COMMANDPOSITION
                                frmRicezione.LblCommand.Text = "Command Position"
                                frmRicezione.LblAzimuth.Text = d_Az
                                frmRicezione.LblElevazione.Text = d_El
                                AzCalc = d_Az
                                ElCalc = d_El
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                End If
            Case "AP", "AX"
                If lun > 9 Then
                    d_Az = Mid$(Ordine, 4, 7)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            frmRicezione.LblAzimuth.Text = d_Az
                            AzCalc = d_Az
                            PTK = &H0
                            d_DataInizioProg = ""
                            MainForm.ProgramStatus = d_DataInizioProg
                            i_Status = OP_COMMANDPOSITION
                            frmRicezione.LblCommand.Text = "Command Position"
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 4, 6)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            frmRicezione.LblAzimuth.Text = d_Az
                            AzCalc = d_Az
                            PTK = &H0
                            d_DataInizioProg = ""
                            MainForm.ProgramStatus = d_DataInizioProg
                            i_Status = OP_COMMANDPOSITION
                            frmRicezione.LblCommand.Text = "Command Position"
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                End If
            Case "EP", "EX"
                If lun > 8 Then
                    d_El = Mid$(Ordine, 4, 6)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            PTK = &H0
                            d_DataInizioProg = ""
                            MainForm.ProgramStatus = d_DataInizioProg
                            i_Status = OP_COMMANDPOSITION
                            frmRicezione.LblCommand.Text = "Command Position"
                            frmRicezione.LblElevazione.Text = d_El
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 4, 5)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            PTK = &H0
                            d_DataInizioProg = ""
                            MainForm.ProgramStatus = d_DataInizioProg
                            i_Status = OP_COMMANDPOSITION
                            frmRicezione.LblCommand.Text = "Command Position"
                            frmRicezione.LblElevazione.Text = d_El
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                End If
            Case " A", "AA", "EA"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.CmdOffset.Enabled = False
                frmRicezione.CmdOffset.Visible = False
                i_Status = OP_AUTOTRACK
                i_ProgTrack = 2
                frmRicezione.LblCommand.Text = "Auto Track"
                Exit Function
            Case "PLP"                                          'Comando di polarizzazione 31/10/2022
                d_Az = Mid$(Ordine, 5, 5)
                If IsNumeric(d_Az) Then
                    If d_Az >= -98.9 And d_Az < 98.9 Then
                        i_Status = OP_COMMANDPOSITION
                        frmRicezione.LblCommand.Text = "Command Position"
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                OrdiniMcs = True
                Exit Function
        End Select
        OrdiniMcs = False
        g_aggOrdini = False
    End Function

    Public Function Calcolo_Aquire(Data1 As Date, mSec As Integer) As Boolean
        Dim rs_Acq As Object
        Dim s_program As String = "", i_lun As Integer, d_FineProg As Date
        '06/06/2022
        Dim tmpErrAz As System.Single, tmpErrEl As System.Single
        Debug.WriteLine("Calcolo_Aquire ENTRATA: i_ConStato=" & i_ConStato & " i_Step=" & i_Step & " i_Status=" & i_Status & " i_AzStep=" & i_AzStep & " i_ElStep=" & i_ElStep & " i_AzRange=" & i_AzRange & " i_ElRange=" & i_ElRange & " b_Acquire=" & b_Acquire) ' TEMPORANEO: da rimuovere dopo la diagnosi
        On Error Resume Next
        If IsDate(d_DataFineProg) Then
            d_FineProg = CDate(d_DataFineProg)
        End If
        i_ProgTrack = 1
        '09/06/2022
        ' TEMPORANEO (test): controllo segnale/lock disattivato per poter testare lo Spiral Acquire senza un segnale reale agganciato. Da ripristinare (basta togliere "False AndAlso") a test finito.
        If False AndAlso b_Lock Then
            If Val(s_Lev1) > g_ThrAuto And RTrim(frmRicezione.LblLock.Text) = "LOCK" And Math.Abs(Val(s_LevAz1)) < g_ThrAz And Math.Abs(Val(s_LevEl1)) < g_ThrEl Then     '26/10/2022
                'controllo se è stato ricevuto il segnale
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                i_Status = OP_AUGMENTED
                i_ProgTrack = 1
                frmRicezione.CmdOffset.Enabled = True
                frmRicezione.CmdOffset.Visible = True
                frmRicezione.LblCommand.Text = "Augmented"
                PTK = &H10
                AzOff += i_AzOffsetLock
                ElOff += i_ElOffsetLock
                i_AzOffsetLock = 0
                i_ElOffsetLock = 0
                frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
                frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
                frmRicezione.TxtOrdini.Focus()
                '                frmRicezione.MnuMain(15).Visible = False
                frmRicezione.LblAzErrEn.Visible = False
                frmRicezione.LblElErrEn.Visible = False
                frmRicezione.TmrSpiralAcquire.Enabled = False
                '                Unload frmVisualAcquire
                Exit Function
            Else                'lock ma non nelle soglie
                If Not b_LockOn Then
                    b_LockOn = True         'è passato
                    g_AzTable = CDbl(frmRicezione.LblAzimuth.Text)
                    g_ElTable = CDbl(frmRicezione.LblElevazione.Text)
                    i_AzStep = i_AzStepLock
                    i_ElStep = i_ElStepLock
                    '                   If b_Acquire Then
                    AzOff = i_AzOffsetLock + AzOffset
                    ElOff = i_ElOffsetLock + ElOffset
                    i_AzOffsetLock = 0
                    i_ElOffsetLock = 0
                    '14/06/2022
                    '                    Else
                    '                        AzOff = i_AzOffsetLock + AzOffset
                    '                        ElOff = i_ElOffsetLock + ElOffset
                    '                        i_AzOffsetLock = 0
                    '                        i_ElOffsetLock = 0
                    '                    End If
                    frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
                    frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                    i_ConStato = 0
                    '                    dbOrbite.Execute "DELETE * from tmpEffemeridi"
                    '                     rs_Acq = dbOrbite.OpenRecordset("tmpEffemeridi", dbOpenDynaset)
                    '                    rs_Acq.AddNew
                    '                    rs_Acq!date = Mid$(Data1, 1, 10)
                    '                    rs_Acq!Time = Mid$(Data1, 12, 8)
                    '                    rs_Acq!Azimuth = Format$(g_AzTable, "000.000")
                    '                    rs_Acq!Elevation = Format$(g_ElTable, "00.000")
                    '                    rs_Acq!Orbite = 1
                    '                    rs_Acq!Long = i_AzOffsetLock
                    '                    rs_Acq!lat = i_ElOffsetLock
                    '                    rs_Acq!Lock = LblLock.Text
                    '                    rs_Acq.Update
                    '                    rs_Acq.Close
                    i_Spiral += 1
                    i_Quadrati = 1
                    b_RangeEl = False
                    b_RangeAz = False
                    '                    Exit Function
                End If
            End If
        End If
        'Sistema in time out posiziona l'antenna nelle cordinate iniziali
        If d_FineProg <= DateAdd("h", -i_Off, Now) Then
            i_ProgTrack = 0
            i_AzOffsetLock = 0
            i_ElOffsetLock = 0
            AzOff = 0
            ElOff = 0
            frmRicezione.LblAzErrEn.Text = "0"
            frmRicezione.LblElErrEn.Text = "0"
            frmRicezione.LblAzErrEn.Visible = False
            frmRicezione.LblElErrEn.Visible = False
            frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
            frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
            AzCalc = g_AzTable
            ElCalc = g_ElTable
            frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
            frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
            s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
            i = Len(s_program) + 6
            SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            InvioACU(SEND_ACU)
            i_Status = OP_COMMANDPOSITION
            frmRicezione.LblCommand.Text = "Command Position"
            '            frmRicezione.MnuMain(15).Visible = False
            frmRicezione.TmrSpiralAcquire.Enabled = False
            '            Unload frmVisualAcquire
            Exit Function
        End If
        Select Case i_ConStato
            Case 0
                'inizio della spirale. L'antenna viene spostata di uno +step in azimuth
                If b_Acquire Then
                    'siamo in preset
                    frmRicezione.LblAzimuth.Text = Format$(g_AzTable + i_AzStep, "000.000")
                    frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                    i_AzOffsetLock = Format$(i_AzStep, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                    i_ElOffsetLock = 0
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
        Else
                    i_AzOffsetLock = Format$(i_AzStep, "#0.0#")
                    i_ElOffsetLock = 0
                    Calcolo_Coordinate(Data1, mSec)
            frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                End If
                i_ConStato += 1
                i_Step = 1
            Case 1
                'primo lato
                If i_Quadrati * i_ElStep >= i_ElRange Then
                    b_RangeEl = True
                End If
                If b_Acquire Then
                    'siamo in preset
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    If Not b_RangeEl Then i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
        Else
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    Calcolo_Coordinate(Data1, mSec)
        End If
                i_Step += 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeAz Then
                        i_Step = i_AzRange / i_AzStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 2
                'secondo lato
                If b_Acquire Then
                    'siamo in preset
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) - i_AzStep, "000.000")
                    i_AzOffsetLock = Format$(i_AzOffsetLock - i_AzStep, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
        Else
                    i_AzOffsetLock = Format$(i_AzOffsetLock - i_AzStep, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    Calcolo_Coordinate(Data1, mSec)
        End If
                i_Step += 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeEl Then
                        i_Step = i_ElRange / i_ElStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 3
                'terzo lato

                If b_Acquire Then
                    'siamo in preset
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) - i_ElStep, "00.000")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    i_ElOffsetLock = Format$(i_ElOffsetLock - i_ElStep, "#0.0#")
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    ' TEMPORANEO (test): controllo elevazione minima disattivato. Da ripristinare rimettendo ">= g_ElMin" al posto di "True".
                    If True Then
                        s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                        Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                        i = Len(s_program) + 6
                        SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                        b_int(ACU) = True
                        b_Ricezione(ACU) = False
                        InvioACU(SEND_ACU)
            Else
                        frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                        frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                        i_AzOffsetLock = 0
                        frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                        i_ElOffsetLock = 0
                        frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                        s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                        Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                        i = Len(s_program) + 6
                        SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                        b_int(ACU) = True
                        b_Ricezione(ACU) = False
                        InvioACU(SEND_ACU)
                i_Status = OP_COMMANDPOSITION
                        '                frmRicezione.MnuMain(15).Visible = False
                        frmRicezione.TmrSpiralAcquire.Enabled = False
                        '                Unload frmVisualAcquire
                        Exit Function
                    End If
                Else
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    i_ElOffsetLock = Format$(i_ElOffsetLock - i_ElStep, "#0.0#")
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    Calcolo_Coordinate(Data1, mSec)
        End If
                i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += i_ConStato
                    If b_RangeAz Then
                        i_Step = i_AzRange / i_AzStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 4
                'secondo lato
                If b_Acquire Then
                    'siamo in preset
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                    i_AzOffsetLock = Format$(i_AzOffsetLock + i_AzStep, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
        Else
                    i_AzOffsetLock = Format$(i_AzOffsetLock + i_AzStep, "000.000")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    Calcolo_Coordinate(Data1, mSec)
        End If
                i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeEl Then
                        i_Step = (i_ElRange / i_ElStep) - 1
                    Else
                        i_Step = i_Quadrati - 1
                    End If
                End If
            Case 5
                If i_Step = 0 Then
                    i_ConStato += 1
                Else
                    'ultimo lato
                    If b_Acquire Then
                        'siamo in preset
                        frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                        frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                        i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                        frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                        s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                         Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                        i = Len(s_program) + 6
                        SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                        b_int(ACU) = True
                        b_Ricezione(ACU) = False
                        InvioACU(SEND_ACU)
             Else
                        frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                        i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                        frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                        Calcolo_Coordinate(Data1, mSec)
             End If
                    i_Step -= 1
                    If i_Step = 0 Then
                        i_ConStato += 1
                    End If
                End If
            Case 6
                'Sposta quadrato
                If i_Quadrati * i_AzStep >= i_AzRange Then
                    b_RangeAz = True 'controllo se superato il range in azimuth
                End If
                If b_Acquire Then
                    'siamo in preset
                    If Not b_RangeAz Then frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    If Not b_RangeAz Then i_AzOffsetLock = Format$(i_AzOffsetLock + i_AzStep, "#0.0#")
                    i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
        Else
                    '            frmRicezione.LblAzimuth = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                    '            frmRicezione.LblElevazione = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    If Not b_RangeAz Then i_AzOffsetLock = Format$(i_AzOffsetLock + i_AzStep, "#0.0#")
                    i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    Calcolo_Coordinate(Data1, mSec)
        End If
                i_Quadrati += 1
                If b_RangeEl Then
                    i_Step = i_ElRange / i_ElStep
                Else
                    i_Step = i_Quadrati
                End If
                i_ConStato = 1
            Case 7
                'fine della spirale. L'antenna non viene spostata
                If b_Acquire Then
                    'siamo in preset
                    frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                    frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                    i_AzOffsetLock = 0
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                    i_ElOffsetLock = 0
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
        Else
                    frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                    frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                    i_AzOffsetLock = 0
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                    i_ElOffsetLock = 0
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                    Calcolo_Coordinate(Data1, mSec)
        End If
                i_ConStato += 1
            Case 8
                If b_RangeAz And b_RangeEl Then             'gli spostamenti hanno raggiunto il range in Az ed El
                    tmpErrAz = Format$(Val(frmRicezione.TxtAz.Text) - CDbl(frmRicezione.LblAzimuth.Text), "0.000")
                    tmpErrEl = Format$(Val(frmRicezione.TxtEl.Text) - CDbl(frmRicezione.LblElevazione.Text), "0.000")
                    If Math.Abs(tmpErrAz) > Math.Abs(CoorAcq) Or Math.Abs(tmpErrEl) > Math.Abs(CoorAcq) Then
                        GoTo Rangeout
                    End If
                    b_RangeAz = False
                    b_RangeEl = False
                    i_Quadrati = 1
                    i_ConStato = 0
                i_Spiral += 1
                End If
        End Select
        If b_RangeAz And b_RangeEl Then             'gli spostamenti hanno raggiunto il range in Az ed El
            If i_ConStato < 7 Then i_ConStato = 7
        End If
Rangeout:
        ' TEMPORANEO (test): controllo elevazione minima disattivato. Da ripristinare rimettendo ">= g_ElMin" al posto di "True".
        If True Then
            rs_Acq = dbOrbite.OpenRecordset("tmpEffemeridi", dbOpenDynaset)
            If i_Spiral > 2 Then                          'Cancella anche se in Lock
                If i_Spiral - rs_Acq!Orbite >= 2 Then
                    rs_Acq.MoveFirst
                    rs_Acq.Delete
                    rs_Acq.Update
                End If
            End If
            If i_ConStato < 8 Then
                If IsNumeric(frmRicezione.LblAzimuth.Text) And IsNumeric(frmRicezione.LblElevazione.Text) Then
                    rs_Acq.AddNew
                    rs_Acq!date = Data1.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                    rs_Acq!Time = Data1.ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                    rs_Acq!Azimuth = Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000")
                    rs_Acq!Elevation = Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000")
                    rs_Acq!Long = i_AzOffsetLock
                    rs_Acq!lat = i_ElOffsetLock
                    rs_Acq!Orbite = i_Spiral
                    rs_Acq.Update
                    rs_Acq.Close
                End If
            End If
        Else
            frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
            frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
            i_AzOffsetLock = 0
            frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
            i_ElOffsetLock = 0
            frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
            s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
            i = Len(s_program) + 6
            SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            InvioACU(SEND_ACU)
             i_Status = OP_COMMANDPOSITION
            frmRicezione.LblCommand.Text = "Command Position"
            '                frmRicezione.MnuMain(15).Visible = False
            frmRicezione.TmrSpiralAcquire.Enabled = False
            '             Unload frmVisualAcquire
            i_AzOffsetLock = 0
            i_ElOffsetLock = 0
            b_RangeAz = False
            b_RangeEl = False
            Exit Function
        End If
    End Function
    Public Function Calcolo_Aquire1(Data1 As Date, mSec As Integer) As Boolean
        '17/2/2025 viene creata una routine di Calcolo Acquire semplificata per una ricerca rapida del satellite nel calcolo della fase
        Dim rs_Acq As Object
        Dim s_program As String = "", i_lun As Integer, d_FineProg As Date
        Dim tmpErrAz As System.Single, tmpErrEl As System.Single
        On Error Resume Next
        If IsDate(d_DataFineProg) Then
            d_FineProg = CDate(d_DataFineProg)
        End If
        i_ProgTrack = 1
        '09/06/2022
        If b_Lock Then
            If Val(s_Lev1) > g_ThrAuto And RTrim(frmRicezione.LblLock.Text) = "LOCK" And Math.Abs(Val(s_LevAz1)) < g_ThrAz And Math.Abs(Val(s_LevEl1)) < g_ThrEl Then     '26/10/2022
                'controllo se è stato ricevuto il segnale
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                '                i_Status = OP_AUGMENTED
                '                i_ProgTrack = 1
                '                frmRicezione.CmdOffset.Enabled = True
                '                frmRicezione.CmdOffset.Visible = True
                '                frmRicezione.LblCommand.Text = "Augmented"
                '                PTK = &H10
                AzOff += i_AzOffsetLock
                ElOff += i_ElOffsetLock
                i_AzOffsetLock = 0
                i_ElOffsetLock = 0
                frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
                frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
                frmRicezione.TxtOrdini.Focus()
                '                frmRicezione.MnuMain(15).Visible = False
                frmRicezione.LblAzErrEn.Visible = False
                frmRicezione.LblElErrEn.Visible = False
                frmRicezione.TmrSpiralAcquire.Enabled = False
                i_StepTrack = 12                                'Fine acquire
                Exit Function
            Else                'lock ma non nelle soglie
                If Not b_LockOn Then
                    b_LockOn = True         'è passato
                    g_AzTable = CDbl(frmRicezione.LblAzimuth.Text)
                    g_ElTable = CDbl(frmRicezione.LblElevazione.Text)
                    i_AzStep = i_AzStepLock
                    i_ElStep = i_ElStepLock
                    AzOff = i_AzOffsetLock + AzOffset
                    ElOff = i_ElOffsetLock + ElOffset
                    i_AzOffsetLock = 0
                    i_ElOffsetLock = 0
                    frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
                    frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                    i_ConStato = 0
                    i_Spiral += 1
                    i_Quadrati = 1
                    b_RangeEl = False
                    b_RangeAz = False
                End If
            End If
        End If
        'Sistema in time out posiziona l'antenna nelle cordinate iniziali
        If d_FineProg <= DateAdd("h", -i_Off, Now) Then
            i_ProgTrack = 0
            i_AzOffsetLock = 0
            i_ElOffsetLock = 0
            AzOff = 0
            ElOff = 0
            frmRicezione.LblAzErrEn.Text = "0"
            frmRicezione.LblElErrEn.Text = "0"
            frmRicezione.LblAzErrEn.Visible = False
            frmRicezione.LblElErrEn.Visible = False
            frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
            frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
            AzCalc = g_AzTable
            ElCalc = g_ElTable
            frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
            frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
            s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
            i = Len(s_program) + 6
            SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            InvioACU(SEND_ACU)
            i_Status = OP_COMMANDPOSITION
            frmRicezione.LblCommand.Text = "Command Position"
            '            frmRicezione.MnuMain(15).Visible = False
            frmRicezione.TmrSpiralAcquire.Enabled = False
            '            Unload frmVisualAcquire
            Exit Function
        End If
        Select Case i_ConStato
            Case 0
                'inizio della spirale. L'antenna viene spostata di uno +step in azimuth
                'siamo in preset
                frmRicezione.LblAzimuth.Text = Format$(g_AzTable + i_AzStep, "000.000")
                frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                i_AzOffsetLock = Format$(i_AzStep, "#0.0#")
                frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                i_ElOffsetLock = 0
                frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
                i_ConStato += 1
                i_Step = 1
            Case 1
                'primo lato
                If i_Quadrati * i_ElStep >= i_ElRange Then
                    b_RangeEl = True
                End If
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                If Not b_RangeEl Then i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeAz Then
                        i_Step = i_AzRange / i_AzStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 2
                'secondo lato
                frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) - i_AzStep, "000.000")
                i_AzOffsetLock = Format$(i_AzOffsetLock - i_AzStep, "#0.0#")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                 Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeEl Then
                        i_Step = i_ElRange / i_ElStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 3
                'terzo lato
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) - i_ElStep, "00.000")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                i_ElOffsetLock = Format$(i_ElOffsetLock - i_ElStep, "#0.0#")
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                If CDbl(frmRicezione.LblElevazione.Text) >= g_ElMin Then
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
        Else
                    frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                    frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                    i_AzOffsetLock = 0
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                    i_ElOffsetLock = 0
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
            i_Status = OP_COMMANDPOSITION
                    '                frmRicezione.MnuMain(15).Visible = False
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    '                Unload frmVisualAcquire
                    Exit Function
                End If
                i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeAz Then
                        i_Step = i_AzRange / i_AzStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 4
                frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                i_AzOffsetLock = Format$(i_AzOffsetLock + i_AzStep, "#0.0#")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                 Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeEl Then
                        i_Step = (i_ElRange / i_ElStep) - 1
                    Else
                        i_Step = i_Quadrati - 1
                    End If
                End If
            Case 5
                If i_Step = 0 Then
                    i_ConStato += 1
                Else
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
             i_Step -= 1
                    If i_Step = 0 Then
                        i_ConStato += 1
                    End If
                End If
            Case 6
                'Sposta quadrato
                If i_Quadrati * i_AzStep >= i_AzRange Then
                    b_RangeAz = True 'controllo se superato il range in azimuth
                End If
                If Not b_RangeAz Then frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                If Not b_RangeAz Then i_AzOffsetLock = Format$(i_AzOffsetLock + i_AzStep, "#0.0#")
                i_ElOffsetLock = Format$(i_ElOffsetLock + i_ElStep, "#0.0#")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
             Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
                i_Quadrati += 1
                If b_RangeEl Then
                    i_Step = i_ElRange / i_ElStep
                Else
                    i_Step = i_Quadrati
                End If
                i_ConStato = 1
            Case 7
                'fine della spirale. L'antenna non viene spostata
                frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                i_AzOffsetLock = 0
                frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
                i_ElOffsetLock = 0
                frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                   Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        i_ConStato += 1
            Case 8
                If b_RangeAz And b_RangeEl Then             'gli spostamenti hanno raggiunto il range in Az ed El
                    tmpErrAz = Format$(Val(frmRicezione.TxtAz.Text) - CDbl(frmRicezione.LblAzimuth.Text), "0.000")
                    tmpErrEl = Format$(Val(frmRicezione.TxtEl.Text) - CDbl(frmRicezione.LblElevazione.Text), "0.000")
                    If Math.Abs(tmpErrAz) > Math.Abs(CoorAcq) Or Math.Abs(tmpErrEl) > Math.Abs(CoorAcq) Then
                        GoTo Rangeout
                    End If
                    b_RangeAz = False
                    b_RangeEl = False
                    i_Quadrati = 1
                    i_ConStato = 0
                i_Spiral += 1
                End If
        End Select
        If b_RangeAz And b_RangeEl Then             'gli spostamenti hanno raggiunto il range in Az ed El
            If i_ConStato < 7 Then i_ConStato = 7
        End If
Rangeout:
        If CDbl(frmRicezione.LblElevazione.Text) >= g_ElMin Then
            rs_Acq = dbOrbite.OpenRecordset("tmpEffemeridi", dbOpenDynaset)
            If i_Spiral > 2 Then                          'Cancella anche se in Lock
                If i_Spiral - rs_Acq!Orbite >= 2 Then
                    rs_Acq.MoveFirst
                    rs_Acq.Delete
                    rs_Acq.Update
                End If
            End If
        Else
            frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
            frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
            i_AzOffsetLock = 0
            frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0#")
            i_ElOffsetLock = 0
            frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0#")
            s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
            i = Len(s_program) + 6
            SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            InvioACU(SEND_ACU)
             i_Status = OP_COMMANDPOSITION
            frmRicezione.LblCommand.Text = "Command Position"
            '                frmRicezione.MnuMain(15).Visible = False
            frmRicezione.TmrSpiralAcquire.Enabled = False
            '             Unload frmVisualAcquire
            i_AzOffsetLock = 0
            i_ElOffsetLock = 0
            b_RangeAz = False
            b_RangeEl = False
            Exit Function
        End If
    End Function
    Public Function AnalisiOrdini(Ordine As String) As Boolean
        Dim Send_ORDINI As String
        On Error GoTo esci
        AnalisiOrdini = False
        lun = Len(Ordine)
        i = InStr(2, Ordine, " ", 1)
        If i = 0 Then
            s_analisi = Mid$(Ordine, 1, lun)
        Else
            s_analisi = Mid$(Ordine, 1, i - 1)
        End If
        i_DestOrdine = ACU
        Select Case s_analisi
            Case "*", "?", ">", "<"
                b_Ricezione(ACU) = True
                AnalisiOrdini = True
                Exit Function
            Case " S", "AS", "ES"
                PTK = &H0
                '            d_DataInizioProg = CDate("31/12/3000 00.00.00")
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                AnalisiOrdini = True
                frmRicezione.LblCommand.Text = "Stand By"
                i_Status = OP_STANDBY
                Send_ORDINI = Mid$(Ordine, 1, 2)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case " ST", "AST", "EST", " PS", "APS", "EPS", " AQ", "AAQ", "EAQ", " PI", "API", "EPI", " PO", "APO", "EPO",
            "AOF", "EOF", "POF"
                AnalisiOrdini = True
                Send_ORDINI = Mid$(Ordine, 1, 3)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case " M", "AM", "EM", " W", "AW", "EW", " U", "AU", "EU"
                AnalisiOrdini = True
                Send_ORDINI = Mid$(Ordine, 1, 2)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case " P", " X", "WS", "US", "BP"
                If lun > 15 Then
                    d_Az = Mid$(Ordine, 4, 7)
                    d_El = Mid$(Ordine, 12, 6)
                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            If d_El >= 0 And d_El < 92 Then
                                frmRicezione.LblAzimuth.Text = d_Az
                                frmRicezione.LblElevazione.Text = d_El
                                AzCalc = d_Az
                                ElCalc = d_El
                                Send_ORDINI = Mid$(Ordine, 1, 17)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 4, 6)
                    d_El = Mid$(Ordine, 11, 5)
                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            If d_El >= 0 And d_El < 92 Then
                                frmRicezione.LblAzimuth.Text = d_Az
                                frmRicezione.LblElevazione.Text = d_El
                                AzCalc = d_Az
                                ElCalc = d_El
                                Send_ORDINI = Mid$(Ordine, 1, 15)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "AP", "AX"
                If lun > 9 Then
                    d_Az = Mid$(Ordine, 4, 7)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            frmRicezione.LblAzimuth.Text = d_Az
                            AzCalc = d_Az
                            Send_ORDINI = Mid$(Ordine, 1, 10)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 4, 6)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            frmRicezione.LblAzimuth.Text = d_Az
                            AzCalc = d_Az
                            Send_ORDINI = Mid$(Ordine, 1, 9)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "EP", "EX"
                If lun > 8 Then
                    d_El = Mid$(Ordine, 4, 6)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            frmRicezione.LblElevazione.Text = d_El
                            ElCalc = d_El
                            Send_ORDINI = Mid$(Ordine, 1, 9)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 4, 5)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            frmRicezione.LblElevazione.Text = d_El
                            ElCalc = d_El
                            Send_ORDINI = Mid$(Ordine, 1, 8)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "PP"
                If lun < 8 Then Exit Function
                d_Po = Mid$(Ordine, 5, 4)
                d_El = Mid$(Ordine, 4, 1)
                If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                    If IsNumeric(d_Po) Then
                        If d_Po >= 0 And d_Po < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 8)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case " A", "AA", "EA"
                AnalisiOrdini = True
                Send_ORDINI = Mid$(Ordine, 1, 2) & " "
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case "BM"
                AnalisiOrdini = True
                Send_ORDINI = Mid$(Ordine, 1, 4)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case " SAT", "ASAT", "ESAT", "SATK"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 39 Then
                        Send_ORDINI = Mid$(Ordine, 1, 9)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
        End Select

        Select Case s_analisi
            Case "IT"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El > 0 And d_El <= 15 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "ST"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 1 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "SS"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 5 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "XT"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 5 And d_El <= 35 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "YT"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 16 And d_El <= 85 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "RT"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 1 And d_El <= 60 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "AO", "EO"
                If lun > 12 Then
                    d_Az = Mid$(Ordine, 8, 6)
                    d_El = Mid$(Ordine, 7, 1)
                    If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                        If IsNumeric(d_Az) Then
                            If d_Az >= 0 And d_Az < 92 Then
                                Send_ORDINI = Mid$(Ordine, 1, 13)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 8, 5)
                    d_El = Mid$(Ordine, 7, 1)
                    If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                        If IsNumeric(d_Az) Then
                            If d_Az >= 0 And d_Az < 92 Then
                                Send_ORDINI = Mid$(Ordine, 1, 12)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "PO"
                If lun < 11 Then Exit Function
                d_Po = Mid$(Ordine, 8, 4)
                d_El = Mid$(Ordine, 7, 1)
                If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                    If IsNumeric(d_Po) Then
                        If d_Po >= 0 And d_Po < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 11)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "AOS", "EOS"
                If lun > 10 Then
                    d_Az = Mid$(Ordine, 6, 6)
                    d_El = Mid$(Ordine, 5, 1)
                    If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                        If IsNumeric(d_Az) Then
                            If d_Az >= 0 And d_Az < 92 Then
                                Send_ORDINI = Mid$(Ordine, 1, 11)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 6, 5)
                    d_El = Mid$(Ordine, 5, 1)
                    If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                        If IsNumeric(d_Az) Then
                            If d_Az >= 0 And d_Az < 92 Then
                                Send_ORDINI = Mid$(Ordine, 1, 10)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "POS"
                If lun < 9 Then Exit Function
                d_Po = Mid$(Ordine, 6, 4)
                d_El = Mid$(Ordine, 5, 1)
                If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                    If IsNumeric(d_Po) Then
                        If d_Po >= 0 And d_Po < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 9)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "SATS"
                If lun < 23 Then Ordine += " +00.0"
                lun = Len(Ordine)
                If lun > 26 Then
                    d_El = Mid$(Ordine, 6, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El <= 39 Then
                            d_Az = Mid$(Ordine, 9, 7)
                            d_El = Mid$(Ordine, 17, 6)
                            d_Po = Mid$(Ordine, 25, 4)
                            If IsNumeric(d_Az) And IsNumeric(d_El) Then
                                If d_Az >= 0 And d_Az < 360 Then
                                    If d_El >= 0 And d_El < 92 Then
                                        d_El = Mid$(Ordine, 24, 1)
                                        If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                                            If IsNumeric(d_Po) Then
                                                Send_ORDINI = Mid$(Ordine, 1, 28)
                                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                                            Else
                                                Exit Function
                                            End If
                                        Else
                                            Exit Function
                                        End If
                                    End If
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 6, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El <= 39 Then
                            d_Az = Mid$(Ordine, 9, 6)
                            d_El = Mid$(Ordine, 16, 4)
                            d_Po = Mid$(Ordine, 23, 4)
                            If IsNumeric(d_Az) And IsNumeric(d_El) Then
                                If d_Az >= 0 And d_Az < 360 Then
                                    If d_El >= 0 And d_El < 92 Then
                                        d_El = Mid$(Ordine, 22, 1)
                                        If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                                            If IsNumeric(d_Po) Then
                                                Send_ORDINI = Mid$(Ordine, 1, 26)
                                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                                            Else
                                                Exit Function
                                            End If
                                        Else
                                            Exit Function
                                        End If
                                    End If
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "SATKA"
                AnalisiOrdini = True
                Send_ORDINI = Mid$(Ordine, 1, 5)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case "MS"
                If lun > 21 Then
                    d_El = Mid$(Ordine, 4, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El <= 23 Then
                            d_El = Mid$(Ordine, 7, 2)
                            If IsNumeric(d_El) Then
                                If d_El = 0 Or d_El = 15 Or d_El = 30 Or d_El = 45 Then
                                    d_Az = Mid$(Ordine, 10, 7)
                                    d_El = Mid$(Ordine, 18, 6)
                                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                                        If d_Az >= 0 And d_Az < 360 Then
                                            If d_El >= 0 And d_El < 92 Then
                                                Send_ORDINI = Mid$(Ordine, 1, 23)
                                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                                            Else
                                                Exit Function
                                            End If
                                        Else
                                            Exit Function
                                        End If
                                    Else
                                        Exit Function
                                    End If
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 4, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El <= 23 Then
                            d_El = Mid$(Ordine, 7, 2)
                            If IsNumeric(d_El) Then
                                If d_El = 0 Or d_El = 15 Or d_El = 30 Or d_El = 45 Then
                                    d_Az = Mid$(Ordine, 10, 6)
                                    d_El = Mid$(Ordine, 17, 5)
                                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                                        If d_Az >= 0 And d_Az < 360 Then
                                            If d_El >= 0 And d_El < 92 Then
                                                Send_ORDINI = Mid$(Ordine, 1, 21)
                                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                                            Else
                                                Exit Function
                                            End If
                                        Else
                                            Exit Function
                                        End If
                                    Else
                                        Exit Function
                                    End If
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "MK"
                If lun < 8 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 23 Then
                        d_El = Mid$(Ordine, 7, 2)
                        If IsNumeric(d_El) Then
                            If d_El = 0 Or d_El = 15 Or d_El = 30 Or d_El = 45 Then
                                Send_ORDINI = Mid$(Ordine, 1, 8)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "MT"
                If lun < 6 Then Exit Function
                d_El = Mid$(Ordine, 4, 3)
                If IsNumeric(d_El) Then
                    If d_El >= 1 And d_El <= 999 Then
                        Send_ORDINI = Mid$(Ordine, 1, 6)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "STEP"
                If lun > 7 Then
                    d_El = Mid$(Ordine, 6, 3)
                    If IsNumeric(d_El) Then
                        If d_El >= 1 And d_El <= 999 Then
                            Send_ORDINI = Mid$(Ordine, 1, 8)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 6, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 1 And d_El <= 99 Then
                            Send_ORDINI = Mid$(Ordine, 1, 7)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "SPUP", "SPDW"
                If lun > 10 Then
                    d_El = Mid$(Ordine, 6, 6)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 11)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 6, 5)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 10)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "SPCW", "SPCC"
                If lun > 11 Then
                    d_Az = Mid$(Ordine, 6, 7)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            Send_ORDINI = Mid$(Ordine, 1, 12)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 6, 6)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            Send_ORDINI = Mid$(Ordine, 1, 11)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdini = True
                    Exit Function
                End If
            Case "SPPCW", "SPPCC"
                If lun < 11 Then Exit Function
                d_Po = Mid$(Ordine, 8, 4)
                d_El = Mid$(Ordine, 7, 1)
                If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                    If IsNumeric(d_Po) Then
                        If d_Po >= 0 And d_Po < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 11)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "T", "TIME"        'modificato 19/04
                AnalisiOrdini = True
                Ordine = "TIME " & NowUTC().ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                Exit Function
            Case "DATE"
                AnalisiOrdini = True
                Ordine = "DATE " & NowUTC().ToString("dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture)
                Exit Function
            Case "MRY", "MRN", "SLY", "SLN", "SSY", "SSN", "RES", "MKA"
                AnalisiOrdini = True
                Send_ORDINI = Mid$(Ordine, 1, 3)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case "R", "L"
                AnalisiOrdini = True
                Send_ORDINI = Mid$(Ordine, 1, 1)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case "ATTS"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "ATTA", "ATTE"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "ATRT"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 99 Then
                        SEND = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = SEND
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "ATRA", "ATRE"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 99 Then
                        SEND = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = SEND
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "ASDP", "ESDP"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 10 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
            Case "ALI", "ELI"
                If lun < 6 Then Exit Function
                d_El = Mid$(Ordine, 5, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 1 And d_El <= 6 Then
                        Send_ORDINI = Mid$(Ordine, 1, 6)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdini = True
                Exit Function
                frmRicezione.TxtOrdini.Text = SEND
                Exit Function
            Case "AT1E", "ET1E", "AT1D", "ET1D", "AS1E", "ES1E", "AS1D", "ES1D"     'Abilitano (E) o disabilitano (D) i motori 1
                AnalisiOrdini = True
                SEND = Mid$(Ordine, 1, 4)
                frmRicezione.TxtOrdini.Text = SEND
                Exit Function
            Case "AT2E", "ET2E", "AT2D", "ET2D", "AS2E", "ES2E", "AS2D", "ES2D"     'Abilitano (E) o disabilitano (D) i motori 2
                AnalisiOrdini = True
                SEND = Mid$(Ordine, 1, 4)
                frmRicezione.TxtOrdini.Text = SEND
                Exit Function
            Case "O", "P"
                i_DestOrdine = GRF
                AnalisiOrdini = True
                Exit Function
            Case "B"
                i_DestOrdine = GRF
                AnalisiOrdini = True
                CambioFreq(Mid$(Ordine, 3, 8))
            Exit Function
            Case "Z"
                i_DestOrdine = PCU
                AnalisiOrdini = True
                Exit Function
            Case Else
                Send_ORDINI = Ordine
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                AnalisiOrdini = True
                Exit Function
        End Select
        AnalisiOrdini = True
        Exit Function
esci:
    End Function
    Public Function Calcolo_Coordinate(Data1 As Date, mSec As Integer) As Boolean
        'Apportata modifica per avere un calcolo differenziato sia sulla data, che viene inviata sulla chiamata
        'sia su un valore 0 (invia) 1 (non inviare), per permettere un calcolo delle coordinate ad un tempo
        'qualsiasi senza interessare l'ACU
        Dim i As Double
        Dim tsincex As Double
        Dim pos As Vector = NewVector(), vel As Vector = NewVector()
        Dim ts1 As time_set
        Dim Timen As Double, s_program As String
        Dim EpochDate As Date
        Dim d_nonvalido
        Dim Port As String
        Dim tmpSQL As String
        Calcolo_Coordinate = False
        If AzInibhit And ElInibhit Then Exit Function       'esci se i due assi sono inibiti            'modifiche 14/02/2012

        Az_prov = 0
        El_Prov = 0
        AzCalc = 0
        ElCalc = 0
        data = Data1
        With ts1
            .yr = Year(data)
            .mo = Month(data)
            .dy = data.Day
            .hr = Hour(data)
            .mi = Minute(data)
            .se = Second(data)
            .hu = mSec
        End With
        '    Time_to_UTC ts
        Timen = Julian_Date(ts1)
        tsincex = (Timen - julian_epoch) * xmnpda
        If Algoritmo = 1 Then
            tmpSQL = "SELECT * FROM [Sat2line] order by Sat_Name"
            rs_Ricez = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
            rs_Ricez.FindFirst("Sat_Name ='" & s_Satellite & "'")
         If Not rs_Ricez.NoMatch Then
                ' 16/02/2018 Viene controllato che il valore di validità del TLE sia maggiore di zero, altrimenti non viene calcolato il valore di
                ' Program Track dopo un giorno e il sistema viene messo in Stand By
                If IsDate(rs_Ricez!EpochDate) And g_ValTLE > 0 Then
                    EpochDate = rs_Ricez!EpochDate
                    PTK = &H10         'Elementi validi
                    d_nonvalido = DateAdd("d", g_ValTLE - 1, EpochDate)
                    If CDate(d_nonvalido).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) = Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
                        PTK = &H20     'Elementi non validi < 24 ore
                        If Algoritmo = 1 Then
                            frmNorad.AggNorad
                        End If
                    ElseIf DateAdd("d", 1, d_nonvalido) <= Now Then
                        PTK = &H30 'elementi non validi > 24 ore
                        If Algoritmo = 1 Then
                            frmNorad.AggNorad
                        End If
                    End If
                End If
            End If

            If b_Sat Then

                If ideep = 0 Then
                    PosSGP4(tsincex, pos, vel)
          Else
                    PosSDP4(tsincex, pos, vel)
       End If
                Convert_Sat_State(pos, vel)
            geodetic.Valori(1) = LATSTA
                geodetic.Valori(2) = LONSTA
                geodetic.Valori(3) = h
                Calculate_Obs(pos, vel, Timen, obs_set)
            Az_prov = Format$((obs_set.Valori(1) * RadGradi) + AzOffset1 + AzOffset, "0.000")
                El_Prov = Format$((obs_set.Valori(2) * RadGradi) + ElOffset1 + ElOffset, "0.000")
                AzCalc = Format$((obs_set.Valori(1) * RadGradi) + AzOffset1, "0.000")
                ElCalc = Format$((obs_set.Valori(2) * RadGradi) + ElOffset1, "0.000")
                If Az_prov < 0 Then
                    Az_prov += 360
                ElseIf Az_prov > 360 Then
                    Az_prov -= 360
                End If
                If AzCalc < 0 Then
                    AzCalc += AzCalc
                ElseIf AzCalc > 360 Then
                    AzCalc -= 360
                End If
            End If
        ElseIf Algoritmo = 0 Or Algoritmo = 2 Then
            CalPar11Now()
            If IsNumeric(frmRicezione.LblAzimuth.Text) Then Az_prov = frmRicezione.LblAzimuth.Text
            If IsNumeric(frmRicezione.LblElevazione.Text) Then El_Prov = frmRicezione.LblElevazione.Text
        ElseIf Algoritmo = 3 Then
            TrackTable()
            If IsNumeric(frmRicezione.LblAzimuth.Text) Then Az_prov = frmRicezione.LblAzimuth.Text
            If IsNumeric(frmRicezione.LblElevazione.Text) Then El_Prov = frmRicezione.LblElevazione.Text
        ElseIf Algoritmo = 4 Then
            'per ora solo il sole
            If s_Satellite = "Sun" Then
                SolarAzEl(Timen, pos)
    ElseIf s_Satellite = "Moon" Then
                PosizioneLuna(Timen, pos)
    ElseIf s_Satellite = "Venus" Then
                Call PosizioneVenere(Timen, pos)
            ElseIf s_Satellite = "Cassiopeia" Then
                Call CalcolaCassiopeia(Timen, pos)
            End If
            Az_prov = Format$((pos.Valori(1)) + AzOffset1 + AzOffset, "0.000")
            El_Prov = Format$((pos.Valori(2)) + ElOffset1 + ElOffset, "0.000")
            AzCalc = Format$((pos.Valori(1)) + AzOffset1, "0.000")
            ElCalc = Format$((pos.Valori(2)) + ElOffset1, "0.000")


            If Az_prov < 0 Then
                Az_prov += 360
            ElseIf Az_prov > 360 Then
                Az_prov -= 360
            End If
            If AzCalc < 0 Then
                AzCalc += 360
            ElseIf AzCalc > 360 Then
                AzCalc -= AzCalc
            End If
        End If
        Calcolo_Coordinate = True
        Debug.WriteLine("Calcolo_Coordinate: Algoritmo=" & Algoritmo & " b_Sat=" & b_Sat & " s_Satellite=" & s_Satellite & " Az_prov=" & Az_prov & " El_Prov=" & El_Prov & " g_ElMin=" & g_ElMin & " i_ProgTrack=" & i_ProgTrack & " AzInibhit=" & AzInibhit & " ElInibhit=" & ElInibhit) ' TEMPORANEO: da rimuovere dopo la diagnosi
        If i_ProgTrack = 1 Or i_ProgTrack = 4 Then
            Az_prov = Format$(Az_prov + i_AzOffsetLock, "0.000")
            El_Prov = Format$(El_Prov + i_ElOffsetLock, "0.000")
            If Az_prov < 0 Then
                Az_prov += 360
            ElseIf Az_prov > 360 Then
                Az_prov -= Az_prov
            End If
            If IsNumeric(El_Prov) Then
                If Not AzInibhit Then frmRicezione.LblAzimuth.Text = Format$(Az_prov, "000.000")
                If Not ElInibhit Then frmRicezione.LblElevazione.Text = Format$(El_Prov, "00.000")
                If Not AzInibhit Then frmRicezione.LblAzimuthC.Text = Format$(AzCalc, "000.000")
                If Not ElInibhit Then frmRicezione.LblElevazioneC.Text = Format$(ElCalc, "00.000")
                If El_Prov < g_ElMin Then
                    Port = Canali(ACU).Port_IN
                    Port_Control(Port).Rec_State = PCR_STANDBY
                    frmRicezione.LblAzimuth.ForeColor = Color.Yellow
                    frmRicezione.LblElevazione.ForeColor = Color.Yellow
                    Exit Function
                Else
                    frmRicezione.LblAzimuth.ForeColor = Color.Black
                    frmRicezione.LblElevazione.ForeColor = Color.Black
                End If

                s_program = ""
                If Not AzInibhit And Not ElInibhit Then
                    s_program = " P " & Format$(Az_prov, "000.000") & " " &
                Format$(El_Prov, "00.000")
                ElseIf ElInibhit Then
                    s_program = "AP " & Format$(Az_prov, "000.000")
                ElseIf AzInibhit Then
                    s_program = "EP " & Format$(El_Prov, "00.000")
                End If
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
    End If
        Else
            frmRicezione.LblAzimuth.Text = " "
            frmRicezione.LblElevazione.Text = " "
            frmRicezione.LblAzimuthC.Text = Format$(AzCalc, "000.000")
            frmRicezione.LblElevazioneC.Text = Format$(ElCalc, "00.000")
        End If
    End Function
    Public Function Calcolo_Coordinate1(Data1 As Date) As Boolean
        'Apportata modifica per avere un calcolo differenziato sia sulla data, che viene inviata sulla chiamata
        'sia su un valore 0 (invia) 1 (non inviare), per permettere un calcolo delle coordinate ad un tempo
        'qualsiasi senza interessare l'ACU
        Dim tsincex As Double
        Dim pos As Vector = NewVector(), vel As Vector = NewVector()
        Dim ts1 As time_set
        Dim Timen As Double
        Calcolo_Coordinate1 = False
        Az_prov = 0
        El_Prov = 0
        data = Data1
        With ts1
            .yr = Year(data)
            .mo = Month(data)
            .dy = data.Day
            .hr = Hour(data)
            .mi = Minute(data)
            .se = Second(data)
            .hu = 0
        End With
        '    Time_to_UTC ts
        Timen = Julian_Date(ts1)
        tsincex = (Timen - julian_epoch) * xmnpda
        If Algoritmo = 1 Then
            If b_Sat Then
                If ideep = 0 Then
                    PosSGP4(tsincex, pos, vel)
                Else
                    PosSDP4(tsincex, pos, vel)
                End If
                Convert_Sat_State(pos, vel)
                geodetic.Valori(1) = LATSTA
                geodetic.Valori(2) = LONSTA
                geodetic.Valori(3) = h
                Calculate_Obs(pos, vel, Timen, obs_set)
                Az_prov = (obs_set.Valori(1) * RadGradi) + AzOffset1
                El_Prov = (obs_set.Valori(2) * RadGradi) + ElOffset1
                If Az_prov < 0 Then
                    Az_prov += 360
                ElseIf Az_prov > 360 Then
                    Az_prov -= 360
                End If
                frmRicezione.LblAzimuthC.Text = Format$(Az_prov, "0.000")
                frmRicezione.LblElevazioneC.Text = Format$(El_Prov, "0.000")
                AzCalc = Az_prov
                ElCalc = El_Prov
            End If
        ElseIf Algoritmo = 0 Or Algoritmo = 2 Then
            CalPar11Now()
            If IsNumeric(frmRicezione.LblAzimuth.Text) Then Az_prov = frmRicezione.LblAzimuthC.Text
            If IsNumeric(frmRicezione.LblElevazione.Text) Then El_Prov = frmRicezione.LblElevazioneC.Text
        ElseIf Algoritmo = 3 Then
            TrackTable()
            If IsNumeric(frmRicezione.LblAzimuth.Text) Then Az_prov = frmRicezione.LblAzimuthC.Text
            If IsNumeric(frmRicezione.LblElevazione.Text) Then El_Prov = frmRicezione.LblElevazioneC.Text
        ElseIf Algoritmo = 4 Then
            'per ora solo il sole
            SolarAzEl(Timen, pos)
            Az_prov = Format$((pos.Valori(1)) + AzOffset1 + AzOffset, "0.000")
            El_Prov = Format$((pos.Valori(2)) + ElOffset1 + ElOffset, "0.000")
            frmRicezione.LblAzimuthC.Text = Format$(Az_prov, "0.000")
            frmRicezione.LblElevazioneC.Text = Format$(El_Prov, "0.000")
            AzCalc = Format$((pos.Valori(1)) + AzOffset1, "0.000")
            ElCalc = Format$((pos.Valori(2)) + ElOffset1, "0.000")
        Else
            Exit Function
        End If
        Calcolo_Coordinate1 = True
    End Function
    Public Function ComparaDati() As Boolean

        Dim DataIni As Date
        ComparaDati = False
        If String.IsNullOrEmpty(d_DataInizioProg) Then Return False
        If Not DateTime.TryParse(d_DataInizioProg, DataIni) Then Return False
        DataIni = DateAdd("h", -i_Off, DataIni)
        Calcolo_Coordinate1(DataIni)
frmRicezione.LblAzimuth.Text = ""
        frmRicezione.LblElevazione.Text = ""
        'az1 = Az_prov

        'Attesa 0.5
        'g_Verso = False
        'DataIni = CDate(frmRicezione.mskDataIni) - CDate(Format$(i_Off / (24), "yyyy\-mm\-dd hh\:nn\:ss"))
        'Calcolo_Coordinate1 DataIni
        'az2 = Az_prov
        'IfSystem.Math.Math.Abs(az1 - az2) <= 1 Then
        '    ComparaDati = True
        'Else
        '    ComparaDati = False
        'End If
    End Function

    Public Function Calcolo_StepTrack() As Boolean
        '30/11/2024 Martino - Viene implementata la funzione dello Step Track da attivare nel caso l'ACU non riesca a calcolarla
        Dim rs_Acq As Object
        Dim s_program As String = "", i_lun As Integer
        '06/06/2022
        On Error Resume Next

        Select Case i_ConStato
            Case 0
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 1
                s_Lev3 = d_LevAve        'Memorizzo il livello del beacon
                b_StepTrack = False
                If s_Lev1 < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                'inizio dello step. L'antenna viene spostata di uno +step in azimuth
                'siamo in preset
                frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.TxtAz.Text) + i_AzStep, "000.000")
                frmRicezione.LblStepTrack.Text = "AZ"
                i_ConStato += 1
                s_program = "AP " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                i_StepTrack = 8
            Case 2
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 3
                b_StepTrack = True
                If Val(s_Lev1) < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                If d_LevAve >= s_Lev3 Then              'Livello maggiore vai avanti
                    'inizio dello step. L'antenna viene spostata di uno +step in azimuth
                    'siamo in preset
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                    i_StepTrack = 8
                Else
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) - i_AzStep / 2, "000.000")
                    i_ConStato += 1
                    i_StepTrack = 11
                End If
                s_Lev3 = d_LevAve         'Memorizzo il livello del beacon
                s_program = "AP " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " "
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                i_StepTrack = 11
            Case 4
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 5
                b_StepTrack = True
                If Val(s_Lev1) < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                If d_LevAve >= s_Lev3 Then             'Livello maggiore vai avanti
                    'inizio dello step. L'antenna viene spostata di uno +step in azimuth
                    'siamo in preset
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) - i_AzStep, "000.000")
                    i_StepTrack = 11
                Else
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep / 2, "000.000")
                    i_ConStato += 1
                    i_StepTrack = 8
                End If
                s_program = "AP " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                s_Lev3 = d_LevAve         'Memorizzo il livello del beacon
            Case 6
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 7
                b_StepTrack = True
                If Val(s_Lev1) < i_YT Then                      'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                'inizio dello step. L'antenna viene spostata di uno +step in elevazione UP
                'siamo in preset
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.TxtEl.Text) + i_ElStep, "00.000")
                frmRicezione.LblStepTrack.Text = "EL"
                i_ConStato += 1
                s_program = "EP " & Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                s_Lev3 = d_LevAve
                i_StepTrack = 6
            Case 8
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 9
                b_StepTrack = True
                If Val(s_Lev1) < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                If d_LevAve >= s_Lev3 Then             'Livello maggiore vai avanti
                    'inizio dello step. L'antenna viene spostata di uno +step in elevazione
                    'siamo in preset
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    i_StepTrack = 6
                Else
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.TxtEl.Text) - i_ElStep, "00.000")
                    frmRicezione.LblStepTrack.Text = "EL"
                    i_StepTrack = 8
                    i_ConStato += 1
                End If
                s_Lev3 = d_LevAve         'Memorizzo il livello del beacon
                s_program = "EP " & Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 10
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 11
                b_StepTrack = False
                If Val(s_Lev1) < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                If d_LevAve >= s_Lev3 Then             'Livello maggiore vai avanti
                    'inizio dello step. L'antenna viene spostata di uno +step in azimuth
                    'siamo in preset
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) - i_ElStep, "00.000")
                    frmRicezione.LblStepTrack.Text = "EL"
                    i_StepTrack = 8
                Else
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep / 2, "00.000")
                    i_ConStato += 1
                    frmRicezione.LblStepTrack.Text = "MM"
                    i_StepTrack = 12
                End If
                s_program = "EP " & Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(CStr(i), "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                s_Lev3 = d_LevAve         'Memorizzo il livello del beacon
            Case 12
                i_ConStato = 0
                frmRicezione.TmrSpiralAcquire.Enabled = False
        End Select
    End Function
    Public Sub PulisciAverage()
        Dim i As Integer
        For i = 1 To 10
            d_LevAv(i - 1) = &HFF
        Next
        '    d_LevAve = 0
    End Sub


End Module
