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
Imports System.Threading.Tasks
Imports Microsoft.Web.WebView2.Core
Imports Seting
Imports Utilities
Partial Public Class Map

    Public WithEvents CoreWebView2 As CoreWebView2
    Private Async Function InitaialiseWebview() As Task

        CustomLog.Logger.Debug($"Map {ID} InitaialiseWebview")

        ' https://learn.microsoft.com/en-us/dotnet/api/microsoft.web.webview2.winforms.webview2?view=webview2-dotnet-1.0.2210.55
        AddHandler WebView.CoreWebView2InitializationCompleted, AddressOf Browser_CoreWebView2InitializationCompleted
        Await InitialiseAsync()

    End Function
    Private Async Function InitialiseAsync() As Task

        Dim localAppData As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim cacheFolder As String = Path.Combine(GetTempFolder("webview"), Path.GetRandomFileName)

        Dim options As CoreWebView2EnvironmentOptions = New CoreWebView2EnvironmentOptions
        options.Language = Settings.Language
        options.EnableTrackingPrevention = False

        Dim task As Task(Of CoreWebView2Environment) = CoreWebView2Environment.CreateAsync(Nothing, cacheFolder, options)
        Dim envir As CoreWebView2Environment = task.Result

        Await WebView.EnsureCoreWebView2Async(envir)

    End Function
    Private Sub Browser_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs)

        If e.InitializationException IsNot Nothing Then
            Throw e.InitializationException
        End If

        CoreWebView2 = WebView.CoreWebView2

        CoreWebView2.Settings.IsWebMessageEnabled = True

        CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.Image)

        CustomLog.Logger.Debug($"Map {ID} browser ready")

        _BrowserReady = True

    End Sub
End Class
