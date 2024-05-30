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
Imports System.Reflection
Imports Geodesics
Imports Utilities
Partial Public Class GeodesixUDF
    Public Function Geocode(ByVal request As Object, Optional ByVal location As Object = "") As Object ' Called by Excel

        Dim result As Object = ""

        ' #15#, #16#
        Try
            request = GetValueString(request).ToLower

            If request = "version" Then
                Dim ass As Assembly = Assembly.GetExecutingAssembly
                Return ass.FullName & " at " & ass.CodeBase
            End If

            location = GetValueString(location)

            If request = "" Then
                result = "" ' Happens whilst Excel is prompting for a formula

            ElseIf location = "" Then
                result = "!Location missing"

            Else
                Dim query As Query = Nothing

                If Cache.TryGetQuery(Of GeoQuery)(location, query) AndAlso query.Completed Then

                    result = Geocoded(query, request, location)
                Else
                    query = Cache.MakeQuery(Of GeoQuery)(location)
                    ' #18#
                    ' In excel: =RTD(GEODESIX_RTD,,"geocode", "status", "Tokyo")
                    result = Excel.WorksheetFunction.RTD(GEODESIX_RTD,
                             Nothing,
                             "geocode",
                             request,
                             location).ToString

                End If
                query.Workbook = Excel?.ActiveWorkbook?.Name
            End If

            CustomLog.Logger.Debug("Geocode({0},{1})={2}", request, location, TextSample(result.ToString))

        Catch ex As Exception
            CustomLog.Logger.Error("Geocode({0},{1}) failed {2}", request, location, ex.Message)
            result = ex.Message

        End Try

        Return result

    End Function

End Class
