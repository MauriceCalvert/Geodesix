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
Imports GeoTimeZone
Imports Utilities
Partial Public Class GeodesixUDF

    Public Function TimeZone(latitude As Double, longitude As Double) As String

        Try
            Dim lat As Double
            Dim lng As Double
            ValidateDouble("Latitude", latitude, lat, -90.0, 90.0)
            ValidateDouble("Longitude", longitude, lng, -180.0, 180.0)

            Dim iana As TimeZoneResult = TimeZoneLookup.GetTimeZone(lat, lng)
            Dim result As String = iana.Result
            Return result

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

End Class
