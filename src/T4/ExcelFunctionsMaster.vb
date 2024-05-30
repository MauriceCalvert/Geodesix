Imports System.Collections.Generic
Imports Utilities
Imports System.Linq

Public Class ExcelFunctions
    Inherits Methods

    Private ReadOnly Property DoubleType As Type = GetType(Double)
    Private ReadOnly Property IntegerType As Type = GetType(Integer)
    Private Class Range
        ' Just to have a type name 'Range'
    End Class
    Private ReadOnly Property RangeType As Type = GetType(Range)
    Private ReadOnly Property StringType As Type = GetType(String)
    Private ReadOnly Property Excel As Object
    Public Sub New(excel As Object)

        Me.Excel = excel
        Functions = {
#INCLUDE
        }.ToList
    End Sub

    Public Overrides Function Generate(method As Method) As String

        Dim argf As String = String.Join(", ", method.Parameters.Select(Function(q) q.Formula))

        Dim fun As String = $"{method.Name}({argf})"

        Return fun

    End Function

End Class
