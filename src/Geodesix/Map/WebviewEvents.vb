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
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Windows.Forms
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Utilities
Partial Public Class Map

    Private PostNavigated As List(Of Action)

    Private Sub Browser_MessageReceived(ByVal sender As Object, ByVal e As CoreWebView2WebMessageReceivedEventArgs) _
        Handles CoreWebView2.WebMessageReceived

        Try
            Dim msg As String = e.WebMessageAsJson
            Dim obj As JObject = JsonConvert.DeserializeObject(Of JObject)(msg)
            Dim command As String = obj("command").ToString
            Dim data As String = obj("data").ToString

            CustomLog.Logger.Debug($"Browser message {command} {data}")

            Select Case command

                Case "geodesix"
                    Select Case data
                        Case "ribbon"
                            GeodesiXEXL.RibbonUI.ActivateTab(GeodesiXEXL.GeodesiXRibbonTab.Id)
                    End Select

                Case "rightclick"
                    ' Convert Google US-EN format into local format
                    Dim coords() As String = data.Split(","c)
                    Dim lat As String = Double.Parse(coords(0), System.Globalization.CultureInfo.InvariantCulture).ToString
                    Dim lng As String = Double.Parse(coords(1), System.Globalization.CultureInfo.InvariantCulture).ToString
                    Dim latlon As String = lat & LISTSEP & lng
                    ClickLocation = latlon
                    txtFind.Text = latlon

                    If AddMarkerRow(lat, lng) Then
                        ReGenerate()
                    End If

                Case "zoomChanged"
                    Integer.TryParse(data.ToString, _Zoom)
                    txtZoom.Text = Zoom.ToString

            End Select

        Catch ex As Exception
            CustomLog.Logger.Error("Browser_MessageReceived failed {0}", ex.Message)
        End Try

    End Sub
    Private WithEvents DownLoadOperation As CoreWebView2DownloadOperation
    Private Sub CoreWebView_DownloadStarting(sender As Object, e As CoreWebView2DownloadStartingEventArgs) _
        Handles CoreWebView2.DownloadStarting

        DownLoadOperation = e.DownloadOperation
        StatusStrip1.Visible = True
        StatusText.Text = ""
        Progress.Minimum = 0
        Progress.Maximum = 100
        Progress.Value = 0

    End Sub

    Private Sub DownLoadOperation_BytesReceivedChanged(sender As Object, e As Object) _
        Handles DownLoadOperation.BytesReceivedChanged

        Dim total As ULong? = DownLoadOperation.TotalBytesToReceive

        If total.HasValue Then

            Dim received As Long = DownLoadOperation.BytesReceived
            Progress.Visible = True
            StatusText.Text = $"{received}\1000 K/{total.Value}\1000 K"
            Progress.Value = CInt(CULng(received * 100) \ total.Value)

        End If

    End Sub

    Private Sub CoreWebView_NavigationStarting(sender As Object, e As CoreWebView2NavigationStartingEventArgs) _
        Handles CoreWebView2.NavigationStarting

        BrowserNavigationComplete = False

        Status("")
        Progress.Minimum = 0
        Progress.Maximum = 100
        Progress.Value = 0
        btnBack.Enabled = False
        btnForward.Enabled = False
        btnReload.Enabled = False
        btnStop.Enabled = True
        RaiseEvent startNavigating(Me, New EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Refresh()

    End Sub

    Private Sub CoreWebView_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) _
        Handles CoreWebView2.NavigationCompleted

        Progress.Visible = False
        If Not e.IsSuccess Then
            Dim msg As String = $"Navigation error {[Enum].GetName(GetType(CoreWebView2WebErrorStatus), e.WebErrorStatus)}"
            CustomLog.Logger.Error(msg)
            StatusError(msg)
        End If
        Me.Cursor = Cursors.Default
        btnBack.Enabled = CoreWebView2.CanGoBack
        btnForward.Enabled = CoreWebView2.CanGoForward
        btnRefreshMapSheet.Enabled = URL <> ""
        Progress.Visible = False
        btnReload.Enabled = True
        btnStop.Enabled = False

        BrowserNavigationComplete = True

        If PostNavigated IsNot Nothing Then
            For Each postnav As Action In PostNavigated
                BeginInvoke(Sub() postnav())
            Next postnav
            PostNavigated = Nothing
        End If

        Refresh()

        RaiseEvent finishNavigating(Me, New EventArgs)

    End Sub
    Private Sub CoreWebView_ProcessFailed(sender As Object, e As CoreWebView2ProcessFailedEventArgs) _
        Handles CoreWebView2.ProcessFailed

        ShowError($"Browser process failed {e.ProcessFailedKind} {e.Reason} {e.ProcessDescription}")

    End Sub
    ' KeyDown/Keyup behaviour is bizarre for Webview
    Private ControlPressed As Boolean
    Private Sub WebView_KeyDown(sender As Object, e As KeyEventArgs) Handles WebView.KeyDown

        If e.KeyCode = Keys.ControlKey Then
            ControlPressed = True
        ElseIf ControlPressed AndAlso e.KeyCode = Keys.C Then
            btnCopy_Click(sender, e)
        End If

    End Sub
    Private Sub WebView_KeyUp(sender As Object, e As KeyEventArgs) Handles WebView.KeyUp

        If e.KeyCode = Keys.ControlKey Then
            ControlPressed = False
        End If
    End Sub
End Class
