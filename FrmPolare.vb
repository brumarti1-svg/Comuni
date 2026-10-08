Imports System.Data.SQLite
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class FrmPolare

    Const MaxPunti As Integer = 6000
    Const nCerchi As Integer = 10
    Const nAngoli As Integer = 24
    Const DimCornice As Integer = 9
    Dim NPAsseY As Single
    Dim NPAsseX As Single
    Dim CentroX As Single
    Dim CentroY As Single
    Dim Angolo As Double
    Dim GrafAngolo As Integer
    Dim GrafAngolo1 As Integer
    Dim Intervallo As Single
    Dim RapportoXY As Single
    Dim Raggio As Single
    Dim OraIni As String
    Dim OraFin As String
    Dim DataIni As Date
    Dim DataFin As Date
    Dim tmpDataIni As Date
    Dim tmpDataFin As Date
    Dim Punti(1, 1, MaxPunti) As PuntoGrafico
    Dim NPunti As Integer
    Dim MaxY As Single, MinY As Single
    Dim MaxX As Single, MinX As Single
    Dim tmpUnita As String
    Dim ScalaX As Single, ScalaY As Single
    Dim tmp_Azi As Single, tmp_Ele As Single
    Dim i As Integer, j As Integer, n As Integer
    Dim tmpData As Date, tmpAz As Single
    Dim PassoX As Single, PassoY As Single
    Dim XPrec As Single, YPrec As Single, DataPrec As Single
    Dim tmp2x As Single, tmp2y As Single
    Dim YProv As Single, XProv As Single, DataProv As Single
    Dim tmpSelezione As Integer
    Dim flgGrafico As Integer
    Dim diffX As Single
    Dim tmpSeleziona As Boolean
    Dim diff As Single, i_Intervallo As Integer
    Private _primoPunto As Boolean = True

    ' Stato di disegno GDI+ (sostituisce il disegno diretto su VB6 PictureBox)
    Private DyVisual As SQLiteDataReader
    Private _Cmd As SQLiteCommand
    Private tmpSQL As String
    Private _canvas As Bitmap
    Private _g As Graphics
    Private _penColor As Color = Color.White
    Private _penWidth As Single = 1
    Private _fillStyle As Integer = 1   ' 1=trasparente, 0=solido
    Private _fontSize As Single = 8
    Private _currentX As Single         ' equivalente di picGrafico.CurrentX
    Private _currentY As Single         ' equivalente di picGrafico.CurrentY

    ' -----------------------------------------------------------------------
    ' Gestione canvas (backing bitmap per il disegno GDI+)
    ' -----------------------------------------------------------------------

    Private Sub InitCanvas()
        If _g IsNot Nothing Then _g.Dispose()
        If _canvas IsNot Nothing Then _canvas.Dispose()
        Dim w As Integer = If(picGrafico.ClientSize.Width > 0, picGrafico.ClientSize.Width, 600)
        Dim h As Integer = If(picGrafico.ClientSize.Height > 0, picGrafico.ClientSize.Height, 500)
        _canvas = New Bitmap(w, h)
        _g = Graphics.FromImage(_canvas)
        _g.SmoothingMode = SmoothingMode.AntiAlias
        _g.Clear(Color.White)
        picGrafico.Image = _canvas
        ' Ricalcola geometria polare sulle dimensioni reali
        NPAsseX = w
        NPAsseY = h
        CentroX = w / 2.0F
        CentroY = h / 2.0F
        Raggio = (Math.Min(w, h) / 2.0F) - 20.0F
        Intervallo = Raggio / nCerchi
    End Sub

    Private Sub ClearCanvas()
        _g?.Clear(Color.White)
        picGrafico.Refresh()
    End Sub

    ' -----------------------------------------------------------------------
    ' Metodi di disegno GDI+ (mappano i metodi VB6 di PictureBox)
    ' -----------------------------------------------------------------------

    ' VB6: picGrafico.Circle (cx, cy), r
    Private Sub GfxCircle(cx As Single, cy As Single, r As Single)
        If r <= 0 Then Return
        If _fillStyle = 0 Then
            Using brush As New SolidBrush(_penColor)
                _g.FillEllipse(brush, cx - r, cy - r, r * 2, r * 2)
            End Using
        End If
        Using pen As New Pen(_penColor, _penWidth)
            _g.DrawEllipse(pen, cx - r, cy - r, r * 2, r * 2)
        End Using
    End Sub

    ' VB6: picGrafico.Line (x1, y1)-(x2, y2)
    Private Sub GfxLine(x1 As Single, y1 As Single, x2 As Single, y2 As Single)
        Using pen As New Pen(_penColor, _penWidth)
            _g.DrawLine(pen, x1, y1, x2, y2)
        End Using
        _currentX = x2
        _currentY = y2
    End Sub

    ' VB6: picGrafico.Line -(x2, y2)  (da posizione corrente)
    Private Sub GfxLineTo(x2 As Single, y2 As Single)
        GfxLine(_currentX, _currentY, x2, y2)
    End Sub

    ' VB6: picGrafico.Print str  (alla posizione CurrentX/CurrentY)
    Private Sub GfxPrint(text As String)
        Using brush As New SolidBrush(_penColor)
            Using f As New Font(picGrafico.Font.FontFamily, _fontSize, GraphicsUnit.Point)
                _g.DrawString(text, f, brush, _currentX, _currentY)
            End Using
        End Using
    End Sub

    ' -----------------------------------------------------------------------
    ' Logica applicativa
    ' -----------------------------------------------------------------------

    ' Legge il filtro Dal/Al dalle maschere e lo mette in tmpDataIni/
    ' tmpDataFin. Se un campo è vuoto o incompleto (l'utente non lo ha
    ' ancora impostato, o lo sta modificando), non si filtra su quel lato
    ' invece di far fallire la lettura.
    Private Sub LeggiFiltroDalAl()
        Dim dIni As Date = Date.MinValue, dFin As Date = Date.MaxValue
        Dim tsIni As TimeSpan = TimeSpan.Zero, tsFin As TimeSpan = TimeSpan.FromHours(23.99)
        If IsDate(mskDataIni.Text) Then dIni = CDate(mskDataIni.Text)
        If IsDate(mskDataFin.Text) Then dFin = CDate(mskDataFin.Text)
        If mskOraIni.MaskCompleted Then TimeSpan.TryParse(mskOraIni.Text.Replace("."c, ":"c) & ":00", tsIni)
        If mskOraFin.MaskCompleted Then TimeSpan.TryParse(mskOraFin.Text.Replace("."c, ":"c) & ":00", tsFin)
        tmpDataIni = If(dIni = Date.MinValue, Date.MinValue, dIni.Add(tsIni))
        tmpDataFin = If(dFin = Date.MaxValue, Date.MaxValue, dFin.Add(tsFin))
    End Sub

    Private Function SelezionaSingoliDati() As Boolean
        _primoPunto = True
        MaxY = -99999
        MinY = 99999
        MaxX = -99999
        MinX = 99999
        LeggiFiltroDalAl()
        tmpSQL = "SELECT * FROM [tmpEffemeridi] ORDER BY " &
                 "substr(date,7,4)||substr(date,4,2)||substr(date,1,2), time"

        _Cmd?.Dispose()
        DyVisual?.Close()
        _Cmd = New SQLiteCommand(tmpSQL, dbOrbite.Connection)
        DyVisual = _Cmd.ExecuteReader()

        If Not DyVisual.HasRows Then Return False

        DyVisual.Read()
        NPunti = 1
        XPrec = CSng(DyVisual("Azimuth"))


        ' Riapre per iterare dall'inizio
        DyVisual.Close()
        DyVisual = _Cmd.ExecuteReader()

        Dim _dtPrec As Date = Date.MinValue
        Do While DyVisual.Read()
            Dim _sDate = DyVisual("date").ToString().Split(" "c)(0)
            Dim _sTime = DyVisual("Time").ToString()
            Dim _dtRow As Date = Date.MinValue
            Date.TryParse(_sDate, _dtRow)
            Dim _ts As TimeSpan = TimeSpan.Zero
            TimeSpan.TryParse(_sTime.Replace(".", ":").Replace(",", ":"), _ts)
            Dim dtCurr As Date = _dtRow.Date + _ts
            ' Filtro Dal/Al impostato dall'operatore (default: tutta la
            ' tabella, se i campi sono vuoti/incompleti).
            If dtCurr < tmpDataIni OrElse dtCurr > tmpDataFin Then Continue Do
            Punti(1, 1, NPunti).data = dtCurr
            Punti(1, 1, NPunti).y = CSng(DyVisual("Elevation"))
            Punti(1, 1, NPunti).X = CSng(DyVisual("Azimuth"))
            XProv = CSng(DyVisual("Azimuth"))
            diffX = Math.Abs(XProv - XPrec)

            ' Un'effemeride su 24 ore di un satellite basso contiene più
            ' passaggi separati (orbita ogni ~101 minuti, visibile solo per
            ' una decina di minuti a volta): tra un passaggio e il
            ' successivo c'è un salto di decine di minuti, che non va
            ' collegato con una linea retta attraverso tutto il grafico.
            ' Soglia a 1 minuto: più del normale passo di campionamento
            ' all'interno di un singolo passaggio, meno del distacco tra
            ' due passaggi diversi.
            If _dtPrec <> Date.MinValue AndAlso Math.Abs((dtCurr - _dtPrec).TotalSeconds) > 60 Then
                _primoPunto = True
            End If
            _dtPrec = dtCurr
            DataProv = CSng(dtCurr.Minute + dtCurr.Second / 100.0)
            OraIni = dtCurr.Hour.ToString("D2") & "." & dtCurr.Minute.ToString("D2")
            tmp_Azi = CSng(DyVisual("Azimuth"))
            tmp_Ele = CSng(DyVisual("Elevation"))
            VisualizzaPunto()
            If Punti(1, 1, NPunti).y > MaxY Then MaxY = Int(Punti(1, 1, NPunti).y * 10) / 10 + 0.1F
            If Punti(1, 1, NPunti).y < MinY Then MinY = Int(Punti(1, 1, NPunti).y * 10) / 10
            If Punti(1, 1, NPunti).X > MaxX Then MaxX = Int(Punti(1, 1, NPunti).X * 10) / 10 + 0.1F
            If Punti(1, 1, NPunti).X < MinX Then MinX = Int(Punti(1, 1, NPunti).X * 10) / 10
            NPunti += 1
            If NPunti > MaxPunti Then Exit Do
        Loop
        picGrafico.Refresh()
        Return True
    End Function

    Private Sub VisualizzaAsseX()
        Dim X1 As Single, Y1 As Single
        Dim Raggio1 As Integer

        _penWidth = 1
        _penColor = Color.FromArgb(192, 192, 192)

        ' Cerchi concentrici di elevazione
        For i = 1 To nCerchi
            GfxCircle(CentroX, CentroY, Intervallo * i)
        Next i

        ' Raggi azimutali + etichette dei gradi
        GrafAngolo = 0
        Angolo = GradiRad * GrafAngolo
        For i = 0 To 24
            GrafAngolo1 = 90 - Math.Abs(GrafAngolo)
            If GrafAngolo1 < 0 Then GrafAngolo1 = 360 + GrafAngolo1

            X1 = CSng(Math.Cos(Angolo) * Raggio + CentroX)
            If X1 < 0 Then X1 = CentroX + X1
            Y1 = CSng(Math.Sin(Angolo) * Raggio + CentroY)
            If Y1 < 0 Then Y1 = CentroY + Y1

            _penColor = Color.FromArgb(192, 192, 192)
            GfxLine(CentroX, CentroY, X1, Y1)

            ' Offset etichetta in base al quadrante
            Raggio1 = CInt(Raggio)
            If GrafAngolo1 >= 0 AndAlso GrafAngolo1 < 45 Then
                Raggio1 += 3
            ElseIf GrafAngolo1 >= 45 AndAlso GrafAngolo1 < 90 Then
                Raggio1 = CInt(Raggio) + 1
            ElseIf GrafAngolo1 > 180 AndAlso GrafAngolo1 < 225 Then
                Raggio1 = CInt(Raggio) + 1
            ElseIf GrafAngolo1 >= 225 AndAlso GrafAngolo1 < 255 Then
                Raggio1 = CInt(Raggio) + 3
            ElseIf GrafAngolo1 >= 255 AndAlso GrafAngolo1 < 330 Then
                Raggio1 = CInt(Raggio) + 5
            ElseIf GrafAngolo1 >= 330 Then
                Raggio1 = CInt(Raggio) + 4
            End If

            X1 = CSng(Math.Cos(Angolo) * Raggio1 + CentroX)
            If X1 < 0 Then X1 = CentroX + X1
            Y1 = CSng(Math.Sin(Angolo) * Raggio1 + CentroY)
            If Y1 < 0 Then Y1 = CentroY + Y1
            _currentX = X1
            _currentY = Y1
            _fontSize = 8
            _penColor = Color.Black
            GfxPrint(Format(GrafAngolo1, "000"))

            GrafAngolo -= 15
            Angolo = GradiRad * GrafAngolo
        Next i

        ' Etichette di elevazione sui cerchi (90, 80, 70, ...)
        _fontSize = 6
        Angolo = GradiRad * 90
        Dim radiusMark As Single = (Intervallo * 2) - 3   ' local: GrafAngolo riusato nel VB6 originale
        GrafAngolo1 = 80
        For i = 1 To nCerchi - 1
            X1 = CSng(Math.Cos(Angolo) * radiusMark + CentroX)
            If X1 < 0 Then X1 = CentroX + X1
            Y1 = CSng(Math.Sin(Angolo) * radiusMark + CentroY)
            If Y1 < 0 Then Y1 = CentroY + Y1
            _currentX = X1
            _currentY = Y1
            GfxPrint(Format(GrafAngolo1, "00"))
            radiusMark += Intervallo
            GrafAngolo1 -= 10
        Next i

        picGrafico.Refresh()
    End Sub

    Private Sub CmdElabora()
        If _g Is Nothing Then
            InitCanvas()
        End If
        VisualizzaGrafico()
        If g_GrafPol Then SelezionaSingoliDati()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub VisualizzaGrafico()
        ClearCanvas()
        VisualizzaAsseX()
    End Sub

    Private Sub VisualizzaPunto()
        Dim tmpX1 As Single, tmpY1 As Single
        Dim GrafAng As Single, GrafAng1 As Single
        Dim Raggio1 As Single

        _penWidth = 1
        _penColor = Color.White

        If Not IsNumeric(tmp_Azi) Then Exit Sub
        GrafAng = tmp_Azi - 90
        Angolo = GradiRad * GrafAng
        GrafAng1 = 90 - Math.Abs(GrafAng)
        If GrafAng1 < 0 Then GrafAng1 = 360 - GrafAng1

        If Not IsNumeric(tmp_Ele) Then Exit Sub
        tmpY1 = tmp_Ele
        If tmpY1 < 0 Then tmpY1 = CentroY + tmpY1
        Raggio1 = Raggio - (Raggio / 100 * tmpY1)

        Dim d_Calcolo As Double = DataProv
        If d_Calcolo < DataPrec Then d_Calcolo = DataProv + 60

        tmpX1 = CSng(Math.Cos(Angolo) * Raggio1 + CentroX)
        tmpY1 = CSng(Math.Sin(Angolo) * Raggio1 + CentroY)

        ' Linea dal punto precedente al corrente (sempre, eccetto il primo)
        _penWidth = 0.5F
        _penColor = Color.Black
        If Not _primoPunto Then
            GfxLineTo(tmpX1, tmpY1)
        Else
            _currentX = tmpX1
            _currentY = tmpY1
            _primoPunto = False
        End If

        ' Punto pieno
        _fillStyle = 0
        GfxCircle(tmpX1, tmpY1, 2.0F)
        _fillStyle = 1

        ' Etichetta oraria sul punto
        _currentX = tmpX1 + 4
        _currentY = tmpY1 - 8
        _fontSize = 7
        _penColor = Color.Black
        GfxPrint(OraIni)

        _currentX = tmpX1
        _currentY = tmpY1
        DataPrec = DataProv
    End Sub

    ' -----------------------------------------------------------------------
    ' Event handlers
    ' -----------------------------------------------------------------------

    Private Sub FrmPolare_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Le dimensioni reali arrivano in Shown (dopo il Maximize)
    End Sub

    Private Sub FrmPolare_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' Il filtro Dal/Al ha senso solo per il grafico statico/archivio
        ' (g_GrafPol=True): in modalità real-time (Trace Orbit) i punti
        ' arrivano dal timer via s_Az/s_El correnti, non da una lettura
        ' filtrata di tmpEffemeridi.
        mskDataIni.Enabled = g_GrafPol
        mskOraIni.Enabled = g_GrafPol
        mskDataFin.Enabled = g_GrafPol
        mskOraFin.Enabled = g_GrafPol
        BtnFiltra.Enabled = g_GrafPol
        If g_GrafPol Then
            ' Default: tutta la giornata odierna (i dati possono coprire
            ' più passaggi nelle 24 ore, come per un'effemeride).
            mskDataIni.Text = NowUTC().ToString("ddMMyyyy")
            mskOraIni.Text = "0000"
            mskDataFin.Text = NowUTC().ToString("ddMMyyyy")
            mskOraFin.Text = "2359"
        End If
        InitCanvas()
        CmdElabora()
    End Sub

    Private Sub FrmPolare_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ' Abilita il timer real-time se non in modalità grafico statico
        If Not g_GrafPol Then
            tmrPolare.Interval = If(i_Inter > 0, i_Inter * 1000, 1000)
            tmrPolare.Enabled = True
        End If
    End Sub

    ' Rielabora il grafico con il filtro Dal/Al corrente (solo modalità
    ' statica/archivio: il pulsante resta disabilitato in real-time).
    Private Sub BtnFiltra_Click(sender As Object, e As EventArgs) Handles BtnFiltra.Click
        CmdElabora()
    End Sub

    Private Sub CmdEsci_Click(sender As Object, e As EventArgs) Handles CmdEsci.Click
        tmrPolare.Enabled = False
        Me.Close()
    End Sub

    Private Sub TmrPolare_Tick(sender As Object, e As EventArgs) Handles tmrPolare.Tick
        tmrPolare.Enabled = True
        If IsNumeric(s_Az) Then
            tmp_Azi = CSng(CDbl(Mid(s_Az, 1, 6)) / 1000)
        Else
            Exit Sub
        End If
        If IsNumeric(s_El) Then
            tmp_Ele = CSng(CDbl(Mid(s_El, 1, 5)) / 1000)
        Else
            Exit Sub
        End If
        VisualizzaPunto()
        picGrafico.Refresh()
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        _g?.Dispose()
        _canvas?.Dispose()
        MyBase.OnFormClosed(e)
    End Sub

End Class
