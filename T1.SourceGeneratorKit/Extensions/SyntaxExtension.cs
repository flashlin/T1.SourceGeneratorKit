using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using T1.SourceGeneratorKit.Extensions;

namespace T1.SourceGeneratorKit.Extensions
{
	public static class SyntaxExtension
	{
		public static IEnumerable<InvocationExpressionSyntax> GetInvocationSyntaxNodes(this MethodDeclarationSyntax methodSyntax)
		{
			foreach (var child in methodSyntax.ChildNodes().OfType<BlockSyntax>())
			{
				foreach (var statement in child.Statements.OfType<ExpressionStatementSyntax>())
				{
					var invocation = statement.Expression as InvocationExpressionSyntax;
					if (invocation != null)
					{
						yield return invocation;
					}
				}
			}
		}

		public static string GetIdentifierText(this InvocationExpressionSyntax invocation)
		{
			var memberAccess = invocation.Expression as MemberAccessExpressionSyntax;
			var syntaxList = memberAccess.ChildNodes().ToArray();
			var identifierName = syntaxList.OfType<IdentifierNameSyntax>().First();
			return identifierName.Identifier.ValueText;
		}

		public static string GetMethodName(this InvocationExpressionSyntax invocation)
		{
			var memberAccess = invocation.Expression as MemberAccessExpressionSyntax;
			var syntax = memberAccess.ChildNodes().ToArray()[1];
			var identifier = syntax as GenericNameSyntax;
			return identifier.Identifier.ValueText;
		}

		public static IEnumerable<IdentifierNameSyntax> GetGenericNameArguments(this InvocationExpressionSyntax invocation)
		{
			var memberAccess = invocation.Expression as MemberAccessExpressionSyntax;
			var syntax = memberAccess.ChildNodes().ToArray()[1];
			var identifier = syntax as GenericNameSyntax;
			return identifier.GetGenericNameArguments();
		}

		public static IEnumerable<IdentifierNameSyntax> GetGenericNameArguments(this GenericNameSyntax syntax)
		{
			var typeArgListSyntax = syntax.ChildNodes().OfType<TypeArgumentListSyntax>().First();
			return typeArgListSyntax.Arguments
				.Where(x => x is IdentifierNameSyntax)
				.Cast<IdentifierNameSyntax>();
		}

		public static INamespaceSymbol GetContainingNamespace(this IdentifierNameSyntax nameSyntax, Compilation compilation)
		{
			var typeInfo = nameSyntax.GetTypeInfo(compilation);
			var namespaceName = ((INamedTypeSymbol)typeInfo.Type).ContainingNamespace;
			return namespaceName;
		}

		public static TypeInfo GetTypeInfo(this IdentifierNameSyntax nameSyntax, Compilation compilation)
		{
			var semanticModel = compilation.GetSemanticModel(nameSyntax.SyntaxTree);
			var typeInfo = semanticModel.GetTypeInfo(nameSyntax);
			return typeInfo;
		}

		public static IEnumerable<IMethodSymbol> GetAllMethodsSymbols(this IdentifierNameSyntax nameSyntax, Compilation compilation)
		{
			var typeInfo = nameSyntax.GetTypeInfo(compilation);
			return typeInfo.Type.GetMembers()
				.Where(x => x.Kind == SymbolKind.Method)
				.Cast<IMethodSymbol>();
		}

		public static IEnumerable<IMethodSymbol> GetMethodsSymbols(this IdentifierNameSyntax identifierNameSyntax, Compilation compilation)
		{
			var methodsSymbols = identifierNameSyntax.GetAllMethodsSymbols(compilation);
			foreach (var methodSymbol in methodsSymbols)
			{
				var methodName = methodSymbol.Name;
				if (methodName.StartsWith("get_"))
				{
					continue;
				}
				if (methodName.StartsWith("set_"))
				{
					continue;
				}
				yield return methodSymbol;
			}
		}
	}

	public class TypeNameInfo
	{
		public string ContainingTypename { get; set; }
		public string Identifier { get; set; }
	}
}
