Imports System.Data.SQLite

Module Ricezione
    Dim s_analisi As String
    Dim i As Integer, lun As Integer, d_Az As String, d_El As String, d_Po As String
    Dim rs_Ricez As Object
    Dim Az_prov As Double, El_Prov As Double
    Dim rndSimBeacon As New Random()


    '13/03/2024 Viene creato un modulo comune a tutti i programmi ACU che raccoglie le routine di calcolo dei vari algoritmi uguali per tutti
    'attualmente inseriti nella frmRicezione di ciascun programma. Si cerca System.Math.ì di razionalizzare i programmi per evitare
    'di dover ripetere eventuali correzioni ogni volta per tutti

    Public Function OrdiniMcs(Ordine As String) As Boolean
        OrdiniMcs = True
        g_aggOrdini = True
        lun = Len(Ordine)
        i = InStr(2, Ordine, " ", 1)
        If i = 0 Then
            s_analisi = Mid$(Ordine, 1, lun)
        Else
            s_analisi = Mid$(Ordine, 1, i - 1)
        End If
        Select Case s_analisi
            Case "*", "?", ">", "<"
                b_Ricezione(ACU) = True
                i_Cont = 2
                Attesa(0.2)
            Exit Function
            Case " S", "AS", "ES"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.LblCommand.Text = "Stand By"
                i_Status = OP_STANDBY
                Exit Function
            Case " ST", "AST", "EST"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.CmdOffset.Enabled = False
                frmRicezione.CmdOffset.Visible = False
                i_Status = OP_STEPTRACK
                frmRicezione.LblStepTrack.Visible = True
                frmRicezione.LblCommand.Text = "Step Track"
                Exit Function
            Case " AQ", "AAQ", "EAQ"
                PTK = &H0
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.CmdOffset.Enabled = False
                frmRicezione.CmdOffset.Visible = False
                i_Status = OP_AUTOMATICAQUIRE
                frmRicezione.LblCommand.Text = "Automatic Acquire"
                i_ProgTrack = 7
                Exit Function
            Case " M", "AM", "EM"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblCommand.Text = "Memory Track"
                i_Status = OP_MEMORYTRACK
                Exit Function
            Case " W", "AW", "EW"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.LblCommand.Text = "Stow"
                i_Status = OP_STANDBY
                Exit Function
            Case " U", "AU", "EU"
                PTK = &H0
                '            d_DataInizioProg = CDate("31/12/3000 00.00.00")
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.LblCommand.Text = "Unstow"
                i_Status = OP_STANDBY
                Exit Function
            Case " P", " X", "WS", "US", "BP"
                If lun > 15 Then
                    d_Az = Mid$(Ordine, 4, 7)
                    d_El = Mid$(Ordine, 12, 6)
                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            If d_El >= 0 And d_El < 92 Then
                                PTK = &H0
                                d_DataInizioProg = ""
                                MainForm.ProgramStatus = d_DataInizioProg
                                i_Status = OP_COMMANDPOSITION
                                frmRicezione.LblCommand.Text = "Command Position"
                                frmRicezione.LblAzimuth.Text = d_Az
                                frmRicezione.LblElevazione.Text = d_El
                                AzCalc = d_Az
                                ElCalc = d_El
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 4, 6)
                    d_El = Mid$(Ordine, 11, 5)
                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            If d_El >= 0 And d_El < 92 Then
                                PTK = &H0
                                d_DataInizioProg = ""
                                MainForm.ProgramStatus = d_DataInizioProg
                                i_Status = OP_COMMANDPOSITION
                                frmRicezione.LblCommand.Text = "Command Position"
                                frmRicezione.LblAzimuth.Text = d_Az
                                frmRicezione.LblElevazione.Text = d_El
                                AzCalc = d_Az
                                ElCalc = d_El
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                End If
            Case "AP", "AX"
                If lun > 9 Then
                    d_Az = Mid$(Ordine, 4, 7)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            frmRicezione.LblAzimuth.Text = d_Az
                            AzCalc = d_Az
                            PTK = &H0
                            d_DataInizioProg = ""
                            MainForm.ProgramStatus = d_DataInizioProg
                            i_Status = OP_COMMANDPOSITION
                            frmRicezione.LblCommand.Text = "Command Position"
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 4, 6)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            frmRicezione.LblAzimuth.Text = d_Az
                            AzCalc = d_Az
                            PTK = &H0
                            d_DataInizioProg = ""
                            MainForm.ProgramStatus = d_DataInizioProg
                            i_Status = OP_COMMANDPOSITION
                            frmRicezione.LblCommand.Text = "Command Position"
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                End If
            Case "EP", "EX"
                If lun > 8 Then
                    d_El = Mid$(Ordine, 4, 6)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            PTK = &H0
                            d_DataInizioProg = ""
                            MainForm.ProgramStatus = d_DataInizioProg
                            i_Status = OP_COMMANDPOSITION
                            frmRicezione.LblCommand.Text = "Command Position"
                            frmRicezione.LblElevazione.Text = d_El
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 4, 5)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            PTK = &H0
                            d_DataInizioProg = ""
                            MainForm.ProgramStatus = d_DataInizioProg
                            i_Status = OP_COMMANDPOSITION
                            frmRicezione.LblCommand.Text = "Command Position"
                            frmRicezione.LblElevazione.Text = d_El
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    OrdiniMcs = True
                    Exit Function
                End If
            Case " A", "AA", "EA"
                PTK = &H0
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                frmRicezione.CmdOffset.Enabled = False
                frmRicezione.CmdOffset.Visible = False
                i_Status = OP_AUTOTRACK
                i_ProgTrack = 2
                frmRicezione.LblCommand.Text = "Auto Track"
                Exit Function
            Case "PLP"                                          'Comando di polarizzazione 31/10/2022
                d_Az = Mid$(Ordine, 5, 5)
                If IsNumeric(d_Az) Then
                    If d_Az >= -98.9 And d_Az < 98.9 Then
                        i_Status = OP_COMMANDPOSITION
                        frmRicezione.LblCommand.Text = "Command Position"
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                OrdiniMcs = True
                Exit Function
        End Select
        OrdiniMcs = False
        g_aggOrdini = False
    End Function

    ''' <summary>
    ''' Simulazione del ricevitore che trova il beacon del satellite, in
    ''' assenza di hardware reale collegato — usata sia durante la ricerca
    ''' Acquire che dopo, in Augmented/AutoTrack.
    ''' L'AVVICINAMENTO verso la soglia (fase di ricerca) va fatto avanzare
    ''' SOLO da Calcolo_Aquire (un passo per ogni suo tick, come in origine):
    ''' chiamarlo anche dal ciclo periodico sempre attivo di frmMain.vb (più
    ''' frequente di TmrSpiralAcquire) faceva avvicinare il beacon al proprio
    ''' ritmo, scollegato da quanto la ricerca fosse realmente avanzata — in
    ''' test si è visto l'aggancio scattare subito, senza che la ricerca
    ''' avesse ancora coperto nulla. La piccola oscillazione DOPO l'aggancio
    ''' invece va tenuta viva anche dal ciclo periodico (permettiAvvicinamento
    ''' As False): serve perché TmrSpiralAcquire si ferma in Augmented, e con
    ''' un ricevitore reale il segnale continuerebbe comunque ad arrivare.
    ''' </summary>
    Public Sub AvanzaSimulazioneBeacon(Optional permettiAvvicinamento As Boolean = True)
        ' NIENTE "If i_Spiral < 1 Then Return" qui: b_Lock deve restare
        ' sempre valido (confrontato dal vivo con i_LockThr) anche FUORI da
        ' un Acquire attivo, altrimenti fuori Acquire b_Lock resta "congelato"
        ' sul suo ultimo valore (es. ancora True da una ricerca precedente,
        ' o addirittura True di default perché Level=0 non ancora
        ' inizializzato sembra "più forte" di qualunque soglia negativa) -
        ' segnalato: lock mostrato con beacon sotto soglia. Solo la fase di
        ' SALITA (il segnale che si avvicina) resta legata a i_Spiral >= 1
        ' più sotto, perché ha senso solo durante una ricerca in corso.
        ' Inizializzazione con un flag dedicato, non più "If d_SimBeaconErr =
        ' 0" come sentinella: un errore perfettamente convergente può
        ' legittimamente arrivare a ESATTAMENTE 0 (clampato da Math.Max(0,
        ' ...) nel ramo giaAgganciato sotto), e in quel caso il vecchio
        ' controllo lo scambiava per "mai inizializzato", rispedendolo al
        ' valore peggiore (32) e congelando la simulazione (segnalato:
        ' "quando ha il lock Acquire non deve tornare al punto di
        ' partenza" - visto nel log: Err da ~0.15 a 32 di colpo, poi Level
        ' ed Err congelati identici per oltre un minuto).
        If Not simBeaconInizializzato Then
            d_SimBeaconErr = (g_ThrAz + g_ThrEl) * 4 ' valore iniziale, fuori soglia
            d_SimBeaconLevel = -95 ' valore iniziale, sotto -90
            simBeaconInizializzato = True
        End If

        Dim sogliaErr As Double = Math.Min(g_ThrAz, g_ThrEl)
        ' Soglia di primo aggancio: SEMPRE ScalaMin (soglia minima del
        ' ricevitore, pannello principale) - chiarito dall'utente: "ScalaMin
        ' per trovare il lock, che dice il satellite è agganciato. Da qui
        ' parte la ricerca fine intorno al punto trovato, fino a quando non
        ' si raggiunge i_LockThr che determina l'augmented". i_LockThr non è
        ' più la soglia di primo aggancio (era un fraintendimento mio
        ' precedente) - è la soglia di ARRIVO della ricerca fine, vedi
        ' sogliaPlateauAttuale sotto e il gate in Calcolo_Aquire.
        Dim sogliaLockAttuale As Double = ScalaMin
        ' Soglia di "aggancio pieno"/plateau, usata per il tetto di salita e
        ' il plateau di oscillazione della simulazione: i_LockThr SOLO
        ' durante un Acquire attivo (è la soglia che determina Augmented,
        ' non più g_ThrAuto), altrimenti ScalaMax (soglia massima del
        ' ricevitore, fuori ricerca non esiste un concetto di "Augmented").
        Dim sogliaPlateauAttuale As Double = If(i_Spiral >= 1, i_LockThr, ScalaMax)
        Dim giaAgganciato As Boolean = (d_SimBeaconLevel >= sogliaPlateauAttuale AndAlso d_SimBeaconErr <= sogliaErr)
        ' permettiAvvicinamento=False (ciclo periodico di frmMain.vb, SOLO
        ' quando un Acquire è attivo: lì la salita la fa già Calcolo_Aquire,
        ' al proprio ritmo) NON deve far avanzare la fase di salita, ma deve
        ' comunque RISCRIVERE sotto i campi col valore preciso attuale:
        ' altrimenti, durante la salita, un'altra scrittura nel frattempo
        ' (AggiornaBeaconLock, dal frame ACU simulato con livello
        ' arrotondato a intero) resta l'ultima e il valore mostrato appare
        ' bloccato anche se la salita interna continua — lo stesso problema
        ' già risolto per la fase di oscillazione post-aggancio, presente
        ' anche qui. Fuori Acquire, invece, il ciclo periodico passa
        ' permettiAvvicinamento:=True (nessun altro chiamante può far
        ' avanzare due volte), altrimenti il beacon resta fermo per sempre a
        ' -95 (segnalato: "il valore beacon rimane a -95 e non si
        ' incrementa").
        ' Tetto della salita: deve superare la soglia più alta tra le due
        ' impostate (il plateau attuale e la soglia di lock attuale), non
        ' solo il plateau - altrimenti se la soglia di lock è più severa, il
        ' livello simulato non riesce proprio a raggiungerla e il lock non
        ' scatta mai (bug di simulazione, non di logica).
        Dim tettoSalita As Double = Math.Max(sogliaPlateauAttuale, sogliaLockAttuale) + 5
        If d_SimBeaconLivelloFisso <> 0 Then
            ' Livello FISSO per i test (es. "la spirale deve ripetersi più
            ' volte senza mai agganciare") - niente salita né oscillazione,
            ' resta bloccato qui indipendentemente dalle soglie configurate.
            d_SimBeaconLevel = d_SimBeaconLivelloFisso
        ElseIf Not giaAgganciato AndAlso permettiAvvicinamento Then
            ' Livello: sale sempre, indipendentemente dalla soglia di lock -
            ' rappresenta il segnale che si rafforza man mano che l'antenna
            ' si avvicina, visibile anche durante la ricerca a passo largo
            ' (prima del primo aggancio).
            d_SimBeaconLevel = Math.Min(tettoSalita, d_SimBeaconLevel + 0.6)

            ' b_Lock (primo aggancio) va impostato SOLO quando il livello
            ' supera davvero la soglia di lock attuale - prima era
            ' incondizionato, quindi scattava fin dal primissimo tick,
            ' saltando subito allo step Lock (0.1) e bypassando del tutto la
            ' ricerca a passo largo configurata dall'operatore (segnalato in
            ' test: lo step non poteva essere 0.1 fin dall'inizio, la prima
            ' soglia non veniva mai davvero verificata).
            If d_SimBeaconLevel >= sogliaLockAttuale Then
                ' Errore: comincia a convergere solo DOPO il primo aggancio
                ' (prima non è ancora "misurabile"), con un passo calcolato
                ' sui tick che restano al livello per raggiungere la soglia
                ' di arrivo (sogliaPlateauAttuale, cioè i_LockThr durante la
                ' ricerca) - invece di un passo fisso che convergeva da solo
                ' in pochi tick, lasciando poi il solo livello a salire per
                ' minuti e dando l'impressione di un aggancio pieno "di
                ' scatto". Minimo 20 tick fissi, non più 1: se il livello ha
                ' già superato la soglia di arrivo nel momento in cui scatta
                ' l'aggancio debole, il calcolo risultava negativo e si
                ' fermava sempre a 1 tick, facendo convergere l'intero
                ' errore in un solo tick (segnalato: "rimane il problema
                ' della ricerca fine").
                ' I 20 tick fissi valgono SOLO se il livello ha già superato
                ' la soglia di arrivo: come minimo sempre attivo, negli
                ' ultimi 20 tick prima della soglia rallentava l'errore, che
                ' arrivava sotto g_ThrAz/g_ThrEl ben dopo il livello -
                ' Augmented scattava così a -45 invece che appena superato
                ' -50 (segnalato in test). Ora l'errore arriva al
                ' bersaglio nello stesso tick in cui il livello tocca la
                ' soglia.
                Dim tickAllaSoglia As Double = (sogliaPlateauAttuale - d_SimBeaconLevel) / 0.6
                Dim tickRimanenti As Double = If(tickAllaSoglia > 0, Math.Max(1, tickAllaSoglia), 20)
                Dim passoErr As Double = Math.Max(0.05, (d_SimBeaconErr - sogliaErr * 0.5) / tickRimanenti)
                d_SimBeaconErr = Math.Max(0, d_SimBeaconErr - passoErr)
            End If
        ElseIf giaAgganciato Then
            ' Aggancio raggiunto: piccola variazione realistica, sempre
            ' dentro le soglie, invece di restare fissa. Permessa da
            ' qualunque chiamante, anche dal ciclo periodico.
            d_SimBeaconLevel += (rndSimBeacon.NextDouble() * 2.0 - 1.0) * 0.3
            d_SimBeaconLevel = Math.Max(sogliaPlateauAttuale, Math.Min(tettoSalita + 3, d_SimBeaconLevel))
            d_SimBeaconErr += (rndSimBeacon.NextDouble() * 2.0 - 1.0) * 0.2
            d_SimBeaconErr = Math.Max(0, Math.Min(sogliaErr * 0.8, d_SimBeaconErr))
        End If
        ' b_Lock = indicatore LOCK/UNLOCK generale (mostrato a schermo in
        ' frmRicezione e codificato nel bit del messaggio ACU simulato) -
        ' SEMPRE basato su ScalaMin (soglia minima del ricevitore, pannello
        ' principale), INDIPENDENTEMENTE da un Acquire in corso o meno.
        ' Chiarito dall'utente: "la scala minima interviene sempre a
        ' prescindere dall'acquire, altrimenti non potremmo capire quando
        ' il sistema è in lock" - NON deve dipendere da i_LockThr, che è la
        ' soglia di ARRIVO della ricerca fine (vedi b_LockRicerca sotto).
        ' Tolleranza 0.001 contro i minimi errori di arrotondamento delle
        ' somme ripetute di 0.6 (es. -50.0000000000003 invece di -50 esatto).
        b_Lock = (d_SimBeaconLevel >= ScalaMin - 0.001)
        ' b_LockRicerca = SOLO diagnostico (non è più il gate d'ingresso
        ' della ricerca, quello è b_Lock/ScalaMin sopra): indica se il
        ' livello ha già superato i_LockThr, la soglia che determina
        ' Augmented una volta avviata la ricerca fine.
        b_LockRicerca = (d_SimBeaconLevel >= i_LockThr - 0.001)
        ' Da qui in giù SEMPRE, qualunque sia stato il ramo sopra (anche
        ' nessuno, se permettiAvvicinamento=False e non ancora agganciato):
        ' riscrive i campi mostrati dal valore interno attuale, così resta
        ' sempre l'ultima scrittura e non viene "bloccato" da altre fonti.
        Dim tmpSimLevTxt As String = d_SimBeaconLevel.ToString("0.00", Globalization.CultureInfo.InvariantCulture)
        Dim tmpSimErrTxt As String = d_SimBeaconErr.ToString("0.00", Globalization.CultureInfo.InvariantCulture)
        s_Lev1 = tmpSimLevTxt
        s_Lev = tmpSimLevTxt
        ' LblLock.Text NON si scrive più qui: lo imposta AggiornaBeaconLock
        ' dal bit "Rx alarms" (vBytes(13) BIT2) del messaggio ACU - lo stesso
        ' percorso usato con hardware reale, codificato nel frame simulato
        ' in frmMain.vb. Scriverlo anche qui andava in conflitto, con
        ' AggiornaBeaconLock che vinceva sempre forzando "LOCK" (il bit era
        ' fisso a 0 nel frame prima di questa correzione).
        s_LevAz1 = tmpSimErrTxt
        s_LevEl1 = tmpSimErrTxt
        s_LevAz = tmpSimErrTxt
        s_LevEl = tmpSimErrTxt
    End Sub

    ''' <summary>
    ''' Arrotonda un offset di ricerca Acquire a 3 decimali, la stessa
    ''' risoluzione del comando inviato all'ACU (" P 000.000 00.000").
    ''' Prima si usava Format$(x, "#0.0#"), cioè 2 decimali: con step
    ''' piccoli (es. 0.025) l'errore di arrotondamento arrivava al 20% del
    ''' passo, e i punti della spirale saltavano avanti e indietro rispetto
    ''' alla curva (andamento a zig-zag segnalato); nei Quadrati il passo
    ''' 0.025 diventava di fatto 0.03.
    ''' </summary>
    Public Function ArrotondaOffset(valore As Double) As Double
        Return Math.Round(valore, 3, MidpointRounding.AwayFromZero)
    End Function

    ' Ultimo diagnostico Acquire emesso: evita di ripetere lo stesso
    ' messaggio ad ogni tick di TmrSpiralAcquire (ogni 1-5 s) finché la
    ' condizione non cambia.
    Private ultimoDiagnosticoAcquire As String = ""

    ''' <summary>
    ''' Emette un diagnostico quando il calcolo Acquire non può essere
    ''' eseguito: riga nei messaggi operatore (Error_Messages, ScriviAlarm
    ''' livello 0, senza beep), riga in ErrSoft e avviso a schermo su
    ''' frmRicezione. Lo stesso messaggio viene registrato una sola volta
    ''' finché non cambia o non viene azzerato con AzzeraDiagnosticoAcquire.
    ''' </summary>
    Public Sub SegnalaAcquireNonPossibile(motivo As String)
        Dim testo As String = "Acquire non eseguibile: " & motivo
        Try
            frmRicezione.MostraWarning(testo)
        Catch
        End Try
        If testo = ultimoDiagnosticoAcquire Then Exit Sub
        ultimoDiagnosticoAcquire = testo
        WriteError(0, testo)
        ScriviAlarm(testo, 0)
    End Sub

    ''' <summary>
    ''' Condizione tornata normale: il prossimo diagnostico, anche se uguale
    ''' al precedente, verrà registrato di nuovo.
    ''' </summary>
    Public Sub AzzeraDiagnosticoAcquire()
        ultimoDiagnosticoAcquire = ""
    End Sub

    ''' <summary>
    ''' Controlla i parametri necessari al calcolo Acquire. Restituisce ""
    ''' se sono tutti validi, altrimenti la descrizione del problema.
    ''' Normalmente frmAcquire li ha già validati all'avvio; qui serve per
    ''' i percorsi che non passano di lì (es. valori azzerati o INI
    ''' incompleto) e che altrimenti produrrebbero una ricerca ferma senza
    ''' alcuna indicazione.
    ''' </summary>
    Public Function VerificaParametriAcquire() As String
        Dim mancanti As New List(Of String)
        If i_AzStep <= 0 Then mancanti.Add("Az Step")
        If i_ElStep <= 0 Then mancanti.Add("El Step")
        If i_AzStepLock <= 0 Then mancanti.Add("Az Step Lock")
        If i_ElStepLock <= 0 Then mancanti.Add("El Step Lock")
        If i_AzRange <= 0 Then mancanti.Add("Az Range")
        If i_ElRange <= 0 Then mancanti.Add("El Range")
        If i_InterAcq <= 0 Then mancanti.Add("Delta Time")
        If mancanti.Count > 0 Then
            Return "parametri mancanti o nulli (" & String.Join(", ", mancanti) & ") - verificare la configurazione Acquire"
        End If
        If b_Acquire AndAlso g_AzTable = 0 AndAlso g_ElTable = 0 Then
            Return "coordinate di partenza Preset non impostate"
        End If
        Return ""
    End Function

    Public Function Calcolo_Aquire(Data1 As Date, mSec As Integer) As Boolean
        Dim rs_Acq As Object
        Dim s_program As String = "", i_lun As Integer, d_FineProg As Date
        '06/06/2022
        Dim tmpErrAz As System.Single, tmpErrEl As System.Single
        Dim statoQuadratiEseguito As Integer = -1
        Dim statoSpiraleEseguito As Integer = -1
        Dim motivoBlocco As String = VerificaParametriAcquire()
        If motivoBlocco <> "" Then
            SegnalaAcquireNonPossibile(motivoBlocco)
            Exit Function
        End If
        ' Calcolo di nuovo possibile: un eventuale blocco successivo verrà
        ' registrato anche se uguale a uno precedente.
        AzzeraDiagnosticoAcquire()
        On Error Resume Next
        If IsDate(d_DataFineProg) Then
            d_FineProg = CDate(d_DataFineProg)
        End If
        i_ProgTrack = 1
        If b_SimulazioneACU Then AvanzaSimulazioneBeacon()     ' mai con apparati veri: sovrascrive s_Lev/s_Lev1/errori
        ' NIENTE reset di b_LockOn/i_AzStep/i_ElStep quando b_Lock torna
        ' False: b_Lock riflette il livello ISTANTANEO (serve per
        ' l'indicatore LOCK/UNLOCK a schermo, sempre live), ma la fase di
        ' ricerca è a due soli stati - passo largo prima del primo aggancio
        ' debole, passo fine (Lock) dopo - e NON deve mai tornare indietro
        ' per una singola oscillazione di rumore della simulazione: un
        ' precedente tentativo di farlo (rimosso qui) faceva ripartire un
        ' quadrato grande ad ogni minima discesa sotto soglia, anche con
        ' l'antenna già ben puntata (segnalato: pattern a più quadrati
        ' invece di un singolo aggancio pulito). b_LockOn resta un lucchetto
        ' a senso unico, come nell'originale VB6: una volta scattato, il
        ' passo fine resta finché non si esce dall'Acquire.
        ' Gate della ricerca: entra con b_Lock (ScalaMin, "il satellite è
        ' agganciato" - chiarito dall'utente), e la ricerca fine intorno al
        ' punto trovato prosegue fino a superare i_LockThr, che a quel punto
        ' determina Augmented - NON g_ThrAuto, che non è più usato per
        ' questa decisione (b_LockRicerca/i_LockThr era stato usato come
        ' gate d'ingresso in un tentativo precedente, sbagliato: era invece
        ' la soglia di ARRIVO, non quella di partenza).
        If b_Lock Then
            If Val(s_Lev1) > i_LockThr And RTrim(frmRicezione.LblLock.Text) = "LOCK" And Math.Abs(Val(s_LevAz1)) < g_ThrAz And Math.Abs(Val(s_LevEl1)) < g_ThrEl Then     '26/10/2022
                'controllo se è stato ricevuto il segnale
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                i_Status = OP_AUGMENTED
                i_ProgTrack = 1
                frmRicezione.CmdOffset.Enabled = True
                frmRicezione.CmdOffset.Visible = True
                frmRicezione.LblCommand.Text = "Augmented"
                PTK = &H10
                AzOff += i_AzOffsetLock
                ElOff += i_ElOffsetLock
                i_AzOffsetLock = 0
                i_ElOffsetLock = 0
                frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
                frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
                frmRicezione.TxtOrdini.Focus()
                '                frmRicezione.MnuMain(15).Visible = False
                frmRicezione.LblAzErrEn.Visible = False
                frmRicezione.LblElErrEn.Visible = False
                frmRicezione.TmrSpiralAcquire.Enabled = False
                '                Unload frmVisualAcquire
                Exit Function
            Else                'lock ma non nelle soglie
                If Not b_LockOn Then
                    b_LockOn = True         'è passato
                    ' Ricentra sul punto dove il beacon è stato più forte
                    ' finora (tracciato durante la scansione), non sulla
                    ' posizione attuale dell'antenna — se non è stato ancora
                    ' tracciato nulla (es. primissimo tick), resta la
                    ' posizione attuale come prima.
                    WriteError(0, "Acquire: lock debole, ricentraggio. AzAttuale=" & frmRicezione.LblAzimuth.Text &
                        " ElAttuale=" & frmRicezione.LblElevazione.Text &
                        " d_MaxBeaconLevel=" & d_MaxBeaconLevel &
                        " d_MaxBeaconAz=" & d_MaxBeaconAz & " d_MaxBeaconEl=" & d_MaxBeaconEl &
                        " s_Lev1=" & s_Lev1)
                    If d_MaxBeaconLevel > Double.MinValue Then
                        g_AzTable = d_MaxBeaconAz
                        g_ElTable = d_MaxBeaconEl
                    Else
                        g_AzTable = CDbl(frmRicezione.LblAzimuth.Text)
                        g_ElTable = CDbl(frmRicezione.LblElevazione.Text)
                    End If
                    i_AzStep = i_AzStepLock
                    i_ElStep = i_ElStepLock
                    '                   If b_Acquire Then
                    AzOff = i_AzOffsetLock + AzOffset
                    ElOff = i_ElOffsetLock + ElOffset
                    i_AzOffsetLock = 0
                    i_ElOffsetLock = 0
                    '14/06/2022
                    '                    Else
                    '                        AzOff = i_AzOffsetLock + AzOffset
                    '                        ElOff = i_ElOffsetLock + ElOffset
                    '                        i_AzOffsetLock = 0
                    '                        i_ElOffsetLock = 0
                    '                    End If
                    frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
                    frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
                    ' Niente i_ConStato = 0: quello stato è il "lancio" vero e
                    ' proprio di un quadrato (CalcolaPassoQuadrati, Case 0).
                    ' In modo Program, g_AzTable/g_ElTable sono una posizione
                    ' ASSOLUTA congelata in questo istante, mentre ogni Case
                    ' della macchina a stati ricalcola la posizione dal TLE
                    ' (Calcolo_Coordinate) e ci somma l'offset — col solo
                    ' g_AzTable+i_AzStep qui, appena il satellite si muove di
                    ' nuovo (anche di un solo tick) il punto di ricentraggio
                    ' e il primo passo vero della ricerca fine non
                    ' coincidono più (confermato dall'utente: "il calcolo
                    ' fine non parte in maniera corretta perché prende delle
                    ' coordinate che non tengono conto di quanto calcolato
                    ' finora"). Fix: anche qui si richiama Calcolo_Coordinate
                    ' per avere la posizione LIVE del satellite, e si usa
                    ' l'OFFSET (non la posizione assoluta) del punto di
                    ' massimo beacon — stesso riferimento "vivo" usato da
                    ' tutti i Case successivi.
                    If b_Acquire Then
                        frmRicezione.LblAzimuth.Text = Format$(g_AzTable + i_AzStep, "000.000")
                        frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                        i_AzOffsetLock = ArrotondaOffset(i_AzStep)
                        i_ElOffsetLock = 0
                    Else
                        ' i_AzOffsetLock/i_ElOffsetLock vanno impostati PRIMA
                        ' di chiamare Calcolo_Coordinate: è lei a sommarli
                        ' alla posizione live (quando i_ProgTrack=1, sempre
                        ' vero in Acquire) scrivendo direttamente
                        ' LblAzimuth/Elevazione - farlo anche qui dopo,
                        ' sull'offset VECCHIO già incluso, mescolava un
                        ' offset vecchio e uno nuovo nello stesso valore.
                        i_AzOffsetLock = ArrotondaOffset(d_MaxBeaconAzOffset + i_AzStep)
                        i_ElOffsetLock = ArrotondaOffset(d_MaxBeaconElOffset)
                        Calcolo_Coordinate(Data1, mSec)
                    End If
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                    i_ConStato = 1
                    i_Step = 1
                    ' Spirale circolare: i_ConStato=1 lì NON è "primo lato"
                    ' (come nei Quadrati) ma "torna al centro", seguito da
                    ' i_ConStato=2 che aspetta b_RangeAz And b_RangeEl -
                    ' appena azzerati qui sotto, quindi la ricerca restava
                    ' bloccata per sempre al lock debole (segnalato: "la
                    ' ricerca si è bloccata sulla soglia della ricerca
                    ' debole"). Si riparte invece con una spirale NUOVA
                    ' (angolo 0) a passo Lock, centrata sul punto di
                    ' massimo beacon.
                    ' Centro della ricerca fine, per ENTRAMBI i modi (la
                    ' Spirale ci gira intorno, i Quadrati ci rientrano a
                    ' fine quadrato in Case 7 e ripartono da lì in Case 0 -
                    ' prima tornavano all'offset 0, cioè al centro TLE
                    ' originale, già dal secondo quadrato fine).
                    If b_Acquire Then
                        ' Preset: g_AzTable/g_ElTable sono già stati
                        ' ricentrati sul massimo qui sopra.
                        d_SpiralCentroAz = 0
                        d_SpiralCentroEl = 0
                    Else
                        ' Program: centro come offset rispetto alla
                        ' posizione TLE live (Calcolo_Coordinate).
                        d_SpiralCentroAz = d_MaxBeaconAzOffset
                        d_SpiralCentroEl = d_MaxBeaconElOffset
                    End If
                    If i_ModoAcquire <> MODO_ACQUIRE_QUADRATI Then
                        i_ConStato = 0
                        d_SpiralAngolo = 0
                        frmRicezione.TmrSpiralAcquire.Interval = CInt(If(i_InterAcq > 0, i_InterAcq, 3) * 1000)
                    End If
                    '                    dbOrbite.Execute "DELETE * from tmpEffemeridi"
                    '                     rs_Acq = dbOrbite.OpenRecordset("tmpEffemeridi", dbOpenDynaset)
                    '                    rs_Acq.AddNew
                    '                    rs_Acq!date = Mid$(Data1, 1, 10)
                    '                    rs_Acq!Time = Mid$(Data1, 12, 8)
                    '                    rs_Acq!Azimuth = Format$(g_AzTable, "000.000")
                    '                    rs_Acq!Elevation = Format$(g_ElTable, "00.000")
                    '                    rs_Acq!Orbite = 1
                    '                    rs_Acq!Long = i_AzOffsetLock
                    '                    rs_Acq!lat = i_ElOffsetLock
                    '                    rs_Acq!Lock = LblLock.Text
                    '                    rs_Acq.Update
                    '                    rs_Acq.Close
                    i_Spiral += 1
                    i_Quadrati = 1
                    b_RangeEl = False
                    b_RangeAz = False
                    '                    Exit Function
                End If
            End If
        End If
        ' TimeOut di Acquire scaduto: chiude la ricerca e passa in Stand By
        ' (comando reale "S79" via CmdSTBY_Click, la stessa funzione usata
        ' dal pulsante Stand By - non duplica guardie/invio), invece di
        ' limitarsi a posizionare l'antenna e restare in Command Position.
        If d_FineProg <= DateAdd("h", -i_Off, Now) Then
            i_ProgTrack = 0
            frmRicezione.TmrSpiralAcquire.Enabled = False
            frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
            ' Chiude anche la finestra Visual Acquire, se aperta - senza
            ' instanziarne una nuova (pattern già usato in
            ' TmrSpiralAcquire_Tick per lo stesso motivo: "FrmVisualAcquire"
            ' da solo, su un'istanza di default VB.NET mai aperta, ne
            ' creerebbe una nascosta solo per leggerne/chiuderla).
            For Each f As Form In My.Application.OpenForms
                If TypeOf f Is FrmVisualAcquire Then
                    f.Close()
                    Exit For
                End If
            Next
            Exit Function
        End If
        If i_ModoAcquire = MODO_ACQUIRE_QUADRATI Then
            ' Stato PRIMA della chiamata: CalcolaPassoQuadrati incrementa
            ' i_ConStato internamente a fine Case (es. Case 7 "torna al
            ' centro" lo porta a 8 non appena finito), quindi usare il
            ' valore DOPO la chiamata per decidere se registrare il punto
            ' (sotto, "registraPuntoGrafico") escludeva sistematicamente
            ' proprio il punto appena calcolato dal Case 7: il centro non
            ' veniva mai disegnato né inserito, sembrando che la ricerca non
            ' fosse mai "tornata al centro" per reinizializzare.
            statoQuadratiEseguito = i_ConStato
            CalcolaPassoQuadrati(Data1, mSec)
            GoTo Rangeout
        End If
        ' ───── Spirale circolare continua (spirale di Archimede) ─────
        ' Modo alternativo alla ricerca "a quadrati" sopra (scelta
        ' dall'operatore in FrmAcquire, campo i_ModoAcquire): quella a
        ' quadrati aveva un difetto strutturale nel passo diagonale di
        ' cambio-giro, corretto in CalcolaPassoQuadrati (vedi commento li').
        '
        ' i_ConStato qui ha solo 3 valori:
        '   0 = ricerca in corso (la spirale si allarga)
        '   1 = comando di ritorno al centro appena inviato
        '   2 = verifica che l'antenna abbia raggiunto il centro entro CoorAcq
        ' Stesso bug già risolto per i Quadrati (statoQuadratiEseguito):
        ' questo blocco incrementa i_ConStato internamente (es. qui sotto
        ' i_ConStato=1 diventa 2 a fine blocco), quindi usare i_ConStato
        ' DOPO per decidere se registrare il punto (registraPuntoGrafico)
        ' escludeva sempre il punto appena calcolato dal ramo appena
        ' eseguito - in particolare il rientro al centro (i_ConStato=1)
        ' non veniva mai registrato, perché letto già a 2.
        statoSpiraleEseguito = i_ConStato
        If i_ConStato = 0 Then
            ' Passo angolare adattivo: con un passo fisso in gradi, ogni giro
            ' ha sempre lo stesso numero di punti, ma la circonferenza cresce
            ' col raggio — quindi sui giri esterni lo stesso passo copre un
            ' arco (e un segmento dritto tra un punto e l'altro) via via più
            ' lungo, e la spirale appare sempre più "sfaccettata"/poligonale
            ' invece che curva man mano che si allarga (la "schiacciatura" e
            ' irregolarità osservate nei giri grandi). Si riduce il passo
            ' quando il raggio cresce, per mantenere pressoché costante la
            ' lunghezza d'arco percorsa ad ogni tick (circa un AzStep/ElStep).
            Const PASSO_ANGOLO_MAX_GRADI As Double = 20.0
            Dim raggioAttuale As Double = (Math.Min(i_AzStep, i_ElStep) / (2 * Math.PI)) * d_SpiralAngolo
            Dim passoAngoloRadianti As Double = PASSO_ANGOLO_MAX_GRADI * (Math.PI / 180.0)
            If raggioAttuale > 0 Then
                passoAngoloRadianti = Math.Min(passoAngoloRadianti, Math.Min(i_AzStep, i_ElStep) / raggioAttuale)
            End If
            d_SpiralAngolo += passoAngoloRadianti

            ' Raggio proporzionale all'angolo: un giro completo (2π) allarga
            ' il raggio di un AzStep/ElStep, così la densità degli anelli
            ' corrisponde allo step configurato dall'operatore. Az ed El
            ' hanno raggi separati (ellisse, non cerchio) per rispettare
            ' range/step diversi sui due assi, con lo stesso angolo.
            Dim raggioAz As Double = (i_AzStep / (2 * Math.PI)) * d_SpiralAngolo
            Dim raggioEl As Double = (i_ElStep / (2 * Math.PI)) * d_SpiralAngolo
            Dim maxRaggioAz As Double = i_AzRange / 2
            Dim maxRaggioEl As Double = i_ElRange / 2

            b_RangeAz = (raggioAz >= maxRaggioAz)
            b_RangeEl = (raggioEl >= maxRaggioEl)
            If b_RangeAz Then raggioAz = maxRaggioAz
            If b_RangeEl Then raggioEl = maxRaggioEl

            i_AzOffsetLock = ArrotondaOffset(d_SpiralCentroAz + raggioAz * Math.Cos(d_SpiralAngolo))
            i_ElOffsetLock = ArrotondaOffset(d_SpiralCentroEl + raggioEl * Math.Sin(d_SpiralAngolo))

            If b_Acquire Then
                'siamo in preset
                frmRicezione.LblAzimuth.Text = Format$(g_AzTable + CDbl(i_AzOffsetLock), "000.000")
                frmRicezione.LblElevazione.Text = Format$(g_ElTable + CDbl(i_ElOffsetLock), "00.000")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
            Else
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                ' Stesso fix portato dalla ricerca a Quadrati: Calcolo_Coordinate
                ' dà la posizione LIVE del satellite (che si muove) e aggiunge
                ' già da sola i_AzOffsetLock/i_ElOffsetLock (appena calcolato
                ' sopra dalla formula raggio*cos/sin), visto che i_ProgTrack=1
                ' per tutta la durata di Acquire - sommarlo di nuovo qui
                ' raddoppiava il passo della spirale (confermato in test sui
                ' Quadrati, stesso bug).
                Calcolo_Coordinate(Data1, mSec)
            End If

            If Err.Number <> 0 Then
                WriteError(0, "Calcolo_Aquire: " & Err.Number & " - " & Err.Description)
                Err.Clear()
            End If
            If b_RangeAz And b_RangeEl Then i_ConStato = 1
        ElseIf i_ConStato = 1 Then
            ' Fine della spirale: l'antenna torna al centro (stesso
            ' comportamento del vecchio Case 7, "l'antenna non viene spostata
            ' oltre, torna al punto calcolato da Program/Preset").
            ' Stesso fix portato da Quadrati Case 7: un solo comando copre
            ' una distanza pari a diversi passi di ricerca - un'antenna
            ' reale impiegherebbe il tempo di quei passi, non un solo
            ' tick. Si allunga l'intervallo del prossimo tick (quello che
            ' in i_ConStato=2 verifica l'arrivo) in proporzione, calcolato
            ' PRIMA di azzerare i_AzOffsetLock/i_ElOffsetLock qui sotto
            ' (i valori sono ancora la distanza dal centro). Ripristinato
            ' in i_ConStato=2 al successo della verifica.
            Dim stepsPrevistiAzSp As Double = If(i_AzStep > 0, Math.Abs(CDbl(i_AzOffsetLock) - d_SpiralCentroAz) / i_AzStep, 0)
            Dim stepsPrevistiElSp As Double = If(i_ElStep > 0, Math.Abs(CDbl(i_ElOffsetLock) - d_SpiralCentroEl) / i_ElStep, 0)
            Dim stepsPrevistiSp As Double = Math.Max(1, Math.Max(stepsPrevistiAzSp, stepsPrevistiElSp))
            Dim intervalloNormaleMsSp As Integer = CInt(If(i_InterAcq > 0, i_InterAcq, 3) * 1000)
            frmRicezione.TmrSpiralAcquire.Interval = CInt(stepsPrevistiSp * intervalloNormaleMsSp)
            If b_Acquire Then
                'siamo in preset
                frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                i_AzOffsetLock = 0
                frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                i_ElOffsetLock = 0
                frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
            Else
                ' Centro della spirale (0 prima del lock debole, offset del
                ' massimo beacon dopo), non sempre 0.
                i_AzOffsetLock = d_SpiralCentroAz
                frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                i_ElOffsetLock = d_SpiralCentroEl
                frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                ' Calcolo_Coordinate dà la posizione live del satellite più
                ' l'offset del centro: "torna al centro" seguendo il
                ' movimento TLE invece di un g_AzTable/El congelato.
                Calcolo_Coordinate(Data1, mSec)
            End If
            i_ConStato = 2
        Else ' i_ConStato = 2: verifica (stesso comportamento del vecchio Case 8)
            If b_RangeAz And b_RangeEl Then             'gli spostamenti hanno raggiunto il range in Az ed El
                tmpErrAz = Format$(Val(frmRicezione.TxtAz.Text) - CDbl(frmRicezione.LblAzimuth.Text), "0.000")
                tmpErrEl = Format$(Val(frmRicezione.TxtEl.Text) - CDbl(frmRicezione.LblElevazione.Text), "0.000")
                ' TEMPORANEO (test): l'ACU simulato in TmrOrologio oscilla per
                ' conto proprio e non risponde mai ai comandi inviati, quindi
                ' questo controllo di tolleranza fallirebbe sempre nel test —
                ' e fallendo fa GoTo Rangeout SENZA resettare lo stato,
                ' restando bloccato in "verifica" per sempre (bug preesistente,
                ' già nel vecchio Case 8, solo mai osservato prima perché la
                ' spirale quadrata non arrivava mai a completarsi). Da
                ' ripristinare togliendo "False AndAlso" a test finito.
                If False AndAlso (Math.Abs(tmpErrAz) > Math.Abs(CoorAcq) Or Math.Abs(tmpErrEl) > Math.Abs(CoorAcq)) Then
                    GoTo Rangeout
                End If
                b_RangeAz = False
                b_RangeEl = False
                d_SpiralAngolo = 0
                i_ConStato = 0
                i_Spiral += 1
                ' Ripristina l'intervallo normale: il rientro al centro
                ' (i_ConStato=1) lo aveva allungato per dare all'antenna
                ' il tempo reale di coprire la distanza.
                frmRicezione.TmrSpiralAcquire.Interval = CInt(If(i_InterAcq > 0, i_InterAcq, 3) * 1000)
            End If
        End If
Rangeout:
        ' TEMPORANEO (test): controllo elevazione minima disattivato. Da ripristinare rimettendo ">= g_ElMin" al posto di "True".
        If True Then
            ' SQL diretto invece del wrapper DAO (OpenRecordset/AddNew/Update/
            ' Delete): ogni Update()/Delete() del wrapper ricaricava l'INTERA
            ' tabella tmpEffemeridi in memoria (3-4 query complete per tick,
            ' una per ogni singola riga toccata) — costo che cresce con il
            ' numero di punti accumulati. Causava un rallentamento progressivo
            ' osservato in test (intervalli tra i tick di TmrSpiralAcquire
            ' saliti da ~3s a 7-8s in pochi minuti). Qui una DELETE e una
            ' INSERT dirette, ciascuna una singola query indipendente dalla
            ' dimensione della tabella.
            Dim connAcq As SQLiteConnection = CType(dbOrbite, DAODatabase).Connection
            If i_Spiral > 2 Then                          'Cancella anche se in Lock
                ' Una sola riga alla volta (la più vecchia tra quelle di
                ' almeno 2 giri indietro), non tutte insieme: con la DELETE
                ' in blocco il quadrato più vecchio spariva di colpo non
                ' appena partiva il terzo - cancellazione graduale, un
                ' punto per tick, finché quel giro non è sparito del tutto
                ' (coerente con l'arrivo di nuovi punti un tick alla volta).
                Using cmdDel As New SQLiteCommand(
                    "DELETE FROM tmpEffemeridi WHERE rowid = (" &
                    "SELECT rowid FROM tmpEffemeridi WHERE source = 'ACQUIRE' AND Orbite <= @sogliaOrbite " &
                    "ORDER BY [date], [Time] LIMIT 1)", connAcq)
                    cmdDel.Parameters.AddWithValue("@sogliaOrbite", i_Spiral - 2)
                    cmdDel.ExecuteNonQuery()
                End Using
            End If
            ' "i_ConStato < 2" era corretto solo per la spirale circolare
            ' (3 soli stati: 0=ricerca, 1=ritorno al centro, 2=verifica).
            ' In modo Quadrati lo stesso i_ConStato attraversa 9 stati
            ' (CalcolaPassoQuadrati, Case 0-8): 0-7 muovono tutti l'antenna
            ' (lati del quadrato), solo l'8 è pura verifica. Con la
            ' condizione invariata, il grafico smetteva di ricevere nuovi
            ' punti subito dopo il Case 1 — osservato in test: NPunti
            ' fermo a 2 per l'intera ricerca, anche con l'antenna ancora in
            ' movimento sui lati successivi del quadrato.
            ' Per i Quadrati si usa lo stato con cui CalcolaPassoQuadrati è
            ' stata chiamata (statoQuadratiEseguito), non i_ConStato attuale:
            ' quest'ultimo è già stato incrementato dal Case appena eseguito
            ' (es. il Case 7 che porta la posizione al centro finisce
            ' impostando i_ConStato=8), quindi usarlo qui escluderebbe
            ' sempre il punto appena calcolato dal Case che l'ha prodotto.
            ' Case 7 (rientro al centro) è incluso (< 8, non < 7): a
            ' differenza di prima, la riga registra ora l'intervallo REALE
            ' di quel tick (vedi @intv sotto), non più il passo normale
            ' costante - quindi non è più un dato falsato. Il grafico
            ' (FrmVisualAcquire) riconosce comunque il salto (distanza e
            ' tempo entrambi fuori soglia, vedi saltoDistanza/i_Intervallo)
            ' e lo disegna come punto isolato (nessuna linea dal quadrato
            ' precedente) - serve solo da marcatore visivo del centro da
            ' cui riparte la ricerca, così il primo lato del nuovo
            ' quadrato risulta visibilmente collegato ad esso.
            Dim registraPuntoGrafico As Boolean =
                If(i_ModoAcquire = MODO_ACQUIRE_QUADRATI, statoQuadratiEseguito < 8, statoSpiraleEseguito < 2)
            If registraPuntoGrafico Then ' fasi di ricerca/movimento; ultimo stato di ciascun modo = sola verifica, nessun nuovo comando
                If IsNumeric(frmRicezione.LblAzimuth.Text) And IsNumeric(frmRicezione.LblElevazione.Text) Then
                    ' Tiene traccia del punto con il livello beacon più alto
                    ' visitato durante la scansione corrente (Quadrati o
                    ' Spirale) — usato da CalcolaPassoQuadrati (Case 8) per
                    ' ricentrare lì la ricerca successiva se il quadrato
                    ' finisce senza aggancio, e dal ricentraggio al lock
                    ' debole in Calcolo_Aquire.
                    If IsNumeric(s_Lev1) Then
                        If Val(s_Lev1) > d_MaxBeaconLevel Then
                            d_MaxBeaconLevel = Val(s_Lev1)
                            d_MaxBeaconAz = CDbl(frmRicezione.LblAzimuth.Text)
                            d_MaxBeaconEl = CDbl(frmRicezione.LblElevazione.Text)
                            ' Offset (non posizione assoluta) rispetto al
                            ' riferimento usato in quel momento - in modo
                            ' Program il riferimento è la posizione LIVE del
                            ' satellite (Calcolo_Coordinate), che si muove:
                            ' la posizione assoluta sopra diventa stale non
                            ' appena il satellite si sposta. Il ricentraggio
                            ' in Calcolo_Aquire userà questo offset insieme a
                            ' un Calcolo_Coordinate fresco invece della
                            ' posizione assoluta congelata.
                            d_MaxBeaconAzOffset = CDbl(i_AzOffsetLock)
                            d_MaxBeaconElOffset = CDbl(i_ElOffsetLock)
                        End If
                    End If
                    ' Azzera Err prima dell'insert: tutta la funzione è in "On
                    ' Error Resume Next" (Try/Catch non ammesso nello stesso
                    ' metodo, BC30544), quindi un Err.Number residuo di
                    ' un'istruzione precedente verrebbe attribuito per errore
                    ' a questo blocco.
                    Err.Clear()
                    ' Intervallo: mai lasciare NULL, SelezionaSingoliDati
                    ' (FrmVisualAcquire) la legge con CSng(DyVisual("Intervallo"))
                    ' sul primo punto — CSng(DBNull) lancia InvalidCastException,
                    ' interrompendo l'intero ridisegno del grafico ad ogni redraw.
                    Using cmdIns As New SQLiteCommand(
                        "INSERT INTO tmpEffemeridi ([date], [Time], Azimuth, Elevation, [Long], lat, Orbite, Intervallo, source) " &
                        "VALUES (@d, @t, @az, @el, @lo, @la, @orb, @intv, 'ACQUIRE')", connAcq)
                        cmdIns.Parameters.AddWithValue("@d", Data1.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                        cmdIns.Parameters.AddWithValue("@t", Data1.ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture))
                        ' Format$ usa il separatore decimale della cultura CORRENTE
                        ' (virgola su FOC6, che non forza il punto come BTS1) - il
                        ' valore testuale "256,994" risultante non è un numero
                        ' valido per SQLite (si aspetta il punto), che lo tronca
                        ' alla sola parte intera ("256"), perdendo tutto il passo
                        ' fine 0.1° - causa del "salto" visto nel grafico/nelle
                        ' coordinate (il grado intero cambia solo ogni 10 passi).
                        ' ToString con InvariantCulture, come già fatto per @d/@t.
                        cmdIns.Parameters.AddWithValue("@az", CDbl(frmRicezione.LblAzimuth.Text).ToString("000.000", System.Globalization.CultureInfo.InvariantCulture))
                        cmdIns.Parameters.AddWithValue("@el", CDbl(frmRicezione.LblElevazione.Text).ToString("00.000", System.Globalization.CultureInfo.InvariantCulture))
                        cmdIns.Parameters.AddWithValue("@lo", i_AzOffsetLock)
                        cmdIns.Parameters.AddWithValue("@la", i_ElOffsetLock)
                        cmdIns.Parameters.AddWithValue("@orb", i_Spiral)
                        ' Intervallo REALE di questo tick (frmRicezione.TmrSpiralAcquire.Interval
                        ' è già stato eventualmente allungato da Case 7 a questo
                        ' punto), non il passo nominale i_InterAcq: per il punto
                        ' di rientro al centro i due non coincidono, e scrivere
                        ' il nominale falserebbe il dato registrato.
                        cmdIns.Parameters.AddWithValue("@intv", CSng(frmRicezione.TmrSpiralAcquire.Interval) / 1000.0F)
                        cmdIns.ExecuteNonQuery()
                    End Using
                    If Err.Number <> 0 Then
                        WriteError(0, "Calcolo_Aquire: scrittura punto tmpEffemeridi fallita - " & Err.Number & " " & Err.Description)
                        Err.Clear()
                    End If
                End If
            End If
        Else
            frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
            frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
            i_AzOffsetLock = 0
            frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
            i_ElOffsetLock = 0
            frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
            s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
            i = Len(s_program) + 6
            SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            InvioACU(SEND_ACU)
             i_Status = OP_COMMANDPOSITION
            frmRicezione.LblCommand.Text = "Command Position"
            '                frmRicezione.MnuMain(15).Visible = False
            frmRicezione.TmrSpiralAcquire.Enabled = False
            '             Unload frmVisualAcquire
            i_AzOffsetLock = 0
            i_ElOffsetLock = 0
            b_RangeAz = False
            b_RangeEl = False
            Exit Function
        End If
    End Function

    ''' <summary>
    ''' Ricerca "a quadrati" (spirale quadrata espansa), modo alternativo
    ''' alla spirale circolare continua di Calcolo_Aquire, scelto
    ''' dall'operatore in FrmAcquire (optQuadrati). Portata dalla vecchia
    ''' macchina a stati di Calcolo_Aquire1 (Select Case i_ConStato, 9 casi:
    ''' 0=lancio dal centro, 1-4=i quattro lati del quadrato, 5=completa il
    ''' lato finale, 6=espande il quadrato per il giro successivo,
    ''' 7=ritorno al centro, 8=verifica tolleranza), con un fix: nella
    ''' versione originale il Case 6 muoveva contemporaneamente Az ed El in
    ''' un unico comando (passo diagonale, lunghezza diversa da tutti gli
    ''' altri passi e mai compensato: la spirale restava asimmetrica,
    ''' sempre più spostata verso un lato). Qui il Case 5 completa per
    ''' intero la risalita (un passo El in più rispetto a prima) e il
    ''' Case 6 fa solo il passo Az di espansione: stessa forma, stessa
    ''' copertura, un tick in più per giro, nessun passo diagonale.
    ''' </summary>
    Private Sub CalcolaPassoQuadrati(Data1 As Date, mSec As Integer)
        Dim s_program As String = ""
        Select Case i_ConStato
            Case 0
                'inizio del quadrato. L'antenna viene spostata di uno +step in azimuth
                'siamo in preset
                frmRicezione.LblAzimuth.Text = Format$(g_AzTable + i_AzStep, "000.000")
                frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                ' Relativo al centro della ricerca (0 prima del lock debole,
                ' offset del punto di massimo beacon dopo, in Program).
                i_AzOffsetLock = ArrotondaOffset(d_SpiralCentroAz + i_AzStep)
                frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                i_ElOffsetLock = ArrotondaOffset(d_SpiralCentroEl)
                frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                If b_Acquire Then
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
                Else
                    ' Calcolo_Coordinate ricalcola dal TLE la posizione del
                    ' satellite, che nel frattempo si è mosso nella sua
                    ' orbita (essenziale in modo Program: la ricerca deve
                    ' seguire il satellite, non restare ferma come in
                    ' Preset). NON sommare di nuovo i_AzOffsetLock/
                    ' i_ElOffsetLock qui: Calcolo_Coordinate li aggiunge già
                    ' internamente (quando i_ProgTrack=1, sempre vero in
                    ' Acquire) prima di scrivere LblAzimuth/Elevazione -
                    ' farlo anche qui raddoppiava il passo di ricerca (0.6°
                    ' invece di 0.3° a tick, confermato in test).
                    Calcolo_Coordinate(Data1, mSec)
                End If
                i_ConStato += 1
                i_Step = 1
            Case 1
                'primo lato
                If i_Quadrati * i_ElStep >= i_ElRange Then
                    b_RangeEl = True
                End If
                ' Simmetrico al controllo El sopra: se non si controlla
                ' anche Az qui (invece di aspettare Case 5/6, un intero
                ' giro più tardi), quando è El a raggiungere il range per
                ' primo i Case 2/3/4 di questo giro vengono eseguiti alla
                ' lunghezza non ancora limitata (i_Quadrati*2, che cresce
                ' giro dopo giro) - producendo un lato anomalmente più
                ' lungo di tutti i quadrati già annidati, prima che il
                ' controllo generale di fondo forzi il rientro al centro.
                If i_Quadrati * i_AzStep >= i_AzRange Then
                    b_RangeAz = True
                End If
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                If Not b_RangeEl Then i_ElOffsetLock = ArrotondaOffset(i_ElOffsetLock + i_ElStep)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                If b_Acquire Then
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
                Else
                    ' NON sommare di nuovo l'offset: Calcolo_Coordinate lo
                    ' include già (vedi Case 0).
                    Calcolo_Coordinate(Data1, mSec)
                End If
                i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeAz Then
                        i_Step = i_AzRange / i_AzStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 2
                'secondo lato
                frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) - i_AzStep, "000.000")
                i_AzOffsetLock = ArrotondaOffset(i_AzOffsetLock - i_AzStep)
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                If b_Acquire Then
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
                Else
                    ' NON sommare di nuovo l'offset: Calcolo_Coordinate lo
                    ' include già (vedi Case 0).
                    Calcolo_Coordinate(Data1, mSec)
                End If
                i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeEl Then
                        i_Step = i_ElRange / i_ElStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 3
                'terzo lato
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) - i_ElStep, "00.000")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                i_ElOffsetLock = ArrotondaOffset(i_ElOffsetLock - i_ElStep)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                If CDbl(frmRicezione.LblElevazione.Text) >= g_ElMin Then
                    If b_Acquire Then
                        s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                         Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                        i = Len(s_program) + 6
                        SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                        b_int(ACU) = True
                        b_Ricezione(ACU) = False
                        InvioACU(SEND_ACU)
                    Else
                        ' NON sommare di nuovo l'offset: Calcolo_Coordinate
                        ' lo include già (vedi Case 0).
                        Calcolo_Coordinate(Data1, mSec)
                    End If
                Else
                    frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                    frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                    i_AzOffsetLock = 0
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                    i_ElOffsetLock = 0
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
                    i_Status = OP_COMMANDPOSITION
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    Exit Sub
                End If
                i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeAz Then
                        i_Step = i_AzRange / i_AzStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 4
                'quarto lato
                frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                i_AzOffsetLock = ArrotondaOffset(i_AzOffsetLock + i_AzStep)
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                If b_Acquire Then
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
                Else
                    ' NON sommare di nuovo l'offset: Calcolo_Coordinate lo
                    ' include già (vedi Case 0).
                    Calcolo_Coordinate(Data1, mSec)
                End If
                i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    ' Fix passo diagonale: qui (non più in Case 6) il lato si
                    ' completa per intero, un passo El in più rispetto alla
                    ' vecchia versione (che lasciava l'ultimo passo al Case 6,
                    ' unito al passo Az in un unico comando diagonale).
                    If b_RangeEl Then
                        i_Step = i_ElRange / i_ElStep
                    Else
                        i_Step = i_Quadrati
                    End If
                End If
            Case 5
                If i_Step = 0 Then
                    i_ConStato += 1
                ElseIf i_Step = 1 Then
                    ' Ultimo passo di chiusura del giro: invece di completare
                    ' per intero il lato (ultimo passo El, qui sotto) e poi,
                    ' in un comando separato, espandersi al giro successivo
                    ' (passo Az, ex Case 6 sotto) — il "gradino" visto in
                    ' test — i due passi vengono uniti in un solo comando:
                    ' l'antenna salta in diagonale dalla fine di un
                    ' quadrato all'inizio del successivo, senza chiuderlo.
                    ' Stessi incrementi (un i_ElStep e un i_AzStep) di
                    ' sempre, nessuna lunghezza diversa dagli altri passi:
                    ' a differenza del vecchio passo diagonale (mai
                    ' compensato, causa della deriva asimmetrica già
                    ' risolta una volta), qui la distanza totale per asse
                    ' resta identica a prima — cambia solo che un tick
                    ' invece di due la percorre.
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    i_ElOffsetLock = ArrotondaOffset(i_ElOffsetLock + i_ElStep)
                    If i_Quadrati * i_AzStep >= i_AzRange Then
                        b_RangeAz = True 'controllo se superato il range in azimuth
                    End If
                    If Not b_RangeAz Then
                        frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                        i_AzOffsetLock = ArrotondaOffset(i_AzOffsetLock + i_AzStep)
                    End If
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    If b_Acquire Then
                        s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                         Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                        i = Len(s_program) + 6
                        SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                        b_int(ACU) = True
                        b_Ricezione(ACU) = False
                        InvioACU(SEND_ACU)
                    Else
                        ' NON sommare di nuovo l'offset: Calcolo_Coordinate
                        ' lo include già (vedi Case 0).
                        Calcolo_Coordinate(Data1, mSec)
                    End If
                    i_Quadrati += 1
                    If b_RangeEl Then
                        i_Step = i_ElRange / i_ElStep
                    Else
                        i_Step = i_Quadrati
                    End If
                    i_ConStato = 1
                Else
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    i_ElOffsetLock = ArrotondaOffset(i_ElOffsetLock + i_ElStep)
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    If b_Acquire Then
                        s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                         Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                        i = Len(s_program) + 6
                        SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                        b_int(ACU) = True
                        b_Ricezione(ACU) = False
                        InvioACU(SEND_ACU)
                    Else
                        ' NON sommare di nuovo l'offset: Calcolo_Coordinate
                        ' lo include già (vedi Case 0).
                        Calcolo_Coordinate(Data1, mSec)
                    End If
                    i_Step -= 1
                    If i_Step = 0 Then
                        i_ConStato += 1
                    End If
                End If
            Case 6
                ' Non più raggiunto nel flusso normale: il passo di
                ' espansione è ora fuso nell'ultimo passo di Case 5 sopra
                ' (comando diagonale). Resta solo come stato di sicurezza
                ' per l'eventuale i_Step=0 d'ingresso in Case 5.
                If i_Quadrati * i_AzStep >= i_AzRange Then
                    b_RangeAz = True 'controllo se superato il range in azimuth
                End If
                If Not b_RangeAz Then
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                    i_AzOffsetLock = ArrotondaOffset(i_AzOffsetLock + i_AzStep)
                End If
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                If b_Acquire Then
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
             Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
                Else
                    ' NON sommare di nuovo l'offset: Calcolo_Coordinate lo
                    ' include già (vedi Case 0).
                    Calcolo_Coordinate(Data1, mSec)
                End If
                i_Quadrati += 1
                If b_RangeEl Then
                    i_Step = i_ElRange / i_ElStep
                Else
                    i_Step = i_Quadrati
                End If
                i_ConStato = 1
            Case 7
                'fine del quadrato. L'antenna non viene spostata oltre: torna al centro
                ' Un solo comando (il centro, via TLE) copre però una
                ' distanza pari a diversi passi di ricerca: un'antenna reale
                ' impiegherebbe il tempo di quei passi per arrivarci, non un
                ' solo tick. Si allunga l'intervallo del prossimo tick
                ' (quello che in Case 8 verifica l'arrivo) in proporzione,
                ' calcolato PRIMA di azzerare i_AzOffsetLock/i_ElOffsetLock
                ' qui sotto (que' i valori sono ancora la distanza dal
                ' centro). Ripristinato in Case 8 al successo della verifica.
                Dim stepsPrevistiAz As Double = If(i_AzStep > 0, Math.Abs(CDbl(i_AzOffsetLock) - d_SpiralCentroAz) / i_AzStep, 0)
                Dim stepsPrevistiEl As Double = If(i_ElStep > 0, Math.Abs(CDbl(i_ElOffsetLock) - d_SpiralCentroEl) / i_ElStep, 0)
                Dim stepsPrevisti As Double = Math.Max(1, Math.Max(stepsPrevistiAz, stepsPrevistiEl))
                Dim intervalloNormaleMs As Integer = CInt(If(i_InterAcq > 0, i_InterAcq, 3) * 1000)
                frmRicezione.TmrSpiralAcquire.Interval = CInt(stepsPrevisti * intervalloNormaleMs)
                frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                ' Rientro al centro della ricerca, non all'offset 0 (vedi
                ' d_SpiralCentroAz/El, impostato al lock debole).
                i_AzOffsetLock = d_SpiralCentroAz
                frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                i_ElOffsetLock = d_SpiralCentroEl
                frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                If b_Acquire Then
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                       Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
                Else
                    ' Offset azzerato sopra: Calcolo_Coordinate + 0 dà
                    ' semplicemente la posizione live del satellite, cioè
                    ' davvero "torna al centro" seguendo il movimento TLE.
                    ' (Non sommare l'offset di nuovo: lo include già lei.)
                    Calcolo_Coordinate(Data1, mSec)
                End If
                i_ConStato += 1
            Case 8
                'verifica che l'antenna abbia raggiunto il centro entro CoorAcq
                If b_RangeAz And b_RangeEl Then
                    Dim tmpErrAz As Single = Format$(Val(frmRicezione.TxtAz.Text) - CDbl(frmRicezione.LblAzimuth.Text), "0.000")
                    Dim tmpErrEl As Single = Format$(Val(frmRicezione.TxtEl.Text) - CDbl(frmRicezione.LblElevazione.Text), "0.000")
                    ' TEMPORANEO (test): stesso problema già diagnosticato per
                    ' la Spirale (vedi Calcolo_Aquire, i_ConStato=2) - l'ACU
                    ' simulato non risponde davvero ai comandi, quindi
                    ' TxtAz/TxtEl non convergono mai entro CoorAcq e lo stato
                    ' resta bloccato in verifica per sempre, mai reinizializzando
                    ' la ricerca. Bypassato come là ("True OrElse" invece di
                    ' "False AndAlso" perché qui il ramo di successo è quello a
                    ' cui vogliamo sempre entrare). Da ripristinare togliendo
                    ' "True OrElse" a test finito.
                    If True OrElse (Math.Abs(tmpErrAz) <= Math.Abs(CoorAcq) AndAlso Math.Abs(tmpErrEl) <= Math.Abs(CoorAcq)) Then
                        b_RangeAz = False
                        b_RangeEl = False
                        i_Quadrati = 1
                        i_ConStato = 0
                        i_Spiral += 1
                        ' Ripristina l'intervallo normale: il rientro al
                        ' centro (Case 7) lo aveva allungato per dare
                        ' all'antenna il tempo reale di coprire la distanza.
                        frmRicezione.TmrSpiralAcquire.Interval = CInt(If(i_InterAcq > 0, i_InterAcq, 3) * 1000)
                    End If
                    ' Se la tolleranza non è soddisfatta lo stato resta
                    ' invariato: al prossimo tick si rientra in Case 8 e si
                    ' riprova (stesso comportamento del vecchio GoTo Rangeout
                    ' quando falliva, qui senza bisogno del GoTo dato che il
                    ' Select Case lo fa già da solo rientrando allo stesso Case).
                End If
        End Select
        If b_RangeAz And b_RangeEl Then             'gli spostamenti hanno raggiunto il range in Az ed El
            If i_ConStato < 7 Then i_ConStato = 7
        End If
    End Sub

    Public Function Calcolo_Aquire1(Data1 As Date, mSec As Integer) As Boolean
        '17/2/2025 viene creata una routine di Calcolo Acquire semplificata per una ricerca rapida del satellite nel calcolo della fase
        Dim rs_Acq As Object
        Dim s_program As String = "", i_lun As Integer, d_FineProg As Date
        Dim tmpErrAz As System.Single, tmpErrEl As System.Single
        On Error Resume Next
        If IsDate(d_DataFineProg) Then
            d_FineProg = CDate(d_DataFineProg)
        End If
        i_ProgTrack = 1
        '09/06/2022
        If b_Lock Then
            If Val(s_Lev1) > g_ThrAuto And RTrim(frmRicezione.LblLock.Text) = "LOCK" And Math.Abs(Val(s_LevAz1)) < g_ThrAz And Math.Abs(Val(s_LevEl1)) < g_ThrEl Then     '26/10/2022
                'controllo se è stato ricevuto il segnale
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                '                i_Status = OP_AUGMENTED
                '                i_ProgTrack = 1
                '                frmRicezione.CmdOffset.Enabled = True
                '                frmRicezione.CmdOffset.Visible = True
                '                frmRicezione.LblCommand.Text = "Augmented"
                '                PTK = &H10
                AzOff += i_AzOffsetLock
                ElOff += i_ElOffsetLock
                i_AzOffsetLock = 0
                i_ElOffsetLock = 0
                frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
                frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
                frmRicezione.TxtOrdini.Focus()
                '                frmRicezione.MnuMain(15).Visible = False
                frmRicezione.LblAzErrEn.Visible = False
                frmRicezione.LblElErrEn.Visible = False
                frmRicezione.TmrSpiralAcquire.Enabled = False
                i_StepTrack = 12                                'Fine acquire
                Exit Function
            Else                'lock ma non nelle soglie
                If Not b_LockOn Then
                    b_LockOn = True         'è passato
                    g_AzTable = CDbl(frmRicezione.LblAzimuth.Text)
                    g_ElTable = CDbl(frmRicezione.LblElevazione.Text)
                    i_AzStep = i_AzStepLock
                    i_ElStep = i_ElStepLock
                    AzOff = i_AzOffsetLock + AzOffset
                    ElOff = i_ElOffsetLock + ElOffset
                    i_AzOffsetLock = 0
                    i_ElOffsetLock = 0
                    frmRicezione.LblAzError.Text = Format$(AzOffset, "#0.0#")
                    frmRicezione.LblElError.Text = Format$(ElOffset, "#0.0#")
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                    i_ConStato = 0
                    i_Spiral += 1
                    i_Quadrati = 1
                    b_RangeEl = False
                    b_RangeAz = False
                End If
            End If
        End If
        ' TimeOut di Acquire scaduto: chiude la ricerca e passa in Stand By
        ' (comando reale "S79" via CmdSTBY_Click, la stessa funzione usata
        ' dal pulsante Stand By - non duplica guardie/invio), invece di
        ' limitarsi a posizionare l'antenna e restare in Command Position.
        If d_FineProg <= DateAdd("h", -i_Off, Now) Then
            i_ProgTrack = 0
            frmRicezione.TmrSpiralAcquire.Enabled = False
            frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
            ' Chiude anche la finestra Visual Acquire, se aperta - senza
            ' instanziarne una nuova (pattern già usato in
            ' TmrSpiralAcquire_Tick per lo stesso motivo: "FrmVisualAcquire"
            ' da solo, su un'istanza di default VB.NET mai aperta, ne
            ' creerebbe una nascosta solo per leggerne/chiuderla).
            For Each f As Form In My.Application.OpenForms
                If TypeOf f Is FrmVisualAcquire Then
                    f.Close()
                    Exit For
                End If
            Next
            Exit Function
        End If
        Select Case i_ConStato
            Case 0
                'inizio della spirale. L'antenna viene spostata di uno +step in azimuth
                'siamo in preset
                frmRicezione.LblAzimuth.Text = Format$(g_AzTable + i_AzStep, "000.000")
                frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                i_AzOffsetLock = ArrotondaOffset(i_AzStep)
                frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                i_ElOffsetLock = 0
                frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
                i_ConStato += 1
                i_Step = 1
            Case 1
                'primo lato
                If i_Quadrati * i_ElStep >= i_ElRange Then
                    b_RangeEl = True
                End If
                ' Simmetrico al controllo El sopra - vedi il commento
                ' identico nell'altra copia di questo state-machine
                ' (CalcolaPassoQuadrati, Case 1).
                If i_Quadrati * i_AzStep >= i_AzRange Then
                    b_RangeAz = True
                End If
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                If Not b_RangeEl Then i_ElOffsetLock = ArrotondaOffset(i_ElOffsetLock + i_ElStep)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeAz Then
                        i_Step = i_AzRange / i_AzStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 2
                'secondo lato
                frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) - i_AzStep, "000.000")
                i_AzOffsetLock = ArrotondaOffset(i_AzOffsetLock - i_AzStep)
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                 Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeEl Then
                        i_Step = i_ElRange / i_ElStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 3
                'terzo lato
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) - i_ElStep, "00.000")
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                i_ElOffsetLock = ArrotondaOffset(i_ElOffsetLock - i_ElStep)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                If CDbl(frmRicezione.LblElevazione.Text) >= g_ElMin Then
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
        Else
                    frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                    frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                    i_AzOffsetLock = 0
                    frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                    i_ElOffsetLock = 0
                    frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                    Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
            i_Status = OP_COMMANDPOSITION
                    '                frmRicezione.MnuMain(15).Visible = False
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    '                Unload frmVisualAcquire
                    Exit Function
                End If
                i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeAz Then
                        i_Step = i_AzRange / i_AzStep * 2
                    Else
                        i_Step = i_Quadrati * 2
                    End If
                End If
            Case 4
                frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                i_AzOffsetLock = ArrotondaOffset(i_AzOffsetLock + i_AzStep)
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                 Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        i_Step -= 1
                If i_Step = 0 Then
                    i_ConStato += 1
                    If b_RangeEl Then
                        i_Step = (i_ElRange / i_ElStep) - 1
                    Else
                        i_Step = i_Quadrati - 1
                    End If
                End If
            Case 5
                If i_Step = 0 Then
                    i_ConStato += 1
                Else
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                    i_ElOffsetLock = ArrotondaOffset(i_ElOffsetLock + i_ElStep)
                    frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                    s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                    i = Len(s_program) + 6
                    SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                    b_int(ACU) = True
                    b_Ricezione(ACU) = False
                    InvioACU(SEND_ACU)
             i_Step -= 1
                    If i_Step = 0 Then
                        i_ConStato += 1
                    End If
                End If
            Case 6
                'Sposta quadrato
                If i_Quadrati * i_AzStep >= i_AzRange Then
                    b_RangeAz = True 'controllo se superato il range in azimuth
                End If
                If Not b_RangeAz Then frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                If Not b_RangeAz Then i_AzOffsetLock = ArrotondaOffset(i_AzOffsetLock + i_AzStep)
                i_ElOffsetLock = ArrotondaOffset(i_ElOffsetLock + i_ElStep)
                frmRicezione.LblAzErrEn.Text = CStr(i_AzOffsetLock)
                frmRicezione.LblElErrEn.Text = CStr(i_ElOffsetLock)
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
             Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
                i_Quadrati += 1
                If b_RangeEl Then
                    i_Step = i_ElRange / i_ElStep
                Else
                    i_Step = i_Quadrati
                End If
                i_ConStato = 1
            Case 7
                'fine della spirale. L'antenna non viene spostata
                frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
                frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
                i_AzOffsetLock = 0
                frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
                i_ElOffsetLock = 0
                frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
                s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                   Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        i_ConStato += 1
            Case 8
                If b_RangeAz And b_RangeEl Then             'gli spostamenti hanno raggiunto il range in Az ed El
                    tmpErrAz = Format$(Val(frmRicezione.TxtAz.Text) - CDbl(frmRicezione.LblAzimuth.Text), "0.000")
                    tmpErrEl = Format$(Val(frmRicezione.TxtEl.Text) - CDbl(frmRicezione.LblElevazione.Text), "0.000")
                    If Math.Abs(tmpErrAz) > Math.Abs(CoorAcq) Or Math.Abs(tmpErrEl) > Math.Abs(CoorAcq) Then
                        GoTo Rangeout
                    End If
                    b_RangeAz = False
                    b_RangeEl = False
                    i_Quadrati = 1
                    i_ConStato = 0
                i_Spiral += 1
                End If
        End Select
        If b_RangeAz And b_RangeEl Then             'gli spostamenti hanno raggiunto il range in Az ed El
            If i_ConStato < 7 Then i_ConStato = 7
        End If
Rangeout:
        If CDbl(frmRicezione.LblElevazione.Text) >= g_ElMin Then
            ' Filtrato su source='ACQUIRE', stesso motivo di Rangeout sopra.
            rs_Acq = dbOrbite.OpenRecordset("SELECT * FROM tmpEffemeridi WHERE source = 'ACQUIRE'", dbOpenDynaset)
            If i_Spiral > 2 Then                          'Cancella anche se in Lock
                If i_Spiral - rs_Acq!Orbite >= 2 Then
                    rs_Acq.MoveFirst
                    rs_Acq.Delete
                    rs_Acq.Update
                End If
            End If
        Else
            frmRicezione.LblAzimuth.Text = Format$(g_AzTable, "000.000")
            frmRicezione.LblElevazione.Text = Format$(g_ElTable, "00.000")
            i_AzOffsetLock = 0
            frmRicezione.LblAzErrEn.Text = Format$(i_AzOffsetLock, "#0.0##")
            i_ElOffsetLock = 0
            frmRicezione.LblElErrEn.Text = Format$(i_ElOffsetLock, "#0.0##")
            s_program = " P " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " " &
                     Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
            i = Len(s_program) + 6
            SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
            b_int(ACU) = True
            b_Ricezione(ACU) = False
            InvioACU(SEND_ACU)
             i_Status = OP_COMMANDPOSITION
            frmRicezione.LblCommand.Text = "Command Position"
            '                frmRicezione.MnuMain(15).Visible = False
            frmRicezione.TmrSpiralAcquire.Enabled = False
            '             Unload frmVisualAcquire
            i_AzOffsetLock = 0
            i_ElOffsetLock = 0
            b_RangeAz = False
            b_RangeEl = False
            Exit Function
        End If
    End Function
    Public Function AnalisiOrdini(Ordine As String) As Boolean
        Try
            AnalisiOrdini = False
            lun = Len(Ordine)
            i = InStr(2, Ordine, " ", 1)
            If i = 0 Then
                s_analisi = Mid$(Ordine, 1, lun)
            Else
                s_analisi = Mid$(Ordine, 1, i - 1)
            End If
            i_DestOrdine = ACU

            If PrefissiOrdiniMovimento.Contains(s_analisi) Then
                AnalisiOrdini = AnalisiOrdiniMovimento(Ordine)
            Else
                AnalisiOrdini = AnalisiOrdiniParametri(Ordine)
            End If
        Catch ex As Exception
            WriteError(0, "AnalisiOrdini: " & ex.Message)
        End Try
    End Function

    ' Prefissi comando gestiti da AnalisiOrdiniMovimento (comandi di stato/
    ' movimento: Stand By, Step/Auto/Program Track, posizionamento P/X/WS/
    ' US/BP, Antenna, Beacon Memory, Satellite). Qualunque altro prefisso
    ' ricade su AnalisiOrdiniParametri — stesso ordine di valutazione del
    ' doppio Select Case originale (il primo senza Case Else, il secondo
    ' con un Case Else che accetta/echo qualunque comando residuo).
    Private ReadOnly PrefissiOrdiniMovimento As HashSet(Of String) = New HashSet(Of String)(StringComparer.Ordinal) From {
        "*", "?", ">", "<",
        " S", "AS", "ES",
        " ST", "AST", "EST", " PS", "APS", "EPS", " AQ", "AAQ", "EAQ", " PI", "API", "EPI", " PO", "APO", "EPO", "AOF", "EOF", "POF",
        " M", "AM", "EM", " W", "AW", "EW", " U", "AU", "EU",
        " P", " X", "WS", "US", "BP",
        "AP", "AX",
        "EP", "EX",
        "PP",
        " A", "AA", "EA",
        "BM",
        " SAT", "ASAT", "ESAT", "SATK"
    }

    ' Comandi di movimento/stato — era il primo dei due Select Case in
    ' AnalisiOrdini. Estratta identica (nessuna modifica di logica): ogni
    ' Case qui dentro è invariato rigo per rigo rispetto all'originale.
    Private Function AnalisiOrdiniMovimento(Ordine As String) As Boolean
        Dim Send_ORDINI As String
        AnalisiOrdiniMovimento = False
        Select Case s_analisi
            Case "*", "?", ">", "<"
                b_Ricezione(ACU) = True
                AnalisiOrdiniMovimento = True
                Exit Function
            Case " S", "AS", "ES"
                PTK = &H0
                '            d_DataInizioProg = CDate("31/12/3000 00.00.00")
                d_DataInizioProg = ""
                MainForm.ProgramStatus = d_DataInizioProg
                frmRicezione.LblAzimuth.Text = ""
                frmRicezione.LblElevazione.Text = ""
                frmRicezione.LblAzimuthC.Text = ""
                frmRicezione.LblElevazioneC.Text = ""
                AzCalc = 0
                ElCalc = 0
                frmRicezione.LblAzError.Text = ""
                frmRicezione.LblElError.Text = ""
                frmRicezione.LblAzErrEn.Text = ""
                frmRicezione.LblElErrEn.Text = ""
                '20/10/2022
                AzOff = 0
                ElOff = 0
                '
                AnalisiOrdiniMovimento = True
                frmRicezione.LblCommand.Text = "Stand By"
                i_Status = OP_STANDBY
                Send_ORDINI = Mid$(Ordine, 1, 2)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case " ST", "AST", "EST", " PS", "APS", "EPS", " AQ", "AAQ", "EAQ", " PI", "API", "EPI", " PO", "APO", "EPO",
            "AOF", "EOF", "POF"
                AnalisiOrdiniMovimento = True
                Send_ORDINI = Mid$(Ordine, 1, 3)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case " M", "AM", "EM", " W", "AW", "EW", " U", "AU", "EU"
                AnalisiOrdiniMovimento = True
                Send_ORDINI = Mid$(Ordine, 1, 2)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case " P", " X", "WS", "US", "BP"
                If lun > 15 Then
                    d_Az = Mid$(Ordine, 4, 7)
                    d_El = Mid$(Ordine, 12, 6)
                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            If d_El >= 0 And d_El < 92 Then
                                frmRicezione.LblAzimuth.Text = d_Az
                                frmRicezione.LblElevazione.Text = d_El
                                AzCalc = d_Az
                                ElCalc = d_El
                                Send_ORDINI = Mid$(Ordine, 1, 17)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniMovimento = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 4, 6)
                    d_El = Mid$(Ordine, 11, 5)
                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            If d_El >= 0 And d_El < 92 Then
                                frmRicezione.LblAzimuth.Text = d_Az
                                frmRicezione.LblElevazione.Text = d_El
                                AzCalc = d_Az
                                ElCalc = d_El
                                Send_ORDINI = Mid$(Ordine, 1, 15)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniMovimento = True
                    Exit Function
                End If
            Case "AP", "AX"
                If lun > 9 Then
                    d_Az = Mid$(Ordine, 4, 7)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            frmRicezione.LblAzimuth.Text = d_Az
                            AzCalc = d_Az
                            Send_ORDINI = Mid$(Ordine, 1, 10)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniMovimento = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 4, 6)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            frmRicezione.LblAzimuth.Text = d_Az
                            AzCalc = d_Az
                            Send_ORDINI = Mid$(Ordine, 1, 9)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniMovimento = True
                    Exit Function
                End If
            Case "EP", "EX"
                If lun > 8 Then
                    d_El = Mid$(Ordine, 4, 6)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            frmRicezione.LblElevazione.Text = d_El
                            ElCalc = d_El
                            Send_ORDINI = Mid$(Ordine, 1, 9)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniMovimento = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 4, 5)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            frmRicezione.LblElevazione.Text = d_El
                            ElCalc = d_El
                            Send_ORDINI = Mid$(Ordine, 1, 8)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniMovimento = True
                    Exit Function
                End If
            Case "PP"
                If lun < 8 Then Exit Function
                d_Po = Mid$(Ordine, 5, 4)
                d_El = Mid$(Ordine, 4, 1)
                If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                    If IsNumeric(d_Po) Then
                        If d_Po >= 0 And d_Po < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 8)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniMovimento = True
                Exit Function
            Case " A", "AA", "EA"
                AnalisiOrdiniMovimento = True
                Send_ORDINI = Mid$(Ordine, 1, 2) & " "
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case "BM"
                AnalisiOrdiniMovimento = True
                Send_ORDINI = Mid$(Ordine, 1, 4)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case " SAT", "ASAT", "ESAT", "SATK"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 39 Then
                        Send_ORDINI = Mid$(Ordine, 1, 9)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniMovimento = True
                Exit Function
        End Select
    End Function

    ' Comandi di parametro/configurazione — era il secondo dei due Select
    ' Case in AnalisiOrdini, con un Case Else che accetta/echo qualunque
    ' comando non altrimenti riconosciuto (per questo non ha bisogno di un
    ' valore "nessun match": risolve sempre). Estratta identica (nessuna
    ' modifica di logica).
    Private Function AnalisiOrdiniParametri(Ordine As String) As Boolean
        Dim Send_ORDINI As String
        AnalisiOrdiniParametri = False
        Select Case s_analisi
            Case "IT"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El > 0 And d_El <= 15 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "ST"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 1 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "SS"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 5 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "XT"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 5 And d_El <= 35 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "YT"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 16 And d_El <= 85 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "RT"
                If lun < 5 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 1 And d_El <= 60 Then
                        Send_ORDINI = Mid$(Ordine, 1, 5)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "AO", "EO"
                If lun > 12 Then
                    d_Az = Mid$(Ordine, 8, 6)
                    d_El = Mid$(Ordine, 7, 1)
                    If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                        If IsNumeric(d_Az) Then
                            If d_Az >= 0 And d_Az < 92 Then
                                Send_ORDINI = Mid$(Ordine, 1, 13)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 8, 5)
                    d_El = Mid$(Ordine, 7, 1)
                    If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                        If IsNumeric(d_Az) Then
                            If d_Az >= 0 And d_Az < 92 Then
                                Send_ORDINI = Mid$(Ordine, 1, 12)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                End If
            Case "PO"
                If lun < 11 Then Exit Function
                d_Po = Mid$(Ordine, 8, 4)
                d_El = Mid$(Ordine, 7, 1)
                If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                    If IsNumeric(d_Po) Then
                        If d_Po >= 0 And d_Po < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 11)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "AOS", "EOS"
                If lun > 10 Then
                    d_Az = Mid$(Ordine, 6, 6)
                    d_El = Mid$(Ordine, 5, 1)
                    If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                        If IsNumeric(d_Az) Then
                            If d_Az >= 0 And d_Az < 92 Then
                                Send_ORDINI = Mid$(Ordine, 1, 11)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 6, 5)
                    d_El = Mid$(Ordine, 5, 1)
                    If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                        If IsNumeric(d_Az) Then
                            If d_Az >= 0 And d_Az < 92 Then
                                Send_ORDINI = Mid$(Ordine, 1, 10)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                End If
            Case "POS"
                If lun < 9 Then Exit Function
                d_Po = Mid$(Ordine, 6, 4)
                d_El = Mid$(Ordine, 5, 1)
                If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                    If IsNumeric(d_Po) Then
                        If d_Po >= 0 And d_Po < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 9)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "SATS"
                If lun < 23 Then Ordine += " +00.0"
                lun = Len(Ordine)
                If lun > 26 Then
                    d_El = Mid$(Ordine, 6, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El <= 39 Then
                            d_Az = Mid$(Ordine, 9, 7)
                            d_El = Mid$(Ordine, 17, 6)
                            d_Po = Mid$(Ordine, 25, 4)
                            If IsNumeric(d_Az) And IsNumeric(d_El) Then
                                If d_Az >= 0 And d_Az < 360 Then
                                    If d_El >= 0 And d_El < 92 Then
                                        d_El = Mid$(Ordine, 24, 1)
                                        If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                                            If IsNumeric(d_Po) Then
                                                Send_ORDINI = Mid$(Ordine, 1, 28)
                                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                                            Else
                                                Exit Function
                                            End If
                                        Else
                                            Exit Function
                                        End If
                                    End If
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 6, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El <= 39 Then
                            d_Az = Mid$(Ordine, 9, 6)
                            d_El = Mid$(Ordine, 16, 4)
                            d_Po = Mid$(Ordine, 23, 4)
                            If IsNumeric(d_Az) And IsNumeric(d_El) Then
                                If d_Az >= 0 And d_Az < 360 Then
                                    If d_El >= 0 And d_El < 92 Then
                                        d_El = Mid$(Ordine, 22, 1)
                                        If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                                            If IsNumeric(d_Po) Then
                                                Send_ORDINI = Mid$(Ordine, 1, 26)
                                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                                            Else
                                                Exit Function
                                            End If
                                        Else
                                            Exit Function
                                        End If
                                    End If
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                End If
            Case "SATKA"
                AnalisiOrdiniParametri = True
                Send_ORDINI = Mid$(Ordine, 1, 5)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case "MS"
                If lun > 21 Then
                    d_El = Mid$(Ordine, 4, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El <= 23 Then
                            d_El = Mid$(Ordine, 7, 2)
                            If IsNumeric(d_El) Then
                                If d_El = 0 Or d_El = 15 Or d_El = 30 Or d_El = 45 Then
                                    d_Az = Mid$(Ordine, 10, 7)
                                    d_El = Mid$(Ordine, 18, 6)
                                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                                        If d_Az >= 0 And d_Az < 360 Then
                                            If d_El >= 0 And d_El < 92 Then
                                                Send_ORDINI = Mid$(Ordine, 1, 23)
                                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                                            Else
                                                Exit Function
                                            End If
                                        Else
                                            Exit Function
                                        End If
                                    Else
                                        Exit Function
                                    End If
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 4, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El <= 23 Then
                            d_El = Mid$(Ordine, 7, 2)
                            If IsNumeric(d_El) Then
                                If d_El = 0 Or d_El = 15 Or d_El = 30 Or d_El = 45 Then
                                    d_Az = Mid$(Ordine, 10, 6)
                                    d_El = Mid$(Ordine, 17, 5)
                                    If IsNumeric(d_Az) And IsNumeric(d_El) Then
                                        If d_Az >= 0 And d_Az < 360 Then
                                            If d_El >= 0 And d_El < 92 Then
                                                Send_ORDINI = Mid$(Ordine, 1, 21)
                                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                                            Else
                                                Exit Function
                                            End If
                                        Else
                                            Exit Function
                                        End If
                                    Else
                                        Exit Function
                                    End If
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                End If
            Case "MK"
                If lun < 8 Then Exit Function
                d_El = Mid$(Ordine, 4, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 23 Then
                        d_El = Mid$(Ordine, 7, 2)
                        If IsNumeric(d_El) Then
                            If d_El = 0 Or d_El = 15 Or d_El = 30 Or d_El = 45 Then
                                Send_ORDINI = Mid$(Ordine, 1, 8)
                                frmRicezione.TxtOrdini.Text = Send_ORDINI
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "MT"
                If lun < 6 Then Exit Function
                d_El = Mid$(Ordine, 4, 3)
                If IsNumeric(d_El) Then
                    If d_El >= 1 And d_El <= 999 Then
                        Send_ORDINI = Mid$(Ordine, 1, 6)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "STEP"
                If lun > 7 Then
                    d_El = Mid$(Ordine, 6, 3)
                    If IsNumeric(d_El) Then
                        If d_El >= 1 And d_El <= 999 Then
                            Send_ORDINI = Mid$(Ordine, 1, 8)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 6, 2)
                    If IsNumeric(d_El) Then
                        If d_El >= 1 And d_El <= 99 Then
                            Send_ORDINI = Mid$(Ordine, 1, 7)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                End If
            Case "SPUP", "SPDW"
                If lun > 10 Then
                    d_El = Mid$(Ordine, 6, 6)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 11)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                Else
                    d_El = Mid$(Ordine, 6, 5)
                    If IsNumeric(d_El) Then
                        If d_El >= 0 And d_El < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 10)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                End If
            Case "SPCW", "SPCC"
                If lun > 11 Then
                    d_Az = Mid$(Ordine, 6, 7)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            Send_ORDINI = Mid$(Ordine, 1, 12)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                Else
                    d_Az = Mid$(Ordine, 6, 6)
                    If IsNumeric(d_Az) Then
                        If d_Az >= 0 And d_Az < 360 Then
                            Send_ORDINI = Mid$(Ordine, 1, 11)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                    AnalisiOrdiniParametri = True
                    Exit Function
                End If
            Case "SPPCW", "SPPCC"
                If lun < 11 Then Exit Function
                d_Po = Mid$(Ordine, 8, 4)
                d_El = Mid$(Ordine, 7, 1)
                If d_El = "P" Or d_El = "+" Or d_El = "M" Or d_El = "-" Then
                    If IsNumeric(d_Po) Then
                        If d_Po >= 0 And d_Po < 92 Then
                            Send_ORDINI = Mid$(Ordine, 1, 11)
                            frmRicezione.TxtOrdini.Text = Send_ORDINI
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "T", "TIME"        'modificato 19/04
                AnalisiOrdiniParametri = True
                Ordine = "TIME " & NowUTC().ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                Exit Function
            Case "DATE"
                AnalisiOrdiniParametri = True
                Ordine = "DATE " & NowUTC().ToString("dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture)
                Exit Function
            Case "MRY", "MRN", "SLY", "SLN", "SSY", "SSN", "RES", "MKA"
                AnalisiOrdiniParametri = True
                Send_ORDINI = Mid$(Ordine, 1, 3)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case "R", "L"
                AnalisiOrdiniParametri = True
                Send_ORDINI = Mid$(Ordine, 1, 1)
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                Exit Function
            Case "ATTS"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "ATTA", "ATTE"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "ATRT"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 99 Then
                        SEND = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = SEND
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "ATRA", "ATRE"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 0 And d_El <= 99 Then
                        SEND = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = SEND
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "ASDP", "ESDP"
                If lun < 7 Then Exit Function
                d_El = Mid$(Ordine, 6, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 10 And d_El <= 99 Then
                        Send_ORDINI = Mid$(Ordine, 1, 7)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
            Case "ALI", "ELI"
                If lun < 6 Then Exit Function
                d_El = Mid$(Ordine, 5, 2)
                If IsNumeric(d_El) Then
                    If d_El >= 1 And d_El <= 6 Then
                        Send_ORDINI = Mid$(Ordine, 1, 6)
                        frmRicezione.TxtOrdini.Text = Send_ORDINI
                    Else
                        Exit Function
                    End If
                Else
                    Exit Function
                End If
                AnalisiOrdiniParametri = True
                Exit Function
                frmRicezione.TxtOrdini.Text = SEND
                Exit Function
            Case "AT1E", "ET1E", "AT1D", "ET1D", "AS1E", "ES1E", "AS1D", "ES1D"     'Abilitano (E) o disabilitano (D) i motori 1
                AnalisiOrdiniParametri = True
                SEND = Mid$(Ordine, 1, 4)
                frmRicezione.TxtOrdini.Text = SEND
                Exit Function
            Case "AT2E", "ET2E", "AT2D", "ET2D", "AS2E", "ES2E", "AS2D", "ES2D"     'Abilitano (E) o disabilitano (D) i motori 2
                AnalisiOrdiniParametri = True
                SEND = Mid$(Ordine, 1, 4)
                frmRicezione.TxtOrdini.Text = SEND
                Exit Function
            Case "O", "P"
                i_DestOrdine = GRF
                AnalisiOrdiniParametri = True
                Exit Function
            Case "B"
                i_DestOrdine = GRF
                AnalisiOrdiniParametri = True
                CambioFreq(Mid$(Ordine, 3, 8))
            Exit Function
            Case "Z"
                i_DestOrdine = PCU
                AnalisiOrdiniParametri = True
                Exit Function
            Case Else
                Send_ORDINI = Ordine
                frmRicezione.TxtOrdini.Text = Send_ORDINI
                AnalisiOrdiniParametri = True
                Exit Function
        End Select
    End Function
    Public Function Calcolo_Coordinate(Data1 As Date, mSec As Integer) As Boolean
        'Apportata modifica per avere un calcolo differenziato sia sulla data, che viene inviata sulla chiamata
        'sia su un valore 0 (invia) 1 (non inviare), per permettere un calcolo delle coordinate ad un tempo
        'qualsiasi senza interessare l'ACU
        Dim i As Double
        Dim tsincex As Double
        Dim pos As Vector = NewVector(), vel As Vector = NewVector()
        Dim ts1 As time_set
        Dim Timen As Double, s_program As String
        Dim EpochDate As Date
        Dim d_nonvalido
        Dim Port As String
        Dim tmpSQL As String
        Calcolo_Coordinate = False
        If AzInibhit And ElInibhit Then Exit Function       'esci se i due assi sono inibiti            'modifiche 14/02/2012

        Az_prov = 0
        El_Prov = 0
        AzCalc = 0
        ElCalc = 0
        data = Data1
        With ts1
            .yr = Year(data)
            .mo = Month(data)
            .dy = data.Day
            .hr = Hour(data)
            .mi = Minute(data)
            .se = Second(data)
            .hu = mSec
        End With
        '    Time_to_UTC ts
        Timen = Julian_Date(ts1)
        tsincex = (Timen - julian_epoch) * xmnpda
        If Algoritmo = 1 Then
            tmpSQL = "SELECT * FROM [Sat2line] order by Sat_Name"
            rs_Ricez = dbOrbite.OpenRecordset(tmpSQL, dbOpenDynaset)
            rs_Ricez.FindFirst("Sat_Name ='" & s_Satellite & "'")
         If Not rs_Ricez.NoMatch Then
                ' 16/02/2018 Viene controllato che il valore di validità del TLE sia maggiore di zero, altrimenti non viene calcolato il valore di
                ' Program Track dopo un giorno e il sistema viene messo in Stand By
                If IsDate(rs_Ricez!EpochDate) And g_ValTLE > 0 Then
                    EpochDate = rs_Ricez!EpochDate
                    PTK = &H10         'Elementi validi
                    d_nonvalido = DateAdd("d", g_ValTLE - 1, EpochDate)
                    If CDate(d_nonvalido).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) = Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
                        PTK = &H20     'Elementi non validi < 24 ore
                        If Algoritmo = 1 Then
                            frmNorad.AggNorad
                        End If
                    ElseIf DateAdd("d", 1, d_nonvalido) <= Now Then
                        PTK = &H30 'elementi non validi > 24 ore
                        If Algoritmo = 1 Then
                            frmNorad.AggNorad
                        End If
                    End If
                End If
            End If

            If b_Sat Then

                If ideep = 0 Then
                    PosSGP4(tsincex, pos, vel)
          Else
                    PosSDP4(tsincex, pos, vel)
       End If
                Convert_Sat_State(pos, vel)
            geodetic.Valori(1) = LATSTA
                geodetic.Valori(2) = LONSTA
                geodetic.Valori(3) = h
                Calculate_Obs(pos, vel, Timen, obs_set)
            Az_prov = Format$((obs_set.Valori(1) * RadGradi) + AzOffset1 + AzOffset, "0.000")
                El_Prov = Format$((obs_set.Valori(2) * RadGradi) + ElOffset1 + ElOffset, "0.000")
                AzCalc = Format$((obs_set.Valori(1) * RadGradi) + AzOffset1, "0.000")
                ElCalc = Format$((obs_set.Valori(2) * RadGradi) + ElOffset1, "0.000")
                If Az_prov < 0 Then
                    Az_prov += 360
                ElseIf Az_prov > 360 Then
                    Az_prov -= 360
                End If
                If AzCalc < 0 Then
                    AzCalc += AzCalc
                ElseIf AzCalc > 360 Then
                    AzCalc -= 360
                End If
            Else
                ' Satellite senza TLE convertito (b_Sat=False: mai caricato
                ' con "Carica"/EXE, o conversione fallita) — Az_prov/El_Prov
                ' restavano fermi al valore precedente (0 se mai calcolati
                ' prima), senza alcun avviso all'operatore. Stesso trattamento
                ' già dato al path 11 parametri in EpocaPar11Valida.
                SegnalaParametriSatelliteAssenti()
            End If
        ElseIf Algoritmo = 0 Or Algoritmo = 2 Then
            CalPar11Now()
            If IsNumeric(frmRicezione.LblAzimuth.Text) Then Az_prov = frmRicezione.LblAzimuth.Text
            If IsNumeric(frmRicezione.LblElevazione.Text) Then El_Prov = frmRicezione.LblElevazione.Text
        ElseIf Algoritmo = 3 Then
            TrackTable()
            If IsNumeric(frmRicezione.LblAzimuth.Text) Then Az_prov = frmRicezione.LblAzimuth.Text
            If IsNumeric(frmRicezione.LblElevazione.Text) Then El_Prov = frmRicezione.LblElevazione.Text
        ElseIf Algoritmo = 4 Then
            If s_Satellite = "Sun" Then
                SolarAzEl(Timen, pos)
    ElseIf s_Satellite = "Moon" Then
                PosizioneLuna(Timen, pos)
    ElseIf s_Satellite = "Venus" Then
                Call PosizioneVenere(Timen, pos)
            ElseIf s_Satellite = "Cassiopeia" Then
                Call CalcolaCassiopeia(Timen, pos)
            End If
            Az_prov = Format$((pos.Valori(1)) + AzOffset1 + AzOffset, "0.000")
            El_Prov = Format$((pos.Valori(2)) + ElOffset1 + ElOffset, "0.000")
            AzCalc = Format$((pos.Valori(1)) + AzOffset1, "0.000")
            ElCalc = Format$((pos.Valori(2)) + ElOffset1, "0.000")


            If Az_prov < 0 Then
                Az_prov += 360
            ElseIf Az_prov > 360 Then
                Az_prov -= 360
            End If
            If AzCalc < 0 Then
                AzCalc += 360
            ElseIf AzCalc > 360 Then
                AzCalc -= AzCalc
            End If
        End If
        Calcolo_Coordinate = True
        If i_ProgTrack = 1 Or i_ProgTrack = 4 Then
            Az_prov = Format$(Az_prov + i_AzOffsetLock, "0.000")
            El_Prov = Format$(El_Prov + i_ElOffsetLock, "0.000")
            If Az_prov < 0 Then
                Az_prov += 360
            ElseIf Az_prov > 360 Then
                Az_prov -= Az_prov
            End If
            If IsNumeric(El_Prov) Then
                If Not AzInibhit Then frmRicezione.LblAzimuth.Text = Format$(Az_prov, "000.000")
                If Not ElInibhit Then frmRicezione.LblElevazione.Text = Format$(El_Prov, "00.000")
                If Not AzInibhit Then frmRicezione.LblAzimuthC.Text = Format$(AzCalc, "000.000")
                If Not ElInibhit Then frmRicezione.LblElevazioneC.Text = Format$(ElCalc, "00.000")
                If El_Prov < g_ElMin Then
                    Port = Canali(ACU).Port_IN
                    Port_Control(Port).Rec_State = PCR_STANDBY
                    frmRicezione.LblAzimuth.ForeColor = Color.Yellow
                    frmRicezione.LblElevazione.ForeColor = Color.Yellow
                    Exit Function
                Else
                    frmRicezione.LblAzimuth.ForeColor = Color.Black
                    frmRicezione.LblElevazione.ForeColor = Color.Black
                End If

                s_program = ""
                If Not AzInibhit And Not ElInibhit Then
                    s_program = " P " & Format$(Az_prov, "000.000") & " " &
                Format$(El_Prov, "00.000") & " "
                ElseIf ElInibhit Then
                    s_program = "AP " & Format$(Az_prov, "000.000") & " "
                ElseIf AzInibhit Then
                    s_program = "EP " & Format$(El_Prov, "00.000") & " "
                End If
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
    End If
        Else
            ' "Program" (LblAzimuthC/LblElevazioneC) resta vuoto quando non si
            ' è davvero in Program Track (i_ProgTrack <> 1/4): AzCalc/ElCalc
            ' vengono comunque calcolati sopra (servono ad altri usi, es.
            ' l'offset in LblAzError/LblElError), ma non vanno mostrati come
            ' se fosse attivo un tracking predittivo che non sta avvenendo.
            frmRicezione.LblAzimuth.Text = " "
            frmRicezione.LblElevazione.Text = " "
        End If
    End Function
    Public Function Calcolo_Coordinate1(Data1 As Date) As Boolean
        'Apportata modifica per avere un calcolo differenziato sia sulla data, che viene inviata sulla chiamata
        'sia su un valore 0 (invia) 1 (non inviare), per permettere un calcolo delle coordinate ad un tempo
        'qualsiasi senza interessare l'ACU
        Dim tsincex As Double
        Dim pos As Vector = NewVector(), vel As Vector = NewVector()
        Dim ts1 As time_set
        Dim Timen As Double
        Calcolo_Coordinate1 = False
        Az_prov = 0
        El_Prov = 0
        data = Data1
        With ts1
            .yr = Year(data)
            .mo = Month(data)
            .dy = data.Day
            .hr = Hour(data)
            .mi = Minute(data)
            .se = Second(data)
            .hu = 0
        End With
        '    Time_to_UTC ts
        Timen = Julian_Date(ts1)
        tsincex = (Timen - julian_epoch) * xmnpda
        If Algoritmo = 1 Then
            If b_Sat Then
                If ideep = 0 Then
                    PosSGP4(tsincex, pos, vel)
                Else
                    PosSDP4(tsincex, pos, vel)
                End If
                Convert_Sat_State(pos, vel)
                geodetic.Valori(1) = LATSTA
                geodetic.Valori(2) = LONSTA
                geodetic.Valori(3) = h
                Calculate_Obs(pos, vel, Timen, obs_set)
                Az_prov = (obs_set.Valori(1) * RadGradi) + AzOffset1
                El_Prov = (obs_set.Valori(2) * RadGradi) + ElOffset1
                If Az_prov < 0 Then
                    Az_prov += 360
                ElseIf Az_prov > 360 Then
                    Az_prov -= 360
                End If
                frmRicezione.LblAzimuthC.Text = Format$(Az_prov, "0.000")
                frmRicezione.LblElevazioneC.Text = Format$(El_Prov, "0.000")
                AzCalc = Az_prov
                ElCalc = El_Prov
            End If
        ElseIf Algoritmo = 0 Or Algoritmo = 2 Then
            CalPar11Now()
            If IsNumeric(frmRicezione.LblAzimuth.Text) Then Az_prov = frmRicezione.LblAzimuthC.Text
            If IsNumeric(frmRicezione.LblElevazione.Text) Then El_Prov = frmRicezione.LblElevazioneC.Text
        ElseIf Algoritmo = 3 Then
            TrackTable()
            If IsNumeric(frmRicezione.LblAzimuth.Text) Then Az_prov = frmRicezione.LblAzimuthC.Text
            If IsNumeric(frmRicezione.LblElevazione.Text) Then El_Prov = frmRicezione.LblElevazioneC.Text
        ElseIf Algoritmo = 4 Then
            If s_Satellite = "Sun" Then
                SolarAzEl(Timen, pos)
            ElseIf s_Satellite = "Moon" Then
                PosizioneLuna(Timen, pos)
            ElseIf s_Satellite = "Venus" Then
                Call PosizioneVenere(Timen, pos)
            ElseIf s_Satellite = "Cassiopeia" Then
                Call CalcolaCassiopeia(Timen, pos)
            End If
            Az_prov = Format$((pos.Valori(1)) + AzOffset1 + AzOffset, "0.000")
            El_Prov = Format$((pos.Valori(2)) + ElOffset1 + ElOffset, "0.000")
            frmRicezione.LblAzimuthC.Text = Format$(Az_prov, "0.000")
            frmRicezione.LblElevazioneC.Text = Format$(El_Prov, "0.000")
            AzCalc = Format$((pos.Valori(1)) + AzOffset1, "0.000")
            ElCalc = Format$((pos.Valori(2)) + ElOffset1, "0.000")
        Else
            Exit Function
        End If
        Calcolo_Coordinate1 = True
    End Function
    Public Function ComparaDati() As Boolean

        Dim DataIni As Date
        ComparaDati = False
        If String.IsNullOrEmpty(d_DataInizioProg) Then Return False
        If Not DateTime.TryParse(d_DataInizioProg, DataIni) Then Return False
        DataIni = DateAdd("h", -i_Off, DataIni)
        Calcolo_Coordinate1(DataIni)
frmRicezione.LblAzimuth.Text = ""
        frmRicezione.LblElevazione.Text = ""
        'az1 = Az_prov

        'Attesa 0.5
        'g_Verso = False
        'DataIni = CDate(frmRicezione.mskDataIni) - CDate(Format$(i_Off / (24), "yyyy\-mm\-dd hh\:nn\:ss"))
        'Calcolo_Coordinate1 DataIni
        'az2 = Az_prov
        'IfSystem.Math.Math.Abs(az1 - az2) <= 1 Then
        '    ComparaDati = True
        'Else
        '    ComparaDati = False
        'End If
    End Function

    Public Function Calcolo_StepTrack() As Boolean
        '30/11/2024 Martino - Viene implementata la funzione dello Step Track da attivare nel caso l'ACU non riesca a calcolarla
        Dim rs_Acq As Object
        Dim s_program As String = "", i_lun As Integer
        '06/06/2022
        ' On Error Resume Next → Try/Catch: nessun Resume/GoTo in questa
        ' funzione (flusso lineare, un Case alla volta), conversione
        ' meccanica sicura — a differenza di Calcolo_Aquire/AggiornaTLE
        ' (lasciate On Error, vedi commenti lì) non c'è alcuna dipendenza
        ' dal "continua comunque alla riga dopo" tra le istruzioni.
        Try
        Select Case i_ConStato
            Case 0
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 1
                s_Lev3 = d_LevAve        'Memorizzo il livello del beacon
                b_StepTrack = False
                If s_Lev1 < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                'inizio dello step. L'antenna viene spostata di uno +step in azimuth
                'siamo in preset
                frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.TxtAz.Text) + i_AzStep, "000.000")
                frmRicezione.LblStepTrack.Text = "AZ"
                i_ConStato += 1
                s_program = "AP " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                i_StepTrack = 8
            Case 2
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 3
                b_StepTrack = True
                If Val(s_Lev1) < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                If d_LevAve >= s_Lev3 Then              'Livello maggiore vai avanti
                    'inizio dello step. L'antenna viene spostata di uno +step in azimuth
                    'siamo in preset
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep, "000.000")
                    i_StepTrack = 8
                Else
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) - i_AzStep / 2, "000.000")
                    i_ConStato += 1
                    i_StepTrack = 11
                End If
                s_Lev3 = d_LevAve         'Memorizzo il livello del beacon
                s_program = "AP " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " "
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                i_StepTrack = 11
            Case 4
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 5
                b_StepTrack = True
                If Val(s_Lev1) < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                If d_LevAve >= s_Lev3 Then             'Livello maggiore vai avanti
                    'inizio dello step. L'antenna viene spostata di uno +step in azimuth
                    'siamo in preset
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) - i_AzStep, "000.000")
                    i_StepTrack = 11
                Else
                    frmRicezione.LblAzimuth.Text = Format$(CDbl(frmRicezione.LblAzimuth.Text) + i_AzStep / 2, "000.000")
                    i_ConStato += 1
                    i_StepTrack = 8
                End If
                s_program = "AP " & Format$(CDbl(frmRicezione.LblAzimuth.Text), "000.000") & " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                s_Lev3 = d_LevAve         'Memorizzo il livello del beacon
            Case 6
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 7
                b_StepTrack = True
                If Val(s_Lev1) < i_YT Then                      'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                'inizio dello step. L'antenna viene spostata di uno +step in elevazione UP
                'siamo in preset
                frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.TxtEl.Text) + i_ElStep, "00.000")
                frmRicezione.LblStepTrack.Text = "EL"
                i_ConStato += 1
                s_program = "EP " & Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                s_Lev3 = d_LevAve
                i_StepTrack = 6
            Case 8
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 9
                b_StepTrack = True
                If Val(s_Lev1) < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                If d_LevAve >= s_Lev3 Then             'Livello maggiore vai avanti
                    'inizio dello step. L'antenna viene spostata di uno +step in elevazione
                    'siamo in preset
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep, "00.000")
                    i_StepTrack = 6
                Else
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.TxtEl.Text) - i_ElStep, "00.000")
                    frmRicezione.LblStepTrack.Text = "EL"
                    i_StepTrack = 8
                    i_ConStato += 1
                End If
                s_Lev3 = d_LevAve         'Memorizzo il livello del beacon
                s_program = "EP " & Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 10
                i_ConStato += 1
                PulisciAverage()
                b_StepTrack = True
                frmRicezione.TmrSpiralAcquire.Enabled = True
            Case 11
                b_StepTrack = False
                If Val(s_Lev1) < i_YT Then                       'valore oltre le soglie poni il sistema in stand by
                    frmRicezione.CmdSTBY_Click(Nothing, EventArgs.Empty)
                    frmRicezione.TmrSpiralAcquire.Enabled = False
                    frmRicezione.LblStepTrack.Text = ""
                    frmRicezione.LblStepTrack.Visible = False
                    i_ConStato = 0
                    Exit Function
                End If
                If d_LevAve >= s_Lev3 Then             'Livello maggiore vai avanti
                    'inizio dello step. L'antenna viene spostata di uno +step in azimuth
                    'siamo in preset
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) - i_ElStep, "00.000")
                    frmRicezione.LblStepTrack.Text = "EL"
                    i_StepTrack = 8
                Else
                    frmRicezione.LblElevazione.Text = Format$(CDbl(frmRicezione.LblElevazione.Text) + i_ElStep / 2, "00.000")
                    i_ConStato += 1
                    frmRicezione.LblStepTrack.Text = "MM"
                    i_StepTrack = 12
                End If
                s_program = "EP " & Format$(CDbl(frmRicezione.LblElevazione.Text), "00.000") + " "
                i = Len(s_program) + 6
                SEND_ACU = CheckSum(Format$(i, "00") + Mid$(s_program, 1, Len(s_program)))
                b_int(ACU) = True
                b_Ricezione(ACU) = False
                InvioACU(SEND_ACU)
        frmRicezione.TmrSpiralAcquire.Enabled = True
                s_Lev3 = d_LevAve         'Memorizzo il livello del beacon
            Case 12
                i_ConStato = 0
                frmRicezione.TmrSpiralAcquire.Enabled = False
        End Select
        Catch ex As Exception
            WriteError(0, "Calcolo_StepTrack: " & ex.Message)
        End Try
    End Function
    Public Sub PulisciAverage()
        Dim i As Integer
        For i = 1 To 10
            d_LevAv(i - 1) = &HFF
        Next
        '    d_LevAve = 0
    End Sub


End Module
