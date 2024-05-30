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

' TODO
' !! UnitTests https://zenodo.org/records/32156

' Vincenty functions
' Obtained from http://www.movable-type.co.uk/scripts/latlong-vincenty.html
' Vincenty Inverse Solution of Geodesics on the Ellipsoid (c) Chris Veness 2002-2010             
'                                                                                                 
' from: Vincenty inverse formula - T Vincenty, "Direct and Inverse Solutions of Geodesics on the 
'       Ellipsoid with application of nested equations", Survey Review, vol XXII no 176, 1975    
'       http://www.ngs.noaa.gov/PUBS_LIB/inverse.pdf                                             
'
' Translated from Javascript and improved to use Haversine formula for antipodal cases
' when Vincenty's method doesn't converge.
' Maurice Calvert, July 2010
Public Module Vincenty_

    Public Function toRadians(ByVal a As Double) As Double
        Return a / 180 * Math.PI
    End Function
    Public Function toDegrees(ByVal a As Double) As Double
        Return a * 180 / Math.PI
    End Function

    ' ----------------------------------------------------------------------------------------------------------------------
    Public Structure VincentyDirectResult
        Public Latitude As Double
        Public Longitude As Double
        Public FinalBearing As Double
        Public Iterations As Integer
    End Structure
    Public Structure VincentyInverseResult
        Public InitialBearing As Double
        Public Distance As Double
        Public FinalBearing As Double
        Public Iterations As Integer
    End Structure

    ' WGS-84 ellipsoid params https://earth-info.nga.mil/php/download.php?file=coord-wgs84#.pdf
    Const SEMI_MAJOR As Double = 6378137 ' Equatorial
    Const FLATTENING As Double = 1 / 298.257223563

    ' Polar 6,356,752.314245180 (and not 6356752.314245 as it is sometimes quoted)
    Const SEMI_MINOR As Double = (1 - FLATTENING) * SEMI_MAJOR

    Public Function VincentyDirect(ByVal latitude As Double,
                                   ByVal longitude As Double,
                                   ByVal bearing As Double,
                                   ByVal distance As Double) _
            As VincentyDirectResult

        Dim phi1 As Double = toRadians(latitude)
        Dim lambda1 As Double = toRadians(longitude)
        Dim alpha1 As Double = toRadians(bearing)
        Dim s As Double = distance
        Dim a As Double = SEMI_MAJOR
        Dim b As Double = SEMI_MINOR
        Dim f As Double = FLATTENING
        Dim sinalpha1 As Double = Math.Sin(alpha1)
        Dim cosalpha1 As Double = Math.Cos(alpha1)
        Dim tanU1 As Double = (1 - f) * Math.Tan(phi1) ' reduced latitude
        Dim cosU1 As Double = 1 / Math.Sqrt((1 + tanU1 * tanU1)) ' trig identities
        Dim sinU1 As Double = tanU1 * cosU1
        Dim sigma1 As Double = Math.Atan2(tanU1, cosalpha1) ' angular distance on the sphere from the equator to P1
        Dim sinalpha As Double = cosU1 * sinalpha1 ' azimuth of the geodesic at the equator
        Dim cosSqalpha As Double = 1 - sinalpha * sinalpha
        Dim uSq As Double = cosSqalpha * (a * a - b * b) / (b * b)
        Dim bigA As Double = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)))
        Dim bigB As Double = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)))
        Dim sigma As Double = s / (b * a) ' first approximation
        Dim sinsigma As Double = 0
        Dim cossigma As Double = 0
        Dim cos2sigmam As Double = 0
        Dim sigmaprime As Double = 0
        Dim iterations As Integer = 0

        Do
            cos2sigmam = Math.Cos(2 * sigma1 + sigma)
            sinsigma = Math.Sin(sigma)
            cossigma = Math.Cos(sigma)
            Dim deltasigma As Double = bigB * sinsigma * (cos2sigmam + bigB / 4 *
                (cossigma * (-1 + 2 * cos2sigmam * cos2sigmam) - bigB / 6 *
                cos2sigmam * (-3 + 4 * sinsigma * sinsigma) * (-3 + 4 * cos2sigmam * cos2sigmam)))
            sigmaprime = sigma
            sigma = s / (b * bigA) + deltasigma
            iterations += 1
        Loop While Math.Abs(sigma - sigmaprime) > 0.000000000001 AndAlso iterations < 100

        If iterations >= 100 Then
            Return New VincentyDirectResult With {
                .Latitude = Double.NaN,
                .Longitude = Double.NaN,
                .FinalBearing = Double.NaN,
                .Iterations = iterations
            }
        End If

        Dim x As Double = sinU1 * sinsigma - cosU1 * cossigma * cosalpha1
        Dim phi2 As Double = Math.Atan2(sinU1 * cossigma + cosU1 * sinsigma * cosalpha1, (1 - f) * Math.Sqrt(sinalpha * sinalpha + x * x))
        Dim lambda As Double = Math.Atan2(sinsigma * sinalpha1, cosU1 * cossigma - sinU1 * sinsigma * cosalpha1)
        Dim C As Double = f / 16 * cosSqalpha * (4 + f * (4 - 3 * cosSqalpha))
        Dim L As Double = lambda - (1 - C) * f * sinalpha * (sigma + C * sinsigma * (cos2sigmam + C * cossigma * (-1 + 2 * cos2sigmam * cos2sigmam)))
        Dim lambda2 As Double = lambda1 + L
        Dim alpha2 As Double = Math.Atan2(sinalpha, -x)

        Dim result As New VincentyDirectResult With {
            .Latitude = toDegrees(phi2),
            .Longitude = toDegrees(lambda2),
            .FinalBearing = toDegrees(alpha2),
            .Iterations = iterations
            }

        Return result

    End Function

    Function VincentyInverse(ByVal latitude1 As Double,
                             ByVal longitude1 As Double,
                             ByVal latitude2 As Double,
                             ByVal longitude2 As Double) _
                             As VincentyInverseResult

        Dim L As Double
        Dim U1 As Double
        Dim U2 As Double
        Dim sinU1 As Double
        Dim sinU2 As Double
        Dim cosU1 As Double
        Dim cosU2 As Double
        Dim sinLambda As Double
        Dim cosLambda As Double
        Dim sinsquaredsigma As Double
        Dim cosSigma As Double
        Dim sigma As Double
        Dim sinAlpha As Double
        Dim cosSqAlpha As Double
        Dim cos2SigmaM As Double
        Dim C As Double
        Dim lambda As Double
        Dim lambdaP As Double
        Dim delta As Double
        Dim iterations As Integer
        Dim lat1 As Double = toRadians(latitude1)
        Dim lon1 As Double = toRadians(longitude1)
        Dim lat2 As Double = toRadians(latitude2)
        Dim lon2 As Double = toRadians(longitude2)
        Dim s As Double

        L = lon2 - lon1
        U1 = Math.Atan((1 - FLATTENING) * Math.Tan(lat1))
        U2 = Math.Atan((1 - FLATTENING) * Math.Tan(lat2))
        sinU1 = Math.Sin(U1)
        cosU1 = Math.Cos(U1)
        sinU2 = Math.Sin(U2)
        cosU2 = Math.Cos(U2)
        lambda = L
        iterations = 0
        Do
            sinLambda = Math.Sin(lambda)
            cosLambda = Math.Cos(lambda)
            sinsquaredsigma = Math.Sqrt(Math.Pow(cosU2 * sinLambda, 2) +
                                 Math.Pow(cosU1 * sinU2 - sinU1 * cosU2 * cosLambda, 2))
            If sinsquaredsigma = 0.0 Then ' co-incident points
                Return New VincentyInverseResult ' with everything = 0
            End If
            cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda
            sigma = Math.Atan2(sinsquaredsigma, cosSigma)
            sinAlpha = cosU1 * cosU2 * sinLambda / sinsquaredsigma
            cosSqAlpha = 1.0R - sinAlpha * sinAlpha
            cos2SigmaM = cosSigma - 2.0R * sinU1 * sinU2 / cosSqAlpha
            If cos2SigmaM = Double.NaN Then
                cos2SigmaM = 0  ' equatorial line: cosSqAlpha=0 (§6)
            End If
            C = FLATTENING / 16.0R * cosSqAlpha * (4.0R + FLATTENING * (4.0R - 3.0R * cosSqAlpha))
            lambdaP = lambda
            lambda = L + (1.0R - C) * FLATTENING * sinAlpha *
                (sigma + C * sinsquaredsigma * (cos2SigmaM + C * cosSigma * (-1.0R + 2.0R * cos2SigmaM * cos2SigmaM)))
            delta = lambda - lambdaP
            iterations = iterations - 1
        Loop While (Math.Abs(delta) > 0.000000000001R) And iterations < 100

        ' If it doesn't converge, fallback to the Haversine formula

        ' Note: This only happens for certain combinations with longitudes > 179, where the maximum
        ' error can be up to 33'586 metres. As this only occurs when the two points are almost anitpodal
        ' (where the distance is ~20'000Km) this worst-case error is nonetheless only 0.167% of the distance.

        ' For ALL longitudes < 179 the worst error is 79 micrometres

        If iterations >= 100 Then ' Vincenty failed, use Haversine

            s = Haversine(lat1, lon1, lat2, lon2)

            Dim rb As Double = RoughBearing(lat1, lon1, lat2, lon2)

            Return New VincentyInverseResult With {
                .Distance = s,
                .InitialBearing = rb,
                .FinalBearing = rb,
                .Iterations = iterations
            }
        End If

        ' OK, finish Vincenty calculation
        Dim uSq As Double = cosSqAlpha * (SEMI_MAJOR * SEMI_MAJOR - SEMI_MINOR * SEMI_MINOR) / (SEMI_MINOR * SEMI_MINOR)
        Dim aa As Double = 1.0R + uSq / 16384.0R * (4096.0R + uSq * (-768.0R + uSq * (320.0R - 175.0R * uSq)))
        Dim bb As Double = uSq / 1024.0R * (256.0R + uSq * (-128.0R + uSq * (74.0R - 47.0R * uSq)))
        Dim deltaSigma As Double = bb * sinsquaredsigma * (cos2SigmaM + bb / 4.0R * (cosSigma * (-1.0R + 2.0R * cos2SigmaM * cos2SigmaM) -
            bb / 6.0R * cos2SigmaM * (-3.0R + 4.0R * sinsquaredsigma * sinsquaredsigma) * (-3.0R + 4.0R * cos2SigmaM * cos2SigmaM)))
        s = SEMI_MINOR * aa * (sigma - deltaSigma)

        Dim alpha1 As Double
        If Math.Abs(sinsquaredsigma) < Double.Epsilon Then
            alpha1 = 0
        Else
            alpha1 = Math.Atan2(cosU2 * sinLambda, cosU1 * sinU2 - sinU1 * cosU2 * cosLambda)
        End If

        Dim alpha2 As Double
        If Math.Abs(sinsquaredsigma) < Double.Epsilon Then
            alpha2 = Math.PI
        Else
            alpha2 = Math.Atan2(cosU1 * sinLambda, -sinU1 * cosU2 + cosU1 * sinU2 * cosLambda)
        End If

        Dim result As New VincentyInverseResult With {
            .Distance = s,
            .InitialBearing = toDegrees(alpha1),
            .FinalBearing = toDegrees(alpha2),
            .Iterations = iterations
            }

        Return result

    End Function
    Private Function RoughBearing(lat1 As Double, lon1 As Double, lat2 As Double, lon2 As Double) As Double
        lat1 = toRadians(lat1)
        lon1 = toRadians(lon1)
        lat2 = toRadians(lat2)
        lon2 = toDegrees(lon2)
        Dim x As Double = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1)
        Dim y As Double = Math.Sin(lon2 - lon1) * Math.Cos(lat2)
        Return toDegrees((Math.Atan2(y, x) + Math.PI * 2) Mod (Math.PI * 2))
    End Function
End Module
