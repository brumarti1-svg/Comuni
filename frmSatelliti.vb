Option Explicit On

Public Class FrmSatelliti
    ' CmbSatelliti, TxtAlgorithm, TxtParametri, TxtPolAngle ora dichiarati
    ' nel Designer (frmSatelliti.Designer.vb), dove sono gia' istanziati.

    ' Stubs VB6 non migrati (mantenuti per compatibilità riferimenti)
    Friend datSatelliti As Object
    Friend tdbSatelliti As Object
    Friend ToolBar1 As Object
    Friend frmNewSatellite As Object

    Dim X1 As System.Single, Y1 As System.Single
    Dim tmpSql As String
    Dim abuf(2) As String
    Dim hFile As Integer
    Dim Gr_file As Double
    Dim i_Scelta As Integer, b_Check As Boolean, i_Scelta1 As Integer
    Dim rs_Satel As Object
    Dim sReadBuffer As String, tmp_EpochTime As Double

    ' -------------------------------------------------------
    ' Form Load — carica dati nel DataGridView
    ' -------------------------------------------------------
    Private Sub FrmSatelliti_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        s_Satellite = ""
        LoadSatelliti()
    End Sub

    ' Ricarica il DataGridView dalla tabella Satelliti
    Private Sub LoadSatelliti()
        Try
            dgvSatelliti.Columns.Clear()
            dgvSatelliti.Rows.Clear()
            dgvSatelliti.Columns.Add("NomeSatellite", "Satellite Name")
            dgvSatelliti.Columns.Add("Algoritmo", "Algorithm")
            dgvSatelliti.Columns.Add("FileAgg", "File Orbit Parameter")
            dgvSatelliti.Columns.Add("PolAngle", "PolAngle")
            Dim colVerso As New DataGridViewCheckBoxColumn() With {
                .Name = "Verso", .HeaderText = "Direction",
                .TrueValue = True, .FalseValue = False}
            dgvSatelliti.Columns.Add(colVerso)
            dgvSatelliti.Columns(0).Width = 200
            dgvSatelliti.Columns(2).Width = 260
            Dim rs As Object = dbOrbite.OpenRecordset("SELECT * FROM [Satelliti] ORDER BY NomeSatellite", dbOpenDynaset)
            Do While Not rs.EOF
                Dim versoVal As Boolean = False
                Dim rv = rs!verso
                If Not IsDBNull(rv) AndAlso rv IsNot Nothing Then
                    Dim s = CStr(rv).Trim().ToLower()
                    versoVal = (s = "true" OrElse s = "vero" OrElse s = "1" OrElse s = "-1")
                End If
                dgvSatelliti.Rows.Add(
                    If(IsDBNull(rs!NomeSatellite), "", CStr(rs!NomeSatellite)),
                    If(IsDBNull(rs!Algoritmo), "", CStr(rs!Algoritmo)),
                    If(IsDBNull(rs!FileAgg), "", CStr(rs!FileAgg)),
                    If(IsDBNull(rs!polangle), "", CStr(rs!polangle)),
                    versoVal)
                rs.MoveNext
            Loop
            rs.Close()
        Catch ex As Exception
            WriteError(0, "LoadSatelliti: " & ex.Message)
        End Try
    End Sub

    ' -------------------------------------------------------
    ' Selezione riga nel DataGridView → aggiorna s_Satellite
    ' -------------------------------------------------------
    Private Sub DgvSatelliti_SelectionChanged(sender As Object, e As EventArgs) Handles dgvSatelliti.SelectionChanged
        If dgvSatelliti.CurrentRow IsNot Nothing Then
            Dim v As Object = dgvSatelliti.CurrentRow.Cells("NomeSatellite").Value
            If v IsNot Nothing AndAlso Not IsDBNull(v) Then
                s_Satellite = CStr(v)
                ' CmdLoad_Click (frmRicezione) rilegge il satellite da
                ' LblSatellite.Text ("Satellite = LblSatellite.Text"), non da
                ' s_Satellite: senza questa sincronizzazione, selezionare qui
                ' un satellite non aveva alcun effetto su "Carica" (restava
                ' caricato quello precedentemente mostrato nel combo).
                For Each f As Form In My.Application.OpenForms
                    If TypeOf f Is frmRicezione Then
                        CType(f, frmRicezione).LblSatellite.Text = s_Satellite
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    ' -------------------------------------------------------
    ' Verso — commit immediato al click e salvataggio nel DB
    ' -------------------------------------------------------
    Private Sub DgvSatelliti_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvSatelliti.CurrentCellDirtyStateChanged
        If dgvSatelliti.IsCurrentCellDirty AndAlso
           TypeOf dgvSatelliti.CurrentCell Is DataGridViewCheckBoxCell Then
            dgvSatelliti.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DgvSatelliti_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSatelliti.CellValueChanged
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        Dim colName As String = dgvSatelliti.Columns(e.ColumnIndex).Name
        Dim satName As String = CStr(dgvSatelliti.Rows(e.RowIndex).Cells("NomeSatellite").Value)
        If String.IsNullOrEmpty(satName) Then Return
        Dim rs As Object = dbOrbite.OpenRecordset(
            "SELECT * FROM [Satelliti] WHERE NomeSatellite='" & satName & "'", dbOpenDynaset)
        If Not rs.EOF Then
            rs.Edit()
            Select Case colName
                Case "NomeSatellite" : rs!NomeSatellite = CStr(dgvSatelliti.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                Case "Algoritmo" : rs!Algoritmo = CStr(dgvSatelliti.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                Case "FileAgg" : rs!FileAgg = CStr(dgvSatelliti.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                Case "PolAngle" : rs!polangle = CStr(dgvSatelliti.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                Case "Verso" : rs!verso = CBool(dgvSatelliti.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            End Select
            rs.Update()
        End If
        rs.Close()
    End Sub

    ' -------------------------------------------------------
    ' Menu Exit → popola ComboBox di frmRicezione e chiude
    ' -------------------------------------------------------
    Private Sub MnuExit_Click(sender As Object, e As EventArgs) Handles MnuExit.Click
        Esci()
    End Sub

    ' -------------------------------------------------------
    ' Menu Remove → elimina satellite selezionato
    ' -------------------------------------------------------
    Private Sub MnuRemove_Click(sender As Object, e As EventArgs) Handles MnuRemove.Click
        Elimina()
        LoadSatelliti()
    End Sub

    ' -------------------------------------------------------
    ' Menu Parameter → apre form parametri per il satellite
    ' -------------------------------------------------------
    Private Sub MnuParameter_Click(sender As Object, e As EventArgs) Handles MnuParameter.Click
        Try
            If s_Satellite = "" Then Exit Sub
            Dim rs As Object = dbOrbite.OpenRecordset("SELECT * FROM [Satelliti] WHERE NomeSatellite='" & s_Satellite & "'", dbOpenDynaset)
            If Not rs.EOF Then
                If rs!Algoritmo = 0 Or rs!Algoritmo = 2 Then
                    AggAlgo = rs!Algoritmo
                    Dim f1 As New Frm11Elementi()
                    f1.ShowDialog()
                ElseIf rs!Algoritmo = 1 Then
                    Dim fn As New FrmNorad()
                    fn.ShowDialog()
                End If
            End If
            rs.Close()
        Catch ex As Exception
            WriteError(0, "MnuParameter_Click: " & ex.Message)
        End Try
    End Sub

    ' -------------------------------------------------------
    ' ToolBar — New
    ' -------------------------------------------------------
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Nuovo()
    End Sub

    ' -------------------------------------------------------
    ' ToolBar — Save
    ' -------------------------------------------------------
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Salvo()
    End Sub

    ' -------------------------------------------------------
    ' ToolBar — Print
    ' -------------------------------------------------------
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Stampo()
    End Sub

    ' -------------------------------------------------------
    ' ToolBar — Delete
    ' -------------------------------------------------------
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Elimina()
        LoadSatelliti()
    End Sub

    ' -------------------------------------------------------
    ' CmbSatelliti
    ' -------------------------------------------------------
    Private Sub CmbSatelliti_KeyUp(KeyCode As Integer, Shift As Integer)
        If KeyCode = CR Then
            s_Satellite = CmbSatelliti.Text
        End If
    End Sub

    Private Sub CmbSatelliti_LostFocus()
        s_Satellite = CmbSatelliti.Text
    End Sub

    ' -------------------------------------------------------
    ' Abort nuovo satellite
    ' -------------------------------------------------------
    Private Sub CmdAbort_Click(sender As Object, e As EventArgs) Handles CmdAbort.Click
        pnlNewSatellite.Visible = False
        btnSave.Enabled = False
    End Sub

    ' -------------------------------------------------------
    ' Elimina satellite corrente dal DB
    ' -------------------------------------------------------
    Private Sub Elimina()
        Try
            If s_Satellite = "" Then Exit Sub
            Dim rs As Object = dbOrbite.OpenRecordset("SELECT * FROM [Satelliti] WHERE NomeSatellite='" & s_Satellite & "'", dbOpenDynaset)
            If Not rs.EOF Then
                If rs!Algoritmo = 1 Then
                    DbExecute("Delete * from [Sat2line] where Sat_name ='" & s_Satellite & "'")
                ElseIf rs!Algoritmo = 0 Or rs!Algoritmo = 2 Then
                    DbExecute("Delete * from [Sat11par] where Name ='" & s_Satellite & "'")
                End If
                rs.Delete()
            End If
            rs.Close()
        Catch ex As Exception
            WriteError(0, "Elimina: " & ex.Message)
        End Try
    End Sub

    ' -------------------------------------------------------
    ' Stampa (stub — TDBGrid.PrintInfo non disponibile)
    ' -------------------------------------------------------
    Private Sub Stampo()
        MessageBox.Show("Print not available in .NET version.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub



    ' -------------------------------------------------------
    ' Nuovo satellite — mostra pannello di inserimento
    ' -------------------------------------------------------
    Private Function Nuovo() As Boolean
        Nuovo = True
        pnlNewSatellite.Visible = True
        btnSave.Enabled = True
        TxtParametri.Text = ""
        chkVerso.Checked = False
        CmbSatelliti.Items.Clear()
        TxtAlgorithm.Focus()
    End Function

    ' -------------------------------------------------------
    ' Salvo — salva nuovo satellite nel DB e ricarica griglia
    ' -------------------------------------------------------
    Private Function Salvo() As Boolean
        Dim b_Open As Boolean
        Salvo = True
        'On Error GoTo ErrSalvo
        s_Satellite = CmbSatelliti.Text
        If s_Satellite <> "" Then
            tmpSql = "SELECT * FROM [Satelliti] order by NomeSatellite"
            rs_Satel = dbOrbite.OpenRecordset(tmpSql, dbOpenDynaset)
            b_Open = True
            If Not IsNumeric(TxtPolAngle.Text) Then TxtPolAngle.Text = "0"
            rs_Satel.FindFirst("NomeSatellite ='" & s_Satellite & "'")
            If Not rs_Satel.NoMatch Then
                rs_Satel.Edit()
                rs_Satel!NomeSatellite = s_Satellite
                rs_Satel!Algoritmo = TxtAlgorithm.Text
                rs_Satel!FileAgg = TxtParametri.Text
                rs_Satel!polangle = TxtPolAngle.Text
                rs_Satel!verso = chkVerso.Checked
                rs_Satel.Update()
            Else
                rs_Satel.AddNew()
                rs_Satel!NomeSatellite = s_Satellite
                rs_Satel!Algoritmo = TxtAlgorithm.Text
                rs_Satel!FileAgg = TxtParametri.Text
                rs_Satel!polangle = TxtPolAngle.Text
                rs_Satel!verso = chkVerso.Checked
                rs_Satel.Update()
            End If
            rs_Satel.Close()
            b_Open = False
            pnlNewSatellite.Visible = False
            btnSave.Enabled = False
            Dim nomeSalvato As String = s_Satellite   ' preserva il nome prima del reload
            LoadSatelliti()
            s_Satellite = nomeSalvato                 ' ripristina dopo SelectionChanged
            If IsNumeric(TxtAlgorithm.Text) Then
                If CInt(TxtAlgorithm.Text) = 0 Or CInt(TxtAlgorithm.Text) = 2 Then
                    AggAlgo = CInt(TxtAlgorithm.Text)
                    Aggiorna(CInt(TxtAlgorithm.Text), s_Satellite)
                    Dim f1 As New Frm11Elementi()
                    f1.ShowDialog()
                ElseIf CInt(TxtAlgorithm.Text) = 1 Then
                    Dim fn As New FrmNorad()
                    fn.ShowDialog()
                End If
            End If
        End If
        Exit Function
ErrSalvo:
        If Err.Number = 0 Or Err.Number = 20 Then
            If b_Open Then
                b_Open = False
                rs_Satel.Close()
            End If
            Exit Function
        End If
        WriteError(Err.Number, "Satelliti - Salvo " & Err.Description)
        Err.Clear()
        Resume Next
    End Function

    ' -------------------------------------------------------
    ' Esci — popola ComboBox di frmRicezione con tutti i satelliti
    ' -------------------------------------------------------
    Private Function Esci() As Boolean
        Esci = True
        Dim ric As FrmRicezione = MainForm.frmRicezioneInstance
        If ric Is Nothing Then Me.Close() : Return True
        Dim rs_Orbite As Object = dbOrbite.OpenRecordset("SELECT * FROM [Satelliti] order by NomeSatellite", dbOpenDynaset)
        ric.LblSatellite.Items.Clear()
        Do While Not rs_Orbite.EOF
            If Not IsDBNull(rs_Orbite!NomeSatellite) Then
                ric.LblSatellite.Items.Add(CStr(rs_Orbite!NomeSatellite))
            End If
            rs_Orbite.MoveNext
        Loop
        rs_Orbite.Close()
        ric.LblSatellite.Text = s_Satellite
        Me.Close()
    End Function

    ' -------------------------------------------------------
    ' TxtAlgorithm KeyUp — legge file parametri e popola CmbSatelliti
    ' -------------------------------------------------------
    Private Sub TxtAlgorithm_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TxtAlgorithm.KeyUp
        Dim fileisopen As Boolean
        Try
            CmbSatelliti.Items.Clear()

            If e.KeyCode = Keys.Return Then
                If Not IsNumeric(TxtAlgorithm.Text) Then
                    TxtAlgorithm.Text = "5"
                    Exit Sub
                ElseIf IsNumeric(TxtAlgorithm.Text) And CInt(TxtAlgorithm.Text) < 4 Then
                    Dim fb As New frmBrowse()
                    fb.ShowDialog()
                    If g_Parametri <> "" Then
                        TxtParametri.Text = g_Parametri
                        hFile = FreeFile()
                        FileOpen(hFile, g_Parametri, OpenMode.Input)
                        fileisopen = True
                        Gr_file = LOF(hFile)

                        If Gr_file < LUNGFILE Then
                            FileClose(hFile)
                            fileisopen = False
                            TxtAlgorithm.Focus()
                            Exit Sub
                        End If
                        i_Scelta = 0
                        Select Case CInt(TxtAlgorithm.Text)
                            Case 0
                                Do While Not EOF(hFile)
                                    sReadBuffer = InputString(hFile, CInt(Gr_file))
                                    i_Scelta = InStr(1, sReadBuffer, "FOR", 1)
                                    If i_Scelta <> 0 Then Exit Do
                                Loop
                                If i_Scelta = 0 Then Exit Sub
                                i_Scelta += Len("FOR")
                                i_Scelta1 = InStr(1, sReadBuffer, "/", 1)
                                CmbSatelliti.Items.Add(LTrim(Mid$(sReadBuffer, i_Scelta, i_Scelta1 - i_Scelta)))
                                CmbSatelliti.SelectedIndex = 0
                                s_Satellite = CmbSatelliti.Text
                            Case 1
                                Do While Not EOF(hFile)
                                    If EOF(hFile) Then Exit Do
                                    Input(hFile, sReadBuffer)
                                    CmbSatelliti.Items.Add(sReadBuffer)
                                    Input(hFile, sReadBuffer)
                                    Input(hFile, sReadBuffer)
                                Loop
                                CmbSatelliti.SelectedIndex = 0
                            Case 2
                                Do While Not EOF(hFile)
                                    sReadBuffer = InputString(hFile, CInt(Gr_file))
                                    i_Scelta = InStr(1, sReadBuffer, "SATELLITE NAME:", 1)
                                    If i_Scelta <> 0 Then Exit Do
                                Loop
                                If i_Scelta = 0 Then Exit Sub
                                sReadBuffer = Mid$(sReadBuffer, i_Scelta, Len(sReadBuffer) - i_Scelta)
                                i_Scelta = Len("SATELLITE NAME: ")
                                i_Scelta1 = InStr(1, sReadBuffer, vbCr, 1)
                                CmbSatelliti.Items.Add(LTrim(Mid$(sReadBuffer, i_Scelta, i_Scelta1 - i_Scelta)))
                                CmbSatelliti.SelectedIndex = 0
                                s_Satellite = CmbSatelliti.Text
                            Case 3
                                s_Satellite = CmbSatelliti.Text
                        End Select
                    End If
                ElseIf CInt(TxtAlgorithm.Text) = 4 Then
                    CmbSatelliti.Items.Add("MOON")
                    CmbSatelliti.Items.Add("SUN")
                    CmbSatelliti.SelectedIndex = 0
                End If
            End If
            FileClose(hFile)
            fileisopen = False
        Catch ex As Exception
            If fileisopen Then FileClose(hFile)
            WriteError(0, "FrmSatelliti - TxtAlgorithm " & ex.Message)
        End Try
    End Sub

End Class
