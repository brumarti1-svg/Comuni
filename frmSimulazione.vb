Option Explicit On

Public Class FrmSimulazione

    Private DataEsegui As Date
    Private PTKold As Integer
    Private i_ProgTrackOld As Integer
    Private i_Simulation As Integer
    Private i_StatusOld As Integer

    Private Sub CmdAnnulla_Click(sender As Object, e As EventArgs) Handles CmdAnnulla.Click
        frmRicezione.LblAzimuth.Text = ""
        frmRicezione.LblElevazione.Text = ""
        frmRicezione.LblAzimuthC.Text = ""
        frmRicezione.LblElevazioneC.Text = ""
        AzCalc = 0
        ElCalc = 0
        frmRicezione.LblAzError.Text = ""
        frmRicezione.LblElError.Text = ""
        frmRicezione.LblAzErrEn.Text = ""
        frmRicezione.LblElErrEn.Text = ""
        AzOffset = 0
        ElOffset = 0
        DeltaTime = 0
        Me.Close()
    End Sub

    Private Sub CmdLoad_Click(sender As Object, e As EventArgs) Handles CmdLoad.Click
        If IsDate(mskDataIni.Text) Then
            '            Calcolo_Coordinate1 mskDataIni.Text
        Else
            mskDataIni.Text = "__/__/____ __:__:__"
            mskDataIni.Focus()
        End If
    End Sub

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        i_StatusOld = i_Status
        PTKold = PTK
        i_ProgTrackOld = i_ProgTrack
        i_ProgTrack = 1
        i_Status = OP_SIMULATION
        frmRicezione.LblCommand.Text = "Simulation"
        If IsDate(mskDataIni.Text) And IsDate(mskDataFin.Text) Then
            DataEsegui = CDate(mskDataIni.Text)
            LblDateSimul.Text = Format(DataEsegui, "dd/mm/yyyy hh:mm:ss")
            tmrSimulation.Interval = i_Simulation * 1000
            tmrSimulation.Enabled = True
        End If
    End Sub

    Private Sub Form_Activate(sender As Object, e As EventArgs) Handles Me.Activated
        i_Simulation = CInt(Int(i_InterRic + 0.5))
        If i_Simulation < 0 Then i_Simulation = 1
        mskDataIni.Focus()
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not b_Calcola Then Exit Sub
        ' VB6: Now - CDate(i_Offset/24) — i_Offset è intero in ore, /24 convertiva in giorni OLE
        mskDataIni.Text = Now.AddHours(-i_Offset).ToString("dd/MM/yyyy HH:mm:ss")
    End Sub

    Private Sub MskDataFin_KeyUp(sender As Object, e As KeyEventArgs) Handles mskDataFin.KeyUp
        If e.KeyCode = Keys.Return Then
            If Not IsDate(mskDataFin.Text) OrElse CDate(mskDataIni.Text) > CDate(mskDataFin.Text) Then
                mskDataFin.Text = "__/__/____ __:__:__"
                mskDataFin.Focus()
            End If
        End If
    End Sub

    Private Sub MskDataIni_KeyUp(sender As Object, e As KeyEventArgs) Handles mskDataIni.KeyUp
        If e.KeyCode = Keys.Return Then
            If Not IsDate(mskDataIni.Text) Then
                mskDataIni.Text = "__/__/____ __:__:__"
                mskDataIni.Focus()
            End If
        End If
    End Sub

    Private Sub TmrSimulation_Tick(sender As Object, e As EventArgs) Handles tmrSimulation.Tick
        DataEsegui = DataEsegui.AddSeconds(i_Simulation)
        If DataEsegui <= CDate(mskDataFin.Text) Then
            Calcolo_Coordinate(DataEsegui, 0)
            LblDateSimul.Text = Format(DataEsegui, "dd/mm/yyyy hh:mm:ss")
        Else
            LblDateSimul.BackColor = Color.Yellow
            PTK = 0
            frmRicezione.LblCommand.Text = ""
            i_ProgTrack = i_ProgTrackOld
            FrmMain.stpMessaggio.Text = "Simulation End"
            FrmMain.TmrMsg.Enabled = True
        End If
        tmrSimulation.Enabled = True
    End Sub

End Class
