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
Imports System.Linq

Public Class Method

    Public Event Changed(sender As Object, value As Object)

    Private ReadOnly Property Excel As Object
    Public Sub New(excel As Object,
                   name As String,
                   type As Type,
                   description As String,
                   returns As String,
                   example As String,
                   parameters As IEnumerable(Of Parameter),
                   Optional submethods As Methods = Nothing)

        Me.Excel = excel
        Me.Name = name
        Me.Description = description
        Me.Returns = returns
        Me.Example = example
        Me.Parameters = parameters?.ToList
        Me.Type = type
        For Each parm As Parameter In parameters
            AddHandler parm.Changed, AddressOf Parameter_Changed
        Next
        Me.SubMethods = submethods

    End Sub
    Public ReadOnly Property Description As String
    Public ReadOnly Property Example As String
    Public ReadOnly Property Name As String
    Public ReadOnly Property Parameters As List(Of Parameter)
    Public ReadOnly Property Type As Type
    Public ReadOnly Property Returns As String
    Private Sub Parameter_Changed(sender As Object, value As Object)

        RaiseEvent Changed(sender, value)

    End Sub
    Public ReadOnly Property SubMethods As Methods
    Public Overrides Function ToString() As String
        Return $"{Name}({String.Join(",", Parameters.Select(Function(q) q.Name))})"
    End Function
End Class
