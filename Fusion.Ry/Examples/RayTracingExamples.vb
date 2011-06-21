Public Class RayTracingExamples

    Public Property PictureSize As Size

    Public Sub New(ByVal pictureSize As Size)
        Me.PictureSize = pictureSize
    End Sub

    Public Function FirstRoom(Optional ByVal glassRefractionIndex As Double = 1.4) As RayTraceDrawer(Of RgbLight)
        Dim view = New View3D(observerLocation:=New Vector3D(5, 5, 25),
                              lookAt:=New Vector3D(5, 5, 0),
                              upVector:=New Vector3D(0, 1, 0),
                              xAngleFromMinus1To1:=PI / 3)
        Dim lamp = New LinearPointLightSource(Of RgbLight)(Location:=New Vector3D(5, 9.9, 8), baseLight:=RgbLight.White * 5)

        Dim grayMaterial = ColorMaterials2D.Scattering(New RgbLight(Color.Gray))

        Dim ground = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Plane(Location:=Vector3D.Zero, normal:=New Vector3D(0, 1, 0)),
                                               material:=grayMaterial)

        Dim rightWall = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Plane(Location:=New Vector3D(10, 0, 0), normal:=New Vector3D(-1, 0, 0)),
                                                                 material:=ColorMaterials2D.Scattering(New RgbLight(Color.MediumBlue)))
        Dim leftWall = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Plane(Location:=Vector3D.Zero, normal:=New Vector3D(1, 0, 0)),
                                                                material:=ColorMaterials2D.Scattering(New RgbLight(Color.Orange)))
        Dim ceiling = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Plane(Location:=New Vector3D(0, 10, 0), normal:=New Vector3D(0, -1, 0)),
                                                               material:=ColorMaterials2D.Scattering(0.2))

        Dim frontWall = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Plane(Location:=Vector3D.Zero, normal:=New Vector3D(0, 0, 1)),
                                                                 material:=grayMaterial)

        Dim backWall = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Plane(Location:=New Vector3D(0, 0, 40), normal:=New Vector3D(0, 0, -1)),
                                                                material:=grayMaterial)

        Dim lampSurface = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Sphere(center:=New Vector3D(5, 10, 8), radius:=2),
                                        material:=ColorMaterials2D.LightSource(RgbLight.White * 10))

        Dim reflectingSphereRadius = 2
        Dim reflectingSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Sphere(center:=New Vector3D(3.5, reflectingSphereRadius, 8), radius:=reflectingSphereRadius),
                                material:=ColorMaterials2D.Reflecting)

        Dim glass = ColorMaterials2D.Transparent(scatteringRemission:=New BlackRemission(Of RgbLight),
                                            reflectionRemission:=New ScaledRemission(Of RgbLight)(0.4),
                                            refractionIndexQuotient:=1 / glassRefractionIndex)
        Dim glassInside = glass.Clone
        glassInside.RefractionIndexQuotient = glassRefractionIndex
        glassInside.ReflectionRemission = New BlackRemission(Of RgbLight)

        'Dim cylinder = New InfiniteCylinder(New Vector3D(8, 0, 8), New Vector3D(0, 1, 0), 1)
        'Dim glassCylinder = New SingleMaterialSurface(Of Material2D(Of ExactColor))(cylinder, glass)
        'Dim antiGlassCylinder = New SingleMaterialSurface(Of Material2D(Of ExactColor))(New InfiniteAntiCylinder(cylinder), glassInside)

        Dim refractingSphereRadius = 1.8
        Dim refractionSphereCenter = New Vector3D(8, refractingSphereRadius, 13)
        Dim sphere = New Sphere(center:=refractionSphereCenter,
                                radius:=refractingSphereRadius)
        'Dim cutOutSphere = New Sphere(refractionSphereCenter + New Vector3D(0, 1, 1), 2.2999999999999998)
        'Dim cutOutAntiSphere = New AntiSphere(cutOutSphere)

        'New IntersectedSurface(sphere, cutOutAntiSphere)
        Dim refractingSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(sphere, material:=glass)
        'New UnitedSurface(New AntiSphere(sphere), cutOutSphere)
        Dim innerRefractingSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New AntiSphere(sphere),
                                                              material:=glassInside)


        Dim allSurfaces = New Surfaces(Of Material2D(Of RgbLight)) From {ground, ceiling, rightWall, leftWall, frontWall, backWall, lampSurface,
                                                     reflectingSphere, refractingSphere, innerRefractingSphere} ', glassCylinder, antiGlassCylinder}

        'Dim rayTracer = New RecursiveRayTracer(surface:=allSurfaces, lightSource:=New LightSources, shadedPointLightSources:=New List(Of IPointLightSource(Of ExactColor)) From {lamp})
        Dim rayTracer = New ScatteringRayTracer(Of RgbLight)(surface:=allSurfaces, rayCount:=1, maxIntersectionCount:=7)

        Return New RayTraceDrawer(Of RgbLight)(rayTracer, Me.PictureSize, view)
    End Function

    Public Function SquaredSurfaceDrawer() As RayTraceDrawer(Of RgbLight)
        Dim cameraLocation = New Vector3D(4, 0.3, 0)

        Dim view = New View3D(observerLocation:=cameraLocation,
                              lookAt:=New Vector3D(0, 0.3, 0),
                              upVector:=New Vector3D(0, 1, 0),
                              xAngleFromMinus1To1:=PI / 2)
        Dim groundMaterial1 = New Material2D(Of RgbLight)(sourceLight:=New RgbLight(Color.Gray),
                                             scatteringRemission:=New BlackRemission(Of RgbLight),
                                             reflectionRemission:=New ScaledRemission(Of RgbLight)(0.2),
                                             transparencyRemission:=New BlackRemission(Of RgbLight))
        Dim groundMaterial2 = New Material2D(Of RgbLight)(sourceLight:=New RgbLight(Color.Blue),
                                             scatteringRemission:=New BlackRemission(Of RgbLight),
                                             reflectionRemission:=New ScaledRemission(Of RgbLight)(0.2),
                                             transparencyRemission:=New BlackRemission(Of RgbLight))
        Dim ground = New SquaredMaterialSurface(Of Material2D(Of RgbLight))(New Plane(Location:=New Vector3D(0, -1, 0),
                                                          normal:=New Vector3D(0, 1, 0)),
                                                squaresXVector:=New Vector3D(1, 0, 0),
                                                squaresYVector:=New Vector3D(0, 0, 1),
                                                squareLength:=1,
                                                material1:=groundMaterial1,
                                                material2:=groundMaterial2)

        Dim refractingSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Sphere(center:=New Vector3D(1.5, 0, 1), radius:=1),
                                                         material:=ColorMaterials2D.Transparent(scatteringRemission:=New ScaledRemission(Of RgbLight)(0.2),
                                                                                           reflectionRemission:=New ScaledRemission(Of RgbLight)(0.1),
                                                                                           refractionIndexQuotient:=1 / 2))
        Dim refractingSphereInside = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New AntiSphere(center:=New Vector3D(1.5, 0, 1), radius:=1),
                                                               material:=ColorMaterials2D.Transparent(scatteringRemission:=New ScaledRemission(Of RgbLight)(0.2),
                                                                                                 reflectionRemission:=New ScaledRemission(Of RgbLight)(0.1),
                                                                                                 refractionIndexQuotient:=2))

        Dim sphere2 = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Sphere(center:=New Vector3D(-0.6, 0.8, 0), radius:=1.3),
                                                material:=ColorMaterials2D.Reflecting(albedo:=0.2))

        Dim reflectingSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Sphere(center:=New Vector3D(-0.6, 0.8, -2.5), radius:=1),
                                                         material:=New Material2D(Of RgbLight)(sourceLight:=RgbLight.Black,
                                                                                  scatteringRemission:=New ScaledRemission(Of RgbLight)(0.2),
                                                                                  reflectionRemission:=New FullRemission(Of RgbLight),
                                                                                  transparencyRemission:=New BlackRemission(Of RgbLight)))

        Dim surfaces = New Surfaces(Of Material2D(Of RgbLight)) From {ground, refractingSphere, sphere2, reflectingSphere}
        'Dim surfaces = New MaterialSurfaces
        'For i = 1 To 10
        '    surfaces.Add(Me.RandomSphere)
        'Next

        Dim undirectionalLightSource = New UndirectionalLightSource(Of RgbLight)(New RgbLight(Color.DarkGray))
        Dim directionalLightSoure = New DirectionalLightSource(Of RgbLight)(direction:=New Vector3D(1, 1, 1),
                                                                    baseLight:=New RgbLight(Color.DarkGray))
        Dim pointLightSource1 = New PointLightSource(Of RgbLight)(Location:=1.5 * New Vector3D(1, 1, 1),
                                                     baseLight:=New RgbLight(Color.Orange))
        Dim pointLightSource2 = New PointLightSource(Of RgbLight)(Location:=New Vector3D(2, 0, -1),
                                                     baseLight:=New RgbLight(Color.White))
        Dim lightSources = New LightSources(Of RgbLight) 'From {directionalLightSoure}
        Dim shadedLightSources = New List(Of IPointLightSource(Of RgbLight)) From {pointLightSource1, pointLightSource2}

        Dim rayTracer = New RecursiveRayTracer(Of RgbLight)(surface:=surfaces,
                                                              unshadedLightSource:=lightSources,
                                                              shadedPointLightSources:=shadedLightSources)

        Return New RayTraceDrawer(Of RgbLight)(rayTracer:=rayTracer, Size:=PictureSize, view:=view)
    End Function

    Dim random As Random = New Random()
    Private Function RandomSphere() As ColorfulSphere
        Dim radius = random.NextDouble * 2
        Dim x = random.NextDouble * 6 - 4
        Dim y = random.NextDouble * 6 - 1
        Dim z = random.NextDouble * 8 - 4

        Dim reflectionRemission = New ScaledRemission(Of RgbLight)(albedo:=random.NextDouble)
        Dim scatteringRemission = New ScaledRemission(Of RgbLight)(1 - reflectionRemission.Albedo)
        Dim sphereColor = scatteringRemission.Albedo * RgbLight.White

        Return New ColorfulSphere(New Vector3D(x, y, z), radius, material:=New Material2D(Of RgbLight)(sourceLight:=sphereColor,
                                                                                          scatteringRemission:=scatteringRemission,
                                                                                          reflectionRemission:=reflectionRemission,
                                                                                          transparencyRemission:=New BlackRemission(Of RgbLight)))
    End Function

    Public Function SecondRoom(ByVal cameraZLocation As Double) As RayTraceDrawer(Of RgbLight)
        'Dim view = New View3D(cameraLocation:=New Vector3D(15, 6, 29),
        '                      lookAt:=New Vector3D(3, 3, 0),
        '                      upVector:=New Vector3D(0, 1, 0),
        '                      xAngleFromMinus1To1:=PI * 0.26)
        Dim view = New View3D(observerLocation:=New Vector3D(7.5, 6, 15),
                              lookAt:=New Vector3D(7.5, 3, 0),
                              upVector:=New Vector3D(0, 1, 0),
                              xAngleFromMinus1To1:=PI * 0.26)
        Dim rayTracer = SecondRoomRayTracer()
        'Dim rayTracer = New ScatteringRayTracer(surface:=surfaces, rayCount:=200, maxIntersectionCount:=10)

        Return New RayTraceDrawer(Of RgbLight)(rayTracer:=rayTracer, Size:=PictureSize, view:=view)
    End Function

    Public Function SecondRoomRayTracer() As RelativisticRayTracer(Of RgbLight)
        Dim origin = Vector3D.Zero
        Dim frontLeftDown = New Vector3D(0, 0, 6)
        Dim backLeftDown = New Vector3D(0, 0, 16)
        Dim backRightDown = New Vector3D(14, 0, 16)
        Dim frontRightDown = New Vector3D(14, 0, 0)
        Dim leftFrontDown = New Vector3D(8, 0, 0)
        Dim midDown = New Vector3D(8, 0, 6)

        Dim heightVector = New Vector3D(0, 10, 0)

        Dim originUp = origin + heightVector
        Dim frontLeftUp = frontLeftDown + heightVector
        Dim backLeftUp = backLeftDown + heightVector
        Dim backRightUp = backRightDown + heightVector
        Dim frontRightUp = frontRightDown + heightVector
        Dim leftFrontUp = leftFrontDown + heightVector
        Dim midUp = midDown + heightVector

        Dim lightMaterial = ColorMaterials2D.LightSource(RgbLight.White)
        Dim redMaterial = ColorMaterials2D.Scattering(New RgbLight(Color.Red))
        Dim whiteMaterial = ColorMaterials2D.Scattering(New RgbLight(Color.White))
        Dim greenMaterial = ColorMaterials2D.Scattering(New RgbLight(Color.Green))

        Dim glassRefractionIndex = 1.3

        Dim glass = ColorMaterials2D.Transparent(1 / glassRefractionIndex, reflectionAlbedo:=0.2)
        Dim innerGlass = ColorMaterials2D.TransparentInner(1 / glassRefractionIndex)

        Dim metal = ColorMaterials2D.Reflecting

        Dim blueGroundMaterial = New Material2D(Of RgbLight)(sourceLight:=New RgbLight(Color.Black),
                             scatteringRemission:=New ComponentScaledColorRemission(Color.Blue),
                             reflectionRemission:=New ScaledRemission(Of RgbLight)(0.5),
                             transparencyRemission:=New BlackRemission(Of RgbLight))
        Dim whiteGroundMaterial = New Material2D(Of RgbLight)(sourceLight:=RgbLight.Black,
                                             scatteringRemission:=New ComponentScaledColorRemission(Color.White),
                                             reflectionRemission:=New ScaledRemission(Of RgbLight)(0.5),
                                             transparencyRemission:=New BlackRemission(Of RgbLight))
        Dim groundRectangle = New Fusion.Math.Rectangle(frontRightDown, origin, backLeftDown)

        Dim ground = New SquaredMaterialSurface(Of Material2D(Of RgbLight))(groundRectangle,
                                        squaresXVector:=New Vector3D(1, 0, 0),
                                        squaresYVector:=New Vector3D(0, 0, 1),
                                        squareLength:=1,
                                        material1:=blueGroundMaterial,
                                        material2:=whiteGroundMaterial)

        Dim redWallPlane = New Fusion.Math.Rectangle(frontLeftDown, frontLeftUp, backLeftUp)
        Dim redWall = New SingleMaterialSurface(Of Material2D(Of RgbLight))(redWallPlane, redMaterial)

        Dim frontWallPlane1 = New Fusion.Math.Rectangle(frontLeftDown, midDown, midUp)
        Dim frontWallPlane2 = New Fusion.Math.Rectangle(midDown, leftFrontDown, leftFrontUp)
        Dim frontWallPlane3 = New Fusion.Math.Rectangle(leftFrontDown, frontRightDown, frontRightUp)

        Dim frontWallSurface = New Surfaces From {frontWallPlane1, frontWallPlane2, frontWallPlane3}
        Dim frontWall = New SingleMaterialSurface(Of Material2D(Of RgbLight))(frontWallSurface, whiteMaterial)

        Dim greenWallPlane = New Fusion.Math.Rectangle(frontRightUp, frontRightDown, backRightDown)
        Dim greenWall = New SingleMaterialSurface(Of Material2D(Of RgbLight))(greenWallPlane, greenMaterial)

        Dim backWallPlane = New Fusion.Math.Rectangle(backRightDown, backLeftDown, backLeftUp)
        Dim backWall = New SingleMaterialSurface(Of Material2D(Of RgbLight))(backWallPlane, whiteMaterial)

        Dim pointLightSource = New LinearPointLightSource(Of RgbLight)(location:=New Vector3D(6, 9.5, 10), baseLight:=RgbLight.White * 5)
        Dim shadedLightSources = New List(Of IPointLightSource(Of RgbLight)) From {pointLightSource}

        Dim ceilingPlane = New Fusion.Math.Rectangle(backLeftUp, originUp, frontRightUp)
        Dim ceiling = New SingleMaterialSurface(Of Material2D(Of RgbLight))(ceilingPlane, whiteMaterial)

        Dim scatteringSphereRadius = 0.75
        Dim scatteringSphereCenter = backRightDown + New Vector3D(-1.5, scatteringSphereRadius, -2.5)
        Dim scatteringSphereSurface = New Sphere(scatteringSphereCenter, scatteringSphereRadius)
        Dim scatteringSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(scatteringSphereSurface, whiteMaterial)

        Dim metalSphereRadius = 1.5
        Dim metalSphereCenter = midDown + New Vector3D(0, metalSphereRadius, 3)
        Dim metalSphereSurface = New Sphere(metalSphereCenter, metalSphereRadius)
        Dim metalSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(metalSphereSurface, metal)

        Dim glassLocation = backLeftDown + New Vector3D(3, 0, -4.5)
        Dim glassCylinderHeight = 3

        Dim glassSphereRadius = 1.5
        Dim glassSphereCenter = glassLocation + New Vector3D(0, glassCylinderHeight + glassSphereRadius, 0)
        Dim glassSphereSurface = New Sphere(glassSphereCenter, glassSphereRadius)
        Dim glassSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(glassSphereSurface, glass)
        Dim glassAntiSphereSurface = New AntiSphere(glassSphereSurface)
        Dim glassAntiSphere = New SingleMaterialSurface(Of Material2D(Of RgbLight))(glassAntiSphereSurface, innerGlass)

        Dim glassCylinder = New Cylinder(startCenter:=glassLocation, endCenter:=glassLocation + New Vector3D(0, glassCylinderHeight, 0), radius:=0.15)
        Dim glassAntiCylinder = New AntiCylinder(glassCylinder)
        Dim glassCylinderSurface = New SingleMaterialSurface(Of Material2D(Of RgbLight))(glassCylinder, glass)
        Dim glassAntiCylinderSurface = New SingleMaterialSurface(Of Material2D(Of RgbLight))(glassAntiCylinder, innerGlass)

        Dim frontCylinderHeight = 6
        Dim frontCylinderRadius = 0.75
        Dim frontCylinder = New InfiniteCylinder(New Vector3D(0, frontCylinderHeight + frontCylinderRadius, 0), New Vector3D(1, 0, 0), radius:=frontCylinderRadius)
        Dim frontCylinderSurface = New SingleMaterialSurface(Of Material2D(Of RgbLight))(frontCylinder, material:=whiteMaterial)

        Dim lampHeight = New Vector3D(0, 9.5, 0)
        Dim lampWidth = New Vector3D(2.5, 0, 0)
        Dim lampDepth = New Vector3D(0, 0, 2.5)
        Dim lampThickness = heightVector - lampHeight

        Dim lampFrontLeft = frontLeftDown + New Vector3D(4, 0, 4) + lampHeight
        Dim lampBackRight = lampFrontLeft + lampWidth + lampDepth
        Dim lampFrontRight = lampFrontLeft + lampWidth
        Dim lampBackLeft = lampFrontLeft + lampDepth
        Dim lampRectangle = New Fusion.Math.Rectangle(vertex1:=lampFrontLeft, vertex2:=lampFrontRight,
                                                      vertex3:=lampBackRight)
        Dim lampFront = New Fusion.Math.Rectangle(vertex1:=lampFrontRight, vertex2:=lampFrontLeft,
                                                      vertex3:=lampFrontLeft + lampThickness)
        Dim lampBack = New Fusion.Math.Rectangle(vertex1:=lampBackLeft, vertex2:=lampBackRight,
                                                      vertex3:=lampBackRight + lampThickness)
        Dim lampRight = New Fusion.Math.Rectangle(vertex1:=lampBackRight, vertex2:=lampFrontRight,
                                                      vertex3:=lampFrontRight + lampThickness)
        Dim lampLeft = New Fusion.Math.Rectangle(vertex1:=lampFrontLeft, vertex2:=lampBackLeft,
                                              vertex3:=lampBackLeft + lampThickness)

        Dim light = New SingleMaterialSurface(Of Material2D(Of RgbLight))(lampRectangle, lightMaterial)
        Dim lampSide = New SingleMaterialSurface(Of Material2D(Of RgbLight))(New Surfaces From {lampFront, lampBack,
                                                                                   lampRight, lampLeft},
                                                                whiteMaterial)
        Dim surfaces = New Surfaces(Of Material2D(Of RgbLight)) From {ground, redWall, frontWall, greenWall, backWall, ceiling,
                                                                 light, scatteringSphere,
                                                                 glassSphere, glassAntiSphere,
                                                                 metalSphere,
                                                                 glassCylinderSurface, glassAntiCylinderSurface,
                                                                 frontCylinderSurface,
                                                                 lampSide}
        Return New RelativisticRayTracer(Of RgbLight)(surface:=surfaces, xCameraVelocityInC:=-0.5,
                                                              unshadedLightSource:=New LightSources(Of RgbLight),
                                                              shadedPointLightSources:=shadedLightSources,
                                                              maxIntersectionCount:=10)
    End Function
End Class