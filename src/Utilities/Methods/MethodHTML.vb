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
Imports System.Text
Partial Class Method

    Public Function HTML() As String

        Dim sb As New StringBuilder

        sb.Append("<h1>")
        sb.Append(Name)
        sb.AppendLine(" Function</h1>")

        sb.AppendLine("<h2>Syntax</h2>")

        sb.Append("<div class='syntax'>")
        sb.Append(Name)
        sb.Append("(")
        sb.Append(String.Join(", ", Parameters.Select(Function(q) q.Name)))
        sb.AppendLine(")</div>")

        If Parameters.Any Then
            sb.AppendLine("<table><tr><th>Name</th><th>Type</th><th>Description</th></tr>")
            For Each p As Parameter In Parameters
                Dim ro As String = If(IsEmpty(p.DefaultValue), "Required", "Optional")
                sb.Append($"<tr><td>{p.Name}</td><td>{p.Type}</td><td>{ro} {p.Description}")
                If p.Choices.Any Then
                    sb.AppendLine("")
                    sb.Append("<div>Values allowed : ")
                    sb.Append(String.Join(" ", p.Choices))
                    sb.Append("</div>")
                End If
                sb.AppendLine("</td></tr>")
            Next
            sb.Append("</table>")
        End If

        Return sb.ToString

    End Function

End Class
