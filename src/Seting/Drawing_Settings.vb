Imports System.Collections.Generic
Imports System.Linq
Imports Utilities
Public Class DrawingSettings
	Inherits SettingsBase

	Public Sub New()
		MyBase.New("Geodesix")
	End Sub

	
Public Setting_align As Setting
Public Property align as String
    Get
        Return Setting_align.Value
    End Get
    Set(value As String)
        Setting_align.Value = value
    End Set
End Property
	
Public Setting_arrow As Setting
Public Property arrow as String
    Get
        Return Setting_arrow.Value
    End Get
    Set(value As String)
        Setting_arrow.Value = value
    End Set
End Property
	
Public Setting_arrowColor As Setting
Public Property arrowColor as String
    Get
        Return Setting_arrowColor.Value
    End Get
    Set(value As String)
        Setting_arrowColor.Value = value
    End Set
End Property
	
Public Setting_arrowSize As Setting
Public Property arrowSize as String
    Get
        Return Setting_arrowSize.Value
    End Get
    Set(value As String)
        Setting_arrowSize.Value = value
    End Set
End Property
	
Public Setting_icon As Setting
Public Property icon as String
    Get
        Return Setting_icon.Value
    End Get
    Set(value As String)
        Setting_icon.Value = value
    End Set
End Property
	
Public Setting_iconColor As Setting
Public Property iconColor as String
    Get
        Return Setting_iconColor.Value
    End Get
    Set(value As String)
        Setting_iconColor.Value = value
    End Set
End Property
	
Public Setting_iconSize As Setting
Public Property iconSize as String
    Get
        Return Setting_iconSize.Value
    End Get
    Set(value As String)
        Setting_iconSize.Value = value
    End Set
End Property
	
Public Setting_lineTitle As Setting
Public Property lineTitle as String
    Get
        Return Setting_lineTitle.Value
    End Get
    Set(value As String)
        Setting_lineTitle.Value = value
    End Set
End Property
	
Public Setting_strokeColor As Setting
Public Property strokeColor as String
    Get
        Return Setting_strokeColor.Value
    End Get
    Set(value As String)
        Setting_strokeColor.Value = value
    End Set
End Property
	
Public Setting_strokeOpacity As Setting
Public Property strokeOpacity as String
    Get
        Return Setting_strokeOpacity.Value
    End Get
    Set(value As String)
        Setting_strokeOpacity.Value = value
    End Set
End Property
	
Public Setting_strokeWeight As Setting
Public Property strokeWeight as String
    Get
        Return Setting_strokeWeight.Value
    End Get
    Set(value As String)
        Setting_strokeWeight.Value = value
    End Set
End Property
	
Public Setting_symbols As Setting
Public Property symbols as String
    Get
        Return Setting_symbols.Value
    End Get
    Set(value As String)
        Setting_symbols.Value = value
    End Set
End Property
	
Public Setting_title As Setting
Public Property title as String
    Get
        Return Setting_title.Value
    End Get
    Set(value As String)
        Setting_title.Value = value
    End Set
End Property
Overrides Sub MakeItems()
	_Items = New Dictionary(Of String, Setting)(StringComparer.OrdinalIgnoreCase)
	Setting_align = New Setting("align", GetType(string), "Centre", "Alignment of icons", False, {"Centre","Top","Bottom","Left","Right"})
	Items.Add("align", Setting_align)
	AddHandler Setting_align.Changed, AddressOf Setting_Changed
	Setting_arrow = New Setting("arrow", GetType(string), "Expand_Less", "Material Symbol for arrows. Use underscores for spaces!", False, Nothing)
	Items.Add("arrow", Setting_arrow)
	AddHandler Setting_arrow.Changed, AddressOf Setting_Changed
	Setting_arrowColor = New Setting("arrowColor", GetType(string), "Blue", "Border colour of arrows on lines", False, Nothing)
	Items.Add("arrowColor", Setting_arrowColor)
	AddHandler Setting_arrowColor.Changed, AddressOf Setting_Changed
	Setting_arrowSize = New Setting("arrowSize", GetType(double), "6", "Size of the arrows on lines", False, {"1"})
	Items.Add("arrowSize", Setting_arrowSize)
	AddHandler Setting_arrowSize.Changed, AddressOf Setting_Changed
	Setting_icon = New Setting("icon", GetType(string), "$Push_Pin", "Marker icon URL or Material Symbol - use underscores for spaces!", False, Nothing)
	Items.Add("icon", Setting_icon)
	AddHandler Setting_icon.Changed, AddressOf Setting_Changed
	Setting_iconColor = New Setting("iconColor", GetType(string), "Red", "Color of icons", False, Nothing)
	Items.Add("iconColor", Setting_iconColor)
	AddHandler Setting_iconColor.Changed, AddressOf Setting_Changed
	Setting_iconSize = New Setting("iconSize", GetType(double), "24", "Size of icons in pixels", False, {"1"})
	Items.Add("iconSize", Setting_iconSize)
	AddHandler Setting_iconSize.Changed, AddressOf Setting_Changed
	Setting_lineTitle = New Setting("lineTitle", GetType(string), "", "The text displayed when hovering over a line", False, Nothing)
	Items.Add("lineTitle", Setting_lineTitle)
	AddHandler Setting_lineTitle.Changed, AddressOf Setting_Changed
	Setting_strokeColor = New Setting("strokeColor", GetType(string), "Red", "Colour of lines between markers", False, Nothing)
	Items.Add("strokeColor", Setting_strokeColor)
	AddHandler Setting_strokeColor.Changed, AddressOf Setting_Changed
	Setting_strokeOpacity = New Setting("strokeOpacity", GetType(double), "1", "Opacity of lines between markers", False, {"0 1"})
	Items.Add("strokeOpacity", Setting_strokeOpacity)
	AddHandler Setting_strokeOpacity.Changed, AddressOf Setting_Changed
	Setting_strokeWeight = New Setting("strokeWeight", GetType(double), "1", "Width of lines between markers in pixels", False, {"0"})
	Items.Add("strokeWeight", Setting_strokeWeight)
	AddHandler Setting_strokeWeight.Changed, AddressOf Setting_Changed
	Setting_symbols = New Setting("symbols", GetType(string), "outlined", "See https://fonts.google.com/icons", False, {"outlined","rounded","sharp"})
	Items.Add("symbols", Setting_symbols)
	AddHandler Setting_symbols.Changed, AddressOf Setting_Changed
	Setting_title = New Setting("title", GetType(string), "", "The text displayed when hovering over a marker", False, Nothing)
	Items.Add("title", Setting_title)
	AddHandler Setting_title.Changed, AddressOf Setting_Changed
End Sub
End Class

