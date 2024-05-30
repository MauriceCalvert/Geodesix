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
Partial Public Class GeodesixUDF

    Public Function GeoReverse(ByVal request As Object, latitude As Object, longitude As Object) As Object

        Dim result As Object = ""

        Try
            Dim verb As String = GetValueString(request)
            Dim lat As Double
            Dim lng As Double

            ValidateDouble("Latitude", latitude, lat, -90.0, 90.0)
            ValidateDouble("Longitude", longitude, lng, -180.0, 180.0)

            result = Geocode(verb, lat.ToString & "," & lng.ToString)

        Catch ex As Exception
            CustomLog.Logger.Error("GeoReverse({0},{1},{2}) failed {3}", request, latitude, longitude, ex.Message)
            result = ex.Message

        End Try

        Return result

    End Function

End Class
