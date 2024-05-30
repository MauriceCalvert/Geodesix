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
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

<ComVisible(False)>
Public Class QuadrantSelector

    ' Ghastly hack to allow the form to be narrower than the widows-imposed limit (about 132 in WIndows 7)
    ' Thanks to http://stackoverflow.com/questions/992352/overcome-os-imposed-windows-form-minimum-size-limit

    Private Const WM_WINDOWPOSCHANGING As Integer = &H46
    Private Const WM_GETMINMAXINFO As Integer = &H24
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_WINDOWPOSCHANGING Then
            Dim windowPos As WindowPos = CType(m.GetLParam(GetType(WindowPos)), WindowPos)

            ' Make changes to windowPos

            ' Then marshal the changes back to the message
            Marshal.StructureToPtr(windowPos, m.LParam, True)
        End If

        MyBase.WndProc(m)

        ' Make changes to WM_GETMINMAXINFO after it has been handled by the underlying
        ' WndProc, so we only need to repopulate the minimum size constraints
        If m.Msg = WM_GETMINMAXINFO Then
            Dim minMaxInfo As MINMAXINFO = DirectCast(m.GetLParam(GetType(MINMAXINFO)), MINMAXINFO)
            minMaxInfo.ptMinTrackSize.X = Me.MinimumSize.Width
            minMaxInfo.ptMinTrackSize.Y = Me.MinimumSize.Height
            Marshal.StructureToPtr(minMaxInfo, m.LParam, True)
        End If
    End Sub

    Private Structure WindowPos
        Public hwnd As IntPtr
        Public hwndInsertAfter As IntPtr
        Public x As Integer
        Public y As Integer
        Public width As Integer
        Public height As Integer
        Public flags As UInteger
    End Structure
    <StructLayout(LayoutKind.Sequential)>
    Private Structure MINMAXINFO
        Dim ptReserved As Point
        Dim ptMaxSize As Point
        Dim ptMaxPosition As Point
        Dim ptMinTrackSize As Point
        Dim ptMaxTrackSize As Point
    End Structure

    ' end of ghastly hack   

    Public selected As String = ""
    Public Event Chosen(ByVal quadrant As String)
    Dim startpos As String

    Sub New(ByVal current As String)

        ' This call is required by the designer.
        InitializeComponent()

        startpos = current
        SetCurrent()
        Me.Width = 60
        Me.Height = 40
    End Sub
    Private Sub SetCurrent()
        txtPosition.Text = startpos
        Select Case startpos
            Case "Top"
                AtTop.Visible = True
            Case "Right"
                AtRight.Visible = True
            Case "Bottom"
                AtBottom.Visible = True
            Case "Left"
                AtLeft.Visible = True
        End Select
    End Sub
    Private Sub AtTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AtTop.Click
        RaiseEvent Chosen("Top")
    End Sub
    Private Sub AtTop_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AtTop.MouseMove
        Switch(e.X, e.Y)
    End Sub
    Private Sub AtRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AtRight.Click
        RaiseEvent Chosen("Right")
    End Sub
    Private Sub AtRight_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AtRight.MouseMove
        Switch(e.X + AtRight.Left, e.Y)
    End Sub
    Private Sub AtBottom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AtBottom.Click
        RaiseEvent Chosen("Bottom")
    End Sub
    Private Sub AtBottom_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AtBottom.MouseMove
        Switch(e.X, e.Y + AtBottom.Top)
    End Sub
    Private Sub AtLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AtLeft.Click
        RaiseEvent Chosen("Left")
    End Sub
    Private Sub AtLeft_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AtLeft.MouseMove
        Switch(e.X, e.Y)
    End Sub
    Private Sub Back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Back.Click
        RaiseEvent Chosen("")
    End Sub
    Private Sub Back_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Back.MouseMove
        Switch(e.X, e.Y)
    End Sub
    Private Sub Switch(ByVal x As Integer, ByVal y As Integer)
        If y < 10 AndAlso x >= 10 AndAlso x <= 50 Then ' top
            AtTop.Visible = True
            AtLeft.Visible = False
            AtRight.Visible = False
            AtBottom.Visible = False
            txtPosition.Text = "Top"
        ElseIf x >= 50 AndAlso y >= 10 AndAlso y <= 30 Then ' right
            AtTop.Visible = False
            AtLeft.Visible = False
            AtRight.Visible = True
            AtBottom.Visible = False
            txtPosition.Text = "Right"
        ElseIf y > 30 AndAlso x >= 10 AndAlso x <= 50 Then ' bottom
            AtTop.Visible = False
            AtLeft.Visible = False
            AtRight.Visible = False
            AtBottom.Visible = True
            txtPosition.Text = "Bottom"
        ElseIf x < 10 AndAlso y >= 10 AndAlso y <= 30 Then ' left 
            AtTop.Visible = False
            AtLeft.Visible = True
            AtRight.Visible = False
            AtBottom.Visible = False
            txtPosition.Text = "Left"
        Else ' middle
            AtTop.Visible = False
            AtLeft.Visible = False
            AtRight.Visible = False
            AtBottom.Visible = False
            SetCurrent()
            txtPosition.Text = "Same"
        End If
    End Sub

End Class