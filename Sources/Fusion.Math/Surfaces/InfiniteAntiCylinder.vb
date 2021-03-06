﻿Public Class InfiniteAntiCylinder
    Implements ISurfacedPointSet3D

    Private ReadOnly _Cylinder As InfiniteCylinder
    Public ReadOnly Property Origin As Vector3D
        Get
            Return _Cylinder.Origin
        End Get
    End Property
    Public ReadOnly Property NormalizedDirection As Vector3D
        Get
            Return _Cylinder.NormalizedDirection
        End Get
    End Property
    Public ReadOnly Property Radius As Double
        Get
            Return _Cylinder.Radius
        End Get
    End Property

    Public Sub New(origin As Vector3D, direction As Vector3D, radius As Double)
        _Cylinder = New InfiniteCylinder(origin, direction, Radius)
    End Sub

    Public Sub New(cylinder As InfiniteCylinder)
        _Cylinder = cylinder
    End Sub

    Public Function FirstIntersection(ray As Ray) As SurfacePoint Implements ISurface.FirstIntersection
        Dim allIntersectionRayLengths = _Cylinder.SurfaceIntersectionRayLengths(ray)

        If allIntersectionRayLengths.Count = 0 Then Return Nothing

        Dim rayLength = allIntersectionRayLengths.Max
        Dim intersectionLocation = ray.PointOnRay(distanceFromOrigin:=rayLength)

        Dim relativeIntersection = intersectionLocation - Me.Origin
        Dim normal = relativeIntersection.OrthogonalProjectionOn(Me.NormalizedDirection) - relativeIntersection
        Return New SurfacePoint(location:=intersectionLocation, normal:=normal)
    End Function

    Public Function Contains(point As Fusion.Math.Vector3D) As Boolean Implements Fusion.Math.IPointSet3D.Contains
        Return Not _Cylinder.Contains(point)
    End Function

    Public Function Intersections(ray As Ray) As IEnumerable(Of SurfacePoint) Implements ISurface.Intersections
        Dim intersection = Me.FirstIntersection(ray)

        If intersection Is Nothing Then Return Enumerable.Empty(Of SurfacePoint)()
        Return {intersection}
    End Function
End Class
