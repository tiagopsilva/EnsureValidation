using System.Text.RegularExpressions;

namespace EnsureValidation.Validators;

/// <summary>
/// Validator for common string operations, such as value and maximum length checks.
/// </summary>
/// <param name="value">The string value to be validated.</param>
/// <param name="field">The name of the field associated with the validation.</param>
/// <param name="target">The object that will receive validation notifications.</param>
public partial class StringValidator(string? value, string field, INotifiable target)
{
    /// <summary>
    /// The name of the field being validated. Exposed for use by extension methods.
    /// </summary>
    public string FieldName => field;

    /// <summary>
    /// Ensures the string has a value (not null, empty, or whitespace only).
    /// </summary>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public StringValidator Required(string? message = null)
    {
        if (value.IsEmpty())
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Checks if the string length is at least the specified minimum.
    /// Skipped if the value is null or empty.
    /// </summary>
    /// <param name="min">The minimum length the string must have.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public StringValidator MinLength(int min, string? message = null)
    {
        if (value.IsNotEmpty() && value.Length < min)
            target.AddNotification(field, message ?? Messages.MinLength(field, min));

        return this;
    }

    /// <summary>
    /// Checks if the string does not exceed the specified maximum length.
    /// </summary>
    /// <param name="max">The maximum allowed length for the string.</param>
    /// <param name="message">Error message to be notified if validation fails.</param>
    /// <returns>The current <see cref="StringValidator"/> instance for chaining.</returns>
    public StringValidator MaxLength(int max, string? message = null)
    {
        if (value.IsNotEmpty() && value.Length > max)
            target.AddNotification(field, message ?? Messages.MaxLength(field, max));
        
        return this;
    }

    /// <summary>
    /// Checks if the string length is exactly equal to the specified value.
    /// Skipped if the value is null or empty.
    /// </summary>
    /// <param name="length">The exact length the string must have.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public StringValidator ExactLength(int length, string? message = null)
    {
        if (value.IsNotEmpty() && value.Length != length)
            target.AddNotification(field, message ?? Messages.ExactLength(field, length));
        
        return this;
    }

    /// <summary>
    /// Checks if the string length, after trimming whitespace, is exactly equal to the specified value.
    /// Skipped if the value is null or empty.
    /// </summary>
    /// <param name="length">The exact length the string must have after trimming.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public StringValidator ExactTrimmedLength(int length, string? message = null)
    {
        if (value.IsNotEmpty() && value.Trim().Length != length)
            target.AddNotification(field, message ?? Messages.ExactTrimmedLength(field, length));
        
        return this;
    }

    /// <summary>
    /// Checks if the string matches a regular expression (regex).
    /// Skipped if the value is null or empty.
    /// </summary>
    /// <param name="pattern">The regular expression to use for validation.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public StringValidator Matches(string pattern, string? message = null)
    {
        if (value.IsNotEmpty() && !Regex.IsMatch(value, pattern, RegexOptions.None, TimeSpan.FromSeconds(2)))
            target.AddNotification(field, message ?? Messages.Matches(field));

        return this;
    }

#if NET7_0_OR_GREATER
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex GetEmailAddressRegex();
#else
    private static readonly Regex _emailAddressRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    private static Regex GetEmailAddressRegex() => _emailAddressRegex;
#endif

    /// <summary>
    /// Verifies if the string is a valid email address format.
    /// Skipped if value is null or empty.
    /// </summary>
    public StringValidator EmailAddress(string? message = null)
    {
        if (value.IsNotEmpty() && !GetEmailAddressRegex().IsMatch(value))
            target.AddNotification(field, message ?? Messages.EmailAddress(field));

        return this;
    }

    /// <summary>
    /// Executes a custom validation using a user-supplied predicate.
    /// </summary>
    public StringValidator Must(Func<string?, bool> method, string? message = null)
    {
        if (!method(value))
            target.AddNotification(field, message ?? Messages.Must(field));

        return this;
    }
}