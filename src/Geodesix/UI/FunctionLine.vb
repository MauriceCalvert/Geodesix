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
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Utilities

Public Class FunctionLine

    Public Event ChangedFocus(control As Control)

    Friend WithEvents Parameter As Parameter

    Public Sub New(parameter As Parameter)

        InitializeComponent()

        Me.Parameter = parameter
        txtName.Text = parameter.Name
        txtType.Text = parameter.Type.Name

        If parameter.DefaultValue IsNot Nothing Then
            txtSource.Text = Quoted(parameter.DefaultValue)
            txtValue.Text = txtSource.Text
        End If

        If parameter.Choices IsNot Nothing AndAlso
           (parameter.Choices.Count > 2 OrElse
            Not IsNumeric(parameter.Choices(0))) Then

            Dim combo As New ComboBox
            With combo
                .Name = "cmbSource"
                .Tag = "Source"
                .Left = txtSource.Left
                .Top = txtSource.Top
                .Width = txtSource.Width
                For Each item As Object In parameter.Choices
                    .Items.Add(item)
                Next
                .Visible = True
                If parameter.DefaultValue IsNot Nothing Then
                    .Text = parameter.DefaultValue
                End If
                If Not IsEmpty(parameter.Formula) Then
                    .Text = parameter.Formula
                End If
                Controls.Remove(txtSource)
                Controls.Add(combo)
                AddHandler combo.SelectedValueChanged, AddressOf Combo_Changed
                AddHandler combo.TextChanged, AddressOf Combo_Changed
            End With
        Else
            AddHandler txtSource.TextChanged, AddressOf Text_Changed
        End If

        For Each control As Control In Me.Controls
            AddHandler control.GotFocus, AddressOf Control_GotFocus
            If TypeOf control Is TextBox Then
                control.AutoSize = False
                control.Size = New Size(control.Width, 20)
            End If
        Next

        ToolTip1.SetToolTip(txtSource, parameter.Description)

    End Sub
    Private Sub Control_GotFocus(sender As Object, e As EventArgs) Handles txtName.GotFocus

        RaiseEvent ChangedFocus(sender)

    End Sub
    Public Index As Integer
    Private Sub Parameter_Changed(sender As Object, value As Object) Handles Parameter.Changed

        Try
            txtSource.Text = Parameter.Formula
            txtValue.Text = Parameter.Value

            If Parameter.Valid Then
                txtValue.ForeColor = Color.Black
            Else
                txtValue.ForeColor = Color.Red
            End If

            Invalidate()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub txtSource_Validated(sender As Object, e As EventArgs) Handles txtSource.Validated

        Parameter.Formula = txtSource.Text

    End Sub
    Private Sub Combo_Changed(sender As Object, e As EventArgs)

        Dim combo As ComboBox = DirectCast(sender, ComboBox)
        Parameter.Formula = combo.Text.ToString

    End Sub
    Private Sub Text_Changed(sender As Object, e As EventArgs)

        Dim tb As TextBox = DirectCast(sender, TextBox)
        Parameter.Formula = tb.Text.ToString

    End Sub
    Public Overrides Function ToString() As String
        Return Me.Name & " " & Parameter.ToString
    End Function

End Class
