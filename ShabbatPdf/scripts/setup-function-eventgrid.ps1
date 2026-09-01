# Wire Event Grid blob events -> Function EventGridTriggers.
# Flex Consumption does not poll containers; without these subscriptions the functions never run.
#
# Two subscriptions:
#   shabbat-service-staging → CompressStagingPdf
#   shabbat-service         → ProcessShabbatPdf (teaching extract)
#
# Prerequisites: az login, function already deployed.
#
# From ShabbatPdf product root:
#   .\scripts\setup-function-eventgrid.ps1

param(
    [string] $AppName = "lmm-shabbat-pdf",
    [string] $ResourceGroup = "LmmWebAppGroup",
    [string] $StorageAccount = "livingmessiahstorage",
    [string] $StagingContainer = "shabbat-service-staging",
    [string] $ServiceContainer = "shabbat-service",
    [string] $CompressFunction = "CompressStagingPdf",
    [string] $ExtractFunction = "ProcessShabbatPdf"
)

$ErrorActionPreference = "Stop"

Write-Host "Ensuring Microsoft.EventGrid provider is registered ..." -ForegroundColor Cyan
az provider register --namespace Microsoft.EventGrid --wait 2>$null

Write-Host "Getting eventgrid_extension system key ..." -ForegroundColor Cyan
$keysJson = az functionapp keys list -g $ResourceGroup -n $AppName -o json | ConvertFrom-Json
$egKey = $keysJson.systemKeys.eventgrid_extension
if ([string]::IsNullOrWhiteSpace($egKey)) {
    $egKey = $keysJson.systemKeys.PSObject.Properties |
        Where-Object { $_.Name -match 'eventgrid' } |
        Select-Object -First 1 -ExpandProperty Value
}
if ([string]::IsNullOrWhiteSpace($egKey)) {
    throw "Could not find system key 'eventgrid_extension'. Redeploy the Function app and retry."
}

$storageId = az storage account show -n $StorageAccount -g $ResourceGroup --query id -o tsv

Write-Host "Warming function app ..." -ForegroundColor Cyan
try {
    Invoke-WebRequest -Uri "https://$AppName.azurewebsites.net" -UseBasicParsing -TimeoutSec 120 | Out-Null
} catch {
    Write-Host "Warm request: $($_.Exception.Message)" -ForegroundColor Yellow
}

function New-BlobCreatedSubscription {
    param(
        [string] $ContainerName,
        [string] $FunctionName,
        [string] $SubscriptionName
    )

    $endpoint = "https://$AppName.azurewebsites.net/runtime/webhooks/eventgrid?functionName=$FunctionName&code=$egKey"
    $subjectBegins = "/blobServices/default/containers/$ContainerName/blobs/"

    Write-Host "Creating/updating Event Grid subscription '$SubscriptionName' ($ContainerName → $FunctionName) ..." -ForegroundColor Cyan

    $existing = az eventgrid event-subscription show `
        --source-resource-id $storageId `
        --name $SubscriptionName `
        -o json 2>$null
    if ($LASTEXITCODE -eq 0 -and $existing) {
        Write-Host "Removing existing subscription ..." -ForegroundColor Yellow
        az eventgrid event-subscription delete `
            --source-resource-id $storageId `
            --name $SubscriptionName `
            2>$null
    }

    az eventgrid event-subscription create `
        --name $SubscriptionName `
        --source-resource-id $storageId `
        --endpoint $endpoint `
        --endpoint-type webhook `
        --included-event-types Microsoft.Storage.BlobCreated `
        --subject-begins-with $subjectBegins `
        --subject-ends-with ".pdf" `
        --max-delivery-attempts 10 `
        --event-delivery-schema EventGridSchema `
        -o table

    if ($LASTEXITCODE -ne 0) {
        Write-Host ""
        Write-Host "CLI create failed. Create the subscription in Portal instead:" -ForegroundColor Yellow
        Write-Host "  1. Portal -> storage account livingmessiahstorage -> Events -> + Event Subscription"
        Write-Host "  2. Event Types: Blob Created"
        Write-Host "  3. Endpoint Type: Web Hook"
        Write-Host "  4. Function: $FunctionName"
        Write-Host "  5. Filters: Subject begins with $subjectBegins , ends with .pdf"
        throw "Event Grid subscription create failed for $SubscriptionName."
    }
}

New-BlobCreatedSubscription `
    -ContainerName $StagingContainer `
    -FunctionName $CompressFunction `
    -SubscriptionName "lmm-shabbat-pdf-staging-created"

New-BlobCreatedSubscription `
    -ContainerName $ServiceContainer `
    -FunctionName $ExtractFunction `
    -SubscriptionName "lmm-shabbat-pdf-blob-created"

Write-Host "Done. Uploads to $StagingContainer → $CompressFunction → $ServiceContainer → $ExtractFunction." -ForegroundColor Green
Write-Host "Tip: upload a test PDF to $StagingContainer to fire the chain." -ForegroundColor Yellow
