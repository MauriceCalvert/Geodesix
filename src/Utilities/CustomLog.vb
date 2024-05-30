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
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports NLog
Imports NLog.Common
Imports NLog.Config
Imports NLog.Targets

Public Class CustomLog
    Public Shared ReadOnly Property Debugging As Boolean
    Private Shared _Logger As Logger = Nothing
    Public Shared ReadOnly Property Logger As Logger
        Get
            If _Logger Is Nothing Then ' We were invoked unexpectedly
                StartLogger(True)
                _Logger.Warn("CustomLog.Logger StartLogger was not invoked!")
            End If
            Return _Logger
        End Get
    End Property
    Public Shared Sub StartLogger(debugging As Boolean)
        Try
            _Debugging = debugging

            LogManager.Flush()
            Dim logconfig As New LoggingConfiguration

            Dim filetarget As New FileTarget
            Dim procs() As Diagnostics.Process = Diagnostics.Process.GetProcesses
            Dim troubleshooting As Boolean = False

            For Each proc As System.Diagnostics.Process In procs
                If proc.ProcessName.BeginsWith("TroubleShooter") Then
                    troubleshooting = True
                    Exit For
                End If
            Next
            If Not troubleshooting Then
                filetarget.KeepFileOpen = True
            End If

            logconfig.AddTarget("file", filetarget)
            Dim logpath As String = GetMyDocuments() & "\GeodesiX"
            If Not Directory.Exists(logpath) Then
                Directory.CreateDirectory(logpath)
            End If
            Dim logfile As String = logpath & "\GeodesiX.log"
            If File.Exists(logfile) Then
                Dim fi As FileInfo
                fi = My.Computer.FileSystem.GetFileInfo(logfile)
                If (DateTime.Now - fi.LastWriteTime) > New TimeSpan(2, 0, 0, 0) Then ' > 48 hours
                    Try
                        My.Computer.FileSystem.DeleteFile(logfile)
                    Catch ex As Exception
                    End Try
                End If
            End If
            filetarget.FileName = logfile
            filetarget.Layout = "${date:format=yyyy-MM-dd HH\:mm\:ss.fff} ${message}"

            Dim ll As LogLevel = If(debugging, LogLevel.Debug, LogLevel.Error)
            Dim filerule As New LoggingRule("*", ll, filetarget)
            logconfig.LoggingRules.Add(filerule)

#If DEBUG Then
            LogManager.
                Setup.
                SetupExtensions(Sub(x)
                                    x.RegisterTarget("DebuggerNoNewLine", GetType(DebuggerNoNewLineTarget))
                                End Sub)
            Dim debugtarget As New DebuggerNoNewLineTarget("DebuggerNoNewLine")
            Dim debugrule As New LoggingRule("*", ll, debugtarget)
            debugtarget.Layout = "${date:format=HH\:mm\:ss.fff} ${message}"
            logconfig.AddTarget("DebuggerNoNewLine", debugtarget)
            logconfig.LoggingRules.Add(debugrule)
#End If
            LogManager.Configuration = logconfig
            _Logger = LogManager.GetLogger("GeodesiX")
            _Logger.Info("Loglevel {0}", ll)

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    'Public Shared Sub StopLogger()
    '    Dim ft As FileTarget = CType(LogManager.Configuration.FindTargetByName("file"), FileTarget)
    '    ft.KeepFileOpen = False
    '    LogManager.Flush()
    '    LogManager.DisableLogging()
    'End Sub
End Class

<Target("DebuggerNoNewLine")>
Public NotInheritable Class DebuggerNoNewLineTarget
    Inherits TargetWithLayout

    Public Sub New()
    End Sub

    Public Sub New(ByVal name As String)

        Me.New()
        MyBase.Name = name

    End Sub

    Protected Overrides Sub InitializeTarget()
        MyBase.InitializeTarget()

        If Not CustomLog.Debugging Then
            InternalLogger.Debug("{0}: System.Diagnostics.Debugger.IsLogging()==false. Output has been disabled.", Me)
        End If

    End Sub

    Protected Overrides Sub CloseTarget()

        MyBase.CloseTarget()

    End Sub

    Protected Overrides Sub Write(ByVal logEvent As LogEventInfo)
        If Debugger.IsLogging() Then
            Dim message As String = Me.Layout.Render(logEvent)
            Debug.WriteLine(message)
        End If
    End Sub
End Class

