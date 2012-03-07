Public Class SingleReferenceFrameRelativisticRayTracerWithoutDopplerEffect(Of TLight As {ILight(Of TLight), New})
    Inherits SingleReferenceFrameRelativisticRayTracerBase(Of TLight)

    Public Sub New(classicRayTracer As IRayTracer(Of TLight),
                    cameraVelocity As Vector3D)
        MyBase.New(classicRayTracer:=classicRayTracer, observerVelocity:=cameraVelocity)
    End Sub

    Public Overrides Function GetLight(observerSightRayWithObjectOrigin As Ray) As TLight
        Dim observerSightRay = New SightRay(observerSightRayWithObjectOrigin)
        Dim objectSightRay = ObjectToObserver.Inverse.AtSightRay(observerSightRay).SemiTransformSightRay
        Dim light = ClassicRayTracer.GetLight(objectSightRay.Ray)

        Dim searchlightFactor = ObjectToObserver.AtSightRay(observerSightRay).TransformSpectralRadiance(spectralRadiance:=1)

        Return light.MultiplyBrightness(searchlightFactor)
    End Function

End Class
