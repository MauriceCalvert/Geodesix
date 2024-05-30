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
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports NUglify
Imports Seting
Imports Utilities
Public MustInherit Class Exporter
    Protected MustOverride Sub Prologue()
    Protected MustOverride Sub Body(final As Boolean)
    Protected MustOverride Sub Epilogue()

    Protected ReadOnly Property Columns As New List(Of Integer)
    Protected ReadOnly Property Excel As Object
    Protected ReadOnly Property Keys As New List(Of String)
    Protected ReadOnly Property Lines As New List(Of String)
    Protected ReadOnly Property Sheet As Object
    Protected ReadOnly Property Target As String ' the path+filename that we will write to
    Protected ReadOnly Property Template As List(Of String)
    Private ReadOnly Property Variables As New Dictionary(Of String, Variable)(StringComparer.OrdinalIgnoreCase)

    Public Sub New(ByVal excel As Object, ByVal sheetname As String, ByRef target As String, template As List(Of String))

        _Excel = excel
        _Target = target
        _Template = template

        If IsEmpty(target) Then
            Throw New GeodesixException("Exporter file name missing")
        End If

        If excel IsNot Nothing Then

            Dim wb As Object = excel.ActiveWorkBook

            Variable("workbook").Value = wb.Name.ToString
            Variable("worksheet").Value = sheetname

            WorkBookProperties = New WorkbookProperties(wb)
            Dim drawing As New DrawingSettings("Geodesix", WorkBookProperties)

            For Each setting As Setting In drawing.Items.Values
                Variables.Add(setting.Name, New Variable(setting.Name, setting.Value, setting.Initial IsNot Nothing))
            Next

            If Not TryGetWorkSheet(wb, sheetname, _Sheet) Then
                Throw New GeodesixException($"Worksheet '{sheetname}' not found")
            End If

        End If

    End Sub
    Public Sub AddRow(line As String)

        Lines.Add(line)

    End Sub
    Public Sub AddRows(lines As IEnumerable(Of String))

        For Each line As String In lines
            AddRow(line)
        Next

    End Sub
    Public Sub FlushJavascript(varname As String)

        Dim js As String = String.Join(vbCRLF, Lines) & vbCRLF
        If Settings.Minify = "True" Then
            js = Minify(js, ".js")
        End If
        Variable(varname).Value = js
        Lines.Clear()

    End Sub
    Public Sub FlushRaw(varname As String)

        Variable(varname).Value = String.Join(vbCRLF, Lines) & vbCRLF
        Lines.Clear()

    End Sub
    Public ReadOnly Property hasContent As Boolean
        Get
            Return HeaderRow > 0
        End Get
    End Property
    Protected Function HaveAllKeys(keynames As IEnumerable(Of String)) As Boolean

        Return keynames.All(Function(q) Keys.HasAny(q))

    End Function
    Protected Function HaveAllValues(keynames As IEnumerable(Of String)) As Boolean

        Return keynames.All(Function(q) Keys.HasAny(q) AndAlso Variable(q).HasValue)

    End Function
    Protected Function HaveSomeValues(keynames As IEnumerable(Of String)) As Boolean

        Return keynames.Any(Function(q) Keys.HasAny(q) AndAlso Variable(q).HasValue)

    End Function
    Private _HeaderRow As Integer = -1
    Protected ReadOnly Property HeaderRow As Integer
        Get
            If _HeaderRow = -1 Then
                Dim hr As Integer = GetHeaderRow(Sheet)
                _HeaderRow = hr
            End If

            Return _HeaderRow

        End Get
    End Property
    Public Sub Include(filename As String)

        Variable(filename).Value = $"<#!{filename}#>"

    End Sub
    Private Sub LoadKeys()

        Dim variablecolumns As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim lastcol As Integer = LastUsedCol(Sheet)

        ' Load column indexes and keys
        Columns.Add(0)
        ' Keys are indexed by column number, so key(0)=""
        Keys.Add("")
        Variable("").Value = ""

        For col As Integer = 1 To lastcol
            Dim key As String = CStr(GetValue(Sheet, HeaderRow, col))
            If Not IsEmpty(key) Then
                Columns.Add(col)
                Keys.Add(key)
            End If
        Next

        ' Make sure each 'set' is followed by a 'value'
        Dim keycolumn As Integer = 0
        Dim valuecolumn As Integer = 0

        For col As Integer = 1 To Columns.Count - 1

            Dim key As String = Keys(col)

            Select Case key

                Case "key"
                    If keycolumn > 0 Then
                        Throw New GeodesixException($"repeated 'key' column without subsequent 'value' column")
                    End If
                    keycolumn = col
                    valuecolumn = 0

                Case "value"
                    If valuecolumn > 0 Then
                        Throw New GeodesixException($"'value' column without preceding 'key' column")
                    End If
                    valuecolumn = col
                    keycolumn = 0

                Case Else
                    If Not variablecolumns.ContainsKey(key) Then
                        variablecolumns.Add(key, col)
                    End If
            End Select
        Next

        If keycolumn > 0 Then
            Throw New GeodesixException($"'key' in column {keycolumn} without subsequent 'value' column")
        End If
        If valuecolumn > 0 Then
            Throw New GeodesixException($"'value' in column {valuecolumn} without preceding 'key' column")
        End If
    End Sub
    Private Function Minify(code As String, filetype As String) As String

        Dim before As Integer = code.Length

        Select Case filetype.ToLower
            Case ".htm", ".html"
                code = Uglify.Html(code).Code
            Case ".js"
                code = Uglify.Js(code).Code
            Case ".css"
                code = Uglify.Css(code).Code
        End Select

        Dim after As Integer = code.Length
        Debug.WriteLine($"{filetype} {before} {after} {CInt(after / before * 100)}%")
        Return code

    End Function
    Private Sub Process()

        ' Reset keys at each row
        For Each key As String In Keys.Where(Function(q) Not Variable(q).Mandatory)
            Variable(key).Value = Nothing
        Next

        Dim lur As Integer = LastUsedRow(Sheet)

        For row As Integer = HeaderRow + 1 To lur

            If CBool(Sheet.Cells(row, 1).EntireRow.Hidden) Then
                Continue For
            End If

            For col As Integer = 1 To Columns.Count - 1

                Dim column As Integer = Columns(col)
                Dim key As String = CStr(GetValue(Sheet, HeaderRow, column))
                Dim value As String = CStr(GetFormatted(Sheet, row, column))
                Variable(key).Value = value
            Next
            Body(row = lur)
        Next
    End Sub
    Private ReadOnly Property WorkBookProperties As WorkbookProperties
    Public ReadOnly Property Results As List(Of String)
    Protected Function Substitute(input As String) As String

        Dim answer As String = input

        Dim safety As Integer = 0

        Do While answer.IndexOf("#") >= 0

            Dim start As Integer = answer.IndexOf("<#")
            If start < 0 Then ' all substituted
                Exit Do
            End If

            Dim finish As Integer = answer.IndexOf("#>", start + 2)
            If finish < 0 Then
                answer = $"!Missing #>: {answer}"
                Exit Do
            End If

            If finish - start < 3 Then ' empty variable
                answer = answer.Substring(0, start) & answer.Substring(finish + 2)
            Else
                ' Nested includes start with '!' for example Include("geodesix.js")
                ' will create a variable <#!geodesix.js#>
                Dim key As String = answer.Substring(start + 2, finish - start - 2)
                Dim value As String

                If key.BeginsWith("!") Then ' nested include

                    key = key.Substring(1)
                    Dim templatefile As String = Path.Combine(GetExecutingPath, "templates", key)
                    Dim content As String = String.Join(vbCRLF, ReadFileToString(templatefile))
                    value = Substitute(content) ' recursion
                    If Settings.Minify = "True" Then
                        value = Minify(value, Path.GetExtension(key))
                    End If

                Else ' vanilla variable
                    value = Variable(key).Value
                End If

                answer = answer.Substring(0, start) & value & answer.Substring(finish + 2)
            End If
        Loop
        If answer.Contains("#>") Then
            Throw New GeodesixException($"!Missing <#: {answer}")
        End If
        Return answer
    End Function
    Public Sub Transform()

        _Results = New List(Of String)
        If Sheet Is Nothing OrElse HeaderRow <= 0 Then
            Prologue()
            Epilogue()
        Else
            LoadKeys()
            Prologue()
            Process()
            Epilogue()
        End If

        For row As Integer = 0 To Template.Count - 1
            Dim line As String = Template(row)
            Results.Add(Substitute(line))
        Next

    End Sub

    Protected Property Variable(key As String) As Variable
        Get
            Dim result As Variable = Nothing
            If Not Variables.TryGetValue(key, result) Then
                result = New Variable(key)
                Variables.Add(key, result)
            End If
            Return result
        End Get
        Set(value As Variable)
            Variable(key) = value
        End Set
    End Property
End Class
