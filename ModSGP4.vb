Module ModSGP4

    Public Const ae = 1
    Public Const tothrd = 2 / 3
    Public Const xkmper = 6378.135               'Earth equatorial radius - kilometers (WGS '72)}
    Public Const f = 1 / 298.26                         'Earth flattening (WGS '72)} appiattimento
    Public Const ge = 398600.8                           'Earth gravitational constant (WGS '72)}
    Public Const J2 = 0.0010826158                  'J2 harmonic (WGS '72)}
    Public Const J3 = -0.00000253881             'J3 harmonic (WGS '72)}
    Public Const J4 = -0.00000165597                'J4 harmonic (WGS '72)}
    Public Const ck2 = J2 / 2
    Public Const ck4 = -3 * J4 / 8
    Public Const xj3 = J3
    Public Const qo = ae + 120 / xkmper
    Public Const s = ae + 78 / xkmper
    Public Const e6a = 0.000001
    Public Const dpinit = 1                   'Deep-space initialization code
    Public Const dpsec = 2                    'Deep-space secular code
    Public Const dpper = 3                    'Deep-space periodic code

    Public iflag As Integer, ideep As Integer
    Public xmo As Double, xnodeo As Double, omegao As Double, eo As Double
    Public xincl As Double, xno As Double, xndt2o As Double, xndd6o As Double
    Public bstar As Double, julian_epoch As Double, xke As Double
    Public qoms2t As Double

    Public Const Orbita As String = "\orbita.Txt"
    Public s_TwoLine As String

    Public data As Date
    Public ts As time_set
    Public X1 As Double
    Public r As Double
    Public cAtanr As Single
    Public epoch As Double, EpochDate As Date
    Public iexp As Double
    Public ibexp As Double
    Public elset As Double
    Public xnodp As Double
    Public a1 As Double, ao As Double, del1 As Double
    Public delo As Double, Temp As Double
    Public aodp As Double
    '* Convert to proper units *}

    '       DISTANZA RADIALE DALL'ASSE DI ROTAZIONE
    Public ra As Double
    '       DISTANZA DAL PIANO EQUATORIALE
    Public RZ As Double
    'Costanti per il calcolo del two line di NORAD
    Public Const JulDate2000 = 2451545.5
    Public Const GIOSEC = 36525         'Numero di giorni in un secolo
    Public Const Nor1 = 100.46061837
    Public Const Nor2 = 36000.770053608
    Public Const Nor3 = 0.000387933
    Public Const Nor4 = 38710000
    Public Const n_FraOra = 86400 / 86164 * 360 'fattore di moltiplicazione per tener
    'conto delle ore del giorno
    Public Const n_MinutiGiorno = 24 * 60
    Public Const PIGREC = 3.14159265358979
    Public Const twopi = 2 * PIGREC
    Public Const k = PIGREC / 360
    Public Const k2 = 2 * PIGREC / 360
    'Conversione da log base 10 a ln
    Public Const ln10 = 2.30258509994046
    '       RAGGIO SINCRONO
    Public Const rs = 42164.57
    '       COSTANTI PER LA CONVERSIONE DEI RADIANTI IN GRADI E VICEVERSA
    Public Const RadGradi = 180 / PIGREC
    Public Const GradiRad = PIGREC / 180
    Public Const xmnpda = 1440              'Minutes per day
    Public Const secday = 86400             'Seconds per day
    Public Const milsecday = 86400000       'Millsecond for day
    Public Const omega_E = 1.00273790934     'Earth rotations per sidereal day (non-constant)
    Public Const omega_ER = omega_E * twopi  'Earth rotation, radians per sidereal day
    Public Const hrday = 24                 'ore per giorno
    '
    '       PARAMETRI DELLA TERRA
    '
    '       COSTANTE DI APPIATTIMENTO
    Public Const Ca = 0.0033528131
    '       ECCENTRICITA' DELL'ELLISSE RAPPRESENTANTE LA FORMA SCHIACCIATA DELLA
    '       TERRA
    Public Const EE = 0.08182 ' = Sqrt(1- Raggio Polare Terrestre / Raggio Equatoriale Terrestre
    'Costanti per il calcolo dell'orbita solare
    Public Const SR = 696000       'Raggio solare in Km
    Public Const AU = 149597870    'Unita' astronomica in Km
    '   Per il calcolo coordinate dell'osservatore
    Public Visible As Boolean


    Public a10 As Double
    Public a2 As Double
    Public a3 As Double
    Public a3ovk2 As Double
    Public a4 As Double
    Public a5 As Double
    Public a6 As Double
    Public a7 As Double
    Public a8 As Double
    Public a9 As Double
    Public ainv2 As Double
    Public alfdp As Double
    Public aqnv As Double
    Public atime As Double
    Public aycof As Double
    Public betao As Double
    Public betao2 As Double
    Public betdp As Double
    Public bfact As Double
    Public c As Double
    Public c1 As Double
    Public c1sq As Double
    Public c2 As Double
    Public c3 As Double
    Public c4 As Double
    Public c5 As Double
    Public cc As Double
    Public coef As Double
    Public coef1 As Double
    Public cosg As Double
    Public cosio As Double
    Public cosis As Double
    Public cosok As Double
    Public cosq As Double
    Public cosi2 As Double
    Public ctem As Double
    Public d2 As Double
    Public d2201 As Double
    Public d2211 As Double
    Public d3 As Double
    Public d3210 As Double
    Public d3222 As Double
    Public d4 As Double
    Public d4410 As Double
    Public d4422 As Double
    Public d5220 As Double
    Public d5232 As Double
    Public d5421 As Double
    Public d5433 As Double
    Public dalf As Double
    Public giorno As Double
    Public dbet As Double
    Public del2 As Double
    Public del3 As Double
    Public delmo As Double
    Public delt As Double
    Public dls As Double
    Public e3 As Double
    Public ee2 As Double
    Public eeta As Double
    Public eoc As Double
    Public eosq As Double
    Public eq As Double
    Public eta As Double
    Public etasq As Double
    Public f2 As Double
    Public f220 As Double
    Public f221 As Double
    Public f3 As Double
    Public f311 As Double
    Public f321 As Double
    Public f322 As Double
    Public f330 As Double
    Public f441 As Double
    Public f442 As Double
    Public f522 As Double
    Public f523 As Double
    Public f542 As Double
    Public f543 As Double
    Public fasx2 As Double
    Public fasx4 As Double
    Public fasx6 As Double
    Public ft As Double
    Public g200 As Double
    Public g201 As Double
    Public g211 As Double
    Public g300 As Double
    Public g310 As Double
    Public g322 As Double
    Public g410 As Double
    Public g422 As Double
    Public g520 As Double
    Public g521 As Double
    Public g532 As Double
    Public g533 As Double
    Public gam As Double
    Public iresfl As Double
    Public iret As Double
    Public iretn As Double
    Public isimp As Boolean
    Public isynfl As Double
    Public ls As Double
    Public omegaq As Double
    Public omgcof As Double
    Public omgdot As Double
    Public omgadf As Double
    Public pe As Double
    Public perige As Double
    Public pgh As Double
    Public ph As Double
    Public pinc As Double
    Public pinvsq As Double
    Public pl As Double
    Public preep As Double
    Public psisq As Double
    Public qoms24 As Double
    Public s1 As Double
    Public s2 As Double
    Public s3 As Double
    Public s4 As Double
    Public s5 As Double
    Public s6 As Double
    Public s7 As Double
    Public savtsn As Double
    Public se As Double
    Public se2 As Double
    Public se3 As Double
    Public sel As Double
    Public ses As Double
    Public sgh As Double
    Public sgh2 As Double
    Public sgh3 As Double
    Public sgh4 As Double
    Public sghl As Double
    Public sghs As Double
    Public sh As Double
    Public sh1 As Double
    Public sh2 As Double
    Public sh3 As Double
    Public shs As Double
    Public si As Double
    Public si2 As Double
    Public si3 As Double
    Public sil As Double
    Public sing As Double
    Public sini2 As Double
    Public sinio As Double
    Public sinis As Double
    Public sinmo As Double
    Public sinok As Double
    Public ok As Double
    Public sinq As Double
    Public sinzf As Double
    Public sis As Double
    Public sl As Double
    Public sl2 As Double
    Public sl3 As Double
    Public sl4 As Double
    Public sll As Double
    Public sls As Double
    Public sse As Double
    Public ssg As Double
    Public ssh As Double
    Public ssi As Double
    Public ssl As Double
    Public stem As Double
    Public step2 As Double
    Public stepn As Double
    Public stepp As Double
    Public t2cof As Double
    Public t3cof As Double
    Public t4cof As Double
    Public t5cof As Double
    Public temp1 As Double
    Public temp2 As Double
    Public temp3 As Double
    Public theta2 As Double
    Public theta4 As Double
    Public thgr As Double
    Public tsi As Double
    Public x1m5th As Double
    Public x1mth2 As Double
    Public X2 As Double
    Public x2li As Double
    Public x2omi As Double
    Public x3 As Double
    Public x3thm1 As Double
    Public x4 As Double
    Public x5 As Double
    Public x6 As Double
    Public x7 As Double
    Public x7thm1 As Double
    Public x8 As Double
    Public xfact As Double
    Public xgh2 As Double
    Public xgh3 As Double
    Public xgh4 As Double
    Public xh2 As Double
    Public xh3 As Double
    Public xhdot1 As Double
    Public xi2 As Double
    Public xi3 As Double
    Public xl As Double
    Public xl2 As Double
    Public xl3 As Double
    Public xl4 As Double
    Public xlamo As Double
    Public xlcof As Double
    Public xldot As Double
    Public xli As Double
    Public xls As Double
    Public xmao As Double
    Public xmcof As Double
    Public xmdf As Double
    Public xmdot As Double
    Public xnddt As Double
    Public xndot As Double
    Public xni As Double
    Public xno2 As Double
    Public xnodce As Double
    Public xnodcf As Double
    Public xnodot As Double
    Public xnoi As Double
    Public xnq As Double
    Public xomi As Double
    Public xpidot As Double
    Public xqncl As Double
    Public z1 As Double
    Public z11 As Double
    Public z12 As Double
    Public z13 As Double
    Public z2 As Double
    Public z21 As Double
    Public z22 As Double
    Public z23 As Double
    Public z3 As Double
    Public z31 As Double
    Public z32 As Double
    Public z33 As Double
    Public zcosg As Double
    Public zcosgl As Double
    Public zcosh As Double
    Public zcoshl As Double
    Public zcosi As Double
    Public zcosil As Double
    Public ze As Double
    Public zf As Double
    Public zm As Double
    Public zmo As Double
    Public zmol As Double
    Public zmos As Double
    Public zn As Double
    Public zsing As Double
    Public zsingl As Double
    Public zsinh As Double
    Public zsinhl As Double
    Public zsini As Double
    Public zsinil As Double
    Public Zx As Double
    Public Zy As Double
    Public xx1 As Double, xx2 As Double, xx3 As Double, xx4 As Double
    'dpinit}
    Public eqsq As Double, siniq As Double, cosiq As Double, rteqsq As Double
    Public cosq2 As Double, sinomo As Double, cosomo As Double
    Public bsq As Double, xlldot As Double, omgdt As Double
    'dpsec/dpper}
    Public xll As Double, omgasm As Double, xnodes As Double, em As Double, xinc As Double, xn As Double, T As Double

    ' Additional variables for SDP4 deep-space model
    Public g As Double, iq As Double, omo As Double
    Public q2 As Double, q As Double, i2 As Double
    Public cosinc As Double, io As Double

End Module
