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
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms
Imports ExcelFormulaParser
Imports Utilities
Public Class FunctionBuilder

    Private Property GeodesixEXL As GeodesiXEXL
    Private Property Excel As Object
    Private Property Sheet As Object
    Private ReadOnly Property Target As Object
    Private WithEvents Methods As Methods
    Private WithEvents Method As Method
    Private WithEvents adxExcelEvents As AddinExpress.MSO.ADXExcelAppEvents
    Public Sub New(geodesixexl As GeodesiXEXL, cell As Object, methods As Methods, method As Method)

        InitializeComponent()
        Me.GeodesixEXL = geodesixexl
        Me.Excel = geodesixexl.Excel
        Me.Target = cell
        Me.Methods = methods
        Me.Method = method
        Sheet = Excel.ActiveSheet
        Dim returns As String = method.Returns
        Me.Text = $"{method.Name} - {method.Description}"
        geodesixexl = CType(AddinExpress.MSO.ADXAddinModule.CurrentInstance, GeodesiXEXL)
        adxExcelEvents = geodesixexl.adxExcelEvents

    End Sub

    Private Sub FunctionBuilder_Load(sender As Object, e As EventArgs) Handles Me.Load

        Draw()
        LoadFormula()
        Evaluate()

    End Sub
    Private Sub Draw()

        Try
            GroupBox1.Text = Method.Name
            Dim x As Integer = 10
            Const STARTY As Integer = 20
            Dim y As Integer = STARTY
            Dim lineheight As Integer = 20
            Dim lineno As Integer = 0
            Dim firstline As FunctionLine = Nothing

            ' Get the current formula, if there is one
            Dim formula As String = Target.Formula.ToString ' This is culture-invariant. i.e. with separator ","
            Dim values As IEnumerable(Of Object) = Nothing

            If Not IsEmpty(formula) Then
                Dim excelFormula As New ExcelFormula(formula)
                Dim funcname As String = excelFormula(0).Value
                values = excelFormula.
                    Where(Function(q) q.Subtype = ExcelFormulaTokenSubtype.Range)
                If values.Count <> Method.Parameters.Count Then
                    values = Nothing
                End If
            End If

            For p As Integer = 0 To Method.Parameters.Count - 1

                Dim parm As Parameter = Method.Parameters(p)
                If values IsNot Nothing Then
                    parm.Formula = values(p).Value
                End If

                If parm.Choices?.Count = 1 Then
                    Dim methodname As String = parm.Choices(0).ToString
                    If methodname.EndsWith("()") Then ' A function in GeodesixEXL that returns IEnumerable(of object)
                        methodname = methodname.Substring(0, methodname.Length - 2)
                        Dim method As MethodInfo = GeodesixEXL.GetType.GetMethod(methodname)
                        parm.Choices = method.Invoke(GeodesixEXL, {})
                    End If
                End If

                Dim line As New FunctionLine(parm)
                If firstline Is Nothing Then
                    firstline = line
                End If
                line.Index = lineno
                lineno += 1
                line.Name = $"Line{lineno}"
                GroupBox1.Controls.Add(line)
                line.Left = x
                line.Top = y
                line.Visible = True
                lineheight = line.Height
                y += line.Height + 3
                AddHandler line.ChangedFocus, AddressOf Focus_Changed
            Next
            GroupBox1.Height = STARTY + Method.Parameters.Count * (lineheight + 5)
            Dim below As Integer = GroupBox1.Top + GroupBox1.Height + 5
            txtAnswer.Top = below
            btnOK.Top = txtAnswer.Top + txtAnswer.Height + 5
            btnCancel.Top = btnOK.Top
            btnCopy.Top = btnOK.Top

            Dim defaultline As FunctionLine =
                GroupBox1.
                Controls.
                OfType(Of FunctionLine).
                Where(Function(q) q.Parameter.Value Is Nothing).FirstOrDefault

            If defaultline IsNot Nothing Then
                BeginInvoke(
                    Sub()
                        GroupBox1.Focus()
                        defaultline.txtSource.Focus()
                    End Sub)
            End If

            ClientSize = New Drawing.Size(ClientSize.Width, btnOK.Top + btnOK.Height + 5)

            If firstline IsNot Nothing Then
                BeginInvoke(Sub() firstline.Focus())
            End If

            AddHandler adxExcelEvents.SheetSelectionChange, AddressOf adxExcelEvents_SheetSelectionChange

        Catch ex As Exception
            ShowError($"Error drawing form, {ex.Message}")
            Me.Close()

        End Try

    End Sub
    Private Sub Evaluate()

        Try
            If Method.Parameters.All(Function(q) q.Formula IsNot Nothing) Then

                Dim argv As String = String.Join(", ", Method.Parameters.Select(Function(q) Quoted(q.Value)))
                txtAnswer.Text = $"={Method.Name}({argv})"
                btnOK.Enabled = True
                BeginInvoke(Sub()
                                If btnCancel.Focused Then
                                    btnOK.Focus()
                                End If
                            End Sub)
            Else
                btnOK.Enabled = False
            End If

        Catch ex As Exception
            ShowError($"Failed to evaluate formula, {ex.Message}")
            Me.Close()

        End Try

    End Sub
    Public ReadOnly Property FocusedControl As Control
    Private Sub Focus_Changed(sender As Control)

        If sender.Tag = "Source" Then
            _FocusedControl = sender
        Else
            _FocusedControl = Nothing
        End If

    End Sub
    Private Sub Help_Requested(sender As Object, hlpevent As HelpEventArgs) Handles Me.HelpRequested

        Try
            hlpevent.Handled = True
            GeodesixEXL?.ShowHelp($"ExcelHelp.html", Method.Name)

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub Help_ButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked

        Try
            e.Cancel = True
            GeodesixEXL?.ShowHelp($"ExcelHelp.html", Method.Name)

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub LoadFormula()

        Try
            Dim formula As String = Target.Formula.ToString ' This is culture-invariant. i.e. with separator ","
            If Target.value2 Is Nothing Then
                Exit Sub
            End If

            Dim value As String = Target.Value2.ToString
            If formula = value Then ' a constant
                Exit Sub
            End If

            Dim excelFormula As New ExcelFormula(formula)
            ' Excel functions like Distance are Distance(arg1, arg2,...
            ' SubTypes like Draw Line are Draw(Line, arg1, arg2,...
            ' Find the item that precedes arg1.
            Dim i As Integer = 0
            Do While i < excelFormula.Count AndAlso
                excelFormula(i).Value <> Method.Name

                i += 1
            Loop
            i += 1
            Dim j As Integer = 0
            Do
                Dim token As ExcelFormulaToken = excelFormula(i)

                If token.Type = ExcelFormulaTokenType.Operand Then
                    Select Case token.Subtype

                        Case ExcelFormulaTokenSubtype.Range, ExcelFormulaTokenSubtype.Number
                            Method.Parameters(j).Formula = token.Value

                        Case ExcelFormulaTokenSubtype.Text
                            Method.Parameters(j).Formula = Quoted(excelFormula(i).Value)

                        Case Else
                            Dim xx As Integer = 0

                    End Select
                    j += 1
                End If
                i += 1
            Loop Until i >= excelFormula.Count OrElse j >= Method.Parameters.Count

        Catch ex As Exception
            ShowError($"Unable to load formula, {ex.Message}")
            Me.Close()

        End Try

    End Sub
    Private Sub Method_Changed(sender As Object, value As Object) Handles Method.Changed

        Evaluate()

    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        Dim formula As String = "?"

        Try
            ' Adjust absolute/relative references in the fields 
            Dim targetrow As Integer = Target.Row
            Dim targetcol As Integer = Target.Column
            Dim hr As Integer = GetHeaderRow(Sheet)
            Dim adjusted As Boolean = False

            If hr > 0 Then
                ' Do any of the parameters refer to the header row?
                If Method.Parameters.Any(
                    Function(q)
                        Dim row As Integer
                        Dim col As Integer
                        Dim ref As String = q.Formula
                        If A1toR1C1(ref, row, col) AndAlso
                           row = hr Then
                            Return True
                        End If
                        Return False
                    End Function) Then

                    adjusted = True ' Yes, adjust references

                    For i As Integer = 0 To Method.Parameters.Count - 1

                        Dim row As Integer
                        Dim col As Integer
                        Dim ref As String = Method.Parameters(i).Formula ' $A$1

                        If A1toR1C1(ref, row, col) Then

                            Dim parts As String() = ref.Replace("$", " ").Trim(" ").Split(" ") ' {"A", "1"}

                            If row = hr AndAlso col = targetcol Then
                                Method.Parameters(i).Formula = parts(0) & "$" & parts(1) ' A$1
                            ElseIf row = targetrow Then
                                Method.Parameters(i).Formula = "$" & parts(0) & parts(1) ' $A1
                            Else
                                Method.Parameters(i).Formula = parts(0) & parts(1) ' A1
                            End If
                        End If
                    Next
                End If
            End If

            If Not adjusted Then
                ' Make all references relative
                For i As Integer = 0 To Method.Parameters.Count - 1
                    Dim row As Integer
                    Dim col As Integer
                    Dim ref As String = Method.Parameters(i).Formula ' $A$1
                    If A1toR1C1(ref, row, col) Then
                        Method.Parameters(i).Formula = ref.Replace("$", "") ' A1
                    End If
                Next

            End If

            formula = Methods.Generate(Method)
            Target.Formula = "=" & formula

        Catch ex As Exception
            ShowError($"Invalid formula {formula}")

        Finally
            Me.Close()

        End Try

    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Me.Close()

    End Sub
    Private Sub adxExcelEvents_SheetSelectionChange(sender As Object, sheet As Object, range As Object)

        Try
            ' When a 'source' control of the function line is selected and
            ' he clicks on a cell, copy the address into the source and
            ' move to the next field
            If range.Cells.Count <> 1 Then
                Exit Sub
            End If

            Dim targetrow As Integer = Target.Row
            Dim targetcol As Integer = Target.Column

            If FocusedControl IsNot Nothing Then

                Dim address As String = CStr(range.Address) ' Absolute $A$1

                If TypeOf FocusedControl Is ComboBox Then

                    Dim combo As ComboBox = DirectCast(FocusedControl, ComboBox)
                    combo.SelectedItem = address
                    combo.Text = address

                ElseIf TypeOf FocusedControl Is TextBox Then
                    FocusedControl.Text = address
                End If

                Dim line As FunctionLine = DirectCast(FocusedControl.Parent, FunctionLine)
                Dim nextline As FunctionLine = DirectCast(GroupBox1.Controls((line.Index + 1) Mod GroupBox1.Controls.Count), FunctionLine)
                BeginInvoke(Sub() nextline.txtSource.Focus())
            End If

        Catch ex As Exception ' If something goes wrong, tough
        End Try

    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click

        Dim fun As String = Methods.Generate(Method)
        Clipboard.SetText(fun)
        txtAnswer.Text = $"{fun} copied to clipboard"

    End Sub

    Private Sub FunctionBuilder_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        RemoveHandler adxExcelEvents.SheetSelectionChange, AddressOf adxExcelEvents_SheetSelectionChange

    End Sub
End Class