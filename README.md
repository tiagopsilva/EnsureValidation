# EnsureValidation

[![NuGet](https://img.shields.io/nuget/v/EnsureValidation.svg)](https://www.nuget.org/packages/EnsureValidation)
[![CI](https://github.com/tiagosilva/EnsureValidation/actions/workflows/ci.yml/badge.svg)](https://github.com/tiagosilva/EnsureValidation/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A lightweight, **zero-reflection** fluent validation library for .NET, designed for DDD domain entities and value objects.

## Why EnsureValidation?

| Feature | EnsureValidation | FluentValidation |
|---|---|---|
| Reflection | **None** | Heavy (expression trees) |
| Allocation | Minimal | Higher (validators, rules) |
| Integration | Built into entities | External validator classes |
| Learning curve | Minimal | Moderate |
| DDD focus | **First-class** | General purpose |

EnsureValidation embeds validation directly into your domain entities using a fluent DSL -- no separate validator classes, no reflection, no magic.

## Quick Start

```bash
dotnet add package EnsureValidation
```

### 1. Define your entity

```csharp
using EnsureValidation;

public class Customer : Notifiable<Customer>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int? Age { get; set; }

    protected override void OnValidate()
    {
        Ensure.That(Name, nameof(Name))
            .Required()
            .MinLength(3)
            .MaxLength(100);

        Ensure.That(Email, nameof(Email))
            .Required()
            .EmailAddress();

        Ensure.That(Age, nameof(Age))
            .Required()
            .Between(18, 120);
    }
}
```

### 2. Validate

```csharp
var customer = new Customer { Name = "AB", Email = "invalid", Age = 15 };
customer.Validate();

if (customer.IsInvalid)
{
    foreach (var notification in customer.Notifications)
        Console.WriteLine($"{notification.Field}: {notification.Message}");
}
```

## Available Validators

### String
`Required()` `MinLength(n)` `MaxLength(n)` `ExactLength(n)` `ExactTrimmedLength(n)` `Matches(regex)` `EmailAddress()` `Must(predicate)`

### Numeric (int, long, decimal, double)
`Required()` `EqualTo(n)` `NotEqualTo(n)` `GreaterThan(n)` `GreaterThanOrEqualTo(n)` `LessThan(n)` `LessThanOrEqualTo(n)` `Between(min, max)` `Must(predicate)`

### Bool
`Required()` `IsTrue()` `IsFalse()` `Must(predicate)`

### Guid
`Required()` `NotEmpty()` `Must(predicate)`

### DateTime
`Required()` `After(date)` `Before(date)` `Between(from, to)` `InTheFuture()` `InThePast()` `Must(predicate)`

### Enum
`Required()` `InEnum()` `GreaterThan(value)`

### Object (nested validation)
`Required()` `IsValid()` `Must(predicate)`

### Collection
`Required()` `AreValid()`

## Internationalization (i18n)

Default messages are in English. Override them globally:

```csharp
Messages.To = (methodName, args) => methodName switch
{
    nameof(Messages.Required) => $"{args[0]} is required.",
    nameof(Messages.MinLength) => $"{args[0]} must be at least {args[1]} characters.",
    // ... other messages
    _ => $"{args[0]} is invalid."
};
```

For Portuguese (pt-BR) messages, install the companion package:

```bash
dotnet add package EnsureValidation.PtBR
```

## EnsureValidation.PtBR

The `EnsureValidation.PtBR` companion package provides:

- **Default messages in Portuguese (pt-BR)** — all built-in validation messages are automatically translated.
- **Brazilian document validators** — additional string validators for `Cpf()` and `Cnpj()`.

```bash
dotnet add package EnsureValidation.PtBR
```

```csharp
using EnsureValidation.PtBR;

// Apply pt-BR messages globally (call once at startup)
PtBRMessages.Configure();

// Cpf and Cnpj validators become available on string chains
Ensure.That(cpf, nameof(cpf)).Required().Cpf();
Ensure.That(cnpj, nameof(cnpj)).Required().Cnpj();
```

## Nested Object Validation

```csharp
public class Order : Notifiable<Order>
{
    public Customer? Customer { get; set; }
    public List<OrderItem>? Items { get; set; }

    protected override void OnValidate()
    {
        Ensure.ThatObject(Customer, nameof(Customer))
            .Required()
            .IsValid();

        Ensure.ThatCollection(Items, nameof(Items))
            .Required()
            .AreValid();
    }
}
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Write tests for your changes
4. Ensure all tests pass (`dotnet test`)
5. Submit a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

