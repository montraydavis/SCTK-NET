using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json;
using SCTK.Models;
using static TypeScriptParser;

namespace SCTK.Tokenizers
{
    public class TypeScriptTokenizer : ITokenizer
    {
        public Dictionary<string, string> terminalDictionary { get; set; } = new Dictionary<string, string>();
        public IList<SourceCodeToken> Tokens { get; set; } = new List<SourceCodeToken>();

        SourceCodeToken? PreviousToken => Tokens.LastOrDefault();

        bool CanAddTerminal(IParseTree ctx)
        {
            if (ctx.GetType() == typeof(Antlr4.Runtime.Tree.TerminalNodeImpl))
            {
                string ner = "";
                var tokenType = SourceCodeTokenType.NAN;

                if (PreviousToken != null)
                {
                    if (string.IsNullOrWhiteSpace(PreviousToken.Name) == false)
                    {
                        if (PreviousToken.EntityName.IndexOf("_IDENTIFIER_DECL") >= 0)
                        {
                            if (ctx.GetText() == "=")
                            {
                                ner = $"_ASSIGNMENT";
                                tokenType = SourceCodeTokenType.ASSIGNMENT;
                            }
                        }
                        else
                        {
                            switch (PreviousToken.Name)
                            {
                                case "class":
                                    ner = $"_IDENTIFIER";
                                    tokenType = SourceCodeTokenType.IDENTIFIER;
                                    break;

                                case "let":
                                    ner = $"_IDENTIFIER_DECL";
                                    tokenType = SourceCodeTokenType.IDENTIFIER_DECL;
                                    break;
                            }
                        }
                    }
                }



                var tkn = ParseTerminalNode((Antlr4.Runtime.Tree.TerminalNodeImpl)ctx, tokenType);
                tkn.EntityName = $"{tkn.EntityName}{ner}";
                Tokens.Add(tkn);

                return true;
            }

            return false;
        }

        void ParseReferencePrimType(ReferencePrimTypeContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeReferenceContext))
                {
                    ParseTypeReference((TypeReferenceContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParsePredefinedType(PredefinedTypeContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParsePredefinedPrimType(PredefinedPrimTypeContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(PredefinedTypeContext))
                {
                    ParsePredefinedType((PredefinedTypeContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseToken(IParseTree token)
        {
            if (token.GetType() == typeof(Antlr4.Runtime.Tree.TerminalNodeImpl))
            {
                var tkn = ParseTerminalNode((Antlr4.Runtime.Tree.TerminalNodeImpl)token);
                Tokens.Add(tkn);
            }
            else if (token.GetType() == typeof(SourceElementContext))
            {
                ParseSourceElement((SourceElementContext)token);
            }
            else if (token.GetType() == typeof(SourceElementsContext))
            {
                ParseSourceElements((SourceElementsContext)token);
            }
            else if (token.GetType() == typeof(StatementContext))
            {
                ParseStatement((StatementContext)token);
            }
            else if (token.GetType() == typeof(ClassDeclarationContext))
            {
                ParseClassDeclaration((ClassDeclarationContext)token);
            }
            else if (token.GetType() == typeof(TypeParametersContext))
            {
                ParseTypeParameters((TypeParametersContext)token);
            }
            else if (token.GetType() == typeof(TypeParameterListContext))
            {
                ParseTypeParameterList((TypeParameterListContext)token);
            }
            else if (token.GetType() == typeof(TypeParameterContext))
            {
                ParseTypeParameter((TypeParameterContext)token);
            }
            else if (token.GetType() == typeof(ClassHeritageContext))
            {
                ParseClassHeritage((ClassHeritageContext)token);
            }
            else if (token.GetType() == typeof(ClassTailContext))
            {
                ParseClassTail((ClassTailContext)token);
            }
            else if (token.GetType() == typeof(ClassElementContext))
            {
                ParseClassElement((ClassElementContext)token);
            }
            else if (token.GetType() == typeof(PropertyDeclarationExpressionContext))
            {
                ParsePropertyDeclarationExpression((PropertyDeclarationExpressionContext)token);
            }
            else if (token.GetType() == typeof(PropertyMemberBaseContext))
            {
                ParsePropertyMemberBase((PropertyMemberBaseContext)token);
            }
            else if (token.GetType() == typeof(AccessibilityModifierContext))
            {
                ParseAccessibilityModifier((AccessibilityModifierContext)token);
            }
            else if (token.GetType() == typeof(PropertyNameContext))
            {
                ParsePropertyName((PropertyNameContext)token);
            }
            else if (token.GetType() == typeof(IdentifierNameContext))
            {
                ParseIdentifierName((IdentifierNameContext)token);
            }
            else if (token.GetType() == typeof(TypeAnnotationContext))
            {
                ParseTypeAnnotation((TypeAnnotationContext)token);
            }
            else if (token.GetType() == typeof(Type_Context))
            {
                ParseType((Type_Context)token);
            }
            else if (token.GetType() == typeof(PrimaryContext))
            {
                ParsePrimary((PrimaryContext)token);
            }
            else if (token.GetType() == typeof(ReferencePrimTypeContext))
            {
                ParseReferencePrimType((ReferencePrimTypeContext)token);
            }
            else if (token.GetType() == typeof(TypeReferenceContext))
            {
                ParseTypeReference((TypeReferenceContext)token);
            }
            else if (token.GetType() == typeof(TypeNameContext))
            {
                ParseTypeName((TypeNameContext)token);
            }
            else if (token.GetType() == typeof(MethodDeclarationExpressionContext))
            {
                ParseMethodDeclarationExpression((MethodDeclarationExpressionContext)token);
            }
            else if (token.GetType() == typeof(CallSignatureContext))
            {
                ParseCallSignature((CallSignatureContext)token);
            }
            else if (token.GetType() == typeof(ParameterListContext))
            {
                ParseParameterList((ParameterListContext)token);
            }
            else if (token.GetType() == typeof(ParameterContext))
            {
                ParseParameter((ParameterContext)token);
            }
            else if (token.GetType() == typeof(RequiredParameterContext))
            {
                ParseRequiredParameter((RequiredParameterContext)token);
            }
            else if (token.GetType() == typeof(IdentifierOrPatternContext))
            {
                ParseIdentifierOrPattern((IdentifierOrPatternContext)token);
            }
            else if (token.GetType() == typeof(PredefinedPrimTypeContext))
            {
                ParsePredefinedPrimType((PredefinedPrimTypeContext)token);
            }
            else if (token.GetType() == typeof(PredefinedTypeContext))
            {
                ParsePredefinedType((PredefinedTypeContext)token);
            }
            else if (token.GetType() == typeof(FunctionBodyContext))
            {
                ParseFunctionBody((FunctionBodyContext)token);
            }
            else if (token.GetType() == typeof(ExpressionStatementContext))
            {
                ParseExpressionStatement((ExpressionStatementContext)token);
            }
            else if (token.GetType() == typeof(ExpressionSequenceContext))
            {
                ParseExpressionSequence((ExpressionSequenceContext)token);
            }
            else if (token.GetType() == typeof(AssignmentExpressionContext))
            {
                ParseAssignmentExpression((AssignmentExpressionContext)token);
            }
            else if (token.GetType() == typeof(MemberDotExpressionContext))
            {
                ParseMemberDotExpression((MemberDotExpressionContext)token);
            }
            else if (token.GetType() == typeof(ThisExpressionContext))
            {
                ParseThisExpression((ThisExpressionContext)token);
            }
            else if (token.GetType() == typeof(IdentifierExpressionContext))
            {
                ParseIdentifierExpression((IdentifierExpressionContext)token);
            }
            else if (token.GetType() == typeof(ArgumentsContext))
            {
                ParseArguments((ArgumentsContext)token);
            }
            else if (token.GetType() == typeof(ArgumentContext))
            {
                ParseArgument((ArgumentContext)token);
            }
            else if (token.GetType() == typeof(ArgumentsExpressionContext))
            {
                ParseArgumentsExpression((ArgumentsExpressionContext)token);
            }
            else if (token.GetType() == typeof(ArgumentListContext))
            {
                ParseArgumentList((ArgumentListContext)token);
            }
            else if (token.GetType() == typeof(LiteralExpressionContext))
            {
                ParseLiteralExpression((LiteralExpressionContext)token);
            }
            else if (token.GetType() == typeof(LiteralContext))
            {
                ParseLiteral((LiteralContext)token);
            }
            else if (token.GetType() == typeof(TemplateStringLiteralContext))
            {
                ParseTemplateStringLiteral((TemplateStringLiteralContext)token);
            }
            else if (token.GetType() == typeof(TemplateStringAtomContext))
            {
                ParseTemplateStringAtom((TemplateStringAtomContext)token);
            }
            else if (token.GetType() == typeof(VariableStatementContext))
            {
                ParseVariableStatement((VariableStatementContext)token);
            }
            else if (token.GetType() == typeof(VarModifierContext))
            {
                ParseVarModifier((VarModifierContext)token);
            }
            else if (token.GetType() == typeof(VariableDeclarationListContext))
            {
                ParseVariableDeclarationList((VariableDeclarationListContext)token);
            }
            else if (token.GetType() == typeof(VariableDeclarationContext))
            {
                ParseVariableDeclaration((VariableDeclarationContext)token);
            }
            else if (token.GetType() == typeof(IdentifierOrKeyWordContext))
            {
                ParseIdentifierOrKeyWord((IdentifierOrKeyWordContext)token);
            }
            else if (token.GetType() == typeof(NewExpressionContext))
            {
                ParseNewExpression((NewExpressionContext)token);
            }
            else if (token.GetType() == typeof(GenericTypesContext))
            {
                ParseGenericTypes((GenericTypesContext)token);
            }
            else if (token.GetType() == typeof(TypeArgumentsContext))
            {
                ParseTypeArguments((TypeArgumentsContext)token);
            }
            else if (token.GetType() == typeof(TypeArgumentContext))
            {
                ParseTypeArgument((TypeArgumentContext)token);
            }
            else if (token.GetType() == typeof(TypeArgumentListContext))
            {
                ParseTypeArgumentList((TypeArgumentListContext)token);
            }
            else if (token.GetType() == typeof(NumericLiteralContext))
            {
                ParseNumericLiteral((NumericLiteralContext)token);
            }
            else if (token.GetType() == typeof(InterfaceDeclarationContext))
            {
                ParseInterfaceDeclaration((InterfaceDeclarationContext)token);
            }
            else if (token.GetType() == typeof(ObjectTypeContext))
            {
                ParseObjectType((ObjectTypeContext)token);
            }
            else if (token.GetType() == typeof(TypeBodyContext))
            {
                ParseTypeBody((TypeBodyContext)token);
            }
            else if (token.GetType() == typeof(TypeMemberListContext))
            {
                ParseTypeMemberList((TypeMemberListContext)token);
            }
            else if (token.GetType() == typeof(TypeMemberContext))
            {
                ParseTypeMember((TypeMemberContext)token);
            }
            else if (token.GetType() == typeof(MethodSignatureContext))
            {
                ParseMethodSignature((MethodSignatureContext)token);
            }
            else if (token.GetType() == typeof(ImplementsClauseContext))
            {
                ParseImplementsClause((ImplementsClauseContext)token);
            }
            else if (token.GetType() == typeof(ClassOrInterfaceTypeListContext))
            {
                ParseClassOrInterfaceTypeList((ClassOrInterfaceTypeListContext)token);
            }
            else if (token.GetType() == typeof(NestedTypeGenericContext))
            {
                ParseNestedTypeGeneric((NestedTypeGenericContext)token);
            }
            else if (token.GetType() == typeof(TypeGenericContext))
            {
                ParseTypeGeneric((TypeGenericContext)token);
            }
            else
            {
                throw new Exception($"`{token.GetType()}` Not Implemented");
            }
        }

        void Walk(IList<IParseTree> children)
        {
            foreach (var c in children)
            {
                ParseToken(c);
                var grandchildren = new List<IParseTree>();

                for (var i = 0; i < c.ChildCount; i++)
                {
                    grandchildren.Add(c.GetChild(i));
                }

                Walk(grandchildren);
            }
        }

        SourceCodeToken ParseTerminalNode(Antlr4.Runtime.Tree.TerminalNodeImpl terminal, SourceCodeTokenType tokenType = SourceCodeTokenType.NAN)
        {
            if (terminalDictionary!.ContainsKey(terminal.GetText()))
            {
                return new SourceCodeToken(terminal.GetText(), terminalDictionary[terminal.GetText()], tokenType);
            }
            else
            {
                return new SourceCodeToken(terminal.GetText(), "NAN", tokenType);
            }
        }

        void ParseTypeParameter(TypeParameterContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeParameterList(TypeParameterListContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeParameterContext))
                {
                    ParseTypeParameter((TypeParameterContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeParameters(TypeParametersContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeParameterListContext))
                {
                    ParseTypeParameterList((TypeParameterListContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseAccessibilityModifier(AccessibilityModifierContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParsePropertyMemberBase(PropertyMemberBaseContext ctx)
        {
            if (ctx.children == null)
                return;

            foreach (var child in ctx.children)
            {
                if (child.GetType() == typeof(AccessibilityModifierContext))
                {
                    ParseAccessibilityModifier((AccessibilityModifierContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseIdentifierName(IdentifierNameContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParsePropertyName(PropertyNameContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(IdentifierNameContext))
                {
                    ParseIdentifierName((IdentifierNameContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParsePrimary(PrimaryContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ReferencePrimTypeContext))
                {
                    ParseReferencePrimType((ReferencePrimTypeContext)child);
                }
                else if (child.GetType() == typeof(PredefinedPrimTypeContext))
                {
                    ParsePredefinedPrimType((PredefinedPrimTypeContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseType(Type_Context ctx)
        {
            foreach (var child in ctx.children)
            {
                if (child.GetType() == typeof(PrimaryContext))
                {
                    ParsePrimary((PrimaryContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeAnnotation(TypeAnnotationContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(Type_Context))
                {
                    ParseType((Type_Context)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseInitializer(InitializerContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(LiteralExpressionContext))
                {
                    ParseLiteralExpression((LiteralExpressionContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParsePropertyDeclarationExpression(PropertyDeclarationExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(PropertyMemberBaseContext))
                {
                    ParsePropertyMemberBase((PropertyMemberBaseContext)child);
                }
                else if (child.GetType() == typeof(PropertyNameContext))
                {
                    ParsePropertyName((PropertyNameContext)child);
                }
                else if (child.GetType() == typeof(TypeAnnotationContext))
                {
                    ParseTypeAnnotation((TypeAnnotationContext)child);
                }
                else if (child.GetType() == typeof(InitializerContext))
                {
                    ParseInitializer((InitializerContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseIdentifierOrPattern(IdentifierOrPatternContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(IdentifierNameContext))
                {
                    ParseIdentifierName((IdentifierNameContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseRequiredParameter(RequiredParameterContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(IdentifierOrPatternContext))
                {
                    ParseIdentifierOrPattern((IdentifierOrPatternContext)child);
                }
                else if (child.GetType() == typeof(TypeAnnotationContext))
                {
                    ParseTypeAnnotation((TypeAnnotationContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseParameter(ParameterContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(RequiredParameterContext))
                {
                    ParseRequiredParameter((RequiredParameterContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseParameterList(ParameterListContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ParameterContext))
                {
                    ParseParameter((ParameterContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseCallSignature(CallSignatureContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ParameterListContext))
                {
                    ParseParameterList((ParameterListContext)child);
                }
                else if (child.GetType() == typeof(TypeAnnotationContext))
                {
                    ParseTypeAnnotation((TypeAnnotationContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseFunctionBody(FunctionBodyContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(SourceElementsContext))
                {
                    ParseSourceElements((SourceElementsContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseMethodDeclarationExpression(MethodDeclarationExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(PropertyMemberBaseContext))
                {
                    ParsePropertyMemberBase((PropertyMemberBaseContext)child);
                }
                else if (child.GetType() == typeof(PropertyNameContext))
                {
                    ParsePropertyName((PropertyNameContext)child);
                }
                else if (child.GetType() == typeof(CallSignatureContext))
                {
                    ParseCallSignature((CallSignatureContext)child);
                }
                else if (child.GetType() == typeof(FunctionBodyContext))
                {
                    ParseFunctionBody((FunctionBodyContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseFormalParameterArg(FormalParameterArgContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(IdentifierOrKeyWordContext))
                {
                    ParseIdentifierOrKeyWord((IdentifierOrKeyWordContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseFormalParameterList(FormalParameterListContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(FormalParameterArgContext))
                {
                    ParseFormalParameterArg((FormalParameterArgContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseConstructorDeclaration(ConstructorDeclarationContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(PropertyDeclarationExpressionContext))
                {
                    ParsePropertyDeclarationExpression((PropertyDeclarationExpressionContext)child);
                }
                else if (child.GetType() == typeof(FormalParameterListContext))
                {
                    ParseFormalParameterList((FormalParameterListContext)child);
                }
                else if (child.GetType() == typeof(FunctionBodyContext))
                {
                    ParseFunctionBody((FunctionBodyContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseClassElement(ClassElementContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (child.GetType() == typeof(PropertyDeclarationExpressionContext))
                {
                    ParsePropertyDeclarationExpression((PropertyDeclarationExpressionContext)child);
                }
                else if (child.GetType() == typeof(MethodDeclarationExpressionContext))
                {
                    ParseMethodDeclarationExpression((MethodDeclarationExpressionContext)child);
                }
                else if (child.GetType() == typeof(ConstructorDeclarationContext))
                {
                    ParseConstructorDeclaration((ConstructorDeclarationContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseClassTail(ClassTailContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ClassElementContext))
                {
                    ParseClassElement((ClassElementContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeName(TypeNameContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeGeneric(TypeGenericContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeArgumentListContext))
                {
                    ParseTypeArgumentList((TypeArgumentListContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseNestedTypeGeneric(NestedTypeGenericContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeGenericContext))
                {
                    ParseTypeGeneric((TypeGenericContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeReference(TypeReferenceContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeNameContext))
                {
                    ParseTypeName((TypeNameContext)child);
                }
                else if (child.GetType() == typeof(NestedTypeGenericContext))
                {
                    ParseNestedTypeGeneric((NestedTypeGenericContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseClassOrInterfaceTypeList(ClassOrInterfaceTypeListContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeReferenceContext))
                {
                    ParseTypeReference((TypeReferenceContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseImplementsClause(ImplementsClauseContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ClassOrInterfaceTypeListContext))
                {
                    ParseClassOrInterfaceTypeList((ClassOrInterfaceTypeListContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseClassHeritage(ClassHeritageContext ctx)
        {
            if (ctx.children == null)
                return;

            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ImplementsClauseContext))
                {
                    ParseImplementsClause((ImplementsClauseContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseClassDeclaration(ClassDeclarationContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeParametersContext))
                {
                    ParseTypeParameters((TypeParametersContext)child);
                }
                else if (child.GetType() == typeof(ClassHeritageContext))
                {
                    ParseClassHeritage((ClassHeritageContext)child);
                }
                else if (child.GetType() == typeof(ClassTailContext))
                {
                    ParseClassTail((ClassTailContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseThisExpression(ThisExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseMemberDotExpression(MemberDotExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ThisExpressionContext))
                {
                    ParseThisExpression((ThisExpressionContext)child);
                }
                else if (child.GetType() == typeof(IdentifierNameContext))
                {
                    ParseIdentifierName((IdentifierNameContext)child);
                }
                else if (child.GetType() == typeof(IdentifierExpressionContext))
                {
                    ParseIdentifierExpression((IdentifierExpressionContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeArgument(TypeArgumentContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(Type_Context))
                {
                    ParseType((Type_Context)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeArgumentList(TypeArgumentListContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeArgumentContext))
                {
                    ParseTypeArgument((TypeArgumentContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeArguments(TypeArgumentsContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeArgumentListContext))
                {
                    ParseTypeArgumentList((TypeArgumentListContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseGenericTypes(GenericTypesContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeArgumentsContext))
                {
                    ParseTypeArguments((TypeArgumentsContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseIdentifierExpression(IdentifierExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(IdentifierNameContext))
                {
                    ParseIdentifierName((IdentifierNameContext)child);
                }
                else if (child.GetType() == typeof(GenericTypesContext))
                {
                    ParseGenericTypes((GenericTypesContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseAssignmentExpression(AssignmentExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(AssignmentExpressionContext))
                {
                    ParseAssignmentExpression((AssignmentExpressionContext)child);
                }
                else if (child.GetType() == typeof(MemberDotExpressionContext))
                {
                    ParseMemberDotExpression((MemberDotExpressionContext)child);
                }
                else if (child.GetType() == typeof(IdentifierExpressionContext))
                {
                    ParseIdentifierExpression((IdentifierExpressionContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTemplateStringAtom(TemplateStringAtomContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(MemberDotExpressionContext))
                {
                    ParseMemberDotExpression((MemberDotExpressionContext)child);
                }
                else if (child.GetType() == typeof(IdentifierExpressionContext))
                {
                    ParseIdentifierExpression((IdentifierExpressionContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTemplateStringLiteral(TemplateStringLiteralContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TemplateStringAtomContext))
                {
                    ParseTemplateStringAtom((TemplateStringAtomContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }


        void ParseNumericLiteral(NumericLiteralContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseLiteral(LiteralContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TemplateStringLiteralContext))
                {
                    ParseTemplateStringLiteral((TemplateStringLiteralContext)child);
                }
                else if (child.GetType() == typeof(NumericLiteralContext))
                {
                    ParseNumericLiteral((NumericLiteralContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseLiteralExpression(LiteralExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(LiteralContext))
                {
                    ParseLiteral((LiteralContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseArgument(ArgumentContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(LiteralExpressionContext))
                {
                    ParseLiteralExpression((LiteralExpressionContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }


        void ParseArgumentList(ArgumentListContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ArgumentContext))
                {
                    ParseArgument((ArgumentContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseArgumentsContext(ArgumentsContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ArgumentListContext))
                {
                    ParseArgumentList((ArgumentListContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseArgumentsExpression(ArgumentsExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(MemberDotExpressionContext))
                {
                    ParseMemberDotExpression((MemberDotExpressionContext)child);
                }
                else if (child.GetType() == typeof(ArgumentsContext))
                {
                    ParseArgumentsContext((ArgumentsContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseExpressionSequence(ExpressionSequenceContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(AssignmentExpressionContext))
                {
                    ParseAssignmentExpression((AssignmentExpressionContext)child);
                }
                else if (child.GetType() == typeof(ArgumentsExpressionContext))
                {
                    ParseArgumentsExpression((ArgumentsExpressionContext)child);
                }
                else if (child.GetType() == typeof(NewExpressionContext))
                {
                    ParseNewExpression((NewExpressionContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseExpressionStatement(ExpressionStatementContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ExpressionSequenceContext))
                {
                    ParseExpressionSequence((ExpressionSequenceContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseVarModifier(VarModifierContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseIdentifierOrKeyWord(IdentifierOrKeyWordContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseArguments(ArgumentsContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ArgumentContext))
                {
                    ParseArgument((ArgumentContext)child);
                }
                else if (child.GetType() == typeof(ArgumentListContext))
                {
                    ParseArgumentList((ArgumentListContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseNewExpression(NewExpressionContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(IdentifierExpressionContext))
                {
                    ParseIdentifierExpression((IdentifierExpressionContext)child);
                }
                else if (child.GetType() == typeof(ArgumentsContext))
                {
                    ParseArguments((ArgumentsContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseVariableDeclaration(VariableDeclarationContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(IdentifierOrKeyWordContext))
                {
                    ParseIdentifierOrKeyWord((IdentifierOrKeyWordContext)child);
                }
                else if (child.GetType() == typeof(NewExpressionContext))
                {
                    ParseNewExpression((NewExpressionContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseVariableDeclarationList(VariableDeclarationListContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(VariableDeclarationContext))
                {
                    ParseVariableDeclaration((VariableDeclarationContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseVariableStatement(VariableStatementContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(VarModifierContext))
                {
                    ParseVarModifier((VarModifierContext)child);
                }
                else if (child.GetType() == typeof(VariableDeclarationListContext))
                {
                    ParseVariableDeclarationList((VariableDeclarationListContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseMethodSignature(MethodSignatureContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(PropertyNameContext))
                {
                    ParsePropertyName((PropertyNameContext)child);
                }
                else if (child.GetType() == typeof(CallSignatureContext))
                {
                    ParseCallSignature((CallSignatureContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeMember(TypeMemberContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(MethodSignatureContext))
                {
                    ParseMethodSignature((MethodSignatureContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeMemberList(TypeMemberListContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeMemberContext))
                {
                    ParseTypeMember((TypeMemberContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseTypeBody(TypeBodyContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeMemberListContext))
                {
                    ParseTypeMemberList((TypeMemberListContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseObjectType(ObjectTypeContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeBodyContext))
                {
                    ParseTypeBody((TypeBodyContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseInterfaceDeclaration(InterfaceDeclarationContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(TypeParametersContext))
                {
                    ParseTypeParameters((TypeParametersContext)child);
                }
                else if (child.GetType() == typeof(ObjectTypeContext))
                {
                    ParseObjectType((ObjectTypeContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseStatement(StatementContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(ClassDeclarationContext))
                {
                    ParseClassDeclaration((ClassDeclarationContext)child);
                }
                else if (child.GetType() == typeof(ExpressionStatementContext))
                {
                    ParseExpressionStatement((ExpressionStatementContext)child);
                }
                else if (child.GetType() == typeof(VariableStatementContext))
                {
                    ParseVariableStatement((VariableStatementContext)child);
                }
                else if (child.GetType() == typeof(InterfaceDeclarationContext))
                {
                    ParseInterfaceDeclaration((InterfaceDeclarationContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseSourceElement(SourceElementContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (CanAddTerminal((IParseTree)child))
                {

                }
                else if (child.GetType() == typeof(StatementContext))
                {
                    ParseStatement((StatementContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        void ParseSourceElements(SourceElementsContext ctx)
        {
            foreach (var child in ctx.children)
            {
                if (child.GetType() == typeof(SourceElementContext))
                {
                    ParseSourceElement((SourceElementContext)child);
                }
                else
                {
                    throw new Exception($"`{child.GetType()}` Not Implemented");
                }
            }
        }

        public void LoadCorpus(string path)
        {
            var text = File.ReadAllText(path);

            AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
            TypeScriptLexer speakLexer = new TypeScriptLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            TypeScriptParser speakParser = new TypeScriptParser(commonTokenStream);

            var prog = speakParser.program();

            ParseSourceElements(prog.sourceElements());
        }

        public TypeScriptTokenizer(string dictTokensPath = null)
        {
            if (dictTokensPath != null)
            {
                this.terminalDictionary = JsonConvert.DeserializeObject<string[][]>(File.ReadAllText(dictTokensPath))
                .ToDictionary(x => x[0], x => x[1]);

            }


        }
    }
}