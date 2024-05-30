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
Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TemplateEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TemplateEditor))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.scTree = New System.Windows.Forms.SplitContainer()
        Me.btnLoadTree = New System.Windows.Forms.Button()
        Me.tvSamples = New System.Windows.Forms.TreeView()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.scRight = New System.Windows.Forms.SplitContainer()
        Me.dgvRules = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnLoadTemplate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.cmbAdd = New System.Windows.Forms.ToolStripComboBox()
        Me.btnPreview = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnSaveAs = New System.Windows.Forms.ToolStripButton()
        Me.rtbNotes = New System.Windows.Forms.RichTextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.Action = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Identifier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Output = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Source = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.scTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scTree.Panel1.SuspendLayout()
        Me.scTree.Panel2.SuspendLayout()
        Me.scTree.SuspendLayout()
        CType(Me.scRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scRight.Panel1.SuspendLayout()
        Me.scRight.Panel2.SuspendLayout()
        Me.scRight.SuspendLayout()
        CType(Me.dgvRules, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'scTree
        '
        Me.scTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scTree.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scTree.Location = New System.Drawing.Point(0, 0)
        Me.scTree.Name = "scTree"
        '
        'scTree.Panel1
        '
        Me.scTree.Panel1.Controls.Add(Me.btnLoadTree)
        Me.scTree.Panel1.Controls.Add(Me.tvSamples)
        '
        'scTree.Panel2
        '
        Me.scTree.Panel2.Controls.Add(Me.txtMessage)
        Me.scTree.Panel2.Controls.Add(Me.scRight)
        Me.scTree.Size = New System.Drawing.Size(1132, 522)
        Me.scTree.SplitterDistance = 459
        Me.scTree.TabIndex = 2
        '
        'btnLoadTree
        '
        Me.btnLoadTree.Location = New System.Drawing.Point(335, 25)
        Me.btnLoadTree.Name = "btnLoadTree"
        Me.btnLoadTree.Size = New System.Drawing.Size(59, 23)
        Me.btnLoadTree.TabIndex = 2
        Me.btnLoadTree.Text = "Sample..."
        Me.ToolTip1.SetToolTip(Me.btnLoadTree, "Load a sample JSON or XML file")
        Me.btnLoadTree.UseVisualStyleBackColor = True
        '
        'tvSamples
        '
        Me.tvSamples.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tvSamples.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvSamples.Location = New System.Drawing.Point(0, 0)
        Me.tvSamples.Name = "tvSamples"
        Me.tvSamples.ShowNodeToolTips = True
        Me.tvSamples.Size = New System.Drawing.Size(459, 522)
        Me.tvSamples.TabIndex = 1
        '
        'txtMessage
        '
        Me.txtMessage.CausesValidation = False
        Me.txtMessage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtMessage.Location = New System.Drawing.Point(0, 502)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ReadOnly = True
        Me.txtMessage.Size = New System.Drawing.Size(669, 20)
        Me.txtMessage.TabIndex = 11
        '
        'scRight
        '
        Me.scRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scRight.Location = New System.Drawing.Point(0, 0)
        Me.scRight.Name = "scRight"
        Me.scRight.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scRight.Panel1
        '
        Me.scRight.Panel1.Controls.Add(Me.dgvRules)
        Me.scRight.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'scRight.Panel2
        '
        Me.scRight.Panel2.Controls.Add(Me.rtbNotes)
        Me.scRight.Size = New System.Drawing.Size(669, 522)
        Me.scRight.SplitterDistance = 439
        Me.scRight.TabIndex = 0
        '
        'dgvRules
        '
        Me.dgvRules.AllowDrop = True
        Me.dgvRules.AllowUserToAddRows = False
        Me.dgvRules.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvRules.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Action, Me.Identifier, Me.Output, Me.Source, Me.Value, Me.Type, Me.ID})
        Me.dgvRules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRules.Location = New System.Drawing.Point(0, 25)
        Me.dgvRules.Name = "dgvRules"
        Me.dgvRules.RowHeadersWidth = 30
        Me.dgvRules.Size = New System.Drawing.Size(669, 414)
        Me.dgvRules.TabIndex = 8
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnLoadTemplate, Me.ToolStripLabel1, Me.cmbAdd, Me.btnPreview, Me.btnSave, Me.btnSaveAs})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(669, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnLoadTemplate
        '
        Me.btnLoadTemplate.Image = CType(resources.GetObject("btnLoadTemplate.Image"), System.Drawing.Image)
        Me.btnLoadTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadTemplate.Name = "btnLoadTemplate"
        Me.btnLoadTemplate.Size = New System.Drawing.Size(53, 22)
        Me.btnLoadTemplate.Text = "Load"
        Me.btnLoadTemplate.ToolTipText = "Load a template"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripLabel1.Image = CType(resources.GetObject("ToolStripLabel1.Image"), System.Drawing.Image)
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(45, 22)
        Me.ToolStripLabel1.Text = "Add"
        '
        'cmbAdd
        '
        Me.cmbAdd.Items.AddRange(New Object() {"Expression", "Import", "Skip", "Sort"})
        Me.cmbAdd.Name = "cmbAdd"
        Me.cmbAdd.Size = New System.Drawing.Size(75, 25)
        Me.cmbAdd.ToolTipText = "Add a new : Expression, Import, Skip, Sort"
        '
        'btnPreview
        '
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(68, 22)
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.ToolTipText = "Preview the resulting dataset"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "Save"
        Me.btnSave.ToolTipText = "Save this template"
        '
        'btnSaveAs
        '
        Me.btnSaveAs.Image = CType(resources.GetObject("btnSaveAs.Image"), System.Drawing.Image)
        Me.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(74, 22)
        Me.btnSaveAs.Text = "Save as..."
        Me.btnSaveAs.ToolTipText = "Save this template as..."
        '
        'rtbNotes
        '
        Me.rtbNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbNotes.Location = New System.Drawing.Point(0, 0)
        Me.rtbNotes.Name = "rtbNotes"
        Me.rtbNotes.Size = New System.Drawing.Size(669, 79)
        Me.rtbNotes.TabIndex = 9
        Me.rtbNotes.Text = ""
        Me.ToolTip1.SetToolTip(Me.rtbNotes, "Notes describing this template")
        '
        'Action
        '
        Me.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Action.DataPropertyName = "Action"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Action.DefaultCellStyle = DataGridViewCellStyle1
        Me.Action.HeaderText = "Action"
        Me.Action.Name = "Action"
        Me.Action.ReadOnly = True
        Me.Action.Width = 62
        '
        'Identifier
        '
        Me.Identifier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Identifier.DataPropertyName = "Identifier"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Identifier.DefaultCellStyle = DataGridViewCellStyle2
        Me.Identifier.HeaderText = "Name"
        Me.Identifier.Name = "Identifier"
        Me.Identifier.Width = 60
        '
        'Output
        '
        Me.Output.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Output.DataPropertyName = "Output"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.NullValue = False
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Output.DefaultCellStyle = DataGridViewCellStyle3
        Me.Output.HeaderText = "Output"
        Me.Output.Name = "Output"
        Me.Output.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Output.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Output.Width = 64
        '
        'Source
        '
        Me.Source.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Source.DataPropertyName = "Source"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Source.DefaultCellStyle = DataGridViewCellStyle4
        Me.Source.HeaderText = "Source"
        Me.Source.MinimumWidth = 50
        Me.Source.Name = "Source"
        Me.Source.Width = 66
        '
        'Value
        '
        Me.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Value.DataPropertyName = "Value"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Value.DefaultCellStyle = DataGridViewCellStyle5
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        Me.Value.ReadOnly = True
        Me.Value.Width = 59
        '
        'Type
        '
        Me.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Type.DataPropertyName = "Type"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Type.DefaultCellStyle = DataGridViewCellStyle6
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.ReadOnly = True
        Me.Type.Width = 56
        '
        'ID
        '
        Me.ID.DataPropertyName = "ID"
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        Me.ID.Width = 50
        '
        'TemplateEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1132, 522)
        Me.Controls.Add(Me.scTree)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TemplateEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form1"
        Me.scTree.Panel1.ResumeLayout(False)
        Me.scTree.Panel2.ResumeLayout(False)
        Me.scTree.Panel2.PerformLayout()
        CType(Me.scTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scTree.ResumeLayout(False)
        Me.scRight.Panel1.ResumeLayout(False)
        Me.scRight.Panel1.PerformLayout()
        Me.scRight.Panel2.ResumeLayout(False)
        CType(Me.scRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scRight.ResumeLayout(False)
        CType(Me.dgvRules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents scTree As SplitContainer
    Friend WithEvents tvSamples As TreeView
    Friend WithEvents btnLoadTree As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents scRight As SplitContainer
    Friend WithEvents dgvRules As DataGridView
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnLoadTemplate As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents cmbAdd As ToolStripComboBox
    Friend WithEvents btnPreview As ToolStripButton
    Friend WithEvents btnSave As ToolStripButton
    Friend WithEvents btnSaveAs As ToolStripButton
    Friend WithEvents rtbNotes As RichTextBox
    Friend WithEvents txtMessage As TextBox
    Friend WithEvents HelpProvider1 As HelpProvider
    Friend WithEvents Action As DataGridViewTextBoxColumn
    Friend WithEvents Identifier As DataGridViewTextBoxColumn
    Friend WithEvents Output As DataGridViewCheckBoxColumn
    Friend WithEvents Source As DataGridViewTextBoxColumn
    Friend WithEvents Value As DataGridViewTextBoxColumn
    Friend WithEvents Type As DataGridViewTextBoxColumn
    Friend WithEvents ID As DataGridViewTextBoxColumn
End Class
