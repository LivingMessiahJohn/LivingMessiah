# Package Linux Ghostscript, upload to Azure Files, mount on the Flex Function app,
# and set PdfCompress__GhostscriptPath.
#
# Prerequisites:
#   - az login (subscription with LmmWebAppGroup)
#   - Docker Desktop running (Linux engine)
#
# From repo root:
#   .\scripts\setup-ghostscript-mount.ps1
#   .\scripts\setup-ghostscript-mount.ps1 -WhatIf

[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [string] $AppName = "lmm-shabbat-pdf",
    [string] $ResourceGroup = "LmmWebAppGroup",
    [string] $StorageAccount = "livingmessiahstorage",
    [string] $ShareName = "function-tools",
    [string] $MountName = "tools",
    [string] $MountPath = "/mounts/tools",
    # Path the Function app will execute (wrapper script on the mount)
    [string] $GhostscriptRelativePath = "ghostscript/bin/gs",
    [string] $DebianImage = "debian:bookworm-slim"
)

$ErrorActionPreference = "Stop"
$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$exportDir = Join-Path $repoRoot "out\gs-linux-export"
$packageScript = Join-Path $PSScriptRoot "package-ghostscript-linux.sh"
$gsRemotePrefix = "ghostscript"

function Assert-Az {
    az account show -o none 2>$null
    if ($LASTEXITCODE -ne 0) {
        throw "Not logged in to Azure CLI. Run: az login"
    }
}

function Assert-Docker {
    # docker writes warnings to stderr; do not treat as terminating errors
    $prev = $ErrorActionPreference
    $ErrorActionPreference = "Continue"
    try {
        docker info 1>$null 2>$null
        if ($LASTEXITCODE -ne 0) {
            throw "Docker is not running. Start Docker Desktop (Linux engine), then re-run this script."
        }
    }
    finally {
        $ErrorActionPreference = $prev
    }
}

function Get-StorageKey {
    param([string] $Account, [string] $Group)
    $key = az storage account keys list `
        --account-name $Account `
        --resource-group $Group `
        --query "[0].value" -o tsv
    if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($key)) {
        throw "Failed to read storage account key for $Account"
    }
    return $key.Trim()
}

Write-Host "=== Ghostscript mount setup for $AppName ===" -ForegroundColor Cyan
Assert-Az
Assert-Docker

if (-not (Test-Path $packageScript)) {
    throw "Missing package script: $packageScript"
}

# --- 1. Package Ghostscript (Linux) via Docker ---
Write-Host "Packaging Ghostscript from $DebianImage ..." -ForegroundColor Cyan
if (Test-Path $exportDir) {
    Remove-Item -Recurse -Force $exportDir
}
New-Item -ItemType Directory -Force -Path $exportDir | Out-Null

# Docker needs a Unix-style path for the bind mount on Windows
$exportDocker = ($exportDir -replace '\\', '/')
$scriptDocker = ($packageScript -replace '\\', '/')

$prevEa = $ErrorActionPreference
$ErrorActionPreference = "Continue"
try {
    docker run --rm `
        -v "${exportDocker}:/export" `
        -v "${scriptDocker}:/package.sh:ro" `
        $DebianImage `
        bash /package.sh
    $dockerExit = $LASTEXITCODE
}
finally {
    $ErrorActionPreference = $prevEa
}

if ($dockerExit -ne 0) {
    throw "Docker packaging failed (exit $dockerExit)."
}

$gsBin = Join-Path $exportDir "bin\gs.bin"
$gsWrap = Join-Path $exportDir "bin\gs"
if (-not (Test-Path $gsBin) -or -not (Test-Path $gsWrap)) {
    throw "Packaging incomplete: expected bin/gs and bin/gs.bin under $exportDir"
}

$fileCount = (Get-ChildItem $exportDir -Recurse -File).Count
Write-Host "Packaged $fileCount files under $exportDir" -ForegroundColor Green

# --- 2. Ensure Azure Files share ---
Write-Host "Ensuring Azure Files share '$ShareName' on $StorageAccount ..." -ForegroundColor Cyan
$key = Get-StorageKey -Account $StorageAccount -Group $ResourceGroup

$exists = az storage share exists `
    --name $ShareName `
    --account-name $StorageAccount `
    --account-key $key `
    --query exists -o tsv
if ($exists -ne "true") {
    if ($PSCmdlet.ShouldProcess("$StorageAccount/$ShareName", "Create file share")) {
        az storage share create `
            --name $ShareName `
            --account-name $StorageAccount `
            --account-key $key `
            --quota 5 `
            -o none
        if ($LASTEXITCODE -ne 0) {
            throw "Failed to create file share $ShareName"
        }
    }
}
else {
    Write-Host "Share already exists." -ForegroundColor DarkGray
}

# --- 3. Upload package to share (ghostscript/...) ---
Write-Host "Uploading package to $ShareName/$gsRemotePrefix/ ..." -ForegroundColor Cyan
if ($PSCmdlet.ShouldProcess("$ShareName/$gsRemotePrefix", "Upload Ghostscript tree")) {
    $prevEa = $ErrorActionPreference
    $ErrorActionPreference = "Continue"
    try {
        # Clear previous tree (best-effort). Flag name varies by az version.
        az storage directory delete `
            --share-name $ShareName `
            --name $gsRemotePrefix `
            --account-name $StorageAccount `
            --account-key $key `
            --recursive `
            -o none 2>$null

        # upload-batch: destination is the share URL; destination-path is the folder
        az storage file upload-batch `
            --account-name $StorageAccount `
            --account-key $key `
            --destination "https://$StorageAccount.file.core.windows.net/$ShareName" `
            --destination-path $gsRemotePrefix `
            --source $exportDir `
            --no-progress `
            -o none
        $uploadExit = $LASTEXITCODE
    }
    finally {
        $ErrorActionPreference = $prevEa
    }

    if ($uploadExit -ne 0) {
        throw "Upload to Azure Files failed (exit $uploadExit)."
    }
}

# --- 4. Mount share on Function app ---
$gsFullPath = "$MountPath/$GhostscriptRelativePath".Replace('//', '/')
Write-Host "Configuring OS mount $MountName -> $MountPath ..." -ForegroundColor Cyan

$existing = az webapp config storage-account list `
    -g $ResourceGroup `
    -n $AppName `
    -o json 2>$null | ConvertFrom-Json

$already = $false
if ($existing) {
    $already = $null -ne ($existing | Where-Object { $_.name -eq $MountName -or $_.customId -eq $MountName })
}

if ($already) {
    if ($PSCmdlet.ShouldProcess($AppName, "Update storage mount $MountName")) {
        az webapp config storage-account update `
            -g $ResourceGroup `
            -n $AppName `
            --custom-id $MountName `
            --storage-type AzureFiles `
            --share-name $ShareName `
            --account-name $StorageAccount `
            --access-key $key `
            --mount-path $MountPath `
            -o none
        if ($LASTEXITCODE -ne 0) {
            throw "Failed to update storage mount"
        }
    }
}
else {
    if ($PSCmdlet.ShouldProcess($AppName, "Add storage mount $MountName")) {
        az webapp config storage-account add `
            -g $ResourceGroup `
            -n $AppName `
            --custom-id $MountName `
            --storage-type AzureFiles `
            --share-name $ShareName `
            --account-name $StorageAccount `
            --access-key $key `
            --mount-path $MountPath `
            -o none
        if ($LASTEXITCODE -ne 0) {
            throw "Failed to add storage mount"
        }
    }
}

# --- 5. App settings for PdfCompress ---
Write-Host "Setting PdfCompress__GhostscriptPath=$gsFullPath ..." -ForegroundColor Cyan
if ($PSCmdlet.ShouldProcess($AppName, "Set PdfCompress app settings")) {
    az functionapp config appsettings set `
        -g $ResourceGroup `
        -n $AppName `
        --settings `
            "PdfCompress__Enabled=true" `
            "PdfCompress__MaxBytes=68157440" `
            "PdfCompress__PdfSettings=/ebook" `
            "PdfCompress__TimeoutSeconds=600" `
            "PdfCompress__GhostscriptPath=$gsFullPath" `
        -o none
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to set app settings"
    }
}

Write-Host ""
Write-Host "Done." -ForegroundColor Green
Write-Host "  Share:     $StorageAccount / $ShareName"
Write-Host "  Mount:     $MountPath  (id: $MountName)"
Write-Host "  GS path:   $gsFullPath"
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "  1. Deploy the Function code that includes PdfCompress (if not already):"
Write-Host "       .\scripts\deploy-function.ps1"
Write-Host "  2. Restart the app so the mount is picked up:"
Write-Host "       az functionapp restart -g $ResourceGroup -n $AppName"
Write-Host "  3. Upload a large agenda PDF to shabbat-service and check logs for:"
Write-Host "       Shrink {Name}: compressed=True original=... final=..."
Write-Host ""
Write-Host "Note: Azure Files SMB mounts use the storage account key (not managed identity yet)."
Write-Host "If you rotate the storage key, re-run this script or update the mount access key."
