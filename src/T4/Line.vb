Public Class Line
    Inherits Row

    Public Sub New(generator As Generator, text As String)

        MyBase.New(generator)

        Dim words As String() = text.Split({Chr(9)}, StringSplitOptions.None).ToArray
        Noun = words(0)

        Template = generator.Templates(Noun)

        Items = words.Skip(1).Take(Template.Items.Count).ToList

        Keys = New Dictionary(Of String, String)(Template.Keys)

        For i As Integer = 0 To Items.Count - 1
            Keys(Template.Items(i)) = Items(i)
        Next

    End Sub
    Public Function Arguments() As String

        Dim args As New List(Of String)

        For Each pair As KeyValuePair(Of String, String) In Keys

            Dim key As String = pair.Key
            Dim value As String = pair.Value
            Dim type As String = Template.Type(key)

            Select Case type

                Case "String"
                    args.Add($"""{value}""")

                Case "MinMax", "List"
                    If value = "" Then
                        args.Add("Nothing")
                    Else
                        args.Add($"{value}")
                    End If

                Case "Object"
                    If value = "" Then
                        args.Add("Nothing")
                    Else
                        Dim junk As Double
                        If Double.TryParse(value, junk) OrElse value.Contains("(") Then
                            args.Add(value)
                        Else
                            args.Add($"""{value}""")
                        End If
                    End If

                Case "Type"
                    If value = "" Then
                        args.Add("Nothing")
                    Else
                        args.Add($"GetType({value})")
                    End If

                Case Else
                    args.Add(value)
            End Select
        Next
        Return String.Join(", ", args)

    End Function
    Public Children As New List(Of Line)
    Private _Template As Template
    Public Property Template As Template
        Get
            Return _Template
        End Get
        Friend Set(value As Template)
            _Template = value
        End Set
    End Property
    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function
End Class
