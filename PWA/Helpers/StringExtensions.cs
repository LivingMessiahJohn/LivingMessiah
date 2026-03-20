using System.Text;

namespace PWA.Helpers;

public static class StringExtensions
{
	public static StringBuilder AppendWhen(
		this StringBuilder sb, string value, bool predicate) => predicate ? sb.Append(value) : sb;

}
