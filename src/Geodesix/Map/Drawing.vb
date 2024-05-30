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
Imports System.Globalization
Imports System.Linq
Imports System.Web.Script.Serialization
Imports System.Xml
Imports System.Xml.Linq

Imports Utilities

Partial Class Map
    Sub drawCircle(ByVal id As Integer,
                  ByVal lat As Double,
                 ByVal lng As Double,
                 ByVal radius As Double,
                 ByVal content As String,
                 ByVal strokeColor As String,
                 ByVal strokeOpacity As Double,
                 ByVal strokeWeight As Integer,
                 ByVal fillColor As String,
                 ByVal fillOpacity As String)

        RunScript("drawCircle", id, lat, lng, radius, content,
                            strokeColor, strokeOpacity, strokeWeight, fillColor, fillOpacity)
    End Sub
    Sub drawDirections(ByVal id As Integer,
                       ByVal points() As Double,
                       ByVal mode As String,
                       ByVal strokeColor As String,
                       ByVal strokeOpacity As Double,
                       ByVal strokeWeight As Integer,
                       ByVal suppressInfoWindows As Boolean,
                       ByVal suppressMarkers As Boolean)

        RunScript("drawDirections", id, "[" & String.Join(",", points.Select(Function(s) s)) & "]",
                            mode, strokeColor, strokeOpacity, strokeWeight, suppressInfoWindows, suppressMarkers)
    End Sub
    Sub drawLegend(ByVal id As Integer, ByVal htmlurl As String, ByVal quadrant As String)

        '  TOP_CENTER indicates that the control should be placed along the top center of the map.
        '  TOP_LEFT indicates that the control should be placed along the top left of the map, with any sub-elements of the control "flowing" towards the top center.
        '  TOP_RIGHT indicates that the control should be placed along the top right of the map, with any sub-elements of the control "flowing" towards the top center.
        '  LEFT_TOP indicates that the control should be placed along the top left of the map, but below any TOP_LEFT elements.
        '  RIGHT_TOP indicates that the control should be placed along the top right of the map, but below any TOP_RIGHT elements.
        '  LEFT_CENTER indicates that the control should be placed along the left side of the map, centered between the TOP_LEFT and BOTTOM_LEFT positions.
        '  RIGHT_CENTER indicates that the control should be placed along the right side of the map, centered between the TOP_RIGHT and BOTTOM_RIGHT positions.
        '  LEFT_BOTTOM indicates that the control should be placed along the bottom left of the map, but above any BOTTOM_LEFT elements.
        '  RIGHT_BOTTOM indicates that the control should be placed along the bottom right of the map, but above any BOTTOM_RIGHT elements.
        '  BOTTOM_CENTER indicates that the control should be placed along the bottom center of the map.
        '  BOTTOM_LEFT indicates that the control should be placed along the bottom left of the map, with any sub-elements of the control "flowing" towards the bottom center.
        '  BOTTOM_RIGHT indicates that the control should be placed along the bottom right of the map, with any sub-elements of the control "flowing" towards the bottom center.

        Dim html As String = ""

        GetHTTP(htmlurl, html)

        RunScript("drawLegend", id, html, quadrant)
    End Sub
    Sub drawInfoWindow(ByVal id As Integer,
                       ByVal lat As Double,
                      ByVal lng As Double,
                      ByVal content As String)

        RunScript("drawInfoWindow", id, lat, lng, content)
    End Sub
    Sub DrawKML(ByVal id As Integer,
                ByVal kmlurl As String,
                ByVal text As String,
                ByVal strokeColor As String,
                ByVal strokeOpacity As Double,
                ByVal strokeWeight As Integer,
                ByVal fillColor As String,
                ByVal fillOpacity As Double)

        Dim args() As String
        Dim kml As xDocument
        Dim name As String
        Dim coords As String = Nothing
        Dim point() As String
        Dim points() As Double = Nothing
        Dim pointser As String
        Dim serialiser As New JavaScriptSerializer

        If Not kmlurl.Contains("://") Then ' Use local Geodesix\kml
            kmlurl = "file://" & GetExecutingPath() & "\kml\" & kmlurl
            If Not kmlurl.ToLower.EndsWith(".kml", StringComparison.OrdinalIgnoreCase) Then
                kmlurl = kmlurl & ".kml"
            End If
        End If

        kml = XDocument.Load(kmlurl)

        ' KML may have MultiGeometry, each with a Polygon and Coordinates
        ' Loop over all of them, drawing each polygon individually
        ' (Can't group them, finding start-finish points would be costly)

        For Each placemark As XElement In kml...<Placemark>

            name = placemark.<name>.Value

            For Each coord As XElement In placemark...<coordinates>
                coords = coord.Value

                If coords IsNot Nothing AndAlso Not IsEmpty(coords) Then

                    point = coords.Split(New Char() {}, StringSplitOptions.RemoveEmptyEntries) ' "New Char() {}" = WhiteSpace

                    If point.Length > 0 Then

                        ReDim points(point.Length * 2 - 1)

                        For i As Integer = 0 To point.Length - 1

                            Dim pair() As String = point(i).Split(","c)

                            ' 1 and 0 because polygons are long,lat,alt
                            points(i * 2) = Double.Parse(pair(1), CultureInfo.InvariantCulture)
                            points(i * 2 + 1) = Double.Parse(pair(0), CultureInfo.InvariantCulture)
                        Next

                        pointser = serialiser.Serialize(points)
                        args = {CStr(id), pointser, text, strokeColor, CStr(strokeOpacity), CStr(strokeWeight), fillColor, CStr(fillOpacity)}

                        RunScript("drawPolygon", args)
                    End If
                End If
            Next
        Next
    End Sub
    Sub drawPolygon(ByVal id As Integer,
                    ByVal points() As Double,
                    ByVal text As String,
                    ByVal strokeColor As String,
                    ByVal strokeOpacity As Double,
                    ByVal strokeWeight As Integer,
                    ByVal fillColor As String,
                    ByVal fillOpacity As Double)

        Dim pointser As String
        Dim serialiser As New JavaScriptSerializer
        pointser = serialiser.Serialize(points)
        RunScript("drawPolygon", id, pointser, text, strokeColor, strokeOpacity, strokeWeight, fillColor, fillOpacity)
    End Sub
    Sub drawPolyLine(ByVal id As Integer,
                     ByVal points() As Double,
                    ByVal text As String,
                    ByVal colour As String,
                    ByVal opacity As Double,
                    ByVal weight As Integer)
        Dim pointser As String
        Dim serialiser As New JavaScriptSerializer
        pointser = serialiser.Serialize(points)
        RunScript("drawPolygon", id, pointser, text, colour, opacity, weight)
    End Sub

End Class
