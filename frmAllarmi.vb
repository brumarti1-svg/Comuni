Imports System.Data.SQLite
Imports System.Data

Public Class frmAllarmi

    Private Sub frmAllarmi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblAntenna.Text = "Antenna: " & Antenna
        ImpostaFiltroDefault()
        CaricaAllarmi()
    End Sub

    Private Sub frmAllarmi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        LblAntenna.Text = "Antenna: " & Antenna
        ImpostaFiltroDefault()
        CaricaAllarmi()
    End Sub

    Private Sub ImpostaFiltroDefault()
        Dim now As DateTime = NowUTC()
        mskDataFin.Text = now.ToString("dd/MM/yyyy")
        mskOraFin.Text  = now.ToString("HH:mm")
        mskDataIni.Text = now.AddDays(-1).ToString("dd/MM/yyyy")
        mskOraIni.Text  = now.ToString("HH:mm")
    End Sub

    Private Sub CaricaAllarmi()
        Try
            Dim dataIni As DateTime = ParseFiltro(mskDataIni.Text, mskOraIni.Text)
            Dim dataFin As DateTime = ParseFiltro(mskDataFin.Text, mskOraFin.Text)
            Dim sql As String = "SELECT MSG_Date AS Data, Error AS Messaggio, Level AS Livello " &
                                "FROM Error_Messages " &
                                "WHERE MSG_Date >= @ini AND MSG_Date <= @fin " &
                                "ORDER BY MSG_Date DESC"
            Dim conn As SQLiteConnection = CType(dbOrbite, DAODatabase).Connection
            Dim dt As New DataTable()
            Using Cmd As New SQLiteCommand(sql, conn)
                Cmd.Parameters.AddWithValue("@ini", dataIni.ToString("yyyy-MM-dd HH:mm:ss"))
                Cmd.Parameters.AddWithValue("@fin", dataFin.ToString("yyyy-MM-dd HH:mm:ss"))
                Using adapter As New SQLiteDataAdapter(Cmd)
                    adapter.Fill(dt)
                End Using
            End Using
            dgvAllarmi.DataSource = dt
        Catch ex As Exception
            ' nessun dato disponibile
        End Try
    End Sub

    Private Function ParseFiltro(data As String, ora As String) As DateTime
        Try
            Return DateTime.ParseExact(data.Trim() & " " & ora.Trim(), "dd/MM/yyyy HH:mm",
                                       System.Globalization.CultureInfo.InvariantCulture)
        Catch
            Return NowUTC()
        End Try
    End Function

    Private Sub CmdRicerca_Click(sender As Object, e As EventArgs) Handles CmdRicerca.Click
        CaricaAllarmi()
    End Sub

    Private Sub CmdReset_Click(sender As Object, e As EventArgs) Handles CmdReset.Click
        If MessageBox.Show("Cancellare tutti gli allarmi?", "Conferma",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            DbExecute("DELETE FROM [Error_Messages]")
            DbExecute("DELETE FROM [tmp_Error_Messages]")
            CaricaAllarmi()
        End If
    End Sub

    Private Sub CmdAnnulla_Click(sender As Object, e As EventArgs) Handles CmdAnnulla.Click
        Me.Close()
    End Sub

    Private Sub mskDataIni_KeyUp(sender As Object, e As KeyEventArgs) Handles mskDataIni.KeyUp
        If e.KeyCode = Keys.Return Then CaricaAllarmi()
    End Sub

    Private Sub mskDataFin_KeyUp(sender As Object, e As KeyEventArgs) Handles mskDataFin.KeyUp
        If e.KeyCode = Keys.Return Then CaricaAllarmi()
    End Sub

End Class
