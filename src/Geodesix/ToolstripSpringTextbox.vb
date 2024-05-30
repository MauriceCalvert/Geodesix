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
Public Class ToolStripSpringTextBox
    Inherits ToolStripTextBox

    Public Overrides Function GetPreferredSize(
        ByVal constrainingSize As Size) As Size

        ' Use the default size if the text box is on the overflow menu
        ' or is on a vertical ToolStrip.
        If IsOnOverflow Or Owner.Orientation = Orientation.Vertical Then
            Return DefaultSize
        End If

        ' Declare a variable to store the total available width as 
        ' it is calculated, starting with the display width of the 
        ' owning ToolStrip.
        Dim width As Int32 = Owner.DisplayRectangle.Width

        ' Subtract the width of the overflow button if it is displayed. 
        If Owner.OverflowButton.Visible Then
            width = width - Owner.OverflowButton.Width -
                Owner.OverflowButton.Margin.Horizontal()
        End If

        ' Declare a variable to maintain a count of ToolStripSpringTextBox 
        ' items currently displayed in the owning ToolStrip. 
        Dim springBoxCount As Int32 = 0

        For Each item As ToolStripItem In Owner.Items

            ' Ignore items on the overflow menu.
            If item.IsOnOverflow Then Continue For

            If TypeOf item Is ToolStripSpringTextBox Then
                ' For ToolStripSpringTextBox items, increment the count and 
                ' subtract the margin width from the total available width.
                springBoxCount += 1
                width -= item.Margin.Horizontal
            Else
                ' For all other items, subtract the full width from the total
                ' available width.
                width = width - item.Width - item.Margin.Horizontal
            End If
        Next

        ' If there are multiple ToolStripSpringTextBox items in the owning
        ' ToolStrip, divide the total available width between them. 
        If springBoxCount > 1 Then width = CInt(width / springBoxCount)

        ' If the available width is less than the default width, use the
        ' default width, forcing one or more items onto the overflow menu.
        If width < DefaultSize.Width Then width = DefaultSize.Width

        ' Retrieve the preferred size from the base class, but change the
        ' width to the calculated width. 
        Dim preferredSize As Size = MyBase.GetPreferredSize(constrainingSize)
        preferredSize.Width = width
        Return preferredSize

    End Function
End Class