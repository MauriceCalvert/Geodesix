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
Imports Utilities

Partial Public Class GeodesixUDF
    Public Function JavaScript(script As Object) As Object

        script = GetValue(script)

        Dim result As Object = ""

        Try
            Dim geodesixexl As GeodesiXEXL = DirectCast(AddinExpress.MSO.ADXAddinModule.CurrentInstance, GeodesiXEXL)
            Dim map As Map = geodesixexl.MapTaskPane.SheetMap
            Dim returnrange As Object = Excel.Caller
            If isExcelErrorCode(returnrange) Then
                Return ExcelErrorCode(returnrange)
            End If
            Dim cells As Integer = returnrange.Cells.Count

            If cells = 1 Then
                map.ExecuteScript(script)

            ElseIf cells = 2 Then

                result = {script, ""}

                map.ExecuteScript(script,
                                  Sub(returned As Object)
                                      result(1) = returned
                                  End Sub)
            Else
                Return "!Result range must be 1 or 2 cells"
            End If

        Catch ex As Exception
            CustomLog.Logger.Error("script({0}) failed {1}", script, ex.Message)
            result = ex.Message

        End Try

        Return result

    End Function
End Class
