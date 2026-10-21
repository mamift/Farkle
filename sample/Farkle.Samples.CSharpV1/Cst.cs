// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using System.Collections.Immutable;
using Farkle.Grammars;
using Farkle.Parser;
using Farkle.Parser.Semantics;

namespace Farkle.Samples.CSharpV1;

/// <summary>
/// A node of the concrete syntax tree produced by the C# 1.0 parser.
/// Nonterminal nodes have <see cref="Text"/> set to <see langword="null"/>;
/// terminal nodes have no children and hold the matched text.
/// </summary>
public sealed class CstNode
{
    public CstNode(string name, string? text, TextPosition position, TextPosition endPosition,
        ImmutableArray<CstNode> children)
    {
        Name = name;
        Text = text;
        Position = position;
        EndPosition = endPosition;
        Children = children;
    }

    /// <summary>The nonterminal or terminal name as it appears in the grammar.</summary>
    public string Name { get; }

    /// <summary>The matched text, for terminal nodes; otherwise <see langword="null"/>.</summary>
    public string? Text { get; }

    public TextPosition Position { get; }

    public TextPosition EndPosition { get; }

    public ImmutableArray<CstNode> Children { get; }

    public bool IsToken => Text is not null;

    public override string ToString() =>
        IsToken
            ? $"{Name}: \"{Text}\""
            : $"{Name} ({Children.Length} children)";
}

/// <summary>
/// Builds a <see cref="CstNode"/> for every token and reduction using the grammar's
/// introspection API. This lets the grammar itself stay untyped while still producing
/// a complete syntax tree.
/// </summary>
public sealed class CstSemanticProvider : ISemanticProvider<char, CstNode>
{
    private readonly Grammar _grammar;

    public CstSemanticProvider(Grammar grammar) => _grammar = grammar;

    public object? Transform(ref ParserState parserState, TokenSymbolHandle symbol, ReadOnlySpan<char> characters)
    {
        TokenSymbolDefinition definition = _grammar.GetTokenSymbol(symbol);

        // Whitespace, comments, and the '#' directive noise are not part of any
        // production; avoid allocating nodes for them.
        if ((definition.Attributes & TokenSymbolAttributes.Noise) != 0)
        {
            return null;
        }

        TextPosition start = parserState.CurrentPosition;
        TextPosition end = parserState.GetPositionAfter(characters);
        return new CstNode(definition.Name, characters.ToString(), start, end, ImmutableArray<CstNode>.Empty);
    }

    public object? Fuse(ref ParserState parserState, ProductionHandle production, Span<object?> members)
    {
        ProductionDefinition definition = _grammar.GetProduction(production);
        ImmutableArray<CstNode>.Builder children = ImmutableArray.CreateBuilder<CstNode>(members.Length);
        foreach (object? member in members)
        {
            if (member is CstNode node)
            {
                children.Add(node);
            }
        }

        ImmutableArray<CstNode> array = children.ToImmutable();
        TextPosition start = array.Length > 0 ? array[0].Position : parserState.CurrentPosition;
        TextPosition end = array.Length > 0 ? array[^1].EndPosition : start;
        return new CstNode(definition.Head.Name, null, start, end, array);
    }
}
