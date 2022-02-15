using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace T1.SourceGeneratorKit.Extensions
{
	public static class SymbolExtensions
	{
		public static bool HasAttribute(this ISymbol symbol, string atrributeName)
		{
			string atrributeName2 = atrributeName;
			return symbol.GetAttributes().Any((_) => _.AttributeClass?.ToDisplayString() == atrributeName2);
		}

		public static AttributeData FindAttribute(this ISymbol symbol, string atrributeName)
		{
			string atrributeName2 = atrributeName;
			return symbol.GetAttributes().FirstOrDefault((_) => _.AttributeClass?.ToDisplayString() == atrributeName2);
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
			return symbol.AllInterfaces.Any((_) => _.ToDisplayString() == typeName2);
		}

		public static IEnumerable<IPropertySymbol> GetSymbolProperties(this ITypeSymbol classSymbol)
		{
			var targetSymbolMembers = classSymbol.GetMembers().OfType<IPropertySymbol>()
				 .Where(x => x.SetMethod != null && x.CanBeReferencedByName);
			return targetSymbolMembers;
		}
	}
}
