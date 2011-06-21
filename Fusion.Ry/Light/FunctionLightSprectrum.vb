﻿Public Class FunctionLightSprectrum
    Implements ILightSpectrum

    Private ReadOnly _IntensityFunction As IntensityFunction

    Public Delegate Function IntensityFunction(ByVal wavelength As Double) As Double

    Public Sub New(ByVal intensityFunction As IntensityFunction)
        _IntensityFunction = intensityFunction
    End Sub

    Public Function GetIntensity(ByVal wavelength As Double) As Double Implements ILightSpectrum.GetIntensity
        Return _IntensityFunction.Invoke(wavelength)
    End Function

End Class
