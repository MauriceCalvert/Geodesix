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
Partial Class FieldPicker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FieldPicker))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgvGoogleFields = New System.Windows.Forms.DataGridView()
        Me.GoogleField = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.dgvDrawingFields = New System.Windows.Forms.DataGridView()
        Me.DrawingField = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvGoogleFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDrawingFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvGoogleFields)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvDrawingFields)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBox3)
        Me.SplitContainer1.Size = New System.Drawing.Size(419, 393)
        Me.SplitContainer1.SplitterDistance = 199
        Me.SplitContainer1.TabIndex = 0
        '
        'dgvGoogleFields
        '
        Me.dgvGoogleFields.AllowUserToAddRows = False
        Me.dgvGoogleFields.AllowUserToDeleteRows = False
        Me.dgvGoogleFields.AllowUserToResizeColumns = False
        Me.dgvGoogleFields.AllowUserToResizeRows = False
        Me.dgvGoogleFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGoogleFields.ColumnHeadersVisible = False
        Me.dgvGoogleFields.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GoogleField})
        Me.dgvGoogleFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvGoogleFields.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvGoogleFields.EnableHeadersVisualStyles = False
        Me.dgvGoogleFields.Location = New System.Drawing.Point(0, 20)
        Me.dgvGoogleFields.MultiSelect = False
        Me.dgvGoogleFields.Name = "dgvGoogleFields"
        Me.dgvGoogleFields.ReadOnly = True
        Me.dgvGoogleFields.RowHeadersVisible = False
        Me.dgvGoogleFields.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvGoogleFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvGoogleFields.ShowCellErrors = False
        Me.dgvGoogleFields.ShowCellToolTips = False
        Me.dgvGoogleFields.ShowEditingIcon = False
        Me.dgvGoogleFields.ShowRowErrors = False
        Me.dgvGoogleFields.Size = New System.Drawing.Size(199, 373)
        Me.dgvGoogleFields.TabIndex = 1
        '
        'GoogleField
        '
        Me.GoogleField.DataPropertyName = "GoogleField"
        Me.GoogleField.HeaderText = "Text"
        Me.GoogleField.Name = "GoogleField"
        Me.GoogleField.ReadOnly = True
        '
        'TextBox2
        '
        Me.TextBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBox2.Location = New System.Drawing.Point(0, 0)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(199, 20)
        Me.TextBox2.TabIndex = 0
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "Information fields from Google Maps"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'dgvDrawingFields
        '
        Me.dgvDrawingFields.AllowUserToAddRows = False
        Me.dgvDrawingFields.AllowUserToDeleteRows = False
        Me.dgvDrawingFields.AllowUserToResizeColumns = False
        Me.dgvDrawingFields.AllowUserToResizeRows = False
        Me.dgvDrawingFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDrawingFields.ColumnHeadersVisible = False
        Me.dgvDrawingFields.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DrawingField})
        Me.dgvDrawingFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDrawingFields.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvDrawingFields.EnableHeadersVisualStyles = False
        Me.dgvDrawingFields.Location = New System.Drawing.Point(0, 20)
        Me.dgvDrawingFields.MultiSelect = False
        Me.dgvDrawingFields.Name = "dgvDrawingFields"
        Me.dgvDrawingFields.ReadOnly = True
        Me.dgvDrawingFields.RowHeadersVisible = False
        Me.dgvDrawingFields.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvDrawingFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvDrawingFields.ShowCellErrors = False
        Me.dgvDrawingFields.ShowCellToolTips = False
        Me.dgvDrawingFields.ShowEditingIcon = False
        Me.dgvDrawingFields.ShowRowErrors = False
        Me.dgvDrawingFields.Size = New System.Drawing.Size(216, 373)
        Me.dgvDrawingFields.TabIndex = 2
        '
        'DrawingField
        '
        Me.DrawingField.DataPropertyName = "DrawingField"
        Me.DrawingField.HeaderText = "DrawingField"
        Me.DrawingField.Name = "DrawingField"
        Me.DrawingField.ReadOnly = True
        '
        'TextBox3
        '
        Me.TextBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBox3.Location = New System.Drawing.Point(0, 0)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(216, 20)
        Me.TextBox3.TabIndex = 1
        Me.TextBox3.TabStop = False
        Me.TextBox3.Text = "Drawing overrides"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(419, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "Double-click a field to insert it in the current cell"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(100, 100)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(49, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Close"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FieldPicker
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(419, 413)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TextBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FieldPicker"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Field picker"
        Me.TopMost = True
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvGoogleFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDrawingFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvGoogleFields As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents dgvDrawingFields As System.Windows.Forms.DataGridView
    Friend WithEvents GoogleField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DrawingField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
End Class
