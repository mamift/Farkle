// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using Farkle.Samples.CSharpV1;

namespace Farkle.Samples.CSharpV1.Runner;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrintUsage();
            return 1;
        }

        if (args is ["--help"] or ["-h"] or ["/?"])
        {
            PrintUsage();
            return 0;
        }

        CharParser<CstNode> parser = CSharpV1Parser.Create();

        if (args is ["--tree", var treePath])
        {
            return RunTree(parser, treePath) ? 0 : 1;
        }

        int parsed = 0;
        int failed = 0;
        foreach (string path in args.SelectMany(ExpandPath))
        {
            if (RunFile(parser, path))
            {
                parsed++;
            }
            else
            {
                failed++;
            }
        }

        Console.WriteLine($"Parsed {parsed} file(s), {failed} failed.");
        return failed == 0 ? 0 : 1;
    }

    private static void PrintUsage()
    {
        Console.WriteLine(
            "Farkle C# 1.0 parser runner\n" +
            "\n" +
            "Usage:\n" +
            "  dotnet run --project sample/Farkle.Samples.CSharpV1.Runner -- <file-or-directory>...\n" +
            "  dotnet run --project sample/Farkle.Samples.CSharpV1.Runner -- --tree <file>\n" +
            "\n" +
            "Directories are searched recursively for *.cs files.");
    }

    private static IEnumerable<string> ExpandPath(string path)
    {
        if (Directory.Exists(path))
        {
            foreach (string file in Directory.EnumerateFiles(path, "*.cs", SearchOption.AllDirectories))
            {
                yield return file;
            }

            yield break;
        }

        // A file, glob, or nonexistent path; RunFile reports read errors.
        yield return path;
    }

    private static bool RunFile(CharParser<CstNode> parser, string path)
    {
        if (!TryParseFile(parser, path, out ParserResult<CstNode> result))
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

    private static bool RunTree(CharParser<CstNode> parser, string path)
    {
        if (!TryParseFile(parser, path, out ParserResult<CstNode> result))
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

    private static bool TryParseFile(CharParser<CstNode> parser, string path, out ParserResult<CstNode> result)
    {
        try
        {
            result = parser.ParseFile(path);
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"ERROR {path}: {ex.Message}");
            result = default;
            return false;
        }
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
}
