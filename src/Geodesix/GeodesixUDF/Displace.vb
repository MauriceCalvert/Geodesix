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

    Public Function Displace(latitude As Object, longitude As Object, bearing As Object, distance As Object) As Object

        Try
            Dim lat As Double
            Dim lng As Double
            Dim brg As Double
            Dim dis As Double
            ValidateDouble("Latitude", latitude, lat, -90.0, 90.0)
            ValidateDouble("Longitude", longitude, lng, -90.0, 90.0)
            ValidateDouble("Bearing", bearing, brg)
            ValidateDouble("Distance", distance, dis)

            Dim result As VincentyDirectResult = VincentyDirect(lat, lng, brg, dis)

            Dim returnrange As Object = Excel.Caller

            Dim cells As Integer = returnrange.Cells.Count

            If cells = 1 Then
                Return $"{result.Latitude},{result.Longitude}"
            ElseIf cells = 2 Then
                Return {result.Latitude, result.Longitude}
            Else
                Return "!Result range must be 1 or 2 cells"
            End If

        Catch ex As Exception
            Dim msg As String = $"Displace({latitude},{longitude},{bearing},{distance}) failed {ex.Message}"
            CustomLog.Logger.Error(msg)
            Return ex.Message

        End Try
    End Function

End Class
