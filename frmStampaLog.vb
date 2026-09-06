Public Class FrmStampaLog

    Private dtLog As New DataTable()

    Private Function ParseFormDate(dateTxt As String, timeTxt As String) As Date
        Dim d As Date = CDate(dateTxt)
        Dim t As String = timeTxt.Replace(".", ":")
        Dim parts() As String = t.Split(":"c)
        Dim h As Integer = If(parts.Length > 0 AndAlso IsNumeric(parts(0)), CInt(parts(0)), 0)
        Dim m As Integer = If(parts.Length > 1 AndAlso IsNumeric(parts(1)), CInt(parts(1)), 0)
        Return New Date(d.Year, d.Month, d.Day, h, m, 0)
    End Function

    Private Function ControllaDati() As Boolean
        If Not IsDate(TxtDataIni.Text) Then
            TxtDataIni.Focus()
            Return False
        End If
        If Not IsDate(TxtDataFin.Text) Then
            TxtDataFin.Focus()
            Return False
        End If
        Try
            ParseFormDate(TxtDataIni.Text, TxtOraIni.Text)
        Catch
            TxtOraIni.Focus()
            Return False
        End Try
        Try
            ParseFormDate(TxtDataFin.Text, TxtOraFin.Text)
        Catch
            TxtOraFin.Focus()
            Return False
        End Try
        If ParseFormDate(TxtDataIni.Text, TxtOraIni.Text) > ParseFormDate(TxtDataFin.Text, TxtOraFin.Text) Then
            TxtDataIni.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub SelezionaDati()
        Dim ini As Date = ParseFormDate(TxtDataIni.Text, TxtOraIni.Text)
        Dim fin As Date = ParseFormDate(TxtDataFin.Text, TxtOraFin.Text)
        Dim iniStr As String = ini.ToString("MM/dd/yyyy HH:mm")
        Dim finStr As String = fin.ToString("MM/dd/yyyy HH:mm")

        Dim levelClause As String = ""
        If rad0.Checked Then
            levelClause = " AND Level = 0"
        ElseIf rad1.Checked Then
            levelClause = " AND Level = 1"
        End If

        Dim tmpSql As String =
            "SELECT * FROM Error_messages WHERE MSG_date >= #" & iniStr &
            "# AND MSG_date <= #" & finStr & "#" & levelClause & " ORDER BY MSG_date DESC"

        Try
            Dim rs As Object = dbOrbite.OpenRecordset(tmpSql, dbOpenSnapshot)
            dtLog = New DataTable()
            Do While Not CBool(rs.EOF)
                If dtLog.Columns.Count = 0 Then
                    For i As Integer = 0 To rs.Fields.Count - 1
                        dtLog.Columns.Add(CStr(rs.Fields(i).Name), GetType(String))
                    Next
                End If
                Dim row As DataRow = dtLog.NewRow()
                For i As Integer = 0 To rs.Fields.Count - 1
                    row(i) = If(IsDBNull(rs.Fields(i).Value), "", CStr(rs.Fields(i).Value))
                Next
                dtLog.Rows.Add(row)
                rs.MoveNext()
            Loop
            rs.Close()
            dgvLog.DataSource = dtLog
        Catch ex As Exception
            MessageBox.Show("Query error: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FrmStampaLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        TxtDataIni.Text = Now.ToString("dd/MM/yy")
        TxtOraIni.Text = "00:00"
        TxtDataFin.Text = Now.ToString("dd/MM/yy")
        TxtOraFin.Text = Now.ToString("HH:mm")
        radAll.Checked = True
    End Sub

    Private Sub CmdRicerca_Click(sender As Object, e As EventArgs) Handles CmdRicerca.Click
        If Not ControllaDati() Then Exit Sub
        Cursor.Current = Cursors.WaitCursor
        Try
            DbExecute("DELETE FROM tmp_error_messages")
            SelezionaDati()
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CmdEsci_Click(sender As Object, e As EventArgs) Handles CmdEsci.Click
        Me.Close()
    End Sub

    Private Sub FrmStampaLog_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub

End Class

