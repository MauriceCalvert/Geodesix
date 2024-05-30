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
Imports System.Diagnostics

Public Module Shell_

    Public Function Shell(ByVal command As String,
                          Optional arguments As String = "",
                          Optional ByVal windowstyle As ProcessWindowStyle = ProcessWindowStyle.Minimized,
                          Optional ByVal Wait As Boolean = False,
                          Optional UseShellExecute As Boolean = False,
                          Optional ByVal Timeout As Integer = -1) As Integer

        Dim startInfo As ProcessStartInfo
        Dim msiprocess As Diagnostics.Process
        startInfo = New ProcessStartInfo(command)
        startInfo.Arguments = arguments
        startInfo.UseShellExecute = UseShellExecute
        startInfo.RedirectStandardError = False
        startInfo.WindowStyle = windowstyle
        startInfo.CreateNoWindow = False
        msiprocess = Process.Start(startInfo)

        If Wait Then
            If Timeout = -1 Then
                msiprocess.WaitForExit()
            Else
                msiprocess.WaitForExit(Timeout)
            End If
        End If
        Return 0
    End Function
End Module
