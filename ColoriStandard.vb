' ============================================================
' ColoriStandard.vb — regole generali di presentazione, valide
' per tutti i programmi (come nel VB6):
'   - valori numerici allineati a destra
'   - sola presentazione dati (coordinate, valori comandati, ...):
'       giallo se sconosciuti, 128,255,255 se conosciuti
'   - dati con allarme: rosso = allarme, verde = valido,
'       giallo = non collegato
'   - comandi in evidenza e diciture: grigi
' Uso per i campi di sola presentazione: ColoriStandard.RegistraDati(...)
' nel Load del form; il colore segue il testo (vuoto o "---" = giallo).
' ============================================================

Public Module ColoriStandard

    Public ReadOnly COL_DATO_IGNOTO As System.Drawing.Color = System.Drawing.Color.Yellow                    ' vbYellow
    Public ReadOnly COL_DATO_NOTO As System.Drawing.Color = System.Drawing.Color.FromArgb(128, 255, 255)     ' &HFFFF80
    Public ReadOnly COL_ALLARME As System.Drawing.Color = System.Drawing.Color.Red                           ' vbRed
    Public ReadOnly COL_VALIDO As System.Drawing.Color = System.Drawing.Color.Lime                           ' vbGreen
    Public ReadOnly COL_NON_COLLEGATO As System.Drawing.Color = System.Drawing.Color.Yellow                  ' vbYellow
    Public ReadOnly COL_GRIGIO As System.Drawing.Color = System.Drawing.SystemColors.Control                 ' comandi e diciture

    ''' <summary>
    ''' Campi di sola presentazione: giallo finché il valore non è noto,
    ''' 128,255,255 appena il campo contiene un valore.
    ''' </summary>
    Public Sub RegistraDati(ParamArray controlli() As System.Windows.Forms.Control)
        For Each c As System.Windows.Forms.Control In controlli
            If c Is Nothing Then Continue For
            RemoveHandler c.TextChanged, AddressOf AggiornaDato
            AddHandler c.TextChanged, AddressOf AggiornaDato
            AggiornaDato(c, EventArgs.Empty)
        Next
    End Sub

    ''' <summary>Vero se il testo rappresenta un valore noto (non vuoto, non "---").</summary>
    Public Function DatoNoto(testo As String) As Boolean
        If String.IsNullOrWhiteSpace(testo) Then Return False
        Return testo.Trim().Trim("-"c, "?"c).Length > 0
    End Function

    Private Sub AggiornaDato(sender As Object, e As EventArgs)
        Dim c As System.Windows.Forms.Control = DirectCast(sender, System.Windows.Forms.Control)
        c.BackColor = If(DatoNoto(c.Text), COL_DATO_NOTO, COL_DATO_IGNOTO)
    End Sub

End Module
