﻿Public Class FieldInitializer
    Implements IInitializer(Of ParticleField2D)

    Private _startParticles As List(Of Particle2D)
    Public Property StartParticles() As List(Of Particle2D)
        Get
            Return _startParticles
        End Get
        Set(ByVal value As List(Of Particle2D))
            _startParticles = value
        End Set
    End Property

    Public Sub New(ByVal startParticle As Particle2D)
        _startParticles = New List(Of Particle2D)
        _startParticles.Add(startParticle)
    End Sub

    Public Sub New(ByVal startParticles As List(Of Particle2D))
        _startParticles = startParticles
    End Sub

    Public Function Initialize() As Physics.ParticleField2D Implements IInitializer(Of Physics.ParticleField2D).Initialize
        Return New ParticleField2D(forceType:=New Electric2D, particles:=Me.StartParticles)
    End Function
End Class
