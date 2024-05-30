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
Imports Geodesics
Imports Utilities

Partial Public Class GeodesixUDF
    Public Function Travel(ByVal request As Object, ByVal origin As Object, ByVal destination As Object, ByVal mode As Object) As Object

        request = GetValueString(request)
        origin = GetValueString(origin)
        destination = GetValueString(destination)
        mode = GetValueString(mode)

        Dim workbook As Object = ActiveWorkbook
        Dim result As Object = ""

        Try
            request = request.ToLower
            If request <> "distance" AndAlso request <> "duration" AndAlso request <> "status" Then
                Return $"!{request}?"
            End If

            mode = mode.ToUpper
            Select Case mode
                Case "TRANSIT"
                Case "WALKING"
                Case "BICYCLING"
                Case "DRIVING"
                Case Else
                    Return $"!{mode}?"
            End Select

            Dim key As String = TravelKey(origin, destination, mode)

            If request = "" OrElse key = "||" Then
                result = "" ' Happens whilst Excel is prompting for a formula

            Else
                Dim query As Query = Nothing

                If Cache.TryGetQuery(Of TravelQuery)(key, query) AndAlso query.Completed Then

                    result = Traveled(query, request, origin, destination, mode)
                Else
                    query = Cache.MakeQuery(Of TravelQuery)(key)
                    ' #18#
                    ' In excel: =RTD(GEODESIX_RTD,,"directions", "geneva", "Lausanne", "DRIVING")
                    result = Excel.WorksheetFunction.rtd(
                                GEODESIX_RTD,
                                Nothing,
                                "directions",
                                request,
                                origin,
                                destination,
                                mode)
                End If
                If result Is Nothing Then
                    result = ""
                End If

                query.Workbook = Excel.ActiveWorkbook.Name
            End If

            CustomLog.Logger.Debug("Travel({0},{1},{2},{3})={4}", request, origin, destination, mode, TextSample(result.ToString))

        Catch ex As Exception
            CustomLog.Logger.Error("Travel({0},{1},{2},{3}) failed {4}", request, origin, destination, mode, ex.Message)
            result = ex.Message

        End Try

        Return result

    End Function

End Class
