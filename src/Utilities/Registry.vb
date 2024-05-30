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
Imports Microsoft.Win32 ' for registry
Public Module Registry

    'Public Function HKEYLocalMachine() As Microsoft.Win32.RegistryKey

    '    Dim rk As Microsoft.Win32.RegistryKey

    '    If Is64BitOperatingSystem() Then
    '        rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
    '    Else
    '        rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
    '    End If

    '    Return rk
    'End Function
    'Public Function HKEYClassesRoot() As Microsoft.Win32.RegistryKey

    '    Dim rk As Microsoft.Win32.RegistryKey

    '    If Is64BitOperatingSystem() Then
    '        rk = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64)
    '    Else
    '        rk = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32)
    '    End If

    '    Return rk
    'End Function
    Public Property RegistryValue(key As String) As String
        Get
            Dim result As String = ""
            Try
                Dim gk As RegistryKey = GetSubKey(key, False)
                Dim v As Object = gk.GetValue(key)
                If v Is Nothing Then
                    Return ""
                End If
                result = v.ToString
                If String.IsNullOrEmpty(result) Then
                    result = ""
                End If
                gk.Close()
            Catch ex As Exception
            End Try
            Return result
        End Get
        Set(value As String)
            Dim gk As RegistryKey = GetSubKey(key, True)
            gk.SetValue(key, value)
            gk.Close()
        End Set
    End Property
    Private Function GetSubKey(key As String, write As Boolean) As RegistryKey
        Dim rk As RegistryKey = Nothing
        Dim sk As RegistryKey = Nothing
        Dim gk As RegistryKey = Nothing
        Dim value As String = ""

        rk = HKEYCurrentUser()
        sk = rk.OpenSubKey("Software", False)
        If sk Is Nothing Then
            Throw New GeodesixException("Registry key CurrentUser/Software is missing!")
        End If
        gk = sk.OpenSubKey("Geodesix", write)
        If gk Is Nothing Then
            sk.Close()
            sk = rk.OpenSubKey("Software", True)
            sk.CreateSubKey("Geodesix")
            gk = sk.OpenSubKey("Geodesix", write)
        End If
        sk.Close()
        rk.Close()
        Return gk
    End Function
    Public Function HKEYCurrentUser() As Microsoft.Win32.RegistryKey

        Dim rk As Microsoft.Win32.RegistryKey

        If Is64BitOperatingSystem() Then
            rk = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
        Else
            rk = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32)
        End If

        Return rk
    End Function

End Module
