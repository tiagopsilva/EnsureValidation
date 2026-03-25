namespace EnsureValidation.Validators;

/// <summary>
/// Validator for integer values, allowing validation notifications to be added.
/// </summary>
public class IntValidator(int? value, string field, INotifiable target)
{
    /// <summary>
    /// Ensures the property has a value (is not null).
    /// </summary>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public IntValidator Required(string? message = null)
    {
        if (!value.HasValue)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Checks if the value is equal to the specified comparand.
    /// Adds a notification if validation fails.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>Returns the current instance for chaining.</returns>
    public IntValidator EqualTo(int comparand, string? message = null)
    {
        if (value.HasValue && value.Value != comparand)
            target.AddNotification(field, message ?? Messages.EqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is different from the specified comparand.
    /// Adds a notification if validation fails.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>Returns the current instance for chaining.</returns>
    public IntValidator NotEqualTo(int comparand, string? message = null)
    {
        if (value.HasValue && value.Value == comparand)
            target.AddNotification(field, message ?? Messages.NotEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is greater than the specified comparand.
    /// Adds a notification if validation fails.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>Returns the current instance for chaining.</returns>
    public IntValidator GreaterThan(int comparand, string? message = null)
    {
        if (value.HasValue && value.Value <= comparand)
            target.AddNotification(field, message ?? Messages.GreaterThan(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is greater than or equal to the specified comparand.
    /// Adds a notification if validation fails.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>Returns the current instance for chaining.</returns>
    public IntValidator GreaterThanOrEqualTo(int comparand, string? message = null)
    {
        if (value.HasValue && value.Value < comparand)
            target.AddNotification(field, message ?? Messages.GreaterThanOrEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is less than the specified comparand.
    /// Adds a notification if validation fails.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>Returns the current instance for chaining.</returns>
    public IntValidator LessThan(int comparand, string? message = null)
    {
        if (value.HasValue && value.Value >= comparand)
            target.AddNotification(field, message ?? Messages.LessThan(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is less than or equal to the specified comparand.
    /// Adds a notification if validation fails.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>Returns the current instance for chaining.</returns>
    public IntValidator LessThanOrEqualTo(int comparand, string? message = null)
    {
        if (value.HasValue && value.Value > comparand)
            target.AddNotification(field, message ?? Messages.LessThanOrEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is within a range (inclusive).
    /// Adds a notification if validation fails.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="from">The lower bound of the range.</param>
    /// <param name="to">The upper bound of the range.</param>
    /// <param name="er">Notification message in case of failure.</param>
    /// <returns>Returns the current instance for chaining.</returns>
    public IntValidator Between(int from, int to, string? er = null)
    {
        if (value.HasValue && (value.Value < from || value.Value > to))
            target.AddNotification(field, er ?? Messages.Between(field, from, to));

        return this;
    }

    /// <summary>
    /// Applies a custom validation function to the value.
    /// Adds a notification if the validation fails.
    /// Ignored if the value is null.
    /// </summary>
    /// <param name="method">A function that receives the value and returns true if valid.</param>
    /// <param name="errorMessage">Notification message in case of failure.</param>
    /// <returns>Returns the current validator instance for chaining.</returns>
    public IntValidator Must(Func<int?, bool> method, string? errorMessage = null)
    {
        if (!method(value))
            target.AddNotification(field, errorMessage ?? Messages.Must(field));

        return this;
    }
}
