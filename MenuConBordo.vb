' ============================================================
' MenuConBordo.vb — voci della barra di menu con bordo fisso
' Con il renderer predefinito le voci di primo livello (Exit, ACU,
' Phase, ...) sono solo testo e non si distinguono come comandi:
' qui ognuna ha un bordo in rilievo, sempre visibile, come un
' pulsante. Le voci dei sottomenu e dei menu contestuali restano
' invariate. Le voci di menu non hanno una proprietà "bordo": il loro
' aspetto lo decide il renderer della barra, da qui questa classe.
' Uso: MenuConBordo.ApplicaATutti() una volta all'avvio (tutte le barre
' di menu dell'applicazione), oppure MenuConBordo.Applica(menu).
' ============================================================

Public Class MenuConBordo
    Inherits System.Windows.Forms.ToolStripProfessionalRenderer

    ''' <summary>
    ''' Renderer predefinito di tutta l'applicazione: vale per ogni barra
    ''' di menu con RenderMode = ManagerRenderMode (il default), anche dei
    ''' form aperti dopo.
    ''' </summary>
    Public Shared Sub ApplicaATutti()
        System.Windows.Forms.ToolStripManager.Renderer = New MenuConBordo()
    End Sub

    ''' <summary>Solo per una barra di menu.</summary>
    Public Shared Sub Applica(menu As System.Windows.Forms.MenuStrip)
        If menu Is Nothing Then Return
        menu.Renderer = New MenuConBordo()
    End Sub

    ' Chiamata quando il renderer viene assegnato a una barra: piccolo
    ' spazio tra le voci di primo livello, così i bordi non si toccano.
    Protected Overrides Sub Initialize(toolStrip As System.Windows.Forms.ToolStrip)
        MyBase.Initialize(toolStrip)
        If TypeOf toolStrip Is System.Windows.Forms.MenuStrip Then
            For Each voce As System.Windows.Forms.ToolStripItem In toolStrip.Items
                voce.Margin = New System.Windows.Forms.Padding(2)
            Next
        End If
    End Sub

    Protected Overrides Sub OnRenderMenuItemBackground(e As System.Windows.Forms.ToolStripItemRenderEventArgs)
        MyBase.OnRenderMenuItemBackground(e)
        If e.Item.IsOnDropDown OrElse Not TypeOf e.ToolStrip Is System.Windows.Forms.MenuStrip Then Return
        Dim r As New System.Drawing.Rectangle(System.Drawing.Point.Empty, e.Item.Size)
        ' Premuto (sottomenu aperto) = incassato, altrimenti in rilievo.
        Dim stile As System.Windows.Forms.Border3DStyle =
            If(e.Item.Pressed, System.Windows.Forms.Border3DStyle.Sunken, System.Windows.Forms.Border3DStyle.Raised)
        System.Windows.Forms.ControlPaint.DrawBorder3D(e.Graphics, r, stile)
    End Sub
End Class
