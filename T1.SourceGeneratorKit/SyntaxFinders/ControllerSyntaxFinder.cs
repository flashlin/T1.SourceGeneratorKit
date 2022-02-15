using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace T1.SourceGeneratorKit.SyntaxFinders
{
	public class ControllerSyntaxFinder : ISyntaxReceiver
	{
		public List<ClassDeclarationSyntax> Controllers { get; } = new List<ClassDeclarationSyntax>();

		public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
		{
			if (syntaxNode is ClassDeclarationSyntax controller)
			{
				if (controller.Identifier.ValueText.EndsWith("Controller"))
				{
					Controllers.Add(controller);
				}
			}
		}
	}
}
