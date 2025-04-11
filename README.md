# 🧮 FInt — FixedInteger for C#

**FInt** (*FixedInteger*) is a simple, single-file C# utility for working with integers in a fixed, immutable, and mathematically expressive way. It was designed to be lightweight, dependency-free, and easily portable across projects.

---

## 🚀 Features

- 📁 **Single file**: One file (`FInt.cs`) to drop into any C# project
- ⚖️ **Immutable** struct-based design
- 🔢 Easy to construct and compare fixed integers
- 🧪 Fully tested with GitHub Actions CI
- 🎯 Targets .NET 8.0+

---

## ✅ Build & Test Status

| Workflow | Status |
|----------|--------|
| **CI: Build & Test** | ![Test Status](https://github.com/AronDavis/FInt/actions/workflows/main-github-action.yml/badge.svg?branch=master) |

Automated tests run on every push and pull request to `master`.

---

## 📦 Latest Release

Get the latest version of **`FInt.cs`** directly from GitHub Releases:

- 🔗 [Download Latest `FInt.cs`](https://github.com/AronDavis/FInt/releases/latest/download/FInt.cs)

> ℹ️ The release contains only the `FInt.cs` file. GitHub auto-generates `.zip` and `.tar.gz` source downloads — those can be ignored.

---

## 🛠️ Usage

### Installation

Simply copy [`FInt.cs`](https://github.com/AronDavis/FInt/releases/latest/download/FInt.cs) into your C# project:

```bash
curl -O https://github.com/AronDavis/FInt/releases/latest/download/FInt.cs
```

### Example

```csharp
var ten = new FInt(10);
var five = new FInt(5);

var sum = ten + five;
Console.WriteLine(sum.Value); // Output: 15
```

FInt supports arithmetic, equality, and can be extended for custom numeric logic.

---

## 🧪 Running Tests

To run the tests locally:

```bash
dotnet test
```

Tests are automatically run in CI. All pushes to `master` must pass tests before a release is created.

---

## 📚 Documentation

The library is fully self-documented within `FInt.cs` via XML comments. For quick reference:

- `FInt(int value)` — create a new fixed integer
- `.Value` — retrieve the underlying value
- `+`, `-`, `==`, `!=` — basic operators supported
- `ToString()` — prints the integer value

---

## 🧰 Requirements

- .NET 8.0 SDK or higher
- Works with any .NET-compatible project (console, library, Unity, etc.)

---

## 💡 Motivation

`FInt` was created to explore a simplified, immutable numeric type with full test coverage and clean CI/CD automation, all contained in a single, drop-in file. Ideal for teaching, prototyping, or minimalistic development.

---

## 🤝 Contributing

Contributions are welcome! Fork the repo, make your changes, and open a pull request. All code should be covered by tests and conform to the current style.

---

## 📄 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---

## 🌟 Acknowledgements

Thanks to:
- [GitHub Actions](https://github.com/features/actions) for CI/CD
- [.NET](https://dotnet.microsoft.com/) for the excellent platform
- You, for checking this out!

---