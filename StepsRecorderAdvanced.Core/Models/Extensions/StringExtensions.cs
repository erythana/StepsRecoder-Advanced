using static System.IO.Path;

namespace StepsRecorderAdvanced.Core.Models.Extensions
{
    public static class StringExtensions
    {
        public static string SanitizeFilename(this string fileString) =>
            GetInvalidFileNameChars().Aggregate(fileString, (current, c) => current.Replace(c, '-'));
    }
}