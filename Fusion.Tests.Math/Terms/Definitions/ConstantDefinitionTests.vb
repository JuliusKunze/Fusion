Public Class ConstantDefinitionTests

    <Test()>
    Public Sub Test()
        Dim d = New ConstantAssignment(definition:="Real a = 4", context:=TermContext.Default)
        Dim e = d.GetNamedConstantExpression
        Assert.AreEqual(e.Signature.Name, "a")
        Assert.AreEqual(CDbl(e.Expression.Value), 4)
    End Sub


    <Test()>
    Public Sub Test2()
        Dim d = New ConstantAssignment(definition:="Real b12 = (2+2)/4", context:=TermContext.Default)
        Dim e = d.GetNamedConstantExpression
        Assert.That(e.Signature.Name = "b12")
        Assert.That(CDbl(e.Expression.Value) = 1)
    End Sub

    <Test()>
    Public Sub Test3()
        Dim d = New ConstantAssignment(Definition:="Real b = (a+2)/4", context:=New TermContext(constants:={New ConstantAssignment(Definition:="Real a = 2", context:=TermContext.Default).GetNamedConstantExpression}, parameters:={}, Functions:={}, types:=NamedTypes.Default))

        Dim e = d.GetNamedConstantExpression
        Assert.That(e.Signature.Name = "b")
        Assert.That(CDbl(e.Expression.Value) = 1)
    End Sub

End Class
