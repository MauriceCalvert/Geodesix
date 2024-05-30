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
Imports System.Globalization
Imports System.Linq
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports AddinExpress.MSO
Imports Geodesics
Imports Seting
Imports Utilities
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Diagnostics

''' <summary>
''' Interface to handle Excel events
''' </summary>
''' <remarks>Handles clicks on buttons and right-click context menu</remarks>
<GuidAttribute("B3933D3D-FA7A-48E4-A1A3-97EE6331C22F"), ProgIdAttribute(GEODESIX_EXL)>
Public Class GeodesiXEXL
    Inherits ADXAddinModule

    Private ReadOnly Property CurrentCulture As System.Globalization.CultureInfo = Nothing
    Private ReadOnly Property OfficeCulture As System.Globalization.CultureInfo = Nothing
    Friend WithEvents Geodesic As Geodesic
    Friend Property RibbonUI As IRibbonUI

#Region " Add-in Express automatic code "

    'Required by Add-in Express - do not modify
    'the methods within this region

    Public Overrides Function GetContainer() As System.ComponentModel.IContainer
        If components Is Nothing Then
            components = New System.ComponentModel.Container
        End If
        GetContainer = components
    End Function

    <ComRegisterFunctionAttribute()>
    Public Shared Sub AddinRegister(ByVal t As Type)
        AddinExpress.MSO.ADXAddinModule.ADXRegister(t)
    End Sub

    <ComUnregisterFunctionAttribute()>
    Public Shared Sub AddinUnregister(ByVal t As Type)
        AddinExpress.MSO.ADXAddinModule.ADXUnregister(t)
    End Sub

    Public Overrides Sub UninstallControls()
        MyBase.UninstallControls()
    End Sub

#End Region
    Private Sub AddinModule_AddinStartupComplete(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Me.AddinStartupComplete

        Try
            Dim debugging As Boolean = Settings.Debugging = "True"
#If DEBUG Then
            debugging = True
#Else
            Dim msg1 As String = ""
            Dim msg2 As String = ""
            If debugging Then
                msg1 = "Options->Advanced->LogLevel Is Debug, performance will be degraded"
            End If
            If Settings.Minify <> "True" Then
                msg2 = "Options->Advanced->Compress disabled, generated files will be bloated"
            End If
            If Not IsEmpty(msg1) OrElse Not IsEmpty(msg2) Then
                Dim msg As String = (msg1 & vbCR & msg2).Trim(vbCR)
                ShowBox(msg, icon:=System.Windows.Forms.MessageBoxIcon.Warning)
            End If
#End If
            CustomLog.StartLogger(debugging)
            CustomLog.Logger.Debug("{0} AddinStartupComplete in {1}", GEODESIX_EXL, System.AppDomain.CurrentDomain.FriendlyName)

            RibbonDebugging.Visible = debugging

            Singleton.Cache = New GlobalCache(Excel)
            _GlobalCache = Singleton.Cache

            SetRealtime()
            ClearTempFolders()

            Geodesic = New Geodesic()

            AddHandler Settings.Changed, AddressOf Setting_Changed

            _CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture
            ' Place.MsoAppLanguageID.msoLanguageIDUI = 2, but we can't import Office, need to stay version-independent
            _OfficeCulture = New System.Globalization.CultureInfo(CInt(Excel.LanguageSettings.LanguageID(2)))
            CustomLog.Logger.Debug("Windows culture {0}", CurrentCulture.DisplayName)
            CustomLog.Logger.Debug("Office  culture {0}", OfficeCulture.DisplayName)

            ' Office language and region
            If Settings.ForceUSCulture = "True" Then
                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-us")
            Else
                Dim cc As String = Settings.CultureCheck
                If cc <> "Never" Then
                    If OfficeCulture.LCID <> CurrentCulture.LCID Then
                        If OfficeCulture.ThreeLetterISOLanguageName = CurrentCulture.ThreeLetterISOLanguageName Then
                            If cc = "Culture" Then
                                Dim msg As String = "Installed version of Office Is  " & OfficeCulture.DisplayName & vbCRLF &
                                                    "Regional settings are currently " & CurrentCulture.DisplayName & vbCRLF &
                                                    "If Geocode returns #VALUE, you will need to make them the same Or install the Office language pack."
                                CustomLog.Logger.Warn(msg.Replace(vbCRLF, " "))
                                ShowBox(msg, icon:=System.Windows.Forms.MessageBoxIcon.Warning)
                            End If
                        Else
                            Dim msg As String = "Installed version of Office Is  " & OfficeCulture.DisplayName & vbCRLF &
                                                "Regional settings are currently " & CurrentCulture.DisplayName & vbCRLF &
                                                "THIS Is Not SUPPORTED (Geocode will always return #VALUE) until you have installed the regional Office language pack."
                            CustomLog.Logger.Fatal(msg.Replace(vbCRLF, " "))
                            ShowBox(msg, icon:=System.Windows.Forms.MessageBoxIcon.Stop)
                        End If
                    End If
                End If
            End If

            ' Google language and region
            Dim version As String = Excel.Evaluate("=Geocode(""version"",0)").ToString ' #26# *ESSENTIAL* Force load in current AppDomain

            If Settings.APIKey = "" Then ' Hasn't supplied a key yet

                ShowFormDialog(New Options(Me))

                If IsEmpty(Settings.APIKey) Then ' Didn't supply a key
                    CustomLog.Logger.Error("Geodesix functions won't work until you supply an API key!")
                    ShowBox("Geodesix functions won't work until you supply an API key!")
                Else
                    Task.Run(Sub()
                                 Try
                                     Shell(Path.Combine(GetExecutingPath, "ZoneStripper.exe"), "-r .", windowstyle:=ProcessWindowStyle.Hidden)
                                 Catch ex As Exception
                                     CustomLog.Logger.Error(ex, "ZoneStripper")
                                 End Try
                             End Sub)

                    If Settings.Welcome <> "True" Then
                        Settings.Welcome = "True"
                        RibbonUI?.ActivateTab(GeodesiXRibbonTab.Id)
                        ShowHelp("welcome.html")
                    End If
                End If
            End If

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Public ReadOnly Property ActiveWorkbook As Object
        Get
            Return Excel.ActiveWorkbook
        End Get
    End Property
    Private Shared _DefaultGeocoder As String = ""
    Public ReadOnly Property DefaultGeocoder As String
        Get
            If _DefaultGeocoder = "" Then
                _DefaultGeocoder = MakeBasic(Path.Combine(DataFolder, "geocoder.htm"))
            End If
            Return _DefaultGeocoder
        End Get
    End Property
    Public ReadOnly Property Excel() As Object
        Get
            Return HostApplication
        End Get
    End Property
    Private Sub GeodesiXEXL_OnError(e As ADXErrorEventArgs) Handles Me.OnError
        HandleError("Addin Error", e.ADXError)
    End Sub
    Public Function GeoFields() As IEnumerable(Of Object)

        Return GlobalCache.GeoFields

    End Function
    Public ReadOnly Property GlobalCache As GlobalCache
    Public Property Language As String
        Get
            Return Settings.Language
        End Get
        Set(value As String)
            If value <> Settings.Language Then
                Settings.Language = value
                _DefaultGeocoder = "" ' Force re-creation
            End If
        End Set
    End Property
    Friend Property LargeIcons As Boolean
        Get
            Return _ShowLargeIcons
        End Get
        Set(value As Boolean)
            _ShowLargeIcons = value
            For Each rg As ADXRibbonGroup In GeodesiXRibbonTab.Controls.OfType(Of ADXRibbonGroup)
                For Each btn As ADXRibbonButton In rg.Controls.OfType(Of ADXRibbonButton)
                    If ShowLargeIcons Then
                        btn.Size = ADXRibbonXControlSize.Large
                    Else
                        btn.Size = ADXRibbonXControlSize.Regular
                    End If
                Next
            Next
            Settings.LargeIcons = ShowLargeIcons
        End Set
    End Property
    Private _MapTaskPane As MapTaskPane
    Public Property MapTaskPane() As MapTaskPane
        Get
            Return _MapTaskPane
        End Get
        Friend Set(value As MapTaskPane)
            _MapTaskPane = value
        End Set
    End Property
    Public Property Region As String
        Get
            Return Settings.Region
        End Get
        Set(value As String)
            If value <> Settings.Region Then
                Settings.Region = value
                _DefaultGeocoder = "" ' Force re-creation
            End If
        End Set
    End Property
    Private ReadOnly Property ShowLargeIcons As Boolean
    Private Sub Setting_Changed(setting As Setting, value As String)

        Dim reload As Boolean

        If setting.Name = "Language" Then
            If Geodesic.Language <> setting.Value Then
                Geodesic.Language = setting.Value
                reload = True
            End If
        End If

        If setting.Name = "Region" Then
            If Geodesic.Region <> setting.Value Then
                Geodesic.Region = setting.Value
                reload = True
            End If
        End If

        If reload Then
            _DefaultGeocoder = ""
            MapTaskPane.ReGenerate()
        End If

    End Sub

End Class

