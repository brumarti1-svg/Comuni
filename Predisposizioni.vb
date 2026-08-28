Imports Quartz

Module Predisposizioni

    ' Aggiunto il 6/12/2016 per un riordino dei declare e per impostare le System.Math.tanti per la lettura della data sui file in memoria

    Public Declare Function FileTimeToLocalFileTime Lib "kernel32" (lpFileTime As FILETIME, lpLocalFileTime As FILETIME) As Long
    Public Declare Function FileTimeToSystemTime Lib "kernel32" (lpFileTime As FILETIME, lpSystemTime As SystemTime) As Long
    Public Declare Function OpenFile Lib "kernel32" (ByVal lpFileName As String, lpReOpenBuff As OFSTRUCT, ByVal wStyle As Long) As Long
    Public Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long
    Public Declare Function GetFileTime Lib "kernel32" (ByVal hFile As Long, lpCreationTime As FILETIME, lpLastAccessTime As FILETIME, lpLastWriteTime As FILETIME) As Long
    Public Declare Function GetTimeFormat Lib "kernel32.dll" Alias "GetTimeFormatA" (ByVal Locale As Long, ByVal dwFlags As Long, lpTime As SystemTime, ByVal lpFormat As Long, ByVal lpTimeStr As String, ByVal cchTime As Long) As Long
    Public Declare Sub GetLocalTime Lib "kernel32.dll" (lpSystemTime As SystemTime)
    Public Declare Function SetSystemTime Lib "kernel32" _
(lpSystemTime As SystemTime) As Long



    'Aggiunte il 21/11/2016 per il controllo della data dei log

    Public Const OF_READ = &H0
    Public Const OF_READWRITE = &H2
    Public Const OF_REOPEN = &H8000
    Public Const OF_SHARE_COMPAT = &H0
    Public Const OF_SHARE_DENY_NONE = &H40
    Public Const OF_SHARE_DENY_READ = &H30
    Public Const OF_SHARE_DENY_WRITE = &H20
    Public Const OF_SHARE_EXCLUSIVE = &H10
    Public Const OF_VERIFY = &H400
    Public Const OF_WRITE = &H1
    Public Const OFS_MAXPATHNAME = 128

    Public Const TLE_LEN_LINE_DATA = 69

    Public Structure FILETIME
        Public dwLowDateTime As Long
        Public dwHighDateTime As Long
    End Structure

    Public Structure OFSTRUCT
        Public cBytes As Byte
        Public fFixedDisk As Byte
        Public nErrCode As Integer
        Public Reserved1 As Integer
        Public Reserved2 As Integer
        Public szPathName() As Byte
    End Structure

    Public Const TIME_ZONE_ID_INVALID As Long = &HFFFFFFFF
    Public Const TIME_ZONE_ID_STANDARD As Long = 1
    Public Const TIME_ZONE_ID_UNKNOWN As Long = 0

    Public Const TIME_ZONE_ID_DAYLIGHT As Long = 2
    ' Funzioni API


    Public Const LOCALE_SYSTEM_DEFAULT = &H400
    Public Const LOCALE_USER_DEFAULT = &H800
    Public Const LOCALE_NOUSEROVERRIDE = &H80000000
    Public Const LOCALE_USE_CP_ACP = &H40000000
    Public Const TIME_NOMINUTESORSECONDS = &H1
    Public Const TIME_NOSECONDS = &H2
    Public Const TIME_NOTIMEMARKER = &H4
    Public Const TIME_FORCE24HOURFORMAT = &H8


    Public b_Scelta As Boolean
    Public Const ACK = 6
    Public Const BIT1 = &H1
    Public Const BIT2 = &H2
    Public Const BIT3 = &H4
    Public Const BIT4 = &H8
    Public Const BIT5 = &H10
    Public Const BIT6 = &H20
    ' CNL_3_IN, CNL_4_IN declared in AppState.vb
    Public Const CR = 13
    Public Const EOT = 4
    Public Const ETX = 3
    Public Const NAK = 21
    Public Const ACU = 1                'Indice della connessione della ACU
    Public Const MAC = 2               'Indice del SAP
    Public Const RX = 3                 ' indice per ricevitore
    Public Const GRF = 4               ' INDICE PER GENERATORE DI FREQUENZA
    Public Const INTELSAT = 5      ' INDICE PER INTELSAT (trasmissione dati taggati)
    Public Const PCU = 6               ' indice per dati polarizzazione e LNA
    Public Const LNA = 7               ' gestione LNA
    Public Const SCU = 8              ' SWITCH CONTROL UNIT
    Public Const MODPOL = 9       ' modalita' polarizzatore Lineare - Circolare RHCP LHCP Valore angolare
    Public Const OP_AUGMENTED = 5
    Public Const OP_AUGMENTEDST = 10                    'Modo Augmented per Step Track
    Public Const OP_AUTOMATICAQUIRE = 9
    Public Const OP_AUTOTRACK = 4
    Public Const OP_COMMANDPOSITION = 6
    Public Const OP_MEMORYTRACK = 8
    Public Const OP_PROGRAMTRACK = 3
    Public Const OP_SIMULATION = 7
    Public Const OP_STANDBY = 1
    Public Const OP_STEPTRACK = 2
    Public Const OP_UNSTOW = 10
    '09/12/21
    Public Const OP_SPIRALACQUIRE = 11
    '03/03/2026
    Public Const OP_PROIEZ = 12

    ''
    Public Const PCR_RECEIVE = 1   ' RICEZIONE MSG
    Public Const PCS_ERROR = 5     ' ERRORE DI CKS
    Public Const PCS_FAIL_COL As Integer = &HFF0000      ' AVARIA
    Public Const PCS_MAINT = 1     ' MANUTENZIONE
    Public ReadOnly PCS_NORMAL_COL As System.Drawing.Color = System.Drawing.Color.LightGreen
    Public ReadOnly PCS_RESET_COL As System.Drawing.Color = System.Drawing.Color.Magenta
    Public Const PCS_STOP = 4      ' RICEZIONE BLOCCATA
    Public ReadOnly SCS_FAIL_COL As System.Drawing.Color = System.Drawing.Color.Red
    Public ReadOnly SCS_MAINT_COL As System.Drawing.Color = System.Drawing.Color.Yellow
    Public Const SCS_NORMAL_COL As Integer = &HC0C0C0 ' NORMALE
    Public Const STX = 2
    Public Const ComponentCode = &H1090
    Public Const ComponentCodeIF1 = &H1050
    Public Const ComponentCodeIF2 = &H1051
    Public Const ComponentCodeIF3 = &H1052
    Public Const Conv_Volt = 0.004884       '(20 / 2048)
    Public Const LanCal = 6
    Public Const LanCtrl = 2
    Public Const LanLog = 4
    Public Const LanMon = 1
    Public Const LanPhase = 5
    Public Const LanRst = 3
    Public Const LenMesCalTable = 56            'lunghezza messaggio calibration table
    Public Const LenMesMon = 20                 'lunghezza messaggio di phasing e calibration
    Public Const LenMesPhasing = 44             'lunghezza messaggio di phasing e calibration
    Public Const Minuti = 15                       ' intervallo di interpolazione dei dati
    Public Const omega As Double = 2 * PIGREC / 96
    Public Const RxCal = 3090             'Calibration
    Public Const RxCtrl = 3001            'Control
    Public Const RxLog = 3040             'Logging Data
    Public Const RxMon = 3000             'Monitor
    Public Const RxPhase = 3080           'Phase
    Public Const RxRst = 3002             'Reset


    Public d_Pol As String                      'Indica il modo operativo del polarizzatore
    Public rs_App As Object
    Public f_Log As String
    Public f_Log1 As String
    Public f_Log2 As String
    Public f_Log3 As String
    Public f_Log4 As String
    Public g_aggOrdini As Boolean                'Modifica 11/03/2016
    Public g_AggPar As Boolean
    Public g_CirLin As String
    Public g_Conv As Integer
    Public g_DirPar As String
    Public g_ElMin As System.Single
    Public g_Frequenza As String
    Public g_GrafPol As Boolean             'Aggiunta il 30/06/2014 per la rapPresentazione Polare
    ' = false rapPresenta i valori provenienti dall'antenna
    ' = true rapPresenta i valori provenienti da una tabella
    Public g_Verso As Boolean       '07/10/2025 per il calcolo della coordinata con tempo o senza
    Public g_LNA As String
    Public g_ParACU As String        'Tipo ACU per i parametri
    Public g_Parametri As String
    Public g_ParAntenna As String    'Tipo Antenna per i parametri
    Public g_ParEquipment As String  'Ubicazione antenna per i parametri
    Public g_ParSatellite As String  'Tipo Satellite per i parametri
    Public g_ParUbicazione As String 'Ubicazione antenna per i parametri
    Public g_PhaseAz As String               ' Aggiunta il 22/03/2012 per la FOC-C03
    Public g_PhaseEl As String
    Public g_Polar As String
    Public g_PolErr As String
    Public g_RevAz As String
    Public g_Sat As String
    Public g_Socket As String       ' determina se e' collegato in rete
    Public g_ThrAuto As System.Single      'soglia del segnale somma per far entrare l'Auto Track
    Public Max_Offset As Double = 9.999    'offset massimo consentito (era Private in frmConfigurazione)
    Public g_ThrAz As System.Single        'soglia dell'errore Az per far entrare l'Auto Track
    Public g_ThrEl As System.Single        'soglia dell'errore El per far entrare l'Auto Track
    Public g_ValPol As String
    Public g_IntPol As Integer              '= 0 Leggi Pol = 1 Invia Valore
    Public i_IntCom As System.Single
    Public i_Inter As System.Single        'intervallo di interrogazione ACU
    Public i_InterLNA As System.Single     'intervallo di interrogazione LNA
    Public i_InterPol As System.Single     'intervallo di interrogazione Polarizzatore
    Public i_InterRic As System.Single     'intervallo di interrogazione Ricevitore
    Public i_InterSCU As System.Single     'intervallo di interrogazione Switch
    Public i_InterST As System.Single    'intervallo modo step track 18/12/2024
    '16/03/2024
    Public i_DestOrdine As Integer 'parametro per suddividere gli ordini
    '05/12/21 Aggiunte variabili per gestione acquire
    Public i_InterAcq As System.Single      'intervallo per calcolo acquire
    Public i_AzRange As System.Single      'Azimuth range per spostamento antenna in Acquire
    Public i_ElRange As System.Single       'Elevation range per spostamento antenna in Acquire
    Public i_AzStep As System.Single         'Azimuth Step per spostamento antenna in Acquire
    Public i_ElStep As System.Single          'Elevation step per spostamento antenna in Acquire
    Public i_AzStepLock As System.Single         'Azimuth Step per spostamento antenna in Acquire
    Public i_ElStepLock As System.Single          'Elevation step per spostamento antenna in Acquire
    Public i_Spiral As Integer          'Spirali effettuate
    Public i_AzOffsetLock As Double, i_ElOffsetLock As Double  'Memorizza gli off in caso di lock su Acquire
    Public b_LockOn As Boolean              ' Indica se è stato già visto un lock
    Public b_RangeAz As Boolean, b_RangeEl As Boolean     'Variabili per il controllo del Range
    '09/06/2022
    Public b_Lock As Boolean
    '15/12/2021 Vengono impostati i contatori per il calcolo dell'off nello stato operativo Spiral Acquire
    Public i_Quadrati As Integer  'Contatore di quadrati
    Public i_Step As Integer       'Contatori di Step per lato
    Public i_ConStato As Integer 'Contatore di stato - Indica a che punto della spirale mi trovo
    Public i_StepTrack As Integer 'Contiene lo stato dello step track
    '14/06/2022
    Public i_LockThr As Integer     'soglia per accettare il lock

    'Utilizzati per i parametri 11/04/2023
    Public b_Origine As Boolean  'false = Dacu true = File
    Public par_Buffer As String
    '18/12/2024 Usati per i parametri di step track se calcolato da PC1
    Public i_IT As System.Single                       'Integration timer
    Public i_AT As System.Single                       'Step lenght azimuth
    Public i_ET As System.Single                       'Step Lenght elevetion
    Public i_RT As System.Single                       'Restart automatico
    Public i_CY As System.Single
    Public i_BM As String

    'Variabili per il controllo dei limiti software, per evitare comandi di posizione errati
    Public s_SPCC As String          'limite azimuth CC
    Public s_SPCW As String         'limite azimuth CW
    Public s_SPDW As String         'limite elevazione DW
    Public s_SPUP As String           'limite elevazione UP
    Public s_SPPCC As String        'limite polarizzazione CC
    Public s_SPPCW As String       'limite polarizzazione CW

    Public i_LockBell As Integer                            'Indica se occorre abilitare il bell per il beacon
    Public i_RX As Integer
    Public iConnection As Integer
    Public Interroga As Boolean
    Public Logo As String
    Public Max_Off As System.Single
    Public NumMsg As Integer
    Public Pendenza As System.Single
    Public Pendenza1 As System.Single
    Public Ptic As String
    Public s_ACUType As String
    'Public    rs_App As Object
    Public SatChange As Boolean
    Public ScalaMax As System.Single
    Public ScalaMin As System.Single
    Public Valore0 As System.Single
    Public CoorOff As System.Single
    Public CoorAcq As System.Single
    Public wsOrbite As Object
    Public aAz As System.Single, faseAz As System.Single
    Public ACUOldMilSec As Double
    Public AddressDC As Integer
    Public AddressLNA As Integer
    Public AddressSCU As Integer
    Public aEl As System.Single, faseEl As System.Single
    ' g_ChangePhase declared in AppState.vb (As String)
    '09/07/2020 variabili per il calcolo della scala nel resize
    Public i_Width As System.Single
    Public i_Height As System.Single
    Public AggAlgo As Integer
    Public AlarmPc As Byte
    Public i_Ephemeris As Integer '0 = Moon, 1 = Planet, 2 = Satelliti, 3 = Sole
    Public Antenna As String
    Public AzimuthGainSlope As System.Single
    Public AzInibhit As Boolean
    Public AzOff As Double       'Memorizza il valore di Az calcolato dal Program Track
    Public AzPhaseValue As System.Single
    Public b_Abort As Boolean
    Public b_Aggiorna As Boolean
    ' b_ClosePre declared in AppState.vb
    Public b_errore As Boolean
    Public b_LNA As Boolean             'Abilita (True) o Disabilita (False) l'interrogazione dell'LNA
    Public b_MCS As Boolean                           '11/12/2018 False = richiesta PC1 Vero = Richiesta MCS
    Public b_Memory As Boolean                      'False = i dati per la Table non sono validi True = validi
    Public b_SCU As Boolean             'Abilita (True) o Disabilita (False) l'interrogazione dello Switch
    Public b_Acquire As Boolean        'indica se il dato di Az e El per lo Spiral Acquire è preso dal Program Track (=Vero) o dall'Antenna (=Falso)
    ' Public    BackAT As Integer
    Public BackPT As Integer
    Public BackUpAutoT As String
    Public BackUpPT As String
    Public BuffStatus As String
    Public CampoOp As String

    Public CN As Integer
    Public d_DataFineProg As String
    Public ElevationGainSlope As System.Single
    Public ElInibhit As Boolean
    Public ElOff As Double
    ' ElOffset1 declared in AppState.vb

    Public ElPhaseValue As System.Single
    Public f_DCStructure As Byte
    Public f_Frequency As System.Single
    Public f_FrqBeacon As System.Single
    Public b_frequency As Boolean               '12/11/2022
    Public f_PowerOut As System.Single
    Public g_Assi As Integer
    Public g_Validi As Integer
    Public g_ValTLE As Integer
    Public g_Versione As String
    Public ACU_PORT As Integer
    Public GRF_PORT As Integer
    Public INTELSAT_PORT As Integer
    Public PCU_PORT As Integer
    Public LNA_PORT As Integer
    Public SCU_PORT As Integer
    Public MODPOL_PORT As Integer
    Public s_Internet As String
    '       ALTITUDINE SOPRA L'ELLISSOIDE
    Public h As Double
    Public i As Integer
    Public i_Alarm As Boolean       'Condizione di allarme generale
    Public i_Assi As Integer       '0 = Tutti, 1 = Azimuth, 2 = Elevazione
    Public i_BackupAuto As String     'Condizione di back up
    ' = 0   Invia richiesta di stato
    ' = 1   Invia program track
    ' = 2   Durante altri invii
    Public MilSecR As Double, MilSecA As Double, MilSecC As Double
    Public MilSecP As Double
    Public MilSecN As Double, MilSecS As Double     'LNA, SWITCH CONTROL UNIT 29/08/2014
    Public MilSecT As Double                                    'Periodo Step-Track 18/12/2024
    '09/12/21
    Public MilSecSA As Double                                 'Spiral Acquire
    '
    Public i_Lna As String
    Public i_Par As Integer
    Public i_Phase As Integer          'indica se il calcolo della fase deve essere manuale o automatico
    Public i_PhaseFrm As Integer
    Public i_Pol As Byte           'Valore tipo polarizzazione
    ' = 0 Stati Stand-by, Step Track, Command Position
    ' = 1 Program Track
    ' = 2 Autotrack
    ' = 4 Augmented
    ' = 7 Automatic Acquire
    Public i_ric As Integer
    Public i_Switches As String
    Public i_VelAZ As Integer        '0 = motori in continua   1 - 1 Velocita'  2 - 2 velocita'
    Public i_VelEL As Integer        '0 = motori in continua   1 - 1 Velocita'  2 - 2 velocita'
    Public inizio As Double
    Public InputBuffer1 As String       'Buffer di Input #da rete per SENA
    Public InputBuffer2 As String       'Buffer di Input #da rete per MCS
    Public InputBuffer3 As String       'Buffer di Input #da rete per MCS
    Public ip_Agent As String       'indirizzo IP del polarizzatore in SNMP
    Public l_Tolleranza As Double
    ' Public    LanRx(1 To 9) As Lan_control_rec
    Public LanRx(0 To 8) As Port_control_rec
    Public LastMemory As Integer
    '       LATITUDINE (NORD)
    Public LATSTA As Double
    Public LockRic As String
    '       LONGITUDINE
    Public LONSTA As Double
    Public m_sizeX1 As Double
    Public m_sizeY1 As Double
    Public StepIni As Boolean
    Public All_DC As String
    Public StatusDC As String
    Public NowPar11 As Par11
    Public par11Sat As Par11
    Public PhaseValue As System.Single
    Public PhaseValue1 As System.Single
    Public PhaseValue2 As System.Single
    Public PhaseValueAz As System.Single
    Public PhasingDataFlow As System.Single
    Public PhasingMode As System.Single
    Public PolarizationAngle As System.Single
    Public Port_Control(0 To 32) As Port_control_rec
    Public PortaMain As Integer
    Public PortaRx As Integer
    Public Process As System.Single
    Public RequestDTR As Integer                'Flag di richiesta verso DTR: 1 = Phasing
    '2 = Calibration
    '3 = Phasing calibration table
    '4 = Extended phasing calibration table
    Public Response%
    Public RfInputFrequency As System.Single
    Public rs_Orbite As Object
    Public s_ACULR As String
    Public s_ACUStructure As String
    Public s_Appoggio As String
    Public s_Appoggio1 As String
    Public s_Az As String           'Memorizza valore di azimuth
    Public s_AzTS As String         'Stato Brake di azimuth
    Public s_BufferStructure As String
    Public s_El As String           'Memorizza valore di elevazione
    Public s_ElTS As String         'Stato brake di elevazione
    Public s_FileStars As String
    Public s_Latitudine As Double
    Public s_Lev As String          'Valore livello del valore somma
    Public s_Lev2 As String          'Valore livello del valore somma
    Public s_Lev3 As System.Single          'Valore livello precedente nella funzione step track
    Public d_LevAv(10) As System.Single    'Tabella valori beacon per media
    Public d_LevAve As System.Single        'Valore beacon medio durante step track
    Public s_LevAz As String        'errore di azimuth
    Public s_LevAz1 As String        'errore di azimuth da ricevitore
    Public s_LevEl As String        'errore di elevazione
    Public s_LevEl1 As String        'errore di elevazione da ricevitore
    Public s_Longitudine As Double
    Public d_TLEDate As Date
    Public d_TLEFile As Date
    Public s_Mod As String          'Modo operativo
    Public s_OpMode As String
    Public s_Ora As String
    Public s_Parametri As String
    Public s_POL As String          'Valore di polarizzazione
    Public s_Pol1 As String         'Valore di polarizzazione per inizializzazione antenna aggiunto il 10/11/2017
    Public s_ProgramNr As String
    Public s_ProgramRelease As String
    Public s_Satellite As String
    Public s_StatusPTIC(20) As Byte    'Buffer di stato verso Intelsat
    Public Salva As String
    Public SceltaFont As String
    Public SEND As String                                'Per invio dati generale
    '09/01/2019 Distinte le variabile stringa per ciascun tipo di device per evitare fastidiose sovrapposizione
    Public SEND_ACU As String                         'Per invio dati ACU
    Public SEND_MAC As String
    Public Send_GRF As String                         'Per invio dati GRF/DC
    Public Sin_Calcola As Boolean
    Public Sin_Date As Date, b_StepTrack As Boolean
    Public StatusPC As String
    Public SystemConfiguration As System.Single
    Public t_Pol As String          'Tipo polarizzazione
    Public TagDato As String                ' Buffer del dato con l'indicazione della data
    Public TagError As String
    Public Valid As System.Single
    Public ValorMedioAz As System.Single, ValorMedioEL As System.Single
    Public vBytes(15) As Byte       'Appoggio valori di ingresso
    Public vBytesold(15) As Byte       'Appoggio vecchi valori per controllo variazioni
    Public s_MsgAcu As String
    Public nSock1 As Integer
    Public intSock1Cnt As Integer
    'Aggiunti il 16/01/2017
    Public s_AZTrkCur1 As String   'Az Trk motor 1 current
    Public s_AZTrkCur2 As String   'Az Trk motor 2 current
    Public s_ELTrkCur1 As String   'El Trk motor 1 current
    Public s_ELTrkCur2 As String   'El Trk motor 2 current
    Public s_AZSlewCur1 As String   'Az Slew motor 1 current
    Public s_AZSlewCur2 As String   'Az Slew motor 2 current
    Public s_ELSlewCur1 As String   'El Slew motor 1 current
    Public s_ELSlewCur2 As String   'El Slew motor 2 current
    Public ValVolt As System.Single
    '01/09/2020                             'aggiunta variante per controllo augmented
    Public LanMain(0 To 8) As Lan_control_rec
    ' Tabella Port_control
    Public Structure Port_control_rec
        Public Com As Integer
        Public Canale As Integer        ' canale
        Public Port_settings As String  ' parametri di settaggio della porta
        Public Time_out As Double       ' time out in ricezione messaggi in
        Public Buffer As String         ' buffer dei dati in ingresso
        Public Port_State As Object    ' stato della porta
        Public Rec_State As Integer     ' stato della ricezione
        Public CKS_Error As Integer     ' errori di CKS
        Public Socket As String            ' S o N indica se e' in rete
        Public IPAdress As String       ' Indirizzo IP interessato
        Public LanPort As Double        ' Porta di uscita
        Public LocalPort As Double        ' Porta di uscita
        Public Listen As String            'S/N indica se sull'indirizzo occorre fare una connect o una listen
    End Structure

    Public Structure Lan_control_rec
        Public Com As Integer
        Public Canale As Integer        ' canale
        Public Port_settings As String  ' parametri di settaggio della porta
        Public Time_out As Double       ' time out in ricezione messaggi in
        Public Buffer As String         ' buffer dei dati in ingresso
        Public Port_State As Object    ' stato della porta
        Public Rec_State As Integer     ' stato della ricezione
        Public CKS_Error As Integer     ' errori di CKS
        Public Socket As String            ' S o N indica se e' in rete
        Public IPAdress As String       ' Indirizzo IP interessato
        Public LanPort As Double        ' Porta di uscita
        Public LocalPort As Double        ' Porta di uscita
        Public Listen As String            'S/N indica se sull'indirizzo occorre fare una connect o una listen
    End Structure

    ' Tabella Canale
    Public Structure Canale_rec
        Public Lbl As String           ' label
        Public Port_IN As Integer      ' porta di input
        Public Port_OUT As Integer     ' porta di output
        Public Prot_entity As String   ' valore da leggere nel protocollo
        Public File_Log As String      ' nome del file di log
        Public Inter As Integer        ' valore dell'intervallo di interrogazione
    End Structure

    Public Structure Par11
        Public LM0 As Double
        Public LM1 As Double
        Public LM2 As Double
        Public LONC As Double
        Public LONC1 As Double
        Public LONS As Double
        Public LONS1 As Double
        Public LATC As Double
        Public LATC1 As Double
        Public LATS As Double
        Public LATS1 As Double
        '
        Public EPOCYE As String         'porzione di anno sul quale sono basate le effemeridi
        Public EPOCMO As String          'mese
        Public EPOCDA As String         'giorno
        Public EPOCHO As String         'ore
        Public EPOCMI As String          'minuto
        Public EPOCSE As String          'secondi
    End Structure
    Public Structure PuntoGrafico
        Public data As Date
        Public X As System.Single
        Public y As System.Single
        Public c As System.Single
    End Structure


    ' Public    Structure TwoLine
    'End Structure

    Public Structure PuntoGrafico2
        Public X As Date
        Public y As System.Single
    End Structure
    'Variabili per il comando proiezione
    Public Structure TrackPoint
        Public T As Double   ' timestamp come numero seriale VB
        Public az As Double   ' azimut in gradi decimali
        Public el As Double   ' elevazione in gradi decimali
        Public alt As Double       'altezza in metri
    End Structure

    Public pts() As TrackPoint
    Public nextPt As TrackPoint
    Public GetGrado(4) As Integer

End Module
