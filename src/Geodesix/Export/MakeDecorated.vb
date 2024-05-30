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
Imports System.Collections.Generic
Imports System.IO
Imports Utilities
Public Module MakePage_
    Public Function MakeDecorated(ByRef excel As Object, sheetname As String, target As String) As Boolean

        Dim template As List(Of String) = ReadFileToList(Path.Combine(GetExecutingPath, "templates", "base.html"))

        Dim exporter As New ExportDecorated(excel, sheetname, target, template)

        exporter.Transform()

        WriteFile(target, exporter.Results)

        Return exporter.hasContent

    End Function
End Module
