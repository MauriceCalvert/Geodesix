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
Imports System.Collections.Generic
Imports System.Runtime.InteropServices

<ComVisible(False)>
Public Class Column
    Sub New(parent As Node, name As String)
        _Parent = parent
        _Name = name
        _Offset = parent.Unique()
    End Sub
    Sub Add(row As Integer, value As Object)
        If value Is Nothing Then
            Exit Sub
        End If
        If String.IsNullOrEmpty(value.ToString) Then
            Exit Sub
        End If
        Values.Add(row, value)
    End Sub
    Property Current As Object
    ReadOnly Property Offset As Integer
    ReadOnly Property Name As String
    ReadOnly Property Parent As Node
    Public Overrides Function ToString() As String
        Return Name & " " & Offset
    End Function
    Property Values As New Dictionary(Of Integer, Object)
End Class
