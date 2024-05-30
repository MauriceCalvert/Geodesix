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
Public Class RuleExpression
    Inherits RuleValue

    Friend Sub New(template As Template,
                   Optional name As String = Nothing,
                   Optional type As String = Nothing,
                   Optional output As Boolean = Nothing,
                   Optional formula As String = Nothing)

        MyBase.New(name)
        Me.Name = name
        Me.Type = type
        Me.Output = output
        Me.Source = formula

    End Sub

End Class
