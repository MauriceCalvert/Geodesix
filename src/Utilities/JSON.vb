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
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Module JSON
    Public Function DeserializeJSON(ByVal json As String) As Object
        ' https://stackoverflow.com/a/19140420/338101
        Return ToObject(JToken.Parse(json))
    End Function
    Private Function ToObject(ByVal token As JToken) As Object
        Select Case token.Type
            Case JTokenType.Object
                Return token.Children(Of JProperty)().ToDictionary(Function(prop) prop.Name, Function(prop) ToObject(prop.Value))
            Case JTokenType.Array
                Return token.[Select](AddressOf ToObject).ToList()
            Case Else
                Return (CType(token, JValue)).Value
        End Select
    End Function

End Module
