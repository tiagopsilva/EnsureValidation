namespace EnsureValidation.Tests;

public class DecimalValidatorTests
{
    private class DecimalTestEntity : Notifiable<DecimalTestEntity>
    {
        public decimal? Salary { get; set; }

        protected override void OnValidate()
        {
            Ensure.That(Salary, nameof(Salary))
                .Required("O salário é obrigatório.")
                .GreaterThanOrEqualTo(1500.50m, "O salário deve ser no mínimo 1500.50.");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenNullableDecimalIsNull()
    {
        // Arrange
        var entity = new DecimalTestEntity { Salary = null };

        // Act
        entity.Validate();

        // Assert
        Assert.True(entity.IsInvalid);
        Assert.Contains(entity.Notifications, n => n.Field == "Salary" && n.Message == "O salário é obrigatório.");
    }

    [Fact]
    public void IsGreaterThanOrEqualTo_ShouldAddNotification_WhenValueIsInvalid()
    {
        // Arrange
        var entity = new DecimalTestEntity { Salary = 1499.99m };

        // Act
        entity.Validate();

        // Assert
        Assert.True(entity.IsInvalid);
        Assert.Contains(entity.Notifications, n => n.Field == "Salary" && n.Message == "O salário deve ser no mínimo 1500.50.");
    }
}