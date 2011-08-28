﻿Public Class TermContext

    Private ReadOnly _Types As NamedTypes
    Public ReadOnly Property Types As NamedTypes
        Get
            Return _Types
        End Get
    End Property

    Private ReadOnly _Constants As IEnumerable(Of ConstantInstance)
    Public ReadOnly Property Constants As IEnumerable(Of ConstantInstance)
        Get
            Return _Constants
        End Get
    End Property

    Private ReadOnly _Parameters As IEnumerable(Of NamedParameter)
    Public ReadOnly Property Parameters As IEnumerable(Of NamedParameter)
        Get
            Return _Parameters
        End Get
    End Property

    Private ReadOnly _Functions As IEnumerable(Of FunctionInstance)
    Public ReadOnly Property Functions As IEnumerable(Of FunctionInstance)
        Get
            Return _Functions
        End Get
    End Property

    Private _GroupedFunctions As IEnumerable(Of IGrouping(Of String, FunctionInstance))
    Public ReadOnly Property GroupedFunctionsAndDelegateParameters As IEnumerable(Of IGrouping(Of String, FunctionInstance))
        Get
            If _GroupedFunctions Is Nothing Then
                Dim delegateParameterFunctions =
                        From parameter In Me.Parameters
                        Where parameter.Type.IsDelegate
                        Select parameter.ToFunctionInstance

                _GroupedFunctions = _Functions.Concat(delegateParameterFunctions).GroupBy(Function(instance) instance.Signature.Name).ToArray
            End If

            Return _GroupedFunctions
        End Get
    End Property

    Public Sub New(Optional constants As IEnumerable(Of ConstantInstance) = Nothing,
                   Optional parameters As IEnumerable(Of NamedParameter) = Nothing,
                   Optional functions As IEnumerable(Of FunctionInstance) = Nothing,
                   Optional types As NamedTypes = Nothing)
        _Constants = If(constants Is Nothing, Enumerable.Empty(Of ConstantInstance), constants)
        _Parameters = If(parameters Is Nothing, Enumerable.Empty(Of NamedParameter), parameters)
        _Functions = If(functions Is Nothing, Enumerable.Empty(Of FunctionInstance), functions)
        _Types = If(types Is Nothing, NamedTypes.Empty, types)
    End Sub

    Private Shared ReadOnly _Default As New TermContext(Constants:={New ConstantInstance(Of Boolean)("True", True, TypeNamedTypeDictionary.Default),
                                                                    New ConstantInstance(Of Boolean)("False", False, TypeNamedTypeDictionary.Default),
                                                                    New ConstantInstance(Of Double)("Pi", System.Math.PI, TypeNamedTypeDictionary.Default),
                                                                    New ConstantInstance(Of Double)("E", System.Math.E, TypeNamedTypeDictionary.Default)
                                                                   },
                                                        Functions:={New FunctionInstance(Of Func(Of Double, Double))("Sqrt", Function(x) System.Math.Sqrt(x), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double))("Exp", Function(x) System.Math.Exp(x), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double))("Sin", Function(x) System.Math.Sin(x), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double))("Cos", Function(x) System.Math.Cos(x), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double))("Tan", Function(x) System.Math.Tan(x), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double))("Asin", Function(x) System.Math.Asin(x), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double))("Acos", Function(x) System.Math.Acos(x), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double))("Abs", Function(x) System.Math.Abs(x), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double, Double))("Max", Function(a, b) System.Math.Max(a, b), TypeNamedTypeDictionary.Default),
                                                                    New FunctionInstance(Of Func(Of Double, Double, Double))("Min", Function(a, b) System.Math.Min(a, b), TypeNamedTypeDictionary.Default)
                                                                   },
                                                        Types:=NamedTypes.Default)

    Public Shared ReadOnly Property [Default] As TermContext
        Get
            Return _Default
        End Get
    End Property

    Public Function Merge(second As TermContext) As TermContext
        Return New TermContext(Constants:=_Constants.Concat(second._Constants),
                               Functions:=_Functions.Concat(second._Functions),
                               Parameters:=_Parameters.Concat(second._Parameters),
                               Types:=_Types.Merge(second.Types))
    End Function

    Public Function ParseSingleFunctionWithName(name As String) As FunctionInstance
        Dim matchingGroup = Me.GetMatchingFunctionGroup(name)
        If matchingGroup.Count > 1 Then Throw New CompilerException(String.Format("There are multiple definitions for function with name '{0}'.", name))

        Return matchingGroup.Single
    End Function

    Public Function ParseFunction(functionCall As FunctionCall) As FunctionInstance
        Dim matchingFunctionGroup = Me.GetMatchingFunctionGroup(functionCall.FunctionName)
        Dim matchingFunctions = matchingFunctionGroup.Where(Function(instance) instance.Signature.DelegateType.Parameters.Count = functionCall.Arguments.Count)
        If Not matchingFunctions.Any Then Throw New CompilerException(String.Format("Function '{0}' with parameter count {1} not defined in this context.", functionCall.FunctionName, functionCall.Arguments.Count))
        Return matchingFunctions.Single
    End Function

    Private Function GetMatchingFunctionGroup(ByVal functionName As String) As IGrouping(Of String, FunctionInstance)
        Dim matchingFunctionGroups = Me.GroupedFunctionsAndDelegateParameters.Where(Function(group) CompilerTools.IdentifierEquals(group.Key, functionName))
        If Not matchingFunctionGroups.Any Then Throw New CompilerException(String.Format("Function '{0}' not defined in this context.", functionName))
        Return matchingFunctionGroups.Single
    End Function

    Public Function TryParseConstant(name As String) As ConstantInstance
        Dim matchingConstants = From constant In Me.Constants Where CompilerTools.IdentifierEquals(name, constant.Signature.Name)
        If Not matchingConstants.Any Then Return Nothing

        Return matchingConstants.Single
    End Function

    Public Function TryParseParameter(name As String) As NamedParameter
        Dim matchingParameters = From parameter In Me.Parameters Where CompilerTools.IdentifierEquals(name, parameter.Name)
        If Not matchingParameters.Any Then Return Nothing

        Return matchingParameters.Single
    End Function

End Class
