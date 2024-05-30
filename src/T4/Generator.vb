Imports System.IO
Imports System.Linq
Imports Utilities
Public MustInherit Class Generator

    MustOverride Sub Prologue()
    MustOverride Sub Body(line As Line)
    MustOverride Sub Epilogue()


    Public ReadOnly Property TemplateFile As String
    Public ReadOnly Property MasterFile As String
    Public ReadOnly Property OutputFile As String
    Public ReadOnly Property Writer As StreamWriter
    Public ReadOnly Property Templates As New Dictionary(Of String, Template)
    Public Sub New(masterfile As String,
                   templatefile As String,
                   outputfile As String)

        Me.TemplateFile = templatefile
        Me.MasterFile = masterfile
        Me.OutputFile = outputfile

    End Sub
    Public Function ObjArray(s As String) As Object()

        If isempty(s) Then
            Return {}
        End If

        Return {
                s.
                Replace("{", "").
                Replace("}", "").
                Split(","c).
                Select(Function(q) q).
                ToArray
        }
    End Function
    Public Sub Run()

        Using output As New StreamWriter(OutputFile)

            _Writer = output

            Prologue()

            Using master As New StreamReader(MasterFile)

                Do While Not master.EndOfStream

                    Dim masterline As String = master.ReadLine

                    If masterline.StartsWith("#INCLUDE") Then

                        Dim template As New StreamReader(TemplateFile)

                        Do While Not template.EndOfStream

                            Dim text As String = template.ReadLine

                            If isempty(text) Then
                                Continue Do
                            End If

                            If text.StartsWith("#") Then
                                Dim newtemplate As New Template(Me, text)
                                Templates.Add(newtemplate.Noun, newtemplate)
                            Else
                                Dim line As New Line(Me, text)
                                line.Template = Templates(line.Noun)
                                Body(line)
                            End If

                        Loop
                        Epilogue()
                    Else
                        output.WriteLine(masterline)
                    End If
                Loop
            End Using

            output.Close()

        End Using

    End Sub
    Protected Sub Write(text As String)

        Dim spaces As Integer = text.TakeWhile(Function(q) Char.IsWhiteSpace(q)).Count

        Do While text.IndexOf("  ") >= 0
            text = Replace(text, "  ", " ")
        Loop

        Do While text.IndexOf(Chr(13) & Chr(10)) >= 0
            text = Replace(text, Chr(13) & Chr(10), " ")
        Loop

        Writer.Write(New String(" "c, spaces) & text)

    End Sub
    Protected Sub WriteLine(text As String)

        Dim spaces As Integer = text.TakeWhile(Function(q) Char.IsWhiteSpace(q)).Count

        Do While text.IndexOf("  ") >= 0
            text = Replace(text, "  ", " ")
        Loop

        Do While text.IndexOf(Chr(13) & Chr(10)) >= 0
            text = Replace(text, Chr(13) & Chr(10), " ")
        Loop

        Writer.WriteLine(New String(" "c, spaces) & text)

    End Sub

End Class
