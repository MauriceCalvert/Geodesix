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
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

<ComVisible(False)>
Public Class SettingsUI
    Inherits Form

    Private Settings As SettingsBase
    Public Sub New(settings As SettingsBase)

        InitializeComponent()
        Me.Settings = settings

    End Sub
    Private Sub SettingsUI_Load(sender As Object, e As EventArgs) Handles Me.Load

        Draw()

    End Sub
    Private Sub Draw()

        Const PAD As Integer = 6
        Dim x As Integer = PAD
        Dim y As Integer = PAD
        Dim lineheight As Integer = 20
        Dim showhidden As Boolean = False
#If DEBUG Then
        showhidden = True
#End If
        Dim visibleitems As List(Of Setting) = Settings.Items.Values.Where(Function(q) showhidden OrElse Not q.Hidden).ToList

        For Each setting As Setting In visibleitems

            Dim line As New SettingLine(setting)
            Panel1.Controls.Add(line)
            line.Left = x
            line.Top = y
            line.Visible = True
            lineheight = line.Height
            y += line.Height - 2
        Next

        Dim widestname As Integer = 0
        Dim widestvalue As Integer = 0
        Dim widesthelp As Integer = 0

        For Each pl As SettingLine In Panel1.Controls.OfType(Of SettingLine)
            widestname = Max(widestname, pl.txtName.Width)
            widestvalue = Max(widestvalue, pl.ValueControl.Width)
            widesthelp = Max(widesthelp, pl.txtDescription.Width)
        Next
        widestvalue = Min(100, widestvalue)
        widesthelp = Min(300, widesthelp)

        For Each pl As SettingLine In Panel1.Controls.OfType(Of SettingLine)
            pl.txtName.Width = widestname + PAD
            pl.ValueControl.Left = widestname + 2 * PAD
            pl.ValueControl.Width = widestvalue + PAD
            pl.txtDescription.Left = widestname + widestvalue + 4 * PAD
        Next

        Dim panelheight As Integer = visibleitems.Count * (lineheight - 2) + btnClose.Height + PAD
        Dim clientwidth As Integer = widestname + widestvalue + 4 * PAD + widesthelp + PAD
        ClientSize = New Drawing.Size(clientwidth, panelheight + StatusStrip1.Height)

        SettingsUI_ResizeEnd(Me, Nothing)

    End Sub

    Private Sub SettingsUI_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd

        btnClose.Left = ClientSize.Width - btnClose.Width - 5
        btnClose.Top = StatusStrip1.Top - btnClose.Height - 1
        btnReset.Top = btnClose.Top

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        Settings.Reset()

    End Sub
End Class
