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
Imports System.Text
Imports Utilities
Public Class ExportGeoJson
    Inherits Exporter

    Public Sub New(ByRef excel As Object, sheetname As String, ByRef target As String, template As List(Of String))
        MyBase.New(excel, sheetname, target, template)
    End Sub
    Private Sub Add(ByRef feature As StringBuilder, keys As IEnumerable(Of String))

        Dim values As New List(Of String)
        Dim added As Boolean = False

        For Each key As String In keys

            Dim value As String = Variable(key).Value
            If value = "" Then
                Continue For
            End If

            If key.Includes("color") Then
                value = Colour(value, "#rgb")
            End If
            added = True
            Dim sb As New StringBuilder
            value = value.Replace("""", "\""")
            sb.Append("""")
            sb.Append(key)
            sb.Append(""":""")
            sb.Append(value)
            sb.Append("""")
            values.Add(sb.ToString)
        Next

        If added Then
            feature.Append(",")
            feature.Append(String.Join(",", values))
        End If

    End Sub
    Protected Overrides Sub Body(final As Boolean)

        Dim latitude As Double = 0
        Dim longitude As Double = 0

        Dim feature As New StringBuilder

        If HaveAllValues({"latitude", "longitude"}) Then
            If Not Double.TryParse(Variable("latitude").Value, latitude) Then
                Return
            End If
            If Not Double.TryParse(Variable("longitude").Value, longitude) Then
                Return
            End If
        Else
            Return
        End If

        If latitude <> 0 AndAlso longitude <> 0 Then

            feature.Append("{""type"":""Feature"",""properties"":{")
            feature.Append("},""geometry"":{""type"":""Point"",""coordinates"":[")
            feature.Append(longitude)
            feature.Append(",")
            feature.Append(latitude)
            feature.Append("]")
            feature.Append("}")
            Add(feature, Keys)
            feature.Append("}")

            If Not final Then
                feature.Append(",")
            End If

            AddRow(feature.ToString)
        End If

    End Sub
    Protected Overrides Sub Epilogue()

        AddRow("]}")
        FlushRaw("features")

    End Sub
    Protected Overrides Sub Prologue()

        AddRow("{""type"": ""FeatureCollection"", ""features"": [")
        ' Variable("icon").DefaultValue = "http://maps.google.com/mapfiles/kml/paddle/red-circle.png"

    End Sub
End Class
