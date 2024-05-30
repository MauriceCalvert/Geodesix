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
Public Module IsEmpty_

    Public Sub IfNotEmpty(s As Object, a As Action(Of String))

        If Not IsEmpty(s) Then
            a(s.ToString)
        End If

    End Sub
    Public Function IsEmpty(o As Object) As Boolean

        If o Is Nothing Then
            Return True
        End If

        If TypeOf (o) Is String Then
            Return String.IsNullOrEmpty(CStr(o))
        End If

        Return False

    End Function

End Module
