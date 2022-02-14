using Microsoft.CodeAnalysis;

namespace T1.SourceGeneratorKit.SyntaxFinders
{
	public class ClassesWithInterfacesReceiver : SyntaxReceiver
	{
		private string implementedInterface;

		public override bool CollectClassSymbol
		{
			get;
		} = true;


		public ClassesWithInterfacesReceiver(string implementedInterface)
		{
			this.implementedInterface = implementedInterface;
		}

		protected override bool ShouldCollectClassSymbol(INamedTypeSymbol classSymbol)
		{
			return classSymbol.IsImplements(implementedInterface);
		}
	}
}
