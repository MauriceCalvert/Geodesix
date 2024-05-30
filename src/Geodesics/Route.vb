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
Imports System.Collections.ObjectModel

''' <summary>
''' One or more sets of directions on how to get from A to B
''' </summary>
''' <remarks>Contains one or more Legs, describing the itinerary in more detail</remarks>

Public Class Route


    ''' <summary>
    ''' A leg of a route, describing an intermediate step between A and B
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Leg
        ''' <summary>
        ''' A step in a Leg. The smallest element in a set of Directions
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Stepp
            ''' <summary>
            ''' Mode of travel: Transit, Driving, Bicycling or Walking
            ''' </summary>
            Public Property Mode As String
            Public Property Start As New Location
            Public Property Finish As New Location
            Public Property Polyline As New Polyline
            ''' <summary>
            ''' The time that Google Directions estimates to complete this step
            ''' </summary>
            Public Property Duration As Double ' Days, for Excel
            ''' <summary>
            ''' The distance in metres that Google Directions estimates to complete this step
            ''' </summary>
            Public Property Distance As Integer ' in metres 
            ''' <summary>
            ''' Textual description of how to achcieve this step
            ''' </summary>
            Public Property HTML_Instructions As String
        End Class

        Private ReadOnly Property _Steps As New List(Of Stepp)
        ''' <summary>
        ''' The steps in this Leg
        ''' </summary>
        Public ReadOnly Property Steps As ReadOnlyCollection(Of Stepp)
            Get
                Return _Steps.AsReadOnly
            End Get
        End Property
        ''' <summary>
        ''' The distance, in metres, of this step (sum of the Step.Distances)
        ''' </summary>
        Public Property Distance As Integer ' in metres 
        ''' <summary>
        ''' The duration of this step (sum of the Step.Durations)
        ''' </summary>
        Public Property Duration As Double ' Days, for Excel
        Public Property EndAddress As String
        Public Property EndLocation As New Location
        Public Property StartAddress As String
        Public Property StartLocation As New Location
        Public Function AddStep() As Stepp
            Dim result As Stepp = New Stepp
            _Steps.Add(result)
            Return result
        End Function
    End Class

    Private HaveTotals As Boolean = False
    Private _Distance As Integer = 0
    Private _Time As Double = 0
    Private ReadOnly _Legs As New List(Of Leg)
    Sub New(key As String)
        _Key = key
    End Sub
    Public Function AddLeg() As Leg
        Dim result As Leg = New Leg
        _Legs.Add(result)
        Return result
    End Function
    Private Sub CalculateTotals()
        HaveTotals = True
        For Each leg As Leg In _Legs
            _Distance = _Distance + leg.Distance
            _Time = _Time + leg.Duration
        Next
    End Sub
    ''' <summary>
    ''' The distance of this route. Calculated, the sum of the Leg.Distances
    ''' </summary>
    Public Property Distance As Integer ' in metres
        Get
            If Not HaveTotals Then ' first time
                CalculateTotals()
            End If
            Return _Distance
        End Get
        Set(ByVal value As Integer)
            _Distance = value
            HaveTotals = True
        End Set
    End Property
    ''' <summary>
    ''' The duration of this route. Calculated, the sum of the Leg.Durations
    ''' </summary>
    Public Property Duration As Double ' Days, for Excel ' Days, for Excel
        Get
            If Not HaveTotals Then ' first time
                CalculateTotals()
            End If
            Return _Time
        End Get
        Set(ByVal value As Double)  ' Days, for Excel
            _Time = value
            HaveTotals = True
        End Set
    End Property
    Public ReadOnly Property Key As String
    Public ReadOnly Property Legs As ReadOnlyCollection(Of Leg)
        Get
            Return _Legs.AsReadOnly
        End Get
    End Property
End Class
