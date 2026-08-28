Imports System.Math

Public Class FrmConvAngoli

    ' Stato di calcolo condiviso tra le funzioni geodetiche (pattern VB6 originale)
    Dim sfactor As Double, RadY_Rot As Double, RadZ_Rot As Double
    Dim RadX_Rot As Double, RootXYSqrt As Double
    Dim a As Double, e2 As Double, PHI1 As Double, z As Double
    Dim phi As Double, v As Double, PHI2 As Double, RadPHI As Double
    Dim RadLAM As Double, RadPHI0 As Double, RadLAM0 As Double
    Dim af0 As Double, f0 As Double, bf0 As Double, B As Double
    Dim nu As Double, rho As Double, eta2 As Double
    Dim p As Double, XIX As Double, XX As Double, n As Double
    Dim VI As Double, IV As Double, M As Double, i As Double
    Dim ii As Double, III As Double, IIIA As Double
    Dim ET As Double, VII As Double, VIII As Double, IX As Double
    Dim PHId As Double, X As Double, XI As Double, XII As Double
    Dim XIIA As Double, XIII As Double, XIV As Double, XV As Double
    Dim XVI As Double, XVII As Double, XVIII As Double
    Dim XXI As Double, XXII As Double, XXIII As Double
    Dim Nm As Double, Diffe As Double, Diffn As Double
    Dim GridBearing As Double, Ratio As Double, GridAngle As Double
    Dim Convergence As Double, t_minus_T As Double

    ' -----------------------------------------------------------------------
    ' Trasformazione di Helmert
    ' -----------------------------------------------------------------------

    Function Helmert_X(X As Double, y As Double, z As Double,
                       DX As Double, Y_Rot As Double, Z_Rot As Double,
                       s As Double) As Double
        sfactor = s * 0.000001
        RadY_Rot = (Y_Rot / 3600) * GradiRad
        RadZ_Rot = (Z_Rot / 3600) * GradiRad
        Return X + (X * sfactor) - (y * RadZ_Rot) + (z * RadY_Rot) + DX
    End Function

    Function Helmert_Y(X As Double, y As Double, z As Double,
                       dy As Double, X_Rot As Double, Z_Rot As Double,
                       s As Double) As Double
        sfactor = s * 0.000001
        RadX_Rot = (X_Rot / 3600) * GradiRad
        RadZ_Rot = (Z_Rot / 3600) * GradiRad
        Return (X * RadZ_Rot) + y + (y * sfactor) - (z * RadX_Rot) + dy
    End Function

    Function Helmert_Z(X As Double, y As Double, z As Double,
                       DZ As Double, X_Rot As Double, Y_Rot As Double,
                       s As Double) As Double
        sfactor = s * 0.000001
        RadX_Rot = (X_Rot / 3600) * GradiRad
        RadY_Rot = (Y_Rot / 3600) * GradiRad
        Return (-1 * X * RadY_Rot) + (y * RadX_Rot) + z + (z * sfactor) + DZ
    End Function

    ' -----------------------------------------------------------------------
    ' Conversioni XYZ cartesiane ↔ coordinate geodetiche
    ' -----------------------------------------------------------------------

    Function XYZ_to_Lat(X As Double, y As Double, z As Double,
                        a As Double, B As Double) As Double
        ' Richiede Iterate_XYZ_to_Lat
        RootXYSqrt = Sqrt((X ^ 2) + (y ^ 2))
        e2 = ((a ^ 2) - (B ^ 2)) / (a ^ 2)
        PHI1 = Atan(z / (RootXYSqrt * (1 - e2)))
        phi = Iterate_XYZ_to_Lat(a, e2, PHI1, z, RootXYSqrt)
        Return phi * RadGradi
    End Function

    Function Iterate_XYZ_to_Lat(a As Double, e2 As Double, PHI1 As Double,
                                 z As Double, RootXYSqrt As Double) As Double
        v = a / Sqrt(1 - (e2 * (Sin(PHI1) ^ 2)))
        PHI2 = Atan((z + (e2 * v * Sin(PHI1))) / RootXYSqrt)
        Do While Abs(PHI1 - PHI2) > 0.000000001
            PHI1 = PHI2
            v = a / Sqrt(1 - (e2 * (Sin(PHI1) ^ 2)))
            PHI2 = Atan((z + (e2 * v * Sin(PHI1))) / RootXYSqrt)
        Loop
        Return PHI2
    End Function

    Function XYZ_to_Long(X As Double, y As Double) As Double
        Return Atan(y / X) * RadGradi
    End Function

    Function XYZ_to_H(X As Double, y As Double, z As Double,
                      a As Double, B As Double) As Double
        ' Richiede XYZ_to_Lat
        Dim h As Double
        phi = XYZ_to_Lat(X, y, z, a, B)
        RadPHI = phi * GradiRad
        RootXYSqrt = Sqrt((X ^ 2) + (y ^ 2))
        e2 = ((a ^ 2) - (B ^ 2)) / (a ^ 2)
        v = a / Sqrt(1 - (e2 * (Sin(RadPHI) ^ 2)))
        h = (RootXYSqrt / Cos(RadPHI)) - v
        Return h
    End Function

    ' -----------------------------------------------------------------------
    ' Conversioni geodetiche ↔ cartesiane
    ' -----------------------------------------------------------------------

    Function Lat_Long_H_to_X(phi As Double, LAM As Double, H As Double,
                              a As Double, B As Double) As Double
        RadPHI = phi * GradiRad
        RadLAM = LAM * GradiRad
        e2 = ((a ^ 2) - (B ^ 2)) / (a ^ 2)
        v = a / Sqrt(1 - (e2 * (Sin(RadPHI) ^ 2)))
        Return (v + H) * Cos(RadPHI) * Cos(RadLAM)
    End Function

    Function Lat_Long_H_to_Y(phi As Double, LAM As Double, H As Double,
                              a As Double, B As Double) As Double
        RadPHI = phi * GradiRad
        RadLAM = LAM * GradiRad
        e2 = ((a ^ 2) - (B ^ 2)) / (a ^ 2)
        v = a / Sqrt(1 - (e2 * (Sin(RadPHI) ^ 2)))
        Return (v + H) * Cos(RadPHI) * Sin(RadLAM)
    End Function

    Function Lat_H_to_Z(phi As Double, H As Double,
                        a As Double, B As Double) As Double
        RadPHI = phi * GradiRad
        e2 = ((a ^ 2) - (B ^ 2)) / (a ^ 2)
        v = a / Sqrt(1 - (e2 * (Sin(RadPHI) ^ 2)))
        Return ((v * (1 - e2)) + H) * Sin(RadPHI)
    End Function

    ' -----------------------------------------------------------------------
    ' Proiezione Transverse Mercator
    ' -----------------------------------------------------------------------

    Function Lat_Long_to_East(phi As Double, LAM As Double,
                              a As Double, B As Double, e0 As Double,
                              f0 As Double, PHI0 As Double, LAM0 As Double) As Double
        RadPHI = phi * GradiRad
        RadLAM = LAM * GradiRad
        RadPHI0 = PHI0 * GradiRad
        RadLAM0 = LAM0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        n = (af0 - bf0) / (af0 + bf0)
        nu = af0 / Sqrt(1 - (e2 * (Sin(RadPHI) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(RadPHI) ^ 2))
        eta2 = (nu / rho) - 1
        p = RadLAM - RadLAM0
        IV = nu * Cos(RadPHI)
        v = (nu / 6) * (Cos(RadPHI) ^ 3) * ((nu / rho) - (Tan(RadPHI) ^ 2))
        VI = (nu / 120) * (Cos(RadPHI) ^ 5) * (5 - (18 * (Tan(RadPHI) ^ 2)) + (Tan(RadPHI) ^ 4) + (14 * eta2) - (58 * (Tan(RadPHI) ^ 2) * eta2))
        Return e0 + (p * IV) + ((p ^ 3) * v) + ((p ^ 5) * VI)
    End Function

    Function Lat_Long_to_North(phi As Double, LAM As Double,
                               a As Double, B As Double, e0 As Double, n0 As Double,
                               f0 As Double, PHI0 As Double, LAM0 As Double) As Double
        ' Richiede Marc
        RadPHI = phi * GradiRad
        RadLAM = LAM * GradiRad
        RadPHI0 = PHI0 * GradiRad
        RadLAM0 = LAM0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        n = (af0 - bf0) / (af0 + bf0)
        nu = af0 / Sqrt(1 - (e2 * (Sin(RadPHI) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(RadPHI) ^ 2))
        eta2 = (nu / rho) - 1
        p = RadLAM - RadLAM0
        M = Marc(bf0, n, RadPHI0, RadPHI)
        i = M + n0
        ii = (nu / 2) * Sin(RadPHI) * Cos(RadPHI)
        III = ((nu / 24) * Sin(RadPHI) * (Cos(RadPHI) ^ 3)) * (5 - (Tan(RadPHI) ^ 2) + (9 * eta2))
        IIIA = ((nu / 720) * Sin(RadPHI) * (Cos(RadPHI) ^ 5)) * (61 - (58 * (Tan(RadPHI) ^ 2)) + (Tan(RadPHI) ^ 4))
        Return i + ((p ^ 2) * ii) + ((p ^ 4) * III) + ((p ^ 6) * IIIA)
    End Function

    ' -----------------------------------------------------------------------
    ' Inversione Transverse Mercator (E/N → Lat/Long)
    ' -----------------------------------------------------------------------

    Function E_N_to_Lat(East As Double, North As Double,
                        a As Double, B As Double, e0 As Double, n0 As Double,
                        f0 As Double, PHI0 As Double, LAM0 As Double) As Double
        ' Richiede Marc e InitialLat
        RadPHI0 = PHI0 * GradiRad
        RadLAM0 = LAM0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        n = (af0 - bf0) / (af0 + bf0)
        ET = East - e0
        PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0)
        nu = af0 / Sqrt(1 - (e2 * (Sin(PHId) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(PHId) ^ 2))
        eta2 = (nu / rho) - 1
        VII = Tan(PHId) / (2 * rho * nu)
        VIII = (Tan(PHId) / (24 * rho * (nu ^ 3))) * (5 + (3 * (Tan(PHId) ^ 2)) + eta2 - (9 * eta2 * (Tan(PHId) ^ 2)))
        IX = (Tan(PHId) / (720 * rho * (nu ^ 5))) * (61 + (90 * (Tan(PHId) ^ 2)) + (45 * (Tan(PHId) ^ 4)))
        Return RadGradi * (PHId - ((ET ^ 2) * VII) + ((ET ^ 4) * VIII) - ((ET ^ 6) * IX))
    End Function

    Function E_N_to_Long(East As Double, North As Double,
                         a As Double, B As Double, e0 As Double, n0 As Double,
                         f0 As Double, PHI0 As Double, LAM0 As Double) As Double
        ' Richiede Marc e InitialLat
        RadPHI0 = PHI0 * GradiRad
        RadLAM0 = LAM0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        n = (af0 - bf0) / (af0 + bf0)
        ET = East - e0
        PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0)
        nu = af0 / Sqrt(1 - (e2 * (Sin(PHId) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(PHId) ^ 2))
        eta2 = (nu / rho) - 1
        X = (Cos(PHId) ^ -1) / nu
        XI = ((Cos(PHId) ^ -1) / (6 * (nu ^ 3))) * ((nu / rho) + (2 * (Tan(PHId) ^ 2)))
        XII = ((Cos(PHId) ^ -1) / (120 * (nu ^ 5))) * (5 + (28 * (Tan(PHId) ^ 2)) + (24 * (Tan(PHId) ^ 4)))
        XIIA = ((Cos(PHId) ^ -1) / (5040 * (nu ^ 7))) * (61 + (662 * (Tan(PHId) ^ 2)) + (1320 * (Tan(PHId) ^ 4)) + (720 * (Tan(PHId) ^ 6)))
        Return RadGradi * (RadLAM0 + (ET * X) - ((ET ^ 3) * XI) + ((ET ^ 5) * XII) - ((ET ^ 7) * XIIA))
    End Function

    ' -----------------------------------------------------------------------
    ' Funzioni ausiliarie
    ' -----------------------------------------------------------------------

    Function InitialLat(North As Double, n0 As Double, afo As Double,
                        PHI0 As Double, n As Double, bfo As Double) As Double
        ' Richiede Marc
        PHI1 = ((North - n0) / afo) + PHI0
        M = Marc(bfo, n, PHI0, PHI1)
        PHI2 = ((North - n0 - M) / afo) + PHI1
        Do While Abs(North - n0 - M) > 0.00001
            PHI2 = ((North - n0 - M) / afo) + PHI1
            M = Marc(bfo, n, PHI0, PHI2)
            PHI1 = PHI2
        Loop
        Return PHI2
    End Function

    Function Marc(bf0 As Double, n As Double, PHI0 As Double, phi As Double) As Double
        Return bf0 * (((1 + n + ((5 / 4) * (n ^ 2)) + ((5 / 4) * (n ^ 3))) * (phi - PHI0)) _
            - (((3 * n) + (3 * (n ^ 2)) + ((21 / 8) * (n ^ 3))) * Sin(phi - PHI0) * Cos(phi + PHI0)) _
            + ((((15 / 8) * (n ^ 2)) + ((15 / 8) * (n ^ 3))) * Sin(2 * (phi - PHI0)) * Cos(2 * (phi + PHI0))) _
            - (((35 / 24) * (n ^ 3)) * Sin(3 * (phi - PHI0)) * Cos(3 * (phi + PHI0))))
    End Function

    ' -----------------------------------------------------------------------
    ' Convergenza e fattore di scala locale
    ' -----------------------------------------------------------------------

    Function Lat_Long_to_C(phi As Double, LAM As Double, LAM0 As Double,
                           a As Double, B As Double, f0 As Double) As Double
        RadPHI = phi * GradiRad
        RadLAM = LAM * GradiRad
        RadLAM0 = LAM0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        nu = af0 / Sqrt(1 - (e2 * (Sin(RadPHI) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(RadPHI) ^ 2))
        eta2 = (nu / rho) - 1
        p = RadLAM - RadLAM0
        XIII = Sin(RadPHI)
        XIV = ((Sin(RadPHI) * (Cos(RadPHI) ^ 2)) / 3) * (1 + (3 * eta2) + (2 * (eta2 ^ 2)))
        XV = ((Sin(RadPHI) * (Cos(RadPHI) ^ 4)) / 15) * (2 - (Tan(RadPHI) ^ 2))
        Return RadGradi * ((p * XIII) + ((p ^ 3) * XIV) + ((p ^ 5) * XV))
    End Function

    Function E_N_to_C(East As Double, North As Double,
                      a As Double, B As Double, e0 As Double, n0 As Double,
                      f0 As Double, PHI0 As Double) As Double
        ' Richiede Marc e InitialLat
        RadPHI0 = PHI0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        n = (af0 - bf0) / (af0 + bf0)
        ET = East - e0
        PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0)
        nu = af0 / Sqrt(1 - (e2 * (Sin(PHId) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(PHId) ^ 2))
        eta2 = (nu / rho) - 1
        XVI = Tan(PHId) / nu
        XVII = (Tan(PHId) / (3 * (nu ^ 3))) * (1 + (Tan(PHId) ^ 2) - eta2 - (2 * (eta2 ^ 2)))
        XVIII = (Tan(PHId) / (15 * (nu ^ 5))) * (2 + (5 * (Tan(PHId) ^ 2)) + (3 * (Tan(PHId) ^ 4)))
        Return RadGradi * ((ET * XVI) - ((ET ^ 3) * XVII) + ((ET ^ 5) * XVIII))
    End Function

    Function Lat_Long_to_LSF(phi As Double, LAM As Double, LAM0 As Double,
                             a As Double, B As Double, f0 As Double) As Double
        RadPHI = phi * GradiRad
        RadLAM = LAM * GradiRad
        RadLAM0 = LAM0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        nu = af0 / Sqrt(1 - (e2 * (Sin(RadPHI) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(RadPHI) ^ 2))
        eta2 = (nu / rho) - 1
        p = RadLAM - RadLAM0
        XIX = ((Cos(RadPHI) ^ 2) / 2) * (1 + eta2)
        XX = ((Cos(RadPHI) ^ 4) / 24) * (5 - (4 * (Tan(RadPHI) ^ 2)) + (14 * eta2) - (28 * (Tan(RadPHI * eta2) ^ 2)))
        Return f0 * (1 + ((p ^ 2) * XIX) + ((p ^ 4) * XX))
    End Function

    Function E_N_to_LSF(East As Double, North As Double,
                        a As Double, B As Double, e0 As Double, n0 As Double,
                        f0 As Double, PHI0 As Double) As Double
        ' Richiede Marc e InitialLat
        RadPHI0 = PHI0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        n = (af0 - bf0) / (af0 + bf0)
        ET = East - e0
        PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0)
        nu = af0 / Sqrt(1 - (e2 * (Sin(PHId) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(PHId) ^ 2))
        eta2 = (nu / rho) - 1
        XXI = 1 / (2 * rho * nu)
        XXII = (1 + (4 * eta2)) / (24 * (rho ^ 2) * (nu ^ 2))
        Return f0 * (1 + ((ET ^ 2) * XXI) + ((ET ^ 4) * XXII))
    End Function

    Function E_N_to_t_minus_T(AtEast As Double, AtNorth As Double,
                              ToEast As Double, ToNorth As Double,
                              a As Double, B As Double, e0 As Double, n0 As Double,
                              f0 As Double, PHI0 As Double) As Double
        ' Richiede Marc e InitialLat
        RadPHI0 = PHI0 * GradiRad
        af0 = a * f0
        bf0 = B * f0
        e2 = ((af0 ^ 2) - (bf0 ^ 2)) / (af0 ^ 2)
        n = (af0 - bf0) / (af0 + bf0)
        Nm = (AtNorth + ToNorth) / 2
        PHId = InitialLat(Nm, n0, af0, RadPHI0, n, bf0)
        nu = af0 / Sqrt(1 - (e2 * (Sin(PHId) ^ 2)))
        rho = (nu * (1 - e2)) / (1 - (e2 * Sin(PHId) ^ 2))
        XXIII = 1 / (6 * nu * rho)
        Return RadGradi * ((2 * (AtEast - e0)) + (ToEast - e0)) * (AtNorth - ToNorth) * XXIII
    End Function

    Function TrueAzimuth(AtEast As Double, AtNorth As Double,
                         ToEast As Double, ToNorth As Double,
                         a As Double, B As Double, e0 As Double, n0 As Double,
                         f0 As Double, PHI0 As Double) As Double
        ' Richiede Marc, InitialLat, E_N_to_t_minus_T, E_N_to_C
        Diffe = ToEast - AtEast
        Diffn = ToNorth - AtNorth

        If Diffe = 0 Then
            GridBearing = If(Diffn < 0, 180.0, 0.0)
        Else
            Ratio = Diffn / Diffe
            GridAngle = RadGradi * Atan(Ratio)
            If Diffe > 0 Then
                GridBearing = 90 - GridAngle
            Else
                GridBearing = 270 - GridAngle
            End If
        End If

        Convergence = E_N_to_C(AtEast, AtNorth, a, B, e0, n0, f0, PHI0)
        t_minus_T = E_N_to_t_minus_T(AtEast, AtNorth, ToEast, ToNorth, a, B, e0, n0, f0, PHI0)
        Return GridBearing + Convergence - t_minus_T
    End Function

    ' -----------------------------------------------------------------------
    ' Event handlers
    ' -----------------------------------------------------------------------

    Private Sub FrmConvAngoli_Load(sender As Object, e As EventArgs) Handles Me.Load
        If g_Conv = 1 Then
            TxtSecLat.Visible = False
            TxtSecLon.Visible = False
        End If
        s_Latitudine = 370      ' valore sentinella: nessun dato valido
        s_Longitudine = 370
    End Sub

    Private Sub CmdCalcolo_Click(sender As Object, e As EventArgs) Handles CmdCalcolo.Click
        Try
            Dim latDec As Double
            Dim lonDec As Double

            If g_Conv = 0 Then
                ' Gradi/Primi/Secondi → decimali
                Dim secLat = CDbl(TxtSecLat.Text)
                Dim primiLat = CDbl(TxtPrimiLat.Text)
                Dim graLat = CDbl(TxtGraLat.Text)
                latDec = ((secLat / 60) + primiLat) / 60 + graLat
                If UCase(CmbLat.Text) = "S" Then latDec *= -1

                Dim secLon = CDbl(TxtSecLon.Text)
                Dim primiLon = CDbl(TxtPrimiLon.Text)
                Dim graLon = CDbl(TxtGraLon.Text)
                lonDec = ((secLon / 60) + primiLon) / 60 + graLon
                If UCase(CmbLong.Text) = "W" Then lonDec *= -1
            Else
                ' Gradi/Decimi → decimali
                latDec = CDbl(TxtGraLat.Text) + CDbl(TxtPrimiLat.Text) / 60
                If UCase(CmbLat.Text) = "S" Then latDec *= -1

                lonDec = CDbl(TxtGraLon.Text) + CDbl(TxtPrimiLon.Text) / 60
                If UCase(CmbLong.Text) = "W" Then lonDec *= -1
            End If

            LblLatitudine.Text = CStr(latDec)
            LblLongitudine.Text = CStr(lonDec)

            If IsNumeric(LblLatitudine.Text) AndAlso IsNumeric(LblLongitudine.Text) Then
                s_Latitudine = latDec
                s_Longitudine = lonDec
            End If
        Catch ex As Exception
            MessageBox.Show("Valori non validi: " & ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub CmdEsci_Click(sender As Object, e As EventArgs) Handles CmdEsci.Click
        Me.Close()
    End Sub

    Private Sub TxtGraLat_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtGraLat.KeyUp
        If e.KeyCode = Keys.Return Then
            If Not IsNumeric(TxtGraLat.Text) OrElse
               (IsNumeric(TxtGraLat.Text) AndAlso CDbl(TxtGraLat.Text) > 180) Then
                TxtGraLat.Text = ""
                TxtGraLat.Focus()
                Exit Sub
            End If
            TxtPrimiLat.Focus()
        End If
    End Sub

End Class
