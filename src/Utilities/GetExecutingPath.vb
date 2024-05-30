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
Public Module GetExecutingPath_
    ''' <summary>
    ''' Get the path to the currently executing assembly
    ''' </summary>
    ''' <returns>Path, without a trailing "\"</returns>
    Public Function GetExecutingPath() As String
        ' #37#
        Dim execpath As String

        execpath = System.IO.Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)

        If execpath.BeginsWith("file:") Then
            execpath = execpath.Substring("file:".Length)
        End If

        Do While execpath.BeginsWith("\")
            execpath = execpath.Substring(1)
        Loop

        Do While execpath.EndsWith("\")
            execpath = execpath.Substring(0, execpath.Length - 1)
        Loop

        Return execpath

    End Function

End Module
