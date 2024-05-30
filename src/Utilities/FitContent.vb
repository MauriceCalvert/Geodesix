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
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Public Module FitContent_

    <Extension>
    Public Sub FitContent(tb As TextBox)

        Dim size As Size = TextRenderer.MeasureText(tb.Text, tb.Font)
        tb.Width = Math.Max(100, Math.Min(300, size.Width))

    End Sub

    <Extension>
    Public Sub FitContent(tb As ToolStripTextBox)

        Dim size As Size = TextRenderer.MeasureText(tb.Text, tb.Font)
        tb.Width = Math.Max(100, Math.Min(250, size.Width + 10))

    End Sub

    <Extension>
    Public Sub FitContent(tb As ComboBox)

        Dim width As Integer = TextRenderer.MeasureText(tb.Text, tb.Font).Width

        Dim itemwidth As Integer = tb.
                                   Items.
                                   OfType(Of Object).
                                   Select(Function(q) TextRenderer.MeasureText(tb.Text, tb.Font).Width).
                                   Max

        width = Math.Max(width, itemwidth) + 15
        tb.Width = Math.Max(100, Math.Min(300, width))

    End Sub

End Module
