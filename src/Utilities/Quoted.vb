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
Public Module Quoted_

    Public Function Quoted(s As Object) As Object

        If TypeOf s Is String OrElse TypeOf s Is Char Then

            Dim trimmed As String
            If IsEmpty(s) Then
                Return """"""
            Else
                trimmed = s.ToString
            End If
            trimmed = trimmed.Trim(" "c)

            If trimmed.BeginsWith("""") AndAlso trimmed.EndsWith("""") Then
                ' Check that it has balanced quotes
                Dim literal As String = trimmed.
                                        Substring(1, trimmed.Length - 2)
                If literal.Count(Function(q) q = """"c) Mod 2 = 0 Then
                    Return trimmed
                Else
                    trimmed = literal
                End If
            End If
            Return """" & trimmed.Replace("""", """""") & """"
        Else
            Return s
        End If

    End Function
    Public Function JSQuote(s As String) As String

        Dim trimmed As String = s.Trim(" "c)

        If trimmed.BeginsWith("'") AndAlso trimmed.EndsWith("'") Then
            ' Check that it's correctly escaped, with balanced quotes
            Dim literal As String = trimmed.
                                    Substring(1, trimmed.Length - 2).
                                    Replace("\'", "'")
            If literal.Count(Function(q) q = "'"c) Mod 2 = 0 Then
                Return trimmed
            Else
                trimmed = literal
            End If
        End If

        ' Remove control characters.
        ' Remove multiple blanks.
        ' Double single quotes with \'

        Dim result As New StringBuilder(trimmed.Length)
        result.Append("'")
        Dim previous As Char = "X"c

        For Each ch As Char In trimmed
            If ch = "'"c Then
                result.Append("\'")
            ElseIf ch = " "c AndAlso previous <> " "c Then
                result.Append(" ")
            ElseIf ch > " "c Then
                result.Append(ch)
            ElseIf previous = " "c Then
                ch = " "c
            End If
            previous = ch
        Next
        result.Append("'")

        Return result.ToString

    End Function

End Module
