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
Imports System.Drawing
Imports System.Text

Public Module Colour_
    ''' <summary>
    ''' Convert a colour to a hex string
    ''' </summary>
    ''' <param name="colour">A 6/8 digit hex color or a known color</param>
    ''' <param name="format">Any combination of the letters #argb</param>
    ''' <returns>Corresponding hex string</returns>
    Public Function Colour(clr As String, Optional format As String = "#RGB") As String

        Dim hexa As String = ""

        ' Is it exactly 6 or 8 hex characters?
        Dim value As Integer = 0

        If clr.Length > 1 AndAlso clr.BeginsWith("#") Then ' Remove leading hash sign for parsing
            clr = clr.Substring(1)
        End If

        If Integer.TryParse(clr, Globalization.NumberStyles.HexNumber, Globalization.CultureInfo.InvariantCulture, value) Then

            Dim trial As String = clr
            Dim len As Integer = trial.Length
            If len = 6 Then
                hexa = "FF" & clr
            ElseIf len = 8 Then
                hexa = clr
            End If
        End If

        If hexa = "" Then ' Wasn't colour, try a known colour. Defaults to ARGB(0,0,0,0)
            Dim known As Color = Color.FromName(clr)
            If known.A = 0 AndAlso known.R = 0 AndAlso known.G = 0 AndAlso known.B = 0 Then
                Return "!Invalid colour {clr}"
            End If
            hexa = $"{known.A:X2}{known.R:X2}{known.G:X2}{known.B:X2}"
        End If

        Dim result As New StringBuilder(9)

        For Each ch As Char In format.ToLower
            Select Case ch
                Case "#"c
                    result.Append("#")
                Case "a"c
                    result.Append(hexa.Substring(0, 2))
                Case "r"c
                    result.Append(hexa.Substring(2, 2))
                Case "g"c
                    result.Append(hexa.Substring(4, 2))
                Case "b"c
                    result.Append(hexa.Substring(6, 2))
                Case Else
                    result.Append(ch)
            End Select
        Next
        Return result.ToString
    End Function

End Module
