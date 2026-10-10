Imports System.IO
Imports System.Data.SQLite
Imports System.Windows.Forms

' ============================================================
' frmEffemeridi.vb — convertito da frmEffemeridi.frm (VB6)
' ============================================================

Partial Public Class FrmEffemeridi
    Inherits System.Windows.Forms.Form

    ' -------------------------------------------------------
    ' Variabili locali
    ' -------------------------------------------------------
    Private i_Sec As Integer
    Private i_mill As Integer
    Private alleore As Date
    Private Data1 As Date
    Private seceffemeridi As Double

    Public Sub New()
        InitializeComponent()
    End Sub

    ' -------------------------------------------------------
    ' Form Load
    ' -------------------------------------------------------
    Private Sub FrmEffemeridi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Lo svuotamento qui è voluto: senza, riaprendo Ephemeris su un
        ' satellite diverso si vedrebbero ancora i dati vecchi del
        ' satellite precedente (confermato: GEO seguito da POLAR mostrava
        ' ancora i dati di GEO). Scoped a source='EFFEMERIDI' (più le righe
        ' legacy senza source): non deve cancellare un Acquire in corso.
        DbExecute("DELETE FROM tmpEffemeridi WHERE source = 'EFFEMERIDI' OR source IS NULL")
        MnuCalcola.Enabled = True
        MnuInviaTabella.Enabled = False
        MnuSalva.Enabled = True
        MnuStampa.Enabled = True
        TxtOre.Text = "24"
        LblAntenna.Text = Antenna
    End Sub

    ' -------------------------------------------------------
    ' Form Activate
    ' -------------------------------------------------------
    Private Sub FrmEffemeridi_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Select Case i_Ephemeris
            Case 0 : LblSatellite.Text = "Alfa Cassiopiae (Schedar)"
            Case 1 : LblSatellite.Text = "Moon"
            Case 2 : LblSatellite.Text = s_Satellite
            Case 3 : LblSatellite.Text = "Sun"
            Case 4 : LblSatellite.Text = "Venus"
        End Select
        DbExecute("DELETE FROM tmpEffemeridi WHERE source = 'EFFEMERIDI' OR source IS NULL")
        MskDataIni.Text = NowUTC().ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
        TxtMinuti.Focus()
    End Sub

    ' -------------------------------------------------------
    ' Bottone Calcola (era CmdCalcola)
    ' -------------------------------------------------------
    Private Sub MnuCalcola_Click(sender As Object, e As EventArgs) Handles MnuCalcola.Click
        If Not IsNumeric(TxtMinuti.Text) Then Exit Sub

        Dim Minuti As Double = CDbl(TxtMinuti.Text)
        TmrEffemeridi.Interval = 60000

        If Minuti >= 60 Then
            TxtMinuti.Text = "60"
            Minuti = 60
        End If

        i_mill = CInt((Minuti * 100 - CInt(Minuti * 100)) * 1000)
        i_Sec = CInt((Minuti - CInt(Minuti)) * 100)
        If i_Sec > 59 Then TxtMinuti.Text = CStr(CInt(Minuti) + (i_Sec / 100) + i_mill / 100000)

        MnuInviaTabella.Enabled = True

        DbExecute("DELETE FROM tmpEffemeridi WHERE source = 'EFFEMERIDI' OR source IS NULL")
        Me.Refresh()

        Dim dataIni As Date
        If Not DateTime.TryParseExact(MskDataIni.Text.Trim().Replace(".", ":"), "dd/MM/yyyy HH:mm:ss",
                                      System.Globalization.CultureInfo.InvariantCulture,
                                      System.Globalization.DateTimeStyles.None, dataIni) Then
            dataIni = NowUTC()
            MskDataIni.Text = dataIni.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
        End If
        If Minuti >= 0.001 Then
            seceffemeridi = CDbl(TxtOre.Text) * 3600
        Else
            seceffemeridi = 5
        End If
        Data1 = dataIni.AddSeconds(seceffemeridi)
        alleore = Data1

        Select Case i_Ephemeris
            Case 0 : EffCASSIOPEA(Minuti)
            Case 1 : EffMOON(Minuti)
            Case 2
                If Algoritmo = 1 Then
                    EffNORAD(Minuti)
                ElseIf Algoritmo = 0 Or Algoritmo = 2 Then
                    CalPar11(Minuti)
                End If
            Case 3 : EffSUN(Minuti)
            Case 4 : EffPLANETS(Minuti)
        End Select

        AggiornaGriglia()
        Me.Refresh()
    End Sub

    ' -------------------------------------------------------
    ' Popola dgvEffemeridi da tmpEffemeridi
    ' -------------------------------------------------------
    Private Sub AggiornaGriglia()
        Dim dt As New System.Data.DataTable()
        dt.Columns.Add("date")
        dt.Columns.Add("time")
        dt.Columns.Add("azimuth")
        dt.Columns.Add("elevation")
        dt.Columns.Add("long")
        dt.Columns.Add("lat")
        Try
            Dim conn As SQLiteConnection = CType(dbOrbite, DAODatabase).Connection
            Using Cmd As New SQLiteCommand("SELECT date, time, azimuth, elevation, long, lat FROM tmpEffemeridi WHERE source = 'EFFEMERIDI' ORDER BY date, time", conn)
                Using dr = Cmd.ExecuteReader()
                    Dim ic As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture
                    Do While dr.Read()
                        dt.Rows.Add(
                            CDate(dr(0)).ToString("dd/MM/yyyy"),
                            dr(1).ToString(),
                            CDbl(dr(2)).ToString("000.000", ic),
                            CDbl(dr(3)).ToString("00.000", ic),
                            CDbl(dr(4)).ToString("0.000", ic),
                            CDbl(dr(5)).ToString("0.000", ic))
                    Loop
                End Using
            End Using
        Catch ex As Exception
            WriteError(0, "AggiornaGriglia: " & ex.Message)
        End Try
        If dgvEffemeridi.AutoGenerateColumns Then
            dgvEffemeridi.AutoGenerateColumns = False
            dgvEffemeridi.Columns("Date").DataPropertyName = "date"
            dgvEffemeridi.Columns("Time").DataPropertyName = "time"
            dgvEffemeridi.Columns("Azimuth").DataPropertyName = "azimuth"
            dgvEffemeridi.Columns("Elevation").DataPropertyName = "elevation"
            dgvEffemeridi.Columns("Long").DataPropertyName = "long"
            dgvEffemeridi.Columns("Lat").DataPropertyName = "lat"
        End If
        dgvEffemeridi.DataSource = dt
    End Sub

    ' -------------------------------------------------------
    ' Menu — gestione voci (era MnuEffemeridi_Click array)
    ' -------------------------------------------------------
    Private Sub MnuChiudi_Click(sender As Object, e As EventArgs) Handles MnuChiudi.Click
        TmrTable.Enabled = False
        b_Parametri = False
        TmrEffemeridi.Enabled = False
        Me.Close()
    End Sub

    Private Sub MnuInviaTabella_Click(sender As Object, e As EventArgs) Handles MnuInviaTabella.Click
        b_Parametri = True
        b_Ricezione(ACU) = False
        b_int(ACU) = True
        SEND_ACU = CheckSum("09MKA")
        InvioACU(SEND_ACU)
        TmrTable.Enabled = True
        LastMemory = 0
    End Sub

    Private Sub MnuSalva_Click(sender As Object, e As EventArgs) Handles MnuSalva.Click
        Salva()
    End Sub

    Private Sub MnuStampa_Click(sender As Object, e As EventArgs) Handles MnuStampa.Click
        Stampa()
    End Sub

    ' -------------------------------------------------------
    ' Timer Effemeridi — ricalcolo periodico
    ' -------------------------------------------------------
    Private Sub TmrEffemeridi_Tick(sender As Object, e As EventArgs) Handles TmrEffemeridi.Tick
        DbExecute("DELETE FROM tmpEffemeridi WHERE source = 'EFFEMERIDI' OR source IS NULL")
        Select Case i_Ephemeris
            Case 0 : EffCASSIOPEA(CDbl(TxtMinuti.Text))
            Case 1 : EffMOON(CDbl(TxtMinuti.Text))
            Case 2
                If Algoritmo = 1 Then
                    EffNORAD(CDbl(TxtMinuti.Text))
                ElseIf Algoritmo = 0 Or Algoritmo = 2 Then
                    CalPar11(CDbl(TxtMinuti.Text))
                End If
            Case 3 : EffSUN(CDbl(TxtMinuti.Text))
            Case 4 : EffPLANETS(CDbl(TxtMinuti.Text))
        End Select
        AggiornaGriglia()
        Me.Refresh()
        TmrEffemeridi.Interval = 60000
    End Sub

    ' -------------------------------------------------------
    ' Timer Tabella — invio progressivo tabella all'ACU
    ' -------------------------------------------------------
    Public Sub TmrTable_Tick(sender As Object, e As EventArgs) Handles TmrTable.Tick
        If b_errore Then
            LastMemory += 1
            Exit Sub
        End If
        Cursor = Cursors.WaitCursor
        b_errore = False
        b_int(ACU) = True
        b_Ricezione(ACU) = False
        SEND_ACU = InviaTabella()
        If Len(SEND_ACU) = 0 Then
            b_Parametri = False
            TmrTable.Enabled = False
            i_Cont = 0
            Cursor = Cursors.Default
            Exit Sub
        End If
        b_int(ACU) = True
        b_Ricezione(ACU) = False
        InvioACU(SEND_ACU)
        LastMemory += 1
    End Sub

    ' -------------------------------------------------------
    ' Calcolo effemeridi NORAD (SGP4/SDP4)
    ' -------------------------------------------------------
    Private Function EffNORAD(Minuti As Double) As Boolean
        Dim az As Double, el As Double
        Dim tsince As Double, tsince1 As Double, tsince2 As Double
        Dim pos As Vector = NewVector()
        Dim vel As Vector = NewVector()
        Dim timec As Double, minu As Integer
        EffNORAD = False
        Try
            If Minuti = 15 Then
                i_mill = 0
                i_Sec = 0
            Else
                i_mill = CInt((Minuti * 100 - CInt(Minuti * 100)) * 1000)
                i_Sec = CInt((Minuti - CInt(Minuti)) * 100)
            End If

            Dim dataIni As Date
            If Not DateTime.TryParseExact(MskDataIni.Text.Replace(".", ":"), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, dataIni) Then
                MskDataIni.Text = NowUTC().ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                MessageBox.Show("Invalid date. Enter it in the format dd/MM/yyyy HH:mm:ss", "EffNORAD", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            If Minuti >= 0.001 Then
                seceffemeridi = CDbl(TxtOre.Text) * 3600
            Else
                seceffemeridi = 5
            End If
            Data1 = dataIni.AddSeconds(seceffemeridi)

            With ts
                .yr = Data1.Year : .mo = Data1.Month : .dy = Data1.Day
                .hr = Data1.Hour : .mi = Data1.Minute : .se = Data1.Second : .hu = 0
            End With
            tsince2 = Julian_Date(ts)

            minu = If(Minuti = 15, 0, dataIni.Minute)
            With ts
                .yr = dataIni.Year : .mo = dataIni.Month : .dy = dataIni.Day
                .hr = dataIni.Hour : .mi = minu : .se = dataIni.Second : .hu = 0
            End With

            Dim IX As Double = CDbl(Fraction_of_Day(0, CInt(Minuti), i_Sec, i_mill + 0.55))
            timec = Julian_Date(ts)
            tsince1 = (timec - julian_epoch) * xmnpda
            tsince2 = ((tsince2 - julian_epoch) * xmnpda) - IX
            Minuti = CInt(Minuti) + (i_Sec / 60 * 100) / 100 + (i_mill / 60000)

            If ideep <> 0 Then PosSDP4(tsince, pos, vel)

            Dim connN As SQLiteConnection = CType(dbOrbite, DAODatabase).Connection
            Dim azOff As Double = If(IsNumeric(TxtAzOffset.Text), CDbl(TxtAzOffset.Text), 0)
            Dim elOff As Double = If(IsNumeric(TxtElOffset.Text), CDbl(TxtElOffset.Text), 0)
            Using tx As SQLiteTransaction = connN.BeginTransaction()
                Using Cmd As New SQLiteCommand("INSERT INTO tmpEffemeridi (date, time, azimuth, elevation, long, lat, intervallo, source) VALUES (@d,@t,@az,@el,@lo,@la,@in,'EFFEMERIDI')", connN)
                    Cmd.Transaction = tx
                    For tsince = tsince1 To tsince2 Step Minuti
                        If ideep = 0 Then
                            PosSGP4(tsince, pos, vel)
                        Else
                            PosSDP4(tsince, pos, vel)
                        End If
                        Convert_Sat_State(pos, vel)
                        Calculate_Obs(pos, vel, timec, obs_set)

                        az = (obs_set.Valori(1) * RadGradi)
                        If az < 0 Then az += 360
                        If az > 360 Then az -= 360
                        el = (obs_set.Valori(2) * RadGradi)

                        Calculate_LatLonAlt(pos, timec, geodetic)

                        If el >= 0 Then
                            Dim ts2 As time_set = Time_of_Effemeridi(timec, False, 3)
                            Cmd.Parameters.Clear()
                            Cmd.Parameters.AddWithValue("@d", Calendar_Date(timec, 0))
                            Cmd.Parameters.AddWithValue("@t", String.Format("{0:00}:{1:00}:{2:00}", ts2.hr, ts2.mi, ts2.se))
                            Dim ic As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture
                            Cmd.Parameters.AddWithValue("@az", (az + AzOffset1 + azOff).ToString("000.000", ic))
                            Cmd.Parameters.AddWithValue("@el", (el + ElOffset1 + elOff).ToString("00.000", ic))
                            Cmd.Parameters.AddWithValue("@lo", (geodetic.Valori(2) * RadGradi).ToString("0.000", ic))
                            Cmd.Parameters.AddWithValue("@la", (geodetic.Valori(1) * RadGradi).ToString("0.000", ic))
                            Cmd.Parameters.AddWithValue("@in", TxtMinuti.Text)
                            Cmd.ExecuteNonQuery()
                        End If
                        timec += CDbl(IX)
                    Next
                End Using
                tx.Commit()
            End Using

            EffNORAD = True

        Catch ex As Exception
            Dim main As FrmMain = TryCast(Me.MdiParent, FrmMain)
            If main IsNot Nothing Then
                main.MessaggioStatus = "Date not valid"
                main.TmrMsg.Enabled = True
            End If
            WriteError(0, "EffNORAD: " & ex.Message)
        End Try
    End Function

    ' -------------------------------------------------------
    ' Calcolo effemeridi CASSIOPEA
    ' -------------------------------------------------------
    Private Function EffCASSIOPEA(Minuti As Double) As Boolean

        If Minuti = 0 Then Return False
        EffCASSIOPEA = False
        Try
            i_Sec = CInt((Minuti - CInt(Minuti)) * 100)
            Dim dataIni As Date = DateTime.ParseExact(MskDataIni.Text.Replace(".", ":"), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            seceffemeridi = CDbl(TxtOre.Text) * 3600
            Data1 = dataIni.AddSeconds(seceffemeridi)
            With ts
                .yr = Data1.Year : .mo = Data1.Month : .dy = Data1.Day
                .hr = Data1.Hour : .mi = CInt(Minuti) : .se = i_Sec : .hu = 0
            End With
            Dim tsince1 As Double = Julian_Date(ts)
            Dim minu As Integer = If(Minuti = 15, CInt(Minuti), dataIni.Minute)
            With ts
                .yr = dataIni.Year : .mo = dataIni.Month : .dy = dataIni.Day
                .hr = dataIni.Hour : .mi = minu : .se = 0 : .hu = 0
            End With
            Dim i As Double = Fraction_of_Day(0, CInt(Minuti), i_Sec, 0)
            Dim timec As Double = Julian_Date(ts)
            tsince1 = ((tsince1 - julian_epoch) * xmnpda) - i
            Dim tsince As Double = (timec - julian_epoch) * xmnpda
            Minuti = CInt(Minuti) + (i_Sec / 60 * 100) / 100

            Using Cmd As New SQLiteCommand("INSERT INTO tmpEffemeridi (date, time, azimuth, elevation, long, lat, intervallo, source) VALUES (@d,@t,@az,@el,@lo,@la,@in,'EFFEMERIDI')", CType(dbOrbite, DAODatabase).Connection)
                Dim Count As Double = tsince
                Do While Count <= tsince1
                    Dim pos As New Vector()
                    CalcolaCassiopeia(timec, pos)
                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@d", Calendar_Date(timec, 0))
                    Cmd.Parameters.AddWithValue("@t", Format(Time_of_Day(timec, 1, 0), "HH:mm:ss"))
                    Dim icC As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture
                    Cmd.Parameters.AddWithValue("@az", pos.Valori(1).ToString("000.000", icC))
                    Cmd.Parameters.AddWithValue("@el", pos.Valori(2).ToString("00.000", icC))
                    Cmd.Parameters.AddWithValue("@lo", (pos.Valori(1) * RadGradi).ToString("0.000", icC))
                    Cmd.Parameters.AddWithValue("@la", (pos.Valori(2) * RadGradi).ToString("0.000", icC))
                    Cmd.Parameters.AddWithValue("@in", TxtMinuti.Text)
                    Cmd.ExecuteNonQuery()
                    timec += i
                    Count += Minuti
                Loop
            End Using
            EffCASSIOPEA = True
        Catch ex As Exception
            WriteError(0, "EffCASSIOPEA: " & ex.Message)
        End Try
    End Function

    ' -------------------------------------------------------
    ' Calcolo effemeridi LUNA
    ' -------------------------------------------------------
    Private Function EffMOON(Minuti As Double) As Boolean
        If Minuti = 0 Then Return False
        EffMOON = False
        Try
            i_Sec = CInt((Minuti - CInt(Minuti)) * 100)
            Dim dataIni As Date = DateTime.ParseExact(MskDataIni.Text.Replace(".", ":"), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            seceffemeridi = CDbl(TxtOre.Text) * 3600
            Data1 = dataIni.AddSeconds(seceffemeridi)
            With ts
                .yr = Data1.Year : .mo = Data1.Month : .dy = Data1.Day
                .hr = Data1.Hour : .mi = CInt(Minuti) : .se = i_Sec : .hu = 0
            End With
            Dim tsince1 As Double = Julian_Date(ts)
            Dim minu As Integer = If(Minuti = 15, CInt(Minuti), dataIni.Minute)
            With ts
                .yr = dataIni.Year : .mo = dataIni.Month : .dy = dataIni.Day
                .hr = dataIni.Hour : .mi = minu : .se = 0 : .hu = 0
            End With
            Dim IX As Double = Fraction_of_Day(0, CInt(Minuti), i_Sec, 0)
            Dim timec As Double = Julian_Date(ts)
            tsince1 = ((tsince1 - julian_epoch) * xmnpda) - IX
            Dim tsince As Double = (timec - julian_epoch) * xmnpda
            Minuti = CInt(Minuti) + (i_Sec / 60 * 100) / 100

            Using Cmd As New SQLiteCommand("INSERT INTO tmpEffemeridi (date, time, azimuth, elevation, long, lat, intervallo, source) VALUES (@d,@t,@az,@el,@lo,@la,@in,'EFFEMERIDI')", CType(dbOrbite, DAODatabase).Connection)
                Dim Count As Double = tsince
                Do While Count <= tsince1
                    Dim pos As New Vector()
                    PosizioneLuna(timec, pos)
                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@d", Calendar_Date(timec, 0))
                    Cmd.Parameters.AddWithValue("@t", Format(Time_of_Day(timec, 1, 0), "HH:mm:ss"))
                    Dim icM As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture
                    Cmd.Parameters.AddWithValue("@az", pos.Valori(1).ToString("000.000", icM))
                    Cmd.Parameters.AddWithValue("@el", pos.Valori(2).ToString("00.000", icM))
                    Cmd.Parameters.AddWithValue("@lo", (pos.Valori(3) * RadGradi).ToString("0.000", icM))
                    Cmd.Parameters.AddWithValue("@la", (pos.Valori(4) * RadGradi).ToString("0.000", icM))
                    Cmd.Parameters.AddWithValue("@in", TxtMinuti.Text)
                    Cmd.ExecuteNonQuery()
                    timec += IX
                    Count += Minuti
                Loop
            End Using
            EffMOON = True
        Catch ex As Exception
            WriteError(0, "EffMOON: " & ex.Message)
        End Try
    End Function

    ' -------------------------------------------------------
    ' Calcolo effemeridi SOLE
    ' -------------------------------------------------------
    Private Function EffSUN(Minuti As Double) As Boolean
        If Minuti = 0 Then Return False
        EffSUN = False
        Try
            i_Sec = CInt((Minuti - CInt(Minuti)) * 100)
            Dim dataIni As Date = DateTime.ParseExact(MskDataIni.Text.Replace(".", ":"), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            seceffemeridi = CDbl(TxtOre.Text) * 3600
            Data1 = dataIni.AddSeconds(seceffemeridi)
            With ts
                .yr = Data1.Year : .mo = Data1.Month : .dy = Data1.Day
                .hr = Data1.Hour : .mi = CInt(Minuti) : .se = i_Sec : .hu = 0
            End With
            Dim tsince1 As Double = Julian_Date(ts)
            Dim minu As Integer = If(Minuti = 15, CInt(Minuti), dataIni.Minute)
            With ts
                .yr = dataIni.Year : .mo = dataIni.Month : .dy = dataIni.Day
                .hr = dataIni.Hour : .mi = minu : .se = 0 : .hu = 0
            End With
            Dim i As Double = Fraction_of_Day(0, CInt(Minuti), i_Sec, 0)
            Dim timec As Double = Julian_Date(ts)
            tsince1 = ((tsince1 - julian_epoch) * xmnpda) - i
            Dim tsince As Double = (timec - julian_epoch) * xmnpda
            Minuti = CInt(Minuti) + (i_Sec / 60 * 100) / 100

            Using Cmd As New SQLiteCommand("INSERT INTO tmpEffemeridi (date, time, azimuth, elevation, long, lat, intervallo, source) VALUES (@d,@t,@az,@el,@lo,@la,@in,'EFFEMERIDI')", CType(dbOrbite, DAODatabase).Connection)
                Dim Count As Double = tsince
                Do While Count <= tsince1
                    Dim pos As New Vector()
                    SolarAzEl(timec, pos)
                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@d", Calendar_Date(timec, 0))
                    Cmd.Parameters.AddWithValue("@t", Format(Time_of_Day(timec, 1, 0), "HH:mm:ss"))
                    Dim icM As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture
                    Cmd.Parameters.AddWithValue("@az", pos.Valori(1).ToString("000.000", icM))
                    Cmd.Parameters.AddWithValue("@el", pos.Valori(2).ToString("00.000", icM))
                    Cmd.Parameters.AddWithValue("@lo", (pos.Valori(3) * RadGradi).ToString("0.000", icM))
                    Cmd.Parameters.AddWithValue("@la", (pos.Valori(4) * RadGradi).ToString("0.000", icM))
                    Cmd.Parameters.AddWithValue("@in", TxtMinuti.Text)
                    Cmd.ExecuteNonQuery()
                    timec += i
                    Count += Minuti
                Loop
            End Using
            EffSUN = True
        Catch ex As Exception
            WriteError(0, "EffSUN: " & ex.Message)
        End Try
    End Function

    ' -------------------------------------------------------
    ' Calcolo effemeridi PIANETI (Venere)
    ' -------------------------------------------------------
    Private Function EffPLANETS(Minuti As Double) As Boolean
        If Minuti = 0 Then Return False
        EffPLANETS = False
        Try
            i_Sec = CInt((Minuti - CInt(Minuti)) * 100)
            Dim dataIni As Date = DateTime.ParseExact(MskDataIni.Text.Replace(".", ":"), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            seceffemeridi = CDbl(TxtOre.Text) * 3600
            Data1 = dataIni.AddSeconds(seceffemeridi)
            With ts
                .yr = Data1.Year : .mo = Data1.Month : .dy = Data1.Day
                .hr = Data1.Hour : .mi = CInt(Minuti) : .se = i_Sec : .hu = 0
            End With
            Dim tsince1 As Double = Julian_Date(ts)
            Dim minu As Integer = If(Minuti = 15, CInt(Minuti), dataIni.Minute)
            With ts
                .yr = dataIni.Year : .mo = dataIni.Month : .dy = dataIni.Day
                .hr = dataIni.Hour : .mi = minu : .se = 0 : .hu = 0
            End With
            Dim i As Double = Fraction_of_Day(0, CInt(Minuti), i_Sec, 0)
            Dim timec As Double = Julian_Date(ts)
            tsince1 = ((tsince1 - julian_epoch) * xmnpda) - i
            Dim tsince As Double = (timec - julian_epoch) * xmnpda
            Minuti = CInt(Minuti) + (i_Sec / 60 * 100) / 100

            Using Cmd As New SQLiteCommand("INSERT INTO tmpEffemeridi (date, time, azimuth, elevation, long, lat, intervallo, source) VALUES (@d,@t,@az,@el,@lo,@la,@in,'EFFEMERIDI')", CType(dbOrbite, DAODatabase).Connection)
                Dim Count As Double = tsince
                Do While Count <= tsince1
                    Dim pos As New Vector()
                    PosizioneVenere(timec, pos)
                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@d", Calendar_Date(timec, 0))
                    Cmd.Parameters.AddWithValue("@t", Format(Time_of_Day(timec, 1, 0), "HH:mm:ss"))
                    Dim icM As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture
                    Cmd.Parameters.AddWithValue("@az", pos.Valori(1).ToString("000.000", icM))
                    Cmd.Parameters.AddWithValue("@el", pos.Valori(2).ToString("00.000", icM))
                    Cmd.Parameters.AddWithValue("@lo", (pos.Valori(3) * RadGradi).ToString("0.000", icM))
                    Cmd.Parameters.AddWithValue("@la", (pos.Valori(4) * RadGradi).ToString("0.000", icM))
                    Cmd.Parameters.AddWithValue("@in", TxtMinuti.Text)
                    Cmd.ExecuteNonQuery()
                    timec += i
                    Count += Minuti
                Loop
            End Using
            EffPLANETS = True
        Catch ex As Exception
            WriteError(0, "EffPLANETS: " & ex.Message)
        End Try
    End Function

    ' -------------------------------------------------------
    ' Invia tabella all'ACU (riga per riga)
    ' -------------------------------------------------------
    Public Function InviaTabella() As String
        Try
            InviaTabella = ""
            Using Cmd As New SQLiteCommand("SELECT time, azimuth, elevation FROM tmpEffemeridi WHERE source = 'EFFEMERIDI' ORDER BY date, time", CType(dbOrbite, DAODatabase).Connection)
                Using dr = Cmd.ExecuteReader()
                    Dim rowCount As Integer = 0
                    Do While dr.Read()
                        If rowCount = LastMemory Then
                            Dim s_program As String = "MS " &
                                Format(CDate(dr("time")), "HH:mm") & " " &
                                Format(CDbl(dr("azimuth")), "000.000") & " " &
                                Format(CDbl(dr("elevation")), "00.000")
                            Dim i As Integer = Len(s_program) + 6
                            SEND_ACU = CheckSum(Format(CStr(i), "00") & Mid(s_program, 1, Len(s_program)))
                            Return SEND_ACU
                        End If
                        rowCount += 1
                    Loop
                End Using
            End Using
            ' Fine tabella
            Cursor = Cursors.Default
            TmrTable.Enabled = False
            MessageBox.Show("End SEND table", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            b_Parametri = False
        Catch ex As Exception
            WriteError(0, "InviaTabella: " & ex.Message)
        End Try
        Return ""
    End Function

    ' -------------------------------------------------------
    ' Salva effemeridi su file testo
    ' -------------------------------------------------------
    Private Function Salva() As Boolean
        Salva = True
        If g_Parametri = "" Then Return True
        Cursor = Cursors.WaitCursor
        Try
            Using sw As New StreamWriter(g_Parametri, False)
                sw.WriteLine("Effemeridi del " & Now.ToString("dd-MMM-yyyy") & " del satellite " & s_Satellite)
                sw.WriteLine("   Data           Ora          Az.         El.")
                Using Cmd As New SQLiteCommand("SELECT date, time, azimuth, elevation FROM tmpEffemeridi WHERE source = 'EFFEMERIDI' ORDER BY date, time", CType(dbOrbite, DAODatabase).Connection)
                    Using dr = Cmd.ExecuteReader()
                        Do While dr.Read()
                            sw.WriteLine(dr("date").ToString() & "  " &
                                         dr("time").ToString() & " " &
                                         Format(CDbl(dr("azimuth")), "000.000") & " " &
                                         Format(CDbl(dr("elevation")), "00.000"))
                        Loop
                    End Using
                End Using
            End Using
        Catch ex As Exception
            WriteError(0, "Salva: " & ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Function

    ' -------------------------------------------------------
    ' Stampa (Crystal Reports → RDLC)
    ' TODO: implementare con ReportViewer RDLC
    ' -------------------------------------------------------
    Private Function Stampa() As Boolean
        MessageBox.Show("Print not yet implemented. Use the Save function to export the data.",
                        "Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return True
    End Function

    ' -------------------------------------------------------
    ' Selezione file per salvataggio
    ' -------------------------------------------------------
    Private Sub MnuSalvaFile_Click(sender As Object, e As EventArgs) Handles MnuSalvaFile.Click
        Using dlg As New SaveFileDialog()
            dlg.Filter = "File testo (*.Txt)|*.Txt"
            dlg.InitialDirectory = "C:\TLE\"
            If dlg.ShowDialog() = DialogResult.OK Then
                g_Parametri = dlg.FileName
                Salva()
            End If
        End Using
    End Sub

    ' -------------------------------------------------------
    ' KeyUp su TxtMinuti — avvia calcolo con Invio
    ' -------------------------------------------------------
    Private Sub TxtMinuti_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtMinuti.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtMinuti.Text) Then
                TxtMinuti.Text = CStr(CDbl(TxtMinuti.Text))
                If b_Calcola Then MnuCalcola_Click(Nothing, EventArgs.Empty)
            End If
        End If
    End Sub

End Class

