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
Imports Newtonsoft.Json
Imports Utilities

Partial Public Class GeodesixSettings
    Inherits SettingsBase

    Public Sub New(name As String)

        MyBase.New(name)

        Dim settingspath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), name)
        If Not Directory.Exists(settingspath) Then
            Directory.CreateDirectory(settingspath)
        End If
        Me.SettingsPath = Path.Combine(settingspath, $"GeodesixSettings.json")

        Load()

    End Sub
    Public Overrides Sub Clear()

        SyncLock (Me)
            Reset()
        End SyncLock

    End Sub
    Public Overrides Sub Load()

        Try
            SyncLock (Me)

                Dim data As IEnumerable(Of Pair) = JsonConvert.DeserializeObject(Of IEnumerable(Of Pair))(ReadFileToString(SettingsPath))

                If data Is Nothing Then
                    Exit Sub
                End If

                For Each pair As Pair In data
                    _Items(pair.Key).Initialise(pair.Value)
                Next

            End SyncLock

        Catch ex As Exception
            Clear()
        End Try

    End Sub
    Public Overrides Sub Save()

        SyncLock (Me)
            Try
                Dim pairs As IEnumerable(Of Pair) =
                    Items.Values.Select(Function(q) New Pair(q.Name, q.Value))

                Dim json As String = JsonConvert.SerializeObject(pairs, Formatting.None)

                WriteFileFromString(SettingsPath, json)

            Catch ex As Exception
                HandleError("Saving settings", ex)
            End Try
        End SyncLock

    End Sub
    Public ReadOnly Property SettingsPath As String

End Class
