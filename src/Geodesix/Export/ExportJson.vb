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
Imports System.IO
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Utilities
Public Class ExportJson
    Inherits Exporter

    Public Sub New(ByRef excel As Object, sheetname As String, ByRef target As String, template As List(Of String))
        MyBase.New(excel, sheetname, target, template)
    End Sub
    Protected Overrides Sub Prologue()

        AddRow("{""Root"": [")

    End Sub
    Protected Overrides Sub Body(final As Boolean)

        Dim jo As New JObject

        For Each key As String In Keys.Where(Function(q) Not IsEmpty(q))

            Dim val As String = Variable(key).Value
            If IsEmpty(val) Then
                Continue For
            End If
            jo.Add(New JProperty(key, val))
        Next
        Dim row As String = JsonConvert.SerializeObject(jo)
        If Not final Then
            row = row & ","
        End If
        AddRow(row)

    End Sub
    Protected Overrides Sub Epilogue()

        AddRow("]}")
        FlushRaw("body")

    End Sub
End Class
