﻿Public NotInheritable Class Materials2D
    Private Sub New()
    End Sub

    Public Shared Function Black() As Material2D
        Return Materials2D.LightSource(sourceLight:=ExactColor.Black)
    End Function

    Public Shared Function LightSource(ByVal sourceLight As ExactColor) As Material2D
        Return New Material2D(sourceLight:=sourceLight,
                              scatteringRemission:=New BlackColorRemission,
                              reflectionRemission:=New BlackColorRemission,
                              transparencyRemission:=New BlackColorRemission)
    End Function

    Public Shared Function Scattering(ByVal color As ExactColor) As Material2D
        Return New Material2D(sourceLight:=ExactColor.Black,
                              scatteringRemission:=New ComponentScaledColorRemission(color),
                              reflectionRemission:=New BlackColorRemission,
                              transparencyRemission:=New BlackColorRemission)
    End Function

    Public Shared Function Transparent(ByVal refractionIndexQuotient As Double, ByVal reflectionAlbedo As Double) As Material2D
        Return Materials2D.Transparent(scatteringRemission:=New BlackColorRemission,
                                       reflectionRemission:=New ScaledColorRemission(reflectionAlbedo),
                                       refractionIndexQuotient:=refractionIndexQuotient)
    End Function

    Public Shared Function Transparent(ByVal scatteringRemission As ILightRemission(Of ExactColor),
                                       ByVal reflectionRemission As ILightRemission(Of ExactColor),
                                       ByVal refractionIndexQuotient As Double) As Material2D

        Return New Material2D(sourceLight:=ExactColor.Black,
                              scatteringRemission:=scatteringRemission,
                              reflectionRemission:=reflectionRemission,
                              transparencyRemission:=New FullColorRemission,
                              refractionIndexQuotient:=refractionIndexQuotient)
    End Function

    Public Shared Function TransparentInner(ByVal refractionIndexQuotient As Double) As Material2D
        Return New Material2D(sourceLight:=ExactColor.Black,
                              scatteringRemission:=New BlackColorRemission,
                              reflectionRemission:=New BlackColorRemission,
                              transparencyRemission:=New FullColorRemission,
                              refractionIndexQuotient:=1 / refractionIndexQuotient)
    End Function

    Public Shared Function Reflecting(Optional ByVal albedo As Double = 1) As Material2D
        Return New Material2D(sourceLight:=ExactColor.Black,
                              scatteringRemission:=New BlackColorRemission,
                              reflectionRemission:=New ScaledColorRemission(albedo:=albedo),
                              transparencyRemission:=New BlackColorRemission)
    End Function

    Public Shared Function Scattering(Optional ByVal albedo As Double = 1) As Material2D
        Return New Material2D(sourceLight:=ExactColor.Black,
                              scatteringRemission:=New ScaledColorRemission(albedo:=albedo),
                              reflectionRemission:=New BlackColorRemission,
                              transparencyRemission:=New BlackColorRemission)
    End Function

End Class
