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
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Utilities
Public Class SettingLine

    Friend Setting As Setting
    Public Sub New(Setting As Setting)

        InitializeComponent()

        Me.Setting = Setting

        Draw()

    End Sub
    Friend Sub Draw()

        txtName.Text = Setting.Name
        txtName.FitContent
        txtValue.Text = Setting.Value
        txtValue.FitContent
        txtDescription.Text = Setting.Description
        txtDescription.FitContent

        If Setting.Values Is Nothing Then
            ValueControl = txtValue
        Else
            Dim combo As New ComboBox
            ValueControl = combo

            With combo
                Controls.Remove(txtValue)
                Controls.Add(combo)

                .Left = txtValue.Left
                .Top = txtValue.Top
                .Width = txtValue.Width

                If GetTypeName(Setting.Values) = "List<Pair>" Then

                    combo.DisplayMember = "Key"
                    combo.ValueMember = "Value"
                    combo.DataSource = Setting.Values
                Else
                    combo.DataSource = Setting.Values
                End If

                .Visible = True
                .FitContent
                txtName.Top += 1
                If GetTypeName(Setting.Values) = "List<Pair>" Then

                    Dim items As List(Of Pair) = DirectCast(Setting.Values, List(Of Pair))

                    .SelectedItem = items.Where(Function(q) q.Value = Setting.Value).FirstOrDefault
                Else
                    .SelectedItem = Setting.Value
                End If
                AddHandler combo.SelectedValueChanged, AddressOf Combo_Changed
                AddHandler combo.TextChanged, AddressOf Combo_Changed
                AddHandler combo.Resize, AddressOf Combo_Resized
            End With
        End If

    End Sub
    Private Sub txtValue_Validating(sender As Object, e As CancelEventArgs) Handles txtValue.Validating

        Try
            Setting.Value = txtValue.Text

        Catch ex As Exception
            ShowError(ex.Message)
            e.Cancel = True
        End Try

    End Sub
    Private Sub Combo_Changed(sender As Object, e As EventArgs)

        Try
            Dim combo As ComboBox = DirectCast(sender, ComboBox)
            If combo.SelectedItem Is Nothing Then
                Exit Sub
            End If

            Setting.Value = Value

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub Combo_Resized(sender As Object, e As EventArgs)

        ' Ghastly hack: When a ComboBox resizes, it selects all the text. Ugly. 

        Dim combo As ComboBox = DirectCast(sender, ComboBox)
        combo.SelectionLength = 0

    End Sub
    Private ReadOnly Property Value As String
        Get
            If TypeOf ValueControl Is TextBox Then
                Return txtValue.Text
            Else
                Dim combo As ComboBox = DirectCast(ValueControl, ComboBox)

                If GetTypeName(Setting.Values) = "List<Pair>" Then

                    Dim item As Pair = DirectCast(combo.SelectedItem, Pair)
                    Return item.Value
                Else
                    Return CStr(combo.SelectedItem)
                End If
            End If
        End Get
    End Property
    Public Property ValueControl As Control

End Class
