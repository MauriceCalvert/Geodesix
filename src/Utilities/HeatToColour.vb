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

Public Class HeatMapper

    Private ReadOnly MIN As Long
    Private ReadOnly MAX As Long
    Private ReadOnly TABLESIZE As Long = 0
    Private ReadOnly Table() As Color
    Public Sub New(MinValue As Integer, MaxValue As Integer)
        MIN = MinValue
        MAX = MaxValue
        Dim range As Long = MAX - MIN + 1
        TABLESIZE = range
        ReDim Table(Convert.ToInt32(range))
        For x As Long = 0 To TABLESIZE
            Table(Convert.ToInt32(x)) = HeatToColour(x, 0, TABLESIZE)
        Next
    End Sub
    ' Fast version, about 12-fold
    Public Function Color(value As Integer) As Color

        If value < MIN Then
            Return Color.Black
        End If

        If value > MAX Then
            Return Color.White
        End If

        Dim i As Long = (value - MIN) * TABLESIZE \ (MAX - MIN + 1)
        Dim answer As Color = Table(Convert.ToInt32(i))
        Return answer

    End Function
End Class

Public Module HeatToColour_
    ' Thanks to Ian Boyd's excellent post here:
    ' http://stackoverflow.com/questions/2374959/algorithm-to-convert-any-positive-double-to-an-rgb-value

    Private Const MinVisibleWaveLength As Double = 390.0
    Private Const MaxVisibleWaveLength As Double = 750.0
    Private Const Gamma As Double = 0.8
    Private Const IntensityMax As Double = 255

    Public Function HeatToColour(ByVal value As Double, ByVal MinValue As Double, ByVal MaxValue As Double) As Color

        Dim wavelength As Double
        Dim Red As Double
        Dim Green As Double
        Dim Blue As Double
        Dim Factor As Double
        Dim scaled As Double

        If value < MinValue Then
            value = MinValue
        ElseIf value > MaxValue Then
            value = MaxValue
        End If

        scaled = (value - MinValue) / (MaxValue - MinValue)

        wavelength = scaled * (MaxVisibleWaveLength - MinVisibleWaveLength) + MinVisibleWaveLength

        Select Case Math.Floor(wavelength)

            Case 380 To 439
                Red = -(wavelength - 440) / (440 - 380)
                Green = 0.0
                Blue = 1.0

            Case 440 To 489
                Red = 0.0
                Green = (wavelength - 440) / (490 - 440)
                Blue = 1.0

            Case 490 To 509
                Red = 0.0
                Green = 1.0
                Blue = -(wavelength - 510) / (510 - 490)

            Case 510 To 579
                Red = (wavelength - 510) / (580 - 510)
                Green = 1.0
                Blue = 0.0

            Case 580 To 644
                Red = 1.0
                Green = -(wavelength - 645) / (645 - 580)
                Blue = 0.0

            Case 645 To 780
                Red = 1.0
                Green = 0.0
                Blue = 0.0

                'Case Else
                '    Red = 0.0
                '    Green = 0.0
                '    Blue = 0.0

        End Select

        ' Let the intensity fall off near the vision limits
        Select Case Math.Floor(wavelength)
            Case 380 To 419
                Factor = 0.3 + 0.7 * (wavelength - 380) / (420 - 380)
            Case 420 To 700
                Factor = 1.0
            Case 701 To 780
                Factor = 0.3 + 0.7 * (780 - wavelength) / (780 - 700)
        End Select

        Dim R As Integer = Adjust(Red, Factor)
        Dim G As Integer = Adjust(Green, Factor)
        Dim B As Integer = Adjust(Blue, Factor)

        Dim result As Color = System.Drawing.Color.FromArgb(255, R, G, B)
        Dim resulthsv As New HSV
        resulthsv = ColorToHSV(result)
        resulthsv.Value = 0.7 + 0.1 * scaled + 0.2 * Math.Sin(scaled * Math.PI)
        result = HSVToColour(resulthsv)

        Return result

    End Function
    Private Function Adjust(ByVal Colour As Double, ByVal Factor As Double) As Integer
        If Colour = 0 Then
            Return 0
        Else
            Return Convert.ToInt32(IntensityMax * Math.Pow(Colour * Factor, Gamma))
        End If
    End Function
End Module
