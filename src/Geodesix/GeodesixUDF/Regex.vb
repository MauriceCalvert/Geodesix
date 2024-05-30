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
Imports System.Text.RegularExpressions
Imports Seting
Imports Utilities
Partial Public Class GeodesixUDF

    Public Function Regex(text As Object, pattern As Object, group As Object, item As Object) As Object

        Try
            Dim texts As String = GetValueString(text)
            If texts Is Nothing Then
                Return "!Text to parse is missing"
            End If
            Dim patterns As String = GetValueString(pattern)
            If patterns Is Nothing Then
                Return "!Regex pattern is missing"
            End If
            Dim groups As String = GetValueString(group)
            If groups Is Nothing Then
                Return "!Group number is missing"
            End If
            Dim items As String = GetValueString(item)
            If items Is Nothing Then
                Return "!Item number is missing"
            End If

            Dim expr As New Regex(patterns, RegexOptions.None, New TimeSpan(0, 0, 0, 0, Settings.RegexTimeout))
            Dim matches As MatchCollection = expr.Matches(texts)
            If matches.Count = 0 Then
                Return "!No matches"
            End If
            If matches(0).Groups.Count < 2 Then
                Return "!Matched but no groups"
            End If
            Dim result As Object = ""

            Dim groupd As Double = 0
            If Double.TryParse(groups, groupd) Then
                Dim groupi As Integer = CInt(groupd)
                If groupi < 0 Then
                    Return "!Group must be >= 0"
                End If
                If groupi >= matches.Count Then
                    Return $"!Group {groupi} >= matches {matches.Count }"
                End If

                Dim itemd As Double = 0
                If Not Double.TryParse(items, itemd) Then
                    Return $"!Item number {items} is not numeric"
                End If
                Dim itemi As Integer = CInt(itemd)
                If itemi < 0 Then
                    Return "!Item must be >= 0"
                End If
                If itemi >= matches(groupi).Groups.Count Then
                    Return $"!Item {itemi} >= Group({groupi}).Items {matches(groupi).Groups.Count }"
                End If
                Return matches(groupi).Groups(itemi).Value
            End If

            Select Case groups.ToString.ToLower
                Case "matches"
                    Return matches(0).Groups.Count - 1
                Case Else
                    Return $"!Invalid group {groups}"
            End Select

        Catch ex As Exception
            Return $"!Regex error " & ex.Message
        End Try
    End Function

End Class
