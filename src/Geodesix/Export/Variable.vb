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
Public Class Variable
    Public Sub New(key As String, Optional defaultvalue As String = "", Optional mandatory As Boolean = True)

        _Key = key
        _DefaultValue = defaultvalue
        _Mandatory = mandatory

    End Sub
    Private _DefaultValue As String
    Public Property DefaultValue As String
        Get
            Return _DefaultValue
        End Get
        Set(value As String)
            If Mandatory AndAlso IsEmpty(value) Then
                Throw New GeodesixException($"Default value for {Key} cannot be null")
            End If
            _DefaultValue = value
        End Set
    End Property
    Public ReadOnly Property HasValue As Boolean
        Get
            Return Not IsEmpty(Value)
        End Get
    End Property
    Public ReadOnly Property Key As String
    Public ReadOnly Property Mandatory As Boolean
    Private _Value As String
    Public Property Value As String
        Get
            If IsEmpty(_Value) Then
                Return DefaultValue
            End If
            Return _Value
        End Get
        Set(value As String)
            _Value = value
        End Set
    End Property
    Public Overrides Function ToString() As String
        Return $"{Key}={Value}"
    End Function
End Class
