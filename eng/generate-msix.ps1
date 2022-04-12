param(
    [string]$pw,

    [string]$thumbprint
)

$password = ConvertTo-SecureString -String $pw -Force -AsPlainText
msbuild ..\src\Spacetime\Spacetime.csproj /restore /t:Publish /p:TargetFramework=net6.0-windows10.0.19041 /p:configuration=release /p:GenerateAppxPackageOnBuild=true /p:Platform=x64 /p:AppxPackageSigningEnabled=true /p:PackageCertificateThumbprint="${thumbprint}" /p:PackageCertificatePassword="${password}"