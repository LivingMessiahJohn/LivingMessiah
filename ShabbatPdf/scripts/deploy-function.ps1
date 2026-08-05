# Publish ShabbatPdf.Functions to Azure (Flex Consumption).
#
# Monorepo layout: this script lives under LivingMessiah/ShabbatPdf/scripts/.
# "$repoRoot" is the ShabbatPdf product folder (parent of scripts/), not the
# LivingMessiah solution root.
#
# Prerequisites:
#   - az login (subscription that owns the Function app)
#   - Function app already exists (default: lmm-shabbat-pdf in LmmWebAppGroup)
#
# From ShabbatPdf (product root):
#   cd C:\Source\repos\LivingMessiah\ShabbatPdf
#   .\scripts\deploy-function.ps1
#   .\scripts\deploy-function.ps1 -AppName lmm-shabbat-pdf -ResourceGroup LmmWebAppGroup
#
# Staging (gitignored via ShabbatPdf/.gitignore out/):
#   out\func-publish\     publish folder
#   out\func-publish.zip  zip uploaded to Azure

param(
    [string] $AppName = "lmm-shabbat-pdf",
    [string] $ResourceGroup = "LmmWebAppGroup",
    [string] $ProjectPath = "src/Functions/ShabbatPdf.Functions.csproj",
    [string] $Configuration = "Release"
)

$ErrorActionPreference = "Stop"
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
Set-Location $repoRoot

# Resolve relative paths against ShabbatPdf root so invoke-from-anywhere works.
if (-not [System.IO.Path]::IsPathRooted($ProjectPath)) {
    $ProjectPath = Join-Path $repoRoot $ProjectPath
}
if (-not (Test-Path -LiteralPath $ProjectPath)) {
    throw "Functions project not found: $ProjectPath"
}

$publishDir = Join-Path $repoRoot "out\func-publish"
$zipPath = Join-Path $repoRoot "out\func-publish.zip"

Write-Host "ShabbatPdf root: $repoRoot" -ForegroundColor DarkGray
Write-Host "Publishing $ProjectPath ($Configuration) ..." -ForegroundColor Cyan
if (Test-Path $publishDir) {
    Remove-Item -Recurse -Force $publishDir
}
dotnet publish $ProjectPath -c $Configuration -o $publishDir
if ($LASTEXITCODE -ne 0) {
    throw "dotnet publish failed (exit $LASTEXITCODE)."
}

if (-not (Test-Path (Join-Path $publishDir ".azurefunctions"))) {
    throw "Publish output missing .azurefunctions - cannot deploy."
}

Write-Host "Creating zip with Linux-friendly paths ..." -ForegroundColor Cyan
if (Test-Path $zipPath) {
    Remove-Item -Force $zipPath
}

Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem

$fs = [System.IO.File]::Open($zipPath, [System.IO.FileMode]::Create)
$archive = New-Object System.IO.Compression.ZipArchive($fs, [System.IO.Compression.ZipArchiveMode]::Create)
try {
    foreach ($file in Get-ChildItem -Path $publishDir -Recurse -File -Force) {
        $rel = $file.FullName.Substring($publishDir.Length).TrimStart('\', '/')
        $entryName = $rel.Replace('\', '/')
        [void][System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile(
            $archive,
            $file.FullName,
            $entryName,
            [System.IO.Compression.CompressionLevel]::Optimal)
    }
}
finally {
    $archive.Dispose()
    $fs.Dispose()
}

Write-Host "Deploying to $AppName ($ResourceGroup) ..." -ForegroundColor Cyan
az functionapp deployment source config-zip `
    -g $ResourceGroup `
    -n $AppName `
    --src $zipPath
if ($LASTEXITCODE -ne 0) {
    throw "az functionapp deployment failed (exit $LASTEXITCODE)."
}

Write-Host "Verifying functions ..." -ForegroundColor Cyan
az functionapp function list -g $ResourceGroup -n $AppName -o table

Write-Host "Done. Portal: https://portal.azure.com/#resource/subscriptions/$(az account show --query id -o tsv)/resourceGroups/$ResourceGroup/providers/Microsoft.Web/sites/$AppName" -ForegroundColor Green
