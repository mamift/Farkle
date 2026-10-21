// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using System.Diagnostics;
using Farkle.Builder;
using Farkle.Diagnostics;
using Farkle.Grammars;
using Farkle.Parser.Tokenizers;

namespace Farkle.Samples.CSharpV1;

public static class Program
{
    private readonly record struct TestCase(string Name, string Source, bool ShouldParse);

    public static int Main(string[] args)
    {
        BuilderOptions options = new() { LogLevel = DiagnosticSeverity.Warning };
        options.OnDiagnostic += diagnostic => Console.Error.WriteLine(diagnostic);

        Stopwatch buildTimer = Stopwatch.StartNew();
        IGrammarBuilder builder = CSharpV1Grammar.CreateBuilder();
        CharParser<CstNode?> checker = builder.BuildSyntaxCheck<CstNode>(options);
        buildTimer.Stop();

        if (checker.IsFailing)
        {
            ParserResult<CstNode?> failure = checker.Parse("");
            Console.Error.WriteLine(failure.Error);
            return 1;
        }

        Grammar grammar = checker.GetGrammar();
        ChainedTokenizerComponent<char> rankBrackets = ChainedTokenizerComponent<char>.Create(
            grammarProvider => new RankBracketTokenizer(
                (TokenSymbolHandle)grammarProvider.GetSymbolFromSpecialName("rank-left-bracket")));

        CharParser<CstNode> parser = checker
            .WithTokenizerChain(rankBrackets, ChainedTokenizerComponent<char>.Default)
            .WithSemanticProvider(new CstSemanticProvider(grammar));

        PrintGrammarStats(grammar);
        Console.WriteLine($"Grammar built in {buildTimer.ElapsedMilliseconds} ms.");

        if (args.Length == 0)
        {
            int result = RunSelfTests(parser);
            string inputs = Path.Combine(AppContext.BaseDirectory, "Inputs");
            if (Directory.Exists(inputs))
            {
                foreach (string file in Directory.GetFiles(inputs, "*.cs"))
                {
                    if (!RunFile(parser, file))
                    {
                        result = 1;
                    }
                }
            }
            return result;
        }

        if (args is ["--self-test"])
        {
            return RunSelfTests(parser);
        }

        if (args is ["--tree", var treePath])
        {
            return RunTree(parser, treePath) ? 0 : 1;
        }

        int exitCode = 0;
        foreach (string path in args)
        {
            if (!RunFile(parser, path))
            {
                exitCode = 1;
            }
        }
        return exitCode;
    }

    private static void PrintGrammarStats(Grammar grammar)
    {
        Console.WriteLine(
            $"Grammar: {grammar.TokenSymbols.Count} token symbols, " +
            $"{grammar.Nonterminals.Count} nonterminals, " +
            $"{grammar.Productions.Count} productions, " +
            $"{grammar.LrStateMachine?.Count ?? 0} LR states, " +
            $"{grammar.DfaOnChar?.Count ?? 0} DFA states.");
    }

    private static int RunSelfTests(CharParser<CstNode> parser)
    {
        int passed = 0;
        int failed = 0;

        foreach (TestCase test in s_testCases)
        {
            ParserResult<CstNode> result = parser.Parse(test.Source);
            bool ok = result.IsSuccess == test.ShouldParse;
            if (ok)
            {
                passed++;
                Console.WriteLine($"PASS {test.Name}");
            }
            else
            {
                failed++;
                Console.WriteLine($"FAIL {test.Name}: expected " +
                    $"{(test.ShouldParse ? "success" : "failure")}, got " +
                    $"{(result.IsSuccess ? "success" : $"failure ({result.Error})")}");
            }
        }

        // Exercise the custom tokenizer across streamed input, where a '[' can be
        // split from the characters that determine whether it is a rank bracket.
        const string streamSource =
            "class C { void M() { int[] a = new int[] { 1, 2 }; int x = a[0]; int[,] b; } }";
        using (SlowTextReader reader = new(streamSource))
        {
            ParserResult<CstNode> streamed = parser.Parse(reader);
            if (streamed.IsSuccess)
            {
                passed++;
                Console.WriteLine("PASS streamed input");
            }
            else
            {
                failed++;
                Console.WriteLine($"FAIL streamed input: {streamed.Error}");
            }
        }

        Console.WriteLine($"{passed} passed, {failed} failed.");
        return failed == 0 ? 0 : 1;
    }

    private static bool RunTree(CharParser<CstNode> parser, string path)
    {
        string resolved = ResolvePath(path);
        if (!TryParseFile(parser, path, resolved, out ParserResult<CstNode> result))
        {
            return false;
        }
        if (result.IsError)
        {
            Console.Error.WriteLine($"{path}: {result.Error}");
            return false;
        }

        Console.WriteLine($"{path}:");
        PrintTree(result.Value, 0, maxDepth: 8);
        return true;
    }

    private static bool RunFile(CharParser<CstNode> parser, string path)
    {
        string resolved = ResolvePath(path);
        if (!TryParseFile(parser, path, resolved, out ParserResult<CstNode> result))
        {
            return false;
        }
        if (result.IsError)
        {
            Console.WriteLine($"FAIL {path}: {result.Error}");
            return false;
        }

        Console.WriteLine($"OK   {path} ({CountNodes(result.Value)} CST nodes)");
        return true;
    }

    private static bool TryParseFile(CharParser<CstNode> parser, string displayPath, string resolvedPath,
        out ParserResult<CstNode> result)
    {
        try
        {
            result = parser.ParseFile(resolvedPath);
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"{displayPath}: {ex.Message}");
            result = default;
            return false;
        }
    }

    private static string ResolvePath(string path)
    {
        if (Path.IsPathRooted(path) || File.Exists(path))
        {
            return path;
        }

        string fromOutput = Path.Combine(AppContext.BaseDirectory, path);
        return File.Exists(fromOutput) ? fromOutput : path;
    }

    private static int CountNodes(CstNode node)
    {
        int count = 1;
        foreach (CstNode child in node.Children)
        {
            count += CountNodes(child);
        }
        return count;
    }

    private static void PrintTree(CstNode node, int depth, int maxDepth)
    {
        Console.Write(new string(' ', depth * 2));
        if (node.IsToken)
        {
            Console.WriteLine($"{node.Name}: \"{node.Text}\" @ {node.Position}");
        }
        else
        {
            Console.WriteLine($"{node.Name} @ {node.Position}");
            if (depth >= maxDepth)
            {
                if (node.Children.Length > 0)
                {
                    Console.WriteLine(new string(' ', (depth + 1) * 2) + "...");
                }
                return;
            }
            foreach (CstNode child in node.Children)
            {
                PrintTree(child, depth + 1, maxDepth);
            }
        }
    }

    private static readonly TestCase[] s_testCases =
    [
        new("empty file", "", true),
        new("empty class", "class C { }", true),
        new("namespace and arithmetic", """
            namespace N
            {
                class C
                {
                    int x;
                    void M() { x = 1 + 2 * 3; }
                }
            }
            """, true),
        new("closed cast", "class C { void M(object o) { int x = (int)o; } }", true),
        new("named cast", "class C { void M(object o) { Foo f = (Foo)o; } }", true),
        new("qualified named cast", "class C { void M(object o) { Foo.Bar f = (Foo.Bar)o; } }", true),
        new("cast versus subtraction", "class C { void M() { int x = (a) - b; } }", true),
        new("cast versus invocation", "class C { void M() { int x = (a)(b); } }", true),
        new("cast versus postfix", "class C { void M() { (a)++; } }", true),
        new("cast of unary", "class C { void M() { int x = (a)!b; } }", true),
        new("parenthesized member access", "class C { void M() { int x = (a.b) [c]; } }", true),
        new("array type and creation", "class C { void M() { int[] a = new int[5]; int[,] b = new int[2, 3]; } }", true),
        new("jagged array", "class C { void M() { int[][] a = new int[3][]; } }", true),
        new("array initializer", "class C { void M() { int[] a = new int[] { 1, 2, 3, }; } }", true),
        new("element access", "class C { void M() { a[0] = b[1, 2]; } }", true),
        new("parenthesized element access", "class C { void M() { (new int[5])[0] = 1; } }", true),
        new("dangling else", "class C { void M() { if (a) if (b) x(); else y(); } }", true),
        new("explicit interface", """
            interface I { void M(); }
            class C : I { void I.M() { } }
            """, true),
        new("explicit interface with predefined type", """
            interface I { string M(); }
            class C : I { string I.M() { return null; } }
            """, true),
        new("dotted interface name", """
            namespace N { interface I { void M(); } }
            class C : N.I { void N.I.M() { } }
            """, true),
        new("attributes and named arguments", """
            [assembly: Foo]
            class C
            {
                [Obsolete("x", IsError = true)]
                void M() { }
            }
            """, true),
        new("attributes in member positions", """
            class C
            {
                [return: Obsolete]
                void M([Obsolete] int x) { }
                int P { [Obsolete] get { return 0; } }
            }
            enum E { [Obsolete] A }
            """, true),
        new("modifiers and bodies", "abstract class C { public abstract void M(); extern static void E(); }", true),
        new("params and multiple declarators", "class C { int a, b; void M(params int[] xs) { } }", true),
        new("operators and conversions", """
            class C
            {
                public static C operator +(C a, C b) { return a; }
                public static implicit operator int(C c) { return 0; }
                public static explicit operator C(int x) { return null; }
                public static bool operator true(C c) { return false; }
                public static bool operator false(C c) { return false; }
            }
            """, true),
        new("properties, indexers and events", """
            class C
            {
                int P { get { return 0; } set { } }
                int this[int i] { get { return 0; } set { } }
                event EventHandler E;
                event EventHandler F { add { } remove { } }
            }
            """, true),
        new("constructors and destructor", """
            class C
            {
                C() : this(0) { }
                C(int x) : base() { }
                static C() { }
                ~C() { }
            }
            """, true),
        new("constants and fixed buffers", """
            unsafe struct S
            {
                const int N = 3;
                fixed int buffer[N];
                readonly int x;
                volatile int y;
            }
            """, true),
        new("unsafe pointer code", """
            unsafe class C
            {
                void M()
                {
                    int* p = null;
                    fixed (int* q = p) { *q = 1; }
                    int* r = stackalloc int[10];
                    int n = sizeof(int);
                }
            }
            """, true),
        new("statements", """
            class C
            {
                void M()
                {
                    int i = 0;
                    for (int j = 0; j < 10; j++) { }
                    while (i < 3) { i++; }
                    do { i--; } while (i > 0);
                    foreach (int x in a) { }
                    if (i == 0) { } else { }
                    switch (i)
                    {
                        case 1:
                        case 2:
                            i++;
                            break;
                        default:
                            i--;
                            break;
                    }
                    try { } catch (Exception e) { } finally { }
                    checked { i++; } unchecked { i--; }
                    lock (this) { }
                    using (Resource r = new Resource()) { }
                    goto done;
                done:
                    return;
                }
            }
            """, true),
        new("typeof/default/checked expressions", """
            class C
            {
                void M()
                {
                    object t = typeof(void);
                    int n = default(int);
                    int m = checked(1 + 2);
                    int k = unchecked(3 * 4);
                    bool b = o is C;
                    C c = o as C;
                }
            }
            """, true),
        new("preprocessor lines ignored", """
            #define FOO
            #region Region
            class C { }
            #endregion
            """, true),
        new("byte order mark ignored", "\uFEFFclass C { }", true),
        new("verbatim identifiers", "class C { void M() { @class @int = null; } }", true),
        new("integer member access", "class C { void M() { int x = 1.ToString().Length; } }", true),
        new("generics rejected", "class C<T> { }", false),
        new("lambda rejected", "class C { void M() { var f = () => 1; } }", false),
        new("nullable rejected", "class C { void M() { int? x = null; } }", false),
        // `a * b;` is not rejected: syntactically it is a valid declaration of a
        // pointer variable b of type a*. C# resolves this with semantic information,
        // which a syntax-only parser does not have. See REPORT.md.
        new("multiplication statement rejected", "class C { void M() { 1 * 2; } }", false),
        new("anonymous method rejected", "class C { void M() { Action a = delegate { }; } }", false),
        new("unicode identifier rejected", "class Café { }", false),
        new("partial type rejected", "partial class C { }", false),
        new("extern alias rejected", "extern alias X; class C { }", false),
        new("digit separator rejected", "class C { void M() { int x = 1_000; } }", false),
        new("null coalescing rejected", "class C { void M() { int x = a ?? b; } }", false),
        new("null conditional rejected", "class C { void M() { int x = a?.b; } }", false),
        new("interpolated string rejected", "class C { void M() { string s = $\"x\"; } }", false),
        new("empty switch section rejected", "class C { void M() { switch (x) { case 1: } } }", false),
        new("inactive preprocessor code is parsed", "#if false\n@@@\n#endif\nclass C { }", false),
        // Syntactic over-acceptance, documented in REPORT.md.
        new("auto-property accepted syntactically", "class C { int P { get; set; } }", true),
        new("array creation element access over-accepted", "class C { void M() { int x = new int[5][0]; } }", true),
        new("pointer declaration over-accepted", "class C { void M() { a * b; } }", true),
        new("var is an identifier", "class C { void M() { var x = 1; } }", true),
    ];

    /// <summary>
    /// A reader that yields one character per call, to exercise tokenizer
    /// suspension and resumption.
    /// </summary>
    private sealed class SlowTextReader(string text) : TextReader
    {
        private int _offset;

        public override int Read(char[] buffer, int index, int count)
        {
            if (_offset >= text.Length || count == 0)
            {
                return 0;
            }
            buffer[index] = text[_offset++];
            return 1;
        }

        public override int Read(Span<char> buffer)
        {
            if (_offset >= text.Length || buffer.IsEmpty)
            {
                return 0;
            }
            buffer[0] = text[_offset++];
            return 1;
        }
    }
}
