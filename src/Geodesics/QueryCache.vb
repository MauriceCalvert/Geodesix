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
Imports System.Collections.Concurrent

Public Class QueryCache

    Public ReadOnly Property Purged As Boolean

    Private ReadOnly Property Excel As Object
    Public ReadOnly Property GUIDs As String
    Public Local As New ConcurrentDictionary(Of String, Query)
    Public Sub New(excel As Object, GUIDs As String)

        Me.Excel = excel
        Me.GUIDs = GUIDs

    End Sub
    Public Function Contains(key As String) As Boolean

        Return Local.ContainsKey(key)

    End Function
    Public Function GetQuery(Of T As {New, Query})(key As String) As Query

        Dim result As Query = Nothing
        If Local.TryGetValue(key, result) Then
            Return result
        End If

        result = New T
        result.Key = key
        Local.TryAdd(key, result)
        Return result

    End Function
    Friend Property Ready As Boolean
    Public ReadOnly Property Queries As IEnumerable(Of Query)
        Get
            Return Local.Values
        End Get
    End Property
    Public Sub TryAdd(query As Query)

        Local.TryAdd(query.Key, query)

    End Sub
    Public Function TryGetQuery(key As String, ByRef query As Query) As Boolean

        Return Local.TryGetValue(key, query)

    End Function
End Class
