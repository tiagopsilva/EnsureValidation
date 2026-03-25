namespace EnsureValidation.Tests;

public class StringValidatorTests
{
    private class StringTestEntity : Notifiable<StringTestEntity>
    {
        public string? RequiredField { get; set; }
        public string? OptionalFieldWithRules { get; set; }

        protected override void OnValidate()
        {
            Ensure.That(RequiredField, nameof(RequiredField))
                .Required("O campo é obrigatório.")
                .MinLength(5, "Deve ter no mínimo 5 caracteres.")
                .MaxLength(10, "Deve ter no máximo 10 caracteres.");

            Ensure.That(OptionalFieldWithRules, nameof(OptionalFieldWithRules))
                .EmailAddress("Deve ser um e-mail válido.");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenValueIsNull()
    {
        // Arrange
        var entity = new StringTestEntity { RequiredField = null };

        // Act
        entity.Validate();

        // Assert
        Assert.True(entity.IsInvalid);
        Assert.Contains(entity.Notifications, n => n.Field == "RequiredField" && n.Message == "O campo é obrigatório.");
    }

    [Theory]
    [InlineData("abc")] // Less than the minimum
    [InlineData("abcdefghijk")] // Greater than the maximum
    public void LengthChecks_ShouldAddNotification_WhenLengthIsInvalid(string invalidName)
    {
        // Arrange
        var entity = new StringTestEntity { RequiredField = invalidName };

        // Act
        entity.Validate();

        // Assert
        Assert.True(entity.IsInvalid);
        Assert.Contains(entity.Notifications, n =>
            n.Field == "RequiredField" &&
            (n.Message == "Deve ter no mínimo 5 caracteres." || n.Message == "Deve ter no máximo 10 caracteres.")
        );
    }

    [Fact]
    public void IsEmail_ShouldBeValid_WhenOptionalFieldIsNull()
    {
        // Arrange
        var entity = new StringTestEntity { RequiredField = "Valid Name", OptionalFieldWithRules = null };

        // Act
        entity.Validate();

        // Assert
        Assert.True(entity.IsValid);
    }

    [Fact]
    public void IsEmail_ShouldAddNotification_WhenFormatIsInvalid()
    {
        // Arrange
        var entity = new StringTestEntity { RequiredField = "Valid Name", OptionalFieldWithRules = "email_invalido.com" };

        // Act
        entity.Validate();

        // Assert
        Assert.True(entity.IsInvalid);
        Assert.Contains(entity.Notifications, n => n.Field == "OptionalFieldWithRules" && n.Message == "Deve ser um e-mail válido.");
    }
}