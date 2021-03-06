Public Class ConstantInstance
    Private ReadOnly _Signature As ConstantSignature
    Public ReadOnly Property Signature As ConstantSignature
        Get
            Return _Signature
        End Get
    End Property

    Private ReadOnly _Expression As ConstantExpression
    Public ReadOnly Property Expression As ConstantExpression
        Get
            Return _Expression
        End Get
    End Property

    Public Sub New(signature As ConstantSignature, value As Object)
        _Signature = signature
        _Expression = Expressions.Expression.Constant(value:=value, type:=signature.Type.SystemType)
    End Sub

    Friend Function ToExpressionWithNamedType() As ExpressionWithNamedType
        Return Me.Expression.WithNamedType(Me.Signature.Type)
    End Function

    Public Function ToFunctionInstance() As FunctionInstance
        Return New FunctionInstance(Signature:=_Signature.ToFunctionSignature, invokableExpression:=_Expression)
    End Function

    Public Shared Function Create(Of T)(name As String,
                   value As T,
                   typeDictionary As TypeDictionary,
                   Optional description As String = Nothing) As ConstantInstance
        Return New ConstantInstance(Of T)(name, value, typeDictionary, description)
    End Function
End Class

Public Class ConstantInstance(Of T)
    Inherits ConstantInstance

    Public Sub New(name As String,
                   value As T,
                   typeDictionary As TypeDictionary,
                   Optional description As String = Nothing)
        MyBase.New(New ConstantSignature(name:=name, Type:=typeDictionary.GetNamedType(GetType(T)), description:=description), value:=value)
    End Sub
End Class
