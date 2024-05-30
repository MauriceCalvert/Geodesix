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
Public Module DMS2D_
    Public Function DMS2D(value As String) As Object

        Dim s As String = value.ToUpper
        Dim updown As Char = " "c

        ' Starts with NSEW+-?
        Dim leading As Char = s(0)
        If "NSEW+-".IndexOf(leading) >= 0 Then
            updown = leading
            s = s.Substring(1)
        End If

        ' Ends with NSEW+-?
        Dim trailing As Char = s(s.Length - 1)
        If "NSEW+-".IndexOf(trailing) >= 0 Then
            If updown <> " "c Then
                Throw New FormatException($"Leading {updown} and trailing {trailing} is invalid")
            End If
            updown = trailing
            s = s.Substring(0, s.Length - 1)
        End If

        ' S,W,- = negative
        Dim multiplier As Double = 1
        If "SW-".IndexOf(updown) >= 0 Then
            multiplier = -1
        End If

        Dim result As Double
        If Double.TryParse(value, result) Then ' Simple decimal degrees?
            Return result * multiplier
        End If

        Dim degrees As Double
        Parse("degrees", "°"c, s, degrees, -180.0, 180.0)

        Dim limit As Double = 60 - Double.Epsilon ' 59.999999...

        Dim minutes As Double
        Parse("minutes", """"c, s, minutes, -limit, limit)

        Dim seconds As Double
        Parse("seconds", "'"c, s, seconds, -limit, limit)

        If s.Trim <> "" Then
            Throw New FormatException($"Invalid extraneous characters {s}")
        End If

        result = multiplier * degrees + minutes / 60.0 + seconds / 3600.0

        If result < -180.0 OrElse result > 180.0 Then
            Throw New FormatException($"{result} is out of range")
        End If

        Return result

    End Function
    Private Sub Parse(name As String, unit As Char, ByRef s As String, ByRef result As Double, min As Double, max As Double)

        Dim unitpos As Integer = s.IndexOf(unit)

        If unitpos >= 0 Then

            If unitpos = 0 Then
                Throw New FormatException($"{name} value missing before {unit}")
            End If

            Dim val As String = s.Substring(0, unitpos)

            If Not Double.TryParse(val, result) Then
                If val.Trim = "" Then
                    Throw New FormatException($"{name}: missing number")
                Else
                    Throw New FormatException($"{name}: {val} is not a number")
                End If
            End If

            If result < min OrElse result > max Then
                Throw New FormatException($"{name} {result} out of range {min}..{max}")
            End If

            s = $"{s} ".Substring(unitpos + 1) ' Add an extra space to avoid failure parsing end-of-string
        End If
    End Sub

End Module
