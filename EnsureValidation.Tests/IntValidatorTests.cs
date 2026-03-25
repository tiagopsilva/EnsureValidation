namespace EnsureValidation.Tests;

public class IntValidatorTests
{
    private class IntTestEntity : Notifiable<IntTestEntity>
    {
        public int? RequiredAge { get; set; }
        public int? OptionalPoints { get; set; }

        protected override void OnValidate()
        {
            Ensure.That(RequiredAge, nameof(RequiredAge))
                .Required("A idade é obrigatória.")
                .Between(18, 99, "A idade deve estar entre 18 e 99.");

            Ensure.That(OptionalPoints, nameof(OptionalPoints))
                .GreaterThan(0, "Se informados, os pontos devem ser maiores que zero.");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenNullableIntIsNull()
    {
        // Arrange
        var entity = new IntTestEntity { RequiredAge = null };

        // Act
        entity.Validate();

        // Assert
        Assert.Contains(entity.Notifications,
            n => n.Field == "RequiredAge" &&
            n.Message == "A idade é obrigatória.");
    }

    [Fact]
    public void IsBetween_ShouldAddNotification_WhenValueIsOutsideRange()
    {
        // Arrange
        var entity = new IntTestEntity { RequiredAge = 17 };

        // Act
        entity.Validate();

        // Assert
        Assert.Contains(entity.Notifications,
            n => n.Field == "RequiredAge" &&
            n.Message == "A idade deve estar entre 18 e 99.");
    }

    [Fact]
    public void OptionalInt_ShouldBeValid_WhenValueIsNull()
    {
        // Arrange
        var entity = new IntTestEntity { RequiredAge = 25, OptionalPoints = null };

        // Act
        entity.Validate();

        // Assert
        Assert.True(entity.IsValid);
    }

    [Fact]
    public void IsGreaterThan_ShouldAddNotification_WhenOptionalValueIsInvalid()
    {
        // Arrange
        var entity = new IntTestEntity { RequiredAge = 30, OptionalPoints = 0 };

        // Act
        entity.Validate();

        // Assert
        Assert.Contains(entity.Notifications, 
            n => n.Field == "OptionalPoints" && 
            n.Message == "Se informados, os pontos devem ser maiores que zero.");
    }
}