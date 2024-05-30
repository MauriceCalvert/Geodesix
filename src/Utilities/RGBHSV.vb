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
Imports System.Drawing

''' <summary>
''' An HSV value
''' </summary>
''' <remarks></remarks>
Public Structure HSV
    Implements IEquatable(Of HSV)

    Public Hue As Double
    Public Saturation As Double
    Public Value As Double

    Shared Sub New()
    End Sub
    Public Sub New(color As Color)
        Dim result As HSV = ColorToHSV(color)
        Hue = result.Hue
        Saturation = result.Saturation
        Value = result.Value
    End Sub
    Public Sub New(ByVal H As Double, ByVal S As Double, ByVal V As Double)
        Hue = H
        Saturation = S
        Value = V
    End Sub

    Public Function Equals1(other As HSV) As Boolean Implements IEquatable(Of HSV).Equals
        ' Converting to/from coloours creates rounding errors, sometimes quite ugly.
        ' As this is purely for human consumption, we don't give a fuck
        Return Math.Abs(Hue - other.Hue) < 0.000001 AndAlso
               Math.Abs(Saturation - other.Saturation) < 0.000001 AndAlso
               Math.Abs(Value - other.Value) < 0.000001
    End Function
    Public Overrides Function Equals(obj As Object) As Boolean
        Return Equals1(DirectCast(obj, HSV))
    End Function

    Public Shared Operator =(a As HSV, b As HSV) As Boolean
        Return a.Equals(b)
    End Operator
    Public Shared Operator <>(a As HSV, b As HSV) As Boolean
        Return Not a.Equals(b)
    End Operator
    Public Overrides Function ToString() As String
        Return $"{Hue * 360}°,{Saturation * 100}%,{Value * 100}%"
    End Function
End Structure

''' <summary>
''' Convert between RGB an HSV
''' </summary>
''' <remarks></remarks>
Public Module RGBHSV

    ''' <summary>
    ''' Convert a System.Color to an HSV value
    ''' </summary>
    ''' <param name="color">any colour</param>
    ''' <returns>the HSV equivalent</returns>
    ''' <remarks>Converting between RGB and HSV always works, but it is not guaranteed to be reflexive.
    ''' IOW, converting colour C to an HSV value H and then converting H back to C doesn't always
    ''' give the original value
    ''' </remarks>
    Public Function ColorToHSV(ByVal color As Color) As HSV
        Dim max As Double = Math.Max(color.R, Math.Max(color.G, color.B))
        Dim min As Double = Math.Min(color.R, Math.Min(color.G, color.B))
        Dim result As New HSV
        With result
            .Hue = color.GetHue() / 360
            .Saturation = If((max = 0), 0, 1.0 - (min / max))
            .Value = max / 255
        End With
        Return result
    End Function

    ''' <summary>
    ''' Convert an HSV value to a System.Color
    ''' </summary>
    ''' <param name="hsv">an HSV value</param>
    ''' <returns>the corresponding colour</returns>
    ''' <remarks>Converting between RGB and HSV always works, but it is not guaranteed to be reflexive.
    ''' IOW, converting colour C to an HSV value H and then converting H back to C doesn't always
    ''' give the original value
    ''' </remarks>
    Public Function HSVToColour(ByVal hsv As HSV) As Color
        Dim hi As Double
        Dim f As Double

        With hsv
            Dim hu As Double = .Hue * 360
            hi = Convert.ToInt32(Math.Floor(hu / 60)) Mod 6
            f = hu / 60 - Math.Floor(hu / 60)
            Dim v255 As Double = .Value * 255
            Dim v As Integer = Convert.ToInt32(v255)
            Dim p As Integer = Convert.ToInt32(v255 * (1 - .Saturation))
            Dim q As Integer = Convert.ToInt32(v255 * (1 - f * .Saturation))
            Dim t As Integer = Convert.ToInt32(v255 * (1 - (1 - f) * .Saturation))

            v = Clamp(v, 0, 255)
            p = Clamp(p, 0, 255)
            q = Clamp(q, 0, 255)
            t = Clamp(t, 0, 255)

            If hi = 0 Then
                Return Color.FromArgb(255, v, t, p)
            ElseIf hi = 1 Then
                Return Color.FromArgb(255, q, v, p)
            ElseIf hi = 2 Then
                Return Color.FromArgb(255, p, v, t)
            ElseIf hi = 3 Then
                Return Color.FromArgb(255, p, q, v)
            ElseIf hi = 4 Then
                Return Color.FromArgb(255, t, p, v)
            Else
                Return Color.FromArgb(255, v, p, q)
            End If
        End With
    End Function

End Module
