using Microsoft.CodeAnalysis;
using T1.SourceGeneratorKit.Extensions;

namespace T1.SourceGeneratorKit.SyntaxFinders
{
	public class MethodsWithAttributeReceiver : SyntaxReceiver
	{
		private string expectedAttribute;

		public override bool CollectMethodSymbol
		{
			get;
		} = true;


		public MethodsWithAttributeReceiver(string expectedAttribute)
		{
			this.expectedAttribute = expectedAttribute;
		}

		protected override bool ShouldCollectMethodSymbol(IMethodSymbol methodSymbol)
		{
			return methodSymbol.HasAttribute(expectedAttribute);
		}
	}
}
