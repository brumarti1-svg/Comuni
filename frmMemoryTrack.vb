Public Class FrmMemoryTrack

    Friend datMemoryTrack As Object
    Friend tdbMemoryTrack As Object

    Dim tmpSql As String
    Dim b_inter As Boolean
    Dim Campo As String
    Dim pos As Integer
    Dim i_Memory As System.Single


    Private Sub frmMemoryTrack_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' datMemoryTrack.DatabaseName = g_Database   ' VB6 Data control — not available in VB.NET
        ' datMemoryTrack.RecordSource = "SELECT * FROM [MemoryTrack] ORDER BY Ora"
        Me.Refresh()
        ' tdbMemoryTrack.ReBind  ' VB6 DBGrid ReBind — not available in VB.NET
        Me.Refresh()
    End Sub

    Public Function ValoreMemory(Messaggio As String) As Boolean
        Dim rs_ValMem As Object
        Dim t_Memory As Date, TabAz As String, TabEl As String, Port As Integer
        On Error Resume Next
        ValoreMemory = True
        frmRicezione.TmrTimeoutMSG(ACU).Interval = Port_Control(ACU).Time_out * 1000
        i_Cont = 0
        Campo = Mid$(Messaggio, 6, 1)
        i = 1
    rs_ValMem = dbOrbite.OpenRecordset("MemoryTrack", dbOpenDynaset)
    If Not rs_ValMem.BOF Then
            rs_ValMem.MoveFirst
        End If
        ' leggo i campi solo per lo status
        '07/06/2016
        pos = 2
        t_Memory = Format$(CDate("0000"), "hh:mm")
        For i = 1 To 96
            rs_ValMem.AddNew
            pos += 5
            TabAz = Mid$(Messaggio, pos, 6)
            pos += 6
            TabEl = Mid$(Messaggio, pos, 5)
            rs_ValMem!ora = Format$(CDate(t_Memory), "hh:mm")
            If IsNumeric(TabAz) Then rs_ValMem!az = Format$(TabAz / 1000, "000.000")
            If IsNumeric(TabEl) Then rs_ValMem!el = Format$(TabEl / 1000, "00.000")
            t_Memory = DateAdd("n", 15, t_Memory)
            rs_ValMem.Update
        Next i
        rs_ValMem.Close
        Me.Refresh()
        ' tdbMemoryTrack.ReBind  ' VB6 DBGrid ReBind — not available in VB.NET
        Me.Refresh()
        b_Parametri = False
    End Function


    Private Sub MnuMemory_Click(Index As Integer)
        Select Case Index
            Case 0
                i_Cont = 0
                b_Parametri = False
                Me.Close()
'            frmRicezione.Show (vbModal)
            Case 1
                i_Cont = 2
                b_int(ACU) = True
                b_Ricezione(ACU) = True
                DbExecute("Delete * from [MemoryTrack]")
                Me.Refresh()
                ' tdbMemoryTrack.ReBind  ' VB6 DBGrid ReBind — not available in VB.NET
                Me.Refresh()
                frmRicezione.TmrTimeoutMSG(ACU).Enabled = False
                If Port_Control(ACU).Time_out < 0.5 Then
                    frmRicezione.TmrTimeoutMSG(ACU).Interval = 0.5 * 1000
                    Port_Control(ACU).Time_out = LeggiFileIni("Canale1", "TimeOut")
                End If
                b_Parametri = True
                Attesa(0.2)               'evita di interrompere il buffer di stato
                InvioACU(Chr(STX) & "07>3B" & Chr(ETX))                         'chiedi la tabella memory alla ACU
                frmRicezione.TmrTimeoutMSG(ACU).Enabled = True
            Case 2
                Stampa()
        End Select
    End Sub

    Private Function Stampa() As Boolean
        Dim s_Header As String
        Stampa = True
        s_Header = "Memory Track del  " & Format(Now, "dd\-mmm\-yyyy")
        On Error Resume Next
        ' tdbMemoryTrack.PrintInfo.PageHeader = s_Header  ' VB6 DBGrid PrintInfo — not available in VB.NET
        ' tdbMemoryTrack.PrintInfo.PrintData
        ' datMemoryTrack.RecordSource = "SELECT * FROM [MemoryTrack] ORDER BY Ora"  ' VB6 Data control — not available in VB.NET
        Me.Refresh()
    End Function

End Class

