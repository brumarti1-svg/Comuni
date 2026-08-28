Imports System.Math

Module ModLuna
    ' ============================================================
    '  Effemeridi Lunari  -  Modulo di Calcolo
    '  Algoritmo: Jean Meeus, "Astronomical Algorithms" (2a ed.)
    '  CORREZIONI APPLICATE:
    '  - segno lonsta11 nell'angolo orario (+ invece di -)
    '  - +180 nell'azimut
    '  - rifrazione atmosferica (Bennett)
    '  - RA*15 in LunaRiseTranSet
    '  - rimossa Norm360 da m0 in LunaRiseTranSet
    '  - conversione ore locali corretta (m*24 + utOffset)
    ' ============================================================

    Public Function CalcolaJD(anno As Integer, mese As Integer,
                           giorno As Integer, oreUT As Double) As Double
        Dim a As Long, b As Long
        Dim y As Integer, m As Integer
        y = anno : m = mese
        If m <= 2 Then y -= 1 : m += 12
        a = Int(y / 100)
        b = 2 - a + Int(a / 4)
        CalcolaJD = Int(365.25 * (y + 4716)) +
                Int(30.6001 * (m + 1)) +
                giorno + oreUT / 24.0# + b - 1524.5
    End Function

    Public Function Norm360(a As Double) As Double
        Norm360 = a - 360.0# * Int(a / 360.0#)
        If Norm360 < 0 Then Norm360 += 360.0#
    End Function

    Public Function Atan2(y As Double, X As Double) As Double
        Const PI As Double = 3.14159265358979
        If X > 0 Then
            Atan2 = System.Math.Atan(y / X)
        ElseIf X < 0 And y >= 0 Then
            Atan2 = System.Math.Atan(y / X) + PI
        ElseIf X < 0 And y < 0 Then
            Atan2 = System.Math.Atan(y / X) - PI
        ElseIf X = 0 And y > 0 Then
            Atan2 = PI / 2.0#
        ElseIf X = 0 And y < 0 Then
            Atan2 = -PI / 2.0#
        Else
            Atan2 = 0#
        End If
    End Function

    Public Function Acos(X As Double) As Double
        Const PI As Double = 3.14159265358979
        If X >= 1.0# Then Acos = 0# : Exit Function
        If X <= -1.0# Then Acos = PI : Exit Function
        Acos = PI / 2.0# - System.Math.Atan(X /System.Math.Sqrt(1.0# - X * X))
    End Function


    Public Function PosizioneSole(jd As Double) As Double
        Const DEG As Double = 3.14159265358979 / 180.0#
        Dim T As Double
        T = (jd - 2451545.0#) / 36525.0#
        Dim L0 As Double, m As Double, c As Double
        L0 = 280.46646 + 36000.76983 * T + 0.0003032 * T * T
        m = Norm360(357.52911 + 35999.05029 * T - 0.0001537 * T * T)
        c = (1.914602 - 0.004817 * T - 0.000014 * T * T) *System.Math.Sin(m * DEG) +
         (0.019993 - 0.000101 * T) *System.Math.Sin(2 * m * DEG) +
         0.000289 *System.Math.Sin(3 * m * DEG)
        PosizioneSole = Norm360(L0 + c)
    End Function

    Public Sub PosizioneLuna(jd As Double, pos As Vector)

        Const PI As Double = 3.14159265358979
        Const DEG As Double = PI / 180.0#
        Const RAD As Double = 180.0# / PI

        Dim T As Double
        T = (jd - 2451545.0#) / 36525.0#

        Dim Lp As Double, d As Double, m As Double, Mp As Double, f As Double
        Lp = Norm360(218.3164477 + 481267.88123421 * T - 0.0015786 * T * T + T * T * T / 538841.0# - T * T * T * T / 65194000.0#)
        d = Norm360(297.8501921 + 445267.1114034 * T - 0.0018819 * T * T + T * T * T / 545868.0# - T * T * T * T / 113065000.0#)
        m = Norm360(357.5291092 + 35999.0502909 * T - 0.0001536 * T * T + T * T * T / 24490000.0#)
        Mp = Norm360(134.9633964 + 477198.8675055 * T + 0.0087414 * T * T + T * T * T / 69699.0# - T * T * T * T / 14712000.0#)
        f = Norm360(93.272095 + 483202.0175233 * T - 0.0036539 * T * T - T * T * T / 3526000.0# + T * T * T * T / 863310000.0#)

        Dim e As Double
        e = 1.0# - 0.002516 * T - 0.0000074 * T * T

        Dim A1 As Double, A2 As Double, A3 As Double
        A1 = Norm360(119.75 + 131.849 * T)
        A2 = Norm360(53.09 + 479264.29 * T)
        A3 = Norm360(313.45 + 481266.484 * T)

        Dim SL As Double, SR As Double
        SL = 0# : SR = 0#

        Dim lD(59) As Integer, lM(59) As Integer, lMp(59) As Integer, lF(59) As Integer
        Dim CL(59) As Long, CR(59) As Long

        lD(0) = 0 : lM(0) = 0 : lMp(0) = 1 : lF(0) = 0 : CL(0) = 6288774 : CR(0) = -20905355
        lD(1) = 2 : lM(1) = 0 : lMp(1) = -1 : lF(1) = 0 : CL(1) = 1274027 : CR(1) = -3699111
        lD(2) = 2 : lM(2) = 0 : lMp(2) = 0 : lF(2) = 0 : CL(2) = 658314 : CR(2) = -2955968
        lD(3) = 0 : lM(3) = 0 : lMp(3) = 2 : lF(3) = 0 : CL(3) = 213618 : CR(3) = -569925
        lD(4) = 0 : lM(4) = 1 : lMp(4) = 0 : lF(4) = 0 : CL(4) = -185116 : CR(4) = 48888
        lD(5) = 0 : lM(5) = 0 : lMp(5) = 0 : lF(5) = 2 : CL(5) = -114332 : CR(5) = -3149
        lD(6) = 2 : lM(6) = 0 : lMp(6) = -2 : lF(6) = 0 : CL(6) = 58793 : CR(6) = 246158
        lD(7) = 2 : lM(7) = -1 : lMp(7) = -1 : lF(7) = 0 : CL(7) = 57066 : CR(7) = -152138
        lD(8) = 2 : lM(8) = 0 : lMp(8) = 1 : lF(8) = 0 : CL(8) = 53322 : CR(8) = -170733
        lD(9) = 2 : lM(9) = -1 : lMp(9) = 0 : lF(9) = 0 : CL(9) = 45758 : CR(9) = -204586
        lD(10) = 0 : lM(10) = 1 : lMp(10) = -1 : lF(10) = 0 : CL(10) = -40923 : CR(10) = -129620
        lD(11) = 1 : lM(11) = 0 : lMp(11) = 0 : lF(11) = 0 : CL(11) = -34720 : CR(11) = 108743
        lD(12) = 0 : lM(12) = 1 : lMp(12) = 1 : lF(12) = 0 : CL(12) = -30383 : CR(12) = 104755
        lD(13) = 2 : lM(13) = 0 : lMp(13) = 0 : lF(13) = -2 : CL(13) = 15327 : CR(13) = 10321
        lD(14) = 0 : lM(14) = 0 : lMp(14) = 1 : lF(14) = 2 : CL(14) = -12528 : CR(14) = 0
        lD(15) = 0 : lM(15) = 0 : lMp(15) = 1 : lF(15) = -2 : CL(15) = 10980 : CR(15) = 79661
        lD(16) = 4 : lM(16) = 0 : lMp(16) = -1 : lF(16) = 0 : CL(16) = 10675 : CR(16) = -34782
        lD(17) = 0 : lM(17) = 0 : lMp(17) = 3 : lF(17) = 0 : CL(17) = 10034 : CR(17) = -23210
        lD(18) = 4 : lM(18) = 0 : lMp(18) = -2 : lF(18) = 0 : CL(18) = 8548 : CR(18) = -21636
        lD(19) = 2 : lM(19) = 1 : lMp(19) = -1 : lF(19) = 0 : CL(19) = -7888 : CR(19) = 24208
        lD(20) = 2 : lM(20) = 1 : lMp(20) = 0 : lF(20) = 0 : CL(20) = -6766 : CR(20) = 30824
        lD(21) = 1 : lM(21) = 0 : lMp(21) = -1 : lF(21) = 0 : CL(21) = -5163 : CR(21) = -8379
        lD(22) = 1 : lM(22) = 1 : lMp(22) = 0 : lF(22) = 0 : CL(22) = 4987 : CR(22) = -16675
        lD(23) = 2 : lM(23) = -1 : lMp(23) = 1 : lF(23) = 0 : CL(23) = 4036 : CR(23) = -12831
        lD(24) = 2 : lM(24) = 0 : lMp(24) = 2 : lF(24) = 0 : CL(24) = 3994 : CR(24) = -10445
        lD(25) = 4 : lM(25) = 0 : lMp(25) = 0 : lF(25) = 0 : CL(25) = 3861 : CR(25) = -11650
        lD(26) = 2 : lM(26) = 0 : lMp(26) = -3 : lF(26) = 0 : CL(26) = 3665 : CR(26) = 14403
        lD(27) = 0 : lM(27) = 1 : lMp(27) = -2 : lF(27) = 0 : CL(27) = -2689 : CR(27) = -7003
        lD(28) = 2 : lM(28) = 0 : lMp(28) = -1 : lF(28) = 2 : CL(28) = -2602 : CR(28) = 0
        lD(29) = 2 : lM(29) = -1 : lMp(29) = -2 : lF(29) = 0 : CL(29) = 2390 : CR(29) = 10056
        lD(30) = 1 : lM(30) = 0 : lMp(30) = 1 : lF(30) = 0 : CL(30) = -2348 : CR(30) = 6322
        lD(31) = 2 : lM(31) = -2 : lMp(31) = 0 : lF(31) = 0 : CL(31) = 2236 : CR(31) = -9884
        lD(32) = 0 : lM(32) = 1 : lMp(32) = 2 : lF(32) = 0 : CL(32) = -2120 : CR(32) = 0
        lD(33) = 0 : lM(33) = 2 : lMp(33) = 0 : lF(33) = 0 : CL(33) = -2069 : CR(33) = 0
        lD(34) = 2 : lM(34) = -2 : lMp(34) = -1 : lF(34) = 0 : CL(34) = 2048 : CR(34) = -4950
        lD(35) = 2 : lM(35) = 0 : lMp(35) = 1 : lF(35) = -2 : CL(35) = -1773 : CR(35) = 4130
        lD(36) = 2 : lM(36) = 0 : lMp(36) = 0 : lF(36) = 2 : CL(36) = -1595 : CR(36) = 0
        lD(37) = 4 : lM(37) = -1 : lMp(37) = -1 : lF(37) = 0 : CL(37) = 1215 : CR(37) = -3958
        lD(38) = 0 : lM(38) = 0 : lMp(38) = 2 : lF(38) = 2 : CL(38) = -1110 : CR(38) = 0
        lD(39) = 3 : lM(39) = 0 : lMp(39) = -1 : lF(39) = 0 : CL(39) = -892 : CR(39) = 3258
        lD(40) = 2 : lM(40) = 1 : lMp(40) = 1 : lF(40) = 0 : CL(40) = -810 : CR(40) = 2616
        lD(41) = 4 : lM(41) = -1 : lMp(41) = -2 : lF(41) = 0 : CL(41) = 759 : CR(41) = -883
        lD(42) = 0 : lM(42) = 2 : lMp(42) = -1 : lF(42) = 0 : CL(42) = -713 : CR(42) = -1128
        lD(43) = 2 : lM(43) = 2 : lMp(43) = -1 : lF(43) = 0 : CL(43) = -700 : CR(43) = -1229
        lD(44) = 2 : lM(44) = 1 : lMp(44) = -2 : lF(44) = 0 : CL(44) = 691 : CR(44) = 0
        lD(45) = 2 : lM(45) = -1 : lMp(45) = 0 : lF(45) = -2 : CL(45) = 596 : CR(45) = 0
        lD(46) = 4 : lM(46) = 0 : lMp(46) = 1 : lF(46) = 0 : CL(46) = 549 : CR(46) = -500
        lD(47) = 0 : lM(47) = 0 : lMp(47) = 4 : lF(47) = 0 : CL(47) = 537 : CR(47) = -459
        lD(48) = 4 : lM(48) = -1 : lMp(48) = 0 : lF(48) = 0 : CL(48) = 520 : CR(48) = -381
        lD(49) = 1 : lM(49) = 0 : lMp(49) = -2 : lF(49) = 0 : CL(49) = -487 : CR(49) = -214
        lD(50) = 2 : lM(50) = 1 : lMp(50) = 0 : lF(50) = -2 : CL(50) = -399 : CR(50) = 0
        lD(51) = 0 : lM(51) = 0 : lMp(51) = 2 : lF(51) = -2 : CL(51) = -381 : CR(51) = 195
        lD(52) = 1 : lM(52) = 1 : lMp(52) = 1 : lF(52) = 0 : CL(52) = 351 : CR(52) = 0
        lD(53) = 3 : lM(53) = 0 : lMp(53) = -2 : lF(53) = 0 : CL(53) = -340 : CR(53) = 0
        lD(54) = 4 : lM(54) = 0 : lMp(54) = -3 : lF(54) = 0 : CL(54) = 330 : CR(54) = 0
        lD(55) = 2 : lM(55) = -1 : lMp(55) = 2 : lF(55) = 0 : CL(55) = 327 : CR(55) = 0
        lD(56) = 0 : lM(56) = 2 : lMp(56) = 1 : lF(56) = 0 : CL(56) = -323 : CR(56) = 0
        lD(57) = 1 : lM(57) = 1 : lMp(57) = -1 : lF(57) = 0 : CL(57) = 299 : CR(57) = 0
        lD(58) = 2 : lM(58) = 0 : lMp(58) = 3 : lF(58) = 0 : CL(58) = 294 : CR(58) = 0
        lD(59) = 2 : lM(59) = 0 : lMp(59) = -1 : lF(59) = 2 : CL(59) = 0 : CR(59) = 0

        Dim i As Integer, arg As Double, eCorr As Double
        For i = 0 To 59
            arg = lD(i) * d + lM(i) * m + lMp(i) * Mp + lF(i) * f
            eCorr = 1.0#
            If Math.Abs(lM(i)) = 1 Then eCorr = e
            If Math.Abs(lM(i)) = 2 Then eCorr = e * e
            SL += CL(i) * eCorr * System.Math.Sin(arg * DEG)
            SR += CR(i) * eCorr * System.Math.Cos(arg * DEG)
        Next i

        SL += 3958.0# * System.Math.Sin(A1 * DEG) + 1962.0# * System.Math.Sin((Lp - f) * DEG) + 318.0# * System.Math.Sin(A2 * DEG)

        Dim SB As Double : SB = 0#
        Dim bD(29) As Integer, bM(29) As Integer, bMp(29) As Integer, bF(29) As Integer
        Dim cb(29) As Long

        bD(0) = 0 : bM(0) = 0 : bMp(0) = 0 : bF(0) = 1 : cb(0) = 5128122
        bD(1) = 0 : bM(1) = 0 : bMp(1) = 1 : bF(1) = 1 : cb(1) = 280602
        bD(2) = 0 : bM(2) = 0 : bMp(2) = 1 : bF(2) = -1 : cb(2) = 277693
        bD(3) = 2 : bM(3) = 0 : bMp(3) = 0 : bF(3) = -1 : cb(3) = 173237
        bD(4) = 2 : bM(4) = 0 : bMp(4) = -1 : bF(4) = 1 : cb(4) = 55413
        bD(5) = 2 : bM(5) = 0 : bMp(5) = -1 : bF(5) = -1 : cb(5) = 46271
        bD(6) = 2 : bM(6) = 0 : bMp(6) = 0 : bF(6) = 1 : cb(6) = 32573
        bD(7) = 0 : bM(7) = 0 : bMp(7) = 2 : bF(7) = 1 : cb(7) = 17198
        bD(8) = 2 : bM(8) = 0 : bMp(8) = 1 : bF(8) = -1 : cb(8) = 9266
        bD(9) = 0 : bM(9) = 0 : bMp(9) = 2 : bF(9) = -1 : cb(9) = 8822
        bD(10) = 2 : bM(10) = -1 : bMp(10) = 0 : bF(10) = -1 : cb(10) = 8216
        bD(11) = 2 : bM(11) = 0 : bMp(11) = -2 : bF(11) = -1 : cb(11) = 4324
        bD(12) = 2 : bM(12) = 0 : bMp(12) = 1 : bF(12) = 1 : cb(12) = 4200
        bD(13) = 2 : bM(13) = 1 : bMp(13) = 0 : bF(13) = -1 : cb(13) = -3359
        bD(14) = 2 : bM(14) = -1 : bMp(14) = -1 : bF(14) = 1 : cb(14) = 2463
        bD(15) = 2 : bM(15) = -1 : bMp(15) = 0 : bF(15) = 1 : cb(15) = 2211
        bD(16) = 2 : bM(16) = -1 : bMp(16) = -1 : bF(16) = -1 : cb(16) = 2065
        bD(17) = 0 : bM(17) = 1 : bMp(17) = -1 : bF(17) = -1 : cb(17) = -1870
        bD(18) = 4 : bM(18) = 0 : bMp(18) = -1 : bF(18) = -1 : cb(18) = 1828
        bD(19) = 0 : bM(19) = 1 : bMp(19) = 0 : bF(19) = 1 : cb(19) = -1794
        bD(20) = 0 : bM(20) = 0 : bMp(20) = 0 : bF(20) = 3 : cb(20) = -1749
        bD(21) = 0 : bM(21) = 1 : bMp(21) = -1 : bF(21) = 1 : cb(21) = -1565
        bD(22) = 1 : bM(22) = 0 : bMp(22) = 0 : bF(22) = 1 : cb(22) = -1491
        bD(23) = 0 : bM(23) = 1 : bMp(23) = 1 : bF(23) = 1 : cb(23) = -1475
        bD(24) = 0 : bM(24) = 1 : bMp(24) = 1 : bF(24) = -1 : cb(24) = -1410
        bD(25) = 0 : bM(25) = 1 : bMp(25) = 0 : bF(25) = -1 : cb(25) = -1344
        bD(26) = 1 : bM(26) = 0 : bMp(26) = 0 : bF(26) = -1 : cb(26) = -1335
        bD(27) = 0 : bM(27) = 0 : bMp(27) = 3 : bF(27) = 1 : cb(27) = 1107
        bD(28) = 4 : bM(28) = 0 : bMp(28) = 0 : bF(28) = -1 : cb(28) = 1021
        bD(29) = 4 : bM(29) = 0 : bMp(29) = -1 : bF(29) = 1 : cb(29) = 833

        For i = 0 To 29
            arg = bD(i) * d + bM(i) * m + bMp(i) * Mp + bF(i) * f
            eCorr = 1.0#
            If Math.Abs(bM(i)) = 1 Then eCorr = e
            If Math.Abs(bM(i)) = 2 Then eCorr = e * e
            SB += cb(i) * eCorr * System.Math.Sin(arg * DEG)
        Next i

        SB -= 2235.0# * System.Math.Sin(Lp * DEG) + 382.0# * System.Math.Sin(A3 * DEG) +
             175.0# * System.Math.Sin((A1 - f) * DEG) + 175.0# * System.Math.Sin((A1 + f) * DEG) +
             127.0# * System.Math.Sin((Lp - Mp) * DEG) - 115.0# * System.Math.Sin((Lp + Mp) * DEG)

        Dim eps As Double, dec As Double, dist As Double
        eps = 23.4392911 - 0.013004167 * T - 0.0000001639 * T * T + 0.0000005036 * T * T * T

        pos.Valori(4) = Norm360(Lp + SL / 1000000.0#)
        pos.Valori(3) = SB / 1000000.0#
        dist = 385000.56 + SR / 1000.0#

        Dim lonR As Double, latR As Double, epsR As Double
        lonR = pos.Valori(4) * DEG : latR = pos.Valori(3) * DEG : epsR = eps * DEG

        ra = Norm360(Atan2(Sin(lonR) * System.Math.Cos(epsR) - Tan(latR) *System.Math.Sin(epsR), System.Math.Cos(lonR)) * RAD)
        dec = Asin(Sin(latR) * System.Math.Cos(epsR) + System.Math.Cos(latR) *System.Math.Sin(epsR) *System.Math.Sin(lonR)) * RAD

        Dim theta0 As Double
        theta0 = Norm360(280.46061837 + 360.98564736629 * (jd - 2451545.0#) +
             0.000387933 * T * T - T * T * T / 38710000.0#)

        ' Angolo orario: + lonsta11 per longitudine Est
        Dim angH As Double
        angH = Norm360(theta0 + lonsta11 - ra)

        Dim latsta11R As Double, DecR As Double, angHR As Double
        latsta11R = latsta11 * DEG : DecR = dec * DEG : angHR = angH * DEG

        pos.Valori(2) = Asin(Sin(latsta11R) *System.Math.Sin(DecR) + System.Math.Cos(latsta11R) * System.Math.Cos(DecR) * System.Math.Cos(angHR)) * RAD

        ' Rifrazione atmosferica (formula Bennett) - risultato in gradi
        If pos.Valori(2) > -0.5 Then
            Dim rif As Double
            rif = (1.02 / Tan((pos.Valori(2) + 10.3 / (pos.Valori(2) + 5.11)) * DEG)) / 60.0#
            pos.Valori(2) = pos.Valori(2) + rif
        End If

        pos.Valori(1) = Norm360(Atan2(Sin(angHR), System.Math.Cos(angHR) *System.Math.Sin(latsta11R) - Tan(DecR) * System.Math.Cos(latsta11R)) * RAD + 180.0#)

        ' Converti RA in ore per visualizzazione
        ra /= 15.0#

        Dim lonSole As Double, illuminazione As Double, etaLuna As Double
        lonSole = PosizioneSole(jd) * DEG
        Dim elongRad As Double
        elongRad = Acos(Sin(latR) *System.Math.Sin(0#) + System.Math.Cos(latR) * System.Math.Cos(0#) * System.Math.Cos(lonR - lonSole))
        illuminazione = (1.0# + System.Math.Cos(PI - elongRad)) / 2.0#

        etaLuna = (jd - 2451550.1) - 29.53058868 * Int((jd - 2451550.1) / 29.53058868)
        If etaLuna < 0 Then etaLuna += 29.53058868
    End Sub


    ' ============================================================
    '  Funzioni di formattazione
    ' ============================================================
    Public Function FormatRA(RAore As Double) As String
        Dim h As Integer, m As Integer, s As Double
        h = Int(RAore)
        m = Int((RAore - h) * 60.0#)
        s = ((RAore - h) * 60.0# - m) * 60.0#
        FormatRA = Format(h, "00") & "h " & Format(m, "00") & "m " & Format(s, "00.0") & "s"
    End Function

    Public Function FormatOra(ore As Double) As String
        If ore < 0 Then FormatOra = "N/D" : Exit Function
        Dim h As Integer, m As Integer
        h = Int(ore) Mod 24
        m = Int((ore - Int(ore)) * 60)
        FormatOra = Format(h, "00") & ":" & Format(m, "00")
    End Function

    Public Function FaseLunare(eta As Double) As String
        Select Case eta
            Case Is < 1.85 : FaseLunare = "Luna Nuova"
            Case Is < 7.38 : FaseLunare = "Crescente (I quarto)"
            Case Is < 9.22 : FaseLunare = "Primo Quarto"
            Case Is < 14.77 : FaseLunare = "Gibbosa crescente"
            Case Is < 16.61 : FaseLunare = "Luna Piena"
            Case Is < 22.15 : FaseLunare = "Gibbosa calante"
            Case Is < 24.0# : FaseLunare = "Ultimo Quarto"
            Case Else : FaseLunare = "Falce calante"
        End Select
    End Function

    Public Function DirezioneAzimut(az As Double) As String
        Dim nomi(15) As String
        nomi(0) = "N" : nomi(1) = "NNE" : nomi(2) = "NE" : nomi(3) = "ENE"
        nomi(4) = "E" : nomi(5) = "ESE" : nomi(6) = "SE" : nomi(7) = "SSE"
        nomi(8) = "S" : nomi(9) = "SSO" : nomi(10) = "SO" : nomi(11) = "OSO"
        nomi(12) = "O" : nomi(13) = "ONO" : nomi(14) = "NO" : nomi(15) = "NNO"
        DirezioneAzimut = nomi(Int((az + 11.25) / 22.5) Mod 16)
    End Function

    Public Function SegnoZodiacale(Lon As Double) As String
        Dim segni(11) As String
        segni(0) = "Ariete" : segni(1) = "Toro" : segni(2) = "Gemelli"
        segni(3) = "Cancro" : segni(4) = "Leone" : segni(5) = "Vergine"
        segni(6) = "Bilancia" : segni(7) = "Scorpione" : segni(8) = "Sagittario"
        segni(9) = "Capricorno" : segni(10) = "Acquario" : segni(11) = "Pesci"
        SegnoZodiacale = segni(Int(Lon / 30.0#) Mod 12)
    End Function

End Module
