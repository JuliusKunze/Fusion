Public Class RayTracerTests
    <Test()>
    Public Sub Reflection()
        Dim reflectingSphere = New MaterialSurface(Of RgbLight)(New Sphere(center:=New Vector3D(-1, 0, 0), radius:=1),
                                                         material:=New Material2D(Of RgbLight)(sourceLight:=New RgbLight(0, 0, 1),
                                                                                  scatteringRemission:=New BlackRemission(Of RgbLight),
                                                                                  reflectionRemission:=New BlackRemission(Of RgbLight),
                                                                                  transparencyRemission:=New BlackRemission(Of RgbLight)))

        Dim colorSphere = New MaterialSurface(Of RgbLight)(New Sphere(center:=New Vector3D(3, -3, 0), radius:=1),
                                                         material:=RgbLightMaterials2D.LightSource(sourceLight:=New RgbLight(0, 0, 1)))

        Dim surfaces = New Surfaces(Of RgbLight)({reflectingSphere, colorSphere})
        Dim rayTracer = New ScatteringRayTracer(Of RgbLight)(surfaces)

        Dim startRay = New SightRay(New Ray(origin:=New Vector3D(1, 1, 0), direction:=New Vector3D(-1, -1, 0)))
        Dim color = rayTracer.GetLight(startRay)
        Assert.AreEqual(0, color.Red)
        Assert.AreEqual(0, color.Green)
        Assert.AreEqual(1, color.Blue)
    End Sub
End Class
