using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Text;

namespace T1.SourceGeneratorKit.Builders
{
	public class DefaultSourceFileWriter : ISourceFileWriter
	{
		private readonly GeneratorExecutionContext _context;

		public DefaultSourceFileWriter(GeneratorExecutionContext context)
		{
			_context = context;
		}

		public void WriteFiles(IEnumerable<(string Filename, string Source)> sourceFiles)
		{
			foreach (var sourceFile in sourceFiles)
			{
				AddFile(sourceFile);
			}
		}

		protected virtual void AddFile((string Filename, string Source) sourceFile)
		{
			_context.AddSource(sourceFile.Filename, SourceText.From(sourceFile.Source, Encoding.UTF8));
		}
	}
}
