using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace T1.SourceGeneratorKit
{
	public static class GeneratorExecutionContextExtension
	{
		public static ImmutableList<ClassDeclarationSyntax> GetControllersDeclarationSyntax(this GeneratorExecutionContext context)
		{
			var controllers = context.Compilation
				 .SyntaxTrees
				 .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes())
				 .Where(x => x is ClassDeclarationSyntax)
				 .Cast<ClassDeclarationSyntax>()
				 .Where(c => c.Identifier.ValueText.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
				 .ToImmutableList();
			return controllers;
		}

		//public void GetFileContext(this GeneratorExecutionContext context, string filenamePattern)
		//{
		//	var myFiles = context.AdditionalFiles.Where(at => at.Path.EndsWith(".xml"));
		//	foreach (var file in myFiles)
		//	{
		//		var content = file.GetText(context.CancellationToken);
		//		return content.ToString();
		//		//// do some transforms based on the file context
		//		//string output = MyXmlToCSharpCompiler.Compile(content);
		//	}
		//}
	}
}
