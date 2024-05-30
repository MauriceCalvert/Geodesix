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
Partial Class Importer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Importer))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbSingle = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.btnBrowseFile = New System.Windows.Forms.Button()
        Me.rbFolder = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFolder = New System.Windows.Forms.TextBox()
        Me.btnBrowseFolder = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbFilter = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkCluster = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtClusterSize = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.btnSamples = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSamples)
        Me.GroupBox1.Controls.Add(Me.rbSingle)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtFile)
        Me.GroupBox1.Controls.Add(Me.btnBrowseFile)
        Me.GroupBox1.Controls.Add(Me.rbFolder)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtFolder)
        Me.GroupBox1.Controls.Add(Me.btnBrowseFolder)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbFilter)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(488, 167)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Source"
        '
        'rbSingle
        '
        Me.rbSingle.AutoSize = True
        Me.rbSingle.Location = New System.Drawing.Point(16, 23)
        Me.rbSingle.Name = "rbSingle"
        Me.rbSingle.Size = New System.Drawing.Size(93, 17)
        Me.rbSingle.TabIndex = 0
        Me.rbSingle.Text = "Import a single"
        Me.rbSingle.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(46, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "file"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(75, 46)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(319, 20)
        Me.txtFile.TabIndex = 2
        '
        'btnBrowseFile
        '
        Me.btnBrowseFile.Location = New System.Drawing.Point(400, 44)
        Me.btnBrowseFile.Name = "btnBrowseFile"
        Me.btnBrowseFile.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseFile.TabIndex = 6
        Me.btnBrowseFile.Text = "Browse..."
        Me.btnBrowseFile.UseVisualStyleBackColor = True
        '
        'rbFolder
        '
        Me.rbFolder.AutoSize = True
        Me.rbFolder.Location = New System.Drawing.Point(16, 87)
        Me.rbFolder.Name = "rbFolder"
        Me.rbFolder.Size = New System.Drawing.Size(145, 17)
        Me.rbFolder.TabIndex = 1
        Me.rbFolder.TabStop = True
        Me.rbFolder.Text = "Import multiple files from a"
        Me.rbFolder.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "folder"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolder
        '
        Me.txtFolder.Location = New System.Drawing.Point(75, 110)
        Me.txtFolder.Name = "txtFolder"
        Me.txtFolder.Size = New System.Drawing.Size(319, 20)
        Me.txtFolder.TabIndex = 3
        '
        'btnBrowseFolder
        '
        Me.btnBrowseFolder.Location = New System.Drawing.Point(400, 108)
        Me.btnBrowseFolder.Name = "btnBrowseFolder"
        Me.btnBrowseFolder.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseFolder.TabIndex = 7
        Me.btnBrowseFolder.Text = "Browse..."
        Me.btnBrowseFolder.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(37, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Filter"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbFilter
        '
        Me.cmbFilter.FormattingEnabled = True
        Me.cmbFilter.Items.AddRange(New Object() {"*.json", "*.geojson", "*.xml", "*.*"})
        Me.cmbFilter.Location = New System.Drawing.Point(75, 137)
        Me.cmbFilter.Name = "cmbFilter"
        Me.cmbFilter.Size = New System.Drawing.Size(69, 21)
        Me.cmbFilter.TabIndex = 9
        Me.cmbFilter.Text = "*.json"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(30, 198)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Template"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbTemplate
        '
        Me.cmbTemplate.FormattingEnabled = True
        Me.cmbTemplate.Location = New System.Drawing.Point(87, 195)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(169, 21)
        Me.cmbTemplate.TabIndex = 2
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(262, 194)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(56, 23)
        Me.btnEdit.TabIndex = 10
        Me.btnEdit.Text = "Edit..."
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(87, 269)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(75, 23)
        Me.btnImport.TabIndex = 11
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(412, 269)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkCluster
        '
        Me.chkCluster.AutoSize = True
        Me.chkCluster.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCluster.Location = New System.Drawing.Point(42, 234)
        Me.chkCluster.Name = "chkCluster"
        Me.chkCluster.Size = New System.Drawing.Size(58, 17)
        Me.chkCluster.TabIndex = 13
        Me.chkCluster.Text = "Cluster"
        Me.chkCluster.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCluster.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(105, 235)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "results within"
        '
        'txtClusterSize
        '
        Me.txtClusterSize.Location = New System.Drawing.Point(179, 232)
        Me.txtClusterSize.Name = "txtClusterSize"
        Me.txtClusterSize.Size = New System.Drawing.Size(76, 20)
        Me.txtClusterSize.TabIndex = 15
        Me.txtClusterSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(259, 235)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(22, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Km"
        '
        'btnSamples
        '
        Me.btnSamples.Location = New System.Drawing.Point(400, 73)
        Me.btnSamples.Name = "btnSamples"
        Me.btnSamples.Size = New System.Drawing.Size(75, 23)
        Me.btnSamples.TabIndex = 10
        Me.btnSamples.Text = "Samples..."
        Me.btnSamples.UseVisualStyleBackColor = True
        '
        'Importer
        '
        Me.AcceptButton = Me.btnImport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(513, 300)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtClusterSize)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkCluster)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbTemplate)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnCancel)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Importer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import file"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFolder As System.Windows.Forms.TextBox
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents rbFolder As System.Windows.Forms.RadioButton
    Friend WithEvents rbSingle As System.Windows.Forms.RadioButton
    Friend WithEvents cmbFilter As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseFolder As System.Windows.Forms.Button
    Friend WithEvents btnBrowseFile As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkCluster As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtClusterSize As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents btnSamples As Windows.Forms.Button
End Class
