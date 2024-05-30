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

Public Module GenericPath_

    Private RegexIndices As New Regex("\[(\d+)\]", RegexOptions.Compiled)

    Public Function GenericPath(path As String) As String

        ' Replace all [nn] in a path with [*] except for the trailing [nn]
        '
        ' From the path A[2].B[6][7]
        ' Get A[*].B[*][7]

        If Not path.EndsWith("]") Then ' A[2].B[6].XYZ = A[*].B[*].XYZ
            Return RegexIndices.Replace(path, "[*]")
        End If

        ' Path finishes with [nn]
        Dim bp As Integer = path.LastIndexOf("[")
        If bp < 0 Then
            Return path
        End If

        Dim before As String = ""
        If bp > 0 Then
            before = RegexIndices.Replace(path.Substring(0, bp), "[*]")
        End If

        Dim result As String = before & path.Substring(bp, path.Length - bp)
        Return result

    End Function


End Module
