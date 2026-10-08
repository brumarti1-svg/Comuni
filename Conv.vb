Module Conv
    Public Const LUNGFILE = 100
    Dim catanr As Integer


    Public Function Convert_Satellite_Data(s_Satel As String) As Boolean
        Dim rs_Conv As Object = Nothing
        Dim el As Double
        Convert_Satellite_Data = False
        Try
            Dim tmpSQL As String = "SELECT * FROM [Sat2line] order by Sat_Name"
            rs_Conv = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
            rs_Conv.FindFirst("Sat_Name ='" & s_Satel & "'")
            If rs_Conv.NoMatch Then
                FrmMain.stpMessaggio.Text = "Satellite not found"
                FrmMain.TmrMsg.Enabled = True
                FrmRicezione.LblSatellite.BackColor = Color.Yellow
                rs_Conv.Close()
                Return False
            End If

            ' Helper locale — CDbl sicuro da Object (gestisce Boolean→Double via DAOWrapper)
            Dim R As Func(Of String, Object) =
                Function(f) If(rs_Conv(f) IsNot Nothing, rs_Conv(f), 0.0)

            catanr = CInt(CDbl(R("Sat_Number")))
            epoch = CDbl(R("EpochTime"))
            Dim sED As String = CStr(If(rs_Conv("EpochDate") IsNot Nothing, rs_Conv("EpochDate"), ""))
            If IsDate(sED) Then EpochDate = CDate(sED)
            julian_epoch = Julian_Date_of_Epoch(epoch)
            xndt2o = CDbl(R("Ftdmm"))
            xndd6o = CDbl(R("Stdmm"))
            bstar = CDbl(R("bstar"))
            ibexp = CInt(CDbl(R("TEphe")))
            el = CDbl(R("Element"))
            xincl = CDbl(R("Inclination"))
            xnodeo = CDbl(R("Raan"))
            eo = CDbl(R("Eccentricity"))
            omegao = CDbl(R("ArgPer"))
            xmo = CDbl(R("Anomaly"))
            xno = CDbl(R("MeanMo"))
            rs_Conv.Close()

            xnodeo *= GradiRad
            omegao *= GradiRad
            xmo *= GradiRad
            xincl *= GradiRad
            xno *= twopi / xmnpda
            xndt2o *= twopi / xmnpda ^ 2
            xndd6o *= twopi / Cube(xmnpda)

            a1 = Power(xke / xno, tothrd)
            Temp = 1.5 * ck2 * (3 * System.Math.Cos(xincl) ^ 2 - 1) / Power(1 - eo * eo, 1.5)
            del1 = Temp / (a1 * a1)
            ao = a1 * (1 - del1 * (0.5 * tothrd + del1 * (1 + 134 / 81 * del1)))
            delo = Temp / (ao * ao)
            xnodp = xno / (1 + delo)
            ideep = If(twopi / xnodp >= 225, 1, 0)
            Convert_Satellite_Data = True
        Catch ex As Exception
            If rs_Conv IsNot Nothing Then Try : rs_Conv.Close() : Catch : End Try
            If ex.HResult = 0 Or ex.HResult = 20 Then Return False
            WriteError(0, "Conv - Convert_Satellite_Data " & ex.Message)
        End Try
    End Function

    Public Function Convert_Sat_State(pos As Vector, vel As Vector) As Boolean

        Dim i As Byte
        Convert_Sat_State = True
        For i = 1 To 3

            pos.Valori(i) = pos.Valori(i) * xkmper   'kilometers}
            vel.Valori(i) = vel.Valori(i) * (xkmper / 60) 'kilometers/second}
        Next
        Magnitude(pos)
        Magnitude(vel)
    End Function



End Module
