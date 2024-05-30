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
Imports Newtonsoft.Json.Linq
Imports Utilities
Public MustInherit Class Rule

    Protected Sub New(name As String)

        ID = Unique()
        Me.Name = name

    End Sub

    Public ReadOnly Property ID As Integer
    Private _Name As String
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
            If Not IsEmpty(_Name) Then
                _Name = Trim(_Name)
            End If
        End Set
    End Property
    Friend Property Node As JValue ' Helper for Parser
    Private _Message As String
    Public Property Message As String
        Get
            Return _Message
        End Get
        Friend Set(value As String)
            If IsEmpty(value) Then
                _Message = Nothing
            Else
                _Message = value
            End If
        End Set
    End Property
    Public Overrides Function ToString() As String

        Return $"{Me.GetType.Name} {Name}"

    End Function

End Class
