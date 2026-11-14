' ============================================================
' MenuConBordo.vb — voci della barra di menu con bordo fisso
' Con il renderer predefinito le voci di primo livello (Exit, ACU,
' Phase, ...) sono solo testo e non si distinguono come comandi:
' qui ognuna ha un bordo in rilievo, sempre visibile, come un
' pulsante. Le voci dei sottomenu restano invariate.
' Uso: MenuConBordo.Applica(MenuStrip1)
' ============================================================

Public Class MenuConBordo
    Inherits System.Windows.Forms.ToolStripProfessionalRenderer

    ''' <summary>
    ''' Applica il bordo alle voci di primo livello del menu e lascia un
    ''' piccolo spazio tra una voce e l'altra.
    ''' </summary>
    Public Shared Sub Applica(menu As System.Windows.Forms.MenuStrip)
        If menu Is Nothing Then Return
        menu.Renderer = New MenuConBordo()
        For Each voce As System.Windows.Forms.ToolStripItem In menu.Items
            voce.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Next
    End Sub

    Protected Overrides Sub OnRenderMenuItemBackground(e As System.Windows.Forms.ToolStripItemRenderEventArgs)
        MyBase.OnRenderMenuItemBackground(e)
        If e.Item.IsOnDropDown Then Return
        Dim r As New System.Drawing.Rectangle(System.Drawing.Point.Empty, e.Item.Size)
        ' Premuto (sottomenu aperto) = incassato, altrimenti in rilievo.
        Dim stile As System.Windows.Forms.Border3DStyle =
            If(e.Item.Pressed, System.Windows.Forms.Border3DStyle.Sunken, System.Windows.Forms.Border3DStyle.Raised)
        System.Windows.Forms.ControlPaint.DrawBorder3D(e.Graphics, r, stile)
    End Sub
End Class
