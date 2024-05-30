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
Imports System.Text
Public Module TextRectangle_

    Public Function TextRectangle(s As String) As String

        ' Aspect = width / Height

        Dim chars As Integer = s.Length
        If chars < 20 Then
            Return s
        End If
        Dim words As String() = s.Split({" "c}, StringSplitOptions.RemoveEmptyEntries)
        If words.Count < 2 Then
            Return s
        End If
        Dim lines As Integer = CInt(Math.Round(2 + chars / 111))
        Dim halfword As Integer = CInt(words.Select(Function(q) q.Length).Average / 2)

        Dim linelength As Integer = chars \ lines
        Dim result As New StringBuilder(lines * (linelength + 6))
        Dim i As Integer = 0
        Do
            Dim line As New StringBuilder(linelength + 6)
            Do
                line.Append(words(i))
                line.Append(" ")
                i += 1
            Loop Until i > words.Count - 1 Or line.Length + halfword >= linelength
            result.Append(line.ToString.TrimEnd({" "c}))
            If i < words.Count Then
                result.Append("<br />")
            Else
                Exit Do
            End If
        Loop
        Return result.ToString

    End Function

End Module
