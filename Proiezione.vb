Module Proiezione

    ' ============================================================
    '03/03/2026 Viene implementato il modulo proiezione per il calcolo dei punti di una traiettoria partendo da una tabella dati
    'di az e el in tempo reale
    ' ============================================================


    ' ============================================================
    '  CALCOLA IL PUNTO SUCCESSIVO  (versione polinomiale grado 2)
    ' ============================================================
    Public Sub CalcolaSuccessivo2(pts() As TrackPoint, nPts As Long,
                      nFit As Long, nextPt As TrackPoint)
        Dim i As Long
        Dim k As Long

        If nFit > nPts Then nFit = nPts

        Dim xArr() As Double
        Dim azArr() As Double
        Dim elArr() As Double

        ReDim xArr(nFit - 1)
        ReDim azArr(nFit - 1)
        ReDim elArr(nFit - 1)

        k = 0
        For i = nPts - nFit To nPts - 1
            xArr(k) = pts(i).T
            azArr(k) = pts(i).az
            elArr(k) = pts(i).el
            k += 1
        Next i

        Dim aAz As Double, bAz As Double, cAz As Double
        Dim aEl As Double, bEl As Double, cEl As Double

        PolyRegression2(xArr, azArr, nFit, aAz, bAz, cAz)
        PolyRegression2(xArr, elArr, nFit, aEl, bEl, cEl)

    ' Prossimo secondo
        Dim tNext As Double
        tNext = pts(nPts - 1).T + (1.0# / 86400.0#)

        nextPt.T = tNext
        nextPt.az = (aAz + bAz * tNext + cAz * tNext * tNext) + AzOffset1
        nextPt.el = (aEl + bEl * tNext + cEl * tNext * tNext) + ElOffset1

        ' Mantieni azimut in range 0-360
        Do While nextPt.az < 0
            nextPt.az += 360.0#
        Loop
        Do While nextPt.az >= 360.0#
            nextPt.az -= 360.0#
        Loop

        ' Mantieni elevazione in range -90/+90
        If nextPt.el > 90.0# Then nextPt.el = 90.0#
        If nextPt.el < -90.0# Then nextPt.el = -90.0#
        InvioPro()

    End Sub

    ' ============================================================
    '  CALCOLA IL PUNTO SUCCESSIVO
    ' ============================================================
    Public Sub CalcolaSuccessivoLineare(pts() As TrackPoint, nPts As Long,
                      nFit As Long, nextPt As TrackPoint)
        Dim i As Long
        Dim k As Long

        If nFit > nPts Then nFit = nPts

        Dim xArr() As Double
        Dim azArr() As Double
        Dim elArr() As Double

        ReDim xArr(nFit - 1)
        ReDim azArr(nFit - 1)
        ReDim elArr(nFit - 1)

        k = 0
        For i = nPts - nFit To nPts - 1
            xArr(k) = pts(i).T
            azArr(k) = pts(i).az
            elArr(k) = pts(i).el
            k += 1
        Next i

        Dim aAz As Double, bAz As Double
        Dim aEl As Double, bEl As Double

        LinearRegression(xArr, azArr, nFit, aAz, bAz)
        LinearRegression(xArr, elArr, nFit, aEl, bEl)

    ' Prossimo secondo (1 secondo = 1/86400 in numero seriale VB)
        Dim tNext As Double
        tNext = pts(nPts - 1).T + (1.0# / 86400.0#)

        nextPt.T = tNext
        nextPt.az = aAz + bAz * tNext
        nextPt.el = aEl + bEl * tNext

        ' Mantieni azimut in range 0-360
        Do While nextPt.az < 0
            nextPt.az += 360.0#
        Loop
        Do While nextPt.az >= 360.0#
            nextPt.az -= 360.0#
        Loop

        ' Mantieni elevazione in range -90/+90
        If nextPt.el > 90.0# Then nextPt.el = 90.0#
        If nextPt.el < -90.0# Then nextPt.el = -90.0#
    End Sub

    ' ============================================================
    '  REGRESSIONE LINEARE (minimi quadrati)  y = a + b*x
    ' ============================================================
    Sub LinearRegression(X() As Double, y() As Double, n As Long,
                     ByRef a As Double, ByRef b As Double)
        Dim sx As Double
        Dim sy As Double
        Dim sxx As Double
        Dim sxy As Double
        Dim denom As Double
        Dim i As Long

        sx = 0 : sy = 0 : sxx = 0 : sxy = 0
        For i = 0 To n - 1
            sx += X(i)
            sy += y(i)
            sxx += X(i) * X(i)
            sxy += X(i) * y(i)
        Next i

        denom = n * sxx - sx * sx
        If Math.Abs(denom) < 0.000000000000001 Then
            b = 0
            a = sy / n
        Else
            b = (n * sxy - sx * sy) / denom
            a = (sy - b * sx) / n
        End If
    End Sub

    ' ============================================================
    '  REGRESSIONE POLINOMIALE GRADO 2  y = a + b*x + c*x^2
    '  Risolve il sistema con eliminazione di Gauss
    ' ============================================================
    Sub PolyRegression2(X() As Double, y() As Double, n As Long,
                    ByRef a As Double, ByRef b As Double, ByRef c As Double)
        ' Calcola le somme necessarie
        Dim sx As Double, sx2 As Double, sx3 As Double, sx4 As Double
        Dim sy As Double, sxy As Double, sx2y As Double
        Dim i As Long

        sx = 0 : sx2 = 0 : sx3 = 0 : sx4 = 0
        sy = 0 : sxy = 0 : sx2y = 0

        For i = 0 To n - 1
            Dim XI As Double
            Dim xi2 As Double
            XI = X(i)
            xi2 = XI * XI
            sx += XI
            sx2 += xi2
            sx3 += xi2 * XI
            sx4 += xi2 * xi2
            sy += y(i)
            sxy += XI * y(i)
            sx2y += xi2 * y(i)
        Next i

        ' Matrice aumentata 3x4 del sistema normale
        Dim m(2, 3) As Double
        m(0, 0) = n : m(0, 1) = sx : m(0, 2) = sx2 : m(0, 3) = sy
        m(1, 0) = sx : m(1, 1) = sx2 : m(1, 2) = sx3 : m(1, 3) = sxy
        m(2, 0) = sx2 : m(2, 1) = sx3 : m(2, 2) = sx4 : m(2, 3) = sx2y

        ' Eliminazione di Gauss
        Dim row As Integer, col As Integer
        Dim factor As Double

        For col = 0 To 2
            ' Pivot
            For row = col + 1 To 2
                If Math.Abs(m(col, col)) < 0.000000000000001 Then GoTo SkipPivot
                factor = m(row, col) / m(col, col)
                Dim j As Integer
                For j = col To 3
                    m(row, j) = m(row, j) - factor * m(col, j)
                Next j
SkipPivot:
            Next row
        Next col

        ' Sostituzione a ritroso
        Dim res(2) As Double
        For row = 2 To 0 Step -1
            res(row) = m(row, 3)
            For col = row + 1 To 2
                res(row) = res(row) - m(row, col) * res(col)
            Next col
            If Math.Abs(m(row, row)) > 0.000000000000001 Then
                res(row) = res(row) / m(row, row)
            Else
                res(row) = 0
            End If
        Next row

        a = res(0)
        b = res(1)
        c = res(2)
    End Sub

    ' ============================================================
    '  CONVERTE STRINGA "GG/MM/AAAA HH:MM:SS" IN NUMERO SERIALE
    ' ============================================================
    Function ParseDateTime(s As String) As Double
        Dim parts() As String
        Dim dateParts() As String
        Dim timeParts() As String
        Dim gg As Integer, mm As Integer, aaaa As Integer
        Dim hh As Integer, mi As Integer, ss As Integer

        s = Trim(s)
        parts = Split(s, " ")
        dateParts = Split(parts(0), "/")
        timeParts = Split(parts(1), ":")

        gg = CInt(dateParts(0))
        mm = CInt(dateParts(1))
        aaaa = CInt(dateParts(2))
        hh = CInt(timeParts(0))
        mi = CInt(timeParts(1))
        ss = CInt(timeParts(2))

        ParseDateTime = New Date(aaaa, mm, gg, hh, mi, ss).ToOADate()
    End Function

    ' ============================================================
    '  CONVERTE NUMERO SERIALE IN STRINGA "GG/MM/AAAA HH:MM:SS"
    ' ============================================================
    Function FormatDateTime(d As Double) As String
        Dim DT As Date = Date.FromOADate(d)
        FormatDateTime = Format(DT.Day, "00") & "/" &
                     Format(DT.Month, "00") & "/" &
                     Format(DT.Year, "0000") & " " &
                     Format(DT.Hour, "00") & ":" &
                     Format(DT.Minute, "00") & ":" &
                     Format(DT.Second, "00")
    End Function

    Public Sub InvioPro()
        'Invia all'ACU i dati di Az ed El
        Dim Port As Integer
        Dim s_program As String = ""
        If IsNumeric(nextPt.el) Then
            If Not AzInibhit Then frmRicezione.LblAzimuth.Text = Format$(nextPt.az, "000.000")
            If Not ElInibhit Then frmRicezione.LblElevazione.Text = Format$(nextPt.el, "00.000")
            If Not AzInibhit Then frmRicezione.LblAzimuthC.Text = Format$(nextPt.az, "000.000")
            If Not ElInibhit Then frmRicezione.LblElevazioneC.Text = Format$(nextPt.el, "00.000")
            If nextPt.el < g_ElMin Then
                Port = Canali(ACU).Port_IN
                Port_Control(Port).Rec_State = PCR_STANDBY
                frmRicezione.LblAzimuth.ForeColor = Color.Yellow
                frmRicezione.LblElevazione.ForeColor = Color.Yellow
                Exit Sub
            Else
                frmRicezione.LblAzimuth.ForeColor = Color.Black
                frmRicezione.LblElevazione.ForeColor = Color.Black
            End If

            If Not AzInibhit And Not ElInibhit Then
                s_program = " P " & Format$(nextPt.az, "000.000") & " " &
                Format$(nextPt.el, "00.000") & " "
            ElseIf ElInibhit Then
                s_program = "AP " & Format$(nextPt.az, "000.000") & " "
            ElseIf AzInibhit Then
                s_program = "EP " & Format$(nextPt.el, "00.000") & " "
            End If
            i = Len(s_program) + 6
            SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            InvioACU(SEND_ACU)
        End If

    End Sub
    ' ============================================================
    '  REGRESSIONE POLINOMIALE GRADO 3  y = a + b*x + c*x^2 + d*x^3
    ' ============================================================
    Sub PolyRegression3(X() As Double, y() As Double, n As Long,
                    ByRef a As Double, ByRef b As Double,
                    ByRef c As Double, ByRef d As Double)
        Dim sx As Double, sx2 As Double, sx3 As Double
        Dim sx4 As Double, sx5 As Double, sx6 As Double
        Dim sy As Double, sxy As Double, sx2y As Double, sx3y As Double
        Dim i As Long

        sx = 0 : sx2 = 0 : sx3 = 0 : sx4 = 0 : sx5 = 0 : sx6 = 0
        sy = 0 : sxy = 0 : sx2y = 0 : sx3y = 0

        For i = 0 To n - 1
            Dim XI As Double
            Dim xi2 As Double
            Dim xi3 As Double
            XI = X(i)
            xi2 = XI * XI
            xi3 = xi2 * XI
            sx += XI
            sx2 += xi2
            sx3 += xi3
            sx4 += xi3 * XI        ' Σx⁴ — era "sx4 = ..." (sovrascriveva invece di sommare)
            sx5 += xi3 * xi2       ' Σx⁵ — era "sx5 += sx5 + ..." (raddoppiava sé stesso ad ogni giro)
            sx6 += xi3 * xi3       ' Σx⁶ — era "sx6 += sy + y(i)" (non una potenza di x, mescolava dentro y)
            sy += y(i)              ' Σy — mancava del tutto: restava sempre 0, termine noto della prima equazione corrotto
            sxy += XI * y(i)
            sx2y += xi2 * y(i)
            sx3y += xi3 * y(i)
        Next i

        ' Matrice aumentata 4x5
        Dim m(3, 4) As Double
        m(0, 0) = n : m(0, 1) = sx : m(0, 2) = sx2 : m(0, 3) = sx3 : m(0, 4) = sy
        m(1, 0) = sx : m(1, 1) = sx2 : m(1, 2) = sx3 : m(1, 3) = sx4 : m(1, 4) = sxy
        m(2, 0) = sx2 : m(2, 1) = sx3 : m(2, 2) = sx4 : m(2, 3) = sx5 : m(2, 4) = sx2y
        m(3, 0) = sx3 : m(3, 1) = sx4 : m(3, 2) = sx5 : m(3, 3) = sx6 : m(3, 4) = sx3y

        ' Eliminazione di Gauss
        Dim row As Integer, col As Integer
        Dim factor As Double

        For col = 0 To 3
            For row = col + 1 To 3
                If Math.Abs(m(col, col)) < 0.000000000000001 Then GoTo SkipPivot3
                factor = m(row, col) / m(col, col)
                Dim j As Integer
                For j = col To 4
                    m(row, j) = m(row, j) - factor * m(col, j)
                Next j
SkipPivot3:
            Next row
        Next col

        ' Sostituzione a ritroso
        Dim res(3) As Double
        For row = 3 To 0 Step -1
            res(row) = m(row, 4)
            For col = row + 1 To 3
                res(row) = res(row) - m(row, col) * res(col)
            Next col
            If Math.Abs(m(row, row)) > 0.000000000000001 Then
                res(row) = res(row) / m(row, row)
            Else
                res(row) = 0
            End If
        Next row

        a = res(0)
        b = res(1)
        c = res(2)
        d = res(3)
    End Sub
    ' ============================================================
    '  CALCOLA IL PUNTO SUCCESSIVO  (con scelta del grado)
    '  grado: 1=lineare  2=parabolico  3=cubico
    ' ============================================================
    Sub CalcolaSuccessivo(pts() As TrackPoint, nPts As Long,
                      nFit As Long, grado As Integer,
                      ByRef nextPt As TrackPoint)
        ' "nextPt As TrackPoint" senza ByRef: essendo una Structure (tipo
        ' valore), veniva passata per copia — ogni calcolo qui dentro
        ' (gradi 1/2/3/4) scriveva solo sulla copia locale, mai sulla
        ' variabile globale Public nextPt che InvioPro() legge davvero.
        ' Risultato: il calcolo era sempre corretto internamente ma
        ' InvioPro mostrava sempre lo stato precedente (0/vuoto),
        ' qualunque fosse il grado scelto. Con ByRef le scritture tornano
        ' alla variabile del chiamante.
        Dim i As Long
        Dim k As Long

        If nFit > nPts Then nFit = nPts

        Dim xArr() As Double
        Dim azArr() As Double
        Dim elArr() As Double

        ReDim xArr(nFit - 1)
        ReDim azArr(nFit - 1)
        ReDim elArr(nFit - 1)

        ' Tempi ricentrati rispetto al campione più recente, IN SECONDI
        ' (non più il numero seriale assoluto ~46000, quasi identico tra un
        ' campione e l'altro, né la differenza in giorni, troppo piccola:
        ' ~0,00001). Il grado 3 soprattutto arriva a sommare x^6 nelle
        ' equazioni normali: con numeri enormi (46000^6≈1e31) o minuscoli
        ' (0,00001^6≈1e-30) la matrice resta mal condizionata, ben oltre le
        ' ~15-17 cifre significative di un Double — in entrambi i casi
        ' risultati assurdi (es. elevazione sbattuta al clamp di 90°, o
        ' segno invertito). In secondi, su una finestra di decine di
        ' secondi, i valori restano in un intervallo numericamente sano
        ' (decine, non 1e31 né 1e-30) e il fit è stabile.
        Dim t0 As Double = pts(nPts - 1).T
        k = 0
        For i = nPts - nFit To nPts - 1
            xArr(k) = (pts(i).T - t0) * 86400.0#
            azArr(k) = pts(i).az
            elArr(k) = pts(i).el
            k += 1
        Next i

        ' Unwrap azimut: un passaggio che attraversa 0°/360° (es. 358°→2°)
        ' viene letto da un fit come un salto di quasi -356° invece che di
        ' +4°, mandando in overflow la regressione (o, per il grado 4, la
        ' media). Si "srotola" la sequenza rendendola continua oltre
        ' 0-360 prima del fit; il risultato finale rientra comunque
        ' nell'intervallo 0-360 grazie al "mantieni azimut" più sotto.
        For k = 1 To nFit - 1
            Do While azArr(k) - azArr(k - 1) > 180.0#
                azArr(k) -= 360.0#
            Loop
            Do While azArr(k) - azArr(k - 1) < -180.0#
                azArr(k) += 360.0#
            Loop
        Next k

        Dim tNext As Double
        tNext = pts(nPts - 1).T + (1.0# / 86400.0#)
        Dim tNextRel As Double = (tNext - t0) * 86400.0#   ' = 1.0 esatto (1 secondo), stessa unità di xArr

        nextPt.T = tNext

        Select Case grado
            Case 1
                ' Lineare
                Dim aAz1 As Double, bAz1 As Double
                Dim aEl1 As Double, bEl1 As Double
                LinearRegression(xArr, azArr, nFit, aAz1, bAz1)
            LinearRegression(xArr, elArr, nFit, aEl1, bEl1)
            nextPt.az = aAz1 + bAz1 * tNextRel
                nextPt.el = aEl1 + bEl1 * tNextRel

            Case 2
                ' Parabolico
                Dim aAz2 As Double, bAz2 As Double, cAz2 As Double
                Dim aEl2 As Double, bEl2 As Double, cEl2 As Double
                PolyRegression2(xArr, azArr, nFit, aAz2, bAz2, cAz2)
            PolyRegression2(xArr, elArr, nFit, aEl2, bEl2, cEl2)
            nextPt.az = aAz2 + bAz2 * tNextRel + cAz2 * tNextRel * tNextRel
                nextPt.el = aEl2 + bEl2 * tNextRel + cEl2 * tNextRel * tNextRel

            Case 3
                ' Cubico
                Dim aAz3 As Double, bAz3 As Double, cAz3 As Double, dAz3 As Double
                Dim aEl3 As Double, bEl3 As Double, cEl3 As Double, dEl3 As Double
                PolyRegression3(xArr, azArr, nFit, aAz3, bAz3, cAz3, dAz3)
            PolyRegression3(xArr, elArr, nFit, aEl3, bEl3, cEl3, dEl3)
            nextPt.az = aAz3 + bAz3 * tNextRel + cAz3 * tNextRel * tNextRel + dAz3 * tNextRel * tNextRel * tNextRel
                nextPt.el = aEl3 + bEl3 * tNextRel + cEl3 * tNextRel * tNextRel + dEl3 * tNextRel * tNextRel * tNextRel
            Case 4
                ' Geostazionario: mai implementato nel porting ("frmPredict
                ' not in project") — nextPt.az/el restavano a zero (il
                ' default di TrackPoint), mostrando "000.000"/"00.000"
                ' invece di una previsione. Per un bersaglio geostazionario
                ' (quasi fermo) la previsione è la media mobile sugli ultimi
                ' nFit punti (azArr/elArr, già preparati sopra con la stessa
                ' finestra usata dagli altri gradi): filtra il rumore della
                ' telemetria invece di affidarsi a un singolo campione.
                Dim sommaAz As Double = 0.0#, sommaEl As Double = 0.0#
                For i = 0 To nFit - 1
                    sommaAz += azArr(i)
                    sommaEl += elArr(i)
                Next i
                nextPt.az = sommaAz / nFit
                nextPt.el = sommaEl / nFit
        End Select

        ' Mantieni azimut in range 0-360
        Do While nextPt.az < 0
            nextPt.az += 360.0#
        Loop
        Do While nextPt.az >= 360.0#
            nextPt.az -= 360.0#
        Loop

        ' Mantieni elevazione in range -90/+90
        If nextPt.el > 90.0# Then nextPt.el = 90.0#
        If nextPt.el < -90.0# Then nextPt.el = -90.0#
        InvioPro()
    End Sub


End Module
