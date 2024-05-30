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
Imports System.Runtime.InteropServices
Imports AddinExpress.RTD
Imports Geodesics
Imports Utilities
''' <summary>
''' RTD (Real Time Data) interface for Excel
''' </summary>
''' <remarks>Handles Connect and RefreshData. Disconnect unused in this application</remarks>
<GuidAttribute("965BA605-2BCD-4AB1-AD77-0785C48429A1"), ProgIdAttribute(GEODESIX_RTD)>
Public Class GeodesiXRTD
    Inherits AddinExpress.RTD.ADXRTDServerModule

    Private WithEvents Topic1 As ADXRTDTopic
    Private WithEvents Topic2 As ADXRTDTopic
    Private WithEvents Topic3 As ADXRTDTopic
    Private WithEvents Topic4 As ADXRTDTopic
    Private WithEvents Topic5 As ADXRTDTopic

#Region " Component Designer generated code. "
    'Required by designer
    Private components As System.ComponentModel.IContainer

    'Required by designer - do not modify
    'the following method
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Topic1 = New AddinExpress.RTD.ADXRTDTopic(Me.components)
        Me.Topic2 = New AddinExpress.RTD.ADXRTDTopic(Me.components)
        Me.Topic3 = New AddinExpress.RTD.ADXRTDTopic(Me.components)
        Me.Topic4 = New AddinExpress.RTD.ADXRTDTopic(Me.components)
        Me.Topic5 = New AddinExpress.RTD.ADXRTDTopic(Me.components)
        '
        'Topic1
        '
        Me.Topic1.CheckOldValue = True
        Me.Topic1.DefaultValue = "Fetching..."
        Me.Topic1.String01 = "*"
        Me.Topic1.Tag = ""
        '
        'Topic2
        '
        Me.Topic2.CheckOldValue = True
        Me.Topic2.DefaultValue = "Fetching..."
        Me.Topic2.String01 = "*"
        Me.Topic2.String02 = "*"
        Me.Topic2.Tag = ""
        '
        'Topic3
        '
        Me.Topic3.CheckOldValue = True
        Me.Topic3.DefaultValue = "Fetching..."
        Me.Topic3.String01 = "*"
        Me.Topic3.String02 = "*"
        Me.Topic3.String03 = "*"
        Me.Topic3.Tag = ""
        '
        'Topic4
        '
        Me.Topic4.CheckOldValue = True
        Me.Topic4.DefaultValue = "Fetching..."
        Me.Topic4.String01 = "*"
        Me.Topic4.String02 = "*"
        Me.Topic4.String03 = "*"
        Me.Topic4.String04 = "*"
        Me.Topic4.Tag = ""
        '
        'Topic5
        '
        Me.Topic5.CheckOldValue = True
        Me.Topic5.DefaultValue = "Fetching..."
        Me.Topic5.String01 = "*"
        Me.Topic5.String02 = "*"
        Me.Topic5.String03 = "*"
        Me.Topic5.String04 = "*"
        Me.Topic5.String05 = "*"
        Me.Topic5.Tag = ""
        '
        'GeodesiXRTD
        '
        Me.Interval = 1000

    End Sub

#End Region

#Region " Add-in Express automatic code "

    'Required by Add-in Express - do not modify
    'the methods within this region

    Public Overrides Function GetContainer() As System.ComponentModel.IContainer
        If components Is Nothing Then
            components = New System.ComponentModel.Container
        End If
        GetContainer = components
    End Function

    <ComRegisterFunctionAttribute()>
    Public Shared Sub RTDServerRegister(ByVal t As Type)
        CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)
        AddinExpress.RTD.ADXRTDServerModule.ADXRTDServerRegister(t)
    End Sub

    <ComUnregisterFunctionAttribute()>
    Public Shared Sub RTDServerUnregister(ByVal t As Type)
        AddinExpress.RTD.ADXRTDServerModule.ADXRTDServerUnregister(t)
    End Sub

#End Region

    Public Sub New()
        ' Don't even THINK about doing anything else here
        MyBase.New()
        InitializeComponent()
        _GeodesixEXL = DirectCast(AddinExpress.MSO.ADXAddinModule.CurrentInstance, GeodesiXEXL)
        CustomLog.Logger.Debug("{0} starts in {1}", GEODESIX_RTD, System.AppDomain.CurrentDomain.FriendlyName)
    End Sub
    Private ReadOnly Property GeodesixEXL As GeodesiXEXL
    ' Topics.
    ' The number of strings (with a '*' in the StringNN template) in a topic matches the number
    ' of arguments in the function being called.
    ' The request 'geocode','status','Geneva' is of type Topic3 (which has String01/02/03 all = '*').
    ' The Travel query is of the form 'directions','distance','Geneva','Lausanne','driving' is of type Topic5

    ' #1# #2#
    Private Sub Topic_Connect(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Topic1.Connect, Topic2.Connect, Topic3.Connect, Topic4.Connect, Topic5.Connect


        Dim service As String = ""
        Dim p1 As String = ""
        Dim p2 As String = ""
        Dim p3 As String = ""
        Dim p4 As String = ""
        Dim result As Object = ""
        Dim query As Query = Nothing

        Try
            Dim topic As ADXRTDTopic = DirectCast(sender, ADXRTDTopic)
            'topic.UseStoredValue = True ' Don't do this, it makes the value momentarily appear as '0'
            'topic.CheckOldValue = True
            service = topic.String01.ToLower
            p1 = topic.String02
            p2 = topic.String03
            p3 = topic.String04
            p4 = topic.String05

            CustomLog.Logger.Debug("Topic_Connect({0},{1})", service, String.Join(",", {p1, p2, p3, p4}))

            Select Case service

                Case "geocode"
                    query = Cache.MakeQuery(Of GeoQuery)(p2)

                Case "directions"
                    query = Cache.MakeQuery(Of TravelQuery)(TravelKey(p2, p3, p4))

                Case Else
                    Throw New ArgumentException("Invalid service " & service)

            End Select

            query.RTDModule = CurrentInstance

            If Not query.ContainsTopic(topic) Then
                query.AddTopic(topic)
            End If

            If Not query.Completed Then
                GeodesixEXL.Geodesic.Run(query)
            End If

        Catch ex As Exception
            HandleError("connecting for " & service & " " & String.Join(",", {p1, p2, p3, p4}), ex)

        End Try

    End Sub
    Dim warned As Boolean = False
    Private Sub RTDServer_OnError(ByVal e As AddinExpress.RTD.ADXErrorEventArgs) _
        Handles Me.OnError

        CustomLog.Logger.Error("RTDServer_OnError {0}", e.ADXError.Message)

        If Not warned Then
            ShowBox("RTD server error: " & e.ADXError.Message, icon:=System.Windows.Forms.MessageBoxIcon.Warning)
            warned = True
        End If

        e.Handled = True

    End Sub

    Private Function Topic_RefreshData(ByVal sender As Object) As Object _
        Handles Topic1.RefreshData, Topic2.RefreshData, Topic3.RefreshData, Topic4.RefreshData, Topic5.RefreshData

        ' #12#, #13#
        Dim service As String = ""
        Dim p1 As String = ""
        Dim p2 As String = ""
        Dim p3 As String = ""
        Dim p4 As String = ""

        Dim result As Object = ""

        Try
            Dim topic As ADXRTDTopic = DirectCast(sender, ADXRTDTopic)
            service = topic.String01
            p1 = topic.String02
            p2 = topic.String03
            p3 = topic.String04
            p4 = topic.String05

            CustomLog.Logger.Debug("Topic_RefreshData({0},{1})", service, String.Join(",", {p1, p2, p3, p4}))

            Select Case service ' #14#

                Case "geocode"
                    Dim query As GeoQuery = Cache.MakeQuery(Of GeoQuery)(p2)
                    result = Geocoded(query, p1, p2)

                Case "directions"
                    Dim query As TravelQuery = Cache.MakeQuery(Of TravelQuery)(TravelKey(p2, p3, p4))
                    result = Traveled(query, p1, p2, p3, p4)

                Case Else
                    Throw New ArgumentException("Invalid service " & service)

            End Select

        Catch ex As Exception
            HandleError("refreshing for " & service & " " & String.Join(",", {p1, p2, p3, p4}), ex)
        End Try

        Return result

    End Function

    'Private Sub GeodesiXRTD_OnRegister(sender As Object, e As EventArgs) Handles Me.OnRegister
    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)
    'End Sub

    'Private Sub GeodesiXRTD_RTDInitialize(sender As Object, e As EventArgs) Handles Me.RTDInitialize
    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)
    'End Sub

    'Private Sub GeodesiXRTD_OnBeforeDisplayAlerts(sender As Object, e As ADXBeforeDisplayEventArgs) Handles Me.OnBeforeDisplayAlerts
    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)
    'End Sub

    'Private Sub GeodesiXRTD_OnSendMessage(sender As Object, e As ADXSendMessageEventArgs) Handles Me.OnSendMessage
    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)
    'End Sub

    'Private Sub GeodesiXRTD_OnUpdaterBeforeStart(sender As Object, e As ADXUpdateStartCancelEventArgs) Handles Me.OnUpdaterBeforeStart
    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)
    'End Sub

    'Private Sub GeodesiXRTD_OnUpdaterStarted(sender As Object, e As EventArgs) Handles Me.OnUpdaterStarted
    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)
    'End Sub

    ' Maybe one day we'll need to do someting when disconnecting
    'Private Sub Topic_Disconnect(ByVal sender As Object, ByVal e As System.EventArgs) _
    '    Handles Topic1.Disconnect, Topic2.Disconnect, Topic3.Disconnect, Topic4.Disconnect, Topic5.Disconnect

    '    If Not MySettings.Tracing Then
    '        Exit Sub
    '    End If

    '    Dim p1 As String = ""
    '    Dim p2 As String = ""
    '    Dim p3 As String = ""
    '    Dim p4 As String = ""
    '    Dim topic As ADXRTDTopic
    '    Dim service As String = ""

    '    Try

    '        topic = DirectCast(sender, ADXRTDTopic)
    '        If topic.String01 IsNot Nothing Then
    '            service = topic.String01
    '        End If
    '        If topic.String02 IsNot Nothing Then
    '            p1 = topic.String02
    '        End If
    '        If topic.String03 IsNot Nothing Then
    '            p2 = topic.String03
    '        End If
    '        If topic.String04 IsNot Nothing Then
    '            p3 = topic.String04
    '        End If
    '        If topic.String05 IsNot Nothing Then
    '            p4 = topic.String05
    '        End If
    '        Logging.CustomLog.Logger.Debug("Topic_Disconnect({0},{1},{2},{3},{4})", service, p1, p2, p3, p4)

    '    Catch ex As Exception
    '        HandleError("disconnecting for " & service & " " & p1 & " " & p2 & " " & p3 & " " & p4, ex)
    '    End Try

    'End Sub

End Class

