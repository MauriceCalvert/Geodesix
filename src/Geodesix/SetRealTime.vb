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
Imports Seting
Imports Utilities

Public Module SetRealTime_
    ''' <summary>
    ''' Force the Excel RTD to update in real time, rather than the default 2 seconds.
    ''' </summary>
    ''' <remarks>Makes the interface snappier and doesn't present a performance issue, as we know we
    ''' won't be getting updates from Google very fast and anyway each request is 'final' as it never
    ''' is updated again. Requires write privilege to HKCU.</remarks>
    Private Const INTERVAL As Integer = 0
    Public Sub SetRealtime()
        ' #34#
        If Settings.RealTimeSet Then
            Exit Sub
        End If
        Settings.Initialise("RealTimeSet", True)

        Dim rk As RegistryKey
        Dim sk As RegistryKey
        Dim versions() As String
        Dim names() As String
        Dim versionnumber As Double = 0
        Dim throttle As Integer


        Try
            rk = HKEYCurrentUser()
            rk = rk.OpenSubKey("Software\Microsoft\Office", False)
            If rk IsNot Nothing Then ' Looks like office is installed

                versions = rk.GetSubKeyNames

                For Each version As String In versions ' Examine each Place version

                    If Double.TryParse(version, versionnumber) Then ' Something like "11.0'" ?

                        sk = rk.OpenSubKey(version)
                        If sk IsNot Nothing Then sk = sk.OpenSubKey("Excel\Options", True)

                        If sk IsNot Nothing Then ' OK, we have HKEY_CURRENT_USER\Software\Microsoft\Place\[version]

                            names = sk.GetValueNames
                            throttle = -1

                            For Each name As String In names

                                If name = "RTDThrottleInterval" Then ' Throttle interval already created
                                    throttle = CInt(sk.GetValue("RTDThrottleInterval"))
                                    If throttle <> 0 Then ' If it's not realtime, force it so
                                        If throttle <> INTERVAL Then
                                            sk.SetValue("RTDThrottleInterval", INTERVAL)
                                            CustomLog.Logger.Debug("SetRealtime Throttle changed from {0} to {1}", throttle, INTERVAL)
                                        End If
                                    End If
                                End If
                            Next

                            If throttle = -1 Then ' Didn't find Throttle interval, add it, realtime
                                sk.SetValue("RTDThrottleInterval", INTERVAL)
                                CustomLog.Logger.Debug("SetRealtime Throttle created = {0}", INTERVAL)
                            End If

                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            Utilities.ShowBox("Unable to set real-time RTDThrottleInterval, " & ex.Message & vbCRLF &
                   "Updates will only occur every 5 seconds", "Realtime disabled",
                   icon:=System.Windows.Forms.MessageBoxIcon.Information)
        End Try

    End Sub

End Module
