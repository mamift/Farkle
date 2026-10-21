// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using Farkle.Builder;
using Farkle.Builder.OperatorPrecedence;
using UntypedNonterminal = Farkle.Builder.Untyped.Nonterminal;

namespace Farkle.Samples.CSharpV1;

/// <summary>
/// The C# 1.0/1.1 grammar. The grammar is intentionally untyped; a
/// <see cref="CstSemanticProvider"/> reconstructs the syntax tree from the
/// built grammar.
/// </summary>
public sealed partial class CSharpV1Grammar
{
    private IGrammarSymbol _identifier = null!;
    private IGrammarSymbol _integerLiteral = null!;
    private IGrammarSymbol _realLiteral = null!;
    private IGrammarSymbol _characterLiteral = null!;
    private IGrammarSymbol _regularStringLiteral = null!;
    private IGrammarSymbol _verbatimStringLiteral = null!;

    private UntypedNonterminal _compilationUnit = null!;

    private object _castPrecedenceToken = null!;
    private object _ifPrecedenceToken = null!;
    private object _arrayPrecedenceToken = null!;
    private object _switchSectionPrecedenceToken = null!;
    private object _exprPrecedenceToken = null!;
    private object _namePrecedenceToken = null!;

    private CSharpV1Grammar()
    {
        _castPrecedenceToken = new object();
        _ifPrecedenceToken = new object();
        _arrayPrecedenceToken = new object();
        _switchSectionPrecedenceToken = new object();
        _exprPrecedenceToken = new object();
        _namePrecedenceToken = new object();

        DefineTerminals();
        DefineTypes();
        DefineExpressions();
        DefineStatements();
        DefineDeclarations();
    }

    /// <summary>Creates the grammar builder. A fresh grammar is produced on every call.</summary>
    public static IGrammarBuilder CreateBuilder() => new CSharpV1Grammar().Configure();

    private IGrammarBuilder Configure()
    {
        OperatorScope scope = new(
            canResolveReduceReduceConflicts: true,
            new PrecedenceOnly(
                "++", "--",
                "+", "-", "*", "/", "%", "<<", ">>", "<", ">", "<=", ">=", "==", "!=",
                "&", "^", "|", "&&", "||", "?", ":",
                "=", "+=", "-=", "*=", "/=", "%=", "&=", "|=", "^=", "<<=", ">>=",
                "is", "as", "case", "default",
                _ifPrecedenceToken),
            new PrecedenceOnly("else"),
            new PrecedenceOnly(_castPrecedenceToken),
            // Array creation is lower than '[': when a '[' follows the last
            // dimension of a new-array expression, it continues the array's
            // bracket suffixes rather than starting an element access.
            new PrecedenceOnly(_arrayPrecedenceToken),
            new PrecedenceOnly("(", "["),
            new PrecedenceOnly(_exprPrecedenceToken),
            new PrecedenceOnly(_namePrecedenceToken),
            // A completed switch section is reduced (and a new section starts) in
            // preference to shifting case/default as another label.
            new PrecedenceOnly(_switchSectionPrecedenceToken));

        return _compilationUnit
            .AddLineComment("//")
            .AddBlockComment("/*", "*/")
            // Preprocessor directives are line-based. Farkle has no conditional
            // compilation hook, so '#' lines are ignored as comments. See REPORT.md.
            .AddLineComment("#")
            .AddNoiseSymbol("vertical-tab", Regex.OneOf('\v'))
            .AddNoiseSymbol("form-feed", Regex.OneOf('\f'))
            .AddNoiseSymbol("byte-order-mark", Regex.OneOf('\uFEFF'))
            .WithGrammarName("C# 1.0")
            .WithOperatorScope(scope);
    }

    private void DefineTerminals()
    {
        _identifier = Terminal.Create(
            "identifier",
            Regex.FromRegexString("@?[A-Za-z_][A-Za-z0-9_]*"));

        // Decimal and hexadecimal integer literals, with any valid combination of
        // the unsigned (u/U) and long (l/L) suffixes.
        _integerLiteral = Terminal.Create(
            "integer-literal",
            Regex.Choice(
                Regex.FromRegexString("0[xX][0-9a-fA-F]+([uU][lL]?|[lL][uU]?)?"),
                Regex.FromRegexString("[0-9]+([uU][lL]?|[lL][uU]?)?")));

        // Decimal real literals. A dot must be followed by at least one digit, so
        // "1.ToString()" still lexes as the integer 1 followed by a member access.
        _realLiteral = Terminal.Create(
            "real-literal",
            Regex.Choice(
                Regex.FromRegexString("[0-9]+\\.[0-9]+([eE][+-]?[0-9]+)?[fFdDmM]?"),
                Regex.FromRegexString("[0-9]+[eE][+-]?[0-9]+[fFdDmM]?"),
                Regex.FromRegexString("[0-9]+[fFdDmM]")));

        Regex hexDigit = Regex.OneOf("0123456789abcdefABCDEF");
        Regex escapeSequence =
            Regex.Literal('\\') +
            Regex.Choice(
                Regex.OneOf("'\"\\0abfnrtv"),
                Regex.Literal('x') + hexDigit.Between(1, 4),
                Regex.Literal('u') + hexDigit.Repeat(4));

        _characterLiteral = Terminal.Create(
            "character-literal",
            Regex.Literal('\'') +
            (Regex.NotOneOf('\'', '\\', '\r', '\n') | escapeSequence) +
            Regex.Literal('\''));

        _regularStringLiteral = Terminal.Create(
            "string-literal",
            Regex.Literal('"') +
            (Regex.NotOneOf('"', '\\', '\r', '\n') | escapeSequence).ZeroOrMore() +
            Regex.Literal('"'));

        _verbatimStringLiteral = Terminal.Create(
            "verbatim-string-literal",
            Regex.Literal('@') +
            Regex.Literal('"') +
            (Regex.NotOneOf('"') | Regex.Literal("\"\"")).ZeroOrMore() +
            Regex.Literal('"'));
    }

    private static ProductionBuilder P(params object[] members) => new(members.AsSpan());
}
