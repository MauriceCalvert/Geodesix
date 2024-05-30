Imports System.Text

Public Class GeneratorHelp
    Inherits Generator

    Public Sub New(masterfile As String,
                   templatefile As String,
                   outputfile As String)

        MyBase.New(masterfile, templatefile, outputfile)

    End Sub
    Public Overrides Sub Prologue()

    End Sub

    Public Overrides Sub Body(line As Line)

        Select Case line.Noun

            Case "Method"
                FlushMethod()
                Method = line

            Case "Parameter"
                Method.Children.Add(line)

        End Select
    End Sub
    Private Method As Line
    Private Methods As New List(Of Line)
    Public Overrides Sub Epilogue()

        FlushMethod()

        For m As Integer = 0 To Methods.Count - 1

            Dim method As Line = Methods(m)
            Dim children As Integer = method.Children.Count

            WriteLine($"<h1>{method("Name")} function</h1>")
            WriteLine("<h2>Syntax</h2>")
            Dim methodname As String = $"<b>{method("Name")}</b>"
            Write($"  <code>{methodname}(")

            For p As Integer = 0 To method.Children.Count - 1
                Dim parameter As Line = method.Children(p)
                Dim pcomma As String = If(p = children - 1, "", ", ")
                Write($"{parameter("Name")}){pcomma}")
            Next
            WriteLine(")</code>")
            WriteLine($"  The {methodname} function has these named arguments:")
            WriteLine("  <table><th><td>Name</td><td>Type</td><td>Description</td></th></th>")
            For p As Integer = 0 To method.Children.Count - 1
                Dim parameter As Line = method.Children(p)
                WriteLine($"    <tr><td>{parameter("Name")}</td><td>{parameter("Type")}</td><td>{parameter("Description")}</td></th></tr>")
            Next
            WriteLine("  </table>")

        Next

    End Sub
    Private Sub FlushMethod()

        If Method IsNot Nothing Then
            Methods.Add(Method)
            Method = Nothing
        End If

    End Sub
End Class
