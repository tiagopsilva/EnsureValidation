namespace EnsureValidation.Validators;

/// <summary>
/// Validator for DateTime values (nullable or not), allowing fluent validation notifications.
/// </summary>
/// <param name="value">The DateTime value to be validated.</param>
/// <param name="field">The name of the field being validated.</param>
/// <param name="target">The notifiable object that will receive the errors.</param>
public class DateTimeValidator(DateTime? value, string field, INotifiable target)
{
    /// <summary>
    /// Ensures the property has a value (is not null).
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DateTimeValidator Required(string? message = null)
    {
        if (!value.HasValue)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Checks if the date is after the specified comparison date.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="dateToCompare">The date to compare against.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DateTimeValidator After(DateTime dateToCompare, string? message = null)
    {
        if (value.HasValue && value.Value <= dateToCompare)
            target.AddNotification(field, message ?? Messages.After(field, dateToCompare));

        return this;
    }

    /// <summary>
    /// Checks if the date is before the specified comparison date.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="dateToCompare">The date to compare against.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DateTimeValidator Before(DateTime dateToCompare, string? message = null)
    {
        if (value.HasValue && value.Value >= dateToCompare)
            target.AddNotification(field, message ?? Messages.Before(field, dateToCompare));

        return this;
    }

    /// <summary>
    /// Checks if the date falls within a range (inclusive).
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="fromDate">The start date of the range.</param>
    /// <param name="toDate">The end date of the range.</param>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DateTimeValidator Between(DateTime fromDate, DateTime toDate, string? message = null)
    {
        if (value.HasValue && (value.Value < fromDate || value.Value > toDate))
            target.AddNotification(field, message ?? Messages.Between(field, fromDate, toDate));

        return this;
    }

    /// <summary>
    /// Checks if the date is in the future.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DateTimeValidator InTheFuture(string? message = null)
    {
        if (value.HasValue && value.Value <= DateTime.UtcNow)
            target.AddNotification(field, message ?? Messages.InTheFuture(field));

        return this;
    }

    /// <summary>
    /// Checks if the date is in the past.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public DateTimeValidator InThePast(string? message = null)
    {
        if (value.HasValue && value.Value >= DateTime.UtcNow)
            target.AddNotification(field, message ?? Messages.InThePast(field));

        return this;
    }

    /// <summary>
    /// Executes a custom validation on the DateTime value using a provided function.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="method">
    /// A function that receives the DateTime? value and returns a boolean indicating whether validation succeeded.
    /// </param>
    /// <param name="message">
    /// Optional custom notification message.
    /// </param>
    /// <returns>
    /// The current validator instance for chaining.
    /// </returns>
    public DateTimeValidator Must(Func<DateTime?, bool> method, string? message = null)
    {
        if (!method(value))
            target.AddNotification(field, message ?? Messages.Must(field));

        return this;
    }
}