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
Imports AddinExpress.XL

Partial Public Class GeodesiXEXL

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New()

        Application.EnableVisualStyles()

        'This call is required by the Component Designer
        InitializeComponent()

        'Please add any initialization code to the AddinInitialize event handler

    End Sub

    'Component overrides dispose to clean up the component list.
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

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeodesiXEXL))
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.adxExcelEvents = New AddinExpress.MSO.ADXExcelAppEvents(Me.components)
        Me.GeodesiXCommandBar = New AddinExpress.MSO.ADXCommandBar(Me.components)
        Me.cbbMap = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbBrowser = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbEarth = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbTSS = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbImportJSON = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbImportXML = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbExportJSON = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbExportHTML = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbExportKML = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbOptions = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbDefaults = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbIcons = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.btnHelp = New AddinExpress.MSO.ADXCommandBarPopup(Me.components)
        Me.cbbHelpReadme = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbHelpVBA = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbHelpImpExp = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbHelpFunctions = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.cbbHelpOnline = New AddinExpress.MSO.ADXCommandBarButton(Me.components)
        Me.GeodesiXRibbonTab = New AddinExpress.MSO.ADXRibbonTab(Me.components)
        Me.RibbonGroupGeodesix = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.RibbonViewMap = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonViewBrowser = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonViewEarth = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonLocate = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonGroupInsert = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.RibbonInsertFunction = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonInsertField = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonInsertIcon = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonGroupImport = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.RibbonImportStructured = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonGroupExport = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.RibbonExportGeoJSON = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonExportHTML = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonExportKML = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonExportTabbed = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonGroupTools = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.RibbonViewTSS = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonOptions = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonDrawing = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonSettings = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonHelp = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonDebugging = New AddinExpress.MSO.ADXRibbonGroup(Me.components)
        Me.RibbonDeveloperTools = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonShowSource = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonShowCache = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.RibbonPurgeCache = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.rbtnWeb = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        Me.TaskPanesManager = New AddinExpress.XL.ADXExcelTaskPanesManager(Me.components)
        Me.TheTaskPane = New AddinExpress.XL.ADXExcelTaskPanesCollectionItem(Me.components)
        Me.RibbonExportJSON = New AddinExpress.MSO.ADXRibbonButton(Me.components)
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "map.png")
        Me.ImageList.Images.SetKeyName(1, "chrome.png")
        Me.ImageList.Images.SetKeyName(2, "earth.png")
        Me.ImageList.Images.SetKeyName(3, "fx.png")
        Me.ImageList.Images.SetKeyName(4, "html.png")
        Me.ImageList.Images.SetKeyName(5, "kml.png")
        Me.ImageList.Images.SetKeyName(6, "xml.png")
        Me.ImageList.Images.SetKeyName(7, "json.png")
        Me.ImageList.Images.SetKeyName(8, "map_go.png")
        Me.ImageList.Images.SetKeyName(9, "setting_tools.png")
        Me.ImageList.Images.SetKeyName(10, "help.png")
        Me.ImageList.Images.SetKeyName(11, "script.png")
        Me.ImageList.Images.SetKeyName(12, "document_import.png")
        Me.ImageList.Images.SetKeyName(13, "car.png")
        Me.ImageList.Images.SetKeyName(14, "mixer.png")
        Me.ImageList.Images.SetKeyName(15, "draw_ink.png")
        Me.ImageList.Images.SetKeyName(16, "table_export.png")
        Me.ImageList.Images.SetKeyName(17, "eye.png")
        Me.ImageList.Images.SetKeyName(18, "math.png")
        Me.ImageList.Images.SetKeyName(19, "map_magnify.png")
        Me.ImageList.Images.SetKeyName(20, "google_map_satellite.png")
        Me.ImageList.Images.SetKeyName(21, "slideshow.png")
        Me.ImageList.Images.SetKeyName(22, "geojson.png")
        Me.ImageList.Images.SetKeyName(23, "tab_add.png")
        Me.ImageList.Images.SetKeyName(24, "data_table.png")
        '
        'GeodesiXCommandBar
        '
        Me.GeodesiXCommandBar.CommandBarName = "GeodesiX"
        Me.GeodesiXCommandBar.CommandBarTag = "dd302251-df72-4713-90ce-4a78fdcc163f"
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbMap)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbBrowser)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbEarth)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbTSS)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbImportJSON)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbImportXML)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbExportJSON)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbExportHTML)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbExportKML)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbOptions)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbDefaults)
        Me.GeodesiXCommandBar.Controls.Add(Me.cbbIcons)
        Me.GeodesiXCommandBar.Controls.Add(Me.btnHelp)
        Me.GeodesiXCommandBar.Description = "GeodesiX functions"
        Me.GeodesiXCommandBar.SupportedApps = AddinExpress.MSO.ADXOfficeHostApp.ohaExcel
        Me.GeodesiXCommandBar.Temporary = True
        Me.GeodesiXCommandBar.UpdateCounter = 26
        '
        'cbbMap
        '
        Me.cbbMap.Caption = "Map"
        Me.cbbMap.ControlTag = "05f0f6b5-c632-45e6-a6dc-46b00b15032f"
        Me.cbbMap.DescriptionText = "Show / Hide GeodesiX map pane"
        Me.cbbMap.ImageList = Me.ImageList
        Me.cbbMap.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbMap.OlExplorerItemTypes = AddinExpress.MSO.ADXOlExplorerItemTypes.olUnknownItem
        Me.cbbMap.OlInspectorItemTypes = AddinExpress.MSO.ADXOlInspectorItemTypes.olUnknown
        Me.cbbMap.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbMap.Temporary = True
        Me.cbbMap.TooltipText = "Show / Hide GeodesiX map pane"
        Me.cbbMap.UpdateCounter = 51
        '
        'cbbBrowser
        '
        Me.cbbBrowser.Caption = "Browser"
        Me.cbbBrowser.ControlTag = "2e7240e4-4be4-4eb8-91c0-d93f653340da"
        Me.cbbBrowser.ImageList = Me.ImageList
        Me.cbbBrowser.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbBrowser.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbBrowser.Temporary = True
        Me.cbbBrowser.UpdateCounter = 9
        '
        'cbbEarth
        '
        Me.cbbEarth.Caption = "Earth"
        Me.cbbEarth.ControlTag = "50de20c3-c3fe-46cd-8d17-9b3ca727807f"
        Me.cbbEarth.DescriptionText = "Show in Google Earth"
        Me.cbbEarth.ImageList = Me.ImageList
        Me.cbbEarth.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbEarth.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbEarth.Temporary = True
        Me.cbbEarth.TooltipText = "Show in Google Earth"
        Me.cbbEarth.UpdateCounter = 17
        '
        'cbbTSS
        '
        Me.cbbTSS.Caption = "TSS"
        Me.cbbTSS.ControlTag = "1367aeaa-9834-45a9-b5dc-6d275c6f16fa"
        Me.cbbTSS.DescriptionText = "Travelling Salesman Solver"
        Me.cbbTSS.ImageList = Me.ImageList
        Me.cbbTSS.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbTSS.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbTSS.Temporary = True
        Me.cbbTSS.TooltipText = "Travelling Salesman Solver"
        Me.cbbTSS.UpdateCounter = 14
        '
        'cbbImportJSON
        '
        Me.cbbImportJSON.Caption = "Import JSON"
        Me.cbbImportJSON.ControlTag = "ca2b72b8-34db-4e4c-9109-127f01684299"
        Me.cbbImportJSON.ImageList = Me.ImageList
        Me.cbbImportJSON.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbImportJSON.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbImportJSON.Temporary = True
        Me.cbbImportJSON.UpdateCounter = 9
        '
        'cbbImportXML
        '
        Me.cbbImportXML.Caption = "Import XML"
        Me.cbbImportXML.ControlTag = "eb2d1c16-5241-4101-bf50-1084e6b9ad65"
        Me.cbbImportXML.ImageList = Me.ImageList
        Me.cbbImportXML.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbImportXML.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbImportXML.Temporary = True
        Me.cbbImportXML.UpdateCounter = 9
        '
        'cbbExportJSON
        '
        Me.cbbExportJSON.Caption = "Export JSON"
        Me.cbbExportJSON.ControlTag = "8d744235-350f-4432-b1c2-e6ad0515b9db"
        Me.cbbExportJSON.DescriptionText = "Export as a GeoJson file (.JSON)"
        Me.cbbExportJSON.ImageList = Me.ImageList
        Me.cbbExportJSON.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbExportJSON.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbExportJSON.Temporary = True
        Me.cbbExportJSON.TooltipText = "Export as a GeoJson file (.JSON)"
        Me.cbbExportJSON.UpdateCounter = 15
        '
        'cbbExportHTML
        '
        Me.cbbExportHTML.Caption = "Export HTML"
        Me.cbbExportHTML.ControlTag = "607526dd-0ced-46cc-8ed0-557c8f527f8c"
        Me.cbbExportHTML.DescriptionText = "Export to a stand-alone HTML file"
        Me.cbbExportHTML.ImageList = Me.ImageList
        Me.cbbExportHTML.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbExportHTML.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbExportHTML.Temporary = True
        Me.cbbExportHTML.TooltipText = "Export to a stand-alone HTML file"
        Me.cbbExportHTML.UpdateCounter = 20
        '
        'cbbExportKML
        '
        Me.cbbExportKML.Caption = "Export KML"
        Me.cbbExportKML.ControlTag = "dde6812a-64f5-4637-91b3-8965d4a2f718"
        Me.cbbExportKML.ImageList = Me.ImageList
        Me.cbbExportKML.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbExportKML.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbExportKML.Temporary = True
        Me.cbbExportKML.UpdateCounter = 9
        '
        'cbbOptions
        '
        Me.cbbOptions.Caption = "Options"
        Me.cbbOptions.ControlTag = "6d4ec4a1-4007-45bc-bac0-0551bc5002f8"
        Me.cbbOptions.DescriptionText = "Change GeodesiX options"
        Me.cbbOptions.ImageList = Me.ImageList
        Me.cbbOptions.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbOptions.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndWrapCaptionBelow
        Me.cbbOptions.Temporary = True
        Me.cbbOptions.TooltipText = "Change GeodesiX options"
        Me.cbbOptions.UpdateCounter = 31
        '
        'cbbDefaults
        '
        Me.cbbDefaults.Caption = "Defaults"
        Me.cbbDefaults.ControlTag = "e750e69b-86ca-46c6-b8f3-cbe9943dae7b"
        Me.cbbDefaults.ImageList = Me.ImageList
        Me.cbbDefaults.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbDefaults.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndCaptionBelow
        Me.cbbDefaults.Temporary = True
        Me.cbbDefaults.UpdateCounter = 6
        '
        'cbbIcons
        '
        Me.cbbIcons.Caption = "Icons"
        Me.cbbIcons.ControlTag = "2552e749-f38a-4c5c-8949-010ad547ca4c"
        Me.cbbIcons.ImageList = Me.ImageList
        Me.cbbIcons.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbIcons.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndCaptionBelow
        Me.cbbIcons.Temporary = True
        Me.cbbIcons.UpdateCounter = 5
        '
        'btnHelp
        '
        Me.btnHelp.Caption = "Help"
        Me.btnHelp.Controls.Add(Me.cbbHelpReadme)
        Me.btnHelp.Controls.Add(Me.cbbHelpVBA)
        Me.btnHelp.Controls.Add(Me.cbbHelpImpExp)
        Me.btnHelp.Controls.Add(Me.cbbHelpFunctions)
        Me.btnHelp.Controls.Add(Me.cbbHelpOnline)
        Me.btnHelp.ControlTag = "92e26450-4008-4222-95f8-754a0303f903"
        Me.btnHelp.Temporary = True
        Me.btnHelp.UpdateCounter = 5
        '
        'cbbHelpReadme
        '
        Me.cbbHelpReadme.Caption = "ReadMe"
        Me.cbbHelpReadme.ControlTag = "9bb72dac-abde-4bf8-b580-8c9038a30c6f"
        Me.cbbHelpReadme.ImageList = Me.ImageList
        Me.cbbHelpReadme.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbHelpReadme.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndCaption
        Me.cbbHelpReadme.Temporary = True
        Me.cbbHelpReadme.UpdateCounter = 11
        '
        'cbbHelpVBA
        '
        Me.cbbHelpVBA.Caption = "VBA"
        Me.cbbHelpVBA.ControlTag = "c8db74e3-2781-4d59-8d98-d045844ecb74"
        Me.cbbHelpVBA.ImageList = Me.ImageList
        Me.cbbHelpVBA.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbHelpVBA.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndCaption
        Me.cbbHelpVBA.Temporary = True
        Me.cbbHelpVBA.UpdateCounter = 11
        '
        'cbbHelpImpExp
        '
        Me.cbbHelpImpExp.Caption = "Import Export"
        Me.cbbHelpImpExp.ControlTag = "56a3dafe-f424-4b05-8493-cf7ea1310297"
        Me.cbbHelpImpExp.ImageList = Me.ImageList
        Me.cbbHelpImpExp.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbHelpImpExp.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndCaption
        Me.cbbHelpImpExp.Temporary = True
        Me.cbbHelpImpExp.UpdateCounter = 13
        '
        'cbbHelpFunctions
        '
        Me.cbbHelpFunctions.Caption = "Functions"
        Me.cbbHelpFunctions.ControlTag = "1e5c1992-2845-4f98-b74a-c31a21d2a5c2"
        Me.cbbHelpFunctions.ImageList = Me.ImageList
        Me.cbbHelpFunctions.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbHelpFunctions.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndCaption
        Me.cbbHelpFunctions.Temporary = True
        Me.cbbHelpFunctions.UpdateCounter = 9
        '
        'cbbHelpOnline
        '
        Me.cbbHelpOnline.Caption = "Online"
        Me.cbbHelpOnline.ControlTag = "e90ec05f-70c7-4e11-afc9-e7f7b80975ad"
        Me.cbbHelpOnline.ImageList = Me.ImageList
        Me.cbbHelpOnline.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.cbbHelpOnline.Style = AddinExpress.MSO.ADXMsoButtonStyle.adxMsoButtonIconAndCaption
        Me.cbbHelpOnline.Temporary = True
        Me.cbbHelpOnline.UpdateCounter = 10
        '
        'GeodesiXRibbonTab
        '
        Me.GeodesiXRibbonTab.Caption = "GeodesiX"
        Me.GeodesiXRibbonTab.Controls.Add(Me.RibbonGroupGeodesix)
        Me.GeodesiXRibbonTab.Controls.Add(Me.RibbonGroupInsert)
        Me.GeodesiXRibbonTab.Controls.Add(Me.RibbonGroupImport)
        Me.GeodesiXRibbonTab.Controls.Add(Me.RibbonGroupExport)
        Me.GeodesiXRibbonTab.Controls.Add(Me.RibbonGroupTools)
        Me.GeodesiXRibbonTab.Controls.Add(Me.RibbonDebugging)
        Me.GeodesiXRibbonTab.Id = "adxRibbonTaSettting_0168b6f423b74f96858de54f82c67546"
        Me.GeodesiXRibbonTab.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonGroupGeodesix
        '
        Me.RibbonGroupGeodesix.Caption = "View"
        Me.RibbonGroupGeodesix.Controls.Add(Me.RibbonViewMap)
        Me.RibbonGroupGeodesix.Controls.Add(Me.RibbonViewBrowser)
        Me.RibbonGroupGeodesix.Controls.Add(Me.RibbonViewEarth)
        Me.RibbonGroupGeodesix.Controls.Add(Me.RibbonLocate)
        Me.RibbonGroupGeodesix.Id = "adxRibbonGroup_8a5825f16c8441c085e9cfd9bb96b6f2"
        Me.RibbonGroupGeodesix.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonGroupGeodesix.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonViewMap
        '
        Me.RibbonViewMap.Caption = "Map"
        Me.RibbonViewMap.Description = "Map"
        Me.RibbonViewMap.Id = "adxRibbonButton_2d983daa1ce24cc6aaad9fd4c744593d"
        Me.RibbonViewMap.Image = 20
        Me.RibbonViewMap.ImageList = Me.ImageList
        Me.RibbonViewMap.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonViewMap.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonViewMap.ScreenTip = "Show/Hide the GeodesiX map pane"
        Me.RibbonViewMap.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonViewBrowser
        '
        Me.RibbonViewBrowser.Caption = "Browser"
        Me.RibbonViewBrowser.Id = "adxRibbonButton_bf6e85519c934951b42adf54486bf803"
        Me.RibbonViewBrowser.Image = 1
        Me.RibbonViewBrowser.ImageList = Me.ImageList
        Me.RibbonViewBrowser.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonViewBrowser.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonViewBrowser.ScreenTip = "Show the current map in the browser"
        Me.RibbonViewBrowser.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonViewEarth
        '
        Me.RibbonViewEarth.Caption = "Earth"
        Me.RibbonViewEarth.Id = "adxRibbonButton_a4f2048afea3491abcfe825a93e8d8c9"
        Me.RibbonViewEarth.Image = 2
        Me.RibbonViewEarth.ImageList = Me.ImageList
        Me.RibbonViewEarth.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonViewEarth.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonViewEarth.ScreenTip = "View the current sheet as a KML file in Google Earth"
        Me.RibbonViewEarth.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonLocate
        '
        Me.RibbonLocate.Caption = "Locate"
        Me.RibbonLocate.Id = "adxRibbonButton_7e8cdcfc2a9a403db772e5db62226ea2"
        Me.RibbonLocate.Image = 19
        Me.RibbonLocate.ImageList = Me.ImageList
        Me.RibbonLocate.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonLocate.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonLocate.ScreenTip = "Show the place referred to by the current cell on the map"
        Me.RibbonLocate.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonGroupInsert
        '
        Me.RibbonGroupInsert.Caption = "Insert"
        Me.RibbonGroupInsert.Controls.Add(Me.RibbonInsertFunction)
        Me.RibbonGroupInsert.Controls.Add(Me.RibbonInsertField)
        Me.RibbonGroupInsert.Controls.Add(Me.RibbonInsertIcon)
        Me.RibbonGroupInsert.Id = "adxRibbonGroup_b32c5e7044a341879b03ebb37246e2a9"
        Me.RibbonGroupInsert.Image = 3
        Me.RibbonGroupInsert.ImageList = Me.ImageList
        Me.RibbonGroupInsert.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonGroupInsert.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonInsertFunction
        '
        Me.RibbonInsertFunction.Caption = "Function"
        Me.RibbonInsertFunction.Id = "adxRibbonButton_00649b420c53480691374e4935d45f3b"
        Me.RibbonInsertFunction.Image = 18
        Me.RibbonInsertFunction.ImageList = Me.ImageList
        Me.RibbonInsertFunction.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonInsertFunction.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonInsertFunction.ScreenTip = "Insert  a Geodesix function in the current cell"
        Me.RibbonInsertFunction.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonInsertField
        '
        Me.RibbonInsertField.Caption = "Field"
        Me.RibbonInsertField.Id = "adxRibbonButton_25c6544ea152413a84ad3db927437041"
        Me.RibbonInsertField.Image = 23
        Me.RibbonInsertField.ImageList = Me.ImageList
        Me.RibbonInsertField.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonInsertField.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonInsertField.ScreenTip = "Insert  a Geodesix field in the current cell"
        Me.RibbonInsertField.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonInsertIcon
        '
        Me.RibbonInsertIcon.Caption = "Icon"
        Me.RibbonInsertIcon.Id = "adxRibbonButton_b93da868f9144049a96c1427b499b90c"
        Me.RibbonInsertIcon.Image = 17
        Me.RibbonInsertIcon.ImageList = Me.ImageList
        Me.RibbonInsertIcon.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonInsertIcon.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonInsertIcon.ScreenTip = "Insert  an icon URL in the current cell"
        Me.RibbonInsertIcon.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonGroupImport
        '
        Me.RibbonGroupImport.Caption = "Import"
        Me.RibbonGroupImport.Controls.Add(Me.RibbonImportStructured)
        Me.RibbonGroupImport.Id = "adxRibbonGroup_b685a384c9ee4d9ea77b85c345990def"
        Me.RibbonGroupImport.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonGroupImport.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonImportStructured
        '
        Me.RibbonImportStructured.Caption = "Structured"
        Me.RibbonImportStructured.Id = "adxRibbonButton_35130e352ada4a8e9fa6886afcfe2edc"
        Me.RibbonImportStructured.Image = 24
        Me.RibbonImportStructured.ImageList = Me.ImageList
        Me.RibbonImportStructured.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonImportStructured.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonImportStructured.ScreenTip = "Import a JSON / XML file"
        Me.RibbonImportStructured.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonGroupExport
        '
        Me.RibbonGroupExport.Caption = "Export"
        Me.RibbonGroupExport.Controls.Add(Me.RibbonExportJSON)
        Me.RibbonGroupExport.Controls.Add(Me.RibbonExportGeoJSON)
        Me.RibbonGroupExport.Controls.Add(Me.RibbonExportHTML)
        Me.RibbonGroupExport.Controls.Add(Me.RibbonExportKML)
        Me.RibbonGroupExport.Controls.Add(Me.RibbonExportTabbed)
        Me.RibbonGroupExport.Id = "adxRibbonGroup_c5c98ca800264872b93c1184d1c74bcb"
        Me.RibbonGroupExport.ImageList = Me.ImageList
        Me.RibbonGroupExport.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonGroupExport.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonExportGeoJSON
        '
        Me.RibbonExportGeoJSON.Caption = "GeoJSON"
        Me.RibbonExportGeoJSON.Id = "adxRibbonButton_72017342e08c412a871e63055083d8d3"
        Me.RibbonExportGeoJSON.Image = 22
        Me.RibbonExportGeoJSON.ImageList = Me.ImageList
        Me.RibbonExportGeoJSON.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonExportGeoJSON.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonExportGeoJSON.ScreenTip = "Export the current sheet as a Geo-JSON file"
        Me.RibbonExportGeoJSON.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonExportHTML
        '
        Me.RibbonExportHTML.Caption = "HTML"
        Me.RibbonExportHTML.Id = "adxRibbonButton_e7aa6589b74b4982a1f34b5e477366a4"
        Me.RibbonExportHTML.Image = 4
        Me.RibbonExportHTML.ImageList = Me.ImageList
        Me.RibbonExportHTML.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonExportHTML.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonExportHTML.ScreenTip = "Export the current sheet as an HTML file"
        Me.RibbonExportHTML.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonExportKML
        '
        Me.RibbonExportKML.Caption = "KML"
        Me.RibbonExportKML.Id = "adxRibbonButton_ccd5ab3ff2c34800933008b05abe0e7b"
        Me.RibbonExportKML.Image = 5
        Me.RibbonExportKML.ImageList = Me.ImageList
        Me.RibbonExportKML.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonExportKML.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonExportKML.ScreenTip = "Export the current sheet as a KML file"
        Me.RibbonExportKML.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonExportTabbed
        '
        Me.RibbonExportTabbed.Caption = "Tabbed"
        Me.RibbonExportTabbed.Id = "adxRibbonButton_997bbf1d54304eef9480d99d9b3c1170"
        Me.RibbonExportTabbed.Image = 16
        Me.RibbonExportTabbed.ImageList = Me.ImageList
        Me.RibbonExportTabbed.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonExportTabbed.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonExportTabbed.ScreenTip = "Export the current sheet as a Tab-delimited file (without adding quotes)"
        Me.RibbonExportTabbed.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonGroupTools
        '
        Me.RibbonGroupTools.Caption = "Tools"
        Me.RibbonGroupTools.Controls.Add(Me.RibbonViewTSS)
        Me.RibbonGroupTools.Controls.Add(Me.RibbonOptions)
        Me.RibbonGroupTools.Controls.Add(Me.RibbonDrawing)
        Me.RibbonGroupTools.Controls.Add(Me.RibbonSettings)
        Me.RibbonGroupTools.Controls.Add(Me.RibbonHelp)
        Me.RibbonGroupTools.Id = "adxRibbonGroup_0e98b99815e44fbd919c9aa497711b19"
        Me.RibbonGroupTools.Image = 15
        Me.RibbonGroupTools.ImageList = Me.ImageList
        Me.RibbonGroupTools.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonGroupTools.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonViewTSS
        '
        Me.RibbonViewTSS.Caption = "TSS"
        Me.RibbonViewTSS.Description = "Travelling Salesman Solver"
        Me.RibbonViewTSS.Id = "adxRibbonButton_3314487abd0e43f7a2692d81b1baef49"
        Me.RibbonViewTSS.Image = 13
        Me.RibbonViewTSS.ImageList = Me.ImageList
        Me.RibbonViewTSS.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonViewTSS.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonViewTSS.ScreenTip = "Travelling Salesman Solver"
        Me.RibbonViewTSS.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonOptions
        '
        Me.RibbonOptions.Caption = "Options"
        Me.RibbonOptions.Id = "adxRibbonButton_6c6bb763ed2644f7ac631cd396fd7e50"
        Me.RibbonOptions.Image = 9
        Me.RibbonOptions.ImageList = Me.ImageList
        Me.RibbonOptions.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonOptions.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonOptions.ScreenTip = "Edit the advanced Geodesix options"
        Me.RibbonOptions.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonDrawing
        '
        Me.RibbonDrawing.Caption = "Drawing"
        Me.RibbonDrawing.Id = "adxRibbonButton_dcc08bd8e1e34e9595474112cc0e35fd"
        Me.RibbonDrawing.Image = 15
        Me.RibbonDrawing.ImageList = Me.ImageList
        Me.RibbonDrawing.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonDrawing.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonDrawing.ScreenTip = "Edit the default drawing parameters"
        Me.RibbonDrawing.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonSettings
        '
        Me.RibbonSettings.Caption = "Settings"
        Me.RibbonSettings.Id = "adxRibbonButton_1042c8162cf845d5a79d42c65d93c00c"
        Me.RibbonSettings.Image = 14
        Me.RibbonSettings.ImageList = Me.ImageList
        Me.RibbonSettings.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonSettings.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonSettings.ScreenTip = "Edit the Geodesix settings"
        Me.RibbonSettings.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonHelp
        '
        Me.RibbonHelp.Caption = "Help"
        Me.RibbonHelp.Id = "adxRibbonButton_33956925606f4527942fd9ff35e367aa"
        Me.RibbonHelp.Image = 10
        Me.RibbonHelp.ImageList = Me.ImageList
        Me.RibbonHelp.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonHelp.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonHelp.ScreenTip = "Display help"
        Me.RibbonHelp.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'RibbonDebugging
        '
        Me.RibbonDebugging.Caption = "Debugging"
        Me.RibbonDebugging.Controls.Add(Me.RibbonDeveloperTools)
        Me.RibbonDebugging.Controls.Add(Me.RibbonShowSource)
        Me.RibbonDebugging.Controls.Add(Me.RibbonShowCache)
        Me.RibbonDebugging.Controls.Add(Me.RibbonPurgeCache)
        Me.RibbonDebugging.Id = "adxRibbonGroup_eb10bbe5e35b4b7aab5751c9aec18f3c"
        Me.RibbonDebugging.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonDebugging.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonDeveloperTools
        '
        Me.RibbonDeveloperTools.Caption = "Developer tools"
        Me.RibbonDeveloperTools.Id = "adxRibbonButton_14b53e92fc7d46f9a4b080bce99567c3"
        Me.RibbonDeveloperTools.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonDeveloperTools.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonShowSource
        '
        Me.RibbonShowSource.Caption = "Show source"
        Me.RibbonShowSource.Id = "adxRibbonButton_0008be3a36c24bbf993f40071dc490bd"
        Me.RibbonShowSource.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonShowSource.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonShowCache
        '
        Me.RibbonShowCache.Caption = "Show cache"
        Me.RibbonShowCache.Id = "adxRibbonButton_af747b2939c740b99b3dad93822b9869"
        Me.RibbonShowCache.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonShowCache.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'RibbonPurgeCache
        '
        Me.RibbonPurgeCache.Caption = "Purge cache"
        Me.RibbonPurgeCache.Id = "adxRibbonButton_10800bd86f004a5e923a3edd13dcba7e"
        Me.RibbonPurgeCache.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonPurgeCache.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        '
        'rbtnWeb
        '
        Me.rbtnWeb.Caption = "Web"
        Me.rbtnWeb.Description = "Export to a stand-alone HTML file"
        Me.rbtnWeb.Id = "adxRibbonButton_9e61c859e2794bccaca70da8fd873f8c"
        Me.rbtnWeb.ImageList = Me.ImageList
        Me.rbtnWeb.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.rbtnWeb.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.rbtnWeb.ScreenTip = "Export to a stand-alone HTML file"
        Me.rbtnWeb.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'TaskPanesManager
        '
        Me.TaskPanesManager.Items.Add(Me.TheTaskPane)
        Me.TaskPanesManager.SetOwner(Me)
        '
        'TheTaskPane
        '
        Me.TheTaskPane.AllowedDropPositions = CType((((AddinExpress.XL.ADXExcelAllowedDropPositions.Top Or AddinExpress.XL.ADXExcelAllowedDropPositions.Bottom) _
            Or AddinExpress.XL.ADXExcelAllowedDropPositions.Right) _
            Or AddinExpress.XL.ADXExcelAllowedDropPositions.Left), AddinExpress.XL.ADXExcelAllowedDropPositions)
        Me.TheTaskPane.Position = AddinExpress.XL.ADXExcelTaskPanePosition.Right
        Me.TheTaskPane.TaskPaneClassName = "Geodesix.MapTaskPane"
        '
        'RibbonExportJSON
        '
        Me.RibbonExportJSON.Caption = "JSON"
        Me.RibbonExportJSON.Id = "adxRibbonButton_b67639d733a44f50816bb45d65b6355c"
        Me.RibbonExportJSON.Image = 7
        Me.RibbonExportJSON.ImageList = Me.ImageList
        Me.RibbonExportJSON.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.RibbonExportJSON.Ribbons = AddinExpress.MSO.ADXRibbons.msrExcelWorkbook
        Me.RibbonExportJSON.ScreenTip = "Export the current sheet as a JSON file"
        Me.RibbonExportJSON.Size = AddinExpress.MSO.ADXRibbonXControlSize.Large
        '
        'GeodesiXEXL
        '
        Me.AddinName = "GeodesiX"
        Me.Description = "Geocoder and GCD for Excel"
        Me.SupportedApps = AddinExpress.MSO.ADXOfficeHostApp.ohaExcel

    End Sub
    Friend WithEvents GeodesiXCommandBar As AddinExpress.MSO.ADXCommandBar
    Friend WithEvents cbbMap As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents GeodesiXRibbonTab As AddinExpress.MSO.ADXRibbonTab
    Friend WithEvents RibbonGroupGeodesix As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents RibbonViewMap As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents cbbOptions As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbTSS As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents RibbonViewTSS As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents btnHelp As AddinExpress.MSO.ADXCommandBarPopup
    Friend WithEvents cbbHelpOnline As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbHelpReadme As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbHelpVBA As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbExportHTML As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbEarth As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbExportJSON As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents RibbonGroupExport As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents RibbonExportHTML As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonExportKML As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonExportGeoJSON As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonHelp As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents rbtnWeb As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonViewBrowser As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonViewEarth As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonGroupImport As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents RibbonImportStructured As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonGroupTools As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents RibbonOptions As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents cbbBrowser As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbImportJSON As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbImportXML As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbExportKML As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbHelpImpExp As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbHelpFunctions As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents RibbonDebugging As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents RibbonDeveloperTools As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonShowSource As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonSettings As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents cbbDefaults As AddinExpress.MSO.ADXCommandBarButton
    Friend WithEvents cbbIcons As AddinExpress.MSO.ADXCommandBarButton
    Public WithEvents adxExcelEvents As AddinExpress.MSO.ADXExcelAppEvents
    Friend WithEvents RibbonShowCache As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonPurgeCache As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonDrawing As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonExportTabbed As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonGroupInsert As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents RibbonInsertFunction As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonInsertIcon As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonLocate As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RibbonInsertField As AddinExpress.MSO.ADXRibbonButton
    Public WithEvents TheTaskPane As ADXExcelTaskPanesCollectionItem
    Public WithEvents TaskPanesManager As ADXExcelTaskPanesManager
    Friend WithEvents RibbonExportJSON As AddinExpress.MSO.ADXRibbonButton
End Class

