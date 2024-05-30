
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
Imports Seting

Public Class GeodesixFunctions
     Inherits Methods

     Private Class Range
         ' Just to have a type name 'Range'
     End Class
     Private ReadOnly Property Excel As Object
     Public Sub New(excel As Object)

         Me.Excel = excel
         Functions = {
New Method(excel, "clicked", GetType(String), "Get the location clicked on the map", "Lat,Lon", "'=Geodesix(""clicked"") = ""40.4131629,-4.0273437""", {
}),
New Method(excel, "default", Nothing, "Show the default map", Nothing, "'=Geodesix(""default"")", {
}),
New Method(excel, "language", GetType(String), "Get/Set the current GoogleMap language", "2-Letter language code", "'=Geodesix(""language"", """") = ""EN""", {
New Parameter(excel, "language", GetType(String), "See https://developers.google.com/admin-sdk/directory/v1/languages", Nothing, Nothing)
}),
New Method(excel, "mode", GetType(String), "Type of page being displayed in the Map Pane", "Map, Browser or Hidden", "'=Geodesix(""mode"", """") = ""Map""", {
New Parameter(excel, "mode", GetType(String), "Blank=Just get the mode. Display the map, the browser, or hide the pane", Nothing, Nothing)
}),
New Method(excel, "navigate", Nothing, "Display a webpage in the Geodesix pane", "URL", "'=Geodesix(""navigate"", ""http://www.google.com"") = ""www.google.com""", {
New Parameter(excel, "URL", GetType(String), "URL of webpage to display", Nothing, Nothing)
}),
New Method(excel, "overlay", GetType(Boolean), "Get/Set the visibility of a previously-drawn overlay", "True or False", "'=Geodesix(""overlay"", ""12"") = True", {
New Parameter(excel, "ID", GetType(Integer), "ID number of overlay", Nothing, {0}),
New Parameter(excel, "show", GetType(Boolean), "Blank=Just get visibility. True=Show it. False=Hide it", Nothing, {"","True","False"})
}),
New Method(excel, "position", GetType(String), "Get/Set the docking position of the map pane", "Left, Right, Center, Bottom", "=Geodesix(""position"") = ""Right""", {
New Parameter(excel, "quadrant", GetType(String), "Quadrant in which to display. Blank=just get quadrant", Nothing, {"Centre","Top","Bottom","Left","Right"})
}),
New Method(excel, "setting", GetType(String), "Get/Set the value of a setting", "Current setting", "'=Geodesix(""preference"", ""StartLat"") = 42", {
New Parameter(excel, "name", GetType(String), "Name of preference", Nothing, Settings.Names),
New Parameter(excel, "value", GetType(String), "New value to set. Blank=just get current value", Nothing, Nothing)
}),
New Method(excel, "programfolder", GetType(String), "Get the path of the Geodesix installation", "Path", "'=Geodesix(""programfolder"") = ""C:\Program Files (x86)\Geodesix\Geodesix""", {
}),
New Method(excel, "regextimeout", GetType(Integer), "Get/Set the regular expression timeout", "milliseconds", "'=Geodesix(""regextimeout"") = 15", {
New Parameter(excel, "timeout", GetType(Integer), "Timeout in milliseconds. Blank=just get value", Nothing, {10,5000})
}),
New Method(excel, "region", GetType(String), "Get/Set the region bias for searches", "ISO-3166 code", "'=Geodesix(""region"") = ""us""", {
New Parameter(excel, "region", GetType(Integer), "ISO-3166-1 region code. Blank=just get current value", Nothing, Nothing)
}),
New Method(excel, "showlocation", Nothing, "Show a place on the map", Nothing, "'=Geodesix(""showlocation"", ""Helsinki"")", {
New Parameter(excel, "place", GetType(String), "Name of a place or a lat,lon", Nothing, Nothing)
}),
New Method(excel, "showsheet", Nothing, "Display the map for a worksheet", Nothing, "'=Geodesix(""showsheet"", ""Home"")", {
New Parameter(excel, "sheet", GetType(String), "Worksheet name", Nothing, Nothing)
}),
New Method(excel, "url", GetType(String), "Get the URL currently displayed", "Path or URL", "'=Geodesix(""url"") = ""C:\Users\YourName\AppData\Local\Geodesix\geocoder.htm""", {
}),
New Method(excel, "zoomToContent", GetType(String), "Zoom the map to fit the current overlays", Nothing, "'=Geodesix(""ZoomToContent"")", {
})
         }.ToList
     End Sub
     Public Overrides Function Generate(method As Method) As String

         Dim argf As String = String.Join(", ", method.Parameters.Select(Function(q) q.Formula))

         Dim fun As String = $"Geodesix(""{method.Name}"",{argf})"

         Return fun

     End Function

End Class


