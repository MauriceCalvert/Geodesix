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
Imports System.Windows.Forms
Imports Newtonsoft.Json.Linq
Imports Utilities.GeodesixException_
Public Module LoadTree_

    Public Sub LoadTree(root As JToken, treeview As TreeView)

        Try
            treeview.BeginUpdate()
            treeview.Nodes.Clear()
            AddTokenNodes(root, "Root", treeview.Nodes)

        Finally
            treeview.ExpandAll()
            If treeview.Nodes.Count > 0 Then
                treeview.Nodes(0).EnsureVisible()
            End If
            treeview.EndUpdate()
        End Try

    End Sub
    ' Reminder
    ' JToken      - abstract base class     
    ' 	JContainer  - abstract base class of JTokens that can contain other JTokens
    ' 		JArray      - represents a JSON array (contains an ordered list of JTokens)
    ' 		JObject     - represents a JSON object (contains a collection of JProperties)
    ' 		JProperty   - represents a JSON property (a name/JToken pair inside a JObject)
    ' 	JValue      - represents a primitive JSON value (string, number, boolean, null)

    Public Sub AddObjectNodes(token As JToken, name As String, parent As TreeNodeCollection)

        Dim obj As JObject = DirectCast(token, JObject)
        Dim node As New TreeNode($"{name}")
        node.Tag = token
        parent.Add(node)

        For Each prop As JProperty In obj.Properties()
            AddTokenNodes(prop.Value, prop.Name, node.Nodes)
        Next

    End Sub
    Private Sub AddArrayNodes(token As JArray, name As String, parent As TreeNodeCollection)

        Dim node As New TreeNode($"{name}")
        node.Tag = token
        parent.Add(node)

        For i As Integer = 0 To token.Count - 1

            Dim item As JToken = token(i)
            AddTokenNodes(item, String.Format("[{0}]", i), node.Nodes)

        Next
    End Sub
    Private Sub AddTokenNodes(token As JToken, name As String, parent As TreeNodeCollection)

        If TypeOf token Is JValue Then

            Dim item As JValue = CType(token, JValue)
            Dim node As New TreeNode($"{name}: {item.Value}")
            node.Tag = token
            parent.Add(node)

        ElseIf TypeOf token Is JArray Then

            Dim item As JArray = DirectCast(token, JArray)
            AddArrayNodes(item, name, parent)

        ElseIf TypeOf token Is JObject Then

            Dim item As JObject = DirectCast(token, JObject)
            AddObjectNodes(item, name, parent)

        ElseIf TypeOf token Is JProperty Then

            Dim item As JProperty = DirectCast(token, JProperty)
            AddObjectNodes(item, name, parent)

        Else
            Throw New GeodesixException($"Unknown token type {token.GetType.Name}")
        End If
    End Sub
End Module
