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
Partial Class Browser
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Browser))
        Me.WebView1 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnHome = New System.Windows.Forms.PictureBox()
        Me.btnForward = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.WebView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHome, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnForward, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WebView1
        '
        Me.WebView1.AllowExternalDrop = True
        Me.WebView1.CreationProperties = Nothing
        Me.WebView1.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebView1.Location = New System.Drawing.Point(0, 0)
        Me.WebView1.Name = "WebView1"
        Me.WebView1.Size = New System.Drawing.Size(834, 711)
        Me.WebView1.TabIndex = 3
        Me.WebView1.ZoomFactor = 1.0R
        '
        'btnHome
        '
        Me.btnHome.BackColor = System.Drawing.Color.White
        Me.btnHome.Image = Global.GeodesiX.My.Resources.Resources.house_narrow
        Me.btnHome.Location = New System.Drawing.Point(63, 1)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Size = New System.Drawing.Size(24, 24)
        Me.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnHome.TabIndex = 1
        Me.btnHome.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnHome, "Go home")
        '
        'btnForward
        '
        Me.btnForward.BackColor = System.Drawing.Color.White
        Me.btnForward.Image = Global.GeodesiX.My.Resources.Resources.forward
        Me.btnForward.Location = New System.Drawing.Point(33, 1)
        Me.btnForward.Name = "btnForward"
        Me.btnForward.Size = New System.Drawing.Size(24, 24)
        Me.btnForward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnForward.TabIndex = 2
        Me.btnForward.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnForward, "Go forward")
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.White
        Me.btnBack.Image = Global.GeodesiX.My.Resources.Resources.back
        Me.btnBack.Location = New System.Drawing.Point(3, 1)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(24, 24)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnBack.TabIndex = 0
        Me.btnBack.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnBack, "Go back")
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(111, 112)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Browser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(834, 711)
        Me.Controls.Add(Me.btnHome)
        Me.Controls.Add(Me.btnForward)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.WebView1)
        Me.Controls.Add(Me.btnClose)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Browser"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Geodesix Help"
        Me.TopMost = True
        CType(Me.WebView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHome, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnForward, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnBack As System.Windows.Forms.PictureBox
    Friend WithEvents btnHome As System.Windows.Forms.PictureBox
    Friend WithEvents btnForward As System.Windows.Forms.PictureBox
    Friend WithEvents WebView1 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
