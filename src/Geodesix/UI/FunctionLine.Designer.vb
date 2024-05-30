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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FunctionLine
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblEquals = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.SystemColors.Control
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(3, 2)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(70, 13)
        Me.txtName.TabIndex = 1
        Me.txtName.TabStop = False
        Me.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblEquals
        '
        Me.lblEquals.AutoSize = True
        Me.lblEquals.Location = New System.Drawing.Point(214, 3)
        Me.lblEquals.Name = "lblEquals"
        Me.lblEquals.Size = New System.Drawing.Size(13, 13)
        Me.lblEquals.TabIndex = 6
        Me.lblEquals.Text = "="
        '
        'txtValue
        '
        Me.txtValue.BackColor = System.Drawing.SystemColors.Control
        Me.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtValue.Location = New System.Drawing.Point(228, 3)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = True
        Me.txtValue.Size = New System.Drawing.Size(211, 13)
        Me.txtValue.TabIndex = 8
        Me.txtValue.TabStop = False
        '
        'txtSource
        '
        Me.txtSource.BackColor = System.Drawing.SystemColors.Window
        Me.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSource.Location = New System.Drawing.Point(77, 2)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(97, 20)
        Me.txtSource.TabIndex = 9
        Me.txtSource.Tag = "Source"
        '
        'txtType
        '
        Me.txtType.BackColor = System.Drawing.SystemColors.Control
        Me.txtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtType.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtType.Location = New System.Drawing.Point(179, 4)
        Me.txtType.Name = "txtType"
        Me.txtType.ReadOnly = True
        Me.txtType.Size = New System.Drawing.Size(35, 10)
        Me.txtType.TabIndex = 11
        Me.txtType.TabStop = False
        Me.txtType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'FunctionLine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtType)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.lblEquals)
        Me.Name = "FunctionLine"
        Me.Size = New System.Drawing.Size(440, 24)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblEquals As System.Windows.Forms.Label
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents txtType As System.Windows.Forms.TextBox
End Class
