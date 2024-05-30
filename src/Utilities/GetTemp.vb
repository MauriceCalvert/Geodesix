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
Public Module GetTemp_

    Public Sub ClearTempFolders()
        Exit Sub
        For Each d As String In Directory.EnumerateDirectories(Path.GetTempPath, "geodesix*")
            Dim di As DirectoryInfo = New DirectoryInfo(d)
            di.Delete(True)
        Next

    End Sub
    Public Function GetTempFolder(Optional subfolder As String = "geodesix") As String

        Dim folder As String
        Dim i As Integer = 0
        Do
            folder = Path.Combine(Path.GetTempPath, subfolder & i.ToString("0000"))
            If Not Directory.Exists(folder) Then
                Exit Do
            End If
            i += 1
        Loop

        Directory.CreateDirectory(folder)

        Return folder

    End Function
    Public Function GetTempFile(filetype As String, Optional subfolder As String = "geodesix") As String

        Dim folder As String = GetTempFolder(subfolder)

        Dim result As String
        Do
            Dim fn As String = $"temp{Unique():000000}.{filetype}"
            result = Path.Combine(folder, fn)

        Loop Until Not File.Exists(result)

        Dim fs As FileStream = File.Create(result)
        fs.Close()

        Return result

    End Function

End Module
