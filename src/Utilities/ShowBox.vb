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
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Module Msgs_
    ''' <summary>
    ''' Drop-in replacement for ShowBox, but that ALWAYS displays as the topmost window (unhideable)
    ''' </summary>
    ''' <param name="prompt">Text of message to display</param>
    ''' <param name="title">Title text</param>
    ''' <param name="buttons">Buttons to display</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ShowBox(ByVal prompt As String,
                            Optional ByVal title As String = "GeodesiX says",
                            Optional ByVal buttons As MessageBoxButtons = MessageBoxButtons.OK,
                            Optional ByVal icon As MessageBoxIcon = MessageBoxIcon.Information) As DialogResult

        Dim result As DialogResult
        Using f As New Form With {.TopMost = True}
            result = MessageBox.Show(f, prompt, title, buttons)
        End Using

        Return result
    End Function
    Public Sub ShowError(prompt As String, Optional title As String = "Something needs fixing!")

        ShowBox(prompt, title, icon:=MessageBoxIcon.Error)

    End Sub
    Public Sub ShowInfo(prompt As String, Optional title As String = "FYI")

        ShowBox(prompt, title, icon:=MessageBoxIcon.Information)

    End Sub
    Public Sub ShowWarning(prompt As String, Optional title As String = "Oops!")

        ShowBox(prompt, title, icon:=MessageBoxIcon.Warning)

    End Sub
    Public Function AreYouSure(prompt As String) As Boolean

        Return ShowBox(prompt, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes

    End Function

End Module