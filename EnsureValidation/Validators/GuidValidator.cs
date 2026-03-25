namespace EnsureValidation.Validators;

/// <summary>
/// Validator for Guid values (nullable or not), allowing fluent validation notifications.
/// </summary>
/// <param name="value">The Guid value to be validated.</param>
/// <param name="field">The name of the field being validated.</param>
/// <param name="target">The notifiable object that will receive the errors.</param>
public class GuidValidator(Guid? value, string field, INotifiable target)
{
    /// <summary>
    /// Ensures the Guid property has a value (is not null). Applicable only for Guid?.
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public GuidValidator Required(string? message = null)
    {
        if (!value.HasValue)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Checks if the Guid is not empty (not Guid.Empty).
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public GuidValidator NotEmpty(string? message = null)
    {
        if (value.HasValue && value.Value == Guid.Empty)
            target.AddNotification(field, message ?? Messages.NotEmpty(field));

        return this;
    }
    
    /// <summary>
    /// Executes a custom validation on the Guid value using a provided function.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="method">
    /// A function that receives the Guid? value and returns a boolean indicating whether validation succeeded.
    /// </param>
    /// <param name="message">
    /// Optional custom notification message.
    /// </param>
    /// <returns>
    /// The current <see cref="GuidValidator"/> instance for chaining.
    /// </returns>
    /// <remarks>
    /// If the function returns <c>false</c>, a custom validation notification will be added to the target object.
    /// </remarks>
    public GuidValidator Must(Func<Guid?, bool> method, string? message = null)
    {
        if (!method(value))
            target.AddNotification(field, message ?? Messages.Must(field));

        return this;
    }
}