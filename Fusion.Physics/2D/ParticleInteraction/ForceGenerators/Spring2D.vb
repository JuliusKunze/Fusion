﻿<Serializable()>
Public Class Spring2D
    Implements IForceGenerator2D

    Public Sub New(ByVal springConstant As Double, Optional ByVal length As Double = 0)
        Me.SpringConstant = springConstant
        Me.Length = length
    End Sub

    Public Property SpringConstant As Double
    Public Property Length As Double

    Public Function Force(ByVal targetParticle As Particle2D, ByVal causeParticle As Particle2D) As Math.Vector2D Implements IForceGenerator2D.Force
        Dim connection = targetParticle.Location - causeParticle.Location

        Return -Me.SpringConstant * (connection.Length - Me.Length) * connection / connection.Length
    End Function

    Public Function PotentialEnergy(ByVal particle1 As Particle2D, ByVal particle2 As Particle2D) As Double Implements IForceGenerator2D.PotentialEnergy
        Dim connection = particle1.Location - particle2.Location
        Dim distance = (connection.Length - Me.Length)

        Return 0.5 * Me.SpringConstant * distance ^ 2
    End Function
End Class