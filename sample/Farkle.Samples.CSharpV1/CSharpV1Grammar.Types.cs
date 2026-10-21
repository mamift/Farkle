// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using Farkle.Builder;
using UntypedNonterminal = Farkle.Builder.Untyped.Nonterminal;

namespace Farkle.Samples.CSharpV1;

public sealed partial class CSharpV1Grammar
{
    private IGrammarSymbol _rankLeftBracket = null!;
    private UntypedNonterminal _namespaceOrTypeName = null!;
    private UntypedNonterminal _typeName = null!;
    private UntypedNonterminal _predefinedType = null!;
    private UntypedNonterminal _dimSeparators = null!;
    private UntypedNonterminal _rankSpecifier = null!;
    private UntypedNonterminal _rankSpecifiers = null!;
    private UntypedNonterminal _pointerStar = null!;
    private UntypedNonterminal _voidPointer = null!;
    private UntypedNonterminal _openType = null!;
    private UntypedNonterminal _nonExpressionType = null!;
    private UntypedNonterminal _type = null!;
    private UntypedNonterminal _returnType = null!;
    private UntypedNonterminal _typeofType = null!;

    private void DefineTypes()
    {
        // A virtual terminal produced by RankBracketTokenizer only for '[' that
        // begins a rank specifier ([] or [,]). This removes the one-token
        // lookahead ambiguity between a rank suffix and an element access/array
        // length bracket.
        _rankLeftBracket = Terminal.Virtual("rank-left-bracket")
            .AddSpecialName("rank-left-bracket");

        _namespaceOrTypeName = Nonterminal.CreateUntyped("namespace-or-type-name");
        _typeName = Nonterminal.CreateUntyped("type-name");
        _predefinedType = Nonterminal.CreateUntyped("predefined-type");
        _dimSeparators = Nonterminal.CreateUntyped("dimension-separators");
        _rankSpecifier = Nonterminal.CreateUntyped("rank-specifier");
        _rankSpecifiers = Nonterminal.CreateUntyped("rank-specifiers");
        _pointerStar = Nonterminal.CreateUntyped("pointer");
        _voidPointer = Nonterminal.CreateUntyped("void-pointer");
        _openType = Nonterminal.CreateUntyped("open-type");
        _nonExpressionType = Nonterminal.CreateUntyped("non-expression-type");
        _type = Nonterminal.CreateUntyped("type");
        _returnType = Nonterminal.CreateUntyped("return-type");
        _typeofType = Nonterminal.CreateUntyped("typeof-type");

        // The NAME precedence is used together with EXPR to force a name-only
        // parenthesized sequence to be treated as a type-name/cast prefix.
        _namespaceOrTypeName.SetProductions(
            P(_identifier).WithPrecedence(_namePrecedenceToken),
            P(_namespaceOrTypeName, ".", _identifier).WithPrecedence(_namePrecedenceToken));

        _typeName.SetProductions(
            P(_namespaceOrTypeName));

        _predefinedType.SetProductions(
            P("bool"), P("byte"), P("char"), P("decimal"), P("double"), P("float"),
            P("int"), P("long"), P("object"), P("sbyte"), P("short"), P("string"),
            P("uint"), P("ulong"), P("ushort"));

        _dimSeparators.SetProductions(
            P(","),
            P(_dimSeparators, ","));

        _rankSpecifier.SetProductions(
            P(_rankLeftBracket, "]"),
            P(_rankLeftBracket, _dimSeparators, "]"));

        _rankSpecifiers.SetProductions(
            P(_rankSpecifier),
            P(_rankSpecifiers, _rankSpecifier));

        _pointerStar.SetProductions(
            P("*"),
            P(_pointerStar, "*"));

        _voidPointer.SetProductions(
            P("void", _pointerStar));

        _openType.SetProductions(
            P(_typeName));

        // Types that cannot be parsed as expressions: predefined types (with
        // optional pointer/rank suffixes), type names with at least one suffix,
        // and void*.
        _nonExpressionType.SetProductions(
            P(_predefinedType),
            P(_predefinedType, _pointerStar),
            P(_predefinedType, _rankSpecifiers),
            P(_predefinedType, _pointerStar, _rankSpecifiers),
            P(_typeName, _pointerStar),
            P(_typeName, _rankSpecifiers),
            P(_typeName, _pointerStar, _rankSpecifiers),
            P(_voidPointer));

        _type.SetProductions(
            P(_openType),
            P(_nonExpressionType));

        _returnType.SetProductions(
            P(_type),
            P("void"));

        _typeofType.SetProductions(
            P(_type),
            P("void"));
    }
}
