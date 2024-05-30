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
Imports System.Web.HttpUtility
Imports Google.OpenLocationCode
Imports Seting
Imports Utilities
'
' Need reference to System.Web.Extensions
'
Partial Public Class Geodesic

    ''' <summary>
    ''' Talks to Google to resolve a geocoding request (Geocode or Directions).
    ''' This module runs synchronously and won't return until conversation with Google is complete.
    ''' (We are called by Geodesic.Worker).
    ''' </summary>
    ''' <param name="query">The Query to resolve</param>
    ''' <remarks>
    ''' Uses GoogleMap's JSON format rather than XML for efficiency.
    ''' </remarks>
    Private Sub Resolve(ByVal query As Query)

        Try
            ' #6#
            Dim url As String
            Dim done As Boolean = False

            CustomLog.Logger.Debug("Resolving {0}", query)

            query.Status = "Requesting" ' Make sure query.status changes, whatever happens

            Select Case query.GetType

                Case GetType(GeoQuery)

                    Dim location As String = query.Key

                    ' Deal with reverse geocodes in differing cultures
                    If location.Contains(LISTSEP) Then
                        Dim parts() As String
                        parts = location.Split(LISTSEP)
                        If parts.Length = 2 Then
                            Dim lat As Double
                            Dim lng As Double
                            If Double.TryParse(parts(0), lat) Then
                                If Double.TryParse(parts(1), lng) Then
                                    location = lat.ToString(System.Globalization.CultureInfo.InvariantCulture) &
                                           "," &
                                           lng.ToString(System.Globalization.CultureInfo.InvariantCulture)
                                End If
                            End If
                        End If
                    ElseIf location.Contains("+") Then ' Open Location code?
                        Try
                            Dim code = OpenLocationCode.Decode(location)
                            location = code.CenterLatitude.ToString & "," & code.CenterLongitude.ToString
                        Catch ex As Exception
                        End Try
                    End If

                    url = Settings.GeocodeURL & "&address=" & UrlEncode(location)

                Case GetType(TravelQuery)

                    Dim q As TravelQuery = DirectCast(query, TravelQuery)
                    url = Settings.DirectionsURL &
                          "&origin=" & EncodeURL(q.Origin) &
                          "&destination=" & EncodeURL(q.Destination) &
                          "&mode=" & q.Mode &
                          "&units=metric"

                Case Else
                    Throw New ArgumentException("Query type '" & query.GetType.Name & "' is invalid")

            End Select

            If _Language <> "" Then
                url = url & "&language=" & Language
            End If

            If _Region <> "" Then
                url = url & "&region=" & _Region
            End If

            url = url & "&key=" & Settings.APIKey

            Dim sw As New Stopwatch
            sw.Start()

            ' Make request to GoogleMaps REST API.
            Dim response As HttpWebResponse = Nothing
            Try
                Dim request As WebRequest = WebRequest.Create(url) ' he will create a new URI, which will urlencode

                request.Timeout = CInt(Settings.WebTimeout)

                ' Deal with proxies.
                If WebRequest.DefaultWebProxy Is Nothing Then
                    Throw New WebException("Unable to detect web proxy configuration")
                Else
                    request.Proxy = WebRequest.DefaultWebProxy
                End If

                request.Proxy.Credentials = CredentialCache.DefaultCredentials
                request.Credentials = CredentialCache.DefaultCredentials

                response = CType(request.GetResponse(), HttpWebResponse) ' #7#

                InterpretResponseStatus(response, query)

            Catch ex As WebException
                query.Status = "!" & ex.Status.ToString

            Catch ex As Exception
                query.Status = "!HTTP request to GoogleMaps failed: " & ex.Message

            Finally
                CustomLog.Logger.Debug("Google got {0} in {1}", query.Key, sw.Elapsed)
                sw.Stop()
                response?.Close()

            End Try

            CustomLog.Logger.Debug("Resolved {0}", query)

        Catch ex As Exception
            HandleError("Resolve failed", ex)
        End Try
    End Sub
    Private Sub InterpretResponseStatus(response As HttpWebResponse, query As Query)

        Dim reader As StreamReader
        Dim jsonstring As String

        query.Status = "Response " & response.StatusCode

        Select Case response.StatusCode

            Case HttpStatusCode.OK

                Using dataStream As Stream = response.GetResponseStream()
                    reader = New StreamReader(dataStream)
                    jsonstring = reader.ReadToEnd
                    reader.Close()
                End Using

                Dim json = DeserializeJSON(jsonstring)

#Disable Warning BC42016 ' Implicit conversion
                query.Status = json("status")
#Enable Warning BC42016 ' Implicit conversion

                Dim errormessage As String = ""
                json.TryGetValue("error_message", errormessage)

                Select Case query.Status
                    Case "OK"
                        Try
                            query.Parse(json, Here)

                        Catch ex As Exception
                            query.Status = "GetLocations/Routes error: " & ex.Message
                        End Try

                    Case "MAX_WAYPOINTS_EXCEEDED"
                        query.Status = "Maximum waypoints exceeded " & errormessage

                    Case "NOT_FOUND", "ZERO_RESULTS" ' geocode was successful but returned no results
                        query.Status = NOTHINGFOUND

                    Case "OVER_QUERY_LIMIT" ' you are over your quota.
                        query.Status = "Quota Exceeded"

                    Case "REQUEST_DENIED"  ' your request was denied, generally because of lack of a sensor parameter.
                        query.Status = "Request denied (sensor missing?) " & errormessage

                    Case "INVALID_REQUEST" ' query (address or latlng) is missing
                        query.Status = "Invalid request (something is missing?) " & errormessage

                    Case "UNKNOWN_ERROR"
                        query.Status = "Unknown error (try again?) " & errormessage

                    Case Else
                        query.Status = "Unexpected status " & query.Status & " " & errormessage

                End Select

            Case DirectCast(602, HttpStatusCode) ' Google says we're asking too fast
                query.Status = "Throttling"

            Case Else
                query.Status = response.StatusDescription

        End Select

    End Sub
End Class
