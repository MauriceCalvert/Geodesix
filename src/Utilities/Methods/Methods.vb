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
Public MustInherit Class Methods

    Private _Methods As List(Of Method)
    Public ReadOnly Property Children As List(Of Methods)
        Get
            Return Functions.
                   Where(Function(q) q.SubMethods IsNot Nothing).
                   Select(Function(q) q.SubMethods).
                   Distinct.
                   ToList
        End Get
    End Property
    Public Property Functions As List(Of Method)
        Get
            Return _Methods
        End Get
        Protected Set(value As List(Of Method))
            _Methods = value
        End Set
    End Property
    Public MustOverride Function Generate(method As Method) As String
    Public Overrides Function ToString() As String

        Return GetTypeName(Me) & " " & String.Join(vbCR, Functions.Select(Function(q) q))

    End Function
End Class
