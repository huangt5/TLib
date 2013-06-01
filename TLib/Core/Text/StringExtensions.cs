using System.Text.RegularExpressions;

namespace TLib.Core.Text
{
    public static class StringExtensions
    {
        /// <summary>
        /// Match Group[1] of regular expression pattern.
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="pattern"></param>
        /// <returns>Empty string if pattern is not matched.</returns>
        public static string Match1(this string inputString, string pattern)
        {
            var match = new Regex(pattern, RegexOptions.IgnoreCase).Match(inputString);
            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }
            return "";
        }
    }
}
