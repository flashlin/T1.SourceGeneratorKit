using System;
using System.Collections.Generic;

namespace T1.SourceGeneratorKit.Extensions
{
	public static class CodeStringExtension
	{
		public static string JoinToLines<T>(this IEnumerable<T> items)
		{
			return string.Join($"{Environment.NewLine}", items);
		}

		public static string JoinWith<T>(this IEnumerable<T> items, string separate)
		{
			return string.Join(separate, items);
		}
	}
}
