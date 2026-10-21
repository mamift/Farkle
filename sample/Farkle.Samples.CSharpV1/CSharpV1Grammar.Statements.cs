// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using Farkle.Builder;
using UntypedNonterminal = Farkle.Builder.Untyped.Nonterminal;

namespace Farkle.Samples.CSharpV1;

public sealed partial class CSharpV1Grammar
{
    private UntypedNonterminal _block = null!;
    private UntypedNonterminal _statementList = null!;
    private UntypedNonterminal _statement = null!;
    private UntypedNonterminal _embeddedStatement = null!;
    private UntypedNonterminal _declarationStatement = null!;
    private UntypedNonterminal _localVariableDeclaration = null!;
    private UntypedNonterminal _localConstantDeclaration = null!;
    private UntypedNonterminal _expressionStatement = null!;
    private UntypedNonterminal _statementExpression = null!;
    private UntypedNonterminal _statementExpressionList = null!;
    private UntypedNonterminal _ifStatement = null!;
    private UntypedNonterminal _switchStatement = null!;
    private UntypedNonterminal _switchBlock = null!;
    private UntypedNonterminal _switchSections = null!;
    private UntypedNonterminal _switchSection = null!;
    private UntypedNonterminal _switchLabels = null!;
    private UntypedNonterminal _switchLabel = null!;
    private UntypedNonterminal _whileStatement = null!;
    private UntypedNonterminal _doStatement = null!;
    private UntypedNonterminal _forStatement = null!;
    private UntypedNonterminal _forInitializer = null!;
    private UntypedNonterminal _forInitializerOpt = null!;
    private UntypedNonterminal _forCondition = null!;
    private UntypedNonterminal _forConditionOpt = null!;
    private UntypedNonterminal _forIterator = null!;
    private UntypedNonterminal _forIteratorOpt = null!;
    private UntypedNonterminal _foreachStatement = null!;
    private UntypedNonterminal _breakStatement = null!;
    private UntypedNonterminal _continueStatement = null!;
    private UntypedNonterminal _gotoStatement = null!;
    private UntypedNonterminal _returnStatement = null!;
    private UntypedNonterminal _throwStatement = null!;
    private UntypedNonterminal _tryStatement = null!;
    private UntypedNonterminal _catchClauses = null!;
    private UntypedNonterminal _catchClause = null!;
    private UntypedNonterminal _finallyClause = null!;
    private UntypedNonterminal _checkedStatement = null!;
    private UntypedNonterminal _uncheckedStatement = null!;
    private UntypedNonterminal _unsafeStatement = null!;
    private UntypedNonterminal _lockStatement = null!;
    private UntypedNonterminal _usingStatement = null!;
    private UntypedNonterminal _resourceAcquisition = null!;
    private UntypedNonterminal _fixedStatement = null!;
    private UntypedNonterminal _fixedPointerDeclarators = null!;
    private UntypedNonterminal _fixedPointerDeclarator = null!;
    private UntypedNonterminal _labeledStatement = null!;
    private UntypedNonterminal _emptyStatement = null!;

    private void DefineStatements()
    {
        _block = Nonterminal.CreateUntyped("block");
        _statementList = Nonterminal.CreateUntyped("statement-list");
        _statement = Nonterminal.CreateUntyped("statement");
        _embeddedStatement = Nonterminal.CreateUntyped("embedded-statement");
        _declarationStatement = Nonterminal.CreateUntyped("declaration-statement");
        _localVariableDeclaration = Nonterminal.CreateUntyped("local-variable-declaration");
        _localConstantDeclaration = Nonterminal.CreateUntyped("local-constant-declaration");
        _expressionStatement = Nonterminal.CreateUntyped("expression-statement");
        _statementExpression = Nonterminal.CreateUntyped("statement-expression");
        _statementExpressionList = Nonterminal.CreateUntyped("statement-expression-list");
        _ifStatement = Nonterminal.CreateUntyped("if-statement");
        _switchStatement = Nonterminal.CreateUntyped("switch-statement");
        _switchBlock = Nonterminal.CreateUntyped("switch-block");
        _switchSections = Nonterminal.CreateUntyped("switch-sections");
        _switchSection = Nonterminal.CreateUntyped("switch-section");
        _switchLabels = Nonterminal.CreateUntyped("switch-labels");
        _switchLabel = Nonterminal.CreateUntyped("switch-label");
        _whileStatement = Nonterminal.CreateUntyped("while-statement");
        _doStatement = Nonterminal.CreateUntyped("do-statement");
        _forStatement = Nonterminal.CreateUntyped("for-statement");
        _forInitializer = Nonterminal.CreateUntyped("for-initializer");
        _forInitializerOpt = Nonterminal.CreateUntyped("for-initializer-opt");
        _forCondition = Nonterminal.CreateUntyped("for-condition");
        _forConditionOpt = Nonterminal.CreateUntyped("for-condition-opt");
        _forIterator = Nonterminal.CreateUntyped("for-iterator");
        _forIteratorOpt = Nonterminal.CreateUntyped("for-iterator-opt");
        _foreachStatement = Nonterminal.CreateUntyped("foreach-statement");
        _breakStatement = Nonterminal.CreateUntyped("break-statement");
        _continueStatement = Nonterminal.CreateUntyped("continue-statement");
        _gotoStatement = Nonterminal.CreateUntyped("goto-statement");
        _returnStatement = Nonterminal.CreateUntyped("return-statement");
        _throwStatement = Nonterminal.CreateUntyped("throw-statement");
        _tryStatement = Nonterminal.CreateUntyped("try-statement");
        _catchClauses = Nonterminal.CreateUntyped("catch-clauses");
        _catchClause = Nonterminal.CreateUntyped("catch-clause");
        _finallyClause = Nonterminal.CreateUntyped("finally-clause");
        _checkedStatement = Nonterminal.CreateUntyped("checked-statement");
        _uncheckedStatement = Nonterminal.CreateUntyped("unchecked-statement");
        _unsafeStatement = Nonterminal.CreateUntyped("unsafe-statement");
        _lockStatement = Nonterminal.CreateUntyped("lock-statement");
        _usingStatement = Nonterminal.CreateUntyped("using-statement");
        _resourceAcquisition = Nonterminal.CreateUntyped("resource-acquisition");
        _fixedStatement = Nonterminal.CreateUntyped("fixed-statement");
        _fixedPointerDeclarators = Nonterminal.CreateUntyped("fixed-pointer-declarators");
        _fixedPointerDeclarator = Nonterminal.CreateUntyped("fixed-pointer-declarator");
        _labeledStatement = Nonterminal.CreateUntyped("labeled-statement");
        _emptyStatement = Nonterminal.CreateUntyped("empty-statement");

        _block.SetProductions(
            P("{", "}"),
            P("{", _statementList, "}"));

        _statementList.SetProductions(
            P(_statement),
            P(_statementList, _statement));

        _statement.SetProductions(
            P(_embeddedStatement),
            P(_declarationStatement),
            P(_labeledStatement));

        _embeddedStatement.SetProductions(
            P(_block),
            P(_emptyStatement),
            P(_expressionStatement),
            P(_ifStatement),
            P(_switchStatement),
            P(_whileStatement),
            P(_doStatement),
            P(_forStatement),
            P(_foreachStatement),
            P(_breakStatement),
            P(_continueStatement),
            P(_gotoStatement),
            P(_returnStatement),
            P(_throwStatement),
            P(_tryStatement),
            P(_checkedStatement),
            P(_uncheckedStatement),
            P(_unsafeStatement),
            P(_lockStatement),
            P(_usingStatement),
            P(_fixedStatement));

        _declarationStatement.SetProductions(
            P(_localVariableDeclaration, ";"),
            P(_localConstantDeclaration, ";"));

        // The semicolon belongs to declaration-statement; for-initializers and
        // using-resource-acquisitions use the same declaration without one.
        _localVariableDeclaration.SetProductions(
            P(_type, _variableDeclarators));

        _localConstantDeclaration.SetProductions(
            P("const", _type, _constantDeclarators));

        _expressionStatement.SetProductions(
            P(_statementExpression, ";"));

        _statementExpression.SetProductions(
            P(_invocationExpression),
            P(_objectCreationExpression),
            P(_assignment),
            P(_preIncrementExpression),
            P(_preDecrementExpression),
            P(_postIncrementExpression),
            P(_postDecrementExpression));

        _statementExpressionList.SetProductions(
            P(_statementExpression),
            P(_statementExpressionList, ",", _statementExpression));

        _ifStatement.SetProductions(
            P("if", "(", _expression, ")", _embeddedStatement).WithPrecedence(_ifPrecedenceToken),
            P("if", "(", _expression, ")", _embeddedStatement, "else", _embeddedStatement));

        _switchStatement.SetProductions(
            P("switch", "(", _expression, ")", _switchBlock));

        _switchBlock.SetProductions(
            P("{", "}"),
            P("{", _switchSections, "}"));

        _switchSections.SetProductions(
            P(_switchSection),
            P(_switchSections, _switchSection));

        // Requiring at least one statement makes consecutive labels accumulate into
        // the same section instead of creating an empty section, which removes the
        // shift/reduce ambiguity around case/default. Empty switch sections are
        // therefore not accepted; see REPORT.md.
        _switchSection.SetProductions(
            P(_switchLabels, _statementList).WithPrecedence(_switchSectionPrecedenceToken));

        _switchLabels.SetProductions(
            P(_switchLabel),
            P(_switchLabels, _switchLabel));

        _switchLabel.SetProductions(
            P("case", _constantExpression, ":"),
            P("default", ":"));

        _whileStatement.SetProductions(
            P("while", "(", _expression, ")", _embeddedStatement));

        _doStatement.SetProductions(
            P("do", _embeddedStatement, "while", "(", _expression, ")", ";"));

        _forStatement.SetProductions(
            P("for", "(", _forInitializerOpt, ";", _forConditionOpt, ";", _forIteratorOpt, ")",
                _embeddedStatement));

        _forInitializerOpt.SetProductions(
            P(),
            P(_forInitializer));

        _forInitializer.SetProductions(
            P(_localVariableDeclaration),
            P(_statementExpressionList));

        _forConditionOpt.SetProductions(
            P(),
            P(_forCondition));

        _forCondition.SetProductions(
            P(_expression));

        _forIteratorOpt.SetProductions(
            P(),
            P(_forIterator));

        _forIterator.SetProductions(
            P(_statementExpressionList));

        _foreachStatement.SetProductions(
            P("foreach", "(", _type, _identifier, "in", _expression, ")", _embeddedStatement));

        _breakStatement.SetProductions(
            P("break", ";"));

        _continueStatement.SetProductions(
            P("continue", ";"));

        _gotoStatement.SetProductions(
            P("goto", _identifier, ";"),
            P("goto", "case", _constantExpression, ";"),
            P("goto", "default", ";"));

        _returnStatement.SetProductions(
            P("return", ";"),
            P("return", _expression, ";"));

        _throwStatement.SetProductions(
            P("throw", ";"),
            P("throw", _expression, ";"));

        _tryStatement.SetProductions(
            P("try", _block, _catchClauses),
            P("try", _block, _finallyClause),
            P("try", _block, _catchClauses, _finallyClause));

        _catchClauses.SetProductions(
            P(_catchClause),
            P(_catchClauses, _catchClause));

        _catchClause.SetProductions(
            P("catch", "(", _type, ")", _block),
            P("catch", "(", _type, _identifier, ")", _block),
            P("catch", _block));

        _finallyClause.SetProductions(
            P("finally", _block));

        _checkedStatement.SetProductions(
            P("checked", _block));

        _uncheckedStatement.SetProductions(
            P("unchecked", _block));

        _unsafeStatement.SetProductions(
            P("unsafe", _block));

        _lockStatement.SetProductions(
            P("lock", "(", _expression, ")", _embeddedStatement));

        _usingStatement.SetProductions(
            P("using", "(", _resourceAcquisition, ")", _embeddedStatement));

        _resourceAcquisition.SetProductions(
            P(_localVariableDeclaration),
            P(_expression));

        _fixedStatement.SetProductions(
            P("fixed", "(", _type, _fixedPointerDeclarators, ")", _embeddedStatement));

        _fixedPointerDeclarators.SetProductions(
            P(_fixedPointerDeclarator),
            P(_fixedPointerDeclarators, ",", _fixedPointerDeclarator));

        _fixedPointerDeclarator.SetProductions(
            P(_identifier, "=", _expression));

        _labeledStatement.SetProductions(
            P(_identifier, ":", _statement));

        _emptyStatement.SetProductions(
            P(";"));
    }
}
