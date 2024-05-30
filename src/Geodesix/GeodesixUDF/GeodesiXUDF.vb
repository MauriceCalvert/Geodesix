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
Imports System.Runtime.InteropServices
Imports Utilities

''' <summary>
''' Wrapper functions for Excel to hide the ugly RTD functions
''' </summary>
''' <remarks></remarks>
<GuidAttribute("6DDDDDC4-B6E2-448B-8028-D158B1006783"),
ProgIdAttribute(GEODESIX_UDF), ClassInterface(ClassInterfaceType.AutoDual)>
Public Class GeodesiXUDF
    Inherits AddinExpress.MSO.ADXExcelAddinModule

#Region " Component Designer generated code. "
    'Required by designer
    Private components As System.ComponentModel.IContainer

    'Required by designer - do not modify
    'the following method
    Private Sub InitializeComponent()

    End Sub

#End Region

#Region " Add-in Express automatic code "

    <ComRegisterFunctionAttribute()>
    Public Shared Sub AddinRegister(ByVal t As Type)
        AddinExpress.MSO.ADXExcelAddinModule.ADXExcelAddinRegister(t)
    End Sub

    <ComUnregisterFunctionAttribute()>
    Public Shared Sub AddinUnregister(ByVal t As Type)
        AddinExpress.MSO.ADXExcelAddinModule.ADXExcelAddinUnregister(t)
    End Sub

#End Region

    Public Sub New()
        ' Don't even THINK about doing anything else here
        MyBase.New()
        InitializeComponent()

    End Sub
    Public ReadOnly Property ActiveWorkbook As Object
        Get
            Return Excel.ActiveWorkbook
        End Get
    End Property
    Private ReadOnly Property Excel() As Object
        Get
            Return CType(HostApplication, Object)
        End Get
    End Property
    Dim GeodesiXEXL As GeodesiXEXL = DirectCast(AddinExpress.MSO.ADXAddinModule.CurrentInstance, GeodesiXEXL)

End Class
