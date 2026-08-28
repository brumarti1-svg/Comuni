Imports System.Text

Module WindowsAPI
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Object, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Long, ByVal lpFileName As String) As Long
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Object, ByVal lpString As Object, ByVal lpFileName As String) As Long
    Declare Function MessageBeep Lib "user32" (ByVal wStructure As Long) As Long
    Public Declare Function Beep Lib "kernel32" (ByVal dwFreq As Long, ByVal dwDuration As Long) As Long
    'Declare Function GetPrinter Lib "winspool" Alias "GetPrinterA" (ByVal hPrinter As Long, ByVal Level As Long, pPrinter As Byte, ByVal cbBuf As Long, pcbNeeded As Long) As Boolean
    Declare Function GetDiskFreeSpace Lib "kernel32" Alias "GetDiskFreeSpaceA" (ByVal lpRootPathName As String, lpSectorsPerCluster As Long, lpBytesPerSector As Long, lpNumberOfFreeClusters As Long, lpTtoalNumberOfClusters As Long) As Long
    Declare Function GetFileSize Lib "kernel32" (ByVal hFile As Long, lpFileSizeHigh As Long) As Long
    Declare Function GetSystemDirectory Lib "kernel32" Alias "GetSystemDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long
    Declare Function GetSystemMetrics Lib "user32" (ByVal nIndex As Long) As Long
    ' System.Math.tanti
    Public Const MB_ICONASTERISK = &H40&
    Public Const MB_ICONEXCLAMATION = &H30&
    Public Const MB_ICONHAND = &H10&
    Public Const MB_ICONINFORMATION = MB_ICONASTERISK
    Public Const MB_ICONMASK = &HF0&
    Public Const MB_ICONQUESTION = &H20&
    Public Const MB_ICONSTOP = MB_ICONHAND
    Public Const MB_OK = &H0&

    '   Dichiarazioni del sistema di Guida.

    Declare Function WinHelp Lib "user32" Alias "WinHelpA" (ByVal hwnd As Long, ByVal lpHelpFile As String, ByVal wCommand As Long, ByVal dwData As Long) As Long
    Public Const HELP_COMMAND = &H102&
    Public Const HELP_CONTENTS = &H3&
    Public Const HELP_CONTEXT = &H1          '  Visualizza l'argomento in ulTopic
    Public Const HELP_CONTEXTPOPUP = &H8&
    Public Const HELP_FINDER = &HB
    Public Const HELP_FORCEFILE = &H9&
    Public Const HELP_HELPONHELP = &H4       '  Visualizza la Guida sull'uso della Guida
    Public Const HELP_INDEX = &H3            '  Visualizza l'indice
    Public Const HELP_KEY = &H101            '  Visualizza l'argomento per la parola chiave in offabData
    Public Const HELP_MULTIKEY = &H201&
    Public Const HELP_PARTIALKEY = &H105&
    Public Const HELP_QUIT = &H2             '  Chiude la Guida
    Public Const HELP_SETCONTENTS = &H5&
    Public Const HELP_SETINDEX = &H5         '  Imposta l'indice corrente per Guide con più indici
    Public Const HELP_SETWINPOS = &H203&

    Public Structure MULTIKEYHELP
        Public mkSize As Integer
        <VBFixedString(1)> Public mkKeylist As String
        <VBFixedString(253)> Public szKeyphrase As String
    End Structure


    ' gestione form
    Public Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As Long, ByVal bRevert As Long) As Long
    Public Declare Function DeleteMenu Lib "user32" (ByVal hMenu As Long, ByVal nPosition As Long, ByVal wFlags As Long) As Long
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Object) As Long
    Public Declare Function SENDMessage Lib "user32" _
Alias "SENDMessageA" _
(ByVal hwnd As Long,
ByVal wMsg As Long,
ByVal wParam As Long,
lParam As String) As Long
    Public Const SC_CLOSE = &HF060
    Public Const SC_MOVE = &HF010
    Public Const SC_SIZE = &HF000
    Public Const MF_BYCOMMAND = &H0&
    Public Const WM_NCACTIVATE = &H86
    Public Const SC_ARRANGE = &HF110
    Public Const SC_KEYMENU = &HF100
    Public Const SC_MAXIMIZE = &HF030
    Public Const SC_MINIMIZE = &HF020
    Public Const SC_MOUSEMENU = &HF090
    Public Const SC_RESTORE = &HF120
    Public Const SC_SCREENSAVE = &HF140

    'Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
    'Public Const SWP_NOMOVE = 2
    'Public Const SWP_NOSIZE = 1
    'Public Const FLAGS = SWP_NOMOVE Or SWP_NOSIZE
    'Public Const HWND_TOPMOST = -1
    'Public Const HWND_NOTOPMOST = -2


    Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
    ' SetWindowPos Flags
    Public Const SWP_NOSIZE = &H1
    Public Const SWP_NOMOVE = &H2
    Public Const SWP_NOZORDER = &H4
    Public Const SWP_NOREDRAW = &H8
    Public Const SWP_NOACTIVATE = &H10
    Public Const SWP_FRAMECHANGED = &H20        '  The frame changed: SEND WM_NCCALCSIZE
    Public Const SWP_SHOWWINDOW = &H40
    Public Const SWP_HIDEWINDOW = &H80
    Public Const SWP_NOCOPYBITS = &H100
    Public Const SWP_NOOWNERZORDER = &H200      '  Don't do owner Z ordering

    Public Const SWP_DRAWFRAME = SWP_FRAMECHANGED
    Public Const SWP_NOREPOSITION = SWP_NOOWNERZORDER

    ' SetWindowPos() hwndInsertAfter values
    Public Const HWND_TOP = 0
    Public Const HWND_BOTTOM = 1
    Public Const HWND_TOPMOST = -1
    Public Const HWND_NOTOPMOST = -2

    Public Const FLAGS = SWP_NOMOVE Or SWP_NOSIZE

    ' lettura nome utente
    Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, nSize As Long) As Long

    ' gestione della tastiera
    Declare Function GetKeyboardState Lib "user32" (pbKeyState As Byte) As Long


    ' gestione processi
    ' STARTUPINFO
    Public Structure STARTUPINFO
        Public cb As Long
        Public lpReserved As String
        Public lpDesktop As String
        Public lpTitle As String
        Public dwX As Long
        Public dwY As Long
        Public dwXSize As Long
        Public dwYSize As Long
        Public dwXCountChars As Long
        Public dwYCountChars As Long
        Public dwFillAttribute As Long
        Public dwFlags As Long
        Public wShowWindow As Integer
        Public cbReserved2 As Integer
        Public lpReserved2 As Long
        Public hStdInput As Long
        Public hStdOutput As Long
        Public hStdError As Long
    End Structure
    ' PROCESS_INFORMATION
    Public Structure PROCESS_INFORMATION
        Public hProcess As Long
        Public hThread As Long
        Public dwProcessID As Long
        Public dwThreadID As Long
    End Structure
    ' CreateProcessA
    Declare Function CreateProcessA Lib "kernel32" (ByVal lpApplicationName As Long, ByVal lpCommandLine As String, ByVal lpProcessAttributes As Long, ByVal lpThreadAttributes As Long, ByVal bInheritHandles As Long, ByVal dwCreationFlags As Long, ByVal lpEnvironment As Long, ByVal lpCurrentDirectory As Long, lpStartupInfo As STARTUPINFO, lpProcessInformation As PROCESS_INFORMATION) As Long
    ' CloseHandle Lib
    Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long
    ' TerminateProcess
    Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As Long, ByVal uExitCode As Long) As Long

    ' NORMAL_PRIORITY_CLASS
    Public Const NORMAL_PRIORITY_CLASS = &H20&
    ' INFINITE
    Public Const INFINITE = -1&

    Public Const STARTF_USESHOWWINDOW = &H1
    Public Const SW_HIDE = 0




End Module
