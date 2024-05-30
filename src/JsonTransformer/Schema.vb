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
Imports System.Text.RegularExpressions
Imports Utilities
Public Class Schema

    Public Sub New(template As Template)

        Me.Template = template

    End Sub
    Public Function AddExpression(Optional name As String = Nothing,
                                  Optional type As String = Nothing,
                                  Optional output As Boolean = False,
                                  Optional formula As String = Nothing) As RuleExpression

        Dim rule As New RuleExpression(Template, name, type, output, formula)
        Rules.Add(rule)
        Return rule

    End Function
    Public Function AddImport(Optional name As String = Nothing) As RuleImport

        Dim rule As New RuleImport(name)
        Rules.Add(rule)
        Return rule

    End Function
    Public Function AddPath(Optional name As String = Nothing,
                            Optional type As String = Nothing,
                            Optional output As Boolean = False,
                            Optional path As String = Nothing,
                            Optional value As Object = Nothing) As RulePath

        Dim rule As New RulePath(Template, name, type, output, path, value)
        Rules.Add(rule)
        Return rule

    End Function
    Public Function AddSkip(Optional name As String = Nothing,
                            Optional type As String = Nothing,
                            Optional formula As String = Nothing) As RuleSkip

        Dim rule As New RuleSkip(Template, name, type, formula)
        Rules.Add(rule)
        Return rule

    End Function
    Public Function AddSort(Optional name As String = Nothing) As RuleSort

        Dim rule As New RuleSort(name)
        Rules.Add(rule)
        Return rule

    End Function
    Public Sub Clear()

        _Rules = New List(Of Rule)

    End Sub
    Friend ReadOnly Property Evaluator As Evaluator
    Public Function isSelected(path As String) As Boolean

        Dim generic As String = GenericPath(path)

        Return Rules.
               OfType(Of RulePath).
               Any(Function(q) q.Source = generic)

    End Function
    Private _Message As String
    Public Property Message As String
        Get
            Return _Message
        End Get
        Friend Set(value As String)
            _Message = value
        End Set
    End Property
    Public Sub Remove(rule As Rule)

        Dim index As Integer = Rules.IndexOf(rule)
        If index >= 0 Then
            Rules.RemoveAt(index)
        End If

    End Sub
    Public Function Rule(index As Integer) As Rule

        Return _Rules(index)

    End Function
    Public Property Rules As New List(Of Rule)
    Private ReadOnly Property Template As Template
    Public ReadOnly Property Valid As Boolean
        Get
            ' Reset messages
            Message = Nothing
            For Each vr As Rule In Rules
                vr.Message = Nothing
            Next

            ' Ensure all names are legal
            Dim misnamed As IEnumerable(Of Rule) =
                Rules.
                Where(Function(q) TypeOf q IsNot RuleImport).
                Where(Function(q) Not ValidName(q))

            For Each rule As Rule In misnamed
                rule.Message = $"Invalid name {rule.Name}. Can only consist of letters."
            Next

            ' Ensure no duplicate names
            Dim duplicates As IEnumerable(Of Rule) =
                Rules.
                Where(Function(q) TypeOf q IsNot RuleSort).
                GroupBy(Function(q) q.Name, StringComparer.InvariantCultureIgnoreCase).
                Where(Function(q) q.Count > 1).
                SelectMany(Function(q) q)

            For Each rule As Rule In duplicates
                rule.Message = $"Duplicate name {rule.Name}"
            Next

            ' Reset path values to the sample value
            Dim missingvalues As IEnumerable(Of RulePath) =
                Rules.
                OfType(Of RulePath).
                Where(Function(q) IsEmpty(q.Value))

            For Each rule As RulePath In missingvalues
                rule.Value = rule.Sample
            Next

            ' Ensure all Imports are legal
            Dim badimports As IEnumerable(Of RuleImport) =
                Rules.
                OfType(Of RuleImport).
                Where(Function(q)
                          Dim type As Type = Nothing
                          Return Not TryGetType(q.Name, type)
                      End Function)

            For Each badimport As RuleImport In badimports
                badimport.Message = $"Invalid import {badimport.Name}"
            Next

            ' Ensure only 1 sort
            Dim sorts As IEnumerable(Of Rule) = Rules.OfType(Of RuleSort)
            If sorts.Count > 1 Then
                For Each badsort As RuleSort In sorts
                    badsort.Message = $"Only one sort allowed"
                Next

            End If

            ' Ensure all sorts are legal
            Dim badsorts As IEnumerable(Of RuleSort) =
                Rules.
                OfType(Of RuleSort).
                Where(Function(x)
                          Return Not Rules.
                                     OfType(Of RuleValue).
                                     Where(Function(y) x.Name = y.Name).
                                     Any
                      End Function)

            For Each badsort As RuleSort In badsorts
                badsort.Message = $"Invalid sort {badsort.Name}"
            Next

            ' Ensure all expressions have formulas
            Dim missingformulas As IEnumerable(Of RuleValue) =
                Rules.
                OfType(Of RuleValue).
                Where(Function(q) IsEmpty(q.Source))

            For Each rule As RuleValue In missingformulas
                rule.Message = $"Missing formula"
            Next

            ' Errors so far?
            Dim failed As IEnumerable(Of Rule) = Rules.Where(Function(q) Not IsEmpty(q.Message))

            If failed.Count <> 0 Then ' Yes, no point in evaluating
                Message = $"{failed.Count} errors"
                Return False
            End If

            ' Preliminary checks OK, setup calculations
            _Evaluator = New Evaluator

            Try
                _Evaluator.Load(Rules)
            Catch ex As Exception
                Message = $"Evaluation failed {ex.Message}"
                Return False
            End Try

            ' Get calculated values
            For Each expr As RuleExpression In Rules.OfType(Of RuleExpression)
                Try
                    Dim val As Object = Evaluator.GetValue(expr.Name)
                    expr.Value = val

                Catch ex As Exception
                    expr.Message = ex.Message
                End Try
            Next

            failed = Rules.Where(Function(q) Not IsEmpty(q.Message))

            If failed.Count <> 0 Then
                Message = $"{failed.Count} error(s)"
                Return False
            End If

            Message = $"{Rules.Count} rules validated"

            Return True

        End Get
    End Property
    Private Shared ReadOnly Property IdentifierRegex As New Regex("^[a-zA-Z]*$", RegexOptions.Compiled)
    Private Function ValidName(rule As Rule) As Boolean

        If IsEmpty(rule.Name) Then
            rule.Message = "Missing name"
            Return False
        End If

        If Not IdentifierRegex.IsMatch(rule.Name) Then
            rule.Message = $"{rule.Name} has invalid characters (only A..Z allowed, no digits)"
            Return False
        End If

        Return True

    End Function
    Public Function Value(variable As String) As Object

        Return Evaluator.GetValue(variable)

    End Function
End Class
