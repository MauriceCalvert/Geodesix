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

Public Module GetTypeName_
    Public Function GetTypeName(t As Object) As String
        Return GetTypeName(t.GetType)
    End Function
    Public Function GetTypeName(t As Type) As String
        If t.IsGenericType Then
            Return String.Format("{0}<{1}>", t.Name.Substring(0, t.Name.LastIndexOf("`", StringComparison.InvariantCulture)),
                                 String.Join(", ", t.GetGenericArguments().Select(AddressOf GetTypeName)))
        Else
            Return t.Name
        End If
    End Function
End Module
