﻿Public Class InfiniteAntiCylinder
    Implements ISurfacedPointSet3D

    Private ReadOnly _cylinder As InfiniteCylinder
    Public ReadOnly Property Origin As Vector3D
        Get
            Return _cylinder.Origin
        End Get
    End Property
    Public ReadOnly Property NormalizedDirection As Vector3D
        Get
            Return _cylinder.NormalizedDirection
        End Get
    End Property
    Public ReadOnly Property Radius As Double
        Get
            Return _cylinder.Radius
        End Get
    End Property

    Public Sub New(ByVal origin As Vector3D, ByVal direction As Vector3D, ByVal radius As Double)
        _cylinder = New InfiniteCylinder(origin, direction, Radius)
    End Sub

    Public Sub New(ByVal cylinder As InfiniteCylinder)
        _cylinder = cylinder
    End Sub

    Public Function Intersection(ByVal ray As Ray) As SurfacePoint Implements ISurfacedPointSet3D.FirstIntersection
        Dim allIntersectionRayLengths = _cylinder.SurfaceIntersectionRayLengths(ray)

        If allIntersectionRayLengths.Count = 0 Then Return Nothing

        Dim rayLength = allIntersectionRayLengths.Max
        Dim intersectionLocation = ray.PointOnRay(distanceFromOrigin:=rayLength)

        Dim relativeRayOrigin = ray.Origin - Me.Origin
        Dim normal = relativeRayOrigin - relativeRayOrigin.OrthogonalProjectionOn(Me.NormalizedDirection)
        Return New SurfacePoint(location:=intersectionLocation, normal:=normal)
    End Function

    Public Function Contains(ByVal point As Fusion.Math.Vector3D) As Boolean Implements Fusion.Math.IPointSet3D.Contains
        Return Not _cylinder.Contains(point)
    End Function

    Public Function Intersections(ByVal ray As Ray) As System.Collections.Generic.IEnumerable(Of SurfacePoint) Implements ISurface.Intersections
        Dim intersection = Me.Intersection(ray)

        If intersection Is Nothing Then Return Enumerable.Empty(Of SurfacePoint)()
        Return {intersection}
    End Function
End Class