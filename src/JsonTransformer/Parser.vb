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
Imports Newtonsoft.Json.Linq
Imports Utilities
Public Class Parser

    Public Sub New(template As Template)

        Me.Template = template

    End Sub
    Public Sub Cancel()

        _Cancelled = True

    End Sub
    Public ReadOnly Property Cancelled As Boolean
    Private Property Evaluator As Evaluator
    Public Function Parse() As DataTable

        Schema = Template.Schema
        Evaluator = New Evaluator
        Evaluator.Load(Schema.Rules)

        SkipRules = Schema.
                    Rules.
                    OfType(Of RuleSkip).
                    ToList

        ExpressionRules = Schema.
                          Rules.
                          OfType(Of RuleExpression).
                          ToList

        OutputRules = Schema.
                      Rules.
                      OfType(Of RuleValue).
                      Where(Function(q) q.Output).ToList

        ' Sources as a dictionary keyed on the source path name
        PathRules =
            Schema.
            Rules.
            OfType(Of RulePath).
            ToDictionary(Function(q) q.Source, Function(q) q,
                         StringComparer.OrdinalIgnoreCase)

        _Datatable = New DataTable

        ' Create columns
        For Each rule As RuleValue In OutputRules
            Dim type As Type = Nothing
            TryGetType(rule.Type, type)
            Datatable.Columns.Add(New DataColumn(rule.Name, type))
        Next
        Dim pk As New DataColumn("_ID_", GetType(Integer))
        Datatable.Columns.Add(pk)
        Datatable.PrimaryKey = {pk}

        Dim breakpath As String = Template.BreakPath

        ' Find all the container nodes whose paths are the breakpath
        ' Ignoring, of course, properties, they aren't true branches
        Dim fathers As IEnumerable(Of JContainer) =
            Template.
            Root.
            OfType(Of JContainer).
            Descendants.
            OfType(Of JContainer).
            Where(Function(q) TypeOf q IsNot JProperty).
            Where(Function(q) WildPath(q.Path) = breakpath)

        For Each node As JContainer In fathers

            Evaluator.SetVariable("Path", node.Path)

            Dim children As IEnumerable(Of JValue) =
                node.
                Descendants.
                Where(Function(q) PathRules.ContainsKey(GenericPath(q.Path))).
                OfType(Of JValue)

            For Each child As JValue In children

                Dim childpath As String = GenericPath(child.Path)
                Dim rule As RulePath = PathRules(childpath)

                rule.Node = child
                rule.Value = DirectCast(child, JValue).Value
            Next

            Flush()

            If Cancelled Then
                Exit For
            End If
        Next

        Schema.Message = $"Output {Datatable.Rows.Count} rows"
        Template.ResetValues()

        Return Datatable

    End Function
    Private Sub Flush()

        ' Set the path values
        For Each rule As RulePath In PathRules.Values
            Evaluator.SetVariable(rule.Name, rule.Value)
        Next

        For Each expr As RuleExpression In ExpressionRules
            Evaluator.SetFormula(expr.Name, expr.Source)
        Next

        Evaluator.Engine.Recalculate()

        ' Get the expression values
        For Each rule As RuleExpression In ExpressionRules
            rule.Value = Evaluator.GetValue(rule.Name)
        Next

        If Not SkipRules.Any(Function(q) CBool(q.Value) = True) Then

            ' Add the results
            Dim row As DataRow = Datatable.NewRow
            For Each rule As RuleValue In OutputRules
                row(rule.Name) = rule.Value
            Next
            row("_ID_") = Unique()
            Datatable.Rows.Add(row)

            ' Set path rules' values previous values
            For Each path As RulePath In PathRules.Values
                Evaluator.SetVariable("_" & path.Name, path.Value)
            Next

            ' Set expressions' previous values
            For Each expr As RuleExpression In ExpressionRules
                Evaluator.SetVariable("_" & expr.Name, expr.Value)
            Next

        End If

        ' Reset all the path rules' values
        For Each path As RulePath In PathRules.Values
            path.Value = DefaultValueOf(path.Type)
        Next

    End Sub
    Private ReadOnly Property Datatable As DataTable
    Private Property ExpressionRules As List(Of RuleExpression)
    Private Property PathRules As Dictionary(Of String, RulePath)
    Private Property Schema As Schema
    Private Property SkipRules As List(Of RuleSkip)
    Private ReadOnly Property Template As Template
    Private Property OutputRules As List(Of RuleValue)

End Class
