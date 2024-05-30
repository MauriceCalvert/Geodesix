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
Imports System.Collections.Specialized
Imports System.Windows.Forms
Imports Utilities
Public Class TravellingSalesmanSolver

    Public Class Visit
        Private _name As String
        Public Property column As Integer = -1
        Public Property lat As Double
        Public Property lng As Double
        Public Property name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Public Property order As Integer
        Public Property row As Integer = -1
        Public Property seen As Boolean
    End Class

    Public Cancel As Boolean = False
    Public InitialTemperature As Double = 10000
    Public CoolingRate As Double = 0.99999
    Public AbsoluteTemperature As Double = 0.00001
    Public Cities As New OrderedDictionary
    Public ShortestDistance As Double = Double.MaxValue
    Public Changed As Boolean = False

    Private currentOrder As New List(Of Integer)
    Private nextOrder As New List(Of Integer)
    Private distances As Double(,)
    Private random As New Random()

    Sub New()
    End Sub
    ''' <summary>
    ''' Annealing Process
    ''' </summary>
    Public Sub Anneal(ByVal advanced As Boolean, ByVal txttemperature As TextBox, ByVal txtiterations As TextBox, ByVal txtdistance As TextBox)
        Dim iteration As Integer = 0

        Dim temperature As Double = InitialTemperature
        Dim deltaDistance As Double = 0
        Dim cooling As Double = CoolingRate
        Dim absolute As Double = AbsoluteTemperature

        Dim distance As Double = GetTotalDistance(currentOrder)

        While temperature > absolute

            _Iterations += 1

            nextOrder = GetNextArrangement(currentOrder)

            deltaDistance = GetTotalDistance(nextOrder) - distance

            'if the new order has a smaller distance
            'or if the new order has a larger distance but satisfies Boltzman condition then accept the arrangement
            If (deltaDistance < 0) OrElse (distance > 0 AndAlso Math.Exp(-deltaDistance / temperature) > random.NextDouble()) Then
                For i As Integer = 0 To nextOrder.Count - 1
                    currentOrder(i) = nextOrder(i)
                Next

                distance = deltaDistance + distance
            End If

            'cool down the temperature
            temperature *= cooling

            iteration = iteration + 1

            If advanced AndAlso iteration Mod 100000 = 0 Then
                If temperature > 10 Then
                    txttemperature.Text = String.Format("{0:0}", temperature)
                Else
                    txttemperature.Text = String.Format("{0:0.0###########}", temperature)
                End If
                txttemperature.Refresh()
                txtiterations.Text = iteration.ToString
                txtiterations.Refresh()
                txtdistance.Text = String.Format("{0:0.000}", distance / 1000)
                txtdistance.Refresh()
                Application.DoEvents()
                If Cancel Then
                    Return
                End If
            End If

        End While

        ShortestDistance = distance
    End Sub
    Public Function AddCity(ByVal name As String, ByVal row As Integer, ByVal col As Integer, ByVal latitude As Double, ByVal longitude As Double) As Visit
        Dim city As New Visit
        city.name = name
        city.lat = latitude
        city.lng = longitude
        city.row = row
        city.column = col
        Cities.Add(name, city)
        city.order = Cities.Count
        Changed = True
        Return city
    End Function
    Public Sub RemoveCity(ByVal name As String)
        Dim removing As Visit
        If Cities.Contains(name) Then
            removing = DirectCast(Cities(name), Visit)
            For Each v As Visit In Cities.Values
                If v.order > removing.order Then
                    v.order = v.order - 1
                End If
            Next
            Cities.Remove(name)
        End If
        Changed = True
    End Sub
    Public ReadOnly Property City(ByVal name As String) As Visit
        Get
            If Cities.Contains(name) Then
                Return DirectCast(Cities(name), Visit)
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property Iterations As Integer
    Public Sub Solve(ByVal advanced As Boolean, ByVal txttemperature As TextBox, ByVal txtiterations As TextBox, ByVal txtdistance As TextBox)
        Dim savedorder As New List(Of Integer)
        Dim saveddistance As Double

        If currentOrder.Count > 0 AndAlso currentOrder.Count = Cities.Count Then
            savedorder = currentOrder
            saveddistance = ShortestDistance
        Else
            saveddistance = Double.MaxValue
        End If

        currentOrder = New List(Of Integer)
        ReDim distances(Cities.Count - 1, Cities.Count - 1)
        For i As Integer = 0 To Cities.Count - 1
            For j As Integer = 0 To Cities.Count - 1
                distances(i, j) = Haversine(CDbl(Cities.Item(i).lat),
                                           CDbl(Cities.Item(i).lng),
                                           CDbl(Cities.Item(j).lat),
                                           CDbl(Cities.Item(j).lng))
            Next
            currentOrder.Add(i)
        Next
        Cancel = False
        _Iterations = 0
        Anneal(advanced, txttemperature, txtiterations, txtdistance)
        If ShortestDistance < saveddistance Then
            For i As Integer = 0 To currentOrder.Count - 1
                Cities(currentOrder(i)).order = i
            Next
        Else
            ShortestDistance = saveddistance
        End If
        Changed = False
    End Sub

    ''' <summary>
    ''' Calculate the total distance which is the objective function
    ''' </summary>
    ''' <param name="order">A list containing the order of Cities</param>
    ''' <returns></returns>
    Private Function GetTotalDistance(ByVal order As List(Of Integer)) As Double
        Dim distance As Double = 0

        For i As Integer = 0 To order.Count - 2
            distance += distances(order(i), order(i + 1))
        Next

        If order.Count > 0 Then
            distance += distances(order(order.Count - 1), 0)
        End If
        Return distance
    End Function

    ''' <summary>
    ''' Get the next random arrangements of Cities
    ''' </summary>
    ''' <param name="order"></param>
    ''' <returns></returns>
    Private Function GetNextArrangement(ByVal order As List(Of Integer)) As List(Of Integer)
        Dim newOrder As New List(Of Integer)

        For i As Integer = 0 To order.Count - 1
            newOrder.Add(order(i))
        Next

        'we will only rearrange two Cities by random
        'starting point should be always zero - so zero should not be included

        Dim firstRandomCityIndex As Integer = random.[Next](1, newOrder.Count)
        Dim secondRandomCityIndex As Integer = random.[Next](1, newOrder.Count)

        Dim dummy As Integer = newOrder(firstRandomCityIndex)
        newOrder(firstRandomCityIndex) = newOrder(secondRandomCityIndex)
        newOrder(secondRandomCityIndex) = dummy

        Return newOrder
    End Function
End Class

