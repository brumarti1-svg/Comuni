Imports System.IO
Imports System.Windows.Forms

' ============================================================
' frmBrowse.vb — convertito da frmBrowse.frm (VB6)
' Selettore file parametri (.par, .Txt, .csv)
' ============================================================
' NOTE DI CONVERSIONE:
'   - DirListBox + DriveListBox + ListBox → FolderBrowserDialog + ListBox
'   - FindFirstFile / FindNextFile API → Directory.GetFiles()
'   - ShellExecute → Process.Start()
'   - Me.Close() → Me.Close()
' ============================================================

Partial Public Class frmBrowse
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
    End Sub

    ' -------------------------------------------------------
    ' Form Load — popola la lista file
    ' -------------------------------------------------------
    Private Sub frmBrowse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Parameter List"
        ' VB6: la DirListBox partiva dalla cartella corrente dell'exe (nessun
        ' Path impostato in Designer) - qui l'equivalente è g_DirPar (la
        ' cartella parametri configurata in Configurazione/INI, "Server" >
        ' "Directory"), dove si trovano davvero i file .par. "Documenti" di
        ' Windows non li contiene mai: la lista restava vuota/sbagliata e il
        ' file voluto non si poteva selezionare.
        Dim cartellaIniziale As String
        If Not String.IsNullOrWhiteSpace(g_DirPar) AndAlso Directory.Exists(g_DirPar) Then
            cartellaIniziale = g_DirPar
        Else
            cartellaIniziale = Application.StartupPath
        End If
        CaricaFile(cartellaIniziale)
        LblPreview.Text = cartellaIniziale
    End Sub

    ' -------------------------------------------------------
    ' Bottone Sfoglia cartella (sostituisce DriveListBox + DirListBox)
    ' -------------------------------------------------------
    Private Sub btnSfoglia_Click(sender As Object, e As EventArgs) Handles btnSfoglia.Click
        Using dlg As New FolderBrowserDialog()
            dlg.Description = "Seleziona la cartella dei parametri"
            dlg.SelectedPath = If(Not String.IsNullOrWhiteSpace(g_DirPar) AndAlso Directory.Exists(g_DirPar), g_DirPar, Application.StartupPath)
            If dlg.ShowDialog() = DialogResult.OK Then
                CaricaFile(dlg.SelectedPath)
                LblPreview.Text = dlg.SelectedPath
            End If
        End Using
    End Sub

    ' -------------------------------------------------------
    ' Carica file filtrati nella ListBox
    ' (era GetFiles con FindFirstFile API)
    ' -------------------------------------------------------
    Private Sub CaricaFile(folderPath As String)
        lstMdb.Items.Clear()
        Try
            Dim files = Directory.GetFiles(folderPath, "*.*")
            For Each f As String In files
                Dim ext As String = System.IO.Path.GetExtension(f).ToLower()
                If ext = ".par" OrElse ext = ".txt" OrElse ext = ".csv" Then
                    lstMdb.Items.Add(f)
                End If
            Next
            Me.Text = "Parameter List — " & lstMdb.Items.Count & " file"
        Catch ex As Exception
            LblPreview.Text = "Error: " & ex.Message
        End Try
    End Sub

    ' -------------------------------------------------------
    ' Doppio clic su file — seleziona e chiude
    ' (era lstMdb_DblClick)
    ' -------------------------------------------------------
    Private Sub lstMdb_DoubleClick(sender As Object, e As EventArgs) Handles lstMdb.DoubleClick
        If lstMdb.SelectedIndex < 0 Then Exit Sub
        g_Parametri = lstMdb.SelectedItem.ToString()
        Me.Close()
    End Sub

    ' -------------------------------------------------------
    ' Menu Chiudi
    ' -------------------------------------------------------
    Private Sub MnuChiudi_Click(sender As Object, e As EventArgs) Handles MnuChiudi.Click
        Me.Close()
    End Sub

End Class
