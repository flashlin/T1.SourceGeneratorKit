using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace T1.SourceGeneratorKit.Builders
{
	public interface ISourceBuilder
   {
      void Initialize(GeneratorInitializationContext context);
      IEnumerable<(string Filename, string Source)> Build(GeneratorExecutionContext context);
   }
}
