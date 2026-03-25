namespace EnsureValidation;

/// <summary>
/// Defines the contract for any object that can be notified with validation errors.
/// </summary>
public interface INotifiable
{
    /// <summary>
    /// Adds a validation error notification for a specific field.
    /// </summary>
    void AddNotification(string field, string message);

    /// <summary>
    /// Adds a validation error notification using a Notification object.
    /// </summary>
    void AddNotification(Notification notification);
}
