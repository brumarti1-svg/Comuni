Imports System.Math

Module ModCassVenere

    ' ============================================================
    '  Modulo Cassiopeia + Venere
    '  CORREZIONI APPLICATE:
    '  - segno lonsta11 nell'angolo orario (+ invece di -)
    '  - +180 nell'azimut
    '  - rifrazione atmosferica
    '  - RA*15 in VenereRiseTran e CassiopeaRiseSet
    '  - rimossa Norm360v da m0
    '  - conversione ore locali corretta
    ' ============================================================

    Private Structure TStella
        Public Nome As String
        Public RAh As Double
        Public Decl As Double
        Public Mag As Double
        Public mRA As Double
        Public mDec As Double
    End Structure

Private Function GetStelleCas() As TStella
        Dim s As TStella
        s.Nome = "Alpha Cas (Schedar)" : s.RAh = 0.675122 : s.Decl = 56.537331 : s.Mag = 2.24 : s.mRA = 0.636 : s.mDec = -0.0114
        '    s(1).Nome = "Beta Cas  (Caph)": s(1).RAh = 0.152968: s(1).Decl = 59.150307: s(1).Mag = 2.27: s(1).mRA = 5.234: s(1).mDec = -0.0497
        '    s(2).Nome = "Gamma Cas":        s(2).RAh = 0.945148: s(2).Decl = 60.716682: s(2).Mag = 2.47: s(2).mRA = 0.264: s(2).mDec = -0.0103
        '    s(3).Nome = "Delta Cas (Ruchbah)": s(3).RAh = 1.430264: s(3).Decl = 60.235317: s(3).Mag = 2.68: s(3).mRA = 1.095: s(3).mDec = -0.0142
        '    s(4).Nome = "Eps Cas   (Segin)": s(4).RAh = 1.90659: s(4).Decl = 63.670102: s(4).Mag = 3.38: s(4).mRA = 0.378: s(4).mDec = -0.0056
        GetStelleCas = s
    End Function

    ' ============================================================
    '  Precessione J2000 -> epoca JD  (Meeus cap.21)
    ' ============================================================
    Private Sub PrecessaStella(RAj2000 As Double, Decj2000 As Double,
                            mRA As Double, mDec As Double,
                            jd As Double, RAout As Double)
        Const DEG As Double = 3.14159265358979 / 180.0#
        Const RAD As Double = 180.0# / 3.14159265358979

        Dim T As Double
        T = (jd - 2451545.0#) / 36525.0#
        Dim anni As Double
        anni = T * 100.0#

        Dim RA0 As Double, Dec0 As Double
        RA0 = RAj2000 + (mRA / 3600.0#) * anni / 15.0#
        Dec0 = Decj2000 + (mDec / 3600.0#) * anni

        Dim zeta As Double, z As Double, theta As Double
        zeta = ((2306.2181 + 1.39656 * T) * T + (0.30188 - 0.000344 * T) * T * T + 0.017998 * T * T * T) / 3600.0#
        z = ((2306.2181 + 1.39656 * T) * T + (1.09468 + 0.000066 * T) * T * T + 0.018203 * T * T * T) / 3600.0#
        theta = ((2004.3109 - 0.8533 * T) * T - (0.42665 + 0.000217 * T) * T * T - 0.041775 * T * T * T) / 3600.0#

        Dim RA_r As Double, Dec_r As Double
        RA_r = RA0 * 15.0# * DEG
        Dec_r = Dec0 * DEG

        Dim a As Double, b As Double, c As Double
        a = System.Math.Cos(Dec_r) * System.Math.Sin(RA_r + zeta * DEG)
        b = System.Math.Cos(theta * DEG) * System.Math.Cos(Dec_r) * System.Math.Cos(RA_r + zeta * DEG) - System.Math.Sin(theta * DEG) * System.Math.Sin(Dec_r)
        c = System.Math.Sin(theta * DEG) * System.Math.Cos(Dec_r) * System.Math.Cos(RA_r + zeta * DEG) + System.Math.Cos(theta * DEG) * System.Math.Sin(Dec_r)

        RAout = Norm360v(Atan2v(a, b) * RAD + z) / 15.0#
        '   Decout = Asin(c) * RAD
    End Sub

    ' ============================================================
    '  Calcolo Az/El topocentrico per una stella
    ' ============================================================
    Public Sub PosizioneStellaOrizzontale(RAore As Double, Decgr As Double,
                                       jd As Double, latsta11 As Double, lonsta11 As Double, azimut As Double, elevazione As Double)
        Const DEG As Double = 3.14159265358979 / 180.0#
        Const RAD As Double = 180.0# / 3.14159265358979

        Dim T As Double
        T = (jd - 2451545.0#) / 36525.0#
        Dim theta0 As Double
        theta0 = Norm360v(280.46061837 + 360.98564736629 * (jd - 2451545.0#) + 0.000387933 * T * T - T * T * T / 38710000.0#)

        ' Angolo orario: + lonsta11 per longitudine Est
        Dim angH As Double
        angH = Norm360v(theta0 + lonsta11 - RAore * 15.0#)

        Dim latR As Double, DecR As Double, angHR As Double
        latR = latsta11 * DEG
        DecR = Decgr * DEG
        angHR = angH * DEG

        elevazione = Asin(Sin(latR) * System.Math.Sin(DecR) + System.Math.Cos(latR) * System.Math.Cos(DecR) * System.Math.Cos(angHR)) * RAD

        ' Rifrazione atmosferica
        If elevazione > -0.5 Then
            Dim rif As Double
            rif = (1.02 / Tan((elevazione + 10.3 / (elevazione + 5.11)) * DEG)) / 60.0#
            elevazione += rif
        End If

        azimut = Norm360v(Atan2v(Sin(angHR), System.Math.Cos(angHR) * System.Math.Sin(latR) - Tan(DecR) * System.Math.Cos(latR)) * RAD + 180.0#)
    End Sub

    ' ============================================================
    '  CASSIOPEIA - posizione delle 5 stelle principali
    ' ============================================================
    Public Sub CalcolaCassiopeia(jd As Double, pos As Vector)
        Dim stelle As TStella
        stelle = GetStelleCas()

        '    sempCircumpolare = (latsta11 > (90# - stelle.Decl))

        Dim RAprec As Double, Decprec As Double
        Dim az As Double, el As Double

        '    For i = 0 To 4
        Call PrecessaStella(stelle.RAh, stelle.Decl,
                             stelle.mRA, stelle.mDec, jd, RAprec)
        Call PosizioneStellaOrizzontale(RAprec, Decprec, jd, LATSTA11, LONSTA11, az, el)

        pos.Valori(1) = az
        pos.Valori(2) = el

        '        risultati = stelle.Nome & _
        '                       "  Mag=" & Format(stelle.Mag, "0.00") & _
        '                       "  Az=" & Format(az, "0.000") & Chr(176) & _
        '                       "  El=" & Format(el, "0.000") & Chr(176) & _
        '                       "  " & visStr
        '    Next i
    End Sub

    ' ============================================================
    '  Sorgere/Transito/Tramonto Cassiopeia (Alpha Cas)
    ' ============================================================
    Public Sub CassiopeaRiseSet(anno As Integer, mese As Integer, giorno As Integer,
                             latsta11 As Double, lonsta11 As Double, utOff As Double,
                             oreSorgere As Double, oreTransito As Double, oreTramonto As Double)

        Const DEG As Double = 3.14159265358979 / 180.0#
        Const RAD As Double = 180.0# / 3.14159265358979
        Const hOrizzStella As Double = -0.5667

        Dim stelle As TStella
        stelle = GetStelleCas()

        Dim JD0 As Double
        JD0 = CalcolaJD(anno, mese, giorno, 0#)

        Dim RAp As Double, Decp As Double
        Call PrecessaStella(stelle.RAh, stelle.Decl,
                         stelle.mRA, stelle.mDec, JD0, RAp)

        ' Convertire RA da ore a gradi
        RAp *= 15.0#

        Dim T As Double
        T = (JD0 - 2451545.0#) / 36525.0#
        Dim theta0 As Double
        theta0 = Norm360v(280.46061837 + 360.98564736629 * (JD0 - 2451545.0#) + 0.000387933 * T * T)

        Dim cosH0 As Double
        cosH0 = (Sin(hOrizzStella * DEG) -System.Math.Sin(latsta11 * DEG) *System.Math.Sin(Decp * DEG)) /
               (Cos(latsta11 * DEG) * System.Math.Cos(Decp * DEG))

        If cosH0 < -1.0# Then
            oreSorgere = -2 : oreTransito = -2 : oreTramonto = -2
            Exit Sub
        End If
        If cosH0 > 1.0# Then
            oreSorgere = -3 : oreTransito = -3 : oreTramonto = -3
            Exit Sub
        End If

        Dim angH0 As Double
        angH0 = AcosV(cosH0) * RAD

        Dim m0 As Double, m1 As Double, m2 As Double

        ' Senza Norm360v per permettere valori negativi
        m0 = (RAp + lonsta11 - theta0) / 360.0#
        m0 -= Int(m0) : If m0 < 0 Then m0 += 1

        m1 = m0 - angH0 / 360.0#
        m2 = m0 + angH0 / 360.0#

        m1 -= Int(m1) : If m1 < 0 Then m1 += 1
        m2 -= Int(m2) : If m2 < 0 Then m2 += 1

        ' Conversione in ore locali
        oreSorgere = m1 * 24.0# + utOff
        oreTransito = m0 * 24.0# + utOff
        oreTramonto = m2 * 24.0# + utOff

        oreSorgere -= 24.0# * Int(oreSorgere / 24.0#) : If oreSorgere < 0 Then oreSorgere += 24.0#
        oreTransito -= 24.0# * Int(oreTransito / 24.0#) : If oreTransito < 0 Then oreTransito += 24.0#
        oreTramonto -= 24.0# * Int(oreTramonto / 24.0#) : If oreTramonto < 0 Then oreTramonto += 24.0#
    End Sub

    ' ============================================================
    '  VENERE - Posizione VSOP87 (termini principali)
    ' ============================================================
    Public Sub PosizioneVenere(jd As Double, pos As Vector)

        Const PI As Double = 3.14159265358979
        Const DEG As Double = PI / 180.0#
        Const RAD As Double = 180.0# / PI

        Dim T As Double
        T = (jd - 2451545.0#) / 36525.0#
        Dim tau As Double
        tau = T / 10.0#

        Dim L0 As Double
        L0 = 317614667.0# + 1353968.0# * System.Math.Cos(5.5931332 + 10213.2855462 * tau) +
         89892.0# * System.Math.Cos(5.3065 + 20426.5710904 * tau) + 5477.0# * System.Math.Cos(4.4163 + 7860.4193924 * tau) +
         3456.0# * System.Math.Cos(2.6996 + 11790.6290887 * tau) + 2372.0# * System.Math.Cos(2.9938 + 3930.2096962 * tau) +
         1664.0# * System.Math.Cos(4.2502 + 1577.3435424 * tau) + 1438.0# * System.Math.Cos(4.1575 + 9683.5945798 * tau) +
         1317.0# * System.Math.Cos(5.1867 + 26.2983197 * tau) + 1201.0# * System.Math.Cos(6.1536 + 30213.8857478 * tau) +
         769.0# * System.Math.Cos(0.8161 + 9437.7629717 * tau) + 761.0# * System.Math.Cos(1.9502 + 529.6909651 * tau) +
         708.0# * System.Math.Cos(1.0547 + 775.522504 * tau) + 585.0# * System.Math.Cos(3.9989 + 191.4482661 * tau) +
         500.0# * System.Math.Cos(4.1228 + 15720.8387848 * tau) + 429.0# * System.Math.Cos(3.5865 + 19367.1891784 * tau)

        Dim L1 As Double
        L1 = 1021352943053.0# + 95708.0# * System.Math.Cos(2.46424 + 10213.2855462 * tau) +
         14445.0# * System.Math.Cos(0.51625 + 20426.5710924 * tau) + 213.0# * System.Math.Cos(1.795 + 30639.856584 * tau) +
         174.0# * System.Math.Cos(2.655 + 26.298 * tau) + 152.0# * System.Math.Cos(6.106 + 1577.344 * tau) +
         82.0# * System.Math.Cos(5.7 + 191.448 * tau) + 70.0# * System.Math.Cos(2.681 + 9437.763 * tau) +
         52.0# * System.Math.Cos(3.6 + 775.523 * tau) + 38.0# * System.Math.Cos(1.033 + 529.691 * tau)

        Dim L2 As Double
        L2 = 54127.0# + 3891.0# * System.Math.Cos(0.3451 + 10213.2855 * tau) + 1338.0# * System.Math.Cos(2.0201 + 20426.571 * tau) +
         24.0# * System.Math.Cos(2.05 + 26.298 * tau) + 19.0# * System.Math.Cos(3.54 + 30639.857 * tau) +
         10.0# * System.Math.Cos(3.97 + 775.523 * tau) + 7.0# * System.Math.Cos(1.52 + 1577.344 * tau)

        Dim L3 As Double
        L3 = 136.0# + 78.0# * System.Math.Cos(4.835 + 10213.286 * tau) + 26.0# * System.Math.Cos(PI)

        Dim L_rad As Double
        L_rad = (L0 + L1 * tau + L2 * tau * tau + L3 * tau * tau * tau) / 100000000.0#
        L_rad -= Int(L_rad / (2.0# * PI)) * (2.0# * PI)
        If L_rad < 0 Then L_rad += 2.0# * PI

        Dim B0 As Double
        B0 = 5923638.0# * System.Math.Cos(0.26702 + 10213.28555 * tau) + 40108.0# * System.Math.Cos(1.14737 + 20426.57109 * tau) +
         32815.0# * System.Math.Cos(3.14737) + 1011.0# * System.Math.Cos(1.08946 + 30639.85664 * tau) +
         149.0# * System.Math.Cos(6.2539 + 26.29832 * tau) + 138.0# * System.Math.Cos(0.8602 + 1577.34354 * tau) +
         130.0# * System.Math.Cos(3.67203 + 5507.55323 * tau) + 120.0# * System.Math.Cos(3.70506 + 10404.73381 * tau) +
         108.0# * System.Math.Cos(4.53992 + 2352.866 * tau)

        Dim B1 As Double
        B1 = 513348.0# * System.Math.Cos(1.803643 + 10213.285546 * tau) + 4380.0# * System.Math.Cos(3.38623 + 20426.57109 * tau) +
         199.0# * System.Math.Cos(0#) + 197.0# * System.Math.Cos(2.53012 + 30639.85664 * tau)

        Dim B2 As Double
        B2 = 22378.0# * System.Math.Cos(3.38509 + 10213.28555 * tau) + 282.0# * System.Math.Cos(0#) +
         173.0# * System.Math.Cos(5.2568 + 20426.57109 * tau) + 27.0# * System.Math.Cos(3.87067 + 30639.85664 * tau)

        Dim B3 As Double
        B3 = 647.0# * System.Math.Cos(4.992 + 10213.286 * tau) + 20.0# * System.Math.Cos(PI)

        Dim B_rad As Double
        B_rad = (B0 + B1 * tau + B2 * tau * tau + B3 * tau * tau * tau) / 100000000.0#

        Dim R0 As Double
        R0 = 72334821.0# + 489824.0# * System.Math.Cos(4.021518 + 10213.285546 * tau) +
         1658.0# * System.Math.Cos(4.9021 + 20426.5711 * tau) + 1632.0# * System.Math.Cos(2.8455 + 7860.4194 * tau) +
         1378.0# * System.Math.Cos(1.1285 + 11790.6291 * tau) + 498.0# * System.Math.Cos(2.587 + 9683.595 * tau) +
         374.0# * System.Math.Cos(1.423 + 3930.21 * tau) + 264.0# * System.Math.Cos(5.529 + 9437.763 * tau) +
         237.0# * System.Math.Cos(2.551 + 15720.839 * tau) + 222.0# * System.Math.Cos(2.013 + 19367.189 * tau) +
         126.0# * System.Math.Cos(2.728 + 1577.344 * tau) + 119.0# * System.Math.Cos(3.02 + 10404.734 * tau)

        Dim R1 As Double
        R1 = 34551.0# * System.Math.Cos(0.89199 + 10213.28555 * tau) + 234.0# * System.Math.Cos(1.772 + 20426.571 * tau) + 234.0#

        Dim R2 As Double
        R2 = 1407.0# * System.Math.Cos(5.0637 + 10213.2855 * tau) + 16.0# * System.Math.Cos(5.47 + 20426.57 * tau) + 13.0#

        Dim Rv As Double
        Rv = (R0 + R1 * tau + R2 * tau * tau) / 100000000.0#

        Dim lonSoleGeo As Double
        lonSoleGeo = PosizioneSoleFull(jd)

        Dim M_sole As Double
        M_sole = Norm360v(357.52911 + 35999.05029 * T - 0.0001537 * T * T)
        Dim Rsole As Double
        Rsole = 1.000001018 * (1.0# - 0.01671123 * System.Math.Cos(M_sole * DEG) - 0.00013999 * System.Math.Cos(2.0# * M_sole * DEG))

        Dim L_terra As Double
        L_terra = (lonSoleGeo + 180.0#) * DEG

        Dim X As Double, y As Double, z As Double
        X = Rv * System.Math.Cos(B_rad) * System.Math.Cos(L_rad) - Rsole * System.Math.Cos(L_terra)
        y = Rv * System.Math.Cos(B_rad) *System.Math.Sin(L_rad) - Rsole *System.Math.Sin(L_terra)
        z = Rv *System.Math.Sin(B_rad)

        Dim delta As Double
        delta =System.Math.Sqrt(X * X + y * y + z * z)

        Dim lonV As Double, latV As Double
        lonV = Norm360v(Atan2v(y, X) * RAD)
        latV = System.Math.Atan(z /System.Math.Sqrt(X * X + y * y)) * RAD

        lonV -= 0.005693 / delta * System.Math.Cos((lonSoleGeo - lonV) * DEG)
        latV -= 0.005693 / delta * System.Math.Sin(latV * DEG)
        Dim dist As Double, dec As Double
        pos.Valori(4) = lonV
        pos.Valori(3) = latV
        dist = delta

        Dim eps As Double
        eps = 23.4392911 - 0.013004167 * T - 0.0000001639 * T * T + 0.0000005036 * T * T * T

        Dim lonR As Double, latR As Double, epsR As Double, azimut As Double, elev As Double, elongaz As Double
        lonR = lonV * DEG : latR = latV * DEG : epsR = eps * DEG

        ra = Norm360v(Atan2v(Sin(lonR) * System.Math.Cos(epsR) - Tan(latR) *System.Math.Sin(epsR), System.Math.Cos(lonR)) * RAD)
        dec = Asin(Sin(latR) * System.Math.Cos(epsR) + System.Math.Cos(latR) *System.Math.Sin(epsR) *System.Math.Sin(lonR)) * RAD

        Call PosizioneStellaOrizzontale(ra / 15.0#, dec, jd, LATSTA11, LONSTA11, azimut, elev)

        elongaz = Norm360v(lonV - lonSoleGeo)
        If elongaz > 180.0# Then elongaz -= 360.0#

        Dim cosI As Double, fase As Double, illum As Double
        cosI = (Rsole * Rsole + delta * delta - Rv * Rv) / (2.0# * Rsole * delta)
        If cosI > 1.0# Then cosI = 1.0#
        If cosI < -1.0# Then cosI = -1.0#
        fase = AcosV(cosI) * RAD
        illum = (1.0# + System.Math.Cos(fase * DEG)) / 2.0#
        Dim magn As Double
        magn = -4.4 + 5.0# * Log(Rv * delta) / Log(10.0#) +
           0.0009 * fase + 0.000239 * fase * fase - 0.00000065 * fase * fase * fase
        pos.Valori(1) = azimut
        pos.Valori(2) = elev
    End Sub


    ' ============================================================
    '  Longitudine eclittica geocentrica del Sole
    ' ============================================================
    Public Function PosizioneSoleFull(jd As Double) As Double
        Const DEG As Double = 3.14159265358979 / 180.0#
        Dim T As Double
        T = (jd - 2451545.0#) / 36525.0#
        Dim L0 As Double, m As Double, c As Double, omega As Double
        L0 = Norm360v(280.46646 + 36000.76983 * T + 0.0003032 * T * T)
        m = Norm360v(357.52911 + 35999.05029 * T - 0.0001537 * T * T)
        c = (1.914602 - 0.004817 * T - 0.000014 * T * T) *System.Math.Sin(m * DEG) +
            (0.019993 - 0.000101 * T) *System.Math.Sin(2.0# * m * DEG) + 0.000289 *System.Math.Sin(3.0# * m * DEG)
        omega = Norm360v(125.04 - 1934.136 * T)
        PosizioneSoleFull = Norm360v(L0 + c - 0.00569 - 0.00478 *System.Math.Sin(omega * DEG))
    End Function

    ' ============================================================
    '  Descrizione visibilita Venere
    ' ============================================================
    Public Function VisibilitaVenere(elongaz As Double, elev As Double) As String
        If elev <= 0 Then
            If Math.Abs(elongaz) < 5 Then
                VisibilitaVenere = "Congiunzione (non visibile)"
            ElseIf elongaz > 0 Then
                VisibilitaVenere = "Sotto orizzonte (stella serale)"
            Else
                VisibilitaVenere = "Sotto orizzonte (stella mattutina)"
            End If
        ElseIf elongaz > 0 Then
            VisibilitaVenere = "Stella SERALE  (visibile a ovest dopo tramonto)"
        Else
            VisibilitaVenere = "Stella MATTUTINA  (visibile a est prima dell'alba)"
        End If
    End Function

    ' ============================================================
    '  Funzioni matematiche locali
    ' ============================================================
    Public Function Norm360v(a As Double) As Double
        Norm360v = a - 360.0# * Int(a / 360.0#)
        If Norm360v < 0 Then Norm360v += 360.0#
    End Function

    Public Function Atan2v(y As Double, X As Double) As Double
        Const PI As Double = 3.14159265358979
        If X > 0 Then
            Atan2v = System.Math.Atan(y / X)
        ElseIf X < 0 And y >= 0 Then
            Atan2v = System.Math.Atan(y / X) + PI
        ElseIf X < 0 And y < 0 Then
            Atan2v = System.Math.Atan(y / X) - PI
        ElseIf X = 0 And y > 0 Then
            Atan2v = PI / 2.0#
        ElseIf X = 0 And y < 0 Then
            Atan2v = -PI / 2.0#
        Else
            Atan2v = 0#
        End If
    End Function

    Public Function AcosV(X As Double) As Double
        Const PI As Double = 3.14159265358979
        If X >= 1.0# Then AcosV = 0# : Exit Function
        If X <= -1.0# Then AcosV = PI : Exit Function
        AcosV = PI / 2.0# - System.Math.Atan(X /System.Math.Sqrt(1.0# - X * X))
    End Function



End Module
