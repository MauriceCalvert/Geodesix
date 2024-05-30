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
Public Module Clamp_

    Public Function Clamp(Of T As {IComparable(Of T)})(value As T, min As T, max As T) As T

        Dim low As T
        Dim high As T

        If min.CompareTo(max) < 0 Then
            low = min
            high = max
        Else
            low = max
            high = min
        End If

        If value.CompareTo(min) < 0 Then
            Return min
        ElseIf value.CompareTo(max) > 0 Then
            Return max
        Else
            Return value
        End If

    End Function

End Module
