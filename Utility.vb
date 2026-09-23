Module Utility
    Public sol1 As Vector, sat1 As Vector
    Const civil = (-6 * GradiRad)
    Const nautical = (-12 * GradiRad)
    Const astronomical = (-18 * GradiRad)

    Public Structure Vector
        Public Valori() As Double
    End Structure

    Public Structure Matrix
        Public Bidim(,) As Double
    End Structure
    Public Solar_Vector As Vector
    Dim zero As Vector

    Public Function NewVector() As Vector
        Dim v As New Vector
        ReDim v.Valori(4)
        Return v
    End Function

    Public Function Calculate_Solar_Position(Time As Double, ByRef Solar_Vector As Vector) As Boolean
        Dim mjd As Double, year As Double, T As Double, m As Double
        Dim L As Double, e As Double, c As Double, O As Double
        Dim Lsa As Double, nu As Double, r As Double, eps As Double
        Calculate_Solar_Position = True
        Solar_Vector.Valori(1) = 0
        Solar_Vector.Valori(2) = 0
        Solar_Vector.Valori(3) = 0
        Solar_Vector.Valori(4) = 0
        mjd = Time - JulDate2000
        year = 2000 + Int(mjd / 365.25)
        T = (mjd + Delta_ET(year) / secday) / GIOSEC
        m = GradiRad * (Modulus(358.47583 + Modulus(35999.04975 * T, 360) - (0.00015 + 0.0000033 * T) * (T ^ 2), 360))
        L = GradiRad * (Modulus(279.69668 + Modulus(36000.76892 * T, 360) + 0.0003025 * (T ^ 2), 360))
        e = 0.01675104 - (0.0000418 + 0.000000126 * T) * T
        c = GradiRad * ((1.91946 - (0.004789 + 0.000014 * T) * T) * Math.Sin(m) + (0.020094 - 0.0001 * T) * Math.Sin(2 * m) + 0.000293 * Math.Sin(3 * m))
        O = GradiRad * (Modulus(259.18 - 1934.142 * T, 360))
        Lsa = Modulus(L + c - GradiRad * (0.00569 - 0.00479 * Math.Sin(O)), twopi)
        nu = Modulus(m * c, twopi)
        r = 1.0000002 * (1 - (e ^ 2)) / (1 + e * Math.Cos(nu))
        eps = GradiRad * (23.452294 - (0.0130125 + (0.00000164 - 0.000000503 * T) * T) * T + 0.00256 * Math.Cos(O))
        r = AU * r
        Solar_Vector.Valori(1) = r * Math.Cos(Lsa)
        Solar_Vector.Valori(2) = r * Math.Sin(Lsa) * Math.Cos(eps)
        Solar_Vector.Valori(3) = r * Math.Sin(Lsa) * Math.Sin(eps)
        Solar_Vector.Valori(4) = r
    End Function

    Public Function Sat_Eclipsed(sat As Vector, sol As Vector, ByRef depth As Double) As Boolean
        Dim sd_sun As Double, sd_earth As Double
        Dim delta As Double
        Dim rho As Vector, earth As Vector
        rho = NewVector()
        earth = NewVector()
        sol1 = sol
        sat1 = sat
        sd_earth = ArcSin(xkmper / sat.Valori(4))
        Vec_Sub(sol1, sat1, rho)
        sd_sun = ArcSin(SR / rho.Valori(4))
        Scalar_Multiply(-1, sat, earth)
        delta = Angle(sol, earth)
        depth = sd_earth - sd_sun - delta
        If sd_earth < sd_sun Then
            Sat_Eclipsed = False
        Else
            If depth >= 0 Then
                Sat_Eclipsed = True
            Else
                Sat_Eclipsed = False
            End If
        End If
    End Function

    Public Function Sign(arg As Double) As Integer
        If arg > 0 Then
            Sign = 1
        ElseIf arg < 0 Then
            Sign = -1
        Else
            Sign = 0
        End If
    End Function

    Public Function Cube(arg As Double) As Double
        Cube = arg * (arg ^ 2)
    End Function

    Public Function Power(arg As Double, pwr As Double) As Double
        If arg > 0 Then
            Power = Math.Exp(pwr * Math.Log(arg))
        Else
            Power = 0
            MainForm.stpMessaggio.Text = "Invalid argument in function Power!"
            MainForm.TmrMsg.Enabled = True
        End If
    End Function

    Public Function ArcSin(arg As Double) As Double
        If Math.Abs(arg) >= 1 Then
            ArcSin = Sign(arg) * PIGREC / 2
        Else
            ArcSin = Math.Atan(arg / Math.Sqrt(1 - (arg ^ 2)))
        End If
    End Function

    Public Function ArcCos(arg As Double) As Double
        ArcCos = PIGREC / 2 - ArcSin(arg)
    End Function

    Public Function Modulus(arg1 As Double, arg2 As Double) As Double
        Dim i As Double
        Dim modu As Double
        i = Int(arg1 / arg2)
        modu = arg1 - i * arg2
        If modu >= 0 Then
            Modulus = modu
        Else
            Modulus = modu + arg2
        End If
    End Function

    Public Function Fmod2p(arg As Double) As Double
        Fmod2p = Modulus(arg, twopi)
    End Function

    Public Function Magnitude(ByRef v As Vector) As Boolean
        Magnitude = True
        v.Valori(4) = Math.Sqrt((v.Valori(1) ^ 2) + (v.Valori(2) ^ 2) + (v.Valori(3) ^ 2))
    End Function

    Public Function Vec_Add(v1 As Vector, v2 As Vector, ByRef v3 As Vector) As Boolean
        Vec_Add = True
        Dim i As Byte
        For i = 1 To 3
            v3.Valori(i) = v1.Valori(i) + v2.Valori(i)
        Next
        Magnitude(v3)
    End Function

    Public Function Vec_Sub(v1 As Vector, v2 As Vector, ByRef v3 As Vector) As Boolean
        Dim i As Byte
        Vec_Sub = True
        For i = 1 To 3
            v3.Valori(i) = v1.Valori(i) - v2.Valori(i)
        Next
        Magnitude(v3)
    End Function

    Public Function Scalar_Multiply(k As Double, v1 As Vector, ByRef v2 As Vector) As Boolean
        Dim i As Byte
        Scalar_Multiply = True
        For i = 1 To 3
            v2.Valori(i) = k * v1.Valori(i)
        Next
        v2.Valori(4) = Math.Abs(k) * v1.Valori(4)
    End Function

    Public Function Dot(v1 As Vector, v2 As Vector) As Double
        Dot = v1.Valori(1) * v2.Valori(1) + v1.Valori(2) * v2.Valori(2) + v1.Valori(3) * v2.Valori(3)
    End Function

    Public Function Angle(v1 As Vector, v2 As Vector) As Double
        Magnitude(v1)
        Magnitude(v2)
        Angle = ArcCos(Dot(v1, v2) / (v1.Valori(4) * v2.Valori(4)))
    End Function

    Public Function Cross(v1 As Vector, v2 As Vector, ByRef v3 As Vector) As Boolean
        Cross = True
        v3.Valori(1) = v1.Valori(2) * v2.Valori(3) - v1.Valori(3) * v2.Valori(2)
        v3.Valori(2) = v1.Valori(3) * v2.Valori(1) - v1.Valori(1) * v2.Valori(3)
        v3.Valori(3) = v1.Valori(1) * v2.Valori(2) - v1.Valori(2) * v2.Valori(1)
        Magnitude(v3)
    End Function

    Public Function Normalize(ByRef v As Vector) As Boolean
        Dim i As Byte
        Normalize = True
        For i = 1 To 3
            v.Valori(i) = v.Valori(i) / v.Valori(4)
        Next
    End Function

    Function IMin(arg1 As Integer, arg2 As Integer) As Integer
        If arg1 < arg2 Then
            IMin = arg1
        Else
            IMin = arg2
        End If
    End Function

    Public Function IMax(arg1 As Integer, arg2 As Integer) As Integer
        If arg1 > arg2 Then
            IMax = arg1
        Else
            IMax = arg2
        End If
    End Function

    Public Function RMin(arg1 As Single, arg2 As Single) As Single
        If arg1 < arg2 Then
            RMin = arg1
        Else
            RMin = arg2
        End If
    End Function

    Public Function RMax(arg1 As Single, arg2 As Single) As Single
        If arg1 > arg2 Then
            RMax = arg1
        Else
            RMax = arg2
        End If
    End Function

    Public Function DMin(arg1 As Double, arg2 As Double) As Double
        If arg1 < arg2 Then
            DMin = arg1
        Else
            DMin = arg2
        End If
    End Function

    Public Function DMax(arg1 As Double, arg2 As Double) As Double
        If arg1 > arg2 Then
            DMax = arg1
        Else
            DMax = arg2
        End If
    End Function

    Public Function AcTan(sinx As Double, cosx As Double) As Double
        If cosx = 0 Then
            If sinx > 0 Then
                AcTan = PIGREC / 2
            Else
                AcTan = 3 * PIGREC / 2
            End If
        ElseIf cosx > 0 Then
            AcTan = Math.Atan(sinx / cosx)
        Else
            AcTan = PIGREC + Math.Atan(sinx / cosx)
        End If
    End Function

    Public Sub Attesa(Tempo As Single)
        Dim inizio As Single
        inizio = CSng(Now.TimeOfDay.TotalSeconds)
        Do While CSng(Now.TimeOfDay.TotalSeconds) < inizio + Tempo
            Application.DoEvents()
            If CSng(Now.TimeOfDay.TotalSeconds) < inizio Then Exit Do
        Loop
    End Sub

    Public Function SolarAzEl(timex As Double, ByRef pos As Vector) As Boolean
        Dim jd As Double, d As Double, W As Double, e As Double, m As Double, L As Double, oblecl As Double
        Dim X As Double, y As Double, z As Double, r As Double, v As Double, xeclip As Double, yeclip As Double, zeclip As Double
        Dim xequat As Double, yequat As Double, zequat As Double, RA As Double, delta As Double, J2000 As Double
        Dim UTH As Double, GMST0 As Double, SIDTIME As Double, HA As Double, xhor As Double, yhor As Double, zhor As Double
        Dim az As Double, el As Double, Ex As Double, Laty As Double, Lony As Double, Lon As Double
        Laty = LATSTA11
        Lony = LONSTA11

        jd = timex
        d = jd - JulDate2000
        W = 282.9404 + 0.0000470935 * d
        e = 0.016709 - 0.000000001151 * d
        m = Modulus(356.047 + 0.9856002585 * d, 360)
        L = W + m
        oblecl = 23.4393 - 0.0000003563 * d

        Ex = m + (RadGradi) * e * Math.Sin(m * (GradiRad)) * (1 + e * Math.Cos(m * (GradiRad)))

        X = Math.Cos(Ex * (GradiRad)) - e
        y = Math.Sin(Ex * (GradiRad)) * Math.Sqrt(1 - e ^ 2)

        r = Math.Sqrt(X ^ 2 + y ^ 2)
        v = Atan2(y, X) * (RadGradi)

        Lon = v + W

        xeclip = r * Math.Cos(Lon * (GradiRad))
        yeclip = r * Math.Sin(Lon * (GradiRad))
        zeclip = 0

        xequat = xeclip
        yequat = yeclip * Math.Cos(oblecl * (GradiRad)) + zeclip * Math.Sin(oblecl * (GradiRad))
        zequat = yeclip * Math.Sin(23.4406 * (GradiRad)) + zeclip * Math.Cos(oblecl * (GradiRad))

        r = Math.Sqrt(xequat ^ 2 + yequat ^ 2 + zequat ^ 2) - (h / 149598000)
        RA = Atan2(yequat, xequat) * (RadGradi)
        delta = Asin(zequat / r)
        delta *= RadGradi

        J2000 = jd - JulDate2000
        ts = Time_of_Effemeridi(timex, False, 3)
        UTH = ts.hr + ts.mi / 60 + ts.se / 3600

        GMST0 = Modulus(L + 180, 360) / 15
        SIDTIME = GMST0 + UTH + Lony / 15

        HA = (SIDTIME * 15 - RA)

        X = Math.Cos(HA * (GradiRad)) * Math.Cos(delta * (GradiRad))
        y = Math.Sin(HA * (GradiRad)) * Math.Cos(delta * (GradiRad))
        z = Math.Sin(delta * (GradiRad))

        xhor = X * Math.Cos((90 - Laty) * (GradiRad)) - z * Math.Sin((90 - Laty) * (GradiRad))
        yhor = y
        zhor = X * Math.Sin((90 - Laty) * (GradiRad)) + z * Math.Cos((90 - Laty) * (GradiRad))

        az = Atan2(yhor, xhor) * (RadGradi) + 180.0
        el = Asin(zhor) * RadGradi
        pos.Valori(1) = az
        pos.Valori(2) = el
        Return True
    End Function

    Public Function Atan2(y As Double, X As Double) As Double
        If X = 0 Then
            If y > 0 Then
                Atan2 = PIGREC / 2
            Else
                Atan2 = -PIGREC / 2
            End If
        Else
            Atan2 = Math.Atan(y / X)
            If X < 0 Then Atan2 += PIGREC
        End If
    End Function

    Public Function FNAtan2(y As Double, X As Double) As Double
        Dim a As Double
        a = Math.Atan(y / X)
        If X < 0 Then a += PIGREC
        If y < 0 And X > 0 Then a += twopi
        FNAtan2 = a
    End Function

    Public Function Asin(X As Double) As Double
        If X >= 1.0 Then Asin = 3.14159265358979 / 2.0 : Exit Function
        If X <= -1.0 Then Asin = -3.14159265358979 / 2.0 : Exit Function
        Asin = Math.Atan(X / Math.Sqrt(1.0 - X * X))
    End Function

    Public Function MoonAzEl(timex As Double, ByRef pos As Vector) As Boolean
        Dim jd As Double, d As Double, W As Double, a As Double, e As Double, m As Double, L As Double, oblecl As Double
        Dim X As Double, y As Double, z As Double, r As Double, v As Double, xeclip As Double, yeclip As Double, zeclip As Double
        Dim xequat As Double, yequat As Double, zequat As Double, RA As Double, delta As Double, J2000 As Double
        Dim UTH As Double, GMST0 As Double, SIDTIME As Double, HA As Double, xhor As Double, yhor As Double, zhor As Double
        Dim az As Double, el As Double, e0 As Double, E1 As Double, Laty As Double, Lony As Double, Lon As Double, n As Double, ii As Double
        Dim T As Double
        Laty = LATSTA11
        Lony = LONSTA11

        jd = timex
        d = jd - JulDate2000
        n = 125.1228 - 0.0529538083 * d
        ii = 0
        W = 318.0634 + 0.16435732235 * d
        a = 60.2666
        e = 0.0001
        m = 333.1953
        L = 0.03660099000006
        n = rev(n)
        W = rev(W)
        m = rev(m)
        e0 = m + (RadGradi) * e * Math.Sin(m) * (1 + e * Math.Cos(m))

        Do While Math.Abs(e0 - E1) > 0.005
            E1 = e0 - (e0 - (RadGradi) * e * Math.Sin(e0) - m) / (1 - e * Math.Cos(e0))
            e0 = E1
        Loop
        E1 *= GradiRad
        X = a * (Math.Cos(E1) - e)
        y = a * Math.Sqrt(1 - e * e) * Math.Sin(E1)

        r = Math.Sqrt(X ^ 2 + y ^ 2)
        v = Atan2(y, X)
        Lon = v + W * GradiRad
        n += GradiRad
        ii *= GradiRad
        xeclip = r * (Math.Cos(n) * Math.Cos(Lon) - Math.Sin(n) * Math.Sin(Lon) * Math.Cos(ii))
        yeclip = r * (Math.Sin(n) * Math.Cos(Lon) + Math.Cos(n) * Math.Sin(Lon) * Math.Cos(ii))
        zeclip = r * Math.Sin(Lon) * Math.Sin(ii)
        T = (jd - JulDate2000) / 36525
        oblecl = 23.4391 - 0.0130042 * T - 0.00000016 * T * T + 0.000000504 * T * T * T

        xequat = xeclip
        yequat = yeclip * Math.Cos(oblecl * (GradiRad)) + zeclip * Math.Sin(oblecl * (GradiRad))
        zequat = yeclip * Math.Sin(23.4406 * (GradiRad)) + zeclip * Math.Cos(oblecl * (GradiRad))

        r = Math.Sqrt(xequat ^ 2 + yequat ^ 2 + zequat ^ 2) - (h / 149598000)
        RA = Atan2(yequat, xequat) * (RadGradi)
        delta = Asin(zequat / r)
        delta *= RadGradi

        J2000 = d
        ts = Time_of_Effemeridi(timex, False, 3)
        UTH = ts.hr + ts.mi / 60 + ts.se / 3600

        GMST0 = Modulus(L + 180, 360) / 15
        SIDTIME = GMST0 + UTH + Lony / 15
        HA = (SIDTIME * 15 - RA)

        X = Math.Cos(HA * (GradiRad)) * Math.Cos(delta * (GradiRad))
        y = Math.Sin(HA * (GradiRad)) * Math.Cos(delta * (GradiRad))
        z = Math.Sin(delta * (GradiRad))

        xhor = X * Math.Cos((90 - Laty) * (GradiRad)) - z * Math.Sin((90 - Laty) * (GradiRad))
        yhor = y
        zhor = X * Math.Sin((90 - Laty) * (GradiRad)) + z * Math.Cos((90 - Laty) * (GradiRad))

        az = Atan2(yhor, xhor) * (RadGradi)
        el = Asin(zhor) * RadGradi
        pos.Valori(1) = az
        pos.Valori(2) = el
        Return True
    End Function

    Public Function WriteError(ByVal Messaggio As String, Routine As String) As Boolean
        Dim Buffer As String
        On Error Resume Next
        Buffer = CDate(DateAdd("h", -i_Off, Now)).ToString("dd/MM/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) & ", " & ", " & Routine
        Dim filePath As String = Application.StartupPath & "\" & f_Log4
        System.IO.File.AppendAllText(filePath, Buffer & Environment.NewLine)
        Return True
    End Function

    Private Function Rev(Ang As Double) As Double
        Rev = Ang - Int(Ang / 360) * 360
        If Rev < 0 Then Rev += 360
    End Function

    Private Function FNrange(III As Double) As Double
        FNrange = III - Int(III / twopi) * twopi
    End Function

    Private Sub EquatorialeToOrizzontale(AR As Double, Dec As Double, jd As Double, lat As Double, Lon As Double, ByRef az As Double, ByRef alt As Double)
        Dim LST As Double, HA As Double
        LST = (100.46 + 0.985647 * (jd - 2451545.0) + Lon) Mod 360
        If LST < 0 Then LST += 360
        HA = (LST - AR) * GradiRad
        lat *= GradiRad
        Dec *= GradiRad
        alt = Asin(Math.Sin(Dec) * Math.Sin(lat) + Math.Cos(Dec) * Math.Cos(lat) * Math.Cos(HA)) * RadGradi
        az = Atan2(-Math.Sin(HA), Math.Tan(Dec) * Math.Cos(lat) - Math.Sin(lat) * Math.Cos(HA)) * RadGradi
        If az < 0 Then az += 360
    End Sub

    Private Sub CorreggiParallasse(ByRef elev As Double, az As Double, DistKm As Double)
        Dim RT As Double, piRad As Double, Eg As Double
        RT = 6378
        piRad = Math.Atan(RT / DistKm)
        Eg = elev * GradiRad
        elev = Asin(Math.Sin(Eg) * Math.Cos(piRad) - Math.Cos(Eg) * Math.Sin(piRad) * Math.Cos(az * GradiRad)) * RadGradi
    End Sub

End Module
