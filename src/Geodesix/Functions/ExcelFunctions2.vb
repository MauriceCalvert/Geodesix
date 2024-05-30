
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

Public Class ExcelFunctions
     Inherits Methods

     Private Class Range
         ' Just to have a type name 'Range'
     End Class
     Private ReadOnly Property Excel As Object
     Public Sub New(excel As Object)

         Me.Excel = excel
         Functions = {
New Method(excel, "Area", GetType(Double), "Get the area of a polygon", "Square metres", "=Area({{48.8577953, 2.2919178}, {48.8601066, 2.2951631}, {48.8538506, 2.3047225}, {48.8516625, 2.3014853}}) = 339422.50", {
New Parameter(excel, "points", GetType(Range), "A N*2 array of Latitudes and Longitudes", Nothing, Nothing)
}),
New Method(excel, "Azimuth", GetType(Double), "Get the bearing from one place to another", "Bearing or a 2-cell array initial and final bearing", "=Azimuth(48.856614, 2.3522219, 43.296482, 5.36978) = 153.208", {
New Parameter(excel, "originlat", GetType(Double), "Latitude of origin", Nothing, {-90,90}),
New Parameter(excel, "originlong", GetType(Double), "Longitude of origin", Nothing, {-180,180}),
New Parameter(excel, "destlat", GetType(Double), "Latitude of destination", Nothing, {-90,90}),
New Parameter(excel, "destllong", GetType(Double), "Longitude of destination", Nothing, {-180,180})
}),
New Method(excel, "Color", GetType(String), "Convert a Color to hexadecimal", "Hexadecimal Color", "=Color(""LightSteelBlue"", ""rgb"") = ""B0C4DE""", {
New Parameter(excel, "Color", GetType(String), "A Color name or 6-character hex Color code", Nothing, Nothing),
New Parameter(excel, "format", GetType(String), "A combination of letters #ARGB", "#RGB", Nothing)
}),
New Method(excel, "Displace", GetType(Double), "Find a point that is offset by a bearing and a distance", "LatLon or a 2-cell array lat lon", "=Displace(48.856614, 2.3522219, 158.208779697697, 660495.456719927/2) = 46.087160,3.9368456", {
New Parameter(excel, "latitude", GetType(Double), "Latitude of origin", Nothing, {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "Longitude of origin", Nothing, {-180,180}),
New Parameter(excel, "bearing", GetType(Double), "Bearing of offset", Nothing, {-360,360}),
New Parameter(excel, "distance", GetType(Double), "Distance to offset", Nothing, {0})
}),
New Method(excel, "Distance", GetType(Double), "Get the straight-line distance between 2 points. Calculated using Vincenty's formulae, which has sub-millimeter precision", "Distance in meters", "=Distance(51.508039, -0.128069, 51.5098597, -0.1342809) = 476.476", {
New Parameter(excel, "originlat", GetType(Double), "Latitude of origin", Nothing, {-90,90}),
New Parameter(excel, "originlong", GetType(Double), "Longitude of origin", Nothing, {-180,180}),
New Parameter(excel, "destlat", GetType(Double), "Latitude of destination", Nothing, {-90,90}),
New Parameter(excel, "destlLong", GetType(Double), "Longitude of destination", Nothing, {-180,180})
}),
New Method(excel, "DMS", GetType(Double), "Convert Degrees-Minutes-Seconds to degrees", "Decimal degrees", "=DMS(""136° 58"""" 19'"") = 136.97194", {
New Parameter(excel, "dms", GetType(String), "A Latitude/Longitude in Degrees° Minutes"""" Seconds'. Note the symbols ° """" '", Nothing, Nothing)
}),
New Method(excel, "Draw", GetType(String), "Draw a circle / line / marker on the map", "The name of the drawn geometry", Nothing, {
},SubMethods:=New DrawingFunctions(Excel)),
New Method(excel, "Formulae", GetType(String), "Gets a cell's formula with references replaced by their values", "Formula", "=Formulae($F$29) = Geocode(""postal_code"", ""Trafalgar square"")", {
New Parameter(excel, "cell", GetType(String), "The cell for which the formula is desited", Nothing, Nothing)
}),
New Method(excel, "Geocode", GetType(String), "Get Geodesic information", Nothing, "=Geocode(""postal_code"", ""Trafalgar square"") = ""WC2N 5DS""", {
New Parameter(excel, "request", GetType(String), "The type of information to return", Nothing, {"GeoFields()"}),
New Parameter(excel, "place", GetType(String), "The place for which the information is to be obtained", Nothing, Nothing)
}),
New Method(excel, "Geodesix", Nothing, "Functions to manipulate parameters and settings", Nothing, Nothing, {
},SubMethods:=New GeodesixFunctions(Excel)),
New Method(excel, "GeoReverse", GetType(String), "Perform a reverse geocode", Nothing, "=GeoReverse(""formatted_address"", 51.508039, -0.128069) = ""Greater London, UK""", {
New Parameter(excel, "request", GetType(String), "The type of information to return", Nothing, {"GeoFields()"}),
New Parameter(excel, "latitude", GetType(Double), "Latitude", Nothing, {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "Longitude", Nothing, {-180,180})
}),
New Method(excel, "Heat", GetType(String), "Get a Color from a range of values to make a Heat-map. The colours returned are adjusted so as to appear linear to human perception", "Hexadecimal Color", "=Heat(5, 1, 10) = ""99F0000""", {
New Parameter(excel, "value", GetType(Double), "A value in the range minimum .. maximum", Nothing, Nothing),
New Parameter(excel, "minimum", GetType(Double), "Lowest value of range", Nothing, Nothing),
New Parameter(excel, "maximum", GetType(Double), "Highest value of range", Nothing, Nothing)
}),
New Method(excel, "JavaScript", GetType(String), "Execute a Javascript function on the browser in the Geodesix pane", Nothing, "=JavaScript(""alert('Hello world!'"")", {
New Parameter(excel, "function", GetType(String), "Any Javascript function", Nothing, Nothing)
}),
New Method(excel, "PlusCode", GetType(String), "Convert lat/long to/from Google PlusCodes", "PlusCode", "=PlusCode(""9C3XGV5C+6Q"", ""latitude"") = 51.5080625", {
New Parameter(excel, "latitude", GetType(Double), "A Latitude to convert to a PlusCode", Nothing, {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "A Longitude to convert to a PlusCode", Nothing, {-180,180}),
New Parameter(excel, "length", GetType(Integer), "The desired length of the PlusCode", Nothing, Nothing)
}),
New Method(excel, "Regex", GetType(String), "Parse a string with a regular expression", "Parsed value", "=Regex(""22.7,33.2 44.7"", ""([+-]?[0-9]*(\.[0-9]*))"", 1, 0) = ""33.2""", {
New Parameter(excel, "string", GetType(String), "A string to parse", Nothing, Nothing),
New Parameter(excel, "pattern", GetType(String), "A regular expression", Nothing, Nothing),
New Parameter(excel, "group", GetType(Integer), "Index of group to retrieve: 0 ..N", Nothing, Nothing),
New Parameter(excel, "item", GetType(Integer), "Index of item in group: 0 ..N", Nothing, Nothing)
}),
New Method(excel, "TimeOffset", GetType(Double), "Get the UTC time offset at a Latitude and Longitude", "The time offset in days, an Excel Time value, to be added to a Date. Note: When formatted as a time, Excel doesn't negative values (west of Greenwich) correctly", "=TimeOffset(-35.2757878, 149.130732) = 10:00:00", {
New Parameter(excel, "latitude", GetType(Double), "Latitude", Nothing, {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "Longitude", Nothing, {-180,180})
}),
New Method(excel, "TimeZone", GetType(String), "Get the time zone at a Latitude and Longitude", "The name of the time zone", "=TimeZone(-35.2757878, 149.130732) = ""Australia/Sydney""", {
New Parameter(excel, "latitude", GetType(Double), "Latitude", Nothing, {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "Longitude", Nothing, {-180,180})
}),
New Method(excel, "Travel", GetType(Double), "Get travelling distances and times", "Distance in metres or Time in days", "=Travel(""Duration"", ""Trafalgar Square"", ""Picadilly Circus"", ""Bicycling"") = 00:04:53", {
New Parameter(excel, "type", GetType(String), "Distance or Duration", Nothing, {"Distance","Duration"}),
New Parameter(excel, "origin", GetType(String), "Origin, name of a place", Nothing, Nothing),
New Parameter(excel, "destination", GetType(String), "Destination, name of a place", Nothing, Nothing),
New Parameter(excel, "mode", GetType(String), "Mode of travel", Nothing, { "Transit","Driving","Walking","Bicycling"})
})
         }.ToList
     End Sub

     Public Overrides Function Generate(method As Method) As String

         Dim argf As String = String.Join(", ", method.Parameters.Select(Function(q) q.Formula))

         Dim fun As String = $"{method.Name}({argf})"

         Return fun

     End Function

End Class


