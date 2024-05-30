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
Public Module Coordinates_

    ''' <summary>
    ''' Give a pair of coordinates, e.g. "22.7,19.3" in English or "22,7;19,3" in French,
    ''' Return it in English style, i.e. "22.7,19.3"
    ''' </summary>
    ''' <param name="coords"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InvariantCoordinates(ByVal coords As String) As String

        Dim ans As String = coords
        Dim lat As Double
        Dim lng As Double
        If TryParseCoordinates(coords, lat, lng) Then
            ans = lat.ToString(System.Globalization.CultureInfo.InvariantCulture) &
                  "," &
                  lng.ToString(System.Globalization.CultureInfo.InvariantCulture)
        End If
        Return ans
    End Function
    Public Function TryParseCoordinates(coordinates As String, ByRef latitude As Double, ByRef longitude As Double) As Boolean
        Dim sep As Char = CChar(LISTSEP)
        If coordinates.Contains(LISTSEP) Then
            sep = CChar(LISTSEP)
        ElseIf coordinates.Contains(",") Then
            sep = ","c
        Else
            Return False
        End If
        Dim parts() As String
        parts = coordinates.Split(sep)
        If parts.Length = 2 Then
            Dim lat As Double
            Dim lng As Double
            If Double.TryParse(parts(0), lat) Then ' in the local culture
                If Double.TryParse(parts(1), lng) Then
                    latitude = lat
                    longitude = lng
                    Return True
                End If
            End If
        End If
        Return False
    End Function

End Module
