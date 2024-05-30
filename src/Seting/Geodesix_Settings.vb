Imports System.Collections.Generic
Imports System.Linq
Imports Utilities
Public Class GeodesixSettings
	Inherits SettingsBase

	Public Sub New()
		MyBase.New("Geodesix")
	End Sub

	
Public Setting_APIKey As Setting
Public Property APIKey as String
    Get
        Return Setting_APIKey.Value
    End Get
    Set(value As String)
        Setting_APIKey.Value = value
    End Set
End Property
	
Public Setting_CultureCheck As Setting
Public Property CultureCheck as String
    Get
        Return Setting_CultureCheck.Value
    End Get
    Set(value As String)
        Setting_CultureCheck.Value = value
    End Set
End Property
	
Public Setting_Debugging As Setting
Public Property Debugging as String
    Get
        Return Setting_Debugging.Value
    End Get
    Set(value As String)
        Setting_Debugging.Value = value
    End Set
End Property
	
Public Setting_DirectionsURL As Setting
Public Property DirectionsURL as String
    Get
        Return Setting_DirectionsURL.Value
    End Get
    Set(value As String)
        Setting_DirectionsURL.Value = value
    End Set
End Property
	
Public Setting_ExportFile As Setting
Public Property ExportFile as String
    Get
        Return Setting_ExportFile.Value
    End Get
    Set(value As String)
        Setting_ExportFile.Value = value
    End Set
End Property
	
Public Setting_Frames As Setting
Public Property Frames as String
    Get
        Return Setting_Frames.Value
    End Get
    Set(value As String)
        Setting_Frames.Value = value
    End Set
End Property
	
Public Setting_ForceUSCulture As Setting
Public Property ForceUSCulture as String
    Get
        Return Setting_ForceUSCulture.Value
    End Get
    Set(value As String)
        Setting_ForceUSCulture.Value = value
    End Set
End Property
	
Public Setting_GeocodeURL As Setting
Public Property GeocodeURL as String
    Get
        Return Setting_GeocodeURL.Value
    End Get
    Set(value As String)
        Setting_GeocodeURL.Value = value
    End Set
End Property
	
Public Setting_ImportFile As Setting
Public Property ImportFile as String
    Get
        Return Setting_ImportFile.Value
    End Get
    Set(value As String)
        Setting_ImportFile.Value = value
    End Set
End Property
	
Public Setting_ImportFolder As Setting
Public Property ImportFolder as String
    Get
        Return Setting_ImportFolder.Value
    End Get
    Set(value As String)
        Setting_ImportFolder.Value = value
    End Set
End Property
	
Public Setting_Installed As Setting
Public Property Installed as String
    Get
        Return Setting_Installed.Value
    End Get
    Set(value As String)
        Setting_Installed.Value = value
    End Set
End Property
	
Public Setting_Language As Setting
Public Property Language as String
    Get
        Return Setting_Language.Value
    End Get
    Set(value As String)
        Setting_Language.Value = value
    End Set
End Property
	
Public Setting_LargeIcons As Setting
Public Property LargeIcons as String
    Get
        Return Setting_LargeIcons.Value
    End Get
    Set(value As String)
        Setting_LargeIcons.Value = value
    End Set
End Property
	
Public Setting_Libraries As Setting
Public Property Libraries as String
    Get
        Return Setting_Libraries.Value
    End Get
    Set(value As String)
        Setting_Libraries.Value = value
    End Set
End Property
	
Public Setting_LogLevel As Setting
Public Property LogLevel as String
    Get
        Return Setting_LogLevel.Value
    End Get
    Set(value As String)
        Setting_LogLevel.Value = value
    End Set
End Property
	
Public Setting_Minify As Setting
Public Property Minify as String
    Get
        Return Setting_Minify.Value
    End Get
    Set(value As String)
        Setting_Minify.Value = value
    End Set
End Property
	
Public Setting_MapStyle As Setting
Public Property MapStyle as String
    Get
        Return Setting_MapStyle.Value
    End Get
    Set(value As String)
        Setting_MapStyle.Value = value
    End Set
End Property
	
Public Setting_RealTimeSet As Setting
Public Property RealTimeSet as String
    Get
        Return Setting_RealTimeSet.Value
    End Get
    Set(value As String)
        Setting_RealTimeSet.Value = value
    End Set
End Property
	
Public Setting_RegexTimeout As Setting
Public Property RegexTimeout as String
    Get
        Return Setting_RegexTimeout.Value
    End Get
    Set(value As String)
        Setting_RegexTimeout.Value = value
    End Set
End Property
	
Public Setting_Region As Setting
Public Property Region as String
    Get
        Return Setting_Region.Value
    End Get
    Set(value As String)
        Setting_Region.Value = value
    End Set
End Property
	
Public Setting_StartLat As Setting
Public Property StartLat as String
    Get
        Return Setting_StartLat.Value
    End Get
    Set(value As String)
        Setting_StartLat.Value = value
    End Set
End Property
	
Public Setting_StartLong As Setting
Public Property StartLong as String
    Get
        Return Setting_StartLong.Value
    End Get
    Set(value As String)
        Setting_StartLong.Value = value
    End Set
End Property
	
Public Setting_StartZoom As Setting
Public Property StartZoom as String
    Get
        Return Setting_StartZoom.Value
    End Get
    Set(value As String)
        Setting_StartZoom.Value = value
    End Set
End Property
	
Public Setting_TemplateName As Setting
Public Property TemplateName as String
    Get
        Return Setting_TemplateName.Value
    End Get
    Set(value As String)
        Setting_TemplateName.Value = value
    End Set
End Property
	
Public Setting_WebTimeout As Setting
Public Property WebTimeout as String
    Get
        Return Setting_WebTimeout.Value
    End Get
    Set(value As String)
        Setting_WebTimeout.Value = value
    End Set
End Property
	
Public Setting_Welcome As Setting
Public Property Welcome as String
    Get
        Return Setting_Welcome.Value
    End Get
    Set(value As String)
        Setting_Welcome.Value = value
    End Set
End Property
Overrides Sub MakeItems()
	_Items = New Dictionary(Of String, Setting)(StringComparer.OrdinalIgnoreCase)
	Setting_APIKey = New Setting("APIKey", GetType(string), "", "Google Maps API key", True, Nothing)
	Items.Add("APIKey", Setting_APIKey)
	AddHandler Setting_APIKey.Changed, AddressOf Setting_Changed
	Setting_CultureCheck = New Setting("CultureCheck", GetType(string), "Never", "Check Windows and Office culture compatibility", False, {"Never","Language","Culture"})
	Items.Add("CultureCheck", Setting_CultureCheck)
	AddHandler Setting_CultureCheck.Changed, AddressOf Setting_Changed
	Setting_Debugging = New Setting("Debugging", GetType(boolean), "False", "Yup, debugging", True, Nothing)
	Items.Add("Debugging", Setting_Debugging)
	AddHandler Setting_Debugging.Changed, AddressOf Setting_Changed
	Setting_DirectionsURL = New Setting("DirectionsURL", GetType(string), "https://maps.googleapis.com/maps/api/directions/json?&sensor=false", "API for Google Directions requests", True, Nothing)
	Items.Add("DirectionsURL", Setting_DirectionsURL)
	AddHandler Setting_DirectionsURL.Changed, AddressOf Setting_Changed
	Setting_ExportFile = New Setting("ExportFile", GetType(string), "", "Last folder/file exported to", True, Nothing)
	Items.Add("ExportFile", Setting_ExportFile)
	AddHandler Setting_ExportFile.Changed, AddressOf Setting_Changed
	Setting_Frames = New Setting("Frames", GetType(string), "", "Form positions", True, Nothing)
	Items.Add("Frames", Setting_Frames)
	AddHandler Setting_Frames.Changed, AddressOf Setting_Changed
	Setting_ForceUSCulture = New Setting("ForceUSCulture", GetType(boolean), "False", "Force comma separator for lists and numbers", False, {"True","False"})
	Items.Add("ForceUSCulture", Setting_ForceUSCulture)
	AddHandler Setting_ForceUSCulture.Changed, AddressOf Setting_Changed
	Setting_GeocodeURL = New Setting("GeocodeURL", GetType(string), "https://maps.google.com/maps/api/geocode/json?&sensor=false", "API for Google Directions requests", True, Nothing)
	Items.Add("GeocodeURL", Setting_GeocodeURL)
	AddHandler Setting_GeocodeURL.Changed, AddressOf Setting_Changed
	Setting_ImportFile = New Setting("ImportFile", GetType(string), "", Nothing, True, Nothing)
	Items.Add("ImportFile", Setting_ImportFile)
	AddHandler Setting_ImportFile.Changed, AddressOf Setting_Changed
	Setting_ImportFolder = New Setting("ImportFolder", GetType(string), "", Nothing, True, Nothing)
	Items.Add("ImportFolder", Setting_ImportFolder)
	AddHandler Setting_ImportFolder.Changed, AddressOf Setting_Changed
	Setting_Installed = New Setting("Installed", GetType(boolean), "False", Nothing, True, Nothing)
	Items.Add("Installed", Setting_Installed)
	AddHandler Setting_Installed.Changed, AddressOf Setting_Changed
	Setting_Language = New Setting("Language", GetType(string), "en", "Force Google Map language: See https://developers.google.com/admin-sdk/directory/v1/languages", False, GoogleLanguages())
	Items.Add("Language", Setting_Language)
	AddHandler Setting_Language.Changed, AddressOf Setting_Changed
	Setting_LargeIcons = New Setting("LargeIcons", GetType(boolean), "True", "Show large icons in the ribbon", False, {"True","False"})
	Items.Add("LargeIcons", Setting_LargeIcons)
	AddHandler Setting_LargeIcons.Changed, AddressOf Setting_Changed
	Setting_Libraries = New Setting("Libraries", GetType(string), "geometry", Nothing, True, Nothing)
	Items.Add("Libraries", Setting_Libraries)
	AddHandler Setting_Libraries.Changed, AddressOf Setting_Changed
	Setting_LogLevel = New Setting("LogLevel", GetType(string), "Warn", "Default Log Level", True, {"Trace","Debug","Info","Warn","Error","Fatal"})
	Items.Add("LogLevel", Setting_LogLevel)
	AddHandler Setting_LogLevel.Changed, AddressOf Setting_Changed
	Setting_Minify = New Setting("Minify", GetType(boolean), "True", "Minify javascript & css", True, {"True","False"})
	Items.Add("Minify", Setting_Minify)
	AddHandler Setting_Minify.Changed, AddressOf Setting_Changed
	Setting_MapStyle = New Setting("MapStyle", GetType(string), "roadmap", "Default map style", False, {"minimal","hybrid","roadmap","satellite","terrain"})
	Items.Add("MapStyle", Setting_MapStyle)
	AddHandler Setting_MapStyle.Changed, AddressOf Setting_Changed
	Setting_RealTimeSet = New Setting("RealTimeSet", GetType(boolean), "False", Nothing, True, {"True","False"})
	Items.Add("RealTimeSet", Setting_RealTimeSet)
	AddHandler Setting_RealTimeSet.Changed, AddressOf Setting_Changed
	Setting_RegexTimeout = New Setting("RegexTimeout", GetType(integer), "100", "Timeout while parsing regular expressions in milliseconds", False, {"10 5000"})
	Items.Add("RegexTimeout", Setting_RegexTimeout)
	AddHandler Setting_RegexTimeout.Changed, AddressOf Setting_Changed
	Setting_Region = New Setting("Region", GetType(string), "us", "Region bias for searches: ISO-3166-1 code", False, GoogleRegions())
	Items.Add("Region", Setting_Region)
	AddHandler Setting_Region.Changed, AddressOf Setting_Changed
	Setting_StartLat = New Setting("StartLat", GetType(double), "42", "Initial map centre latitude", False, {"-90 90"})
	Items.Add("StartLat", Setting_StartLat)
	AddHandler Setting_StartLat.Changed, AddressOf Setting_Changed
	Setting_StartLong = New Setting("StartLong", GetType(double), "19", "Initial map centre longitude", False, {"-180 180"})
	Items.Add("StartLong", Setting_StartLong)
	AddHandler Setting_StartLong.Changed, AddressOf Setting_Changed
	Setting_StartZoom = New Setting("StartZoom", GetType(integer), "4", "Initial map Zoom", False, {"0 16"})
	Items.Add("StartZoom", Setting_StartZoom)
	AddHandler Setting_StartZoom.Changed, AddressOf Setting_Changed
	Setting_TemplateName = New Setting("TemplateName", GetType(string), "", Nothing, True, Nothing)
	Items.Add("TemplateName", Setting_TemplateName)
	AddHandler Setting_TemplateName.Changed, AddressOf Setting_Changed
	Setting_WebTimeout = New Setting("WebTimeout", GetType(integer), "10000", "Timeout for web requests to Google in milliseconds", False, {"10 10000"})
	Items.Add("WebTimeout", Setting_WebTimeout)
	AddHandler Setting_WebTimeout.Changed, AddressOf Setting_Changed
	Setting_Welcome = New Setting("Welcome", GetType(boolean), "False", Nothing, True, Nothing)
	Items.Add("Welcome", Setting_Welcome)
	AddHandler Setting_Welcome.Changed, AddressOf Setting_Changed
End Sub
End Class

