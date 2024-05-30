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
Imports System.Reflection
Public MustInherit Class SettingsBase

    MustOverride Sub Load()
    MustOverride Sub Clear()
    MustOverride Sub Save()
    MustOverride Sub MakeItems()

    Public Event Changed(sender As Setting, value As String)

    Protected Const flags As BindingFlags = BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance
    Public Sub New(name As String)

        Me.MyType = Me.GetType
        Me.Folder = name
        MakeItems()

    End Sub
    Public ReadOnly Property Folder As String
    Public Sub Initialise(key As String, value As String) ' Change a setting with raising Changed

        _Items(key).Initialise(value)

    End Sub
    Protected _Items As Dictionary(Of String, Setting)
    ReadOnly Property Items As Dictionary(Of String, Setting)
        Get
            Return _Items
        End Get
    End Property
    Public ReadOnly Property MyType As Type
    Public ReadOnly Property Names As IEnumerable(Of String)
        Get
            Return _Items.Keys
        End Get
    End Property
    Public Sub Reset()

        For Each setting As Setting In Items.Values
            If setting.Name <> "apikey" Then
                setting.Value = setting.Initial
                RaiseEvent Changed(setting, setting.Initial)
            End If
        Next

        Save()

    End Sub
    Protected Sub Setting_Changed(setting As Setting, value As String)

        Save()

        RaiseEvent Changed(setting, value)

    End Sub
    Public Overrides Function ToString() As String

        Return $"{Folder} has {Items.Count} settings"

    End Function
    Public Function TryGetSetting(name As String, ByRef setting As Setting) As Boolean

        Return Items.TryGetValue(name, setting)

    End Function
End Class
