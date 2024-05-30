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

Public Class WorkbookProperties

    ''' <summary>
    ''' Wrapper to ease using Office CustomDocumentProperties
    ''' </summary>
    Private ReadOnly Property WorkBook As Object
    Private ReadOnly Property Props As Object
    Private ReadOnly Property PropsType As Type

    Public Sub New(WorkBook As Object)

        Me.WorkBook = WorkBook
        Me.Props = WorkBook.CustomDocumentProperties
        Me.PropsType = Props.GetType

    End Sub
    Public Sub Delete(key As String)

        SetProp(key, "")

    End Sub
    Public ReadOnly Property Guids As String
        Get
            Dim result As String = GetProp("Guid")
            If result = "" OrElse result = Guid.Empty.ToString Then
                Dim gid As Guid = Guid.NewGuid
                result = gid.ToString
                SetProp("Guid", result)
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
    Private Function GetProp(key As String) As String

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
        ' msoPropertyTypeString = 4
        Dim args As Object() = {key, False, 4, value}
        PropsType.InvokeMember("Add", BindingFlags.Default Or BindingFlags.InvokeMethod, Nothing, Props, args)

    End Sub

End Class