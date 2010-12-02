﻿Public Class UndirectionalLightSource
    Implements ILightSource

    Public Sub New(ByVal color As Color)
        Me.New(New ExactColor(color))
    End Sub

    Public Sub New(ByVal color As ExactColor)
        Me.Color = color
    End Sub

    Public Property Color As ExactColor

    Public Function LightColor(ByVal surfacePoint As SurfacePoint) As ExactColor Implements ILightSource.LightColor
        Return Me.Color
    End Function

End Class