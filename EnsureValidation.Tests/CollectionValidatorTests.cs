namespace EnsureValidation.Tests;

public class CollectionValidatorTests
{
    // Class representing the item in the list
    private class OrderItem : Notifiable<OrderItem>
    {
        public string Product { get; }
        public int Quantity { get; }

        public OrderItem(string product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Validate();
        }

        protected override void OnValidate()
        {
            Ensure.That(Product, nameof(Product)).Required("O nome do produto é obrigatório.");
            Ensure.That(Quantity, nameof(Quantity)).GreaterThan(0, "A quantidade deve ser maior que zero.");
        }
    }

    // Parent class that contains the list
    private class Order : Notifiable<Order>
    {
        public List<OrderItem>? Items { get; }

        public Order(List<OrderItem>? items)
        {
            Items = items;
            Validate();
        }

        protected override void OnValidate()
        {
            Ensure.ThatCollection(Items, nameof(Items)).Required().AreValid();
        }
    }

    [Fact]
    public void AreValid_ShouldAddIndexedNotifications_WhenItemsInCollectionAreInvalid()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new("Produto Válido", 10), // Item at position 0
            new("", 0)                 // Invalid item at position 1
        };
        var order = new Order(items);

        // Act
        // Validation already happened in the constructor

        // Assert
        Assert.True(order.IsInvalid);
        Assert.Equal(2, order.Notifications.Count);

        // Checks that errors were added with the correct indexed path
        Assert.Contains(order.Notifications, n => n.Field == "Items[1].Product" && n.Message == "O nome do produto é obrigatório.");
        Assert.Contains(order.Notifications, n => n.Field == "Items[1].Quantity" && n.Message == "A quantidade deve ser maior que zero.");
    }

    [Fact]
    public void AreValid_ShouldBeValid_WhenAllItemsInCollectionAreValid()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new("Produto A", 1),
            new("Produto B", 2)
        };
        var order = new Order(items);

        // Act
        // Validation in the constructor

        // Assert
        Assert.True(order.IsValid);
        Assert.Empty(order.Notifications);
    }

    [Fact]
    public void AreValid_ShouldHandleEmptyCollection_AsValid()
    {
        // Arrange
        var items = new List<OrderItem>();
        var order = new Order(items);

        // Act & Assert
        Assert.True(order.IsValid);
        Assert.Empty(order.Notifications);
    }

    [Fact]
    public void AreValid_ShouldAddNotification_WhenCollectionIsNull()
    {
        // Arrange
        List<OrderItem>? items = null;
        var order = new Order(items);

        // Act & Assert
        Assert.True(order.IsInvalid);
        Assert.Contains(order.Notifications, n => n.Field == "Items" && n.Message.Contains("required", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void AreValid_ShouldAddIndexedNotifications_ForMultipleInvalidItems()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new("", 0), // Invalid
            new("", 2), // Invalid (Product)
            new("Produto C", 0) // Invalid (Quantity)
        };
        var order = new Order(items);

        // Act & Assert
        Assert.True(order.IsInvalid);
        Assert.Equal(4, order.Notifications.Count);
        Assert.Contains(order.Notifications, n => n.Field == "Items[0].Product");
        Assert.Contains(order.Notifications, n => n.Field == "Items[0].Quantity");
        Assert.Contains(order.Notifications, n => n.Field == "Items[1].Product");
        Assert.Contains(order.Notifications, n => n.Field == "Items[2].Quantity");
    }
}