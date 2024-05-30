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
    Public Function Azimuth(latitude1 As Object, longitude1 As Object, latitude2 As Object, longitude2 As Object) As Object

        Dim lat1 As Double
        Dim lon1 As Double
        Dim lat2 As Double
        Dim lon2 As Double
        Try
            ValidateDouble("Origin latitude", latitude1, lat1, -90.0, 90.0)
            ValidateDouble("Origin longitude", longitude1, lon1, -180.0, 180.0)
            ValidateDouble("Destination latitude", latitude2, lat2, -90.0, 90.0)
            ValidateDouble("Destination longitude", longitude2, lon2, -180.0, 180.0)

            Dim result As VincentyInverseResult = VincentyInverse(lat1, lon1, lat2, lon2)

            Dim returnrange As Object = Excel.Caller

            Dim cells As Integer = returnrange.Cells.Count

            If cells = 1 Then
                Return result.InitialBearing
            ElseIf cells = 2 Then
                Return {result.InitialBearing, result.FinalBearing}
            Else
                Return "!Result range must be 1 or 2 cells"
            End If

            Return toDegrees(result.InitialBearing)

        Catch ex As Exception
            Dim msg As String = $"Distance({latitude1},{longitude1},{latitude2},{longitude2}) failed {ex.Message}"
            CustomLog.Logger.Error(msg)
            Return ex.Message

        End Try
    End Function

End Class
