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
Imports JsonTransformer
Imports Utilities
Public Class RuleEditor

    Public Sub New(arule As Rule)

        ID = arule.ID
        Rule = arule
        RuleValue = TryCast(arule, RuleValue)

        Action = arule.GetType.Name.Substring(4) ' Remove 'Rule' prefix 

    End Sub
    Public Property Action As String
    Public ReadOnly Property ID As Integer
    Public Property Identifier As String
        Get
            Return Rule.Name
        End Get
        Set(value As String)
            Rule.Name = value
        End Set
    End Property
    Public Property Output As String
        Get
            Return RuleValue?.Output.ToString
        End Get
        Set(value As String)
            Boolean.TryParse(value, RuleValue.Output)
        End Set
    End Property
    Public ReadOnly Property Rule As Rule
    Private RuleValue As RuleValue
    Public Property Source As String
        Get
            Return RuleValue?.Source
        End Get
        Set(value As String)
            RuleValue.Source = value
        End Set
    End Property
    Public ReadOnly Property Type As String
        Get
            Return RuleValue?.Type
        End Get
    End Property
    Public ReadOnly Property Value As String
        Get
            Return Coalesce(Rule?.Message, RuleValue?.Value?.ToString)
        End Get
    End Property

End Class
