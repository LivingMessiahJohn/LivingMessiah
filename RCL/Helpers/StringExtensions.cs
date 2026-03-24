using System.Text;

namespace RCL.Helpers;

public static class StringExtensions
{
  public static string Truncate(this string value, int maxLength)
  {
    return string.IsNullOrEmpty(value)
      ? value
      : value.Length <= maxLength
        ? value
        : value[..maxLength] + "...";
    /*
    if (string.IsNullOrEmpty(value)) return value;
		return value.Length <= maxLength ? value : value.Substring(0, maxLength); 
    */
  }

  public static StringBuilder AppendWhen(
    this StringBuilder sb, string value, bool predicate) => predicate ? sb.Append(value) : sb;

  public static string PhoneNumber(this string value)
  {
    if (string.IsNullOrEmpty(value)) return string.Empty;
    value = new System.Text.RegularExpressions.Regex(@"\D")
        .Replace(value, string.Empty);
    value = value.TrimStart('1');
    if (value.Length == 7)
      return Convert.ToInt64(value).ToString("###-####");
    if (value.Length == 10)
      return Convert.ToInt64(value).ToString("###-###-####");
    if (value.Length > 10)
      return Convert.ToInt64(value)
          .ToString("###-###-#### " + new String('#', (value.Length - 10)));
    return value;
  }
}

