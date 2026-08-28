Module SDP4

    Const zns = 0.0000119459
    Const c1ss = 0.0000029864797
    Const zes = 0.01675
    Const znl = 0.00015835218
    Const c1l = 0.00000047968065
    Const zel = 0.0549
    Const zcosis = 0.91744867
    Const zsinis = 0.39785416
    Const zsings = -0.98088458
    Const zcosgs = 0.1945905
    Const zsinhs = 1
    Const zcoshs = 1
    Const q22 = 0.0000017891679
    Const q31 = 0.0000021460748
    Const q33 = 0.00000022123015
    Const g22 = 5.7686396
    Const g32 = 0.95240898
    Const g44 = 1.8014998
    Const g52 = 1.050833
    Const g54 = 4.4108898
    Const root22 = 0.0000017891679
    Const root32 = 0.00000037393792
    Const root44 = 0.0000000073636953
    Const root52 = 0.00000011428639
    Const root54 = 0.0000000021765803
    Const thdt = 0.0043752691



    Dim s_messaggio As String

    '
    ' cNoradSDP4.h
    '
    ' This class implements the NORAD Simple Deep Perturbation 4 orbit
    ' model. This model provides the ECI coordinates/velocity of satellites
    ' with periods >= 225 minutes.
    '
    ' Copyright (c) 2003 Michael F. Henry
    '

    'include "cNoradBase.h"





    'dpinit}
    ' Deep Initialization
    ' Deep Secular, Periodic
    ' Variables shared by "Deep" routines
    Dim dp_e3 As Double, dp_ee2 As Double, dp_savtsn As Double, dp_se2 As Double
    Dim dp_se3 As Double, dp_sgh2 As Double, dp_sgh3 As Double, dp_sgh4 As Double
    Dim dp_sghs As Double, dp_sh2 As Double, dp_sh3 As Double, dp_si2 As Double
    Dim dp_si3 As Double, dp_sl2 As Double, dp_sl3 As Double, dp_sl4 As Double
    Dim dp_xgh2 As Double, dp_xgh3 As Double, dp_xgh4 As Double, dp_xh2 As Double
    Dim dp_xh3 As Double, dp_xi2 As Double, dp_xi3 As Double, dp_xl2 As Double
    Dim dp_xl3 As Double, dp_xl4 As Double, dp_xqncl As Double, dp_zmol As Double
    Dim dp_zmos As Double

    Dim dp_atime As Double, dp_d2201 As Double, dp_d2211 As Double, dp_d3210 As Double
    Dim dp_d3222 As Double, dp_d4410 As Double, dp_d4422 As Double, dp_d5220 As Double
    Dim dp_d5232 As Double, dp_d5421 As Double, dp_d5433 As Double, dp_del1 As Double
    Dim dp_del2 As Double, dp_del3 As Double, dp_fasx2 As Double, dp_fasx4 As Double
    Dim dp_fasx6 As Double, dp_omegaq As Double, dp_sse As Double, dp_ssg As Double
    Dim dp_ssh As Double, dp_ssi As Double, dp_ssl As Double, dp_step2 As Double
    Dim dp_stepn As Double, dp_stepp As Double, dp_thgr As Double, dp_xfact As Double
    Dim dp_xlamo As Double, dp_xli As Double, dp_xni As Double

    Dim dp_iresfl As Boolean
    Dim dp_isynfl As Boolean

    ' DeepInit vars that change with epoch
    Dim dpi_c As Double, dpi_ctem As Double, dpi_day As Double, dpi_gam As Double
    Dim dpi_stem As Double, dpi_xnodce As Double, dpi_zcosgl As Double, dpi_zcoshl As Double
    Dim dpi_zcosil As Double, dpi_zsingl As Double, dpi_zsinhl As Double, dpi_zsinil As Double
    Dim dpi_zx As Double, dpi_zy As Double

    ' NORAD SDP4 implementation. See historical note in cNoradBase.cpp

    Public Sub InitSDP4()
        InitSGP()

        sing = System.Math.Sin(omegao)
        g = System.Math.Cos(omegao)


    End Sub



    '''''''''''''''''''''''''''''''''''''''
    Public Function DeepInit(eqsq As Double, Sinio As Double, cosio As Double,
                          betao As Double, aodp As Double, theta2 As Double,
                         Sing As Double, cosg As Double, betao2 As Double,
                          xmdot As Double, omgdot As Double, xnodott As Double, xnodp As Double)


        Dim pass As Integer, bInitOnExit As Boolean
        'On Error GoTo Errore 'TODO: convertire in Try/Catch
        eqsq = eqsq
        siniq = Sinio
        cosiq = cosio
        rteqsq = betao
        ao = aodp
        q2 = theta2
        sinomo = Sing
        omo = g
        bsq = betao2
        xlldot = xmdot
        omgdt = omgdot
        xnodot = xnodott

        ' Deep space initialization
        '   cJulian jd = Orbit.epoch()

        dp_thgr = ThetaG(julian_epoch)
        eq = eo
        xnq = xnodp
        aqnv = 1 / ao
        dp_xqncl = xincl
        xmao = xmo
        xpidot = omgdt + xnodot
        sinq = System.Math.Sin(xnodeo)
        cosq = System.Math.Cos(xnodeo)
        dp_omegaq = omegao

        ' Initialize lunar solar terms
        '   giorno = 18261.5
        giorno = ds50 + 18261.5    'DaysSystem.Math.Since 1900 Jan 0.5}
        If (giorno <> dpi_day) Then

            dpi_day = giorno
            dpi_xnodce = (4.523602 - 0.00092422029 * giorno)
            dpi_stem = System.Math.Sin(dpi_xnodce)
            dpi_ctem = System.Math.Cos(dpi_xnodce)
            dpi_zcosil = 0.91375164 - 0.03568096 * dpi_ctem
            dpi_zsinil = Math.Sqrt(1.0# - dpi_zcosil * dpi_zcosil)
            dpi_zsinhl = 0.089683511 * dpi_stem / dpi_zsinil
            dpi_zcoshl = Math.Sqrt(1.0# - dpi_zsinhl * dpi_zsinhl)
            dpi_c = 4.7199672 + 0.2299715 * giorno
            dpi_gam = 5.8351514 + 0.001944368 * giorno
            dp_zmol = Fmod2p(dpi_c - dpi_gam)
            dpi_zx = 0.39785416 * dpi_stem / dpi_zsinil
            dpi_zy = dpi_zcoshl * dpi_ctem + 0.91744867 * dpi_zsinhl * dpi_stem
            dpi_zx = AcTan(dpi_zx, dpi_zy) + dpi_gam - dpi_xnodce
            dpi_zcosgl = System.Math.Cos(dpi_zx)
            dpi_zsingl = System.Math.Sin(dpi_zx)
            dp_zmos = 6.2565837 + 0.017201977 * giorno
            dp_zmos = Fmod2p(dp_zmos)
        End If

        dp_savtsn = 1.0E+20

        zcosg = zcosgs
        zsing = zsings
        zcosi = zcosis
        zsini = zsinis
        zcosh = cosq
        zsinh = sinq
        cc = c1ss
        zn = zns
        ze = zes
        zmo = dp_zmos
        xnoi = 1 / xnodp

        se = 0
        si = 0
        sl = 0
        sgh = 0
        sh = 0
        ' Apply the solar and lunar terms on the first pass, then re-apply the
        ' solar terms again on the second pass.

        For pass = 1 To 2

            ' Do solar terms
            a1 = zcosg * zcosh + zsing * zcosi * zsinh
            a3 = -zsing * zcosh + zcosg * zcosi * zsinh
            a7 = -zcosg * zsinh + zsing * zcosi * zcosh
            a8 = zsing * zsini
            a9 = zsing * zsinh + zcosg * zcosi * zcosh
            a10 = zcosg * zsini
            a2 = cosiq * a7 + siniq * a8
            a4 = cosiq * a9 + siniq * a10
            a5 = -siniq * a7 + iq * a8
            a6 = -siniq * a9 + iq * a10
            X1 = a1 * omo + a2 * sinomo
            X2 = a3 * omo + a4 * sinomo
            x3 = -a1 * sinomo + a2 * omo
            x4 = -a3 * sinomo + a4 * omo
            x5 = a5 * sinomo
            x6 = a6 * sinomo
            x7 = a5 * omo
            x8 = a6 * omo
            z31 = 12.0# * X1 * X1 - 3.0# * x3 * x3
            z32 = 24.0# * X1 * X2 - 6.0# * x3 * x4
            z33 = 12.0# * X2 * X2 - 3.0# * x4 * x4
            z1 = 3.0# * (a1 * a1 + a2 * a2) + z31 * eqsq
            z2 = 6.0# * (a1 * a3 + a2 * a4) + z32 * eqsq
            z3 = 3.0# * (a3 * a3 + a4 * a4) + z33 * eqsq
            z11 = -6.0# * a1 * a5 + eqsq * (-24.0# * X1 * x7 - 6.0# * x3 * x5)
            z12 = -6.0# * (a1 * a6 + a3 * a5) +
            eqsq * (-24.0# * (X2 * x7 + X1 * x8) - 6.0# * (x3 * x6 + x4 * x5))
            z13 = -6.0# * a3 * a6 + eqsq * (-24.0# * X2 * x8 - 6.0# * x4 * x6)
            z21 = 6.0# * a2 * a5 + eqsq * (24.0# * X1 * x5 - 6.0# * x3 * x7)
            z22 = 6.0# * (a4 * a5 + a2 * a6) +
            eqsq * (24.0# * (X2 * x5 + X1 * x6) - 6.0# * (x4 * x7 + x3 * x8))
            z23 = 6.0# * a4 * a6 + eqsq * (24.0# * X2 * x6 - 6.0# * x4 * x8)
            z1 = z1 + z1 + bsq * z31
            z2 = z2 + z2 + bsq * z32
            z3 = z3 + z3 + bsq * z33
            s3 = cc * xnoi
            s2 = -0.5 * s3 / rteqsq
            s4 = s3 * rteqsq
            s1 = -15.0# * eq * s4
            s5 = X1 * x3 + X2 * x4
            s6 = X2 * x3 + X1 * x4
            s7 = X2 * x4 - X1 * x3
            se = s1 * zn * s5
            si = s2 * zn * (z11 + z13)
            sl = -zn * s3 * (z1 + z3 - 14.0# - 6.0# * eqsq)
            sgh = s4 * zn * (z31 + z33 - 6.0#)
            sh = -zn * s2 * (z21 + z23)

            If (dp_xqncl < 0.052359877) Then sh = 0#

            dp_ee2 = 2.0# * s1 * s6
            dp_e3 = 2.0# * s1 * s7
            dp_xi2 = 2.0# * s2 * z12
            dp_xi3 = 2.0# * s2 * (z13 - z11)
            dp_xl2 = -2.0# * s3 * z2
            dp_xl3 = -2.0# * s3 * (z3 - z1)
            dp_xl4 = -2.0# * s3 * (-21.0# - 9.0# * eqsq) * ze
            dp_xgh2 = 2.0# * s4 * z32
            dp_xgh3 = 2.0# * s4 * (z33 - z31)
            dp_xgh4 = -18.0# * s4 * ze
            dp_xh2 = -2.0# * s2 * z22
            dp_xh3 = -2.0# * s2 * (z23 - z21)

            If (pass = 1) Then

                ' Do lunar terms
                dp_sse = se
                dp_ssi = si
                dp_ssl = sl
                dp_ssh = sh / siniq
                dp_ssg = sgh - iq * dp_ssh
                dp_se2 = dp_ee2
                dp_si2 = dp_xi2
                dp_sl2 = dp_xl2
                dp_sgh2 = dp_xgh2
                dp_sh2 = dp_xh2
                dp_se3 = dp_e3
                dp_si3 = dp_xi3
                dp_sl3 = dp_xl3
                dp_sgh3 = dp_xgh3
                dp_sh3 = dp_xh3
                dp_sl4 = dp_xl4
                dp_sgh4 = dp_xgh4
                zcosg = dpi_zcosgl
                zsing = dpi_zsingl
                zcosi = dpi_zcosil
                zsini = dpi_zsinil
                zcosh = dpi_zcoshl * q + dpi_zsinhl * sinq
                zsinh = sinq * dpi_zcoshl - q * dpi_zsinhl
                zn = znl
                cc = c1l
                ze = zel
                zmo = dp_zmol
            End If
        Next

        dp_sse += se
        dp_ssi += si
        dp_ssl += sl
        dp_ssg += sgh - cosiq / siniq * sh
        dp_ssh += sh / siniq

        ' Geopotential resonance initialization for 12 hour orbits
        dp_iresfl = False
        dp_isynfl = False

        bInitOnExit = True

        ' Determine if orbit is 12- or 24-hour resonant.
        ' Mean motion is given in radians per minute.
        If ((xnodp > 0.0034906585) And (xnodp < 0.0052359877)) Then
            ' Orbit is within the Clarke Belt (period is 24-hour resonant).
            ' Synchronous resonance terms initialization
            dp_iresfl = True
            dp_isynfl = True

            'region 24-hour resonant

            g200 = 1.0# + eqsq * (-2.5 + 0.8125 * eqsq)

            g310 = 1.0# + 2.0# * eqsq

            g300 = 1.0# + eqsq * (-6.0# + 6.60937 * eqsq)

            f220 = 0.75 * (1.0# + cosiq) * (1.0# + iq)

            f311 = 0.9375 * siniq * siniq * (1.0# + 3 * cosiq) - 0.75 * (1.0# + cosiq)
            f330 = 1.0# + iq

            f330 = 1.875 * f330 * f330 * f330


            dp_del1 = 3.0# * xnodp * xnodp * aqnv * aqnv
            dp_del2 = 2.0# * dp_del1 * f220 * g200 * q22
            dp_del3 = 3.0# * dp_del1 * f330 * g300 * q33 * aqnv

            dp_del1 = dp_del1 * f311 * g310 * q31 * aqnv
            dp_xlamo = xmao + xnodeo + omegao - dp_thgr
            bfact = xlldot + xpidot - thdt
            bfact = bfact + dp_ssl + dp_ssg + dp_ssh

        ElseIf (((xnodp >= 0.00826) And (xnodp <= 0.00924)) And (eq >= 0.5)) Then
            ' Period is 12-hour resonant
            dp_iresfl = True

            'region 12-hour resonant

            eoc = eq * eqsq
            g201 = -0.306 - (eq - 0.64) * 0.44


            If (eq <= 0.65) Then
                g211 = 3.616 - 13.247 * eq + 16.29 * eqsq
                g310 = -19.302 + 117.39 * eq - 228.419 * eqsq + 156.591 * eoc
                g322 = -18.9068 + 109.7927 * eq - 214.6334 * eqsq + 146.5816 * eoc
                g410 = -41.122 + 242.694 * eq - 471.094 * eqsq + 313.953 * eoc
                g422 = -146.407 + 841.88 * eq - 1629.014 * eqsq + 1083.435 * eoc
                g520 = -532.114 + 3017.977 * eq - 5740.0# * eqsq + 3708.276 * eoc
            Else
                g211 = -72.099 + 331.819 * eq - 508.738 * eqsq + 266.724 * eoc
                g310 = -346.844 + 1582.851 * eq - 2415.925 * eqsq + 1246.113 * eoc
                g322 = -342.585 + 1554.908 * eq - 2366.899 * eqsq + 1215.972 * eoc
                g410 = -1052.797 + 4758.686 * eq - 7193.992 * eqsq + 3651.957 * eoc
                g422 = -3581.69 + 16178.11 * eq - 24462.77 * eqsq + 12422.52 * eoc

                If (eq <= 0.715) Then
                    g520 = 1464.74 - 4664.75 * eq + 3763.64 * eqsq
                Else
                    g520 = -5149.66 + 29936.92 * eq - 54087.36 * eqsq + 31324.56 * eoc
                End If
            End If
            If (eq < 0.7) Then
                g533 = -919.2277 + 4988.61 * eq - 9064.77 * eqsq + 5542.21 * eoc
                g521 = -822.71072 + 4568.6173 * eq - 8491.4146 * eqsq + 5337.524 * eoc
                g532 = -853.666 + 4690.25 * eq - 8624.77 * eqsq + 5341.4 * eoc
            Else
                g533 = -37995.78 + 161616.52 * eq - 229838.2 * eqsq + 109377.94 * eoc
                g521 = -51752.104 + 218913.95 * eq - 309468.16 * eqsq + 146349.42 * eoc
                g532 = -40023.88 + 170470.89 * eq - 242699.48 * eqsq + 115605.82 * eoc
            End If

            sini2 = siniq * siniq
            cosi2 = cosiq * cosiq

            f220 = 0.75 * (1.0# + 2.0# * iq + i2)

            f221 = 1.5 * sini2
            f321 = 1.875 * siniq * (1.0# - 2.0# * cosiq - 3.0# * cosi2)
            f322 = -1.875 * siniq * (1.0# + 2.0# * cosiq - 3.0# * cosi2)
            f441 = 35.0# * sini2 * f220
            f442 = 39.375 * sini2 * sini2
            f522 = 9.84375 * siniq * (sini2 * (1.0# - 2.0# * cosiq - 5.0# * i2) +
                     0.33333333 * (-2.0# + 4.0# * cosiq + 6.0# * i2))
            f523 = siniq * (4.92187512 * sini2 * (-2.0# - 4.0# * cosiq + 10.0# * i2) +
                     6.56250012 * (1.0# + 2.0# * cosiq - 3.0# * i2))
            f542 = 29.53125 * siniq * (2.0# - 8.0# * cosiq + i2 * (-12.0# + 8.0# * iq + 10.0# * i2))
            f543 = 29.53125 * siniq * (-2.0# - 8.0# * cosiq + i2 * (12.0# + 8.0# * iq - 10.0# * i2))
            xno2 = xnodp * xnodp
            ainv2 = aqnv * aqnv
            temp1 = 3.0# * xno2 * ainv2


            Temp = temp1 * root22

            dp_d2201 = Temp * f220 * g201
            dp_d2211 = Temp * f221 * g211
            temp1 *= aqnv
            Temp *= root32
            dp_d3210 = Temp * f321 * g310
            dp_d3222 = Temp * f322 * g322
            temp1 *= aqnv
            Temp = 2.0# * temp1 * root44
            dp_d4410 = Temp * f441 * g410
            dp_d4422 = Temp * f442 * g422
            temp1 *= aqnv
            Temp = temp1 * root52
            dp_d5220 = Temp * f522 * g520
            dp_d5232 = Temp * f523 * g532
            Temp = 2.0# * temp1 * root54
            dp_d5421 = Temp * f542 * g521
            dp_d5433 = Temp * f543 * g533
            dp_xlamo = xmao + xnodeo + xnodeo - dp_thgr - dp_thgr
            bfact = xlldot + xnodot + xnodot - thdt - thdt
            bfact = bfact + dp_ssl + dp_ssh + dp_ssh

        End If


        If (dp_iresfl Or dp_isynfl) Then
            dp_xfact = bfact - xnodp

            ' Initialize integrator
            dp_xli = dp_xlamo
            dp_xni = xnodp
            If g_Verso Then dp_atime = 0#  'performed by runtime
            dp_stepp = 720.0#
            dp_stepn = -720.0#
            dp_step2 = 259200.0#
        Else
            dp_xfact = 0#
            dp_xli = 0#
            dp_xni = 0#
            dp_atime = 0#
            dp_stepp = 0#
            dp_stepn = 0#
            dp_step2 = 0#

        End If





        DeepInit = True
Errore:

    End Function


    '''''''''''''''''''''''''''''''''''''''
    Private Function DeepCalcDotTerms(pxndot As Double, pxnddt As Double, pxldot As Double) As Boolean

        DeepCalcDotTerms = True

        Const dp_fasx2 As Double = 0.13130908
        Const dp_fasx4 As Double = 2.8843198
        Const dp_fasx6 As Double = 0.37448087

        ' Dot terms calculated
        If (dp_isynfl) Then

            pxndot = dp_del1 *System.Math.Sin(dp_xli - dp_fasx2) +
                dp_del2 *System.Math.Sin(2 * (dp_xli - dp_fasx4)) +
                dp_del3 *System.Math.Sin(3 * (dp_xli - dp_fasx6))
            pxnddt = dp_del1 * System.Math.Cos(dp_xli - dp_fasx2) +
                2 * dp_del2 * System.Math.Cos(2 * (dp_xli - dp_fasx4)) +
                3 * dp_del3 * System.Math.Cos(3 * (dp_xli - dp_fasx6))

        Else

            xomi = dp_omegaq + omgdt * dp_atime
            x2omi = xomi + xomi
            x2li = dp_xli + dp_xli

            pxndot = dp_d2201 *System.Math.Sin(x2omi + dp_xli - g22) +
                dp_d2211 *System.Math.Sin(dp_xli - g22) +
                dp_d3210 *System.Math.Sin(xomi + dp_xli - g32) +
                dp_d3222 *System.Math.Sin(-xomi + dp_xli - g32) +
                dp_d4410 *System.Math.Sin(x2omi + x2li - g44) +
                dp_d4422 *System.Math.Sin(x2li - g44) +
                dp_d5220 *System.Math.Sin(xomi + dp_xli - g52) +
                dp_d5232 *System.Math.Sin(-xomi + dp_xli - g52) +
                dp_d5421 *System.Math.Sin(xomi + x2li - g54) +
                dp_d5433 *System.Math.Sin(-xomi + x2li - g54)

            pxnddt = dp_d2201 * System.Math.Cos(x2omi + dp_xli - g22) +
                dp_d2211 * System.Math.Cos(dp_xli - g22) +
                dp_d3210 * System.Math.Cos(xomi + dp_xli - g32) +
                dp_d3222 * System.Math.Cos(-xomi + dp_xli - g32) +
                dp_d5220 * System.Math.Cos(xomi + dp_xli - g52) +
                dp_d5232 * System.Math.Cos(-xomi + dp_xli - g52) +
                2 * (dp_d4410 * System.Math.Cos(x2omi + x2li - g44) +
                dp_d4422 * System.Math.Cos(x2li - g44) +
                dp_d5421 * System.Math.Cos(xomi + x2li - g54) +
                dp_d5433 * System.Math.Cos(-xomi + x2li - g54))

        End If
        pxldot = dp_xni + dp_xfact
        pxnddt = (pxnddt) * (pxldot)

        DeepCalcDotTerms = True
    End Function

    '''''''''''''''''''''''''''''''''''''''
    Private Function DeepCalcIntegrator(pxndot As Double, pxnddt As Double,
                                    pxldot As Double, delt As Double) As Boolean

        DeepCalcIntegrator = True
        DeepCalcDotTerms(pxndot, pxnddt, pxldot)

   dp_xli = dp_xli + (pxldot) * delt + (pxndot) * dp_step2
        dp_xni = dp_xni + (pxndot) * delt + (pxnddt) * dp_step2
        dp_atime += delt
    End Function


    Private Function DeepSecular(xmdf As Double, omgadf As Double, xnode As Double,
                             emm As Double, xincc As Double, xnn As Double,
                             tsince As Double) As Boolean

        Dim fDone As Boolean
        DeepSecular = True

        ' Deep space secular effects
        xmdf += dp_ssl * tsince
        omgadf += dp_ssg * tsince
        xnode += dp_ssh * tsince
        emm = eo + dp_sse * tsince
        xincc = xincl + dp_ssi * tsince

        If (xincc < 0) Then

            xincc = -xincc
            xnode += PIGREC
            omgadf -= PIGREC
        End If

        xnddt = 0
        xndot = 0
        xldot = 0
        ft = 0
        delt = 0

        fDone = False

        If (dp_iresfl) Then

            Do While Not fDone

                If ((dp_atime = 0) Or
            ((tsince >= 0) And (dp_atime < 0)) Or
            ((tsince < 0) And (dp_atime >= 0))) Then

                    If (tsince < 0) Then
                        delt = dp_stepn
                    Else
                        delt = dp_stepp
                    End If
                    ' Epoch restart
                    dp_atime = 0
                    dp_xni = xnodp
                    dp_xli = dp_xlamo

                    fDone = True

                Else

                    If (Math.Abs(tsince) <Math.Abs(dp_atime)) Then

                        delt = dp_stepp

                        If (tsince >= 0) Then delt = dp_stepn

                        DeepCalcIntegrator(xndot, xnddt, xldot, delt)

            Else

                        delt = dp_stepn

                        If (tsince > 0) Then delt = dp_stepp

                        fDone = True
                    End If
                End If

            Loop

            Do While (Math.Abs(tsince - dp_atime) >= dp_stepp)

                DeepCalcIntegrator(xndot, xnddt, xldot, delt)

      Loop
            ft = tsince - dp_atime

            DeepCalcDotTerms(xndot, xnddt, xldot)

      xnn = dp_xni + xndot * ft + xnddt * ft * ft * 0.5

            xl = dp_xli + xldot * ft + xndot * ft * ft * 0.5
            Temp = -xnode + dp_thgr + tsince * thdt

            xmdf = xl - omgadf + Temp

            If Not dp_isynfl Then xmdf = xl + Temp + Temp
        End If


    End Function


    Public Function DeepPeriodics(e As Double, xincc As Double,
                               omgadf As Double, xnode As Double,
                               xmam As Double, tsince As Double) As Boolean

        ' Lunar-solar periodics

        DeepPeriodics = False
        sinis = System.Math.Sin(xincc)
        cosinc = System.Math.Cos(xincc)

        sghs = 0#
        shs = 0#
        sh1 = 0#
        pe = 0#
        pinc = 0#
        pl = 0#
        sghl = 0#

        zm = dp_zmos + zns * tsince
        zf = zm + 2.0# * zes *System.Math.Sin(zm)
       Sinzf =System.Math.Sin(zf)
        f2 = 0.5 *Sinzf *Sinzf - 0.25
        f3 = -0.5 *Sinzf *System.Math.Cos(zf)
        ses = dp_se2 * f2 + dp_se3 * f3
        sis = dp_si2 * f2 + dp_si3 * f3
        sls = dp_sl2 * f2 + dp_sl3 * f3 + dp_sl4 *Sinzf

        sghs = dp_sgh2 * f2 + dp_sgh3 * f3 + dp_sgh4 *Sinzf
        shs = dp_sh2 * f2 + dp_sh3 * f3
        zm = dp_zmol + znl * tsince
        zf = zm + 2.0# * zel *System.Math.Sin(zm)
       Sinzf =System.Math.Sin(zf)
        f2 = 0.5 *Sinzf *Sinzf - 0.25
        f3 = -0.5 *Sinzf *System.Math.Cos(zf)

        sel = dp_ee2 * f2 + dp_e3 * f3
        sil = dp_xi2 * f2 + dp_xi3 * f3
        sll = dp_xl2 * f2 + dp_xl3 * f3 + dp_xl4 *Sinzf

        sghl = dp_xgh2 * f2 + dp_xgh3 * f3 + dp_xgh4 *Sinzf
        sh1 = dp_xh2 * f2 + dp_xh3 * f3
        pe = ses + sel
        pinc = sis + sil
        pl = sls + sll

        pgh = sghs + sghl
        ph = shs + sh1

        xincc += pinc
        e += pe

        If (dp_xqncl >= 0.2) Then

            ' Apply periodics directly
            ph /= sinio
            pgh -= io * ph
            omgadf += pgh
            xnode += ph
            xmam += pl
        Else
            ' Apply periodics with Lyddane modification
           Sinok =System.Math.Sin(xnode)
            ok =System.Math.Cos(xnode)
            alfdp =Sinis *Sinok
            betdp =Sinis * ok
            dalf = ph * ok + pinc * cosinc *Sinok
            dbet = -ph *Sinok + pinc * cosinc * ok

            alfdp += dalf
            betdp += dbet

            xls = xmam + omgadf + cosinc * xnode
            dls = pl + pgh - pinc * xnode *sinis

            xls += dls
            xnode = AcTan(alfdp, betdp)
            xmam += pl
            omgadf = xls - xmam -System.Math.Cos(xincc) * xnode
        End If

        DeepPeriodics = True
    End Function



    '''''''''''''''''''''''''''''''''''''''
    ' getPosition()
    ' This procedure returns the ECI position and velocity for the satellite
    ' in the orbit at the given number of minutesSystem.Math.Since the TLE epoch time
    ' using the NORAD Simplified General Perturbation 4, "deep space" orbit
    ' model.
    '
    ' tsince  - Time in minutesSystem.Math.Since the TLE epoch (GMT).
    ' pECI    - pointer to location to store the ECI data.
    '           To convert the returned ECI position vector to km,
    '           multiply each component by:
    '              (XKMMPER / AE).
    '           To convert the returned ECI velocity vector to km/sec,
    '           multiply each component by:
    '              (XKMPER / AE) * (MIN_PER_DAY / 86400).
    Public Sub PosSDP4(tsince As Double, pos As Vector, vel As Vector)

        Dim xnoddf As Double, tsq As Double, xnode As Double
        Dim tempa As Double, tempe As Double, templ As Double
        Dim a As Double, e As Double, xmam As Double
        InitSDP4()
        DeepInit(eqsq, sinio, cosio, betao, aodp, theta2,
           sing, cosg, betao2, xmdot, omgdot, xnodot, xnodp)

        ' Update for secular gravity and atmospheric drag
        xmdf = xmo + xmdot * tsince
        omgadf = omegao + omgdot * tsince
        xnoddf = xnodeo + xnodot * tsince
        tsq = tsince * tsince
        xnode = xnoddf + xnodcf * tsq
        tempa = 1.0# - c1 * tsince
        tempe = bstar * c4 * tsince
        templ = t2cof * tsq
        xn = xnodp

        DeepSecular(xmdf, omgadf, xnode, em, xinc, xn, tsince)

        a = System.Math.Pow(xke / xn, tothrd) * Math.Sqrt(tempa)
        e = em - tempe
        xmam = xmdf + xnodp * templ


        DeepPeriodics(e, xinc, omgadf, xnode, xmam, tsince)

        xl = xmam + omgadf + xnode

        xn = xke / (a ^ 1.5)


        FinalPosition(omgadf, e, a, xl, xnode, xn, pos, vel)

    End Sub



End Module
