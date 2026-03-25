namespace EnsureValidation.Tests;

public class BoolValidatorTests
{
    private class BoolTestEntity : Notifiable<BoolTestEntity>
    {
        public bool? AcceptedTerms { get; set; }

        protected override void OnValidate() { }

        public void ValidateRequired(bool? value)
        {
            Ensure.That(value, nameof(AcceptedTerms)).Required("Field is required");
        }

        public void ValidateIsTrue(bool? value)
        {
            Ensure.That(value, nameof(AcceptedTerms)).IsTrue("Must be true");
        }

        public void ValidateIsFalse(bool? value)
        {
            Ensure.That(value, nameof(AcceptedTerms)).IsFalse("Must be false");
        }

        public void ValidateMust(bool? value)
        {
            Ensure.That(value, nameof(AcceptedTerms)).Must(v => v == true, "Custom rule failed");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenNull()
    {
        var entity = new BoolTestEntity();
        entity.ValidateRequired(null);
        Assert.Contains(entity.Notifications, n => n.Field == "AcceptedTerms" && n.Message == "Field is required");
    }

    [Fact]
    public void Required_ShouldNotAddNotification_WhenHasValue()
    {
        var entity = new BoolTestEntity();
        entity.ValidateRequired(false);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void IsTrue_ShouldAddNotification_WhenFalse()
    {
        var entity = new BoolTestEntity();
        entity.ValidateIsTrue(false);
        Assert.Contains(entity.Notifications, n => n.Message == "Must be true");
    }

    [Fact]
    public void IsTrue_ShouldNotAddNotification_WhenTrue()
    {
        var entity = new BoolTestEntity();
        entity.ValidateIsTrue(true);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void IsTrue_ShouldNotAddNotification_WhenNull()
    {
        var entity = new BoolTestEntity();
        entity.ValidateIsTrue(null);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void IsFalse_ShouldAddNotification_WhenTrue()
    {
        var entity = new BoolTestEntity();
        entity.ValidateIsFalse(true);
        Assert.Contains(entity.Notifications, n => n.Message == "Must be false");
    }

    [Fact]
    public void IsFalse_ShouldNotAddNotification_WhenFalse()
    {
        var entity = new BoolTestEntity();
        entity.ValidateIsFalse(false);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void IsFalse_ShouldNotAddNotification_WhenNull()
    {
        var entity = new BoolTestEntity();
        entity.ValidateIsFalse(null);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void Must_ShouldAddNotification_WhenPredicateFails()
    {
        var entity = new BoolTestEntity();
        entity.ValidateMust(false);
        Assert.Contains(entity.Notifications, n => n.Message == "Custom rule failed");
    }

    [Fact]
    public void Must_ShouldNotAddNotification_WhenPredicatePasses()
    {
        var entity = new BoolTestEntity();
        entity.ValidateMust(true);
        Assert.Empty(entity.Notifications);
    }
}
