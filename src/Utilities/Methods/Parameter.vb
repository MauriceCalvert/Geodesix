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

Public Class Parameter

    Public Event Changed(sender As Object, value As Object)
    Private ReadOnly Property Excel As Object

    Public Sub New(excel As Object,
                   name As String,
                   type As Type, description As String,
                   defaultvalue As Object,
                   choices As IEnumerable(Of Object))

        Me.Excel = excel
        Me.Name = name
        Me.Type = type
        Me.Description = description

        Me.DefaultValue = defaultvalue

        If defaultvalue IsNot Nothing Then
            ' For the purposes of prompting, we need a zero-length string to be in quotes
            _Formula = Quoted(defaultvalue).ToString
            _Value = _Formula
        End If
        Me.Choices = choices

    End Sub
    Public Property Choices As IEnumerable(Of Object) = Nothing
    Public ReadOnly Property DefaultValue As Object
    Private _Formula As String = ""
    Public Property Formula As String
        Get
            Return _Formula
        End Get
        Set(value As String)

            _Formula = value.Trim

            Try
                If IsEmpty(_Formula) Then
                    Exit Property
                End If

                _Value = Excel.Evaluate(_Formula)

                If TypeOf _Value Is Integer AndAlso _Value < 0 Then ' Excel couldn't make sense of it

                    If Type.Name = "String" Then
                        _Value = Quoted(_Formula)
                    Else
                        _Value = _Formula
                    End If
                    _Formula = _Value.ToString
                Else
                    _Value = GetValue(_Value)
                End If

            Catch ex As Exception
                _Value = ex.Message
            End Try

            RaiseEvent Changed(Me, value)

        End Set
    End Property
    Public ReadOnly Property Name As String
    Public ReadOnly Property Description As String
    Public Overrides Function ToString() As String
        Return $"{Name} {Formula}={Value}"
    End Function
    Public ReadOnly Property Type As Type
    Public ReadOnly Property Valid As Boolean
        Get
            If Value Is Nothing AndAlso DefaultValue Is Nothing Then
                Return False
            End If

            If Choices IsNot Nothing Then

                Select Case Choices.Count
                    Case 0
                        Return False
                    Case 1
                        Return CBool(Value >= Choices(0))
                    Case 2
                        Dim junk As Double
                        If Double.TryParse(Value.ToString, junk) Then
                            Return Between(CDbl(Value), CDbl(Choices(0)), CDbl(Choices(1)))
                        Else
                            Return Choices.Any(Function(q) q.ToString = Value.ToString)
                        End If
                    Case Else
                        Return Choices.Any(Function(q) q.ToString = Value.ToString)
                End Select
            End If
            If Value.GetType Is Type Then
                Return True
            End If
            Return IsNumeric(Value) AndAlso (Type.Name = "Double" OrElse Type.Name = "Int32")
        End Get
    End Property
    Public ReadOnly Property Value As Object

End Class
