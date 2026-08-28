Imports System.Numerics
Imports System.Math
Imports System.Drawing

Module NoradBase

    '
    ' cNoradBase.cpp
    '
    ' Historical Note
    ' The equations used here (and in derived classes) to determine satellite
    ' ECI coordinates/velocity come from the December, 1980 NORAD document
    ' "Space Track Report No. 3". The report details 6 orbital models and
    ' provides FORTRAN IV implementations of each. The classes here
    ' implement only two of the orbital models  SGP4 and SDP4. These two models,
    ' one for "near-earth" objects and one for "deep space" objects, are widely
    ' used in satellite tracking software and can produce very accurate results
    ' when used with current NORAD two-line element datum.
    '
    ' The NORAD FORTRAN IV SGP4/SDP4 implementations were converted to Pascal by
    ' Dr. TS Kelso in 1995. In 1996 these routines were ported in a straight-
    ' forward manner to C++ by Varol Okan. The SGP4/SDP4 classes here were
    ' written by Michael F. Henry in 2002-03 and are a modern C++ re-write of
    ' the work done by Okan. In addition to introducing an object-oriented
    ' architecture, the last residues of the original FORTRAN code (such as
    ' labels and gotos) were eradicated.
    '
    ' For excellent information on the underlying physics of orbits, visible
    ' satellite observations, current NORAD TLE data, and other related material,
    ' see http 'www.celestrak.com which is maintained by Dr. TS Kelso.
    '
    ' Copyright (c) 2003 Michael F. Henry
    '
    ' mfh 12/07/2003
    '
    ' cNoradBase.h
    '
    ' This class provides a base class for the NORAD SGP4/SDP4
    ' orbit models.
    '
    ' Copyright (c) 2003 Michael F. Henry
    '



    Public Function Define_Derived_Constants() As Boolean
        Define_Derived_Constants = True
        xke = Sqrt(3600 * ge / Cube(xkmper))   'Sqrt(ge) ER^3/min^2}
        qoms2t = (qo - s) ^ 4    '(qo-s)^4 ER^4}
    End Function  'Define_Derived_Constants}


    '''''''''''''''''''''''''''''''''''''''
    ' Initialize()
    ' Perform the initialization of member variables, specifically the variables
    ' used by derived-class objects to calculate ECI coordinates.
    Public Function InitSGP() As Boolean
        On Error GoTo div0
        InitSGP = True
        ' Initialize Object variables which are time-independent when
        ' calculating the ECI coordinates of the satellite.
        a1 = Power(xke / xno, tothrd)
        cosio = Cos(xincl)
        theta2 = cosio ^ 2
        x3thm1 = 3 * theta2 - 1
        eosq = eo ^ 2
        betao2 = 1 - eosq
        betao = Sqrt(betao2)
        del1 = 1.5 * ck2 * x3thm1 / (a1 ^ 2 * betao * betao2)
        ao = a1 * (1 - del1 * (0.5 * tothrd + del1 * (1 + 134 / 81 * del1)))
        delo = 1.5 * ck2 * x3thm1 / (ao ^ 2 * betao * betao2)
        xnodp = xno / (1 + delo)
        aodp = ao / (1 - delo)
        ' Initialization }
        ' For perigee less than 220 kilometers, the isimp flag is set and
        '  the equations are truncated to linear variation in Sqrtt a and
        '  quadratic variation in mean anomaly.  Also, the c3 term, the
        '  delta omega term, and the delta m term are dropped. }
        isimp = False
        If (aodp * (1 - eo) / ae) < (220 / xkmper + ae) Then isimp = True
        ' For perigee below 156 km, the values of s and qoms2t are altered. }
        s4 = s
        qoms24 = qoms2t
        perige = (aodp * (1 - eo) - ae) * xkmper
        If (perige < 156) Then
            s4 = perige - 78
            If (perige <= 98) Then
                s4 = 20
            End If
            qoms24 = Power((120 - s4) * ae / xkmper, 4)
            s4 = s4 / xkmper + ae
        End If
        pinvsq = 1 / (aodp ^ 2 * betao2 ^ 2)
        tsi = 1 / (aodp - s4)
        eta = aodp * eo * tsi
        etasq = eta ^ 2
        eeta = eo * eta
        psisq = Abs(1 - etasq)
        coef = qoms24 * tsi ^ 4
        coef1 = coef / psisq ^ 3.5
        c2 = coef1 * xnodp * (aodp * (1 + 1.5 * etasq + eeta * (4 + etasq)) _
        + 0.375 * ck2 * tsi / psisq * x3thm1 * (8 + 3 * etasq * (8 + etasq)))
        c1 = bstar * c2
        sinio = Sin(xincl)
        a3ovk2 = -xj3 / ck2 * Power(ae, 3)
        c3 = coef * tsi * a3ovk2 * xnodp * ae * sinio / eo
        x1mth2 = 1 - theta2
        xx1 = 2 + 0.5 * etasq
        xx2 = 0.5 + 2 * etasq
        xx3 = 1 - 2 * eeta + etasq * (1.5 - 0.5 * eeta)
        xx4 = 2 * etasq - eeta * (1 + etasq)
        c4 = 2 * xnodp * coef1 * aodp * betao2
        c4 *= (eta * (xx1) _
        + eo * (xx2) - 2 * ck2 * tsi / (aodp * psisq) _
        * (-3 * x3thm1 * (xx3) _
        + 0.75 * x1mth2 * (xx4) * Cos(2 * omegao)))
        c5 = 2 * coef1 * aodp * betao2 * (1 + 2.75 * (etasq + eeta) + eeta * etasq)
        theta4 = theta2 ^ 2
        temp1 = 3 * ck2 * pinvsq * xnodp
        temp2 = temp1 * ck2 * pinvsq
        temp3 = 1.25 * ck4 * pinvsq ^ 2 * xnodp
        xmdot = xnodp + 0.5 * temp1 * betao * x3thm1 _
           + 0.0625 * temp2 * betao * (13 - 78 * theta2 + 137 * theta4)
        x1m5th = 1 - 5 * theta2
        omgdot = -0.5 * temp1 * x1m5th + 0.0625 * temp2 * (7 - 114 * theta2 + 395 * theta4) _
            + temp3 * (3 - 36 * theta2 + 49 * theta4)
        xhdot1 = -temp1 * cosio
        xnodot = xhdot1 + (0.5 * temp2 * (4 - 19 * theta2) _
            + 2 * temp3 * (3 - 7 * theta2)) * cosio
        omgcof = bstar * c3 * Cos(omegao)
        xmcof = -tothrd * coef * bstar * ae / eeta
        xnodcf = 3.5 * betao2 * xhdot1 * c1
        t2cof = 1.5 * c1
        xlcof = 0.125 * a3ovk2 * sinio * (3 + 5 * cosio) / (1 + cosio)
        aycof = 0.25 * a3ovk2 * sinio
        delmo = Power(1 + eta * Cos(xmo), 3)
        sinmo = Sin(xmo)
        x7thm1 = 7 * theta2 - 1
        If (twopi / xnodp >= 225) Then
        End If
        Exit Function
div0:
        FrmMain.stpMessaggio.Text = "Not valid parameters"
        FrmMain.TmrMsg.Enabled = True
        frmRicezione.LblSatellite.BackColor = Color.Yellow
    End Function


    '''''''''''''''''''''''''''''''''''''''
    Public Function FinalPosition(omega As Double,
                               e As Double, a As Double,
                               xl As Double, xnode As Double,
                               xn As Double,
                               pos As Vector, vel As Vector) As Boolean

        Dim beta As Double, axn As Double, aynl As Double, xlt As Double
        Dim ayn As Double, capu As Double, sinepw As Double, cosepw As Double
        Dim temp4 As Double, temp5 As Double, temp6 As Double, epw As Double
        Dim ecose As Double, esine As Double, elsq As Double, pl As Double
        Dim r As Double, temp1 As Double, rdot As Double, rfdot As Double
        Dim betal As Double, cosu As Double, u As Double
        Dim sin2u As Double, cos2u As Double, rk As Double, cosuk As Double
        Dim sinik As Double, sinnok As Double, sinu As Double, cosnok As Double
        Dim sinuk As Double, rdotk As Double, rfdotk As Double
        Dim uk As Double, ux As Double, uy As Double, cosik As Double
        Dim uz As Double, vx As Double, vy As Double, vz As Double
        Dim xinck As Double, xll As Double
        Dim xmx As Double, xmy As Double, xnodek As Double
        Dim X As Double, y As Double, z As Double, xdot As Double, ydot As Double, zdot As Double
        Dim fDone As Boolean
        FinalPosition = True
        If ((e ^ 2) > 1) Then Exit Function
        beta = Sqrt(1 - e ^ 2)
        '  xn = xke / (a ^ 1.5)
        ' Long period periodics }
        axn = e * Cos(omega)
        Temp = 1 / (a * beta ^ 2)
        xll = Temp * xlcof * axn
        aynl = Temp * aycof
        xlt = xl + xll
        ayn = e * Sin(omega) + aynl
        ' Solve Kepler's Equation }
        capu = Fmod2p(xlt - xnode)
        temp2 = capu
        temp3 = 0
        temp4 = 0
        temp5 = 0
        temp6 = 0
        sinepw = 0
        cosepw = 0
        fDone = False
        For i = 1 To 10 And Not fDone

            sinepw = Sin(temp2)
            cosepw = Cos(temp2)
            temp3 = axn * sinepw
            temp4 = ayn * cosepw
            temp5 = axn * cosepw
            temp6 = ayn * sinepw
            epw = (capu - temp4 + temp3 - temp2) / (1 - temp5 - temp6) + temp2
            If (Abs(epw - temp2) <= e6a) Then Exit For
            temp2 = epw
            If Abs(epw - temp2) <= e6a Then
                fDone = True
            Else
                temp2 = epw
            End If
        Next  'for i}
        ' Short period preliminary quantities }

        ecose = temp5 + temp6
        esine = temp3 - temp4
        elsq = axn ^ 2 + ayn ^ 2
        Temp = 1 - elsq
        pl = a * Temp
        r = a * (1 - ecose)
        temp1 = 1 / r
        rdot = xke * Sqrt(a) * esine * temp1
        rfdot = xke * Sqrt(pl) * temp1
        temp2 = a * temp1
        betal = Sqrt(Temp)
        temp3 = 1 / (1 + betal)
        cosu = temp2 * (cosepw - axn + ayn * esine * temp3)
        sinu = temp2 * (sinepw - ayn - axn * esine * temp3)
        u = AcTan(sinu, cosu)
        sin2u = 2 * sinu * cosu
        cos2u = 2 * cosu ^ 2 - 1
        Temp = 1 / pl
        temp1 = ck2 * Temp
        temp2 = temp1 * Temp
        ' Update for short periodics }
        rk = r * (1 - 1.5 * temp2 * betal * x3thm1) + 0.5 * temp1 * x1mth2 * cos2u
        uk = u - 0.25 * temp2 * x7thm1 * sin2u
        xnodek = xnode + 1.5 * temp2 * cosio * sin2u
        xinck = xincl + 1.5 * temp2 * cosio * sinio * cos2u
        rdotk = rdot - xn * temp1 * x1mth2 * sin2u
        rfdotk = rfdot + xn * temp1 * (x1mth2 * cos2u + 1.5 * x3thm1)
        ' Orientation vectors }
        sinuk = Sin(uk)
        cosuk = Cos(uk)
        sinik = Sin(xinck)
        cosik = Cos(xinck)
        sinnok = Sin(xnodek)
        cosnok = Cos(xnodek)
        xmx = -sinnok * cosik
        xmy = cosnok * cosik
        ux = xmx * sinuk + cosnok * cosuk
        uy = xmy * sinuk + sinnok * cosuk
        uz = sinik * sinuk
        vx = xmx * cosuk - cosnok * sinuk
        vy = xmy * cosuk - sinnok * sinuk
        vz = sinik * cosuk
        ' Position and velocity }
        X = rk * ux
        pos.Valori(1) = X
        y = rk * uy
        pos.Valori(2) = y
        z = rk * uz
        pos.Valori(3) = z
        xdot = rdotk * ux + rfdotk * vx
        vel.Valori(1) = xdot
        ydot = rdotk * uy + rfdotk * vy
        vel.Valori(2) = ydot
        zdot = rdotk * uz + rfdotk * vz
        vel.Valori(3) = zdot

        '   cVector vecVel(xdot, ydot, zdot)

        '   time_t gmt = m_Orbit.EpochTime() + (time_t)(tsince * 60.0)

        '   eci = cEci(vecPos, vecVel, cJulian(gmt))

    End Function

    'Public Sub AggNorad()
    '   Dim hFile As Long, tmpSQL As String
    '   Dim Gr_file As Double
    '   Dim i_Scelta As Integer, b_Check As Boolean
    '   Dim sReadBuffer As String
    '   Dim abuf(2) As String, s_URL As String, s_TwoLine As String
    '   Dim s_Linea As String
    '   Dim s_Partenza As String, s_Salva As String, rs2 As DAO.Recordset
    '    On Error Resume Next
    '        tmpSQL = "SELECT * FROM [Satelliti] order by NomeSatellite"
    '        Set rs2 = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
    '        rs2.FindFirst "NomeSatellite ='" & Satellite & "'"
    '        s_URL = rs2!URL
    '        If IsNull(rs2!FileAgg) Or rs2!FileAgg = "" Then Exit Sub
    '        s_TwoLine = rs2!FileAgg
    '        i = InStr(1, s_TwoLine, ":", 1)
    '        If i = 0 Then s_TwoLine = App.Path & "\" & s_TwoLine
    '        rs2.Close
    ''        If s_URL <> "" Then CopyURLToFile s_URL, s_TwoLine   'Funziona
    '       hFile = FreeFile
    '       Open s_TwoLine For Binary Access Read As hFile
    '       Gr_file = LOF(1)
    '
    '        If Gr_file < LUNGFILE Then
    '            Close #hFile              'Stringa piu' corta - esci senza azzerare
    '            Exit Sub
    '        End If
    '        i_Scelta = 0
    '        Do While i_Scelta = 0
    '            Input #hFile, sReadBuffer
    '            If EOF(hFile) Then Exit Do
    '            i_Scelta = InStr(1, sReadBuffer, Satellite, 1)
    '            If i_Scelta <> 0 Then Exit Do
    '        Loop
    ' occorre prelevare il buffer two-Line   abuf = sat_data(arg)
    '* Decode Card 1 *}
    '    If i_Scelta = 0 Then
    '       Response = MsgBox("Satellite non trovato", vbOKOnly)
    '        frmMain.stbMain1.Panels("messaggio") = "Satellite not found"
    '        frmMain.tmrMsg.Enabled = True
    '        Close #hFile
    '       Exit Sub
    '    End If
    'carica i dati dei parametro
    '       Input #hFile, abuf(1)
    '       b_Check = IsValidLine(abuf(1), 1)
    '       If Not b_Check Then
    ''           Response = MsgBox("Parametri non validi", vbOKOnly)
    '            frmMain.stbMain1.Panels("messaggio") = "Not valid parameters"
    '            frmMain.tmrMsg.Enabled = True
    '           Close #hFile
    '          Exit Sub
    '      End If
    '      Input #hFile, abuf(2)
    '      b_Check = IsValidLine(abuf(2), 2)
    '      If Not b_Check Then
    ' '          Response = MsgBox("Parametri non validi", vbOKOnly)
    '            frmMain.stbMain1.Panels("messaggio") = "Not valid parameters"
    '            frmMain.tmrMsg.Enabled = True
    '          Close #hFile
    '         Exit Sub
    '     End If
    '          tmpSQL = "SELECT * FROM [Sat2line] order by Sat_Name"
    '          Set rs2 = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
    '          rs2.FindFirst "Sat_Name ='" & Satellite & "'"
    '          If Not rs2.NoMatch Then
    '              rs2.Edit
    '          Else
    '              rs2.AddNew
    '              rs2!Sat_Name = Satellite
    '          End If
    '          rs2!linea1 = abuf(1)
    '          rs2!linea2 = abuf(2)
    '          rs2!Sat_Number = Mid$(abuf(1), 3, 5)
    '          rs2!EpochTime = Val(Mid$(abuf(1), 19, 14))
    '          rs2!Ftdmm = Val(Mid$(abuf(1), 34, 10))
    '          iexp = Val(Mid$(abuf(1), 51, 2))  'esponenziale
    '          rs2!Stdmm = Val(Mid$(abuf(1), 45, 6)) * 0.00001
    '          rs2!Stdmm = rs2!Stdmm * (10 ^ iexp)
    '          ibexp = Val(Mid$(abuf(1), 60, 2))     'tipo effemeridi
    '          rs2!bstar = Val(Mid$(abuf(1), 54, 6)) * 0.00001
    '          rs2!bstar = rs2!bstar * (10 ^ ibexp)
    '          rs2!TEphe = Val(Mid$(abuf(1), 63, 1))
    '          rs2!Element = Val(Mid$(abuf(1), 66, 3))
    '          If Val(Mid$(abuf(2), 9, 8)) > 0 Then
    '              rs2!Inclination = Val(Mid$(abuf(2), 9, 8))
    '          Else
    '              rs2!Inclination = 0.0001
    '          End If
    '          rs2!Raan = Val(Mid$(abuf(2), 18, 8))
    '          rs2!Eccentricity = Val(Mid$(abuf(2), 27, 7)) * 0.0000001    'eccentricita
    '          rs2!ArgPer = Val(Mid$(abuf(2), 35, 8))
    '          rs2!Anomaly = Val(Mid$(abuf(2), 44, 8))
    '          rs2!MeanMo = Val(Mid$(abuf(2), 53, 11))
    '          rs2!RevEpoch = Val(Mid$(abuf(2), 64, 5))
    '          rs2.Update
    '          rs2.Close
    '          Close #hFile
    '  s_Partenza = rs2!Day + rs2!Mounth + Mid$(rs2!year, 3, 2)
    '  rs2.Close
    '  Close #hFile
    '  s_Salva = App.Path & "\TWOLine" & s_Partenza & ".Txt"
    '  If Dir(s_Salva) <> "" Then Kill s_Salva
    '  FileCopy s_Linea, s_Salva
    '  Kill s_Linea
    '  Inizializza
    '
    'End Sub



End Module
