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
Imports Utilities

Partial Class Map
    ''' <summary>
    ''' Show a GoogleMap page with a straight line from one lat/long to another
    ''' </summary>
    ''' <param name="latitude1">Latitude of origin</param>
    ''' <param name="longitude1">Longitude of origin</param>
    ''' <param name="latitude2">Latitude of destination</param>
    ''' <param name="longitude2">Longitude of destination</param>
    ''' <remarks>If the route is west-about (E.G. New York - Tokyo) the line wraps to the map edges</remarks>
    Public Sub ShowFlight(ByVal latitude1 As Double,
                                 ByVal longitude1 As Double,
                                 ByVal latitude2 As Double,
                                 ByVal longitude2 As Double)

        CustomLog.Logger.Debug("drawLine {0} {1} {2} {3}", latitude1, longitude1, latitude2, longitude2)
        Mode = "Map"

        RunScript("drawLine", Unique(), latitude1, longitude1, latitude2, longitude2, "", "Red", 1, 2)

    End Sub
    ''' <summary>
    ''' Show a place on a GoogleMap
    ''' </summary>
    ''' <param name="location">The name or address of the place</param>
    Public Sub ShowLocation(ByVal location As String)

        Mode = "Map"
        txtFind.Text = location

        CustomLog.Logger.Debug("ShowLocation {0}", location)

        SubmitQuery(location)

    End Sub
    ''' <summary>
    ''' Show a GoogleMap page with the directions from A to B
    ''' </summary>
    ''' <param name="origin">The name or address of the origin</param>
    ''' <param name="destination">The name or address of the destination</param>
    ''' <param name="mode">The mode of transport: Transit, Driving, Bicycling or Walking</param>
    ''' <remarks></remarks>
    Public Sub showRoute(ByVal origin As String, ByVal destination As String, ByVal travelmode As String)

        Mode = "Map"

        CustomLog.Logger.Debug("showRoute {0} {1} {2}", origin, destination, travelmode)

        RunScript("showRoute", origin, destination, travelmode)

    End Sub
End Class
