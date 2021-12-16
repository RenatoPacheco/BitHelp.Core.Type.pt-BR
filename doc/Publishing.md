# Publishing

## Setting

Update the version parameters and release notes in the file [BitHelp.Core.Type.pt-BR.csproj]:

```xml
<Version>0.1.0</Version>
<PackageReleaseNotes>Describe the release notes here.</PackageReleaseNotes>
```

More details on how to configure the dotnet CLI package, [click here].

## Generating package

To generate the package, run the script below from the project root:

```sh
dotnet pack src --configuration Release
```

Once the package has been created, run the publish script as shown below:

```sh
dotnet nuget push nuget/BitHelp.Core.Type.pt-BR.[set version].nupkg -k [set your password] -s https://api.nuget.org/v3/index.json
```

[BitHelp.Core.Type.pt-BR.csproj]: <../src/BitHelp.Core.Type.pt-BR.csproj>
[click here]: <https://docs.microsoft.com/pt-BR/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli>
