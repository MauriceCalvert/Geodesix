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
Imports AddinExpress.XL

''' <summary>
''' Wrapper to ease using Office CustomProperties
''' </summary>
Public Class WorksheetProperties

    Private ReadOnly Property Props As Object
    Private ReadOnly Property PropsType As Type

    Public Sub New(Worksheet As Object)

        Try ' No CustomProperties on protected / readonly / downloaded Workbooks
            Me.Props = Worksheet.CustomProperties
            Me.PropsType = Props.GetType
        Catch ex As Exception
        End Try

    End Sub
    Public Property Display As Boolean
        Get
            Dim result As String = GetProp("display")
            Return result = "1"
        End Get
        Set(value As Boolean)
            If value Then
                SetProp("display", "1")
            Else
                SetProp("display", "0")
            End If
        End Set
    End Property
    Public ReadOnly Property ID As Guid
        Get
            Dim guids As String = GetProp("id")
            Dim result As Guid

            If guids = "" Then
                result = Guid.NewGuid
                SetProp("id", result.ToString)

            ElseIf Not Guid.TryParse(guids, result) Then
                result = Guid.NewGuid
                SetProp("id", result.ToString)
            End If
            Return result
        End Get
    End Property
    Default Public Property Item(key As String) As String
        Get
            Return GetProp(key)
        End Get
        Set(value As String)
            SetProp(key, value)
        End Set
    End Property
    Public Property MapStyle As String
        Get
            Dim result As String = Item("mapstyle")
            If result = "" Then
                result = "hybrid"
            End If
            Return result
        End Get
        Set(value As String)
            Item("mapstyle") = value
        End Set
    End Property
    Public Property PaneHeight As Integer
        Get
            Dim height As String = Item("PaneHeight")
            Dim result As Integer = 0
            Integer.TryParse(height, result)
            Return result
        End Get
        Set(value As Integer)
            Item("PaneHeight") = value.ToString
        End Set
    End Property
    Public Property PaneWidth As Integer
        Get
            Dim width As String = Item("PaneWidth")
            Dim result As Integer = 0
            Integer.TryParse(width, result)
            Return result
        End Get
        Set(value As Integer)
            Item("PaneWidth") = value.ToString
        End Set
    End Property
    Public Property Zoom As Integer
        Get
            Dim zs As String = Item("Zoom")
            Dim result As Integer = 0
            Integer.TryParse(zs, result)
            Return result
        End Get
        Set(value As Integer)
            Item("Zoom") = value.ToString
        End Set
    End Property
    Public Property TaskPanePosition As ADXExcelTaskPanePosition
        Get
            Dim results As String = Item("position")
            If results = "" Then
                results = "r"
            End If
            Dim result As ADXExcelTaskPanePosition

            Select Case results.ToLower.Substring(0, 1)
                Case "t"
                    result = ADXExcelTaskPanePosition.Top
                Case "b"
                    result = ADXExcelTaskPanePosition.Bottom
                Case "l"
                    result = ADXExcelTaskPanePosition.Left
                Case Else
                    result = ADXExcelTaskPanePosition.Right
            End Select
            Return result
        End Get
        Set(value As ADXExcelTaskPanePosition)
            Select Case value
                Case ADXExcelTaskPanePosition.Top
                    Item("position") = "t"
                Case ADXExcelTaskPanePosition.Bottom
                    Item("position") = "b"
                Case ADXExcelTaskPanePosition.Left
                    Item("position") = "l"
                Case Else
                    Item("position") = "r"
            End Select
        End Set
    End Property

    Private Function GetProp(key As String) As String

        If Props Is Nothing Then
            Return ""
        End If

        Dim propscount As Integer = CInt(PropsType.InvokeMember("Count", BindingFlags.GetProperty Or BindingFlags.[Default], Nothing, Props, New Object() {}))

        For counter As Integer = 1 To propscount

            Dim propitem As Object = PropsType.InvokeMember("Item", BindingFlags.GetProperty Or BindingFlags.[Default], Nothing, Props, New Object() {counter})
            Dim propname As String = CStr(PropsType.InvokeMember("Name", BindingFlags.GetProperty Or BindingFlags.[Default], Nothing, propitem, New Object() {}))

            If key = propname Then
                Dim proptype As Type = propitem.GetType
                Dim result As String = CStr(proptype.InvokeMember("Value", BindingFlags.GetProperty Or BindingFlags.Default, Nothing, propitem, New Object() {}))
                Return result
            End If
        Next
        Return ""
    End Function
    Private Sub SetProp(key As String, value As String)

        If Props Is Nothing Then
            Exit Sub
        End If

        Dim propscount As Integer = CInt(PropsType.InvokeMember("Count", BindingFlags.GetProperty Or BindingFlags.[Default], Nothing, Props, New Object() {}))

        For counter As Integer = 1 To propscount

            Dim propitem As Object = PropsType.InvokeMember("Item", BindingFlags.GetProperty Or BindingFlags.[Default], Nothing, Props, New Object() {counter})
            Dim propname As String = CStr(PropsType.InvokeMember("Name", BindingFlags.GetProperty Or BindingFlags.[Default], Nothing, propitem, New Object() {}))

            If key = propname Then
                Dim proptype As Type = propitem.GetType
                proptype.InvokeMember("Value", BindingFlags.SetProperty Or BindingFlags.Default, Nothing, propitem, New Object() {value})
                Return
            End If
        Next
        Dim args As Object() = {key, value}
        PropsType.InvokeMember("Add", BindingFlags.Default Or BindingFlags.InvokeMethod, Nothing, Props, args)
    End Sub

End Class
