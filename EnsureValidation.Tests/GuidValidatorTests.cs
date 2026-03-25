namespace EnsureValidation.Tests;

public class GuidValidatorTests
{
    private class GuidTestEntity : Notifiable<GuidTestEntity>
    {
        protected override void OnValidate() { }

        public void ValidateRequired(Guid? value)
        {
            Ensure.That(value, "Id").Required("Id is required");
        }

        public void ValidateNotEmpty(Guid? value)
        {
            Ensure.That(value, "Id").NotEmpty("Id must not be empty");
        }

        public void ValidateMust(Guid? value)
        {
            Ensure.That(value, "Id").Must(v => v != null && v.ToString()!.StartsWith("a"), "Must start with 'a'");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenNull()
    {
        var entity = new GuidTestEntity();
        entity.ValidateRequired(null);
        Assert.Contains(entity.Notifications, n => n.Field == "Id" && n.Message == "Id is required");
    }

    [Fact]
    public void Required_ShouldNotAddNotification_WhenHasValue()
    {
        var entity = new GuidTestEntity();
        entity.ValidateRequired(Guid.NewGuid());
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void NotEmpty_ShouldAddNotification_WhenGuidEmpty()
    {
        var entity = new GuidTestEntity();
        entity.ValidateNotEmpty(Guid.Empty);
        Assert.Contains(entity.Notifications, n => n.Field == "Id" && n.Message == "Id must not be empty");
    }

    [Fact]
    public void NotEmpty_ShouldNotAddNotification_WhenGuidHasValue()
    {
        var entity = new GuidTestEntity();
        entity.ValidateNotEmpty(Guid.NewGuid());
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void NotEmpty_ShouldNotAddNotification_WhenNull()
    {
        var entity = new GuidTestEntity();
        entity.ValidateNotEmpty(null);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void Must_ShouldAddNotification_WhenPredicateFails()
    {
        var entity = new GuidTestEntity();
        var guid = Guid.Parse("b0000000-0000-0000-0000-000000000000");
        entity.ValidateMust(guid);
        Assert.Contains(entity.Notifications, n => n.Message == "Must start with 'a'");
    }

    [Fact]
    public void Must_ShouldNotAddNotification_WhenPredicatePasses()
    {
        var entity = new GuidTestEntity();
        var guid = Guid.Parse("a0000000-0000-0000-0000-000000000000");
        entity.ValidateMust(guid);
        Assert.Empty(entity.Notifications);
    }
}
