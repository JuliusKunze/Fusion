﻿Public Class ConstantAssignment
    Inherits Assignment

    Public Sub New(definition As LocatedString, context As TermContext)
        MyBase.New(definition:=definition, context:=context)
    End Sub

    Public Function GetNamedConstantExpression() As ConstantInstance
        Dim instance = ConstantSignature.FromText(text:=_SignatureString, typeContext:=_Context.Types)

        Dim term = New Term(term:=_TermString, Type:=instance.Type, Context:=_Context)

        Return New ConstantInstance(Signature:=instance, value:=term.GetDelegate.DynamicInvoke(Nothing))
    End Function
End Class
