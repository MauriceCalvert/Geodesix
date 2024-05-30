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
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Geodesics
Imports Google.OpenLocationCode
Imports Seting
Imports Utilities
Partial Public Class GeodesiXEXL

    Private Sub Browser()
        Try
            OpenBrowser("file:///" & MapTaskPane.SheetMap.URL)

        Catch gex As GeodesixException
            ShowBox(gex.Message, "Something needs fixing")

        Catch ex As Exception
            HandleError("Export for browser", ex)
        End Try
    End Sub
    Private Sub ChangeSettings()
        Try
            ShowFormDialog(New SettingsUI(Settings) With {.Text = "General settings"})

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub Drawings()
        Try
            Dim wbp As New WorkbookProperties(Excel.ActiveWorkBook)

            Dim drawing As New DrawingSettings("Geodesix", wbp)
            Dim map As Map = MapTaskPane.SheetMap
            AddHandler drawing.Changed, Sub() map.ReGenerate()

            Dim drawings As New SettingsUI(drawing) With {.Text = "Drawing preferences"}

            ShowFormDialog(drawings)

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Sub Earth()
        Try
            Dim temp As String = GetTempFile("kml")
            Dim exporter As New ExportKML(Excel, Excel.ActiveSheet.Name.ToString, temp,
                                          ReadFileToList(Path.Combine(GetExecutingPath, "templates", "earth.kml")))
            exporter.Transform()
            WriteFile(temp, exporter.Results)
            OpenBrowser("file:///" & temp)

        Catch gex As GeodesixException
            ShowBox(gex.Message, "Something needs fixing")

        Catch ex As Exception
            HandleError("Exporting for Earth", ex)

        End Try
    End Sub
    Sub ExportGEOJson()
        Try
            Dim filename As String = ""
            If BrowseForFile(filename, "geojson", "JSON files (*.geojson)|*.geojson",
                             Coalesce(Settings.ExportFile, GetMyDocuments), False) Then

                If File.Exists(filename) Then
                    If ShowBox($"Overwrite {filename}?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                        Exit Sub
                    End If
                End If

                Dim exporter As New ExportGeoJson(Excel, Excel.ActiveSheet.Name.ToString, Path.GetDirectoryName(filename),
                                                  ReadFileToList(Path.Combine(GetExecutingPath, "templates", "geojson.json")))
                exporter.Transform()
                WriteFile(filename, exporter.Results)

                Settings.ExportFile = filename
            End If



        Catch gex As GeodesixException
            ShowBox(gex.Message, "Something needs fixing")

        Catch ex As Exception
            HandleError("Exporting GeoJSON", ex)

        End Try
    End Sub
    Sub ExportHTML()
        Try
            Dim filename As String = ""
            If BrowseForFile(filename, "html", "HTML files (*.htm;*.html)|*.htm*",
                             Coalesce(Settings.ExportFile, GetMyDocuments), False) Then

                If File.Exists(filename) Then
                    If ShowBox($"Overwrite {filename}?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                        Exit Sub
                    End If
                End If

                Dim wsname As String = Excel.ActiveSheet.Name.ToString

                If Not MakeDecorated(Excel, wsname, filename) Then
                    ShowWarning($"Nothing was drawn, worksheet {wsname} should have 'Latitude' and a 'Longitude' fields")
                End If

                Settings.ExportFile = filename
            End If

        Catch gex As GeodesixException
            ShowBox(gex.Message, "Something needs fixing")

        Catch ex As Exception
            HandleError("Exporting HTML", ex)

        End Try
    End Sub
    Sub ExportJson()
        Try
            Dim filename As String = ""
            If BrowseForFile(filename, "json", "JSON files (*.json)|*.json",
                             Coalesce(Settings.ExportFile, GetMyDocuments), False) Then

                If File.Exists(filename) Then
                    If ShowBox($"Overwrite {filename}?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                        Exit Sub
                    End If
                End If

                Dim exporter As New ExportJson(Excel, Excel.ActiveSheet.Name.ToString, Path.GetDirectoryName(filename),
                                                  ReadFileToList(Path.Combine(GetExecutingPath, "templates", "json.json")))
                exporter.Transform()
                WriteFile(filename, exporter.Results)

                Settings.ExportFile = filename
            End If



        Catch gex As GeodesixException
            ShowBox(gex.Message, "Something needs fixing")

        Catch ex As Exception
            HandleError("Exporting GeoJSON", ex)

        End Try
    End Sub
    Sub ExportKML()
        Try
            Dim filename As String = ""
            If BrowseForFile(filename, "kml", "KML files (*.klm)|*.klm",
                             Coalesce(Settings.ExportFile, GetMyDocuments), False) Then

                If File.Exists(filename) Then
                    If ShowBox($"Overwrite {filename}?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                        Exit Sub
                    End If
                End If

                Dim exporter As New ExportKML(Excel, Excel.ActiveSheet.Name.ToString, filename,
                                              ReadFileToList(Path.Combine(GetExecutingPath, "templates", "earth.kml")))
                exporter.Transform()
                WriteFile(filename, exporter.Results)

                Settings.ExportFile = filename
            End If

        Catch gex As GeodesixException
            ShowBox(gex.Message, "Something needs fixing")

        Catch ex As Exception
            HandleError("Exporting KML", ex)

        End Try
    End Sub
    Sub Export_Tabbed()
        Try
            Dim filename As String = ""
            If BrowseForFile(filename, "txt", "Text files (*.txt)|*.txt",
                             Coalesce(Settings.ExportFile, GetMyDocuments), False) Then

                If File.Exists(filename) Then
                    If ShowBox($"Overwrite {filename}?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                        Exit Sub
                    End If
                End If

                Dim sheet As Object = Excel.ActiveSheet
                Dim lur As Integer = LastUsedRow(sheet)
                Dim luc As Integer = LastUsedCol(sheet)

                Using sw As New StreamWriter(filename)

                    For r As Integer = 1 To lur
                        Dim sb As New StringBuilder
                        For c As Integer = 1 To luc
                            sb.Append(GetValue(sheet.cells(r, c)))
                            If c < luc Then
                                sb.Append(Microsoft.VisualBasic.Chr(9))
                            End If
                        Next
                        sw.WriteLine(sb.ToString)
                    Next

                    sw.Close()

                End Using

                Settings.ExportFile = filename
            End If

        Catch gex As GeodesixException
            ShowBox(gex.Message, "Something needs fixing")

        Catch ex As Exception
            HandleError("Exporting KML", ex)

        End Try
    End Sub
    Private Sub FunctionChooser()

        Dim cell As Object = Nothing
        Try
            cell = Excel.ActiveCell
            If cell Is Nothing Then
                Exit Sub
            End If
            Dim ip As New FunctionChooser(Me, cell)
            ShowFormDialog(ip)

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Private Sub ImportStructured()
        Try
            Dim importer As New Importer(Me, "json", "JSON files (*.json)|*.json|XML files (*.xml)|*.xml")
            ShowFormDialog(importer)

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub InsertField()
        Try
            If Excel.ActiveCell Is Nothing Then
                ShowWarning("Select a cell first")
                Exit Sub
            End If
            Dim fp As New FieldPicker(Me)
            ShowFormDialog(fp)

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Sub Options()
        Try
            If ShowFormDialog(New Options(Me)) = DialogResult.OK Then
                Language = Settings.Language
                Region = Settings.Region
                Geodesic.Language = Language
                Geodesic.Region = Region
            End If

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub PickIcon()

        Dim cell As Object = Nothing
        Try
            cell = Excel.ActiveCell
            If cell Is Nothing Then
                Exit Sub
            End If
            Dim ip As New IconPicker(Me, cell)
            ShowFormDialog(ip)

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Function ShowHelp(filename As String, Optional fragment As String = "") As Browser

        Try
            Dim helpfile As String = "file:///" & Path.Combine(GetExecutingPath(), "help", filename)
            Dim browser As New Browser(Excel, Path.Combine(GetExecutingPath(), "help", "index.html"), helpfile, fragment)

            ShowForm(browser)
            Return browser

        Catch ex As Exception
            ShowBox("ShowHelp error: " & ex.Message)
        End Try

        Return Nothing

    End Function
    Sub ShowOnMap()

        ' Right-clicked on cell, show on map

        Dim cell As Object = Excel.ActiveCell

        Try
            If cell Is Nothing Then
                Exit Sub
            End If

            Dim formula As String = cell.Formula.ToString ' This is culture-invariant. i.e. with separator ","
            If cell.value2 Is Nothing Then
                Exit Sub
            End If
            Dim value As String = cell.Value2.ToString

            CustomLog.Logger.Debug("GeodesiXEXL.ShowMap_Click for {0}={1}", formula, value)

            Dim map As GeodesiX.Map = MapTaskPane.SheetMap
            If map Is Nothing Then
                Throw New GeodesixException("There is no active sheet!")
                Return
            End If
            map.Display = True

            Dim funcname As String = "geocode"

            If formula = value Then ' a constant
                Dim lat As Double
                Dim lng As Double
                If TryParseCoordinates(value, lat, lng) Then
                    map.ShowLocation(value)
                    Exit Sub
                End If

                If OpenLocationCode.IsValid(value) Then
                    Dim codearea As CodeArea = OpenLocationCode.Decode(value)
                    map.ShowLocation($"{codearea.CenterLatitude},{codearea.CenterLongitude}")
                    Exit Sub
                End If

                map.ShowLocation(value)

            Else ' ah-ha, a formula

                ' If the cell contains a formula, parse it to see if it is one of *our* formulas

                Try ' so many things can go wrong here...
                    formula = formula.ToLower

                    ' Split into function name and the arguments
                    Dim ref As Match = Regex.Match(formula, "=(.+)\(([^)]*)\)")

                    If ref.Success AndAlso ref.Groups.Count >= 3 Then

                        funcname = ref.Groups(1).Value.ToLower

                        If funcname.Contains(".") Then
                            funcname = funcname.Substring(funcname.LastIndexOf(".") + 1)
                        End If
                        Dim args() As Object = ref.Groups(2).Value.Split(","c)
                        ' Convert strings to objects
                        Dim values() As Object = ref.Groups(2).Value.Split(","c).
                            Select(Of Object)(Function(q) CObj(q)).ToArray

                        For i As Integer = 0 To args.Length - 1
                            Try
                                Dim arg As Object = args(i)
                                Dim val As Object = GetValue(Excel.Evaluate(arg))
                                values(i) = val
                            Catch ex As Exception
                            End Try
                        Next

                        Select Case funcname ' One of *ours* ?

                            Case "Area"
                                Dim xr As New ExcelRectangle(values(0))
                                map.ExecuteScript($"map.fitBounds({{east:{xr.East},north:{xr.North},south:{xr.South},west:{xr.West}}})")
                                Exit Sub

                            Case "azimuth", "distance", "greatcircledistance"
                                map.ShowFlight(CDbl(values(0)), CDbl(values(1)), CDbl(values(2)), CDbl(values(3)))
                                Exit Sub

                            Case "Displace"
                                Dim result As VincentyDirectResult = VincentyDirect(CDbl(values(0)), CDbl(values(1)), CDbl(values(2)), CDbl(values(3)))
                                map.ShowLocation(result.Latitude.ToString & "," & result.Longitude)
                                Exit Sub

                            Case "georeverse"
                                map.ShowLocation(values(1) & "," & values(2))
                                Exit Sub

                            Case "geocode"
                                map.ShowLocation(values.Last)
                                Exit Sub

                            Case "pluscode"
                                Dim codearea As CodeArea = OpenLocationCode.Decode(values(1))
                                map.ShowLocation($"{codearea.CenterLatitude},{codearea.CenterLongitude}")
                                Exit Sub

                            Case "travel"
                                map.showRoute(values(1), values(2), values(3))
                                Exit Sub

                        End Select
                    End If

                Catch ex As Exception
                End Try

                ' Formula didn't work out
                map.ShowLocation(value)
            End If

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Sub TSS()
        Try
            Dim ws As Object = Excel.ActiveSheet
            Dim frmtss As New TravellingSalesmanForm(Me, ws.Name.ToString)
            Dim result As DialogResult = ShowForm(frmtss)

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Sub ViewMap()

        Try
            Dim mtp As MapTaskPane = MapTaskPane()
            Dim crnt As Map = mtp.SheetMap
            crnt.Display = Not crnt.Display

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub
End Class
