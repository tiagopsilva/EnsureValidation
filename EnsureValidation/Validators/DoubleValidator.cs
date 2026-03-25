namespace EnsureValidation.Validators;

/// <summary>
/// Validator for comparison operations with double values.
/// </summary>
public class DoubleValidator(double? value, string field, INotifiable target)
{
    /// <summary>
    /// Ensures the property has a value (is not null).
    /// </summary>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DoubleValidator Required(string? message = null)
    {
        if (!value.HasValue)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Checks if the value is equal to the comparand.
    /// Adds a notification if the condition is not met.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DoubleValidator EqualTo(double comparand, string? message = null)
    {
        if (!value.HasValue)
            return this;

        if (value != comparand)
            target.AddNotification(field, message ?? Messages.EqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is different from the comparand.
    /// Adds a notification if the condition is not met.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message in case of failure (optional).</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DoubleValidator NotEqualTo(double comparand, string? message = null)
    {
        if (!value.HasValue)
            return this;

        if (value.Value == comparand)
            target.AddNotification(field, message ?? Messages.NotEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is greater than the comparand.
    /// Adds a notification if the condition is not met.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DoubleValidator GreaterThan(double comparand, string? message = null)
    {
        if (!value.HasValue)
            return this;

        if (value <= comparand)
            target.AddNotification(field, message ?? Messages.GreaterThan(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is greater than or equal to the comparand.
    /// Adds a notification if the condition is not met.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DoubleValidator GreaterThanOrEqualTo(double comparand, string? message = null)
    {
        if (!value.HasValue)
            return this;

        if (value < comparand)
            target.AddNotification(field, message ?? Messages.GreaterThanOrEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is less than the comparand.
    /// Adds a notification if the condition is not met.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DoubleValidator LessThan(double comparand, string? message = null)
    {
        if (!value.HasValue) return this;

        if (value >= comparand)
            target.AddNotification(field, message ?? Messages.LessThan(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is less than or equal to the comparand.
    /// Adds a notification if the condition is not met.
    /// </summary>
    /// <param name="comparand">The value to compare against.</param>
    /// <param name="message">Notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DoubleValidator LessThanOrEqualTo(double comparand, string? message = null)
    {
        if (!value.HasValue)
            return this;

        if (value > comparand)
            target.AddNotification(field, message ?? Messages.LessThanOrEqualTo(field, comparand));

        return this;
    }

    /// <summary>
    /// Checks if the value is within a range (inclusive).
    /// Adds a notification if the condition is not met.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="from">The lower bound of the range.</param>
    /// <param name="to">The upper bound of the range.</param>
    /// <param name="message">Notification message in case of failure (optional).</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DoubleValidator Between(double from, double to, string? message = null)
    {
        if (!value.HasValue) return this;

        if (value.Value < from || value.Value > to)
            target.AddNotification(field, message ?? Messages.Between(field, from, to));

        return this;
    }

    /// <summary>
    /// Executes a custom validation on the double value.
    /// Adds a notification if the provided function returns false.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="method">
    /// A function that receives the double value (or null) and returns a boolean indicating whether validation succeeded.
    /// </param>
    /// <param name="message">
    /// Notification message in case of failure (optional).
    /// </param>
    /// <returns>
    /// The current <see cref="DoubleValidator"/> instance for chaining.
    /// </returns>
    public DoubleValidator Must(Func<double?, bool> method, string? message = null)
    {
        if (!method(value))
            target.AddNotification(field, message ?? Messages.Must(field));

        return this;
    }
}