﻿Public Class NamedAndTypedObject


    Private ReadOnly _Name As String
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Private ReadOnly _Type As NamedType
    Public ReadOnly Property Type As NamedType
        Get
            Return _Type
        End Get
    End Property

    Public Sub New(name As String, type As NamedType)
        _Name = name
        _Type = type
    End Sub



End Class
