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
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms
Imports AddinExpress.XL
Imports NUglify.JavaScript.Syntax
Imports Utilities
Partial Public Class Map
    Private Sub AfterNavigated(a As Action)
        If PostNavigated Is Nothing Then
            PostNavigated = New List(Of Action)
        End If
        PostNavigated.Add(Sub()
                              Try
                                  a()
                              Catch ex As Exception
                                  HandleError("AfterNavigated", ex)
                              End Try
                          End Sub)
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            CoreWebView2.GoBack()
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnCloseAddressBar_Click(sender As Object, e As EventArgs) Handles btnCloseAddressBar.Click

        Mode = "Map"

    End Sub
    Private Sub btnCloseMap_Click(sender As Object, e As EventArgs) Handles btnCloseMap.Click

        Me.Display = False

    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            Dim screenShot As Bitmap = Nothing
            Dim graphics As Graphics
            Dim tlc As Point

            Dim size As New Size(Bounds.Width, Bounds.Height - MapToolstrip.Height)
            screenShot = New Bitmap(size.Width, size.Height)
            graphics = Graphics.FromImage(screenShot)

            tlc = PointToScreen(New Point(Left, Top + MapToolstrip.Height))

            graphics.CopyFromScreen(tlc.X, tlc.Y, 0, 0, size, CopyPixelOperation.SourceCopy)

            Clipboard.SetImage(screenShot)

            RunScript("drawPopup", "Map image copied to clipboard")

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Try
            SubmitQuery(txtFind.Text)
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnForward_Click(sender As Object, e As EventArgs) Handles btnForward.Click
        Try
            CoreWebView2.GoForward()
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnHybrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHybrid.Click
        Try
            MapStyle = "hybrid"
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnMinimal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMinimal.Click
        Try
            MapStyle = "minimal"
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Friend Sub btnQuadrants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuadrants.Click
        Try
            If QuadrantsForm Is Nothing Then

                Dim current As String = GeodesiXEXL.TaskPanesManager.Items(0).Position.ToString()

                current = current.Substring(current.LastIndexOf(".") + 1)
                QuadrantsForm = New QuadrantSelector(current)
                Dim screenpos As Point = MapTaskPane.Location
                Dim rect As Rectangle = Me.Bounds
                screenpos.Y = screenpos.Y + btnQuadrants.Height - 2
                screenpos.X = screenpos.X + rect.Width - (QuadrantsForm.Width + btnQuadrants.Width) - 4
                QuadrantsForm.Left = screenpos.X
                QuadrantsForm.Top = screenpos.Y
                QuadrantsForm.StartPosition = FormStartPosition.Manual
                AddHandler QuadrantsForm.Chosen, AddressOf QuadrantChosen
                QuadrantsForm.Show()
            Else
                RemoveHandler QuadrantsForm.Chosen, AddressOf QuadrantChosen
                QuadrantsForm.Close()
                QuadrantsForm = Nothing
            End If
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub
    Public Sub QuadrantChosen(ByVal chosen As String)

        Dim taskpane As AddinExpress.XL.ADXExcelTaskPanesCollectionItem = GeodesiXEXL.TaskPanesManager.Items(0)
        Dim current As String = GeodesiXEXL.TaskPanesManager.Items(0).Position.ToString()
        chosen = chosen & " "

        Select Case chosen.ToLower.Substring(0, 1)

            Case "t"
                GeodesiXEXL.TaskPanesManager.Items(0).Position = ADXExcelTaskPanePosition.Top
                TaskPanePosition = ADXExcelTaskPanePosition.Top

            Case "b"
                GeodesiXEXL.TaskPanesManager.Items(0).Position = ADXExcelTaskPanePosition.Bottom
                TaskPanePosition = ADXExcelTaskPanePosition.Bottom

            Case "l"
                GeodesiXEXL.TaskPanesManager.Items(0).Position = ADXExcelTaskPanePosition.Left
                TaskPanePosition = ADXExcelTaskPanePosition.Left

            Case Else
                GeodesiXEXL.TaskPanesManager.Items(0).Position = ADXExcelTaskPanePosition.Right
                TaskPanePosition = ADXExcelTaskPanePosition.Right

        End Select

        If QuadrantsForm IsNot Nothing Then
            RemoveHandler QuadrantsForm.Chosen, AddressOf QuadrantChosen
            QuadrantsForm.Close()
            QuadrantsForm = Nothing
        End If

        Me.Display = False ' Poke to make it redraw in the new quadrant
        Me.Display = True
        WebView.Refresh()

    End Sub
    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click, btnRefreshMapSheet.Click
        Try
            Reload()
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnRoadmap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRoadmap.Click
        Try
            MapStyle = "roadmap"
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnSatellite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSatellite.Click
        Try
            MapStyle = "satellite"
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Try
            CoreWebView2.Stop()
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnTerrain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTerrain.Click
        Try
            MapStyle = "terrain"
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnCloseBrowserBar_Click(sender As Object, e As EventArgs) Handles btnCloseBrowserBar.Click
        Try
            Mode = "Map"

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnShowBrowserBar_Click(sender As Object, e As EventArgs) Handles btnShowBrowserBar.Click
        Try
            Mode = "Browser"

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnZoomIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomIn.Click
        Try
            changeZoom(1)
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub btnZoomOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomOut.Click
        Try
            changeZoom(-1)
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub cmbSheets_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSheets.DropDown

        PopulateSheetsDropdown()

    End Sub
    Private Sub cmbSheets_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSheets.DropDownClosed

        Dim worksheet As Object = Nothing

        Try
            SetSheet(CStr(cmbSheets.SelectedItem))

        Catch ex As Exception
            HandleError("Sheet change failed", ex)

        End Try

    End Sub
    Private Sub OnNavigated() Handles Me.finishNavigating
        Try

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub txtAddress_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAddress.KeyDown

        If e.KeyCode = Keys.Return Then
            Navigate(txtAddress.Text)
        End If

    End Sub
    Private Sub txtFind_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFind.KeyUp
        Try
            If e.KeyCode = Keys.Return Then
                SubmitQuery(txtFind.Text)
            End If
        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Sub txtFind_TextChanged(sender As Object, e As EventArgs) Handles txtFind.TextChanged

        txtFind.FitContent

    End Sub
    Private Sub txtFind_Leave(sender As Object, e As EventArgs) Handles txtFind.Leave

        txtFind.SelectionStart = 0
        txtFind.SelectionLength = 0

    End Sub

End Class
