using System.Collections.Generic;

namespace T1.SourceGeneratorKit.Builders
{
	public interface ISourceFileWriter
	{
		void WriteFiles(IEnumerable<(string Filename, string Source)> sourceFiles);
	}
}
