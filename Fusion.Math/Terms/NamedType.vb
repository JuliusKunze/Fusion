﻿Public Class NamedType

    Private ReadOnly _Name As String
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Private ReadOnly _SystemType As Type
    Public ReadOnly Property SystemType As Type
        Get
            If _IsDelegateType Then Throw New InvalidOperationException("The type must be a non delegate type.")

            Return _SystemType
        End Get
    End Property

    Private ReadOnly _DelegateType As DelegateType
    Public ReadOnly Property DelegateType As DelegateType
        Get
            If Not _IsDelegateType Then Throw New InvalidOperationException("The type must be a delegate type.")

            Return _DelegateType
        End Get
    End Property

    Private ReadOnly _IsDelegateType As Boolean
    Public ReadOnly Property IsDelegateType As Boolean
        Get
            Return _IsDelegateType
        End Get
    End Property

    Public Sub New(name As String, systemType As System.Type)
        _IsDelegateType = False
        _Name = name
        _SystemType = systemType
    End Sub

    Public Sub New(name As String, delegateType As DelegateType)
        _IsDelegateType = True
        _Name = name
        _DelegateType = delegateType
    End Sub

    Public Sub CheckIsAssignableFrom(other As NamedType)
        If _IsDelegateType Then
            If Not other.IsDelegateType Then Me.ThrowNotAssignableFromException(other.Name)

            Me.DelegateType.CheckIsAssignableFrom(other.DelegateType)
        Else
            If Not Me.SystemType.IsAssignableFrom(other.SystemType) Then Me.ThrowNotAssignableFromException(other.Name)
        End If
    End Sub

    Private Const _KeyWord = "delegate"

    Public Shared Function NamedDelegateTypeFromText(text As String, typeContext As NamedTypes) As NamedType
        Dim trimmed = text.Trim
        If Not trimmed.StartsWith(_KeyWord, StringComparison.OrdinalIgnoreCase) Then Throw New ArgumentException("text", "Invalid delegate declaration.")
        Dim rest = trimmed.Substring(startIndex:=_KeyWord.Count)
        Dim signature = FunctionSignature.FromText(text:=rest, typeContext:=typeContext)

        Return signature.AsNamedDelegateType
    End Function

    Private Function ThrowNotAssignableFromException(otherName As String) As ArgumentException
        Throw New ArgumentException(String.Format("Type '{0}' is not assignable to type '{1}'.", otherName, Me.Name))
    End Function

    Private Shared ReadOnly _Real As New NamedType("Real", GetType(Double))
    Public Shared ReadOnly Property Real() As NamedType
        Get
            Return _Real
        End Get
    End Property

    Private Shared ReadOnly _Vector3D As New NamedType("Vector", GetType(Vector3D))
    Public Shared ReadOnly Property Vector3D() As NamedType
        Get
            Return _Vector3D
        End Get
    End Property

End Class
