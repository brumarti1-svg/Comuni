Public Class FrmPhase
    ' Stubs VB6 non migrati
    Friend datPhase    As Object
    Friend datSatelliti As Object
    Friend tdbPhase    As Object
    Friend MnuMain()   As System.Windows.Forms.ToolStripMenuItem

    Public Response As Integer

    Dim tmpSQL As String

    ' -------------------------------------------------------
    ' Form Load — carica Fasi nel DataGridView
    ' -------------------------------------------------------
    Private Sub FrmPhase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        Response = 0
        LoadFasi()
    End Sub

    Private Sub LoadFasi()
        On Error Resume Next
        dgvPhase.Columns.Clear()
        dgvPhase.Rows.Clear()

        Dim sql As String
        If b_Scelta Then
            sql = "SELECT * FROM [Fasi] ORDER BY Sat, frequenza, faseEl"
        Else
            sql = "SELECT * FROM [Fasi] WHERE Sat='" & s_Satellite & "' ORDER BY frequenza, faseEl"
        End If

        Dim rs As Object = dbOrbite.OpenRecordset(sql, dbOpenDynaset)
        If rs.EOF Then
            rs.Close()
            Exit Sub
        End If

        ' Aggiunge colonne leggendo i nomi dal recordset
        Dim fieldCount As Integer = rs.Fields.Count
        For i As Integer = 0 To fieldCount - 1
            dgvPhase.Columns.Add(rs.Fields(i).Name, rs.Fields(i).Name)
        Next
        ' Larghezze colonne chiave
        For Each col As DataGridViewColumn In dgvPhase.Columns
            Select Case col.Name.ToLower()
                Case "sat"        : col.Width = 130
                Case "frequenza"  : col.Width = 80
                Case "faseel"     : col.Width = 60
                Case "faseaz"     : col.Width = 60
                Case "pol"        : col.Width = 60
                Case "lna"        : col.Width = 40
                Case "ultima"     : col.Width = 45
                Case Else         : col.Width = 60
            End Select
        Next

        Do While Not rs.EOF
            Dim rowVals(fieldCount - 1) As Object
            For i As Integer = 0 To fieldCount - 1
                rowVals(i) = If(IsDBNull(rs.Fields(i).Value), "", rs.Fields(i).Value)
            Next
            dgvPhase.Rows.Add(rowVals)
            rs.MoveNext
        Loop
        rs.Close()
    End Sub

    ' -------------------------------------------------------
    ' Menu Apply (era MnuMain(1)) — legge riga selezionata
    ' -------------------------------------------------------
    Private Sub MnuApply_Click(sender As Object, e As EventArgs) Handles MnuApply.Click
        On Error Resume Next
        Response = 1
        If dgvPhase.CurrentRow Is Nothing Then
            Response = 0
            Me.Close()
            Exit Sub
        End If
        Dim row As DataGridViewRow = dgvPhase.CurrentRow
        g_Sat      = CellVal(row, "sat")
        Dim frq As String = CellVal(row, "frequenza")
        If IsNumeric(frq) Then
            g_Frequenza = Format$(CDbl(frq), "#0000.00#")
        Else
            g_Frequenza = frq
        End If
        g_PhaseEl  = CellVal(row, "faseel")
        g_PhaseAz  = CellVal(row, "faseaz")
        g_CirLin   = CellVal(row, "cirlin")
        g_Polar    = CellVal(row, "pol")
        g_PolErr   = CellVal(row, "polerr")
        g_ValPol   = CellVal(row, "valpol")
        g_LNA      = CellVal(row, "lna")
        g_RevAz    = CellVal(row, "revazerr")
        Me.Close()
    End Sub

    Private Function CellVal(row As DataGridViewRow, colName As String) As String
        For Each col As DataGridViewColumn In dgvPhase.Columns
            If String.Compare(col.Name, colName, StringComparison.OrdinalIgnoreCase) = 0 Then
                Dim v As Object = row.Cells(col.Index).Value
                Return If(v Is Nothing OrElse IsDBNull(v), "", CStr(v))
            End If
        Next
        Return ""
    End Function

    ' -------------------------------------------------------
    ' Menu Exit (era MnuMain(0)) — esce senza scegliere: l'antenna non viene abilitata
    ' -------------------------------------------------------
    Private Sub MnuExit_Click(sender As Object, e As EventArgs) Handles MnuExit.Click
        Response = 0
        Me.Close()
    End Sub

    ' -------------------------------------------------------
    ' Menu Receiver (era MnuMain(2)) — apre FrmRicevitore per mettere a punto le fasi
    ' -------------------------------------------------------
    Private Sub MnuReceiver_Click(sender As Object, e As EventArgs) Handles MnuReceiver.Click
        Dim fRic As New FrmRicevitore()
        fRic.ShowDialog()
        LoadFasi()
    End Sub

    ' -------------------------------------------------------
    ' Menu Remove (era MnuMain(3)) — cancella la riga selezionata
    ' -------------------------------------------------------
    Private Sub MnuRemove_Click(sender As Object, e As EventArgs) Handles MnuRemove.Click
        On Error Resume Next
        If dgvPhase.CurrentRow Is Nothing Then Exit Sub
        Dim sat As String  = CellVal(dgvPhase.CurrentRow, "sat")
        Dim frq As String  = CellVal(dgvPhase.CurrentRow, "frequenza")
        Dim pol As String  = CellVal(dgvPhase.CurrentRow, "pol")
        Dim lna As String  = CellVal(dgvPhase.CurrentRow, "lna")
        If sat = "" Then Exit Sub
        DbExecute("DELETE FROM [Fasi] WHERE Sat='" & sat & "' AND frequenza='" & frq & "' AND Pol='" & pol & "' AND Lna='" & lna & "'")
        LoadFasi()
    End Sub

    ' -------------------------------------------------------
    ' Menu Print (era MnuMain(4)) — stampa la tabella
    ' -------------------------------------------------------
    Private Sub MnuPrint_Click(sender As Object, e As EventArgs) Handles MnuPrint.Click
        Stampa()
    End Sub

    Private Sub Stampa()
        On Error Resume Next
        MessageBox.Show("Print not available.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Elimina()
        MnuRemove_Click(Nothing, EventArgs.Empty)
    End Sub

    Private Sub TdbPhase_HeadClick(ByVal ColIndex As Integer)
        ' non più applicabile con DataGridView
    End Sub

End Class
