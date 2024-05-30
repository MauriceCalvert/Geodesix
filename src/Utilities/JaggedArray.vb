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
Public Module JaggedArray_

    Public Function JaggedArray(Of T)(ByVal twoDimensionalArray As T(,)) As T()()

        ' https://stackoverflow.com/a/45794393/338101

        Dim rowsFirstIndex As Integer = twoDimensionalArray.GetLowerBound(0)
        Dim rowsLastIndex As Integer = twoDimensionalArray.GetUpperBound(0)
        Dim numberOfRows As Integer = rowsLastIndex - rowsFirstIndex + 1
        Dim columnsFirstIndex As Integer = twoDimensionalArray.GetLowerBound(1)
        Dim columnsLastIndex As Integer = twoDimensionalArray.GetUpperBound(1)
        Dim numberOfColumns As Integer = columnsLastIndex - columnsFirstIndex + 1
        Dim result As T()() = New T(numberOfRows - 1)() {}

        For i As Integer = 0 To numberOfRows - 1
            result(i) = New T(numberOfColumns - 1) {}

            For j As Integer = 0 To numberOfColumns - 1
                result(i)(j) = twoDimensionalArray(i + rowsFirstIndex, j + columnsFirstIndex)
            Next
        Next

        Return result

    End Function

End Module
