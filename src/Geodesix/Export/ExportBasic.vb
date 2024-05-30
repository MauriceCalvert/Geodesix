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
Imports System.Globalization
Imports Seting
Imports Utilities

Public Class ExportBasic
    Inherits Exporter
    Public Sub New(ByRef excel As Object, sheetname As String, ByRef target As String, template As List(Of String))
        MyBase.New(excel, sheetname, target, template)
    End Sub
    Protected Overrides Sub Prologue()

        Include("geodesix.css")
        Include("geodesix.js")
        Include("initialise.js")

        AddRow($"var apikey = '{Settings.APIKey}';")
        AddRow($"var libraries = '{Settings.Libraries}';")
        AddRow($"var startlat = {Settings.StartLat};")
        AddRow($"var startlong = {Settings.StartLong};")
        AddRow($"var startzoom = {Settings.StartZoom};")
        AddRow($"var mapstyle = '{Settings.MapStyle}';")

        Dim language As String = Settings.Language
        If language = "" Then
            language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName
        End If
        AddRow($"var language = '{language}';")


        Dim region As String = Settings.Region
        If region = "" Then
            region = RegionInfo.CurrentRegion.TwoLetterISORegionName
        End If
        AddRow($"var region = '{region}';")

        FlushRaw("settings")

    End Sub
    Protected Overrides Sub Body(final As Boolean)
    End Sub
    Protected Overrides Sub Epilogue()
    End Sub
End Class
