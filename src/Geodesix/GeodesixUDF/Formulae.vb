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
Imports System.Text
Imports ExcelFormulaParser
Imports Utilities

Partial Public Class GeodesixUDF

    Public Function Formulae(cell As Object) As String

        Dim result As String = ""
        Dim formula As String = ""

        Try
            If IsNumeric(cell) Then
                Return cell.ToString
            End If

            formula = cell.Formula.ToString ' This is culture-invariant. i.e. with separator ","

            Dim excelFormula As New ExcelFormula(formula)

            Dim tokens As Integer = excelFormula.Count

            If tokens < 1 Then
                Return formula
            End If

            Dim funcname As String = excelFormula(0).Value

            If funcname.Contains(".") Then
                funcname = funcname.Substring(funcname.LastIndexOf(".") + 1)
            End If

            Dim sb As New StringBuilder

            For i As Integer = 0 To tokens - 1

                Dim token As ExcelFormulaToken = excelFormula(i)

                ' Concatenation
                ' Error
                ' Intersection
                ' Logical
                ' Math
                ' Nothing
                ' Number
                ' Range
                ' Start
                ' Stop
                ' Text
                ' Union

                Select Case token.Type

                    Case ExcelFormulaTokenType.Argument
                        sb.Append(", ")

                    Case ExcelFormulaTokenType.Function

                        If token.Subtype = ExcelFormulaTokenSubtype.Start Then
                            sb.Append(token.Value)
                            sb.Append("(")
                        Else
                            sb.Append(")")
                        End If

                    Case ExcelFormulaTokenType.Operand

                        Select Case token.Subtype

                            Case ExcelFormulaTokenSubtype.Text
                                sb.Append(Quoted(token.Value))

                            Case ExcelFormulaTokenSubtype.Number
                                sb.Append(token.Value)

                            Case ExcelFormulaTokenSubtype.Range
                                sb.Append(Literal(Excel.Evaluate(token.Value)))

                            Case Else
                                sb.Append("SubType ")
                                sb.Append(token.Subtype)
                                sb.Append("?")

                        End Select

                    Case ExcelFormulaTokenType.OperatorInfix,
                         ExcelFormulaTokenType.OperatorPostfix,
                         ExcelFormulaTokenType.OperatorPrefix
                        sb.Append(token.Value)

                    Case Else
                        sb.Append("Type ")
                        sb.Append(token.Type)
                        sb.Append("?")

                End Select
            Next

            result = sb.ToString

        Catch ex As Exception
            CustomLog.Logger.Error($"Formulae failed {ex.Message}")
            result = ex.Message

        End Try

        Return result

    End Function
    Private Function Literal(ref As Object) As Object

        If ref Is Nothing Then
            Return ""
        End If

        Try
            If TypeOf ref Is Array Then

                If CInt(ref.rank) = 2 Then
                    ref = JaggedArray(ref)
                End If

                Dim sb As New StringBuilder
                sb.Append("{")

                Dim first As Integer = ref.GetLowerBound(0)
                Dim last As Integer = ref.GetUpperBound(0)

                For i As Integer = first To last

                    sb.Append(Literal(ref(i)))
                    If i < last Then
                        sb.Append(", ")
                    End If
                Next
                sb.Append("}")
                Return sb.ToString

            End If

            Select Case ref.GetType.Name

                Case "__ComObject"
                    Return Literal(ref.Value)

                Case "String"
                    Return Quoted(ref)

                Case Else
                    Return ref.ToString

            End Select

        Catch ex As Exception
            Return $"!{ex.Message}"
        End Try

        Return ref

    End Function

End Class
