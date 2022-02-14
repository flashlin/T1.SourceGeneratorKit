using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace T1.SourceGeneratorKit.Builders
{
	public class SourceBuilder : ISourceBuilder
	{
		public void Initialize(GeneratorInitializationContext context)
		{
		}

		public IEnumerable<(string Filename, string Source)> Build(GeneratorExecutionContext context)
		{
			// Here should be an actual source code generator implementation
			throw new NotImplementedException();
		}
	}
}
