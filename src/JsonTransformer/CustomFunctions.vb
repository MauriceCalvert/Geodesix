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
Imports GeoTimeZone
Imports TimeZoneConverter

Public Module CustomFunctions
    Public Function Azimuth(latitude1 As Double, longitude1 As Double, latitude2 As Double, longitude2 As Double) As Double

        Dim result As VincentyInverseResult = VincentyInverse(latitude1, longitude1, latitude2, longitude2)

        Return toDegrees(result.InitialBearing)

    End Function
    Public Function Displace(latitude As Double, longitude As Double, bearing As Double, distance As Double) As String

        Dim result As VincentyDirectResult = VincentyDirect(latitude, longitude, bearing, distance)

        Return $"{result.Latitude},{result.Longitude}"

    End Function
    Public Function Distance(ByVal latitude1 As Double, ByVal longitude1 As Double, ByVal latitude2 As Double, ByVal longitude2 As Double) As Double

        Return VincentyInverse(latitude1, longitude1, latitude2, longitude2).Distance

    End Function
    Public Function DMS(value As String) As Double

        Return CDbl(DMS2D(value))

    End Function
    Public Function TimeOffset(latitude As Double, longitude As Double) As Double

        Dim iana As TimeZoneResult = TimeZoneLookup.GetTimeZone(latitude, longitude)
        Dim tzi As TimeZoneInfo = TZConvert.GetTimeZoneInfo(iana.Result)
        Dim result As TimeSpan = tzi.BaseUtcOffset
        Return result.TotalDays

    End Function
    Public Function TimeZone(latitude As Double, longitude As Double) As String

        Dim iana As TimeZoneResult = TimeZoneLookup.GetTimeZone(latitude, longitude)
        Dim result As String = iana.Result
        Return result

    End Function

End Module
