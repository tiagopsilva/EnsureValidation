namespace EnsureValidation;

/// <summary>
/// Provides a central configuration point and factory for formatted validation messages.
/// Supports customization and internationalization (i18n) of default error messages.
/// </summary>
public static class Messages
{
    /// <summary>
    /// Internal dictionary containing the default messages in English.
    /// Keys match validation method names for consistency.
    /// </summary>
    private static readonly Dictionary<string, string> DefaultMessages = new()
    {
        // General
        { nameof(Required), "The field '{0}' is required." },
        { nameof(NotEmpty), "The field '{0}' cannot be empty." },
        { nameof(Must), "The field '{0}' is invalid." },

        // String
        { nameof(ExactLength), "The field '{0}' must be exactly {1} characters long." },
        { nameof(ExactTrimmedLength), "The field '{0}' must be exactly {1} characters long (trimmed)." },
        { nameof(MinLength), "The field '{0}' must be at least {1} characters long." },
        { nameof(MaxLength), "The field '{0}' must be at most {1} characters long." },
        { nameof(Matches), "The field '{0}' has an invalid format." },
        { nameof(EmailAddress), "The field '{0}' is not a valid email address." },

        // Numeric (Int, Long, Decimal, Double)
        { nameof(EqualTo), "The field '{0}' must be equal to {1}." },
        { nameof(NotEqualTo), "The field '{0}' must not be equal to {1}." },
        { nameof(GreaterThan), "The field '{0}' must be greater than {1}." },
        { nameof(GreaterThanOrEqualTo), "The field '{0}' must be greater than or equal to {1}." },
        { nameof(LessThan), "The field '{0}' must be less than {1}." },
        { nameof(LessThanOrEqualTo), "The field '{0}' must be less than or equal to {1}." },
        { nameof(Between), "The field '{0}' must be between {1} and {2}." },

        // DateTime
        { nameof(After), "The field '{0}' must be after {1}." },
        { nameof(Before), "The field '{0}' must be before {1}." },
        { nameof(InTheFuture), "The field '{0}' must be in the future." },
        { nameof(InThePast), "The field '{0}' must be in the past." },

        // Boolean
        { nameof(IsTrue), "The field '{0}' must be true." },
        { nameof(IsFalse), "The field '{0}' must be false." },

        // Enum
        { nameof(InEnum), "The field '{0}' must contain a valid value." }
    };

    /// <summary>
    /// Defines the function used by the DSL to obtain message format strings.
    /// Defaults to English. Can be replaced at application startup for i18n.
    /// </summary>
    public static Func<string, string> To { get; set; } = (key) =>
        DefaultMessages.GetValueOrDefault(key, $"[No message registered for '{key}']");

    public static string Required(string field)
        => string.Format(To(nameof(Required)), field);

    public static string NotEmpty(string field)
        => string.Format(To(nameof(NotEmpty)), field);

    public static string Must(string field)
        => string.Format(To(nameof(Must)), field);

    public static string ExactLength(string field, int length)
        => string.Format(To(nameof(ExactLength)), field, length);

    public static string ExactTrimmedLength(string field, int length)
        => string.Format(To(nameof(ExactTrimmedLength)), field, length);

    public static string MinLength(string field, int min)
        => string.Format(To(nameof(MinLength)), field, min);

    public static string MaxLength(string field, int max)
        => string.Format(To(nameof(MaxLength)), field, max);

    public static string Matches(string field)
        => string.Format(To(nameof(Matches)), field);

    public static string EmailAddress(string field)
        => string.Format(To(nameof(EmailAddress)), field);

    public static string EqualTo(string field, object value)
        => string.Format(To(nameof(EqualTo)), field, value);

    public static string NotEqualTo(string field, object value)
        => string.Format(To(nameof(NotEqualTo)), field, value);

    public static string GreaterThan(string field, object value)
        => string.Format(To(nameof(GreaterThan)), field, value);

    public static string GreaterThanOrEqualTo(string field, object value)
        => string.Format(To(nameof(GreaterThanOrEqualTo)), field, value);

    public static string LessThan(string field, object value)
        => string.Format(To(nameof(LessThan)), field, value);

    public static string LessThanOrEqualTo(string field, object value)
        => string.Format(To(nameof(LessThanOrEqualTo)), field, value);

    public static string Between(string field, object min, object max)
        => string.Format(To(nameof(Between)), field, min, max);

    public static string After(string field, object date)
        => string.Format(To(nameof(After)), field, date);

    public static string Before(string field, object date)
        => string.Format(To(nameof(Before)), field, date);

    public static string InTheFuture(string field)
        => string.Format(To(nameof(InTheFuture)), field);

    public static string InThePast(string field)
        => string.Format(To(nameof(InThePast)), field);

    public static string IsTrue(string field)
        => string.Format(To(nameof(IsTrue)), field);

    public static string IsFalse(string field)
        => string.Format(To(nameof(IsFalse)), field);

    public static string InEnum(string field)
        => string.Format(To(nameof(InEnum)), field);
}