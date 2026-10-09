Imports System.Security.Cryptography
Imports System.IO

Module Calcolo11Par

    '       SMOD0698 - VERSIONE DEL 03/10/2005
    '
    '       PROGRAMMA DI CALCOLO DELLE ORBITE DI SATELLITI GEOSTAZIONARI
    '       EFFETTUATO CON IL METODO INTELSAT (11 PARAMETRI)
    '
    '      System.Math.CosTANTI DI CALCOLO
    '       -------------------
    '

    '
    '
    '
    '
    '       PARAMETRI DEL SATELLITE

    Dim LM0 As Double
    Dim LM1 As Double
    Dim LM2 As Double
    Dim LONC As Double
    Dim LONC1 As Double
    Dim LONS As Double
    Dim LONS1 As Double
    Dim LATC As Double
    Dim LATC1 As Double
    Dim LATS As Double
    Dim LATS1 As Double
    '
    Dim LM0A As Double
    Dim LM1A As Double
    Dim LM2A As Double
    Dim LONCA As Double
    Dim LONC1A As Double
    Dim LONSA As Double
    Dim LONS1A As Double
    Dim LATCA As Double
    Dim LATC1A As Double
    Dim LATSA As Double
    Dim LATS1A As Double

    Dim W As Double

    '       INIZIO LOOP
    '       -----------
    '
    Dim T As Double
    Dim X2 As Double
    '
    '       CALCOLO DELLA POSIZIONE DEL SATELLITE
    '       -------------------------------------
    '
    Dim RSAT As Double
    '
    '       CALCOLO DEGLI ANGOLI DI PUNTAMENTO DALLA STAZIONE
    '       -------------------------------------------------
    '
    Dim x3 As Double
    Dim DR As Double
    Dim DRX As Double
    Dim DRY As Double
    Dim DRZ As Double
    Dim DRNOR As Double
    Dim DRZEN As Double
    Dim az As Double
    Dim el As Double
    Dim LONSAT As Double
    Dim LATSAT As Double
    Dim LOSAT As Double
    Dim LASAT As Double
    '
    '       CORREZIONE DELLA RIFRAZIONE ATMOSFERICA
    '       ---------------------------------------
    '
    Dim X As Double

    Dim Time As Double, t_Data As time_set, t_Inizio As time_set
    Dim delta As Double, inizio As Double
    Dim d_nonvalido As Date
    Dim s_Linea As String

    '
    '       CALCOLO DELLE COORDINATE DI STAZIONE (GEOCENTRICHE)
    '       ---------------------------------------------------
    '
    Public Function CalPar11(Min As Integer) As Boolean

        Dim hFile As Double
        Dim Count As Double
        Dim Buffer As String
        Dim Tempo1 As Date, inizio As Double
        Dim d_partenza As Date, rs_11Par As Object
        Dim Fine As Double
        Dim delta As Double
        CalPar11 = True
        If Min = 0 Then Exit Function
        If Not b_Calcola Then Exit Function
        Const V As String = " "

        With NowPar11
            d_partenza = CDate(.EPOCDA + "/" + .EPOCMO + "/" + .EPOCYE + V + .EPOCHO + ":" + .EPOCMI + ":" + .EPOCSE)
            d_nonvalido = d_partenza.AddDays(g_Validi - 1)
            If d_nonvalido.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) = NowUTC().ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
                '               FrmMain.StbMain1.ProgressBar("messaggio") = "File data not valid 24 hour"
                MainForm.stpMessaggio.Text = "File data not valid 24 hour"
                FrmMain.TmrMsg.Enabled = True
                '        Exit Function
            ElseIf DateAdd("d", 1, d_nonvalido) <= Now Then
                MainForm.stpmessaggio.text = "File data not valid over 24 hours"
                FrmMain.TmrMsg.Enabled = True
                Exit Function
                'Aggiornamento non possibile. Vai nel modo di back up
            End If
            With t_Data
                .yr = Year(d_partenza)
                .mo = Month(d_partenza)
                .dy = d_partenza.Day
                .hr = Hour(d_partenza)
                .mi = Minute(d_partenza)
                .se = Second(d_partenza)
                .hu = 0
            End With
            LM0 = .LM0
            LM1 = .LM1
            LM2 = .LM2
            LONC = .LONC
            LONC1 = .LONC1
            LONS = .LONS
            LONS1 = .LONS1
            LATC = .LATC
            LATC1 = .LATC1
            LATS = .LATS
            LATS1 = .LATS1
        End With
        Tempo1 = Now.AddHours(-i_Off)


        With t_Inizio
            .yr = Year(Tempo1)
            .mo = Month(Tempo1)
            .dy = Tempo1.Day
            .hr = Hour(Tempo1)
            .mi = Minute(Tempo1)
            .se = Second(Tempo1)
            .hu = 0
        End With
        '    Time_to_UTC ts
        Time = Julian_Date(t_Inizio)
        delta = Julian_Date(t_Data)
        If Time < delta Then
            '    Response = MessageBox.Show("Date non congruenti. ", vbOKOnly)
            FrmMain.stpMessaggio.Text = "Date not valid"
            FrmMain.TmrMsg.Enabled = True
            Exit Function
        End If
        inizio = (Int(((Time - delta) * xmnpda) / Min)) * Min
        W = LM1 + 360.98564
        hFile = FreeFile()
        FileOpen(hFile, Orbita, OpenMode.Output)
        PrintLine(hFile, V)
        FileClose(hFile)
        X1 = LATSTA11 * k2
        r = xkmper / System.Math.Sqrt(1 - f * (2 - f) * (Math.Sin(X1) ^ 2))
        '       DISTANZA RADIALE DALL'ASSE DI ROTAZIONE
        ra = (r + h) * System.Math.Cos(X1)
        '       DISTANZA DAL PIANO EQUATORIALE
        RZ = (r * ((1 - f) ^ 2) + h) * System.Math.Sin(X1)
        '
        '
        '       INIZIO LOOP
        '       -----------
        '
        'LPRINT "ORA", "AZ", "EL", "LONSAT", "LATSAT"
        'LPRINT
        'For Count = 0 To 170 Step 1 / 60
        rs_11Par = dbOrbite.OpenRecordset("tmpEffemeridi", dbOpenDynaset)
        Fine = (inizio + (24 * 60)) - Min
        'For Count = Inizio To (170 * 60) Step minuti
        For Count = inizio To Fine Step Min
            '
            '       CALCOLO DEL TEMPO
            '       -----------------
            '
            T = Count / xmnpda
            X1 = W * T * k2
            X2 = 2 * X1
            '
            '       CALCOLO DELLA POSIZIONE DEL SATELLITE
            '       -------------------------------------
            '
            LONSAT = LM0 + LM1 * T + LM2 * T ^ 2 + ((LONC + (LONC1 * T)) * (System.Math.Cos(X1)))
            LONSAT += ((LONS + (LONS1 * T)) * (Math.Sin(X1))) + ((k / 2) * (((LATC ^ 2 - LATS ^ 2) * System.Math.Sin(X2))))
            LONSAT -= k * LATC * LATS * System.Math.Cos(X2)
            LATSAT = ((LATC + LATC1 * T) * System.Math.Cos(X1)) + ((LATS + LATS1 * T) * System.Math.Sin(X1))
            RSAT = rs * (1 - 2 * LM1 / (3 * (W - LM1))) * (1 + k * LONC * System.Math.Sin(X1) -
    k * LONS * System.Math.Cos(X1))
            '
            '       CALCOLO DEGLI ANGOLI DI PUNTAMENTO DALLA STAZIONE
            '       -------------------------------------------------
            '
            X1 = LATSAT * k2
            X2 = (LONSAT - LONSTA11) * k2
            x3 = LATSTA11 * k2
            DR = RSAT - r
            DRX = RSAT * System.Math.Cos(X1) * System.Math.Cos(X2) - ra
            DRY = RSAT * System.Math.Cos(X1) * System.Math.Sin(X2)
            DRZ = RSAT * System.Math.Sin(X1) - RZ
            DRNOR = -DRX * System.Math.Sin(x3) + DRZ * System.Math.Cos(x3)
            DRZEN = DRX * System.Math.Cos(x3) + DRZ * System.Math.Sin(x3)
            ' Print DR, DRX, DRY, DRZ, DRNOR, DRZEN
            X1 = DRY / DRNOR
            X2 = DRZEN / System.Math.Sqrt(DRNOR ^ 2 + DRY ^ 2)
            ' Print X1, X2
            az = System.Math.Atan(X1) * RadGradi
            If DRNOR <= 0 Then az += 180
            If az < 0 Then
                az += 360
            ElseIf az > 360 Then
                az -= 360
            End If
            el = System.Math.Atan(X2) * RadGradi
            '
            '       CORREZIONE DELLA RIFRAZIONE ATMOSFERICA
            '       ---------------------------------------
            '
            If el > 10.2 Then
                el += 0.01617 * System.Math.Cos(el * k2) / System.Math.Sin(el * k2)
            Else
                X = el + 0.589
                el += 0.58804392 - 0.17941557 * X + 0.29906946 * X ^ 2 * 0.1 _
    - 0.251874 * X ^ 3 * 0.01 + 0.82622101 * X ^ 4 * 0.0001
            End If
            az = Int(az * 1000 + 0.5) / 1000 + AzOffset
            el = Int(el * 1000 + 0.5) / 1000 + ElOffset
            LONSAT = Int(LONSAT * 10000 + 0.5) / 10000
            LATSAT = Int(LATSAT * 10000 + 0.5) / 10000
            data = d_partenza.AddDays(T)
            'LPRINT Count, AZ, EL, LONSAT, LATSAT
            Buffer = data.ToString("dd/MM/yyyy HH mm ss", System.Globalization.CultureInfo.InvariantCulture) + V + Str(az) + V + Str(el) + V + Str(LONSAT) + V + Str(LATSAT) + Chr(13) + Chr(10)
            FileOpen(hFile, Application.StartupPath & Orbita, OpenMode.Append)
            PrintLine(hFile, Buffer)
            FileClose(hFile)
            rs_11Par.AddNew
            rs_11Par!date = data.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            rs_11Par!Time = data.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture)
            rs_11Par!Azimuth = az
            rs_11Par!Elevation = el
            rs_11Par!Long = LONSAT
            rs_11Par!lat = LATSAT
            rs_11Par!source = "EFFEMERIDI"
            rs_11Par.Update
        Next Count
        rs_11Par.Close
    End Function
    '
    '       CALCOLO DELLE COORDINATE DI STAZIONE (GEOCENTRICHE)
    '       ---------------------------------------------------
    '
    Public Function CalPar11Now() As Boolean

        Dim d_partenza As Date
        CalPar11Now = True
        With NowPar11
            PTK = &H10         'Elementi validi
            d_partenza = CDate(.EPOCDA + "/" + .EPOCMO + "/" + .EPOCYE + " " + .EPOCHO + ":" + .EPOCMI + ":" + .EPOCSE)
            d_nonvalido = d_partenza.AddDays(g_Validi - 1)
            If d_nonvalido.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) = NowUTC().ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
                PTK = &H20     'Elementi non validi < 24 ore
            ElseIf DateAdd("d", 1, d_nonvalido) <= Now Then
                PTK = &H30 'elementi non validi > 24 ore
                b_Aggiorna = Aggiorna(AggAlgo, s_Satellite)
                If b_Aggiorna Then
                    b_Calcola = InitPar11()

                    frmRicezione.LblSatellite.BackColor = Color.Yellow
                    If b_Calcola Then PTK = &H10        'Aggiornamento possibile calcola la prossima volta
                    Exit Function
                End If
                'Aggiornamento non possibile. Vai nel modo di back up
            End If
            LM0 = .LM0
            LM1 = .LM1
            LM2 = .LM2
            LONC = .LONC
            LONC1 = .LONC1
            LONS = .LONS
            LONS1 = .LONS1
            LATC = .LATC
            LATC1 = .LATC1
            LATS = .LATS
            LATS1 = .LATS1
        End With
        W = LM1 + 360.98564
        X1 = LATSTA11 * k2
        r = xkmper / System.Math.Sqrt(1 - f * (2 - f) * (Math.Sin(X1) ^ 2))
        '       DISTANZA RADIALE DALL'ASSE DI ROTAZIONE
        ra = (r + h) * System.Math.Cos(X1)
        '       DISTANZA DAL PIANO EQUATORIALE
        RZ = (r * ((1 - f) ^ 2) + h) * System.Math.Sin(X1)
        '
        '
        '       INIZIO LOOP
        '       -----------
        '
        'LPRINT "ORA", "AZ", "EL", "LONSAT", "LATSAT"
        'LPRINT
        '
        '       CALCOLO DEL TEMPO
        '       -----------------
        '
        Dim oraNow As Date = Now.AddHours(-i_Off)
        T = (oraNow - d_partenza).TotalHours
        'T = (NowUTC() - d_partenza)
        X1 = W * T * k2
        X2 = 2 * X1
        '       CALCOLO DELLA POSIZIONE DEL SATELLITE
        '       -------------------------------------
        '
        LONSAT = LM0 + LM1 * T + LM2 * T ^ 2 + ((LONC + (LONC1 * T)) * (System.Math.Cos(X1)))
        LONSAT += ((LONS + (LONS1 * T)) * (Math.Sin(X1))) + ((k / 2) * (((LATC ^ 2 - LATS ^ 2) * System.Math.Sin(X2))))
        LONSAT -= k * LATC * LATS * System.Math.Cos(X2)
        LATSAT = ((LATC + LATC1 * T) * System.Math.Cos(X1)) + ((LATS + LATS1 * T) * System.Math.Sin(X1))
        RSAT = rs * (1 - 2 * LM1 / (3 * (W - LM1))) * (1 + k * LONC * System.Math.Sin(X1) -
    k * LONS * System.Math.Cos(X1))
        '
        '       CALCOLO DEGLI ANGOLI DI PUNTAMENTO DALLA STAZIONE
        '       -------------------------------------------------
        '
        X1 = LATSAT * k2
        X2 = (LONSAT - LONSTA11) * k2
        x3 = LATSTA11 * k2
        DR = RSAT - r
        DRX = RSAT * System.Math.Cos(X1) * System.Math.Cos(X2) - ra
        DRY = RSAT * System.Math.Cos(X1) * System.Math.Sin(X2)
        DRZ = RSAT * System.Math.Sin(X1) - RZ
        DRNOR = -DRX * System.Math.Sin(x3) + DRZ * System.Math.Cos(x3)
        DRZEN = DRX * System.Math.Cos(x3) + DRZ * System.Math.Sin(x3)
        ' Print DR, DRX, DRY, DRZ, DRNOR, DRZEN
        X1 = DRY / DRNOR
        X2 = DRZEN / System.Math.Sqrt(DRNOR ^ 2 + DRY ^ 2)
        ' Print X1, X2
        az = System.Math.Atan(X1) * RadGradi
        If DRNOR <= 0 Then az += 180
        el = System.Math.Atan(X2) * RadGradi
        '
        '       CORREZIONE DELLA RIFRAZIONE ATMOSFERICA
        '       ---------------------------------------
        '
        If el > 10.2 Then
            el += 0.01617 * System.Math.Cos(el * k2) / System.Math.Sin(el * k2)
        Else
            X = el + 0.589
            el += 0.58804392 - 0.17941557 * X + 0.29906946 * X ^ 2 * 0.1 _
    - 0.251874 * X ^ 3 * 0.01 + 0.82622101 * X ^ 4 * 0.0001
        End If
        AzCalc = Int(az * 1000 + 0.5) / 1000                    '15/10/2024
        az = Int(az * 1000 + 0.5) / 1000 + AzOffset
        '21/02/2024
        If az < 0 Then
            az = 360 + az
        ElseIf az > 360 Then
            az -= 360
        End If
        '15/10/2024
        If AzCalc < 0 Then
            AzCalc = 360 + AzCalc
        ElseIf AzCalc > 360 Then
            AzCalc -= 360
        End If
        ElCalc = Int(el * 1000 + 0.5) / 1000
        el = Int(el * 1000 + 0.5) / 1000 + ElOffset
        LONSAT = Int(LONSAT * 10000 + 0.5) / 10000
        LATSAT = Int(LATSAT * 10000 + 0.5) / 10000
        data = d_partenza.AddDays(T)
        If Not AzInibhit Then AzComandato = az
        If Not ElInibhit Then ElComandato = el
    End Function
    Public Function ObsNew(geodetic As Vector) As Boolean
        ObsNew = True
        '
        LONSAT = geodetic.Valori(2)
        LATSAT = geodetic.Valori(1)
        RSAT = geodetic.Valori(3)
        '
        '       CALCOLO DEGLI ANGOLI DI PUNTAMENTO DALLA STAZIONE
        '       -------------------------------------------------
        '
        X1 = LATSAT * k2
        X2 = (LONSAT - LONSTA11) * k2
        x3 = LATSTA11 * k2
        DR = RSAT - r
        DRX = RSAT * System.Math.Cos(X1) * System.Math.Cos(X2) - ra
        DRY = RSAT * System.Math.Cos(X1) * System.Math.Sin(X2)
        DRZ = RSAT * System.Math.Sin(X1) - RZ
        DRNOR = -DRX * System.Math.Sin(x3) + DRZ * System.Math.Cos(x3)
        DRZEN = DRX * System.Math.Cos(x3) + DRZ * System.Math.Sin(x3)
        ' Print DR, DRX, DRY, DRZ, DRNOR, DRZEN
        X1 = DRY / DRNOR
        X2 = DRZEN / System.Math.Sqrt(DRNOR ^ 2 + DRY ^ 2)
        ' Print X1, X2
        az = System.Math.Atan(X1) * RadGradi
        If DRNOR <= 0 Then az += 180
        el = System.Math.Atan(X2) * RadGradi
        '
        '       CORREZIONE DELLA RIFRAZIONE ATMOSFERICA
        '       ---------------------------------------
        '
        If el > 10.2 Then
            el += 0.01617 * System.Math.Cos(el * k2) / System.Math.Sin(el * k2)
        Else
            X = el + 0.589
            el += 0.58804392 - 0.17941557 * X + 0.29906946 * X ^ 2 * 0.1 _
    - 0.251874 * X ^ 3 * 0.01 + 0.82622101 * X ^ 4 * 0.0001
        End If
        az = Int(az * 1000 + 0.5) / 1000
        el = Int(el * 1000 + 0.5) / 1000
        LONSAT = Int(LONSAT * 10000 + 0.5) / 10000
        LATSAT = Int(LATSAT * 10000 + 0.5) / 10000
        AzCalc = az
        ElCalc = el
    End Function
    '
    '       CALCOLO DELLE COORDINATE DI STAZIONE (GEOCENTRICHE)
    '       ---------------------------------------------------
    '
    Public Function TabReg() As Boolean

        Dim Count As Double
        Dim Tempo1 As Date, inizio As Integer
        Dim d_partenza As Date, rs3 As Object
        Dim Fine As Double
        Dim delta As Double
        TabReg = False
        ' TMath.Math.AbsinReg not migrated
        DbExecute("Delete * from [tmpMemory]")
        With NowPar11
            d_partenza = CDate(.EPOCDA + "/" + .EPOCMO + "/" + .EPOCYE + " " + .EPOCHO + ":" + .EPOCMI + ":" + .EPOCSE)
            d_nonvalido = d_partenza.AddDays(g_Validi)
            If DateAdd("d", 1, d_nonvalido) < Now Then          ' Dati non validi
                Exit Function
            End If
            With t_Data
                .yr = Year(d_partenza)
                .mo = Month(d_partenza)
                .dy = d_partenza.Day
                .hr = Hour(d_partenza)
                .mi = Minute(d_partenza)
                .se = Second(d_partenza)
                .hu = 0
            End With
            LM0 = .LM0
            LM1 = .LM1
            LM2 = .LM2
            LONC = .LONC
            LONC1 = .LONC1
            LONS = .LONS
            LONS1 = .LONS1
            LATC = .LATC
            LATC1 = .LATC1
            LATS = .LATS
            LATS1 = .LATS1
        End With
        Tempo1 = Now.AddHours(-i_Off).AddDays(-1)
        With t_Inizio
            .yr = Year(Tempo1)
            .mo = Month(Tempo1)
            .dy = Tempo1.Day
            .hr = Hour(Tempo1)
            .mi = Minute(Tempo1) + 15
            .se = Second(Tempo1)
            .hu = 0
        End With
        '    Time_to_UTC ts
        Time = Julian_Date(t_Inizio)
        delta = Julian_Date(t_Data)
        If Time < delta Then
            Exit Function
        End If
        inizio = (Int(((Time - delta) * xmnpda) / Minuti)) * Minuti
        W = LM1 + 360.98564
        X1 = LATSTA11 * k2
        r = xkmper / System.Math.Sqrt(1 - f * (2 - f) * (Math.Sin(X1) ^ 2))
        '       DISTANZA RADIALE DALL'ASSE DI ROTAZIONE
        ra = (r + h) * System.Math.Cos(X1)
        '       DISTANZA DAL PIANO EQUATORIALE
        RZ = (r * ((1 - f) ^ 2) + h) * System.Math.Sin(X1)
        '
        '
        '       INIZIO LOOP
        '       -----------
        '
        'LPRINT "ORA", "AZ", "EL", "LONSAT", "LATSAT"
        'LPRINT
        'For Count = 0 To 170 Step 1 / 60
        rs3 = dbOrbite.OpenRecordset("tmpMemory", dbOpenDynaset)
        Fine = (inizio + (24 * 60)) - Minuti
        'For Count = Inizio To (170 * 60) Step minuti
        For Count = inizio To Fine Step Minuti
            '
            '       CALCOLO DEL TEMPO
            '       -----------------
            '
            T = Count / xmnpda
            X1 = W * T * k2
            X2 = 2 * X1
            '
            '       CALCOLO DELLA POSIZIONE DEL SATELLITE
            '       -------------------------------------
            '
            LONSAT = LM0 + LM1 * T + LM2 * T ^ 2 + ((LONC + (LONC1 * T)) * (System.Math.Cos(X1)))
            LONSAT += ((LONS + (LONS1 * T)) * (Math.Sin(X1))) + ((k / 2) * (((LATC ^ 2 - LATS ^ 2) * System.Math.Sin(X2))))
            LONSAT -= k * LATC * LATS * System.Math.Cos(X2)
            LATSAT = ((LATC + LATC1 * T) * System.Math.Cos(X1)) + ((LATS + LATS1 * T) * System.Math.Sin(X1))
            RSAT = rs * (1 - 2 * LM1 / (3 * (W - LM1))) * (1 + k * LONC * System.Math.Sin(X1) -
    k * LONS * System.Math.Cos(X1))
            '
            '       CALCOLO DEGLI ANGOLI DI PUNTAMENTO DALLA STAZIONE
            '       -------------------------------------------------
            '
            X1 = LATSAT * k2
            X2 = (LONSAT - LONSTA11) * k2
            x3 = LATSTA11 * k2
            DR = RSAT - r
            DRX = RSAT * System.Math.Cos(X1) * System.Math.Cos(X2) - ra
            DRY = RSAT * System.Math.Cos(X1) * System.Math.Sin(X2)
            DRZ = RSAT * System.Math.Sin(X1) - RZ
            DRNOR = -DRX * System.Math.Sin(x3) + DRZ * System.Math.Cos(x3)
            DRZEN = DRX * System.Math.Cos(x3) + DRZ * System.Math.Sin(x3)
            ' Print DR, DRX, DRY, DRZ, DRNOR, DRZEN
            X1 = DRY / DRNOR
            X2 = DRZEN / System.Math.Sqrt(DRNOR ^ 2 + DRY ^ 2)
            ' Print X1, X2
            az = System.Math.Atan(X1) * RadGradi
            If DRNOR <= 0 Then az += 180
            el = System.Math.Atan(X2) * RadGradi
            '
            '       CORREZIONE DELLA RIFRAZIONE ATMOSFERICA
            '       ---------------------------------------
            '
            If el > 10.2 Then
                el += 0.01617 * System.Math.Cos(el * k2) / System.Math.Sin(el * k2)
            Else
                X = el + 0.589
                el += 0.58804392 - 0.17941557 * X + 0.29906946 * X ^ 2 * 0.1 _
    - 0.251874 * X ^ 3 * 0.01 + 0.82622101 * X ^ 4 * 0.0001
            End If
            az = Int(az * 1000 + 0.5) / 1000 + AzOffset
            el = Int(el * 1000 + 0.5) / 1000 + ElOffset
            LONSAT = Int(LONSAT * 10000 + 0.5) / 10000
            LATSAT = Int(LATSAT * 10000 + 0.5) / 10000
            data = d_partenza.AddDays(T)
            rs3.AddNew
            rs3!date = data.ToString("dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture)
            rs3!Azimuth = az
            rs3!Elevation = el
            rs3.Update
        Next Count
        rs3.Close
        ' System.Math.Sin_Calcola not migrated
        ' TMath.Math.AbsinReg not migrated
        TabReg = True
    End Function
    Public Function Aggiorna(Alg As Integer, NomeSatellite As String) As Boolean
        Dim hFile As Long
        Dim Gr_file As Double
        Dim i_Scelta As Integer
        Dim sReadBuffer As String
        Dim rs_11Par As Object
        Dim tmpSQL As String, s_Linea As String
        Dim i As Integer, s_Partenza As String
        Dim origBuf As String
        Dim yPos As Integer, lm0Pos As Integer, loncPos As Integer, latcPos As Integer, epochPos As Integer
        Dim Long170 As Double, Lat170 As Double
        Dim Tempo1 As Date
        Dim d_partenza As Date, d_nonvalido As Date
        sReadBuffer = ""
        Aggiorna = False
        s_Satellite = NomeSatellite
        'On Error GoTo Err_Aggiorna
        tmpSQL = "SELECT * FROM [Satelliti] order by NomeSatellite"
        rs_11Par = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
        rs_11Par.FindFirst("NomeSatellite ='" & s_Satellite & "'")
        s_Linea = rs_11Par!FileAgg
        i = InStr(1, s_Linea, ":", 1)
        If i = 0 Then s_Linea = Application.StartupPath & "\" & s_Linea
        rs_11Par.Close
        If Dir(s_Linea) = "" Then Exit Function
        hFile = FreeFile()

        FileOpen(hFile, s_Linea, OpenMode.Input)
        Gr_file = LOF(hFile)

        If Gr_file < LUNGFILE Then
            FileClose(hFile)              'Stringa piu' corta - esci senza azzerare
            GoTo Err_Aggiorna
        End If
        i_Scelta = 0
        Do While Not EOF(hFile)
            sReadBuffer = InputString(hFile, Gr_file)
            i_Scelta = InStr(1, sReadBuffer, "YEAR", 1)
            If i_Scelta <> 0 Then Exit Do
        Loop
        ' occorre prelevare il buffer two-Line   abuf = sat_data(arg)
        '* Decode Card 1 *}
        If i_Scelta = 0 Then
            GoTo Err_Aggiorna
        Else
            ' Trim al "YEAR" e salva buffer completo — tutte le ricerche successive
            ' usano origBuf con posizioni esplicite invece del trimming progressivo
            ' che causava la perdita di "LATC" dal buffer.
            origBuf = Mid$(sReadBuffer, i_Scelta)
        End If
        rs_11Par = dbOrbite.OpenRecordset("Sat11Par", dbOpenDynaset)
        rs_11Par.FindFirst("Name ='" & s_Satellite & "'")
        If Not rs_11Par.NoMatch Then
            rs_11Par.Edit
        Else
            rs_11Par.AddNew
            rs_11Par!Name = s_Satellite
        End If

        ' YEAR data line (1 riga di intestazione da saltare)
        yPos = AvanzaLinee(origBuf, 1, 1)
        If yPos = 0 Then GoTo Err_Aggiorna
        sReadBuffer = Mid$(origBuf, yPos)

        If Alg = 0 Then
            rs_11Par!year = Mid$(sReadBuffer, 1, 4)
            rs_11Par!mounth = Mid$(sReadBuffer, 7, 2)
            rs_11Par!day = Mid$(sReadBuffer, 14, 2)
            rs_11Par!hour = Mid$(sReadBuffer, 19, 2)
            rs_11Par!Minute = Mid$(sReadBuffer, 25, 2)
            rs_11Par!second = Mid$(sReadBuffer, 33, 2)

            ' LM0 — cerca in origBuf dalla posizione dei dati anno
            lm0Pos = InStr(yPos, origBuf, "LM0", 1)
            If lm0Pos = 0 Then GoTo Err_Aggiorna
            lm0Pos = AvanzaLinee(origBuf, lm0Pos, 2)   ' salta header + unità
            If lm0Pos = 0 Then GoTo Err_Aggiorna
            sReadBuffer = Mid$(origBuf, lm0Pos)
            If Mid$(sReadBuffer, 4, 1) = "." Then
                rs_11Par!L0 = Val(Mid$(sReadBuffer, 1, 8))
                rs_11Par!L1 = Val(Mid$(sReadBuffer, 15, 7))
                rs_11Par!L2 = Val(Mid$(sReadBuffer, 31, 9))
            Else
                rs_11Par!L0 = Val(Mid$(sReadBuffer, 1, 7))
                rs_11Par!L1 = Val(Mid$(sReadBuffer, 14, 7))
                rs_11Par!L2 = Val(Mid$(sReadBuffer, 30, 9))
            End If

            ' LONC — cerca in origBuf dalla posizione LM0
            loncPos = InStr(lm0Pos, origBuf, "LONC", 1)
            If loncPos = 0 Then GoTo Err_Aggiorna
            loncPos = AvanzaLinee(origBuf, loncPos, 2)
            If loncPos = 0 Then GoTo Err_Aggiorna
            sReadBuffer = Mid$(origBuf, loncPos)
            If Mid$(sReadBuffer, 1, 1) = "-" Then
                rs_11Par!LONC = Val(Mid$(sReadBuffer, 1, 7))
                rs_11Par!LONC1 = Val(Mid$(sReadBuffer, 17, 7))
                rs_11Par!LONS = Val(Mid$(sReadBuffer, 33, 7))
                rs_11Par!LONS1 = Val(Mid$(sReadBuffer, 49, 7))
            Else
                rs_11Par!LONC = Val(Mid$(sReadBuffer, 2, 6))
                rs_11Par!LONC1 = Val(Mid$(sReadBuffer, 17, 7))
                rs_11Par!LONS = Val(Mid$(sReadBuffer, 33, 7))
                rs_11Par!LONS1 = Val(Mid$(sReadBuffer, 49, 7))
            End If

            ' LATC — cerca in origBuf dalla posizione LONC
            latcPos = InStr(loncPos, origBuf, "LATC", 1)
            If latcPos = 0 Then GoTo Err_Aggiorna
            latcPos = AvanzaLinee(origBuf, latcPos, 2)
            If latcPos = 0 Then GoTo Err_Aggiorna
            sReadBuffer = Mid$(origBuf, latcPos)
            If Mid$(sReadBuffer, 1, 1) = "-" Then
                rs_11Par!LATC = Val(Mid$(sReadBuffer, 1, 7))
                rs_11Par!LATC1 = Val(Mid$(sReadBuffer, 17, 7))
                rs_11Par!LATS = Val(Mid$(sReadBuffer, 33, 7))
                rs_11Par!LATS1 = Val(Mid$(sReadBuffer, 49, 7))
            Else
                rs_11Par!LATC = Val(Mid$(sReadBuffer, 2, 6))
                rs_11Par!LATC1 = Val(Mid$(sReadBuffer, 17, 7))
                rs_11Par!LATS = Val(Mid$(sReadBuffer, 33, 7))
                rs_11Par!LATS1 = Val(Mid$(sReadBuffer, 49, 7))
            End If

        ElseIf Alg = 2 Then
            i = 1
            rs_11Par!year = Mid$(Year(Now), 1, 2) + Mid$(sReadBuffer, i, 2)
            i += 7
            rs_11Par!mounth = Mid$(sReadBuffer, i, 2)
            i += 7
            rs_11Par!day = Mid$(sReadBuffer, i, 2)
            i += 7
            rs_11Par!hour = Mid$(sReadBuffer, i, 2)
            i += 7
            rs_11Par!Minute = Mid$(sReadBuffer, i, 2)
            i += 7
            rs_11Par!second = Mid$(sReadBuffer, i, 2)

            ' LM0
            lm0Pos = InStr(yPos, origBuf, "LM0", 1)
            If lm0Pos = 0 Then GoTo Err_Aggiorna
            lm0Pos = AvanzaLinee(origBuf, lm0Pos, 2)
            If lm0Pos = 0 Then GoTo Err_Aggiorna
            sReadBuffer = Mid$(origBuf, lm0Pos)
            If Mid$(sReadBuffer, 4, 1) = "." Then
                rs_11Par!L0 = Val(Mid$(sReadBuffer, 1, 11))
                rs_11Par!L1 = Val(Mid$(sReadBuffer, 15, 10))
                rs_11Par!L2 = Val(Mid$(sReadBuffer, 31, 10))
            Else
                rs_11Par!L0 = Val(Mid$(sReadBuffer, 1, 10))
                rs_11Par!L1 = Val(Mid$(sReadBuffer, 14, 10))
                rs_11Par!L2 = Val(Mid$(sReadBuffer, 30, 10))
            End If

            ' LONC
            loncPos = InStr(lm0Pos, origBuf, "LONC", 1)
            If loncPos = 0 Then GoTo Err_Aggiorna
            loncPos = AvanzaLinee(origBuf, loncPos, 2)
            If loncPos = 0 Then GoTo Err_Aggiorna
            sReadBuffer = Mid$(origBuf, loncPos)
            If Mid$(sReadBuffer, 1, 1) = "-" Then
                rs_11Par!LONC = Val(Mid$(sReadBuffer, 1, 10))
                rs_11Par!LONC1 = Val(Mid$(sReadBuffer, 17, 10))
                rs_11Par!LONS = Val(Mid$(sReadBuffer, 33, 10))
                rs_11Par!LONS1 = Val(Mid$(sReadBuffer, 49, 10))
            Else
                rs_11Par!LONC = Val(Mid$(sReadBuffer, 2, 9))
                rs_11Par!LONC1 = Val(Mid$(sReadBuffer, 17, 10))
                rs_11Par!LONS = Val(Mid$(sReadBuffer, 33, 10))
                rs_11Par!LONS1 = Val(Mid$(sReadBuffer, 49, 10))
            End If

            ' LATC
            latcPos = InStr(loncPos, origBuf, "LATC", 1)
            If latcPos = 0 Then GoTo Err_Aggiorna
            latcPos = AvanzaLinee(origBuf, latcPos, 2)
            If latcPos = 0 Then GoTo Err_Aggiorna
            sReadBuffer = Mid$(origBuf, latcPos)
            If Mid$(sReadBuffer, 1, 1) = "-" Then
                rs_11Par!LATC = Val(Mid$(sReadBuffer, 1, 10))
                rs_11Par!LATC1 = Val(Mid$(sReadBuffer, 17, 10))
                rs_11Par!LATS = Val(Mid$(sReadBuffer, 33, 10))
                rs_11Par!LATS1 = Val(Mid$(sReadBuffer, 49, 10))
            Else
                rs_11Par!LATC = Val(Mid$(sReadBuffer, 2, 9))
                rs_11Par!LATC1 = Val(Mid$(sReadBuffer, 17, 10))
                rs_11Par!LATS = Val(Mid$(sReadBuffer, 33, 10))
                rs_11Par!LATS1 = Val(Mid$(sReadBuffer, 49, 10))
            End If
        End If

        d_partenza = (CDate(rs_11Par!day + "/" + rs_11Par!mounth + "/" + rs_11Par!year + " " + rs_11Par!hour + ":" + rs_11Par!Minute + ":" + rs_11Par!second))
        d_nonvalido = d_partenza.AddDays(g_Validi - 1)
        If d_nonvalido.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) = NowUTC().ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
            FrmMain.stpMessaggio.Text = "File data not valid 24 hour"
            FrmMain.TmrMsg.Enabled = True
        ElseIf DateAdd("d", 1, d_nonvalido) <= Now Then
            FrmMain.stpMessaggio.Text = "File data not valid over 24 hours"
            FrmMain.TmrMsg.Enabled = True
            Exit Function
        End If

        ' EPOCH — cerca in origBuf dalla posizione LATC (entrambi Alg=0 e Alg=2)
        epochPos = InStr(If(latcPos > 0, latcPos, 1), origBuf, "EPOCH", 1)
        If epochPos = 0 Then GoTo Err_Aggiorna
        sReadBuffer = Mid$(origBuf, epochPos)
        Long170 = Val(Mid$(sReadBuffer, 11, 8))
        Lat170 = Val(Mid$(sReadBuffer, 32, 7))
        rs_11Par!Long170 = Long170
        rs_11Par!Lat170 = Lat170
        LM0A = rs_11Par!L0
        LM1A = rs_11Par!L1
        LM2A = rs_11Par!L2
        LONCA = rs_11Par!LONC
        LONC1A = rs_11Par!LONC1
        LONSA = rs_11Par!LONS
        LONS1A = rs_11Par!LONS1
        LATCA = rs_11Par!LATC
        LATC1A = rs_11Par!LATC1
        LATSA = rs_11Par!LATS
        LATS1A = rs_11Par!LATS1
        Tempo1 = DateAdd("h", 170, d_partenza)
        'Controllo della validita' dei dati tramite calcolo alle 170 ore
        CalPar170(d_partenza, Tempo1)
        If (Math.Abs(LOSAT - Long170) <= l_Tolleranza) And (Math.Abs(LASAT - Lat170) <= l_Tolleranza) And d_partenza <= Now Then
            Aggiorna = True
            rs_11Par.Update
        End If
        s_Partenza = rs_11Par!day + rs_11Par!mounth + Mid$(rs_11Par!year, 3, 2)
        rs_11Par.Close
        FileClose(hFile)
        '06/04/2017 La copia di back-up del file degli 11 Elementi non e' piu' necessaria
        '    s_Salva = Application.StartupPath & "\11El" & s_Partenza & ".Txt"
        '    If Dir(s_Salva) <> "" Then Kill s_Salva
        '    FileCopy s_Linea, s_Salva
        '    Kill s_Linea
        Exit Function
Err_Aggiorna:
        '    Response = MessageBox.Show("Errore nell'aggiornamento dati", vbOKOnly)
        FrmMain.stpMessaggio.Text = "No Find File"
        FrmMain.TmrMsg.Enabled = True
        FileClose(hFile)
        Exit Function
    End Function

    ' Avanza di n righe nel buffer a partire da startPos.
    ' Usa +2 per saltare il \n e il primo carattere della riga successiva
    ' (convenzione originale del codice VB6).
    ' Restituisce 0 se non trovato.
    Private Function AvanzaLinee(buf As String, startPos As Integer, n As Integer) As Integer
        Dim pos As Integer = startPos
        For j As Integer = 1 To n
            Dim nl As Integer = InStr(pos, buf, vbLf)
            If nl = 0 Then Return 0
            pos = nl + 2
        Next
        Return pos
    End Function

    '
    '       CALCOLO DELLE COORDINATE DI STAZIONE (GEOCENTRICHE)
    '       ---------------------------------------------------
    '
    Private Function CalPar170(Datapart As Date, DataInizio As Date) As Boolean
        Dim d_partenza As Date, Tempo1 As Date
        CalPar170 = True
        d_partenza = Datapart
        Tempo1 = DataInizio
        With t_Data
            .yr = Year(d_partenza)
            .mo = Month(d_partenza)
            .dy = d_partenza.Day
            .hr = Hour(d_partenza)
            .mi = Minute(d_partenza)
            .se = Second(d_partenza)
            .hu = 0
        End With
        With t_Inizio
            .yr = Year(Tempo1)
            .mo = Month(Tempo1)
            .dy = Tempo1.Day
            .hr = Hour(Tempo1)
            .mi = Minute(Tempo1)
            .se = Second(Tempo1)
            .hu = 0
        End With
        '    Time_to_UTC ts
        Time = Julian_Date(t_Inizio)
        delta = Julian_Date(t_Data)
        inizio = Int((Time - delta) * xmnpda)
        W = LM1A + 360.98564
        X1 = LATSTA11 * k2
        r = xkmper / System.Math.Sqrt(1 - f * (2 - f) * (Math.Sin(X1) ^ 2))
        '       DISTANZA RADIALE DALL'ASSE DI ROTAZIONE
        ra = (r + h) * System.Math.Cos(X1)
        '       DISTANZA DAL PIANO EQUATORIALE
        RZ = (r * ((1 - f) ^ 2) + h) * System.Math.Sin(X1)
        '
        '       CALCOLO DEL TEMPO
        '       -----------------
        '
        T = inizio / xmnpda
        X1 = W * T * k2
        X2 = 2 * X1
        '
        '       CALCOLO DELLA POSIZIONE DEL SATELLITE
        '       -------------------------------------
        '
        LOSAT = LM0A + LM1A * T + LM2A * T ^ 2 + ((LONCA + (LONC1A * T)) * (System.Math.Cos(X1)))
        LOSAT += ((LONSA + (LONS1A * T)) * (Math.Sin(X1))) + ((k / 2) * (((LATCA ^ 2 - LATSA ^ 2) * System.Math.Sin(X2))))
        LOSAT -= k * LATCA * LATSA * System.Math.Cos(X2)
        LASAT = ((LATCA + LATC1A * T) * System.Math.Cos(X1)) + ((LATSA + LATS1A * T) * System.Math.Sin(X1))
        RSAT = rs * (1 - 2 * LM1 / (3 * (W - LM1))) * (1 + k * LONCA * System.Math.Sin(X1) -
    k * LONS * System.Math.Cos(X1))
        LOSAT = Int(LOSAT * 10000 + 0.5) / 10000
        LASAT = Int(LASAT * 10000 + 0.5) / 10000

    End Function


    Public Sub REGSin()
        '
        ' Calcola i parametri della regressione da utilizzare per le interpolazioni nei 15 minuti di intervallo tra uno Step Track e l'altro

        '   RegressioneSystem.Math.Sinusoidale y = a *System.Math.Sin(omega * x + fase).
        Dim i&
        Dim OpAz!, SinFAz!, FAz!
        Dim SYSinAz!, SSinCosAz!, SYCosAz!, SSin2Az!, SCos2Az!
        Dim OpEl!, SinFEl!, FEl!
        Dim SSinCosEl!, SYCosEl!, SSin2El!, SCos2El!, SYSinEl!
        Dim rs3 As Object

        rs3 = dbOrbite.OpenRecordset("tmpMemory", dbOpenDynaset)
        rs3.MoveFirst
        ValorMedioAz = 0
        ValorMedioEL = 0
        For i = 1 To 96
            ValorMedioAz += rs3!Azimuth
            ValorMedioEL += rs3!Elevation
            rs3.MoveNext
        Next i
        ValorMedioAz \= 96
        ValorMedioEL \= ValorMedioEL
        '
        rs3.MovePrevious
        SYSinAz = 0
        SYCosAz = 0
        SSinCosAz = 0
        SSin2Az = 0
        SCos2Az = 0
        SYCosEl = 0
        SSinCosEl = 0
        SSin2El = 0
        SCos2El = 0
        rs3.Move(-(CN - 1))
        For i = 96 - CN + 1 To 96
            OpAz = omega * i
            SYSinAz += (rs3!Azimuth - ValorMedioAz) * System.Math.Sin(OpAz)
            SYCosAz += (rs3!Azimuth - ValorMedioAz) * System.Math.Cos(OpAz)
            SSinCosAz += System.Math.Sin(OpAz) * System.Math.Cos(OpAz)
            SSin2Az += System.Math.Sin(OpAz) * System.Math.Sin(OpAz)
            SCos2Az += System.Math.Cos(OpAz) * System.Math.Cos(OpAz)
            OpEl = omega * i
            SYSinEl += (rs3!Elevation - ValorMedioEL) * System.Math.Sin(OpEl)
            SYCosEl += (rs3!Elevation - ValorMedioEL) * System.Math.Cos(OpEl)
            SSinCosEl += System.Math.Sin(OpEl) * System.Math.Cos(OpEl)
            SSin2El += System.Math.Sin(OpEl) * System.Math.Sin(OpEl)
            SCos2El += System.Math.Cos(OpEl) * System.Math.Cos(OpEl)
            rs3.MoveNext
        Next i
        '
        rs3.MovePrevious
        faseAz = System.Math.Atan((SYSinAz * SSinCosAz - SYCosAz * SSin2Az) / (SYCosAz * SSinCosAz - SYSinAz * SCos2Az))
        SinFAz = System.Math.Sin(faseAz)
        FAz = System.Math.Cos(faseAz)
        aAz = (FAz * SYSinAz + SinFAz * SYCosAz) _
      / (FAz * FAz * SSin2Az + SinFAz * SinFAz * SCos2Az + 2.0! * SinFAz * FAz * SSinCosAz)
        '
        If aAz < 0! Then
            aAz = -aAz
            faseAz += PIGREC
        End If
        If faseAz < 0! Then faseAz += 2.0! * PIGREC

        faseEl = System.Math.Atan((SYSinEl * SSinCosEl - SYCosEl * SCos2El) / (SSinCosEl * SSinCosEl - SYSinEl * SCos2El))
        SinFEl = System.Math.Sin(faseEl)
        FEl = System.Math.Cos(faseEl)
        aEl = (FEl * SYSinEl + SinFEl * SYCosEl) _
      / (FEl * FEl * SSin2El + SinFEl * SinFEl * SCos2El + 2.0! * SinFEl * FEl * SSinCosEl)
        '
        If aEl < 0! Then
            aEl = -aEl
            faseEl += PIGREC
        End If
        If faseEl < 0! Then faseEl += 2.0! * PIGREC
       ' System.Math.Sin_Calcola not migrated
    End Sub



End Module

