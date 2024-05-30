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

Partial Public Class Map

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents MapToolstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnCopy As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefreshMapSheet As System.Windows.Forms.ToolStripButton
    Friend WithEvents sep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblMapSheet As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbSheets As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lblFind As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtFind As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnFind As System.Windows.Forms.ToolStripButton
    Friend WithEvents sep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnZoomIn As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnZoomOut As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnMapType As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents btnMinimal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnHybrid As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRoadmap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSatellite As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnTerrain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnCloseMap As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnQuadrants As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblZoom As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtZoom As System.Windows.Forms.ToolStripLabel

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Map))
        Me.MapToolstrip = New System.Windows.Forms.ToolStrip()
        Me.btnMapType = New System.Windows.Forms.ToolStripDropDownButton()
        Me.btnHybrid = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnMinimal = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnRoadmap = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSatellite = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnTerrain = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnCopy = New System.Windows.Forms.ToolStripButton()
        Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
        Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
        Me.sep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblFind = New System.Windows.Forms.ToolStripLabel()
        Me.txtFind = New System.Windows.Forms.ToolStripTextBox()
        Me.btnFind = New System.Windows.Forms.ToolStripButton()
        Me.sep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblMapSheet = New System.Windows.Forms.ToolStripLabel()
        Me.cmbSheets = New System.Windows.Forms.ToolStripComboBox()
        Me.btnCloseMap = New System.Windows.Forms.ToolStripButton()
        Me.btnRefreshMapSheet = New System.Windows.Forms.ToolStripButton()
        Me.btnShowBrowserBar = New System.Windows.Forms.ToolStripButton()
        Me.btnQuadrants = New System.Windows.Forms.ToolStripButton()
        Me.txtZoom = New System.Windows.Forms.ToolStripLabel()
        Me.lblZoom = New System.Windows.Forms.ToolStripLabel()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.AddressToolStrip = New System.Windows.Forms.ToolStrip()
        Me.btnBack = New System.Windows.Forms.ToolStripButton()
        Me.btnForward = New System.Windows.Forms.ToolStripButton()
        Me.btnReload = New System.Windows.Forms.ToolStripButton()
        Me.btnStop = New System.Windows.Forms.ToolStripButton()
        Me.txtAddress = New GeodesiX.ToolStripSpringTextBox()
        Me.btnCloseAddressBar = New System.Windows.Forms.ToolStripButton()
        Me.btnViewSource = New System.Windows.Forms.ToolStripButton()
        Me.btnCloseBrowserBar = New System.Windows.Forms.ToolStripButton()
        Me.WebView = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Progress = New System.Windows.Forms.ToolStripProgressBar()
        Me.StatusText = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MapToolstrip.SuspendLayout()
        Me.AddressToolStrip.SuspendLayout()
        CType(Me.WebView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MapToolstrip
        '
        Me.MapToolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.MapToolstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnMapType, Me.btnCopy, Me.btnZoomIn, Me.btnZoomOut, Me.sep1, Me.lblFind, Me.txtFind, Me.btnFind, Me.sep2, Me.lblMapSheet, Me.cmbSheets, Me.btnCloseMap, Me.btnRefreshMapSheet, Me.btnShowBrowserBar, Me.btnQuadrants, Me.txtZoom, Me.lblZoom})
        Me.MapToolstrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.MapToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.MapToolstrip.Name = "MapToolstrip"
        Me.MapToolstrip.Size = New System.Drawing.Size(789, 25)
        Me.MapToolstrip.TabIndex = 1
        Me.MapToolstrip.Text = "ToolStrip1"
        '
        'btnMapType
        '
        Me.btnMapType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnMapType.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnHybrid, Me.btnMinimal, Me.btnRoadmap, Me.btnSatellite, Me.btnTerrain})
        Me.btnMapType.Image = Global.GeodesiX.My.Resources.Resources.world16
        Me.btnMapType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnMapType.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnMapType.Name = "btnMapType"
        Me.btnMapType.Size = New System.Drawing.Size(29, 22)
        Me.btnMapType.ToolTipText = "Change map style"
        '
        'btnHybrid
        '
        Me.btnHybrid.Name = "btnHybrid"
        Me.btnHybrid.Size = New System.Drawing.Size(122, 22)
        Me.btnHybrid.Text = "hybrid"
        '
        'btnMinimal
        '
        Me.btnMinimal.Name = "btnMinimal"
        Me.btnMinimal.Size = New System.Drawing.Size(122, 22)
        Me.btnMinimal.Text = "minimal"
        '
        'btnRoadmap
        '
        Me.btnRoadmap.Name = "btnRoadmap"
        Me.btnRoadmap.Size = New System.Drawing.Size(122, 22)
        Me.btnRoadmap.Text = "roadmap"
        '
        'btnSatellite
        '
        Me.btnSatellite.Name = "btnSatellite"
        Me.btnSatellite.Size = New System.Drawing.Size(122, 22)
        Me.btnSatellite.Text = "satellite"
        '
        'btnTerrain
        '
        Me.btnTerrain.Name = "btnTerrain"
        Me.btnTerrain.Size = New System.Drawing.Size(122, 22)
        Me.btnTerrain.Text = "terrain"
        '
        'btnCopy
        '
        Me.btnCopy.AutoSize = False
        Me.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCopy.Image = CType(resources.GetObject("btnCopy.Image"), System.Drawing.Image)
        Me.btnCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnCopy.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.btnCopy.Size = New System.Drawing.Size(23, 22)
        Me.btnCopy.ToolTipText = "Copy map image to the clipboard. Use Control-C to avoid having this tooltip in th" &
    "e picture"
        '
        'btnZoomIn
        '
        Me.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnZoomIn.Image = CType(resources.GetObject("btnZoomIn.Image"), System.Drawing.Image)
        Me.btnZoomIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnZoomIn.Name = "btnZoomIn"
        Me.btnZoomIn.Size = New System.Drawing.Size(23, 22)
        Me.btnZoomIn.ToolTipText = "Zoom in"
        '
        'btnZoomOut
        '
        Me.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnZoomOut.Image = CType(resources.GetObject("btnZoomOut.Image"), System.Drawing.Image)
        Me.btnZoomOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnZoomOut.Name = "btnZoomOut"
        Me.btnZoomOut.Size = New System.Drawing.Size(23, 22)
        Me.btnZoomOut.ToolTipText = "Zoom out"
        '
        'sep1
        '
        Me.sep1.AutoSize = False
        Me.sep1.Name = "sep1"
        Me.sep1.Size = New System.Drawing.Size(6, 25)
        '
        'lblFind
        '
        Me.lblFind.AutoSize = False
        Me.lblFind.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFind.Name = "lblFind"
        Me.lblFind.Size = New System.Drawing.Size(30, 22)
        Me.lblFind.Text = "Find"
        '
        'txtFind
        '
        Me.txtFind.AutoSize = False
        Me.txtFind.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(120, 25)
        Me.txtFind.ToolTipText = "Type the name of a place to find"
        '
        'btnFind
        '
        Me.btnFind.AutoSize = False
        Me.btnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnFind.Image = CType(resources.GetObject("btnFind.Image"), System.Drawing.Image)
        Me.btnFind.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnFind.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(23, 22)
        Me.btnFind.ToolTipText = "Find the place"
        '
        'sep2
        '
        Me.sep2.Name = "sep2"
        Me.sep2.Size = New System.Drawing.Size(6, 25)
        '
        'lblMapSheet
        '
        Me.lblMapSheet.AutoSize = False
        Me.lblMapSheet.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblMapSheet.Name = "lblMapSheet"
        Me.lblMapSheet.Size = New System.Drawing.Size(62, 22)
        Me.lblMapSheet.Text = "Map sheet"
        '
        'cmbSheets
        '
        Me.cmbSheets.AutoSize = False
        Me.cmbSheets.Name = "cmbSheets"
        Me.cmbSheets.Size = New System.Drawing.Size(90, 23)
        Me.cmbSheets.Sorted = True
        Me.cmbSheets.ToolTipText = "Select a worksheet to display on the map"
        '
        'btnCloseMap
        '
        Me.btnCloseMap.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnCloseMap.AutoSize = False
        Me.btnCloseMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCloseMap.Image = CType(resources.GetObject("btnCloseMap.Image"), System.Drawing.Image)
        Me.btnCloseMap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnCloseMap.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnCloseMap.Name = "btnCloseMap"
        Me.btnCloseMap.Size = New System.Drawing.Size(23, 22)
        Me.btnCloseMap.Text = "ToolStripButton2"
        Me.btnCloseMap.ToolTipText = "Close this map window"
        '
        'btnRefreshMapSheet
        '
        Me.btnRefreshMapSheet.AutoSize = False
        Me.btnRefreshMapSheet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRefreshMapSheet.Image = CType(resources.GetObject("btnRefreshMapSheet.Image"), System.Drawing.Image)
        Me.btnRefreshMapSheet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnRefreshMapSheet.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnRefreshMapSheet.Name = "btnRefreshMapSheet"
        Me.btnRefreshMapSheet.Size = New System.Drawing.Size(23, 22)
        Me.btnRefreshMapSheet.ToolTipText = "Refresh the map"
        '
        'btnShowBrowserBar
        '
        Me.btnShowBrowserBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnShowBrowserBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnShowBrowserBar.Image = CType(resources.GetObject("btnShowBrowserBar.Image"), System.Drawing.Image)
        Me.btnShowBrowserBar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnShowBrowserBar.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnShowBrowserBar.Name = "btnShowBrowserBar"
        Me.btnShowBrowserBar.Size = New System.Drawing.Size(23, 22)
        Me.btnShowBrowserBar.ToolTipText = "Show the URL bar"
        '
        'btnQuadrants
        '
        Me.btnQuadrants.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnQuadrants.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnQuadrants.Image = CType(resources.GetObject("btnQuadrants.Image"), System.Drawing.Image)
        Me.btnQuadrants.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnQuadrants.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnQuadrants.Name = "btnQuadrants"
        Me.btnQuadrants.Size = New System.Drawing.Size(23, 22)
        Me.btnQuadrants.Text = "Quadrants"
        Me.btnQuadrants.ToolTipText = "Change the map pane position (left, right, top, bottom)"
        '
        'txtZoom
        '
        Me.txtZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.txtZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.txtZoom.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.txtZoom.Name = "txtZoom"
        Me.txtZoom.Size = New System.Drawing.Size(13, 22)
        Me.txtZoom.Text = "0"
        Me.txtZoom.ToolTipText = "Current zoom"
        '
        'lblZoom
        '
        Me.lblZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblZoom.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblZoom.Name = "lblZoom"
        Me.lblZoom.Size = New System.Drawing.Size(39, 22)
        Me.lblZoom.Text = "Zoom"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "copy.png")
        Me.ImageList1.Images.SetKeyName(1, "refresh.png")
        '
        'AddressToolStrip
        '
        Me.AddressToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.AddressToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.AddressToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnBack, Me.btnForward, Me.btnReload, Me.btnStop, Me.txtAddress, Me.btnCloseAddressBar, Me.btnViewSource, Me.btnCloseBrowserBar})
        Me.AddressToolStrip.Location = New System.Drawing.Point(0, 25)
        Me.AddressToolStrip.Name = "AddressToolStrip"
        Me.AddressToolStrip.Size = New System.Drawing.Size(789, 27)
        Me.AddressToolStrip.Stretch = True
        Me.AddressToolStrip.TabIndex = 7
        Me.AddressToolStrip.Text = "AddressToolStrip"
        '
        'btnBack
        '
        Me.btnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnBack.Enabled = False
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(23, 24)
        Me.btnBack.Text = "Back"
        Me.btnBack.ToolTipText = "Go back"
        '
        'btnForward
        '
        Me.btnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnForward.Enabled = False
        Me.btnForward.Image = CType(resources.GetObject("btnForward.Image"), System.Drawing.Image)
        Me.btnForward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnForward.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnForward.Name = "btnForward"
        Me.btnForward.Size = New System.Drawing.Size(23, 24)
        Me.btnForward.Text = "Forward"
        Me.btnForward.ToolTipText = "Go forward"
        '
        'btnReload
        '
        Me.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnReload.Enabled = False
        Me.btnReload.Image = CType(resources.GetObject("btnReload.Image"), System.Drawing.Image)
        Me.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(24, 24)
        Me.btnReload.Text = "Reload"
        Me.btnReload.ToolTipText = "Reload this page"
        '
        'btnStop
        '
        Me.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnStop.Image = CType(resources.GetObject("btnStop.Image"), System.Drawing.Image)
        Me.btnStop.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(24, 24)
        '
        'txtAddress
        '
        Me.txtAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl
        Me.txtAddress.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(591, 27)
        '
        'btnCloseAddressBar
        '
        Me.btnCloseAddressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnCloseAddressBar.AutoSize = False
        Me.btnCloseAddressBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCloseAddressBar.Image = CType(resources.GetObject("btnCloseAddressBar.Image"), System.Drawing.Image)
        Me.btnCloseAddressBar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnCloseAddressBar.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnCloseAddressBar.Name = "btnCloseAddressBar"
        Me.btnCloseAddressBar.Size = New System.Drawing.Size(23, 22)
        Me.btnCloseAddressBar.Text = "ToolStripButton2"
        Me.btnCloseAddressBar.ToolTipText = "Close map window"
        '
        'btnViewSource
        '
        Me.btnViewSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnViewSource.Image = Global.GeodesiX.My.Resources.Resources.text_document
        Me.btnViewSource.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnViewSource.Name = "btnViewSource"
        Me.btnViewSource.Size = New System.Drawing.Size(24, 24)
        Me.btnViewSource.Text = "ToolStripButton1"
        '
        'btnCloseBrowserBar
        '
        Me.btnCloseBrowserBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnCloseBrowserBar.AutoSize = False
        Me.btnCloseBrowserBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCloseBrowserBar.Image = CType(resources.GetObject("btnCloseBrowserBar.Image"), System.Drawing.Image)
        Me.btnCloseBrowserBar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnCloseBrowserBar.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnCloseBrowserBar.Name = "btnCloseBrowserBar"
        Me.btnCloseBrowserBar.Size = New System.Drawing.Size(23, 22)
        Me.btnCloseBrowserBar.ToolTipText = "Show the Map bar"
        '
        'WebView
        '
        Me.WebView.AllowExternalDrop = True
        Me.WebView.CreationProperties = Nothing
        Me.WebView.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebView.Location = New System.Drawing.Point(0, 52)
        Me.WebView.Name = "WebView"
        Me.WebView.Size = New System.Drawing.Size(789, 441)
        Me.WebView.TabIndex = 8
        Me.WebView.ZoomFactor = 1.0R
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Progress, Me.StatusText})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 471)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(789, 22)
        Me.StatusStrip1.TabIndex = 9
        Me.StatusStrip1.Text = "StatusStrip1"
        Me.StatusStrip1.Visible = False
        '
        'Progress
        '
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(100, 16)
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'StatusText
        '
        Me.StatusText.Name = "StatusText"
        Me.StatusText.Size = New System.Drawing.Size(641, 17)
        Me.StatusText.Spring = True
        Me.StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Map
        '
        Me.AutoSize = True
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.WebView)
        Me.Controls.Add(Me.AddressToolStrip)
        Me.Controls.Add(Me.MapToolstrip)
        Me.Name = "Map"
        Me.Size = New System.Drawing.Size(789, 493)
        Me.MapToolstrip.ResumeLayout(False)
        Me.MapToolstrip.PerformLayout()
        Me.AddressToolStrip.ResumeLayout(False)
        Me.AddressToolStrip.PerformLayout()
        CType(Me.WebView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AddressToolStrip As ToolStrip
    Friend WithEvents btnBack As ToolStripButton
    Friend WithEvents btnForward As ToolStripButton
    Friend WithEvents btnReload As ToolStripButton
    Friend WithEvents txtAddress As ToolStripSpringTextBox
    Private WithEvents btnCloseAddressBar As ToolStripButton
    Friend WithEvents btnViewSource As ToolStripButton
    Friend WithEvents btnShowBrowserBar As ToolStripButton
    Friend WithEvents WebView As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents btnStop As ToolStripButton
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents btnCloseBrowserBar As ToolStripButton
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents Progress As ToolStripProgressBar
    Friend WithEvents StatusText As ToolStripStatusLabel
End Class