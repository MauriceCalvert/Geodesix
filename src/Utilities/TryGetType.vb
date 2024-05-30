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
Imports System.Reflection
Imports System.Linq
Public Module TryGetType_
    Public Function TryGetType(typename As String, ByRef type As Type) As Boolean

        type = Nothing

        If IsEmpty(typename) Then
            Return False
        End If

        Try
            type = Type.GetType(typename)

            If type Is Nothing Then
                type = Type.GetType($"System.{typename}")
            End If

            If type Is Nothing Then

                Dim lp As Integer = typename.LastIndexOf(".")
                If lp < 0 Then
                    Return False
                End If

                Dim ns As String = typename.Substring(0, lp)
                Dim tn As String = typename.Substring(lp + 1)
                Dim ass As Assembly = AppDomain.CurrentDomain.GetAssemblies.Where(Function(q) q.GetName.Name = ns).FirstOrDefault
                If ass IsNot Nothing Then
                    type = ass.GetTypes.Where(Function(q) q.Name = tn).FirstOrDefault
                    If type IsNot Nothing Then
                        Return True
                    End If
                End If

            End If

        Catch ex As Exception
        End Try

        Return type IsNot Nothing

    End Function

End Module
