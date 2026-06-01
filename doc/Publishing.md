# Publishing

## Setup

Update the version and release notes in the file [BitHelp.Core.Type.pt-BR.csproj]:

```xml
<Version>0.1.0</Version>
<PackageReleaseNotes>Describe the release notes here.</PackageReleaseNotes>
```

More details on how to configure the .NET CLI package are available [here].

## Generate package

From the project root, run:

```sh
dotnet pack src --configuration Release
```

After the package is created, publish it with:

```sh
dotnet nuget push nuget/BitHelp.Core.Type.pt-BR.[set version].nupkg -k [set your password] -s https://api.nuget.org/v3/index.json
```

[BitHelp.Core.Type.pt-BR.csproj]: <../src/BitHelp.Core.Type.pt-BR.csproj>
[here]: <https://docs.microsoft.com/pt-BR/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli>
