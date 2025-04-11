# 🧮 FInt - FixedInteger for C#

**FInt** (*FixedInteger*) is a lightweight and deterministic C# library for working with numbers using fixed-point arithmetic.  With its simple, portable design, FInt is completely dependency-free and seamlessly integrates into any project requiring reliable fixed-point math.

---

## 🚀 Features

- 📁 **Only one file** (`FInt.cs`) required for your projects!
- 🔨 **Works** with any .NET-compatible project!
- 🚫 **Dependency-free** with no external libraries required!
- ⚖️ **Fast & Immutable** struct-based design!
- 🧪 **Fully tested** with GitHub Actions CI!

---

## ✅ Build & Test Status

| Workflow | Status |
|----------|--------|
| **CI: Build & Test** | ![Test Status](https://img.shields.io/github/actions/workflow/status/AronDavis/FInt/main-github-action.yml?branch=master&&label=Tests)

Automated tests run on every push and pull request to `master`.

---

## 📦 Latest Release

Get the latest version of **`FInt.cs`** here:

- 🔗 [Download Latest `FInt.cs`](https://github.com/AronDavis/FInt/releases/latest/download/FInt.cs)

> ℹ️ Only the `FInt.cs` file is needed in the release. GitHub auto-generates `.zip` and `.tar.gz` files that can be ignored.

---

## 🛠️ Usage

### Installation

Simply copy [`FInt.cs`](https://github.com/AronDavis/FInt/releases/latest/download/FInt.cs) into your C# project directory and you're good to go!

```bash
curl -O https://github.com/AronDavis/FInt/releases/latest/download/FInt.cs
```

### Usage Example

```csharp
FInt ten = new FInt(10);
FInt five = new FInt(5);

FInt sum = ten + five;
Console.WriteLine(sum); // Output: 15
```
---

## 🧪 Running Tests

To run the tests locally:

```bash
dotnet test
```

Tests are automatically run in GitHub CI. Every push to `master` must pass tests before a release is created.

---

## 📚 Documentation

The library is fully self-documented within `FInt.cs` via XML comments.

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