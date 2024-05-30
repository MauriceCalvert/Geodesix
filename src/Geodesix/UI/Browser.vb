' --------------------------------------------------------------------
' Geodesix
' Copyright © 2024 Maurice Calvert
' 
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
' 
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
' 
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
' --------------------------------------------------------------------
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Microsoft.Web.WebView2.Core
Imports Seting
Imports Utilities


Public Class Browser

    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Boolean
    End Function
    <DllImport("user32.dll")>
    Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    Public ReadOnly Property BrowserReady As Boolean
    Private Property BrowserNavigationComplete As Boolean
    Private ReadOnly Property Excel As Object
    Private Property URL As String
    Private Property Fragment As String
    Private Property Home As String
    Public Sub New(excel As Object, home As String, url As String, fragment As String)

        InitializeComponent()
        Me.Excel = excel
        Me.Home = home
        Me.URL = url
        Me.Fragment = fragment

    End Sub
    Private Sub Browser_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsDesignerHosted(Me) Then
            Exit Sub
        End If

        Dim t As Task = InitialiseWebview()

        t.ContinueWith(Sub() BeginInvoke(Sub() Navigate(URL, Fragment)))

    End Sub

    Public WithEvents CoreWebView2 As CoreWebView2
    Private Async Function InitialiseWebview() As Task

        ' https://learn.microsoft.com/en-us/dotnet/api/microsoft.web.webview2.winforms.webview2?view=webview2-dotnet-1.0.2210.55
        AddHandler WebView1.CoreWebView2InitializationCompleted, AddressOf Browser_CoreWebView2InitializationCompleted
        Await InitialiseAsync()

    End Function
    Private Async Function InitialiseAsync() As Task

        Dim localAppData As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim cacheFolder As String = Path.Combine(GetTempFolder("WebView"), Path.GetRandomFileName)

        Dim options As CoreWebView2EnvironmentOptions = New CoreWebView2EnvironmentOptions
        options.Language = Settings.Language
        options.EnableTrackingPrevention = False

        Dim task As Task(Of CoreWebView2Environment) = CoreWebView2Environment.CreateAsync(Nothing, cacheFolder, options)
        Dim envir As CoreWebView2Environment = task.Result

        Await WebView1.EnsureCoreWebView2Async(envir)

    End Function
    Private Sub Browser_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs)

        If e.InitializationException IsNot Nothing Then
            Throw e.InitializationException
        End If

        CoreWebView2 = WebView1.CoreWebView2

        CoreWebView2.Settings.IsWebMessageEnabled = True

        CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.Image)

        _BrowserReady = True

    End Sub
    Private Sub CoreWebView_NavigationStarting(sender As Object, e As CoreWebView2NavigationStartingEventArgs) _
        Handles CoreWebView2.NavigationStarting

        Dim url As String = Uri.UnescapeDataString(e.Uri)
        url = url.Split("#")(0) ' remove fragment
        Dim extension As String = Path.GetExtension(url)

        If e.NavigationKind = CoreWebView2NavigationKind.NewDocument Then

            ' Only local HTML files get opened in this browser.
            ' Everything else (in particular HTM without the L) gets opened externally

            If extension <> ".html" Then

                e.Cancel = True

                If url.BeginsWith("file://") Then

                    url = url.Substring("file://".Length)
                    ' Sometimes 'file:///', fix that
                    url = url.TrimStart({"/"c})

                End If

                Select Case True

                    Case extension.BeginsWith(".xls")
                        ' We can't open a workbook here, fire up a task to do it
                        Task.Run(
                        Sub()
                            Try
                                Dim wb As Object = Excel.Workbooks.Open(url)
                                ' He won't be in the foreground. Bring him forward
                                Dim caption As String = Excel.Caption
                                Dim handler As IntPtr = FindWindow(Nothing, caption)
                                SetForegroundWindow(handler)

                            Catch ex As Exception
                                ShowError(ex.Message)
                            End Try
                        End Sub)
                        ' We have to close, we're switching to a new workbook
                        BeginInvoke(Sub() Close())

                    Case extension = ".htm"
                        OpenBrowser(url & Fragment)

                    Case Else ' .CSS, .TXT, etc.
                        Try
                            Shell("C:\Program Files (x86)\Notepad++\Notepad++.exe", Quoted(url))
                        Catch ex As Exception
                            Shell("Notepad.exe", Quoted(url))
                        End Try

                End Select
                Exit Sub
            End If ' extension <> ".html"
        End If

        SetText()
        BrowserNavigationComplete = False
        btnBack.Enabled = False
        btnForward.Enabled = False
        btnHome.Enabled = False
        Me.Cursor = Cursors.WaitCursor

    End Sub
    Private Sub CoreWebView_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) _
        Handles CoreWebView2.NavigationCompleted

        Me.Cursor = Cursors.Default
        btnBack.Enabled = CoreWebView2.CanGoBack
        btnForward.Enabled = CoreWebView2.CanGoForward
        btnHome.Enabled = True

        BrowserNavigationComplete = True
        Refresh()

    End Sub
    Private Sub CoreWebView_ProcessFailed(sender As Object, e As CoreWebView2ProcessFailedEventArgs) _
        Handles CoreWebView2.ProcessFailed

        ShowError($"Browser process failed {e.ProcessFailedKind} {e.Reason} {e.ProcessDescription}")

    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        CoreWebView2.GoBack()
        SetText()

    End Sub
    Private Sub btnForward_Click(sender As Object, e As EventArgs) Handles btnForward.Click

        CoreWebView2.GoForward()
        SetText()

    End Sub
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click

        Navigate(Home, "")

    End Sub
    Public Sub Navigate(url As String, Optional fragment As String = "")

        Me.URL = url
        Me.Fragment = fragment
        Dim builder As New UriBuilder(url)
        Dim uri As Uri = builder.Uri

        Dim target As String = uri.AbsoluteUri
        If Not IsEmpty(fragment) Then
            target = target & "#" & fragment
        End If

        WebView1.Source = New Uri(target)

    End Sub
    Private Sub SetText()

        Dim title As String = Source()
        Dim sp As Integer = title.LastIndexOf("\")
        If sp >= 0 Then
            title = title.Substring(sp + 1)
        End If
        Me.Text = $"Geodesix Help - {title}"


    End Sub
    Private Function Source() As String

        Dim result As String = URL

        If Not IsEmpty(Fragment) Then
            result = result & "#" & Fragment
        End If

        Return result

    End Function
End Class