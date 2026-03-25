namespace EnsureValidation.Tests;

public class EnumValidatorTests
{
    private enum Status { None = 0, Active = 1, Inactive = 2, Suspended = 3 }

    private class EnumTestEntity : Notifiable<EnumTestEntity>
    {
        protected override void OnValidate() { }

        public void ValidateRequired(Status? value)
        {
            Ensure.ThatEnum(value, "Status").Required("Status is required");
        }

        public void ValidateInEnum(Status? value)
        {
            Ensure.ThatEnum(value, "Status").InEnum("Invalid status value");
        }

        public void ValidateGreaterThan(Status? value, Status comparand)
        {
            Ensure.ThatEnum(value, "Status").GreaterThan(comparand, "Status must be greater");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenNull()
    {
        var entity = new EnumTestEntity();
        entity.ValidateRequired(null);
        Assert.Contains(entity.Notifications, n => n.Field == "Status" && n.Message == "Status is required");
    }

    [Fact]
    public void Required_ShouldNotAddNotification_WhenHasValue()
    {
        var entity = new EnumTestEntity();
        entity.ValidateRequired(Status.Active);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void InEnum_ShouldAddNotification_WhenValueNotDefined()
    {
        var entity = new EnumTestEntity();
        entity.ValidateInEnum((Status)99);
        Assert.Contains(entity.Notifications, n => n.Message == "Invalid status value");
    }

    [Fact]
    public void InEnum_ShouldNotAddNotification_WhenValueIsDefined()
    {
        var entity = new EnumTestEntity();
        entity.ValidateInEnum(Status.Active);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void InEnum_ShouldSkip_WhenNull()
    {
        var entity = new EnumTestEntity();
        entity.ValidateInEnum(null);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void GreaterThan_ShouldAddNotification_WhenNotGreater()
    {
        var entity = new EnumTestEntity();
        entity.ValidateGreaterThan(Status.Active, Status.Inactive);
        Assert.Contains(entity.Notifications, n => n.Message == "Status must be greater");
    }

    [Fact]
    public void GreaterThan_ShouldNotAddNotification_WhenGreater()
    {
        var entity = new EnumTestEntity();
        entity.ValidateGreaterThan(Status.Suspended, Status.Active);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void GreaterThan_ShouldAddNotification_WhenEqual()
    {
        var entity = new EnumTestEntity();
        entity.ValidateGreaterThan(Status.Active, Status.Active);
        Assert.Contains(entity.Notifications, n => n.Message == "Status must be greater");
    }

    [Fact]
    public void GreaterThan_ShouldSkip_WhenNull()
    {
        var entity = new EnumTestEntity();
        entity.ValidateGreaterThan(null, Status.Active);
        Assert.Empty(entity.Notifications);
    }
}
