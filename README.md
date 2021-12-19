# BitHelp.Core.Type.pt-BR

[![Licensed under the MIT License](https://img.shields.io/badge/License-MIT-blue.svg)](./LICENSE)
[![Integration Tests](https://github.com/RenatoPacheco/BitHelp.Core.Type.pt-BR/workflows/Integration%20Tests/badge.svg?branch=master)](https://github.com/RenatoPacheco/BitHelp.Core.Type.pt-BR/actions/workflows/integration-tests.yml)
[![BitHelp.Core.Type.pt-BR on fuget.org](https://www.fuget.org/packages/BitHelp.Core.Type.pt-BR/badge.svg)](https://www.fuget.org/packages/BitHelp.Core.Type.pt-BR)
[![NuGet](https://img.shields.io/nuget/v/BitHelp.Core.Type.pt-BR.svg)](https://nuget.org/packages/BitHelp.Core.Type.pt-BR)
[![Nuget](https://img.shields.io/nuget/dt/BitHelp.Core.Type.pt-BR.svg)](https://nuget.org/packages/BitHelp.Core.Type.pt-BR)

Project with a collection of data types usually used in Brazil.

# Getting Started

## Software dependencies

[.NET Standard 2.0](https://docs.microsoft.com/pt-BR/dotnet/standard/net-standard)

## Installation process

This package is available through Nuget Packages: https://www.nuget.org/packages/BitHelp.Core.Type.pt-BR

**Nuget**
```
Install-Package BitHelp.Core.Type.pt-BR
```

**.NET CLI**
```
dotnet add package BitHelp.Core.Type.pt-BR
```

## Latest releases

## Release 0.1.1

**Features:**

- Fixing namespace.
- Adding more build settings.

To read about others releases access [RELEASES.md](./RELEASES.md)

# Build and Test

Using Visual Studio Code, you can build and test the project from the terminal.

Build and restore the project:

```
dotnet restore
dotnet build --no-restore
```

Tests:

```
dotnet test --no-build --verbosity normal
```