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
Imports System.Text
Imports Microsoft.VisualBasic
Public Module NormaliseWhiteSpace_

    Private ReadOnly Property WHITE As String = $" {vbCR}{vbLF}{vbTab}"

    Public Function NormaliseWhiteSpace(ByVal input As String) As String

        Dim len As Integer = input.Length
        Dim src() As Char = input.ToCharArray()
        Dim skip As Boolean = False
        Dim ch As Char
        Dim sb As New StringBuilder(len)

        For i As Integer = 0 To len - 1
            ch = src(i)
            If WHITE.Contains(ch) Then
                If Not skip Then
                    sb.Append(" ")
                End If
                skip = True
            Else
                sb.Append(ch)
                skip = False
            End If
        Next
        Dim result As String = sb.ToString
        Return result
    End Function
End Module
