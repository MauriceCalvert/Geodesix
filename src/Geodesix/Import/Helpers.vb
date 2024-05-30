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
Imports System.IO
Imports System.Windows.Forms
Imports Utilities

Public Module Helpers

    Public Function BrowseForFile(ByRef file As String, extension As String, filter As String, directory As String,
                                  Optional fileexists As Boolean = True) As Boolean

        Dim ofd As New OpenFileDialog
        With ofd
            .CheckFileExists = fileexists
            .CheckPathExists = True
            .DefaultExt = extension
            .Filter = filter & "|*All files (*.*)|*.*"
            .InitialDirectory = directory
            .Multiselect = False
            .ShowReadOnly = False
            Dim result As DialogResult = ShowCommonDialog(ofd)
            If result <> DialogResult.OK Then
                Return False
            End If
            file = .FileName
        End With

        Return True
    End Function

End Module
