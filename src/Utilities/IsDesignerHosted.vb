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
Imports System.ComponentModel
Imports System.Windows.Forms

Public Module IsDesignerHosted_
    ''' <summary>
    ''' The DesignMode property does not correctly tell you if
    ''' you are in design mode.  IsDesignerHosted is a corrected
    ''' version of that property.
    ''' (see https://connect.microsoft.com/VisualStudio/feedback/details/553305
    ''' and http://stackoverflow.com/a/2693338/238419 )
    ''' </summary>
    Public Function IsDesignerHosted(him As Control) As Boolean
        If LicenseManager.UsageMode = LicenseUsageMode.Designtime Then
            Return True
        End If

        Dim ctrl As Control = him
        While ctrl IsNot Nothing
            If ctrl.Site IsNot Nothing AndAlso ctrl.Site.DesignMode Then
                Return True
            End If
            ctrl = ctrl.Parent
        End While
        Return False
    End Function
End Module
