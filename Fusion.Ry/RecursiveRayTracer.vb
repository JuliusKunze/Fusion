﻿Public Class RecursiveRayTracer
    Implements IRayTracer

    Public Sub New(ByVal surface As IMaterialSurface(Of Material2D))
        Me.New(surface, LightSource:=New LightSources, ShadedPointLightSources:=New ShadedLightSources(surface))
    End Sub

    Public Sub New(ByVal surface As IMaterialSurface(Of Material2D),
                   ByVal lightSource As ILightSource,
                   ByVal shadedPointLightSources As List(Of IPointLightSource),
                   Optional ByVal maxIntersectionCount As Integer = 10)
        Me.Surface = surface
        Me.LightSource = lightSource
        Me.ShadedPointLightSources = shadedPointLightSources
        Me.MaxIntersectionCount = maxIntersectionCount
    End Sub

    Public Property Surface As IMaterialSurface(Of Material2D)
    Public Property LightSource As ILightSource

    Public Property ShadedPointLightSources As List(Of IPointLightSource)
        Get
            Return _shadedLightSources
        End Get
        Set(ByVal value As List(Of IPointLightSource))
            _shadedLightSources = New ShadedLightSources(pointLightSources:=value, shadowingSurface:=Me.Surface)
        End Set
    End Property

    Private _shadedLightSources As ShadedLightSources

    Private Function TraceColor(ByVal ray As Ray, ByVal intersectionCount As Integer) As ExactColor
        Dim firstIntersection = Me.Surface.FirstMaterialIntersection(ray)

        If firstIntersection Is Nothing Then Return Me.BackColor

        Dim materialColor = firstIntersection.Material.LightSourceColor

        Dim finalColor = materialColor
        If firstIntersection.Material.Scatters Then
            Dim lightColor = Me.LightSource.LightColor(firstIntersection) + _shadedLightSources.LightColor(firstIntersection)
            finalColor += firstIntersection.Material.ScatteringRemission.Color(lightColor)
        End If

        If intersectionCount >= Me.MaxIntersectionCount Then
            Return finalColor
        Else
            Dim rayChanger = New RayChanger(ray)
            If firstIntersection.Material.Reflects Then
                Dim reflectedRay = rayChanger.ReflectedRay(firstIntersection)
                Dim reflectionColor = TraceColor(ray:=reflectedRay, intersectionCount:=intersectionCount + 1)
                finalColor += firstIntersection.Material.ReflectionRemission.Color(reflectionColor)
            End If
            If firstIntersection.Material.IsTranslucent Then
                Dim passedRay As Ray
                If firstIntersection.Material.Refracts Then
                    passedRay = rayChanger.RefractedRay(firstIntersection)
                Else
                    passedRay = rayChanger.PassedRay(firstIntersection)
                End If
                Dim passedColor = TraceColor(ray:=passedRay, intersectionCount:=intersectionCount + 1)
                finalColor += firstIntersection.Material.TransparencyRemission.Color(passedColor)
            End If
            Return finalColor
        End If
    End Function

    Public Property BackColor As ExactColor = ExactColor.Black
    Public Property MaxIntersectionCount As Integer

    Public Function GetColor(ByVal startRay As Ray) As ExactColor Implements IRayTracer.GetColor
        Return Me.TraceColor(startRay, intersectionCount:=0)
    End Function

End Class