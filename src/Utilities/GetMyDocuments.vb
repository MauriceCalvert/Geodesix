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
Imports System.Environment
Public Module GetMyDocuments_
    ''' <summary>
    ''' Get the path to "My Documents"
    ''' </summary>
    ''' <returns>Path, without the trailing "\"</returns>
    Public Function GetMyDocuments() As String
        ' #38#
        Dim result As String

        result = System.Environment.GetFolderPath(SpecialFolder.MyDocuments)

        Do While result.EndsWith("\")
            result = result.Substring(0, result.Length - 1)
        Loop

        Return result

    End Function

End Module
