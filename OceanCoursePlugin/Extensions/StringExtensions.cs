using System.Linq;

namespace OceanCoursePlugin.Extensions
{
    public static class StringExtensions
    {
        public static bool HasAnyDigits(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }
            //
            return source.Any(char.IsDigit);
        }
    }
}
