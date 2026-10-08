Imports System.Data.SQLite

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
    ' -------------------------------------------------------
    ' Incolla multi-cella (Ctrl+V) — DataGridView non lo gestisce da solo:
    ' senza questo, il testo copiato (con Tab tra le colonne e a-capo tra le
    ' righe) finisce interamente nella sola cella corrente invece di
    ' distribuirsi sulle colonne/righe di destinazione.
    ' -------------------------------------------------------
    Private Sub DgvPhase_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvPhase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            IncollaDaClipboard()
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub IncollaDaClipboard()
        If Not Clipboard.ContainsText() Then Exit Sub
        If dgvPhase.CurrentCell Is Nothing Then Exit Sub

        Dim testo As String = Clipboard.GetText()
        Dim righe() As String = testo.Replace(vbCrLf, vbLf).TrimEnd(ControlChars.Lf).Split(ControlChars.Lf)
        If righe.Length = 0 Then Exit Sub

        Dim startRow As Integer = dgvPhase.CurrentCell.RowIndex
        Dim startCol As Integer = dgvPhase.CurrentCell.ColumnIndex

        For r As Integer = 0 To righe.Length - 1
            Dim targetRow As Integer = startRow + r
            ' Aggiunge righe se servono oltre l'ultima riga vuota di inserimento.
            While targetRow >= dgvPhase.Rows.Count - 1 AndAlso dgvPhase.AllowUserToAddRows
                dgvPhase.Rows.Add()
            End While
            If targetRow >= dgvPhase.Rows.Count OrElse dgvPhase.Rows(targetRow).IsNewRow Then Continue For

            Dim celle() As String = righe(r).Split(ControlChars.Tab)
            For c As Integer = 0 To celle.Length - 1
                Dim targetCol As Integer = startCol + c
                If targetCol >= dgvPhase.Columns.Count Then Continue For
                dgvPhase.Rows(targetRow).Cells(targetCol).Value = celle(c)
            Next
        Next
    End Sub

    Private Sub FrmPhase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        Response = 0
        LoadFasi()
    End Sub

    ' -------------------------------------------------------
    ' Form Closing — salva le modifiche fatte nella griglia (celle
    ' corrette, righe nuove) prima di chiudere, qualunque sia il
    ' pulsante che ha innescato la chiusura (Exit o Apply).
    ' Le cancellazioni fatte con "Remove" sono già immediate e non
    ' rientrano in questo salvataggio.
    ' -------------------------------------------------------
    Private Sub FrmPhase_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SalvaFasi()
    End Sub

    Private Sub SalvaFasi()
        Try
            Dim conn As SQLiteConnection = CType(dbOrbite, DAODatabase).Connection
            Dim colNames = (From c As DataGridViewColumn In dgvPhase.Columns Select c.Name).ToArray()
            If colNames.Length = 0 Then Exit Sub

            Using tx As SQLiteTransaction = conn.BeginTransaction()
                ' Ambito coerente con LoadFasi(): se la finestra è filtrata su un
                ' satellite (b_Scelta = False) si toccano solo le sue fasi, altrimenti
                ' l'intera tabella (come già fa LoadFasi con "WHERE Sat=..." / tutte).
                Dim delSql As String = If(b_Scelta,
                    "DELETE FROM [Fasi]",
                    "DELETE FROM [Fasi] WHERE Sat=@sat")
                Using CmdDel As New SQLiteCommand(delSql, conn)
                    CmdDel.Transaction = tx
                    If Not b_Scelta Then CmdDel.Parameters.AddWithValue("@sat", s_Satellite)
                    CmdDel.ExecuteNonQuery()
                End Using

                Dim placeholders = String.Join(",", colNames.Select(Function(n) "@" & n))
                Dim insSql As String = "INSERT INTO [Fasi] (" & String.Join(",", colNames) & ") VALUES (" & placeholders & ")"
                Using CmdIns As New SQLiteCommand(insSql, conn)
                    CmdIns.Transaction = tx
                    For Each row As DataGridViewRow In dgvPhase.Rows
                        If row.IsNewRow Then Continue For
                        ' Riga senza satellite indicato: la si ignora invece di
                        ' salvarla vuota (evita righe "fantasma" da un Enter di troppo
                        ' sulla riga vuota di inserimento).
                        If CellVal(row, "sat") = "" Then Continue For
                        CmdIns.Parameters.Clear()
                        For Each colName In colNames
                            Dim v As Object = row.Cells(colName).Value
                            Dim isEmpty = v Is Nothing OrElse IsDBNull(v) OrElse v.ToString() = ""
                            CmdIns.Parameters.AddWithValue("@" & colName, If(isEmpty, CType(DBNull.Value, Object), v))
                        Next
                        CmdIns.ExecuteNonQuery()
                    Next
                End Using
                tx.Commit()
            End Using
        Catch ex As Exception
            WriteError(0, "FrmPhase.SalvaFasi: " & ex.Message)
        End Try
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
            ' Una tabella Fasi vuota (o senza righe per questo satellite) è
            ' una condizione normale, non un errore: la griglia resta
            ' semplicemente vuota. Il filtro per satellite in
            ' frmRicezione.CmdLoad_Click evita comunque di aprire questa
            ' maschera quando non c'è nulla da scegliere.
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
            ' Colonne numeriche: valori allineati a destra (Fasi: Frequenza REAL,
            ' le altre INTEGER; il resto è testo e resta allineato a sinistra).
            Select Case col.Name.ToLower()
                Case "frequenza", "faseel", "faseaz", "polangle", "lna", "offsetaz", "offsetel"
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
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
