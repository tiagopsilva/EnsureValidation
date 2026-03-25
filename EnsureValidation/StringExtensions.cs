using System.Diagnostics.CodeAnalysis;

namespace EnsureValidation
{
    internal static class StringExtensions
    {
        public static bool IsEmpty([NotNullWhen(false)] this string? value)
            => value == null || value.Trim().Length == 0;
        
        public static bool IsNotEmpty([NotNullWhen(true)] this string? value)
            => !value.IsEmpty();
    }
}
