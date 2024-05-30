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
Imports Utilities
Public MustInherit Class RuleValue
    Inherits Rule

    Protected Sub New(name As String,
                      Optional type As String = Nothing,
                      Optional output As Boolean = False,
                      Optional value As Object = Nothing)

        MyBase.New(name)
        Me.Output = output
        Me.Value = value
        Me.Type = Coalesce(type, value?.GetType.Name)

    End Sub
    Public Overrides Function ToString() As String

        Return $"{MyBase.ToString} = {Coalesce(Value, "[empty]")} {Source}"

    End Function
    Public Property Output As Boolean
    Public Property Source As String ' Formula or Path
    Private _Type As String
    Public Property Type As String
        Get
            Return _Type
        End Get
        Protected Set(value As String)
            _Type = value
        End Set
    End Property
    Private _Value As Object
    Public Overridable Property Value As Object
        Get
            Return _Value
        End Get
        Set(value As Object)
            IfNotEmpty(value, Sub() Type = value.GetType.Name)
            _Value = value
        End Set
    End Property
End Class
