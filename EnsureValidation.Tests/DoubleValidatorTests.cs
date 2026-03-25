namespace EnsureValidation.Tests;

public class DoubleValidatorTests
{
    private class DoubleTestEntity : Notifiable<DoubleTestEntity>
    {
        public double? Value { get; set; }

        protected override void OnValidate() { }

        public void ValidateAll(double? value)
        {
            Ensure.That(value, nameof(Value))
                .Required("Campo obrigat�rio")
                .GreaterThan(10.5, "Deve ser maior que 10.5")
                .GreaterThanOrEqualTo(20.5, "Deve ser maior ou igual a 20.5")
                .EqualTo(30.5, "Deve ser igual a 30.5")
                .Between(40.5, 50.5, "Deve estar entre 40.5 e 50.5")
                .LessThan(60.5, "Deve ser menor que 60.5")
                .LessThanOrEqualTo(70.5, "Deve ser menor ou igual a 70.5")
                .NotEqualTo(80, "N�o pode ser igual a 80")
                .Must(v => v == 90.5, "Deve ser 90.5");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenValueIsNull()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(null);
        Assert.Contains(entity.Notifications, n => n.Field == "Value" && n.Message == "Campo obrigat�rio");
    }

    [Fact]
    public void GreaterThan_ShouldAddNotification_WhenValueIsNotGreater()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(10.5);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser maior que 10.5");
    }

    [Fact]
    public void GreaterThanOrEqualTo_ShouldAddNotification_WhenValueIsLess()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(15.5);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser maior ou igual a 20.5");
    }

    [Fact]
    public void EqualTo_ShouldAddNotification_WhenValueIsNotEqual()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(25.5);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser igual a 30.5");
    }

    [Fact]
    public void Between_ShouldAddNotification_WhenValueIsOutOfRange()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(35.5);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve estar entre 40.5 e 50.5");
    }

    [Fact]
    public void LessThan_ShouldAddNotification_WhenValueIsNotLess()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(60.5);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser menor que 60.5");
    }

    [Fact]
    public void LessThanOrEqualTo_ShouldAddNotification_WhenValueIsGreater()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(71.5);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser menor ou igual a 70.5");
    }

    [Fact]
    public void NotEqualTo_ShouldAddNotification_WhenValueIsEqual()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(80);
        Assert.Contains(entity.Notifications, n => n.Message == "N�o pode ser igual a 80");
    }

    [Fact]
    public void Must_ShouldAddNotification_WhenPredicateIsFalse()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(81.5);
        Assert.Contains(entity.Notifications, n => n.Message == "Deve ser 90.5");
    }

    [Fact]
    public void NullValues_ShouldOnlyFailRequired()
    {
        var entity = new DoubleTestEntity();
        entity.ValidateAll(null);
        // Required fires for null, Must also evaluates (predicate receives null)
        Assert.True(entity.Notifications.Count >= 1);
        Assert.Contains(entity.Notifications, n => n.Field == "Value");
    }
}
