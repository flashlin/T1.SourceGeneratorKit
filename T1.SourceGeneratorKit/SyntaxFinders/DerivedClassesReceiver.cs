using Microsoft.CodeAnalysis;
using T1.SourceGeneratorKit.Extensions;

namespace T1.SourceGeneratorKit.SyntaxFinders
{
	public class DerivedClassesReceiver : SyntaxReceiver
	{
		private string baseTypeName;

		public override bool CollectClassSymbol
		{
			get;
		} = true;


		public DerivedClassesReceiver(string baseTypeName)
		{
			this.baseTypeName = baseTypeName;
		}

		protected override bool ShouldCollectClassSymbol(INamedTypeSymbol classSymbol)
		{
			return classSymbol.IsDerivedFromType(baseTypeName);
		}
	}
}
