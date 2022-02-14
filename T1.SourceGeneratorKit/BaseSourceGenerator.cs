using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T1.CodeSourceGenerator;
using T1.SourceGeneratorKit.Builders;

namespace T1.SourceGeneratorKit
{
	//[Generator]
	public class BaseSourceGenerator : ISourceGenerator
	{
		private readonly ISourceBuilder _sourceBuilder;

		public BaseSourceGenerator()
		{
			_sourceBuilder = new SourceBuilder();
		}

		public void Execute(GeneratorExecutionContext context)
		{
			var classes = ((MySyntaxReceiver)context.SyntaxContextReceiver)?.Classes;
			//var sourceCode = CreateSourceCode(classes);
			//context.AddSource("helloGenerated.g.cs", SourceText.From(sourceCode, Encoding.UTF8));

			//var fileWriter = new DebugSourceFileWriter(context, $@"D:\demo\code-gen");
			//var fileBuilders = _sourceBuilder.Build(context);
			//fileWriter.WriteFiles(fileBuilders);
		}

		public void Initialize(GeneratorInitializationContext context)
		{
			//System.Diagnostics.Debugger.Launch();
			_sourceBuilder.Initialize(context);
			context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
		}

		class MySyntaxReceiver : SyntaxReceiver
		{
			public override bool CollectClassSymbol
			{
				get;
			} = true;


			protected override bool ShouldCollectClassSymbol(INamedTypeSymbol classSymbol)
			{
				return classSymbol.BaseType.Name == "Object";
			}
		}
	}
}
