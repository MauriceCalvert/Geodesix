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
Imports System.Linq
Imports System.IO
Imports Microsoft.Win32

Public Module GetExcelPath__
    Public Function GetExcelPath() As String ' this is EXPENSIVE!

        ' System.Diagnostics.Debugger.Launch()
        Dim pf64 As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        Dim pf86 As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
        If pf64 = pf86 Then ' When we're targeting X86, see https://stackoverflow.com/a/53114436/338101
            pf64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion")?.GetValue("ProgramFilesDir")
        End If

        Dim offices As New List(Of String)
        For Each path As String In {pf64, pf86}
            If Not String.IsNullOrEmpty(path) Then
                Dim office() As String = Directory.GetDirectories(path, "Microsoft Office*")
                offices.AddRange(office)
            End If
        Next path
        If Not offices.Any Then
            Microsoft.VisualBasic.MsgBox($"Unable to find Microsoft Office directory in {pf64} or {pf86}",, "Geodesix Postinstall")
            Return ""
        End If

        Dim excels As New List(Of String)
        For Each excel As String In offices.Distinct
            If Not String.IsNullOrEmpty(excel) Then
                Dim instances() As String = Directory.GetFiles(excel, "excel.exe", SearchOption.AllDirectories)
                excels.AddRange(instances)
            End If
        Next excel
        If Not excels.Any Then
            Microsoft.VisualBasic.MsgBox("Unable to find Excel.exe in any of the Microsoft Office directories",, "Geodesix Postinstall")
            Return ""
        End If

        Dim newest As DateTime = DateTime.MinValue
        Dim chosen As String = ""
        For Each version As String In excels
            Dim created As DateTime = File.GetCreationTime(version)
            If created > newest Then
                chosen = version
                newest = created
            End If
        Next version
        Return chosen
    End Function
End Module
