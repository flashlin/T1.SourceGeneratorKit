using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace T1.SourceGeneratorKit
{
	public class SyntaxReceiver : ISyntaxContextReceiver
	{
		public List<MethodSyntaxInfo> Methods
		{
			get;
		} = new List<MethodSyntaxInfo>();


		public List<FieldSyntaxInfo> Fields
		{
			get;
		} = new List<FieldSyntaxInfo>();


		public List<PropertySyntaxInfo> Properties
		{
			get;
		} = new List<PropertySyntaxInfo>();


		public List<ClassSyntaxInfo> Classes
		{
			get;
		} = new List<ClassSyntaxInfo>();


		public virtual bool CollectMethodSymbol
		{
			get;
		}

		public virtual bool CollectFieldSymbol
		{
			get;
		}

		public virtual bool CollectPropertySymbol
		{
			get;
		}

		public virtual bool CollectClassSymbol
		{
			get;
		}

		public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
		{
			SyntaxNode node = context.Node;
			var methodDeclarationSyntax = node as MethodDeclarationSyntax;
			if (methodDeclarationSyntax == null)
			{
				var propertyDeclarationSyntax = node as PropertyDeclarationSyntax;
				if (propertyDeclarationSyntax == null)
				{
					var fieldDeclarationSyntax = node as FieldDeclarationSyntax;
					if (fieldDeclarationSyntax == null)
					{
						var classDeclarationSyntax = node as ClassDeclarationSyntax;
						if (classDeclarationSyntax != null)
						{
							OnVisitClassDeclaration(classDeclarationSyntax, context.SemanticModel);
						}
					}
					else
					{
						OnVisitFieldDeclaration(fieldDeclarationSyntax, context.SemanticModel);
					}
				}
				else
				{
					OnVisitPropertyDeclaration(propertyDeclarationSyntax, context.SemanticModel);
				}
			}
			else
			{
				OnVisitMethodDeclaration(methodDeclarationSyntax, context.SemanticModel);
			}
		}

		protected virtual void OnVisitMethodDeclaration(MethodDeclarationSyntax methodDeclarationSyntax, SemanticModel model)
		{
			if (CollectMethodSymbol && ShouldCollectMethodDeclaration(methodDeclarationSyntax))
			{
				IMethodSymbol methodSymbol = model.GetDeclaredSymbol(methodDeclarationSyntax) as IMethodSymbol;
				if (methodSymbol != null && ShouldCollectMethodSymbol(methodSymbol))
				{
					Methods.Add(new MethodSyntaxInfo
					{
						Syntax = methodDeclarationSyntax,
						Model = model,
						Symbol = methodSymbol
					});
				}
			}
		}

		protected virtual bool ShouldCollectMethodDeclaration(MethodDeclarationSyntax methodDeclarationSyntax)
		{
			return true;
		}

		protected virtual bool ShouldCollectMethodSymbol(IMethodSymbol methodSymbol)
		{
			return true;
		}

		protected virtual void OnVisitFieldDeclaration(FieldDeclarationSyntax fieldDeclarationSyntax, SemanticModel model)
		{
			if (CollectFieldSymbol && ShouldCollectFieldDeclaration(fieldDeclarationSyntax))
			{
				IFieldSymbol fieldSymbol = model.GetDeclaredSymbol(fieldDeclarationSyntax) as IFieldSymbol;
				if (fieldSymbol != null && ShouldCollectFieldSymbol(fieldSymbol))
				{
					Fields.Add(new FieldSyntaxInfo
					{
						Syntax= fieldDeclarationSyntax,
						Model= model,
						Symbol = fieldSymbol
					});
				}
			}
		}

		protected virtual bool ShouldCollectFieldDeclaration(FieldDeclarationSyntax fieldDeclarationSyntax)
		{
			return true;
		}

		protected virtual bool ShouldCollectFieldSymbol(IFieldSymbol fieldSymbol)
		{
			return true;
		}

		protected virtual void OnVisitPropertyDeclaration(PropertyDeclarationSyntax propertyDeclarationSyntax, SemanticModel model)
		{
			if (CollectPropertySymbol && ShouldCollectPropertyDeclaration(propertyDeclarationSyntax))
			{
				IPropertySymbol propertySymbol = model.GetDeclaredSymbol(propertyDeclarationSyntax) as IPropertySymbol;
				if (propertySymbol != null && ShouldCollectPropertySymbol(propertySymbol))
				{
					Properties.Add(new PropertySyntaxInfo
					{
						Syntax = propertyDeclarationSyntax,
						Model = model,
						Symbol = propertySymbol
					});
				}
			}
		}

		protected virtual bool ShouldCollectPropertyDeclaration(PropertyDeclarationSyntax propertyDeclarationSyntax)
		{
			return true;
		}

		protected virtual bool ShouldCollectPropertySymbol(IPropertySymbol propertySymbol)
		{
			return true;
		}

		protected virtual void OnVisitClassDeclaration(ClassDeclarationSyntax classDeclarationSyntax, SemanticModel model)
		{
			if (CollectClassSymbol && ShouldCollectClassDeclaration(classDeclarationSyntax))
			{
				var namedTypeSymbol = model.GetDeclaredSymbol(classDeclarationSyntax) as INamedTypeSymbol;
				if (namedTypeSymbol != null && ShouldCollectClassSymbol(namedTypeSymbol))
				{
					Classes.Add(new ClassSyntaxInfo
					{
						Syntax = classDeclarationSyntax,
						Model = model,
						Symbol = namedTypeSymbol
					});
				}
			}
		}

		protected virtual bool ShouldCollectClassDeclaration(ClassDeclarationSyntax classDeclarationSyntax)
		{
			return true;
		}

		protected virtual bool ShouldCollectClassSymbol(INamedTypeSymbol classSymbol)
		{
			return true;
		}
	}
}
