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
Public Module TextSample_
    Public Function TextSample(s As String) As String
        If String.IsNullOrEmpty(s) Then
            Return ""
        End If
        Dim result As String = s.Substring(0, Math.Min(s.Length, 100))
        result = NormaliseWhiteSpace(result)
        Dim l As Integer = result.Length
        If l > 30 Then
            result = result.Substring(0, 30) & "..."
        End If
        Return result
    End Function
End Module
