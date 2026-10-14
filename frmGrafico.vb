Imports System.Windows.Forms
Imports System.Drawing

' ============================================================
' frmGrafico.vb — convertito da frmGrafico.frm (VB6)
' Grafico livello beacon in tempo reale
' ============================================================
' NOTE DI CONVERSIONE:
'   - picGrafico (PictureBox VB6) → PictureBox + Graphics
'   - Graphics.DrawLine() → Graphics.DrawLine()
'   - Graphics.DrawString() → Graphics.DrawString()
'   - gfx.Clear() → picGrafico.Invalidate() o Graphics.Clear()
'   - picGrafico.CurrentX/Y → tenere variabili locali
'   - vbSolid, vbDot → DashStyle.Solid, DashStyle.Dot
'   - FrmGrafico.Width/Height → Me.Width/Height
' ============================================================

Partial Public Class frmGrafico
    Inherits System.Windows.Forms.Form

    ' -------------------------------------------------------
    ' Struttura punto grafico (era StructurePuntoGrafico in VB6)
    ' -------------------------------------------------------
    Private Structure PuntoGrafico
        Public data As String
        Public X As System.Single
        Public y As System.Single
    End Structure

    ' -------------------------------------------------------
    ' Variabili locali
    ' -------------------------------------------------------
    Private MaxPunti As Integer
    Private NPAsseY As System.Single
    Private NPAsseX As System.Single
    Private Const DimCornice As Integer = 9
    ' Margine inferiore maggiorato: con solo DimCornice (9px) le etichette
    ' dell'asse X (VisualizzaAsseX) venivano disegnate 2px sotto l'ultima
    ' griglia, cioè oltre il bordo inferiore del bitmap — tagliate fuori,
    ' invisibili. Usato come riferimento Y "basso" al posto di DimCornice
    ' ovunque si calcola la posizione della riga MinY.
    Private Const MargineBasso As Integer = 22
    Private Intervallo As Integer

    Private OraIni As String
    Private DataIni As Date, DataFin As Date

    Private Punti(3600) As PuntoGrafico
    Private MaxY As System.Single, MinY As System.Single
    Private MaxX As System.Single, MinX As System.Single
    Private ScalaX As System.Single, ScalaY As System.Single
    Private PassoX As System.Single, PassoY As System.Single
    Private stepinc As System.Single
    Private m_SizeXGra As System.Single, m_SizeYGra As System.Single

    ' Grafica
    Private bmp As Bitmap
    Private gfx As Graphics

    Public Sub New()
        InitializeComponent()
    End Sub

    ' Controlli dichiarati in frmGrafico.Designer.vb

    ' -------------------------------------------------------
    ' Form Load
    ' -------------------------------------------------------
    Private Sub frmGrafico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtMin.Text = CStr(ScalaMin)
        TxtMax.Text = CStr(ScalaMax)
        LblAntenna.Text = Antenna
        CmbAssex.Text = CStr(i_AsseXGrafico)
        m_SizeXGra = Me.Width
        m_SizeYGra = Me.Height
    End Sub

    ' -------------------------------------------------------
    ' Form Activate
    ' -------------------------------------------------------
    Private Sub frmGrafico_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        TxtMin.Text = CStr(ScalaMin)
        TxtMax.Text = CStr(ScalaMax)
        LblAntenna.Text = Antenna
        CmbAssex.Text = CStr(i_AsseXGrafico)
        m_SizeXGra = Me.Width
        m_SizeYGra = Me.Height
        Inizio()
    End Sub

    ' Nessuna riscalatura proporzionale sul resize: form non a schermo
    ' intero. picGrafico usa Anchor = All per adattarsi automaticamente.

    ' -------------------------------------------------------
    ' Bottone Esci
    ' -------------------------------------------------------
    Private Sub CmdEsci_Click(sender As Object, e As EventArgs) Handles CmdEsci.Click
        TmrGrafico.Enabled = True
        Me.Close()
    End Sub

    ' -------------------------------------------------------
    ' Bottone Scale — applica scala manuale
    ' -------------------------------------------------------
    Private Sub CmdScale_Click(sender As Object, e As EventArgs) Handles CmdScale.Click
        If Not IsNumeric(TxtMax.Text) OrElse Not IsNumeric(TxtMin.Text) Then Exit Sub
        If Math.Abs(CDbl(TxtMax.Text) - CDbl(TxtMin.Text)) < 1 Then Exit Sub

        ScalaMax = CSng(TxtMax.Text)
        ScalaMin = CSng(TxtMin.Text)
        ScriviFileIni("Default", "ScalaMax", TxtMax.Text)
        ScriviFileIni("Default", "ScalaMin", TxtMin.Text)

        ' Aggiorna label su frmRicezione (controlli non migrati)
        Inizio()
    End Sub

    ' -------------------------------------------------------
    ' ComboBox asse X — cambia scala temporale
    ' -------------------------------------------------------
    Private Sub CmbAssex_LostFocus(sender As Object, e As EventArgs) Handles CmbAssex.LostFocus
        AggiornaCmbAssex()
    End Sub

    Private Sub CmbAssex_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbAssex.KeyUp
        If e.KeyCode = Keys.Return Then AggiornaCmbAssex()
    End Sub

    Private Sub AggiornaCmbAssex()
        If Not IsNumeric(CmbAssex.Text) Then Exit Sub
        Dim nuovoMax As System.Single = CSng(CmbAssex.Text)
        If nuovoMax = MaxX Then Exit Sub

        MaxX = nuovoMax
        PassoX = MaxX / 10
        ScalaX = MaxX / (picGrafico.Width - 2 * DimCornice)

        If MaxX = 2 Then
            MaxPunti = CInt(2 * 60 * 1 / i_Inter)
            TmrGrafico.Interval = CInt(i_Inter * 1000)
            For i As Integer = 1 To MaxPunti - 1
                Punti(i).data = "" : Punti(i).X = 0 : Punti(i).y = 0
            Next
        Else
            MaxPunti = 60 * 60
            TmrGrafico.Interval = 1000
            For i As Integer = 1 To MaxPunti - 1
                If Punti(i).data <> "" Then
                    Punti(i).X = MaxX - ((MaxPunti - i) * PassoX / (6 * MaxX))
                End If
            Next
            If MaxPunti > 0 Then Punti(MaxPunti).X = MaxX
        End If
        i_AsseXGrafico = CInt(MaxX)
        TmrGrafico.Enabled = True
    End Sub

    ' -------------------------------------------------------
    ' Timer Grafico — aggiorna il grafico ogni tick
    ' -------------------------------------------------------
    Private Sub TmrGrafico_Tick(sender As Object, e As EventArgs) Handles TmrGrafico.Tick
        Try
            Dim ValBeacon As System.Single
            ' Era hardcoded a -150 ("TxtLev not migrated"): legge ora il
            ' livello beacon reale da frmRicezione (LblLev_ctrl, aggiornato
            ' da AggiornaBeaconLock — dato ricevitore s_Lev1, o fallback sul
            ' dato grezzo ACU s_Lev se il ricevitore non è collegato).
            ' -150 resta il valore di sentinella VB6 per "nessun dato".
            If frmRicezioneRef IsNot Nothing AndAlso IsNumeric(frmRicezioneRef.LblLev_ctrl.Text) Then
                ValBeacon = CSng(frmRicezioneRef.LblLev_ctrl.Text)
            Else
                ValBeacon = -150
            End If

            MaxPunti = If(MaxX = 2, CInt(2 * 60 * 1 / i_Inter), 60 * 60)

            If Me.Visible Then
                LblLev.Text = CStr(ValBeacon)
                If i_AsseXGrafico <> 2 Then
                    For i As Integer = 1 To MaxPunti - 1
                        If Punti(i + 1).data <> "" Then
                            Punti(i).data = Punti(i + 1).data
                            Punti(i).y = Punti(i + 1).y
                            Punti(i).X = Punti(i + 1).X - (PassoX / (6 * MaxX))
                        End If
                    Next
                Else
                    For i As Integer = 1 To MaxPunti - 1
                        If Punti(i + 1).data <> "" Then
                            Punti(i).data = Punti(i + 1).data
                            Punti(i).y = Punti(i + 1).y
                            Punti(i).X = MaxX / MaxPunti * i
                        End If
                    Next
                End If
                Punti(MaxPunti).data = Now.AddHours(-i_Offset).ToString("HH.mm.ss")
                Punti(MaxPunti).X = MaxX
                Punti(MaxPunti).y = ValBeacon
                VisualizzaGrafico()
            Else
                For i As Integer = 1 To MaxPunti - 1
                    If Punti(i + 1).data <> "" Then
                        Punti(i).data = Punti(i + 1).data
                        Punti(i).y = Punti(i + 1).y
                        Punti(i).X = Punti(i + 1).X - (1.0F / (6 * i_AsseXGrafico))
                    End If
                Next
                Punti(MaxPunti).data = Now.AddHours(-i_Offset).ToString("HH.mm.ss")
                Punti(MaxPunti).X = i_AsseXGrafico
                Punti(MaxPunti).y = ValBeacon
            End If
            TmrGrafico.Enabled = True

        Catch ex As Exception
            TmrGrafico.Enabled = True
            WriteError(0, "TmrGrafico: " & ex.Message)
        End Try
    End Sub

    ' -------------------------------------------------------
    ' Inizializza il grafico
    ' -------------------------------------------------------
    Private Function Inizio() As Boolean
        Inizio = True
        If Not IsNumeric(TxtMax.Text) OrElse Not IsNumeric(TxtMin.Text) Then Return False
        If CDbl(TxtMax.Text) <= CDbl(TxtMin.Text) Then Return False

        MaxY = CSng(TxtMax.Text)
        MinY = CSng(TxtMin.Text)
        MaxX = CSng(CmbAssex.Text)
        ScalaX = MaxX / (picGrafico.Width - 2 * DimCornice)
        ScalaY = (MaxY - MinY) / (picGrafico.Height - DimCornice - MargineBasso)
        stepinc = (MaxY - MinY) / 10
        PassoX = MaxX / 10
        ' Era mai assegnato ("TxtIntervallo = Abs(TxtMax - TxtMin)" in VB6,
        ' persa nel porting): il campo restava sempre vuoto.
        If TxtIntervallo IsNot Nothing Then TxtIntervallo.Text = CStr(Math.Abs(MaxY - MinY))

        If i_AsseXGrafico = 2 Then
            MaxPunti = CInt(2 * 60 * 1 / i_Inter)
            TmrGrafico.Interval = CInt(i_Inter * 1000)
            For i As Integer = 1 To MaxPunti - 1
                Punti(i).data = "" : Punti(i).X = 0 : Punti(i).y = 0
            Next
        Else
            MaxPunti = 60 * 60
            TmrGrafico.Interval = 1000
            For i As Integer = 1 To MaxPunti - 1
                If Punti(i).data <> "" Then
                    Punti(i).X = MaxX - ((MaxPunti - i) * PassoX / (6 * MaxX))
                End If
            Next
            If MaxPunti > 0 Then Punti(MaxPunti).X = MaxX
        End If

        ' Ridisegna assi
        picGrafico.Invalidate()
    End Function

    ' -------------------------------------------------------
    ' Disegna il grafico sul PictureBox (era Graphics.DrawLine())
    ' -------------------------------------------------------
    Private Sub VisualizzaGrafico()
        Try
            ' Crea bitmap se necessario
            If bmp Is Nothing OrElse bmp.Width <> picGrafico.Width OrElse bmp.Height <> picGrafico.Height Then
                bmp = New Bitmap(picGrafico.Width, picGrafico.Height)
                gfx = Graphics.FromImage(bmp)
            End If
            gfx.Clear(Color.White)

            VisualizzaAsseX(gfx)
            VisualizzaAsseY(gfx)

            ' Disegna curva beacon
            Dim n As Integer = If(MaxX <> 2, CInt(MaxPunti - MaxX * 60) + 1, 1)
            If n < 1 Then n = 1

            Using penRed As New Pen(Color.Red, 2)
                penRed.DashStyle = Drawing2D.DashStyle.Solid
                ' YPrec deve essere la coordinata PIXEL del punto precedente
                ' (per DrawLine), non il valore grezzo — tenuto a parte in
                ' YValPrec solo per il controllo di range min/max. Prima
                ' DrawLine riceveva il valore grezzo (es. -48..-103) al posto
                ' del pixel Y, disegnando segmenti verticali impazziti invece
                ' di una linea continua (si vedeva solo con dati che variano:
                ' prima, con ValBeacon fisso a -150, la riga non era quasi
                ' mai nel range min/max e quindi non veniva mai disegnata).
                Dim XPrec As System.Single = 0, YPrec As System.Single = 0
                Dim YValPrec As System.Single = 0
                Dim started As Boolean = False

                For i As Integer = n To MaxPunti
                    If Punti(i).data = "" Then Continue For
                    Dim tmpX As System.Single = Punti(i).X / ScalaX + DimCornice
                    Dim tmpY As System.Single = (picGrafico.Height - MargineBasso) -Math.Abs((Punti(i).y - MinY)) / ScalaY

                    If started AndAlso Punti(i).y >= MinY AndAlso Punti(i).y <= MaxY AndAlso
                       YValPrec >= MinY AndAlso YValPrec <= MaxY Then
                        gfx.DrawLine(penRed, XPrec, YPrec, tmpX, tmpY)
                    End If

                    XPrec = tmpX
                    YPrec = tmpY
                    YValPrec = Punti(i).y
                    started = True
                Next
            End Using

            picGrafico.Image = bmp

        Catch ex As Exception
            WriteError(0, "VisualizzaGrafico: " & ex.Message)
        End Try
    End Sub

    ' -------------------------------------------------------
    ' Disegna asse X con griglia e etichette
    ' -------------------------------------------------------
    Private Sub VisualizzaAsseX(g As Graphics)
        If ScalaX = 0 Then Exit Sub
        Using penGrid As New Pen(Color.Black) With {.DashStyle = Drawing2D.DashStyle.Dot}
        Using penAxis As New Pen(Color.Red)
        Using font As New Font("Arial", 6)
        Using brush As New SolidBrush(Color.Red)
            Dim tmpAz As System.Single = 0
            Do While tmpAz <= MaxX + 0.01F
                Dim tmpX As System.Single = tmpAz / ScalaX + DimCornice
                Dim tmpY As System.Single = picGrafico.Height - MargineBasso
                ' Linea verticale griglia
                g.DrawLine(penGrid, tmpX, DimCornice, tmpX, tmpY)
                ' Etichetta (ora dentro MargineBasso, non più tagliata fuori
                ' dal bitmap)
                Dim Lbl As String = Format(tmpAz - MaxX, "##0.#")
                g.DrawString(Lbl, font, brush, tmpX - 4, tmpY + 2)
                tmpAz += PassoX
            Loop
        End Using : End Using : End Using : End Using
    End Sub

    ' -------------------------------------------------------
    ' Disegna asse Y con griglia e etichette
    ' -------------------------------------------------------
    Private Sub VisualizzaAsseY(g As Graphics)
        If ScalaY = 0 Then Exit Sub
        Using penGrid As New Pen(Color.Black) With {.DashStyle = Drawing2D.DashStyle.Dot}
        Using font As New Font("Arial", 6)
        Using brush As New SolidBrush(Color.Black)
            Dim tmp2y As System.Single = MinY
            Do While tmp2y <= MaxY
                Dim tmpY As System.Single = (picGrafico.Height - MargineBasso) - (tmp2y - MinY) / ScalaY
                ' Linea orizzontale griglia
                g.DrawLine(penGrid, DimCornice, tmpY, picGrafico.Width - DimCornice, tmpY)
                ' EtichettaSystem.Math.Sinistra e destra
                Dim Lbl As String = Format(tmp2y, "#0.#")
                g.DrawString(Lbl, font, brush, 2, tmpY - 6)
                g.DrawString(Lbl, font, brush, picGrafico.Width - DimCornice, tmpY - 6)
                tmp2y += stepinc
            Loop
        End Using : End Using : End Using
    End Sub

    ' -------------------------------------------------------
    ' Funzioni troncamento scala (invariate)
    ' -------------------------------------------------------
    Private Function Tronca(Numero As System.Single) As System.Single
        Dim tmpNumero As System.Single =Math.Abs(Numero)
        If tmpNumero >= 10 Then
            tmpNumero = If(Numero >= 0, CInt(tmpNumero / 10) * 10, (CInt(tmpNumero / 10) + 1) * 10)
        Else
            tmpNumero = If(Numero >= 0, 0, tmpNumero + 1)
        End If
        Return If(Numero >= 0, CInt(tmpNumero), -CInt(tmpNumero))
    End Function

    Private Function Tronc1(Numero As System.Single) As System.Single
        Dim tmpNumero As System.Single =Math.Abs(Numero)
        If tmpNumero >= 10 Then
            If tmpNumero > 1000 Then
                tmpNumero = (CInt(tmpNumero / 100) + 1) * 100
            ElseIf tmpNumero < 1000 AndAlso tmpNumero > 10 AndAlso Numero >= 0 Then
                tmpNumero = (CInt(tmpNumero / 10) + 1) * 10
            ElseIf Numero < 0 Then
                tmpNumero = CInt(tmpNumero / 10) * 10
            End If
        Else
            tmpNumero = If(Numero > 0, 10, 0)
        End If
        Return If(Numero >= 0, CInt(tmpNumero), -CInt(tmpNumero))
    End Function

    ' -------------------------------------------------------
    ' KeyUp su TxtMax e TxtMin
    ' -------------------------------------------------------
    Private Sub TxtMax_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtMax.KeyUp
        If e.KeyCode = Keys.Return AndAlso Not IsNumeric(TxtMax.Text) Then
            TxtMax.Text = ""
        End If
    End Sub

    Private Sub TxtMin_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtMin.KeyUp
        If e.KeyCode = Keys.Return AndAlso Not IsNumeric(TxtMin.Text) Then
            TxtMin.Text = ""
        End If
    End Sub

    ' -------------------------------------------------------
    ' Riferimento a frmRicezione (per leggere TxtLev, LblBcn)
    ' -------------------------------------------------------
    Public frmRicezioneRef As frmRicezione

End Class
