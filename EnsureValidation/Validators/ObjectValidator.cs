namespace EnsureValidation.Validators;

/// <summary>
/// Validator for properties that are other notifiable objects, allowing nested validations.
/// </summary>
/// <typeparam name="TProperty">The type of the nested object, which must be notifiable.</typeparam>
/// <param name="value">The object to be validated.</param>
/// <param name="field">The name of the field containing the object.</param>
/// <param name="target">The parent notifiable object that will receive the errors.</param>
public class ObjectValidator<TProperty>(TProperty? value, string field, INotifiable target)
    where TProperty : Notifiable<TProperty>
{
    /// <summary>
    /// Ensures the object property has an instance (is not null).
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public ObjectValidator<TProperty> Required(string? message = null)
    {
        if (value is null)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Runs the nested object's validations and aggregates its notifications to the parent object,
    /// prefixing the field name for context.
    /// Skipped if the object is null.
    /// </summary>
    /// <returns>The current validator instance for chaining.</returns>
    public ObjectValidator<TProperty> IsValid()
    {
        if (value is null)
            return this;

        // Runs validation on the nested object; if invalid, copies notifications with prefix
        if (!value.Validate())
        {
            foreach (var notification in value.Notifications)
                target.AddNotification($"{field}.{notification.Field}", notification.Message);
        }

        return this;
    }

    /// <summary>
    /// Executes a custom validation on the nested object using a provided function.
    /// </summary>
    /// <param name="method">
    /// A function that receives the property value and returns a boolean indicating whether validation succeeded.
    /// </param>
    /// <param name="message">
    /// Optional custom notification message.
    /// </param>
    /// <returns>
    /// The current <see cref="ObjectValidator{TProperty}"/> instance for chaining.
    /// </returns>
    /// <remarks>
    /// If the value is not null and the validation function returns false, an error notification is added to the parent object.
    /// </remarks>
    public ObjectValidator<TProperty> Must(Func<TProperty?, bool> method, string? message = null)
    {
        if (!method(value))
            target.AddNotification(field, message ?? Messages.Must(field));

        return this;
    }
}