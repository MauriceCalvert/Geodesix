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
Imports Flee.CalcEngine.PublicTypes
Imports Flee.PublicTypes
Imports Utilities
Imports System.Xml
Friend Class Evaluator

    Friend ReadOnly Property Engine As CalculationEngine
    Private ReadOnly Property Context As ExpressionContext
    Friend ReadOnly Property Variables As VariableCollection

    Friend Sub New()

        Engine = New CalculationEngine
        Context = New ExpressionContext
        Variables = Context.Variables

    End Sub
    Public Function GetValue(name As String) As Object

        If Engine.Contains(name) Then
            Return Engine.GetResult(name)
        Else
            Return Variables(name)
        End If

    End Function
    Public Sub Load(rules As IEnumerable(Of Rule))

        Context.Imports.AddType(GetType(CustomFunctions))

        ' Import namespaces

        Dim imported As IEnumerable(Of String) = rules.
                                                 OfType(Of RuleImport).
                                                 Select(Function(q) q.Name)

        Dim standard As String() = {"Math", "DateTime", "Geodesix.GeodesixUDF"}

        For Each import As String In imported.Concat(standard).Distinct

            Dim type As Type = Nothing

            If TryGetType(import, type) Then
                Context.Imports.AddType(type)
            Else
                Throw New GeodesixException($"Invalid import {import}")
            End If
        Next

        ' Set path values
        For Each path As RulePath In rules.OfType(Of RulePath)
            Try
                SetVariable(path.Name, path.Value)
                SetVariable("_" & path.Name, path.Value)

            Catch ex As Exception
                path.Message = ex.Message
            End Try
        Next

        ' Give previous's a value
        For Each expr As RuleExpression In rules.OfType(Of RuleExpression)
            Try
                Dim dft As Object = DefaultValueOf(expr.Type)
                SetVariable("_" & expr.Name, dft)

            Catch ex As Exception
                expr.Message = ex.Message
            End Try
        Next

        ' Set the actual formulas
        For Each expr As RuleExpression In rules.OfType(Of RuleExpression)
            Try
                SetFormula(expr.Name, expr.Source)

            Catch ex As Exception
                expr.Message = ex.Message
            End Try
        Next

        Engine.Recalculate()

    End Sub
    Public Sub SetFormula(name As String, formula As String)

        If Engine.Contains(name) Then
            Engine.Remove(name)
        End If

        If Variables.ContainsKey(name) Then
            Throw New GeodesixException($"Can't set formula {formula} for variable {name}")
        End If

        Engine.Add(name, formula, Context)

    End Sub
    Public Sub SetVariable(name As String, value As Object)

        Variables(name) = value

    End Sub
End Class
