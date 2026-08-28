Module Observer
    Public geodetic As Vector = NewVector()
    Public obs_pos As Vector = NewVector()
    Public obs_vel As Vector = NewVector()
    Public obs_ As Vector = NewVector()
    Public obs_set As Vector = NewVector()
    Public Const c_b2 = 428.4014

    Public Function Calculate_User_PosVel(timex As Double,
                                obs_pos As Vector, obs_vel As Vector) As Boolean

        Dim theta As Double
        Dim c As Double, s As Double, achcp As Double
        Calculate_User_PosVel = True
        Const mfactor = twopi * omega_E / secday
        theta = Modulus(ThetaG_JD(timex) + LONSTA, twopi)
        c = 1 / Math.Sqrt(1 + f * (f - 2) * (Math.Sin(LATSTA) ^ 2))
        s = ((1 - f) ^ 2) * c
        achcp = (xkmper * c + H) * Math.Cos(LATSTA)
        obs_pos.Valori(1) = achcp * Math.Cos(theta)
        obs_pos.Valori(2) = achcp * Math.Sin(theta)
        obs_pos.Valori(3) = (xkmper * s + H) * Math.Sin(LATSTA)
        obs_vel.Valori(1) = -mfactor * obs_pos.Valori(2)
        obs_vel.Valori(2) = mfactor * obs_pos.Valori(1)
        obs_vel.Valori(3) = 0
        Magnitude(obs_pos)
        Magnitude(obs_vel)
    End Function

    Public Function Calculate_LatLonAlt(pos As Vector, timex As Double, geodetic As Vector) As Boolean
        Dim lat As Double, Lon As Double, alt As Double, delta As Double
        Dim theta As Double, r As Double, e2 As Double, phi As Double, c As Double
        Calculate_LatLonAlt = True
        theta = AcTan(pos.Valori(2), pos.Valori(1))
        Lon = Modulus(theta - ThetaG_JD(timex), twopi)
        r = Math.Sqrt((pos.Valori(1) ^ 2) + (pos.Valori(2) ^ 2))
        e2 = f * (2 - f)
        lat = AcTan(pos.Valori(3), r)
        delta = 0.0000000001
        Do While Math.Abs(lat - phi) >= delta
            phi = lat
            c = 1 / Math.Sqrt(1 - e2 * (Math.Sin(phi) ^ 2))
            lat = AcTan(pos.Valori(3) + xkmper * c * e2 * Math.Sin(phi), r)
        Loop
        alt = r / Math.Cos(lat) - xkmper * c
        geodetic.Valori(1) = lat
        geodetic.Valori(2) = Lon
        geodetic.Valori(3) = alt
        geodetic.Valori(4) = theta
    End Function

    Public Function Calculate_Obs(pos As Vector, vel As Vector,
                                timex As Double, obs_ As Vector) As Boolean

        Dim theta As Double
        Dim Sin_lat As Double, cos_lat As Double
        Dim Sin_theta As Double, cos_theta As Double
        Dim el As Double, azim As Double
        Dim top_s As Double, top_e As Double, top_z As Double
        Dim obs_pos As Vector = NewVector(), obs_vel As Vector = NewVector()
        Dim range As Vector = NewVector(), rgvel As Vector = NewVector()
        Dim d2 As Double, d1 As Double
        Calculate_Obs = True
        Calculate_User_PosVel(timex, obs_pos, obs_vel)
        For i As Integer = 1 To 3
            range.Valori(i) = pos.Valori(i) - obs_pos.Valori(i)
            rgvel.Valori(i) = vel.Valori(i) - obs_vel.Valori(i)
        Next
        Magnitude(range)
        theta = Modulus(ThetaG_JD(timex) + LONSTA, twopi)
        Sin_lat = Math.Sin(LATSTA)
        cos_lat = Math.Cos(LATSTA)
        Sin_theta = Math.Sin(theta)
        cos_theta = Math.Cos(theta)
        top_s = Sin_lat * cos_theta * range.Valori(1) _
             + Sin_lat * Sin_theta * range.Valori(2) _
             - cos_lat * range.Valori(3)
        top_e = -Sin_theta * range.Valori(1) _
             + cos_theta * range.Valori(2)
        top_z = cos_lat * cos_theta * range.Valori(1) _
             + cos_lat * Sin_theta * range.Valori(2) _
             + Sin_lat * range.Valori(3)
        azim = Math.Atan(-top_e / top_s)
        If top_s > 0 Then
            azim += PIGREC
        End If
        If azim < 0 Then
            azim += twopi
        End If
        el = ArcSin(top_z / range.Valori(4))
        obs_set.Valori(1) = azim
        obs_set.Valori(2) = el
        obs_set.Valori(3) = range.Valori(4)
        obs_set.Valori(4) = Dot(range, rgvel) / range.Valori(4)
        d2 = RadGradi * el + 10.3 / (el * RadGradi + 5.11)
        d1 = 1.02 / Math.Tan(GradiRad * d2) / 60
        obs_set.Valori(2) = obs_set.Valori(2) + (GradiRad * d1)
        If obs_set.Valori(2) < 0 Then
            Visible = False
        ElseIf obs_set.Valori(2) >= 0 Then
            Visible = True
        End If
    End Function

    Public Function Calculate_RADec(pos As Vector, vel As Vector, geodetic As Vector,
                                      timex As Double, obs_ As Vector) As Boolean

        Dim phi As Double, theta As Double, Sin_theta As Double, cos_theta As Double
        Dim Sin_phi As Double, cos_phi As Double
        Dim az As Double, el As Double, Lxh As Double, Lyh As Double, Lzh As Double
        Dim Sx As Double, Ex As Double, Zx As Double
        Dim Sy As Double, Ey As Double, Zy As Double
        Dim Sz As Double, Ez As Double, Zz As Double
        Dim Lx As Double, Ly As Double, Lz As Double, cos_delta As Double
        Dim Sin_alpha As Double, cos_alpha As Double
        Calculate_RADec = True
        Calculate_Obs(pos, vel, timex, obs_set)
        If Visible Then
            az = obs_set.Valori(1)
            el = obs_set.Valori(2)
            phi = LATSTA
            theta = Modulus(ThetaG_JD(timex) + LONSTA, twopi)
            Sin_theta = Math.Sin(theta)
            cos_theta = Math.Cos(theta)
            Sin_phi = Math.Sin(phi)
            cos_phi = Math.Cos(phi)
            Lxh = -Math.Cos(az) * Math.Cos(el)
            Lyh = Math.Sin(az) * Math.Cos(el)
            Lzh = Math.Sin(el)
            Sx = Sin_phi * cos_theta
            Ex = -Sin_theta
            Zx = cos_theta * cos_phi
            Sy = Sin_phi * Sin_theta
            Ey = cos_theta
            Zy = Sin_theta * cos_phi
            Sz = -cos_phi
            Ez = 0
            Zz = Sin_phi
            Lx = Sx * Lxh + Ex * Lyh + Zx * Lzh
            Ly = Sy * Lxh + Ey * Lyh + Zy * Lzh
            Lz = Sz * Lxh + Ez * Lyh + Zz * Lzh
            obs_set.Valori(2) = ArcSin(Lz)
            cos_delta = Math.Sqrt(1 - (Lz ^ 2))
            Sin_alpha = Ly / cos_delta
            cos_alpha = Lx / cos_delta
            obs_set.Valori(1) = AcTan(Sin_alpha, cos_alpha)
            obs_set.Valori(1) = Modulus(obs_set.Valori(1), twopi)
        End If
    End Function

End Module
