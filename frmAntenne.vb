Imports System.Data.SQLite

Partial Public Class FrmAntenne
    Inherits System.Windows.Forms.Form

    Private _dt As System.Data.DataTable

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmAntenne_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MigraSchemaAntenne()
        CaricaAntenne()
    End Sub

    Private Sub MigraSchemaAntenne()
        Try
            Dim conn As SQLiteConnection = CType(dbOrbite, DAODatabase).Connection
            Using Cmd As New SQLiteCommand("", conn)
                Cmd.CommandText = "SELECT type FROM pragma_table_info('Antenne') WHERE name='Latitudine'"
                Dim colType = Cmd.ExecuteScalar()?.ToString().ToUpper()
                If colType = "REAL" Then Return  ' già migrata
                Using tx As SQLiteTransaction = conn.BeginTransaction()
                    Cmd.Transaction = tx
                    Cmd.CommandText =
                        "CREATE TABLE Antenne_new (" &
                        "  Indice INTEGER, Antenna TEXT, " &
                        "  Latitudine REAL, Longitudine REAL, Altezza REAL, " &
                        "  AZoffset REAL, ELoffset REAL)"
                    Cmd.ExecuteNonQuery()
                    Cmd.CommandText = "INSERT INTO Antenne_new SELECT Indice, Antenna, CAST(Latitudine AS REAL), CAST(Longitudine AS REAL), CAST(Altezza AS REAL), CAST(AZoffset AS REAL), CAST(ELoffset AS REAL) FROM Antenne"
                    Cmd.ExecuteNonQuery()
                    Cmd.CommandText = "DROP TABLE Antenne"
                    Cmd.ExecuteNonQuery()
                    Cmd.CommandText = "ALTER TABLE Antenne_new RENAME TO Antenne"
                    Cmd.ExecuteNonQuery()
                    tx.Commit()
                End Using
            End Using
        Catch ex As Exception
            WriteError(0, "MigraSchemaAntenne: " & ex.Message)
        End Try
    End Sub

    Private Sub CaricaAntenne()
        _dt = New System.Data.DataTable()
        _dt.Columns.Add("indice", GetType(Integer))
        _dt.Columns.Add("Antenna", GetType(String))
        _dt.Columns.Add("latitudine", GetType(Double))
        _dt.Columns.Add("Longitudine", GetType(Double))
        _dt.Columns.Add("Altezza", GetType(Double))
        _dt.Columns.Add("AzOffset", GetType(Double))
        _dt.Columns.Add("ElOffset", GetType(Double))
        Try
            Dim conn As SQLiteConnection = CType(dbOrbite, DAODatabase).Connection
            Using Cmd As New SQLiteCommand("SELECT Indice, Antenna, Latitudine, Longitudine, Altezza, AZoffset, ELoffset FROM Antenne ORDER BY Indice", conn)
                Using dr = Cmd.ExecuteReader()
                    Do While dr.Read()
                        _dt.Rows.Add(
                            If(IsDBNull(dr(0)), 0, CInt(dr(0))),
                            If(IsDBNull(dr(1)), "", dr(1).ToString()),
                            If(IsDBNull(dr(2)), 0.0, CDbl(dr(2))),
                            If(IsDBNull(dr(3)), 0.0, CDbl(dr(3))),
                            If(IsDBNull(dr(4)), 0.0, CDbl(dr(4))),
                            If(IsDBNull(dr(5)), 0.0, CDbl(dr(5))),
                            If(IsDBNull(dr(6)), 0.0, CDbl(dr(6))))
                    Loop
                End Using
            End Using
        Catch ex As Exception
            WriteError(0, "FrmAntenne.CaricaAntenne: " & ex.Message)
        End Try
        dgvAntenne.AutoGenerateColumns = False
        dgvAntenne.Columns("Indice").DataPropertyName = "indice"
        dgvAntenne.Columns("Antenna").DataPropertyName = "Antenna"
        dgvAntenne.Columns("Latitudine").DataPropertyName = "latitudine"
        dgvAntenne.Columns("Longitudine").DataPropertyName = "Longitudine"
        dgvAntenne.Columns("Altezza").DataPropertyName = "Altezza"
        dgvAntenne.Columns("AZoffset").DataPropertyName = "AzOffset"
        dgvAntenne.Columns("ELoffset").DataPropertyName = "ElOffset"
        dgvAntenne.DataSource = _dt
    End Sub

    Private Sub SalvaAntenne()
        Try
            Dim conn As SQLiteConnection = CType(dbOrbite, DAODatabase).Connection
            Using tx As SQLiteTransaction = conn.BeginTransaction()
                Using CmdDel As New SQLiteCommand("DELETE FROM Antenne", conn)
                    CmdDel.Transaction = tx
                    CmdDel.ExecuteNonQuery()
                End Using
                Using CmdIns As New SQLiteCommand(
                    "INSERT INTO Antenne (Indice, Antenna, Latitudine, Longitudine, Altezza, AZoffset, ELoffset) VALUES (@i,@a,@la,@lo,@alt,@az,@el)", conn)
                    CmdIns.Transaction = tx
                    For Each row As System.Data.DataRow In _dt.Rows
                        CmdIns.Parameters.Clear()
                        CmdIns.Parameters.AddWithValue("@i", row("indice"))
                        CmdIns.Parameters.AddWithValue("@a", row("Antenna"))
                        CmdIns.Parameters.AddWithValue("@la", row("latitudine"))
                        CmdIns.Parameters.AddWithValue("@lo", row("Longitudine"))
                        CmdIns.Parameters.AddWithValue("@alt", row("Altezza"))
                        CmdIns.Parameters.AddWithValue("@az", row("AzOffset"))
                        CmdIns.Parameters.AddWithValue("@el", row("ElOffset"))
                        CmdIns.ExecuteNonQuery()
                    Next
                End Using
                tx.Commit()
            End Using
        Catch ex As Exception
            WriteError(0, "FrmAntenne.SalvaAntenne: " & ex.Message)
        End Try
    End Sub

    Private Sub MnuSeleziona_Click(sender As Object, e As EventArgs) Handles MnuSeleziona.Click
        dgvAntenne.EndEdit()
        SalvaAntenne()
        PredisponiAcquisizione()
        Me.Close()
    End Sub

    Private Sub MnuElimina_Click(sender As Object, e As EventArgs) Handles MnuElimina.Click
        If dgvAntenne.CurrentRow IsNot Nothing AndAlso Not dgvAntenne.CurrentRow.IsNewRow Then
            dgvAntenne.Rows.RemoveAt(dgvAntenne.CurrentRow.Index)
        End If
    End Sub

    Private Sub MnuConvDMS_Click(sender As Object, e As EventArgs) Handles MnuConvDMS.Click
        g_Conv = 0
        FrmConvAngoli.ShowDialog()
    End Sub

    Private Sub MnuConvDD_Click(sender As Object, e As EventArgs) Handles MnuConvDD.Click
        g_Conv = 1
        FrmConvAngoli.ShowDialog()
    End Sub

End Class
