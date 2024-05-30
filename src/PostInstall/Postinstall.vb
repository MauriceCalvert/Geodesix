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
Imports GeodesiX
Imports Seting
Imports Utilities
Imports System.IO
Module Main_

    Sub Main()
        Try
            Dim settings As New GeodesixSettings("Geodesix")
            If File.Exists(settings.SettingsPath) Then
                Try
                    File.Delete(settings.SettingsPath)
                Catch ex As Exception
                End Try
            End If
            Dim cmd As String = GetExcelPath()

            If IsEmpty(cmd) Then
                Microsoft.VisualBasic.MsgBox("Please start Excel to complete the installation of Geodesix")
                Exit Sub
            End If
            cmd = """" & cmd & """"

            Shell(cmd, arguments:="/embedded", windowstyle:=ProcessWindowStyle.Hidden)

        Catch ex As Exception
            Microsoft.VisualBasic.MsgBox("PostInstall failed (" & ex.GetType.ToString & ") " & ex.Message,, "Geodesix Postinstall")

        End Try
    End Sub
End Module
