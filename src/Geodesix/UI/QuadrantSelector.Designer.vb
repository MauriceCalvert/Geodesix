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
Partial Class QuadrantSelector
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QuadrantSelector))
        Me.AtBottom = New System.Windows.Forms.PictureBox()
        Me.AtRight = New System.Windows.Forms.PictureBox()
        Me.AtLeft = New System.Windows.Forms.PictureBox()
        Me.AtTop = New System.Windows.Forms.PictureBox()
        Me.Back = New System.Windows.Forms.PictureBox()
        Me.txtPosition = New System.Windows.Forms.TextBox()
        CType(Me.AtBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AtRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AtLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AtTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Back, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AtBottom
        '
        Me.AtBottom.BackColor = System.Drawing.Color.Transparent
        Me.AtBottom.Image = CType(resources.GetObject("AtBottom.Image"), System.Drawing.Image)
        Me.AtBottom.Location = New System.Drawing.Point(5, 25)
        Me.AtBottom.Name = "AtBottom"
        Me.AtBottom.Size = New System.Drawing.Size(50, 10)
        Me.AtBottom.TabIndex = 7
        Me.AtBottom.TabStop = False
        Me.AtBottom.Visible = False
        '
        'AtRight
        '
        Me.AtRight.BackColor = System.Drawing.Color.Transparent
        Me.AtRight.Image = CType(resources.GetObject("AtRight.Image"), System.Drawing.Image)
        Me.AtRight.Location = New System.Drawing.Point(45, 5)
        Me.AtRight.Name = "AtRight"
        Me.AtRight.Size = New System.Drawing.Size(10, 30)
        Me.AtRight.TabIndex = 6
        Me.AtRight.TabStop = False
        Me.AtRight.Visible = False
        '
        'AtLeft
        '
        Me.AtLeft.BackColor = System.Drawing.Color.Transparent
        Me.AtLeft.Image = CType(resources.GetObject("AtLeft.Image"), System.Drawing.Image)
        Me.AtLeft.Location = New System.Drawing.Point(5, 5)
        Me.AtLeft.Name = "AtLeft"
        Me.AtLeft.Size = New System.Drawing.Size(10, 30)
        Me.AtLeft.TabIndex = 5
        Me.AtLeft.TabStop = False
        Me.AtLeft.Visible = False
        '
        'AtTop
        '
        Me.AtTop.BackColor = System.Drawing.Color.Transparent
        Me.AtTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.AtTop.Image = CType(resources.GetObject("AtTop.Image"), System.Drawing.Image)
        Me.AtTop.Location = New System.Drawing.Point(5, 5)
        Me.AtTop.Name = "AtTop"
        Me.AtTop.Size = New System.Drawing.Size(50, 10)
        Me.AtTop.TabIndex = 4
        Me.AtTop.TabStop = False
        Me.AtTop.Visible = False
        '
        'Back
        '
        Me.Back.Image = CType(resources.GetObject("Back.Image"), System.Drawing.Image)
        Me.Back.Location = New System.Drawing.Point(0, 0)
        Me.Back.Margin = New System.Windows.Forms.Padding(0)
        Me.Back.Name = "Back"
        Me.Back.Size = New System.Drawing.Size(60, 40)
        Me.Back.TabIndex = 8
        Me.Back.TabStop = False
        '
        'txtPosition
        '
        Me.txtPosition.BackColor = System.Drawing.Color.White
        Me.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPosition.CausesValidation = False
        Me.txtPosition.Cursor = System.Windows.Forms.Cursors.Default
        Me.txtPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPosition.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.txtPosition.Location = New System.Drawing.Point(16, 16)
        Me.txtPosition.Margin = New System.Windows.Forms.Padding(0)
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.ReadOnly = True
        Me.txtPosition.ShortcutsEnabled = False
        Me.txtPosition.Size = New System.Drawing.Size(28, 8)
        Me.txtPosition.TabIndex = 9
        Me.txtPosition.TabStop = False
        Me.txtPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtPosition.WordWrap = False
        '
        'QuadrantSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(60, 40)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtPosition)
        Me.Controls.Add(Me.AtRight)
        Me.Controls.Add(Me.AtLeft)
        Me.Controls.Add(Me.AtBottom)
        Me.Controls.Add(Me.AtTop)
        Me.Controls.Add(Me.Back)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QuadrantSelector"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        CType(Me.AtBottom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AtRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AtLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AtTop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Back, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AtBottom As System.Windows.Forms.PictureBox
    Friend WithEvents AtRight As System.Windows.Forms.PictureBox
    Friend WithEvents AtLeft As System.Windows.Forms.PictureBox
    Friend WithEvents AtTop As System.Windows.Forms.PictureBox
    Private WithEvents Back As System.Windows.Forms.PictureBox
    Private WithEvents txtPosition As System.Windows.Forms.TextBox
End Class
