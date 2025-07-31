using System.Text;

namespace LivingMessiahAdmin.Helpers;

public static class StringExtensions
{
	public static StringBuilder AppendWhen(
		this StringBuilder sb, string value, bool predicate) => predicate ? sb.Append(value) : sb;


	public static StringBuilder AppendIf(
		this StringBuilder builder, bool condition, string value)
	{
		if (condition) builder.Append(value);
		return builder;
	}

	public static StringBuilder AppendIfElse(
		this StringBuilder builder, bool condition, string ifValue, string elseValue)
	{
		if (condition)
		{
			return builder.Append(ifValue);
		}
		else
		{
			return builder.Append(elseValue);
		}
	}

	public static string Repeat(this string input, int count)
	{
		if (string.IsNullOrEmpty(input) || count <= 1)
			return input;

		var builder = new StringBuilder(input.Length * count);

		for (var i = 0; i < count; i++) builder.Append(input);

		return builder.ToString();
	}

	//ToDo: NOTE BEING USED
	public static string Truncate(this string value, int maxLength)
	{
		if (string.IsNullOrEmpty(value)) return value;
		return value.Length <= maxLength ? value : value.Substring(0, maxLength);
	}
}
