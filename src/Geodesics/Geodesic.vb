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
Imports System.Threading
Imports System.Threading.Tasks
Imports Seting
Imports Utilities
''' <summary>
''' Perform geodesic functions using Google Maps API
''' </summary>
''' <remarks>
''' Geodesic functions are initiated with the Find method.
''' 
''' The geocoding itself is performed by an asynchronous task, which works on a queue
''' of requests, raising the events Started and Completed before and after each
''' request and the Idle event when it has no work to do.
''' Started and Idle will usually be used to update the UI.
''' 
''' The Completed event returns a Query object that contains the results.
''' </remarks>

Public Class Geodesic
    ''' <summary>
    ''' Starting is raised just before initiating the conversation with Google
    ''' </summary>
    ''' <param name="query">A Query object, whose status will be "Fetching.."</param>
    ''' <remarks></remarks>
    Public Event Starting(ByVal query As Query)
    ''' <summary>
    ''' Completed is raised when the results are ready
    ''' </summary>
    ''' <param name="query">A Query object with all the geocoding information</param>
    ''' <remarks></remarks>
    Public Event Completed(ByVal query As Query)

    Private _Language As String = ""
    Private _Region As String = ""
    Private Delegate Sub GeodesicCompletedCallback(ByVal QueryCompleted As Query)
    Public Sub New()
        _Language = Settings.Language
        _Region = Settings.Region
    End Sub
    Private _Context As SynchronizationContext
    Private ReadOnly Property Context As SynchronizationContext
        Get
            If _Context Is Nothing Then
                _Context = SynchronizationContext.Current
            End If
            Return _Context
        End Get
    End Property
    Public ReadOnly Property Here As New Here
    Public Property Language As String
        Get
            Return _Language
        End Get
        Set(ByVal value As String)
            _Language = value
        End Set
    End Property
    Public Property Region As String
        Get
            Return _Region
        End Get
        Set(ByVal value As String)
            _Region = value
            If _Region.Length > 2 Then
                _Region = _Region.Substring(0, 2)
            End If
        End Set
    End Property
    Public Sub Run(query As Query)

        CustomLog.Logger.Debug("Geodesic run {0}", query)

        ' #4#
        If Context Is Nothing Then
            Throw New NullReferenceException("Context not available!")
        End If
        query.Status = "Queued"
        query.Semaphore.Set()
        RaiseEvent Starting(query)

        Task.Run(Sub() RunAsync(query))

    End Sub
    Private Sub RunAsync(query As Query)

        Try ' This sometimes fails when Excel is shutting down
            query.Status = "Resolving"
            Resolve(query)
            Dim cbd As New GeodesicCompletedCallback(AddressOf QueryCompleted)
            Context.Post(Function() cbd.DynamicInvoke(query), Nothing)

        Catch ex As Exception
        End Try
    End Sub
    Private Sub QueryCompleted(query As Query)

        Try ' This sometimes fails when Excel is shutting down
            query.Completed = True

            RaiseEvent Completed(query)
        Catch ex As Exception
        End Try
    End Sub
End Class
