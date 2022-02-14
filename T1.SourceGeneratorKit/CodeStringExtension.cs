using System;
using System.Collections.Generic;

namespace T1.CodeSourceGenerator
{
	public static class CodeStringExtension
	{
		public static string JoinToLines<T>(this IEnumerable<T> items)
		{
			return string.Join($"{Environment.NewLine}", items);
		}
	}
}
