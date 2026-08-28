Imports System.Runtime.InteropServices
Imports System.Text

Public Class HTTP

    Private hInternetSession As IntPtr
    Private hInternetConnect As IntPtr
    Private hHttpOpenRequest As IntPtr

    Private Sub HTTP_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.StartPosition = FormStartPosition.CenterScreen
        SetStatus("InternetOpen")
        hInternetSession = InternetOpen(scUserAgent, INTERNET_OPEN_TYPE_PRECONFIG, String.Empty, String.Empty, 0)
        If hInternetSession <> IntPtr.Zero Then
            SetStatus("Ready")
        Else
            SetStatus("InternetOpen failed. Error: " & Marshal.GetLastWin32Error())
        End If
    End Sub

    Private Sub BtClear_Click(sender As Object, e As EventArgs) Handles BtClear.Click
        For Each ctrl As Control In Me.Controls
            Select Case ctrl.GetType().Name
                Case "Label"
                    If ctrl.Tag IsNot Nothing AndAlso CBool(ctrl.Tag) Then
                        ctrl.Text = String.Empty
                    End If
                Case "TextBox"
                    ctrl.Text = String.Empty
            End Select
        Next
        BtSend.Enabled = True
        SetStatus("Ready")
    End Sub

    Private Sub BtExit_Click(sender As Object, e As EventArgs) Handles BtExit.Click
        If hHttpOpenRequest <> IntPtr.Zero Then InternetCloseHandle(hHttpOpenRequest)
        If hInternetConnect <> IntPtr.Zero Then InternetCloseHandle(hInternetConnect)
        If hInternetSession <> IntPtr.Zero Then InternetCloseHandle(hInternetSession)
        Me.Close()
    End Sub

    Private Sub BtGet_Click(sender As Object, e As EventArgs) Handles BtGet.Click
        BtGet.Enabled = False
        Cursor.Current = Cursors.WaitCursor
        Try
            Dim buffer(2047) As Byte
            Dim lBytesRead As Integer
            Dim sb As New StringBuilder()

            ProgressBar1.Minimum = 0
            Dim contentLen As Integer = 0
            Integer.TryParse(LblContentLength.Text.Trim(), contentLen)
            If contentLen > 0 Then ProgressBar1.Maximum = contentLen
            ProgressBar1.Value = 0
            SetStatus("InternetReadFile")

            Do
                Array.Clear(buffer, 0, buffer.Length)
                Dim ok As Integer = InternetReadFile(hHttpOpenRequest, buffer, buffer.Length, lBytesRead)
                If ok = 0 OrElse lBytesRead = 0 Then Exit Do
                sb.Append(Encoding.Default.GetString(buffer, 0, lBytesRead))
                If contentLen > 0 Then
                    ProgressBar1.Value = Math.Min(ProgressBar1.Value + lBytesRead, ProgressBar1.Maximum)
                End If
            Loop

            TxtHTML.Text = sb.ToString()
            ProgressBar1.Value = 0
            SetStatus("Ready")
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub BtSend_Click(sender As Object, e As EventArgs) Handles BtSend.Click
        Cursor.Current = Cursors.WaitCursor
        BtSend.Enabled = True
        Dim sStatus As String = ""

        Try
            If hInternetSession = IntPtr.Zero Then
                sStatus = "InternetOpen call failed. Error: " & Marshal.GetLastWin32Error()
                Return
            End If

            SetStatus("InternetQueryOption")
            Dim vDllVersion As WinInetDLLVersion
            Dim optLen As Integer = Marshal.SizeOf(vDllVersion)
            InternetQueryOption(hInternetSession, INTERNET_OPTION_VERSION, vDllVersion, optLen)
            LblMajor.Text = vDllVersion.lMajorVersion.ToString()
            LblMinor.Text = vDllVersion.lMinorVersion.ToString()

            SetStatus("InternetConnect")
            hInternetConnect = InternetConnect(hInternetSession, CheckUrl(), INTERNET_DEFAULT_HTTP_PORT,
                                               String.Empty, String.Empty, INTERNET_SERVICE_HTTP, 0, IntPtr.Zero)
            If hInternetConnect = IntPtr.Zero Then
                sStatus = "InternetConnect call failed. Error: " & Marshal.GetLastWin32Error()
                Return
            End If

            SetStatus("HttpOpenRequest")
            hHttpOpenRequest = HttpOpenRequest(hInternetConnect, "GET", String.Empty, "HTTP/1.0",
                                               String.Empty, IntPtr.Zero, INTERNET_FLAG_RELOAD, IntPtr.Zero)
            If hHttpOpenRequest = IntPtr.Zero Then
                sStatus = "HttpOpenRequest call failed. Error: " & Marshal.GetLastWin32Error()
                Return
            End If

            SetStatus("HttpSendRequest")
            Dim iRetVal As Integer = HttpSendRequest(hHttpOpenRequest, String.Empty, 0, IntPtr.Zero, 0)
            If iRetVal = 0 Then
                sStatus = "HttpSendRequest call failed. Error: " & Marshal.GetLastWin32Error()
                Return
            End If

            SetStatus("HttpQueryInfo")
            GetQueryInfo(hHttpOpenRequest, LblContentType, HTTP_QUERY_CONTENT_TYPE)
            GetQueryInfo(hHttpOpenRequest, LblContentLength, HTTP_QUERY_CONTENT_LENGTH)
            GetQueryInfo(hHttpOpenRequest, LblLastModified, HTTP_QUERY_LAST_MODIFIED)
            GetQueryInfo(hHttpOpenRequest, LblVersion, HTTP_QUERY_VERSION)
            GetQueryInfo(hHttpOpenRequest, LblStatusCode, HTTP_QUERY_STATUS_CODE)
            GetQueryInfo(hHttpOpenRequest, LblStatusText, HTTP_QUERY_STATUS_TEXT)
            GetQueryInfo(hHttpOpenRequest, LblRawHeaders, HTTP_QUERY_RAW_HEADERS)
            GetQueryInfo(hHttpOpenRequest, TxtResponseHeaders, HTTP_QUERY_RAW_HEADERS_CRLF)
            GetQueryInfo(hHttpOpenRequest, LblForwarded, HTTP_QUERY_FORWARDED)
            GetQueryInfo(hHttpOpenRequest, LblServer, HTTP_QUERY_SERVER)
            GetQueryInfo(hHttpOpenRequest, LblRequestMethod, HTTP_QUERY_REQUEST_METHOD)
            GetQueryInfo(hHttpOpenRequest, LblPragma, HTTP_QUERY_FLAG_REQUEST_HEADERS Or HTTP_QUERY_PRAGMA)
            GetQueryInfo(hHttpOpenRequest, TxtRequestHeaders, HTTP_QUERY_FLAG_REQUEST_HEADERS Or HTTP_QUERY_RAW_HEADERS_CRLF)
            GetQueryInfo(hHttpOpenRequest, LblUserAgent, HTTP_QUERY_FLAG_REQUEST_HEADERS Or HTTP_QUERY_USER_AGENT)
            GetQueryInfo(hHttpOpenRequest, LblRequestMethod2, HTTP_QUERY_FLAG_REQUEST_HEADERS Or HTTP_QUERY_REQUEST_METHOD)

            sStatus = "Ready"
            BtSend.Enabled = False
            BtGet.Enabled = True
        Finally
            SetStatus(sStatus)
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function GetQueryInfo(ByVal hRequest As IntPtr, ByVal ctrl As Control, ByVal iInfoLevel As Integer) As Boolean
        Dim sb As New StringBuilder(1024)
        Dim lLen As Integer = sb.Capacity
        Dim lIndex As Integer = 0
        Dim result As Boolean = CBool(HttpQueryInfo(hRequest, iInfoLevel, sb, lLen, lIndex))
        ctrl.Text = sb.ToString()
        Return result
    End Function

    Private Function CheckUrl() As String
        If TxtURL.Text.Length = 0 Then TxtURL.Text = "celestrak.com/NORAD/"
        Return TxtURL.Text
    End Function

    Private Sub TabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabMain.SelectedIndexChanged
        ' TabControl in VB.NET gestisce automaticamente la visualizzazione delle schede.
    End Sub

    Public Sub SetStatus(sStatus As String)
        sbrMain.Panels(0).Text = sStatus
        Application.DoEvents()
    End Sub

End Class
