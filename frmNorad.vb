Public Class FrmNorad
    Friend WithEvents TxtLinea1 As TextBox
    Friend WithEvents TxtLinea2 As TextBox
    Friend WithEvents TxtCatalogo As TextBox
    Friend WithEvents TxtEpoch As TextBox
    Friend WithEvents TxtDecay As TextBox
    Friend WithEvents TxtBSTAR As TextBox
    Friend WithEvents TxtElement As TextBox
    Friend WithEvents TxtInclination As TextBox
    Friend WithEvents TxtRAAN As TextBox
    Friend WithEvents TxtEccentricity As TextBox
    Friend WithEvents TxtPerigee As TextBox
    Friend WithEvents TxtAnomaly As TextBox
    Friend WithEvents TxtMotion As TextBox
    Friend WithEvents TxtOrbite As TextBox
    Friend WithEvents TxtEpochDate As TextBox
    Friend WithEvents TxtSatellite As TextBox
    Friend WithEvents LblInvalido As Label

    Dim tmpSql As String
    Dim julian_epoch1 As Double, s_url As String
    Dim rs_Norad As Object

    Private Sub FrmNorad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' MnuNorad_Click ha già il proprio gestore d'errore.
        MnuNorad_Click(2)
        If LblInvalido IsNot Nothing Then LblInvalido.BackColor = Color.White
    End Sub
    Private Sub MnuNorad_Click(Index As Integer)
        Dim abuf(2) As String
        Dim hFile As Long
        Dim Gr_file As Double, tmp_EpochTime As Double, EpochDate As Date
        Dim i_Scelta As Integer, b_Check As Boolean
        Dim sReadBuffer As String
        Dim tmpSql As String
        Dim iexp As Double          'esponenziale
        Dim ibexp As Double     'tipo effemeridi
        Try
        Select Case Index
            Case 0
                If IsNumeric(TxtEpoch.Text) And IsNumeric(TxtInclination.Text) And IsNumeric(TxtRAAN.Text) And IsNumeric(TxtEccentricity.Text) And IsNumeric(TxtPerigee.Text) _
        And IsNumeric(TxtMotion.Text) And IsNumeric(TxtAnomaly.Text) Then
                    If TxtInclination.Text = 0 Then TxtInclination.Text = 0.0001
                    tmpSql = "SELECT * FROM [Sat2line] order by Sat_Name"
                rs_Norad = dbOrbite.OpenRecordset(tmpSql, dbOpenDynaset)
                TxtSatellite.Text = s_Satellite
                    rs_Norad.FindFirst("Sat_Name ='" & s_Satellite & "'")
                If Not rs_Norad.NoMatch Then
                        rs_Norad.Edit
                    Else
                        rs_Norad.AddNew
                        rs_Norad("Sat_Name") = s_Satellite
                    End If
                    rs_Norad("linea1")    = TxtLinea1.Text
                    rs_Norad("linea2")    = TxtLinea2.Text
                    If IsNumeric(TxtCatalogo.Text) Then rs_Norad("Sat_Number") = TxtCatalogo.Text
                    rs_Norad("EpochTime") = CDbl(Val(TxtEpoch.Text))
                    tmp_EpochTime = CDbl(rs_Norad("EpochTime"))
                    tmp_EpochTime = Julian_Date_of_Epoch(tmp_EpochTime)
                    rs_Norad("EpochDate")    = Calendar_Date(tmp_EpochTime, 0) & " " & Time_of_Day(tmp_EpochTime, 1, 0)
                    rs_Norad("Ftdmm")        = CDbl(Val(TxtDecay.Text))
                    rs_Norad("Stdmm")        = 0.0
                    rs_Norad("bstar")        = CDbl(Val(TxtBSTAR.Text))
                    rs_Norad("Element")      = CInt(Val(TxtElement.Text))
                    rs_Norad("Inclination")  = CDbl(Val(TxtInclination.Text))
                    rs_Norad("Raan")         = CDbl(Val(TxtRAAN.Text))
                    rs_Norad("Eccentricity") = CDbl(Val(TxtEccentricity.Text))
                    rs_Norad("ArgPer")       = CDbl(Val(TxtPerigee.Text))
                    rs_Norad("Anomaly")      = CDbl(Val(TxtAnomaly.Text))
                    rs_Norad("MeanMo")       = CDbl(Val(TxtMotion.Text))
                    rs_Norad("RevEpoch")     = CInt(Val(TxtOrbite.Text))
                    rs_Norad.Update
                    rs_Norad.Close()
                End If
                s_Satellite = ""
                Me.Close()
    Case 1
                s_Satellite = ""
                Me.Close()
    Case 2
                tmpSql = "SELECT * FROM [Satelliti] order by NomeSatellite"
        rs_Norad = dbOrbite.OpenRecordset(tmpSql, dbOpenDynaset)
        TxtSatellite.Text = s_Satellite
                If s_Satellite = "" Then
                    TxtSatellite.BackColor = Color.White
                    LblInvalido.Text = "Satellite name not found"
                    b_Sat = False
                    b_Calcola = b_Sat
                    Exit Sub
                End If
                rs_Norad.FindFirst("NomeSatellite ='" & s_Satellite & "'")
        If rs_Norad("FileAgg") = "" Or IsDBNull(rs_Norad("FileAgg")) Then
                    LblInvalido.Text = "TLE data not found"
                    b_Sat = False
                    b_Calcola = b_Sat
                    rs_Norad.Close()
                    Exit Sub
                End If
                s_TwoLine = CStr(rs_Norad("FileAgg"))
                i = InStr(1, s_TwoLine, ":", 1)
                If i = 0 Then s_TwoLine = Application.StartupPath & "\" & s_TwoLine
                rs_Norad.Close()
                If s_TwoLine <> "" Then b_Check = AggiornaTLE(s_TwoLine, 2)
                If Not b_Check Then
                    TxtSatellite.BackColor = Color.Red
                    LblInvalido.Text = g_ParAntenna
                    Exit Sub
                Else
                    TxtSatellite.BackColor = Color.White
                End If
                Inizialize()
        End Select
        Catch ex As Exception
            WriteError(0, "MnuNorad_Click(" & Index & "): " & ex.Message)
        End Try
    End Sub
    Private Sub Inizialize()
        Try
        tmpSql = "SELECT * FROM [Sat2line] order by Sat_Name"
    rs_Norad = dbOrbite.OpenRecordset(tmpSql, dbOpenDynaset)
    TxtSatellite.Text = s_Satellite
        rs_Norad.FindFirst("Sat_Name ='" & s_Satellite & "'")
    If Not rs_Norad.NoMatch Then
            TxtLinea1.Text      = CStr(If(rs_Norad("linea1")      IsNot Nothing, rs_Norad("linea1"),      ""))
            TxtLinea2.Text      = CStr(If(rs_Norad("linea2")      IsNot Nothing, rs_Norad("linea2"),      ""))
            TxtCatalogo.Text    = CStr(If(rs_Norad("Sat_Number")  IsNot Nothing, rs_Norad("Sat_Number"),  ""))
            TxtEpoch.Text       = CStr(If(rs_Norad("EpochTime")   IsNot Nothing, rs_Norad("EpochTime"),   ""))
            TxtDecay.Text       = CStr(If(rs_Norad("Ftdmm")       IsNot Nothing, rs_Norad("Ftdmm"),       ""))
            TxtBSTAR.Text       = CStr(If(rs_Norad("bstar")       IsNot Nothing, rs_Norad("bstar"),       ""))
            TxtElement.Text     = CStr(If(rs_Norad("Element")     IsNot Nothing, rs_Norad("Element"),     ""))
            TxtInclination.Text = CStr(If(rs_Norad("Inclination") IsNot Nothing, rs_Norad("Inclination"), ""))
            TxtRAAN.Text        = CStr(If(rs_Norad("Raan")        IsNot Nothing, rs_Norad("Raan"),        ""))
            TxtEccentricity.Text = CStr(If(rs_Norad("Eccentricity") IsNot Nothing, rs_Norad("Eccentricity"), ""))
            TxtPerigee.Text     = CStr(If(rs_Norad("ArgPer")      IsNot Nothing, rs_Norad("ArgPer"),      ""))
            TxtAnomaly.Text     = CStr(If(rs_Norad("Anomaly")     IsNot Nothing, rs_Norad("Anomaly"),     ""))
            TxtMotion.Text      = CStr(If(rs_Norad("MeanMo")      IsNot Nothing, rs_Norad("MeanMo"),      ""))
            TxtOrbite.Text      = CStr(If(rs_Norad("RevEpoch")    IsNot Nothing, rs_Norad("RevEpoch"),    ""))
            If TxtEpoch.Text <> "" Or Not IsDBNull(TxtEpoch.Text) Then
                julian_epoch1 = Julian_Date_of_Epoch(TxtEpoch.Text)
                TxtEpochDate.Text = Calendar_Date(julian_epoch1, 0) & " " & Time_of_Day(julian_epoch1, 1, 0)
            End If
        Else
            TxtLinea1.Text = ""
            TxtLinea2.Text = ""
            TxtCatalogo.Text = 0
            TxtEpoch.Text = ""
            TxtDecay.Text = 0
            TxtBSTAR.Text = 0
            TxtElement.Text = 0
            TxtInclination.Text = ""
            TxtRAAN.Text = ""
            TxtEccentricity.Text = ""
            TxtPerigee.Text = ""
            TxtAnomaly.Text = ""
            TxtMotion.Text = ""
            TxtOrbite.Text = 0
        End If
        Catch ex As Exception
            WriteError(0, "Inizialize: " & ex.Message)
        End Try
    End Sub
    Public Sub AggNorad()
        Dim tmpsqlAgg As String
        Dim rs_Norad1 As Object
        Try
            If s_Internet = "N" Then Exit Sub
            ' Aggiorna è una colonna TEXT ("True"/"False", talvolta "1"/"-1" o
            ' residui storici come "Vero"/"Falso"): un "WHERE Aggiorna" nudo la
            ' valuta numericamente (SQLite tenta la conversione a numero), quindi
            ' con un valore testuale come "True" risultava sempre falso e la
            ' query non restituiva mai nessuna riga, anche con la casella spuntata.
            tmpsqlAgg = "SELECT * FROM [URL] WHERE UPPER(Aggiorna) IN ('TRUE','1','-1') order by URL"
            rs_Norad1 = dbOrbite.OpenRecordset(tmpsqlAgg, dbOpenDynaset)
            rs_Norad1.MoveFirst

            ' Raccoglie prima tutte le righe da scaricare, poi esegue i
            ' download in parallelo (erano uno alla volta, con 2s fissi di
            ' pausa dopo ciascuno — non presenti nel VB6 originale, nessuna
            ' giustificazione nota, costavano da soli 8s+ a ogni avvio con
            ' le 4 URL attualmente configurate). Col download in parallelo
            ' il tempo totale è quello del più lento, non la somma di tutti.
            Dim urls As New List(Of String)
            Dim dest As New List(Of String)
            Do While Not rs_Norad1.EOF
                Dim urlRiga As String = ""
                If Not IsDBNull(rs_Norad1("URL")) Then urlRiga = CStr(rs_Norad1("URL"))
                If Not (rs_Norad1("FileAgg") = "" OrElse IsDBNull(rs_Norad1("FileAgg"))) AndAlso urlRiga <> "" Then
                    Dim destPath As String = CStr(rs_Norad1("FileAgg"))
                    Dim idx As Integer = InStr(1, destPath, ":", 1)
                    If idx = 0 Then destPath = Application.StartupPath & "\" & destPath
                    urls.Add(urlRiga)
                    dest.Add(destPath)
                End If
                rs_Norad1.MoveNext
            Loop
            rs_Norad1.Close()

            Dim tasks(urls.Count - 1) As System.Threading.Tasks.Task
            For i_url As Integer = 0 To urls.Count - 1
                Dim urlLoc As String = urls(i_url)
                Dim destLoc As String = dest(i_url)
                tasks(i_url) = System.Threading.Tasks.Task.Run(
                    Sub()
                        Try
                            ' Nome completo "System.IO" obbligatorio: nel progetto esiste
                            ' una variabile globale "io As Double" (ModSGP4.vb) che, essendo
                            ' VB.NET case-insensitive, altrimenti vince su "IO.Path"/"IO.Directory".
                            Dim destDir As String = System.IO.Path.GetDirectoryName(destLoc)
                            If Not String.IsNullOrEmpty(destDir) AndAlso Not System.IO.Directory.Exists(destDir) Then
                                System.IO.Directory.CreateDirectory(destDir)
                            End If
                            Using wc As New System.Net.WebClient()
                                If Not wc.DownloadFileTaskAsync(urlLoc, destLoc).Wait(15000) Then
                                    Throw New TimeoutException("Timeout scaricando " & urlLoc)
                                End If
                            End Using
                        Catch exRiga As Exception
                            WriteError(0, "AggNorad (" & urlLoc & "): " & exRiga.Message)
                        End Try
                    End Sub)
            Next
            System.Threading.Tasks.Task.WaitAll(tasks)

            d_TLEDate = Now.AddHours(-i_Offset)

            '        s_Satellite = Satellite
            '10/01/2019 E' sembrato un aggiornamento eccessivo. Ce ne sono troppi
            '        b_Check = AggiornaTLE(s_TwoLine, 2)
            '                If Not b_Check Then
            '                    frmRicezione.LblSatellite.BackColor = vbYellow
            '                Else
            '                    frmRicezione.LblSatellite.BackColor = &HFFFF00
            '                End If
            '                Exit Sub
            '    Inizialize
        Catch ex As Exception
            WriteError(0, "AggNorad: " & ex.Message)
        End Try
    End Sub


    Private Sub TxtEpoch_KeyUp(KeyCode As Integer, Shift As Integer)
        If KeyCode = CR Then
            If TxtEpoch.Text <> "" Or Not IsDBNull(TxtEpoch.Text) Then
                julian_epoch1 = Julian_Date_of_Epoch(TxtEpoch.Text)
                TxtEpochDate.Text = Calendar_Date(julian_epoch1, 0) & " " & Time_of_Day(julian_epoch1, 1, 0)
            End If
        End If
    End Sub

    ' ── Menu Save (era MnuNorad_Click(0)) ────────────────────
    Private Sub MnuSave_Click(sender As Object, e As EventArgs) Handles MnuSave.Click
        MnuNorad_Click(0)
    End Sub

    ' ── Menu Cancel (era MnuNorad_Click(1)) ──────────────────
    Private Sub MnuClose_Click(sender As Object, e As EventArgs) Handles MnuClose.Click
        MnuNorad_Click(1)
    End Sub

End Class
