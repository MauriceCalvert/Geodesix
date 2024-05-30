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
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms
Imports JsonTransformer
Imports Seting
Imports Utilities

Public Class Importer

    Public Const UNIQUEKEYNAME As String = "_ID_"

    Private Property Extension As String
    Private Property Filter As String
    Private Property GeodesixEXL As GeodesiXEXL
    Private Property Excel As Object

    Public Sub New(geodesixexl As GeodesiXEXL, extension As String, filter As String)

        InitializeComponent()

        Me.GeodesixEXL = geodesixexl
        Me.Excel = geodesixexl.Excel
        Me.Extension = extension
        Me.Filter = filter

    End Sub
    Private Sub Importer_Load(sender As Object, e As EventArgs) Handles Me.Load

        Template = New Template
        cmbTemplate.DataSource = {"<new>"}.Concat(Template.TemplateNames).ToList
        cmbTemplate.Text = Settings.TemplateName
        If IsEmpty(cmbTemplate.Text) Then
            cmbTemplate.Text = "<new>"
        End If
        txtFolder.Text = Coalesce(Settings.ImportFolder, GetMyDocuments)
        txtFile.Text = Settings.ImportFile

    End Sub
    Private Sub btnBrowseFile_Click(sender As Object, e As EventArgs) Handles btnBrowseFile.Click

        Try
            rbSingle.Checked = True
            If BrowseForFile(txtFile.Text, Extension, Filter,
                             Path.GetDirectoryName(Coalesce(Settings.ImportFile, GetMyDocuments))) Then
                Settings.ImportFile = txtFile.Text
            End If

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Private Sub btnBrowseFolder_Click(sender As Object, e As EventArgs) Handles btnBrowseFolder.Click

        Try
            rbFolder.Checked = True

            Dim fbd As New FolderBrowserDialog
            With fbd
                .SelectedPath = Coalesce(Settings.ImportFolder, GetExecutingPath)
                Dim result As DialogResult = ShowCommonDialog(fbd)
                If result <> DialogResult.OK Then
                    Exit Sub
                End If
                Settings.ImportFolder = .SelectedPath
                txtFolder.Text = .SelectedPath
            End With

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        DialogResult = DialogResult.Cancel
        Close()

    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Try
            Template.LoadTemplate(cmbTemplate.Text)

            ShowFormDialog(New TemplateEditor(GeodesixEXL, Template, txtFile.Text))

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click

        Try
            Template.LoadTemplate(cmbTemplate.Text)

            Dim files As IEnumerable(Of String)

            If rbSingle.Checked Then
                files = {txtFile.Text}
            Else
                files = Directory.EnumerateFiles(txtFolder.Text, cmbFilter.Text, SearchOption.AllDirectories)
            End If

            If Not files.Any Then
                ShowError("No files found")
                Exit Sub
            End If

            Dim clustersizemetres As Double = 0
            If chkCluster.Checked Then
                If Not Double.TryParse(txtClusterSize.Text, clustersizemetres) Then
                    ShowError("Invalid nunber ")
                    txtClusterSize.Focus()
                    Exit Sub
                End If
            End If

            Loader = New Loader(Me, Excel, Template, clustersizemetres * 1000)

            Loader.Load(files)

            Settings.TemplateName = cmbTemplate.Text

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Private Sub btnSamples_Click(sender As Object, e As EventArgs) Handles btnSamples.Click

        Try
            rbSingle.Checked = True
            If BrowseForFile(txtFile.Text, Extension, "JSON files (*.json)|*.json|XML files (*.xml)|*.xml",
                             Path.Combine(GetExecutingPath, "help")) Then
                Settings.ImportFile = txtFile.Text
            End If

        Catch ex As Exception
            HandleError(MethodBase.GetCurrentMethod().Name, ex)

        End Try

    End Sub
    Private Sub Help_Requested(sender As Object, hlpevent As HelpEventArgs) Handles Me.HelpRequested

        Try
            hlpevent.Handled = True
            GeodesixEXL?.ShowHelp("importer.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub
    Private Sub Help_ButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked

        Try
            e.Cancel = True
            GeodesixEXL?.ShowHelp("importer.html")

        Catch ex As Exception
            ShowError(ex.Message)
        End Try

    End Sub

    Private Property Loader As Loader
    Private Property Template As Template
    Private Sub txtFile_TextChanged(sender As Object, e As EventArgs) Handles txtFile.TextChanged

        rbSingle.Checked = True
        Settings.ImportFile = txtFile.Text

    End Sub

    Private Sub txtFolder_TextChanged(sender As Object, e As EventArgs) Handles txtFolder.TextChanged

        rbFolder.Checked = True
        Settings.ImportFolder = txtFolder.Text

    End Sub

End Class