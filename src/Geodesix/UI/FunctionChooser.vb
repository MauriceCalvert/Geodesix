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
Imports System.Linq
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ExcelFormulaParser
Imports Utilities

<ComVisible(False)>
Public Class FunctionChooser

    Friend Structure Row
        Public Property FunctionName As String
        Public Property FunctionDescription As String
        Public Property Method As Method
    End Structure
    Private Property GeodesixEXL As GeodesiXEXL
    Private Property Excel As Object
    Private ReadOnly Property Cell As Object
    Private ReadOnly Property Methods As Methods
    Public Sub New(geodesixexl As GeodesiXEXL, cell As Object)

        InitializeComponent()

        Me.GeodesixEXL = geodesixexl
        Me.Excel = geodesixexl.Excel
        Me.Cell = cell
        Methods = New ExcelFunctions(Excel)

    End Sub
    Private Sub FunctionChooser_Load(sender As Object, e As EventArgs) Handles Me.Load

        LoadMethods(Methods)

    End Sub
    Private Sub Help_Requested(sender As Object, hlpevent As HelpEventArgs) Handles Me.HelpRequested

        Try
            hlpevent.Handled = True
            GeodesixEXL?.ShowHelp("functionchooser.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub Help_ButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked

        Try
            e.Cancel = True
            GeodesixEXL?.ShowHelp("functionchooser.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub LoadMethods(methods As Methods)

        _Methods = methods

        Try
            Dim formula As String = Cell.Formula.ToString ' This is culture-invariant. i.e. with separator ","

            If formula.BeginsWith("=") Then

                Dim excelFormula As New ExcelFormula(formula)

                Dim funcname As String = excelFormula(0).Value

                Dim method As Method = methods.Functions.Where(Function(q) q.Name = funcname).SingleOrDefault

                If method Is Nothing Then
                    Me.Close()
                    ShowWarning($"{funcname}() is not a Geodesix function")
                    Exit Sub
                End If

                If method.SubMethods IsNot Nothing Then

                    Dim submethod As Method = method.
                                              SubMethods.
                                              Functions.
                                              Where(Function(q) q.Name = excelFormula(1).Value).
                                              SingleOrDefault

                    If submethod Is Nothing Then
                        Me.Close()
                        ShowWarning($"{funcname}(""{excelFormula(1).Value}"") is not a Geodesix function")
                        Exit Sub
                    End If
                    _Methods = method.SubMethods
                    OpenBuilder(submethod)
                    Exit Sub
                Else
                    OpenBuilder(method)
                    Exit Sub
                End If


            ElseIf Cell.value2 IsNot Nothing Then

                Dim value As String = Cell.Value2.ToString

                If formula = value Then ' a constant
                    Me.Close()
                    ShowWarning("Cell must be empty or a Geodesix formula")
                    Exit Sub
                End If
            End If

            Dim functions As List(Of Method) = methods.Functions
            Dim rows As List(Of Row) =
            functions.Select(Function(q)
                                 Return New Row With {
                                    .FunctionName = q.Name,
                                    .FunctionDescription = q.Description,
                                    .Method = q
                                 }
                             End Function
                            ).ToList

            Dim bl As New BindingList(Of Row)(rows)

            DataGridView1.AutoGenerateColumns = False
            DataGridView1.DataSource = bl

            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim btnindex As Integer = DataGridView1.Columns("btnselect").Index
                row.Cells(btnindex).Value = row.DataBoundItem.functionname
            Next

            Me.ClientSize = New Drawing.Size(
                Min(800, DataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.None) + 2),
                Min(800, DataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.None) + 2)
            )

        Catch ex As Exception
            HandleError("Loading functions", ex)
        End Try

    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Try
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim method As Method = DirectCast(row.DataBoundItem.method, Method)

            If method.SubMethods IsNot Nothing Then
                LoadMethods(method.SubMethods)
            Else
                OpenBuilder(method)
            End If

        Catch ex As Exception
            HandleError("Loading functions", ex)
        End Try

    End Sub
    Private Sub OpenBuilder(method As Method)

        Try
            Dim fb As New FunctionBuilder(GeodesixEXL, Cell, Methods, method)
            fb.StartPosition = FormStartPosition.Manual
            fb.Location = New Drawing.Point(Location.X + (Width - fb.Width) \ 2, Location.Y + (Height - fb.Height) \ 2)
            Me.Close()
            fb.Show()
            fb.BeginInvoke(Sub() fb.BringToFront())

        Catch ex As Exception
            HandleError("Loading functions", ex)
        End Try

    End Sub

End Class
