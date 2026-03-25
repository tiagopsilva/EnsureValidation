namespace EnsureValidation.Validators;

/// <summary>
/// Validator for collections of objects that implement INotifiable.
/// </summary>
/// <typeparam name="TItem">The type of item in the collection, which must be notifiable.</typeparam>
public class CollectionValidator<TItem>(IEnumerable<TItem>? collection, string field, INotifiable target)
    where TItem : Notifiable<TItem>
{
    /// <summary>
    /// Ensures the collection has an instance (is not null).
    /// </summary>
    /// <param name="message">Optional custom notification message.</param>
    /// <returns>The current validator instance for chaining.</returns>
    public CollectionValidator<TItem> Required(string? message = null)
    {
        if (collection is null)
            target.AddNotification(field, message ?? Messages.Required(field));

        return this;
    }

    /// <summary>
    /// Iterates over each item in the collection, checks if they are valid, and aggregates their notifications
    /// to the parent object, prefixing the field name with the index for context.
    /// </summary>
    /// <returns>The current validator instance for possible future chaining.</returns>
    public CollectionValidator<TItem> AreValid()
    {
        if (collection is null)
            return this;

        var items = collection.ToList();
        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];

            item.Validate();

            if (item is Notifiable<TItem> notifiableItem && notifiableItem.IsInvalid)
                foreach (var notification in notifiableItem.Notifications)
                    target.AddNotification($"{field}[{i}].{notification.Field}", notification.Message);
        }

        return this;
    }
}
