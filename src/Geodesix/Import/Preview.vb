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
Imports System.Data

Public Class Preview
    Public Sub New(datatable As DataTable)

        InitializeComponent()

        DataGridView1.DataSource = datatable
        Preview_Resize(Me, Nothing)
        Me.Text = $"Preview {datatable.Rows.Count:#,##0} rows"
    End Sub

    Private Sub Preview_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        With Button1
            .Left = ClientSize.Width \ 2 - .Width \ 2
            .Top = ClientSize.Height - .Height - 1
        End With
    End Sub
End Class