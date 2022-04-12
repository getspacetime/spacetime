param(
    [string]$pw,

    [string]$thumbprint
)

$password = ConvertTo-SecureString -String $pw -Force -AsPlainText

Export-PfxCertificate -cert "Cert:\CurrentUser\My\${thumbprint}" -FilePath certificate.pfx -Password $password