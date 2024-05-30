Imports Utilities
Public Class Template
    Inherits Row

    Public Sub New(generator As Generator, text As String)

        MyBase.New(generator)

        Dim words As String() = text.Split({Chr(9)}, StringSplitOptions.None).ToArray

        Debug.Assert(words(0).StartsWith("#"))

        Noun = words(0).Substring(1)
        Debug.Assert(Not isempty(Noun))

        For Each item As String In words.Skip(1).TakeWhile(Function(q) q <> "")

            Dim s As String() = item.Split(":"c)

            Items.Add(s(0))
            Type(s(0)) = s(1)
        Next

        Keys = Items.ToDictionary(Of String, String)(Function(q) q, Function(q) Nothing)

    End Sub
    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function
    Public ReadOnly Property Type As New Dictionary(Of String, String)
End Class
