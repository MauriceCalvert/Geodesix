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
Public Module ExcelErrorCode_

    Dim table As New Dictionary(Of Integer, String) From {
        {&H800A07D0, "#NULL!"},
        {&H800A07D7, "#DIV/0!"},
        {&H800A07DF, "#VALUE!"},
        {&H800A07E7, "#REF!"},
        {&H800A07ED, "#NAME?"},
        {&H800A07F4, "#NUM!"},
        {&H800A07FA, "#N/A"}
    }

    Public Function ExcelErrorCode(code As Integer) As String

        Dim result As String = code.ToString

        table.TryGetValue(code, result)

        Return result

    End Function

    Public Function isExcelErrorCode(value As Object) As Boolean

        If TypeOf value IsNot Integer Then
            Return False
        End If

        Return ExcelErrorCode(CInt(value)) <> ""

    End Function

End Module
