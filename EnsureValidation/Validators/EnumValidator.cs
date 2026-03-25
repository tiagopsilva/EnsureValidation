namespace EnsureValidation.Validators;

/// <summary>
/// Validator for enumerated types (<see cref="Enum"/>), allowing checks for required values and defined enum members.
/// </summary>
/// <typeparam name="TEnum">The enum type to be validated.</typeparam>
/// <param name="value">The enum value to be validated.</param>
/// <param name="field">The name of the field associated with the value.</param>
/// <param name="target">The object that will be notified in case of a validation error.</param>
public class EnumValidator<TEnum>(TEnum? value, string field, INotifiable target)
    where TEnum : struct, Enum
{
    /// <summary>
    /// Checks if the enum value was provided (not null).
    /// </summary>
    /// <param name="message">Optional custom error message. If not provided, a default message will be used.</param>
    /// <returns>Returns the current <see cref="EnumValidator{TEnum}"/> instance for method chaining.</returns>
    public EnumValidator<TEnum> Required(string? message = null)
    {
        if (value is null)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Checks if the provided value is defined in the enum type (<typeparamref name="TEnum"/>).
    /// </summary>
    /// <param name="message">Optional custom error message. If not provided, a default message will be used.</param>
    /// <returns>Returns the current <see cref="EnumValidator{TEnum}"/> instance for method chaining.</returns>
    public EnumValidator<TEnum> InEnum(string? message = null)
    {
        if (value.HasValue && !Enum.IsDefined(typeof(TEnum), value))
            target.AddNotification(field, message ?? Messages.InEnum(field));

        return this;
    }
    
    /// <summary>
    /// Checks if the enum value is greater than the value specified in <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The enum value for comparison.</param>
    /// <param name="message">
    /// Optional custom error message.
    /// If not provided, a default message will be used.
    /// </param>
    /// <returns>
    /// Returns the current <see cref="EnumValidator{TEnum}"/> instance for method chaining.
    /// </returns>
    public EnumValidator<TEnum> GreaterThan(TEnum other, string? message = null)
    {
        if (value.HasValue && Comparer<TEnum>.Default.Compare(value.Value, other) <= 0)
            target.AddNotification(field, message ?? Messages.GreaterThan(field, other));
        return this;
    }
}
