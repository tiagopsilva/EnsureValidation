namespace EnsureValidation.Validators;

/// <summary>
/// Validator for comparison operations with decimal values.
/// </summary>
public class DecimalValidator(decimal? value, string field, INotifiable target)
{
    /// <summary>
    /// Ensures the property has a value (is not null).
    /// </summary>
    /// <param name="message">Notification message in case of failure.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DecimalValidator Required(string? message = null)
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
    public DecimalValidator EqualTo(decimal comparand, string? message = null)
    {
        if (value.HasValue && value != comparand)
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
    public DecimalValidator NotEqualTo(decimal comparand, string? message = null)
    {
        if (value.HasValue && value.Value == comparand)
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
    public DecimalValidator GreaterThan(decimal comparand, string? message = null)
    {
        if (value.HasValue && value <= comparand)
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
    public DecimalValidator GreaterThanOrEqualTo(decimal comparand, string? message = null)
    {
        if (value.HasValue && value < comparand)
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
    public DecimalValidator LessThan(decimal comparand, string? message = null)
    {
        if (value.HasValue && value >= comparand)
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
    public DecimalValidator LessThanOrEqualTo(decimal comparand, string? message = null)
    {
        if (value.HasValue && value > comparand)
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
    public DecimalValidator Between(decimal from, decimal to, string? message = null)
    {
        if (value.HasValue && (value.Value < from || value.Value > to))
            target.AddNotification(field, message ?? Messages.Between(field, from, to));

        return this;
    }

    /// <summary>
    /// Executes a custom validation using a user-supplied function.
    /// Adds a notification if the function returns <c>false</c>.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="method">
    /// A function that receives the decimal value (or null) and returns <c>true</c> if validation succeeds,
    /// or <c>false</c> otherwise.
    /// </param>
    /// <param name="message">
    /// Notification message in case of failure (optional).
    /// </param>
    /// <returns>
    /// The current validator instance for chaining.
    /// </returns>
    public DecimalValidator Must(Func<decimal?, bool> method, string? message = null)
    {
        if (!method(value))
            target.AddNotification(field, message ?? Messages.Must(field));

        return this;
    }
}