
Imports System.Collections.Generic
Imports Utilities
Imports System.Linq

Public Class ExcelFunctions
 Inherits Methods

 Private ReadOnly Property DoubleType As Type = GetType(Double)
 Private ReadOnly Property IntegerType As Type = GetType(Integer)
 Private Class Range
 ' Just to have a type name 'Range'
 End Class
 Private ReadOnly Property RangeType As Type = GetType(Range)
 Private ReadOnly Property StringType As Type = GetType(String)
 Private ReadOnly Property Excel As Object
 Public Sub New(excel As Object)

 Me.Excel = excel
 Functions = {
New Method(excel, "Area", GetType(Double), "Get the area of a polygon", "Square metres", {
New Parameter(excel, "points", GetType(Range), "A N*2 array of Latitudes and Longitudes", "", Nothing)
}),
New Method(excel, "Azimuth", GetType(Double), "Get the bearing from one place to another", "Bearing or a 2-cell array initial and final bearing", {
New Parameter(excel, "originlat", GetType(Double), "Latitude of origin", "", {-90,90}),
New Parameter(excel, "originlong", GetType(Double), "Longitude of origin", "", {-180,180}),
New Parameter(excel, "destlat", GetType(Double), "Latitude of destination", "", {-90,90}),
New Parameter(excel, "destllong", GetType(Double), "Longitude of destination", "", {-180,180})
}),
New Method(excel, "Colour", GetType(String), "Convert a colour to hexadecimal", "Hexadecimal colour", {
New Parameter(excel, "colour", GetType(String), "A colour name or 6-character hex colour code", "", Nothing),
New Parameter(excel, "format", GetType(String), "A combination of letters #ARGB", "#RGB", Nothing)
}),
New Method(excel, "Displace", GetType(Double), "Find a point that is offset by a bearing and a distance", "LatLon or a 2-cell array lat lon", {
New Parameter(excel, "latitude", GetType(Double), "Latitude of origin", "", {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "Longitude of origin", "", {-180,180}),
New Parameter(excel, "bearing", GetType(Double), "Bearing of offset", "", {-360,360}),
New Parameter(excel, "distance", GetType(Double), "Distance to offset", "", {0})
}),
New Method(excel, "Distance", GetType(Double), "Get the straight-line distance between 2 points", "Distance in meters", {
New Parameter(excel, "originlat", GetType(Double), "Latitude of origin", "", {-90,90}),
New Parameter(excel, "originlong", GetType(Double), "Longitude of origin", "", {-180,180}),
New Parameter(excel, "destlat", GetType(Double), "Latitude of destination", "", {-90,90}),
New Parameter(excel, "destlLong", GetType(Double), "Longitude of destination", "", {-180,180})
}),
New Method(excel, "DMS", GetType(Double), "Convert Degrees-Minutes-Seconds to degrees", "Decimal degrees", {
New Parameter(excel, "dms", GetType(String), "A Latitude/Longitude in Degrees° Minutes"" Seconds'", "", Nothing)
}),
New Method(excel, "Geocode", GetType(String), "Get information about a place", "Field value", {
New Parameter(excel, "request", GetType(String), "Field name of information", "", Nothing),
New Parameter(excel, "location", GetType(String), "Place to find", "", Nothing)
}),
New Method(excel, "Heat", GetType(String), "Get a colour from a range of values to make a HeatMap", "Hexadecimal colour", {
New Parameter(excel, "value", GetType(Double), "A value in the range minimum .. maximum", "", Nothing),
New Parameter(excel, "minimum", GetType(Double), "Lowest value of range", "", Nothing),
New Parameter(excel, "maximum", GetType(Double), "Highest value of range", "", Nothing)
}),
New Method(excel, "PlusCodeFrom", GetType(String), "Convert lat/long to Google PlusCodes", "PlusCode", {
New Parameter(excel, "latitude", GetType(Double), "A Latitude to convert to a PlusCode", "", {-90,90}),
New Parameter(excel, "longitude", GetType(Double), "A Longitude to convert to a PlusCode", "", {-180,180})
}),
New Method(excel, "PlusCodeTo", GetType(Double), "Convert a Google PlusCode to lat/long", "Lat or Long", {
New Parameter(excel, "pluscode", GetType(String), "A PlusCode to convert to Latitude or Longitude", "", Nothing),
New Parameter(excel, "convertto", GetType(String), "What to convert PlusCode to: 'Latitude'", "", {"Latitude","Longitude"})
}),
New Method(excel, "Regex", GetType(String), "Parse a string with a regular expression", "Parsed value", {
New Parameter(excel, "string", GetType(String), "A string to parse", "", Nothing),
New Parameter(excel, "pattern", GetType(String), "A regular expression", "", Nothing),
New Parameter(excel, "group", GetType(Integer), "Index of group to retrieve: 0 ..N", "", Nothing),
New Parameter(excel, "item", GetType(Integer), "Index of item in group: 0 ..N", "", Nothing)
}),
New Method(excel, "Script", GetType(String), "Execute a Javascript function on the browser in the Geodesix pane", "", {
New Parameter(excel, "function", GetType(String), "Any Javascript function", "", Nothing)
}),
New Method(excel, "Travel", GetType(Double), "Get travelling distances and times", "Distance in metres or Time in days", {
New Parameter(excel, "type", GetType(String), "Distance or Duration", "", {"Distance","Duration"}),
New Parameter(excel, "origin", GetType(String), "Origin, name of a place", "", Nothing),
New Parameter(excel, "destination", GetType(String), "Destination, name of a place", "", Nothing),
New Parameter(excel, "mode", GetType(String), "Mode of travel", "", { "Transit","Driving","Walking","Bicycling"})
})
 }.ToList
 End Sub

 Public Overrides Function Generate(method As Method) As String

 Dim argf As String = String.Join(", ", method.Parameters.Select(Function(q) q.Formula))

 Dim fun As String = $"{method.Name}({argf})"

 Return fun

 End Function

End Class

