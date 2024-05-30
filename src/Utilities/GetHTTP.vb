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
Imports System.Net
Imports System.Net.Cache

Public Module GetHTTP_

    Public Function GetHTTP(ByVal url As String, ByRef result As String, Optional ByVal timeout As Integer = 5000) As Boolean

        Dim request As WebRequest = Nothing
        Dim response As WebResponse = Nothing
        Dim OK As Boolean = False
        Dim dataStream As Stream = Nothing
        Dim reader As StreamReader = Nothing


        Try
            result = ""
            request = WebRequest.Create(url) ' he will create a new URI, which will urlencode

            ' Deal with proxies.
            If WebRequest.DefaultWebProxy Is Nothing Then
                result = "Unable to detect web proxy configuration"
            Else
                request.Proxy = WebRequest.DefaultWebProxy
            End If

            request.Proxy.Credentials = CredentialCache.DefaultCredentials
            request.Credentials = CredentialCache.DefaultCredentials

            ' use cache if available 
            Dim rcp As New RequestCachePolicy(RequestCacheLevel.CacheIfAvailable)
            request.CachePolicy = rcp

            request.Timeout = timeout

            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            reader = New StreamReader(dataStream)
            result = reader.ReadToEnd
            OK = True

        Catch ex As Exception

            result = ex.Message
            OK = False

        Finally
            reader?.Close()
            response?.Close()
            dataStream?.Close()

        End Try

        Return OK

    End Function

End Module
