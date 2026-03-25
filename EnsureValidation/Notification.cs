namespace EnsureValidation;

/// <summary>
/// Represents a single validation notification, containing the field and the message.
/// The use of 'record' ensures immutability and simplifies comparison.
/// </summary>
public record Notification(string Field, string Message);
