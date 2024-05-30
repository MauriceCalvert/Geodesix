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
Public Module IsNumeric_
    Public Function IsNumeric(ByRef value As Object) As Boolean
        Return TypeOf value Is SByte OrElse TypeOf value Is Byte OrElse TypeOf value Is Short OrElse TypeOf value Is UShort OrElse
               TypeOf value Is Integer OrElse TypeOf value Is UInteger OrElse TypeOf value Is Long OrElse TypeOf value Is ULong OrElse
               TypeOf value Is Single OrElse TypeOf value Is Double OrElse TypeOf value Is Decimal
    End Function
    Public Function IsNumeric(ByRef type As Type) As Boolean
        ' https://stackoverflow.com/a/1750002/338101
        Return NumericTypes.Contains(type)
    End Function
    Private NumericTypes As HashSet(Of Type) = New HashSet(Of Type) From {
        GetType(Decimal),
        GetType(Integer),
        GetType(UInteger),
        GetType(Long),
        GetType(ULong),
        GetType(Single),
        GetType(Double),
        GetType(Short),
        GetType(UShort)
    }
End Module
