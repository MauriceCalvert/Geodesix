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
Imports Geodesics
Imports Utilities
Partial Public Class GeodesixUDF
    Public Function TravellingSalesman(ByVal namex As Object,
                                       ByVal latitudex As Object,
                                       ByVal longitudex As Object) As Object
        Dim caller As Object
        Dim ws As Object
        Dim row As Integer
        Dim col As Integer
        Dim name As String
        Dim latitude As Double
        Dim longitude As Double
        Dim result As Object
        Dim temp As Object
        Dim city As TravellingSalesmanSolver.Visit
        Dim tss As TravellingSalesmanSolver
        Dim workbook As Object = ActiveWorkbook

        Try
            Excel.Volatile(True)

            name = GetValueString(namex).ToLower

            temp = GetValue(latitudex)
            If TypeOf temp Is Double Then
                latitude = DirectCast(temp, Double)
            Else
                If temp Is Nothing Then
                    Return "Missing latitude"
                Else
                    If Not Double.TryParse(temp.ToString, latitude) Then
                        Return "Invalid latitude " & temp.ToString
                    End If
                End If
            End If

            temp = GetValue(longitudex)
            If TypeOf temp Is Double Then
                longitude = DirectCast(temp, Double)
            Else
                If temp Is Nothing Then
                    Return "Missing longitude"
                Else
                    If Not Double.TryParse(temp.ToString, longitude) Then
                        Return "Invalid longitude " & temp.ToString
                    End If
                End If
            End If

            caller = Excel.Caller
            ws = caller.Worksheet
            row = CInt(caller.Row)
            col = CInt(caller.Column)
            tss = Cache.TSS

            If tss.Cities.Contains(name) Then
                city = DirectCast(tss.Cities(name), TravellingSalesmanSolver.Visit)
                city.lat = latitude
                city.lng = longitude
                city.row = row
                city.column = col
            Else
                city = tss.AddCity(name, row, col, latitude, longitude)
            End If
            result = city.order


        Catch ex As Exception
            CustomLog.Logger.Error("TravellingSalesman('{0}','{1}','{2}') failed {3}", namex, latitudex, longitudex, ex.Message)
            result = ex.Message

        End Try

        Return result

    End Function
    Public Function TravellingSalesmanDistance() As Double

        Dim workbook As Object = ActiveWorkbook
        Dim tss As TravellingSalesmanSolver
        Dim caller As Object
        Dim ws As Object

        Excel.Volatile(True)

        caller = Excel.Caller
        ws = caller.Worksheet

        tss = Cache.TSS
        If tss.ShortestDistance = Double.MaxValue Then
            Return 0
        Else
            Return tss.ShortestDistance
        End If

    End Function

End Class
