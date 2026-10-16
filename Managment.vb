Imports System.Runtime.CompilerServices
Imports Microsoft.SqlServer
Imports Microsoft.VisualBasic.Logging
Imports System.Text

Module Managment
    Dim tmpSql As String
    'Dim s_msgACU As String
    ' ***********************************************************
    Const TLE_LEN_LINE_DATA = 69
    Dim nlen As Integer
    Dim nSum As Integer

    Public m_Locale As Long
    Declare Function GetProfileString Lib "kernel32" _
Alias "GetProfileStringA" _
(ByVal lpAppName As String,
ByVal lpKeyName As String,
ByVal lpDefault As String,
ByVal lpReturnedString As String,
ByVal nSize As Long) As Long
    ' Id e Product KEY per BNComm
    'Public Const NPID = "19390-0702010016-00006"
    'Public Const NPKEY = "9394E7E29E909597"

    Sub ImpComboBox(Box As ComboBox, Valore As String)
        Dim Ind As Integer

        For Ind = 0 To (Box.Items.Count - 1)
            If Box.Items(Ind).ToString() = Valore Then
                Box.SelectedIndex = Ind
                Exit Sub
            End If
        Next Ind
        Box.SelectedIndex = -1
    End Sub

    ' Cache delle righe del file INI: LeggiFileIni veniva chiamata decine di
    ' volte in sequenza (es. da LeggiParametri) e ognuna rileggeva e
    ' ri-parsava l'intero file da disco — contribuiva da sola per oltre 2
    ' secondi all'avvio. La cache resta valida finché il timestamp di
    ' modifica del file non cambia (si invalida da sola se il file viene
    ' modificato da fuori); ScriviFileIni la aggiorna direttamente, senza
    ' bisogno di rileggere da disco dopo ogni scrittura.
    Private _iniCacheLines As String() = Nothing
    Private _iniCacheTimestamp As DateTime = DateTime.MinValue

    Private Function LeggiRigheIni(FileName As String) As String()
        Dim lastWrite As DateTime = System.IO.File.GetLastWriteTimeUtc(FileName)
        If _iniCacheLines Is Nothing OrElse _iniCacheTimestamp <> lastWrite Then
            _iniCacheLines = System.IO.File.ReadAllLines(FileName)
            _iniCacheTimestamp = lastWrite
        End If
        Return _iniCacheLines
    End Function

    Public Function LeggiFileIni(Section As String, key As String) As String
        Dim FileName As String = Application.StartupPath & "\SMOD1574.INI"
        If Not System.IO.File.Exists(FileName) Then
            MessageBox.Show("The file SMOD1574.INI is missing - The computer program will be closed ")
            End
        End If
        Dim inSection As Boolean = False
        For Each line As String In LeggiRigheIni(FileName)
            Dim trimmed As String = line.Trim()
            If trimmed.StartsWith("[") AndAlso trimmed.EndsWith("]") Then
                inSection = (String.Compare(trimmed.Substring(1, trimmed.Length - 2), Section, StringComparison.OrdinalIgnoreCase) = 0)
            ElseIf inSection AndAlso Not trimmed.StartsWith(";") AndAlso Not trimmed.StartsWith("'") Then
                Dim eqPos As Integer = trimmed.IndexOf("="c)
                If eqPos > 0 Then
                    Dim k As String = trimmed.Substring(0, eqPos).Trim()
                    If String.Compare(k, key, StringComparison.OrdinalIgnoreCase) = 0 Then
                        Return trimmed.Substring(eqPos + 1).Trim()
                    End If
                End If
            End If
        Next
        Return ""
    End Function

    ' Legge e riscrive il file riga per riga (niente WritePrivateProfileString):
    ' l'API Win32 mantiene una cache separata da System.IO.File, quindi una
    ' lettura subito dopo la scrittura poteva restituire il valore precedente.
    Public Sub ScriviFileIni(Section As String, key As String, Valore As String)
        Dim FileName As String = Application.StartupPath & "\SMOD1574.INI"
        Dim lines As New List(Of String)
        If System.IO.File.Exists(FileName) Then
            lines.AddRange(LeggiRigheIni(FileName))
        End If

        Dim sectionStart As Integer = -1
        Dim sectionEnd As Integer = lines.Count
        For i As Integer = 0 To lines.Count - 1
            Dim trimmed As String = lines(i).Trim()
            If trimmed.StartsWith("[") AndAlso trimmed.EndsWith("]") Then
                If sectionStart >= 0 Then
                    sectionEnd = i
                    Exit For
                End If
                If String.Compare(trimmed.Substring(1, trimmed.Length - 2), Section, StringComparison.OrdinalIgnoreCase) = 0 Then
                    sectionStart = i
                End If
            End If
        Next

        If sectionStart < 0 Then
            If lines.Count > 0 AndAlso lines(lines.Count - 1).Trim() <> "" Then lines.Add("")
            lines.Add("[" & Section & "]")
            lines.Add(key & "=" & Valore)
        Else
            Dim keyLine As Integer = -1
            For i As Integer = sectionStart + 1 To sectionEnd - 1
                Dim trimmed As String = lines(i).Trim()
                If Not trimmed.StartsWith(";") AndAlso Not trimmed.StartsWith("'") Then
                    Dim eqPos As Integer = trimmed.IndexOf("="c)
                    If eqPos > 0 AndAlso String.Compare(trimmed.Substring(0, eqPos).Trim(), key, StringComparison.OrdinalIgnoreCase) = 0 Then
                        keyLine = i
                        Exit For
                    End If
                End If
            Next
            If keyLine >= 0 Then
                lines(keyLine) = key & "=" & Valore
            Else
                lines.Insert(sectionEnd, key & "=" & Valore)
            End If
        End If

        System.IO.File.WriteAllLines(FileName, lines)
        _iniCacheLines = lines.ToArray()
        _iniCacheTimestamp = System.IO.File.GetLastWriteTimeUtc(FileName)
    End Sub

    Public Function CheckSum(Buffer As String) As String
        Dim CKS As Long
        Dim i As Integer
        Dim s_Buffer As String
        Dim LenBuff As Integer
        Dim j As String
        CKS = 0
        s_Buffer = Chr(STX) + Buffer
        LenBuff = Len(s_Buffer)
        ' verifico i dati ricevuti
        For i = 1 To LenBuff
            CKS = (CKS Xor Asc(Mid$(s_Buffer, i, 1))) Mod 256
        Next i
        ' verifico la CKS
        j = Hex(CKS \ 16) + Hex(CKS And &HF)
        CheckSum = s_Buffer + j + Chr(ETX)

    End Function
    Public Function CheckDC(Buffer As String) As String
        Dim CKS As Long
        Dim i As Integer
        Dim LenBuff As Integer
        Dim j As String

        CKS = 0
        LenBuff = Len(Buffer)
        ' verifico i dati ricevuti
        For i = 1 To LenBuff
            CKS = (CKS + (Asc(Mid$(Buffer, i, 1)) - 32)) Mod 95
        Next i
        ' verifico la CKS
        j = CKS + 32
        CheckDC = Buffer & Chr(j)

    End Function
    Public Function CRC16(Buffer As String) As String
        '19/06/2019 Implementato il calcolo del CRC 16 bit (preso da Internet e adattato)
        Dim CRC16Lo As Byte, CRC16Hi As Byte 'CRC register
        Dim CL As Byte, ch As Byte 'Polynomial codes & HA001
        Dim SaveHi As Byte, SaveLo As Byte


        Dim i As Integer
        Dim Flag As Integer
        Dim LenBuff As Integer

        CRC16Lo = &H0 '&HFF
        CRC16Hi = &H0 '&HFF
        CL = &H1
        ch = &HA0
        LenBuff = Len(Buffer)

        For i = 1 To LenBuff
            CRC16Lo = CRC16Lo Xor Asc(Mid$(Buffer, i, 1))   'for each data and CRC register XOR

            For Flag = 0 To 7
                SaveHi = CRC16Hi
                SaveLo = CRC16Lo
                CRC16Hi \= 2 'peak shift to the right one
                CRC16Lo \= 2 'shift to the right a low

                If ((SaveHi And &H1) = &H1) Then 'If the high byte last one for a
                    CRC16Lo = CRC16Lo Or &H80 'then the low byte shifted to the right after the meeting in front of a
                End If 'Otherwise, auto-fill 0

                If ((SaveLo And &H1) = &H1) Then 'If the LSB is 1, then XOR with the polynomial codes
                    CRC16Hi = CRC16Hi Xor ch
                    CRC16Lo = CRC16Lo Xor CL
                End If
            Next Flag
        Next i

        CRC16 = Hex(CRC16Hi) & Hex(CRC16Lo)
    End Function
    Public Function CRC16CCITT(Buffer As String) As String
        '19/06/2019 Implementato il calcolo del CRC16 bit CCITT  (preso da Internet e adattato)
        Dim x16 As Integer    ' we'll use this to hold the XOR mask
        Dim i As Integer
        Dim Flag As Integer
        Dim LenBuff As Integer
        Dim s As Byte
        Dim CRCChe As Integer
        x16 = &H8408
        CRCChe = &HFFFF

        LenBuff = Len(Buffer)

        For i = 1 To LenBuff
            s = Asc(Mid$(Buffer, i, 1))
            CRCChe = CRCChe Xor s
            For Flag = 0 To 7
                'XOR current D0 and next input bit to determine x16 value
                If (CRCChe And &H1) Then
                    CRCChe = (CRCChe / 2) Xor x16
                Else
                    CRCChe \= 2
                End If
            Next Flag
        Next i
        CRC16CCITT = Hex(CRCChe)
    End Function
    Public Function CRC16CCITTB(Buffer As String) As String
        '19/06/2019 Implementato il calcolo del CRC16 bit CCITT  (preso da Internet e adattato)
        Dim x16 As Integer    ' we'll use this to hold the XOR mask
        Dim i As Integer
        Dim Flag As Integer
        Dim LenBuff As Integer
        Dim s As Byte
        Dim CRCChe As Integer

        CRCChe = &HFFFF

        LenBuff = Len(Buffer)

        For i = 1 To LenBuff
            s = Asc(Mid$(Buffer, i, 1))
            For Flag = 0 To 7
                'XOR current D0 and next input bit to determine x16 value
                If ((CRCChe And &H1) Xor (s And &H1)) Then
                    '            x16 = &H8408
                    x16 = &H8005
                Else
                    x16 = &H0
                End If
                ' shift crc buffer
                CRCChe \= 2
                'XOR in the x16 value
                CRCChe = CRCChe Xor x16
                'shift input for next iteration
                s \= 2
            Next Flag
        Next i
        CRC16CCITTB = Hex(CRCChe)
    End Function


    Public Function InvioACU(s_send As String) As Boolean
        Dim Port As Integer, i_lun As Integer
        ' frmRicezione.LblRisposta not migrated
        InvioACU = True
        Try
            If s_send = "" Then Exit Function

            If Port_Control(ACU).Socket = "N" Then
                ' ComPort serial control not migrated
                Port_Control(ACU).Rec_State = PCR_WAIT
            Else
                SendMain(ACU, s_send)
                Port_Control(ACU).Rec_State = PCR_WAIT
            End If
            If g_Log Then ScriviLOG(s_send, 0)
            frmRicezione.TmrTimeoutMSG(ACU).Enabled = False
            frmRicezione.TmrTimeoutMSG(ACU).Enabled = True
            ScriviLOG1(s_send, 0)
        Catch ex As Exception
            WriteError(0, "InvioACU: " & ex.Message)
        End Try
End Function
    Public Function InitPar11() As Boolean
        On Error GoTo Par11Error
        Dim rs_INITPAR As Object
        Dim d_partenza As Date, d_nonvalido As Date
        InitPar11 = False
        rs_INITPAR = dbOrbite.OpenRecordset("Sat11Par", dbOpenDynaset)
        rs_INITPAR.FindFirst("Name ='" & s_Satellite & "'")
    If Not rs_INITPAR.NoMatch Then
            With NowPar11
                .EPOCYE = rs_INITPAR!year
                .EPOCMO = rs_INITPAR!mounth
                .EPOCDA = rs_INITPAR!day
                .EPOCHO = rs_INITPAR!hour
                .EPOCMI = rs_INITPAR!Minute
                .EPOCSE = rs_INITPAR!second
                .LM0 = rs_INITPAR!L0
                .LM1 = rs_INITPAR!L1
                .LM2 = rs_INITPAR!L2
                .LONC = rs_INITPAR!LONC
                .LONC1 = rs_INITPAR!LONC1
                .LONS = rs_INITPAR!LONS
                .LONS1 = rs_INITPAR!LONS1
                .LATC = rs_INITPAR!LATC
                .LATC1 = rs_INITPAR!LATC1
                .LATS = rs_INITPAR!LATS
                .LATS1 = rs_INITPAR!LATS1
                d_partenza = CDate(.EPOCDA + "/" + .EPOCMO + "/" + .EPOCYE + " " + .EPOCHO + ":" + .EPOCMI + ":" + .EPOCSE)
            End With
            d_nonvalido = d_partenza.AddDays(g_Validi - 1)
            If d_nonvalido.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) = NowUTC().ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
                FrmMain.stpMessaggio.Text = "File data not valid 24 hour"
                FrmMain.TmrMsg.Enabled = True
                '            Exit Function
            ElseIf DateAdd("d", 1, d_nonvalido) <= Now Then
                FrmMain.stpMessaggio.Text = "File data not valid over 24 hours"
                FrmMain.TmrMsg.Enabled = True
                Exit Function
                'Aggiornamento non possibile. Vai nel modo di back up
            End If
            InitPar11 = True
        Else
            '        Response = MessageBox.Show("Non sono disponibili i parametri per il Satellite selezionato!", vbOKOnly)
            FrmMain.stpMessaggio.Text = "No parameters available for satellite: " & s_Satellite
            FrmMain.TmrMsg.Enabled = True
        End If
        Exit Function
Par11Error:
        '    Response = MessageBox.Show("Non sono disponibili i parametri per il Satellite selezionato!", vbOKOnly)
        FrmMain.stpMessaggio.Text = "No parameters available for satellite: " & s_Satellite
        FrmMain.TmrMsg.Enabled = True
    End Function

    '////////////////////////////////////////////////////////////////////////////
    '// IsTleFormat()
    '// Returns true if "str" is a valid data line of a two-line element set,
    '//  else false.
    '//
    '// To be valid a line must:
    '//      Have as the first character the line number
    '//      Have as the second character a blank
    '//      Be TLE_LEN_LINE_DATA characters long
    '//      Have a valid checksum (note: no longer required as of 12/96)
    '//
    Public Function IsValidLine(TleLine As String, eTleLine As Integer) As Boolean

        nlen = Len(TleLine)

        IsValidLine = False
        'If (nlen <> TLE_LEN_LINE_DATA) Then Exit Function
        ' First char in string must be line number
        If (Val(Mid$(TleLine, 1, 1) <> eTleLine)) Then Exit Function

        ' Second char in string must be blank
        If (Mid$(TleLine, 2, 1) <> " ") Then Exit Function

        '
        '   NOTE: 12/96
        '   The requirement that the last char in the line data must be a valid
        '   checksum is too restrictive.

        '  // Last char in string must be checksum
        nSum = Check_Nor(TleLine)
        '16/01/19 Modificato per eliminare l'errore su stringa piùn corta
        If Mid$(TleLine, TLE_LEN_LINE_DATA, 1) = "" Then Exit Function
        If (nSum <> Mid$(TleLine, TLE_LEN_LINE_DATA, 1)) Then Exit Function


        IsValidLine = True

    End Function
    '////////////////////////////////////////////////////////////////////////////
    '/ CheckSum()
    '/ Calculate the check sum for a given line of TLE data, the last character
    '/ of which is the current checksum. (Although there is no check here,
    '/ the current checksum should match the one we calculate.)
    '/ The checksum algorithm:
    '/      Each number in the data line is summed, modulo 10.
    '/    Non-numeric characters are zero, except minus signs, which are 1.
    '/
    Public Function Check_Nor(TleLine As String) As Integer
        Dim i As Integer
        Dim ch As String
        ' The length is "- 1" because we don't include the current (existing)
        ' checksum character in the checksum calculation.
        '   nlen = Len(TleLine) - 1
        nlen = TLE_LEN_LINE_DATA - 1
        nSum = 0

        For i = 1 To nlen
            ch = Mid$(TleLine, i, 1)
            If (IsNumeric(ch)) Then
                nSum += ch
            ElseIf (ch = "-") Then
                nSum += 1
            End If
        Next i

        Check_Nor = nSum Mod 10

    End Function
    Public Sub ClearAllTxt(frm As Form)
        Dim ctrl As Control
        Dim i As Integer

        For i = 0 To (frm.Controls.Count - 1)
            ctrl = frm.Controls(i)
            'If it's a textbox, clear it
            If TypeOf ctrl Is TextBox Then
                ctrl.Text = ""
            ElseIf TypeOf ctrl Is ComboBox Then
                'ctrl.Index =
                ctrl.Text = ""
            ElseIf TypeOf ctrl Is MaskedTextBox Then
                ctrl.Text = "__/__/____"

            End If
        Next i

    End Sub
    Public Function ExtractValue(ByVal IDString As String, TextBuffer As String) As String
        'IDString passata per valore perchè ci si lavora sopra

        Dim dopo As Integer
        Dim IDLenght As Integer
        Dim prima As Integer
        Dim found As Boolean 'booleano

        'Inizializza la flag
        found = False
        ExtractValue = ""
        'Sommo lo spazio all'ID: ci deve essere!
        IDString += Space(1)
        'lungh. dell'ID + lo spazio dopo
        IDLenght = Len(IDString)

        'cerca la stringa e lo spazio all'interno di TextBuffer
        prima = InStr(TextBuffer, IDString)
        If prima <> 0 Then
            'TROVATA! ora il problema è estrarre quello che c'è dopo:
            prima += IDLenght
            'Salta eventuali blanks tra identificatore e valore numerico
            Do While Mid$(TextBuffer, prima, 1) = Space(1)
                prima += 1
            Loop
            'Cerca lo spazio successivo a partire da prima
            dopo = InStr(prima, TextBuffer, " ")
            If dopo <> 0 Then
                'isolato un intervallo buono
                'segnala il ritrovamento !
                found = True
                ExtractValue = Mid$(TextBuffer, prima, dopo - prima)
            End If
        End If

        If Not found Then
            'buffer scorretto
            'basta passare qui una sola volta per assegnare False alla publice
            '        BufferValido = False
            'Flag convenzionale
            ExtractValue = "---"
        End If

    End Function



    Public Sub BELL(Durata As Double)
        ' MnuFile not migrated; use simple delay via Threading
        Dim t0 As Double = Microsoft.VisualBasic.Timer
        Do While Microsoft.VisualBasic.Timer < t0 + Durata
            If Microsoft.VisualBasic.Timer < t0 Then Exit Do
        Loop
    End Sub



    Public Sub ScriviAlarm(ByVal Messaggio As String, Level As Integer)
        Dim rs_ScriviAlarm As Object
        Dim Buffer As String, d_data As String
        '    Dim Errore As Long
        Try
            ' preparo il buffer per la scrittura
            If Level = 1 Then FrmMain.TmrBeep.Enabled = True
            ' Era un'assegnazione diretta del valore Date (senza stringa
            ' formattata) per evitare un bug di Format$("dd/mm/yy...") con
            ' "mm" minuscolo, letto da VB.NET come minuti anche nella parte
            ' data (non come mese): produceva stringhe tipo "23/46/26" (46 =
            ' minuti al posto del mese). Ma salvare il Date grezzo nel campo
            ' TEXT usa la rappresentazione di default di .NET, che include i
            ' secondi con moltissimi decimali (es. "19:08:21.8142965Z").
            ' DateTime.ToString() coi token .NET (non Format$) non ha
            ' l'ambiguità mm/MM: minuti e mese sono sempre distinti dal caso,
            ' indipendentemente dal contesto.
            d_data = NowUTC().ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            '    Buffer = "*** " + Format$(Now, "dd-mm-yy hh.nn.ss") + "  " + Messaggio
            rs_ScriviAlarm = dbOrbite.OpenRecordset("Error_Messages", dbOpenDynaset)
            ' scrivo sulla tabella di log
            rs_ScriviAlarm.AddNew
            rs_ScriviAlarm("MSG_Date") = d_data
            rs_ScriviAlarm("Error") = Messaggio
            rs_ScriviAlarm("Level") = Level

            rs_ScriviAlarm.Update
            rs_ScriviAlarm = dbOrbite.OpenRecordset("tmp_Error_Messages", dbOpenDynaset)
            ' scrivo sulla tabella di log
            rs_ScriviAlarm.AddNew
            rs_ScriviAlarm("MSG_Date") = d_data
            rs_ScriviAlarm("Error") = Messaggio
            rs_ScriviAlarm("Level") = Level
            rs_ScriviAlarm.Update
            ' Datlog/tdbLog controls not migrated
            If Level = 1 Then i_Alarm = True            '3/2/2026
            rs_ScriviAlarm.Close
        Catch ex As Exception
            WriteError(0, "ScriviAlarm: " & ex.Message)
        End Try
    End Sub

    Public Function CalcoloVolt(Valore As String) As Double
        CalcoloVolt = CDbl(Asc(Mid$(Valore, 1, 1)) And &HF) + (CDbl(Asc(Mid$(Valore, 2, 1)) And &HF) * 16) + (CDbl(Asc(Mid$(Valore, 3, 1)) And &HF) * 256)
        CalcoloVolt *= Conv_Volt
    End Function

    ' Spezza Buffer in righe da lunMax caratteri (le righe successive alla
    ' prima rientrate di 4 spazi) e le scrive su hFile. segnalaAnomalia
    ' antepone "xxx " a ogni riga (usato da ScriviLOG1 quando l'orario della
    ' ACU risulta successivo a quello del PC).
    Private Sub ScriviWrappato(hFile As Integer, Buffer As String, lunMax As Integer, Optional segnalaAnomalia As Boolean = False)
        Dim Ind As Integer, i_Conto As Integer, i_lun As Integer, s_msg As String
        i_lun = Int(Len(Buffer) / lunMax)
        If i_lun > 0 Then
            For Ind = 1 To i_lun
                i_Conto = 1 + ((Ind - 1) * lunMax)
                If Ind = 1 Then
                    s_msg = Mid$(Buffer, i_Conto, lunMax)
                Else
                    s_msg = "    " + Mid$(Buffer, i_Conto, lunMax)
                End If
                If segnalaAnomalia Then s_msg = "xxx " & s_msg
                PrintLine(hFile, s_msg)
            Next
            i_lun = Len(Buffer) Mod lunMax
            If i_lun > 0 Then
                i_Conto = 1 + ((Ind - 1) * lunMax)
                s_msg = "    " + Mid$(Buffer, i_Conto, i_lun)
                PrintLine(hFile, s_msg)
            End If
        Else
            If segnalaAnomalia Then Buffer = "xxx " & Buffer
            PrintLine(hFile, Buffer)
        End If
    End Sub

    Sub ScriviLOG1(Messaggio As String, Tipo As Integer)
        Dim hFile As Long
        Dim Buffer As String
        Dim s_msg As String, Ind1 As System.Single
        Dim lunMax As Integer
        Dim mSec As Integer, year As Integer, month As Integer, day As Integer, hour As Integer
        Dim minut As Integer, second As Integer
        Dim t_Ciclo As SYSTEMTIME
        Try
            lunMax = 70
            Ind1 = 0

            GetSystemTime(t_Ciclo)
            With t_Ciclo
                year = .wYear
                month = .wMonth
                day = .wDay
                hour = .wHour
                minut = .wMinute
                second = .wSecond
                mSec = .wMilliseconds
            End With
            ' 08/01/2019 Impostato l'orario compreso il giorno per un calcolo più accurato
            s_msg = Format$(day, "00") & Format$(hour, "00") & Format$(minut, "00") & Format$(second, "00") & Format$(mSec, "000")

            ' preparo il buffer per la scrittura
            If Tipo = 0 Then
                Buffer = NowUTC().ToString("dd/MM/yy ", System.Globalization.CultureInfo.InvariantCulture) & Format$(hour, "00") & ":" & Format$(minut, "00") & ":" & Format$(second, "00") & "." & Format$(mSec, "000") & "; " & Messaggio
            Else
                '08/01/2019 Controlla che l'orario della ACU non sia successivo a quello del PC
                If s_MsgAcu <> "" Then Ind1 = CDbl(s_msg) - CDbl(s_MsgAcu)
                Buffer = "D " & NowUTC().ToString("dd/MM/yy ", System.Globalization.CultureInfo.InvariantCulture) & Format$(hour, "00") & ":" & Format$(minut, "00") & ":" & Format$(second, "00") & "." & Format$(mSec, "000") & "; " & Messaggio
            End If
            ' scrivo il messaggio sul file di log
            ' apro il file di log se non esiste lo creo
            hFile = FreeFile()
            s_msg = f_Log2
            FileOpen(hFile, Application.StartupPath + "\" & s_msg, OpenMode.Append)
            '    Open Canali(ACU).File_Log For Append As #

            ScriviWrappato(hFile, Buffer, lunMax, Ind1 < 0)
            FileClose(hFile)
        Catch ex As Exception
            WriteError(0, "ScriviLOG1: " & ex.Message)
        End Try
End Sub
    Public Sub ScriviLOG2(Messaggio As String, Tipo As Integer)
        Dim hFile As Long
        Dim Buffer As String
        Dim s_msg As String
        Dim lunMax As Integer
        Try
            lunMax = 70
            hFile = FreeFile()
            ' preparo il buffer per la scrittura
            If Tipo = 0 Then
                Buffer = NowUTC().ToString("dd/MM/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "  " + Messaggio
            Else
                Buffer = "D " & NowUTC().ToString("dd/MM/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "  " + Messaggio
            End If
            ' scrivo il messaggio sul file di log
            ' apro il file di log se non esiste lo creo
            s_msg = f_Log1
            FileOpen(hFile, Application.StartupPath + "\" & s_msg, OpenMode.Append)

            ScriviWrappato(hFile, Buffer, lunMax)
            FileClose(hFile)
        Catch ex As Exception
            WriteError(0, "ScriviLOG2: " & ex.Message)
        End Try
End Sub
    Public Function ScriviLOG3() As Boolean
        ' scrive sul file di log <giorno>.Txt i valori di azimuth, elevazione e beacon
        Dim hFile As Long
        Dim Buffer As String
        Dim Ind As Integer, i_lun As Integer, s_msg As String, Messaggio As String
        Dim lunMax As Integer, pos As Integer, i As Integer
        ScriviLOG3 = True
        Try
            hFile = FreeFile()
            If b_Parametri Then Exit Function
            Messaggio = " "

            If IsNumeric(s_Az) Then Messaggio &= "Az. " & Format$(CDbl(Mid(s_Az, 1, 6)) / 1000, "000.000") & "  "   'Chr(CR) & vbLf
            If IsNumeric(s_El) Then Messaggio &= "El. " & Format$(CDbl(Mid(s_El, 1, 5)) / 1000, "00.000") & "  "   'Chr(CR) & vbLf
            '                Messaggio &=   "Livello " & Mid(s_Lev1, 1, 1) + Format$(CDbl(Mid(s_Lev1, 2, 3)) / 100, "0.0") & "  "   'Chr(CR) & vbLf
            If IsNumeric(s_Lev) Then Messaggio &= "Lev. " & s_Lev & "  "
            If Not String.IsNullOrEmpty(s_OpMode) Then Messaggio &= s_OpMode & " "
            If frmRicezione.LblCommand.Text = "Augmented" Then
                If IsNumeric(frmRicezione.LblAzError.Text) Then Messaggio &= "Az.Off " & frmRicezione.LblAzError.Text & " "
                If IsNumeric(frmRicezione.LblElError.Text) Then Messaggio &= "El.Off " & frmRicezione.LblElError.Text & " "
                ' TxtAzEr/TxtElEr not migrated
            End If
            Buffer = NowUTC().ToString("dd/MM/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) & "   " & Messaggio
            ' scrivo il messaggio sul file di log
            ' apro il file di log se non esiste lo creo
            FileOpen(hFile, Application.StartupPath + "\" + f_Log, OpenMode.Append)
            PrintLine(hFile, Buffer)
            FileClose(hFile)
        Catch ex As Exception
            WriteError(0, "ScriviLOG3: " & ex.Message)
        End Try
End Function

    Public Function AggiornaTLE(File As String, Tipo As Integer) As Boolean
        'Tipo = 1   Two Line
        'Tipo = 2    Three line
        Dim abuf(2) As String
        Dim hFile As Long
        Dim Gr_file As Double
        Dim i_Scelta As Integer, b_CheckTLE As Boolean, tmpSql As String
        Dim rsTLE As Object = Nothing
        Dim sReadBuffer As String, tmp_EpochTime As Double
        Dim d_nonvalido As Date
        Dim n_error As Integer
        On Error GoTo ErrTle
        '10/01/2019 Aggiunta la variabile per passare diagnostici
        AggiornaTLE = False
        hFile = FreeFile()       'file handle
        FileOpen(hFile, File, OpenMode.Input)
       Gr_file = LOF(hFile)

        If Gr_file < LUNGFILE Then
            GoTo ErrTle
        End If
        'carica i dati dei parametro
        If Tipo = 2 Or Tipo = 3 Then                 'cerco il satellite
            i_Scelta = 0
            Do While Not EOF(hFile)
                sReadBuffer = LineInput(hFile)
             i_Scelta = InStr(1, sReadBuffer, s_Satellite, 1)
                If i_Scelta <> 0 Then Exit Do
                sReadBuffer = LineInput(hFile)
             sReadBuffer = LineInput(hFile)
         Loop
            ' occorre prelevare il buffer two-Line   abuf = sat_data(arg)
            '* Decode Card 1 *}
            If i_Scelta = 0 Then
                GoTo ErrTle
            End If
        End If
        abuf(1) = LineInput(hFile)
        b_CheckTLE = IsValidLine(abuf(1), 1)
        If Not b_CheckTLE Then
            g_ParAntenna = "Wrong checksum Line 1"
            FrmMain.stpMessaggio.Text = "Wrong checksum"
            FrmMain.TmrMsg.Enabled = True
        End If
        abuf(2) = LineInput(hFile)
    abuf(2) = Mid$(abuf(2), 1, 69)
        b_CheckTLE = IsValidLine(abuf(2), 2)
        If Not b_CheckTLE Then
            FrmMain.stpMessaggio.Text = "Wrong checksum"
            FrmMain.TmrMsg.Enabled = True
            g_ParAntenna = "Wrong checksum Line 2"
        End If
        '10/01/2019 Non era stato riportato il controllo dell'inclinazione
        '         If Val(Mid$(abuf(2), 9, 8)) = 0 Then  'inclinazione = 0 Valori non validi
        '            GoTo ERRFILE
        '         End If
        tmpSql = "SELECT * FROM [Sat2line] order by Sat_Name"
        rsTLE = dbOrbite.OpenRecordset(tmpSql, dbOpenDynaset)
        rsTLE.FindFirst("Sat_Name ='" & s_Satellite & "'")
         If Not rsTLE.NoMatch Then
            rsTLE.Edit
            '31/10/2018 I dati vengono comunque aggiornati anche se meno recenti rispetto a quelli giaà memorizzati
            '             tmp_EpochTime = rsTLE!EpochTime
            '             If tmp_EpochTime > Val(Mid$(abuf(1), 19, 14)) Then
            '                If Tipo <> 3 Then
            'Modificato il 30/01/2018
            '                    Response = MessageBox.Show("Dati memorizzati piu' recenti o uguali ai dati su file. Proseguire?", vbOKCancel, "Abort")
            '                    If Response = 2 Then
            'Non aggiornare il file perche' piu' recente del nuovo
            '                        rsTLE.Close
            '                        FileClose(hFile)
            '                        AggiornaTLE = True
            '                        Exit Function
            '                    End If
            '                End If
            '            End If
        Else
            rsTLE.AddNew
            rsTLE!Sat_Name = s_Satellite
        End If
        rsTLE!linea1 = abuf(1)
        rsTLE!linea2 = abuf(2)
        rsTLE!Sat_Number = Mid$(abuf(1), 3, 5)
        rsTLE!EpochTime = Val(Mid$(abuf(1), 19, 14))
        tmp_EpochTime = CDbl(rsTLE!EpochTime)
        tmp_EpochTime = Julian_Date_of_Epoch(tmp_EpochTime)
        rsTLE!EpochDate = Calendar_Date(tmp_EpochTime, 0) & " " & Time_of_Day(tmp_EpochTime, 1, 0)
        Dim sEpochDate As String = CStr(If(rsTLE!EpochDate IsNot Nothing, rsTLE!EpochDate, ""))
        If IsDate(sEpochDate) Then d_TLEFile = CDate(sEpochDate)
        rsTLE!Ftdmm = Val(Mid$(abuf(1), 34, 10))
        iexp = Val(Mid$(abuf(1), 51, 2))  'esponenziale
        rsTLE!Stdmm = Val(Mid$(abuf(1), 45, 6)) * 0.00001
        rsTLE!Stdmm = CDbl(rsTLE!Stdmm) * (10 ^ iexp)
        ibexp = Val(Mid$(abuf(1), 60, 2))     'tipo effemeridi
        rsTLE!bstar = Val(Mid$(abuf(1), 54, 6)) * 0.00001
        rsTLE!bstar = CDbl(rsTLE!bstar) * (10 ^ ibexp)
        rsTLE!TEphe = Val(Mid$(abuf(1), 63, 1))
        rsTLE!Element = Val(Mid$(abuf(1), 66, 3))
        'modificato il 20/12/2013 per precedente dimenticanza
        If Val(Mid$(abuf(2), 9, 8)) > 0 Then
            rsTLE!Inclination = Val(Mid$(abuf(2), 9, 8))
        Else
            rsTLE!Inclination = 0.0001
        End If
        rsTLE!Raan = Val(Mid$(abuf(2), 18, 8))
        rsTLE("Eccentricity") = Val(Mid$(abuf(2), 27, 7)) * 0.0000001    'eccentricita
        rsTLE!ArgPer = Val(Mid$(abuf(2), 35, 8))
        rsTLE!Anomaly = Val(Mid$(abuf(2), 44, 8))
        rsTLE!MeanMo = Val(Mid$(abuf(2), 53, 11))
        rsTLE!RevEpoch = Val(Mid$(abuf(2), 64, 5))
        '31/10/2018 Riposizionare il record del satellite e controllare la validità dei dati
        '10/01/2019 Tolta
        '             rsTLE.FindFirst("Sat_Name ='" & s_Satellite & "'")
        '10/01/2019 aggiunto g_ParAntenna per diagnostica
        Dim sED As String = CStr(If(rsTLE!EpochDate IsNot Nothing, rsTLE!EpochDate, ""))
        If IsDate(sED) Then
            EpochDate = CDate(sED)
            PTK = &H10         'Elementi validi
            g_ValTLE = CInt(Val(LeggiFileIni("Default", "ValTLE")))
            If g_ValTLE > 0 Then
                d_nonvalido = EpochDate.AddDays(g_ValTLE - 1)
                If d_nonvalido.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) = Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
                    PTK = &H20     'Elementi non validi < 24 ore
                ElseIf DateAdd("d", 1, d_nonvalido) <= Now Then
                    PTK = &H30 'elementi non validi > 24 ore
                    FrmMain.stpMessaggio.Text = "Not valid date TLE "
                    FrmMain.TmrMsg.Enabled = True
                    g_ParAntenna = "Not valid date TLE "
                    FileClose(hFile)
                    rsTLE.Close
                    Exit Function
                End If
            End If
        End If
        rsTLE.Update
        rsTLE.Close
        FileClose(hFile)
        ' "s_Satellite = Satellite" era un confronto tra due variabili
        ' ridondanti (unificate in s_Satellite): sempre vero, quindi rimosso.
        Define_Derived_Constants
        b_Sat = Convert_Satellite_Data(s_Satellite)
        b_Calcola = b_Sat
        If b_Calcola Then
            FrmRicezione.LblSatellite.BackColor = Color.LightGreen
        Else
            frmRicezione.LblSatellite.BackColor = Color.Yellow
        End If
        If Not b_Sat Then GoTo ErrTle
        AggiornaTLE = True     'Compara azimuth controlla che i primi due valori dellla traiettoria siano compatibili - Diff <=1
        Exit Function
ErrTle:
        n_error = Err.Number
        Err.Clear()

        Select Case n_error
            Case 53
                FrmMain.stpMessaggio.Text = "Satellite file not found"
                FrmMain.TmrMsg.Enabled = True
                g_ParAntenna = "Satellite file not found"
                FileClose(hFile)
            If rsTLE IsNot Nothing Then rsTLE.Close()
                WriteError(Err.Number, "Gestione - Aggiorna Tle " & Err.Description)
       Case 55                                  'File dati già aperto
                Resume Next
            Case Else
                FrmMain.stpMessaggio.Text = "Parameters not valid"
                FrmMain.TmrMsg.Enabled = True
                g_ParAntenna = "Parameters not valid"
                FileClose(hFile)
            If rsTLE IsNot Nothing Then rsTLE.Close()
                WriteError(Err.Number, "Gestione - Aggiorna Tle " & Err.Description)
    End Select
    End Function

    Public Sub ResAcu()
        Dim Opmod As Byte
        Opmod = s_Mod And &HF
        ' FrmMain.PortRe(ACU) - not migrated
        b_Ricezione(ACU) = False
        b_int(ACU) = True
        SEND_ACU = Chr(STX) & "09RES4F" & Chr(ETX)
        InvioACU(SEND_ACU)
    FrmMain.stpMessaggio.Text = "ACU reset"
        ScriviAlarm("Re ACU", 1)
    FrmMain.TmrMsg.Enabled = True
        Attesa(10)
            Select Case (Opmod)                 'Ripristina modo operativo
            Case 0
                SEND_ACU = Chr(STX) & "08 S79" & Chr(ETX)
            Case 1
                SEND_ACU = Chr(STX) & "09 P 5B" & Chr(ETX)
            Case 4
                SEND_ACU = CheckSum("10 AP ")
            Case 5
                SEND_ACU = Chr(STX) & "09 ST2C" & Chr(ETX)
            Case 7
                SEND_ACU = CheckSum("10 AQ ")
            Case Else
                SEND_ACU = Chr(STX) & "08 S79" & Chr(ETX)
        End Select
        If SEND_ACU <> "" Then InvioACU(SEND_ACU)
End Sub

    Public Sub CmdDate()
        Dim s_Data As String
        b_Ricezione(ACU) = False
        b_int(ACU) = True
        s_Data = NowUTC().ToString("dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture)
        SEND_ACU = CheckSum("19DATE " & s_Data)
        InvioACU(SEND_ACU)

End Sub
    Public Sub CmdTime()
        Dim s_Time As String
        ' Use the time now.
        ' Format the time.
        b_Ricezione(ACU) = False
        b_int(ACU) = True
        s_Time =
    NowUTC().ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
        SEND_ACU = CheckSum("19TIME " & s_Time)
        InvioACU(SEND_ACU)
End Sub
    Public Function RiempiMemory(File As String)
        Dim rs_RiempiMemory As Object
        Dim hFile As Long, FN As String, oral As String, salta As Boolean
        Dim Gr_file As Double, SEND1 As String, DayInseg As String
        Dim i_Scelta As Integer, b_Check As Boolean, tmpSql As String
        Dim rs2 As Object, Aggiungi As Boolean, UltimoGiorno As String = "", UltimoTime As String = ""
        Dim sReadBuffer As String, tmp_EpochTime As Double, Dato As String, lun As Integer, Quarto As Integer
        Try
        DayInseg = " "
        RiempiMemory = True
        hFile = FreeFile()         'file handle
        FileOpen(hFile, File, OpenMode.Input)
        Gr_file = LOF(hFile)

        '    If Gr_file < LUNGFILE Then
        '        GoTo ERRFILE
        '    End If

        FN = LCase$(Right$(File, 3))

        'carica i dati di azimuth ed elevazione
        i_Scelta = 0
        oral = NowUTC().ToString("HH:mm:ss")
        lun = Len(LTrim(oral))
        If SatChange Then
            DbExecute("Delete * from [TrackTable] ")       'cancella la tabella di memory track
        Else
            DbExecute("Delete * from [TrackTable] where day <#" & CDate(Format$(NowUTC(), "mm/dd/yyyy ")) & "#")       'cancella la tabella di memory track
            DbExecute("Delete * from [TrackTable]  where day=#" & CDate(Format$(NowUTC(), "mm/dd/yyyy ")) &
            "# and ora < #" & CDate(Mid$(oral, 1, lun - 3)) & "#")       'cancella la tabella di memory track
        End If
        tmpSql = "SELECT * FROM [TrackTable] ORDER BY day asc, Ora asc"
        rs_RiempiMemory = dbOrbite.OpenRecordset(tmpSql, dbOpenDynaset)
        If FN = "csv" Then
            If Not rs_RiempiMemory.BOF Then
                rs_RiempiMemory.MoveFirst
                UltimoGiorno = rs_RiempiMemory!day
                UltimoTime = CDate(rs_RiempiMemory!ora).ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture)
                Aggiungi = False
            Else
                Aggiungi = True
            End If
            Do While Not EOF(hFile)
                sReadBuffer = LineInput(hFile)
                i_Scelta = InStr(1, sReadBuffer, ";", 1)
                If i_Scelta <> 0 Then
                    Dato = Mid$(sReadBuffer, 1, i_Scelta - 1)
                    If IsDate(Dato) Then
                        If Aggiungi Then
                            rs_RiempiMemory.AddNew
                        Else
                            If CDate(DayInseg) = UltimoGiorno And UltimoTime <= CDate(Dato).ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) _
                            Or CDate(DayInseg) > UltimoGiorno Then
                                rs_RiempiMemory.FindFirst("day =#" & CDate(DayInseg) &
                                   "# and ora = #" & CDate(CDate(Dato).ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)) & "#")
                                If Not rs_RiempiMemory.NoMatch Then
                                    rs_RiempiMemory.Edit
                                Else
                                    rs_RiempiMemory.AddNew
                                End If
                            Else
                                salta = True
                            End If
                        End If
                        If Not salta Then
                            rs_RiempiMemory!day = DayInseg
                            rs_RiempiMemory!ora = CDate(Dato).ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                            lun = Len(sReadBuffer)
                            sReadBuffer = Mid$(sReadBuffer, i_Scelta + 1, lun - i_Scelta)
                            i_Scelta = InStr(1, sReadBuffer, ";", 1)
                            If i_Scelta <> 0 Then
                                Dato = Mid$(sReadBuffer, 1, i_Scelta - 2)
                                rs_RiempiMemory!az = Dato
                                lun = Len(sReadBuffer)
                                sReadBuffer = Mid$(sReadBuffer, i_Scelta + 1, lun - i_Scelta - 1)
                                rs_RiempiMemory!el = sReadBuffer
                            End If
                            rs_RiempiMemory.Update
                        End If
                    End If
                Else
                    Dato = RTrim(sReadBuffer)
                    If IsDate(Dato) Then DayInseg = CDate(Dato).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                End If
            Loop
        ElseIf FN = "Txt" Then
            If Not rs_RiempiMemory.BOF Then
                rs_RiempiMemory.MoveFirst
                UltimoGiorno = rs_RiempiMemory!day
                UltimoTime = CDate(rs_RiempiMemory!ora).ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture)
                Aggiungi = False
            Else
                Aggiungi = True
            End If
            Do While Not EOF(hFile)
                sReadBuffer = LineInput(hFile)
                i_Scelta = InStr(1, sReadBuffer, " ", 1)
                Dato = Mid$(sReadBuffer, 1, i_Scelta - 1)
                If IsDate(Dato) Then
                    DayInseg = CDate(Dato).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                    lun = Len(sReadBuffer)
                    sReadBuffer = Mid$(sReadBuffer, i_Scelta + 1, lun - i_Scelta + 1)
                    Dato = Mid$(sReadBuffer, 1, 8)

                    If Aggiungi Then
                        rs_RiempiMemory.AddNew
                    Else
                        If CDate(DayInseg) = UltimoGiorno And UltimoTime <= CDate(Dato).ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture) _
                            Or CDate(DayInseg) > UltimoGiorno Then
                            rs_RiempiMemory.FindFirst("day =#" & DayInseg &
                                   "# and ora = #" & CDate(CDate(Dato).ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture)) & "#")
                                If Not rs_RiempiMemory.NoMatch Then
                                rs_RiempiMemory.Edit
                            Else
                                rs_RiempiMemory.AddNew
                            End If
                        Else
                            salta = True
                        End If
                    End If
                    If Not salta Then
                        rs_RiempiMemory!day = DayInseg
                        rs_RiempiMemory!ora = CDate(Dato).ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                        i_Scelta = InStr(1, sReadBuffer, " ", 1)
                        lun = Len(sReadBuffer)
                        sReadBuffer = Mid$(sReadBuffer, i_Scelta, lun - i_Scelta + 1)
                        Dato = Mid$(sReadBuffer, 1, 9)
                        rs_RiempiMemory!az = Dato
                        sReadBuffer = Mid$(sReadBuffer, 10, 8)
                        rs_RiempiMemory!el = sReadBuffer
                        rs_RiempiMemory.Update
                    End If
                End If
            Loop
        End If
        rs_RiempiMemory.Close
        b_Calcola = True
        frmRicezione.LblSatellite.BackColor = Color.Yellow
        SatChange = False
        Catch ex As Exception
            b_Calcola = False
            RiempiMemory = False
            If TypeOf ex Is System.IO.FileNotFoundException Then
                FrmMain.stpMessaggio.Text = "Celestial Corp not found"
                FrmMain.TmrMsg.Enabled = True
            Else
                WriteError(0, "RiempiMemory: " & ex.Message)
            End If
            FileClose(hFile)
            If rs_RiempiMemory IsNot Nothing Then rs_RiempiMemory.Close()
        End Try
    End Function
    Public Function TrackTable() As Boolean
        Dim rs_TrackTable As Object
        Dim oral As String, ora1 As Date, ora2 As Date, AzTab As System.Single, ElTab As System.Single, AzTab1 As System.Single, ElTab1 As System.Single
        Dim lun As Integer, AngNeg As System.Single, Intervallo As System.Single, intervallo1 As System.Single
        oral = NowUTC().ToString("HH:mm:ss")
        tmpSql = "SELECT * FROM [TrackTable] ORDER BY day asc, Ora asc"
        rs_TrackTable = dbOrbite.OpenRecordset(tmpSql, dbOpenDynaset)
        TrackTable = False
        lun = Len(LTrim(oral))
        rs_TrackTable.FindFirst("day =#" & CDate(Format$(NowUTC(), "mm/dd/yyyy")) &
       "# and ora >= #" & Mid$(oral, 1, lun - 3) & "#")
    If rs_TrackTable.NoMatch Then
            '07/03/2018 Controllo se comunque esiste ancora un record di dati
            '       dbOrbite.Execute ("Delete * from [TrackTable] where day <#" & CDate(Format$(NowUTC(), "mm/dd/yyyy ")) & "#")       'cancella la tabella di memory track
            '         dbOrbite.Execute ("Delete * from [TrackTable]  where day=#" & CDate(Format$(NowUTC(), "mm/dd/yyyy ")) & _
            '       "# and ora < #" & CDate(Mid$(oral, 1, lun - 3)) & "#")       'cancella la tabella di memory track
            '       tmpSQL = "SELECT * FROM [TrackTable] ORDER BY day asc, Ora asc"
            '        rs_TrackTable = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
            '       frmRicezione.LblAzimuth = ""
            '       frmRicezione.LblElevazione = ""
            '       frmRicezione.LblAzimuthC.Text = ""
            '       frmRicezione.LblElevazioneC.Text = ""
            '      AzCalc = 0
            '      ElCalc = 0
            '       ScriviAlarm "Track Table not is possible", 1
            '       frmMain.stbMain1.Panels("messaggio") = "Track Table not is possible"
            '       frmMain.TmrMSG.Enabled = True
            ' '       frmRicezione.CmdAutoTrack_Click
            ' '       i_Status = OP_AUTOTRACK
            '      b_int(ACU) = True
            '      b_Ricezione(ACU) = False
            '      SEND_ACU = CheckSum("08 U")
            '      InvioACU SEND_ACU
            '      PTK = &H0
            '      frmRicezione.CmdOffset.Enabled = False
            '      frmRicezione.CmdOffset.Visible = False
            '      i_Status = OP_UNSTOW
            Exit Function
        End If
        ora1 = rs_TrackTable!ora
        If ora1 > oral Then
            rs_TrackTable.MovePrevious
            If rs_TrackTable.BOF Then
                rs_TrackTable.Close
                Exit Function
            End If
            ora1 = rs_TrackTable!ora
        End If
        AzTab = rs_TrackTable!az           'Valori di Az ed Elevazione da raggiungere
        ElTab = rs_TrackTable!el
        rs_TrackTable.MoveNext
        If rs_TrackTable.EOF Then              'finito il buffer
            rs_TrackTable.Close                    '14/03/2018 Ho finito la tabella, metto l'antenna in unstow
            frmRicezione.LblAzimuth.Text = ""
            frmRicezione.LblElevazione.Text = ""
            frmRicezione.LblAzimuthC.Text = ""
            frmRicezione.LblElevazioneC.Text = ""
            AzCalc = 0
            ElCalc = 0
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            SEND_ACU = CheckSum("08 U")
            InvioACU(SEND_ACU)
        Attesa(0.2)
        PTK = &H0
            frmRicezione.CmdOffset.Enabled = False
            frmRicezione.CmdOffset.Visible = False
            i_Status = OP_UNSTOW
            frmRicezione.LblCommand.Text = "Un Stow"
            Exit Function
        End If
        ora2 = rs_TrackTable!ora
        Intervallo = DateDiff("s", ora1, ora2)
        intervallo1 = DateDiff("s", oral, ora2)
        If Math.Abs(intervallo1) > 1000 Then
            TrackTable = False
            ora1 = rs_TrackTable!ora
            '        ora1 = Format$(DateAdd("n", -5, ora1), "hh:mm:ss")
            d_DataInizioProg = rs_TrackTable!day & " " & ora1
            g_AzTable = rs_TrackTable!az
            g_ElTable = rs_TrackTable!el
            '15/03/2018 Il valore dell'elevazioni viene portato al minimo conSENDito
            If g_ElTable < g_ElMin Then g_ElTable = g_ElMin
            FrmMain.stpProgram.Text = d_DataInizioProg
            d_DataFineProg = "31/12/2999 23.59"
            rs_TrackTable.Close                    '14/03/2018 Ho finito la tabella, metto l'antenna in unstow
            frmRicezione.LblAzimuth.Text = ""
            frmRicezione.LblElevazione.Text = ""
            frmRicezione.LblAzimuthC.Text = ""
            frmRicezione.LblElevazioneC.Text = ""
            AzCalc = 0
            ElCalc = 0
            frmRicezione.LblAzError.Text = ""
            frmRicezione.LblElError.Text = ""
            '20/10/2022
            AzOff = 0
            ElOff = 0
            '
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            i_Cont = 2
            SEND_ACU = CheckSum("08 U")
            InvioACU(SEND_ACU)
        Attesa(0.2)
        PTK = &H0
            frmRicezione.CmdOffset.Enabled = False
            frmRicezione.CmdOffset.Visible = False
            i_Status = OP_UNSTOW
            frmRicezione.LblCommand.Text = "Un Stow"
            Exit Function
        End If
        If Intervallo = 0 Then                      'Modificato il 28/02/2018
            TrackTable = False
            rs_TrackTable.Close
            Exit Function
        End If
        If Intervallo < 0 Then
            Intervallo = secday + Intervallo
        End If
        AzTab1 = rs_TrackTable!az              'Valori di Az ed Elevazione iniziali
        ElTab1 = rs_TrackTable!el
        rs_TrackTable.Close
        AngNeg = AzTab - AzTab1
        If Math.Abs(AngNeg) > 350 Then
            If AngNeg < 0 Then
                AngNeg = 360 + AngNeg
            Else
                AngNeg = 360 - AngNeg
            End If
        End If
        AzTab = AzTab1 + (AngNeg / ((Intervallo) * 1000)) * ((intervallo1) * 1000)
        If AzTab < 0 Then
            AzTab += 360
        ElseIf AzTab > 360 Then
            AzTab -= 360
        End If

        frmRicezione.LblAzimuth.Text = Format$(AzTab, "0.000")
        frmRicezione.LblElevazione.Text = Format$(ElTab1 + ((ElTab - ElTab1) / ((Intervallo) * 1000)) * ((intervallo1) * 1000), "0.000")
        AzCalc = CDbl(frmRicezione.LblAzimuth.Text)
        ElCalc = CDbl(frmRicezione.LblElevazione.Text)
        TrackTable = True
    End Function

    Public Sub ScriviLOG4(Messaggio As String, Tipo As Integer)
        '28/11/2018 Aggiunto la stampa del messaggio di intelsat
        Dim Buffer As String
        Dim Ind As Integer, i_lun As Integer, s_msg As String
        Dim lunMax As Integer
        Dim i_Conto As Integer, mSec As Integer, year As Integer, month As Integer, day As Integer, hour As Integer
        Dim minut As Integer, second As Integer
        Dim t_Ciclo As SYSTEMTIME
        Try
            lunMax = 70

            GetSystemTime(t_Ciclo)
            With t_Ciclo
                year = .wYear
                month = .wMonth
                day = .wDay
                hour = .wHour
                minut = .wMinute
                second = .wSecond
                mSec = .wMilliseconds
            End With
            ' preparo il buffer per la scrittura
            Buffer = NowUTC().ToString("dd/MM/yy ", System.Globalization.CultureInfo.InvariantCulture) & Format$(hour, "00") & ":" & Format$(minut, "00") & ":" & Format$(second, "00") & "." & Format$(mSec, "000") & " " & Messaggio
            ' scrivo il messaggio sul file di log
            ' apro il file di log se non esiste lo creo
            s_msg = f_Log3
            FileOpen(1, Application.StartupPath + "\" & s_msg, OpenMode.Append)
            '    FileOpen(1, Canali(ACU).File_Log, OpenMode.Append)
            i_lun = Len(Buffer) Mod lunMax

            PrintLine(1, Mid$(Buffer, 1, i_lun - 2))
            FileClose(1)
        Catch ex As Exception
            WriteError(0, "ScriviLOG4: " & ex.Message)
        End Try
    End Sub
    Public Sub Risoluzione()
        'Creata il 1/7/2020 per evitare a fronte di una nuova risoluzione di cambiare su tutti i programmi


        m_sizeX1 = 16000
        m_sizeY1 = 9000


    End Sub
    Public Sub Compatta()
        Dim Ind As Integer
        Dim dbTemp As Object

        Try
            ' Screen.MousePointer not migrated
            ' chiudo il database
            ' compatto il database
            If Dir(g_Server + "\TEMP1.MDB") <> "" Then Kill(g_Server + "\TEMP1.MDB")
            If Dir(g_Server + "\TEMP.MDB") <> "" Then Kill(g_Server + "\TEMP.MDB")
            frmRicezione.Close()
            dbOrbite.Close()
            FileCopy(g_Database, g_Server + "\TEMP.MDB")
            ' DBEngine.CompactDatabase not available in .NET - skip compaction
            FileCopy(g_Server + "\temp1.MDB", Application.StartupPath + "\Orbite.MDB")
            ' riapro i database
            ' dbOrbite = wsOrbite.OpenDatabase(g_Database) - wsOrbite not migrated
            frmRicezione.ShowDialog()
            FrmMain.stpMessaggio.Text = "Data compact"
            FrmMain.TmrMsg.Enabled = True
            ' Screen.MousePointer not migrated
        Catch ex As Exception
            FrmMain.stpMessaggio.Text = ex.Message
            FrmMain.TmrMsg.Enabled = True
            ' dbOrbite = wsOrbite.OpenDatabase(g_Database) - wsOrbite not migrated
            frmRicezione.ShowDialog()
            ' Screen.MousePointer not migrated
        End Try
    End Sub


End Module

