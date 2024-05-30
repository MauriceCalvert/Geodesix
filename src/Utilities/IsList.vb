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
Public Module IsList_
    Public Function IsList(ByVal o As Object) As Boolean
        If o Is Nothing Then Return False
        Return TypeOf o Is IList AndAlso o.[GetType]().IsGenericType AndAlso o.[GetType]().GetGenericTypeDefinition().IsAssignableFrom(GetType(List(Of)))
    End Function

End Module
