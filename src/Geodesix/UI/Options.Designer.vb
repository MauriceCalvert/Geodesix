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
Partial Class Options
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Options))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tbpKey = New System.Windows.Forms.TabPage()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.llDocumentation = New System.Windows.Forms.LinkLabel()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.llConsole = New System.Windows.Forms.LinkLabel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.btnValidate = New System.Windows.Forms.Button()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.txtKey = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.tbpGeocoder = New System.Windows.Forms.TabPage()
        Me.Map = New GeodesiX.Map()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtGeocoder = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.txtAddress = New System.Windows.Forms.RichTextBox()
        Me.txtLongitude = New System.Windows.Forms.TextBox()
        Me.txtLatitude = New System.Windows.Forms.TextBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtPlace = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbRegion = New System.Windows.Forms.ComboBox()
        Me.cmbLanguage = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbpAdvanced = New System.Windows.Forms.TabPage()
        Me.chkMinify = New System.Windows.Forms.CheckBox()
        Me.chkDebugging = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtPercentage = New System.Windows.Forms.TextBox()
        Me.txtMisses = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtHits = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnPurgeCache = New System.Windows.Forms.Button()
        Me.btnResetAll = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtProxy = New System.Windows.Forms.TextBox()
        Me.txtProxyPort = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbpAbout = New System.Windows.Forms.TabPage()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtInstallPath = New System.Windows.Forms.TextBox()
        Me.llEmail = New System.Windows.Forms.LinkLabel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtVersion = New System.Windows.Forms.RichTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tbpKey.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpGeocoder.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tbpAdvanced.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tbpAbout.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TabControl1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOK)
        Me.SplitContainer1.Size = New System.Drawing.Size(578, 362)
        Me.SplitContainer1.SplitterDistance = 332
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 22
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tbpKey)
        Me.TabControl1.Controls.Add(Me.tbpGeocoder)
        Me.TabControl1.Controls.Add(Me.tbpAdvanced)
        Me.TabControl1.Controls.Add(Me.tbpAbout)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.HotTrack = True
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(578, 332)
        Me.TabControl1.TabIndex = 0
        '
        'tbpKey
        '
        Me.tbpKey.Controls.Add(Me.PictureBox2)
        Me.tbpKey.Controls.Add(Me.Label4)
        Me.tbpKey.Controls.Add(Me.llDocumentation)
        Me.tbpKey.Controls.Add(Me.Label34)
        Me.tbpKey.Controls.Add(Me.Label3)
        Me.tbpKey.Controls.Add(Me.llConsole)
        Me.tbpKey.Controls.Add(Me.Label33)
        Me.tbpKey.Controls.Add(Me.Label37)
        Me.tbpKey.Controls.Add(Me.btnValidate)
        Me.tbpKey.Controls.Add(Me.txtMessage)
        Me.tbpKey.Controls.Add(Me.txtKey)
        Me.tbpKey.Controls.Add(Me.Label30)
        Me.tbpKey.Location = New System.Drawing.Point(4, 22)
        Me.tbpKey.Name = "tbpKey"
        Me.tbpKey.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpKey.Size = New System.Drawing.Size(570, 306)
        Me.tbpKey.TabIndex = 0
        Me.tbpKey.Text = "API Key"
        Me.tbpKey.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.GeodesiX.My.Resources.Resources.lightbulb
        Me.PictureBox2.Location = New System.Drawing.Point(20, 126)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 86
        Me.PictureBox2.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(42, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(466, 13)
        Me.Label4.TabIndex = 85
        Me.Label4.Text = "If you prefer to do this later, click Cancel. This prompt will re-appear the next" &
    " time that Excel starts."
        '
        'llDocumentation
        '
        Me.llDocumentation.AutoSize = True
        Me.llDocumentation.Location = New System.Drawing.Point(111, 253)
        Me.llDocumentation.Name = "llDocumentation"
        Me.llDocumentation.Size = New System.Drawing.Size(365, 13)
        Me.llDocumentation.TabIndex = 83
        Me.llDocumentation.TabStop = True
        Me.llDocumentation.Text = "https://developers.google.com/maps/documentation/javascript/get-api-key"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(17, 253)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(88, 13)
        Me.Label34.TabIndex = 84
        Me.Label34.Text = "Read about keys"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(328, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(193, 13)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "it takes a couple of minutes and it's free"
        '
        'llConsole
        '
        Me.llConsole.AutoSize = True
        Me.llConsole.Location = New System.Drawing.Point(136, 48)
        Me.llConsole.Name = "llConsole"
        Me.llConsole.Size = New System.Drawing.Size(186, 13)
        Me.llConsole.TabIndex = 79
        Me.llConsole.TabStop = True
        Me.llConsole.Text = "http://code.google.com/apis/console"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(17, 48)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(116, 13)
        Me.Label33.TabIndex = 81
        Me.Label33.Text = "Click here to get a key:"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(17, 16)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(387, 15)
        Me.Label37.TabIndex = 80
        Me.Label37.Text = "Geodesix requires a GoogleMaps API key to get geodesic information."
        '
        'btnValidate
        '
        Me.btnValidate.Location = New System.Drawing.Point(475, 72)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(61, 25)
        Me.btnValidate.TabIndex = 1
        Me.btnValidate.Text = "&Validate"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'txtMessage
        '
        Me.txtMessage.BackColor = System.Drawing.Color.White
        Me.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMessage.Location = New System.Drawing.Point(184, 101)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ReadOnly = True
        Me.txtMessage.Size = New System.Drawing.Size(285, 13)
        Me.txtMessage.TabIndex = 76
        '
        'txtKey
        '
        Me.txtKey.BackColor = System.Drawing.Color.White
        Me.txtKey.Location = New System.Drawing.Point(184, 75)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(285, 20)
        Me.txtKey.TabIndex = 0
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(17, 78)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(161, 13)
        Me.Label30.TabIndex = 62
        Me.Label30.Text = "Paste the key and click validate:"
        '
        'tbpGeocoder
        '
        Me.tbpGeocoder.Controls.Add(Me.Map)
        Me.tbpGeocoder.Controls.Add(Me.GroupBox2)
        Me.tbpGeocoder.Controls.Add(Me.cmbRegion)
        Me.tbpGeocoder.Controls.Add(Me.cmbLanguage)
        Me.tbpGeocoder.Controls.Add(Me.Label2)
        Me.tbpGeocoder.Controls.Add(Me.Label1)
        Me.tbpGeocoder.Cursor = System.Windows.Forms.Cursors.Default
        Me.tbpGeocoder.Location = New System.Drawing.Point(4, 22)
        Me.tbpGeocoder.Name = "tbpGeocoder"
        Me.tbpGeocoder.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpGeocoder.Size = New System.Drawing.Size(570, 306)
        Me.tbpGeocoder.TabIndex = 1
        Me.tbpGeocoder.Text = "Geocoder"
        Me.tbpGeocoder.UseVisualStyleBackColor = True
        '
        'Map
        '
        Me.Map.AutoSize = True
        Me.Map.ClickLocation = ""
        Me.Map.CoreWebView2 = Nothing
        Me.Map.Display = False
        Me.Map.Generator = Nothing
        Me.Map.Location = New System.Drawing.Point(294, 9)
        Me.Map.MapStyle = Nothing
        Me.Map.Mode = "Map"
        Me.Map.Name = "Map"
        Me.Map.Size = New System.Drawing.Size(262, 282)
        Me.Map.TabIndex = 28
        Me.Map.TaskPaneHeight = 0
        Me.Map.TaskPanePosition = AddinExpress.XL.ADXExcelTaskPanePosition.Unknown
        Me.Map.TaskPaneWidth = 0
        Me.Map.URL = ""
        Me.Map.Zoom = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtGeocoder)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.btnGo)
        Me.GroupBox2.Controls.Add(Me.txtAddress)
        Me.GroupBox2.Controls.Add(Me.txtLongitude)
        Me.GroupBox2.Controls.Add(Me.txtLatitude)
        Me.GroupBox2.Controls.Add(Me.txtStatus)
        Me.GroupBox2.Controls.Add(Me.txtPlace)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 67)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(279, 224)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Uncached Geocoder"
        '
        'txtGeocoder
        '
        Me.txtGeocoder.Enabled = False
        Me.txtGeocoder.Location = New System.Drawing.Point(73, 183)
        Me.txtGeocoder.Name = "txtGeocoder"
        Me.txtGeocoder.ReadOnly = True
        Me.txtGeocoder.Size = New System.Drawing.Size(153, 20)
        Me.txtGeocoder.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(13, 186)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(54, 13)
        Me.Label16.TabIndex = 11
        Me.Label16.Text = "Geocoder"
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(191, 24)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(35, 21)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = "Go"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'txtAddress
        '
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Enabled = False
        Me.txtAddress.Location = New System.Drawing.Point(73, 129)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.Size = New System.Drawing.Size(153, 48)
        Me.txtAddress.TabIndex = 5
        Me.txtAddress.Text = ""
        '
        'txtLongitude
        '
        Me.txtLongitude.Enabled = False
        Me.txtLongitude.Location = New System.Drawing.Point(73, 103)
        Me.txtLongitude.Name = "txtLongitude"
        Me.txtLongitude.ReadOnly = True
        Me.txtLongitude.Size = New System.Drawing.Size(153, 20)
        Me.txtLongitude.TabIndex = 4
        '
        'txtLatitude
        '
        Me.txtLatitude.Enabled = False
        Me.txtLatitude.Location = New System.Drawing.Point(73, 77)
        Me.txtLatitude.Name = "txtLatitude"
        Me.txtLatitude.ReadOnly = True
        Me.txtLatitude.Size = New System.Drawing.Size(153, 20)
        Me.txtLatitude.TabIndex = 3
        '
        'txtStatus
        '
        Me.txtStatus.Enabled = False
        Me.txtStatus.Location = New System.Drawing.Point(73, 51)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(153, 20)
        Me.txtStatus.TabIndex = 2
        '
        'txtPlace
        '
        Me.txtPlace.Location = New System.Drawing.Point(73, 25)
        Me.txtPlace.Name = "txtPlace"
        Me.txtPlace.Size = New System.Drawing.Size(112, 20)
        Me.txtPlace.TabIndex = 0
        Me.txtPlace.Text = "London"
        Me.ToolTips.SetToolTip(Me.txtPlace, "Enter the name of a place and click Go to display it on the map")
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(22, 131)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 13)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "Address"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(13, 106)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 13)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Longitude"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(22, 80)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Latitude"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(30, 54)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "status"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(33, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Place"
        '
        'cmbRegion
        '
        Me.cmbRegion.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRegion.FormattingEnabled = True
        Me.cmbRegion.Items.AddRange(New Object() {"", "AC - Ascension Island", "AD - Andorra", "AE - United Arab Emirates", "AF - Afghanistan", "AG - Antigua and Barbuda", "AI - Anguilla", "AL - Albania", "AM - Armenia", "AN - Netherlands Antilles (being phased out)", "AO - Angola", "AQ - Antarctica", "AR - Argentina", "AS - American Samoa", "AT - Austria", "AU - Australia", "AW - Aruba", "AX - Aland Islands", "AZ - Azerbaijan", "BA - Bosnia and Herzegovina", "BB - Barbados", "BD - Bangladesh", "BE - Belgium", "BF - Burkina Faso", "BG - Bulgaria", "BH - Bahrain", "BI - Burundi", "BJ - Benin", "BL - Saint Barthelemy", "BM - Bermuda", "BN - Brunei Darussalam", "BO - Bolivia", "BQ - Bonaire, Saint Eustatius and Saba", "BR - Brazil", "BS - Bahamas", "BT - Bhutan", "BV - Bouvet Island", "BW - Botswana", "BY - Belarus", "BZ - Belize", "CA - Canada", "CC - Cocos (Keeling) Islands", "CD - Congo, The Democratic Republic of the", "CF - Central African Republic", "CG - Congo", "CH - Switzerland", "CI - Cote d'Ivoire", "CK - Cook Islands", "CL - Chile", "CM - Cameroon", "CN - China", "CO - Colombia", "CR - Costa Rica", "CU - Cuba", "CV - Cape Verde", "CW - Curaçao", "CX - Christmas Island", "CY - Cyprus", "CZ - Czech Republic", "DE - Germany", "DJ - Djibouti", "DK - Denmark", "DM - Dominica", "DO - Dominican Republic", "DZ - Algeria", "EC - Ecuador", "EE - Estonia", "EG - Egypt", "EH - Western Sahara", "ER - Eritrea", "ES - Spain", "ET - Ethiopia", "EU - European Union", "FI - Finland", "FJ - Fiji", "FK - Falkland Islands (Malvinas)", "FM - Micronesia, Federated States of", "FO - Faroe Islands", "FR - Frange", "GA - Gabon", "GB - United Kingdom", "GD - Grenada", "GE - Georgia", "GF - French Guiana", "GG - Guernsey", "GH - Ghana", "GI - Gibraltar", "GL - Greenland", "GM - Gambia", "GN - Guinea", "GP - Guadeloupe", "GQ - Equatorial Guinea", "GR - Greece", "GS - South Georgia and the South Sandwich Islands", "GT - Guatemala", "GU - Guam", "GW - Guinea-Bissau", "GY - Guyana", "HK - Hong Kong", "HM - Heard Island and McDonald Islands", "HN - Honduras", "HR - Croatia", "HT - Haiti", "HU - Hungary", "ID - Indonesia", "IE - Ireland", "IL - Israel", "IM - Isle of Man", "IN - India", "IO - British Indian Ocean Territory", "IQ - Iraq", "IR - Iran, Islamic Republic of", "IS - Iceland", "IT - Italy", "JE - Jersey", "JM - Jamaica", "JO - Jordan", "JP - Japan", "KE - Kenya", "KG - Kyrgyzstan", "KH - Cambodia", "KI - Kiribati", "KM - Comoros", "KN - Saint Kitts and Nevis", "KP - Korea, Democratic People's Republic of", "KR - Korea, Republic of", "KW - Kuwait", "KY - Cayman Islands", "KZ - Kazakhstan", "LA - Lao People's Democratic Republic", "LB - Lebanon", "LC - Saint Lucia", "LI - Liechtenstein", "LK - Sri Lanka", "LR - Liberia", "LS - Lesotho", "LT - Lithuania", "LU - Luxembourg", "LV - Latvia", "LY - Libyan Arab Jamahiriya", "MA - Morocco", "MC - Monaco", "MD - Moldova, Republic of", "ME - Montenegro", "MF - Saint Martin (French part)", "MG - Madagascar", "MH - Marshall Islands", "MK - Macedonia, The Former Yugoslav Republic of", "ML - Mali", "MM - Myanmar", "MN - Mongolia", "MO - Macao", "MP - Northern Mariana Islands", "MQ - Martinique", "MR - Mauritania", "MS - Montserrat", "MT - Malta", "MU - Mauritius", "MV - Maldives", "MW - Malawi", "MX - Mexico", "MY - Malaysia", "MZ - Mozambique", "NA - Namibia", "NC - New Caledonia", "NE - Niger", "NF - Norfolk Island", "NG - Nigeria", "NI - Nicaragua", "NL - Netherlands", "NO - Norway", "NP - Nepal", "NR - Nauru", "NU - Niue", "NZ - New Zealand", "OM - Oman", "PA - Panama", "PE - Peru", "PF - French Polynesia", "PG - Papua New Guinea", "PH - Philippines", "PK - Pakistan", "PL - Poland", "PM - Saint Pierre and Miquelon", "PN - Pitcairn", "PR - Puerto Rico", "PS - Palestinian Territory, Occupied", "PT - Portugal", "PW - Palau", "PY - Paraguay", "QA - Qatar", "RE - Reunion", "RO - Romania", "RS - Serbia", "RU - Russian Federation", "RW - Rwanda", "SA - Saudi Arabia", "SB - Solomon Islands", "SC - Seychelles", "SD - Sudan", "SE - Sweden", "SG - Singapore", "SH - Saint Helena", "SI - Slovenia", "SJ - Svalbard and Jan Mayen", "SK - Slovakia", "SL - Sierra Leone", "SM - San Marino", "SN - Senegal", "SO - Somalia", "SR - Suriname", "ST - Sao Tome and Principe", "SU - Soviet Union (being phased out)", "SV - El Salvador", "SX - Sint Maarten (Dutch part)", "SY - Syrian Arab Republic", "SZ - Swaziland", "TC - Turks and Caicos Islands", "TD - Chad", "TF - French Southern Territories", "TG - Togo", "TH - Thailand", "TJ - Tajikistan", "TK - Tokelau", "TL - Timor-Leste", "TM - Turkmenistan", "TN - Tunisia", "TO - Tonga", "TP - Portuguese Timor (being phased out)", "TR - Turkey", "TT - Trinidad and Tobago", "TV - Tuvalu", "TW - Taiwan", "TZ - Tanzania, United Republic of", "UA - Ukraine", "UG - Uganda", "UK - United Kingdom", "UM - United States Minor Outlying Islands", "US - United States", "UY - Uruguay", "UZ - Uzbekistan", "VA - Holy See (Vatican City State)", "VC - Saint Vincent and the Grenadines", "VE - Venezuela, Bolivarian Republic of", "VG - Virgin Islands, British", "VI - Virgin Islands, U.S.", "VN - Viet Nam", "VU - Vanuatu", "WF - Wallis and Futuna", "WS - Samoa", "YE - Yemen", "YT - Mayotte", "ZA - South Africa", "ZM - Zambia", "ZW - Zimbabwe"})
        Me.cmbRegion.Location = New System.Drawing.Point(82, 36)
        Me.cmbRegion.Name = "cmbRegion"
        Me.cmbRegion.Size = New System.Drawing.Size(153, 22)
        Me.cmbRegion.TabIndex = 1
        '
        'cmbLanguage
        '
        Me.cmbLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLanguage.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLanguage.FormattingEnabled = True
        Me.cmbLanguage.Items.AddRange(New Object() {"", "ar - العربية", "bg - български език", "bn - বাংলা", "ca - Català", "cs - česky, čeština", "da - dansk", "de - Deutsch", "el - Ελληνικά", "en - English", "es - español, castellano", "eu - euskara, euskera", "fa - فارسی", "fi - suomi, suomen kieli", "fr - français, langue française", "gl - Galego", "gu - ગુજરાતી", "hi - हिन्दी, हिंदी", "hr - hrvatski", "hu - Magyar", "id - Bahasa Indonesia", "it - Italiano", "ja - 日本語 (にほんご／にっぽんご)", "kn - ಕನ್ನಡ", "ko - 한국어 (韓國語), 조선말 (朝鮮語)", "lt - lietuvių kalba", "lv - latviešu valoda", "ml - മലയാളം", "mr - मराठी", "nl - Nederlands, Vlaams", "no - Norsk", "pl - polski", "pt - Português", "ro - română", "ru - русский язык", "sk - slovenčina", "sl - slovenščina", "sr - српски језик", "sv - svenska", "ta - தமிழ்", "te - తెలుగు", "th - ไทย", "tl - Wikang Tagalog, ᜏᜒᜃᜅ᜔ ᜆᜄᜎᜓᜄ᜔", "tr - Türkçe", "uk - українська", "vi - Tiếng Việt"})
        Me.cmbLanguage.Location = New System.Drawing.Point(82, 9)
        Me.cmbLanguage.MaxDropDownItems = 25
        Me.cmbLanguage.Name = "cmbLanguage"
        Me.cmbLanguage.Size = New System.Drawing.Size(153, 22)
        Me.cmbLanguage.Sorted = True
        Me.cmbLanguage.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Region Bias"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Language"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbpAdvanced
        '
        Me.tbpAdvanced.Controls.Add(Me.chkMinify)
        Me.tbpAdvanced.Controls.Add(Me.chkDebugging)
        Me.tbpAdvanced.Controls.Add(Me.GroupBox5)
        Me.tbpAdvanced.Controls.Add(Me.btnResetAll)
        Me.tbpAdvanced.Controls.Add(Me.GroupBox1)
        Me.tbpAdvanced.Location = New System.Drawing.Point(4, 22)
        Me.tbpAdvanced.Name = "tbpAdvanced"
        Me.tbpAdvanced.Size = New System.Drawing.Size(570, 306)
        Me.tbpAdvanced.TabIndex = 3
        Me.tbpAdvanced.Text = "Advanced"
        Me.tbpAdvanced.UseVisualStyleBackColor = True
        '
        'chkMinify
        '
        Me.chkMinify.AutoSize = True
        Me.chkMinify.Location = New System.Drawing.Point(28, 254)
        Me.chkMinify.Margin = New System.Windows.Forms.Padding(2)
        Me.chkMinify.Name = "chkMinify"
        Me.chkMinify.Size = New System.Drawing.Size(105, 17)
        Me.chkMinify.TabIndex = 43
        Me.chkMinify.Text = "Compress output"
        Me.ToolTips.SetToolTip(Me.chkMinify, "Javascript and CSS will be minified, the HTML generated will be ~50% smaller")
        Me.chkMinify.UseVisualStyleBackColor = True
        '
        'chkDebugging
        '
        Me.chkDebugging.AutoSize = True
        Me.chkDebugging.Location = New System.Drawing.Point(28, 224)
        Me.chkDebugging.Margin = New System.Windows.Forms.Padding(2)
        Me.chkDebugging.Name = "chkDebugging"
        Me.chkDebugging.Size = New System.Drawing.Size(87, 17)
        Me.chkDebugging.TabIndex = 0
        Me.chkDebugging.Text = "Debug mode"
        Me.ToolTips.SetToolTip(Me.chkDebugging, "Enables debugging tools. Use at your own risk.")
        Me.chkDebugging.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtPercentage)
        Me.GroupBox5.Controls.Add(Me.txtMisses)
        Me.GroupBox5.Controls.Add(Me.Label38)
        Me.GroupBox5.Controls.Add(Me.txtHits)
        Me.GroupBox5.Controls.Add(Me.Label27)
        Me.GroupBox5.Controls.Add(Me.Label26)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Controls.Add(Me.btnPurgeCache)
        Me.GroupBox5.Location = New System.Drawing.Point(9, 87)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(547, 77)
        Me.GroupBox5.TabIndex = 42
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Cache"
        '
        'txtPercentage
        '
        Me.txtPercentage.BackColor = System.Drawing.SystemColors.Window
        Me.txtPercentage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPercentage.Location = New System.Drawing.Point(231, 19)
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.ReadOnly = True
        Me.txtPercentage.Size = New System.Drawing.Size(67, 13)
        Me.txtPercentage.TabIndex = 49
        Me.txtPercentage.Text = "0"
        Me.txtPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMisses
        '
        Me.txtMisses.BackColor = System.Drawing.SystemColors.Window
        Me.txtMisses.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMisses.Location = New System.Drawing.Point(158, 19)
        Me.txtMisses.Name = "txtMisses"
        Me.txtMisses.ReadOnly = True
        Me.txtMisses.Size = New System.Drawing.Size(67, 13)
        Me.txtMisses.TabIndex = 48
        Me.txtMisses.Text = "0"
        Me.txtMisses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(113, 19)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(39, 13)
        Me.Label38.TabIndex = 47
        Me.Label38.Text = "Misses"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHits
        '
        Me.txtHits.BackColor = System.Drawing.SystemColors.Window
        Me.txtHits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtHits.Location = New System.Drawing.Point(40, 19)
        Me.txtHits.Name = "txtHits"
        Me.txtHits.ReadOnly = True
        Me.txtHits.Size = New System.Drawing.Size(67, 13)
        Me.txtHits.TabIndex = 46
        Me.txtHits.Text = "0"
        Me.txtHits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(9, 19)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(25, 13)
        Me.Label27.TabIndex = 45
        Me.Label27.Text = "Hits"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(110, 55)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(425, 13)
        Me.Label26.TabIndex = 44
        Me.Label26.Text = "The data will be discarded when you save the workbook. To undo, close without sav" &
    "ing."
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(110, 38)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(386, 13)
        Me.Label22.TabIndex = 43
        Me.Label22.Text = "This will discard all the information saved about previous queries to GoogleMaps." &
    ""
        '
        'btnPurgeCache
        '
        Me.btnPurgeCache.Location = New System.Drawing.Point(9, 41)
        Me.btnPurgeCache.Name = "btnPurgeCache"
        Me.btnPurgeCache.Size = New System.Drawing.Size(98, 24)
        Me.btnPurgeCache.TabIndex = 42
        Me.btnPurgeCache.Text = "Purge Cache"
        Me.ToolTips.SetToolTip(Me.btnPurgeCache, "Purge the cache (makes the saved file smaller)")
        Me.btnPurgeCache.UseVisualStyleBackColor = True
        '
        'btnResetAll
        '
        Me.btnResetAll.Location = New System.Drawing.Point(17, 181)
        Me.btnResetAll.Name = "btnResetAll"
        Me.btnResetAll.Size = New System.Drawing.Size(98, 24)
        Me.btnResetAll.TabIndex = 1
        Me.btnResetAll.Text = "Reset all options"
        Me.ToolTips.SetToolTip(Me.btnResetAll, "Reset all options to their defaults")
        Me.btnResetAll.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtProxy)
        Me.GroupBox1.Controls.Add(Me.txtProxyPort)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(355, 75)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Proxy"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Server"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtProxy
        '
        Me.txtProxy.BackColor = System.Drawing.Color.White
        Me.txtProxy.Enabled = False
        Me.txtProxy.Location = New System.Drawing.Point(54, 17)
        Me.txtProxy.Name = "txtProxy"
        Me.txtProxy.ReadOnly = True
        Me.txtProxy.Size = New System.Drawing.Size(286, 20)
        Me.txtProxy.TabIndex = 0
        '
        'txtProxyPort
        '
        Me.txtProxyPort.BackColor = System.Drawing.Color.White
        Me.txtProxyPort.Enabled = False
        Me.txtProxyPort.Location = New System.Drawing.Point(54, 43)
        Me.txtProxyPort.Name = "txtProxyPort"
        Me.txtProxyPort.ReadOnly = True
        Me.txtProxyPort.Size = New System.Drawing.Size(74, 20)
        Me.txtProxyPort.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(26, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Port"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbpAbout
        '
        Me.tbpAbout.Controls.Add(Me.Label20)
        Me.tbpAbout.Controls.Add(Me.txtInstallPath)
        Me.tbpAbout.Controls.Add(Me.llEmail)
        Me.tbpAbout.Controls.Add(Me.Label23)
        Me.tbpAbout.Controls.Add(Me.Label24)
        Me.tbpAbout.Controls.Add(Me.Label25)
        Me.tbpAbout.Controls.Add(Me.PictureBox1)
        Me.tbpAbout.Controls.Add(Me.Label15)
        Me.tbpAbout.Controls.Add(Me.txtVersion)
        Me.tbpAbout.Controls.Add(Me.Label5)
        Me.tbpAbout.Location = New System.Drawing.Point(4, 22)
        Me.tbpAbout.Name = "tbpAbout"
        Me.tbpAbout.Size = New System.Drawing.Size(570, 306)
        Me.tbpAbout.TabIndex = 2
        Me.tbpAbout.Text = "About"
        Me.tbpAbout.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(12, 166)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(104, 13)
        Me.Label20.TabIndex = 71
        Me.Label20.Text = "Geodesix install path"
        '
        'txtInstallPath
        '
        Me.txtInstallPath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInstallPath.Location = New System.Drawing.Point(15, 182)
        Me.txtInstallPath.Multiline = True
        Me.txtInstallPath.Name = "txtInstallPath"
        Me.txtInstallPath.ReadOnly = True
        Me.txtInstallPath.Size = New System.Drawing.Size(539, 39)
        Me.txtInstallPath.TabIndex = 70
        '
        'llEmail
        '
        Me.llEmail.AutoSize = True
        Me.llEmail.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.llEmail.Location = New System.Drawing.Point(68, 131)
        Me.llEmail.Name = "llEmail"
        Me.llEmail.Size = New System.Drawing.Size(107, 13)
        Me.llEmail.TabIndex = 63
        Me.llEmail.TabStop = True
        Me.llEmail.Text = "geodesix@calvert.ch"
        Me.llEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(30, 131)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(32, 13)
        Me.Label23.TabIndex = 62
        Me.Label23.Text = "Email"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(68, 107)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(81, 13)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Maurice Calvert"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(25, 107)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(38, 13)
        Me.Label25.TabIndex = 60
        Me.Label25.Text = "Author"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(183, 96)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(60, 60)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 64
        Me.PictureBox1.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(12, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(53, 13)
        Me.Label15.TabIndex = 23
        Me.Label15.Text = "GeodesiX"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVersion
        '
        Me.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVersion.Enabled = False
        Me.txtVersion.Location = New System.Drawing.Point(71, 13)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.ReadOnly = True
        Me.txtVersion.Size = New System.Drawing.Size(483, 77)
        Me.txtVersion.TabIndex = 22
        Me.txtVersion.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Version"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(498, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 25)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(4, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(61, 25)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Options
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(578, 362)
        Me.Controls.Add(Me.SplitContainer1)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Options"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GeodesiX Options"
        Me.TopMost = True
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tbpKey.ResumeLayout(False)
        Me.tbpKey.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpGeocoder.ResumeLayout(False)
        Me.tbpGeocoder.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.tbpAdvanced.ResumeLayout(False)
        Me.tbpAdvanced.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tbpAbout.ResumeLayout(False)
        Me.tbpAbout.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolTips As System.Windows.Forms.ToolTip
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tbpKey As System.Windows.Forms.TabPage
    Friend WithEvents txtKey As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents tbpGeocoder As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtGeocoder As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents txtAddress As System.Windows.Forms.RichTextBox
    Friend WithEvents txtLongitude As System.Windows.Forms.TextBox
    Friend WithEvents txtLatitude As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPlace As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbRegion As System.Windows.Forms.ComboBox
    Friend WithEvents cmbLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbpAdvanced As System.Windows.Forms.TabPage
    Friend WithEvents btnResetAll As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtProxy As System.Windows.Forms.TextBox
    Friend WithEvents txtProxyPort As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbpAbout As System.Windows.Forms.TabPage
    Friend WithEvents llEmail As System.Windows.Forms.LinkLabel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtVersion As System.Windows.Forms.RichTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtInstallPath As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPercentage As System.Windows.Forms.TextBox
    Friend WithEvents txtMisses As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtHits As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnPurgeCache As System.Windows.Forms.Button
    Friend WithEvents chkDebugging As System.Windows.Forms.CheckBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents chkMinify As System.Windows.Forms.CheckBox
    Friend WithEvents Map As Map
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnValidate As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents llConsole As System.Windows.Forms.LinkLabel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents llDocumentation As System.Windows.Forms.LinkLabel
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
End Class
