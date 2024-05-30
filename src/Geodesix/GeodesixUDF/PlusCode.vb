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
Imports Google.OpenLocationCode
Imports Utilities

Partial Public Class GeodesixUDF
    Public Function PlusCode(arg1 As Object, Optional arg2 As Object = Nothing, Optional length As Object = 10) As Object

        Dim result As Object

        Try
            arg1 = GetValue(arg1)
            arg2 = GetValue(arg2)

            Dim latitude As Double
            Dim longitude As Double

            Dim arg1double As Boolean
            Dim arg2double As Boolean

            If TypeOf arg1 Is Double Then
                latitude = arg1
                arg1double = True
            ElseIf Double.TryParse(arg1?.ToString, latitude) Then
                arg1double = True
            End If

            If arg1double Then
                If TypeOf arg2 Is Double Then
                    longitude = arg2
                    arg2double = True
                ElseIf Double.TryParse(arg2?.ToString, longitude) Then
                    arg2double = True
                End If
            End If

            If arg1double AndAlso arg2double Then
                result = PlusCodeFrom(latitude, longitude, length)
            Else
                result = PlusCodeTo(arg1, arg2)
            End If

            CustomLog.Logger.Debug("PlusCode({0},{1},{2})={3}", arg1, arg2, length, result)

        Catch ex As Exception
            CustomLog.Logger.Error($"Formulae failed {ex.Message}")
            result = ex.Message

        End Try

        Return result

    End Function
    Private Function PlusCodeFrom(latitude As Object, longitude As Object, Optional length As Object = 10) As String ' Called by Excel

        Dim lat As Double
        Dim lng As Double

        ValidateDouble("Origin latitude", latitude, lat, -90.0, 90.0)
        ValidateDouble("Origin longitude", longitude, lng, -180.0, 180.0)

        length = GetValue(length)

        Dim result As String

        Try
            result = OpenLocationCode.Encode(lat, lng, length)

        Catch ex As Exception
            result = ex.Message
        End Try

        Return result

    End Function
    Private Function PlusCodeTo(pluscode As String, Optional convertto As String = "") As Object

        Dim result As Object

        Try
            Dim codearea As CodeArea = OpenLocationCode.Decode(pluscode)

            Dim latitude As Double = codearea.CenterLatitude
            Dim longitude As Double = codearea.CenterLongitude

            Dim returnrange As Object = Excel.Caller

            Dim cells As Integer = returnrange.Cells.Count

            If cells = 1 Then
                Select Case convertto.ToLower

                    Case "latitude"
                        result = latitude

                    Case "longitude"
                        result = longitude

                    Case Else
                        result = $"Invalid request {convertto}"

                End Select

            ElseIf cells = 2 Then
                result = {latitude, longitude}
            Else
                Return "!Result range must be 1 or 2 cells"
            End If

        Catch ex As Exception
            result = ex.Message
        End Try

        Return result

    End Function

End Class
