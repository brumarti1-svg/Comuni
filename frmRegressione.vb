Public Class FrmRegressione

    Private Sub CmdExit_Click(sender As Object, e As EventArgs) Handles CmdExit.Click
        ReDim pts(0)
        Me.Close()
    End Sub

    ' Mostra il grado correntemente impostato (0 = mai scelto prima, si
    ' riparte dal default "Degree 2" già spuntato nel Designer).
    Private Sub FrmRegressione_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case grado
            Case 1 : opt1.Checked = True
            Case 3 : opt3.Checked = True
            Case 4 : opt4.Checked = True
            Case Else : opt2.Checked = True
        End Select
    End Sub

    ' opt1-4: i gestori mancavano del tutto ("radio buttons not migrated"),
    ' quindi scegliere un grado non aveva alcun effetto su "grado" — la
    ' maschera principale (CmdProi_Click) continuava a usare il valore
    ' precedente qualunque fosse l'opzione cliccata.
    Private Sub Opt1_CheckedChanged(sender As Object, e As EventArgs) Handles opt1.CheckedChanged
        If opt1.Checked Then grado = 1
    End Sub

    Private Sub Opt2_CheckedChanged(sender As Object, e As EventArgs) Handles opt2.CheckedChanged
        If opt2.Checked Then grado = 2
    End Sub

    Private Sub Opt3_CheckedChanged(sender As Object, e As EventArgs) Handles opt3.CheckedChanged
        If opt3.Checked Then grado = 3
    End Sub

    Private Sub Opt4_CheckedChanged(sender As Object, e As EventArgs) Handles opt4.CheckedChanged
        If opt4.Checked Then grado = 4
    End Sub

End Class
