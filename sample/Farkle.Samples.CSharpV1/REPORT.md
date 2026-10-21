# A C# 1.0 parser built with Farkle

This sample implements a parser for the **C# 1.0/1.1 language** (the pre-generics
language described by ECMA-334, 2nd edition, December 2002). It uses Farkle's
C# grammar-builder API, its default **IELR(1)** table generator, and a small custom
tokenizer. It is a *syntax* parser: it validates the grammar and reconstructs a
concrete syntax tree (CST). It does not perform semantic analysis.

The grammar has:

| Item | Count |
|-|-|
| Token symbols | 139 |
| Nonterminals | 205 |
| Productions | 503 |
| IELR(1) states | 883 |
| Tokenizer DFA states | 438 |
| Build time (Debug, this machine) | ~0.3 s |

The parser passes 54 built-in positive/negative tests (including a one-character-at-a-
time streaming test) and parses the four files in `Inputs/`.

## How to run

```powershell
# self-tests plus the files in Inputs/
dotnet run --project sample/Farkle.Samples.CSharpV1

# just the self-tests
dotnet run --project sample/Farkle.Samples.CSharpV1 -- --self-test

# parse files
dotnet run --project sample/Farkle.Samples.CSharpV1 -- Inputs/Declarations.cs Inputs/Expressions.cs

# print a depth-limited CST
dotnet run --project sample/Farkle.Samples.CSharpV1 -- --tree Inputs/Unsafe.cs
```

## How the sample uses Farkle

### Untyped grammar plus a generic CST provider

Writing a typed semantic action for every one of the 503 productions would add a
lot of noise and no syntax-parsing value. Instead the grammar is built entirely
with untyped symbols (`Nonterminal.CreateUntyped`, `ProductionBuilder`), and the
parser is built as a syntax checker:

```csharp
CharParser<CstNode?> checker = builder.BuildSyntaxCheck<CstNode>(options);
```

The sample then replaces the syntax checker's semantic provider with
`CstSemanticProvider` (`Cst.cs`). It implements the public
`ISemanticProvider<char, CstNode>` interface:

* `Transform` creates a terminal node from the token text and the grammar's token
  symbol name.
* `Fuse` creates a nonterminal node named after the production head and collects
  the child values from the `members` span.

Because the parser pushes one semantic value for every production member, this
single provider reconstructs a complete CST for every production, without any
per-rule fusers. The CST nodes carry `TextPosition` start/end information.

### Grammar organization

* `CSharpV1Grammar.cs` – terminals, lexical configuration, start symbol, operator
  scope.
* `CSharpV1Grammar.Types.cs` – type grammar.
* `CSharpV1Grammar.Expressions.cs` – layered expression grammar and array/object
  creation.
* `CSharpV1Grammar.Statements.cs` – statements.
* `CSharpV1Grammar.Declarations.cs` – namespaces, types, members, attributes.

Expressions use explicit precedence layers, so the operator scope is not used for
ordinary operator precedence. It is used only to resolve the handful of genuinely
ambiguous constructs described below.

### Lexical handling

* Identifier terminal: ASCII identifiers, with optional `@` verbatim prefix.
* Integer literals: decimal and hexadecimal, with valid `u`/`l` suffix combinations.
* Real literals: decimal point and/or exponent, with `f`/`d`/`m` suffixes. A dot
  must be followed by a digit, so `1.ToString()` is still an integer followed by a
  member access.
* Character, regular string, and verbatim string literals, with the C# 1.0 escape
  sequences (`\x` 1–4 hex digits, `\u` exactly 4).
* `//` and `/* */` comments, XML doc comments (as ordinary line comments), and
  `#` preprocessor lines are registered as ignored text.
* Byte-order mark, form feed, and vertical tab are noise symbols.

## Coverage

### Declarations

* Compilation units; `using` namespace directives and aliases; global attributes
  (see the caveats below); nested namespaces.
* Classes, structs, interfaces, enums, and delegates, including nested types and
  optional trailing semicolons.
* All C# 1.0 members: constants, fields, methods, properties, indexers, events
  (field-like and accessor-based), operators, conversion operators, constructors,
  static constructors, destructors, fixed-size buffers, and nested types.
* Explicit interface member implementations, including dotted interface names.
* Attributes with positional and named arguments, attribute target specifiers,
  attributes on types, members, parameters, accessors, enum members, and return
  values.
* `ref`/`out`/`params` parameters, `ref`/`out` arguments, constructors with
  `this`/`base` initializers.

### Statements

Blocks, local and local-constant declarations, expression statements, `if`/`else`,
`switch` (including `goto case`/`goto default`), `while`, `do`, `for`, `foreach`,
`break`, `continue`, `goto`, `return`, `throw` (including rethrow), `try`/`catch`/
`finally`, `checked`/`unchecked`, `unsafe`, `lock`, `using`, `fixed`, labels, and
empty statements.

### Expressions

The full C# 1.0 precedence chain: assignment and compound assignment, conditional,
`||`, `&&`, `|`, `^`, `&`, equality, relational, `is`, `as`, shifts, additive,
multiplicative, unary (including pointer indirection and address-of), casts,
pre/post increment/decrement, invocation, element access, member and pointer-member
access, `this`/`base` access, object creation, array creation and initializers,
`typeof`, `sizeof`, `default`, `checked`/`unchecked`, `stackalloc`, literals, and
`ref`/`out` arguments.

### Unsafe code

Pointer types and suffixes, `*`, `&`, `->`, `fixed` statements, fixed-size buffer
fields, `stackalloc`, `sizeof`, and the `unsafe` modifier/statement are all in the
grammar. Unsafe-context validity is semantic and is not checked.

## The hard parts: C# is not an LR(1) grammar

The C# language specification contains several known grammar ambiguities. Farkle is
an LR(1)/IELR(1) parser generator: it has one token of lookahead, no GLR, and no
backtracking. The sample resolves the ambiguities in the following ways.

### Cast versus parenthesized expression

`(x)-y` can be a subtraction or a cast of `-y` to the type `x`; `(x)(y)` can be an
invocation or a cast. The C# specification (§7.6.6) resolves this with a token-level
rule: a parenthesized sequence starts a cast if the contents are type grammar but
not expression grammar, or if they are type grammar and the next token is `~`, `!`,
`(`, an identifier, a literal, or a keyword other than `as`/`is`.

The grammar splits the cases:

* **Types that are not expressions** (predefined types, array/pointer types,
  `void*`) use a direct cast production: `( non-expression-type ) unary`.
* **Name-only types** (`(Foo)x`, `(Ns.Foo)x`) are both type and expression grammar.
  A neutral `paren-name` nonterminal parses `( qualified-name )` and is used both
  as a primary expression and as a cast prefix. A reduce/reduce precedence pair
  (`NAME` > `EXPR`, with `CanResolveReduceReduceConflicts = true`) forces the name
  reading to be taken as `paren-name`.
* The choice after `)` is resolved with a `CAST` precedence token on
  `primary -> paren-name`:
  * `++`/`--` and binary/assignment/conditional/`is`/`as` operators are below
    `CAST`, so `(x)++` is postfix and `(x)-y` is subtraction;
  * `(` is above `CAST`, so `(x)(y)` is a cast;
  * `!`, `~`, identifiers, literals, and expression-starting keywords have no
    competing action, so they form casts.

This reproduces the specification's cast rule for the combinations tested by the
sample (`(a)-b`, `(a)(b)`, `(a)++`, `(a)!b`, and others).

### Rank specifiers versus `[` for element access / array length

After a type or a `new` array expression, `[` can start a rank specifier (`[]`,
`[,]`) or an element access/array length (`[0]`, `[n]`). The token after `[` decides,
which is two tokens of lookahead. The sample adds `RankBracketTokenizer`, a small
chained tokenizer that scans ahead (skipping whitespace, comments, and `#` lines):

* if the next significant character is `]` or `,`, it emits the virtual terminal
  `rank-left-bracket` and consumes the `[`;
* otherwise it declines, and Farkle's default tokenizer handles the `[` as an
  ordinary literal.

Rank specifiers use `rank-left-bracket`; element access and array lengths use the
ordinary `[`. The tokenizer suspends itself when it needs more input, so it also
works for streamed parsing. The sample includes a one-character-at-a-time reader
test.

`new` expressions use a separate base type without rank suffixes plus bracket
suffixes. This supports `new int[5]`, `new int[] { ... }`, `new int[,] { ... }`,
`new int[3][]`, and `new int[3][,]`.

### Dangling else

The if-without-else production carries an `IF` precedence token, and `else` is one
group above it, so the shift wins and `else` binds to the nearest `if`.

### Attribute named arguments versus positional assignment

C# attribute arguments can be positional expressions or `identifier = expression`
named arguments. A positional argument is parsed as a conditional expression
(which excludes assignment); therefore `identifier = ...` can only be a named
argument. Assignment is not a valid constant expression, so this does not lose any
valid C# 1.0 code.

### Switch labels and sections

Switch sections use `switch-labels statement-list` and require a non-empty
statement list, matching the specification's `statement-list: statement+`.
Consecutive labels therefore accumulate into one section. A completed section is
reduced (rather than shifting `case`/`default` as another label) via a precedence
token. Empty switch sections are rejected, as the grammar specifies.

### Pointer declarations versus multiplication statements

`a * b;` is syntactically a declaration of a pointer variable `b` of type `a*` and
is accepted as such. C# resolves this with semantic information (is `a` a type?),
which a syntax parser does not have. `1 * 2;` is rejected because multiplication is
not a legal `statement-expression`.

### Explicit interface member implementations

The grammar supports all syntactically valid explicit member forms, including
dotted interface names (`void N.I.M()`, `int I.this[int i]`, explicit events).
There is no syntactic ambiguity between a qualified return type and an explicit
member name: the token sequences differ (`A.B M()` versus `A B.M()`), and the type
parser consumes dots greedily. An expression such as `A.B()` is a type followed by
a missing member name, not a valid explicit implementation.

## Limitations and gaps

### Preprocessor directives (largest gap)

`#` lines are ignored as line comments. Farkle has no conditional-compilation hook,
so:

* `#if` / `#elif` / `#else` / `#endif` are **not evaluated**; inactive branches are
  still parsed and must be syntactically valid;
* `#define` / `#undef` do not change the token stream;
* `#line` does not remap positions;
* `#error` and `#warning` are ignored;
* `#pragma` and `#region` are ignored;
* `#` at any token boundary starts a line comment, not only at the start of a line.

A file containing only preprocessor directives and no members is accepted (the
directives are noise), but global attributes without a following member are not.

### No semantic analysis

The parser does not build a symbol table and does not check:

* types, conversions, overload resolution, or members;
* definite assignment, reachability, or `goto` labels;
* accessibility, `override`/`virtual`/`abstract`/`sealed` rules, or interface
  implementation matching;
* constant expressions, array sizes, or attribute arguments/targets;
* constructor-name matching, operator arity/type rules, or `ref`/`out` variable
  requirements;
* unsafe-context rules (pointers, `fixed`, `stackalloc`, `sizeof`);
* accessor spelling/order/count, duplicate or invalid modifiers.

### Lexical limitations

* Identifiers are ASCII plus `@`; Unicode identifier categories and `\uXXXX`
  escapes inside identifiers are not supported.
* New-line handling recognizes CR, LF, and CRLF. NEL/LS/PS are not treated as line
  terminators by comments/strings; they will generally cause a lexical error.
* The source is read as text; Unicode escape preprocessing in identifiers and
  keywords is not performed.
* Character/string escapes cover the C# 1.0 set (`\x` 1–4 hex, `\u` 4 hex). There
  is no `\U` escape (C# 1.0 has none).
* Numeric literals support decimal/hex integers with `u`/`l` suffixes and decimal
  reals with `f`/`d`/`m` suffixes. Binary literals and digit separators are
  post-v1 and are rejected.
* Interpolated, raw, and UTF-8 string literals are post-v1 and are rejected.

### Syntactic over-acceptance

The following are accepted syntactically but would be rejected by a semantic
analyzer:

* `get;`/`set;` accessors on class/struct properties (auto-property syntax is
  accepted; C# 1.0 requires bodies except on abstract/interface properties);
* arbitrary accessor identifiers, order, and counts (`get`/`set`/`add`/`remove`
  are contextual keywords and are lexed as ordinary identifiers);
* any sequence, order, or duplication of modifiers;
* `void` fields, properties, and indexers (the member grammar uses the return-type
  category, which includes `void`);
* operator tokens with the wrong arity (the operator token set is shared, and the
  parameter count decides unary versus binary);
* any expression as a `ref`/`out` argument;
* structs reuse the class-member grammar, so destructors and parameterless
  constructors in structs are accepted syntactically;
* interface accessors and event accessors accept block bodies as well as `;`;
* field-like event declarations may carry initializers or multiple declarators;
* any identifier as an attribute target (`[anything: ...]`);
* top-level target-specified attribute sections before a namespace; they are
  attached to the following member/namespace rather than treated as global
  attributes;
* `new int[5][0]` / `new int[5][6]`: every bracket after the first is accepted as
  another array-dimension suffix, although C# only allows a rank specifier there.
  Use `(new int[5])[0]` for element access on a freshly created array. `new int[]`
  without an initializer is also accepted;
* `a * b;` as a pointer declaration (see above);
* modern contextual keywords (`var`, `dynamic`, `await`, `nameof`, `get`, `set`,
  `add`, `remove`, `value`, `assembly`, `module`) are ordinary identifiers, so code
  that uses them with their modern meaning may parse as declarations or calls;
* `static class C` is accepted as a class carrying a `static` modifier (static
  classes are a C# 2 feature; modifiers are validated semantically).

### Post-v1 features

Generics, nullable types, anonymous methods and lambdas, iterators (`yield`),
partial types, static classes, object/collection initializers, anonymous types,
query expressions, `??`, `?.`, `async`/`await`, `dynamic`, interpolation, and
other C# 2+ syntax are not part of the grammar and are generally rejected. Some
contextual keywords can still appear as identifiers, as noted above.

`extern alias` is not implemented. Fixed-size buffers and `stackalloc` are
implemented.

### Parser-generator limitations

* IELR(1) only: no GLR, no backtracking, and no user-visible k>1 lookahead. The
  `[` ambiguity required a custom tokenizer; the cast and dangling-else ambiguities
  required precedence and grammar factoring.
* One syntax error per parse; there is no error recovery or multi-error reporting.
  The expected-token list can include internal names such as
  `rank-left-bracket`.
* The grammar is built at startup (about 0.3 s in Debug on this machine). The
  optional Farkle precompiler could be added to remove this cost.
* Some CST wrapper nodes for empty optional productions have approximate positions.

## References

* ECMA-334, *C# Language Specification*, 2nd edition, December 2002.
* C# specification, cast expressions and grammar ambiguities:
  <https://learn.microsoft.com/dotnet/csharp/language-reference/language-specification/expressions>
* Farkle documentation:
  <https://farkle.dev>
