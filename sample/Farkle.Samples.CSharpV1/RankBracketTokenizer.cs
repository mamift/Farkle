// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using Farkle.Grammars;
using Farkle.Parser;
using Farkle.Parser.Semantics;
using Farkle.Parser.Tokenizers;

namespace Farkle.Samples.CSharpV1;

/// <summary>
/// Disambiguates '[' between a rank specifier ([] or [,]) and an element-access or
/// array-length bracket. The C# grammar needs two tokens of lookahead for this; a
/// chained tokenizer can provide it at the lexical level.
/// </summary>
/// <remarks>
/// This tokenizer only handles '[' that is followed (ignoring whitespace and
/// comments) by ']' or ','. It emits the virtual terminal
/// <c>rank-left-bracket</c> and consumes the '['. In every other case it declines,
/// and the default tokenizer handles the '[' as an ordinary literal.
/// </remarks>
internal sealed class RankBracketTokenizer : Tokenizer<char>
{
    private readonly TokenSymbolHandle _rankLeftBracket;

    public RankBracketTokenizer(TokenSymbolHandle rankLeftBracket) => _rankLeftBracket = rankLeftBracket;

    public override bool TryGetNextToken(ref ParserInputReader<char> input,
        ITokenSemanticProvider<char> semanticProvider, out TokenizerResult result)
    {
        result = default;
        ReadOnlySpan<char> chars = input.RemainingCharacters;
        if (chars.IsEmpty || chars[0] != '[')
        {
            return false;
        }

        int index = 1;
        while (true)
        {
            if (index >= chars.Length)
            {
                if (!input.IsFinalBlock)
                {
                    input.SuspendTokenizer(this);
                }
                return false;
            }

            char c = chars[index];

            if (c is ' ' or '\t' or '\r' or '\n' or '\f' or '\v' or '\uFEFF')
            {
                index++;
                continue;
            }

            if (c == '#')
            {
                index++;
                while (index < chars.Length && chars[index] is not ('\r' or '\n'))
                {
                    index++;
                }
                if (index >= chars.Length && !input.IsFinalBlock)
                {
                    input.SuspendTokenizer(this);
                    return false;
                }
                continue;
            }

            if (c == '/')
            {
                if (index + 1 >= chars.Length)
                {
                    if (!input.IsFinalBlock)
                    {
                        input.SuspendTokenizer(this);
                    }
                    return false;
                }

                char next = chars[index + 1];
                if (next == '/')
                {
                    index += 2;
                    while (index < chars.Length && chars[index] is not ('\r' or '\n'))
                    {
                        index++;
                    }
                    continue;
                }

                if (next == '*')
                {
                    index += 2;
                    int end = -1;
                    while (index + 1 < chars.Length)
                    {
                        if (chars[index] == '*' && chars[index + 1] == '/')
                        {
                            end = index + 2;
                            break;
                        }
                        index++;
                    }
                    if (end < 0)
                    {
                        if (!input.IsFinalBlock)
                        {
                            input.SuspendTokenizer(this);
                        }
                        return false;
                    }
                    index = end;
                    continue;
                }
            }

            break;
        }

        if (chars[index] is not (']' or ','))
        {
            return false;
        }

        object? data = semanticProvider.Transform(ref input.State, _rankLeftBracket, chars[..1]);
        result = TokenizerResult.CreateSuccess(_rankLeftBracket, data, input.State.CurrentPosition);
        input.Consume(1);
        return true;
    }
}
