using System.Text;

namespace EnsureValidation;

/// <summary>
/// Provides extension methods for handling notifications.
/// </summary>
public static class NotificationExtensions
{
    /// <summary>
    /// Converts a collection of notifications into a formatted string for use in logs.
    /// Each notification is presented in "Field: Message" format on a new line.
    /// </summary>
    /// <param name="notifications">The collection of notifications to be formatted.</param>
    /// <returns>A formatted string with all notifications, or an empty string if the collection is empty.</returns>
    public static string Format(this IEnumerable<Notification> notifications)
    {
        if (notifications == null || !notifications.Any())
            return string.Empty;

        var sb = new StringBuilder("\nErros:\n");
        foreach (var notification in notifications)
            sb.AppendLine($"{notification.Field}: {notification.Message}");

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Converts a collection of notifications into a formatted string with custom separators.
    /// </summary>
    /// <param name="notifications">The collection of notifications to be formatted.</param>
    /// <param name="fieldMessageSeparator">Separator between the field and the message (default: ': ').</param>
    /// <param name="notificationSeparator">Separator between notifications (default: '; ').</param>
    /// <returns>A formatted string with all notifications, or an empty string if the collection is empty.</returns>
    public static string Format(
        this IEnumerable<Notification> notifications, 
        string fieldMessageSeparator = ": ", 
        string notificationSeparator = "; ")
    {
        if (notifications == null || !notifications.Any())
            return string.Empty;

        return "\nErros:\n" + string.Join(
            notificationSeparator, 
            notifications.Select(n => $"{n.Field}{fieldMessageSeparator}{n.Message}")
        );
    }
}