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
Imports System.Data
Imports System.Linq
Imports System.Reflection
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports JsonTransformer
Imports Utilities
Public Class Loader

    Private WithEvents Progress As Progress
    Public Sub New(parent As Form, excel As Object, template As Template, clustersizemetres As Integer)

        Me.Parent = parent
        Me.Excel = excel
        Me.Template = template
        Me.clustersizemetres = clustersizemetres
        Me.Synchronous = parent Is Nothing

    End Sub
    Public Sub Cancel()

        Parser.Cancel()
        _Cancelled = True

    End Sub
    Public ReadOnly Property Cancelled As Boolean
    Private ReadOnly Property clustersizemetres As Integer
    Private Property Excel As Object
    Private Property Files As IEnumerable(Of String)
    Public Sub Load(Files As IEnumerable(Of String))

        Me.Files = Files

        If Synchronous Then
            Worker()
            TransferToExcel()
        Else
            Dim work As New Task(Sub() Worker())
            Progress = New Progress(Me)
            Progress.Show(Control.FromHandle(Parent.Handle))
            work.Start()
            work.ContinueWith(Sub() Parent.BeginInvoke(Sub() Postlude()))
        End If

    End Sub
    Private Sub Postlude()

        Try
            If Result.Rows.Count > 0 Then
                TransferToExcel()
            End If

            Progress.Close()

            If Result.Rows.Count = 0 Then
                ShowWarning($"No results!{vbCR}Is {Template.TemplateName} correct for {Template.FileName}?{vbCR}Are the Skip filters too broad?")
                Exit Sub
            End If

            If Cancelled Then
                ShowWarning($"Operation was cancelled, the {Result.Rows.Count:#,##0} results are probably incomplete")
            End If

            Parent.Close()

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try
    End Sub
    Private Property Parent As Form
    Private Property Parser As Parser
    Private Sub Progress_Cancelled() Handles Progress.Cancelled

        Cancel()

    End Sub
    Public ReadOnly Property Result As DataTable
    Private Property Synchronous As Boolean
    Private Property Template As Template
    Private Sub TransferToExcel()

        If Excel Is Nothing Then
            Exit Sub
        End If
        Dim updating As Boolean = CBool(Excel.ScreenUpdating)
        Dim calculation As Object = Excel.Calculation

        Try
            UpdateStatus($"Transferring {Result.Rows.Count} rows to Excel")

            Excel.ScreenUpdating = False
            calculation = Excel.Calculation
            Excel.Calculation = -4135 ' xlCalculationManual

            Dim workbook As Object = Excel.ActiveWorkBook
            ' -4167 = Excel.XlSheetType.xlWorksheet
            Dim worksheet As Object = Excel.ActiveSheet

            ' Use existing worksheet if it is empty
            If LastUsedRow(worksheet) > 1 OrElse
               LastUsedCol(worksheet) > 1 OrElse
               Not IsEmpty(worksheet.cells(1, 1).value) Then

                worksheet = workbook.
                        Worksheets.
                        Add(Type.Missing,
                            workbook.Worksheets(workbook.Worksheets.Count),
                            1,
                            -4167)
            End If

            Dim datatable As DataTable = Result

            ' Last column is our _ID_ unique key? Remove it
            If datatable.Columns(datatable.Columns.Count - 1).ColumnName = Importer.UNIQUEKEYNAME Then
                datatable.PrimaryKey = Nothing
                datatable.Columns.RemoveAt((datatable.Columns.Count - 1))
            End If

            ' Create worksheet columns, correctly formatted
            For c As Integer = 0 To datatable.Columns.Count - 1

                Dim datacol As DataColumn = datatable.Columns(c)
                worksheet.cells(1, c + 1).Value = datacol.ColumnName

                Select Case datacol.DataType.Name

                    Case "DateTime"
                        worksheet.Columns(c + 1).NumberFormat =
                            "yyyy/mm/dd hh:mm"

                    Case "TimeSpan"
                        worksheet.Columns(c + 1).NumberFormat =
                            "[h]:mm"

                End Select
            Next

            ' Output the datatable to Excel, adjusting data types
            For r As Integer = 0 To datatable.Rows.Count - 1

                If Cancelled Then
                    Exit For
                End If

                For c As Integer = 0 To datatable.Columns.Count - 1

                    Dim value As Object = datatable.Rows(r).Item(c)

                    Select Case value.GetType

                        Case GetType(DateTime)
                            value = DirectCast(value, DateTime).ToOADate

                        Case GetType(TimeSpan)
                            Dim ts As TimeSpan = DirectCast(value, TimeSpan)
                            value = ts.TotalSeconds / 86400

                    End Select

                    worksheet.cells(r + 2, c + 1).Value = value
                Next
            Next

            worksheet.Columns.AutoFit

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        Finally
            Excel.ScreenUpdating = updating
            Excel.Calculation = calculation

        End Try

    End Sub
    Private Sub Worker()

        Try
            Parser = New Parser(Template)

            _Result = New DataTable

            For Each file As String In Files

                UpdateStatus($"Rows so far {Result.Rows.Count:#,##0}. Loading {file}")

                Template.LoadFile(file)

                Dim table As DataTable = Parser.Parse()

                If Result.Rows.Count = 0 Then
                    _Result = table
                Else
                    Result.Merge(table)
                End If

                If Cancelled Then
                    Exit For
                End If
            Next

            If clustersizemetres > 0 Then
                _Result = Clustered(Result, clustersizemetres, AddressOf UpdateStatus)
            End If

            Dim sort As JsonTransformer.Rule = Template.Schema.Rules.OfType(Of RuleSort).SingleOrDefault

            If sort IsNot Nothing Then
                Dim sortedrows As IEnumerable(Of DataRow) = Result.
                                                            AsEnumerable.
                                                            OrderBy(Function(q) q.Field(Of DateTime)(sort.Name))
                _Result = sortedrows.CopyToDataTable
            End If

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        Finally
            If Synchronous Then
                Progress.BeginInvoke(Sub() Progress.Close())
            End If

        End Try
    End Sub
    Private Sub UpdateStatus(msg As String)

        If Not Synchronous Then
            Progress.BeginInvoke(
                Sub()
                    Progress.txtStatus.Text = msg
                    Progress.Refresh()
                End Sub)
        End If

    End Sub
End Class
