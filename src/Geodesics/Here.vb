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
Imports System.Device.Location
Imports Utilities

Public Class Here
    Private WithEvents _GeoCoordinateWatcher As GeoCoordinateWatcher
    Private _Latitude As Double
    Private _Longitude As Double
    Public Sub New()
        _GeoCoordinateWatcher = New GeoCoordinateWatcher
    End Sub
    Private Sub _GeoCoordinateWatcher_PositionChanged(sender As Object, e As GeoPositionChangedEventArgs(Of GeoCoordinate)) Handles _GeoCoordinateWatcher.PositionChanged
        _Latitude = e.Position.Location.Latitude
        _Longitude = e.Position.Location.Longitude
        CustomLog.Logger.Debug($"GeoCoordinateWatcher_PositionChanged {_Latitude},{_Longitude}")
    End Sub
    Private Sub _GeoCoordinateWatcher_StatusChanged(sender As Object, e As GeoPositionStatusChangedEventArgs) Handles _GeoCoordinateWatcher.StatusChanged
        Dim s As String = [Enum].GetName(GetType(GeoPositionStatus), e.Status)
        CustomLog.Logger.Debug($"GeoCoordinateWatcher_StatusChanged {s}")
    End Sub
    Public Function Location() As Location
        Return New Location(_Latitude, _Longitude)
    End Function
End Class
