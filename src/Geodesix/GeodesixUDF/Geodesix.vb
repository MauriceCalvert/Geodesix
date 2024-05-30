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
Imports Seting
Imports Utilities

Partial Public Class GeodesixUDF

    Public Function Geodesix(Optional request As Object = "missing",
                             Optional arg1 As Object = "",
                             Optional arg2 As Object = "") As Object

        request = GetValueString(request)
        Dim arg1s As String = GetValueString(arg1).ToString
        Dim arg2s As String = GetValueString(arg2).ToString

        CustomLog.Logger.Debug("Geodesix({0}({1},{2}))", request, arg1s, arg2s)

        Dim result As Object = ""
        Try
            Dim map As Map = GeodesiXEXL.MapTaskPane?.SheetMap

            Select Case request

                Case "clicked"
                    result = map?.ClickLocation

                Case "default"
                    map?.ShowDefaultGeocoder()

                Case "help"
                    GeodesiXEXL.ShowHelp(arg1s)

                Case "language"
                    If Not IsEmpty(arg1s) Then
                        GeodesiXEXL.Language = arg1s
                    End If
                    result = Settings.Language

                Case "mode"
                    Validate("Mode", arg1s, {"", "Hidden", "Map", "Browser"})
                    If Not IsEmpty(arg1s) Then
                        If map IsNot Nothing Then
                            map.Mode = arg1s
                        End If
                    End If
                    result = map?.Mode

                Case "navigate"
                    IfNotEmpty(arg1s, Sub() map?.Navigate(arg1s))
                    result = map?.URL

                Case "overlay"
                    If Not IsEmpty(arg1s) Then
                        Dim v As String = ""
                        ValidateInteger("ID", arg1s, 0)
                        If arg2s <> "" Then
                            Validate("visible", arg2s, {"", "true", "false"})
                            v = arg2s
                        End If
                        result = map?.Overlay(CInt(arg1s), v)
                    End If

                Case "position"
                    Dim taskpane As AddinExpress.XL.ADXExcelTaskPanesCollectionItem = GeodesiXEXL.TaskPanesManager.Items(0)

                    If Not IsEmpty(arg1s) Then
                        Validate("position", arg1s, {"", "b", "l", "r", "t"})
                        map?.QuadrantChosen(arg1s)
                    End If
                    result = taskpane.Position.ToString.ToLower

                Case "programfolder"
                    result = GetExecutingPath()

                Case "regextimeout"
                    If Not IsEmpty(arg1s) Then
                        ValidateInteger("regextimeout", arg1s, arg1s, 10, 10000)
                        Settings.RegexTimeout = CInt(arg1s)
                    End If
                    result = Settings.RegexTimeout

                Case "region"
                    IfNotEmpty(arg1s, Sub() GeodesiXEXL.Region = arg1s)
                    result = Settings.Region

                Case "ribbon"
                    GeodesiXEXL.RibbonUI.ActivateTab(GeodesiXEXL.GeodesiXRibbonTab.Id)

                Case "setting"
                    If Not IsEmpty(arg1s) Then
                        Dim setting As Setting = Nothing
                        If Not Settings.TryGetSetting(arg1s, setting) Then
                            result = $"No such setting {arg1s}"
                        Else
                            If arg2s <> "" Then
                                setting.Value = arg2s
                            End If
                            result = setting.Value
                        End If
                    End If

                Case "showlocation"
                    IfNotEmpty(arg1s, Sub() map?.ShowLocation(arg1s))

                Case "showsheet"
                    IfNotEmpty(arg1s, Sub() map?.SetSheet(arg1s))

                Case "url"
                    result = map?.URL

                Case "zoomToContent"
                    map?.zoomToContent()

                Case Else
                    result = $"! '{request}' ???"

            End Select

        Catch ex As Exception
            CustomLog.Logger.Error("Geodesix({0}({1},{2}) failed {3}", request, arg1s, arg2s, ex.Message)
            result = "! " & ex.Message

        End Try

        Return result

    End Function
End Class
