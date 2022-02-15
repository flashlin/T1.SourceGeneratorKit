using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace T1.SourceGeneratorKit.Extensions
{
	public static class MethodSymbolExtension
	{
		public static string GetTypename(this ITypeSymbol typeSymbol)
		{
			var assemblyName = typeSymbol.ContainingAssembly.Name;
			var typeName = typeSymbol.ContainingType?.Name ?? typeSymbol.Name;
			return $"{assemblyName}.{typeName}";
		}

		public static string GetDeclaringTypename(this IMethodSymbol methodSymbol)
		{
			var assemblyName = methodSymbol.ContainingType.ContainingAssembly.Name;
			return $"{assemblyName}.{methodSymbol.ContainingType.Name}";
		}

		public static string GetReturnTypename(this IMethodSymbol methodSymbol)
		{
			var assemblyName = methodSymbol.ReturnType.ContainingAssembly.Name;
			return methodSymbol.ReturnType.GetTypename();
		}

		public static System.Reflection.MethodInfo GetMethodInfo(this IMethodSymbol methodSymbol)
		{
			var methodArgumentTypenames = methodSymbol.GetArgumentsTypenames()
				.Select(p => p.ContainingTypename);
			var methodInfo = Type.GetType(methodSymbol.GetDeclaringTypename())
				.GetMethod(methodSymbol.Name, methodArgumentTypenames.Select(typename => Type.GetType(typename)).ToArray());
			return methodInfo;
		}

		public static IEnumerable<TypeNameInfo> GetArgumentsTypenames(this IMethodSymbol methodSymbol)
		{
			return methodSymbol.Parameters.Select(p =>
			{
				var typename = p.Type.ContainingNamespace.Name + "." + p.Type.Name;
				var argumentName = p.Name;
				return new TypeNameInfo
				{
					ContainingTypename = typename,
					Identifier = argumentName,
				};
			});
		}
	}
}
