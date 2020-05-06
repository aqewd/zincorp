using System;
using System.Linq;

namespace ZinCorp.Common
{
    public static class CommonExtensions
    {
        public static bool OneOf<T>(this T value, params T[] items)
        {
            return items.Any(i => value.Equals(i));
        }

        public static bool IsEqual(this string first, string second, StringComparison culture = StringComparison.CurrentCultureIgnoreCase)
        {
            return string.Equals(first?.Trim(), second?.Trim(), culture);
        }
    }
}
