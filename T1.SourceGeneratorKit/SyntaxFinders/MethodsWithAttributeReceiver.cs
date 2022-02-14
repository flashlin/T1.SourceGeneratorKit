using Microsoft.CodeAnalysis;

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
