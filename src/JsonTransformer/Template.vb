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
Imports System.IO
Imports System.Xml
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Seting
Imports Utilities

Public Class Template

    Public Sub New()

        _Schema = New Schema(Me)
        TemplateName = Coalesce(Settings.TemplateName, "<new>")

    End Sub
    Public Function BreakPath() As String

        Dim nodes As List(Of JToken) = Root.OfType(Of JContainer).Descendants.ToList
        Dim containers As List(Of JContainer) = nodes.OfType(Of JContainer).ToList
        Dim branches As List(Of JContainer) = containers.Where(Function(q) q.HasValues).ToList
        Dim arrays As List(Of JArray) = nodes.OfType(Of JArray).ToList
        Dim objects As List(Of JObject) = nodes.OfType(Of JObject).ToList
        Dim properties As List(Of JProperty) = nodes.OfType(Of JProperty).ToList
        Dim values As List(Of JValue) = nodes.OfType(Of JValue).ToList

        ' Get all the leaves' paths
        Dim leaves As Dictionary(Of String, Rule) =
            Schema.
            Rules.
            OfType(Of RulePath).
            ToDictionary(Of String, Rule) _
            (Function(key) key.Source, Function(val) val)

        ' Get a list of all the ancestors.
        ' Each ancestor is everthing above the leaves, up to the root 
        ' Do it in steps to make it more understandable

        ' Start with all the branches of the tree, without the leaves (JValues)
        Dim ancestors As IEnumerable(Of String) =
            Root.
            OfType(Of JContainer).
            Descendants.
            OfType(Of JContainer).
            Select(Function(q) WildPath(q.Path))

        ' Get those that are ancestors of the leaves
        ancestors =
            ancestors.
            Where(Function(ancestor) leaves.
                              Keys.
                              Any(Function(leaf) leaf.BeginsWith(ancestor))).
            Distinct

        If Not ancestors.Any Then
            Return ""
        End If

        ' Sort them by the number of dots in the name
        ' Think:
        '   AAA.BBB.CCC
        ' comes *after*
        '   A.B
        Dim candidates() As String =
            ancestors.
            OrderBy(Function(q) q.Count(Function(x) x = "."c)).
            ToArray

        ' Finally, find the ancestor that is a prefix of all the subsequent, deeper paths.
        ' There may well be none, then the answer is ""
        Dim result As String = ""

        For c As Integer = 0 To candidates.Count - 2

            Dim path As String = candidates(c)

            If candidates.
               Skip(c + 1).
               All(Function(longer) longer.BeginsWith(path)) Then

                result = path
            Else
                Exit For
            End If
        Next

        Return result

    End Function
    Public ReadOnly Property FileName As String
    Public ReadOnly Property Imported As IEnumerable(Of Type)
        Get
            Dim standard As Type() = {
                Type.GetType("System.Math"),
                Type.GetType("System.DateTime")
            }
            Dim custom As IEnumerable(Of Type) =
                Schema.
                Rules.
                OfType(Of RuleImport).
                Select(Function(q) Type.GetType(q.Name))

            Return standard.Concat(custom).Distinct
        End Get
    End Property
    Public Sub LoadFile(filename As String)

        Dim content As String = File.ReadAllText(filename)

        Dim ft As String = Path.GetExtension(filename).ToLower

        Dim json As String

        If (ft.Length = 3 AndAlso Right(ft, 2) = "ml") OrElse
            content.Substring(0, 10).ToLower.BeginsWith("<?xml") Then

            Dim doc As New XmlDocument
            doc.Load(filename)
            json = JsonConvert.SerializeXmlNode(doc)
        Else
            json = content
            If json.TrimStart().BeginsWith("[") Then ' Root is an array
                json = "{ ""Array"": " & content & "}"
            End If
        End If

        _Root = JToken.Parse(json)

        _FileName = filename

    End Sub
    Public Sub LoadTemplate(name As String)

        If IsEmpty(name) OrElse name = "<new>" Then
            _TemplateName = name
            Exit Sub
        End If

        _TemplateName = Path.GetFileNameWithoutExtension(name)
        Dim templatefile As String = TemplatePath(TemplateName)

        Schema.Clear()

        For Each line As String In File.
                                   ReadAllLines(templatefile).
                                   Where(Function(q) Not q.StartsWith("#") AndAlso
                                                     Not IsEmpty(q))

            ' Examples:
            ' Class          Name      Type   Output Source                                      Sample
            ' 0              1         2      3      4                                           5 
            ' RulePath       latitudee Long   True   timelineObjects[*].startLocation.latitudeE7 488784870
            ' RuleExpression latitude  Double True   latitudee / 10000000.0
            ' RuleImport     Geometry
            ' RuleSkip       skipname  Double False  latitudee / 10000000.0
            ' RuleSort       StartTimeStamp

            line = line & vbTab & vbTab & vbTab & vbTab & vbTab ' Pad for missing values

            Dim tokens() As String = line.Split({CChar(vbTab)})
            Dim ruleclass As String = tokens(0)
            Dim rulename As String = tokens(1)
            Dim ruletype As String = tokens(2)
            Dim ruleoutput As Boolean
            Boolean.TryParse(tokens(3), ruleoutput)
            Dim rulesource As String = tokens(4)
            Dim rulevalue As Object = tokens(5)

            ' Adjust value type, if possible
            If Not IsEmpty(rulevalue) Then
                Try
                    Dim type As Type = Nothing
                    If TryGetType(ruletype, type) Then
                        rulevalue = Convert.ChangeType(rulevalue, type)
                    End If
                Catch careless As Exception
                End Try
            End If

            Select Case ruleclass

                Case "Notes"
                    Dim notes64 As String = line.Substring(6)
                    Try
                        NotesRTF = JsonConvert.DeserializeObject(Of String)(notes64)
                    Catch ex As Exception
                    End Try
                    Dim xx As Integer = 0

                Case "RuleExpression"
                    Schema.AddExpression(rulename, ruletype, ruleoutput, rulesource)

                Case "RuleImport"
                    Schema.AddImport(rulename)

                Case "RuleSort"
                    Schema.AddSort(rulename)

                Case "RulePath"
                    Schema.AddPath(rulename, ruletype, ruleoutput, rulesource, rulevalue)

                Case "RuleSkip"
                    Dim sr As RuleSkip = Schema.AddSkip(rulename, ruletype, rulesource)

                Case Else
                    Throw New GeodesixException($"Invalid rule type {ruleclass}")
            End Select

        Next line

    End Sub
    Public Function NewIdentifier() As String

        ' Get a unique name for a new variable. Make it at least 3 characters
        Dim result As String
        Do
            result = IntToName(AlphabetLength * AlphabetLength + Unique())
        Loop Until Not Schema.Rules.Any(Function(q) result = q.Name)

        Return result

    End Function
    Public Property NotesRTF As String

    Public Sub ResetValues()

        For Each rule As RulePath In Schema.Rules.OfType(Of RulePath)
            rule.Reset()
        Next

    End Sub
    Public ReadOnly Property Root As JToken
    Public ReadOnly Property Schema As Schema
    Public Function Sample(sampled As Boolean) As JToken

        _Sampler = New Sampler(Root, sampled)
        Return Sampler.Result

    End Function
    Private ReadOnly Property Sampler As Sampler
    Public Sub Save(templatename As String)

        If Schema.Rules.Any(Function(q) Not IsEmpty(q.Message)) Then
            Throw New GeodesixException("Can't save with errors")
        End If

        If Not Directory.Exists(TemplateFolder) Then
            Directory.CreateDirectory(TemplateFolder)
        End If

        Using sw As New StreamWriter(TemplatePath(templatename))

            sw.WriteLine($"# {Path.GetFileNameWithoutExtension(templatename)} - This is a tab-delimited file")
            sw.WriteLine($"# Rule type{vbTab}Name{vbTab}Source{vbTab}")

            If NotesRTF IsNot Nothing Then
                sw.WriteLine("Notes" & vbTab & JsonConvert.SerializeObject(NotesRTF))
            End If

            For Each item As Rule In Schema.Rules

                sw.Write(item.GetType.Name)
                sw.Write(vbTab)

                sw.Write(item.Name)
                sw.Write(vbTab)

                If TypeOf item Is RuleValue Then
                    Dim rv As RuleValue = DirectCast(item, RuleValue)
                    sw.Write(rv.Type)
                    sw.Write(vbTab)
                    sw.Write(rv.Output.ToString)
                    sw.Write(vbTab)
                Else
                    sw.Write(vbTab)
                    sw.Write(vbTab)
                End If

                Select Case item.GetType

                    Case GetType(RuleExpression)
                        sw.Write(DirectCast(item, RuleExpression).Source)

                    Case GetType(RuleSkip)
                        sw.Write(DirectCast(item, RuleSkip).Source)

                    Case GetType(RuleImport), GetType(RuleSort)

                    Case GetType(RulePath)
                        Dim rule As RulePath = DirectCast(item, RulePath)
                        sw.Write(rule.Source)
                        sw.Write(vbTab)
                        sw.Write(rule.Value)

                    Case Else
                        Throw New GeodesixException($"Invalid rule type {item.GetType.Name}")
                End Select

                sw.WriteLine("")

            Next
            sw.Close()
        End Using
        _TemplateName = Path.GetFileNameWithoutExtension(templatename)

    End Sub
    Public Shared ReadOnly Property TemplateFolder As String = Path.Combine(DataFolder, "Templates")
    Public ReadOnly Property TemplateName As String
    Public Shared Function TemplateNames() As List(Of String)

        Dim tf As String = TemplateFolder

        If Not Directory.Exists(tf) Then ' Copy templates from install templates to datafolder/templates

            Directory.CreateDirectory(tf)

            For Each f As String In Directory.
                                    GetFiles(Path.Combine(GetExecutingPath, "Templates"), "*.template*")

                File.Copy(f, Path.Combine(tf, Path.GetFileName(f)))
            Next
        End If

        Return _
            Directory.
            EnumerateFiles(TemplateFolder, "*.template").
            Select(Function(q) Path.GetFileNameWithoutExtension(q)).
            ToList
    End Function
    Public Function TemplatePath(templatefile As String) As String

        Return Path.Combine(TemplateFolder, templatefile & ".template")

    End Function
End Class
