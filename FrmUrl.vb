Public Class FrmUrl

    Private dtUrl As New DataTable()

    ' Interpreta il valore booleano del campo "Aggiorna" comunque sia stato
    ' salvato nel database (True/False, -1/0, 1/0).
    Private Function ParseAggiorna(v As Object) As Boolean
        If IsDBNull(v) Then Return False
        If TypeOf v Is Boolean Then Return CBool(v)
        Dim s As String = CStr(v).Trim()
        Return s.Equals("True", StringComparison.OrdinalIgnoreCase) OrElse s = "-1" OrElse s = "1"
    End Function

    Private Sub LoadData()
        Try
            Dim rs As Object = dbOrbite.OpenRecordset(
                "SELECT * FROM [URL] ORDER BY Aggiorna", dbOpenSnapshot)
            dtUrl = New DataTable()
            Do While Not CBool(rs.EOF)
                If dtUrl.Columns.Count = 0 Then
                    For i As Integer = 0 To rs.Fields.Count - 1
                        Dim colName As String = CStr(rs.Fields(i).Name)
                        Dim colType As Type = If(colName.Equals("Aggiorna", StringComparison.OrdinalIgnoreCase), GetType(Boolean), GetType(String))
                        dtUrl.Columns.Add(colName, colType)
                    Next
                End If
                Dim row As DataRow = dtUrl.NewRow()
                For i As Integer = 0 To rs.Fields.Count - 1
                    If dtUrl.Columns(i).DataType Is GetType(Boolean) Then
                        row(i) = ParseAggiorna(rs.Fields(i).Value)
                    Else
                        row(i) = If(IsDBNull(rs.Fields(i).Value), "", CStr(rs.Fields(i).Value))
                    End If
                Next
                dtUrl.Rows.Add(row)
                rs.MoveNext()
            Loop
            rs.Close()

            ' Le righe appena caricate sono "esistenti": da qui in poi RowState
            ' distingue le righe già salvate (Unchanged) da una riga nuova
            ' aggiunta dall'utente (Added), che va inserita nel database.
            dtUrl.AcceptChanges()

            dgvUrl.DataSource = dtUrl

            ' Solo la colonna "Aggiorna" è commutabile: tutte le altre restano di sola lettura
            ' (URL e FileAgg si sbloccano temporaneamente solo sulla nuova riga vuota, vedi
            ' DgvUrl_DefaultValuesNeeded).
            For Each col As DataGridViewColumn In dgvUrl.Columns
                col.ReadOnly = Not col.Name.Equals("Aggiorna", StringComparison.OrdinalIgnoreCase)
            Next

            ' Intestazioni in inglese (il nome colonna nel database, usato dal
            ' resto del codice, resta "Aggiorna"/"FileAgg").
            If dgvUrl.Columns.Contains("Aggiorna") Then dgvUrl.Columns("Aggiorna").HeaderText = "Update"
            If dgvUrl.Columns.Contains("FileAgg") Then dgvUrl.Columns("FileAgg").HeaderText = "Local File"
        Catch ex As Exception
            WriteError(0, "FrmUrl.LoadData: " & ex.Message)
        End Try
    End Sub

    ' La riga vuota in fondo alla griglia (per aggiungere una nuova voce) deve
    ' poter ricevere URL e FileAgg, anche se le altre righe restano protette.
    Private Sub DgvUrl_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles dgvUrl.DefaultValuesNeeded
        e.Row.Cells("URL").ReadOnly = False
        e.Row.Cells("FileAgg").ReadOnly = False
    End Sub

    ' Quando l'utente finisce di compilare una nuova riga e passa oltre,
    ' la inserisce nel database e la "blocca" come le righe già esistenti.
    Private Sub DgvUrl_RowLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUrl.RowLeave
        If e.RowIndex < 0 OrElse e.RowIndex >= dgvUrl.Rows.Count Then Exit Sub
        Dim gridRow As DataGridViewRow = dgvUrl.Rows(e.RowIndex)
        If gridRow.IsNewRow Then Exit Sub
        Dim drv As DataRowView = TryCast(gridRow.DataBoundItem, DataRowView)
        If drv Is Nothing OrElse drv.Row.RowState <> DataRowState.Added Then Exit Sub

        Dim urlVal As String = CStr(If(gridRow.Cells("URL").Value, ""))
        If urlVal.Trim() = "" Then Exit Sub ' riga vuota, niente da salvare

        Try
            Dim fileAggVal As String = CStr(If(gridRow.Cells("FileAgg").Value, ""))
            Dim aggiornaVal As Boolean = ParseAggiorna(gridRow.Cells("Aggiorna").Value)

            Dim rs As Object = dbOrbite.OpenRecordset("SELECT * FROM [URL]", dbOpenDynaset)
            rs.AddNew()
            rs.Fields("URL").Value = urlVal
            rs.Fields("FileAgg").Value = fileAggVal
            rs.Fields("Aggiorna").Value = aggiornaVal
            rs.Update()
            rs.Close()

            drv.Row.AcceptChanges()
            gridRow.Cells("URL").ReadOnly = True
            gridRow.Cells("FileAgg").ReadOnly = True
        Catch ex As Exception
            WriteError(0, "FrmUrl.DgvUrl_RowLeave: " & ex.Message)
        End Try
    End Sub

    ' Commuta subito lo stato al clic sulla casella (senza aspettare che il
    ' focus lasci la cella) e lo salva sul database.
    Private Sub DgvUrl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUrl.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        If Not dgvUrl.Columns(e.ColumnIndex).Name.Equals("Aggiorna", StringComparison.OrdinalIgnoreCase) Then Exit Sub
        dgvUrl.EndEdit()
    End Sub

    Private Sub DgvUrl_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUrl.CellValueChanged
        If e.RowIndex < 0 Then Exit Sub
        If Not dgvUrl.Columns(e.ColumnIndex).Name.Equals("Aggiorna", StringComparison.OrdinalIgnoreCase) Then Exit Sub
        Try
            Dim nuovoValore As Boolean = CBool(dgvUrl.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            Dim colChiave As String = dtUrl.Columns(0).ColumnName
            Dim valChiave As String = CStr(dgvUrl.Rows(e.RowIndex).Cells(0).Value)
            Dim rs As Object = dbOrbite.OpenRecordset(
                "SELECT * FROM [URL] WHERE [" & colChiave & "]='" & valChiave & "'", dbOpenDynaset)
            If Not CBool(rs.EOF) Then
                rs.Edit()
                rs.Fields("Aggiorna").Value = nuovoValore
                rs.Update()
            End If
            rs.Close()
        Catch ex As Exception
            WriteError(0, "FrmUrl.DgvUrl_CellValueChanged: " & ex.Message)
        End Try
    End Sub

    Private Sub FrmUrl_Load(sender As Object, e As EventArgs) Handles Me.Load
        s_Satellite = ""
        LoadData()
    End Sub

    Private Sub TsmiEsci_Click(sender As Object, e As EventArgs) Handles tsmiEsci.Click
        Esci()
    End Sub

    Private Sub TsmiElimina_Click(sender As Object, e As EventArgs) Handles tsmiElimina.Click
        Elimina()
    End Sub

    Private Sub Elimina()
        Try
            If dgvUrl.CurrentRow Is Nothing OrElse dtUrl.Columns.Count = 0 Then Exit Sub
            Dim colName As String = dtUrl.Columns(0).ColumnName
            Dim val As String = CStr(dgvUrl.CurrentRow.Cells(0).Value)
            Dim rs As Object = dbOrbite.OpenRecordset(
                "SELECT * FROM [URL] WHERE [" & colName & "]='" & val & "'", dbOpenDynaset)
            If Not CBool(rs.EOF) Then rs.Delete()
            rs.Close()
            LoadData()
        Catch ex As Exception
            WriteError(0, "FrmUrl.Elimina: " & ex.Message)
        End Try
    End Sub

    Private Sub Esci()
        FrmNorad.AggNorad()
        Me.Close()
    End Sub

End Class
