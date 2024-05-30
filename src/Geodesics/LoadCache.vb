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
Imports System.Linq
Imports System.Threading.Tasks
Imports Utilities
Partial Public Class GlobalCache

    Public Sub Load(workbook As Object)

        Load(Of GeoQuery)(workbook, GEOCODECACHEWORKSHEET)
        Load(Of TravelQuery)(workbook, TRAVELCACHEWORKSHEET)

    End Sub
    Public Sub Load(Of T As {New, Query})(workbook As Object, cachesheet As String)

        Dim wbname As String = "?"

        Try
#Disable Warning BC42016 ' Implicit conversion
            wbname = workbook.Name
#Enable Warning BC42016 ' Implicit conversion
            Dim worksheet As Object = Nothing
            If Not TryGetWorkSheet(workbook, cachesheet, worksheet) Then
                Exit Sub
            End If

            Dim lur As Integer = LastUsedRow(worksheet)
            Dim luc As Integer = LastUsedCol(worksheet)

            Dim table(,) As Object = CType(worksheet.Range(worksheet.Cells(1, 1), worksheet.Cells(lur, luc)).Cells.Value2, Object(,))

            If table(1, 2).ToString <> "status" Then ' Invalid Cache, delete it
                Throw New Exception("Cell(1, 2).ToString <> 'Status'")
            End If

            CustomLog.Logger.Debug("{0} {1} cache is {2:#,##0} rows {3:#,##0} cols", workbook.Name, GetType(T).Name, lur - 1, luc - 1)

            Dim added As Integer = 0
            Dim local As QueryCache = GetCache(workbook)

            For row As Integer = 2 To lur

                Dim key As String = CStr(table(row, 1))
                If IsEmpty(key) OrElse key = "status" Then
                    Continue For
                End If

                Dim query As Query = Nothing
                Dim status As String = CStr(table(row, 2))
                If status <> "OK" Then
                    Continue For
                End If

                If Cache.Root.TryGetQuery(key, query) Then

                    CustomLog.Logger.Debug("{0} {1} {2} already cached", wbname, cachesheet, query)
                    local.TryAdd(query)
                Else
                    query = New T
                    query.Key = key
                    query.Workbook = wbname
                    local.TryAdd(query)
                    Root.TryAdd(query)
                    added += 1
                End If
            Next

            CustomLog.Logger.Debug("{0} keys added to cache", added)

            LoadQueries(Of T)(local, table, lur, luc, cachesheet)

        Catch ex As Exception
            CustomLog.Logger.Error($"{wbname} {cachesheet} cache invalid, {ex.Message}")
            DeleteWorksheet(Excel, workbook, cachesheet)

        End Try

    End Sub
    Private Sub LoadQueries(Of T As {New, Query})(local As QueryCache, table(,) As Object, lur As Integer, luc As Integer, name As String)

        Dim sw As New Stopwatch
        sw.Start()
        Dim exceptions As New ConcurrentQueue(Of Exception)

        Dim queries As T() = Cache.Queries.OfType(Of T).ToArray
        Dim added As Integer = 0

        Parallel.For(2, lur + 1,
            Sub(row As Integer)

                Dim query As Query = Nothing
                Try
                    Dim key As String = CStr(table(row, 1))

                    If IsEmpty(key) OrElse key = "status" Then
                        Exit Sub
                    End If

                    If Not Cache.TryGetQuery(Of T)(key, query, False) Then
                        Exit Sub
                    End If

                    Dim tags As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

                    For col As Integer = 2 To luc - 1

                        Dim fieldname As String = CStr(table(1, col))
                        If IsEmpty(fieldname) Then
                            Continue For
                        End If

                        Dim tagvalue As String = CStr(table(row, col))
                        If IsEmpty(tagvalue) Then
                            Continue For
                        End If

                        If Not tags.ContainsKey(fieldname) Then
                            tags.Add(fieldname.Trim, tagvalue.Trim)
                        End If
                    Next

                    With query
                        .Status = CStr(table(row, 2))
                        .Fields = tags
                        .Completed = True
                    End With

                    added += 1

                Catch ex As Exception
                    exceptions.Enqueue(ex)

                Finally
                    query?.Semaphore.Set()
                End Try
            End Sub)

        If exceptions.Any Then
            HandleError("Loading cache data", New AggregateException(exceptions))
        End If

        local.Ready = True

        sw.Stop()
        CustomLog.Logger.Debug("Cache {0} is ready, {1:#,##0} queries, {2:#,##0} mS", name, added, sw.ElapsedMilliseconds)
    End Sub

End Class
