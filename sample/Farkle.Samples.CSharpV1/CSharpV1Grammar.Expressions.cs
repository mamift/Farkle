// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using Farkle.Builder;
using UntypedNonterminal = Farkle.Builder.Untyped.Nonterminal;

namespace Farkle.Samples.CSharpV1;

public sealed partial class CSharpV1Grammar
{
    private UntypedNonterminal _expression = null!;
    private UntypedNonterminal _assignmentExpression = null!;
    private UntypedNonterminal _assignment = null!;
    private UntypedNonterminal _assignmentOperator = null!;
    private UntypedNonterminal _conditionalExpression = null!;
    private UntypedNonterminal _conditionalOrExpression = null!;
    private UntypedNonterminal _conditionalAndExpression = null!;
    private UntypedNonterminal _logicalOrExpression = null!;
    private UntypedNonterminal _logicalXorExpression = null!;
    private UntypedNonterminal _logicalAndExpression = null!;
    private UntypedNonterminal _equalityExpression = null!;
    private UntypedNonterminal _relationalExpression = null!;
    private UntypedNonterminal _shiftExpression = null!;
    private UntypedNonterminal _additiveExpression = null!;
    private UntypedNonterminal _multiplicativeExpression = null!;
    private UntypedNonterminal _unaryExpression = null!;
    private UntypedNonterminal _castExpression = null!;
    private UntypedNonterminal _primaryExpression = null!;
    private UntypedNonterminal _parenthesizedExpression = null!;
    private UntypedNonterminal _parenName = null!;
    private UntypedNonterminal _literal = null!;
    private UntypedNonterminal _simpleName = null!;
    private UntypedNonterminal _memberAccess = null!;
    private UntypedNonterminal _pointerMemberAccess = null!;
    private UntypedNonterminal _invocationExpression = null!;
    private UntypedNonterminal _elementAccess = null!;
    private UntypedNonterminal _thisAccess = null!;
    private UntypedNonterminal _baseAccess = null!;
    private UntypedNonterminal _postIncrementExpression = null!;
    private UntypedNonterminal _postDecrementExpression = null!;
    private UntypedNonterminal _preIncrementExpression = null!;
    private UntypedNonterminal _preDecrementExpression = null!;
    private UntypedNonterminal _objectCreationExpression = null!;
    private UntypedNonterminal _arrayCreationExpression = null!;
    private UntypedNonterminal _newBaseType = null!;
    private UntypedNonterminal _newBracketSuffix = null!;
    private UntypedNonterminal _newArrayType = null!;
    private UntypedNonterminal _typeofExpression = null!;
    private UntypedNonterminal _sizeofExpression = null!;
    private UntypedNonterminal _defaultValueExpression = null!;
    private UntypedNonterminal _checkedExpression = null!;
    private UntypedNonterminal _uncheckedExpression = null!;
    private UntypedNonterminal _stackAllocExpression = null!;
    private UntypedNonterminal _argumentList = null!;
    private UntypedNonterminal _argument = null!;
    private UntypedNonterminal _expressionList = null!;
    private UntypedNonterminal _constantExpression = null!;
    private UntypedNonterminal _variableInitializer = null!;
    private UntypedNonterminal _variableInitializerList = null!;
    private UntypedNonterminal _arrayInitializer = null!;
    private UntypedNonterminal _variableDeclarators = null!;
    private UntypedNonterminal _variableDeclarator = null!;
    private UntypedNonterminal _constantDeclarators = null!;
    private UntypedNonterminal _constantDeclarator = null!;

    private void DefineExpressions()
    {
        _expression = Nonterminal.CreateUntyped("expression");
        _assignmentExpression = Nonterminal.CreateUntyped("assignment-expression");
        _assignment = Nonterminal.CreateUntyped("assignment");
        _assignmentOperator = Nonterminal.CreateUntyped("assignment-operator");
        _conditionalExpression = Nonterminal.CreateUntyped("conditional-expression");
        _conditionalOrExpression = Nonterminal.CreateUntyped("conditional-or-expression");
        _conditionalAndExpression = Nonterminal.CreateUntyped("conditional-and-expression");
        _logicalOrExpression = Nonterminal.CreateUntyped("logical-or-expression");
        _logicalXorExpression = Nonterminal.CreateUntyped("logical-xor-expression");
        _logicalAndExpression = Nonterminal.CreateUntyped("logical-and-expression");
        _equalityExpression = Nonterminal.CreateUntyped("equality-expression");
        _relationalExpression = Nonterminal.CreateUntyped("relational-expression");
        _shiftExpression = Nonterminal.CreateUntyped("shift-expression");
        _additiveExpression = Nonterminal.CreateUntyped("additive-expression");
        _multiplicativeExpression = Nonterminal.CreateUntyped("multiplicative-expression");
        _unaryExpression = Nonterminal.CreateUntyped("unary-expression");
        _castExpression = Nonterminal.CreateUntyped("cast-expression");
        _primaryExpression = Nonterminal.CreateUntyped("primary-expression");
        _parenthesizedExpression = Nonterminal.CreateUntyped("parenthesized-expression");
        _parenName = Nonterminal.CreateUntyped("paren-name");
        _literal = Nonterminal.CreateUntyped("literal");
        _simpleName = Nonterminal.CreateUntyped("simple-name");
        _memberAccess = Nonterminal.CreateUntyped("member-access");
        _pointerMemberAccess = Nonterminal.CreateUntyped("pointer-member-access");
        _invocationExpression = Nonterminal.CreateUntyped("invocation-expression");
        _elementAccess = Nonterminal.CreateUntyped("element-access");
        _thisAccess = Nonterminal.CreateUntyped("this-access");
        _baseAccess = Nonterminal.CreateUntyped("base-access");
        _postIncrementExpression = Nonterminal.CreateUntyped("post-increment-expression");
        _postDecrementExpression = Nonterminal.CreateUntyped("post-decrement-expression");
        _preIncrementExpression = Nonterminal.CreateUntyped("pre-increment-expression");
        _preDecrementExpression = Nonterminal.CreateUntyped("pre-decrement-expression");
        _objectCreationExpression = Nonterminal.CreateUntyped("object-creation-expression");
        _arrayCreationExpression = Nonterminal.CreateUntyped("array-creation-expression");
        _newBaseType = Nonterminal.CreateUntyped("new-base-type");
        _newBracketSuffix = Nonterminal.CreateUntyped("new-bracket-suffix");
        _newArrayType = Nonterminal.CreateUntyped("new-array-type");
        _typeofExpression = Nonterminal.CreateUntyped("typeof-expression");
        _sizeofExpression = Nonterminal.CreateUntyped("sizeof-expression");
        _defaultValueExpression = Nonterminal.CreateUntyped("default-value-expression");
        _checkedExpression = Nonterminal.CreateUntyped("checked-expression");
        _uncheckedExpression = Nonterminal.CreateUntyped("unchecked-expression");
        _stackAllocExpression = Nonterminal.CreateUntyped("stackalloc-expression");
        _argumentList = Nonterminal.CreateUntyped("argument-list");
        _argument = Nonterminal.CreateUntyped("argument");
        _expressionList = Nonterminal.CreateUntyped("expression-list");
        _constantExpression = Nonterminal.CreateUntyped("constant-expression");
        _variableInitializer = Nonterminal.CreateUntyped("variable-initializer");
        _variableInitializerList = Nonterminal.CreateUntyped("variable-initializer-list");
        _arrayInitializer = Nonterminal.CreateUntyped("array-initializer");
        _variableDeclarators = Nonterminal.CreateUntyped("variable-declarators");
        _variableDeclarator = Nonterminal.CreateUntyped("variable-declarator");
        _constantDeclarators = Nonterminal.CreateUntyped("constant-declarators");
        _constantDeclarator = Nonterminal.CreateUntyped("constant-declarator");

        _expression.SetProductions(
            P(_assignmentExpression));

        _assignmentExpression.SetProductions(
            P(_conditionalExpression),
            P(_assignment));

        _assignment.SetProductions(
            P(_unaryExpression, _assignmentOperator, _expression));

        _assignmentOperator.SetProductions(
            P("="), P("+="), P("-="), P("*="), P("/="), P("%="),
            P("&="), P("|="), P("^="), P("<<="), P(">>="));

        _conditionalExpression.SetProductions(
            P(_conditionalOrExpression),
            P(_conditionalOrExpression, "?", _expression, ":", _expression));

        _conditionalOrExpression.SetProductions(
            P(_conditionalAndExpression),
            P(_conditionalOrExpression, "||", _conditionalAndExpression));

        _conditionalAndExpression.SetProductions(
            P(_logicalOrExpression),
            P(_conditionalAndExpression, "&&", _logicalOrExpression));

        _logicalOrExpression.SetProductions(
            P(_logicalXorExpression),
            P(_logicalOrExpression, "|", _logicalXorExpression));

        _logicalXorExpression.SetProductions(
            P(_logicalAndExpression),
            P(_logicalXorExpression, "^", _logicalAndExpression));

        _logicalAndExpression.SetProductions(
            P(_equalityExpression),
            P(_logicalAndExpression, "&", _equalityExpression));

        _equalityExpression.SetProductions(
            P(_relationalExpression),
            P(_equalityExpression, "==", _relationalExpression),
            P(_equalityExpression, "!=", _relationalExpression));

        _relationalExpression.SetProductions(
            P(_shiftExpression),
            P(_relationalExpression, "<", _shiftExpression),
            P(_relationalExpression, ">", _shiftExpression),
            P(_relationalExpression, "<=", _shiftExpression),
            P(_relationalExpression, ">=", _shiftExpression),
            P(_relationalExpression, "is", _type),
            P(_relationalExpression, "as", _type));

        _shiftExpression.SetProductions(
            P(_additiveExpression),
            P(_shiftExpression, "<<", _additiveExpression),
            P(_shiftExpression, ">>", _additiveExpression));

        _additiveExpression.SetProductions(
            P(_multiplicativeExpression),
            P(_additiveExpression, "+", _multiplicativeExpression),
            P(_additiveExpression, "-", _multiplicativeExpression));

        _multiplicativeExpression.SetProductions(
            P(_unaryExpression),
            P(_multiplicativeExpression, "*", _unaryExpression),
            P(_multiplicativeExpression, "/", _unaryExpression),
            P(_multiplicativeExpression, "%", _unaryExpression));

        _unaryExpression.SetProductions(
            P(_primaryExpression),
            P("+", _unaryExpression),
            P("-", _unaryExpression),
            P("!", _unaryExpression),
            P("~", _unaryExpression),
            P("*", _unaryExpression),
            P("&", _unaryExpression),
            P("++", _unaryExpression),
            P("--", _unaryExpression),
            P(_castExpression));

        // Casts to types that cannot be expressions (predefined, array, pointer,
        // void*) are unambiguous. Name-only types are handled through paren-name
        // below, so the parser can decide based on the token after ')'.
        _castExpression.SetProductions(
            P("(", _nonExpressionType, ")", _unaryExpression),
            P(_parenName, _unaryExpression));

        _primaryExpression.SetProductions(
            P(_literal),
            P(_simpleName),
            P(_parenthesizedExpression),
            P(_parenName).WithPrecedence(_castPrecedenceToken),
            P(_memberAccess),
            P(_invocationExpression),
            P(_elementAccess),
            P(_thisAccess),
            P(_baseAccess),
            P(_postIncrementExpression),
            P(_postDecrementExpression),
            P(_objectCreationExpression),
            P(_arrayCreationExpression),
            P(_typeofExpression),
            P(_sizeofExpression),
            P(_defaultValueExpression),
            P(_checkedExpression),
            P(_uncheckedExpression),
            P(_stackAllocExpression),
            P(_pointerMemberAccess));

        _parenthesizedExpression.SetProductions(
            P("(", _expression, ")"));

        _parenName.SetProductions(
            P("(", _namespaceOrTypeName, ")"));

        _literal.SetProductions(
            P(_integerLiteral),
            P(_realLiteral),
            P(_characterLiteral),
            P(_regularStringLiteral),
            P(_verbatimStringLiteral),
            P("true"),
            P("false"),
            P("null"));

        // The reduce/reduce conflict between a name-only parenthesized type and a
        // name-only parenthesized expression is resolved in favor of the type/name
        // reading via NAME > EXPR in the operator scope.
        _simpleName.SetProductions(
            P(_identifier).WithPrecedence(_exprPrecedenceToken));

        _memberAccess.SetProductions(
            P(_primaryExpression, ".", _identifier).WithPrecedence(_exprPrecedenceToken));

        _pointerMemberAccess.SetProductions(
            P(_primaryExpression, "->", _identifier));

        _invocationExpression.SetProductions(
            P(_primaryExpression, "(", ")"),
            P(_primaryExpression, "(", _argumentList, ")"));

        _elementAccess.SetProductions(
            P(_primaryExpression, "[", _expressionList, "]"));

        _thisAccess.SetProductions(
            P("this"));

        _baseAccess.SetProductions(
            P("base", ".", _identifier),
            P("base", "[", _expressionList, "]"));

        _postIncrementExpression.SetProductions(
            P(_primaryExpression, "++"));

        _postDecrementExpression.SetProductions(
            P(_primaryExpression, "--"));

        _preIncrementExpression.SetProductions(
            P("++", _unaryExpression));

        _preDecrementExpression.SetProductions(
            P("--", _unaryExpression));

        // `new` parses its base type without rank suffixes. Every following
        // bracket is then an explicit array dimension (or a rank specifier), which
        // avoids the reduce/shift ambiguity between a rank suffix and the array
        // length bracket. Both `new int[5]` and `new int[] { ... }` are supported.
        _objectCreationExpression.SetProductions(
            P("new", _newBaseType, "(", ")"),
            P("new", _newBaseType, "(", _argumentList, ")"));

        _arrayCreationExpression.SetProductions(
            P("new", _newArrayType).WithPrecedence(_arrayPrecedenceToken),
            P("new", _newArrayType, _arrayInitializer));

        _newBaseType.SetProductions(
            P(_openType),
            P(_predefinedType),
            P(_predefinedType, _pointerStar),
            P(_typeName, _pointerStar),
            P(_voidPointer));

        // Rank brackets use the virtual token emitted by RankBracketTokenizer;
        // length brackets are ordinary '[' tokens.
        _newBracketSuffix.SetProductions(
            P(_rankLeftBracket, "]"),
            P(_rankLeftBracket, _dimSeparators, "]"),
            P("[", _expressionList, "]"));

        _newArrayType.SetProductions(
            P(_newBaseType, _newBracketSuffix),
            P(_newArrayType, _newBracketSuffix));

        _typeofExpression.SetProductions(
            P("typeof", "(", _typeofType, ")"));

        _sizeofExpression.SetProductions(
            P("sizeof", "(", _type, ")"));

        _defaultValueExpression.SetProductions(
            P("default", "(", _type, ")"));

        _checkedExpression.SetProductions(
            P("checked", "(", _expression, ")"));

        _uncheckedExpression.SetProductions(
            P("unchecked", "(", _expression, ")"));

        _stackAllocExpression.SetProductions(
            P("stackalloc", _type, "[", _expression, "]"));

        _argumentList.SetProductions(
            P(_argument),
            P(_argumentList, ",", _argument));

        _argument.SetProductions(
            P(_expression),
            P("ref", _expression),
            P("out", _expression));

        _expressionList.SetProductions(
            P(_expression),
            P(_expressionList, ",", _expression));

        _constantExpression.SetProductions(
            P(_conditionalExpression));

        _variableInitializer.SetProductions(
            P(_expression),
            P(_arrayInitializer));

        _variableInitializerList.SetProductions(
            P(_variableInitializer),
            P(_variableInitializerList, ",", _variableInitializer));

        _arrayInitializer.SetProductions(
            P("{", "}"),
            P("{", _variableInitializerList, "}"),
            P("{", _variableInitializerList, ",", "}"));

        _variableDeclarators.SetProductions(
            P(_variableDeclarator),
            P(_variableDeclarators, ",", _variableDeclarator));

        _variableDeclarator.SetProductions(
            P(_identifier),
            P(_identifier, "=", _variableInitializer));

        _constantDeclarators.SetProductions(
            P(_constantDeclarator),
            P(_constantDeclarators, ",", _constantDeclarator));

        _constantDeclarator.SetProductions(
            P(_identifier, "=", _constantExpression));
    }
}
