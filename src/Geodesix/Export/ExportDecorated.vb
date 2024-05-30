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
Imports System.Collections.Generic
Imports System.IO
Imports Utilities
Public Class ExportDecorated
    Inherits ExportBasic
    Private Enum Modes
        Journey = 1
        Visit = 2
    End Enum
    Private Property PreviousLatitude As Double = 0
    Private Property PreviousLongitude As Double = 0
    Private Property Mode As Modes
    Private DrawLines As New List(Of String)
    Private DrawArrows As New List(Of String)
    Private DrawIcons As New List(Of String)
    Private DrawDistances As New List(Of String)
    Public Sub New(ByRef excel As Object, sheetname As String, ByRef target As String, template As List(Of String))
        MyBase.New(excel, sheetname, target, template)
    End Sub
    Protected Overrides Sub Prologue()

        MyBase.Prologue()

        If HaveAllKeys({"latitude", "longitude"}) Then
            Mode = Mode Or Modes.Visit
        End If

        If HaveAllKeys({"latitudea", "longitudea", "latitudeb", "longitudeb"}) Then
            Mode = Mode Or Modes.Journey
        End If

        AddRow("/* drawDecorations */")
        AddRow("function drawDecorations() {")
        AddRows(ReadFileToList(Path.Combine(GetExecutingPath, "templates", "goverlay.js")))

    End Sub
    Protected Overrides Sub Body(final As Boolean)

        If Variable("icon").Value = "free" Then
            Dim html As String = JSQuote(Variable("title").Value)
            Dim label As String = $"legend{Unique()}"
            DrawIcons.Add($"
                const {label} = document.createElement('span');
                {label}.innerHTML = {html};
                document.body.appendChild({label});
            ")
            Exit Sub
        End If

        If (Mode And Modes.Visit) <> 0 Then
            BodyVisit(final)
        End If

        If (Mode And Modes.Journey) <> 0 Then
            BodyJourney(final)
        End If

    End Sub
    Private Sub BodyJourney(final As Boolean)

        Dim latitudea As Double = 0
        Dim longitudea As Double = 0
        Dim latitudeb As Double = 0
        Dim longitudeb As Double = 0

        Double.TryParse(Variable("latitudea").Value, latitudea)
        Double.TryParse(Variable("longitudea").Value, longitudea)
        Double.TryParse(Variable("latitudeb").Value, latitudeb)
        Double.TryParse(Variable("longitudeb").Value, longitudeb)

        If latitudea = 0 OrElse longitudea = 0 OrElse
            latitudeb = 0 OrElse longitudeb = 0 Then ' Nothing to draw
            Exit Sub
        End If

        EmitLine(latitudea, longitudea, latitudeb, longitudeb)

        EmitArrow(latitudea, longitudea, latitudeb, longitudeb)

        EmitIcon(latitudea, longitudea)

        EmitIcon(latitudeb, longitudeb)

    End Sub
    Private Sub BodyVisit(final As Boolean)

        Dim latitude As Double = 0
        Dim longitude As Double = 0

        Double.TryParse(Variable("latitude").Value, latitude)
        Double.TryParse(Variable("longitude").Value, longitude)

        If latitude = 0 OrElse longitude = 0 Then ' Nothing to draw
            Exit Sub
        End If

        If PreviousLatitude <> 0 AndAlso PreviousLongitude <> 0 Then

            EmitLine(PreviousLatitude, PreviousLongitude, latitude, longitude)

            EmitArrow(PreviousLatitude, PreviousLongitude, latitude, longitude)

        End If

        EmitIcon(latitude, longitude)

        PreviousLatitude = latitude
        PreviousLongitude = longitude

    End Sub
    Protected Overrides Sub Epilogue()

        ' Draw things in the right Z-order so that it all looks nice

        For Each line As String In DrawLines
            AddRow(line)
        Next
        For Each arrow As String In DrawArrows
            AddRow(arrow)
        Next
        For Each distance As String In DrawDistances
            AddRow(distance)
        Next
        For Each icon As String In DrawIcons
            AddRow(icon)
        Next

        AddRow("}; /* end drawDecorations */")
        AddRow("drawDecorations();")
        AddRow("zoomToContent();")

        FlushJavascript("decorations")

        MyBase.Epilogue()

    End Sub
    Private Sub EmitArrow(latitudea As Double, longitudea As Double, latitudeb As Double, longitudeb As Double)

        Dim arrow As String = Variable("arrow").Value
        If IsEmpty(arrow) Then
            Exit Sub
        End If
        Dim size As Double = CDbl(Variable("arrowSize").Value)
        If size <= 0 Then
            Exit Sub
        End If

        Dim linelength As Double = Haversine(latitudea, longitudea, latitudeb, longitudeb)
        Dim inverse As VincentyInverseResult = VincentyInverse(latitudea, longitudea, latitudeb, longitudeb)
        Dim bearing As Double = inverse.InitialBearing
        ' Put the arrow 20% of the way along the line
        Dim direct As VincentyDirectResult = VincentyDirect(latitudea, longitudea, bearing, linelength / 5)

        Dim arrowlatlng As String = $"{{lat:{direct.Latitude},lng:{direct.Longitude}}}"
        Dim arrowcolor As String = Variable("arrowColor").Value

        DrawArrows.Add($"drawGoverlay(
            {Unique()},
            'arrow',
            {arrowlatlng}, 
            '{arrow}',
            '',
            {size},
            '{arrowcolor}',
            'center',
            {bearing},
            '{Variable("symbols").Value}'
        );")

    End Sub
    Private Sub EmitDistance(latitudea As Double, longitudea As Double, latitudeb As Double, longitudeb As Double)

        Dim distance As String = Variable("distance").Value
        If IsEmpty(distance) Then
            Exit Sub
        End If

        Dim lat As Double = (latitudeb + latitudea) / 2
        Dim lng As Double = (longitudeb + longitudea) / 2

        Dim distancelatlng As String = $"{{lat:{lat},lng:{lng}}}"

        DrawDistances.Add($"drawGoverlay(
            {Unique()},
            'distance',
            {distancelatlng}, 
            '{distance}',
            '',
            0,
            '',
            'center',
            0,
            ''
        );")


    End Sub
    Private Sub EmitIcon(latitude As Double, longitude As Double)

        Dim icon As String = Variable("icon").Value
        If icon = "" Then
            Exit Sub
        End If
        Dim size As Double = CDbl(Variable("iconSize").Value)
        If size <= 0 Then
            Exit Sub
        End If
        Dim align As String = JSQuote(Variable("align").Value)
        Dim title As String = Variable("title").Value
        Dim color As String = JSQuote(Variable("iconColor").Value)
        Dim symbols As String = JSQuote(Variable("symbols").Value)
        Dim latlng As String = $"{{lat:{latitude},lng:{longitude}}}"

        ' If the title is plain titletext, split it into lines
        Dim titletext As String = Variable("title").Value
        If titletext.TrimStart({" "c}).BeginsWith("<") Then ' HTML, quote as-is
            titletext = JSQuote(titletext)
        Else
            titletext = JSQuote(TextRectangle(titletext))
        End If

        Select Case True

            Case icon.BeginsWith("$") ' material icon

                DrawIcons.Add($"drawGoverlay(
                    {Unique()},
                    'circle',
                    {latlng}, 
                    '{icon.Substring(1)}',
                    {titletext},
                    {size},
                    {color},
                    {align},
                    0,
                    {symbols}
                );")

            Case icon.IndexOf("://", 1, Math.Min(10, icon.Length - 1)) > 0 ' old-fashioned icon file://, http://, https://, etc.
                DrawIcons.Add($"drawMarker(
                    {Unique()},
                    {latitude}, 
                    {longitude},
                    {JSQuote(icon)},
                    {titletext},
                    {size}
                );")

            Case True ' Text or HTML

                ' If the icon is plain titletext, split it into lines
                Dim icontext As String = Variable("icon").Value
                If icontext.TrimStart({" "c}).BeginsWith("<") Then ' HTML, quote as-is
                    icontext = JSQuote(icontext)
                Else
                    icontext = JSQuote(TextRectangle(icontext))
                End If

                DrawIcons.Add($"drawGoverlay(
                    {Unique()},
                    'box',
                    {latlng}, 
                    {icontext},
                    {titletext},
                    {size},
                    {color},
                    {align},
                    0,
                    {symbols}
                );")

        End Select

    End Sub
    Private Sub EmitLine(latitudea As Double, longitudea As Double, latitudeb As Double, longitudeb As Double)

        If CInt(Variable("strokeWeight").Value) <= 0 OrElse CInt(Variable("strokeOpacity").Value) <= 0 Then
            Exit Sub
        End If

        ' If the line title is plain titletext, split it into lines
        Dim titletext As String = Variable("lineTitle").Value
        If titletext.TrimStart({" "c}).BeginsWith("<") Then ' HTML, quote as-is
            titletext = JSQuote(titletext)
        Else
            titletext = JSQuote(TextRectangle(titletext))
        End If

        DrawLines.Add($"drawLine(
            {Unique()},
            {latitudea},{longitudea}, 
            {latitudeb},{longitudeb}, 
            {titletext},
            '{Variable("strokeColor").Value}',
            {Variable("strokeOpacity").Value},
            {Variable("strokeWeight").Value});")

        EmitDistance(latitudea, longitudea, latitudeb, longitudeb)

    End Sub
End Class
