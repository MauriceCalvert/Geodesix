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
Public Module Haversine_
    ''' <summary>
    ''' Compute the Haversine distance between tow points on the Earth
    ''' </summary>
    ''' <param name="latitude1">Origin latitude, in degrees</param>
    ''' <param name="longitude1">Origin longitude, in degrees</param>
    ''' <param name="latitude2">Destination latitude, in degrees</param>
    ''' <param name="longitude2">Destination longitude, in degrees</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Haversine(ByVal latitude1 As Double,
                              ByVal longitude1 As Double,
                              ByVal latitude2 As Double,
                              ByVal longitude2 As Double) As Double
        Const r As Double = 6367450.0R ' gemetric mean radius of the earth in metres
        Return Math.Acos(Math.Sin(latitude1 * Math.PI / 180.0R) * Math.Sin(latitude2 * Math.PI / 180.0R) +
                          Math.Cos(latitude1 * Math.PI / 180.0R) * Math.Cos(latitude2 * Math.PI / 180.0R) *
                          Math.Cos((longitude2 - longitude1) * Math.PI / 180.0R)) * r

    End Function
End Module
