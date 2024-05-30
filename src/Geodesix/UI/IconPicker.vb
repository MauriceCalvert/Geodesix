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
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Utilities
Public Class IconPicker

    Private Icons As New List(Of PictureBox)
    Private Cell As Object
    Private Property GeodesixEXL As GeodesiXEXL
    Private Property Excel As Object

    Public Sub New(geodesixexl As GeodesiXEXL, cell As Object)

        InitializeComponent()
        Me.GeodesixEXL = geodesixexl
        Me.Excel = geodesixexl.Excel
        Me.Cell = cell

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim sr As New StreamReader(Path.Combine(GetExecutingPath, "icons", "_googleicons.csv"))

        Do While Not sr.EndOfStream

            Dim s As String = sr.ReadLine
            Dim f As String() = s.Split(","c)
            Dim name As String = f(0)
            Dim url As String = f(1)
            Dim src As String = Path.Combine(GetExecutingPath, "icons", $"{name}.png")
            If Not File.Exists(src) Then
                Continue Do
            End If

            Dim size As Size = Image.FromFile(src).Size
            Dim pb As New PictureBox
            With pb
                .Name = name
                .Tag = url
                .ImageLocation = src
                .Size = size
            End With
            Icons.Add(pb)
        Loop

        Filter()

    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        cmbSize.SelectedItem = "All"
        txtSearch.Text = ""

    End Sub
    Private Sub cmbSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSize.SelectedIndexChanged

        Filter()

    End Sub
    Private Sub Filter()

        Const pad As Integer = 5

        Panel1.Visible = False
        For Each ctl As PictureBox In Panel1.Controls.OfType(Of PictureBox)
            RemoveHandler ctl.MouseDown, AddressOf PictureBox1_MouseDoubleClick
        Next
        Panel1.Controls.Clear()

        Dim size As Integer
        If Not Integer.TryParse(cmbSize.Text, size) Then
            size = 64
        End If
        Dim spacing As Integer = size + pad
        Dim search As String = txtSearch.Text

        Dim x As Integer = pad
        Dim y As Integer = pad

        For Each pb As PictureBox In Icons

            Dim add As Boolean = (size = 0 OrElse pb.Size.Width = size) AndAlso
                                 (IsEmpty(search) OrElse
                                  pb.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
            If add Then

                Panel1.Controls.Add(pb)
                With pb
                    .Left = x
                    .Top = y
                    .Visible = True
                End With

                ToolTip1.SetToolTip(pb, $"{pb.Name} {pb.Size.Width}x{pb.Size.Height} {pb.Tag.ToString}")

                AddHandler pb.MouseDoubleClick, AddressOf PictureBox1_MouseDoubleClick

                x += spacing
                If x + spacing > ClientSize.Width Then
                    y += spacing
                    x = pad
                End If
            End If
        Next
        Panel1.Visible = True
    End Sub
    Private Sub Help_Requested(sender As Object, hlpevent As HelpEventArgs) Handles Me.HelpRequested

        Try
            hlpevent.Handled = True
            GeodesixEXL?.ShowHelp("iconpicker.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub Help_ButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked

        Try
            e.Cancel = True
            GeodesixEXL?.ShowHelp("iconpicker.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub IconPicker_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd

        Filter()

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim psi As New ProcessStartInfo(LinkLabel1.Text)
        Process.Start(psi)
        Close()
    End Sub
    Private Sub PictureBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs)

        Dim pb As PictureBox = DirectCast(sender, PictureBox)
        Cell.Value = pb.Tag.ToString
        Close()

    End Sub
    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress

        Filter()

    End Sub
End Class
