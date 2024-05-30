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
Imports System.Collections.Generic
Imports System.Linq
Imports System.Math
Imports Utilities
Partial Public Class GeodesixUDF

    Public Function Area(range As Object) As Object

        Dim result As Double
        Dim xr As ExcelRectangle
        Try
            Dim sheet As Object = Excel.ActiveSheet
            range = GetValue(range)
            xr = New ExcelRectangle(range)

            Dim lastrow As Integer = xr.Data.GetUpperBound(0)
            lastrow = Min(lastrow, LastUsedRow(sheet))

            Dim points As New List(Of Coordinate)

            For row As Integer = 0 To lastrow

                Dim lat As Object = GetValue(xr(row, 0))
                Dim lng As Object = GetValue(xr(row, 1))

                If IsNumeric(lat) AndAlso IsNumeric(lng) AndAlso
                   Between(lat, -90, 90) AndAlso Between(lng, -180, 180) Then

                    points.Add(New Coordinate(lat, lng))
                Else
                    Return $"!Point {row} invalid coordinates {lat},{lng}"
                End If
            Next

            If points.Count < 3 Then
                Return "!Insufficient coordinates, at least 3 required"
            End If

            Dim center As Coordinate = Centroid(points)

            Dim planar(points.Count - 1) As Coordinate

            For i As Integer = 0 To points.Count - 1

                Dim deltalat As Double = VincentyInverse(center.Latitude, center.Longitude, points(i).Latitude, center.Longitude).Distance
                deltalat *= Math.Sign(points(i).Latitude - center.Latitude)

                Dim deltalon As Double = VincentyInverse(center.Latitude, center.Longitude, center.Latitude, points(i).Longitude).Distance
                deltalon *= Math.Sign(points(i).Longitude - center.Longitude)

                planar(i) = New Coordinate(deltalat, deltalon) ' Not really a coordinate, XY in metres
            Next

            result = PolygonArea(planar)

        Catch ex As Exception
            Dim msg As String = $"Area(xr) failed {ex.Message}"
            CustomLog.Logger.Error(msg)
            Return ex.Message

        End Try
        Return result

    End Function
    Private Function Centroid(coordinates As List(Of Coordinate)) As Coordinate
        ' Calculate the average latitude and longitude to use as the centroid
        Dim sumLat As Double = 0
        Dim sumLon As Double = 0

        For Each coord As Coordinate In coordinates
            sumLat += coord.Latitude
            sumLon += coord.Longitude
        Next

        Dim centroidLat As Double = sumLat / coordinates.Count
        Dim centroidLon As Double = sumLon / coordinates.Count

        Return New Coordinate(centroidLat, centroidLon)
    End Function

    Private Function PolygonArea(coordinates As IEnumerable(Of Coordinate)) As Double

        Dim n As Integer = coordinates.Count

        If n < 3 Then
            ' A polygon must have at least 3 vertices to have an area
            Return 0
        End If

        Dim area As Double = 0

        ' Apply the shoelace formula
        For i As Integer = 0 To n - 1
            Dim x1 As Double = coordinates(i).Longitude
            Dim y1 As Double = coordinates(i).Latitude
            Dim x2 As Double = coordinates((i + 1) Mod n).Longitude
            Dim y2 As Double = coordinates((i + 1) Mod n).Latitude

            area += (x1 * y2 - x2 * y1)
        Next

        ' Divide by 2 and take the absolute value to get the area
        area = Math.Abs(area) / 2.0

        Return area
    End Function

End Class
