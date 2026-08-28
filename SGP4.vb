Module SGP4
    '
    ' cNoradSGP4.cpp
    '
    ' NORAD SGP4 implementation. See historical note in cNoradBase.cpp
    ' Copyright (c) 2003 Michael F. Henry
    '
    ' mfh 12/07/2003
    '
    '
    ' cNoradSGP4.h
    '
    ' This class implements the NORAD Simple General Perturbation 4 orbit
    ' model. This model provides the ECI coordiantes/velocity of satellites
    ' with orbit periods less than 225 minutes.
    '
    ' Copyright (c) 2003 Michael F. Henry
    '
    Public Function InitSGP4() As Boolean
        InitSGP4 = True
        InitSGP()
        c5 = 2 * coef1 * aodp * betao2 * (1 + 2.75 * (etasq + eeta) + eeta * etasq)
        omgcof = bstar * c3 * System.Math.Cos(omegao)
        xmcof = -tothrd * coef * bstar * ae / eeta
        delmo = System.Math.Pow(1 + eta * System.Math.Cos(xmo), 3)
        sinmo = System.Math.Sin(xmo)
    End Function


    '''''''''''''''''''''''''''''''''''''''

    '''''''''''''''''''''''''''''''''''''''
    ' getPosition()
    ' This procedure returns the ECI position and velocity for the satellite
    ' in the orbit at the given number of minutesSystem.Math.Since the TLE epoch time
    ' using the NORAD Simplified General Perturbation 4, near earth orbit
    ' model.
    '
    ' tsince - Time in minutesSystem.Math.Since the TLE epoch (GMT).
    ' eci    - ECI object to hold position information.
    '           To convert the returned ECI position vector to km,
    '           multiply each component by
    '              (XKMMPER / AE).
    '           To convert the returned ECI velocity vector to km/sec,
    '           multiply each component by
    '              (XKMPER / AE) * (MIN_PER_DAY / 86400).

    '''''''''''''''''''''''''''''''''''''''

    '''''''''''''''''''''''''''''''''''''''
    ' getPosition()
    ' This procedure returns the ECI position and velocity for the satellite
    ' in the orbit at the given number of minutesSystem.Math.Since the TLE epoch time
    ' using the NORAD Simplified General Perturbation 4, near earth orbit
    ' model.
    '
    ' tsince - Time in minutesSystem.Math.Since the TLE epoch (GMT).
    ' eci    - ECI object to hold position information.
    '           To convert the returned ECI position vector to km,
    '           multiply each component by
    '              (XKMMPER / AE).
    '           To convert the returned ECI velocity vector to km/sec,
    '           multiply each component by
    '              (XKMPER / AE) * (MIN_PER_DAY / 86400).

    Public Sub PosSGP4(tsince As Double, pos As Vector, vel As Vector)

        Dim xl As Double, e As Double, a As Double, tfour As Double
        Dim tcube As Double, delm As Double, delomg As Double, templ As Double
        Dim tempe As Double, tempa As Double, xnode As Double, tsq As Double, xmp As Double
        Dim xnoddf As Double, omgadf As Double, xmdf As Double
        Dim d2 As Double
        Dim d3 As Double
        Dim d4 As Double
        Dim t3cof As Double
        Dim t4cof As Double
        Dim t5cof As Double
        Dim c1sq As Double
        'On Error Resume Next 'TODO: aggiungere Try/Catch se necessario
        ' For m_perigee less than 220 kilometers, the isimp flag is  and
        ' the equations are truncated to linear variation inSystem.Math.Sqrtt a and
        ' quadratic variation in mean anomaly.  Also, the m_c3 term, the
        ' delta omega term, and the delta m term are dropped.
        InitSGP4()
        isimp = False
        If ((aodp * (1 - eo) / ae) < (220.0# / xkmper + ae)) Then isimp = True
        d2 = 0
        d3 = 0
        d4 = 0
        t3cof = 0
        t4cof = 0
        t5cof = 0

        If Not isimp Then
            c1sq = c1 ^ 2
            d2 = 4 * aodp * tsi * c1sq
            Temp = d2 * tsi * c1 / 3
            d3 = (17 * aodp + s4) * Temp
            d4 = 0.5 * Temp * aodp * tsi * (221 * aodp + 31 * s4) * c1
            t3cof = d2 + 2 * c1sq
            t4cof = 0.25 * (3 * d3 + c1 * (12 * d2 + 10 * c1sq))
            t5cof = 0.2 * (3 * d4 + 12 * c1 * d3 + 6 * d2 ^ 2 + 15 * c1sq * (2 * d2 + c1sq))
        End If
        ' Update for secular gravity and atmospheric drag. }
        xmdf = xmo + xmdot * tsince
        omgadf = omegao + omgdot * tsince
        xnoddf = xnodeo + xnodot * tsince
        '  omega = omgadf
        xmp = xmdf
        tsq = tsince ^ 2
        xnode = xnoddf + xnodcf * tsq
        tempa = 1 - c1 * tsince
        tempe = bstar * c4 * tsince
        templ = t2cof * tsq
        If Not isimp Then
            delomg = omgcof * tsince
            delm = xmcof * ((1 + eta * System.Math.Cos(xmdf)) ^ 3) - delmo
            Temp = delomg + delm
            xmp = xmdf + Temp
            '   omega = omgadf - Temp
            omgadf -= Temp
            tcube = tsq * tsince
            tfour = tsince * tcube
            tempa -= d2 * tsq - d3 * tcube - d4 * tfour
            tempe += bstar * c5 * (Math.Sin(xmp) - sinmo)
            templ += t3cof * tcube + tfour * (t4cof + tsince * t5cof)
        End If
        a = aodp * Math.Sqrt(tempa)
        e = eo - tempe
        xl = xmp + omgadf + xnode + xnodp * templ
        xn = xke / a ^ 1.5
        FinalPosition(omgadf, e, a, xl, xnode, xn, pos, vel)

    End Sub




End Module
