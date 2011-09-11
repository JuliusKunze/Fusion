﻿Imports System.Runtime.CompilerServices

Public Module CompilerTools

    Private ReadOnly _ParameterBracketType As BracketType = BracketType.Round
    Public ReadOnly Property ParameterBracketType As BracketType
        Get
            Return _ParameterBracketType
        End Get
    End Property

    Private ReadOnly _ArgumentBracketType As BracketType = BracketType.Round
    Public ReadOnly Property ArgumentBracketType As BracketType
        Get
            Return _ArgumentBracketType
        End Get
    End Property

    Private ReadOnly _AllowedBracketTypes As IEnumerable(Of BracketType) = {BracketType.Round, BracketType.Square, BracketType.Curly}
    Public ReadOnly Property AllowedBracketTypes As IEnumerable(Of BracketType)
        Get
            Return _AllowedBracketTypes
        End Get
    End Property

    Private ReadOnly _TypeArgumentBracketType As BracketType = BracketType.Square
    Public ReadOnly Property TypeArgumentBracketType As BracketType
        Get
            Return _TypeArgumentBracketType
        End Get
    End Property

    Private ReadOnly _CollectionBracketType As BracketType = BracketType.Curly
    Public ReadOnly Property CollectionBracketType As BracketType
        Get
            Return _CollectionBracketType
        End Get
    End Property

    Private ReadOnly _VectorBracketType As BracketType = BracketType.Square
    Public ReadOnly Property VectorBracketType As BracketType
        Get
            Return _VectorBracketType
        End Get
    End Property

    Public Function GetParameters(parametersInBrackets As LocatedString) As IEnumerable(Of LocatedString)
        Return GetArguments(parametersInBrackets, bracketType:=_ParameterBracketType)
    End Function

    Public Function GetArguments(argumentsInBrackets As LocatedString) As IEnumerable(Of LocatedString)
        Return GetArguments(argumentsInBrackets, bracketType:=_ArgumentBracketType)
    End Function

    Public Function GetTypeArguments(typeArgumentsInBrackets As LocatedString) As IEnumerable(Of LocatedString)
        Return GetArguments(typeArgumentsInBrackets, bracketType:=_TypeArgumentBracketType)
    End Function

    Public Function GetCollectionArguments(collectionArgumentsInBrackets As LocatedString) As IEnumerable(Of LocatedString)
        Return GetArguments(collectionArgumentsInBrackets, bracketType:=_CollectionBracketType)
    End Function

    Public Function GetArguments(argumentsInBrackets As LocatedString, bracketType As BracketType) As IEnumerable(Of LocatedString)
        If Not argumentsInBrackets.IsInBrackets(bracketType:=bracketType) Then Throw New LocatedCompilerException(argumentsInBrackets, String.Format("Invalid argument enumeration: '{0}'.", argumentsInBrackets.ToString))

        Dim argumentsText = argumentsInBrackets.Substring(1, argumentsInBrackets.Length - 2)
        Return SplitIfSeparatorIsNotInBrackets(argumentsText, separator:=","c, bracketTypes:=_AllowedBracketTypes)
    End Function

    <Extension()>
    Public Function SplitIfSeparatorIsNotInBrackets(s As LocatedString, separator As Char, bracketTypes As IEnumerable(Of BracketType)) As IEnumerable(Of LocatedString)
        Dim inBracketsArray = s.GetCharIsInBracketsArray(bracketTypes:=_AllowedBracketTypes)
        Dim arguments = New List(Of LocatedString)

        Dim lastSplitCharIndex = -1
        For splitCharIndex = 0 To s.Length
            If splitCharIndex = s.Length OrElse
               (s.ToString(splitCharIndex) = separator AndAlso Not inBracketsArray(splitCharIndex)) Then
                arguments.Add(s.Substring(startIndex:=lastSplitCharIndex + 1, length:=splitCharIndex - lastSplitCharIndex - 1))
                lastSplitCharIndex = splitCharIndex
            End If
        Next
        Return arguments
    End Function

    <Extension()>
    Public Function GetStartingIdentifier(s As LocatedString) As LocatedString
        If s.Length = 0 Then Throw New LocatedCompilerException(s, _IdentifierExpectedExceptionMessage)
        If Not s.ToString.First.IsIdentifierStartChar Then Throw New LocatedCompilerException(s.Substring(0, 1), _IdentifierExpectedExceptionMessage)

        Dim nameLength = 0
        Do While nameLength < s.Length AndAlso s.Chars(nameLength).IsIdentifierChar
            nameLength += 1
        Loop

        Return s.Substring(0, length:=nameLength)
    End Function

    <Extension()>
    Public Function GetStartingType(s As LocatedString, types As NamedTypes, Optional ByRef out_rest As LocatedString = Nothing) As NamedType
        Dim typeNameWithoutParameters = s.GetStartingIdentifier()
        out_rest = s.Substring(startIndex:=typeNameWithoutParameters.ToString.Count).Trim

        If out_rest.ToString.Any AndAlso out_rest.ToString.First <> _TypeArgumentBracketType.OpeningBracket Then Return types.Parse(typeNameWithoutParameters)

        Dim charIsInBracketsArray = out_rest.GetCharIsInBracketsArray(_TypeArgumentBracketType)

        Dim charsInBracketsCount = charIsInBracketsArray.Count
        For index = 0 To charIsInBracketsArray.Count - 1
            If Not charIsInBracketsArray(index) Then
                charsInBracketsCount = index
                Exit For
            End If
        Next

        Dim argumentStrings = CompilerTools.GetTypeArguments(typeArgumentsInBrackets:=out_rest.Substring(0, charsInBracketsCount))
        out_rest = out_rest.Substring(startIndex:=charsInBracketsCount)

        Dim type = types.Parse(typeNameWithoutParameters)
        Dim typeArguments = argumentStrings.Select(Function(argumentString) types.Parse(argumentString))

        Try
            Return type.MakeGenericType(typeArguments)
        Catch ex As CompilerException
            Throw ex.Locate(locatedString:=s)
        End Try
    End Function

    <Extension()>
    Public Function IsValidIdentifier(s As String) As Boolean
        If s = "" Then Return False

        Return s.First.IsIdentifierStartChar AndAlso s.All(Function(c) c.IsIdentifierChar)
    End Function

    <Extension()>
    Public Function IsValidIdendifierStartCharacter(s As String) As Boolean
        If s = "" Then Return False

        Return Char.IsLetter(s.First) AndAlso s.All(Function(c) Char.IsLetterOrDigit(c))
    End Function

    <Extension()>
    Public Function GetCharIsInBracketsArray(s As LocatedString) As Boolean()
        Return s.GetCharIsInBracketsArray(BracketType:=BracketType.Round)
    End Function

    <Extension()>
    Public Function GetCharIsInBracketsArray(s As LocatedString, bracketType As BracketType) As Boolean()
        Return s.GetCharIsInBracketsArray(bracketTypes:={bracketType})
    End Function

    <Extension()>
    Public Function GetCharIsInBracketsArray(s As LocatedString, bracketTypes As IEnumerable(Of BracketType)) As Boolean()
        Dim _CharIsInBrackets(s.Length - 1) As Boolean
        Dim bracketDepth = 0
        Dim openedBracketTypes = New Stack(Of BracketType)

        For charIndex = 0 To s.Length - 1
            Dim character = s.Chars(charIndex)
            Dim openingBracketType = bracketTypes.Where(Function(bracketType) bracketType.OpeningBracket = character).SingleOrDefault
            If openingBracketType IsNot Nothing Then
                openedBracketTypes.Push(openingBracketType)
                bracketDepth += 1
            End If

            If bracketDepth > 0 Then
                _CharIsInBrackets(charIndex) = True
            End If

            Dim closingBracketType = bracketTypes.Where(Function(bracketType) bracketType.ClosingBracket = character).SingleOrDefault
            If closingBracketType IsNot Nothing Then
                If Not openedBracketTypes.Any Then Throw New InvalidTermException(s, message:="End of term expected.")
                If openedBracketTypes.Pop IsNot closingBracketType Then Throw New InvalidTermException(s, message:="Brackets not matching.")
                bracketDepth -= 1
            End If
        Next

        If bracketDepth > 0 Then Throw New InvalidTermException(s, message:="')' expected.")

        Return _CharIsInBrackets
    End Function

    <Extension()>
    Public Function IsInBrackets(s As LocatedString) As Boolean
        Return s.IsInBrackets(bracketType:=BracketType.Round)
    End Function

    <Extension()>
    Public Function IsInBrackets(s As LocatedString, bracketType As BracketType) As Boolean
        Return s.IsInBrackets(bracketTypes:={bracketType})
    End Function

    <Extension()>
    Public Function IsInBrackets(s As LocatedString, bracketTypes As IEnumerable(Of BracketType)) As Boolean
        If s.ToString.Count < 2 Then Return False
        If Not bracketTypes.Select(Function(bracketType) bracketType.OpeningBracket).Contains(s.ToString.First) Then Return False
        If Not bracketTypes.Select(Function(bracketType) bracketType.ClosingBracket).Contains(s.ToString.Last) Then Return False

        Dim charIsInBracketsArray = s.GetCharIsInBracketsArray(bracketTypes:=bracketTypes)

        Return charIsInBracketsArray.All(Function(inBrackets) inBrackets)
    End Function

    Private Const _IdentifierExpectedExceptionMessage = "Identifier expected."

    Public Function GetStartingTypedAndNamedVariable(text As LocatedString, types As NamedTypes, Optional ByRef out_rest As LocatedString = Nothing) As TypeAndName
        Dim trim = text.TrimStart

        Dim rest As LocatedString = Nothing
        Dim type = CompilerTools.GetStartingType(trim, types:=types, out_rest:=rest)

        Dim rest2 = rest.TrimStart
        Dim name = CompilerTools.GetStartingIdentifier(rest2)

        out_rest = rest2.Substring(startIndex:=name.Length)

        Return New TypeAndName(name:=name.ToString, type:=type)
    End Function

    Public Function IdentifierEquals(a As String, b As String) As Boolean
        Return String.Equals(a, b, StringComparison.OrdinalIgnoreCase)
    End Function

    <Extension()>
    Public Function InBrackets(s As String) As String
        Return s.InBrackets(BracketType.Round)
    End Function

    <Extension()>
    Public Function InBrackets(s As String, bracketType As BracketType) As String
        Return bracketType.Round.OpeningBracket & s & bracketType.Round.ClosingBracket
    End Function

    <Extension()>
    Public Function ToAnalized(s As String) As AnalizedString
        Return New AnalizedString(s)
    End Function

    <Extension()>
    Public Function ToLocated(s As String) As LocatedString
        Return s.ToAnalized.ToLocated
    End Function

    <Extension()>
    Public Function GetSurroundingIdentifier(s As LocatedString, index As Integer) As LocatedString
        If index < 0 OrElse index > s.Length Then Throw New ArgumentOutOfRangeException("index")

        Dim startIndex = 0
        Dim endIndex = s.Length

        If s.ToString = "" Then Return s.Substring(startIndex:=index, length:=0)

        If index = 0 Then
            For i = 0 To s.Length - 1
                If Not s(i).IsIdentifierChar Then
                    endIndex = i
                    Exit For
                End If
            Next

            If Not s(startIndex).IsIdentifierStartChar Then Return s.Substring(startIndex:=index, length:=0)
        ElseIf index = s.Length OrElse Not s(index).IsIdentifierChar Then
            endIndex = index

            For i = index - 1 To 0 Step -1
                If Not s(i).IsIdentifierChar Then
                    startIndex = i + 1
                    Exit For
                End If
            Next


        Else

            For i = index To 0 Step -1
                If Not s(i).IsIdentifierChar Then
                    startIndex = i + 1
                    Exit For
                End If
            Next

            For i = index + 1 To s.Length - 1
                If Not s(i).IsIdentifierChar Then
                    endIndex = i
                    Exit For
                End If
            Next
        End If

        If startIndex < s.Length - 1 AndAlso Not s(startIndex).IsIdentifierStartChar Then Return s.Substring(startIndex:=index, length:=0)

        Return s.Substring(startIndex:=startIndex, length:=endIndex - startIndex)
    End Function

End Module
