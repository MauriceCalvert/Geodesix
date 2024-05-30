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
Public Module Validate_

    Public Sub ValidateColour(ByVal name As Object, ByVal value As Object)

        value = GetValue(value)

        If value Is Nothing Then
            Throw New ArgumentException($"{name} is missing")
        End If
        If value.ToString = "" Then
            Throw New ArgumentException($"{name} cannot be empty string")
        End If
        Dim result As String = Colour(value.ToString)
        If result.BeginsWith("!") Then
            Throw New ArgumentException(result)
        End If
    End Sub

    Public Sub Validate(ByVal name As String, ByVal value As Object, ByVal allowed() As Object)
        Dim val As String = value.ToString.ToLower
        Dim msg As String = ""
        For Each s As Object In allowed
            If val.Equals(s) Then
                Return
            End If
            msg = msg & " " & s.ToString
        Next
        msg = name & " '" & val & "' invalid, must be one of " & msg.ToUpper
        Throw New ArgumentException(msg)
    End Sub
    Public Sub ValidateDouble(name As String, input As Object, ByRef output As Double,
                              Optional min As Double = Double.MinValue,
                              Optional max As Double = Double.MaxValue)

        ValidateNumber(Of Double)(name, input, output, min > Double.MinValue OrElse max < Double.MaxValue, min, max)

    End Sub
    Public Sub ValidateInteger(name As String, ByRef input As Object, ByRef output As Integer,
                               Optional min As Integer = Integer.MinValue,
                               Optional max As Integer = Integer.MaxValue)

        ValidateNumber(Of Integer)(name, input, output, min > Integer.MinValue OrElse max < Integer.MaxValue, min, max)

    End Sub
    Private Sub ValidateNumber(Of T As {IConvertible, IComparable, IComparable(Of T), IFormattable, IEquatable(Of T)}) _
        (name As String, input As Object, ByRef output As T, inrange As Boolean, min As T, max As T)

        ' Imperfect, but good enough for us https://stackoverflow.com/a/18120507/338101

        input = GetValueDouble(input)

        If IsEmpty(input) Then
            Throw New ArgumentException($"!{name} is missing")
        End If

        If TypeOf input IsNot Double AndAlso TypeOf input IsNot Integer Then

            Dim inputs As String = input.ToString

            Select Case True

                Case inputs = FETCHING
                    output = DirectCast(Activator.CreateInstance(GetType(T)), T) ' default value
                    Return

                Case inputs = NOTHINGFOUND, inputs.BeginsWith("!"), inputs.BeginsWith("#")
                    Throw New ArgumentException(NOTHINGFOUND) ' Throw = Interrupt calculation and return blank

                Case isExcelErrorCode(input)
                    Throw New ArgumentException(ExcelErrorCode(CInt(input))) ' Throw = Interrupt calculation and return blank

                Case Else
                    Throw New ArgumentException($"Invalid number {input.ToString}") ' Throw = Interrupt calculation and return blank

            End Select
        End If

        If TypeOf input IsNot T Then
            Try
                input = Convert.ChangeType(input, GetType(T))

            Catch ex As Exception
                Throw New ArgumentException($"!{name}: {input} cannot be converted to {GetType(T).Name}")
            End Try
        End If

        If inrange Then
            If input.CompareTo(min) < 0 OrElse input.CompareTo(max) > 0 Then
                If max.CompareTo(Int32.MaxValue) >= 0 Then
                    Throw New ArgumentException($"!{name}: {input} must not be less than {min}")
                Else
                    Throw New ArgumentException($"!{name}: {input} must be between {min} and {max}")
                End If
            End If
        End If

        output = DirectCast(input, T)

    End Sub

End Module
