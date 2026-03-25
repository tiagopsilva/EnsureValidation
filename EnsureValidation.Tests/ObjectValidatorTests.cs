namespace EnsureValidation.Tests;

public class ObjectValidatorTests
{
    private class Child : Notifiable<Child>
    {
        public string? Name { get; set; }
        protected override void OnValidate()
        {
            Ensure.That(Name, nameof(Name)).Required("Name is required");
        }
    }

    private class Parent : Notifiable<Parent>
    {
        public Child? ChildProp { get; set; }
        protected override void OnValidate()
        {
            Ensure.ThatObject(ChildProp, nameof(ChildProp))
                .Required("Child is required")
                .IsValid()
                .Must(c => c?.Name == "Valid", "Child name must be 'Valid'");
        }
    }

    [Fact]
    public void Required_ShouldAddNotification_WhenObjectIsNull()
    {
        var parent = new Parent { ChildProp = null };
        parent.Validate();
        Assert.Contains(parent.Notifications, n => n.Field == "ChildProp" && n.Message == "Child is required");
    }

    [Fact]
    public void IsValid_ShouldAggregateChildNotifications()
    {
        var child = new Child { Name = null };
        var parent = new Parent { ChildProp = child };
        parent.Validate();
        Assert.Contains(parent.Notifications, n => n.Field == "ChildProp.Name" && n.Message == "Name is required");
    }

    [Fact]
    public void Must_ShouldAddNotification_WhenPredicateIsFalse()
    {
        var child = new Child { Name = "Other" };
        var parent = new Parent { ChildProp = child };
        parent.Validate();
        Assert.Contains(parent.Notifications, n => n.Field == "ChildProp" && n.Message == "Child name must be 'Valid'");
    }

    [Fact]
    public void AllValidations_ShouldNotAddNotification_WhenAllAreValid()
    {
        var child = new Child { Name = "Valid" };
        var parent = new Parent { ChildProp = child };
        parent.Validate();
        Assert.True(parent.IsValid);
    }
}