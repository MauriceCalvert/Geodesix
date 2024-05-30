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
Imports System.Reflection
Imports Utilities
Public Class GlobalCache

    Public ReadOnly Property Hits As Integer
    Public ReadOnly Property Misses As Integer
    Private Property Excel As Object
    Public Property Root As QueryCache
    Public Property WorkbookCache As New ConcurrentDictionary(Of String, QueryCache)
    Private Property TravellingSalesmanSolvers As New Dictionary(Of String, TravellingSalesmanSolver)

    Private Const BindingsGet As BindingFlags = BindingFlags.GetProperty Or BindingFlags.[Default]
    Private Const BindingsSet As BindingFlags = BindingFlags.SetProperty Or BindingFlags.[Default]
    Private Const BindingsInvoke As BindingFlags = BindingFlags.InvokeMethod Or BindingFlags.[Default]

    Public Sub New(excel As Object)

        Me.Excel = excel
        Root = New QueryCache(excel, "Global")

    End Sub
    Public Sub Close(workbook As Object)

        If HasGuid(workbook) Then ' Don't change the workbook if we haven't done anything
            Dim junk As QueryCache = Nothing
            WorkbookCache.TryRemove(GetGuid(workbook), junk)
        End If

    End Sub
    Public ReadOnly Property GeoFields As List(Of String)
        Get
            Return GoogleFields.
                   Concat(
                       Root.
                       Queries.
                       SelectMany(Of String)(Function(q) q.Fields.Keys)).
                   Distinct.
                   OrderBy(Function(q) q).
                   ToList
        End Get
    End Property
    Public Function GetCache(workbook As Object) As QueryCache

        If workbook Is Nothing Then
            Return Root
        End If
        Dim guids As String = GetGuid(workbook)
        Dim result As QueryCache = WorkbookCache(guids)
        Return result

    End Function
    Private Function GetGuid(workbook As Object) As String

        Dim wbp As New WorkbookProperties(workbook)
        Dim guids As String = wbp("Guid")

        If guids = "" Then
            guids = Guid.NewGuid.ToString
            wbp("Guid") = guids
            CustomLog.Logger.Debug("New Guid {0} for {1}", guids, workbook.Name)
        End If

        If WorkbookCache.ContainsKey(guids) Then
            Return guids
        End If

        CustomLog.Logger.Debug("Creating cache for {0} {1}", workbook.Name, guids)
        Dim querycache As New QueryCache(Excel, guids)

        If Not WorkbookCache.TryAdd(guids, querycache) Then
            Throw New GeodesixException($"Failed to add cache for {workbook.name}")
        End If

        Return guids

    End Function
    Private Function HasGuid(workbook As Object) As Boolean

        Dim wbp As New WorkbookProperties(workbook)
        Return Not IsEmpty(wbp("Guid"))

    End Function
    Public Function MakeQuery(Of T As {New, Query})(key As String) As Query

        Dim result As Query = Nothing

        If TryGetQuery(Of T)(key, result, False) Then
            Return result ' Was loaded into cache while we were busy
        End If

        result = Root.GetQuery(Of T)(key)
        Dim local As QueryCache = GetCache(Excel.ActiveWorkbook)
        local.TryAdd(result)

        Return result

    End Function
    Public Sub Purge()

        Cache = New GlobalCache(Excel)

    End Sub
    Public ReadOnly Property Queries As IEnumerable(Of Query)
        Get
            Return Root.Queries
        End Get
    End Property
    Public Function TryGetQuery(Of T As {New, Query})(key As String, ByRef query As Query, Optional counthits As Boolean = True) As Boolean

        Dim result As Boolean = Root.TryGetQuery(key, query)

        If counthits Then
            If result Then
                _Hits += 1
            Else
                _Misses += 1
            End If
        End If

        Return result

    End Function
    Public ReadOnly Property TSS() As TravellingSalesmanSolver
        Get
            Dim cps As New WorksheetProperties(Excel.ActiveSheet)
            Dim wsguid As String = cps.ID.ToString

            If Not TravellingSalesmanSolvers.ContainsKey(wsguid) Then
                TravellingSalesmanSolvers.Add(wsguid, New TravellingSalesmanSolver)
            End If
            Return TravellingSalesmanSolvers(wsguid)
        End Get
    End Property

End Class
