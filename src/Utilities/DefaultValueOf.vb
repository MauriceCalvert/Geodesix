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
Public Module DefaultValueOf_
    Public Function DefaultValueOf(typename As String) As Object

        If IsEmpty(typename) Then
            Return 0
        End If

        Dim type As Type = Type.GetType(typename)

        If type Is Nothing Then
            type = Type.GetType($"System.{typename}")
        End If

        Return DefaultValueOf(type)

    End Function
    Public Function DefaultValueOf(type As Type) As Object

        Dim result As Object = Nothing
        Try
            If type Is Nothing Then
                Return 0
            End If
            If type.IsValueType Then
                result = Activator.CreateInstance(type)
            Else
                Dim args As Object = Nothing
                If type Is Type.GetType("System.String") Then
                    args = {" "c}
                End If
                result = Activator.CreateInstance(type, args)
            End If

        Catch ex As Exception
            result = 0
        End Try

        Return result

    End Function

End Module
