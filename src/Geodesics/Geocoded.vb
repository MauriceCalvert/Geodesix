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
Public Module Geocoded_
    ''' <summary>
    ''' Returns a single field (tag) of a geocode request
    ''' </summary>
    ''' <param name="request">The field to return (Status, Latitude, Longitude, etc.)</param>
    ''' <param name="location">The name of the place that was geocoded</param>
    ''' <returns>A value, of the appropriate type (usually String or Double)</returns>
    ''' <remarks>Only applicable to geocodes which have already been requested. New geocodes must be
    ''' made with InitiateGeocode</remarks>
    Public Function Geocoded(ByVal query As Query, ByVal request As String, ByVal location As String) As Object

        Dim result As Object = ""

        If request = "status" Then
            result = query.Status

        ElseIf query.Completed Then

            If query.Status = "OK" Then

                result = query.Field(request)

                If result Is Nothing Then
                    result = $"!No {request} here"
                Else
                    Dim number As Double
                    If Double.TryParse(result.ToString, number) Then
                        result = number
                    End If
                End If
            Else
                result = $"!{query.Status}"
            End If
        End If
        Return result


        Return result

    End Function
End Module
