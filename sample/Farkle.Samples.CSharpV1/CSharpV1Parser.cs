// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using Farkle.Builder;
using Farkle.Diagnostics;
using Farkle.Grammars;
using Farkle.Parser.Tokenizers;

namespace Farkle.Samples.CSharpV1;

/// <summary>
/// Builds a ready-to-use C# 1.0 parser that produces a
/// <see cref="CstNode"/> syntax tree. Encapsulates the grammar build, the
/// chained tokenizer that disambiguates '[' rank specifiers, and the semantic
/// provider, so callers (such as the console runner) need not know these details.
/// </summary>
public static class CSharpV1Parser
{
    /// <summary>
    /// Creates a <see cref="CharParser{T}"/> for the C# 1.0 grammar.
    /// </summary>
    /// <returns>A parser producing a <see cref="CstNode"/> on success.</returns>
    /// <exception cref="InvalidOperationException">The grammar failed to build.</exception>
    public static CharParser<CstNode> Create()
    {
        BuilderOptions options = new() { LogLevel = DiagnosticSeverity.Warning };
        options.OnDiagnostic += diagnostic => Console.Error.WriteLine(diagnostic);

        IGrammarBuilder builder = CSharpV1Grammar.CreateBuilder();
        CharParser<CstNode?> checker = builder.BuildSyntaxCheck<CstNode>(options);

        if (checker.IsFailing)
        {
            ParserResult<CstNode?> failure = checker.Parse("");
            throw new InvalidOperationException($"Failed to build the C# 1.0 grammar: {failure.Error}");
        }

        Grammar grammar = checker.GetGrammar();
        ChainedTokenizerComponent<char> rankBrackets = ChainedTokenizerComponent<char>.Create(
            grammarProvider => new RankBracketTokenizer(
                (TokenSymbolHandle)grammarProvider.GetSymbolFromSpecialName("rank-left-bracket")));

        return checker
            .WithTokenizerChain(rankBrackets, ChainedTokenizerComponent<char>.Default)
            .WithSemanticProvider(new CstSemanticProvider(grammar));
    }
}
