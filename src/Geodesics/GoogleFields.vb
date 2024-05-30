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
Module GoogleFields_

    Public ReadOnly Property GoogleFields As New List(Of String) From {
        "administrative_area_level_1 political",
        "administrative_area_level_2 political",
        "boundsne",
        "boundssw",
        "country political",
        "formatted_address",
        "latitude",
        "location_type",
        "longitude",
        "partial_match",
        "place_id",
        "plus_code",
        "political sublocality sublocality_level_1",
        "postal_code",
        "postal_town",
        "route",
        "status",
        "street_number",
        "types",
        "viewpointne",
        "viewpointsw"
    }

End Module
