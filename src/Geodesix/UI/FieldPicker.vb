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
Imports System.Linq
Imports System.Windows.Forms
Imports Geodesics
Imports Utilities
Public Class FieldPicker
    Private Property GeodesixEXL As GeodesiXEXL
    Private Property Excel As Object
    Private Cell As Object
    Private Property DrawingFields As New List(Of String) From {
        "align",
        "arrow",
        "arrowColor",
        "arrowSize",
        "distance",
        "icon",
        "iconColor",
        "iconSize",
        "label",
        "lineTitle",
        "strokeColor",
        "strokeOpacity",
        "strokeWeight",
        "symbols",
        "title"
    }
    Public Sub New(geodesixexl As GeodesiXEXL)

        InitializeComponent()

        Me.GeodesixEXL = geodesixexl
        Me.Excel = geodesixexl.Excel
        Me.Cell = Excel.ActiveCell

    End Sub
    Private Sub FieldPicker_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim bs1 As New BindingSource
        bs1.DataSource = Singleton.Cache.GeoFields.Select(Function(q) New With {.GoogleField = q})
        dgvGoogleFields.AutoGenerateColumns = False
        dgvGoogleFields.DataSource = bs1

        Dim bs2 As New BindingSource
        bs2.DataSource = DrawingFields.Select(Function(q) New With {.DrawingField = q})
        dgvDrawingFields.AutoGenerateColumns = False
        dgvDrawingFields.DataSource = bs2

        FieldPicker_ResizeEnd(Me, New EventArgs)

    End Sub
    Private Sub dgvDrawingFields_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDrawingFields.CellDoubleClick

        If Not IsEmpty(Cell.Value) Then
            If Not AreYouSure($"Replace value in {Cell.Address}?") Then
                Exit Sub
            End If
        End If

        Dim value As String = dgvDrawingFields.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString
        SetCell(value)

    End Sub
    Private Sub SetCell(value As String)

        Cell.Value = value
        Cell = Excel.ActiveSheet.Cells(Cell.Row, Cell.Column + 1)
        Excel.ActiveSheet.Cells(Cell.Row, Cell.Column).Select

    End Sub
    Private Sub dgvGoogleFields_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvGoogleFields.CellDoubleClick

        If Not IsEmpty(Cell.Value) Then
            If Not AreYouSure($"Replace value in {Cell.Address}?") Then
                Exit Sub
            End If
        End If

        Dim value As String = dgvGoogleFields.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString
        SetCell(value)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

        Close()

    End Sub
    Private Sub FieldPicker_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd

        SplitContainer1.SplitterDistance = ClientSize.Width \ 2
        dgvGoogleFields.Columns(0).Width = dgvGoogleFields.Width - 4
        dgvDrawingFields.Columns(0).Width = dgvDrawingFields.Width - 4
        Button1.Left = ClientSize.Width \ 2 - Button1.Width \ 2
        Button1.Top = ClientSize.Height - Button1.Height - 3

    End Sub
End Class