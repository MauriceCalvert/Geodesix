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
Imports System.Reflection
Imports AddinExpress.MSO
Imports Geodesics
Imports Utilities
Imports Seting
Partial Public Class GeodesiXEXL

    Private Sub adxExcelEvents_NewWorkbook(sender As Object, hostObj As Object) Handles adxExcelEvents.NewWorkbook

        CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

    End Sub
    Private Sub ExcelEvents_SheetActivate(sender As Object, sheet As Object) Handles adxExcelEvents.SheetActivate

        CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

        Try
            MapTaskPane?.Switch()

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Private Sub ExcelEvents_WorkbookBeforeClose(sender As Object, e As ADXHostBeforeActionEventArgs) Handles adxExcelEvents.WorkbookBeforeClose

        CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

        If e.HostObject IsNot Nothing Then
            Dim workbook As Object
            Try
                If CBool(e.HostObject.IsAddin) Then
                    Exit Sub
                End If

                workbook = e.HostObject

                CustomLog.Logger.Debug("Closing {0}", workbook.Name)

                Cache.Close(workbook)

            Catch ex As Exception
                HandleError(MethodBase.GetCurrentMethod().Name, ex)

            End Try
        End If
    End Sub
    Private Sub ExcelEvents_WorkbookBeforeSave(ByVal sender As Object, ByVal e As AddinExpress.MSO.ADXHostBeforeSaveEventArgs) _
        Handles adxExcelEvents.WorkbookBeforeSave

        CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

        If e.HostObject IsNot Nothing Then
            Dim workbook As Object = Nothing
            Try
                If CBool(e.HostObject.IsAddin) Then
                    Exit Sub
                End If
                workbook = e.HostObject

                Dim wbname As String = workbook.Name.ToString
                CustomLog.Logger.Debug("Saving {0}", wbname)

                Cache.Save(workbook)

            Catch ex As Exception
                HandleError(MethodBase.GetCurrentMethod().Name, ex)

            End Try
        End If

    End Sub
    Private Sub ExcelEvents_WorkbookOpen(ByVal sender As Object, ByVal hostObj As Object) _
        Handles adxExcelEvents.WorkbookOpen

        CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

        ' #20#

        If hostObj IsNot Nothing Then
            Dim workbook As Object
            Try
                If CBool(hostObj.IsAddin) Then
                    Exit Sub
                End If

                workbook = hostObj

                CustomLog.Logger.Debug("Opening {0}", workbook.Name)

                Cache.Load(workbook)

            Catch ex As Exception
                HandleError(MethodBase.GetCurrentMethod().Name, ex)

            End Try
        End If

    End Sub

    ' Ready for use one day...

    ' Don't, it happens everxy time a cell value changes
    'Private Sub ExcelEvents_SheetChange(sender As Object, sheet As Object, range As Object) Handles adxExcelEvents.SheetChange
    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)
    'End Sub

    'Private Sub ExcelEvents_NewWorkbook(sender As Object, hostObj As Object) Handles adxExcelEvents.NewWorkbook

    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

    'End Sub    'Private Sub ExcelEvents_WindowActivate(sender As Object, excel As Object, window As Object) Handles adxExcelEvents.WindowActivate

    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

    'End Sub

    'Private Sub ExcelEvents_WindowDeactivate(sender As Object, hostObj As Object, window As Object) Handles adxExcelEvents.WindowDeactivate

    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

    'End Sub

    'Private Sub ExcelEvents_WorkbookActivate(sender As Object, hostObj As Object) Handles adxExcelEvents.WorkbookActivate

    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

    'End Sub
    'Private Sub ExcelEvents_WorkbookNewSheet(sender As Object, workbook As Object, sheet As Object) Handles adxExcelEvents.WorkbookNewSheet

    '    CustomLog.Logger.Debug(MethodBase.GetCurrentMethod().Name)

    'End Sub
End Class
