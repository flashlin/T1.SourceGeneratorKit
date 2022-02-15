using Microsoft.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;

namespace T1.SourceGeneratorKit.Builders
{
	public class DebugSourceFileWriter : DefaultSourceFileWriter
	{
		private readonly string _outputDirectoryRoot;

		public DebugSourceFileWriter(
			 GeneratorExecutionContext context,
			 string outputDirectoryRoot)
			 : base(context)
		{
			_outputDirectoryRoot = outputDirectoryRoot;
		}

		protected override void AddFile((string Filename, string Source) sourceFile)
		{
			if (!Directory.Exists(_outputDirectoryRoot))
			{
				return;
			}
			var done = false;
			while (!done)
			{
				int cnt = 0;
				try
				{
					var fullFileName = Path.Combine(_outputDirectoryRoot, sourceFile.Filename);
					File.WriteAllText(fullFileName, sourceFile.Source, Encoding.UTF8);
					done = true;
				}
				catch
				{
					cnt++;
					if (cnt > 2)
					{
						done = true;
					}
					Thread.Sleep(100);
				}
			}
		}
	}
}
