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
Imports Geodesics
Public Class CacheDump

    Friend Structure Row
        Public Property Cache As String
        Public Property Query As String
    End Structure

    Public Sub New()

        InitializeComponent()

    End Sub

    Private Sub CacheDump_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Text = $"Cache has {Cache.Root.Local.Values.Count:#,##0} queries"

        Dim rows As List(Of Row) =
            Cache.
            Root.
            Local.
            Values.
            Select(Function(q) New Row With {.Cache = Cache.Root.GUIDs, .Query = q.ToString}).
            ToList

        For Each qc As QueryCache In Cache.WorkbookCache.Values

            rows.AddRange(
                qc.
                Local.
                Values.
                Select(Function(q) New Row With {.Cache = qc.GUIDs, .Query = q.ToString}))
        Next

        Dim bl As New BindingList(Of Row)(rows)

        DataGridView1.AutoGenerateColumns = False
        DataGridView1.DataSource = bl

    End Sub
End Class