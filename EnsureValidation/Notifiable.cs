namespace EnsureValidation;

using System.Text.Json.Serialization;

/// <summary>
/// Abstract base class for all entities and value objects that are validatable.
/// Centralizes the notification collection and provides the DSL entry point.
/// </summary>
public abstract class Notifiable<T> : INotifiable where T : Notifiable<T>
{
    private readonly List<Notification> _notifications = [];

    /// <summary>
    /// Validates the current object by clearing existing notifications and executing the internal validation logic.
    /// Returns <c>true</c> if the object is valid (contains no notifications); otherwise, <c>false</c>.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the object is valid; otherwise, <c>false</c>.
    /// </returns>
    public bool Validate()
    {
        _notifications.Clear();
        OnValidate();
        return IsValid;
    }

    /// <summary>
    /// When implemented in a derived class, contains the logic to validate the object's state
    /// and add any relevant notifications. Called by <see cref="Validate"/>.
    /// </summary>
    protected abstract void OnValidate();

    /// <summary>
    /// Gets the collection of notifications associated with this object.
    /// </summary>
    [JsonIgnore]
    public IReadOnlyList<Notification> Notifications => _notifications.AsReadOnly();

    ///  <summary>
    /// Gets a value indicating whether the object is valid (contains no notifications).
    /// </summary>
    [JsonIgnore]
    public bool IsValid => _notifications.Count == 0;

    /// <summary>
    /// Gets a value indicating whether the object is invalid (contains one or more notifications).
    /// </summary>
    [JsonIgnore]
    public bool IsInvalid => _notifications.Count > 0;

    /// <summary>
    /// Adds a notification to the object.
    /// </summary>
    public void AddNotification(string field, string message)
        => _notifications.Add(new Notification(field, message));

    /// <summary>
    /// Adds a notification to the object.
    /// </summary>
    public void AddNotification(Notification notification)
        => _notifications.Add(notification);

    /// <summary>
    /// Protected property that serves as the fluent entry point for the validation DSL.
    /// E.g.: Ensure.That(Name, "Customer.Name").HasValue(...);
    /// </summary>
    [JsonIgnore]
    public EnsureContext<T> Ensure => new((T)(object)this);

    /// <summary>
    /// Returns all notifications as formatted text suitable for logging.
    /// Each notification is presented in "Field: Message" format on a new line.
    /// </summary>
    /// <returns>A formatted string with all notifications, or an empty string if there are none.</returns>
    public override string ToString() => _notifications?.Format() ?? string.Empty;
}
