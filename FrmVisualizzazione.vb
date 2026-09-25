Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Data.SQLite

Public Class FrmVisualizzazione

    ' ── campi dati ─────────────────────────────────────────────────────────────
    Private DyVisual As SQLiteDataReader
    Private _Cmd As SQLiteCommand
    Private tmpSQL As String
    Private Const MaxPunti As Integer = 6000
    Private NPAsseY As Single
    Private NPAsseX As Single
    Private Const DimCornice As Integer = 9   ' margine destra e top
    Private Const ML As Integer = 45          ' margine sinistro (etichette Y)
    Private Const MB As Integer = 30          ' margine inferiore (etichette X)
    Private Intervallo As Integer

    Private OraIni As String, OraFin As String
    Private DataIni As Date, DataFin As Date
    Private tmpDataIni As Date, tmpDataFin As Date
    ' Dim 1..3: indici 0-1 (il codice usa indice 1); Dim 4: 0..MaxPunti (si usa da 1)
    Private Punti(1, 1, 1, MaxPunti) As PuntoGrafico
    Private NPunti As Integer
    Private MaxY As Single, MinY As Single
    Private MaxX As Single, MinX As Single
    Private tmpUnita As String
    Private ScalaX As Single, ScalaY As Single
    Private i As Integer, j As Integer, n As Integer
    Private tmpData As Date, tmpAz As Single
    Private PassoX As Single, PassoY As Single
    Private XPrec As Single, YPrec As Single, DataPrec As Date
    Private tmpX As Single, tmpY As Single, tmp2x As Single, tmp2y As Single
    Private YProv As Single, XProv As Single, DataProv As Date
    Private flgGrafico As Integer
    Private diffX As Single
    Private tmpSelezione As Boolean
    Private diff As Single, i_Intervallo As Single
    Private OraPrec As Integer, OraProv As Integer
    Private InvertiScala As Boolean
    Private i_Size1 As Integer, i_Size2 As Integer, i_Size3 As Integer

    ' ── stato GDI+ ──────────────────────────────────────────────────────────────
    Private _bmp As Bitmap
    Private _g As Graphics
    Private _cx As Single
    Private _cy As Single
    Private _penColor As Color = Color.Black
    Private _penWidth As Single = 1
    Private _dashStyle As DashStyle = DashStyle.Solid
    Private _fillSolid As Boolean = False

    Private ReadOnly Property PicW As Integer
        Get
            Return If(_bmp IsNot Nothing, _bmp.Width, picGrafico.ClientSize.Width)
        End Get
    End Property

    Private ReadOnly Property PicH As Integer
        Get
            Return If(_bmp IsNot Nothing, _bmp.Height, picGrafico.ClientSize.Height)
        End Get
    End Property

    ' ── GDI+ helpers ────────────────────────────────────────────────────────────

    Private Sub InitBitmap()
        _g?.Dispose()
        _bmp?.Dispose()
        _bmp = New Bitmap(picGrafico.ClientSize.Width, picGrafico.ClientSize.Height)
        _g = Graphics.FromImage(_bmp)
        _g.Clear(Color.White)
        _g.SmoothingMode = SmoothingMode.AntiAlias
    End Sub

    Private Sub RefreshPic()
        picGrafico.Image = _bmp
    End Sub

    Private Function MakePen() As Pen
        Dim p As New Pen(_penColor, _penWidth)
        p.DashStyle = _dashStyle
        Return p
    End Function

    Private Sub MoveTo(x As Single, y As Single)
        _cx = x : _cy = y
    End Sub

    ' Linea da posizione corrente a coordinate assolute  (VB6: Line -(x,y))
    Private Sub LineTo(x As Single, y As Single)
        Using p = MakePen()
            _g.DrawLine(p, _cx, _cy, x, y)
        End Using
        _cx = x : _cy = y
    End Sub

    ' Linea da posizione corrente per incremento  (VB6: Line -Step(dx,dy))
    Private Sub LineStep(dx As Single, dy As Single)
        LineTo(_cx + dx, _cy + dy)
    End Sub

    ' Linea assoluta  (VB6: Line (x1,y1)-(x2,y2))
    Private Sub LineAbs(x1 As Single, y1 As Single, x2 As Single, y2 As Single)
        Using p = MakePen()
            _g.DrawLine(p, x1, y1, x2, y2)
        End Using
        _cx = x2 : _cy = y2
    End Sub

    ' Cerchio  (VB6: Circle (x,y), radius)
    Private Sub DrawCircle(x As Single, y As Single, radius As Single)
        Dim r As New RectangleF(x - radius, y - radius, 2 * radius, 2 * radius)
        If _fillSolid Then
            Using b As New SolidBrush(_penColor)
                _g.FillEllipse(b, r)
            End Using
        Else
            Using p = MakePen()
                _g.DrawEllipse(p, r)
            End Using
        End If
    End Sub

    ' Stringa a posizione corrente  (VB6: picGrafico.Print)
    Private Sub DrawStr(text As String, fontSize As Integer)
        Using fnt As New Font("Arial", fontSize)
            Using b As New SolidBrush(_penColor)
                _g.DrawString(text, fnt, b, _cx, _cy)
            End Using
        End Using
    End Sub

    ' ── helper per parsing ore dalla MaskedTextBox ───────────────────────────────
    ' mskOraIni/Fin contiene "HH.mm" — restituisce "HH:mm:ss" per la query SQL
    ' Normalizza "HH.MM.SS" o "HH,MM,SS" o "HH:MM:SS" → TimeSpan.
    ' In locale italiana, MaskedTextBox con mask "00.00.00" usa "," come separatore.
    Private Function OraToTimeSpan(ora As String) As TimeSpan
        Try
            Dim s = ora.Trim().Replace(",", ".").Replace(":", ".")
            Dim parts = s.Split("."c)
            Dim h As Integer = If(parts.Length > 0 AndAlso IsNumeric(parts(0)), CInt(Val(parts(0))), 0)
            Dim m As Integer = If(parts.Length > 1 AndAlso IsNumeric(parts(1)), CInt(Val(parts(1))), 0)
            Dim sec As Integer = If(parts.Length > 2 AndAlso IsNumeric(parts(2)), CInt(Val(parts(2))), 0)
            Return New TimeSpan(h, m, sec)
        Catch
            Return TimeSpan.Zero
        End Try
    End Function

    Private Function ParseOraToSQL(ora As String) As String
        Try
            Dim ts = OraToTimeSpan(ora)
            Return ts.Hours.ToString("00") & ":" & ts.Minutes.ToString("00") & ":00"
        Catch
            Return "00:00:00"
        End Try
    End Function

    ' ── logica ──────────────────────────────────────────────────────────────────

    Private Function ControllaDati() As Boolean
        If Not IsDate(mskDataIni.Text) Then
            mskDataIni.Focus() : Return False
        End If
        If Not IsDate(mskDataFin.Text) Then
            mskDataFin.Focus() : Return False
        End If
        If Val(mskOraIni.Text) < 0 OrElse Val(mskOraIni.Text) >= 24 Then
            mskOraIni.Focus() : Return False
        End If
        If Val(mskOraFin.Text) < 0 OrElse Val(mskOraFin.Text) >= 24 Then
            mskOraFin.Focus() : Return False
        End If
        Dim dtIni As Date = CDate(mskDataIni.Text) + OraToTimeSpan(mskOraIni.Text)
        Dim dtFin As Date = CDate(mskDataFin.Text) + OraToTimeSpan(mskOraFin.Text)
        If dtIni >= dtFin Then
            mskDataIni.Focus() : Return False
        End If
        If (dtFin - dtIni).TotalMinutes < 30 Then
            mskOraFin.Focus() : Return False
        End If
        Return True
    End Function

    Private Function Tronca(Numero As Single) As Single
        Dim t As Single = Math.Abs(Numero)
        If t >= 10 Then
            t = If(Numero >= 0, Int(t / 10) * 10, (Int(t / 10) + 1) * 10)
        Else
            t = If(Numero >= 0, 0, t + 1)
        End If
        Return If(Numero >= 0, CInt(t), -CInt(t))
    End Function

    Private Function SelezionaSingoliDati() As Boolean
        ' VisualizzaGrafico() viene richiamato anche da Form_Resize/ResizeEnd:
        ' se nel frattempo l'utente ha lasciato una maschera data incompleta,
        ' IsDate evita il CDate che altrimenti solleverebbe InvalidCastException.
        If Not IsDate(mskDataIni.Text) OrElse Not IsDate(mskDataFin.Text) Then Return False
        InvertiScala = False
        MaxY = -99999 : MinY = 99999
        MaxX = -99999 : MinX = 99999

        OraIni = mskOraIni.Text
        OraFin = mskOraFin.Text
        DataIni = CDate(mskDataIni.Text)
        DataFin = CDate(mskDataFin.Text)

        ' Costruisce tmpDataIni/Fin sommando la data con la componente oraria.
        ' OraToTimeSpan gestisce "HH.MM", "HH,MM" e "HH:MM" (separatori variabili per locale).
        tmpDataIni = DataIni.Add(OraToTimeSpan(OraIni))
        tmpDataFin = DataFin.Add(OraToTimeSpan(OraFin))

        ' SQLite: ordina per data convertita in formato YYYYMMDD (substr di "dd/mm/yyyy")
        ' I record fuori dall'intervallo [tmpDataIni, tmpDataFin] vengono saltati nel loop.
        tmpSQL = "SELECT * FROM [tmpEffemeridi] ORDER BY " &
                 "substr(date,7,4)||substr(date,4,2)||substr(date,1,2), time"

        _Cmd?.Dispose()
        DyVisual?.Close()
        _Cmd = New SQLiteCommand(tmpSQL, dbOrbite.Connection)
        DyVisual = _Cmd.ExecuteReader()

        If Not DyVisual.HasRows Then Return False

        DyVisual.Read()
        i_Intervallo = CSng(DyVisual("Intervallo"))
        NPunti = 1
        XPrec = CSng(DyVisual("Azimuth"))
        InvertiScala = False

        ' Riapre per iterare dall'inizio
        DyVisual.Close()
        DyVisual = _Cmd.ExecuteReader()

        Do While DyVisual.Read()
            ' data As Date: costruisce la data dal campo "date" (es. "20/05/2026" o DateTime)
            ' e dal campo "Time" ("08:45"). Gestisce entrambe le rappresentazioni SQLite.
            Dim _sDate = DyVisual("date").ToString().Split(" "c)(0)  ' "20/05/2026" o "2026-05-20"
            Dim _sTime = DyVisual("Time").ToString()                  ' "08:45"
            Dim _dtRow As Date = Date.MinValue
            Date.TryParse(_sDate, _dtRow)
            Dim _dtFull As Date = _dtRow.Date + OraToTimeSpan(_sTime)
            ' Filtro per l'intervallo scelto dall'utente: la query non ha WHERE
            ' (il formato testuale "dd/mm/yyyy" di "date" non è ordinabile/filtrabile
            ' correttamente in SQL), quindi va applicato qui riga per riga.
            If _dtFull < tmpDataIni OrElse _dtFull > tmpDataFin Then Continue Do
            Punti(1, 1, 1, NPunti).data = _dtFull
            Punti(1, 1, 1, NPunti).y = CSng(DyVisual("Elevation"))
            Punti(1, 1, 1, NPunti).X = CSng(DyVisual("Azimuth"))
            XProv = CSng(DyVisual("Azimuth"))
            diffX = Math.Abs(XProv - XPrec)
            If diffX > 270 Then InvertiScala = True
            If Punti(1, 1, 1, NPunti).y > MaxY Then MaxY = Int(Punti(1, 1, 1, NPunti).y * 10) / 10 + 0.1F
            If Punti(1, 1, 1, NPunti).y < MinY Then MinY = Int(Punti(1, 1, 1, NPunti).y * 10) / 10
            If Punti(1, 1, 1, NPunti).X > MaxX Then MaxX = Int(Punti(1, 1, 1, NPunti).X * 10) / 10 + 0.1F
            If Punti(1, 1, 1, NPunti).X < MinX Then MinX = Int(Punti(1, 1, 1, NPunti).X * 10) / 10
            XPrec = XProv
            NPunti += 1
            If NPunti > MaxPunti Then Exit Do
        Loop

        Dim result As Boolean = True

        If InvertiScala Then
            MaxX = -99999 : MinX = 99999
            For i = 1 To NPunti
                XProv = Punti(1, 1, 1, i).X
                If XProv <= 180 Then Punti(1, 1, 1, i).X = Punti(1, 1, 1, i).X + 360
                If Punti(1, 1, 1, i).X > MaxX Then MaxX = Int(Punti(1, 1, 1, i).X * 10) / 10 + 0.1F
                If Punti(1, 1, 1, i).X < MinX Then MinX = Int(Punti(1, 1, 1, i).X * 10) / 10
            Next
        End If

        PassoX = Int((MaxX - MinX) / NPAsseX)
        If Int(MaxX - MinX) > 1 Then
            MinX = Int(MinX) : MaxX = Int(MaxX) + 1
            If PassoX = 0 Then PassoX = 1
        Else
            If PassoX = 0 Then PassoX = 0.1F
        End If

        PassoY = Int((MaxY - MinY) / NPAsseY)
        If Int(MaxY - MinY) > 1 Then
            MinY = Int(MinY) : MaxY = Int(MaxY) + 1
            If PassoY = 0 Then PassoY = 1
        Else
            If PassoY = 0 Then PassoY = 0.1F
        End If

        If Math.Abs(MaxY - MinY) > 10 Or Math.Abs(MaxX - MinX) > 10 Then
            result = False
        End If

        NPunti -= 1
        Return result
    End Function

    ' ── asse X ──────────────────────────────────────────────────────────────────

    Private Sub VisualizzaAsseX()
        Dim PassoProv As Double, tmpAz1 As Double
        PassoProv = PassoX
        If ScalaX = 0 Then Return

        diff = 0
        _dashStyle = DashStyle.Solid
        _penWidth = 1
        _penColor = Color.Black
        ' "Azimuth" centrato nel margine inferiore (sotto la linea asse)
        MoveTo(PicW / 2.0F - 28, PicH - 14)
        DrawStr("Azimuth", i_Size3)

        _penColor = Color.Red
        tmpAz = MinX

        If PassoX / ScalaX >= 1 Then
            Do While tmpAz <= MaxX
                If (tmpAz - MinX) >= 0 Then
                    tmpX = (tmpAz - MinX) / ScalaX + ML
                    tmpY = PicH - MB
                    MoveTo(tmpX, tmpY) : LineStep(0, 3)
                    MoveTo(tmpX, tmpY)

                    tmpAz1 = tmpAz
                    If tmpAz1 >= 360 Then tmpAz1 -= 360
                    If chkPrincipale.Checked Then
                        _penWidth = 1 : _penColor = Color.Gray
                        LineStep(0, -(PicH - DimCornice - MB))
                        _penWidth = 1 : _penColor = Color.Red
                    End If
                    ' Etichette numeri: sotto la linea, nel margine MB (tutti i tick principali)
                    MoveTo((tmpAz - MinX) / ScalaX + ML - 1.5F, PicH - MB + 3)
                    DrawStr(Format(tmpAz1, "##0"), i_Size3)
                End If
                tmpAz += PassoX
            Loop
        End If

        ' Griglia secondaria: 9 linee tra ogni coppia di tick principali (PassoProv/10)
        If chkSecondaria.Checked Then
            _penWidth = 1 : _penColor = Color.Silver
            Dim stepSecX As Single = CSng(PassoProv / 10)
            Dim azSec As Single = MinX
            Do While azSec < MaxX
                For k As Integer = 1 To 9
                    Dim xSec As Single = azSec + k * stepSecX
                    If xSec > MinX AndAlso xSec < MaxX Then
                        tmpX = (xSec - MinX) / ScalaX + ML
                        LineAbs(tmpX, DimCornice, tmpX, PicH - MB)
                    End If
                Next
                azSec += CSng(PassoProv)
            Loop
        End If

        ' Mezzo passo: tick mark sull'asse
        _penColor = Color.Blue
        PassoX = CSng(PassoProv / 2)
        tmpAz = MinX
        If PassoX / ScalaX >= 1 Then
            Do While tmpAz <= MaxX
                If (tmpAz - MinX) >= 0 Then
                    tmpX = (tmpAz - MinX) / ScalaX + ML
                    MoveTo(tmpX, PicH - MB) : LineStep(0, 2)
                    MoveTo(tmpX, PicH - MB)
                End If
                tmpAz += PassoX
            Loop
        End If

        ' Decimi: tick mark sull'asse
        _penColor = Color.Blue
        PassoX = CSng(PassoProv / 10)
        tmpAz = MinX
        If PassoX / ScalaX >= 1 Then
            Do While tmpAz < MaxX
                If (tmpAz - MinX) > 0 Then
                    tmpX = (tmpAz - MinX) / ScalaX + ML
                    MoveTo(tmpX, PicH - MB) : LineStep(0, 2)
                    MoveTo(tmpX, PicH - MB)
                End If
                tmpAz = CSng(Format(tmpAz + PassoX, "#0.##"))
            Loop
        End If

        ' Info date (in alto, dopo il margine sinistro)
        _penColor = Color.Black
        MoveTo(ML + 10, 2) : DrawStr("dal: " & tmpDataIni.ToString("dd/MM/yy HH.mm"), i_Size3)
        MoveTo(ML + 10, i_Size3 + 4) : DrawStr("  al: " & tmpDataFin.ToString("dd/MM/yy HH.mm"), i_Size3)
        ' Linea asse X orizzontale
        LineAbs(ML, PicH - MB - diff, PicW - DimCornice, PicH - MB - diff)
        ' Linea asse Y verticale
        MoveTo(ML, PicH - MB) : LineStep(0, -(PicH - DimCornice - MB))
    End Sub

    ' ── asse Y ──────────────────────────────────────────────────────────────────

    Private Sub VisualizzaAsseY()
        Dim PassoProv As Single
        tmpUnita = "Elevation"
        PassoProv = PassoY
        If ScalaY = 0 Then Return

        _penColor = Color.Black
        _dashStyle = DashStyle.Solid
        ' Linea asse Y verticale
        LineAbs(ML, PicH - MB, ML, DimCornice)
        ' Titolo e legenda nel margine superiore
        MoveTo(2, DimCornice - 6) : DrawStr(tmpUnita, i_Size2)
        _penWidth = 1
        If optLinee.Checked Then
            MoveTo(2, DimCornice + i_Size2 + 3) : LineStep(10, 0)
        Else
            _fillSolid = True : DrawCircle(7, DimCornice + i_Size2 + 6, 1.5F) : _fillSolid = False
        End If

        tmp2y = MinY
        Do While tmp2y <= MaxY
            If tmp2y >= MinY Then
                tmpY = (PicH - MB) - (tmp2y - MinY) / ScalaY
                MoveTo(ML, tmpY)
                Dim showValY = tmp2y <> MinY
                If chkPrincipale.Checked AndAlso showValY Then
                    _penWidth = 1 : _penColor = Color.Gray
                    LineStep(PicW - ML - DimCornice, 0)
                    _penWidth = 1 : _penColor = Color.Black
                End If
                If showValY Then
                    ' Etichette a sinistra: x=2, allineate alla linea della griglia
                    MoveTo(2, tmpY - i_Size2 - 1)
                    _penColor = Color.Black
                    DrawStr(Format(tmp2y, "#0.#"), i_Size2)
                End If
            End If
            tmp2y += PassoY
        Loop

        ' Metà passo: solo tick mark, nessuna linea di griglia
        _penColor = Color.Blue
        PassoY = PassoProv / 2
        tmp2y = MinY
        If PassoY / ScalaY > 1 Then
            Do While tmp2y < MaxY
                If (tmp2y - MinY) > 0 Then
                    tmpY = (PicH - MB) - (tmp2y - MinY) / ScalaY
                    MoveTo(ML - 2, tmpY) : LineStep(2, 0)
                End If
                tmp2y = CSng(Format(tmp2y + PassoY, "#0.00"))
            Loop
        End If

        ' Griglia secondaria: 9 linee tra ogni coppia di tick principali (PassoProv/10)
        If chkSecondaria.Checked Then
            _penWidth = 1 : _penColor = Color.Silver
            Dim stepSecY As Single = PassoProv / 10
            Dim ySec As Single = MinY
            Do While ySec < MaxY
                For k As Integer = 1 To 9
                    Dim yVal As Single = ySec + k * stepSecY
                    If yVal > MinY AndAlso yVal < MaxY Then
                        tmpY = (PicH - MB) - (yVal - MinY) / ScalaY
                        LineAbs(ML, tmpY, PicW - DimCornice, tmpY)
                    End If
                Next
                ySec += PassoProv
            Loop
        End If

        ' Decimi: tick mark sull'asse
        _penColor = Color.Blue
        PassoY = PassoProv / 10
        tmp2y = MinY
        If PassoY / ScalaY >= 1 Then
            Do While tmp2y < MaxY
                If (tmp2y - MinY) > 0 Then
                    tmpY = (PicH - MB) - (tmp2y - MinY) / ScalaY
                    MoveTo(ML - 2, tmpY) : LineStep(2, 0)
                End If
                tmp2y = CSng(Format(tmp2y + PassoY, "#0.##"))
            Loop
        End If
    End Sub

    ' ── disegno grafico principale ───────────────────────────────────────────────

    Public Sub VisualizzaGrafico()
        InitBitmap()
        ' NPAsseX/Y devono essere > 0 prima di SelezionaSingoliDati() che calcola PassoX/Y.
        ' Usano m_sizeX1/Y1 se disponibili, altrimenti un valore di default ragionevole.
        NPAsseX = If(m_sizeX1 > 0, m_sizeX1, 8)
        NPAsseY = If(m_sizeY1 > 0, m_sizeY1, 8)
        tmpSelezione = SelezionaSingoliDati()

        i_Size1 = 7
        i_Size2 = 8
        i_Size3 = 9

        ScalaX = (MaxX - MinX) / (PicW - ML - DimCornice)
        ScalaY = (MaxY - MinY) / (PicH - DimCornice - MB)
        VisualizzaAsseX()
        VisualizzaAsseY()

        If NPunti > 1 Then
            n = 1
            Do While Punti(1, 1, 1, n).y = 99999 AndAlso n <= NPunti
                n += 1
            Loop
            XPrec = Punti(1, 1, 1, n).X
            If InvertiScala AndAlso XPrec < MinX Then XPrec += 360
            YPrec = Punti(1, 1, 1, n).y
            DataPrec = CDate(Punti(1, 1, 1, n).data)
            OraPrec = CInt(CDate(Punti(1, 1, 1, n).data).ToString("HH"))
            YProv = Punti(1, 1, 1, n).y
            XProv = Punti(1, 1, 1, n).X
            If InvertiScala AndAlso XProv < MinX Then XProv += 360
            tmpX = (XProv - MinX) / ScalaX + ML
            tmpY = (PicH - MB) - (YProv - MinY) / ScalaY
            MoveTo(tmpX, tmpY)
            If n = 1 Then
                DrawStr(CDate(Punti(1, 1, 1, n).data).ToString("dd HH mm"), i_Size1)
                MoveTo(tmpX, tmpY)
            End If

            _dashStyle = DashStyle.Solid
            _penWidth = 2
            _penColor = Color.Black

            For i = n + 1 To NPunti
                If Punti(1, 1, 1, i).y <> 99999 Then
                    _penColor = If(Punti(1, 1, 1, i).c Mod 2 = 1, Color.Black, Color.Red)
                    YProv = Punti(1, 1, 1, i).y
                    XProv = Punti(1, 1, 1, i).X
                    DataProv = CDate(Punti(1, 1, 1, i).data)
                    If InvertiScala AndAlso XProv < MinX Then XProv += 360
                    OraProv = CInt(CDate(Punti(1, 1, 1, i).data).ToString("HH"))
                    tmpX = (XProv - MinX) / ScalaX + ML
                    tmpY = (PicH - MB) - (YProv - MinY) / ScalaY
                    ' Distanza reale in secondi tra punti consecutivi (sostituisce il
                    ' vecchio calcolo su "mm.ss" formattato come numero decimale, che
                    ' trattava i secondi come frazione dei minuti invece che in base 60
                    ' e non gestiva il cambio d'ora: quasi ogni coppia di punti risultava
                    ' "troppo distante" e veniva disegnata come pallino da 0.2px invece
                    ' che collegata da una linea, rendendo invisibili tutti i punti
                    ' intermedi.
                    Dim diffSecondi As Double = Math.Abs((DataProv - DataPrec).TotalSeconds)
                    If diffSecondi < i_Intervallo + 1 Then
                        If optLinee.Checked Then
                            LineTo(tmpX, tmpY)          ' linea continua, nessun punto
                        Else                            ' optPunti: punto singolo pieno 1px
                            _fillSolid = True
                            DrawCircle(tmpX, tmpY, 1.0F)
                            _fillSolid = False
                            MoveTo(tmpX, tmpY)
                        End If
                    Else
                        _fillSolid = False : DrawCircle(tmpX, tmpY, 0.2F)
                        MoveTo(tmpX, tmpY)
                    End If
                    ' Label orario sul punto: mostrato solo se optDatiSi e l'ora è cambiata
                    If optDatiSi.Checked AndAlso OraProv <> OraPrec Then
                        _penColor = Color.Black
                        MoveTo(tmpX + 1, tmpY - i_Size1 - 2)
                        DrawStr(CDate(Punti(1, 1, 1, i).data).ToString("HH:mm"), i_Size1)
                        MoveTo(tmpX, tmpY)
                    End If
                    XPrec = Punti(1, 1, 1, i).X
                    If InvertiScala AndAlso XPrec < MinX Then XPrec += 360
                    YPrec = Punti(1, 1, 1, i).y
                    DataPrec = CDate(Punti(1, 1, 1, i).data)
                    OraPrec = CInt(CDate(Punti(1, 1, 1, i).data).ToString("HH"))
                End If
            Next i

            If i - 1 <= NPunti Then
                DrawStr(CDate(Punti(1, 1, 1, i - 1).data).ToString("dd HH mm"), i_Size1)
            End If
        End If
        RefreshPic()
    End Sub

    ' ── eventi form ─────────────────────────────────────────────────────────────

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If IsDate(d_DataFineProg) Then
                Dim dtFin As Date = CDate(d_DataFineProg)
                mskDataFin.Text = dtFin.ToString("dd/MM/yyyy")  ' 4 cifre anno = mask "00/00/0000"
                mskOraFin.Text = dtFin.ToString("HH.mm.ss")    ' 6 char = mask "00.00.00"
            End If

            ' Primo record disponibile in tmpEffemeridi per preimpostare le date
            Dim sql As String = "SELECT * FROM [tmpEffemeridi] ORDER BY " &
                                "substr(date,7,4)||substr(date,4,2)||substr(date,1,2), time LIMIT 1"
            Using Cmd As New SQLiteCommand(sql, dbOrbite.Connection)
                Using r As SQLiteDataReader = Cmd.ExecuteReader()
                    If r.Read() Then
                        Dim d = r("date") : Dim t = r("Time")
                        Dim dtRow As Date = Date.MinValue
                        ' d può essere stringa "dd/mm/yyyy" o DateTime (se SQLite la interpreta)
                        If d IsNot Nothing Then
                            Dim ds = CStr(d).Split(" "c)(0)  ' prende solo la parte data
                            Date.TryParse(ds, dtRow)
                        End If
                        If dtRow > Date.MinValue Then
                            mskDataIni.Text = dtRow.ToString("dd/MM/yyyy")
                            ' DataFin = DataIni + 1 giorno
                            mskDataFin.Text = dtRow.AddDays(1).ToString("dd/MM/yyyy")
                        End If
                        If t IsNot Nothing Then
                            ' "08:45" → "08.45.00" per riempire mask "00.00.00"
                            Dim ts = OraToTimeSpan(CStr(t))
                            Dim oraStr = ts.Hours.ToString("00") & "." &
                                         ts.Minutes.ToString("00") & "." &
                                         ts.Seconds.ToString("00")
                            mskOraIni.Text = oraStr
                            mskOraFin.Text = oraStr  ' OraFin = OraIni
                        End If
                    End If
                End Using
            End Using

        Catch ex As Exception
            WriteError(0, "FrmVisualizzazione.Form_Load: " & ex.Message)
        End Try
    End Sub

    Private Sub Form_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' 1394x655 = ClientSize di progettazione nel Designer (vedi FrmVisualizzazione.Designer.vb)
        FormResizer.Init(Me, New Size(1394, 655))
        FormResizer.Resize(Me)
    End Sub

    Private _prevWindowState As FormWindowState = FormWindowState.Normal

    Private Sub Form_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        FormResizer.Resize(Me)
        If Me.WindowState = FormWindowState.Minimized Then Return
        If Me.WindowState <> _prevWindowState Then
            _prevWindowState = Me.WindowState
            If _bmp IsNot Nothing Then VisualizzaGrafico()
        End If
    End Sub

    Private Sub Form_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        If _bmp IsNot Nothing Then VisualizzaGrafico()
    End Sub

    Private Sub CmdElabora_Click(sender As Object, e As EventArgs) Handles CmdElabora.Click
        If Not ControllaDati() Then Exit Sub
        Cursor = Cursors.WaitCursor
        Try
            VisualizzaGrafico()
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CmdStampa_Click(sender As Object, e As EventArgs) Handles CmdStampa.Click
        If _bmp Is Nothing Then Return
        Using pd As New System.Drawing.Printing.PrintDocument()
            AddHandler pd.PrintPage, Sub(s, ev)
                                         Dim destRect = ev.MarginBounds
                                         ev.Graphics.DrawImage(_bmp, destRect,
                    New Rectangle(0, 0, _bmp.Width, _bmp.Height),
                    GraphicsUnit.Pixel)
                                     End Sub
            Using dlg As New PrintDialog()
                dlg.Document = pd
                If dlg.ShowDialog() = DialogResult.OK Then
                    pd.Print()
                End If
            End Using
        End Using
    End Sub

    Private Sub CmdEsci_Click(sender As Object, e As EventArgs) Handles CmdEsci.Click
        Me.Close()
    End Sub

    ' ── eventi controlli ────────────────────────────────────────────────────────

    Private Sub ChkPrincipale_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrincipale.CheckedChanged
        If chkPrincipale.Checked Then
            chkSecondaria.Checked = False
            chkSecondaria.Visible = True
        Else
            chkSecondaria.Checked = False
            chkSecondaria.Visible = False
        End If
    End Sub

    ' ── cleanup ─────────────────────────────────────────────────────────────────

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _g?.Dispose()
            _bmp?.Dispose()
            DyVisual?.Close()
            _Cmd?.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

End Class
