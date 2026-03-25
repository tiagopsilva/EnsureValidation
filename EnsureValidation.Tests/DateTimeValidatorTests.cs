namespace EnsureValidation.Tests;

public class DateTimeValidatorTests
{
    private class DateTimeTestEntity : Notifiable<DateTimeTestEntity>
    {
        protected override void OnValidate() { }

        public void ValidateRequired(DateTime? value)
        {
            Ensure.That(value, "Date").Required("Date is required");
        }

        public void ValidateAfter(DateTime? value, DateTime comparand)
        {
            Ensure.That(value, "Date").After(comparand, "Date must be after comparand");
        }

        public void ValidateBefore(DateTime? value, DateTime comparand)
        {
            Ensure.That(value, "Date").Before(comparand, "Date must be before comparand");
        }

        public void ValidateBetween(DateTime? value, DateTime from, DateTime to)
        {
            Ensure.That(value, "Date").Between(from, to, "Date must be between range");
        }

        public void ValidateInTheFuture(DateTime? value)
        {
            Ensure.That(value, "Date").InTheFuture("Date must be in the future");
        }

        public void ValidateInThePast(DateTime? value)
        {
            Ensure.That(value, "Date").InThePast("Date must be in the past");
        }

        public void ValidateMust(DateTime? value)
        {
            Ensure.That(value, "Date").Must(v => v?.DayOfWeek == DayOfWeek.Monday, "Must be a Monday");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenNull()
    {
        var entity = new DateTimeTestEntity();
        entity.ValidateRequired(null);
        Assert.Contains(entity.Notifications, n => n.Field == "Date" && n.Message == "Date is required");
    }

    [Fact]
    public void Required_ShouldNotAddNotification_WhenHasValue()
    {
        var entity = new DateTimeTestEntity();
        entity.ValidateRequired(DateTime.Now);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void After_ShouldAddNotification_WhenDateIsNotAfter()
    {
        var entity = new DateTimeTestEntity();
        var comparand = new DateTime(2024, 6, 15);
        entity.ValidateAfter(new DateTime(2024, 6, 14), comparand);
        Assert.Contains(entity.Notifications, n => n.Message == "Date must be after comparand");
    }

    [Fact]
    public void After_ShouldNotAddNotification_WhenDateIsAfter()
    {
        var entity = new DateTimeTestEntity();
        var comparand = new DateTime(2024, 6, 15);
        entity.ValidateAfter(new DateTime(2024, 6, 16), comparand);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void After_ShouldSkip_WhenNull()
    {
        var entity = new DateTimeTestEntity();
        entity.ValidateAfter(null, DateTime.Now);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void Before_ShouldAddNotification_WhenDateIsNotBefore()
    {
        var entity = new DateTimeTestEntity();
        var comparand = new DateTime(2024, 6, 15);
        entity.ValidateBefore(new DateTime(2024, 6, 16), comparand);
        Assert.Contains(entity.Notifications, n => n.Message == "Date must be before comparand");
    }

    [Fact]
    public void Before_ShouldNotAddNotification_WhenDateIsBefore()
    {
        var entity = new DateTimeTestEntity();
        var comparand = new DateTime(2024, 6, 15);
        entity.ValidateBefore(new DateTime(2024, 6, 14), comparand);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void Between_ShouldAddNotification_WhenOutOfRange()
    {
        var entity = new DateTimeTestEntity();
        var from = new DateTime(2024, 1, 1);
        var to = new DateTime(2024, 12, 31);
        entity.ValidateBetween(new DateTime(2025, 1, 1), from, to);
        Assert.Contains(entity.Notifications, n => n.Message == "Date must be between range");
    }

    [Fact]
    public void Between_ShouldNotAddNotification_WhenInRange()
    {
        var entity = new DateTimeTestEntity();
        var from = new DateTime(2024, 1, 1);
        var to = new DateTime(2024, 12, 31);
        entity.ValidateBetween(new DateTime(2024, 6, 15), from, to);
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void InTheFuture_ShouldAddNotification_WhenDateIsInThePast()
    {
        var entity = new DateTimeTestEntity();
        entity.ValidateInTheFuture(DateTime.Now.AddDays(-1));
        Assert.Contains(entity.Notifications, n => n.Message == "Date must be in the future");
    }

    [Fact]
    public void InTheFuture_ShouldNotAddNotification_WhenDateIsInTheFuture()
    {
        var entity = new DateTimeTestEntity();
        entity.ValidateInTheFuture(DateTime.Now.AddDays(1));
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void InThePast_ShouldAddNotification_WhenDateIsInTheFuture()
    {
        var entity = new DateTimeTestEntity();
        entity.ValidateInThePast(DateTime.Now.AddDays(1));
        Assert.Contains(entity.Notifications, n => n.Message == "Date must be in the past");
    }

    [Fact]
    public void InThePast_ShouldNotAddNotification_WhenDateIsInThePast()
    {
        var entity = new DateTimeTestEntity();
        entity.ValidateInThePast(DateTime.Now.AddDays(-1));
        Assert.Empty(entity.Notifications);
    }

    [Fact]
    public void Must_ShouldAddNotification_WhenPredicateFails()
    {
        var entity = new DateTimeTestEntity();
        // 2024-06-15 is a Saturday
        entity.ValidateMust(new DateTime(2024, 6, 15));
        Assert.Contains(entity.Notifications, n => n.Message == "Must be a Monday");
    }

    [Fact]
    public void Must_ShouldNotAddNotification_WhenPredicatePasses()
    {
        var entity = new DateTimeTestEntity();
        // 2024-06-17 is a Monday
        entity.ValidateMust(new DateTime(2024, 6, 17));
        Assert.Empty(entity.Notifications);
    }
}
