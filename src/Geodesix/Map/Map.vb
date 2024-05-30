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
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports AddinExpress.XL
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Seting
Imports Utilities

Public Class Map
    Inherits UserControl

    Public Event addOverlay(sender As Object, id As Integer, title As String)
    Public Event mapStyleChanged(sender As Object, ByVal style As String)
    Public Event startNavigating(sender As Object, e As EventArgs)
    Public Event finishNavigating(sender As Object, e As EventArgs)
    Public Event sourceChanged(sender As Object, ByVal url As String)
    Public Event visibilityChanged(sender As Object, visible As Boolean)

    Public ReadOnly Property GeodesiXEXL As GeodesiXEXL
    Private Property QuadrantsForm As QuadrantSelector = Nothing
    Friend ReadOnly Property ID As Guid
    Public ReadOnly Property BrowserReady As Boolean
    Private Property BrowserNavigationComplete As Boolean
    Public ReadOnly Property WorksheetProperties As WorksheetProperties
    Public Sub New()

        InitializeComponent()

        GeodesiXEXL = DirectCast(AddinExpress.MSO.ADXAddinModule.CurrentInstance, GeodesiXEXL)
        Mode = "Map"
        CustomLog.Logger.Debug($"Map New")

    End Sub
    Public Sub New(wsp As WorksheetProperties)

        InitializeComponent()
        WorksheetProperties = wsp
        Me.ID = wsp.ID
        GeodesiXEXL = DirectCast(AddinExpress.MSO.ADXAddinModule.CurrentInstance, GeodesiXEXL)
        Mode = "Map"
        CustomLog.Logger.Debug($"Map {ID} New")

    End Sub
    Private Sub Map_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsDesignerHosted(Me) Then
            Exit Sub
        End If

        CustomLog.Logger.Debug($"Map {ID} Load")

        Dim t As Task = InitaialiseWebview()

        t.ContinueWith(
            Sub()
                BeginInvoke(
                    Sub()
                        ' Catch console messages and log them
                        AddHandler CoreWebView2.GetDevToolsProtocolEventReceiver("Runtime.consoleAPICalled").
                            DevToolsProtocolEventReceived, AddressOf onConsoleMessage

                        CoreWebView2.CallDevToolsProtocolMethodAsync("Runtime.enable", "{}")
                        CoreWebView2.Navigate(_URL)

                    End Sub)
            End Sub)

    End Sub
    Private Sub onConsoleMessage(ByVal sender As Object, ByVal e As CoreWebView2DevToolsProtocolEventReceivedEventArgs)

        If e Is Nothing Then
            Exit Sub
        End If
        Dim raw As String = e.ParameterObjectAsJson
        If raw IsNot Nothing Then
            Dim json As JObject = JsonConvert.DeserializeObject(raw)
            Dim text As String = "Console: " & json("args")(0)("value").ToString
            CustomLog.Logger.Error(text)
            StatusError(text)
        End If

    End Sub

    Public Function AddMarkerRow(latitude As Double, longitude As Double) As Boolean

        Dim sheet As Object = Excel.ActiveSheet
        If sheet Is Nothing Then
            Return False
        End If

        Dim wbp As New WorkbookProperties(Excel.ActiveWorkbook)
        Dim drawing As New DrawingSettings("Geodesix", wbp)
        Dim icon As String = drawing.icon
        Dim hr As Integer = GetHeaderRow(sheet)
        If hr = 0 Then
            Return False
        End If

        Dim latcol As Integer = GetNamedColumn(sheet, hr, "Latitude")
        Dim loncol As Integer = GetNamedColumn(sheet, hr, "Longitude")

        ' Find the first free Latitude/Longitude row
        If latcol > 0 AndAlso loncol > 0 Then

            Dim newrow As Integer = hr
            Do
                newrow += 1
                If newrow > 1000 Then
                    Return False
                End If
            Loop Until (sheet.Cells(newrow, latcol).Value Is Nothing AndAlso
                        sheet.Cells(newrow, loncol).Value Is Nothing)

            sheet.Cells(newrow, latcol) = latitude
            sheet.Cells(newrow, loncol) = longitude

            Return True
        Else
            AfterNavigated(Sub() RunScript("dropMarker", latitude, longitude, "http://maps.google.com/mapfiles/kml/pal2/icon13.png", "", 24))
        End If
        Return False

    End Function
    Public Sub changeZoom(ByVal delta As Integer)

        ExecuteScript("changeZoom(" & delta.ToString & ")")

    End Sub
    Private _ClickLocation As String = ""
    Public Property ClickLocation As String
        Get
            Return _ClickLocation
        End Get
        Set(value As String)
            _ClickLocation = value
        End Set
    End Property
    Private _Display As Boolean = False
    Public Property Display As Boolean
        Get
            Return _Display
        End Get
        Set(value As Boolean)
            _Display = value
            RaiseEvent visibilityChanged(Me, value)
        End Set
    End Property
    Public ReadOnly Property Excel As Object
        Get
            Return GeodesiXEXL.Excel
        End Get
    End Property
    Public Function ExecuteScript(script As String,
                                  Optional callback As Action(Of String) = Nothing) As Object

        If Not BrowserReady Then
            Return "!Browser not ready"
        End If
        Dim result As Object = ""

        Try
            CustomLog.Logger.Debug($"ExecuteScript({script})")

            If BrowserReady AndAlso BrowserNavigationComplete Then

                If callback Is Nothing Then
                    CoreWebView2.ExecuteScriptAsync(script)
                    result = ""
                Else
                    ' Yes, DoEvents is disgusting, but there's no other way, as
                    ' ExecuteScriptAsync uses the UI message pump
                    ' (which is nearly as disgusting)
                    Dim t As Task(Of String) = CoreWebView2.ExecuteScriptAsync(script)
                    Dim done As Boolean
                    t.ContinueWith(
                            Sub(completed As Task(Of String))
                                done = True
                                callback(completed.Result)
                            End Sub)
                    Dim sw As New Stopwatch
                    sw.Start()
                    Do While Not done AndAlso sw.ElapsedMilliseconds < 500
                        Application.DoEvents() ' Eeuurk
                    Loop
                    If Not done Then
                        callback("!Timeout")
                    End If
                End If
            End If

            Return result

        Catch ex As Exception
            result = ex.Message

        End Try

        Return result

    End Function
    Private _Generator As Func(Of String)
    Public Property Generator As Func(Of String)
        Get
            Return _Generator
        End Get
        Set(value As Func(Of String))
            _Generator = value
        End Set
    End Property
    Private Function GetNamedColumn(sheet As Object, headerrow As Integer, columnname As String) As Integer

        For col As Integer = 1 To LastUsedCol(sheet)
            If GetValue(sheet, headerrow, col) = columnname Then
                Return col
            End If
        Next

        Return 0

    End Function
    Private Sub Map_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If MapTaskPane Is Nothing OrElse Not Me.Visible OrElse WorksheetProperties Is Nothing Then
            Exit Sub
        End If

        TaskPaneHeight = MapTaskPane.Height
        WorksheetProperties.PaneHeight = TaskPaneHeight

        TaskPaneWidth = MapTaskPane.Width
        WorksheetProperties.PaneWidth = TaskPaneWidth

    End Sub
    Private _MapStyle As String
    Public Property MapStyle As String
        Get
            Return _MapStyle
        End Get
        Set(value As String)

            If value = _MapStyle Then
                Exit Property
            End If

            Mode = "Map"

            ' 'minimal','hybrid','roadmap','satellite','terrain'
            Select Case value.Substring(0, 1).ToLower
                Case "m"
                    _MapStyle = "minimal"
                Case "h"
                    _MapStyle = "hybrid"
                Case "r"
                    _MapStyle = "roadmap"
                Case "s"
                    _MapStyle = "satellite"
                Case "t"
                    _MapStyle = "terrain"
                Case Else
                    _MapStyle = "hybrid"
            End Select

            If WorksheetProperties IsNot Nothing Then
                WorksheetProperties.MapStyle = _MapStyle
            End If
            CustomLog.Logger.Debug("MapStyle {0}", _MapStyle)
            ExecuteScript("setMapStyle('" & _MapStyle & "')")

            RaiseEvent mapStyleChanged(Me, _MapStyle)
        End Set
    End Property
    Private ReadOnly Property MapTaskPane As MapTaskPane
        Get
            Return GeodesiXEXL?.MapTaskPane
        End Get
    End Property
    Private _Mode As String = ""
    Public Property Mode As String
        Get
            Return _Mode
        End Get
        Set(value As String)
            If value = _Mode Then
                Exit Property
            End If
            _Mode = value

            If Mode = "Map" Then
                MapToolstrip.Visible = True
                AddressToolStrip.Visible = False
                StatusStrip1.Visible = False
                ReGenerate()

            ElseIf Mode = "Browser" Then
                MapToolstrip.Visible = False
                AddressToolStrip.Visible = True
                URL = $"https://www.google.com/search?hl={Settings.Language}&lr={Settings.Language}"

            ElseIf Mode = "Hidden" Then
                Display = False
            End If
        End Set
    End Property
    Public Sub Navigate(ByVal address As String)

        Try
            Mode = "Browser"

            ' Make it into a decent URL
            Dim colon As Integer = address.IndexOf(":")
            If colon < 0 Then
                address = "https://" & address
                txtAddress.Text = address
            End If

            URL = address

        Catch ex As Exception
            ShowBox($"Navigate failed: {ex.Message}")
        End Try

    End Sub
    Protected Overrides Sub OnPreviewKeyDown(e As PreviewKeyDownEventArgs)

        MyBase.OnPreviewKeyDown(e)

    End Sub
    Public Sub OpenDevTools()

        CoreWebView2?.OpenDevToolsWindow()

    End Sub
    Public Function Overlay(ByVal id As Integer, ByVal visible As String) As Object

        Return RunScript("overlay", id, visible)

    End Function
    Public Sub PopulateSheetsDropdown()

        Dim workbook As Object = Nothing

        Try
            Dim sheetnames As New List(Of String)
            Dim maxwidth As Integer = 0
            Dim textwidth As Integer

            Dim excel As Object = GeodesiXEXL.Excel
            sheetnames.Clear()

            sheetnames.Add("")
            If excel.ActiveWorkbook Is Nothing Then
                Exit Sub
            End If
            workbook = excel.ActiveWorkbook
            For i As Integer = 1 To CInt(workbook.Worksheets.Count)

                Dim ws As Object = workbook.Worksheets(i)
                Dim wsname As String = ws.Name
                ' excel.XlSheetVisibility.xlSheetVisible = -1, but we're not going to bring in the entire Excel runtime just for that
                If ws.Visible = -1 Then
                    If wsname <> GEOCODECACHEWORKSHEET AndAlso wsname <> TRAVELCACHEWORKSHEET Then
                        sheetnames.Add(wsname)
                    End If
                End If
            Next

            cmbSheets.Items.Clear()

            Using graphics As Graphics = Me.CreateGraphics

                For Each sheet As String In sheetnames.OrderBy(Function(q) q)
                    textwidth = CInt(graphics.MeasureString(sheet, cmbSheets.Font).Width)
                    If textwidth > maxwidth Then
                        maxwidth = textwidth
                    End If
                    cmbSheets.Items.Add(sheet)
                Next
            End Using
            cmbSheets.Width = maxwidth + 30

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean

        If keyData = Keys.Control And Keys.C Then
            btnCopy_Click(Me, New EventArgs)
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function
    Public Sub ReGenerate(Optional andthen As Action(Of String) = Nothing)

        If Generator IsNot Nothing Then
            URL = Generator()()
        End If

    End Sub
    Public Sub Reload()

        If BrowserReady Then
            CoreWebView2.Reload()
        End If

    End Sub
    Public Function RunScript(func As String, ParamArray args As Object()) As Object

        Dim params As New StringBuilder
        For i As Integer = 0 To args.Count - 1
            Dim arg As Object = args(i)
            If TypeOf (arg) Is Boolean Then
                If CBool(arg) Then ' "True" in .Net, "true" in JS
                    params.Append("true")
                Else
                    params.Append("false")
                End If
            ElseIf TypeOf (arg) Is String AndAlso Not arg.ToString.BeginsWith("[") Then
                params.Append("'")
                params.Append(arg.replace("'", "''"))
                params.Append("'")
            Else
                params.Append(arg)
            End If
            If i < args.Count - 1 Then
                params.Append(",")
            End If
        Next
        Dim command As String = func & "(" & params.ToString & ")"

        Return ExecuteScript(command)

    End Function
    Private Sub SetFocus(lat As Double, lng As Double, zoom As Integer)

        ExecuteScript($"setCentre({lat},{lng}); setZoom(14);")

    End Sub
    Public ReadOnly Property SheetName As String = ""
    Public Sub SetSheet(sheetname As String)

        _SheetName = sheetname
        cmbSheets.Text = _SheetName
        If _SheetName = "" Then
            Generator = Function()
                            Return GeodesiXEXL.DefaultGeocoder
                        End Function
        Else
            Generator = Function()
                            Dim filename As String = GetTempFile("html")
                            If Not MakeDecorated(Excel, _SheetName, filename) Then
                                ShowWarning($"Nothing was drawn, worksheet {sheetname} doesn't have 'Latitude' and 'Longitude' fields")
                                Return GeodesiXEXL.DefaultGeocoder
                            End If
                            Return filename
                        End Function
        End If

        ReGenerate()

    End Sub
    Public Sub ShowDefaultGeocoder()

        Mode = "Map"
        URL = GeodesiXEXL.DefaultGeocoder

    End Sub
    Private Sub StatusInfo(text As String)

        StatusText.ForeColor = Color.Black
        Status(text)

    End Sub
    Private Sub StatusError(text As String)

        StatusText.ForeColor = Color.Red
        Status(text)

    End Sub
    Private Sub Status(text As String)

        If text.Contains("deprecated") Then ' AdDomListener is deprecated, we know
            Exit Sub
        End If

        If IsEmpty(text) Then
            StatusText.Text = ""
            StatusStrip1.Visible = False
        Else
            If StatusStrip1.Height > Me.Height \ 2 Then
                Exit Sub
            End If
            StatusStrip1.Visible = True
            StatusText.Text = StatusText.Text & If(IsEmpty(StatusText.Text), "", vbCR) & text
        End If

    End Sub

    Public Sub SubmitQuery(ByVal query As String)

        CustomLog.Logger.Debug("SubmitQuery {0}", query)

        RunScript("submitQuery", query)

    End Sub
    Public Property TaskPaneHeight As Integer
    Public Property TaskPaneWidth As Integer

    Private _TaskPanePosition As ADXExcelTaskPanePosition = ADXExcelTaskPanePosition.Right
    Public Property TaskPanePosition As ADXExcelTaskPanePosition
        Get
            Return _TaskPanePosition
        End Get
        Set(value As ADXExcelTaskPanePosition)
            _TaskPanePosition = value
            If WorksheetProperties IsNot Nothing Then
                WorksheetProperties.TaskPanePosition = value
            End If
        End Set
    End Property
    Public Overrides Function ToString() As String
        Return $"Map {ID} URL {URL} display {Display} visible {Visible}"
    End Function
    Private _URL As String = ""
    Public Property URL As String
        Get
            Return _URL
        End Get
        Set(value As String)

            _URL = value
            txtAddress.Text = _URL

            If BrowserReady AndAlso Not IsEmpty(_URL) Then
                CoreWebView2.Navigate(_URL)
            End If
        End Set
    End Property
    Private _Zoom As Integer = 4
    Public Property Zoom As Integer
        Get
            Return _Zoom
        End Get
        Set(value As Integer)
            If value = _Zoom Then
                Exit Property
            End If
            _Zoom = value
            ExecuteScript($"setZoom({_Zoom});")
        End Set
    End Property
    Public Sub zoomToContent()

        RunScript("zoomToContent()")

    End Sub



    ' for the record, may be useful in future
    'Private Sub Map_ADXKeyFilter(ByVal sender As Object, ByVal e As AddinExpress.XL.ADXKeyFilterEventArgs) Handles Me.ADXKeyFilter
    '    Debug.WriteLine("Pressed " & e.KeyCode.ToString & " " & e.KeyValue.ToString)
    '    e.Action = AddinExpress.XL.ADXKeyFilterAction.SendToTaskPane
    '    ADXPostMessage(0, 1234567)
    'End Sub
    'Private Sub Map_ADXPostMessageReceived(ByVal sender As Object, ByVal e As AddinExpress.XL.ADXPostMessageReceivedEventArgs) Handles Me.ADXPostMessageReceived
    '    If e.LParam = 1234567 Then
    '        Dim addin As GeodesiXEXL = AddinExpress.MSO.ADXAddinModule.CurrentInstance
    '        Dim tp As AddinExpress.XL.ADXExcelTaskPanesCollectionItem = addin.MapTaskPane
    '        tp.TaskPaneInstance.Activate()
    '        Debug.WriteLine("activated")
    '        ' GoogleMap.activate()
    '    End If
    'End Sub

End Class

