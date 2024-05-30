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
Public Module ReadFile_

    ' These are because File.ReadAllXYZ keeps an open file handle.

    Public Function ReadFileToList(filepath As String) As List(Of String)

        Dim result As New List(Of String)

        If Not File.Exists(filepath) Then
            Return result
        End If

        Using sr As New StreamReader(filepath)
            Do While Not sr.EndOfStream
                result.Add(sr.ReadLine)
            Loop
            sr.Close()
        End Using

        Return result

    End Function
    Public Function ReadFileToString(path As String) As String

        Return String.Join(vbCRLF, ReadFileToList(path))

    End Function
    Public Sub WriteFileFromList(path As String, data As IEnumerable(Of String))

        Using sw As New StreamWriter(path)

            For Each line As String In data
                sw.WriteLine(line)
            Next

            sw.Close()

        End Using

    End Sub
    Public Sub WriteFileFromString(path As String, data As String)

        Using sw As New StreamWriter(path)

            sw.Write(data)

            sw.Close()

        End Using

    End Sub
End Module
