# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2024-XX-XX

### Added
- Fluent validation DSL for domain entities via `Notifiable<T>` base class
- String validator: `Required`, `MinLength`, `MaxLength`, `ExactLength`, `ExactTrimmedLength`, `Matches`, `EmailAddress`, `Cpf`, `Cnpj`, `Must`
- Numeric validators (int, long, decimal, double): `Required`, `EqualTo`, `NotEqualTo`, `GreaterThan`, `GreaterThanOrEqualTo`, `LessThan`, `LessThanOrEqualTo`, `Between`, `Must`
- Bool validator: `Required`, `IsTrue`, `IsFalse`, `Must`
- Guid validator: `Required`, `NotEmpty`, `Must`
- DateTime validator: `Required`, `After`, `Before`, `Between`, `InTheFuture`, `InThePast`, `Must`
- Enum validator: `Required`, `InEnum`, `GreaterThan`
- Object validator for nested `Notifiable<T>` objects: `Required`, `IsValid`, `Must`
- Collection validator for `IEnumerable<T>` of `Notifiable<T>`: `Required`, `AreValid`
- Internationalization support via `Messages.To` delegate
- Default Portuguese (pt-BR) messages
- Multi-target: net8.0 and net9.0
