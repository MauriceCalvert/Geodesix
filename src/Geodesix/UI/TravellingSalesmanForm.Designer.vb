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
Partial Class TravellingSalesmanForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TravellingSalesmanForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFormulas = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnFindRoute = New System.Windows.Forms.Button()
        Me.txtDistance = New System.Windows.Forms.TextBox()
        Me.lblSolution = New System.Windows.Forms.Label()
        Me.lblKilometres = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.btnAdvanced = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTemp = New System.Windows.Forms.TextBox()
        Me.txtInitTemp = New System.Windows.Forms.TextBox()
        Me.txtInitCooling = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtIteration = New System.Windows.Forms.TextBox()
        Me.txtAbsTemp = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Worksheet contains"
        '
        'txtFormulas
        '
        Me.txtFormulas.BackColor = System.Drawing.SystemColors.Control
        Me.txtFormulas.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFormulas.Location = New System.Drawing.Point(120, 14)
        Me.txtFormulas.Name = "txtFormulas"
        Me.txtFormulas.ReadOnly = True
        Me.txtFormulas.Size = New System.Drawing.Size(53, 13)
        Me.txtFormulas.TabIndex = 1
        Me.txtFormulas.TabStop = False
        Me.txtFormulas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(179, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(147, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "TravellingSalesman() formulas"
        '
        'btnFindRoute
        '
        Me.btnFindRoute.Location = New System.Drawing.Point(15, 99)
        Me.btnFindRoute.Name = "btnFindRoute"
        Me.btnFindRoute.Size = New System.Drawing.Size(75, 23)
        Me.btnFindRoute.TabIndex = 0
        Me.btnFindRoute.Text = "Find Route"
        Me.ToolTip1.SetToolTip(Me.btnFindRoute, "Find the optimal route to visit all locations")
        Me.btnFindRoute.UseVisualStyleBackColor = True
        '
        'txtDistance
        '
        Me.txtDistance.BackColor = System.Drawing.SystemColors.Control
        Me.txtDistance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDistance.Location = New System.Drawing.Point(73, 36)
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.ReadOnly = True
        Me.txtDistance.Size = New System.Drawing.Size(100, 13)
        Me.txtDistance.TabIndex = 5
        Me.txtDistance.TabStop = False
        Me.txtDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSolution
        '
        Me.lblSolution.AutoSize = True
        Me.lblSolution.Location = New System.Drawing.Point(12, 36)
        Me.lblSolution.Name = "lblSolution"
        Me.lblSolution.Size = New System.Drawing.Size(55, 13)
        Me.lblSolution.TabIndex = 4
        Me.lblSolution.Text = "Solution is"
        '
        'lblKilometres
        '
        Me.lblKilometres.AutoSize = True
        Me.lblKilometres.Location = New System.Drawing.Point(179, 36)
        Me.lblKilometres.Name = "lblKilometres"
        Me.lblKilometres.Size = New System.Drawing.Size(54, 13)
        Me.lblKilometres.TabIndex = 6
        Me.lblKilometres.Text = "kilometres"
        '
        'txtMessage
        '
        Me.txtMessage.BackColor = System.Drawing.SystemColors.Control
        Me.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMessage.Location = New System.Drawing.Point(14, 59)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ReadOnly = True
        Me.txtMessage.Size = New System.Drawing.Size(334, 34)
        Me.txtMessage.TabIndex = 7
        Me.txtMessage.TabStop = False
        '
        'btnAdvanced
        '
        Me.btnAdvanced.Location = New System.Drawing.Point(270, 99)
        Me.btnAdvanced.Name = "btnAdvanced"
        Me.btnAdvanced.Size = New System.Drawing.Size(75, 23)
        Me.btnAdvanced.TabIndex = 3
        Me.btnAdvanced.Text = "Advanced"
        Me.ToolTip1.SetToolTip(Me.btnAdvanced, "Fiddle with temperatures")
        Me.btnAdvanced.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Initial Temperature"
        '
        'txtTemp
        '
        Me.txtTemp.BackColor = System.Drawing.SystemColors.Control
        Me.txtTemp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTemp.Location = New System.Drawing.Point(129, 231)
        Me.txtTemp.Name = "txtTemp"
        Me.txtTemp.ReadOnly = True
        Me.txtTemp.Size = New System.Drawing.Size(129, 13)
        Me.txtTemp.TabIndex = 12
        Me.txtTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInitTemp
        '
        Me.txtInitTemp.Location = New System.Drawing.Point(129, 138)
        Me.txtInitTemp.Name = "txtInitTemp"
        Me.txtInitTemp.Size = New System.Drawing.Size(129, 20)
        Me.txtInitTemp.TabIndex = 4
        Me.txtInitTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.txtInitTemp, "Increase by factors of 10 to make search longer and better (try 1'000'000)")
        '
        'txtInitCooling
        '
        Me.txtInitCooling.Location = New System.Drawing.Point(129, 168)
        Me.txtInitCooling.Name = "txtInitCooling"
        Me.txtInitCooling.Size = New System.Drawing.Size(129, 20)
        Me.txtInitCooling.TabIndex = 5
        Me.txtInitCooling.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.txtInitCooling, "The closer to 1, the longer and better the search (try 0.999999)")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 171)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Cooling rate"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 272)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Iterations"
        '
        'txtIteration
        '
        Me.txtIteration.BackColor = System.Drawing.SystemColors.Control
        Me.txtIteration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtIteration.Location = New System.Drawing.Point(129, 272)
        Me.txtIteration.Name = "txtIteration"
        Me.txtIteration.ReadOnly = True
        Me.txtIteration.Size = New System.Drawing.Size(129, 13)
        Me.txtIteration.TabIndex = 20
        Me.txtIteration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAbsTemp
        '
        Me.txtAbsTemp.Location = New System.Drawing.Point(129, 198)
        Me.txtAbsTemp.Name = "txtAbsTemp"
        Me.txtAbsTemp.Size = New System.Drawing.Size(129, 20)
        Me.txtAbsTemp.TabIndex = 6
        Me.txtAbsTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.txtAbsTemp, "Determines when search ends (try 0.000001)")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 201)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Absolute Temperature"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 231)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 13)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Current Temperature"
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnOK.Location = New System.Drawing.Point(100, 99)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.btnOK, "Sort on the optimal route")
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(185, 99)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Close without sorting")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'TravellingSalesmanForm
        '
        Me.AcceptButton = Me.btnFindRoute
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(356, 259)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtAbsTemp)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtIteration)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtInitCooling)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtInitTemp)
        Me.Controls.Add(Me.txtTemp)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnAdvanced)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.lblKilometres)
        Me.Controls.Add(Me.txtDistance)
        Me.Controls.Add(Me.lblSolution)
        Me.Controls.Add(Me.btnFindRoute)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtFormulas)
        Me.Controls.Add(Me.Label1)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TravellingSalesmanForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Travelling Salesman Solver"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFormulas As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnFindRoute As System.Windows.Forms.Button
    Friend WithEvents txtDistance As System.Windows.Forms.TextBox
    Friend WithEvents lblSolution As System.Windows.Forms.Label
    Friend WithEvents lblKilometres As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnAdvanced As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTemp As System.Windows.Forms.TextBox
    Friend WithEvents txtInitTemp As System.Windows.Forms.TextBox
    Friend WithEvents txtInitCooling As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtIteration As System.Windows.Forms.TextBox
    Friend WithEvents txtAbsTemp As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
