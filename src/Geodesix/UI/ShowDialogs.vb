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
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports Seting
Imports System.Linq
Imports Utilities
Imports System.Diagnostics
Public Module ShowDialogs_

    Private Function GetPositions() As Dictionary(Of String, Rectangle)

        Dim positions As Dictionary(Of String, Rectangle) = Nothing
        Try
            positions = JsonConvert.DeserializeObject(Of Dictionary(Of String, Rectangle))(Settings.Frames)
        Catch ex As Exception
        End Try

        If IsEmpty(positions) Then
            positions = New Dictionary(Of String, Rectangle)
        End If

        Return positions

    End Function
    Public Function ShowCommonDialog(f As CommonDialog) As DialogResult

        Dim mainhandle As IntPtr = Process.GetCurrentProcess.MainWindowHandle
        Dim natwin As New NativeWindow
        natwin.AssignHandle(mainhandle)
        Dim result As DialogResult = f.ShowDialog(natwin)

        Return result
    End Function
    Private Sub SavePosition(f As Object, e As EventArgs)

        Dim ff As Form = DirectCast(f, Form)
        If e IsNot Nothing Then
            RemoveHandler ff.FormClosing, AddressOf SavePosition
        End If
        Dim rect As New Rectangle(ff.Location, ff.Size)
        Dim positions As Dictionary(Of String, Rectangle) = GetPositions()
        positions(ff.Name) = rect
        Settings.Frames = JsonConvert.SerializeObject(positions)

    End Sub
    Public Function ShowForm(f As Form) As DialogResult

        Return ShowPositioned(f, Function(nw As NativeWindow)
                                     AddHandler f.FormClosing, AddressOf SavePosition
                                     f.Show(nw)
                                     Return DialogResult.OK
                                 End Function)

    End Function
    Public Function ShowFormDialog(f As Form) As DialogResult

        Return ShowPositioned(f, Function(nw As NativeWindow) f.ShowDialog(nw))

    End Function
    Public Function ShowPositioned(f As Form, showit As Func(Of NativeWindow, DialogResult)) As DialogResult

        ' Show the form as a dialog in the middle of the Excel window

        Dim mainhandle As IntPtr = Process.GetCurrentProcess.MainWindowHandle
        Dim natwin As New NativeWindow
        natwin.AssignHandle(mainhandle)

        ' Restore form position to last used
        Dim positions As Dictionary(Of String, Rectangle) = GetPositions()
        Dim rect As Rectangle

        f.StartPosition = FormStartPosition.CenterParent

        If positions.TryGetValue(f.Name, rect) Then

            Dim screensize As Size =
                Screen.
                AllScreens.
                Select(Function(q) q.Bounds).
                Aggregate(Function(x, y) Rectangle.Union(x, y)).
                Size

            If New Rectangle(New Point(0, 0), screensize).Contains(rect) Then
                f.StartPosition = FormStartPosition.Manual
                f.Location = rect.Location
                f.Size = rect.Size
            End If
        End If

        f.BringToFront()

        Dim result As DialogResult = showit(natwin)

        SavePosition(f, Nothing)

        Return result

    End Function


End Module
