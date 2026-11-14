' ============================================================
' BordoPulsanti.vb — bordo fisso in rilievo su tutti i pulsanti
' Con lo stile di Windows 11 i pulsanti (Cmd...) hanno un bordo
' sottile e chiaro e sullo sfondo grigio non si distinguono come
' comandi. Qui ogni Button dei form aperti riceve un bordo 3D sempre
' visibile, come nel VB6: in rilievo, incassato mentre è premuto.
' Uso: BordoPulsanti.Attiva() una volta all'avvio. Vale per tutti i
' form, anche quelli aperti dopo e i pulsanti creati a runtime.
' ============================================================

Public Module BordoPulsanti

    Private _attivo As Boolean
    Private ReadOnly _formFatti As New HashSet(Of System.Windows.Forms.Form)

    Public Sub Attiva()
        If _attivo Then Return
        _attivo = True
        ' Non esiste un evento "form creato": a ogni ciclo di inattività si
        ' controllano i form aperti e si trattano solo quelli nuovi.
        AddHandler System.Windows.Forms.Application.Idle, AddressOf ControllaFormAperti
    End Sub

    Private Sub ControllaFormAperti(sender As Object, e As EventArgs)
        For Each f As System.Windows.Forms.Form In System.Windows.Forms.Application.OpenForms
            If _formFatti.Add(f) Then
                AddHandler f.FormClosed, Sub(s, a) _formFatti.Remove(DirectCast(s, System.Windows.Forms.Form))
                ApplicaA(f)
            End If
        Next
    End Sub

    Private Sub ApplicaA(contenitore As System.Windows.Forms.Control)
        ' RemoveHandler prima di AddHandler: un controllo già trattato non
        ' riceve il gestore due volte.
        RemoveHandler contenitore.ControlAdded, AddressOf ControlloAggiunto
        AddHandler contenitore.ControlAdded, AddressOf ControlloAggiunto
        Dim b As System.Windows.Forms.Button = TryCast(contenitore, System.Windows.Forms.Button)
        If b IsNot Nothing Then
            RemoveHandler b.Paint, AddressOf DisegnaBordo
            AddHandler b.Paint, AddressOf DisegnaBordo
            b.Invalidate()
        End If
        For Each c As System.Windows.Forms.Control In contenitore.Controls
            ApplicaA(c)
        Next
    End Sub

    Private Sub ControlloAggiunto(sender As Object, e As System.Windows.Forms.ControlEventArgs)
        ApplicaA(e.Control)
    End Sub

    Private Sub DisegnaBordo(sender As Object, e As System.Windows.Forms.PaintEventArgs)
        Dim b As System.Windows.Forms.Button = DirectCast(sender, System.Windows.Forms.Button)
        Dim premuto As Boolean = b.Capture AndAlso
            (System.Windows.Forms.Control.MouseButtons And System.Windows.Forms.MouseButtons.Left) <> 0 AndAlso
            b.ClientRectangle.Contains(b.PointToClient(System.Windows.Forms.Cursor.Position))
        System.Windows.Forms.ControlPaint.DrawBorder3D(e.Graphics, b.ClientRectangle,
            If(premuto, System.Windows.Forms.Border3DStyle.Sunken, System.Windows.Forms.Border3DStyle.Raised))
    End Sub

End Module
