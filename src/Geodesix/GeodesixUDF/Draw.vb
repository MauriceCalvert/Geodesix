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
Imports Utilities

Partial Public Class GeodesixUDF

    Public Function Draw(Optional method As Object = Nothing,
                         Optional id As Object = Nothing,
                         Optional arg2 As Object = Nothing,
                         Optional arg3 As Object = Nothing,
                         Optional arg4 As Object = Nothing,
                         Optional arg5 As Object = Nothing,
                         Optional arg6 As Object = Nothing,
                         Optional arg7 As Object = Nothing,
                         Optional arg8 As Object = Nothing,
                         Optional arg9 As Object = Nothing,
                         Optional arga As Object = Nothing,
                         Optional argb As Object = Nothing,
                         Optional argc As Object = Nothing
                        ) As Object

        Dim result As Object = ""
        Try
            Dim map As Map = GeodesiXEXL.MapTaskPane.SheetMap

            method = GetValue(method)

            Dim args As List(Of Object) =
                {id, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arga, argb, argc}.
                Where(Function(q) q IsNot Nothing).
                ToList

            For i As Integer = 0 To args.Count - 1
                args(i) = GetValue(args(i))
                args(i) = Quoted(args(i))
            Next

            Dim js As String = "draw" & method & "(" & String.Join(",", args) & ")"

            map.ExecuteScript(js)

            result = $"{method} {1}"

        Catch ex As Exception
            CustomLog.Logger.Error("draw({0},{1},{2},{3}) failed {4}", method, id, arg2, arg3, ex.Message)
            result = ex.Message

        End Try

        Return result

    End Function
    Structure MapOverlay
        Public map As Map
        Public cell As Object
    End Structure
    Private Overlays As New Dictionary(Of Integer, MapOverlay)
    Private Sub addOverlay(sender As Object, id As Integer, title As String)

        Try
            Dim overlay As MapOverlay = Overlays(id)
            RemoveHandler overlay.map.addOverlay, AddressOf addOverlay
            overlay.cell.Value2 = $"{id} {title}"
            Overlays.Remove(id)

        Catch ex As Exception
            CustomLog.Logger.Error("addOverlay callback failed {0}", ex.Message)
        End Try

    End Sub

End Class
