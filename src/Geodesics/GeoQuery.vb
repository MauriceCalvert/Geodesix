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
Imports System.Linq
Imports Utilities
''' <summary>
''' A geocoding request and its results
''' </summary>
''' <remarks></remarks>
Public Class GeoQuery
    Inherits Query

    Public Sub New()
    End Sub
    Public Sub New(key As String)
        MyBase.New(key)
    End Sub
    Private Function Generalisation(place1 As Place, place2 As Place) As Boolean
        ' Google often returns more than one description of a place, where the 2nd and subsequent ones are just generalisations
        ' of the first one. For example, a request for "Maloja 7516, Stampa, Switzerland" will return the formatted addresses
        ' 1. "Maloja 7516, Stampa, Switzerland"
        ' 2. "7516, Switzerland"
        ' Ignore these generalisations
        Dim address1 As Object = Nothing
        Dim address2 As Object = Nothing
        If Not place1.Fields.TryGetValue("formatted_address", address1) Then
            Return False
        End If
        If Not place2.Fields.TryGetValue("formatted_address", address2) Then
            Return False
        End If
        ' Determine if address1 is a generalisation of address2
        ' It is if 
        '   Address1 has no more words than address2
        '   Every word in address1 is in address2   
        Dim seps() As Char = {" "c, ","c, ";"c, CChar(LISTSEP)}
        Dim word1() As String = address1.ToString.Split(seps, StringSplitOptions.RemoveEmptyEntries)
        Dim word2() As String = address2.ToString.Split(seps, StringSplitOptions.RemoveEmptyEntries)
        If word1.Length <= word2.Length Then
            For Each word As String In word1
                If Not word2.HasAny(word) Then
                    Return False
                End If
            Next
        End If
        CustomLog.Logger.Debug("{0} is a generalisation of {2}", address1, address2)
        Return True
    End Function
    ''' <summary>
    ''' Convert a single result location from JSON format into a place
    ''' with a flat list of tags.
    ''' </summary>
    ''' <param name="r">a JSON object</param>
    ''' <returns>A Place object</returns>
    ''' <remarks></remarks>
    Private Function GetLocation(ByVal r As Object) As Place

        Dim place As New Place(Key)

#Disable Warning BC42016 ' Implicit conversion
        For Each section As KeyValuePair(Of String, Object) In r
#Enable Warning BC42016 ' Implicit conversion

            Select Case section.Key
                Case "types"
                    Try
                        place.Tag(section.Key) = GetTypes(section.Value)
                    Catch ex As Exception
                        place.Tag(section.Key) = ex.Message
                    End Try


                Case "formatted_address"
                    Try
                        place.Tag(section.Key) = section.Value
                    Catch ex As Exception
                        place.Tag(section.Key) = ex.Message
                    End Try

                Case "address_components"
                    Dim key As String = ""
                    Dim name As String = ""
#Disable Warning BC42016 ' Implicit conversion
                    For Each ac As Object In section.Value
#Enable Warning BC42016 ' Implicit conversion
                        Try
#Disable Warning BC42016 ' Implicit conversion
                            If ac.ContainsKey("types") Then
#Enable Warning BC42016 ' Implicit conversion
                                key = GetTypes(ac("types"))
                            End If
#Disable Warning BC42016 ' Implicit conversion
                            If ac.ContainsKey("long_name") Then
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                name = ac("long_name")
#Enable Warning BC42016 ' Implicit conversion
                            End If
                            place.Tag(key) = name
                        Catch ex As Exception
                            If key <> "" Then
                                place.Tag(key) = ex.Message
                            End If
                        End Try
                    Next

                Case "geometry"
#Disable Warning BC42016 ' Implicit conversion
                    For Each gs As KeyValuePair(Of String, Object) In section.Value
#Enable Warning BC42016 ' Implicit conversion
                        Try
                            Select Case gs.Key

                                Case "bounds"
                                    Try
                                        Dim nelat As Double, nelng As Double
#Disable Warning BC42016 ' Implicit conversion
                                        nelat = gs.Value("northeast")("lat")
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        nelng = gs.Value("northeast")("lng")
#Enable Warning BC42016 ' Implicit conversion
                                        place.Tag("boundsne") = nelat.ToString & LISTSEP & nelng.ToString
                                    Catch ex As Exception
                                        place.Tag("boundsne") = ex.Message
                                    End Try
                                    Try
                                        Dim swlat As Double, swlng As Double
#Disable Warning BC42016 ' Implicit conversion
                                        swlat = gs.Value("southwest")("lat")
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        swlng = gs.Value("southwest")("lng")
#Enable Warning BC42016 ' Implicit conversion
                                        place.Tag("boundssw") = swlat.ToString & LISTSEP & swlng.ToString
                                    Catch ex As Exception
                                        place.Tag("boundssw") = ex.Message
                                    End Try

                                Case "location"
                                    Try
                                        Dim lat As Double, lng As Double
#Disable Warning BC42016 ' Implicit conversion
                                        lat = gs.Value("lat")
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        lng = gs.Value("lng")
#Enable Warning BC42016 ' Implicit conversion
                                        place.Tag("latitude") = lat
                                        place.Tag("longitude") = lng
                                    Catch ex As Exception
                                        place.Tag("latitude") = ex.Message
                                    End Try

                                Case "location_type"
                                    Try
                                        place.Tag("location_type") = gs.Value.ToString
                                    Catch ex As Exception
                                        place.Tag("location_type") = ex.Message
                                    End Try

                                Case "viewport"
                                    Try
                                        Dim nelat As Double, nelng As Double
#Disable Warning BC42016 ' Implicit conversion
                                        nelat = gs.Value("northeast")("lat")
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        nelng = gs.Value("northeast")("lng")
#Enable Warning BC42016 ' Implicit conversion
                                        place.Tag("viewpointne") = nelat.ToString & LISTSEP & nelng.ToString
                                    Catch ex As Exception
                                        place.Tag("viewpointne") = ex.Message
                                    End Try
                                    Try
                                        Dim swlat As Double, swlng As Double
#Disable Warning BC42016 ' Implicit conversion
                                        swlat = gs.Value("southwest")("lat")
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        swlng = gs.Value("southwest")("lng")
#Enable Warning BC42016 ' Implicit conversion
                                        place.Tag("viewpointsw") = swlat.ToString & LISTSEP & swlng.ToString
                                    Catch ex As Exception
                                        place.Tag("viewpointsw") = ex.Message
                                    End Try

                                Case Else
#If DEBUG Then
                                    Stop
#End If

                            End Select

                        Catch  ' Geometry not a dictionary, ignore 
#If DEBUG Then
                            Stop
#End If
                        End Try
                    Next

                Case "partial_match"
                    Try
                        place.Tag("partial_match") = section.Value
                    Catch ex As Exception
                        place.Tag("partial_match") = ex.Message
                    End Try

                Case "place_id"
                    Try
                        place.Tag("place_id") = section.Value
                    Catch ex As Exception
                        place.Tag("place_id") = ex.Message
                    End Try

                Case "plus_code"
                    Try
                        place.Tag("plus_code") = section.Value("global_code")
                    Catch ex As Exception
                        place.Tag("plus_code") = ex.Message
                    End Try

                Case Else
                    Try
                        If GetType(IEnumerable(Of Object)).IsAssignableFrom(section.Value.GetType) Then
                            place.Tag(section.Key) = String.Join(" ",
                                CType(section.Value, IEnumerable(Of Object)).
                                Select(Function(q) q.ToString))
                        Else
                            place.Tag(section.Key) = section.Value
                        End If
                    Catch ex As Exception
                        place.Tag("section.Key") = ex.Message
                    End Try

            End Select
        Next
        Return place
    End Function
    ''' <summary>
    ''' Convert a JSON array of strings into a blank-delimited list
    ''' </summary>
    ''' <param name="types">a JSON array</param>
    ''' <returns>String, flsattened array</returns>
    ''' <remarks></remarks>
    Private Function GetTypes(ByVal types As Object) As String
        Dim result As String = ""
#Disable Warning BC42016 ' Implicit conversion
        For Each t As String In types
#Enable Warning BC42016 ' Implicit conversion
            result = result & " " & t
        Next
        If result.Length > 0 Then
            result = result.Substring(1) ' remove leading " "
        End If
        Return result
    End Function
    Overrides Sub Parse(json As Object, here As Here)

        ' #22#
        Dim places As New List(Of Place)
#Disable Warning BC42016 ' Implicit conversion
        For Each r As Object In json("results")
#Enable Warning BC42016 ' Implicit conversion
            Dim p As Place = GetLocation(r)
            If p.Fields.Any Then
                places.Add(p)
            End If
        Next

        ' Remove addresses that are generalisations of others
        If places.Count > 1 Then
            Dim i As Integer = 0
            Do While i < places.Count - 1
                Dim a As Place = places(i)
                Dim b As Place = places(i + 1)
                If Generalisation(a, b) Then
                    places.RemoveAt(i)
                    CustomLog.Logger.Debug("{0}", a)
                    CustomLog.Logger.Debug("is a generalisation of")
                    CustomLog.Logger.Debug("{0}", b)
                ElseIf Generalisation(b, a) Then
                    places.RemoveAt(i + 1)
                    CustomLog.Logger.Debug("{0}", b)
                    CustomLog.Logger.Debug("is a generalisation of")
                    CustomLog.Logger.Debug("{0}", a)
                Else
                    i += 1
                End If
            Loop
        End If

        ' Find the closest match to 'here'
        Dim closest As Place = places.First
        If places.Count > 1 Then
            Dim closer As Double = Haversine(here.Location.Latitude, here.Location.Longitude, closest.Location.Latitude, closest.Location.Longitude)
            For j As Integer = 1 To places.Count - 1
                Dim nearer As Place = places(j)
                Dim d As Double = Haversine(here.Location.Latitude, here.Location.Longitude, nearer.Location.Latitude, nearer.Location.Longitude)
                If d < closer Then
                    CustomLog.Logger.Debug("{0}", closest)
                    CustomLog.Logger.Debug("is closer {0:#,##0}<{1:#,##0} than", d, closer)
                    CustomLog.Logger.Debug("{0}", nearer)
                    closest = nearer
                    closer = d
                End If
            Next
        End If
        Fields = closest.Fields
    End Sub
End Class
