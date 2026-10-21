# C# 1.0 parser sample

This sample parses C# 1.0/1.1 source (the pre-generics language) with Farkle and
builds a concrete syntax tree.

```powershell
dotnet run --project sample/Farkle.Samples.CSharpV1
dotnet run --project sample/Farkle.Samples.CSharpV1 -- --self-test
dotnet run --project sample/Farkle.Samples.CSharpV1 -- Inputs/Declarations.cs
dotnet run --project sample/Farkle.Samples.CSharpV1 -- --tree Inputs/Unsafe.cs
```

The grammar uses IELR(1), a layered expression grammar, operator precedence for a
few genuinely ambiguous constructs, and a small chained tokenizer that
disambiguates `[` rank specifiers from element access. The CST is produced without
per-rule semantic actions by a custom `ISemanticProvider` that reconstructs nodes
from the built grammar.

See [REPORT.md](REPORT.md) for the coverage, the ambiguity solutions, and the
remaining gaps (most notably preprocessor conditional compilation and the absence
of semantic analysis).
