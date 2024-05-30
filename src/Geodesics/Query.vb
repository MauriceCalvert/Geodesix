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
Imports System.Linq
Imports System.Threading
Imports AddinExpress.RTD
Imports Utilities

''' <summary>
''' A geocoding request and its results
''' </summary>
''' <remarks></remarks>

Public MustInherit Class Query

    MustOverride Sub Parse(json As Object, here As Here)
    Public Overridable Property Key As String
    Friend Overridable Property Fields As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
    Public Property RTDModule As ADXRTDServerModule
    Public Semaphore As New ManualResetEventSlim(False)
    ''' <summary>
    ''' The RTD topics which are attached to this query
    ''' </summary>
    ''' <remarks>A single geocode query returns many values (Lat, Long, Address, etc.). For a Directions query
    ''' there is Distance and Duration. This list is the topics which are affected by the query</remarks>
    Public ReadOnly Property Topics() As New List(Of AddinExpress.RTD.ADXRTDTopic)

    Public Sub New()
    End Sub
    Public Sub New(key As String)
        Me.Key = key
    End Sub
    Public Sub AddTopic(ByVal Topic As AddinExpress.RTD.ADXRTDTopic)
        Topics.Add(Topic)
    End Sub
    Private _Completed As Boolean
    Public Property Completed As Boolean ' This query is ready to be used (geocoded and analysed by GeodesicCompletedd)
        Get
            Return _Completed
        End Get
        Set(value As Boolean)

            Debug.Assert(value, "Query completed cannot be cleared")
            _Completed = value

            CustomLog.Logger.Debug("Updating {0} topics for {1}", Topics.Count, Me)
            RTDModule?.UpdateTopics(Topics.ToArray)
        End Set
    End Property
    Private ReadOnly Property TopicComparer As New TopicComparer
    Public ReadOnly Property ContainsTopic(ByVal topic As AddinExpress.RTD.ADXRTDTopic) As Boolean
        Get
            If topic Is Nothing Then
                Return False
            End If
            Return Topics.Contains(topic, TopicComparer)
        End Get
    End Property
    Public Property Field(key As String) As Object
        Get
            Wait("get")
            Dim result As Object = ""
            Fields.TryGetValue(key, result)
            Return result
        End Get
        Set(value As Object)
            Wait("set")
            IfNotEmpty(value, Sub() Fields(key) = value)
        End Set
    End Property
    Public Property Status As String
    Public Overrides Function ToString() As String

        Return $"{Me.GetType.Name}({Key}) fields {Fields.Count} topics {Topics.Count} status {Status} completed {Completed} in {Workbook}"

    End Function
    Private Sub Wait(action As String)

#If DEBUG Then
        Dim sw As Stopwatch = Nothing
        If Not Semaphore.IsSet Then
            CustomLog.Logger.Debug("Query.{0}({1}).Wait", action, Key)
            sw = New Stopwatch
            sw.Start()
        End If
#End If

        Semaphore.Wait()

#If DEBUG Then
        If sw IsNot Nothing Then
            sw.Stop()
            CustomLog.Logger.Debug("Query.{0}({1}).Waited {2} mS", action, Key, sw.ElapsedMilliseconds)
        End If
#End If

    End Sub
    Public Property Workbook As String
End Class
