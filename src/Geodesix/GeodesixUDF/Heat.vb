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
Imports Utilities
Partial Public Class GeodesixUDF
    Public Function Heat(value As Object, minimum As Object, maximum As Object) As String

        Dim result As String = ""
        Dim map As Map = GeodesiXEXL.MapTaskPane.SheetMap

        Try
            Dim min As Double
            ValidateDouble("Minimum", minimum, min)
            Dim max As Double
            ValidateDouble("Maximum", maximum, max)
            Dim val As Double
            ValidateDouble("Value", value, val, min, max)

            Dim c As Color = HeatToColour(val, min, max)

            result = String.Format("{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B)

        Catch ex As Exception
            CustomLog.Logger.Error("Heat({0},{1},{2}) failed {3}", value, minimum, maximum, ex.Message)
            result = ex.Message

        End Try

        Return result

    End Function

End Class
