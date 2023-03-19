using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.Extensions
{
	public static class StringExtensions
	{
		public static string Truncate(this string value, int length)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}

			if (value.Length <= length)
			{
				return value;
			}

			var result = value.Substring(0, length);
			return $"{result} ...";

		}
	}
}
