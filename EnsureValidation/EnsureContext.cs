using EnsureValidation.Validators;

namespace EnsureValidation
{
    /// <summary>
    /// The DSL entry point. Creates specific validators for each data type.
    /// </summary>
    /// <typeparam name="T">The type of the object that implements <see cref="INotifiable"/> and will receive validation notifications.</typeparam>
    public class EnsureContext<T>(T target) where T : INotifiable
    {
        /// <summary>
        /// Creates a validator for operations with nullable boolean values.
        /// </summary>
        /// <param name="value">The nullable boolean value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>A <see cref="BoolValidator"/> instance for validating the boolean value.</returns>
        public BoolValidator That(bool? value, string fieldName) => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nullable <see cref="DateTime"/> values.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>A <see cref="DateTimeValidator"/> instance for validating the date.</returns>
        public DateTimeValidator That(DateTime? value, string fieldName) => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nullable decimal values.
        /// </summary>
        /// <param name="value">The decimal value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>A <see cref="DecimalValidator"/> instance for validating the decimal value.</returns>
        public DecimalValidator That(decimal? value, string fieldName) => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nullable double values.
        /// </summary>
        /// <param name="value">The double value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>A <see cref="DoubleValidator"/> instance for validating the double value.</returns>
        public DoubleValidator That(double? value, string fieldName) => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nullable <see cref="Guid"/> values.
        /// </summary>
        /// <param name="value">The nullable <see cref="Guid"/> value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>A <see cref="GuidValidator"/> instance for validating the Guid value.</returns>
        public GuidValidator That(Guid? value, string fieldName) => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nullable integer values.
        /// </summary>
        /// <param name="value">The integer value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>A <see cref="IntValidator"/> instance for validating the integer value.</returns>
        public IntValidator That(int? value, string fieldName) => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nullable <see cref="long"/> values.
        /// </summary>
        /// <param name="value">The nullable long value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>A <see cref="LongValidator"/> instance for validating the long value.</returns>
        public LongValidator That(long? value, string fieldName) => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with string values.
        /// </summary>
        /// <param name="value">The string value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>A <see cref="StringValidator"/> instance for validating the string value.</returns>
        public StringValidator That(string? value, string fieldName) => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nullable enum values.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to be validated.</typeparam>
        /// <param name="value">The non-nullable enum value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>
        /// A <see cref="EnumValidator{TEnum}"/> instance for validating the enum value.
        /// Allows checking whether the value is required and defined in the enum.
        /// </returns>
        public EnumValidator<TEnum> ThatEnum<TEnum>(TEnum value, string fieldName)
            where TEnum : struct, Enum
            => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nullable enum values.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to be validated.</typeparam>
        /// <param name="value">The nullable enum value to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the validation.</param>
        /// <returns>
        /// A <see cref="EnumValidator{TEnum}"/> instance for validating the enum value.
        /// Allows checking whether the value is required and defined in the enum.
        /// </returns>
        public EnumValidator<TEnum> ThatEnum<TEnum>(TEnum? value, string fieldName)
            where TEnum : struct, Enum
            => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with nested notifiable objects.
        /// </summary>
        /// <typeparam name="TProperty">The type of the notifiable object to be validated, which must inherit from <see cref="Notifiable{TProperty}"/>.</typeparam>
        /// <param name="value">The notifiable object to be validated.</param>
        /// <param name="fieldName">The name of the field associated with the object's validation.</param>
        /// <returns>
        /// A <see cref="ObjectValidator{TProperty}"/> instance for validating the notifiable object.
        /// Allows performing nested validations and aggregating notifications to the parent object.
        /// </returns>
        public ObjectValidator<TProperty> ThatObject<TProperty>(TProperty? value, string fieldName)
            where TProperty : Notifiable<TProperty>
            => new(value, fieldName, target);

        /// <summary>
        /// Creates a validator for operations with collections.
        /// </summary>
        /// <typeparam name="TItem">The type of items in the collection.</typeparam>
        /// <param name="collection">The collection to be validated.</param>
        /// <param name="fieldName">The name of the field representing the collection.</param>
        /// <returns>A <see cref="CollectionValidator{TItem}"/> instance for validating the collection.</returns>
        public CollectionValidator<TItem> ThatCollection<TItem>(IEnumerable<TItem>? collection, string fieldName)
            where TItem : Notifiable<TItem>
            => new(collection, fieldName, target);
    }
}