Public Class FrmUrl

    Private dtUrl As New DataTable()

    Private Sub LoadData()
        Try
            Dim rs As Object = dbOrbite.OpenRecordset(
                "SELECT * FROM [URL] ORDER BY Aggiorna", dbOpenSnapshot)
            dtUrl = New DataTable()
            Do While Not CBool(rs.EOF)
                If dtUrl.Columns.Count = 0 Then
                    For i As Integer = 0 To rs.Fields.Count - 1
                        dtUrl.Columns.Add(CStr(rs.Fields(i).Name), GetType(String))
                    Next
                End If
                Dim row As DataRow = dtUrl.NewRow()
                For i As Integer = 0 To rs.Fields.Count - 1
                    row(i) = If(IsDBNull(rs.Fields(i).Value), "", CStr(rs.Fields(i).Value))
                Next
                dtUrl.Rows.Add(row)
                rs.MoveNext()
            Loop
            rs.Close()
            dgvUrl.DataSource = dtUrl
        Catch ex As Exception
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
        End Try
    End Sub

    Private Sub Esci()
        FrmNorad.AggNorad()
        Me.Close()
    End Sub

End Class
