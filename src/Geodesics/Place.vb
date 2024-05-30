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
Imports Utilities
''' <summary>
''' A place, of one sort or another. For a given query, Google Maps can return up to 
''' 10 matches, each of which are represented as this Place object.
''' </summary>
''' <remarks>
''' Places have Fields, which are the various fields returned by Google:
''' Latitude, Longitude, Formatted_Address, etc
''' </remarks>
Public Class Place

    Public Sub New(key As String)
        _Key = key
    End Sub
    Public ReadOnly Property Key As String
    Public ReadOnly Property Location As Location
        Get
            Dim lat As Object = Nothing
            Dim lng As Object = Nothing
            If Fields.TryGetValue("latitude", lat) AndAlso Fields.TryGetValue("longitude", lng) Then
                Dim latitude As Double
                Dim longitude As Double
                If Double.TryParse(CStr(lat), latitude) AndAlso Double.TryParse(CStr(lng), longitude) Then
                    Return New Location(latitude, longitude)
                End If
            End If
            Return Location.ZERO
        End Get
    End Property
    Public Property Tag(ByVal key As String) As Object
        Get
            Dim result As Object = ""
            Fields.TryGetValue(key, result)
            Return result
        End Get
        Set(value As Object)
            If String.IsNullOrEmpty(key) OrElse String.IsNullOrEmpty(CStr(value)) Then
#If DEBUG Then
                Stop
#End If
                Exit Property
            End If
            Fields(key) = value
        End Set
    End Property
    Public Fields As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
    Public Overrides Function ToString() As String
        Return Key & " " & String.Join(", ", Fields.Select(Function(q) q.Key & "=" & TextSample(CStr(q.Value))))
    End Function
End Class
