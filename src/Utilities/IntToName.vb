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
Imports System.Linq

Public Module IntToName_

    Private ReadOnly Alphabet As Char() = "bcdfghjklmnpqrstvwxyz".ToCharArray() ' Note: No vowels. And no bickering over 'y'
    Public ReadOnly Property AlphabetLength As Integer = Alphabet.Length

    Private ReadOnly CharValues As Dictionary(Of Char, Integer) =
        Alphabet.Select(Function(c As Char, i As Integer) New With {
            .Char = c,
            .Index = i
            }).
        ToDictionary(Function(c) c.Char, Function(c) c.Index)

    Public Function IntToName(ByVal value As Long) As String

        Dim targetBase As Long = AlphabetLength
        Dim buffer As Char() = New Char(Math.Max(CInt(Math.Ceiling(Math.Log(value + 1, targetBase))), 1) - 1) {}
        Dim i As Long = buffer.Length

        Do
            buffer(CInt(Threading.Interlocked.Decrement(i))) = Alphabet(CInt(value Mod targetBase))

            value = value \ targetBase
        Loop While value > 0

        Return New String(buffer, CInt(i), CInt(buffer.Length - i))

    End Function

    Public Function NameToInt(ByVal number As String) As Long

        Dim chrs As Char() = number.ToCharArray()
        Dim m As Integer = chrs.Length - 1
        Dim x As Integer, n As Integer = Alphabet.Length
        Dim result As Long = 0

        For i As Integer = 0 To chrs.Length - 1
            x = CharValues(chrs(i))
            result += x * CLng(Math.Pow(n, Math.Max(Threading.Interlocked.Decrement(m), m + 1)))
        Next

        Return result
    End Function
End Module
