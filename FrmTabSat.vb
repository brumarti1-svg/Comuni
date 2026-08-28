Public Class FrmTabSat
    Dim tmpSQL As String
    Dim b_inter As Boolean
    Dim Campo As String
    Dim i As Integer
    Dim pos As Integer

    Private Sub FrmTabSat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' datTMath.Absat / tdbTMath.Absat VB6 data controls not migrated
    End Sub

    Public Function ValoreSat(Messaggio As String) As Boolean
        Dim rs_Sat As Object
        Dim TabAz As String, TabEl As String, TabPol As String, Port As Integer, s_segn As String
        On Error Resume Next
        ValoreSat = True
        i_Inter = LeggiFileIni("Canale1", "Inter")
        frmRicezione.TmrTimeoutMSG(ACU).Interval = Port_Control(ACU).Time_out * 1000
        Campo = Mid$(Messaggio, 6, 1)
        i = 1
        i_Cont = 1
        rs_Sat = dbOrbite.OpenRecordset("Sat", dbOpenDynaset)
        ' leggo i campi solo per lo status
        pos = 7
        If Campo = "<" Then
            For i = 1 To 40
                rs_Sat.AddNew
                TabAz = Mid$(Messaggio, pos, 6)
                pos = pos + 6
                TabEl = Mid$(Messaggio, pos, 5)
                pos = pos + 5
                s_segn = Mid$(Messaggio, pos, 1)
                pos = pos + 1
                TabPol = Mid$(Messaggio, pos, 3)
                pos = pos + 3
                rs_Sat!sat = i - 1
                If IsNumeric(TabAz) Then rs_Sat!Azimuth = Format$(TabAz / 1000, "000.000")
                If IsNumeric(TabEl) Then rs_Sat!elev = Format$(TabEl / 1000, "00.000")
                If IsNumeric(TabPol) Then rs_Sat!POL = s_segn + Format$(TabPol / 10, "00.0")
                rs_Sat.Update
            Next i
            ' datTMath.Absat / tdbTMath.Absat not migrated
        End If
        rs_Sat.Close
        b_Parametri = False
    End Function


    Private Sub MnuMemory_Click(Index As Integer)
        Select Case Index
            Case 0
                i_Cont = 0
                b_Parametri = False
                Me.Close()
            Case 1
                DbExecute("Delete * from [Sat]")
                ' datTMath.Absat / tdbTMath.Absat not migrated
                b_Parametri = True
                b_Ricezione(ACU) = True
                b_int(ACU) = True
                i_Cont = 2
                Attesa(0.2)
            InvioACU(Chr(STX) & "07<39" & Chr(ETX))
                b_int(ACU) = True
                frmRicezione.TmrTimeoutMSG(ACU).Enabled = False
                frmRicezione.TmrTimeoutMSG(ACU).Interval = 5000
                frmRicezione.TmrTimeoutMSG(ACU).Enabled = True
            Case 2
                Stampa()
        End Select

    End Sub

    Private Function Stampa() As Boolean
        Dim s_Header As String
        Stampa = True
        s_Header = "Tabella Satelliti del  " & Format(Now, "dd\-mmm\-yyyy")
        ' tdbTMath.Absat.PrintInfo not migrated
    End Function


End Class


