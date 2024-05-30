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
Imports System.Data
Imports System.Linq
Imports Utilities
Public Module Clustered_

    Public Function Clustered(datatable As DataTable, separation As Double,
                              Optional progress As Action(Of String) = Nothing) As DataTable

        Dim colnames As List(Of String) = datatable.Columns.Cast(Of DataColumn).Select(Function(q) q.ColumnName).ToList


        If Not (colnames.HasAny("latitude") AndAlso
                colnames.HasAny("longitude")) Then

            Dim message As String = "Cluster requires a 'Latitude' and a 'Longitude' column"
            If progress Is Nothing Then
                Throw New GeodesixException(message)
            Else
                ShowWarning(message)
            End If
            Return datatable
        End If

        Dim result As DataTable = datatable.Clone ' Without the rows

        Dim candidates As Dictionary(Of Integer, DataRow) =
            datatable.
            AsEnumerable.
            ToDictionary(Function(k) k.Field(Of Integer)(Importer.UNIQUEKEYNAME),
                         Function(v) v)


        Do While candidates.Any

            progress?($"Clustering {candidates.Count}")

            Dim startrow As DataRow = candidates.Values.First
            Dim latx As Double = startrow.Field(Of Double)("latitude")
            Dim lngx As Double = startrow.Field(Of Double)("longitude")

            Dim cluster As List(Of DataRow) =
                candidates.
                Values.
                Where(Function(y)
                          Dim laty As Double = y.Field(Of Double)("latitude")
                          Dim lngy As Double = y.Field(Of Double)("longitude")
                          Return Haversine(latx, lngx, laty, lngy) < separation
                      End Function).
                ToList

            If cluster.Any Then

                ' If dataset has a 'duration' field of type TimeStamp, the winner is where he stayed longest.
                ' Otheriwse, the point closest to the centre of the cluster
                Dim duration As String = colnames.Where(Function(q) q.ToLower = "duration").SingleOrDefault
                If datatable.Columns(duration).DataType.Name <> "TimeSpan" Then ' wrong data type
                    duration = Nothing
                End If

                Dim winner As DataRow

                If IsEmpty(duration) Then

                    Dim centrelat As Double =
                    cluster.
                    Select(Function(q) q.Field(Of Double)("latitude")).
                    Average

                    Dim centrelng As Double =
                    cluster.
                    Select(Function(q) q.Field(Of Double)("longitude")).
                    Average

                    winner = cluster.
                             OrderBy(Function(q)
                                         Dim laty As Double = q.Field(Of Double)("latitude")
                                         Dim lngy As Double = q.Field(Of Double)("longitude")
                                         Return Haversine(centrelat, centrelng, laty, lngy)
                                     End Function).
                             First
                Else
                    winner = cluster.
                             OrderByDescending(Function(q) q.Field(Of TimeSpan)(duration)).
                             First
                End If

                result.ImportRow(winner)

                For Each loser As DataRow In cluster
                    candidates.Remove(loser.Field(Of Integer)(Importer.UNIQUEKEYNAME))
                Next
            Else
                result.ImportRow(startrow)
                candidates.Remove(startrow.Field(Of Integer)(Importer.UNIQUEKEYNAME))
            End If
        Loop

        Return result

    End Function

End Module
