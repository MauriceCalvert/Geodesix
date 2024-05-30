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
Imports Utilities
Public Class RulePath
    Inherits RuleValue

    Friend Sub New(template As Template,
                   Optional name As String = Nothing,
                   Optional type As String = Nothing,
                   Optional output As Boolean = False,
                   Optional path As String = Nothing,
                   Optional value As Object = Nothing)

        MyBase.New(name, type, output, value)
        Me.Template = template

        Me.Source = GenericPath(path)

        If IsEmpty(Me.Name) Then

            ' Get the trailing name from the path
            Dim nicename As String = Me.Source

            ' Strip everything preceding '.' or ']'
            For Each delim As String In {".", "]"}

                Dim dp As Integer = nicename.LastIndexOf(delim)

                If dp >= 0 Then
                    If dp < nicename.Length - 1 Then
                        nicename = nicename.Substring(dp + 1)
                    Else
                        nicename = ""
                        Exit For
                    End If
                End If
            Next

            ' Keep only the letters
            Me.Name = New String(nicename.Where(Function(q) Char.IsLetter(q)).ToArray)
            Me.Output = Not IsEmpty(Me.Name)

            ' If it's empty, make up a name
            Me.Name = Coalesce(Me.Name, template.NewIdentifier)
        End If

        Me.Sample = value
        Me.Value = value

    End Sub
    Public Sub Reset()

        Value = Sample

    End Sub
    Public ReadOnly Property Sample As Object
    Private ReadOnly Property Template As Template
End Class
