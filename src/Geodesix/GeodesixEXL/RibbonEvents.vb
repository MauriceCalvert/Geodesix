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
Imports AddinExpress.MSO
Imports Geodesics
Imports Utilities

Partial Public Class GeodesiXEXL
    Private Sub RibbonViewMap_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonViewMap.OnClick
        ViewMap()
    End Sub

    Private Sub RibbonViewBrowser_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonViewBrowser.OnClick
        Browser()
    End Sub

    Private Sub RibbonViewEarth_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonViewEarth.OnClick
        Earth()
    End Sub

    Private Sub RibbonViewTSS_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonViewTSS.OnClick
        TSS()
    End Sub

    Private Sub RibbonImportStructured_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonImportStructured.OnClick
        ImportStructured()
    End Sub

    Private Sub RibbonExportGeoJSON_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonExportGeoJSON.OnClick
        ExportGEOJson()
    End Sub

    Private Sub RibbonExportHTML_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonExportHTML.OnClick
        ExportHTML()
    End Sub

    Private Sub RibbonExportKML_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonExportKML.OnClick
        ExportKML()
    End Sub

    Private Sub RibbonOptions_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonOptions.OnClick
        Options()
    End Sub

    Private Sub RibbonHelp_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonHelp.OnClick
        ShowHelp("index.html")
    End Sub
    Private Sub RibbonDeveloperTools_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonDeveloperTools.OnClick
        MapTaskPane.SheetMap.OpenDevTools()
    End Sub

    Private Sub RibbonShowSource_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonShowSource.OnClick
        Dim source As String = MapTaskPane.SheetMap.URL
        Try
            Shell("C:\Program Files (x86)\Notepad++\Notepad++.exe", source)
        Catch ex As Exception
            Shell("Notepad.exe", source)
        End Try
    End Sub

    Private Sub RibbonSettings_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonSettings.OnClick
        ChangeSettings()
    End Sub

    Private Sub RibbonShowCache_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonShowCache.OnClick
        Dim cd As New CacheDump
        ShowFormDialog(cd)
    End Sub

    Private Sub RibbonPurgeCache_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonPurgeCache.OnClick
        Cache.Purge()
    End Sub

    Private Sub RibbonDrawing_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonDrawing.OnClick
        Drawings()
    End Sub

    Private Sub RibbonExportTabbed_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonExportTabbed.OnClick
        Export_Tabbed()
    End Sub

    Private Sub RibbonInsertFunction_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonInsertFunction.OnClick
        FunctionChooser()
    End Sub

    Private Sub RibbonInsertIcon_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonInsertIcon.OnClick
        PickIcon()
    End Sub

    Private Sub RibbonLocate_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonLocate.OnClick
        ShowOnMap()
    End Sub

    Private Sub RibbonInsertField_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonInsertField.OnClick
        InsertField()
    End Sub

    Private Sub GeodesiXEXL_OnRibbonLoaded(sender As Object, ribbon As IRibbonUI) Handles Me.OnRibbonLoaded
        RibbonUI = ribbon
    End Sub

    Private Sub RibbonExportJSON_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RibbonExportJSON.OnClick
        ExportJson()
    End Sub
End Class
