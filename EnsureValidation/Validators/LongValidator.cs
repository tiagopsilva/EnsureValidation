namespace EnsureValidation.Validators;

/// <summary>
/// Validator for long integer values (nullable or not), allowing fluent validation notifications.
/// </summary>
/// <param name="value">The long value to be validated.</param>
/// <param name="field">The name of the field being validated.</param>
/// <param name="target">The notifiable object that will receive the errors.</param>
public class LongValidator(long? value, string field, INotifiable target)
{
    /// <summary>
    /// Ensures the property has a value (is not null).
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator Required(string? message = null)
    {
        // If the value has no value (is null), adds an error notification.
        if (!value.HasValue)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Checks if the value is greater than the comparand. Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The comparison value.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator GreaterThan(long comparand, string? message = null)
    {
        // If the value exists and is not greater than the comparand, adds a notification.
        if (value.HasValue && value.Value <= comparand)
            target.AddNotification(field, message ?? Messages.GreaterThan(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is greater than or equal to the comparand. Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The comparison value.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator GreaterThanOrEqualTo(long comparand, string? message = null)
    {
        // If the value exists and is less than the comparand, adds a notification.
        if (value.HasValue && value.Value < comparand)
            target.AddNotification(field, message ?? Messages.GreaterThanOrEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is equal to the comparand. Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The comparison value.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator EqualTo(long comparand, string? message = null)
    {
        // If the value exists and differs from the comparand, adds a notification.
        if (value.HasValue && value.Value != comparand)
            target.AddNotification(field, message ?? Messages.EqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is within a range (inclusive). Skipped if the value is null.
    /// </summary>
    /// <param name="min">The minimum allowed value.</param>
    /// <param name="max">The maximum allowed value.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator Between(long min, long max, string? message = null)
    {
        // If the value exists and is outside the range, adds a notification.
        if (value.HasValue && (value.Value < min || value.Value > max))
            target.AddNotification(field, message ?? Messages.Between(field, min, max));

        return this;
    }

    /// <summary>
    /// Checks if the value is less than the comparand. Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The comparison value.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator LessThan(long comparand, string? message = null)
    {
        // If the value exists and is not less than the comparand, adds a notification.
        if (value.HasValue && value.Value >= comparand)
            target.AddNotification(field, message ?? Messages.LessThan(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is less than or equal to the comparand. Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The comparison value.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator LessThanOrEqualTo(long comparand, string? message = null)
    {
        // If the value exists and is greater than the comparand, adds a notification.
        if (value.HasValue && value.Value > comparand)
            target.AddNotification(field, message ?? Messages.LessThanOrEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is different from the comparand. Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The comparison value.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator NotEqualTo(long comparand, string? message = null)
    {
        // If the value exists and equals the comparand, adds a notification.
        if (value.HasValue && value.Value == comparand)
            target.AddNotification(field, message ?? Messages.NotEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Allows custom validation using a user-supplied method.
    /// </summary>
    /// <param name="method">A function that receives the value and returns whether it is valid.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public LongValidator Must(Func<long?, bool> method, string? message = null)
    {
        // If the custom function returns false, adds a notification.
        if (!method(value))
            target.AddNotification(field, message ?? Messages.Must(field));

        return this;
    }
}