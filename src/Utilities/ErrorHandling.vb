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
Public Module ErrorHandling_

    Private Once As Boolean = False
    Private LastError As DateTime = DateTime.Now

    Public Sub HandleError(ByVal doing As String, ByVal ex As Exception)

        CustomLog.Logger.Error(ex, "HandleError")

        If Once Then
            ' Throttle error displays
            If (DateTime.Now - LastError).TotalSeconds < 10 Then
                Exit Sub
            End If
        End If
        Once = True
        LastError = DateTime.Now

        If TypeOf ex Is GeodesixException Then
            ShowError(ex.Message)
            Exit Sub
        End If

        Dim text As String
        Dim ie As Exception = ex.InnerException

        Try
            text = ex.GetType.ToString & " " & doing & ": " & ex.Message

            Do While ie IsNot Nothing
                text = text & vbCR & "...caused by " & ie.Message
                ie = ie.InnerException
            Loop

            text = text & vbCR & "Stack trace:" & vbCR & ex.StackTrace.ToString

            Dim errorform As FatalError
            errorform = New FatalError(text)
            errorform.ShowDialog()

        Catch WTF As Exception
            ' Nothing more we can do
        End Try
    End Sub
End Module
