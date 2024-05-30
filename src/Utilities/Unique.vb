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
Imports System.Threading

Public Module Unique_

    Private _Unique As Integer = 1

    ''' <summary>
    ''' Return a unique, ascending number in a thread-safe manner
    ''' </summary>
    ''' <remarks>This function is thread-safe</remarks>
    Public Function Unique() As Integer

        Dim initialvalue As Integer
        Dim computedvalue As Integer

        Do
            initialvalue = _Unique

            computedvalue = initialvalue + 1

        Loop While initialvalue <> Interlocked.CompareExchange(_Unique, computedvalue, initialvalue)

        Return computedvalue

    End Function

End Module
