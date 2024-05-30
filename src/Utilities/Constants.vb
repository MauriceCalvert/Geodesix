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
Public Module Constants
    Public Const GEOCODECACHEWORKSHEET As String = "$GeoCache$"
    Public Const TRAVELCACHEWORKSHEET As String = "$LocationCache$"
    Public Const FETCHING As String = "Fetching..."
    Public Const NOTHINGFOUND As String = "Nothing found"
    Public Const GEOCODES As String = "Geocodes"
    Public Const GEODESIX_EXL As String = "GeodesiX.Addin"
    Public Const GEODESIX_RTD As String = "GeodesiX.RTD"
    Public Const GEODESIX_UDF As String = "GeodesiX.UDF"
    Public ReadOnly Property vbTab As String = Convert.ToChar(9)
    Public ReadOnly Property vbCR As String = Convert.ToChar(13)
    Public ReadOnly Property vbLF As String = Convert.ToChar(10)
    Public ReadOnly Property vbCRLF As String = Convert.ToChar(13) & Convert.ToChar(10)
    Public ReadOnly Property LISTSEP As Char = CChar(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator)
    Public Enum MsgBoxStyle
        ApplicationModal = &H0
        DefaultButton1 = &H0
        OKOnly = &H0
        OKCancel = &H1
        AbortRetryIgnore = &H2
        YesNoCancel = &H3
        YesNo = &H4
        RetryCancel = &H5
        Critical = &H10
        Question = &H20
        Exclamation = &H30
        Information = &H40
        DefaultButton2 = &H100
        DefaultButton3 = &H200
        SystemModal = &H1000
        MsgBoxHelp = &H4000
        MsgBoxSetForeground = &H10000
        MsgBoxRight = &H80000
        MsgBoxRtlReading = &H100000
    End Enum
    Public Enum MsgBoxResult
        OK = 1
        Cancel = 2
        Abort = 3
        Retry = 4
        Ignore = 5
        Yes = 6
        No = 7
    End Enum
End Module
