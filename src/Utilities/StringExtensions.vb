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
Imports System.Runtime.CompilerServices

Public Module StringExtensions

    <Extension>
    Public Function BeginsWith(s As String, find As String) As Boolean

        Return s.StartsWith(find, StringComparison.InvariantCultureIgnoreCase)

    End Function

    <Extension>
    Public Function Includes(s As String, find As String) As Boolean

        Return s.IndexOf(find, StringComparison.OrdinalIgnoreCase) >= 0

    End Function

    <Extension()>
    Public Function HasAny(source As IEnumerable(Of String), find As String) As Boolean

        Return source.Any(Function(q) q.Equals(find, StringComparison.OrdinalIgnoreCase))

    End Function
End Module
