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
    Public Function Distance(ByVal latitude1 As Object, ByVal longitude1 As Object, ByVal latitude2 As Object, ByVal longitude2 As Object) As Object

        Dim lat1 As Double
        Dim lon1 As Double
        Dim lat2 As Double
        Dim lon2 As Double
        Try
            ValidateDouble("Origin latitude", latitude1, lat1, -90.0, 90.0)
            ValidateDouble("Origin longitude", longitude1, lon1, -180.0, 180.0)
            ValidateDouble("Destination latitude", latitude2, lat2, -90.0, 90.0)
            ValidateDouble("Destination longitude", longitude2, lon2, -180.0, 180.0)

            Return VincentyInverse(lat1, lon1, lat2, lon2).Distance

        Catch ex As Exception
            Dim msg As String = $"Distance({latitude1},{longitude1},{latitude2},{longitude2}) failed {ex.Message}"
            CustomLog.Logger.Error(msg)
            Return ex.Message

        End Try

    End Function
    Public Function GreatCircleDistance(ByVal latitude1 As Object, ByVal longitude1 As Object, ByVal latitude2 As Object, ByVal longitude2 As Object) As Object

        Return Distance(latitude1, longitude1, latitude2, longitude2)

    End Function

End Class
