﻿''' <summary>
''' A ray tracer that provides the geometry effects of a camera 
''' that moves with a velocity (in x-direction) very close to the light velocity.
''' </summary>
''' <remarks></remarks>
Public Class RelativisticRecursiveRayTracer
    Inherits RecursiveRayTracer

    Public Sub New(ByVal surface As IMaterialSurface(Of Material2D), ByVal xCameraVelocityInC As Double)
        MyBase.New(surface:=surface)

        _rayTransformation = New RelativisticRayTransformation(relativeXVelocityInC:=xCameraVelocityInC)
    End Sub

    Public Sub New(ByVal surface As IMaterialSurface(Of Material2D),
                   ByVal lightSource As ILightSource,
                   ByVal shadedPointLightSources As List(Of IPointLightSource),
                   ByVal xCameraVelocityInC As Double,
                   Optional ByVal maxIntersectionCount As Integer = 10)
        MyBase.New(surface:=surface, lightSource:=lightSource, shadedPointLightSources:=shadedPointLightSources, maxIntersectionCount:=maxIntersectionCount)

        _rayTransformation = New RelativisticRayTransformation(relativeXVelocityInC:=xCameraVelocityInC)
    End Sub

    Private _rayTransformation As RelativisticRayTransformation

    Public Overrides Function GetColor(ByVal startRay As Ray) As ExactColor
        Dim transformedRay = _rayTransformation.TransformedRay(ray:=startRay)
        
        Return MyBase.GetColor(transformedRay)
    End Function

End Class
