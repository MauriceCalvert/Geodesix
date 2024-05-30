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
Imports System.Runtime.InteropServices

Public Module Is64BitOS_

    ''' <summary>
    ''' The function determines whether the current operating system is a 
    ''' 64-bit operating system.
    ''' </summary>
    ''' <returns>
    ''' The function returns true if the operating system is 64-bit; 
    ''' otherwise, it returns false.
    ''' </returns>
    Public Function Is64BitOperatingSystem() As Boolean
        If IntPtr.Size = 8 Then
            ' 64-bit programs run only on Win64
            Return True
        Else
            ' 32-bit programs run on both 32-bit and 64-bit Windows
            ' Detect whether the current process is a 32-bit process 
            ' running on a 64-bit system.
            Dim flag As Boolean
            Return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") AndAlso IsWow64Process(GetCurrentProcess(), flag)) AndAlso flag)
        End If
    End Function

    ''' <summary>
    ''' The function determins whether a method exists in the export 
    ''' table of a certain module.
    ''' </summary>
    ''' <param name="moduleName">The name of the module</param>
    ''' <param name="methodName">The name of the method</param>
    ''' <returns>
    ''' The function returns true if the method specified by methodName 
    ''' exists in the export table of the module specified by moduleName.
    ''' </returns>
    Private Function DoesWin32MethodExist(ByVal moduleName As String, ByVal methodName As String) As Boolean
        Dim moduleHandle As IntPtr = GetModuleHandle(moduleName)
        If moduleHandle = IntPtr.Zero Then
            Return False
        End If
        Return (GetProcAddress(moduleHandle, methodName) <> IntPtr.Zero)
    End Function

    <DllImport("kernel32.dll")>
    Private Function GetCurrentProcess() As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Private Function GetModuleHandle(ByVal moduleName As String) As IntPtr
    End Function

    <DllImport("kernel32", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Function GetProcAddress(ByVal hModule As IntPtr, <MarshalAs(UnmanagedType.LPStr)> ByVal procName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Function IsWow64Process(ByVal hProcess As IntPtr, ByRef wow64Process As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function


End Module
