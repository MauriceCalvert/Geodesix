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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Geodesics
Imports Utilities
<ComVisible(False)>
Public Class TravellingSalesmanForm
    Private Property TSS As TravellingSalesmanSolver
    Private Property WorkSheet As Object
    Private Property Excel As Object
    Private Property Advanced As Boolean = True
    Private ReadOnly Property GeodesixExl As GeodesiXEXL

    Sub New(geodesixexl As GeodesiXEXL, ByVal worksheetname As String)

        InitializeComponent()

        Dim workbook As Object = Excel.ActiveWorkbook
        Me.GeodesixExl = geodesixexl
        Me.Excel = geodesixexl.Excel
        WorkSheet = Excel.Worksheets(worksheetname)
        TSS = Cache.TSS
        Me.Height = 170
        If TSS.Cities.Count = 0 Then
            txtMessage.Text = "Looks like this worksheet isn't setup correctly for TSS." & vbCRLF &
                "Have you got =TravellingSalesman(name, latitude, longitude) formulas?"
            txtMessage.ForeColor = Color.Red
            btnFindRoute.Enabled = False
            btnAdvanced.Enabled = False
            Exit Sub
        End If
        txtInitCooling.Text = TSS.CoolingRate.ToString
        txtInitTemp.Text = TSS.InitialTemperature.ToString
        txtAbsTemp.Text = TSS.AbsoluteTemperature
        btnOK.Enabled = False
        btnFindRoute.Focus()
    End Sub
    Private Sub btnAdvanced_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdvanced.Click

        If btnAdvanced.Text = "Advanced" Then
            Advanced = True
            Me.Height = 300
            btnAdvanced.Text = "Basic"
        Else
            btnAdvanced.Text = "Advanced"
            Advanced = False
            Me.Height = 170
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub
    Private Sub btnFindRoute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindRoute.Click

        btnOK.Focus()
        Dim previousdistance As Double

        If TSS.Changed Then
            previousdistance = Double.MaxValue
        Else
            previousdistance = TSS.ShortestDistance
        End If
        btnFindRoute.Enabled = False
        btnOK.Enabled = False
        txtMessage.Text = "Preparing..."
        txtMessage.Refresh()
        Cleanup()
        If TSS.Cities.Count < 4 Then
            If TSS.Cities.Count = 0 Then
                txtMessage.Text = "There are no valid cities left on this worksheet."
            Else
                txtMessage.Text = "Less than 4 cities, the solution is obvious."
            End If
            Exit Sub
        End If
        txtMessage.Text = "Solving..."
        txtMessage.Refresh()
        txtDistance.Visible = True
        lblSolution.Visible = True
        lblKilometres.Visible = True
        lblSolution.Text = "best so far"
        lblSolution.Refresh()
        TSS.Solve(Advanced, txtTemp, txtIteration, txtDistance)
        lblSolution.Text = "Solution is"
        lblSolution.Refresh()

        txtMessage.Text = $"{TSS.Iterations:0,000} iterations. "
        btnFindRoute.Enabled = True
        If TSS.ShortestDistance < previousdistance Then
            If previousdistance < Double.MaxValue Then
                txtMessage.Text &= "Improved by " & Math.Round((previousdistance - TSS.ShortestDistance) / 1000, 3) & " Km. "
            Else
                txtMessage.Text &= "First solution. You may click again to try and find a better one. "
            End If
            txtDistance.Text = TSS.ShortestDistance.ToString
        Else
            txtMessage.Text = "New solution no better, discarded. "
        End If
        txtMessage.Text &= "Click Browser or Map to view results."
        btnOK.Enabled = True
        btnOK.Focus()
        Me.AcceptButton = btnOK
        WorkSheet.Calculate ' Make the TSS formulas update
        ShowHide()

    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim frmpos As Object = WorkSheet.Cells.Find("=TravellingSalesman(", WorkSheet.Cells(1, 1), -4123, 2, 1, 1)

        If frmpos Is Nothing Then
            Exit Sub
        End If

        Dim frmcol As Integer = frmpos.Column
        Dim frmrow As Integer = frmpos.Row
        Dim lastrow As Integer = frmrow
        For r As Integer = lastrow To LastUsedRow(WorkSheet)
            If Not WorkSheet.Cells(r, frmcol).Formula.ToString.Contains("=TravellingSalesman(") Then
                Exit For
            End If
            lastrow = r
        Next
        Dim tl As Object = WorkSheet.Cells(frmrow, 1)
        Dim br As Object = WorkSheet.Cells(lastrow, LastUsedCol(WorkSheet))
        Dim range As Object = WorkSheet.Range(tl, br)

        range.Sort(range.Columns(frmcol), 1)

        Me.Close()

    End Sub

    Private Sub Cleanup()

        ' Check that all cities in the list are still there in Excel (and only those)
        Dim delete As New List(Of String)

        For Each p As TravellingSalesmanSolver.Visit In TSS.Cities.Values
            p.seen = True
            Dim values() As Object = GetFormulaValues(Excel, WorkSheet, p.row, p.column)
            If values(0).ToString.ToLower = "travellingsalesman" Then
                If values.GetUpperBound(0) >= 3 Then
                    Dim lat As Double
                    Dim lng As Double
                    Dim havelat As Boolean
                    If TypeOf values(2) Is Double Then
                        lat = DirectCast(values(2), Double)
                        havelat = True
                    Else
                        havelat = Double.TryParse(values(2).ToString, lat)
                    End If
                    Dim havelon As Boolean
                    If TypeOf values(3) Is Double Then
                        lng = DirectCast(values(3), Double)
                        havelon = True
                    Else
                        havelon = Double.TryParse(values(3).ToString, lng)
                    End If
                    If String.Compare(values(1).ToString, p.name, True) = 0 AndAlso
                        havelat AndAlso havelon AndAlso
                        Round(lat, 7) = Round(p.lat, 7) AndAlso Round(lng, 7) = Round(p.lng, 7) Then
                        p.seen = True
                    Else
                        delete.Add(p.name)
                    End If
                End If
            Else ' Formula no longer travellingsalesman
                delete.Add(p.name)
            End If
        Next

        For Each cityname As String In delete
            TSS.RemoveCity(cityname)
        Next

    End Sub
    Private Sub Help_Requested(sender As Object, hlpevent As HelpEventArgs) Handles Me.HelpRequested

        Try
            hlpevent.Handled = True
            GeodesixExl?.ShowHelp("tss.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub Help_ButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked

        Try
            e.Cancel = True
            GeodesixExl?.ShowHelp("tss.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub

    Private Sub TravellingSalesmanSolver_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtFormulas.Text = TSS.Cities.Count.ToString
        ShowHide()
    End Sub

    Private Sub ShowHide()
        txtDistance.SelectionStart = 0
        txtDistance.SelectionLength = 0
        If TSS.ShortestDistance = Double.MaxValue Then
            txtDistance.Visible = False
            lblSolution.Visible = False
            lblKilometres.Visible = False
        Else
            txtDistance.Text = Math.Round(TSS.ShortestDistance / 1000, 3).ToString
            txtDistance.Visible = True
            lblSolution.Visible = True
            lblKilometres.Visible = True
        End If
    End Sub


    Private Sub txtInitCooling_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInitCooling.Validated
        TSS.CoolingRate = CDbl(txtInitCooling.Text)
    End Sub

    Private Sub txtInitCooling_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtInitCooling.Validating
        Dim temp As Double
        If Double.TryParse(txtInitCooling.Text, temp) AndAlso temp > 0 AndAlso temp < 1 Then
        Else
            txtMessage.Text = "Invalid cooling rate, must be 0 < Cooling < 1"
            e.Cancel = True
        End If
    End Sub

    Private Sub txtInitTemp_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInitTemp.Validated
        TSS.InitialTemperature = CDbl(txtInitTemp.Text)
    End Sub

    Private Sub txtInitTemp_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtInitTemp.Validating
        Dim temp As Double
        If Double.TryParse(txtInitTemp.Text, temp) AndAlso temp > 1000 Then
        Else
            txtMessage.Text = "Invalid initial temperature, must be > 1000"
            e.Cancel = True
        End If
    End Sub

    Private Sub txtAbsTemp_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAbsTemp.Validated
        TSS.AbsoluteTemperature = CDbl(txtAbsTemp.Text)
    End Sub

    Private Sub txtAbsTemp_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAbsTemp.Validating
        Dim temp As Double
        If Double.TryParse(txtAbsTemp.Text, temp) AndAlso temp > 0 AndAlso temp <= 1 Then
        Else
            txtMessage.Text = "Invalid absolute temperature, must be 0 < AbsTemp <= 1"
            e.Cancel = True
        End If
    End Sub

End Class