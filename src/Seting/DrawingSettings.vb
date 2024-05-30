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
Imports Newtonsoft.Json
Imports Utilities

Partial Public Class DrawingSettings
    Inherits SettingsBase

    ' Careful not to define any properties, they'll confuse the loader looking for things that start with '_'
    Private Props As WorkbookProperties
    Public Sub New(name As String, wbp As WorkbookProperties)

        MyBase.New(name)
        Props = wbp
        Load()

    End Sub
    Public Overrides Sub Clear()

        SyncLock (Me)

            For Each setting As Setting In Items.Values
                Props.Delete(setting.Name)
            Next

            Reset()

        End SyncLock

    End Sub
    Public Overrides Sub Load()

        Try
            SyncLock (Me)
                Dim saved As String = Props("DrawingSettings")

                Dim data As IEnumerable(Of Pair) = JsonConvert.DeserializeObject(Of IEnumerable(Of Pair))(saved)

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

                Dim json As String = JsonConvert.SerializeObject(pairs, Formatting.Indented)

                Props("DrawingSettings") = json

            Catch ex As Exception
                HandleError("Saving settings", ex)
            End Try
        End SyncLock

    End Sub

End Class
