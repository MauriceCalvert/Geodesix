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
Public Class TravelQuery
    Inherits Query
    Public Sub New()
    End Sub
    Public Sub New(key As String)
        MyBase.New(key)
    End Sub

    Overrides Sub Parse(ByVal json As Object, here As Here)

        ' #23#
        Dim result As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
        Dim routes As Object = json("routes")
        _Route = GetRoute(routes, True)
        result.Add("json", routes)
        result.Add("distance", Route.Distance)
        result.Add("duration", Route.Duration)
        MyBase.Fields = result
    End Sub
    ''' <summary>
    ''' Convert a single result location from JSON format into a place
    ''' with a flat list of tags.
    ''' </summary>
    ''' <param name="r">a JSON object</param>
    ''' <returns>A Place object</returns>
    ''' <remarks></remarks>
    Private Function GetRoute(ByVal r As Object, ByVal stepdetails As Boolean) As Route

        Dim thisroute As New Route(Key)
        Try
            If IsList(r) Then ' take the first route
                r = r(0)
            End If
#Disable Warning BC42016 ' Implicit conversion
            For Each section As KeyValuePair(Of String, Object) In r
#Enable Warning BC42016 ' Implicit conversion

                Select Case section.Key
                    Case "legs"
#Disable Warning BC42016 ' Implicit conversion
                        For Each legitem As Object In section.Value
#Enable Warning BC42016 ' Implicit conversion
                            Dim thisleg As Route.Leg = thisroute.AddLeg
#Disable Warning BC42016 ' Implicit conversion
                            For Each leg As Object In legitem
#Enable Warning BC42016 ' Implicit conversion
                                Select Case leg.key
#Disable Warning BC42016 ' Implicit conversion
                                    Case "distance"
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        thisleg.Distance = leg.value("value")
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                    Case "duration"
#Enable Warning BC42016 ' Implicit conversion
                                        thisleg.Duration = CDbl(leg.value("value")) / (24 * 60 * 60)
#Disable Warning BC42016 ' Implicit conversion
                                    Case "end_address"
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        thisleg.EndAddress = leg.value
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                    Case "end_location"
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        thisleg.EndLocation = New Location(leg.value("lat"), leg.value("lng"))
#Enable Warning BC42016 ' Implicit conversion
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                    Case "start_location"
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        thisleg.StartLocation = New Location(leg.value("lat"), leg.value("lng"))
#Enable Warning BC42016 ' Implicit conversion
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                    Case "start_address"
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                        thisleg.StartAddress = leg.value
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
                                    Case "steps"
#Enable Warning BC42016 ' Implicit conversion
                                        If stepdetails Then
                                            ' needs error checking, many are missing
                                            'For Each stepps As Object In leg.value
                                            '    Dim thisstep As Route.Leg.Stepp
                                            '    thisstep = thisleg.AddStep
                                            '    thisstep.Mode = stepps("travel_mode")
                                            '    thisstep.Start = New Location(stepps("start_location")("lat"), stepps("start_location")("lng"))
                                            '    thisstep.Finish = New Location(stepps("end_location")("lat"), stepps("end_location")("lng"))
                                            '    thisstep.Duration = TimeSpan.FromSeconds(stepps("duration")("value"))
                                            '    thisstep.Distance = stepps("distance")("value")
                                            '    thisstep.Polyline.Levels = stepps("polyline")("levels")
                                            '    thisstep.Polyline.Points = stepps("polyline")("points")
                                            '    thisstep.HTML_Instructions = stepps("html_instructions")
                                            'Next
                                        End If
                                End Select
                            Next
                        Next
                    Case "summary"
                    Case "copyrights"
                    Case "overview_polyline"
                    Case "warnings"
                    Case "waypoint_order"
                End Select
            Next

        Catch ex As Exception
            CustomLog.Logger.Error(ex, "Failed to parse route {0}", r)

        End Try
        Return thisroute
    End Function
    Public Overrides Property Key As String
        Get
            Return MyBase.Key
        End Get
        Set(value As String)
            MyBase.Key = value
            Dim keys As String() = Key.Split("|"c)
            _Origin = keys(0)
            _Destination = keys(1)
            _Mode = keys(2)
        End Set
    End Property
    Public ReadOnly Property Origin As String
    Public ReadOnly Property Destination As String
    Public ReadOnly Property Mode As String
    ''' <summary>
    ''' The routes found between start and finish locations
    ''' </summary>
    ''' <remarks>Only applies to "Directions" queries</remarks>
    Public ReadOnly Property Route As Route
    Friend Overrides Property Fields As Dictionary(Of String, Object)
        Get
            If MyBase.Fields Is Nothing Then
                MyBase.Fields = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
            End If
            Return MyBase.Fields
        End Get
        Set(value As Dictionary(Of String, Object))
            If Not value.Any Then
                Exit Property
            End If
            MyBase.Fields = value
        End Set
    End Property
End Class