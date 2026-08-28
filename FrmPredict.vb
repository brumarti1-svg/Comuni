Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class FrmPredict

    ' ── struttura punto (VB6 Type → Structure) ──────────────────────────────────
    Private Structure TPunto
        Dim T   As Double
        Dim Az  As Double
        Dim El  As Double
        Dim Src As Integer   ' 0=reale, 1=stimato
    End Structure

    ' ── costanti ────────────────────────────────────────────────────────────────
    Private Const BUF_MAX  As Integer = 1500
    Private Const PRED_MAX As Integer = 50

    ' ── buffer circolare ────────────────────────────────────────────────────────
    Private bufPts()  As TPunto
    Private bufHead   As Integer
    Private bufCount  As Integer
    Private bufSize   As Integer

    ' Accumulatore decimazione
    Private accumT     As Double
    Private accumAz    As Double
    Private accumEl    As Double
    Private accumN     As Integer
    Private accumStart As Double
    Private decimSec   As Long

    ' Contatore rifit
    Private newPtsSinceRifit As Integer
    Private rifitOgni        As Integer

    ' ── modello Fourier ─────────────────────────────────────────────────────────
    Private azCoeffs()  As Double
    Private elCoeffs()  As Double
    Private modelBuilt  As Boolean
    Private nArm        As Integer
    Private modelT      As Double
    Private modelTime   As String

    ' Predizioni
    Private predPts() As TPunto
    Private nPred     As Integer

    ' ── simulazione ─────────────────────────────────────────────────────────────
    Private simT       As Double
    Private simPeriodo As Double
    Private simOmega   As Double
    Private simRunning As Boolean

    ' ── Form_Load ────────────────────────────────────────────────────────────────
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReDim bufPts(BUF_MAX)
        ReDim predPts(PRED_MAX)
        bufHead = 0 : bufCount = 0
        accumN = 0 : newPtsSinceRifit = 0
        modelBuilt = False : simRunning = False

        simPeriodo = 86164.0
        simOmega   = 2.0 * Math.PI / simPeriodo

        ' Configura ListView (VB6 ColumnHeaders → VB.NET Columns)
        lvwPredizioni.View          = View.Details
        lvwPredizioni.FullRowSelect = True
        lvwPredizioni.GridLines     = True
        lvwPredizioni.Columns.Add("#",        40)
        lvwPredizioni.Columns.Add("Tempo(s)", 73)
        lvwPredizioni.Columns.Add("Az (deg)", 67)
        lvwPredizioni.Columns.Add("El (deg)", 67)
        lvwPredizioni.Columns.Add("dAz",      53)
        lvwPredizioni.Columns.Add("dEl",      53)
        lvwPredizioni.Columns.Add("Conf%",    47)

        AggiornaCfgInfo()
        LogMsg("Sistema avviato.")
        SetStatus("Pronto. Premere 'Precarica 24h esempio' per iniziare.")
    End Sub

    ' ── info configurazione ──────────────────────────────────────────────────────
    Private Sub AggiornaCfgInfo()
        Dim decSec As Long   = ValLong(TxtDecim.Text,     300)
        Dim bufOre As Double = ValDbl(TxtBufOre.Text,     12.0)
        bufSize   = CInt(bufOre * 3600.0 / decSec)
        If bufSize < 10     Then bufSize = 10
        If bufSize > BUF_MAX Then bufSize = BUF_MAX
        rifitOgni = CInt(ValLong(TxtRifitOgni.Text, 12))
        If rifitOgni < 1 Then rifitOgni = 1
        LblInfo.Text = "Buffer max: " & bufSize & " punti  (=" &
                       Format(bufOre, "0.0") & "h @ " & Format(decSec / 60, "0") & "min)  " &
                       "Rifit ogni " & rifitOgni & " punti (~" &
                       Format(rifitOgni * decSec / 60, "0") & " min)"
    End Sub

    ' ── feed misura ──────────────────────────────────────────────────────────────
    Private Sub FeedMisura(T As Double, az As Double, el As Double,
                           Optional srcFlag As Integer = 0)
        decimSec = ValLong(TxtDecim.Text, 300)
        If decimSec < 1 Then decimSec = 1

        If accumN = 0 AndAlso bufCount = 0 Then
            accumStart = T - (T Mod decimSec)
            If accumStart < 0 Then accumStart = 0
        End If

        accumT += T : accumAz += az : accumEl += el : accumN += 1

        If T >= accumStart + decimSec Then
            Dim pt As TPunto
            pt.T   = accumT  / accumN
            pt.Az  = accumAz / accumN
            pt.El  = accumEl / accumN
            pt.Src = srcFlag

            bufPts(bufHead) = pt
            bufHead = (bufHead + 1) Mod bufSize
            If bufCount < bufSize Then bufCount += 1

            accumStart += decimSec
            Do While T >= accumStart + decimSec
                accumStart += decimSec
            Loop
            accumT = 0 : accumAz = 0 : accumEl = 0 : accumN = 0

            AggiornaBufDisplay(pt)

            newPtsSinceRifit += 1
            If newPtsSinceRifit >= rifitOgni AndAlso bufCount >= 7 Then
                CostruisciModello()
                newPtsSinceRifit = 0
            End If
        End If
    End Sub

    ' ── aggiorna display buffer ──────────────────────────────────────────────────
    Private Sub AggiornaBufDisplay(pt As TPunto)
        LblBufPunti.Text = "Punti nel buffer: " & bufCount & " / " & bufSize
        Dim pts() As TPunto = Nothing
        Dim n As Integer
        EstraiBuffer(pts, n)
        If n >= 2 Then
            LblBufFinestra.Text = "Finestra: " & Format(pts(0).T / 3600, "0.00") & "h" &
                                  " -> " & Format(pts(n - 1).T / 3600, "0.00") & "h" &
                                  "  (" & Format((pts(n - 1).T - pts(0).T) / 3600, "0.1") & "h)"
        End If
        LblBufUltimo.Text = "Ultima: t=" & Format(pt.T, "0") & "s" &
                            "  Az=" & Format(pt.Az, "0.000") & Chr(176) &
                            "  El=" & Format(pt.El, "0.000") & Chr(176)
    End Sub

    ' ── estrai buffer ordinato temporalmente ─────────────────────────────────────
    Private Sub EstraiBuffer(ByRef pts() As TPunto, ByRef n As Integer)
        n = bufCount
        If n = 0 Then ReDim pts(0) : Exit Sub
        ReDim pts(n - 1)
        Dim startIdx As Integer = ((bufHead - bufCount) Mod bufSize + bufSize) Mod bufSize
        Dim i As Integer
        For i = 0 To n - 1
            pts(i) = bufPts((startIdx + i) Mod bufSize)
        Next i
    End Sub

    ' ── costruisci modello Fourier ────────────────────────────────────────────────
    Private Sub CostruisciModello()
        Dim pts() As TPunto = Nothing
        Dim n As Integer
        EstraiBuffer(pts, n)
        If n < 7 Then Exit Sub

        nArm = CInt(ValLong(TxtArm.Text, 3))
        If nArm < 1 Then nArm = 1
        If nArm > 5 Then nArm = 5

        Dim ts(n - 1)  As Double
        Dim azs(n - 1) As Double
        Dim els(n - 1) As Double
        Dim i As Integer
        For i = 0 To n - 1
            ts(i)  = pts(i).T
            azs(i) = pts(i).Az
            els(i) = pts(i).El
        Next i

        Dim periodo As Double = StimaPeriodo(ts, azs, n)
        If periodo <= 0 Then periodo = simPeriodo
        modelT = periodo

        azCoeffs = FourierFit(ts, azs, n, nArm, periodo)
        elCoeffs = FourierFit(ts, els, n, nArm, periodo)

        Dim fitAz(n - 1) As Double
        Dim fitEl(n - 1) As Double
        For i = 0 To n - 1
            fitAz(i) = EvalFourier(azCoeffs, ts(i), periodo, nArm)
            fitEl(i) = EvalFourier(elCoeffs, ts(i), periodo, nArm)
        Next i

        Dim rAz As Double = CalcRMSE(fitAz, azs, n)
        Dim rEl As Double = CalcRMSE(fitEl, els, n)
        Dim r2v As Double = (CalcR2(fitAz, azs, n) + CalcR2(fitEl, els, n)) / 2.0

        modelBuilt = True
        modelTime  = Now.ToString("HH:mm:ss")

        LblModRMSE.Text   = "RMSE Az: " & Format(rAz, "0.0000") & Chr(176) &
                            "   El: "   & Format(rEl, "0.0000") & Chr(176)
        LblModR2.Text     = "R2: " & Format(r2v, "0.0000") &
                            "    T: "  & Format(periodo / 3600, "0.00") & "h"
        LblModUpdate.Text = "Aggiornato: " & modelTime & "  [" & n & " punti]"

        LogMsg("[RIFIT] n=" & n &
               " RMSEaz=" & Format(rAz, "0.0000") &
               " RMSEel=" & Format(rEl, "0.0000") &
               " R2="     & Format(r2v, "0.0000") &
               " T="      & Format(periodo / 3600, "0.00") & "h")

        DisegnaGrafico()
        DisegnaOrbita()
        SetStatus("Modello [" & modelTime & "]  Az RMSE=" & Format(rAz, "0.0000") &
                  Chr(176) & "  El RMSE=" & Format(rEl, "0.0000") & Chr(176))
    End Sub

    ' ── fit Fourier: layout coeffs [a0, a1, b1, ..., aN, bN, T, nf] ─────────────
    Private Function FourierFit(ts() As Double, ys() As Double,
                                n As Integer, nf As Integer,
                                T As Double) As Double()
        Dim nc As Integer = 1 + 2 * nf
        Dim a(n - 1, nc - 1) As Double
        Dim i As Integer, k As Integer
        For i = 0 To n - 1
            a(i, 0) = 1.0
            For k = 1 To nf
                a(i, 2 * k - 1) = Math.Cos(2.0 * Math.PI * k * ts(i) / T)
                a(i, 2 * k)     = Math.Sin(2.0 * Math.PI * k * ts(i) / T)
            Next k
        Next i

        Dim AtA(nc - 1, nc - 1) As Double
        Dim Atb(nc - 1) As Double
        Dim j As Integer, kk As Integer
        For i = 0 To nc - 1
            Dim sb As Double = 0
            For kk = 0 To n - 1 : sb += a(kk, i) * ys(kk) : Next kk
            Atb(i) = sb
            For j = 0 To nc - 1
                Dim s2 As Double = 0
                For kk = 0 To n - 1 : s2 += a(kk, i) * a(kk, j) : Next kk
                AtA(i, j) = s2
            Next j
        Next i

        Dim co() As Double = GaussElim(AtA, Atb, nc)
        ReDim Preserve co(nc + 1)
        co(nc) = T : co(nc + 1) = nf
        Return co
    End Function

    ' ── valutazione modello ───────────────────────────────────────────────────────
    Private Function EvalFourier(co() As Double, T As Double,
                                 Tper As Double, nf As Integer) As Double
        Dim v As Double = co(0)
        Dim k As Integer
        For k = 1 To nf
            v += co(2 * k - 1) * Math.Cos(2.0 * Math.PI * k * T / Tper)
            v += co(2 * k)     * Math.Sin(2.0 * Math.PI * k * T / Tper)
        Next k
        Return v
    End Function

    Private Function EvalAz(T As Double) As Double
        If Not modelBuilt Then Return 0.0
        ' Layout: [a0..aN bN, T, nf]  — ultimi due slot aggiunti da FourierFit
        Dim nf   As Integer = CInt(azCoeffs(azCoeffs.Length - 1))
        Dim Tper As Double  = azCoeffs(azCoeffs.Length - 2)
        Return EvalFourier(azCoeffs, T, Tper, nf)
    End Function

    Private Function EvalEl(T As Double) As Double
        If Not modelBuilt Then Return 0.0
        Dim nf   As Integer = CInt(elCoeffs(elCoeffs.Length - 1))
        Dim Tper As Double  = elCoeffs(elCoeffs.Length - 2)
        Return EvalFourier(elCoeffs, T, Tper, nf)
    End Function

    ' ── stima periodo (zero-crossing Az) ─────────────────────────────────────────
    Private Function StimaPeriodo(ts() As Double, azs() As Double,
                                  n As Integer) As Double
        Dim mean As Double = 0
        Dim i As Integer
        For i = 0 To n - 1 : mean += azs(i) : Next i
        mean /= n

        Dim cross(n) As Double
        Dim nc As Integer = 0
        For i = 0 To n - 2
            If (azs(i) - mean) * (azs(i + 1) - mean) < 0 Then
                Dim tc As Double = ts(i) + (ts(i + 1) - ts(i)) *
                                   (mean - azs(i)) / (azs(i + 1) - azs(i))
                cross(nc) = tc : nc += 1
            End If
        Next i

        If nc >= 2 Then
            Dim sumD As Double = 0
            For i = 1 To nc - 1 : sumD += cross(i) - cross(i - 1) : Next i
            Return sumD / (nc - 1) * 2.0
        Else
            Return -1
        End If
    End Function

    ' ── calcola predizione ────────────────────────────────────────────────────────
    Public Sub BtnPredici_Click(sender As Object, e As EventArgs) Handles BtnPredici.Click
        If Not IsNumeric(TxtTPred.Text) Then
            MessageBox.Show("Inserire un tempo valido.", "Attenzione",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If bufCount = 0 Then
            MessageBox.Show("Nessun dato nel buffer.", "Attenzione",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tPred    As Double  = CDbl(TxtTPred.Text)
        Dim nPassi   As Integer = CInt(ValLong(TxtNPassi.Text, 6))
        Dim interval As Double  = ValDbl(TxtIntervallo.Text, 3600.0)
        If nPassi < 1       Then nPassi = 1
        If nPassi > PRED_MAX Then nPassi = PRED_MAX

        Dim pts() As TPunto = Nothing
        Dim n As Integer
        EstraiBuffer(pts, n)
        If n = 0 Then Exit Sub
        Dim tMin As Double = pts(0).T
        Dim tMax As Double = pts(n - 1).T

        nPred = 0
        Dim i As Integer
        For i = 0 To nPassi - 1
            predPts(nPred).T  = tPred + i * interval
            predPts(nPred).Az = EvalAz(predPts(nPred).T)
            predPts(nPred).El = EvalEl(predPts(nPred).T)
            nPred += 1
        Next i

        Dim conf As Integer = CalcConf(tPred, tMin, tMax)
        LblPredAz.Text   = "Azimuth:    " & Format(predPts(0).Az, "0.0000")
        LblPredEl.Text   = "Elevazione: " & Format(predPts(0).El, "0.0000")
        LblPredConf.Text = "Confidenza: " & conf & "%  " & BarraConf(conf) &
                           "   T=" & Format(modelT / 3600, "0.00") & "h"

        ' Popola ListView  (VB6 ListItems.Add / Set li = → VB.NET Items.Add)
        lvwPredizioni.Items.Clear()
        For i = 0 To nPred - 1
            Dim li As New ListViewItem(Format(i + 1))
            li.SubItems.Add(Format(predPts(i).T,  "0"))
            li.SubItems.Add(Format(predPts(i).Az, "0.0000"))
            li.SubItems.Add(Format(predPts(i).El, "0.0000"))
            If i > 0 Then
                li.SubItems.Add(Format(predPts(i).Az - predPts(i - 1).Az, "+0.0000;-0.0000"))
                li.SubItems.Add(Format(predPts(i).El - predPts(i - 1).El, "+0.0000;-0.0000"))
            Else
                li.SubItems.Add(Chr(176))
                li.SubItems.Add(Chr(176))
            End If
            li.SubItems.Add(Format(CalcConf(predPts(i).T, tMin, tMax), "0"))
            lvwPredizioni.Items.Add(li)
        Next i

        DisegnaGrafico()
        DisegnaOrbita()
        LogMsg("[PRED] t=" & Format(tPred, "0") & "s  Az=" &
               Format(predPts(0).Az, "0.0000") &
               "  El=" & Format(predPts(0).El, "0.0000") & "  Conf=" & conf & "%")
        SetStatus("Predizione t=" & Format(tPred, "0") & "s  Az=" &
                  Format(predPts(0).Az, "0.0000") & Chr(176) &
                  "  El=" & Format(predPts(0).El, "0.0000") & Chr(176))
    End Sub

    Private Function CalcConf(T As Double, tMn As Double, tMx As Double) As Integer
        Dim span As Double = tMx - tMn + 1
        Dim ext  As Double
        If T > tMx Then
            ext = (T - tMx) / span
        ElseIf T < tMn Then
            ext = (tMn - T) / span
        Else
            ext = 0
        End If
        Dim c As Integer = CInt(100.0 / (1.0 + ext * 1.5))
        If c < 5   Then c = 5
        If c > 100 Then c = 100
        Return c
    End Function

    ' ── precarica esempio 24h ─────────────────────────────────────────────────────
    Private Sub BtnEsempio_Click(sender As Object, e As EventArgs) Handles BtnEsempio.Click
        RisetBuffer()
        Dim nSample   As Integer = 288
        Dim saveRifit As Integer = rifitOgni
        rifitOgni = 9999

        Dim i As Integer
        For i = 0 To nSample - 1
            Dim T  As Double = i * 300.0
            Dim ph As Double = simOmega * T
            Dim az As Double = 180.0 + 4.0 * Math.Sin(ph) +
                               0.08 * Math.Sin(3.0 * ph) + 0.04 * (Rnd() - 0.5)
            Dim el As Double = 45.0  + 6.0 * Math.Sin(2.0 * ph) +
                               0.12 * Math.Cos(4.0 * ph) + 0.03 * (Rnd() - 0.5)
            bufPts(bufHead).T   = T
            bufPts(bufHead).Az  = az
            bufPts(bufHead).El  = el
            bufPts(bufHead).Src = 0
            bufHead = (bufHead + 1) Mod bufSize
            If bufCount < bufSize Then bufCount += 1
        Next i

        rifitOgni = saveRifit
        TxtTPred.Text = Format(nSample * 300.0, "0")

        Dim pts() As TPunto = Nothing
        Dim n As Integer
        EstraiBuffer(pts, n)
        If n > 0 Then
            LblBufUltimo.Text   = "Ultima: t=" & Format(pts(n - 1).T, "0") & "s" &
                                  "  Az=" & Format(pts(n - 1).Az, "0.000") & Chr(176) &
                                  "  El=" & Format(pts(n - 1).El, "0.000") & Chr(176)
            LblBufFinestra.Text = "Finestra: 0s -> " &
                                  Format(pts(n - 1).T / 3600, "0.1") & "h"
        End If
        LblBufPunti.Text = "Punti nel buffer: " & bufCount & " / " & bufSize
        LogMsg("[ESEMPIO] Caricati " & bufCount & " punti (24h, passo 5min)")
        SetStatus("Esempio caricato. Costruzione modello in corso...")
        CostruisciModello()
    End Sub

    ' ── invia misura manuale ──────────────────────────────────────────────────────
    Private Sub BtnFeed_Click(sender As Object, e As EventArgs) Handles BtnFeed.Click
        If Not (IsNumeric(TxtTempo.Text) AndAlso IsNumeric(TxtAzimuth.Text) AndAlso
                IsNumeric(TxtElevazione.Text)) Then
            MessageBox.Show("Inserire valori numerici per Tempo, Azimuth ed Elevazione.",
                            "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        FeedMisura(CDbl(TxtTempo.Text), CDbl(TxtAzimuth.Text), CDbl(TxtElevazione.Text), 0)
        TxtTempo.Text = "" : TxtAzimuth.Text = "" : TxtElevazione.Text = ""
        TxtTempo.Focus()
    End Sub

    ' ── simulazione real-time ─────────────────────────────────────────────────────
    Private Sub BtnSimStart_Click(sender As Object, e As EventArgs) Handles BtnSimStart.Click
        If bufCount > 0 Then
            Dim pts() As TPunto = Nothing
            Dim n As Integer
            EstraiBuffer(pts, n)
            simT = pts(n - 1).T + 1
        Else
            simT = 0
        End If
        simRunning = True
        tmrSim.Enabled     = True
        BtnSimStart.Enabled = False
        BtnSimStop.Enabled  = True
        LogMsg("[SIM] Avviata da t=" & Format(simT, "0") & "s")
    End Sub

    Private Sub TmrSim_Tick(sender As Object, e As EventArgs) Handles tmrSim.Tick
        Dim ph As Double = simOmega * simT
        Dim az As Double = 180.0 + 4.0  * Math.Sin(ph) +
                           0.08 * Math.Sin(3.0 * ph) + 0.015 * (Rnd() - 0.5)
        Dim el As Double = 45.0  + 6.0  * Math.Sin(2.0 * ph) +
                           0.12 * Math.Cos(4.0 * ph) + 0.01  * (Rnd() - 0.5)
        FeedMisura(simT, az, el, 0)
        TxtTempo.Text = Format(simT, "0")
        simT += 1
    End Sub

    Private Sub BtnSimStop_Click(sender As Object, e As EventArgs) Handles BtnSimStop.Click
        tmrSim.Enabled      = False
        simRunning          = False
        BtnSimStart.Enabled = True
        BtnSimStop.Enabled  = False
        LogMsg("[SIM] Fermata a t=" & Format(simT, "0") & "s")
    End Sub

    ' ── reset buffer ─────────────────────────────────────────────────────────────
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        If MessageBox.Show("Cancellare buffer e modello?", "Conferma",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            RisetBuffer()
        End If
    End Sub

    Private Sub RisetBuffer()
        If simRunning Then
            tmrSim.Enabled      = False : simRunning   = False
            BtnSimStart.Enabled = True  : BtnSimStop.Enabled = False
        End If
        bufHead = 0 : bufCount = 0
        accumT = 0 : accumAz = 0 : accumEl = 0 : accumN = 0 : accumStart = 0
        newPtsSinceRifit = 0
        modelBuilt = False : nPred = 0

        LblBufPunti.Text   = "Punti nel buffer: 0 / " & bufSize
        LblBufFinestra.Text = "Finestra:  "
        LblBufUltimo.Text   = "Ultima misura:  "
        LblModRMSE.Text     = "RMSE Az:     El:  "
        LblModR2.Text       = "R2:     Periodo:  "
        LblModUpdate.Text   = "Ultimo aggiornamento:  "
        LblPredAz.Text      = "Azimuth:  "
        LblPredEl.Text      = "Elevazione:  "
        LblPredConf.Text    = "Confidenza:      Periodo:  "
        lvwPredizioni.Items.Clear()
        picGrafico.Image = Nothing
        picOrbita.Image  = Nothing
        LogMsg("[RESET] Buffer e modello azzerati.")
        SetStatus("Buffer azzerato.")
    End Sub

    ' ── eliminazione di Gauss (sistema lineare n×n) ───────────────────────────────
    Private Function GaussElim(a(,) As Double, b() As Double, n As Integer) As Double()
        Dim i As Integer, j As Integer, k As Integer
        Dim m(n - 1, n) As Double
        For i = 0 To n - 1
            For j = 0 To n - 1 : m(i, j) = a(i, j) : Next j
            m(i, n) = b(i)
        Next i

        For i = 0 To n - 1
            ' Pivot parziale
            Dim mxR As Integer = i
            Dim mxV As Double  = Math.Abs(m(i, i))
            For k = i + 1 To n - 1
                If Math.Abs(m(k, i)) > mxV Then
                    mxV = Math.Abs(m(k, i)) : mxR = k
                End If
            Next k
            If mxR <> i Then
                Dim tmp As Double
                For j = 0 To n
                    tmp = m(i, j) : m(i, j) = m(mxR, j) : m(mxR, j) = tmp
                Next j
            End If

            Dim pv As Double = m(i, i)
            ' GoTo SkipP → If/End If (salta divisione se pivot troppo piccolo)
            If Math.Abs(pv) >= 1E-14 Then
                For k = 0 To n - 1
                    If k <> i Then
                        Dim f As Double = m(k, i) / pv
                        For j = i To n : m(k, j) -= f * m(i, j) : Next j
                    End If
                Next k
            End If
        Next i

        Dim res(n - 1) As Double
        For i = 0 To n - 1
            res(i) = If(Math.Abs(m(i, i)) > 1E-14, m(i, n) / m(i, i), 0.0)
        Next i
        Return res
    End Function

    ' ── statistiche ──────────────────────────────────────────────────────────────
    Private Function CalcRMSE(pred() As Double, actual() As Double,
                               n As Integer) As Double
        Dim s As Double = 0
        Dim i As Integer
        For i = 0 To n - 1 : s += (pred(i) - actual(i)) ^ 2 : Next i
        Return Math.Sqrt(s / n)
    End Function

    Private Function CalcR2(pred() As Double, actual() As Double,
                             n As Integer) As Double
        Dim mean As Double = 0
        Dim i As Integer
        For i = 0 To n - 1 : mean += actual(i) : Next i
        mean /= n
        Dim sst As Double = 0, ssr As Double = 0
        For i = 0 To n - 1
            sst += (actual(i) - mean) ^ 2
            ssr += (pred(i)   - actual(i)) ^ 2
        Next i
        If sst < 1E-14 Then Return 1.0
        Dim r2 As Double = 1.0 - ssr / sst
        Return If(r2 < 0, 0.0, r2)
    End Function

    ' ── disegna grafico buffer + predizioni ──────────────────────────────────────
    Private Sub DisegnaGrafico()
        Dim pts() As TPunto = Nothing
        Dim n As Integer
        EstraiBuffer(pts, n)
        If n < 2 Then Exit Sub

        Dim W As Integer = picGrafico.ClientSize.Width
        Dim H As Integer = picGrafico.ClientSize.Height
        If W <= 0 OrElse H <= 0 Then Exit Sub

        Dim bmp As New Bitmap(W, H)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.Clear(Color.White)
        g.SmoothingMode = SmoothingMode.AntiAlias

        ' Calcolo range
        Dim tMn  As Double = pts(0).T  : Dim tMx  As Double = pts(n - 1).T
        Dim azMn As Double = pts(0).Az : Dim azMx As Double = pts(0).Az
        Dim elMn As Double = pts(0).El : Dim elMx As Double = pts(0).El
        Dim i As Integer
        For i = 1 To n - 1
            If pts(i).Az < azMn Then azMn = pts(i).Az
            If pts(i).Az > azMx Then azMx = pts(i).Az
            If pts(i).El < elMn Then elMn = pts(i).El
            If pts(i).El > elMx Then elMx = pts(i).El
        Next i
        If nPred > 0 Then
            If predPts(nPred - 1).T > tMx Then tMx = predPts(nPred - 1).T
            For i = 0 To nPred - 1
                If predPts(i).Az < azMn Then azMn = predPts(i).Az
                If predPts(i).Az > azMx Then azMx = predPts(i).Az
                If predPts(i).El < elMn Then elMn = predPts(i).El
                If predPts(i).El > elMx Then elMx = predPts(i).El
            Next i
        End If
        azMn -= 0.2 : azMx += 0.2
        elMn -= 0.2 : elMx += 0.2

        ' Variabili di disegno condivise tra i blocchi (VB6 era function-scope)
        Dim cx As Integer, cy As Integer, tt As Double
        Dim prX As Integer = 0, prY As Integer = 0, fst As Boolean

        ' Assi
        Using p As New Pen(Color.FromArgb(180, 180, 180))
            g.DrawLine(p, 45, H - 28, W - 10, H - 28)
            g.DrawLine(p, 45, 18, 45, H - 28)
        End Using
        Using fnt As New Font("Arial", 7)
            g.DrawString("Buffer + predizioni  (BLU=Az, VERDE=El, ARANCIO=predette)",
                         fnt, New SolidBrush(Color.FromArgb(60, 60, 60)), 50, 4)
            ' Etichette asse X
            Dim ti As Integer
            For ti = 0 To 4
                Dim tv As Double  = tMn + ti * (tMx - tMn) / 4
                Dim px As Integer = GX(tv, tMn, tMx, W)
                Using tp As New Pen(Color.FromArgb(180, 180, 180))
                    g.DrawLine(tp, px, H - 28, px, H - 22)
                End Using
                Dim Lbl As String = If(tv >= 3600,
                                       Format(tv / 3600, "0.0") & "h",
                                       Format(tv, "0") & "s")
                g.DrawString(Lbl, fnt, New SolidBrush(Color.FromArgb(120, 120, 120)),
                             px - 20, H - 20)
            Next ti
        End Using

        ' Linea separatrice fine buffer
        Dim sepX As Integer = GX(pts(n - 1).T, tMn, tMx, W)
        Using sp As New Pen(Color.FromArgb(200, 180, 80)) With {.DashStyle = DashStyle.DashDot}
            g.DrawLine(sp, sepX, 20, sepX, H - 28)
        End Using

        ' Curve fit tratteggiate
        If modelBuilt Then
            Dim nStep As Integer = 200
            fst = True
            Using fp As New Pen(Color.FromArgb(100, 160, 220), 2) With {.DashStyle = DashStyle.Dash}
                For i = 0 To nStep
                    tt = tMn + i * (tMx - tMn) / nStep
                    cx = GX(tt, tMn, tMx, W) : cy = GY(EvalAz(tt), azMn, azMx, H)
                    If Not fst Then g.DrawLine(fp, prX, prY, cx, cy)
                    fst = False : prX = cx : prY = cy
                Next i
            End Using
            fst = True
            Using fp As New Pen(Color.FromArgb(50, 180, 130), 2) With {.DashStyle = DashStyle.Dash}
                For i = 0 To nStep
                    tt = tMn + i * (tMx - tMn) / nStep
                    cx = GX(tt, tMn, tMx, W) : cy = GY(EvalEl(tt), elMn, elMx, H)
                    If Not fst Then g.DrawLine(fp, prX, prY, cx, cy)
                    fst = False : prX = cx : prY = cy
                Next i
            End Using
        End If

        ' Punti buffer Az
        Using azPen As New Pen(Color.FromArgb(24, 95, 165))
            For i = 0 To n - 1
                cx = GX(pts(i).T, tMn, tMx, W) : cy = GY(pts(i).Az, azMn, azMx, H)
                If pts(i).Src = 1 Then
                    Using dp As New Pen(Color.FromArgb(24, 95, 165)) With {.DashStyle = DashStyle.Dash}
                        g.DrawLine(dp, cx - 3, cy - 3, cx + 3, cy + 3)
                    End Using
                Else
                    g.DrawEllipse(azPen, cx - 3, cy - 3, 6, 6)
                End If
            Next i
        End Using

        ' Punti buffer El
        Using elPen As New Pen(Color.FromArgb(15, 110, 86))
            For i = 0 To n - 1
                cx = GX(pts(i).T, tMn, tMx, W) : cy = GY(pts(i).El, elMn, elMx, H)
                g.DrawEllipse(elPen, cx - 3, cy - 3, 6, 6)
            Next i
        End Using

        ' Predizioni (stessa prX/prY già dichiarate sopra)
        If nPred > 0 Then
            fst = True : prX = 0 : prY = 0
            Using pp As New Pen(Color.FromArgb(216, 90, 48), 2)
                For i = 0 To nPred - 1
                    cx = GX(predPts(i).T, tMn, tMx, W) : cy = GY(predPts(i).Az, azMn, azMx, H)
                    If Not fst Then g.DrawLine(pp, prX, prY, cx, cy)
                    fst = False : prX = cx : prY = cy
                    g.DrawEllipse(pp, cx - 5, cy - 5, 10, 10)
                Next i
            End Using
            fst = True : prX = 0 : prY = 0
            Using pp As New Pen(Color.FromArgb(186, 117, 23), 2)
                For i = 0 To nPred - 1
                    cx = GX(predPts(i).T, tMn, tMx, W) : cy = GY(predPts(i).El, elMn, elMx, H)
                    If Not fst Then g.DrawLine(pp, prX, prY, cx, cy)
                    fst = False : prX = cx : prY = cy
                    g.DrawEllipse(pp, cx - 5, cy - 5, 10, 10)
                Next i
            End Using
            ' Cerchio NEXT Az e El
            cx = GX(predPts(0).T, tMn, tMx, W) : cy = GY(predPts(0).Az, azMn, azMx, H)
            Using rp As New Pen(Color.Red)
                g.DrawEllipse(rp, cx - 9, cy - 9, 18, 18)
            End Using
            cx = GX(predPts(0).T, tMn, tMx, W) : cy = GY(predPts(0).El, elMn, elMx, H)
            Using rp As New Pen(Color.FromArgb(180, 0, 100))
                g.DrawEllipse(rp, cx - 9, cy - 9, 18, 18)
            End Using
        End If

        g.Dispose()
        picGrafico.Image?.Dispose()
        picGrafico.Image = bmp
    End Sub

    ' ── disegna proiezione Az/El ──────────────────────────────────────────────────
    Private Sub DisegnaOrbita()
        Dim pts() As TPunto = Nothing
        Dim n As Integer
        EstraiBuffer(pts, n)
        If n < 2 Then Exit Sub

        Dim W As Integer = picOrbita.ClientSize.Width
        Dim H As Integer = picOrbita.ClientSize.Height
        If W <= 0 OrElse H <= 0 Then Exit Sub

        Dim bmp As New Bitmap(W, H)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.Clear(Color.White)
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim azMn As Double = pts(0).Az : Dim azMx As Double = pts(0).Az
        Dim elMn As Double = pts(0).El : Dim elMx As Double = pts(0).El
        Dim i As Integer
        For i = 1 To n - 1
            If pts(i).Az < azMn Then azMn = pts(i).Az
            If pts(i).Az > azMx Then azMx = pts(i).Az
            If pts(i).El < elMn Then elMn = pts(i).El
            If pts(i).El > elMx Then elMx = pts(i).El
        Next i
        If nPred > 0 Then
            For i = 0 To nPred - 1
                If predPts(i).Az < azMn Then azMn = predPts(i).Az
                If predPts(i).Az > azMx Then azMx = predPts(i).Az
                If predPts(i).El < elMn Then elMn = predPts(i).El
                If predPts(i).El > elMx Then elMx = predPts(i).El
            Next i
        End If
        azMn -= 0.2 : azMx += 0.2
        elMn -= 0.2 : elMx += 0.2

        ' Assi e titoli
        Using gp As New Pen(Color.FromArgb(180, 180, 180))
            g.DrawLine(gp, 45, H - 28, W - 10, H - 28)
            g.DrawLine(gp, 45, 18, 45, H - 28)
        End Using
        Using fnt As New Font("Arial", 7)
            g.DrawString("Proiezione Az/El (figura 8)",
                         fnt, New SolidBrush(Color.FromArgb(60, 60, 60)),
                         W \ 2 - 50, 4)
            g.DrawString("Azimuth " & Chr(176),
                         fnt, New SolidBrush(Color.FromArgb(120, 120, 120)),
                         W \ 2 - 30, H - 20)
        End Using

        ' Traiettoria storica (grigio)
        Dim cx As Integer, cy As Integer
        Dim prX As Integer = 0, prY As Integer = 0, fst As Boolean = True
        Using hp As New Pen(Color.FromArgb(160, 160, 160))
            For i = 0 To n - 1
                cx = GX(pts(i).Az, azMn, azMx, W) : cy = GY(pts(i).El, elMn, elMx, H)
                If Not fst Then g.DrawLine(hp, prX, prY, cx, cy)
                fst = False : prX = cx : prY = cy
            Next i
        End Using

        ' Predizioni
        If nPred > 0 Then
            fst = True : prX = 0 : prY = 0
            Using pp As New Pen(Color.FromArgb(216, 90, 48), 2)
                For i = 0 To nPred - 1
                    cx = GX(predPts(i).Az, azMn, azMx, W)
                    cy = GY(predPts(i).El, elMn, elMx, H)
                    If Not fst Then g.DrawLine(pp, prX, prY, cx, cy)
                    fst = False : prX = cx : prY = cy
                    g.DrawEllipse(pp, cx - 4, cy - 4, 8, 8)
                Next i
            End Using
            ' Cerchio NEXT
            cx = GX(predPts(0).Az, azMn, azMx, W)
            cy = GY(predPts(0).El, elMn, elMx, H)
            Using np As New Pen(Color.FromArgb(24, 95, 165), 2)
                g.DrawEllipse(np, cx - 11, cy - 11, 22, 22)
            End Using
            Using fnt As New Font("Arial", 7)
                g.DrawString("NEXT", fnt,
                             New SolidBrush(Color.FromArgb(24, 95, 165)),
                             cx + 14, cy - 8)
            End Using
        End If

        g.Dispose()
        picOrbita.Image?.Dispose()
        picOrbita.Image = bmp
    End Sub

    ' ── coordinate grafiche ───────────────────────────────────────────────────────
    Private Function GX(v As Double, mn As Double, mx As Double, W As Integer) As Integer
        If mx = mn Then Return 45
        Return CInt(45 + (v - mn) / (mx - mn) * (W - 60))
    End Function

    Private Function GY(v As Double, mn As Double, mx As Double, h As Integer) As Integer
        If mx = mn Then Return (h - 28) \ 2
        Return CInt((h - 28) - (v - mn) / (mx - mn) * (h - 50))
    End Function

    ' ── log e status ─────────────────────────────────────────────────────────────
    Private Sub LogMsg(msg As String)
        If TxtLog.Text.Length > 10000 Then
            TxtLog.Text = TxtLog.Text.Substring(TxtLog.Text.Length - 5000)
        End If
        TxtLog.Text &= "[" & Now.ToString("HH:mm:ss") & "] " & msg & vbCrLf
        TxtLog.SelectionStart = TxtLog.Text.Length
        TxtLog.ScrollToCaret()
    End Sub

    Private Sub SetStatus(msg As String)
        LblStatus.Text = "  " & msg
        LblStatus.Refresh()
    End Sub

    Private Function BarraConf(v As Integer) As String
        Dim s As String = "["
        Dim i As Integer
        For i = 1 To 10
            s &= If(i <= v \ 10, "|", ".")
        Next i
        Return s & "]"
    End Function

    ' ── helper conversione ────────────────────────────────────────────────────────
    Private Function ValDbl(s As String, def As Double) As Double
        Return If(IsNumeric(s), CDbl(s), def)
    End Function

    Private Function ValLong(s As String, def As Long) As Long
        Return If(IsNumeric(s), CLng(s), def)
    End Function

    ' ── eventi config (VB6 _Change → _TextChanged) ───────────────────────────────
    Private Sub TxtDecim_TextChanged(sender As Object, e As EventArgs) _
            Handles TxtDecim.TextChanged
        AggiornaCfgInfo()
    End Sub

    Private Sub TxtBufOre_TextChanged(sender As Object, e As EventArgs) _
            Handles TxtBufOre.TextChanged
        AggiornaCfgInfo()
    End Sub

    Private Sub TxtRifitOgni_TextChanged(sender As Object, e As EventArgs) _
            Handles TxtRifitOgni.TextChanged
        AggiornaCfgInfo()
    End Sub

    Private Sub TxtElevazione_KeyDown(sender As Object, e As KeyEventArgs) _
            Handles TxtElevazione.KeyDown
        If e.KeyCode = Keys.Return Then BtnFeed_Click(sender, EventArgs.Empty)
    End Sub

    ' ── ridimensionamento (VB6 ScaleWidth twips → ClientSize pixels) ──────────────
    Private Sub Form_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then Exit Sub
        Dim cw As Integer = Me.ClientSize.Width
        Dim ch As Integer = Me.ClientSize.Height
        ' Soglia ~8000×7000 twips convertita in pixel (1 twip ≈ 1/15 px a 96dpi)
        If cw < 533 OrElse ch < 467 Then Exit Sub
        picGrafico.Width = CInt(cw * 0.655) - 8
        picOrbita.Left   = CInt(cw * 0.655)
        picOrbita.Width  = cw - CInt(cw * 0.655) - 4
        fraLog.Width     = cw - 8
        LblStatus.Width  = cw
        LblStatus.Top    = ch - 20
    End Sub

    ' ── cleanup ──────────────────────────────────────────────────────────────────
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            picGrafico.Image?.Dispose()
            picOrbita.Image?.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

End Class
