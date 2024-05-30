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
Imports Newtonsoft.Json
Imports Utilities
Partial Public Class GlobalCache

    Public Sub Save(workbook As Object)

        Dim oldcalculation As Object = Excel.Calculation
        Try
            Excel.Calculation = -4135 ' xlCalculationManual
            Dim querycache As QueryCache = GetCache(workbook)

            Save(Of GeoQuery)(workbook, querycache, GEOCODECACHEWORKSHEET)
            Save(Of TravelQuery)(workbook, querycache, TRAVELCACHEWORKSHEET)

        Finally
            Excel.Calculation = oldcalculation
        End Try

    End Sub
    Private Sub Save(Of T As Query)(workbook As Object, querycache As QueryCache, cachesheet As String)

        Dim wbname As String = CStr(workbook.Name)

        If querycache.Purged Then
            CustomLog.Logger.Debug("{0} cache purged", wbname)
            DeleteWorksheet(Excel, workbook, cachesheet)
            Exit Sub
        End If

        Dim sw As New Stopwatch
        sw.Start()

        Dim active As List(Of T) =
            querycache.
            Queries.
            OfType(Of T).
            Where(Function(q) q.Status = "OK").
            OrderBy(Function(q) q.Key).
            ToList

        If Not active.Any Then
            CustomLog.Logger.Debug("No active {0} queries in {1}", cachesheet, wbname)
            Exit Sub
        End If

        CustomLog.Logger.Debug("Saving {0} {1}, {2} queries", wbname, cachesheet, active.Count)

        Dim oldsheet As Object = workbook.ActiveSheet
        Dim worksheet As Object = Nothing
        If Not TryGetWorkSheet(workbook, cachesheet, worksheet) Then
            ' -4167 = Excel.XlSheetType.xlWorksheet
            worksheet = workbook.Worksheets.Add(Type.Missing, workbook.Worksheets(workbook.Worksheets.Count), 1, -4167)
            worksheet.Name = cachesheet
            ' -1 = Excel.XlSheetVisibility.xlSheetVisible
            ' 0  = Excel.XlSheetVisibility.xlSheetHidden
            ' 2  = Excel.XlSheetVisibility.xlSheetVeryHidden
        End If

#If DEBUG Then
        worksheet.Visible = -1
#Else
        worksheet.Visible = 2
#End If
        oldsheet.Activate

        Dim fields As List(Of String) =
            active.
            SelectMany(Of String)(Function(q) q.Fields.Keys).
            Distinct.
            OrderBy(Function(q) q).
            ToList

        Dim qc As Integer = active.Count + 1 ' 1 for the header row
        Dim fc As Integer = fields.Count + 1 ' 1 for the key column
        Dim values(qc, fc) As Object

        ' Column headers
        values(0, 0) = qc + 1
        values(0, 1) = "status"
        Dim col As Integer = 2
        For Each fieldname As String In fields
            values(0, col) = fieldname
            col += 1
        Next

        ' Query rows
        Dim row As Integer = 1

        For Each query As Query In active.OrderBy(Function(q) q.Key)

            values(row, 0) = query.Key
            values(row, 1) = query.Status
            col = 2
            For Each fieldname As String In fields

                If fieldname = "status" Then
                    Continue For
                End If

                Dim v As Object = Nothing

                If query.Fields.TryGetValue(fieldname, v) Then

                    If GetTypeName(v).BeginsWith("List<") Then ' Looks like JSON
                        v = JsonConvert.SerializeObject(v)
                    End If
                    IfNotEmpty(v.ToString, Sub() values(row, col) = v)
                End If
                col += 1
            Next
            row += 1
        Next

        worksheet.Cells.ClearContents

        For r As Integer = 0 To values.GetUpperBound(0)
            For c As Integer = 0 To values.GetUpperBound(1)
                worksheet.cells(r + 1, c + 1).Value = values(r, c)
            Next
        Next

        sw.Stop()
        CustomLog.Logger.Debug("{0} saved {1} queries with {2} fields in {3:#,##0} mS", wbname, qc - 1, fc - 2, sw.ElapsedMilliseconds)
    End Sub

End Class
