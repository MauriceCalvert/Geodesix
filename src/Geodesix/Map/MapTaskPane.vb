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
Imports System.Windows.Forms
Imports AddinExpress.XL
Imports Seting
Imports Utilities
Public Class MapTaskPane
    Inherits ADXExcelTaskPane

    Private ReadOnly Property GeodesixEXL As GeodesiXEXL
    Private ReadOnly Property Excel As Object
    Public ReadOnly Property Maps As New ConcurrentDictionary(Of Guid, Map)
    Public Sub New()
        MyBase.New()

        InitializeComponent()

        Enabled = True
        GeodesixEXL = DirectCast(AddinExpress.MSO.ADXAddinModule.CurrentInstance, GeodesiXEXL)
        Excel = GeodesixEXL.HostApplication

        _ID = Guid.NewGuid.ToString
        CustomLog.Logger.Debug($"MapTaskPane {ID} New")

    End Sub
    Public ReadOnly Property ID As String
    Private Function isDisplaying() As Boolean

        If Excel.ActiveSheet Is Nothing Then ' Excel hasn't finished opening the workbook
            Return False
        End If

        Dim wsp As New WorksheetProperties(Excel.ActiveSheet)
        Return wsp.Display

    End Function
    Private Sub MapPane_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles Me.ADXBeforeTaskPaneShow

        GeodesixEXL.MapTaskPane = Me

        If isDisplaying() Then
            ShowMap()
            Show()
        Else
            Hide()
        End If

    End Sub
    Private Sub Map_StyleChanged(sender As Map, mapstyle As String)

        Dim wsp As New WorksheetProperties(Excel.ActiveSheet)
        wsp.MapStyle = mapstyle

    End Sub
    Private Sub Map_VisibilityChanged(sender As Map, visible As Boolean)

        Dim wsp As New WorksheetProperties(Excel.ActiveSheet)
        wsp.Display = visible ' remember this new visibility

        If visible Then
            ShowMap()
            Show()
        Else
            Hide()
        End If

    End Sub
    Public Function SheetMap() As Map

        Dim map As Map = Nothing
        Dim wsp As New WorksheetProperties(Excel.ActiveSheet)

        If Not Maps.TryGetValue(wsp.ID, map) Then

            map = New Map(wsp)
            map.Display = wsp.Display

            AddHandler map.visibilityChanged, AddressOf Map_VisibilityChanged
            AddHandler map.mapStyleChanged, AddressOf Map_StyleChanged

            Maps.TryAdd(map.ID, map)

        End If

        Return map

    End Function
    Private Sub ShowMap()

        Dim map As Map = SheetMap()

        ' If a map is displaying, hide it so that it doesn't register resize events
        If Me.Controls.Count > 0 Then
            Dim current As Map = Controls(0)
            current.Visible = False
            If Me.Controls(0) IsNot map Then
                Me.Controls.Clear()
            End If
        End If

        If Me.Controls.Count = 0 Then ' Display a new/different map

            Me.Controls.Add(map)
            map.Dock = DockStyle.Fill
            map.AutoSize = True

        End If

        ' Show() ' Make the taskpane visible, but the map yet

        ' Reinstate the taskpane's position and size
        Dim wsp As WorksheetProperties = map.WorksheetProperties

        GeodesixEXL.TaskPanesManager.Items(0).Position = wsp.TaskPanePosition

        Dim height As Integer = wsp.PaneHeight
        If height > 0 Then
            Me.Height = height
        End If
        Dim width As Integer = wsp.PaneWidth
        If width > 0 Then
            Me.Width = width
        End If

        If map.URL = "" Then

            Dim sheetname As String = ""

            If GetHeaderRow(Excel.ActiveSheet) > 0 Then
                sheetname = Excel.ActiveSheet.name
            End If

            map.SetSheet(sheetname)

        End If
        map.MapStyle = wsp.MapStyle
        map.Zoom = Math.Max(wsp.Zoom, CInt(Settings.StartZoom))

        ' and only make it visible once we've resized the pane
        map.Visible = True

    End Sub
    Public Sub ReGenerate()

        For Each map As Map In Maps.Values
            map.URL = ""
        Next
        ShowMap()

    End Sub
    Public Sub Switch()

        Dim map As Map = SheetMap()

        If map.Display Then
            ShowMap()
            Show()
        Else
            Hide()
        End If

    End Sub

End Class