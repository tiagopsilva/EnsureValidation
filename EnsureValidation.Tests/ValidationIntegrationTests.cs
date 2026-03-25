namespace EnsureValidation.Tests;

/// <summary>
/// Tests the integration between the Notifiable class and multiple validators,
/// ensuring that notifications from different types are correctly aggregated.
/// </summary>
public class ValidationIntegrationTests
{
    private class TestEntity : Notifiable<TestEntity>
    {
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public decimal? Salary { get; set; }

        protected override void OnValidate()
        {
            Ensure.That(Name, nameof(Name)).Required("Name is required");
            Ensure.That(Age, nameof(Age)).GreaterThan(17, "Age must be over 17");
            Ensure.That(Salary, nameof(Salary)).GreaterThanOrEqualTo(1000m, "Salary must be at least 1000");
        }
    }

    [Fact]
    public void Should_AggregateNotifications_FromMultipleInvalidValidators()
    {
        // Arrange
        var entity = new TestEntity { Name = "", Age = 15, Salary = 500m };

        // Act
        entity.Validate();

        // Assert
        Assert.True(entity.IsInvalid);
        Assert.Equal(3, entity.Notifications.Count);
        Assert.Contains(entity.Notifications, n => n.Field == "Name");
        Assert.Contains(entity.Notifications, n => n.Field == "Age");
        Assert.Contains(entity.Notifications, n => n.Field == "Salary");
    }
}