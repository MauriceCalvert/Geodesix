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
Imports AddinExpress.RTD

Public Class TopicComparer
    Implements IEqualityComparer(Of ADXRTDTopic)

    Public Shadows Function Equals(x As ADXRTDTopic, y As ADXRTDTopic) As Boolean Implements IEqualityComparer(Of ADXRTDTopic).Equals
        Return x.TopicID = y.TopicID
        'If x Is y Then
        '    Return True
        'End If
        'Dim 
        '    ' String01="geocode" string02="latitude" string03="geneva"
        '' String01="directions" string02="geneva" string03="lausanne" string04="driving"
        'Return _
        '    x.String01 = y.String01 AndAlso
        '    x.String02 = y.String02 AndAlso
        '    x.String03 = y.String03 AndAlso
        '    x.String04 = y.String04
    End Function

    Public Shadows Function GetHashCode(obj As ADXRTDTopic) As Integer Implements IEqualityComparer(Of ADXRTDTopic).GetHashCode
        Return obj.TopicID.GetHashCode
        'Return _
        '    obj.String01.GetHashCode Xor
        '    obj.String02.GetHashCode Xor
        '    obj.String03.GetHashCode Xor
        '    obj.String04.GetHashCode
    End Function
End Class
