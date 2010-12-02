﻿Public Class RayChanger
    Public Property SourceRay As Ray

    Public Sub New(ByVal sourceRay As Ray)
        Me.SourceRay = sourceRay
    End Sub

    Public Function ReflectedRay(ByVal intersection As SurfacePoint) As Ray
        Dim normalizedNormal = intersection.NormalizedNormal
        Return AddSafetyDistance(New Ray(origin:=intersection.Location,
                                 direction:=Me.SourceRay.NormalizedDirection - 2 * Me.SourceRay.NormalizedDirection.OrthogonalProjectionOn(normalizedNormal)))
    End Function

    Public Function RefractedRay(ByVal intersection As MaterialSurfacePoint(Of Material2D)) As Ray
        Dim normalizedNormal = intersection.NormalizedNormal
        Dim refractionIndexQuotient = intersection.Material.RefractionIndexRatio

        Dim startSinusVector = Me.SourceRay.NormalizedDirection - Me.SourceRay.NormalizedDirection.OrthogonalProjectionOn(normalizedNormal)
        Dim startSinus = startSinusVector.Length
        Dim finalSinus = startSinus * refractionIndexQuotient
        Dim finalCosinus = Sqrt(1 - finalSinus ^ 2)
        Dim finalDirection = finalCosinus * -normalizedNormal + finalSinus * startSinusVector.Normalized

        Return AddSafetyDistance(New Ray(origin:=intersection.Location, direction:=finalDirection))
    End Function

    Public Function PassedRay(ByVal intersection As SurfacePoint) As Ray
        Return AddSafetyDistance(New Ray(origin:=intersection.Location, direction:=Me.SourceRay.NormalizedDirection))
    End Function

    Private Shared _random As New Random
    Public Function ScatteredRay(ByVal intersection As SurfacePoint) As Ray
        Dim scatteredRayDirection = normalizedRandomDirection()
        If scatteredRayDirection * intersection.NormalizedNormal < 0 Then
            scatteredRayDirection *= -1
        End If

        Return AddSafetyDistance(New Ray(origin:=intersection.Location, direction:=scatteredRayDirection))

        Throw New NotImplementedException
    End Function

    Private Shared Function normalizedRandomDirection() As Vector3D
        ' The z coordinate of random sphere surface points is uniform distributed in [-1; 1].
        Dim z = _random.NextDouble * 2 - 1
        Dim phi = _random.NextDouble * 2 * PI
        Dim rho = Sqrt(1 - z ^ 2)
        Dim y = rho * Sin(phi)
        Dim x = rho * Cos(phi)

        Return New Vector3D(z, y, x)
    End Function

    ''' <summary>
    ''' Adds a safety distance vector to the ray start location to avoid a double intersection with the same surface.
    ''' </summary>
    ''' <param name="ray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function AddSafetyDistance(ByVal ray As Ray) As Ray
        ray.Origin += ray.NormalizedDirection * SaftyDistance
        Return ray
    End Function

    Public Const SaftyDistance As Double = 0.0000000001

End Class
