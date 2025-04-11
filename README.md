# 🧮 FInt — FixedInteger for C#

**FInt** (*FixedInteger*) is a lightweight and deterministic C# library for working with numbers in a fixed-point arithmetic style.  With its simple, portable design, FInt is completely dependency-free and seamlessly integrates into any project requiring reliable fixed-point math.

---

## 🚀 Features

- 📁 **Single file**: One file (`FInt.cs`) to drop into any C# project!
- ⚖️ **Immutable** struct-based design
- 🔢 Easy to construct and compare fixed integers
- 🧪 Fully tested with GitHub Actions CI

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

Simply copy [`FInt.cs`](https://github.com/AronDavis/FInt/releases/latest/download/FInt.cs) into your C# project directory and you're good to go!

```bash
curl -O https://github.com/AronDavis/FInt/releases/latest/download/FInt.cs
```

### Usage Example

```csharp
var ten = new FInt(10);
var five = new FInt(5);

var sum = ten + five;
Console.WriteLine(sum.Value); // Output: 15
```
---

## 🧪 Running Tests

To run the tests locally:

```bash
dotnet test
```

Tests are automatically run in GitHub CI. All pushes to `master` must pass tests before a release is created.

---

## 📚 Documentation

The library is fully self-documented within `FInt.cs` via XML comments

---

## 🧰 Requirements

- Works with any .NET-compatible project (.NET, Unity, etc.)

---

## 🤝 Contributing

Contributions are welcome! Fork the repo, make your changes, and open a pull request. All code should be covered by tests and conform to the current coding style.

---

## 📄 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---

## 🌟 Acknowledgements

Thanks to:
- You, for checking this out!

---