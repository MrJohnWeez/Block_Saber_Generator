using System;
using System.Text.RegularExpressions;

namespace Utilities
{
    /// <summary>
    /// Functions to extend the string class
    /// </summary>
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) { return value; }

            return value[..Math.Min(value.Length, maxLength)];
        }

        /// <summary>
        /// Returns the first 5 and last 5 characters a a string
        /// </summary>
        /// <param name="value">The string to use</param>
        /// <returns></returns>
        public static string FirstLast5(this string value)
        {
            if (value.Length > 10)
            {
                string first = value.Substring(0, 5);
                string last = value.Substring(value.Length - 5, 5);
                return first + last;
            }

            return value;
        }

        /// <summary>
        /// Determines if a string is null, empty, or spaces
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if string is null, empty, or spaces</returns>
        public static bool IsEmpty(this string value)
        {
            return (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
        }

        /// <summary>
        /// Converts a string to a minecraft safe string
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>A same minecraft string</returns>
        public static string MakeMinecraftSafe(this string value)
        {
            value = value.ToLower();
            Regex rgx = new Regex("[^a-z0-9_-]");
            value = rgx.Replace(value, "");
            rgx = new Regex("[^a-z0-9_]");
            value = rgx.Replace(value, "_");
            return value;
        }
    }
}