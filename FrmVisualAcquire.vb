Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Data.OleDb

Public Class FrmVisualAcquire

    ' ── campi dati ─────────────────────────────────────────────────────────────
    Private DyVisual As OleDbDataReader
    Private _Cmd As OleDbCommand
    Private tmpSql As String
    Private Const MaxPunti As Integer = 6000
    Private NPAsseY As Single
    Private NPAsseX As Single
    Private Const DimCornice As Integer = 9

    Private OraIni As String, OraFin As String
    Private DataIni As Date, DataFin As Date
    Private tmpDataIni As Date, tmpDataFin As Date
    ' Array 4D: prima tre dim hanno solo indice 0; quarta dim 0..MaxPunti
    Private Punti(0, 0, 0, MaxPunti) As PuntoGrafico
    Private NPunti As Integer
    Private MaxY As Single, MinY As Single
    Private MaxX As Single, MinX As Single
    Private tmpUnita As String
    Private ScalaX As Single, ScalaY As Single
    Private i As Integer, n As Integer
    Private tmpAz As Single
    Private PassoX As Single, PassoY As Single
    Private XPrec As Single, YPrec As Single, DataPrec As String
    Private tmpX As Single, tmpY As Single, tmp2x As Single, tmp2y As Single
    Private YProv As Single, XProv As Single, DataProv As String
    Private diffX As Single
    Private tmpSelezione As Boolean
    Private diff As Single, i_Intervallo As Single
    Private d_Calcolo As String
    Private OraPrec As Integer, OraProv As Integer
    Private InvertiScala As Boolean
    Private i_Size1 As Integer, i_Size2 As Integer, i_Size3 As Integer

    ' ── stato GDI+ ──────────────────────────────────────────────────────────────
    Private _bmp As Bitmap
    Private _g As Graphics
    Private _cx As Single           ' posizione corrente X (analogo CurrentX)
    Private _cy As Single           ' posizione corrente Y (analogo CurrentY)
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

    ' Sposta la posizione corrente (VB6: CurrentX/CurrentY)
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

    ' Stringa a posizione corrente  (VB6: picGrafico.Print / Print)
    Private Sub DrawStr(text As String, fontSize As Integer)
        Using fnt As New Font("Arial", fontSize)
            Using b As New SolidBrush(_penColor)
                _g.DrawString(text, fnt, b, _cx, _cy)
            End Using
        End Using
    End Sub

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
        If CDate(mskDataIni.Text & " " & mskOraIni.Text & ".00") >
           CDate(mskDataFin.Text & " " & mskOraFin.Text & ".00") Then
            mskDataIni.Focus() : Return False
        End If
        If Val(mskDataIni.Text) = Val(mskDataFin.Text) AndAlso
           (Val(mskOraFin.Text) - Val(mskOraIni.Text)) < 0.5 Then
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

    Private Function Tronc1(Numero As Single) As Single
        Dim t As Single = Math.Abs(Numero)
        If t >= 10 Then
            If t > 1000 Then
                t = (Int(t / 100) + 1) * 100
            ElseIf t <= 1000 AndAlso t > 10 AndAlso Numero >= 0 Then
                t = (Int(t / 10) + 1) * 10
            ElseIf Numero < 0 Then
                t = Int(t / 10) * 10
            End If
        Else
            t = If(Numero > 0, 10, 0)
        End If
        Return If(Numero >= 0, CInt(t), -CInt(t))
    End Function

    Private Function SelezionaSingoliDati() As Boolean
        InvertiScala = False
        MaxY = -99999 : MinY = 99999
        MaxX = -99999 : MinX = 99999
        OraIni = mskOraIni.Text
        OraFin = mskOraFin.Text
        DataIni = CDate(mskDataIni.Text)
        DataFin = CDate(mskDataFin.Text)
        tmpDataIni = DataIni.Add(TimeSpan.Parse(mskOraIni.Text & ":00"))
        tmpDataFin = DataFin.Add(TimeSpan.Parse(mskOraFin.Text & ":00"))

        tmpSql = "SELECT * FROM tmpEffemeridi ORDER BY date, time"

        ' TODO: dbOrbite è OleDbConnection definita nel modulo di progetto
        _Cmd?.Dispose()
        DyVisual?.Close()
        _Cmd = New OleDbCommand(tmpSql, dbOrbite)
        DyVisual = _Cmd.ExecuteReader()

        If Not DyVisual.Read() Then Return False

        i_Intervallo = CSng(DyVisual("Intervallo"))
        NPunti = 1
        XPrec = CSng(DyVisual("Azimuth"))

        ' Riapre per leggere dall'inizio
        DyVisual.Close()
        DyVisual = _Cmd.ExecuteReader()

        Do While DyVisual.Read()
            Punti(0, 0, 0, NPunti).data = DyVisual("date").ToString() & " " & DyVisual("Time").ToString()
            Punti(0, 0, 0, NPunti).Y = CSng(DyVisual("Elevation"))
            Punti(0, 0, 0, NPunti).X = CSng(DyVisual("Azimuth"))
            Punti(0, 0, 0, NPunti).C = CInt(DyVisual("Orbite"))
            XProv = CSng(DyVisual("Azimuth"))
            diffX = Math.Abs(XProv - XPrec)
            If diffX > 270 Then InvertiScala = True
            If Punti(0, 0, 0, NPunti).Y > MaxY Then MaxY = Int(Punti(0, 0, 0, NPunti).Y * 10) / 10 + 0.1F
            If Punti(0, 0, 0, NPunti).Y < MinY Then MinY = Int(Punti(0, 0, 0, NPunti).Y * 10) / 10
            If Punti(0, 0, 0, NPunti).X > MaxX Then MaxX = Int(Punti(0, 0, 0, NPunti).X * 10) / 10 + 0.1F
            If Punti(0, 0, 0, NPunti).X < MinX Then MinX = Int(Punti(0, 0, 0, NPunti).X * 10) / 10
            XPrec = XProv
            NPunti += 1
            If NPunti > MaxPunti Then Exit Do
        Loop

        If InvertiScala Then
            MaxX = -99999 : MinX = 99999
            For i = 1 To NPunti
                XProv = Punti(0, 0, 0, i).X
                ' Ruota i punti sotto 180° sommando 360° (traiettoria che passa per lo 0)
                If XProv <= 180 Then Punti(0, 0, 0, i).X = XProv + 360
                If Punti(0, 0, 0, i).X > MaxX Then MaxX = Int(Punti(0, 0, 0, i).X * 10) / 10 + 0.1F
                If Punti(0, 0, 0, i).X < MinX Then MinX = Int(Punti(0, 0, 0, i).X * 10) / 10
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

        Dim result As Boolean = Not (Math.Abs(MaxY - MinY) > 10 Or Math.Abs(MaxX - MinX) > 10)
        NPunti -= 1
        Return result
    End Function

    ' ── asse X ──────────────────────────────────────────────────────────────────

    Private Sub VisualizzaAsseX()
        Dim PassoProv As Single, tmpAz1 As Double, IX As Double
        PassoProv = PassoX
        If ScalaX = 0 Then Return

        diff = 0
        _dashStyle = DashStyle.Solid
        _penWidth = 1
        _penColor = Color.Black
        MoveTo(PicW - DimCornice - 15, PicH - DimCornice + 5)
        DrawStr("Azimuth", i_Size3)

        _penColor = Color.Red
        tmpAz = MinX

        If PassoX / ScalaX >= 1 Then
            Do While tmpAz <= MaxX
                If (tmpAz - MinX) >= 0 Then
                    tmpX = (tmpAz - MinX) / ScalaX + DimCornice
                    tmpY = PicH - DimCornice
                    MoveTo(tmpX, tmpY) : LineStep(0, 3)
                    MoveTo(tmpX, tmpY)

                    If PassoX / ScalaX >= 4 Then
                        If chkPrincipale.Checked Then
                            _penColor = Color.Black : LineStep(0, -(PicH - 2 * DimCornice))
                            _penColor = Color.Red
                        End If
                        tmpX = (tmpAz - MinX) / ScalaX + DimCornice - 1.5F
                        tmpY = PicH - DimCornice + 3
                        MoveTo(tmpX, tmpY)
                        tmpAz1 = tmpAz
                        If tmpAz1 >= 360 Then tmpAz1 -= 360
                        If tmpSelezione Then
                            DrawStr(Format(tmpAz1, "##0.#"), i_Size3)
                        ElseIf (CInt(tmpAz1) Mod 2) = 0 Then
                            DrawStr(Format(tmpAz1, "##0.#"), i_Size3)
                        End If
                    Else
                        If chkSecondaria.Checked Then
                            _penColor = Color.Silver : LineStep(0, -(PicH - 2 * DimCornice))
                            _penColor = Color.Red
                        End If
                        If chkPrincipale.Checked Then
                            _penColor = Color.Black : LineStep(0, -(PicH - 2 * DimCornice))
                            _penColor = Color.Red
                        End If
                        tmpX = (tmpAz - MinX) / ScalaX + DimCornice - 1.5F
                        tmpY = PicH - DimCornice + 3
                        MoveTo(tmpX, tmpY)
                        tmpAz1 = tmpAz
                        If tmpAz1 >= 360 Then tmpAz1 -= 360
                        If tmpSelezione Then
                            DrawStr(Format(tmpAz1, "##0.#"), i_Size3)
                        ElseIf (CInt(tmpAz1) Mod 2) = 0 Then
                            DrawStr(Format(tmpAz1, "##0"), i_Size3)
                        End If
                    End If
                End If
                tmpAz += PassoX
            Loop
        End If

        ' Metà passo
        _penColor = Color.Blue
        PassoX = PassoProv / 2
        tmpAz = MinX
        If PassoX / ScalaX >= 1 Then
            Do While tmpAz <= MaxX
                If (tmpAz - MinX) >= 0 Then
                    tmpX = (tmpAz - MinX) / ScalaX + DimCornice
                    tmpY = PicH - DimCornice
                    MoveTo(tmpX, tmpY) : LineStep(0, 2)
                    MoveTo(tmpX, tmpY)
                End If
                If chkSecondaria.Checked Then
                    _penColor = Color.Silver : LineStep(0, -(PicH - 2 * DimCornice))
                    _penColor = Color.Blue
                End If
                tmpAz += PassoX
            Loop
        End If

        ' Decimi
        _penColor = Color.Blue
        PassoX = PassoProv / 10
        tmpAz = MinX
        IX = If(Int(MaxX - MinX) > 1, 0.1, 0.01)
        If PassoX / ScalaX >= 1 Then
            Do While tmpAz < MaxX
                If (tmpAz - MinX) > 0 Then
                    tmpX = (tmpAz - MinX) / ScalaX + DimCornice
                    tmpY = PicH - DimCornice
                    MoveTo(tmpX, tmpY) : LineStep(0, 2)
                    MoveTo(tmpX, tmpY)
                    If PassoX / ScalaX >= 4 Then
                        If chkPrincipale.Checked Then
                            _penColor = Color.Black : LineStep(0, -(PicH - 2 * DimCornice))
                            _penColor = Color.Blue
                        End If
                        MoveTo((tmpAz - MinX) / ScalaX + DimCornice - 1.5F, PicH - DimCornice + 2)
                        If Int(MaxX - MinX) > 1 Then
                            If IX < 1 Then DrawStr(IX.ToString("G"), i_Size1)
                            IX += 0.1 : If IX > 1 Then IX = 0.1
                        Else
                            If IX <= 0.09 Then DrawStr(IX.ToString("G"), i_Size1)
                            IX += 0.01 : If IX > 0.1 Then IX = 0.01
                        End If
                    ElseIf chkSecondaria.Checked Then
                        _penColor = Color.Silver : LineStep(0, -(PicH - 2 * DimCornice))
                        _penColor = Color.Blue
                    End If
                End If
                tmpAz = CSng(Format(tmpAz + PassoX, "#0.##"))
            Loop
        End If

        ' Centesimi
        _penColor = Color.Blue
        PassoX = PassoProv / 100
        tmpAz = MinX
        If PassoX / ScalaX >= 1 Then
            Do While tmpAz < MaxX
                If (tmpAz - MinX) >= 0 Then
                    tmpX = (tmpAz - MinX) / ScalaX + DimCornice
                    tmpY = PicH - DimCornice
                    MoveTo(tmpX, tmpY) : LineStep(0, 2)
                    MoveTo(tmpX, tmpY)
                    If PassoX / ScalaX >= 4 Then
                        If chkPrincipale.Checked Then
                            _penColor = Color.Black : LineStep(0, -(PicH - 2 * DimCornice))
                            _penColor = Color.Blue
                        End If
                        MoveTo((tmpAz - MinX) / ScalaX + DimCornice - 1.5F, PicH - DimCornice + 2)
                    ElseIf chkSecondaria.Checked Then
                        _penColor = Color.Silver : LineStep(0, -(PicH - 2 * DimCornice))
                        _penColor = Color.Blue
                    End If
                End If
                tmpAz = CSng(Format(tmpAz + PassoX, "##0.###"))
            Loop
        End If

        ' Estremi intervallo
        _penColor = Color.Black
        MoveTo(140, 1) : DrawStr("dal: " & tmpDataIni.ToString("dd/MM/yy HH.mm"), i_Size3)
        MoveTo(140, 4) : DrawStr("  al: " & tmpDataFin.ToString("dd/MM/yy HH.mm"), i_Size3)
        LineAbs(DimCornice, PicH - DimCornice - diff, PicW - DimCornice, PicH - DimCornice - diff)
        MoveTo(DimCornice, PicH - DimCornice) : LineStep(0, -(PicH - 2 * DimCornice))
    End Sub

    ' ── asse Y ──────────────────────────────────────────────────────────────────

    Private Sub VisualizzaAsseY()
        Dim PassoProv As Single, IX As Single
        tmpUnita = "Elevation"
        PassoProv = PassoY
        If ScalaY = 0 Then Return

        _penColor = Color.Black
        _dashStyle = DashStyle.Solid
        tmp2x = DimCornice
        LineAbs(tmp2x, PicH - DimCornice, tmp2x, DimCornice)
        MoveTo(tmp2x - DimCornice, DimCornice - 6) : DrawStr(tmpUnita, i_Size2)

        _penWidth = 1
        MoveTo(tmp2x - DimCornice - i_Size2 * tmpUnita.Length * 0.6F + 1, DimCornice - 4)
        LineStep(10, 0)

        tmp2y = MinY
        Do While tmp2y <= MaxY
            If tmp2y >= MinY Then
                tmpY = (PicH - DimCornice) - (tmp2y - MinY) / ScalaY
                MoveTo(tmp2x, tmpY)
                If chkPrincipale.Checked AndAlso tmp2y <> MinY Then
                    _penColor = Color.Black : LineStep(PicW - 2 * DimCornice, 0)
                End If
                MoveTo(2, (PicH - DimCornice) - (tmp2y - MinY) / ScalaY - 1.7F)
                _penColor = Color.Black
                If tmpSelezione Then
                    DrawStr(Format(tmp2y, "#0.#"), i_Size2)
                ElseIf (CInt(tmp2y) Mod 2) = 0 Then
                    DrawStr(Format(tmp2y, "#0"), i_Size2)
                End If
            End If
            tmp2y += PassoY
        Loop

        ' Metà passo
        _penColor = Color.Blue
        PassoY = PassoProv / 2
        tmp2y = MinY
        If PassoY / ScalaY > 1 Then
            Do While tmp2y < MaxY
                If (tmp2y - MinY) > 0 Then
                    tmpY = (PicH - DimCornice) - (tmp2y - MinY) / ScalaY
                    MoveTo(DimCornice - 2, tmpY) : LineStep(2, 0)
                    MoveTo(DimCornice - 2, tmpY)
                End If
                If chkSecondaria.Checked Then
                    MoveTo(DimCornice, tmpY)
                    _penColor = Color.Silver : LineStep(PicW - 2 * DimCornice, 0)
                    _penColor = Color.Blue
                End If
                tmp2y = CSng(Format(tmp2y + PassoY, "#0.00"))
            Loop
        End If

        ' Decimi
        _penColor = Color.Blue
        PassoY = PassoProv / 10
        tmp2y = MinY
        IX = If(Int(MaxY - MinY) > 1, 0.1F, 0.01F)
        If PassoY / ScalaY >= 1 Then
            Do While tmp2y < MaxY
                If (tmp2y - MinY) > 0 Then
                    tmpY = (PicH - DimCornice) - (tmp2y - MinY) / ScalaY
                    MoveTo(DimCornice - 2, tmpY) : LineStep(2, 0)
                    MoveTo(DimCornice - 6, tmpY - 2)
                    If PassoY / ScalaY >= 4 Then
                        If Int(MaxY - MinY) > 1 Then
                            If IX < 1 Then DrawStr(IX.ToString("G"), i_Size1)
                            IX += 0.1F : If IX > 1 Then IX = 0.1F
                        Else
                            If IX <= 0.09F Then DrawStr(IX.ToString("G"), i_Size1)
                            IX += 0.01F : If IX > 0.1F Then IX = 0.01F
                        End If
                    End If
                End If
                If chkSecondaria.Checked Then
                    MoveTo(DimCornice, tmpY)
                    _penColor = Color.Silver : LineStep(PicW - 2 * DimCornice, 0)
                    _penColor = Color.Blue
                End If
                tmp2y = CSng(Format(tmp2y + PassoY, "#0.##"))
            Loop
        End If
    End Sub

    ' ── disegno grafico principale ───────────────────────────────────────────────

    Public Sub VisualizzaGrafico()
        InitBitmap()
        tmpSelezione = SelezionaSingoliDati()

        If tmpSelezione AndAlso NPunti > 1 Then
            i_Size1 = 4 : i_Size2 = 8 : i_Size3 = 10
        Else
            i_Size1 = 2 : i_Size2 = 4 : i_Size3 = 5
        End If

        ScalaX = (MaxX - MinX) / (PicW - 2 * DimCornice)
        ScalaY = (MaxY - MinY) / (PicH - 2 * DimCornice)
        VisualizzaAsseX()
        VisualizzaAsseY()

        If NPunti > 1 Then
            n = 1
            Do While Punti(0, 0, 0, n).Y = 99999 AndAlso n <= NPunti
                n += 1
            Loop
            XPrec = Punti(0, 0, 0, n).X
            If InvertiScala AndAlso XPrec < MinX Then XPrec += 360
            YPrec = Punti(0, 0, 0, n).Y
            DataPrec = CDate(Punti(0, 0, 0, n).data).ToString("mm.ss")
            OraPrec = CInt(CDate(Punti(0, 0, 0, n).data).ToString("HH"))
            YProv = Punti(0, 0, 0, n).Y
            XProv = Punti(0, 0, 0, n).X
            If InvertiScala AndAlso XProv < MinX Then XProv += 360
            tmpX = (XProv - MinX) / ScalaX + DimCornice
            tmpY = (PicH - DimCornice) - (YProv - MinY) / ScalaY
            MoveTo(tmpX, tmpY)
            If n = 1 Then
                DrawStr(CDate(Punti(0, 0, 0, n).data).ToString("dd HH mm"), i_Size1)
                MoveTo(tmpX, tmpY)
            End If

            _dashStyle = DashStyle.Dot
            _penWidth = 2
            _penColor = Color.Black

            For i = n + 1 To NPunti
                If Punti(0, 0, 0, i).Y <> 99999 Then
                    _penColor = If(Punti(0, 0, 0, i).C Mod 2 = 1, Color.Black, Color.Red)
                    YProv = Punti(0, 0, 0, i).Y
                    XProv = Punti(0, 0, 0, i).X
                    DataProv = CDate(Punti(0, 0, 0, i).data).ToString("mm.ss")
                    If InvertiScala AndAlso XProv < MinX Then XProv += 360
                    OraProv = CInt(CDate(Punti(0, 0, 0, i).data).ToString("HH"))
                    tmpX = (XProv - MinX) / ScalaX + DimCornice
                    tmpY = (PicH - DimCornice) - (YProv - MinY) / ScalaY
                    d_Calcolo = DataProv
                    ' Gestione cambio minuto (es. 59→00 = salto di -59 → aggiungi 60)
                    If Val(d_Calcolo) < Val(DataPrec) Then
                        d_Calcolo = Format(Val(DataProv) + 60, "##0.00")
                    End If
                    If Math.Abs(Val(d_Calcolo) - Val(DataPrec)) < i_Intervallo + 1 Then
                        LineTo(tmpX, tmpY)
                        _fillSolid = True : DrawCircle(tmpX, tmpY, 0.6F) : _fillSolid = False
                    Else
                        _fillSolid = False : DrawCircle(tmpX, tmpY, 0.2F)
                        MoveTo(tmpX, tmpY)
                    End If
                    XPrec = Punti(0, 0, 0, i).X
                    If InvertiScala AndAlso XPrec < MinX Then XPrec += 360
                    YPrec = Punti(0, 0, 0, i).Y
                    DataPrec = CDate(Punti(0, 0, 0, i).data).ToString("mm.ss")
                    OraPrec = CInt(CDate(Punti(0, 0, 0, i).data).ToString("HH"))
                End If
            Next i

            If i - 1 <= NPunti Then
                DrawStr(CDate(Punti(0, 0, 0, i - 1).data).ToString("dd HH mm"), i_Size1)
            End If
        End If
        RefreshPic()
    End Sub

    ' ── eventi form ─────────────────────────────────────────────────────────────

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            tmpSql = "SELECT * FROM tmpEffemeridi ORDER BY date, time"
            ' TODO: dbOrbite = OleDbConnection definita nel modulo globale
            Using Cmd As New OleDbCommand(tmpSql, dbOrbite)
                Using reader As OleDbDataReader = Cmd.ExecuteReader()
                    If reader.Read() Then
                        If IsDate(reader("date")) Then
                            mskDataIni.Text = CDate(reader("date")).ToString("dd/MM/yy")
                        End If
                        If IsDate(reader("Time")) Then
                            mskOraIni.Text = CDate(reader("Time")).ToString("HH.mm")
                        End If
                    End If
                End Using
            End Using
            ' TODO: d_DataFineProg, m_sizeX1, m_sizeY1, i_InterAcq, i_AzRange ecc.
            '       sono variabili globali definite nel modulo di progetto
            If IsDate(d_DataFineProg) Then
                Dim dtFin As Date = CDate(d_DataFineProg)
                mskDataFin.Text = dtFin.ToString("dd/MM/yy")
                mskOraFin.Text  = dtFin.ToString("HH.mm")
            End If
            ScalaX = picGrafico.Width / m_sizeX1
            ScalaY = picGrafico.Height / m_sizeY1
            NPAsseX = picGrafico.ClientSize.Width / ScalaX
            NPAsseY = picGrafico.ClientSize.Height / ScalaY
            tmrAcquire.Interval = CInt(i_InterAcq * 1000)
            tmrAcquire.Enabled = True
            TxtAzRange.Text = i_AzRange.ToString()
            TxtElRange.Text = i_ElRange.ToString()
            TxtAzStep.Text = i_AzStep.ToString()
            TxtElStep.Text = i_ElStep.ToString()
            TxtAzStepLock.Text = i_AzStepLock.ToString()
            TxtElStepLock.Text = i_ElStepLock.ToString()
            TxtDeltaTime.Text = i_InterAcq.ToString()
            TxtCoorAcq.Text = CoorAcq.ToString()
            TxtLockThr.Text = i_LockThr.ToString()
        Catch
            Me.Close()
        End Try
    End Sub

    Private Sub Form_Activate(sender As Object, e As EventArgs) Handles MyBase.Activated
        ' disabilito gli altri pulsanti della toolBar (da implementare)
    End Sub

    Private Sub Form_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Static vetDim(,) As Long
        Static ok As Boolean
        Static m_SizeXAcq As Integer
        Static m_SizeYAcq As Integer
        Dim szX As Double, szY As Double
        Dim ctrl As Control

        If Not ok Then
            m_SizeXAcq = Me.Width
            m_SizeYAcq = Me.Height
            ReDim vetDim(Me.Controls.Count, 5)
            For i = 0 To Me.Controls.Count - 1
                ctrl = Me.Controls(i)
                If TypeOf ctrl Is Button OrElse TypeOf ctrl Is Label OrElse
                   TypeOf ctrl Is PictureBox OrElse TypeOf ctrl Is StatusStrip OrElse
                   TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is MaskedTextBox OrElse
                   TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is RadioButton OrElse
                   TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is ComboBox OrElse
                   TypeOf ctrl Is DataGridView OrElse TypeOf ctrl Is ToolStrip OrElse
                   TypeOf ctrl Is ProgressBar OrElse TypeOf ctrl Is Panel Then
                    vetDim(i + 1, 1) = ctrl.Top
                    vetDim(i + 1, 2) = ctrl.Left
                    vetDim(i + 1, 3) = ctrl.Height
                    vetDim(i + 1, 4) = ctrl.Width
                    vetDim(i + 1, 5) = CLng(ctrl.Font.Size)
                End If
            Next
            ok = True
            Exit Sub
        End If

        szX = Me.Width / m_SizeXAcq
        szY = Me.Height / m_SizeYAcq
        If Math.Abs(szX - 1) < 0.1 Then Exit Sub

        If Math.Abs(1 - szX) > 0.5 Then
            szX = 0.5 : Me.Width = CInt(m_sizeX1 * 0.5)
        End If
        If Math.Abs(1 - szY) > 0.5 Then
            szY = 0.5 : Me.Height = CInt(m_sizeY1 * 0.6)
        End If

        For i = 0 To Me.Controls.Count - 1
            ctrl = Me.Controls(i)
            If TypeOf ctrl Is Button OrElse TypeOf ctrl Is Label OrElse
               TypeOf ctrl Is PictureBox OrElse TypeOf ctrl Is TextBox OrElse
               TypeOf ctrl Is MaskedTextBox OrElse TypeOf ctrl Is CheckBox OrElse
               TypeOf ctrl Is RadioButton OrElse TypeOf ctrl Is GroupBox OrElse
               TypeOf ctrl Is ComboBox OrElse TypeOf ctrl Is DataGridView OrElse
               TypeOf ctrl Is ProgressBar OrElse TypeOf ctrl Is Panel Then
                ctrl.Top = CInt(vetDim(i + 1, 1) * szY)
                ctrl.Left = CInt(vetDim(i + 1, 2) * szX)
                ctrl.Height = CInt(vetDim(i + 1, 3) * szY)
                ctrl.Width = CInt(vetDim(i + 1, 4) * szX)
                If vetDim(i + 1, 5) > 0 Then
                    Dim fs As Single = CSng(vetDim(i + 1, 5)) * CSng(Math.Min(szX, szY))
                    If fs > 0 Then ctrl.Font = New Font(ctrl.Font.FontFamily, fs)
                End If
            ElseIf TypeOf ctrl Is StatusStrip OrElse TypeOf ctrl Is ToolStrip Then
                ctrl.Top = CInt(vetDim(i + 1, 1) * szY)
                ctrl.Height = CInt(vetDim(i + 1, 3) * szY)
                ctrl.Width = CInt(vetDim(i + 1, 4) * szX)
            End If
        Next
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

    Private Sub CmdElabora_Click(sender As Object, e As EventArgs) Handles CmdElabora.Click
        If ControllaDati() Then
            Cursor = Cursors.Default
        End If
        Cursor = Cursors.WaitCursor
    End Sub

    Private Sub CmdEsci_Click(sender As Object, e As EventArgs) Handles CmdEsci.Click
        Me.Close()
    End Sub

    Private Sub TmrAcquire_Tick(sender As Object, e As EventArgs) Handles tmrAcquire.Tick
        VisualizzaGrafico()
        tmrAcquire.Enabled = True
    End Sub

    ' ── TextBox KeyUp con validazione ───────────────────────────────────────────

    Private Sub TxtAzRange_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtAzRange.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtAzRange.Text) Then
                Dim v = CDbl(TxtAzRange.Text)
                If v >= 1 AndAlso v <= 30 Then i_AzRange = CSng(v) Else TxtAzRange.Text = i_AzRange.ToString()
            Else
                TxtAzRange.Text = i_AzRange.ToString()
            End If
        End If
    End Sub

    Private Sub TxtAzStep_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtAzStep.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtAzStep.Text) Then
                Dim v = CDbl(TxtAzStep.Text)
                If v >= 0.01 AndAlso v <= 1 Then i_AzStep = CSng(v) Else TxtAzStep.Text = i_AzStep.ToString()
            Else
                TxtAzStep.Text = i_AzStep.ToString()
            End If
        End If
    End Sub

    Private Sub TxtAzStepLock_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtAzStepLock.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtAzStepLock.Text) Then
                Dim v = CDbl(TxtAzStepLock.Text)
                If v >= 0.01 AndAlso v <= 1 Then i_AzStepLock = CSng(v) Else TxtAzStepLock.Text = i_AzStepLock.ToString()
            Else
                TxtAzStepLock.Text = i_AzStepLock.ToString()
            End If
        End If
    End Sub

    Private Sub TxtCoorAcq_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtCoorAcq.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtCoorAcq.Text) Then
                Dim v = CDbl(TxtCoorAcq.Text)
                If v >= 0.05 AndAlso v <= 1 Then CoorAcq = CSng(v) Else TxtCoorAcq.Text = CoorAcq.ToString()
            Else
                TxtCoorAcq.Text = CoorAcq.ToString()
            End If
        End If
    End Sub

    Private Sub TxtDeltaTime_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtDeltaTime.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtDeltaTime.Text) Then
                Dim v = CDbl(TxtDeltaTime.Text)
                If v >= 1 AndAlso v <= 5 Then
                    i_InterAcq = CSng(v)
                    MilSecC = CSng(v)
                    ' TODO: frmRicezione = istanza globale di FrmRicezione
                    frmRicezione.tmrSpiralAcquire.Interval = CInt(i_InterAcq * 1000)
                    frmRicezione.tmrSpiralAcquire.Enabled = True
                Else
                    TxtDeltaTime.Text = i_InterAcq.ToString()
                End If
            Else
                TxtDeltaTime.Text = i_InterAcq.ToString()
            End If
        End If
    End Sub

    Private Sub TxtElRange_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtElRange.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtElRange.Text) Then
                Dim v = CDbl(TxtElRange.Text)
                If v >= 1 AndAlso v <= 30 Then i_ElRange = CSng(v) Else TxtElRange.Text = i_ElRange.ToString()
            Else
                TxtElRange.Text = i_ElRange.ToString()
            End If
        End If
    End Sub

    Private Sub TxtElStep_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtElStep.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtElStep.Text) Then
                Dim v = CDbl(TxtElStep.Text)
                If v >= 0.01 AndAlso v <= 1 Then i_ElStep = CSng(v) Else TxtElStep.Text = i_ElStep.ToString()
            Else
                TxtElStep.Text = i_ElStep.ToString()
            End If
        End If
    End Sub

    Private Sub TxtElStepLock_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtElStepLock.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtElStepLock.Text) Then
                Dim v = CDbl(TxtElStepLock.Text)
                If v >= 0.01 AndAlso v <= 1 Then i_ElStepLock = CSng(v) Else TxtElStepLock.Text = i_ElStepLock.ToString()
            Else
                TxtElStepLock.Text = i_ElStepLock.ToString()
            End If
        End If
    End Sub

    Private Sub TxtLockThr_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtLockThr.KeyUp
        If e.KeyCode = Keys.Return Then
            If IsNumeric(TxtLockThr.Text) Then
                Dim v = CDbl(TxtLockThr.Text)
                If v >= -110 AndAlso v <= -40 Then i_LockThr = CSng(v) Else TxtLockThr.Text = i_LockThr.ToString()
            Else
                TxtLockThr.Text = i_LockThr.ToString()
            End If
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
