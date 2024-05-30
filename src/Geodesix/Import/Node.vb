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
Imports System.Linq
Imports System.Runtime.InteropServices
Imports Utilities
<ComVisible(False)>
Public Class Node
    Private Shared _Unique As Integer = 0
    Sub New(parent As Node, name As String)
        _Parent = parent
        _Name = name
        _Offset = Unique()
        If _Offset > 16300 Then
            Throw New GeodesixException("Output cannot exceed 16384 columns")
        End If
    End Sub
    Function Child(name As String) As Node
        Dim result As Node = Children.Where(Function(q) q.Name = name).FirstOrDefault
        If result Is Nothing Then
            result = New Node(Me, name)
            Children.Add(result)
        End If
        Return result
    End Function
    ReadOnly Property Children As New List(Of Node)
    Function Column(name As String) As Column
        Dim result As Column = Columns.Where(Function(q) q.Name = name).FirstOrDefault
        If result Is Nothing Then
            result = New Column(Me, name)
            Columns.Add(result)
        End If
        Return result
    End Function
    ReadOnly Property Columns As New List(Of Column)
    ReadOnly Property Name As String
    ReadOnly Property Parent As Node
    Friend Shared Sub Reset()
        _Unique = 0
    End Sub
    Public Overrides Function ToString() As String
        Return Name & " " & Offset & "=" & String.Join(" ", Columns)
    End Function
    ReadOnly Property Offset As Integer
    Friend Function Unique() As Integer
        _Unique += 1
        Return _Unique
    End Function
    Friend Visited As Boolean = False
End Class