
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
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
' 
' You should have received a copy of the GNU General Public License
' along with this program. If not, see <http://www.gnu.org/licenses/>.
' --------------------------------------------------------------------
Imports System.Collections.Generic
Imports Utilities
Imports System.Linq

Public Class DrawingFunctions
     Inherits Methods

     Private Class Range
         ' Just to have a type name 'Range'
     End Class
     Private ReadOnly Property Excel As Object
     Public Sub New(excel As Object)

         Me.Excel = excel
         Functions = {
New Method(excel, "Circle", GetType(Integer), "Draw a circle on the map", Nothing, "=Draw(""Circle"", 1, 48.868916, 2.37793, 100000, ""Paris"", ""#ff0000"", 1, 1, ""LightBlue"", 0.3)", {
New Parameter(excel, "id", GetType(Integer), "Identifier", Unique(), Nothing),
New Parameter(excel, "latitude", GetType(Double), "Latitude of centre", Nothing, {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "Longitude of centre", Nothing, {-180,180}),
New Parameter(excel, "radius", GetType(Double), "Radius in meters", Nothing, {1}),
New Parameter(excel, "title", GetType(String), "Hover label", Nothing, Nothing),
New Parameter(excel, "strokeColor", GetType(String), "Line colour", "#000000", Nothing),
New Parameter(excel, "strokeOpacity", GetType(Double), "Line opacity", 1, {0,1}),
New Parameter(excel, "strokeWeight", GetType(Double), "Line weight", 1, {0,10}),
New Parameter(excel, "fillColor", GetType(String), "Fill colour", "#000000", Nothing),
New Parameter(excel, "fillOpacity", GetType(Double), "Fill opacity", 0, {0,1})
}),
New Method(excel, "Line", GetType(Integer), "Draw a line on the map", "Unique identifier of the line", "=Draw(""Line"", 12, 45.825284, 1.273252, 46.19155, 6.129209, ""Dijon-Geneva"", ""Green"", 1, 2)", {
New Parameter(excel, "id", GetType(Integer), "Identifier", Unique(), Nothing),
New Parameter(excel, "origlatitude", GetType(Double), "Latitude of origin", Nothing, {-90,90}),
New Parameter(excel, "origlongitude", GetType(Double), "Longitude of origin", Nothing, {-180,180}),
New Parameter(excel, "destlatitude", GetType(Double), "Latitude of destination", Nothing, {-90,90}),
New Parameter(excel, "destlongitude", GetType(Double), "Longitude of destination", Nothing, {-180,180}),
New Parameter(excel, "title", GetType(String), "Hover label", Nothing, Nothing),
New Parameter(excel, "strokeColor", GetType(String), "Line colour", "#000000", Nothing),
New Parameter(excel, "strokeOpacity", GetType(Double), "Line opacity", 1, {0,1}),
New Parameter(excel, "strokeWeight", GetType(Double), "Line weight", 1, {0,10})
}),
New Method(excel, "Marker", GetType(Integer), "Draw a marker on the map", "Unique identifier of the marker", "=Draw(""Marker"", 16, 47.313063, 5.041563, ""http://maps.google.com/mapfiles/kml/pal3/icon56.png"", ""Dijon Castle"", 24)", {
New Parameter(excel, "id", GetType(Integer), "Identifier", Unique(), Nothing),
New Parameter(excel, "latitude", GetType(Double), "Latitude of centre", Nothing, {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "Longitude of centre", Nothing, {-180,180}),
New Parameter(excel, "icon", GetType(String), "Icon", Nothing, Nothing),
New Parameter(excel, "title", GetType(String), "Hover label", Nothing, Nothing),
New Parameter(excel, "size", GetType(Integer), "Icon size in pixels", 24, Nothing)
})
         }.ToList
     End Sub
     Public Overrides Function Generate(method As Method) As String

         Dim argf As String = String.Join(", ", method.Parameters.Select(Function(q) q.Formula))

         Dim fun As String = "Draw(" & Quoted(method.Name) & "," & argf & ")"

         Return fun

     End Function

End Class

