using Microsoft.CodeAnalysis;
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
