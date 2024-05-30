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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Math
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports System.Windows.Forms.DataGridView
Imports JsonTransformer
Imports Newtonsoft.Json.Linq
Imports Seting
Imports Utilities
Public Module TreeViewExtensions
    <Extension()>
    Public Function Descendants(ByVal tree As TreeView) As IEnumerable(Of TreeNode)

        Dim nodes As IEnumerable(Of TreeNode) = tree.Nodes.Cast(Of TreeNode)()
        Return nodes.SelectMany(Function(x) x.Descendants()).Concat(nodes)

    End Function

    <Extension()>
    Public Function Descendants(ByVal node As TreeNode) As IEnumerable(Of TreeNode)

        Dim nodes As IEnumerable(Of TreeNode) = node.Nodes.Cast(Of TreeNode)()
        Return nodes.SelectMany(Function(x) Descendants(x)).Concat(nodes)

    End Function
End Module
Public Class TemplateEditor

    Private Property GeodesixEXL As GeodesiXEXL
    Public Sub New(geodesixexl As GeodesiXEXL, template As Template, FileName As String)

        InitializeComponent()

        Me.GeodesixEXL = geodesixexl
        Me.Template = template
        Me.FileName = FileName

    End Sub

    Private Sub TemplateEditor_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            btnSave.Enabled = Not IsEmpty(Template.TemplateName) AndAlso Template.TemplateName <> "<new>"

            If Not IsEmpty(FileName) Then
                LoadFile(FileName)
            End If

            BeginInvoke(Sub() ReSizeAll())

        Catch ex As Exception
            ShowError(ex.Message)
            Close()

        End Try

    End Sub
    Private Const RTFPROMPT As String = "Notes about this template..."
    Private Sub btnLoadTemplate_Click(sender As Object, e As EventArgs) Handles btnLoadTemplate.Click

        Try
            Dim filename As String = ""

            If BrowseForFile(filename, "template", "template files (*.template)|*.template", Template.TemplateFolder) Then
                rtbNotes.Text = ""
                Template.LoadTemplate(filename)
                Reload(True)
                ReSizeAll()
                btnSave.Enabled = True
                SetText()
                rtbNotes.Rtf = Template.NotesRTF
                If rtbNotes.Text.Trim().Length = 0 Then
                    rtbNotes.Text = RTFPROMPT
                    scRight.SplitterDistance = scRight.ClientSize.Height - txtMessage.Height - 24
                Else
                    scRight.SplitterDistance = scRight.ClientSize.Height - txtMessage.Height -
                    rtbNotes.GetPreferredSize(Size.Empty).Height
                End If
            End If

        Catch ex As Exception
            Complain(ex.Message)
        End Try

    End Sub
    Private Sub btnLoadTree_Click(sender As Object, e As EventArgs) Handles btnLoadTree.Click
        Try
            Dim filename As String = ""

            If BrowseForFile(filename, "json", "JSON files (*.json;*.geojson)|*.json;*.geojson|XML files (*.xml)|*.xml", Settings.ImportFile) Then
                LoadFile(filename)
                Settings.ImportFile = filename
            End If

        Catch ex As Exception
            Complain(ex.Message)

        End Try

    End Sub
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click

        Try
            Dim parser As New Parser(Template)

            Dim table As DataTable = parser.Parse()

            ' Last column is our _ID_ unique key? Remove it
            If table.Columns(table.Columns.Count - 1).ColumnName = Importer.UNIQUEKEYNAME Then
                table.PrimaryKey = Nothing
                table.Columns.RemoveAt((table.Columns.Count - 1))
            End If


            ' Debugging
            'Using writer As New StreamWriter("D:\test.txt")

            '    writer.WriteLine(String.Join(vbTab,
            '                                 table.
            '                                 Columns.
            '                                 Cast(Of DataColumn).
            '                                 Select(Function(q) q.ColumnName)))

            '    For Each row As DataRow In table.Rows
            '        writer.WriteLine(String.Join(vbTab, row.ItemArray))
            '    Next
            'End Using

            Reload(False)
            Confirm($"Output {table.Rows.Count} rows")

            ShowFormDialog(New Preview(table))

        Catch ex As Exception
            Complain(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If rtbNotes.Text.Trim() = RTFPROMPT Then
                Template.NotesRTF = Nothing
            Else
                Template.NotesRTF = rtbNotes.Rtf
            End If
            Template.Save(Template.TemplateName)
            Confirm($"Template {Template.TemplateName} saved")
            btnSave.Enabled = True

        Catch ex As Exception
            Complain(ex.Message)
        End Try
    End Sub
    Private Sub btnSaveAs_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click

        Try
            Dim filename As String = ""

            If BrowseForFile(filename, "template", "template files (*.template)|*.template",
                             Template.TemplateFolder, False) Then

                If File.Exists(filename) Then
                    If ShowBox($"Overwrite template {filename}?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                        Exit Sub
                    End If
                End If
                If rtbNotes.Text.Trim() = RTFPROMPT Then
                    Template.NotesRTF = Nothing
                Else
                    Template.NotesRTF = rtbNotes.Rtf
                End If
                Template.Save(Path.GetFileNameWithoutExtension(filename))
                Confirm($"Template saved as {filename}")
                btnSave.Enabled = True
                SetText()
            End If

        Catch ex As Exception
            Complain(ex.Message)
        End Try
    End Sub
    Private Sub cmbAdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAdd.SelectedIndexChanged

        Try
            If cmbAdd.SelectedIndex < 0 Then
                Exit Sub
            End If
            Dim source As String = cmbAdd.Text

            Select Case source
                Case "Expression"
                    Template.Schema.AddExpression()
                Case "Import"
                    Template.Schema.AddImport()
                Case "Skip"
                    Template.Schema.AddSkip()
                Case "Sort"
                    Template.Schema.AddSort()
                Case Else
                    Complain($"Invalid source {source}")
            End Select

            Reload(False)

            BeginInvoke(
                Sub()
                    cmbAdd.Text = "" ' clear the combo
                    cmbAdd.SelectedIndex = -1
                    dgvRules.Focus()

                    ' Select the first cell in the last row (that was just added) that isn't readonly
                    Dim row As DataGridViewRow = dgvRules.Rows(dgvRules.Rows.Count - 1)
                    Dim cell As DataGridViewCell = row.Cells(1) ' Source(0) is always readonly

                    Do While cell.ReadOnly AndAlso cell.ColumnIndex < dgvRules.ColumnCount
                        cell = row.Cells(cell.ColumnIndex + 1)
                    Loop
                    dgvRules.CurrentCell = cell
                    dgvRules.BeginEdit(True)
                End Sub)

        Catch ex As Exception
            Complain(ex.Message)
        End Try

    End Sub
    Private Sub Complain(text As String)

        txtMessage.ForeColor = Color.Red
        txtMessage.Text = text
        txtMessage.Font = New Font(txtMessage.Font, FontStyle.Bold)
        '         https://stackoverflow.com/a/20688985/338101
        ' The ForeColor property of a read-only TextBox is married to the BackColor property for some reason.
        ' So if you "tickle" the BackColor property, it will set the ForeColor property after that:
        Dim bc As Color = txtMessage.BackColor
        txtMessage.BackColor = Color.Black
        txtMessage.BackColor = bc

    End Sub
    Private Sub Confirm(text As String)

        txtMessage.ForeColor = Color.Black
        txtMessage.Text = text
        txtMessage.Font = New Font(txtMessage.Font, FontStyle.Regular)
        Dim bc As Color = txtMessage.BackColor
        txtMessage.BackColor = Color.Black
        txtMessage.BackColor = bc

    End Sub
    Private Sub dgvRules_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) _
        Handles dgvRules.DataError

        Complain(e.Exception.Message)

    End Sub
    Private Sub dgvRules_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) _
        Handles dgvRules.RowValidating

        If dgvRules.IsCurrentRowDirty Then
            BeginInvoke(Sub()
                            Reload(True)
                        End Sub)
        End If

    End Sub
    Private Sub dgvRules_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) _
        Handles dgvRules.UserDeletingRow
        Try
            ' Fucking prehistoric DataGridViewCellCollection implements GetEnumerator but doesn't implement IEnumerable,
            ' forcing us to 'For Each'. Peasants.

            Dim todelete As New List(Of RuleEditor)

            For Each row As DataGridViewRow In dgvRules.SelectedRows
                todelete.Add(DirectCast(row.DataBoundItem, RuleEditor))
            Next

            For Each row As RuleEditor In todelete
                Template.Schema.Remove(row.Rule)
            Next
            Confirm($"{todelete.Count} rules deleted")
            Reload(True)

        Catch ex As Exception
            Complain(ex.Message)
        End Try

    End Sub
    Private ReadOnly Property FileName As String
    Private Sub Help_Requested(sender As Object, hlpevent As HelpEventArgs) Handles Me.HelpRequested

        Try
            hlpevent.Handled = True
            GeodesixEXL?.ShowHelp("templateeditor.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub Help_ButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked

        Try
            e.Cancel = True
            GeodesixEXL?.ShowHelp("templateeditor.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub LoadFile(filename As String)

        _FileName = filename
        Template.LoadFile(filename)

        LoadTree(Template.Sample(True), tvSamples)
        scTree.SplitterDistance = ClientRectangle.Width \ 2 ' Ensure that a good portion of nodes are really visible
        Dim sd As Integer = TreeViewWidth(tvSamples.Nodes, 0) ' Find the widest
        sd = Min(sd, ClientRectangle.Width \ 2) ' Don't let it take up more than half the screen
        scTree.SplitterDistance = sd
        Settings.ImportFile = filename

        Confirm($"Loaded {Template.Root.OfType(Of JContainer).Descendants.Count:#,##0} nodes")
        SetText()

        Reload(True)

    End Sub
    Private WithEvents BindingSource As BindingSource
    Private Sub Reload(revalidate As Boolean) ' We get called from event handlers, so BeginInvoke

        BeginInvoke(
            Sub()
                Try
                    Reload1(revalidate)

                Catch ex As Exception
                    HandleError(MethodBase.GetCurrentMethod().Name, ex)

                End Try
            End Sub)
    End Sub
    Private Sub Reload1(revalidate As Boolean)

        Dim rulesvalidated As Boolean = False

        If revalidate Then
            If Template.Schema.Valid Then
                Confirm(Template.Schema.Message)
                rulesvalidated = True
            Else
                Complain(Template.Schema.Message)
            End If
        Else
            txtMessage.Text = Template.Schema.Message
        End If

        BindingSource = New BindingSource
        BindingSource.DataSource =
            Template.
            Schema.
            Rules.
            Select(Function(q) New RuleEditor(q)).
            ToList

        dgvRules.AutoGenerateColumns = False
        dgvRules.DataSource = BindingSource

        For Each row As DataGridViewRow In dgvRules.Rows

            ' Enable/Disable and set colours according to source type, errors, etc.
            Dim rule As RuleEditor = DirectCast(row.DataBoundItem, RuleEditor)

            row.Cells("Value").Style.ForeColor = Color.Black

            IfNotEmpty(rule.Rule.Message, Sub() row.Cells("Value").Style.ForeColor = Color.Red)

            Select Case rule.Action

                Case "Import", "Sort"
                    row.Cells("Source").ReadOnly = True
                    row.Cells("Source").Style.BackColor = Color.WhiteSmoke
                    row.Cells("Output").ReadOnly = True
                    row.Cells("Output").Style.BackColor = Color.WhiteSmoke

                Case "Skip"
                    row.Cells("Output").ReadOnly = True
                    row.Cells("Output").Style.BackColor = Color.WhiteSmoke

            End Select
        Next

        ' Size the Source and Identifier columns, only once on loading
        'ResizeCell("Source")
        'ResizeCell("Identifier")

        Try ' Freeze TreeView while we're colouring
            tvSamples.BeginUpdate()
            tvSamples.CollapseAll()
            Dim highlighted As Boolean
            ' Reset all treenodes to black
            For Each node As TreeNode In
                tvSamples.
                Descendants

                node.ForeColor = Color.Black
            Next

            Dim bluenodes As Boolean
            ' Colour the selected nodes blue
            For Each node As TreeNode In
                tvSamples.
                Descendants.
                Where(Function(q)
                          If TypeOf q.Tag Is JValue Then
                              Dim item As JValue = DirectCast(q.Tag, JValue)
                              If Not Template.Schema.isSelected(item.Path) Then
                                  Return False
                              End If
                              Return True
                          End If
                          Return False
                      End Function)
                node.ForeColor = Color.Blue
                node.EnsureVisible()
                highlighted = True
                bluenodes = True
            Next

            If rulesvalidated Then
                ' Colour the break nodes red
                Dim breakpath As String = Template.BreakPath

                If IsEmpty(breakpath) Then
                    If bluenodes Then
                        Complain("This set of leaves has no common parent. Remove blue nodes until red node(s) appear.")
                    End If
                Else
                    For Each node As TreeNode In
                        tvSamples.
                        Descendants.
                        Where(Function(q)
                                  Dim item As JToken = DirectCast(q.Tag, JToken)
                                  Return WildPath(item.Path) = breakpath
                              End Function)

                        node.ForeColor = Color.Red
                        node.EnsureVisible()
                    Next
                End If
            End If

            If Not highlighted Then
                tvSamples.ExpandAll()
            End If
            tvSamples.TopNode = tvSamples.Nodes(0)

            dgvRules.Refresh()

        Catch ex As Exception
            Throw

        Finally ' Whatever happens, unfreeze TreeView
            tvSamples.EndUpdate()

        End Try

    End Sub
    Private Sub ReSizeAll()

        For Each col As DataGridViewColumn In dgvRules.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            Dim w As Integer = col.Width
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            col.Width = w
        Next

    End Sub
    Private Sub rtbNotes_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles rtbNotes.LinkClicked

        Try
            OpenBrowser(e.LinkText)

        Catch ex As Exception
            Complain(ex.Message)
        End Try

    End Sub

    Private Sub scTree_Resize(sender As Object, e As EventArgs) Handles scTree.Resize

        btnLoadTree.Top = 3
        btnLoadTree.Left = scTree.Panel1.Width - btnLoadTemplate.Width - 25

    End Sub
    Private Sub SetText()

        Me.Text = $"{Template.FileName} - {Template.TemplateName}"

    End Sub
    Private WithEvents Template As Template
    Private Function TreeViewWidth(node As TreeNode, maxwidth As Integer) As Integer

        If node.IsVisible Then
            If node.Nodes IsNot Nothing Then
                Return Max(maxwidth, TreeViewWidth(node.Nodes, maxwidth))
            Else
                Return Max(maxwidth, node.Bounds.Right)
            End If
        Else
            Return maxwidth
        End If

    End Function
    Private Function TreeViewWidth(nodes As TreeNodeCollection, maxwidth As Integer) As Integer

        Dim result As Integer = maxwidth
        For Each node As TreeNode In nodes
            If node.IsVisible Then
                result = Max(result, TreeViewWidth(node, node.Bounds.Right))
            End If
        Next
        Return result

    End Function
    Private Sub tvSamples_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvSamples.NodeMouseDoubleClick

        Try
            Dim node As TreeNode = e.Node

            Dim item As JToken = DirectCast(node.Tag, JToken)

            If e.Button = MouseButtons.Left Then

                Dim val As Object = ""
                If TypeOf item Is JValue Then
                    val = TryDouble(DirectCast(item, JValue).Value)
                End If

                Template.Schema.AddPath(,, True, item.Path, val)
            End If

            Reload(True)
            ReSizeAll()

        Catch ex As Exception
            Complain(ex.Message)
        End Try

    End Sub

    Private Sub tvSamples_NodeMouseHover(sender As Object, e As TreeNodeMouseHoverEventArgs) Handles tvSamples.NodeMouseHover

        If e.Node IsNot Nothing Then
            Dim jtoken As JToken = TryCast(e.Node?.Tag, JToken)
            If jtoken IsNot Nothing AndAlso TypeOf jtoken Is JValue Then
                e.Node.ToolTipText = "Double-click to add this node to the outputs"
            End If
        End If

    End Sub
    Private Sub tvSamples_Resize(sender As Object, e As EventArgs) Handles tvSamples.Resize
        scTree_Resize(sender, e)
    End Sub

    ' ------------------------ Drag-Drop stuff -------------------------
    Private MouseEvent As MouseDownEvent
    Private Sub dgvRules_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvRules.CellMouseDown


        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then
            Return
        End If
        MouseEvent = New MouseDownEvent(Me, Template, dgvRules, e)

    End Sub
    Private Sub dgvRules_CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvRules.CellMouseMove


        MouseEvent?.CellMouseMove(sender, e)

    End Sub
    Private Sub dgvRules_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvRules.CellMouseUp


        MouseEvent?.CellMouseUp(sender, e)
        MouseEvent = Nothing

    End Sub
    Private Class MouseDownEvent

        Private WithEvents Timer As Timer
        Private Property Origin As DataGridViewCellMouseEventArgs
        Private Property Destination As DataGridViewCellMouseEventArgs
        Private Property dgvrules As DataGridView
        Private Property template As Template
        Private Property DragLabel As Label
        Private Property MouseDownLocation As Point
        Private Property MouseUpLocation As Point
        Private Property editor As TemplateEditor

        Sub New(editor As TemplateEditor, template As Template, dgvrules As DataGridView, e As DataGridViewCellMouseEventArgs)

            Try
                Me.editor = editor
                Me.template = template
                Me.dgvrules = dgvrules
                Origin = e
                MouseDownLocation = dgvrules.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False).Location
                AddHandler dgvrules.CellMouseMove, AddressOf CellMouseMove
                AddHandler dgvrules.CellMouseUp, AddressOf CellMouseUp
                Timer = New Timer()
                Timer.Interval = 500
                Timer.Start()

            Catch ex As Exception
                HandleError(MethodBase.GetCurrentMethod().Name, ex)
            End Try

        End Sub
        Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick

            Try
                Timer.Stop()
                Timer.Dispose()

                If Control.MouseButtons = MouseButtons.Left Then
                    DragLabel = New Label With {
                        .AutoSize = True,
                        .ForeColor = Color.White,
                        .BackColor = Color.DarkGray,
                        .Location = MouseDownLocation,
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .Text = String.Join(" ", dgvrules.
                                          Rows(Origin.RowIndex).
                                          Cells.
                                          Cast(Of DataGridViewCell).
                                          Take(4).Select(Function(q) q.Value)),
                        .Parent = dgvrules
                    }
                    dgvrules.ClearSelection()
                Else
                    Finish()
                End If

            Catch ex As Exception
                HandleError(MethodBase.GetCurrentMethod().Name, ex)
            End Try

        End Sub
        Sub CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs)

            Try
                If DragLabel Is Nothing Then
                    Exit Sub
                End If
                If e.Button = MouseButtons.Left Then
                    Dim rect As Rectangle = dgvrules.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False)
                    DragLabel.Location = rect.Location
                End If


            Catch ex As Exception
                HandleError(MethodBase.GetCurrentMethod().Name, ex)
            End Try

        End Sub
        Sub CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs)

            Try
                If DragLabel Is Nothing Then
                    Exit Sub
                End If

                Destination = e
                MouseUpLocation = dgvrules.GetCellDisplayRectangle(Destination.ColumnIndex, Destination.RowIndex, False).Location
                Dim hit As HitTestInfo = dgvrules.HitTest(MouseUpLocation.X, MouseUpLocation.Y)

                If hit.Type <> DataGridViewHitTestType.None Then

                    Dim dropRow As Integer = hit.RowIndex

                    If Origin.RowIndex >= 0 Then

                        Dim tgtRow As Integer = dropRow

                        If tgtRow <> Origin.RowIndex Then

                            Dim row As DataGridViewRow = dgvrules.Rows(Origin.RowIndex)

                            Dim old As JsonTransformer.Rule = DirectCast(row.DataBoundItem, RuleEditor).Rule
                            template.Schema.Rules.Remove(old)
                            template.Schema.Rules.Insert(tgtRow, old)
                            dgvrules.ClearSelection()
                            editor.Reload(False)
                            editor.BeginInvoke(
                            Sub()
                                dgvrules.ClearSelection()
                                dgvrules.CurrentCell = dgvrules.Rows(tgtRow).Cells(0)
                            End Sub)
                        End If
                    End If
                Else
                    dgvrules.Rows(Destination.RowIndex).Cells(0).Selected = True
                End If

                Finish()

            Catch ex As Exception
                HandleError(MethodBase.GetCurrentMethod().Name, ex)
            End Try

        End Sub
        Private Sub Finish()

            Try
                RemoveHandler dgvrules.CellMouseMove, AddressOf CellMouseMove
                RemoveHandler dgvrules.CellMouseUp, AddressOf CellMouseUp
                DragLabel?.Dispose()
                DragLabel = Nothing

            Catch ex As Exception
                HandleError(MethodBase.GetCurrentMethod().Name, ex)
            End Try

        End Sub
    End Class
End Class

