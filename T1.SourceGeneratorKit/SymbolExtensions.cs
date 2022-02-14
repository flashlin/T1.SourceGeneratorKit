using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace T1.SourceGeneratorKit
{
	public static class SymbolExtensions
	{
		public static bool HasAttribute(this ISymbol symbol, string atrributeName)
		{
			string atrributeName2 = atrributeName;
			return symbol.GetAttributes().Any((AttributeData _) => _.AttributeClass?.ToDisplayString() == atrributeName2);
		}

		public static AttributeData FindAttribute(this ISymbol symbol, string atrributeName)
		{
			string atrributeName2 = atrributeName;
			return symbol.GetAttributes().FirstOrDefault((AttributeData _) => _.AttributeClass?.ToDisplayString() == atrributeName2);
		}

		public static bool IsDerivedFromType(this INamedTypeSymbol symbol, string typeName)
		{
			if (symbol.Name == typeName)
			{
				return true;
			}

			if (symbol.BaseType == null)
			{
				return false;
			}

			return symbol.BaseType.IsDerivedFromType(typeName);
		}

		public static bool IsImplements(this INamedTypeSymbol symbol, string typeName)
		{
			string typeName2 = typeName;
			return symbol.AllInterfaces.Any((INamedTypeSymbol _) => _.ToDisplayString() == typeName2);
		}

		public static IEnumerable<IPropertySymbol> GetSymbolProperties(this ITypeSymbol classSymbol)
		{
			var targetSymbolMembers = classSymbol.GetMembers().OfType<IPropertySymbol>()
				 .Where(x => x.SetMethod != null && x.CanBeReferencedByName);
			return targetSymbolMembers;
		}
	}

	public static class SyntaxExtension
	{
		public static void GetNode(this GeneratorSyntaxContext syntaxContext)
		{
			//syntaxContext.Node
		}

		public static void GetInvocationSyntaxNodes(this MethodDeclarationSyntax method)
		{
			//Console.WriteLine(method.Identifier);
			foreach (var child in method.ChildNodes().OfType<InvocationExpressionSyntax>())
			{
				var id = child.Expression as IdentifierNameSyntax;
				Console.WriteLine(id.Identifier.ValueText);
			}
		}
	}

	public class SyntaxNodeVisitor
	{
		public List<MethodSyntaxInfo> Methods
		{
			get;
		} = new List<MethodSyntaxInfo>();

		public List<PropertySyntaxInfo> Properties
		{
			get;
		} = new List<PropertySyntaxInfo>();

		public List<FieldSyntaxInfo> Fields
		{
			get;
		} = new List<FieldSyntaxInfo>();


		public List<ClassSyntaxInfo> Classes
		{
			get;
		} = new List<ClassSyntaxInfo>();


		public void Visit(GeneratorSyntaxContext context)
		{
			var node = context.Node;
			var methodDeclarationSyntax = node as MethodDeclarationSyntax;
			if (methodDeclarationSyntax != null)
			{
				OnVisitMethodDeclaration(methodDeclarationSyntax, context.SemanticModel);
			}

			var propertyDeclarationSyntax = node as PropertyDeclarationSyntax;
			if (propertyDeclarationSyntax != null)
			{
				OnVisitPropertyDeclaration(propertyDeclarationSyntax, context.SemanticModel);
			}

			var fieldDeclarationSyntax = node as FieldDeclarationSyntax;
			if (fieldDeclarationSyntax == null)
			{
				OnVisitFieldDeclaration(fieldDeclarationSyntax, context.SemanticModel);
			}

			var classDeclarationSyntax = node as ClassDeclarationSyntax;
			if (classDeclarationSyntax != null)
			{
				OnVisitClassDeclaration(classDeclarationSyntax, context.SemanticModel);
			}
		}

		protected virtual void OnVisitClassDeclaration(ClassDeclarationSyntax classDeclarationSyntax, SemanticModel model)
		{
			var namedTypeSymbol = model.GetDeclaredSymbol(classDeclarationSyntax) as INamedTypeSymbol;
			if (namedTypeSymbol != null)
			{
				Classes.Add(new ClassSyntaxInfo()
				{
					Syntax = classDeclarationSyntax,
					Model = model,
					Symbol = namedTypeSymbol,
				});
			}
		}

		protected virtual void OnVisitFieldDeclaration(FieldDeclarationSyntax fieldDeclarationSyntax, SemanticModel model)
		{
			IFieldSymbol fieldSymbol = model.GetDeclaredSymbol(fieldDeclarationSyntax) as IFieldSymbol;
			if (fieldSymbol != null)
			{
				Fields.Add(new FieldSyntaxInfo()
				{
					Syntax = fieldDeclarationSyntax,
					Model = model,
					Symbol = fieldSymbol
				});
			}
		}

		protected virtual void OnVisitPropertyDeclaration(PropertyDeclarationSyntax propertyDeclarationSyntax, SemanticModel model)
		{
			var propertySymbol = model.GetDeclaredSymbol(propertyDeclarationSyntax) as IPropertySymbol;
			if (propertySymbol != null)
			{
				Properties.Add(new PropertySyntaxInfo()
				{
					Syntax = propertyDeclarationSyntax,
					Model = model,
					Symbol = propertySymbol
				});
			}
		}

		protected virtual void OnVisitMethodDeclaration(MethodDeclarationSyntax methodDeclarationSyntax, SemanticModel model)
		{
			var methodSymbol = model.GetDeclaredSymbol(methodDeclarationSyntax) as IMethodSymbol;
			if (methodSymbol != null)
			{
				Methods.Add(new MethodSyntaxInfo()
				{
					Syntax = methodDeclarationSyntax,
					Model = model,
					Symbol = methodSymbol
				});
			}
		}
	}

	public class SyntaxInfo
	{
		public SyntaxNode Syntax { get; set; }
		public SemanticModel Model { get; set; }
	}

	public class MethodSyntaxInfo : SyntaxInfo
	{
		public IMethodSymbol Symbol { get; set; }
	}

	public class PropertySyntaxInfo : SyntaxInfo
	{
		public IPropertySymbol Symbol { get; set; }
	}

	public class FieldSyntaxInfo : SyntaxInfo
	{
		public IFieldSymbol Symbol { get; set; }
	}

	public class ClassSyntaxInfo : SyntaxInfo
	{
		public INamedTypeSymbol Symbol { get; set; }
	}
}
