namespace EnsureValidation.Validators;

/// <summary>
/// Validator for boolean values (nullable or not), allowing fluent validation notifications.
/// </summary>
/// <param name="value">The boolean value to be validated.</param>
/// <param name="field">The name of the field being validated.</param>
/// <param name="target">The notifiable object that will receive the errors.</param>
public class BoolValidator(bool? value, string field, INotifiable target)
{
    /// <summary>
    /// Ensures the boolean property has a value (is not null). Applicable only for bool?.
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public BoolValidator Required(string? message = null)
    {
        if (!value.HasValue)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Checks if the boolean value is true.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public BoolValidator IsTrue(string? message = null)
    {
        if (value.HasValue && value.Value == false)
            target.AddNotification(field, message ?? Messages.IsTrue(field));
        
        return this;
    }

    /// <summary>
    /// Checks if the boolean value is false.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public BoolValidator IsFalse(string? message = null)
    {
        if (value.HasValue && value.Value == true)
            target.AddNotification(field, message ?? Messages.IsFalse(field));

        return this;
    }

    /// <summary>
    /// Executes a custom validation on the boolean value using a provided function.
    /// Skipped if the value is null.
    /// </summary>
    /// <param name="method">
    /// A function that receives the boolean value and returns true if valid;
    /// otherwise, returns false.
    /// </param>
    /// <param name="message"></param>
    /// <returns>
    /// The current <see cref="BoolValidator"/> instance for chaining.
    /// </returns>
    /// <remarks>
    /// If the function returns false, a custom validation notification will be added to the target object.
    /// </remarks>
    public BoolValidator Must(Func<bool?, bool> method, string? message = null)
    {
        if (!method(value))
            target.AddNotification(field, message ?? Messages.Must(field));

        return this;
    }
}