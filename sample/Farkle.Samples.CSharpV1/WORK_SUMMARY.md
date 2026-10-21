# Work summary: C# 1.0/1.1 parser sample

This document summarizes the work that produced the sample in this directory.
The work is committed locally on branch `sample/csharp-v1-parser` as commit
`38c2736d60aee1abf6d71b203b29d03d27d7cfb1` ("Add C# 1.0/1.1 parser sample").
Nothing has been pushed.

> This summary file was added after that commit and is not part of it.

## Commit metadata

| | |
|-|-|
| Branch | `sample/csharp-v1-parser` (created from `mainstream`) |
| Commit | `38c2736d60aee1abf6d71b203b29d03d27d7cfb1` |
| Subject | `Add C# 1.0/1.1 parser sample` |
| Changes | 17 files, 2,861 insertions |
| Pushed | No |

The pre-existing untracked `.mcp.json` was intentionally left out. Build outputs
(`bin/`, `obj/`) are ignored by the repository's `.gitignore`.

## What the commit contains

### New project: `sample/Farkle.Samples.CSharpV1/`

**Project file**

- `Farkle.Samples.CSharpV1.csproj` — `net8.0` executable, references `Farkle`
  and `Farkle.Analyzers`; excludes `Inputs/**/*.cs` from compilation and copies
  them to the output directory.

**Grammar**

- `CSharpV1Grammar.cs` — terminals, lexical configuration, start symbol,
  operator scope, comments and noise symbols (including `#` directive lines).
- `CSharpV1Grammar.Types.cs` — predefined/open/non-expression types, arrays,
  pointers, rank specifiers, `void*`, and the virtual `rank-left-bracket`
  terminal.
- `CSharpV1Grammar.Expressions.cs` — layered precedence chain, unary/cast/primary
  expressions, paren-name handling, object/array creation, array initializers,
  arguments, `typeof`/`sizeof`/`default`/`checked`/`unchecked`/`stackalloc`.
- `CSharpV1Grammar.Statements.cs` — blocks, declarations,
  expression/selection/iteration/jump statements, `switch`, `try`,
  `checked`/`unchecked`, `unsafe`, `lock`, `using`, `fixed`, labels.
- `CSharpV1Grammar.Declarations.cs` — compilation units, using directives and
  aliases, namespaces, classes/structs/interfaces/enums/delegates, all member
  kinds, attributes and targets, explicit interface implementations.

The reachable grammar contains 139 token symbols, 205 nonterminals, 503
productions, 883 IELR(1) states, and 438 tokenizer DFA states.

**Ambiguity handling**

- Cast vs parenthesized expression via neutral `paren-name` productions,
  `NAME`/`EXPR` reduce/reduce precedence, and a `CAST` precedence token on
  `primary -> paren-name`.
- Dangling `else` via `IF`/`else` precedence.
- Switch sections via a dedicated precedence token.
- Rank brackets via a chained `RankBracketTokenizer` that emits
  `rank-left-bracket` for `[]`/`[,]`, leaving ordinary `[` for element access and
  array lengths. The tokenizer suspends and resumes for streamed input.
- `new` expressions use a rank-free base type plus explicit bracket suffixes.

**Concrete syntax tree**

- `Cst.cs` — `CstNode` and `CstSemanticProvider`. The provider implements
  `ISemanticProvider<char, CstNode>` and reconstructs the full CST from the
  built grammar, so the grammar stays untyped and no per-production fusers are
  needed.

**Driver and tests**

- `Program.cs` — grammar statistics, build timing, 54 positive/negative
  self-tests (including a one-character-at-a-time streaming test), file parsing,
  path resolution, error handling, `--self-test`, and `--tree`.
- `Inputs/Declarations.cs`, `Inputs/Statements.cs`, `Inputs/Expressions.cs`, and
  `Inputs/Unsafe.cs` — C# 1.0-style sample sources, all of which parse
  successfully.

**Documentation**

- `README.md` — usage summary.
- `REPORT.md` — detailed coverage, ambiguity solutions, and gaps. The main
  documented gaps are preprocessor conditional compilation, absent semantic
  analysis, ASCII-only identifiers, a list of syntactic over-acceptances, and
  post-v1 features.

**Repository integration**

- `Farkle.slnx` — new project added under `/sample/`.
- `sample/README.md` — bullet describing the sample and its report.

## Verification

- Full solution build: `dotnet build Farkle.slnx` succeeded (only pre-existing
  NuGet advisory warnings in `eng/Farkle.Build.fsproj`).
- Grammar: 139 token symbols, 205 nonterminals, 503 productions, 883 IELR(1)
  states, 438 DFA states; builds in about 0.3 s in Debug.
- Tests: 54/54 passed, including the streaming test.
- Input files: 4/4 parsed successfully.
- Git reported normal LF→CRLF normalization warnings on add (repository
  setting); there was no content issue.

## Notes

- The commit was not pushed.
- The `.mcp.json` file that was already untracked before this work was left
  untouched.
- This summary file itself is currently untracked and is not part of commit
  `38c2736d`.
