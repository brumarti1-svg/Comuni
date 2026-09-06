Public Class FrmTrackTable

    Private dtTrack As New DataTable()

    Private Sub LoadData()
        Try
            Dim rs As Object = dbOrbite.OpenRecordset(
                "SELECT * FROM [TrackTable] ORDER BY Day asc, Ora asc", dbOpenSnapshot)
            dtTrack = New DataTable()
            Do While Not CBool(rs.EOF)
                If dtTrack.Columns.Count = 0 Then
                    For i As Integer = 0 To rs.Fields.Count - 1
                        dtTrack.Columns.Add(CStr(rs.Fields(i).Name), GetType(String))
                    Next
                End If
                Dim row As DataRow = dtTrack.NewRow()
                For i As Integer = 0 To rs.Fields.Count - 1
                    row(i) = If(IsDBNull(rs.Fields(i).Value), "", CStr(rs.Fields(i).Value))
                Next
                dtTrack.Rows.Add(row)
                rs.MoveNext()
            Loop
            rs.Close()
            dgvTrackTable.DataSource = dtTrack
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmTrackTable_Load(sender As Object, e As EventArgs) Handles Me.Load
        LblSatellite.Text = Satellite
        LoadData()
    End Sub

    Private Sub TsmiEsci_Click(sender As Object, e As EventArgs) Handles tsmiEsci.Click
        i_Cont = 0
        b_Parametri = False
        Me.Close()
    End Sub

    Private Sub TsmiStampa_Click(sender As Object, e As EventArgs) Handles tsmiStampa.Click
        Stampa()
    End Sub

    Private Sub Stampa()
        Dim s_Header As String = "Track Table del  " & Format(Now, "dd-MMM-yyyy")
        Try
            Using pd As New System.Drawing.Printing.PrintDocument()
                pd.DocumentName = s_Header
                AddHandler pd.PrintPage, Sub(s, ev)
                    Dim y As Single = ev.MarginBounds.Top
                    Dim fnt As New Drawing.Font("Arial", 10)
                    ev.Graphics.DrawString(New String(" "c, 20) & s_Header,
                                           New Drawing.Font("Arial", 12, Drawing.FontStyle.Bold),
                                           Drawing.Brushes.Black, ev.MarginBounds.Left, y)
                    y += 20
                    For Each row As DataRow In dtTrack.Rows
                        Dim line As String = ""
                        For Each col As DataColumn In dtTrack.Columns
                            line &= CStr(row(col)) & "  "
                        Next
                        ev.Graphics.DrawString(line, fnt, Drawing.Brushes.Black,
                                               ev.MarginBounds.Left, y)
                        y += 16
                        If y > ev.MarginBounds.Bottom Then Exit For
                    Next
                End Sub
                pd.Print()
            End Using
        Catch ex As Exception
            MessageBox.Show("Printer error", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
