' ============================================================
' frmPreset.Http.vb
' Punto d'accesso per il server HTTP integrato di BTS1
' (BTS1\HttpServer\AntennaHttpServer.vb, BTS1\frmRicezione.Http.vb).
' Unico file dello Shared Project toccato per questa funzionalità.
' ============================================================

Partial Class FrmPreset

    ''' <summary>
    ''' Imposta i campi Azimuth/Elevazione (solo quelli non inibiti, secondo
    ''' i_Assi già calcolato dal chiamante in FrmRicezione.CmdCmdPos_Click)
    ''' ed esegue esattamente ciò che fa il bottone Execute, riusando
    ''' InviaComando() senza duplicarne la logica di formattazione/checksum/invio.
    ''' Mappatura i_Assi (frmPreset.vb, Select Case i_Assi): 0 = entrambi gli
    ''' assi, 1 = "AP" solo Azimuth (elText viene ignorato), 2 = "EP" solo
    ''' Elevazione (azText viene ignorato).
    ''' </summary>
    Public Sub ImpostaEEsegui(azText As String, elText As String)
        If i_Assi <> 2 AndAlso Not String.IsNullOrWhiteSpace(azText) Then
            MskAzimuth.Text = azText
        End If
        If i_Assi <> 1 AndAlso Not String.IsNullOrWhiteSpace(elText) Then
            MskElevazione.Text = elText
        End If
        InviaComando()
    End Sub

End Class
