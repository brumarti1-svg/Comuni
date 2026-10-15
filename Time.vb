Imports Microsoft.VisualBasic.Devices
Imports System.Runtime.InteropServices

Module Time
    ' NOTE: giorno declared in Module1.vb
    Public DST As Boolean
    Public ds50 As Double

    Declare Function GetTimeZoneInformation Lib "kernel32" _
        (ByRef lpTimeZoneInformation As TIME_ZONE_INFORMATION) As Long

    Declare Sub GetSystemTime Lib "kernel32" _
        (ByRef lpSystemTime As SYSTEMTIME)

    ' SetSystemTime, GetTimeFormat, GetLocalTime declared in Predisposizioni.vb

    Private Declare Function InternetTimeToSystemTime Lib "wininet.dll" _
       (ByVal lpszTime As String,
        ByRef pst As SYSTEMTIME,
        ByVal dwReserved As Long) _
        As Long

    Public Structure SYSTEMTIME
        Public wYear As Short
        Public wMonth As Short
        Public wDayOfWeek As Short
        Public wDay As Short
        Public wHour As Short
        Public wMinute As Short
        Public wSecond As Short
        Public wMilliseconds As Short
    End Structure

    ' TIME_ZONE_ID_* constants declared in Predisposizioni.vb (Public)
    ' LOCALE_* / TIME_* constants declared in Predisposizioni.vb (Public)

    <StructLayout(LayoutKind.Sequential)>
    Public Structure TIME_ZONE_INFORMATION
        Public Bias As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)>
        Public StandardName() As Integer
        Public StandardDate As SYSTEMTIME
        Public StandardBias As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)>
        Public DaylightName() As Integer
        Public DaylightDate As SYSTEMTIME
        Public DaylightBias As Integer
    End Structure

    Public Enum cstDSTType
        cstDSTUnknown = 0
        cstDSTStandard = 1
        cstDSTDaylight = 2
    End Enum

    Private SysTime As SYSTEMTIME
    Private TZInfo As TIME_ZONE_INFORMATION
    Private TZStructure As cstDSTType

    Public Structure time_set
        Public yr As Double
        Public mo As Double
        Public dy As Double
        Public hr As Integer
        Public mi As Integer
        Public se As Integer
        Public hu As Integer
    End Structure

    Public Structure Clock
        Public clock_time As String
        Public DateStr As String
    End Structure

    Public Function Julian_Date_of_Year(year As Double) As Double
        ' Astronomical Formulae for Calculators, Jean Meeus, pages 23-25
        Dim a As Single, B As Single
        year -= 1
        a = Int(year / 100)
        B = Int(2 - a + (a / 4))
        Julian_Date_of_Year = Int(365.25 * year) + Int(30.6001 * 14) + 1720994.5 + B
    End Function

    Public Function Julian_Date_of_Epoch(epoch As Double) As Double
        Dim year As Double, epoch1 As Double
        epoch1 = epoch * 0.001
        year = Int(epoch1) + 2000
        giorno = (epoch1 - Int(epoch1)) * 1000
        Julian_Date_of_Epoch = Julian_Date_of_Year(year) + giorno
    End Function

    Public Function Time_of_Day(jd As Double, full As Boolean, res As Double) As String
        Dim hr As Single, mn As Single
        Dim factor As Double, Timea As Double, sc As Double
        Dim ctime As String, jd1 As Double

        res = IMin(IMax(0, res), 3)
        factor = Power(10, res)
        jd1 = jd + 0.5
        Timea = (jd1 - Int(jd1)) * secday
        Timea = Int((Timea * factor) / factor + 0.5)
        hr = Int(Timea / 3600)
        Timea -= 3600 * hr
        If hr = 24 Then hr = 0
        mn = Int(Timea / 60)
        sc = Timea - 60 * mn
        Timea = 1000000 + 10000 * hr + 100 * mn + sc
        ctime = Mid(CStr(CInt(Timea)), 2, 6)
        If full Then
            ctime = Mid(ctime, 1, 2) & ":" & Mid(ctime, 3, 2) & ":" & Mid(ctime, 5, 2)
        End If
        Time_of_Day = ctime
    End Function

    Public Function Julian_Date(ts As time_set) As Double
        Dim year As Double
        With ts
            year = ts.yr
            Julian_Date = Julian_Date_of_Year(year) + DOY(year, .mo, .dy) + Fraction_of_Day(.hr, .mi, .se, .hu)
        End With
    End Function

    Public Function Date_Time(jd As Double, ByRef ts As time_set) As Boolean
        Date_Time = True
        Dim da As Date = Calendar_Date(jd, 3)
        Dim daStr As String = da.ToString("dd/MM/yyyy")
        Dim tm As Clock
        tm.clock_time = Time_of_Day(jd, False, 3)
        With ts
            .yr = Val(Mid(daStr, 7, 4))
            .mo = Val(Mid(daStr, 4, 2))
            .dy = Val(Mid(daStr, 1, 2))
            .hr = Val(Mid(tm.clock_time, 1, 2))
            .mi = Val(Mid(tm.clock_time, 3, 2))
            .se = Val(Mid(tm.clock_time, 5, 2))
            .hu = Val(Mid(tm.clock_time, 8, 2))
        End With
    End Function

    Public Function Check_Date(ts As time_set) As Boolean
        Dim jt As Double
        Check_Date = True
        jt = Julian_Date(ts)
        Date_Time(jt, ts)
    End Function

    Public Function Time_to_UTC(ts As time_set) As Boolean
        Dim jt As Double
        Time_to_UTC = True
        jt = Julian_Date(ts)
        Date_Time(jt, ts)
    End Function

    Public Function Time_of_Effemeridi(jd As Double, full As Boolean, res As Double) As time_set
        Dim hr As Single, mn As Single
        Dim factor As Double, Timea As Double, sc As Double, msc As Double
        Dim ctime As String, jd1 As Double

        res = IMin(IMax(0, res), 3)
        factor = Power(10, res)
        jd1 = jd + 0.5
        Timea = CDbl((jd1 - Int(jd1)) * milsecday)
        Timea = CDbl(Int((Timea * factor) / factor + 0.5))
        hr = Int(Timea / 3600000)
        Timea -= 3600000 * hr
        If hr = 24 Then hr = 0
        mn = Int(Timea / 60000)
        Timea -= (60000 * mn)
        sc = Int(Timea / 1000)
        msc = (Timea - (sc * 1000))
        Timea = CDbl(1000000000 + 10000000 * hr + 100000 * mn + 1000 * sc + msc)
        ctime = Mid(CStr(CLng(Timea)), 2, 9)
        If full Then
            ctime = Mid(ctime, 1, 2) & ":" & Mid(ctime, 3, 2) & ":" & Mid(ctime, 5, 2) & ":" & Mid(ctime, 7, 2)
        End If
        With ts
            .hr = Val(Mid(ctime, 1, 2))
            .mi = Val(Mid(ctime, 3, 2))
            .se = Val(Mid(ctime, 5, 2))
            .hu = Val(Mid(ctime, 7, 3))
        End With
        Time_of_Effemeridi = ts
    End Function

    Public Function FormatTime(system_time As SYSTEMTIME, ByVal locale_id As Long) As String
        Dim FLAGS As Long
        Dim buf As String = New String(" "c, 200)
        Dim buflen As Integer

        FLAGS = 0
        FLAGS = FLAGS Or TIME_FORCE24HOURFORMAT

        If locale_id = 0 Then
            buflen = GetTimeFormat(locale_id, FLAGS, system_time, 0, buf, Len(buf))
        Else
            buflen = GetTimeFormat(locale_id, 0, system_time, 0, buf, Len(buf))
        End If
        FormatTime = Left(buf, 8)
    End Function

    Private Function DateForSQL(dteDate As Date) As String
        DateForSQL = Format(dteDate, "\m/dd/yyyy h:nn:ss AM/PM \")
    End Function

    Public Function SummerTime(Optional intYear As Long = -1) As Date
        If intYear = -1 Then intYear = Year(Now)
        Dim lngRet As Long
        Dim TZI As New TIME_ZONE_INFORMATION()
        lngRet = GetTimeZoneInformation(TZI)
        With TZI.DaylightDate
            SummerTime = GetSundate(.wMonth, .wDay, intYear).AddHours(.wHour)
        End With
    End Function

    Public Function StandardTime(Optional intYear As Long = -1) As Date
        If intYear = -1 Then intYear = Year(Now)
        Dim lngRet As Long
        Dim TZI As New TIME_ZONE_INFORMATION()
        lngRet = GetTimeZoneInformation(TZI)
        With TZI.StandardDate
            StandardTime = GetSundate(.wMonth, .wDay, intYear).AddHours(.wHour)
        End With
    End Function

    Private Function GetSundate(intMonth As Integer, intSun As Integer,
                            Optional intYear As Long = -1) As Date
        If intYear = -1 Then intYear = Year(Now)
        Dim varRet As Object
        Dim intDayOfWeek As Integer
        varRet = DateSerial(CInt(intYear), intMonth, 1)
        intDayOfWeek = Weekday(varRet)
        If intDayOfWeek <> 1 Then
            varRet = DateAdd("d", 8 - intDayOfWeek, varRet)
        End If
        varRet = DateAdd("ww", intSun - 1, varRet)
        GetSundate = varRet
    End Function

    Public ReadOnly Property DSTType() As cstDSTType
        Get
            Return TZStructure
        End Get
    End Property

    Public ReadOnly Property TimeZoneName() As String
        Get
            If TZStructure = cstDSTType.cstDSTStandard Then
                Return StringFromIntArray(TZInfo.StandardName)
            ElseIf TZStructure = cstDSTType.cstDSTDaylight Then
                Return StringFromIntArray(TZInfo.DaylightName)
            Else
                Return "Unknown"
            End If
        End Get
    End Property

    Public ReadOnly Property StandardTimeZoneName() As String
        Get
            Return StringFromIntArray(TZInfo.StandardName)
        End Get
    End Property

    Public ReadOnly Property DaylightTimeZoneName() As String
        Get
            Return StringFromIntArray(TZInfo.DaylightName)
        End Get
    End Property

    Public ReadOnly Property TimeZoneOffset() As Integer
        Get
            ' s_Ora = "L" (ora locale, nessuna conversione) era una modalità
            ' in disuso: tutti i dati del sistema sono in UTC, quindi lo
            ' scarto va applicato sempre, a prescindere da s_Ora.
            Dim off As Integer = CInt((TZInfo.Bias / 60) * -1)
            If DSTInEffect Then off += 1
            Return off
        End Get
    End Property

    Public ReadOnly Property TimeZoneBias() As Long
        Get
            Return TZInfo.Bias
        End Get
    End Property

    Public ReadOnly Property GMT() As Double
        Get
            With SysTime
                Return DateSerial(.wYear, .wMonth, .wDay).ToOADate() + _
                       TimeSerial(.wHour, .wMinute, .wSecond).ToOADate()
            End With
        End Get
    End Property

    Public ReadOnly Property DSTInEffect() As Boolean
        Get
            Return (TZStructure = cstDSTType.cstDSTDaylight)
        End Get
    End Property

    Public ReadOnly Property DaylightName() As String
        Get
            Return StringFromIntArray(TZInfo.DaylightName)
        End Get
    End Property

    Public ReadOnly Property StandardName() As String
        Get
            Return StringFromIntArray(TZInfo.StandardName)
        End Get
    End Property

    Public Sub Refresh()
        TZStructure = CType(CInt(GetTimeZoneInformation(TZInfo)), cstDSTType)
        GetSystemTime(SysTime)
    End Sub

    Public Function ConvertToGMT(DT As Double) As Double
        ConvertToGMT = DT - (i_Off - CInt(DSTInEffect)) / 24
    End Function

    Private Function StringFromIntArray(IntArray() As Integer) As String
        If IntArray Is Nothing Then Return ""
        Dim Ndx As Long = 0
        Dim c As String = ""
        Do Until Ndx >= IntArray.Length OrElse IntArray(Ndx) = 0
            c &= Chr(IntArray(Ndx))
            Ndx += 1
        Loop
        Return c
    End Function

    Public Function GetGmtTime(Optional StartingDate As Object = Nothing) As Date
        Dim Difference As Long
        Difference = GetTimeDifference()
        If StartingDate Is Nothing Then
            GetGmtTime = DateAdd("s", -Difference, Now)
        Else
            GetGmtTime = DateAdd("s", -Difference, CDate(StartingDate))
        End If
    End Function

    Public Function GetTimeDifference() As Long
        Dim tz As New TIME_ZONE_INFORMATION()
        Dim retcode As Long
        Dim Difference As Long
        retcode = GetTimeZoneInformation(tz)
        Difference = -tz.Bias * 60
        GetTimeDifference = Difference
        If retcode = TIME_ZONE_ID_DAYLIGHT Then
            If tz.DaylightDate.wMonth <> 0 Then
                GetTimeDifference = Difference - tz.DaylightBias * 60
            End If
        End If
    End Function

    Public Function GetTimeHere(gmtTime As Date) As Date
        Dim Differerence As Long
        Differerence = GetTimeDifference()
        GetTimeHere = DateAdd("s", Differerence, gmtTime)
    End Function

    Public Function InternetTimeToVbLocalTime(ByVal DateString As String) As Date
        DateString = Trim(DateString)
        If Mid(DateString, 11, 1) = "T" Then
            InternetTimeToVbLocalTime = Iso8601TimeToLocalVbTime(DateString)
        Else
            InternetTimeToVbLocalTime = Rfc822TimeToLocalVbTime(DateString)
        End If
    End Function

    Private Function Iso8601TimeToLocalVbTime(ByVal sIso8601 As String) As Date
        Dim syear As String
        Dim sMonth As String
        Dim sDay As String
        Dim sHour As String
        Dim sMinute As String
        Dim sSecond As String
        Dim sTimeZone As String
        Dim dtDateTime As Date
        Dim bSign As Boolean
        Dim dGMT As Long

        syear = Left(sIso8601, 4)
        sMonth = Mid(sIso8601, 6, 2)
        sDay = Mid(sIso8601, 9, 2)
        sHour = Mid(sIso8601, 12, 2)
        sMinute = Mid(sIso8601, 15, 2)
        sSecond = Mid(sIso8601, 18, 2)
        sTimeZone = Mid(sIso8601, 20)

        dtDateTime = New Date(CInt(syear), CInt(sMonth), CInt(sDay),
                              CInt(sHour), CInt(sMinute), CInt(sSecond))

        sTimeZone = Replace(sTimeZone, "Z", "+00:00", , , vbTextCompare)
        bSign = IIf(Left(sTimeZone, 1) = "+", True, False)
        dGMT = Val(Mid(sTimeZone, Len(sTimeZone) - 3, 2)) + (CInt(Right(sTimeZone, 2)) * 100 / 60)

        If bSign Then
            dtDateTime = DateAdd("H", -dGMT, dtDateTime)
        Else
            dtDateTime = DateAdd("H", dGMT, dtDateTime)
        End If

        Iso8601TimeToLocalVbTime = GetTimeHere(dtDateTime)
    End Function

    Private Function Rfc822TimeToLocalVbTime(sRfc822 As String) As Date
        Dim uSystemTime As SYSTEMTIME
        Dim sWWW As String
        Dim dGMT As Long
        Dim dtDateTime As Date
        Dim sSign As String
        Dim bSign As Boolean
        Dim sEscapedTime As String
        Dim sTimeZoneString As String
        Dim iPos As Integer

        sWWW = sRfc822
        If InStr(1, sWWW, "GMT", vbTextCompare) > 0 Then
            sWWW = Replace(sWWW, "GMT", "+0000")
        End If
        If Len(Trim(sWWW)) = 25 Then
            sWWW &= " +0000"
        End If
        If (InStr(1, sWWW, ",") = 0) Then sWWW = "Thu, " & sWWW
        Call InternetTimeToSystemTime(sWWW, uSystemTime, 0&)
        With uSystemTime
            dtDateTime = New Date(CInt(.wYear), CInt(.wMonth), CInt(.wDay),
                                  CInt(.wHour), CInt(.wMinute), CInt(.wSecond))
        End With

        sEscapedTime = Replace(sWWW, ":", "")
        sSign = Mid(sEscapedTime, Len(sEscapedTime) - 4, 1)
        bSign = IIf(sSign = "-", False, True)

        iPos = InStrRev(sWWW, " ")
        If iPos > 0 Then
            sTimeZoneString = Mid(sWWW, iPos + 2)
            sTimeZoneString = Replace(sTimeZoneString, ":", "")
            sTimeZoneString = Replace(sTimeZoneString, " ", "")
            dGMT = Val(Left(sTimeZoneString, 2)) + Val(Right(sWWW, 2)) * 100 / 60
        Else
            dGMT = 0
        End If

        If bSign Then
            dtDateTime = DateAdd("H", -dGMT, dtDateTime)
        Else
            dtDateTime = DateAdd("H", dGMT, dtDateTime)
        End If

        Rfc822TimeToLocalVbTime = GetTimeHere(dtDateTime)
    End Function

    Public Function Epoch_Time(jd As Double) As Double
        Dim year As Double, mo As Double, dy As Double
        Dim yr As Double, Time1 As Double
        Dim edate As Date, jd1 As Double
        edate = Calendar_Date(jd, 3)
        Dim edateStr As String = edate.ToString("dd/MM/yyyy")
        year = Val(Mid(edateStr, 7, 4))
        yr = Val(Mid(CStr(CInt(year)), 3, 2))
        mo = Val(Mid(edateStr, 4, 2))
        dy = Val(Mid(edateStr, 1, 2))
        jd1 = jd + 0.5
        Time1 = jd1 - Int(jd1)
        Epoch_Time = yr * 1000 + DOY(year, mo, dy) + Time1
    End Function

    Public Function DOY(yr As Double, mo As Double, dy As Double) As Single
        Dim MyDay As Integer
        Dim LeapYear As Boolean
        Dim i As Integer
        If yr Mod 4 = 0 Then
            If yr Mod 100 = 0 Then
                If yr Mod 400 = 0 Then
                    LeapYear = True
                Else
                    LeapYear = False
                End If
            Else
                LeapYear = True
            End If
        Else
            LeapYear = False
        End If
        DOY = 0
        For i = 1 To mo - 1
            Select Case Val(i)
                Case 1, 3, 5, 7, 8, 10, 12
                    MyDay = 31
                Case 2
                    If LeapYear Then
                        MyDay = 29
                    Else
                        MyDay = 28
                    End If
                Case 4, 11, 6, 9
                    MyDay = 30
            End Select
            DOY += MyDay
        Next
        DOY += dy
    End Function

    Public Function Fraction_of_Day(hr As Integer, mi As Integer, se As Integer, hu As Integer) As Double
        Fraction_of_Day = (hr + (mi + (se + hu / 1000) / 60) / 60) / 24
    End Function

    Public Function Calendar_Date(jd As Double, res As Double) As Date
        ' Astronomical Formulae for Calculators, Jean Meeus, pages 26-27
        Dim z As Single, gDay As Double, month As Single
        Dim a As Double, B As Double, c As Double, d As Double, e As Double, f As Double, alpha As Double
        Dim year As Double
        Dim factor As Double, jd1 As Double
        factor = 0.5 / secday / Power(10, res)
        jd1 = jd + 0.5
        f = (jd1 - Int(jd1))
        If (f + factor >= 1) Then
            jd += factor
            f = 0
        End If
        z = Int(jd + 0.5)
        If z < 2299161 Then
            a = z
        Else
            alpha = Int((z - 1867216.25) / 36524.25)
            a = z + 1 + alpha - Int(alpha / 4)
        End If
        B = a + 1524
        c = Int((B - 122.1) / 365.25)
        d = Int(365.25 * c)
        e = Int((B - d) / 30.6001)
        gDay = (B - d - Int(30.6001 * e) + f)
        If e < 13.5 Then
            month = Math.Round(e - 1)
        Else
            month = Math.Round(e - 13)
        End If
        If month > 2.5 Then
            year = c - 4716
        Else
            year = c - 4715
        End If
        Calendar_Date = New Date(CInt(year), CInt(month), CInt(Int(gDay)))
    End Function

    Public Function Time_from_UTC(ts As time_set) As Boolean
        Dim jt As Double
        Time_from_UTC = True
        jt = Julian_Date(ts)
        jt += i_Off / 24
        Date_Time(jt, ts)
    End Function

    Public Function ThetaG(epoch2 As Double) As Double
        ' Reference: The 1992 Astronomical Almanac, page B6.
        Dim UT As Double, TU As Double, GMST As Double, epoch1 As Double, jd As Double, year As Double
        UT = Modulus(epoch2 + 0.5, 1)
        epoch1 = epoch2 * 0.001
        year = Int(epoch1)
        year += 2000
        giorno = (epoch1 - Int(epoch1)) * 1000
        giorno = Int(giorno)
        jd = Julian_Date_of_Year(year) + giorno
        TU = (epoch2 - JulDate2000 - UT) / 36525
        GMST = 24110.54841 + TU * (8640184.812866 + TU * (0.093104 - TU * 0.0000062))
        GMST = Modulus(GMST + secday * omega_E * UT, secday)
        If (GMST < 0) Then GMST += secday
        ThetaG = twopi * GMST / secday
        ds50 = jd - 2433281.5 + UT
    End Function

    Public Function ThetaG_JD(jd As Double) As Double
        ' Reference: The 1992 Astronomical Almanac, page B6.
        Dim UT, TU, GMST, jd1 As Double
        jd1 = jd + 0.5
        UT = Modulus(jd1, 1)
        jd1 -= UT
        TU = (jd1 - 2451545.5) / 36525
        GMST = 24110.54841 + TU * (8640184.812866 + TU * (0.093104 - TU * 0.0000062))
        GMST = Modulus(GMST + secday * omega_E * UT, secday)
        If GMST < 0 Then GMST += secday
        ThetaG_JD = twopi * (GMST / secday)
    End Function

    Public Function Delta_ET(year As Double) As Double
        ' Values determined using data from 1950-1991 in the 1990 Astronomical Almanac.
        Delta_ET = 26.465 + 0.747622 * (year - 1950) + 1.886913 * Math.Sin(twopi * (year - 1975) / 33)
    End Function

End Module
