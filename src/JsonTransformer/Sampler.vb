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
Imports Newtonsoft.Json.Linq
Imports Utilities
Public Class Sampler

    Public Sub New(root As JToken, sampled As Boolean)

        Me.Root = root
        Me.Sampled = sampled

    End Sub
    Private Function Clone(input As JToken) As JToken

        Select Case input.GetType

            Case GetType(JArray)

                Dim source As JArray = DirectCast(input, JArray)
                Dim target As New JArray

                For Each item As JToken In source
                    target.Add(Clone(item))
                Next
                Return target

            Case GetType(JObject)

                Dim source As JObject = DirectCast(input, JObject)
                Dim target As New JObject

                For Each item As JProperty In source.Children
                    target.Add(Clone(item))
                Next
                Return target

            Case GetType(JProperty)

                Dim source As JProperty = DirectCast(input, JProperty)
                Dim target As New JProperty(source.Name)
                target.Value = Clone(source.Value)
                Return target

            Case GetType(JValue)
                Return input

            Case Else
                Throw New GeodesixException($"Unknown token type {input.GetType.Name}")

        End Select

    End Function
    Private Function CloneSample(input As JToken) As JToken

        Select Case input.GetType

            Case GetType(JArray)

                Dim source As JArray = DirectCast(input, JArray)
                Dim target As New JArray
                If Seen(source) Then
                    Return target
                End If
                ' !! get distinct items in array And take 2 of each
                For i As Integer = 0 To Math.Min(3, source.Count - 1)

                    Dim item As JToken = source(i)

                    ' We only write token nodes which are those that fulfil one of the following:
                    '   have index=0/1
                    '   have descendants properties
                    '   are leaves
                    If item.HasValues OrElse hasPropertyDescendants(item) Then
                        target.Add(CloneSample(item))
                    Else
                        Return target
                    End If
                Next
                Return target

            Case GetType(JObject)

                Dim source As JObject = DirectCast(input, JObject)
                Dim target As New JObject
                If Seen(source) Then
                    Return target
                End If

                For Each item As JProperty In source.Children
                    target.Add(CloneSample(item))
                Next
                Return target

            Case GetType(JProperty)

                Dim source As JProperty = DirectCast(input, JProperty)
                Dim target As New JProperty(source.Name)
                target.Value = CloneSample(source.Value)
                Return target

            Case GetType(JValue)

                Dim source As JValue = DirectCast(input, JValue)
                If Not Paths.ContainsKey(source.Path) Then
                    Paths.Add(source.Path, source)
                End If

                Return input

            Case Else
                Throw New GeodesixException($"Unknown token type {input.GetType.Name}")

        End Select

    End Function
    Private Function hasPropertyDescendants(token As JToken) As Boolean

        If TryCast(token, JContainer)?.
            Descendants.
            Any(Function(q) q.Type = JTokenType.Property) Then
            Return True
        End If

        Return False

    End Function
    Private ReadOnly Property Paths As Dictionary(Of String, JValue)
    Private _Result As JToken
    Public ReadOnly Property Result As JToken
        Get
            If _Result Is Nothing Then
                _Sampled = Sampled
                _Paths = New Dictionary(Of String, JValue)(StringComparer.OrdinalIgnoreCase)
                If Sampled Then
                    _Result = CloneSample(Root)
                Else
                    _Result = Clone(Root)
                End If
            End If
            Return _Result
        End Get
    End Property
    Public ReadOnly Property Root As JToken
    Public ReadOnly Property Sampled As Boolean
    Private Function Seen(token As JContainer) As Boolean

        Return token.
               Descendants.
               OfType(Of JValue).
               All(Function(q) Paths.ContainsKey(q.Path))

    End Function

End Class
