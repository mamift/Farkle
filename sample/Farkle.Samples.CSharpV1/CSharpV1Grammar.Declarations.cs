// Copyright © Theodore Tsirpanis and Contributors.
// SPDX-License-Identifier: MIT

using Farkle.Builder;
using UntypedNonterminal = Farkle.Builder.Untyped.Nonterminal;

namespace Farkle.Samples.CSharpV1;

public sealed partial class CSharpV1Grammar
{
    private UntypedNonterminal _usingDirectives = null!;
    private UntypedNonterminal _usingDirectivesOpt = null!;
    private UntypedNonterminal _usingDirective = null!;
    private UntypedNonterminal _usingAliasDirective = null!;
    private UntypedNonterminal _usingNamespaceDirective = null!;

    private UntypedNonterminal _attributeSections = null!;
    private UntypedNonterminal _attributeSectionsOpt = null!;
    private UntypedNonterminal _attributeSection = null!;
    private UntypedNonterminal _attributeTarget = null!;
    private UntypedNonterminal _attributeTargetSpecifier = null!;
    private UntypedNonterminal _attributeList = null!;
    private UntypedNonterminal _attribute = null!;
    private UntypedNonterminal _attributeName = null!;
    private UntypedNonterminal _attributeArguments = null!;
    private UntypedNonterminal _attributeArgumentList = null!;
    private UntypedNonterminal _positionalArgumentList = null!;
    private UntypedNonterminal _positionalArgument = null!;
    private UntypedNonterminal _namedArgumentList = null!;
    private UntypedNonterminal _namedArgument = null!;

    private UntypedNonterminal _modifiers = null!;
    private UntypedNonterminal _modifiersOpt = null!;
    private UntypedNonterminal _modifier = null!;

    private UntypedNonterminal _namespaceDeclaration = null!;
    private UntypedNonterminal _namespaceBody = null!;
    private UntypedNonterminal _namespaceMemberDeclarations = null!;
    private UntypedNonterminal _namespaceMemberDeclarationsOpt = null!;
    private UntypedNonterminal _namespaceMemberDeclaration = null!;
    private UntypedNonterminal _topLevelNamespaceMemberDeclarations = null!;
    private UntypedNonterminal _topLevelNamespaceMemberDeclarationsOpt = null!;
    private UntypedNonterminal _topLevelNamespaceMemberDeclaration = null!;

    private UntypedNonterminal _typeDeclaration = null!;
    private UntypedNonterminal _topLevelTypeDeclaration = null!;
    private UntypedNonterminal _typeDeclarationBody = null!;
    private UntypedNonterminal _classDeclarationBody = null!;
    private UntypedNonterminal _structDeclarationBody = null!;
    private UntypedNonterminal _interfaceDeclarationBody = null!;
    private UntypedNonterminal _enumDeclarationBody = null!;
    private UntypedNonterminal _delegateDeclarationBody = null!;

    private UntypedNonterminal _classBase = null!;
    private UntypedNonterminal _classBaseOpt = null!;
    private UntypedNonterminal _interfaceBase = null!;
    private UntypedNonterminal _interfaceBaseOpt = null!;
    private UntypedNonterminal _structInterfaces = null!;
    private UntypedNonterminal _structInterfacesOpt = null!;
    private UntypedNonterminal _enumBase = null!;
    private UntypedNonterminal _enumBaseOpt = null!;
    private UntypedNonterminal _typeNameList = null!;
    private UntypedNonterminal _integralType = null!;

    private UntypedNonterminal _classBody = null!;
    private UntypedNonterminal _structBody = null!;
    private UntypedNonterminal _interfaceBody = null!;
    private UntypedNonterminal _enumBody = null!;

    private UntypedNonterminal _classMemberDeclarations = null!;
    private UntypedNonterminal _classMemberDeclaration = null!;
    private UntypedNonterminal _classMemberBody = null!;

    private UntypedNonterminal _interfaceMemberDeclarations = null!;
    private UntypedNonterminal _interfaceMemberDeclaration = null!;
    private UntypedNonterminal _interfaceMemberBody = null!;

    private UntypedNonterminal _constantDeclarationBody = null!;
    private UntypedNonterminal _eventDeclarationBody = null!;
    private UntypedNonterminal _fixedBufferDeclarationBody = null!;
    private UntypedNonterminal _destructorDeclarationBody = null!;
    private UntypedNonterminal _constructorDeclarationBody = null!;
    private UntypedNonterminal _operatorDeclarationBody = null!;

    private UntypedNonterminal _memberSuffix = null!;
    private UntypedNonterminal _memberAfterName = null!;
    private UntypedNonterminal _explicitMemberName = null!;
    private UntypedNonterminal _explicitThisName = null!;

    private UntypedNonterminal _methodBody = null!;
    private UntypedNonterminal _accessorDeclarations = null!;
    private UntypedNonterminal _accessorDeclaration = null!;
    private UntypedNonterminal _accessorBody = null!;
    private UntypedNonterminal _eventAccessorDeclarations = null!;
    private UntypedNonterminal _eventAccessorDeclaration = null!;
    private UntypedNonterminal _interfaceAccessorDeclarations = null!;
    private UntypedNonterminal _interfaceAccessorDeclaration = null!;

    private UntypedNonterminal _formalParameterList = null!;
    private UntypedNonterminal _formalParameter = null!;
    private UntypedNonterminal _fixedParameter = null!;
    private UntypedNonterminal _parameterArray = null!;
    private UntypedNonterminal _parameterModifier = null!;

    private UntypedNonterminal _constructorInitializer = null!;
    private UntypedNonterminal _constructorInitializerOpt = null!;
    private UntypedNonterminal _operatorBody = null!;

    private UntypedNonterminal _fixedBufferDeclarators = null!;
    private UntypedNonterminal _fixedBufferDeclarator = null!;
    private UntypedNonterminal _overloadableOperator = null!;

    private UntypedNonterminal _enumMemberDeclarations = null!;
    private UntypedNonterminal _enumMemberDeclaration = null!;
    private UntypedNonterminal _semicolonOpt = null!;

    private void DefineDeclarations()
    {
        _compilationUnit = Nonterminal.CreateUntyped("compilation-unit");

        _usingDirectives = Nonterminal.CreateUntyped("using-directives");
        _usingDirectivesOpt = Nonterminal.CreateUntyped("using-directives-opt");
        _usingDirective = Nonterminal.CreateUntyped("using-directive");
        _usingAliasDirective = Nonterminal.CreateUntyped("using-alias-directive");
        _usingNamespaceDirective = Nonterminal.CreateUntyped("using-namespace-directive");

        _attributeSections = Nonterminal.CreateUntyped("attribute-sections");
        _attributeSectionsOpt = Nonterminal.CreateUntyped("attribute-sections-opt");
        _attributeSection = Nonterminal.CreateUntyped("attribute-section");
        _attributeTarget = Nonterminal.CreateUntyped("attribute-target");
        _attributeTargetSpecifier = Nonterminal.CreateUntyped("attribute-target-specifier");
        _attributeList = Nonterminal.CreateUntyped("attribute-list");
        _attribute = Nonterminal.CreateUntyped("attribute");
        _attributeName = Nonterminal.CreateUntyped("attribute-name");
        _attributeArguments = Nonterminal.CreateUntyped("attribute-arguments");
        _attributeArgumentList = Nonterminal.CreateUntyped("attribute-argument-list");
        _positionalArgumentList = Nonterminal.CreateUntyped("positional-argument-list");
        _positionalArgument = Nonterminal.CreateUntyped("positional-argument");
        _namedArgumentList = Nonterminal.CreateUntyped("named-argument-list");
        _namedArgument = Nonterminal.CreateUntyped("named-argument");

        _modifiers = Nonterminal.CreateUntyped("modifiers");
        _modifiersOpt = Nonterminal.CreateUntyped("modifiers-opt");
        _modifier = Nonterminal.CreateUntyped("modifier");

        _namespaceDeclaration = Nonterminal.CreateUntyped("namespace-declaration");
        _namespaceBody = Nonterminal.CreateUntyped("namespace-body");
        _namespaceMemberDeclarations = Nonterminal.CreateUntyped("namespace-member-declarations");
        _namespaceMemberDeclarationsOpt = Nonterminal.CreateUntyped("namespace-member-declarations-opt");
        _namespaceMemberDeclaration = Nonterminal.CreateUntyped("namespace-member-declaration");
        _topLevelNamespaceMemberDeclarations = Nonterminal.CreateUntyped("top-level-namespace-member-declarations");
        _topLevelNamespaceMemberDeclarationsOpt = Nonterminal.CreateUntyped("top-level-namespace-member-declarations-opt");
        _topLevelNamespaceMemberDeclaration = Nonterminal.CreateUntyped("top-level-namespace-member-declaration");

        _typeDeclaration = Nonterminal.CreateUntyped("type-declaration");
        _topLevelTypeDeclaration = Nonterminal.CreateUntyped("top-level-type-declaration");
        _typeDeclarationBody = Nonterminal.CreateUntyped("type-declaration-body");
        _classDeclarationBody = Nonterminal.CreateUntyped("class-declaration-body");
        _structDeclarationBody = Nonterminal.CreateUntyped("struct-declaration-body");
        _interfaceDeclarationBody = Nonterminal.CreateUntyped("interface-declaration-body");
        _enumDeclarationBody = Nonterminal.CreateUntyped("enum-declaration-body");
        _delegateDeclarationBody = Nonterminal.CreateUntyped("delegate-declaration-body");

        _classBase = Nonterminal.CreateUntyped("class-base");
        _classBaseOpt = Nonterminal.CreateUntyped("class-base-opt");
        _interfaceBase = Nonterminal.CreateUntyped("interface-base");
        _interfaceBaseOpt = Nonterminal.CreateUntyped("interface-base-opt");
        _structInterfaces = Nonterminal.CreateUntyped("struct-interfaces");
        _structInterfacesOpt = Nonterminal.CreateUntyped("struct-interfaces-opt");
        _enumBase = Nonterminal.CreateUntyped("enum-base");
        _enumBaseOpt = Nonterminal.CreateUntyped("enum-base-opt");
        _typeNameList = Nonterminal.CreateUntyped("type-name-list");
        _integralType = Nonterminal.CreateUntyped("integral-type");

        _classBody = Nonterminal.CreateUntyped("class-body");
        _structBody = Nonterminal.CreateUntyped("struct-body");
        _interfaceBody = Nonterminal.CreateUntyped("interface-body");
        _enumBody = Nonterminal.CreateUntyped("enum-body");

        _classMemberDeclarations = Nonterminal.CreateUntyped("class-member-declarations");
        _classMemberDeclaration = Nonterminal.CreateUntyped("class-member-declaration");
        _classMemberBody = Nonterminal.CreateUntyped("class-member-body");

        _interfaceMemberDeclarations = Nonterminal.CreateUntyped("interface-member-declarations");
        _interfaceMemberDeclaration = Nonterminal.CreateUntyped("interface-member-declaration");
        _interfaceMemberBody = Nonterminal.CreateUntyped("interface-member-body");

        _constantDeclarationBody = Nonterminal.CreateUntyped("constant-declaration-body");
        _eventDeclarationBody = Nonterminal.CreateUntyped("event-declaration-body");
        _fixedBufferDeclarationBody = Nonterminal.CreateUntyped("fixed-buffer-declaration-body");
        _destructorDeclarationBody = Nonterminal.CreateUntyped("destructor-declaration-body");
        _constructorDeclarationBody = Nonterminal.CreateUntyped("constructor-declaration-body");
        _operatorDeclarationBody = Nonterminal.CreateUntyped("operator-declaration-body");

        _memberSuffix = Nonterminal.CreateUntyped("member-suffix");
        _memberAfterName = Nonterminal.CreateUntyped("member-after-name");
        _explicitMemberName = Nonterminal.CreateUntyped("explicit-member-name");
        _explicitThisName = Nonterminal.CreateUntyped("explicit-this-name");

        _methodBody = Nonterminal.CreateUntyped("method-body");
        _accessorDeclarations = Nonterminal.CreateUntyped("accessor-declarations");
        _accessorDeclaration = Nonterminal.CreateUntyped("accessor-declaration");
        _accessorBody = Nonterminal.CreateUntyped("accessor-body");
        _eventAccessorDeclarations = Nonterminal.CreateUntyped("event-accessor-declarations");
        _eventAccessorDeclaration = Nonterminal.CreateUntyped("event-accessor-declaration");
        _interfaceAccessorDeclarations = Nonterminal.CreateUntyped("interface-accessor-declarations");
        _interfaceAccessorDeclaration = Nonterminal.CreateUntyped("interface-accessor-declaration");

        _formalParameterList = Nonterminal.CreateUntyped("formal-parameter-list");
        _formalParameter = Nonterminal.CreateUntyped("formal-parameter");
        _fixedParameter = Nonterminal.CreateUntyped("fixed-parameter");
        _parameterArray = Nonterminal.CreateUntyped("parameter-array");
        _parameterModifier = Nonterminal.CreateUntyped("parameter-modifier");

        _constructorInitializer = Nonterminal.CreateUntyped("constructor-initializer");
        _constructorInitializerOpt = Nonterminal.CreateUntyped("constructor-initializer-opt");
        _operatorBody = Nonterminal.CreateUntyped("operator-body");

        _fixedBufferDeclarators = Nonterminal.CreateUntyped("fixed-buffer-declarators");
        _fixedBufferDeclarator = Nonterminal.CreateUntyped("fixed-buffer-declarator");
        _overloadableOperator = Nonterminal.CreateUntyped("overloadable-operator");

        _enumMemberDeclarations = Nonterminal.CreateUntyped("enum-member-declarations");
        _enumMemberDeclaration = Nonterminal.CreateUntyped("enum-member-declaration");
        _semicolonOpt = Nonterminal.CreateUntyped("semicolon-opt");

        // Attribute sections at the top level can have a target (e.g. [assembly: ...]);
        // they are accepted before the first top-level member. Farkle has no way to
        // distinguish global attributes from member attributes syntactically, so they
        // are attached to the following member in the CST. See REPORT.md.
        _compilationUnit.SetProductions(
            P(_usingDirectivesOpt, _topLevelNamespaceMemberDeclarationsOpt));

        _usingDirectives.SetProductions(
            P(_usingDirective),
            P(_usingDirectives, _usingDirective));

        _usingDirectivesOpt.SetProductions(
            P(),
            P(_usingDirectives));

        _usingDirective.SetProductions(
            P(_usingAliasDirective),
            P(_usingNamespaceDirective));

        _usingAliasDirective.SetProductions(
            P("using", _identifier, "=", _namespaceOrTypeName, ";"));

        _usingNamespaceDirective.SetProductions(
            P("using", _namespaceOrTypeName, ";"));

        _attributeSections.SetProductions(
            P(_attributeSection),
            P(_attributeSections, _attributeSection));

        _attributeSectionsOpt.SetProductions(
            P(),
            P(_attributeSections));

        _attributeSection.SetProductions(
            P("[", _attributeList, "]"),
            P("[", _attributeTargetSpecifier, _attributeList, "]"));

        // Attribute target names are contextual. Most are identifiers; 'return'
        // and 'event' are reserved keywords but are still valid targets.
        _attributeTarget.SetProductions(
            P(_identifier),
            P("return"),
            P("event"));

        _attributeTargetSpecifier.SetProductions(
            P(_attributeTarget, ":"));

        _attributeList.SetProductions(
            P(_attribute),
            P(_attributeList, ",", _attribute));

        _attribute.SetProductions(
            P(_attributeName),
            P(_attributeName, _attributeArguments));

        _attributeName.SetProductions(
            P(_typeName));

        _attributeArguments.SetProductions(
            P("(", ")"),
            P("(", _attributeArgumentList, ")"));

        _attributeArgumentList.SetProductions(
            P(_positionalArgumentList),
            P(_positionalArgumentList, ",", _namedArgumentList),
            P(_namedArgumentList));

        _positionalArgumentList.SetProductions(
            P(_positionalArgument),
            P(_positionalArgumentList, ",", _positionalArgument));

        _positionalArgument.SetProductions(
            P(_conditionalExpression));

        _namedArgumentList.SetProductions(
            P(_namedArgument),
            P(_namedArgumentList, ",", _namedArgument));

        _namedArgument.SetProductions(
            P(_identifier, "=", _conditionalExpression));

        _modifier.SetProductions(
            P("public"), P("protected"), P("internal"), P("private"),
            P("new"), P("static"), P("readonly"), P("volatile"),
            P("virtual"), P("override"), P("abstract"), P("sealed"),
            P("extern"), P("unsafe"));

        _modifiers.SetProductions(
            P(_modifier),
            P(_modifiers, _modifier));

        _modifiersOpt.SetProductions(
            P(),
            P(_modifiers));

        _namespaceDeclaration.SetProductions(
            P("namespace", _namespaceOrTypeName, _namespaceBody),
            P("namespace", _namespaceOrTypeName, _namespaceBody, ";"));

        _namespaceBody.SetProductions(
            P("{", _usingDirectivesOpt, _namespaceMemberDeclarationsOpt, "}"));

        _namespaceMemberDeclarations.SetProductions(
            P(_namespaceMemberDeclaration),
            P(_namespaceMemberDeclarations, _namespaceMemberDeclaration));

        _namespaceMemberDeclarationsOpt.SetProductions(
            P(),
            P(_namespaceMemberDeclarations));

        _namespaceMemberDeclaration.SetProductions(
            P(_namespaceDeclaration),
            P(_typeDeclaration));

        _topLevelNamespaceMemberDeclarations.SetProductions(
            P(_topLevelNamespaceMemberDeclaration),
            P(_topLevelNamespaceMemberDeclarations, _topLevelNamespaceMemberDeclaration));

        _topLevelNamespaceMemberDeclarationsOpt.SetProductions(
            P(),
            P(_topLevelNamespaceMemberDeclarations));

        _topLevelNamespaceMemberDeclaration.SetProductions(
            P(_attributeSectionsOpt, _namespaceDeclaration),
            P(_attributeSectionsOpt, _topLevelTypeDeclaration));

        _typeDeclaration.SetProductions(
            P(_attributeSectionsOpt, _modifiersOpt, _typeDeclarationBody));

        _topLevelTypeDeclaration.SetProductions(
            P(_modifiersOpt, _typeDeclarationBody));

        _typeDeclarationBody.SetProductions(
            P(_classDeclarationBody),
            P(_structDeclarationBody),
            P(_interfaceDeclarationBody),
            P(_enumDeclarationBody),
            P(_delegateDeclarationBody));

        _classDeclarationBody.SetProductions(
            P("class", _identifier, _classBaseOpt, _classBody, _semicolonOpt));

        _structDeclarationBody.SetProductions(
            P("struct", _identifier, _structInterfacesOpt, _structBody, _semicolonOpt));

        _interfaceDeclarationBody.SetProductions(
            P("interface", _identifier, _interfaceBaseOpt, _interfaceBody, _semicolonOpt));

        _enumDeclarationBody.SetProductions(
            P("enum", _identifier, _enumBaseOpt, _enumBody, _semicolonOpt));

        _delegateDeclarationBody.SetProductions(
            P("delegate", _returnType, _identifier, "(", ")", ";"),
            P("delegate", _returnType, _identifier, "(", _formalParameterList, ")", ";"));

        _classBase.SetProductions(
            P(":", _typeNameList));

        _classBaseOpt.SetProductions(
            P(),
            P(_classBase));

        _interfaceBase.SetProductions(
            P(":", _typeNameList));

        _interfaceBaseOpt.SetProductions(
            P(),
            P(_interfaceBase));

        _structInterfaces.SetProductions(
            P(":", _typeNameList));

        _structInterfacesOpt.SetProductions(
            P(),
            P(_structInterfaces));

        _enumBase.SetProductions(
            P(":", _integralType));

        _enumBaseOpt.SetProductions(
            P(),
            P(_enumBase));

        _typeNameList.SetProductions(
            P(_typeName),
            P(_typeNameList, ",", _typeName));

        _integralType.SetProductions(
            P("sbyte"), P("byte"), P("short"), P("ushort"),
            P("int"), P("uint"), P("long"), P("ulong"));

        _classBody.SetProductions(
            P("{", "}"),
            P("{", _classMemberDeclarations, "}"));

        _structBody.SetProductions(
            P("{", "}"),
            P("{", _classMemberDeclarations, "}"));

        _interfaceBody.SetProductions(
            P("{", "}"),
            P("{", _interfaceMemberDeclarations, "}"));

        _enumBody.SetProductions(
            P("{", "}"),
            P("{", _enumMemberDeclarations, "}"),
            P("{", _enumMemberDeclarations, ",", "}"));

        _classMemberDeclarations.SetProductions(
            P(_classMemberDeclaration),
            P(_classMemberDeclarations, _classMemberDeclaration));

        _classMemberDeclaration.SetProductions(
            P(_attributeSectionsOpt, _modifiersOpt, _classMemberBody));

        _classMemberBody.SetProductions(
            P(_constantDeclarationBody),
            P(_eventDeclarationBody),
            P(_fixedBufferDeclarationBody),
            P(_destructorDeclarationBody),
            P(_constructorDeclarationBody),
            P(_operatorDeclarationBody),
            P(_returnType, _memberSuffix),
            P(_typeDeclarationBody));

        _interfaceMemberDeclarations.SetProductions(
            P(_interfaceMemberDeclaration),
            P(_interfaceMemberDeclarations, _interfaceMemberDeclaration));

        _interfaceMemberDeclaration.SetProductions(
            P(_attributeSectionsOpt, _interfaceMemberBody),
            P(_attributeSectionsOpt, "new", _interfaceMemberBody));

        _interfaceMemberBody.SetProductions(
            P(_returnType, _identifier, "(", ")", ";"),
            P(_returnType, _identifier, "(", _formalParameterList, ")", ";"),
            P(_returnType, _identifier, "{", _interfaceAccessorDeclarations, "}"),
            P(_returnType, "this", "[", _formalParameterList, "]", "{", _interfaceAccessorDeclarations, "}"),
            P("event", _type, _identifier, ";"),
            P("event", _type, _identifier, "{", _eventAccessorDeclarations, "}"));

        _constantDeclarationBody.SetProductions(
            P("const", _type, _constantDeclarators, ";"));

        _eventDeclarationBody.SetProductions(
            P("event", _type, _variableDeclarators, ";"),
            P("event", _type, _identifier, "{", _eventAccessorDeclarations, "}"),
            P("event", _type, _explicitMemberName, "{", _eventAccessorDeclarations, "}"));

        _fixedBufferDeclarationBody.SetProductions(
            P("fixed", _type, _fixedBufferDeclarators, ";"));

        _destructorDeclarationBody.SetProductions(
            P("~", _identifier, "(", ")", _block));

        _constructorDeclarationBody.SetProductions(
            P(_identifier, "(", ")", _constructorInitializerOpt, _block),
            P(_identifier, "(", _formalParameterList, ")", _constructorInitializerOpt, _block));

        // Unary and binary operators share the same token grammar; the number of
        // parameters after '(' decides the arity. Operator/arity validity is a
        // semantic concern and is not enforced syntactically.
        _operatorDeclarationBody.SetProductions(
            P(_type, "operator", _overloadableOperator, "(", _type, _identifier, ")", _operatorBody),
            P(_type, "operator", _overloadableOperator, "(", _type, _identifier, ",", _type, _identifier, ")", _operatorBody),
            P("implicit", "operator", _type, "(", _type, _identifier, ")", _operatorBody),
            P("explicit", "operator", _type, "(", _type, _identifier, ")", _operatorBody));

        _memberSuffix.SetProductions(
            P(_variableDeclarators, ";"),
            P(_identifier, _memberAfterName),
            P(_explicitMemberName, _memberAfterName),
            P("this", "[", _formalParameterList, "]", "{", _accessorDeclarations, "}"),
            P(_explicitThisName, "[", _formalParameterList, "]", "{", _accessorDeclarations, "}"));

        _memberAfterName.SetProductions(
            P("(", ")", _methodBody),
            P("(", _formalParameterList, ")", _methodBody),
            P("{", _accessorDeclarations, "}"));

        _explicitMemberName.SetProductions(
            P(_identifier, ".", _identifier),
            P(_explicitMemberName, ".", _identifier));

        _explicitThisName.SetProductions(
            P(_identifier, ".", "this"),
            P(_explicitMemberName, ".", "this"));

        _methodBody.SetProductions(
            P(_block),
            P(";"));

        _accessorDeclarations.SetProductions(
            P(_accessorDeclaration),
            P(_accessorDeclarations, _accessorDeclaration));

        _accessorDeclaration.SetProductions(
            P(_attributeSectionsOpt, _identifier, _accessorBody));

        _accessorBody.SetProductions(
            P(_block),
            P(";"));

        _eventAccessorDeclarations.SetProductions(
            P(_eventAccessorDeclaration),
            P(_eventAccessorDeclarations, _eventAccessorDeclaration));

        _eventAccessorDeclaration.SetProductions(
            P(_attributeSectionsOpt, _identifier, _accessorBody));

        _interfaceAccessorDeclarations.SetProductions(
            P(_interfaceAccessorDeclaration),
            P(_interfaceAccessorDeclarations, _interfaceAccessorDeclaration));

        _interfaceAccessorDeclaration.SetProductions(
            P(_attributeSectionsOpt, _identifier, ";"));

        _formalParameterList.SetProductions(
            P(_formalParameter),
            P(_formalParameterList, ",", _formalParameter));

        _formalParameter.SetProductions(
            P(_attributeSectionsOpt, _fixedParameter),
            P(_attributeSectionsOpt, _parameterArray));

        _fixedParameter.SetProductions(
            P(_type, _identifier),
            P(_parameterModifier, _type, _identifier));

        _parameterArray.SetProductions(
            P("params", _type, _identifier));

        _parameterModifier.SetProductions(
            P("ref"),
            P("out"));

        _constructorInitializer.SetProductions(
            P(":", "base", "(", ")"),
            P(":", "base", "(", _argumentList, ")"),
            P(":", "this", "(", ")"),
            P(":", "this", "(", _argumentList, ")"));

        _constructorInitializerOpt.SetProductions(
            P(),
            P(_constructorInitializer));

        _operatorBody.SetProductions(
            P(_block),
            P(";"));

        _fixedBufferDeclarators.SetProductions(
            P(_fixedBufferDeclarator),
            P(_fixedBufferDeclarators, ",", _fixedBufferDeclarator));

        _fixedBufferDeclarator.SetProductions(
            P(_identifier, "[", _constantExpression, "]"));

        _overloadableOperator.SetProductions(
            P("+"), P("-"), P("!"), P("~"), P("++"), P("--"), P("true"), P("false"),
            P("*"), P("/"), P("%"), P("&"), P("|"), P("^"),
            P("<<"), P(">>"), P("=="), P("!="), P("<"), P(">"), P("<="), P(">="));

        _enumMemberDeclarations.SetProductions(
            P(_enumMemberDeclaration),
            P(_enumMemberDeclarations, ",", _enumMemberDeclaration));

        _enumMemberDeclaration.SetProductions(
            P(_attributeSectionsOpt, _identifier),
            P(_attributeSectionsOpt, _identifier, "=", _constantExpression));

        _semicolonOpt.SetProductions(
            P(),
            P(";"));
    }
}
