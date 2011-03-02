﻿''' <summary>
''' Transforms a ray of a (stationary) reference frame into one which relativly moves
''' with a constant velocity in x-direction.
''' </summary>
''' The origins of the reference frames are equal, their times are equal.
''' <remarks></remarks>
Public Class RelativisticRayTransformation

    Public Sub New(ByVal relativeXVelocityInC As Double)
        If Abs(relativeXVelocityInC) > 1 Then Throw New ArgumentException("A velocity can not be greater than the light velocity.")

        Me.RelativeXVelocityInC = relativeXVelocityInC
        _gamma = 1 / (1 - Me.Beta ^ 2)
    End Sub

    Public Property RelativeXVelocityInC As Double

    Private ReadOnly Property Beta As Double
        Get
            Return Me.RelativeXVelocityInC
        End Get
    End Property

    Private _gamma As Double
    Private ReadOnly Property Gamma As Double
        Get
            Return _gamma
        End Get
    End Property

    ''' <summary>
    ''' The ray in the moved reference frame.
    ''' </summary>
    ''' <param name="ray">The ray in the stationary reference frame.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TransformedRay(ByVal ray As Ray) As Ray
        Dim directionInStationaryFrame = ray.NormalizedDirection
        Dim directionInMovedFrame = New Vector3D(x:=directionInStationaryFrame.X,
                                           y:=directionInStationaryFrame.Y,
                                           z:=Me.Gamma * (directionInStationaryFrame.Z - Me.Beta * directionInStationaryFrame.Length))
        Return New Ray(origin:=Ray.Origin, direction:=directionInMovedFrame)
    End Function
End Class
