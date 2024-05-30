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
Imports System.Text
Imports Utilities

Public Class ExportKML
    Inherits Exporter

    Public Sub New(ByRef excel As Object, sheetname As String, target As String, template As List(Of String))
        MyBase.New(excel, sheetname, target, template)
    End Sub
    Private Sub Add(ByRef result As StringBuilder, ByVal key As String, value As String)
        If value <> "" Then
            result.Append("<")
            result.Append(key)
            result.Append(">")
            result.Append(value)
            result.Append("</")
            result.Append(key)
            result.Append(">")
        End If
    End Sub
    Protected Overrides Sub Prologue()
    End Sub
    Protected Overrides Sub Body(final As Boolean)

        If Not HaveAllValues({"latitude", "longitude"}) Then
            Exit Sub
        End If

        Dim latitude As String = Variable("latitude").Value
        Dim longitude As String = Variable("longitude").Value
        Dim altitude As String = Variable("altitude").Value
        Dim place As New StringBuilder

        place.Append("<Placemark>")
        Add(place, "name", Variable("title").Value)
        Add(place, "description", Variable("description").Value)

        If HaveSomeValues({"icon", "color"}) Then

            place.Append("<Style>")

            If Variable("icon").Value <> "" Then
                place.Append("<IconStyle>")
                Add(place, "Icon", Variable("icon").Value)
                place.Append("</IconStyle>")
            End If

            If Variable("color").Value <> "" Then
                place.Append("<LabelStyle>")
                Add(place, "color", Colour(Variable("color").Value, "abgr"))
                place.Append("</LabelStyle>")
            End If
            place.Append("</Style>")
        End If

        place.Append("<Point><coordinates>")
        place.Append(longitude)
        place.Append(",")
        place.Append(latitude)
        If altitude <> "" Then
            place.Append(",")
            place.Append(altitude)
        End If
        place.Append("</coordinates>")
        Add(place, "altitudeMode", Variable("altitudeMode").Value)
        place.Append("</Point></Placemark>")

        If PreviousLatitude <> "" AndAlso PreviousLongitude <> "" Then

            If HaveSomeValues({"linelabel", "linedescription", "linecolor", "lineweight", "extrude", "tessellate"}) Then

                place.Append("<Placemark>")
                Add(place, "name", Variable("linelabel").Value)
                Add(place, "description", Variable("linedescription").Value)

                If HaveSomeValues({"linecolor", "lineweight"}) Then
                    place.Append("<Style><LineStyle>")
                    Add(place, "color", Colour(Variable("linecolor").Value, "abgr"))
                    Add(place, "width", Variable("lineweight").Value)
                    place.Append("</LineStyle></Style>")
                End If

                place.Append("<LineString>")
                Add(place, "extrude", Variable("extrude").Value)
                Add(place, "tessellate", Variable("tessellate").Value)
                Add(place, "altitudeMode", Variable("altitudeMode").Value)

                place.Append("<coordinates>")
                place.Append(PreviousLongitude)
                place.Append(",")
                place.Append(PreviousLatitude)
                If PreviousAltitude = "" Then
                    PreviousAltitude = "0"
                End If
                place.Append(",")
                place.Append(PreviousAltitude)
                place.Append(" ")
                place.Append(longitude)
                place.Append(",")
                place.Append(latitude)
                place.Append(",")
                place.Append(altitude)
                place.Append("</coordinates></LineString></Placemark>")
            End If
        End If
        AddRow(place.ToString)

        PreviousAltitude = altitude
        PreviousLatitude = latitude
        PreviousLongitude = longitude
    End Sub
    Protected Overrides Sub Epilogue()

        FlushRaw("body")

    End Sub
    Private Property PreviousAltitude As String = ""
    Private Property PreviousLatitude As String = ""
    Private Property PreviousLongitude As String = ""

End Class
