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
Imports System.Linq

Public Class ExcelRectangle

    Iterator Function Flatten(Of T)(o As Object) As IEnumerable(Of T)

#Disable Warning BC42016 ' Implicit conversion
        For Each item As Object In o
#Enable Warning BC42016 ' Implicit conversion
#Disable Warning BC42016 ' Implicit conversion
            Yield item
#Enable Warning BC42016 ' Implicit conversion
        Next

    End Function

    Public ReadOnly Data(,) As Object
    Public Sub New(range As Object)

        ' Reshape whatever it is to N*2 with lowerbounds=0
        Dim oned As List(Of Double) = Flatten(Of Double)(range).ToList
        Dim row As Integer = 0
        Dim col As Integer = 0
        ReDim Data(CInt(range.Length) \ 2 - 1, 1)

        For i As Integer = 0 To oned.Count - 1
            Data(row, col) = oned(i)
            col += 1
            If col > 1 Then
                row += 1
                col = 0
            End If
        Next

        With Data
            Columns = .GetUpperBound(1)
            Rows = .GetUpperBound(0)
        End With
    End Sub
    Public ReadOnly Property Columns As Integer
    Default Public ReadOnly Property Item(row As Integer, col As Integer) As Object
        Get
            Return Data(row, col)
        End Get
    End Property
    Public ReadOnly Property East As Double
        Get
            Return Enumerable.
                   Range(0, Data.GetUpperBound(0) + 1).
                   Select(Function(q) CDbl(Data(q, 1))).
                   Max
        End Get
    End Property
    Public ReadOnly Property North As Double
        Get
            Return Enumerable.
                   Range(0, Data.GetUpperBound(0) + 1).
                   Select(Function(q) CDbl(Data(q, 0))).
                   Min
        End Get
    End Property
    Public ReadOnly Property South As Double
        Get
            Return Enumerable.
                   Range(0, Data.GetUpperBound(0) + 1).
                   Select(Function(q) CDbl(Data(q, 0))).
                   Max
        End Get
    End Property
    Public ReadOnly Property West As Double
        Get
            Return Enumerable.
                   Range(0, Data.GetUpperBound(0) + 1).
                   Select(Function(q) CDbl(Data(q, 1))).
                   Min
        End Get
    End Property
    Public ReadOnly Property Rows As Integer
    Public Overrides Function ToString() As String
        Return $"Array({Rows},{Columns})"
    End Function
End Class
