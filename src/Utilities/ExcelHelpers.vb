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
Imports System.Math
Imports System.Text
Imports ExcelFormulaParser
Imports Utilities
Public Module ExcelHelpers_

    Public Function A1toR1C1(ByVal cell As String, ByRef row As Integer, ByRef column As Integer) As Boolean

        Dim ch As String
        Dim ref As String = cell.ToUpper.Replace("$", "")
        Dim col As Long = 0

        Do
            ch = ref.Substring(0, 1)
            If ch >= "A" AndAlso ch <= "Z" Then
                col = col * 26 + System.Text.Encoding.ASCII.GetBytes(ch)(0) - System.Text.Encoding.ASCII.GetBytes("A")(0) + 1
                If col >= 16384 Then
                    Return False
                End If
                ref = ref.Substring(1)
            End If
        Loop While ch >= "A" AndAlso ch <= "Z" AndAlso ref.Length > 0

        If col < 1 OrElse col > 16384 Then
            Return False
        End If

        column = CInt(col)

        If ref.Length > 0 Then
            If Integer.TryParse(ref, row) Then
                Return True
            End If
        End If

        Return False

    End Function
    Public Sub DeleteWorksheet(ByRef excel As Object, workbook As Object, name As String)
        If workbook.WorkSheets.Count < 2 Then
            Exit Sub
        End If
        Dim wasalert As Boolean = CBool(excel.DisplayAlerts)
        excel.DisplayAlerts = False
        Try
            Dim worksheet As Object = Nothing
            If TryGetWorkSheet(workbook, name, worksheet) Then
                ' -1 = Excel.XlSheetVisibility.xlSheetVisible
                ' 0  = Excel.XlSheetVisibility.xlSheetHidden
                ' 2  = Excel.XlSheetVisibility.xlSheetVeryHidden
                worksheet.Visible = -1
                worksheet.Activate
                worksheet.Delete
            End If
        Catch ex As Exception
            Throw
        Finally
            excel.DisplayAlerts = wasalert
        End Try
    End Sub
    Public Function GetFormatted(ByRef sheet As Object, row As Integer, col As Integer) As Object

        Dim result As Object
        Dim cell As Object = sheet.Cells(row, col)

        If cell Is Nothing Then
            result = Nothing
        Else
            If cell.GetType.Name = "__ComObject" Then
                result = cell.Text
            Else
                result = cell
            End If
        End If

        If isExcelErrorCode(result) Then
            Throw New GeodesixException(ExcelErrorCode(CInt(result)))
        End If

        Return result

    End Function
    Public Function GetFormulaValues(ByVal excel As Object, ByVal worksheet As Object, ByVal row As Integer, ByVal col As Integer) As Object()

        Dim result(1) As Object
        Dim args As Integer = 1
        Dim depth As Integer = 0
        Dim token As ExcelFormulaToken
        Dim sb As New StringBuilder
        Dim val As String = CStr(worksheet.Cells(row, col).Formula)
        Dim formula As String = val
        Dim excelFormula As New ExcelFormula(val)

        If excelFormula.Count = 0 Then
            result(0) = formula
        Else

            result(0) = excelFormula(0).Value ' the function's name

            For i As Integer = 1 To excelFormula.Count - 1
                token = excelFormula(i)
                Select Case token.Type
                    Case ExcelFormulaTokenType.Argument ' ",", start a new argument
                        If depth = 0 Then
                            result(args) = sb.ToString
                            sb = New StringBuilder
                            args = args + 1
                            ReDim Preserve result(args)
                        Else
                            sb.Append(token.Value)
                        End If
                    Case ExcelFormulaTokenType.Function
                        sb.Append(token.Value)
                        If token.Subtype = ExcelFormulaTokenSubtype.Start Then
                            sb.Append("(")
                        ElseIf token.Subtype = ExcelFormulaTokenSubtype.Stop AndAlso depth > 0 Then
                            sb.Append(")")
                        End If
                    Case ExcelFormulaTokenType.Noop,
                        ExcelFormulaTokenType.Operand,
                        ExcelFormulaTokenType.OperatorInfix,
                        ExcelFormulaTokenType.OperatorPostfix,
                        ExcelFormulaTokenType.OperatorPrefix,
                        ExcelFormulaTokenType.Subexpression,
                        ExcelFormulaTokenType.Unknown,
                        ExcelFormulaTokenType.Whitespace
                        sb.Append(token.Value)
                End Select
                If token.Subtype = ExcelFormulaTokenSubtype.Start Then
                    depth = depth + 1
                ElseIf token.Subtype = ExcelFormulaTokenSubtype.Stop Then
                    depth = depth - 1
                End If
            Next
            result(args) = sb.ToString

            ' Now evaluate
            For i As Integer = 1 To args
                Dim work As Object
                work = excel.Evaluate(result(i))
                If work.GetType.ToString.Includes("ComObject") Then ' still an Excel object
                    If work.value Is Nothing Then
                        work = work.formula
                        If work.ToString.BeginsWith("=") Then
                            work = excel.Evaluate(work)
                        End If
                    Else
                        work = work.value
                    End If
                End If
                ' "If(a,b,c)" returns a System.Object[*] array with one element 0.0 or 1.0 (double)
                If work.GetType.ToString.Contains("[") Then ' An Array of some sort?
                    If work.GetLowerbound(0) = work.GetUpperbound(0) Then ' with exactly 1 element?
                        work = work(work.GetLowerbound(0)) ' replace with value
                    End If
                End If
                result(i) = work
            Next
        End If
        Return result
    End Function
    Public Function GetHeaderRow(sheet As Object) As Integer

        Try
            Dim lur As Integer = Min(10, LastUsedRow(sheet))
            Dim luc As Integer = Min(25, LastUsedCol(sheet))
            For hr As Integer = 1 To lur

                Dim sawlat As Boolean = False
                Dim sawlon As Boolean = False

                For col As Integer = 1 To luc

                    Dim v As Object = GetValue(sheet, hr, col)
                    If v Is Nothing Then
                        Continue For
                    End If

                    Dim vs As String = v.ToString.ToLower
                    If vs.BeginsWith("latitude") Then
                        sawlat = True
                    ElseIf vs.BeginsWith("longitude") Then
                        sawlon = True
                    End If
                Next

                If sawlat AndAlso sawlon Then
                    Return hr
                End If
            Next


        Catch ex As Exception
            Return 0
        End Try
        Return 0

    End Function
    Public Function GetValue(ByRef cell As Object) As Object

        Dim result As Object

        If cell Is Nothing Then
            result = Nothing
        Else
            If cell.GetType.Name = "__ComObject" Then
                If Microsoft.VisualBasic.Information.TypeName(cell) = "OLEObject" Then
                    result = cell.ToString
                Else
                    result = cell.Value
                End If
            Else
                result = cell
            End If
        End If

        If isExcelErrorCode(result) Then
            result = ExcelErrorCode(CInt(result))
        End If

        Return result

    End Function
    Public Function GetValueDouble(ByRef cell As Object) As Double

        Dim result As Object = GetValue(cell)

        If IsEmpty(result) Then
            Return Double.NaN
        End If

        Dim answer As Double
        If TypeOf (result) Is Double Then
            answer = CDbl(result) ' could be an object of type double. Convert it
            Return answer
        End If

        If Double.TryParse(result.ToString, answer) Then
            Return answer
        Else
            Return Double.NaN
        End If

    End Function
    Public Function GetValueString(ByRef cell As Object) As String

        Dim result As Object = GetValue(cell)

        If IsEmpty(result) Then
            Return ""
        End If

        Return result.ToString

    End Function
    Public Function GetValue(ByRef sheet As Object, row As Integer, col As Integer) As Object
        Try
            Return GetValue(sheet.Cells(row, col))
        Catch ex As Exception
        End Try
        Return 0
    End Function
    Public Function LastUsedCol(sheet As Object) As Integer
        Try
            If sheet Is Nothing Then
                Return 0
            End If

            Return CInt(sheet.UsedRange.Column + sheet.UsedRange.Columns.Count - 1)
        Catch ex As Exception
        End Try
        Return 0
    End Function
    Public Function LastUsedRow(sheet As Object) As Integer
        Try
            If sheet Is Nothing Then
                Return 0
            End If

            Return CInt(sheet.UsedRange.Row + sheet.UsedRange.Rows.Count - 1)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function TryGetWorkSheet(ByRef workbook As Object, name As String, ByRef worksheet As Object) As Boolean

        For i As Integer = 1 To CInt(workbook.Sheets.Count)
            If workbook.Sheets(i).Name = name Then
                worksheet = workbook.Sheets(i)
                Return True
            End If
        Next
        Return False
    End Function
    ' Maybe useful one day
    ' See bottom of MapTaskPane.vb
    '--------------------------- SetFocusToExcel https://www.add-in-express.com/forum/read.php?FID=1&TID=3616
    'Public Const GW_CHILD As Integer = 5
    'Public Const GW_HWNDNEXT As Integer = 2
    'Public Sub SetFocusToExcel()
    '    Dim h As IntPtr = APIUtils.GetXLMainWindowHandle(APIUtils.GetDesktopWindow())
    '    APIUtils.SetFocus(h)
    'End Sub

    'Public Class APIUtils
    '    Public Shared Function GetXLMainWindowHandle(ByVal DesktopHandle As IntPtr) As IntPtr
    '        Return FindMainWindowInProcess(DesktopHandle, String.Empty, "XLMAIN")
    '    End Function

    '    Friend Shared Function FindMainWindowInProcess(ByVal HWNDParent As IntPtr, ByVal WindowName As String, ByVal WindowClass As String) As IntPtr
    '        Dim FoundWindow As IntPtr = IntPtr.Zero
    '        Dim FindClass As String = String.Empty
    '        Dim FindName As String = String.Empty
    '        Dim WindowProcessID As UInteger
    '        Dim tempWindow As IntPtr = GetWindow(HWNDParent, GW_CHILD)

    '        While CInt(tempWindow) > 0
    '            FindName = GetWindowText(tempWindow)
    '            FindClass = GetClassName(tempWindow)
    '            Dim CompareClass As Boolean = ((FindClass.IndexOf(WindowClass) >= 0) OrElse (WindowClass = String.Empty))
    '            Dim CompareCaption As Boolean = ((FindName.IndexOf(WindowName) >= 0) OrElse (WindowName = String.Empty))

    '            If CompareClass AndAlso CompareCaption Then
    '                GetWindowThreadProcessId(tempWindow, WindowProcessID)

    '                If GetCurrentProcessId() = WindowProcessID Then
    '                    FoundWindow = tempWindow

    '                    If IsWindowVisible(FoundWindow) Then
    '                        Exit While
    '                    End If
    '                End If
    '            End If

    '            tempWindow = GetWindow(tempWindow, GW_HWNDNEXT)
    '        End While

    '        Return FoundWindow
    '    End Function

    '    <DllImport("user32.dll")>
    '    Public Shared Function SetFocus(ByVal hWnd As IntPtr) As IntPtr
    '    End Function
    '    <DllImport("user32.dll")>
    '    Public Shared Function GetDesktopWindow() As IntPtr
    '    End Function
    '    <DllImport("User32.dll", CharSet:=CharSet.Auto)>
    '    Public Shared Function GetWindow(ByVal hwnd As IntPtr, ByVal uCmd As Integer) As IntPtr
    '    End Function
    '    <DllImport("user32.dll")>
    '    Public Shared Function GetWindowText(ByVal hwnd As IntPtr,
    '    <[In]>
    '    <Out> ByVal lpClassName As StringBuilder, ByVal nMaxCount As Integer) As Integer
    '    End Function
    '    <DllImport("user32.dll")>
    '    Public Shared Function GetClassName(ByVal hwnd As IntPtr,
    '    <[In]>
    '    <Out> ByVal lpClassName As StringBuilder, ByVal nMaxCount As Integer) As Integer
    '    End Function
    '    <DllImport("user32.dll")>
    '    Public Shared Function IsWindowVisible(ByVal hWnd As IntPtr) As Boolean
    '    End Function
    '    <DllImport("user32.dll", SetLastError:=True)>
    '    Public Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, <Out> ByRef lpdwProcessId As UInteger) As UInteger
    '    End Function
    '    <DllImport("kernel32.dll")>
    '    Public Shared Function GetCurrentProcessId() As UInteger
    '    End Function

    '    Public Shared Function GetWindowText(ByVal handle As IntPtr) As String
    '        Dim lpBuffer As System.Text.StringBuilder = New System.Text.StringBuilder(255)

    '        If GetWindowText(handle, lpBuffer, 255) > 0 Then
    '            Return lpBuffer.ToString()
    '        Else
    '            Return ""
    '        End If
    '    End Function

    '    Public Shared Function GetClassName(ByVal handle As IntPtr) As String
    '        Dim className As System.Text.StringBuilder = New System.Text.StringBuilder(255)

    '        If GetClassName(handle, className, 255) > 0 Then
    '            Return className.ToString()
    '        Else
    '            Return ""
    '        End If
    '    End Function

    'End Class
End Module
