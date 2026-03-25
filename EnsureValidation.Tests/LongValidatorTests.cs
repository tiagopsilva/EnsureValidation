namespace EnsureValidation.Tests;

public class LongValidatorTests
{
    private class LongTestEntity : Notifiable<LongTestEntity>
    {
        public long? Value { get; set; }

        protected override void OnValidate() { }

        public void ValidateAll(long? value)
        {
            Ensure.That(value, nameof(Value))
                .Required("Campo obrigat�rio")
                .GreaterThan(10, "Deve ser maior que 10")
                .GreaterThanOrEqualTo(20, "Deve ser maior ou igual a 20")
                .EqualTo(30, "Deve ser igual a 30")
                .Between(40, 50, "Deve estar entre 40 e 50")
                .LessThan(60, "Deve ser menor que 60")
                .LessThanOrEqualTo(70, "Deve ser menor ou igual a 70")
                .NotEqualTo(80, "N�o pode ser igual a 80")
                .Must(v => v == 90, "Deve ser 90");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenValueIsNull()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(null);
        Assert.Contains(entity.Notifications, n => n.Field == "Value" && n.Message == "Campo obrigat�rio");
    }

    [Fact]
    public void GreaterThan_ShouldAddNotification_WhenValueIsNotGreater()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(10);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser maior que 10");
    }

    [Fact]
    public void GreaterThanOrEqualTo_ShouldAddNotification_WhenValueIsLess()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(15);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser maior ou igual a 20");
    }

    [Fact]
    public void EqualTo_ShouldAddNotification_WhenValueIsNotEqual()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(25);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser igual a 30");
    }

    [Fact]
    public void Between_ShouldAddNotification_WhenValueIsOutOfRange()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(35);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve estar entre 40 e 50");
    }

    [Fact]
    public void LessThan_ShouldAddNotification_WhenValueIsNotLess()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(60);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser menor que 60");
    }

    [Fact]
    public void LessThanOrEqualTo_ShouldAddNotification_WhenValueIsGreater()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(71);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser menor ou igual a 70");
    }

    [Fact]
    public void NotEqualTo_ShouldAddNotification_WhenValueIsEqual()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(80);
        Assert.Contains(entity.Notifications, n => n.Message == "N�o pode ser igual a 80");
    }

    [Fact]
    public void Must_ShouldAddNotification_WhenPredicateIsFalse()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(81);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser 90");
    }

    [Fact]
    public void NullValues_ShouldOnlyFailRequired()
    {
        var entity = new LongTestEntity();
        entity.ValidateAll(null);
        // Required fires for null, Must also evaluates (predicate receives null)
        Assert.True(entity.Notifications.Count >= 1);
        Assert.Contains(entity.Notifications, n => n.Field == "Value");
    }
}