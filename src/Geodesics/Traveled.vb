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
Public Module Traveled_
    ''' <summary>
    ''' Returns a single field (tag) of a Travel (Directions) request
    ''' </summary>
    ''' <param name="request">Duration or Distance</param>
    ''' <param name="origin">The start location</param>
    ''' <param name="destination">The finish location</param>
    ''' <param name="mode">The mode of travel: Transit, Driving, Bicycling or Walking</param>
    ''' <returns>A value, of the appropriate type (usually String or Double)</returns>
    ''' <remarks>Only applicable to Directions requests which have already been requested. New Directions must be
    ''' made with InitiaiteTravel</remarks>

    Public Function Traveled(ByVal query As Query,
                             ByVal request As String,
                             ByVal origin As String,
                             ByVal destination As String,
                             ByVal mode As String) As Object

        Dim result As Object = ""

        If request = "status" Then
            result = query.Status

        ElseIf query.Completed Then

            If query.Status = "OK" Then
                result = query.Field(request)
                Dim number As Double
#Disable Warning BC42016 ' Implicit conversion
                If Double.TryParse(result, number) Then
#Enable Warning BC42016 ' Implicit conversion
                    result = number
                End If
            Else
                result = $"!{query.Status}"
            End If
        End If
        Return result

    End Function

End Module
