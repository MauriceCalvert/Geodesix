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
Imports Utilities
Public Class Setting

    Public Event Changed(sender As Setting, value As String)
    Public Sub New(name As String, type As Type, initial As String, description As String, hidden As Boolean, valid As Object)

        Me.Name = name
        Me.Type = type
        Me.Initial = initial
        _Value = initial
        Me.Description = description
        Me.Hidden = hidden

        If valid IsNot Nothing Then

            Dim tn As String = GetTypeName(valid)

            Select Case tn

                Case "String[]"
                    If valid.length = 1 Then
                        ' For numbers, a single value means a minimum and a maximum
                        If IsNumeric(type) Then
                            Dim vals As String() = CType(valid(0).Split(" "c), String())
                            Minimum = CInt(Val(0))
                            If vals.Count > 1 Then
                                Maximum = CInt(vals(1))
                            Else
                                Maximum = Integer.MaxValue
                            End If
                        Else
                            Values = valid(0).ToString.Split(" "c).ToList
                        End If
                    Else
                        Values = DirectCast(valid, String()).ToList
                    End If

                Case "Object[]"
                    Values = DirectCast(valid, Object()).ToList

                Case "List<String>"
                    Values = DirectCast(valid, List(Of String))

                Case "List<Pair>"
                    Values = DirectCast(valid, List(Of Pair))

                Case Else
                    Stop

            End Select
        End If
    End Sub
    Public ReadOnly Property Description As String
    Public ReadOnly Property Hidden As Boolean
    Public ReadOnly Property Initial As String
    Friend Sub Initialise(value As String)
        _Value = value
    End Sub
    Public ReadOnly Property Maximum As Integer
    Public ReadOnly Property Minimum As Integer
    Public ReadOnly Property Name As String
    Public Overrides Function ToString() As String
        Return $"{Name}={Value}"
    End Function
    Public ReadOnly Property Type As Type
    Private _Value As String
    Public Property Value As String
        Get
            Return _Value
        End Get
        Set(value As String)

            If value = _Value Then
                Exit Property
            End If
            _Value = value

            Select Case Type.Name

                Case "String"
                    If Values IsNot Nothing Then
                        Dim tn As String = GetTypeName(Values)
                        If tn = "List<Pair>" Then
                            Dim vals As List(Of Pair) = DirectCast(Values, List(Of Pair))
                            If Not vals.Any(Function(q) q.Value = _Value) Then
                                Throw New FormatException($"{Name}: Invalid value {_Value}. Valid: {String.Join(", ", vals.Select(Function(q) q.Value))}")
                            End If
                        Else
                            If Not Values.Any(Function(q) CBool(q = _Value)) Then
                                Throw New FormatException($"{Name}: Invalid value {_Value}. Valid: {String.Join(", ", Values)}")
                            End If
                        End If
                    End If
                    _Value = value

                Case "Boolean"
                    Dim dummy As Boolean
                    If Not Boolean.TryParse(_Value, dummy) Then
                        Throw New FormatException($"{Name}: Invalid boolean {_Value}")
                    End If
                    _Value = dummy.ToString

                Case "Int32"
                    Dim dummy As Integer
                    If Not Integer.TryParse(value, dummy) Then
                        Throw New FormatException($"{Name}: Invalid integer {_Value}")
                    End If
                    If value IsNot Nothing Then
                        If Not Between(dummy, Minimum, Maximum) Then
                            Throw New ArgumentOutOfRangeException($"{Name}: {_Value} must be between {Minimum} and {Maximum}")
                        End If
                    End If
                    _Value = value

                Case "Double"
                    Dim dummy As Double
                    If Not Double.TryParse(value, dummy) Then
                        Throw New FormatException($"{Name}: Invalid real number {_Value}")
                    End If
                    If value IsNot Nothing Then
                        If Not Between(dummy, Minimum, Maximum) Then
                            Throw New ArgumentOutOfRangeException($"{Name}: {_Value} must be between {Minimum} and {Maximum}")
                        End If
                    End If
                    _Value = value

                Case Else
                    Throw New InvalidOperationException($"Unknown data type {Type.Name}")

            End Select

            RaiseEvent Changed(Me, _Value)
        End Set
    End Property

    Public ReadOnly Property Values As IEnumerable(Of Object)
    Public ReadOnly Property ValueType As String
End Class
