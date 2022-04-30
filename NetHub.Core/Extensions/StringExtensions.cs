using System.Text.RegularExpressions;

namespace NetHub.Core.Extensions;

public static class StringExtensions
{
	public static string CamelCaseToWords(this string str) => Regex.Replace(str, "(\\B[A-Z])", " $1");
}